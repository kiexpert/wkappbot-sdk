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
    // -- Focus theft stack capture (set by OnFocusTheft callback before LogError) --
    [ThreadStatic] static string? _lastFocusTheftStack;

    // -- Common error logging --
    static void LogError(string tag, Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine($"[{tag}] Error: {ex.Message}");
        Console.ResetColor();
        if (ex.StackTrace != null)
        {
            var firstFrame = ex.StackTrace.TrimStart();
            if (firstFrame.Length > 300) firstFrame = firstFrame[..300] + "...";
            Console.Error.WriteLine($"[{tag}] at {firstFrame}");
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
                Console.Error.WriteLine($"[{tag}] HotLine: {hotLine}");
            }
        }
        catch { /* UIA/Win32 may fail -- non-critical */ }

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
        Console.Error.WriteLine($"[{tag}] {context}: {ex.Message}");
        Console.ResetColor();
    }

    // Dispatch a one-shot question to claude CLI's -p print mode. Unlike the
    // CDP-based AskClaude, this hits claude.exe locally: no browser, no CDP
    // dependency, and session continuity comes from the -r/-c flag family.
    // Output is captured, printed to the user, and recorded in
    // .wkappbot/ask/<ts>-claude-cli-<slug>.md just like any other ask target
    // so the session log keeps growing project-locally.
    static int AskClaudeCli(string question, bool newSession)
    {
        var claudeExe = ResolveClaudeExe();
        if (claudeExe == null)
        {
            Console.Error.WriteLine("[ASK] claude CLI not found on PATH. Install Claude Code or use a different AI target.");
            return 127;
        }
        // Session continuity via PickClaudeResumeArg: newest idle session wins,
        // live (VSCode-held) ones are skipped in favor of the previous idle
        // session, nothing left -> fresh. Newsession=true forces fresh.
        string? extraArgs = PickClaudeResumeArg(newSession);
        Console.Error.WriteLine($"[ASK] claude-cli {extraArgs ?? "(new session)"} -- \"{(question.Length > 60 ? question[..57] + "..." : question)}\"");

        var psi = new ProcessStartInfo
        {
            FileName = claudeExe,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8,
        };
        if (!string.IsNullOrEmpty(extraArgs))
        {
            foreach (var a in extraArgs.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                psi.ArgumentList.Add(a);
        }
        psi.ArgumentList.Add("-p");
        psi.ArgumentList.Add(question);

        try
        {
            using var proc = System.Diagnostics.Process.Start(psi);
            if (proc == null) { Console.Error.WriteLine("[ASK] claude CLI failed to start"); return 1; }
            var outSb = new StringBuilder();
            var errSb = new StringBuilder();
            proc.OutputDataReceived += (_, e) => { if (e.Data != null) outSb.AppendLine(e.Data); };
            proc.ErrorDataReceived += (_, e) => { if (e.Data != null) errSb.AppendLine(e.Data); };
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();
            var stdout = outSb.ToString();
            var stderr = errSb.ToString();
            if (!string.IsNullOrEmpty(stdout)) Console.Write(stdout);
            if (!string.IsNullOrEmpty(stderr)) Console.Error.Write(stderr);
            try { WriteAskMd("claude-cli", question, stdout); } catch { }
            return proc.ExitCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[ASK] claude-cli error: {ex.Message}");
            return 1;
        }
    }

    // Locate the most recently modified .wkappbot/ask/*-{ai}-*.md for the
    // current project (git-root scoped, same lookup as WriteAskMd). The .md
    // files are written by every ask invocation and form a natural per-project
    // conversation log per AI, so "the last one" is the session we want the
    // AI to continue from. Returns null when the project has no ask history
    // yet, or when anything IO-related fails.
    static string? FindLastAskMdForAi(string ai)
    {
        try
        {
            // Reuse WriteAskMd's cwd-resolution: prefer EyeCmdPipeServer's
            // CallerCwd, else walk up from exe dir to a .git root, else
            // fall back to Environment.CurrentDirectory.
            var cwd = EyeCmdPipeServer.CallerCwd.Value;
            if (string.IsNullOrEmpty(cwd) || !Directory.Exists(Path.Combine(cwd, ".git")))
            {
                var probe = Path.GetDirectoryName(Environment.ProcessPath) ?? Environment.CurrentDirectory;
                for (int i = 0; i < 10 && !string.IsNullOrEmpty(probe); i++)
                {
                    if (Directory.Exists(Path.Combine(probe, ".git"))) { cwd = probe; break; }
                    probe = Path.GetDirectoryName(probe);
                }
                if (string.IsNullOrEmpty(cwd)) cwd = Environment.CurrentDirectory;
            }
            var askDir = Path.Combine(cwd, ".wkappbot", "ask");
            if (!Directory.Exists(askDir)) return null;

            // chatgpt / gpt share a log family; other AIs match on their exact name.
            var names = ai switch
            {
                "gpt" or "chatgpt" => new[] { "gpt", "chatgpt" },
                _                  => new[] { ai },
            };
            var candidates = new List<FileInfo>();
            foreach (var name in names)
                candidates.AddRange(new DirectoryInfo(askDir).GetFiles($"*-{name}-*.md"));
            return candidates
                .OrderByDescending(f => f.LastWriteTimeUtc)
                .FirstOrDefault()?.FullName;
        }
        catch { return null; }
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
        bool noDryRun = false;
        bool useTools = false;
        int loopMaxSteps = 7;
        int loopRetry = 1;
        int loopMaxParallel = 7;
        int timeoutSec = 30;
        string? targetTagOverride = null;
        string? interceptMsg = null;
        bool _interceptIsInterrupt = false;
        int _interceptQid = 0;
        var remaining = new List<string>();
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--intercept" && i + 1 < args.Length)
                interceptMsg = args[++i];
            else if (args[i] == "--interrupt" && i + 1 < args.Length)
                { interceptMsg = args[++i]; _interceptIsInterrupt = true; }
            else if (args[i] == "--qid" && i + 1 < args.Length)
                int.TryParse(args[++i], out _interceptQid);
            else if (args[i] is "--terminate" or "--stop-agent")
                return AskTerminate(ai);
            else if (args[i] == "--new-tab")
                newTab = true;
            else if (args[i] == "--new-session")
                newSession = true;
            else if (args[i] == "--no-wait")
                noWait = true;
            else if (args[i] == "--debate")
            {
                debateMode = true;
                // --debate N = target consensus count (e.g. --debate 5 = need 5 합의 items)
                if (i + 1 < args.Length && int.TryParse(args[i + 1], out var dc) && dc > 0)
                { _debateTargetConsensus = dc; i++; }
            }
            else if (args[i] == "--dry-run")
                _dryRunMode.Value = true;
            else if (args[i] == "--no-dry-run")
                noDryRun = true;
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
            else if (args[i] == "--use-tools")
            {
                useTools = true;
                loopMode = true; // tools require loop mode for TOOL_CALL parsing
            }
            else if (args[i] == "--target-tag" && i + 1 < args.Length)
                targetTagOverride = args[++i];
            else if (args[i] == "--timeout" && i + 1 < args.Length)
                int.TryParse(args[++i], out timeoutSec);
            else if (args[i] == "--image" && i + 1 < args.Length)
                remaining.Add(args[++i]);
            else
                remaining.Add(args[i]);
        }

        // --intercept / --interrupt: post to existing thread without starting new AI session
        if (!string.IsNullOrWhiteSpace(interceptMsg))
        {
            var msg = _interceptQid > 0 ? $"[Q{_interceptQid}] {interceptMsg}" : interceptMsg;
            return AskIntercept(ai, msg, interrupt: _interceptIsInterrupt);
        }

        var (questionParts, attachFiles) = ParseTextAndFilesWithMarkers(remaining.ToArray());
        var question = InlineTextFiles(questionParts, attachFiles);
        if (string.IsNullOrWhiteSpace(question))
            return AskUsage();

        // --use-tools: prepend tool instructions to question (bypasses persona turn-count check)
        if (useTools)
        {
            question = "[SYSTEM] You have tools. To use them, emit:\n" +
                "[APPBOT_TOOL_CALL_BEGIN]\nwkappbot <command> <args>\n[APPBOT_TOOL_CALL_END]\n" +
                "Available: file read/grep/glob, web search/fetch, inspect, windows.\n" +
                "Wait for TOOL_RESULT before continuing.\n\n" + question;
        }

        foreach (var f in attachFiles)
        {
            if (!File.Exists(f))
                return Error($"File not found: {f}");
        }

        // Session continuity: a fresh tab/session starts the AI cold, so auto-
        // attach this project's MOST RECENT .wkappbot/ask/*-{ai}-*.md as
        // context. InlineTextFiles (<100 KB .md cap) inlines it into the
        // question directly, so the AI starts with the prior conversation
        // already in its input. User-provided attachments take priority: we
        // only auto-attach when attachFiles is otherwise empty.
        if ((newTab || newSession) && attachFiles.Count == 0)
        {
            var lastMd = FindLastAskMdForAi(ai);
            if (lastMd != null)
            {
                attachFiles.Add(lastMd);
                Console.Error.WriteLine($"[ASK] auto-attach prior session: {Path.GetFileName(lastMd)}");
                // Re-run the inline pass so the new [file:...] reference (if
                // any) surfaces as context text. Direct attachment works too
                // -- CDP uploaders pick the file up either way -- but inlining
                // keeps the new session primed with the previous exchange.
                question = InlineTextFiles(new List<string> { question, $"[file:{Path.GetFileName(lastMd)}]" }, attachFiles);
            }
        }

        if (attachFiles.Count > 0)
            Console.Error.WriteLine($"[ASK] Attaching {attachFiles.Count} file(s): {string.Join(", ", attachFiles.Select(Path.GetFileName))}");
        if (!string.IsNullOrWhiteSpace(modelHint))
            Console.Error.WriteLine($"[ASK] Model hint: {modelHint}");
        if (noWait && loopMode)
        {
            Console.WriteLine("[ASK] --no-wait enabled: loop mode is ignored for this request.");
            loopMode = false;
        }

        // Ask mode: dry-run ON by default (read-only tool calls).
        // Callers (agent, debate) pass --no-dry-run to allow writes.
        if (!noDryRun)
        {
            _dryRunMode.Value = true;
        }

        return ai switch
        {
            "gemini" => AskGemini(question, true, timeoutSec, newTab, attachFiles, newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait, targetTagOverride),
            "gpt" or "chatgpt" => AskChatGpt(question, true, timeoutSec, newTab, attachFiles, newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait, targetTagOverride),
            "claude" => AskClaude(question, true, timeoutSec, newTab, attachFiles, newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, triadMode, modelHint, noWait, targetTagOverride),
            "claude-cli" or "clicode" => AskClaudeCli(question, newSession),
            "triad" or "all" => AskTriadParallel(question, timeoutSec, attachFiles, newSession, loopMode, loopMaxSteps, loopRetry, loopMaxParallel, modelHint, noWait, debateMode),
            _ => Error($"Unknown AI: {ai} (use gemini, gpt, claude, claude-cli, or triad)")
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
  --intercept ""msg""  훈수두기: post to existing thread without starting new AI session
                       triad -> last triad thread / gemini|gpt|claude -> per-page thread

File attachment:
  Any argument that matches an existing file path is auto-attached.
  Images (png/jpg/gif/webp/bmp) ??clipboard paste, other files ??file input.
  Multiple files supported ??attached in order before sending the question.

Examples:
  wkappbot ask gemini ""What is today's stock market summary?""
  wkappbot ask gpt ""Explain this UI element""
  wkappbot ask gpt ""Analyze this UI screenshot"" screenshot.png
  wkappbot ask gpt ""Review the code"" main.cs test.log
  wkappbot ask gemini ""Start a new analysis session"" --new-session
");
        PrintRelatedSkills("ask");
        return 1;
    }

    // Sequential question counter for triad sessions (helps AIs reference prior questions)
#pragma warning disable CS0169
    static int _triadQuestionCount;
#pragma warning restore CS0169
    // Target consensus count for debate (--debate N): 0 = no target (loop until maxR3Loops)
    internal static int _debateTargetConsensus;

    /// <summary>
    /// Run Gemini, GPT, Claude in parallel with per-AI output prefixes ([gemini], [gpt], [claude]).
    /// Pre-creates a single unified Slack thread before spawning tasks -- all three AIs post replies
    /// to the same thread (AsyncLocal _slackSessionThreadTs inheritance).
    /// If an AI fails, RunTriadAiWithRecovery retries once with context from the other AIs.
    /// </summary>
    static int AskTriadParallel(string question, int timeoutSec, List<string>? attachFiles,
        bool newSession, bool loopMode, int loopMaxSteps, int loopRetry, int loopMaxParallel,
        string? modelHint, bool noWait, bool debateMode = false)
    {
        // Triad minimum timeout: 120s. Analysis questions easily exceed the default 30s.
        // User can still raise via --timeout N; we only enforce the floor here.
        if (timeoutSec < 120) timeoutSec = 120;

        // Triad always starts fresh per-AI -- prevents stale session cross-contamination.
        var freshSession = newSession; // --new-session only when explicitly requested
        Interlocked.Exchange(ref _slackPersonaPostedFlag, 0); // reset: only first AI posts persona
        if (debateMode && !loopMode) _suppressLoopPersona.Value = true; // debate: game rules replace persona (unless --use-tools enables loopMode)
        var modeLabel = debateMode ? "정반합" : "TRIAD";
        ResetEmojis(); // 🦊🐬🐙 fresh race!
        // Game ID will be set after thread creation (uses thread ts)

        // -- Unified Slack thread ----------------------------------------------------------
        // Set _slackSessionThreadTs.Value BEFORE spawning Task.Run children.
        // AsyncLocal inheritance: each child task sees this value from the moment it starts.
        // EnsureSlackThread inside each AI is idempotent -- returns immediately if already set.
        {
            var config = LoadSlackConfig();
            var botToken = config?["bot_token"]?.GetValue<string>();
            var channel  = config?["channel"]?.GetValue<string>();
            if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(channel))
            {
                // Use SlackSendWithThread: auto-splits long messages (header -> channel, overflow -> thread)
                var toolTag = loopMode ? "+🔧" : "";
                var slackHeader = debateMode
                    ? $"🎙️⚔️ *[정반합{toolTag}]* {question}"
                    : $"*[🔱 TRIAD{toolTag}]* {question}";
                var (ok, ts) = PostWithOverflow(botToken, channel, slackHeader, username: GetSendReplyUsername());
                if (ok && ts != null)
                {
                    _slackSessionThreadTs.Value = ts;
                    // Persist for --intercept (훈수두기)
                    try { Directory.CreateDirectory(Path.Combine(DataDir, "runtime")); File.WriteAllText(Path.Combine(DataDir, "runtime", "last_triad_ts.txt"), ts); } catch { }
                    Console.Error.WriteLine($"[TRIAD] Unified Slack thread: {ts}");
                    Console.Error.WriteLine($"[TRIAD] Join: wkappbot slack reply \"your opinion\" --msg {ts}");
                    Console.Error.WriteLine($"[TRIAD:THREAD_TS] {ts}");
                    Console.Error.WriteLine($"[TRIAD] 훈수두기: wkappbot ask triad --intercept \"훈수내용\"");
                }
            }
        }

        // Game ID = thread ts (unique per debate, linkable)
        var gameId = _slackSessionThreadTs.Value ?? DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
        question = $"[G:{gameId}] {question}";
        var hints = new List<string>();
        // debate status
        if (debateMode)
        {
            var dbInfo = "정반합 ON, max 3 rounds";
            if (_debateTargetConsensus > 0) dbInfo += $", target={_debateTargetConsensus}";
            hints.Add(dbInfo);
        }
        else
            hints.Add("정반합 OFF -- add --debate");
        // tools status (ask = always dry-run; use `agent` command for write access)
        if (loopMode) hints.Add("🔧 tools ON (dry-run -- use agent cmd for writes)");
        else hints.Add("tools OFF -- add --use-tools");
        var debateHint = $" ({string.Join(" | ", hints)})";
        Console.Error.WriteLine($"[{modeLabel}] Game {gameId}: Launching Gemini + GPT + Claude in parallel...{debateHint}");

        // -- Shared context for recovery (in-memory + JSONL files) ------------------------
        // Session folder: {DataDir}/triad/{yyyyMMdd_HHmmss} -- one folder per triad run.
        // Each AI writes its steps to {sessionDir}/{ai}.jsonl for crash-safe recovery.
        var sessionId = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
        var sessionDir = Path.Combine(DataDir, "triad", sessionId);
        var ctx = new TriadSharedContext(question, sessionDir);
        _triadLastSessionDir = sessionDir;
        Console.Error.WriteLine($"[TRIAD] Session dir: {sessionDir}");

        // -- Live MD minutes: auto-generate debate transcript --
        ctx.InitLiveMinutes(question);
        _activeTriadCtx = ctx; // enable MD recording for all SlackPostToThread calls
        if (ctx.MdPath != null)
        {
            Console.Error.WriteLine($"[TRIAD] Live MD: {ctx.MdPath}");
            // Open in VS Code for real-time preview
            try { AppBotPipe.StartTracked(new System.Diagnostics.ProcessStartInfo { FileName = "code", Arguments = $"\"{ctx.MdPath}\"", UseShellExecute = false, CreateNoWindow = true }, Environment.CurrentDirectory, "ASK-CODE"); } catch { }
        }

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

            // Emoji already assigned on first chunk arrival (UpdateChunk) -- handle nudging only
            if (done.Result == 0)
            {
                var doneAi = aiNames[idx];

                // AI done + others still working -> moderator nudge
                if (pending.Count > 0)
                {
                    var nudge = $"[MODERATOR]: {AiDisplayName(doneAi)} has finished. Please wrap up your answer promptly.";
                    foreach (var p in pending)
                    {
                        var pIdx = Array.IndexOf(tasks, p);
                        var peerAi = aiNames[pIdx];
                        // DM nudge directly -- NOT UpdateChunk (pollutes peer chunk stream with moderator messages)
                        ctx.InjectToSingle(peerAi, nudge);
                        SlackPostToThread($"📩 *[DM->{peerAi}]* {nudge}", "🦉 Moderator");
                    }
                    Console.Error.WriteLine($"[{modeLabel}] {AiDisplayName(doneAi)} done -> moderator nudging {pending.Count} remaining");
                    SlackPostToThread($"⏰ *{AiDisplayName(doneAi)}* finished! Moderator: wrap up, {pending.Count} AI(s) remaining.", "🦉 Moderator");

                    // Follow-up after 1 second
                    _ = Task.Run(async () =>
                    {
                        await Task.Delay(1000);
                        var followUp = $"[MODERATOR]: Once all answers are in, we begin Round 1 of 정반합 debate. Prepare your [DEBATE_JSON] with STANCE points.";
                        // Broadcast to all AIs via DM -- NOT UpdateChunk (keeps chunk stream clean for peer content only)
                        foreach (var broadcastAi in aiNames)
                            ctx.InjectToSingle(broadcastAi, followUp);
                        SlackPostToThread($"📢 *[Moderator->ALL]* {followUp}", "🦉 Moderator");
                    });
                }
            }
        }
        Console.Error.WriteLine($"[{modeLabel}] R1 Done -- gemini={results[0]} gpt={results[1]} claude={results[2]}");

        // -- 정반합 사회자 루프 (--debate 플래그 시에만) --
        if (debateMode && !noWait && results.Count(r => r == 0) >= 2)
        {
            // Off-topic detection: check if each AI's answer addresses the original question
            var rawQuestion = question.Contains("] ") ? question[(question.IndexOf("] ") + 2)..] : question;
            var qKeywords = TriadDebateLoop.Tokenize(rawQuestion);
            foreach (var ai in aiNames)
            {
                var answer = ctx.GetLatestChunk(ai);
                if (answer == null || answer.Length < 50) continue;
                var aKeywords = TriadDebateLoop.Tokenize(answer);
                var overlap = qKeywords.Intersect(aKeywords, StringComparer.OrdinalIgnoreCase).Count();
                var ratio = qKeywords.Count > 0 ? (double)overlap / qKeywords.Count : 1.0;
                if (ratio < 0.15) // less than 15% keyword overlap -> likely off-topic
                {
                    var redirect = $"[MODERATOR]: !️ Your response appears off-topic. The question is: \"{rawQuestion}\"\n" +
                        $"Please answer the ACTUAL question directly. Do not explore tools or help menus.";
                    ctx.InjectToSingle(ai, redirect);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Error.WriteLine($"[정반합] {ai} off-topic detected (keyword overlap {overlap}/{qKeywords.Count} = {ratio:P0}) -> redirect sent");
                    Console.ResetColor();
                    SlackPostToThread($"!️ *[Moderator->{ai}]* Off-topic detected! Redirect sent.", "🦉 Moderator");
                }
            }

            // R0 done -> enable moderator for debate rounds
            ctx.ModeratorEnabled = true;
            Console.Error.WriteLine($"[정반합] R0 complete. Cross-prompting ON. Moderator starting R1...");
            SlackPostToThread("=== *R0 자유 답변 완료! 정반합 게임 시작!* ===\n📋 DEBATE_JSON + STANCE 포맷으로 응답해주세요.", "🦉 Moderator");
            SlackPostToThread($"📋 *Debate Rules:*\n```\n{BuildDebateOnlyPersona()}\n```", "🦉 Moderator");
            try { RunDebateLoop(question, timeoutSec, ctx); }
            catch (Exception ex) { Console.Error.WriteLine($"[DEBATE] Error: {ex.Message}"); }
        }
        else if (debateMode && !noWait)
        {
            Console.WriteLine("[정반합] Skipping moderator -- need at least 2 successful AIs");
        }

        // -- Schedule reminder into caller's Claude Code prompt via `wkappbot prompt send` --
        // Uses the scheduler (independent process) so TeeTextWriter lifetime is irrelevant.
        var threadTs = _slackSessionThreadTs.Value;
        var callerCwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
        var replyCmd = string.IsNullOrEmpty(threadTs) ? "" :
            $" -> 슬랙 쓰레드에 reply: wkappbot slack reply \"종합의견...\" --msg {threadTs}";
        var promptMsg = $"YOU MUST 위 삼두 답변을 꼼꼼히 읽고 반드시 한국어로 종합 의견을 작성하세요! 각 AI별 핵심 차이점과 본인 의견 포함{replyCmd}";
        try
        {
            var cwdTag = AbbreviateCwd(callerCwd);
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule!.FileName,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            psi.ArgumentList.Add("prompt");
            psi.ArgumentList.Add("send");
            psi.ArgumentList.Add(cwdTag);
            psi.ArgumentList.Add(promptMsg);
            psi.ArgumentList.Add("--after");
            psi.ArgumentList.Add("5s");
            AppBotPipe.StartTracked(psi, callerCwd, "TRIAD-PROMPT");
            Console.Error.WriteLine($"[TRIAD] Reminder scheduled (prompt send --after 5s) -> {cwdTag}");
        }
        catch (Exception ex) { LogWarning("TRIAD", "Reminder schedule error", ex); }
        if (!string.IsNullOrEmpty(threadTs))
        {
            var body = $"🔔 *삼두 완료* -- 종합의견 리마인더 예약됨 (5s)\n📌 `--msg {threadTs}`";
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
                Console.Error.WriteLine($"[ASK] GrapHelper: browser not found ({browserGrap})");
                return false;
            }

            var (_, root, _) = resolved.Value;
            var tab = GrapHelper.FindByNameOrAid(root, tabPattern);
            if (tab == null)
            {
                Console.Error.WriteLine($"[ASK] GrapHelper: tab \"{tabPattern}\" not found");
                return false;
            }

            if (!GrapHelper.IsTabItem(tab))
            {
                Console.Error.WriteLine($"[ASK] GrapHelper: \"{tabPattern}\" matched but not a TabItem (already active?)");
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
}
