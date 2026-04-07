// AppBotEyeClaudeFallback.cs — Gemini auto-fallback when Claude hits rate limits or server errors.
//
// Flow:
//   Eye tick (1s) → CheckClaudeSessionsForErrors()
//     → detects error patterns in each card's JSONL last entries
//     → TryLaunchGeminiHandoff(): writes session file, injects JSONL notification, spawns agent gemini
//
// Agent loop (RunGeminiLoopAsync) → TryInjectGeminiResponseToSession()
//     → appends each Gemini step response back into Claude session JSONL
//     → Claude Code renders new entries on next scroll/view

using System.Text;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── Per-CWD dedup: don't re-fire for the same error within 10 minutes ──
    static readonly Dictionary<string, DateTime> _geminiHandoffFiredAt = new(StringComparer.OrdinalIgnoreCase);
    static readonly TimeSpan GeminiFallbackCooldown = TimeSpan.FromMinutes(10);

    /// <summary>
    /// Called every tick: scan all active card CWDs for Claude session error patterns.
    /// Fires Gemini handoff on first detection per CWD (rate-limited by cooldown).
    /// </summary>
    static void CheckClaudeSessionsForErrors()
    {
        var cards = _cachedCards;
        if (cards == null || cards.Count == 0) return;

        var now = DateTime.UtcNow;
        foreach (var card in cards)
        {
            var cwd = card.Cwd;
            if (string.IsNullOrEmpty(cwd)) continue;

            // Cooldown check — don't re-fire within 10 minutes per CWD
            if (_geminiHandoffFiredAt.TryGetValue(cwd, out var lastFired)
                && (now - lastFired) < GeminiFallbackCooldown)
                continue;

            var (_, _, jsonlPath, _) = GetContextInfoForCwdEx(cwd);
            if (string.IsNullOrEmpty(jsonlPath) || !File.Exists(jsonlPath)) continue;

            var errorText = DetectClaudeSessionError(jsonlPath);
            if (errorText == null) continue;

            // Mark fired before async work to prevent double-fire
            _geminiHandoffFiredAt[cwd] = now;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[FALLBACK] Claude error in {AbbreviateCwd(cwd)}: {errorText}");
            Console.ResetColor();

            var cwdCapture = cwd;
            var jsonlCapture = jsonlPath;
            Task.Run(() => TryLaunchGeminiHandoff(cwdCapture, jsonlCapture, errorText));
        }
    }

    // Minimum silence before treating "no response" as a fallback trigger.
    // Compaction typically takes 30-120s; we wait 5 minutes to avoid false positives.
    static readonly TimeSpan NoResponseFallbackThreshold = TimeSpan.FromMinutes(5);
    // Packet bomb: assistant streaming without stop_reason for >30 minutes.
    static readonly TimeSpan StreamingTooLongThreshold = TimeSpan.FromMinutes(30);

    /// <summary>
    /// Scan the last ~20 lines of a Claude session JSONL for known error patterns,
    /// AND detect unanswered user prompts (covers compaction / hang / silent rate-limit),
    /// AND detect runaway streaming with no stop_reason for 30+ minutes (packet bomb).
    /// Returns the error description string, or null if no error detected.
    /// </summary>
    static string? DetectClaudeSessionError(string jsonlPath)
    {
        try
        {
            var lines = ReadLastLines(jsonlPath, 20);

            // ── 1. Error text in assistant turns ──
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var node = JsonNode.Parse(line);
                if (node == null) continue;

                if (node["type"]?.GetValue<string>() != "assistant") continue;
                var content = node["message"]?["content"]?.AsArray();
                if (content == null) continue;

                foreach (var item in content)
                {
                    var text = item?["text"]?.GetValue<string>() ?? "";
                    if (IsClaudeErrorText(text, out var desc)) return desc;
                }
            }

            // ── 2 & 3. Walk last entries in reverse; collect last user/assistant state ──
            DateTime? lastUserTs = null;
            bool lastAssistantComplete = true;  // assume complete until proven otherwise
            bool foundAssistant = false;

            for (int i = lines.Count - 1; i >= 0; i--)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;
                var node = JsonNode.Parse(line);
                if (node == null) continue;

                var type = node["type"]?.GetValue<string>();

                if (type == "assistant" && !foundAssistant)
                {
                    foundAssistant = true;
                    var stopReason = node["message"]?["stop_reason"]?.GetValue<string>();
                    lastAssistantComplete = !string.IsNullOrEmpty(stopReason);
                    continue;
                }

                if (type == "user" && lastUserTs == null)
                {
                    var tsRaw = node["timestamp"]?.GetValue<string>();
                    if (tsRaw != null && DateTime.TryParse(tsRaw, null,
                            System.Globalization.DateTimeStyles.RoundtripKind, out var ts))
                        lastUserTs = ts.ToUniversalTime();
                    break;
                }
            }

            if (lastUserTs.HasValue)
            {
                var age = DateTime.UtcNow - lastUserTs.Value;

                // ── 2. Unanswered user prompt (no assistant entry at all, or user is last) ──
                if (!foundAssistant && age >= NoResponseFallbackThreshold)
                    return $"no response for {(int)age.TotalMinutes}m (compacting/hang?)";

                // ── 3. Packet bomb: assistant running with no stop_reason for 30+ min ──
                if (foundAssistant && !lastAssistantComplete && age >= StreamingTooLongThreshold)
                    return $"streaming {(int)age.TotalMinutes}m with no stop_reason (packet bomb?)";
            }
        }
        catch { }
        return null;
    }

    static bool IsClaudeErrorText(string text, out string description)
    {
        description = "";
        if (string.IsNullOrEmpty(text)) return false;

        if (text.Contains("rate limit", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("한도를 초과", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("usage limit", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("you've hit", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("you have hit", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("limit reached", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("too many requests", StringComparison.OrdinalIgnoreCase))
        { description = "rate limit"; return true; }

        if (text.Contains("overloaded", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("server error", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("API Error", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("internal error", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("서버 오류", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("일시적 오류", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("529", StringComparison.Ordinal) ||
            text.Contains("503", StringComparison.Ordinal))
        { description = "server error"; return true; }

        return false;
    }

    /// <summary>Read the last N lines from a file without loading it entirely.</summary>
    static List<string> ReadLastLines(string path, int count)
    {
        var result = new List<string>(count);
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var sr = new StreamReader(fs, new UTF8Encoding(false));
        var buffer = new Queue<string>(count);
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            if (buffer.Count >= count) buffer.Dequeue();
            buffer.Enqueue(line);
        }
        result.AddRange(buffer);
        return result;
    }

    /// <summary>
    /// Read the last user-typed prompt from a Claude session JSONL.
    /// Skips VS Code system injections (ide_opened_file etc.).
    /// </summary>
    static string? GetLastUserPromptFromJsonl(string jsonlPath)
    {
        try
        {
            using var fs = new FileStream(jsonlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(fs, new UTF8Encoding(false));
            string? lastUserText = null;
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var node = JsonNode.Parse(line);
                if (node?["type"]?.GetValue<string>() != "user") continue;
                var content = node["message"]?["content"]?.AsArray();
                if (content == null) continue;

                var parts = new StringBuilder();
                foreach (var item in content)
                {
                    if (item?["type"]?.GetValue<string>() != "text") continue;
                    var text = item["text"]?.GetValue<string>() ?? "";
                    if (text.StartsWith("<ide_") || text.StartsWith("<system-reminder")) continue;
                    if (text.Length > 0) parts.AppendLine(text.Trim());
                }
                var candidate = parts.ToString().Trim();
                if (candidate.Length > 0) lastUserText = candidate;
            }
            if (lastUserText != null && lastUserText.Length > 2000)
                lastUserText = lastUserText[..2000] + "\n...(truncated)";
            return lastUserText;
        }
        catch { return null; }
    }

    /// <summary>
    /// Claude error → Gemini handoff:
    ///   1. Write session context file to wkappbot.hq/sessions/
    ///   2. Inject notification into Claude JSONL (visible in chat)
    ///   3. Spawn `wkappbot agent gemini` with last user prompt + session file
    /// </summary>
    static void TryLaunchGeminiHandoff(string cwd, string jsonlPath, string errorReason)
    {
        try
        {
            var cwdTag = AbbreviateCwd(cwd);
            if (string.IsNullOrEmpty(cwdTag)) cwdTag = "session";

            // ── 1. Write session context file ──
            var sessionDir = Path.Combine(DataDir, "sessions");
            Directory.CreateDirectory(sessionDir);
            var sessionFile = Path.Combine(sessionDir, $"{cwdTag}-{DateTime.Now:yyyyMMdd-HHmmss}.md");

            var sb = new StringBuilder();
            sb.AppendLine($"# Gemini Handoff Session — {DateTime.Now:yyyy-MM-dd HH:mm}");
            sb.AppendLine($"**Workspace**: {cwd}");
            sb.AppendLine($"**Reason**: Claude {errorReason}");
            sb.AppendLine();
            sb.AppendLine("## Context");
            sb.AppendLine($"Claude Code encountered: {errorReason}");
            sb.AppendLine("You (Gemini) are taking over as the active agent for this workspace.");
            sb.AppendLine("Use `wkappbot file glob/grep/read` to explore, `wkappbot file edit` to change code.");
            sb.AppendLine("Use `wkappbot slack send` for user communication.");
            sb.AppendLine();

            var thought = ReadClotThoughtForCwd(cwd, null);
            if (!string.IsNullOrWhiteSpace(thought))
            {
                sb.AppendLine("## Last Claude Activity");
                foreach (var tl in thought.Split('\n'))
                    if (!string.IsNullOrWhiteSpace(tl)) sb.AppendLine(tl.Trim());
                sb.AppendLine();
            }

            var latestTick = _cachedLatestTick;
            if (latestTick != null)
            {
                var (progress, done, next, block) = BuildKroStatus3(latestTick);
                sb.AppendLine("## KRO Status");
                if (!string.IsNullOrWhiteSpace(progress)) sb.AppendLine($"- 진행: {progress}");
                if (!string.IsNullOrWhiteSpace(done))     sb.AppendLine($"- 완료: {done}");
                if (!string.IsNullOrWhiteSpace(next))     sb.AppendLine($"- 예정: {next}");
                if (!string.IsNullOrWhiteSpace(block))    sb.AppendLine($"- 이슈: {block}");
                sb.AppendLine();
            }

            sb.AppendLine("## Instructions");
            sb.AppendLine("1. Review session context and CLAUDE.md / MEMORY.md");
            sb.AppendLine("2. Check `wkappbot eye tick` for current status");
            sb.AppendLine("3. Continue where Claude left off");

            File.WriteAllText(sessionFile, sb.ToString(), new UTF8Encoding(false));
            Console.Error.WriteLine($"[FALLBACK] Session file: {sessionFile}");

            // ── 2. Create live MD + open in VS Code (replaces JSONL inject) ──
            var agentId = $"appbot-{cwdTag}";

            // ── 3. Spawn Gemini agent with last user prompt ──
            var lastPrompt = GetLastUserPromptFromJsonl(jsonlPath);
            var question = !string.IsNullOrEmpty(lastPrompt)
                ? lastPrompt
                : $"Claude {errorReason}. Continue from session file as active agent for: {cwd}";

            // Create live MD for real-time output in VS Code
            var liveMdPath = CreateFallbackLiveMd(cwd, errorReason, question);
            var liveMdArg = !string.IsNullOrEmpty(liveMdPath) ? $" --live-md \"{EscapeCmdArg(liveMdPath)}\"" : "";

            var fallbackModel = GetActiveFallbackModel(); // gemini | gpt | claude
            var corePath = Environment.ProcessPath ?? "wkappbot";
            var args = $"agent {fallbackModel} \"{EscapeCmdArg(question)}\" \"{sessionFile}\" --agent-id {agentId}{liveMdArg}";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Error.WriteLine($"[FALLBACK] Launching {fallbackModel} agent (id={agentId}, reason={errorReason})...");
            Console.ResetColor();

            AppBotPipe.Spawn(corePath, args, cwd, caller: "EYE-GEMINI-FALLBACK");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[FALLBACK] TryLaunchGeminiHandoff failed: {ex.Message}");
        }
    }

    // ── Live MD file for real-time Gemini fallback output ──
    static readonly AsyncLocal<string?> _agentLiveMdPath = new();

    /// <summary>
    /// Create the live MD file at handoff time. Opens it in VS Code.
    /// Returns the path, also stored in _agentLiveMdPath for step appends.
    /// </summary>
    static string? CreateFallbackLiveMd(string cwd, string errorReason, string question)
    {
        try
        {
            var askDir = Path.Combine(cwd, ".wkappbot", "ask");
            Directory.CreateDirectory(askDir);
            var ts = DateTime.Now.ToString("yyyyMMdd-HHmm");
            var slug = BuildSlug(question, 30);
            var mdPath = Path.Combine(askDir, $"{ts}-gemini-fallback-{slug}.md");

            var sb = new StringBuilder();
            sb.AppendLine($"# Gemini Fallback — {DateTime.Now:yyyy-MM-dd HH:mm}");
            sb.AppendLine();
            sb.AppendLine($"> **Reason**: Claude {errorReason}");
            sb.AppendLine($"> **Question**: {question}");
            sb.AppendLine();
            sb.AppendLine("---");
            sb.AppendLine();
            sb.AppendLine("_Waiting for Gemini response..._");
            sb.AppendLine();
            File.WriteAllText(mdPath, sb.ToString(), new UTF8Encoding(false));

            // Open in VS Code + trigger markdown preview (Ctrl+Shift+V)
            try
            {
                AppBotPipe.StartTracked(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "code", Arguments = $"\"{mdPath}\"",
                    UseShellExecute = false, CreateNoWindow = true
                }, cwd, "FALLBACK-MD");
                // Wait for VS Code to open the file, then trigger markdown preview
                Task.Run(async () =>
                {
                    await Task.Delay(1500); // wait for file to open in editor
                    try
                    {
                        var corePath = Environment.ProcessPath ?? "wkappbot";
                        AppBotPipe.Spawn(corePath,
                            "a11y type \"*Visual Studio Code*\" \"\" --hotkey Ctrl+Shift+V",
                            cwd, caller: "FALLBACK-PREVIEW");
                    }
                    catch { }
                });
            } catch { }

            _agentLiveMdPath.Value = mdPath;
            return mdPath;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[FALLBACK] CreateFallbackLiveMd failed: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Append Gemini step response to live MD + scroll VS Code to bottom.
    /// Replaces the old JSONL inject approach (which didn't render in real-time).
    /// </summary>
    static void TryInjectGeminiResponseToSession(int step, string answer)
    {
        var mdPath = _agentLiveMdPath.Value;
        if (string.IsNullOrEmpty(mdPath)) return;

        Task.Run(() =>
        {
            try
            {
                var text = answer.Length > 8000 ? answer[..8000] + "\n\n...(truncated)" : answer;
                var section = new StringBuilder();
                section.AppendLine($"## Step {step} — {DateTime.Now:HH:mm:ss}");
                section.AppendLine();
                section.AppendLine(text);
                section.AppendLine();

                File.AppendAllText(mdPath, section.ToString(), new UTF8Encoding(false));

                // Force VS Code to scroll to bottom via file open with last line number
                var lineCount = File.ReadAllLines(mdPath).Length;
                try { AppBotPipe.StartTracked(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "code", Arguments = $"--goto \"{mdPath}:{lineCount}\"",
                    UseShellExecute = false, CreateNoWindow = true
                }, Environment.CurrentDirectory, "FALLBACK-SCROLL"); } catch { }
            }
            catch { }
        });
    }
}
