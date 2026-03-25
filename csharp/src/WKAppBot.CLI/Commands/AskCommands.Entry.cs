using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.UIA3;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using NativeMethods = WKAppBot.Win32.Native.NativeMethods;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── Focus theft stack capture (set by OnFocusTheft callback before LogError) ──
    [ThreadStatic] static string? _lastFocusTheftStack;

    // ── Common error logging ──
    static void LogError(string tag, Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[{tag}] Error: {ex.Message}");
        Console.ResetColor();
        if (ex.StackTrace != null)
        {
            var firstFrame = ex.StackTrace.TrimStart();
            if (firstFrame.Length > 300) firstFrame = firstFrame[..300] + "...";
            Console.WriteLine($"[{tag}] at {firstFrame}");
        }

        // Capture a11y hot focus chain (non-blocking, best-effort)
        string hotLine = "";
        try
        {
            var snap = WKAppBot.Win32.Input.KeyboardInput.FocusSnapshot.Capture();
            if (!snap.IsEmpty)
            {
                var fgTitle = NativeMethods.GetWindowTextW(snap.Foreground);
                var fgClass = GetClassNameHelper(snap.Foreground);
                NativeMethods.GetWindowThreadProcessId(snap.Foreground, out uint pid);
                var procName = "?";
                try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
                hotLine = $"fg=0x{snap.Foreground:X} \"{fgTitle}\" ({fgClass}) pid={pid} [{procName}]";
                if (snap.FocusedCtl != IntPtr.Zero && snap.FocusedCtl != snap.Foreground)
                {
                    var ctlTitle = NativeMethods.GetWindowTextW(snap.FocusedCtl);
                    var ctlClass = GetClassNameHelper(snap.FocusedCtl);
                    hotLine += $"\n  focus=0x{snap.FocusedCtl:X} \"{ctlTitle}\" ({ctlClass})";
                }
                Console.WriteLine($"[{tag}] HotLine: {hotLine}");
            }
        }
        catch { /* UIA/Win32 may fail — non-critical */ }

        // Auto bug report via suggest command (Slack + suggestions.jsonl handled by suggest)
        Task.Run(() =>
        {
            try
            {
                var stack = _lastFocusTheftStack ?? ex.StackTrace ?? "(no stack)";
                var callerCmd = string.Join(" ", EyeCmdPipeServer.CallerArgs.Value ?? []);
                var logFile = _currentLogPath != null ? System.IO.Path.GetFileName(_currentLogPath) : "(no log)";
                var bugText = $"[BUG-AUTO] [{tag}] {ex.GetType().Name}: {ex.Message}\nHotLine: {hotLine}\ncmd: {callerCmd}\nlog: {logFile}\n{stack}";
                RunSuggestCommand(bugText);
            }
            catch { /* non-critical */ }
        });
    }

    static void RunSuggestCommand(string text)
    {
        try { SuggestCommand(new[] { text }); }
        catch { /* non-critical */ }
    }

    static string GetClassNameHelper(IntPtr hwnd)
    {
        var sb = new System.Text.StringBuilder(256);
        NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
        return sb.ToString();
    }

    static void LogWarning(string tag, string context, Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[{tag}] {context}: {ex.Message}");
        Console.ResetColor();
    }

    static int AskCommand(string[] args)
    {
        if (args.Length < 2)
            return AskUsage();

        var ai = args[0].ToLowerInvariant();
        // CDP reliability warning moved to CdpClient.ConnectAsync (shared by all CDP paths)
        string? modelHint = null;
        bool newTab = false;
        bool newSession = false;
        bool noWait = false;
        bool loopMode = false;
        bool debateMode = false;
        bool triadMode = false;
        int loopMaxSteps = 7;
        int loopRetry = 1;
        int loopMaxParallel = 7;
        int timeoutSec = 30;
        string? targetTagOverride = null;
        var remaining = new List<string>();
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--new-tab")
                newTab = true;
            else if (args[i] == "--new-session")
                newSession = true;
            else if (args[i] == "--no-wait")
                noWait = true;
            else if (args[i] == "--debate")
                debateMode = true;
            else if (args[i] == "--loop")
            {
                loopMode = true;
                if (i + 1 < args.Length && int.TryParse(args[i + 1], out var loopN))
                {
                    loopMaxSteps = Math.Max(1, loopN);
                    i++;
                }
            }
            else if (args[i] == "--max-steps" && i + 1 < args.Length)
            {
                if (int.TryParse(args[++i], out var maxSteps))
                    loopMaxSteps = Math.Max(1, maxSteps);
            }
            else if (args[i] == "--retry" && i + 1 < args.Length)
            {
                if (int.TryParse(args[++i], out var retryN))
                    loopRetry = Math.Max(0, retryN);
            }
            else if (args[i] == "--max-parallel" && i + 1 < args.Length)
            {
                if (int.TryParse(args[++i], out var mpN))
                    loopMaxParallel = Math.Max(1, mpN);
            }
            else if (args[i] == "--model" && i + 1 < args.Length)
                modelHint = args[++i];
            else if (args[i] == "--triad")
                triadMode = true;
            else if (args[i] == "--target-tag" && i + 1 < args.Length)
                targetTagOverride = args[++i];
            else if (args[i] == "--timeout" && i + 1 < args.Length)
                int.TryParse(args[++i], out timeoutSec);
            else if (args[i] == "--image" && i + 1 < args.Length)
                remaining.Add(args[++i]);
            else
                remaining.Add(args[i]);
        }

        var (questionParts, attachFiles) = ParseTextAndFilesWithMarkers(remaining.ToArray());
        var question = InlineTextFiles(questionParts, attachFiles);
        if (string.IsNullOrWhiteSpace(question))
            return AskUsage();

        foreach (var f in attachFiles)
        {
            if (!File.Exists(f))
                return Error($"File not found: {f}");
        }

        if (attachFiles.Count > 0)
            Console.WriteLine($"[ASK] Attaching {attachFiles.Count} file(s): {string.Join(", ", attachFiles.Select(Path.GetFileName))}");
        if (!string.IsNullOrWhiteSpace(modelHint))
            Console.WriteLine($"[ASK] Model hint: {modelHint}");
        if (noWait && loopMode)
        {
            Console.WriteLine("[ASK] --no-wait enabled: loop mode is ignored for this request.");
            loopMode = false;
        }

        return ai switch
        {
            "gemini" => AskGemini(question, true, timeoutSec, newTab, attachFiles, newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait, targetTagOverride),
            "gpt" or "chatgpt" => AskChatGpt(question, true, timeoutSec, newTab, attachFiles, newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait, targetTagOverride),
            "claude" => AskClaude(question, true, timeoutSec, newTab, newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait, targetTagOverride),
            "triad" or "all" => AskTriadParallel(question, timeoutSec, attachFiles, newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, modelHint, noWait, debateMode),
            _ => Error($"Unknown AI: {ai} (use gemini, gpt, claude, or triad)")
        };
    }

    static string BuildNoWaitQueuedMessage(string aiName)
    {
        var logDir = Path.Combine(DataDir, "logs");
        return $"[{aiName}] Prompt queued. Returning control immediately (--no-wait). Check ongoing output/logs at: {logDir}";
    }

    static int AskUsage()
    {
        Console.WriteLine(@"
WKAppBot Ask ??one-command AI Q&A via WebBot

Usage:
  wkappbot ask gemini ""question"" [files...] [--timeout 30] [--new-tab] [--new-session] [--loop [N]] [--max-steps N] [--retry N] [--triad]
  wkappbot ask gpt ""question"" [files...]   [--timeout 30] [--new-tab] [--new-session] [--loop [N]] [--max-steps N] [--retry N] [--triad]
  wkappbot ask claude ""question""            [--timeout 30] [--new-tab] [--new-session] [--loop [N]] [--max-steps N] [--retry N] [--triad]

Options:
  --timeout N     Max seconds to wait for response (default: 30)
  --new-tab       Open in a new tab (default: reuse existing tab)
  --new-session   Start fresh conversation in existing tab (navigate to new chat URL)
  --no-wait       Return immediately after prompt send (do not wait for model response)
  --loop [N]      Enable remote tool-exec loop (default max steps: 3)
  --max-steps N   Max loop steps (same as --loop N)
  --retry N       Tool execution retry count per step (default: 1)
  --model 4.1     Model/version hint for remote AI (provider remains ask <ai>)
  --triad         Add triad-planning hints to loop persona

File attachment:
  Any argument that matches an existing file path is auto-attached.
  Images (png/jpg/gif/webp/bmp) ??clipboard paste, other files ??file input.
  Multiple files supported ??attached in order before sending the question.

Examples:
  wkappbot ask gemini ""?ㅻ뒛 肄붿뒪???뱀쭠二??뚮젮以?""
  wkappbot ask gpt ""???⑦꽩 遺꾩꽍?댁쨾""
  wkappbot ask gpt ""??UI 遺꾩꽍?댁쨾"" screenshot.png
  wkappbot ask gpt ""肄붾뱶 由щ럭?댁쨾"" main.cs test.log
  wkappbot ask gemini ""???몄뀡?쇰줈 吏덈Ц"" --new-session
");
        return 1;
    }

    /// <summary>
    /// Run Gemini, GPT, Claude in parallel with per-AI output prefixes ([gemini], [gpt], [claude]).
    /// Pre-creates a single unified Slack thread before spawning tasks — all three AIs post replies
    /// to the same thread (AsyncLocal _slackSessionThreadTs inheritance).
    /// If an AI fails, RunTriadAiWithRecovery retries once with context from the other AIs.
    /// </summary>
    static int AskTriadParallel(string question, int timeoutSec, List<string>? attachFiles,
        bool newSession, bool loopMode, int loopMaxSteps, int loopRetry, int loopMaxParallel,
        string? modelHint, bool noWait, bool debateMode = false)
    {
        // Triad always starts fresh per-AI — prevents stale session cross-contamination.
        var freshSession = true;
        Interlocked.Exchange(ref _slackPersonaPostedFlag, 0); // reset: only first AI posts persona
        var modeLabel = debateMode ? "정반합" : "TRIAD";
        Console.WriteLine($"[{modeLabel}] Launching Gemini + GPT + Claude in parallel (fresh sessions)...");

        // ── Unified Slack thread ──────────────────────────────────────────────────────────
        // Set _slackSessionThreadTs.Value BEFORE spawning Task.Run children.
        // AsyncLocal inheritance: each child task sees this value from the moment it starts.
        // EnsureSlackThread inside each AI is idempotent — returns immediately if already set.
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(channel))
            {
                // Use SlackSendWithThread: auto-splits long messages (header → channel, overflow → thread)
                var (ok, ts) = PostWithOverflow(botToken, channel,
                    debateMode ? $"*[정반합]* {question}" : $"*[🔱 TRIAD]* {question}", username: GetSendReplyUsername());
                if (ok && ts != null)
                {
                    _slackSessionThreadTs.Value = ts;
                    Console.WriteLine($"[TRIAD] Unified Slack thread: {ts}");
                    Console.WriteLine($"[TRIAD] Join: wkappbot slack reply \"your opinion\" --msg {ts}");
                    Console.WriteLine($"[TRIAD:THREAD_TS] {ts}");
                }
            }
        }

        // ── Shared context for recovery (in-memory + JSONL files) ────────────────────────
        // Session folder: {DataDir}/triad/{yyyyMMdd_HHmmss} — one folder per triad run.
        // Each AI writes its steps to {sessionDir}/{ai}.jsonl for crash-safe recovery.
        var sessionId = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
        var sessionDir = Path.Combine(DataDir, "triad", sessionId);
        var ctx = new TriadSharedContext(question, sessionDir);
        _triadLastSessionDir = sessionDir;
        Console.WriteLine($"[TRIAD] Session dir: {sessionDir}");

        var triadCwd = EyeCmdPipeServer.CallerCwd.Value;
        var triadHwnd = EyeCmdPipeServer.CallerHwnd.Value;
        var tasks = new[]
        {
            Task.Run(() => { EyeCmdPipeServer.CallerArgs.Value = new[]{"ask","gemini",question}; EyeCmdPipeServer.CallerCwd.Value = triadCwd; EyeCmdPipeServer.CallerHwnd.Value = triadHwnd;
                return RunTriadAiWithRecovery("gemini", question, timeoutSec, attachFiles,
                freshSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, modelHint, noWait, ctx, "[gemini] "); }),
            Task.Run(() => { EyeCmdPipeServer.CallerArgs.Value = new[]{"ask","gpt",question}; EyeCmdPipeServer.CallerCwd.Value = triadCwd; EyeCmdPipeServer.CallerHwnd.Value = triadHwnd;
                return RunTriadAiWithRecovery("gpt", question, timeoutSec, attachFiles,
                freshSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, modelHint, noWait, ctx, "[gpt] "); }),
            Task.Run(() => { EyeCmdPipeServer.CallerArgs.Value = new[]{"ask","claude",question}; EyeCmdPipeServer.CallerCwd.Value = triadCwd; EyeCmdPipeServer.CallerHwnd.Value = triadHwnd;
                return RunTriadAiWithRecovery("claude", question, timeoutSec, attachFiles,
                freshSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, modelHint, noWait, ctx, "[claude] "); }),
        };
        // Wait with nudging: first-done AI's answer streams to peers, then moderator nudges
        var aiNames = new[] { "gemini", "gpt", "claude" };
        var pending = tasks.ToList();
        var results = new int[3];
        while (pending.Count > 0)
        {
            var done = Task.WhenAny(pending).GetAwaiter().GetResult();
            var idx = Array.IndexOf(tasks, done);
            results[idx] = done.Result;
            pending.Remove(done);

            // AI done + others still working → moderator nudge every time
            if (pending.Count > 0 && done.Result == 0)
            {
                var doneAi = aiNames[idx];
                var nudge = $"[MODERATOR]: {doneAi} has finished. Please wrap up your answer promptly.";
                foreach (var p in pending)
                {
                    var pIdx = Array.IndexOf(tasks, p);
                    var peerAi = aiNames[pIdx];
                    // Inject nudge via cross-prompt infrastructure
                    ctx.UpdateChunk(doneAi, nudge);
                }
                Console.WriteLine($"[{modeLabel}] {doneAi} done → moderator nudging {pending.Count} remaining");
                SlackPostToThread($"⏰ *{doneAi}* finished! Moderator: please wrap up, remaining {pending.Count} AI(s).", "Moderator");

                // Follow-up after 1 second
                _ = Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    var followUp = $"[MODERATOR]: Once all answers are in, we begin Round 1 of 정반합 debate. Prepare your [DEBATE_JSON] with STANCE points.";
                    ctx.UpdateChunk("moderator", followUp);
                    SlackPostToThread("📢 All answers in → 정반합 Round 1 starts! Prepare STANCE + DEBATE_JSON.", "Moderator");
                });
            }
        }
        Console.WriteLine($"[{modeLabel}] R1 Done — gemini={results[0]} gpt={results[1]} claude={results[2]}");

        // ── 정반합 사회자 루프 (--debate 플래그 시에만) ──
        if (debateMode && !noWait && results.Count(r => r == 0) >= 2)
        {
            // R0 done → enable cross-prompting for debate rounds
            ctx.CrossPromptEnabled = true;
            Console.WriteLine($"[정반합] R0 complete. Cross-prompting ON. Moderator starting R1...");
            SlackPostToThread("═══ *R0 자유 답변 완료! 정반합 게임 시작!* ═══\n📋 DEBATE_JSON + STANCE 포맷으로 응답해주세요.", "Moderator");
            SlackPostToThread($"📋 *Debate Rules:*\n```\n{BuildDebateOnlyPersona()[..Math.Min(400, BuildDebateOnlyPersona().Length)]}\n```", "Moderator");
            try { RunDebateLoop(question, timeoutSec, ctx); }
            catch (Exception ex) { Console.Error.WriteLine($"[DEBATE] Error: {ex.Message}"); }
        }
        else if (debateMode && !noWait)
        {
            Console.WriteLine("[정반합] Skipping moderator — need at least 2 successful AIs");
        }

        // ── Schedule reminder into caller's Claude Code prompt via `wkappbot prompt send` ──
        // Uses the scheduler (independent process) so TeeTextWriter lifetime is irrelevant.
        var threadTs = _slackSessionThreadTs.Value;
        var callerCwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
        var replyCmd = string.IsNullOrEmpty(threadTs) ? "" :
            $" → 슬랙 쓰레드에 reply: wkappbot slack reply \"종합의견...\" --msg {threadTs}";
        var promptMsg = $"YOU MUST 위 삼두 답변을 꼼꼼히 읽고 반드시 한국어로 종합 의견을 작성하세요! 각 AI별 핵심 차이점과 본인 의견 포함{replyCmd}";
        try
        {
            var cwdTag = AbbreviateCwd(callerCwd);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule!.FileName,
                Arguments = $"prompt send \"{cwdTag}\" \"{promptMsg.Replace("\"", "\\\"")}\" --after 5s",
                UseShellExecute = false,
                CreateNoWindow = true,
            });
            Console.WriteLine($"[TRIAD] Reminder scheduled (prompt send --after 5s) → {cwdTag}");
        }
        catch (Exception ex) { LogWarning("TRIAD", "Reminder schedule error", ex); }
        if (!string.IsNullOrEmpty(threadTs))
        {
            var body = $"🔔 *삼두 완료* — 종합의견 리마인더 예약됨 (5s)\n📌 `--msg {threadTs}`";
            SlackPostToThread(body, "앱봇아이");
        }

        return results.Any(r => r == 0) ? 0 : 1; // success if at least one AI answered
    }

    static bool EnsureTabViaGrap(string browserGrap, string tabPattern)
    {
        try
        {
            using var automation = new UIA3Automation();
            var resolved = GrapHelper.ResolveFullGrap(browserGrap, automation);
            if (resolved == null || resolved.Value.error != null)
            {
                Console.WriteLine($"[ASK] GrapHelper: browser not found ({browserGrap})");
                return false;
            }

            var (_, root, _) = resolved.Value;
            var tab = GrapHelper.FindByNameOrAid(root, tabPattern);
            if (tab == null)
            {
                Console.WriteLine($"[ASK] GrapHelper: tab \"{tabPattern}\" not found");
                return false;
            }

            if (!GrapHelper.IsTabItem(tab))
            {
                Console.WriteLine($"[ASK] GrapHelper: \"{tabPattern}\" matched but not a TabItem (already active?)");
                return true;
            }

            ClickZoomHelper? tabZoom = null;
            try
            {
                var tabRect = tab.BoundingRectangle;
                if (tabRect.Width > 0 && tabRect.Height > 0)
                {
                    var hwnd = resolved.Value.hwnd;
                    var screenRect = new System.Drawing.Rectangle(
                        (int)tabRect.X, (int)tabRect.Y, (int)tabRect.Width, (int)tabRect.Height);
                    tabZoom = ClickZoomHelper.BeginFromRect(screenRect, hwnd, "tab-select", tabPattern);
                }
            }
            catch { }

            var ok = GrapHelper.SwitchToTab(tab);
            if (ok)
                tabZoom?.ShowPass("tab activated");
            else
                tabZoom?.ShowFail("tab switch failed");
            tabZoom?.Dispose();
            return ok;
        }
        catch (Exception ex)
        {
            LogWarning("ASK", "GrapHelper tab routing failed", ex);
            return false;
        }
    }

    sealed record CdpPageTarget(string Id, string Url, string Title, string WebSocketDebuggerUrl);

    // AsyncLocal prefix: each async execution context (Task.Run lambda) has its own prefix.
    // A single AsyncPrefixWriter is installed globally on first use and reads this per-context value.
    static readonly System.Threading.AsyncLocal<string?> _asyncLinePrefix = new();
    static volatile bool _asyncPrefixWriterInstalled;
    static readonly object _prefixInstallLock = new();

    /// <summary>
    /// Set the output line prefix for the current async execution context (e.g. "[gpt] ").
    /// Installs a single AsyncPrefixWriter on Console.Out the first time it is called.
    /// Each parallel Task.Run context gets its own AsyncLocal value → correct per-AI prefix
    /// even when multiple AIs stream output concurrently.
    /// </summary>
    static IDisposable? ApplyOutputPrefix(string? prefix)
    {
        if (string.IsNullOrEmpty(prefix)) return null;
        if (!_asyncPrefixWriterInstalled)
        {
            lock (_prefixInstallLock)
            {
                if (!_asyncPrefixWriterInstalled)
                {
                    Console.SetOut(new AsyncPrefixWriter(Console.Out));
                    _asyncPrefixWriterInstalled = true;
                }
            }
        }
        var prev = _asyncLinePrefix.Value;
        _asyncLinePrefix.Value = prefix;
        return new RestoreAsyncPrefix(prev);
    }

    sealed class RestoreAsyncPrefix(string? prev) : IDisposable
    {
        public void Dispose() => _asyncLinePrefix.Value = prev;
    }

    /// <summary>
    /// Single global Console.Out replacement. Reads AsyncLocal prefix per async execution context.
    /// Buffers per-context line, flushes with prefix atomically when '\n' is seen.
    /// Parallel Task.Run tasks each have their own AsyncLocal → correct AI label per output line.
    /// </summary>
    sealed class AsyncPrefixWriter(TextWriter sink) : TextWriter
    {
        static readonly object _writeLock = new();
        // Per-async-context line buffer (AsyncLocal isolates per Task.Run execution context)
        static readonly System.Threading.AsyncLocal<System.Text.StringBuilder?> _buf = new();
        static System.Text.StringBuilder Buf => _buf.Value ??= new();

        public override System.Text.Encoding Encoding => sink.Encoding;
        public override void Write(char value) { Buf.Append(value); if (value == '\n') FlushLine(); }
        public override void Write(string? value) { if (value != null) foreach (var c in value) Write(c); }
        public override void WriteLine(string? value) { Write(value ?? ""); Write('\n'); }
        public override void WriteLine() => Write('\n');
        public override void Flush() { if (Buf.Length > 0) FlushLine(); sink.Flush(); }

        void FlushLine()
        {
            var line = Buf.ToString();
            Buf.Clear();
            var prefix = _asyncLinePrefix.Value;
            lock (_writeLock)
            {
                if (!string.IsNullOrEmpty(prefix)) sink.Write(prefix);
                sink.Write(line);
                sink.Flush();
            }
        }
        protected override void Dispose(bool disposing) { if (disposing) Flush(); }
    }

    static string BuildAskTargetTag(string provider)
    {
        var hash = GetSessionTag() ?? "default";
        return $"{provider}-{hash}";
    }

    /// <summary>
    /// Build a sandbox key: "{command}+{subcommand}+{hwnd:X8}".
    /// HWND-based: each prompt window → isolated Chrome tab, guaranteed no cross-contamination.
    /// Falls back to cwdHash if HWND not available (direct CLI mode, no Eye).
    /// </summary>
    static string BuildSandboxKey(string command, string subcommand)
    {
        var hwnd = EyeCmdPipeServer.CallerHwnd.Value ?? IntPtr.Zero;
        var hwndStr = hwnd != IntPtr.Zero
            ? hwnd.ToInt64().ToString("X8")
            : (GetSessionTag() ?? "00000000");
        return $"{command}+{subcommand}+{hwndStr}";
    }

    /// <summary>
    /// GetOrCreateSandboxedTab — core sandboxing algorithm:
    ///   ① Registry hit → validate URL against expectedHost
    ///       match  → return existing tab (reconnect)
    ///       mismatch → fast-fail: invalidate registry + create new tab (leave polluted tab for debugging)
    ///   ② Registry miss → EnsureCorrectWindowAsync (existing positioning logic) → register
    /// </summary>
    static async Task<string?> GetOrCreateSandboxedTabAsync(CdpClient cdp, int port, string key, string expectedHost)
    {
        var entry = AskTargetRegistry.GetEntry(key);
        if (entry != null)
        {
            // Registry hit — validate URL
            var targets = await GetPageTargetsAsync(port);
            var tab = targets.FirstOrDefault(t => t.Id == entry.TargetId);
            if (tab != null)
            {
                if (tab.Url.Contains(expectedHost, StringComparison.OrdinalIgnoreCase))
                {
                    // ✓ URL OK — reconnect to this tab
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"[SANDBOX] ✓ Hit: {key} → {entry.TargetId[..Math.Min(8,entry.TargetId.Length)]}");
                    Console.ResetColor();
                    if (cdp.TargetId != entry.TargetId)
                        await cdp.SwitchToTargetAsync(entry.TargetId, port);
                    return entry.TargetId;
                }
                else
                {
                    // ✗ URL mismatch — fast-fail: invalidate + new tab
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[SANDBOX] ✗ Mismatch: {key}");
                    Console.WriteLine($"[SANDBOX]   expected host: {expectedHost}");
                    Console.WriteLine($"[SANDBOX]   actual url:    {tab.Url[..Math.Min(80, tab.Url.Length)]}");
                    Console.WriteLine($"[SANDBOX]   → invalidating registry, creating clean tab");
                    Console.ResetColor();

                    AskTargetRegistry.RemoveEntry(key);
                    // Leave polluted tab alive (debugging) — create a new clean tab
                    try
                    {
                        var result = await cdp.SendAsync("Target.createTarget",
                            new System.Text.Json.Nodes.JsonObject { ["url"] = $"https://{expectedHost}" });
                        var newId = result?["targetId"]?.GetValue<string>();
                        if (newId != null)
                        {
                            await cdp.SwitchToTargetAsync(newId, port);
                            AskTargetRegistry.SetEntry(key, newId, expectedHost);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"[SANDBOX] ✓ New tab after mismatch: {newId[..Math.Min(8,newId.Length)]}");
                            Console.ResetColor();
                            return newId;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogWarning("SANDBOX", "Create failed after mismatch", ex);
                    }
                    return null;
                }
            }
            else
            {
                // Tab gone — remove stale entry, fall through to create
                Console.WriteLine($"[SANDBOX] Stale entry {key} (tab no longer exists) — removing");
                AskTargetRegistry.RemoveEntry(key);
            }
        }

        // Registry miss — ALWAYS create a fresh isolated tab.
        // Do NOT call EnsureCorrectWindowAsync here: its Step 3 steals any host-matching tab,
        // breaking sandbox isolation (e.g. whisper-study grabs the regular ask tab, or vice-versa).
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"[SANDBOX] Miss: {key} — creating fresh tab for {expectedHost}");
        Console.ResetColor();
        try
        {
            var result = await cdp.SendAsync("Target.createTarget",
                new System.Text.Json.Nodes.JsonObject { ["url"] = $"https://{expectedHost}" });
            var newId = result?["targetId"]?.GetValue<string>();
            if (newId != null)
            {
                await cdp.SwitchToTargetAsync(newId, port);
                AskTargetRegistry.SetEntry(key, newId, expectedHost);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[SANDBOX] ✓ Fresh tab: {newId[..Math.Min(8, newId.Length)]}");
                Console.ResetColor();
                return newId;
            }
        }
        catch (Exception ex)
        {
            LogWarning("SANDBOX", "Create failed", ex);
        }
        return null;
    }

    static CdpClient? EnsureCdpConnection(int port = 9222, string? preferredHost = null, bool newTab = false, string? targetTag = null)
    {
        var task = Task.Run(async () =>
        {
            try
            {
                var active = await ChromeLauncher.IsPortActiveAsync(port);
                if (!active)
                {
                    var launchUrl = !string.IsNullOrWhiteSpace(preferredHost) ? $"https://{preferredHost}" : null;
                    Console.WriteLine($"[ASK] Launching Chrome ??{launchUrl ?? "about:blank"}...");
                    await ChromeLauncher.LaunchAsync(port: port, url: launchUrl);
                    await Task.Delay(2500);
                }

                var cdp = new CdpClient();
                try
                {
                    await cdp.ConnectAsync(port, timeoutMs: 5000);
                }
                catch
                {
                    // Connection failed — Chrome may not be running. Force launch and retry.
                    Console.WriteLine("[ASK] CDP connect failed — launching Chrome...");
                    var launchUrl2 = !string.IsNullOrWhiteSpace(preferredHost) ? $"https://{preferredHost}" : null;
                    await ChromeLauncher.LaunchAsync(port: port, url: launchUrl2);
                    await Task.Delay(3000);
                    await cdp.ConnectAsync(port, timeoutMs: 10000);
                }

                // ── Pre-minimize Chrome BEFORE any tab action (prevent focus steal) ──
                var preMinHwnd = cdp.GetChromeWindowHandle();
                if (preMinHwnd != IntPtr.Zero && !NativeMethods.IsIconic(preMinHwnd))
                {
                    NativeMethods.ShowWindow(preMinHwnd, 6); // SW_MINIMIZE
                    // Wait until actually iconic (up to 500ms) — tab action must not start before minimize completes
                    for (int wi = 0; wi < 50 && !NativeMethods.IsIconic(preMinHwnd); wi++)
                        await Task.Delay(10);
                    Console.WriteLine("[ASK] Chrome minimized before tab action (focus-steal prevention)");
                }

                // Sandbox: Registry hit → URL validate → fast-fail on mismatch
                // Registry miss → EnsureCorrectWindowAsync (positioning + creation)
                string? resolvedId;
                if (!string.IsNullOrWhiteSpace(preferredHost) && !string.IsNullOrWhiteSpace(targetTag))
                {
                    resolvedId = await GetOrCreateSandboxedTabAsync(cdp, port, targetTag, preferredHost);
                }
                else
                {
                    // No host constraint — fall back to old EnsureCorrectWindowAsync path
                    var savedTargetId = !string.IsNullOrWhiteSpace(targetTag) ? AskTargetRegistry.GetTargetId(targetTag) : null;
                    await CloseBlankTabs(port);
                    var navigateUrl = !string.IsNullOrWhiteSpace(preferredHost) ? $"https://{preferredHost}" : null;
                    resolvedId = await cdp.EnsureCorrectWindowAsync(port, targetTag, navigateUrl, savedTargetId: savedTargetId);
                    if (resolvedId != null && !string.IsNullOrWhiteSpace(targetTag))
                    {
                        AskTargetRegistry.SetTargetId(targetTag, resolvedId);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"[ASK] Target: {targetTag} → {resolvedId[..Math.Min(8, resolvedId.Length)]}");
                        Console.ResetColor();
                    }
                }

                var chromeHwnd = cdp.GetChromeWindowHandle();
                if (chromeHwnd != IntPtr.Zero)
                {
                    var readiness = new WKAppBot.Win32.Input.InputReadiness();
                    var blocker = readiness.DetectBlocker(chromeHwnd);
                    if (blocker != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[AAR:CDP] Blocker: {blocker.Title} (hwnd={blocker.Handle:X})");
                        Console.ResetColor();
                    }
                    if (NativeMethods.IsIconic(chromeHwnd))
                    {
                        Console.WriteLine($"[AAR:CDP] Chrome minimized ??focusless restore");
                        NativeMethods.ShowWindow(chromeHwnd, 4);
                        await Task.Delay(300);
                    }
                }

                cdp.ChromeWindowHandle = (nint)cdp.GetChromeWindowHandle();
                cdp.EnableFocusTheftMonitoring = true;

                // Magnifier: show on Chrome window for ALL CDP operations (non-foreground target)
                cdp.OnZoomRequired = (hwnd) =>
                {
                    try
                    {
                        NativeMethods.GetWindowRect(hwnd, out var wr);
                        if (wr.Width > 0 && wr.Height > 0)
                            return ClickZoomHelper.BeginFromRect(
                                new System.Drawing.Rectangle(wr.Left, wr.Top, wr.Width, wr.Height),
                                hwnd, "cdp-session", cdp.OperationContext ?? "CDP");
                    }
                    catch { }
                    return null;
                };

                cdp.OnFocusTheft = (method, prevFg, curFg) =>
                {
                    // Capture real call stack for diagnostics (not just "no stack")
                    var stack = new System.Diagnostics.StackTrace(true).ToString();
                    var focusEx = new InvalidOperationException(
                        $"Focus stolen @ CDP:{method}: was={prevFg:X8} now={curFg:X8}");
                    // Inject real stack via helper field
                    _lastFocusTheftStack = stack;
                    LogError("BUG-AUTO", focusEx);
                    _lastFocusTheftStack = null;
                };

                return (CdpClient?)cdp;
            }
            catch (Exception ex)
            {
                LogError("ASK", ex);
                return (CdpClient?)null;
            }
        });
        return task.GetAwaiter().GetResult();
    }

    static async Task<List<CdpPageTarget>> GetPageTargetsAsync(int port)
    {
        var result = new List<CdpPageTarget>();
        try
        {
            using var http = new HttpClient();
            var json = await http.GetStringAsync($"http://localhost:{port}/json");
            var arr = JsonSerializer.Deserialize<JsonArray>(json);
            if (arr == null) return result;

            foreach (var node in arr)
            {
                var type = node?["type"]?.GetValue<string>() ?? "";
                if (!string.Equals(type, "page", StringComparison.OrdinalIgnoreCase))
                    continue;

                var ws = node?["webSocketDebuggerUrl"]?.GetValue<string>() ?? "";
                if (string.IsNullOrWhiteSpace(ws))
                    continue;

                var url = node?["url"]?.GetValue<string>() ?? "";
                var title = node?["title"]?.GetValue<string>() ?? "";
                var id = node?["id"]?.GetValue<string>() ?? string.Empty;
                result.Add(new CdpPageTarget(id, url, title, ws));
            }
        }
        catch { }

        return result;
    }
}
