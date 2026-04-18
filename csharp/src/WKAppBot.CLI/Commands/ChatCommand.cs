using System.Diagnostics;
using System.Text;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Claude Code CLI passthrough with rate-limit fallback to ask triad.
    ///
    ///   wkappbot chat                -> exec 'claude' (interactive, stdio inherited)
    ///   wkappbot chat "<q>"          -> 'claude -p <q>' capture + fallback on limit
    ///   wkappbot chat -p "<q>"       -> same as above
    ///   wkappbot chat --no-fallback  -> disable auto-fallback to ask triad
    ///
    /// Fallback triggers:
    ///   1. `claude` binary not found on PATH
    ///   2. stdout/stderr contains rate-limit markers
    ///   3. claude exits non-zero with a known limit signature
    /// </summary>
    // Supported shell targets. AI entries run as REPL via NonBlockingLineReader;
    // OS shells (cmd/powershell/bash) are stdio-inherited subprocesses.
    static readonly HashSet<string> _shellAis =
        new(StringComparer.OrdinalIgnoreCase) { "gpt", "gemini", "triad" };
    static readonly HashSet<string> _shellOsNames =
        new(StringComparer.OrdinalIgnoreCase) { "cmd", "powershell", "pwsh", "bash" };

    /// <summary>Path of persisted default-shell pointer. One line, shell name.</summary>
    static string ChatDefaultShellFile => Path.Combine(DataDir, "chat-default.txt");

    static string GetDefaultShell()
    {
        try
        {
            if (File.Exists(ChatDefaultShellFile))
            {
                var v = File.ReadAllText(ChatDefaultShellFile).Trim().ToLowerInvariant();
                if (!string.IsNullOrEmpty(v)) return v;
            }
        }
        catch { }
        return "claude"; // bootstrap default
    }

    static int SetDefaultShell(string shell)
    {
        shell = shell.Trim().ToLowerInvariant();
        if (shell != "claude" && shell != "codex" && !_shellAis.Contains(shell) && !_shellOsNames.Contains(shell))
        {
            Console.Error.WriteLine($"[CHAT] --set-default: unknown shell '{shell}'. Valid: claude | codex | cmd | powershell | bash | gemini | gpt | triad");
            return 2;
        }
        try
        {
            Directory.CreateDirectory(DataDir);
            File.WriteAllText(ChatDefaultShellFile, shell + "\n");
            Console.WriteLine($"[CHAT] default shell set to '{shell}' -> {ChatDefaultShellFile}");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CHAT] --set-default failed: {ex.Message}");
            return 1;
        }
    }

    static int ChatCommand(string[] args)
    {
        bool printMode = false;
        bool noFallback = false;
        string? explicitShell = null;
        var qParts = new List<string>();
        for (int i = 0; i < args.Length; i++)
        {
            var a = args[i];
            if (a is "-p" or "--print") printMode = true;
            else if (a is "--no-fallback") noFallback = true;
            else if (a is "--help" or "-h") { PrintChatHelp(); return 0; }
            else if (a == "--shell" && i + 1 < args.Length)
                explicitShell = args[++i].ToLowerInvariant();
            else if (a == "--set-default" && i + 1 < args.Length)
                return SetDefaultShell(args[++i]);
            else if (a == "--ai" && i + 1 < args.Length)
            {
                var picked = args[++i].ToLowerInvariant();
                if (picked is "gpt" or "gemini" or "claude" or "triad")
                    _chatFallbackAi = picked;
                else
                {
                    Console.Error.WriteLine($"[CHAT] --ai {picked} invalid. Use gpt|gemini|claude|triad.");
                    return 2;
                }
            }
            // First positional arg as shell-name shortcut: "wkappbot chat cmd" / "chat gemini".
            // Only when it exactly matches a known shell AND no question-like args preceded it.
            else if (explicitShell == null && qParts.Count == 0 &&
                     (_shellAis.Contains(a) || _shellOsNames.Contains(a)
                      || a.Equals("claude", StringComparison.OrdinalIgnoreCase)
                      || a.Equals("codex",  StringComparison.OrdinalIgnoreCase)))
                explicitShell = a.ToLowerInvariant();
            else qParts.Add(a);
        }

        // Resolve target shell: explicit flag > positional > persisted default.
        var shell = explicitShell ?? GetDefaultShell();

        // Sticky default: whenever the user picks a shell explicitly (flag OR
        // positional), silently promote it to be the default for future
        // "wkappbot chat" with no args. So a workflow like
        //   wkappbot chat cmd    # today's session
        //   wkappbot chat        # tomorrow -- jumps back into cmd
        // "just works" without --set-default ceremony.
        if (explicitShell != null)
        {
            try
            {
                Directory.CreateDirectory(DataDir);
                File.WriteAllText(ChatDefaultShellFile, explicitShell + "\n");
            }
            catch { /* best effort */ }
        }

        // OS-shell route: stdio-inherited subprocess. No question mode, no fallback.
        if (_shellOsNames.Contains(shell))
            return ExecOsShell(shell);

        // AI-REPL route: enters the new ChatLoopCommand (MVP router + interrupt channel).
        // MCP loop is untouched; the experimental dispatcher lives only in ChatLoopCommand.
        if (_shellAis.Contains(shell))
        {
            _chatFallbackAi = shell;
            var aq = string.Join(' ', qParts).Trim();
            if (!string.IsNullOrEmpty(aq)) return AskSingleAiFallback(aq, shell);
            Console.Error.WriteLine($"[CHAT] shell=ask-{shell} -- loop mode. /help for commands, /quit to exit.");
            return ChatLoopCommand(args);
        }

        // Codex CLI route -- same "resume last session" convenience as claude:
        // with no args we enter `codex resume --last` which lands the user
        // back in whatever conversation codex was last handling (typically
        // the VSCode Codex extension's active session). Codex binary often
        // isn't on PATH, so we fall back to the known ~/.codex install dir.
        if (shell == "codex")
        {
            var codexExe = ResolveCodexExe();
            if (codexExe == null)
            {
                Console.Error.WriteLine("[CHAT] codex CLI not found. Expected on PATH or at %USERPROFILE%\\.codex\\.sandbox-bin\\codex.exe");
                return 127;
            }
            var cq = string.Join(' ', qParts).Trim();
            if (string.IsNullOrEmpty(cq))
                return ExecClaudeInteractive(codexExe, "resume --last");
            // One-shot prompt: forward verbatim to codex's default (new-session) CLI.
            return ExecClaudeInteractive(codexExe, $"\"{cq.Replace("\"", "\\\"")}\"");
        }

        // Default: claude shell (rate-limit-aware passthrough, existing behavior).
        var question = string.Join(' ', qParts).Trim();
        bool interactive = string.IsNullOrEmpty(question);

        var claudeExe = ResolveClaudeExe();
        if (claudeExe == null)
        {
            if (noFallback)
            {
                Console.Error.WriteLine("[CHAT] `claude` CLI not found on PATH and --no-fallback set. Install Claude Code or drop --no-fallback.");
                return 127;
            }
            if (interactive)
            {
                Console.Error.WriteLine($"[CHAT] `claude` CLI not found on PATH -- entering ask-{_chatFallbackAi} chat loop.");
                Console.Error.WriteLine("[CHAT] /help for commands, /quit or Ctrl+D to exit.");
                return ChatLoopCommand(args);
            }
            Console.Error.WriteLine($"[CHAT] `claude` CLI not installed -- routing to ask {_chatFallbackAi}");
            return AskTriadFallback(question);
        }

        if (interactive)
        {
            // Interactive: replace stdio, inherit TTY. No output capture (cannot detect limit mid-stream).
            // PickClaudeResumeArg walks newest -> oldest and returns -r <id>
            // for the first idle session, skipping any that VSCode or another
            // CLI is currently writing to. Null means "everything's live or
            // no history yet" -- fork fresh.
            return ExecClaudeInteractive(claudeExe, PickClaudeResumeArg(newSession: false) ?? "");
        }

        // Print mode: capture output, scan for limit markers
        var (exit, stdout, stderr) = RunClaudePrint(claudeExe, question, printMode);
        var combined = (stdout ?? "") + "\n" + (stderr ?? "");
        bool limitHit = DetectRateLimitMarker(combined) || (exit != 0 && DetectLimitExitSignature(combined));

        if (!limitHit)
        {
            if (!string.IsNullOrEmpty(stdout)) Console.Write(stdout);
            if (!string.IsNullOrEmpty(stderr)) Console.Error.Write(stderr);
            return exit;
        }

        if (noFallback)
        {
            Console.Error.WriteLine("[CHAT] Rate-limit detected but --no-fallback set -- returning claude's output");
            if (!string.IsNullOrEmpty(stdout)) Console.Write(stdout);
            if (!string.IsNullOrEmpty(stderr)) Console.Error.Write(stderr);
            return exit == 0 ? 1 : exit;
        }

        Console.Error.WriteLine("[CHAT] Rate-limit marker detected in claude output -> routing to ask triad");
        return AskTriadFallback(question);
    }

    static void PrintChatHelp()
    {
        Console.WriteLine("chat [<shell>|<question>] [-p] [--no-fallback] [--shell S] [--ai AI] [--set-default S]");
        Console.WriteLine("  Unified shell router: pick a shell target to pipe into.");
        Console.WriteLine();
        Console.WriteLine("Shell targets:");
        Console.WriteLine("  claude           Claude Code CLI (rate-limit-aware, fallback on limit).");
        Console.WriteLine("  cmd              Windows cmd.exe (stdio inherited).");
        Console.WriteLine("  powershell|pwsh  Windows PowerShell.");
        Console.WriteLine("  bash             Git Bash or WSL bash.");
        Console.WriteLine("  gemini|gpt|triad AI REPL via NonBlockingLineReader (type-ahead works).");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  wkappbot chat                   Use persisted default shell (init=claude).");
        Console.WriteLine("  wkappbot chat cmd               Launch cmd.exe.");
        Console.WriteLine("  wkappbot chat gemini            Gemini REPL loop.");
        Console.WriteLine("  wkappbot chat \"<q>\"             One-shot: claude -p <q>, fallback on limit.");
        Console.WriteLine("  wkappbot chat gemini \"<q>\"      One-shot via Gemini.");
        Console.WriteLine("  wkappbot chat --shell bash      Force bash shell (disambiguates arg vs shell).");
        Console.WriteLine("  wkappbot chat --set-default cmd Persist cmd as default shell.");
        Console.WriteLine("  wkappbot chat --ai gpt          Claude fallback AI override (default gemini).");
        Console.WriteLine("  wkappbot chat --no-fallback     Disable auto-fallback on claude rate-limit.");
        Console.WriteLine();
        Console.WriteLine("Rate-limit fallback (claude shell only):");
        Console.WriteLine("  Triggered by: 'usage limit', 'rate limit', '5-hour limit',");
        Console.WriteLine("                'session exhausted', 'HTTP 429'");
        Console.WriteLine("  Routes to: ask <--ai>  (default gemini; cheap single-AI, not triad).");
    }

    static string? ResolveClaudeExe()
    {
        var exts = new[] { ".cmd", ".exe", ".bat", "" };

        // 1) Normal PATH walk (covers most installs when PATH was refreshed after install)
        var pathEnv = Environment.GetEnvironmentVariable("PATH") ?? "";
        foreach (var dir in pathEnv.Split(Path.PathSeparator))
        {
            if (string.IsNullOrWhiteSpace(dir)) continue;
            foreach (var ext in exts)
            {
                var p = Path.Combine(dir, "claude" + ext);
                if (File.Exists(p)) return p;
            }
        }

        // 2) Well-known npm global install locations on Windows. Eye often starts
        // BEFORE the user installs @anthropic-ai/claude-code via npm, so its cached
        // PATH doesn't yet include %APPDATA%\npm. Explicit probe avoids needing an
        // Eye restart.
        var fallbackDirs = new List<string>();
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        if (!string.IsNullOrEmpty(appData))
            fallbackDirs.Add(Path.Combine(appData, "npm"));
        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        if (!string.IsNullOrEmpty(localAppData))
        {
            fallbackDirs.Add(Path.Combine(localAppData, "npm"));
            fallbackDirs.Add(Path.Combine(localAppData, "Programs", "claude")); // official installer target
        }
        fallbackDirs.Add(@"C:\Program Files\nodejs");
        fallbackDirs.Add(@"C:\Program Files (x86)\nodejs");

        foreach (var dir in fallbackDirs)
        {
            foreach (var ext in exts)
            {
                var p = Path.Combine(dir, "claude" + ext);
                if (File.Exists(p))
                {
                    Console.Error.WriteLine($"[CHAT] claude resolved via fallback probe: {p}");
                    Console.Error.WriteLine($"[CHAT] (PATH did not include {dir} -- Eye started before install; restart Eye or add to system PATH)");
                    return p;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Launch an OS-shell subprocess with inherited stdio. No question mode, no
    /// rate-limit fallback -- the shell manages its own lifecycle. Exit code
    /// passthrough. Used for "wkappbot chat cmd / powershell / bash".
    /// </summary>
    /// <summary>
    /// Launch an OS shell (cmd / powershell / bash / ...) with full bidirectional
    /// IOCP-style pipe streaming: child stdout / stderr relayed to the user's
    /// terminal as each line arrives (no buffering), and lines typed by the user
    /// at the Launcher's terminal forwarded into the child's stdin.
    ///
    /// Why not inherited stdio? On Windows, when a child inherits a parent's
    /// console, its output uses direct Write/WriteConsole calls that don't flow
    /// through the parent process's Console.Out -- the messages literally draw
    /// on screen directly, which works in a vanilla terminal but breaks when
    /// the parent's stdio has been captured or repointed (as in our
    /// Launcher -> Core chain and future pipe-mode wrappers). Piped redirect
    /// + async BeginOutputReadLine is the reliable "works anywhere" path.
    /// </summary>
    static int ExecOsShell(string shellName)
    {
        var exe = shellName switch
        {
            "cmd"        => "cmd.exe",
            "powershell" => "powershell.exe",
            "pwsh"       => "pwsh.exe",
            "bash"       => ResolveBashExe() ?? "bash.exe",
            _            => shellName, // caller already validated
        };

        // cmd.exe specifics:
        //   /Q                  ECHO OFF -- kills the pipe-mode "command line
        //                       echoed twice" effect. Harmless in ConPTY.
        //   /K @chcp 65001>nul  UTF-8 code page (kills CP949 mojibake both
        //                       ways: output AND input).
        //   PROMPT=[WK]-badge   The PROMPT env var is the *right* place to
        //                       brand the prompt because cmd.exe counts its
        //                       rendered width when tracking the cursor --
        //                       Home/End/arrows/Backspace all remain correctly
        //                       positioned. $E is cmd.exe's literal ESC.
        //                       Layout: magenta [WK] badge, space, bold-green
        //                       path + '>', trailing space. End with a reset
        //                       so user input and command output render plain.
        string? shellArgs = null;
        if (shellName == "cmd")
        {
            Environment.SetEnvironmentVariable(
                "PROMPT",
                "$E[1;7;95m[WK]$E[0m $E[1;32m$P$G$E[0m ");
            shellArgs = "/Q /K @chcp 65001>nul";
        }

        var streamer = new PromptDecoratingStreamer();

        // ConPTY path: gives the child shell a real terminal so cmd.exe's
        // native line-editing (Tab complete, arrow keys, F7/F8 history) works.
        // Requires Win10 1809+. On older Windows or on failure we fall through
        // to the pipe-based byte relay below.
        if (WKAppBot.Shared.PseudoConsoleRunner.IsSupported)
        {
            try
            {
                Console.WriteLine($"[CHAT] [MODE=ConPTY] shell: {exe}  (type 'exit' to return; leading-space or ?/! routes to chat)");
                Console.Out.Flush();
                return WKAppBot.Shared.PseudoConsoleRunner.Run(
                    exe: exe,
                    args: shellArgs,
                    cwd: Environment.CurrentDirectory,
                    onOutput: streamer.OnBytes,           // write to stdout
                    onLineReady: InterceptChatOrShell,    // Enter-key chat detector
                    mirrorToTerminal: false);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CHAT] ConPTY failed ({ex.Message}); falling back to pipe mode");
            }
        }

        try
        {
            Console.WriteLine($"[CHAT] [MODE=pipe] shell: {exe}  (type 'exit' to return)");
            Console.Out.Flush();

            var psi = new ProcessStartInfo
            {
                FileName = exe,
                Arguments = shellArgs ?? "",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardInputEncoding  = Encoding.UTF8, // match chcp 65001: cmd.exe reads input as UTF-8
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding  = Encoding.UTF8,
                CreateNoWindow = true,
                WorkingDirectory = Environment.CurrentDirectory,
            };
            using var proc = Process.Start(psi);
            if (proc == null) { Console.Error.WriteLine("[CHAT] failed to start shell"); return 1; }

            // -- Byte-level output relay (stdout + stderr) --
            // OutputDataReceived line-buffers and swallows partial lines (the
            // prompt!). Read raw bytes from BaseStream instead so prompts like
            // "D:\> " reach the terminal before the user types anything.
            var outCts = new CancellationTokenSource();
            var outThread = new Thread(() => ReadBytesToStreamer(proc.StandardOutput.BaseStream, streamer, outCts.Token))
            { IsBackground = true, Name = "chat-shell-stdout" };
            var errThread = new Thread(() => ReadBytesToStreamer(proc.StandardError.BaseStream, streamer, outCts.Token))
            { IsBackground = true, Name = "chat-shell-stderr" };
            outThread.Start();
            errThread.Start();

            // -- Input forwarder --
            // User types at the Launcher's terminal; NonBlockingLineReader picks
            // each line up and we pipe it into the child's stdin. Closing stdin
            // on EOF tells the shell to exit (natural Ctrl+Z / Ctrl+D behavior).
            using var userReader = new WKAppBot.Shared.NonBlockingLineReader();
            var inputCts = new CancellationTokenSource();
            var inputThread = new Thread(() =>
            {
                try
                {
                    while (!proc.HasExited && !inputCts.IsCancellationRequested)
                    {
                        if (!userReader.TryTake(out var line, timeoutMs: 250, inputCts.Token))
                            continue;
                        if (line == null)
                        {
                            try { proc.StandardInput.Close(); } catch { }
                            break;
                        }
                        try
                        {
                            proc.StandardInput.WriteLine(line);
                            proc.StandardInput.Flush();
                        }
                        catch { break; }
                    }
                }
                catch { /* best effort */ }
            })
            {
                IsBackground = true,
                Name = "chat-shell-stdin",
            };
            inputThread.Start();

            proc.WaitForExit();
            outCts.Cancel();
            inputCts.Cancel();
            try { outThread.Join(500); } catch { }
            try { errThread.Join(500); } catch { }
            try { inputThread.Join(500); } catch { }
            streamer.CloseStyle(); // reset any lingering ANSI color state
            return proc.ExitCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CHAT] shell '{shellName}' error: {ex.Message}");
            return 1;
        }
    }

    // Raw-byte reader for the pipe fallback path. BeginOutputReadLine buffers
    // by line and hides the shell prompt (no trailing \n); we read bytes ourselves
    // and hand them off to the same streamer the ConPTY path uses, so prompts and
    // decoration are identical across both modes.
    static void ReadBytesToStreamer(Stream input, PromptDecoratingStreamer streamer, CancellationToken ct)
    {
        var buf = new byte[4096];
        try
        {
            while (!ct.IsCancellationRequested)
            {
                int n = input.Read(buf, 0, buf.Length);
                if (n <= 0) break;
                streamer.OnBytes(buf, n);
            }
        }
        catch { /* pipe closed on child exit */ }
    }

    // Pure byte-pass-through to the real console. Prompt branding/coloring
    // lives in the PROMPT env var now (cmd.exe renders [WK] badge + bold-green
    // path itself, which keeps cursor tracking correct for Home/End/arrows).
    // This class still exists as the shared hook point for future tee-to-
    // ToolOutputStore capture, but today it does nothing fancy.
    sealed class PromptDecoratingStreamer
    {
        static readonly byte[] ResetStyle = Encoding.ASCII.GetBytes("\x1b[0m");

        readonly Stream _stdout = Console.OpenStandardOutput();
        readonly object _lock = new();

        public void OnBytes(byte[] buf, int len)
        {
            if (len <= 0) return;
            lock (_lock)
            {
                _stdout.Write(buf, 0, len);
                _stdout.Flush();
            }
        }

        public void CloseStyle()
        {
            lock (_lock)
            {
                _stdout.Write(ResetStyle, 0, ResetStyle.Length);
                _stdout.Flush();
            }
        }
    }

    // Enter-key interceptor handed to PseudoConsoleRunner. Returns true to
    // CONSUME the Enter (line handled here, don't run in shell); false to let
    // the shell execute it normally. Routing mirrors ChatLoopCommand's
    // LooksLikeShellCommand ladder:
    //   - leading space                 -> force chat
    //   - trailing ? or ! (+ fullwidth) -> chat
    //   - any non-ASCII                 -> chat
    //   - otherwise                     -> shell (return false)
    // When consumed, print a blank line for visual separation, dispatch to
    // the fallback AI, then another blank line so the fresh cmd.exe prompt
    // (forced by PseudoConsoleRunner's Enter-replacement) lands cleanly.
    static bool InterceptChatOrShell(string line)
    {
        if (string.IsNullOrWhiteSpace(line)) return false;

        Action<string>? dispatch = null;
        if (IsAppBotCommand(line))      dispatch = DispatchAppBotCommand;
        else if (LooksLikeChatLine(line)) dispatch = DispatchChatToClaudeOrFallback;
        if (dispatch == null) return false;

        // PseudoConsoleRunner already sent ESC (cmd.exe's typed line erased) and
        // will send CR afterwards (cmd.exe emits "\r\n + fresh prompt"). We
        // contribute one CRLF so our output starts on a fresh line, then let
        // cmd.exe's own CRLF below handle the separator to the next prompt.
        var stdout = Console.OpenStandardOutput();
        var crlf = System.Text.Encoding.ASCII.GetBytes("\r\n");
        stdout.Write(crlf, 0, crlf.Length);
        stdout.Flush();

        try { dispatch(line); }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CHAT] dispatch failed: {ex.Message}");
        }
        return true;
    }

    // Primary chat route inside an OS shell (chat cmd / chat bash etc.): send
    // the line to Claude CLI first because it's the cheapest, most capable
    // local agent available, and -r/-c gives us exact session continuity with
    // whatever VSCode Claude Code is already doing. Only fall back to the
    // CDP-based gpt/gemini/triad path when claude CLI is unreachable (not
    // installed, exit 127). This matches the user's ask: "if the shell isn't
    // an AI, pick claude CLI as the first agent."
    static void DispatchChatToClaudeOrFallback(string line)
    {
        var q = line.TrimStart();
        var rc = AskClaudeCli(q, newSession: false);
        if (rc == 127)
        {
            Console.Error.WriteLine($"[CHAT] claude CLI unavailable -- falling back to ask {_chatFallbackAi}");
            AskSingleAiFallback(q, _chatFallbackAi);
        }
    }

    // First token is "wkappbot" or "a11y" (our CLI entry points). We intercept
    // these so cmd.exe's tokenizer never gets to touch them -- '?' as a glob,
    // '&' as a separator, naked quotes etc. all stop being traps when we pass
    // the raw line directly to Process.Start.
    static bool IsAppBotCommand(string line)
    {
        var t = line.TrimStart();
        return t.StartsWith("wkappbot ", StringComparison.OrdinalIgnoreCase)
            || t.Equals("wkappbot", StringComparison.OrdinalIgnoreCase)
            || t.StartsWith("a11y ", StringComparison.OrdinalIgnoreCase)
            || t.Equals("a11y", StringComparison.OrdinalIgnoreCase);
    }

    // Spawn the AppBot command as a child with inherited stdio so its output
    // flows to the same terminal. UseShellExecute=false -> no cmd.exe layer,
    // no re-parsing of '?' / '"' / '&'. The user's line comes in verbatim
    // (minus the exe name) as the process arguments.
    static void DispatchAppBotCommand(string line)
    {
        var t = line.TrimStart();
        int sp = t.IndexOf(' ');
        string exe = sp < 0 ? t : t[..sp];
        string args = sp < 0 ? "" : t[(sp + 1)..];
        var psi = new ProcessStartInfo
        {
            FileName = exe,
            Arguments = args,
            UseShellExecute = false,
            WorkingDirectory = Environment.CurrentDirectory,
        };
        using var proc = Process.Start(psi);
        proc?.WaitForExit();
    }

    // ASCII-only classification; returns true when the line should be routed
    // to the AI rather than the shell. Kept local to ChatCommand because
    // ChatLoopCommand's own LooksLikeShellCommand is the inverse (and does
    // additional PATH probing which isn't needed here: inside `chat cmd` the
    // user is already past the "is this a shell command?" gate, so we only
    // pull out the clear chat signals).
    static bool LooksLikeChatLine(string line)
    {
        if (line.Length > 0 && line[0] == ' ') return true;
        var trimmed = line.TrimEnd();
        if (trimmed.Length > 0)
        {
            char last = trimmed[^1];
            if (last is '?' or '!' or '\uFF1F' or '\uFF01') return true;
        }
        foreach (var ch in line)
            if (ch > 0x7F) return true;
        return false;
    }

    static string? ResolveBashExe()
    {
        foreach (var p in new[]
        {
            @"C:\Program Files\Git\bin\bash.exe",
            @"C:\Program Files\Git\usr\bin\bash.exe",
            @"C:\Windows\System32\bash.exe", // WSL wrapper
        })
            if (File.Exists(p)) return p;
        return null;
    }

    // PATH lookup first (in case the user added codex to PATH after install),
    // then the OpenAI-installer's canonical location at
    // %USERPROFILE%\.codex\.sandbox-bin\codex.exe.
    static string? ResolveCodexExe()
    {
        try
        {
            var pathEnv = Environment.GetEnvironmentVariable("PATH") ?? "";
            foreach (var dir in pathEnv.Split(Path.PathSeparator))
            {
                if (string.IsNullOrWhiteSpace(dir)) continue;
                var candidate = Path.Combine(dir, "codex.exe");
                if (File.Exists(candidate)) return candidate;
            }
        }
        catch { }
        try
        {
            var home = Environment.GetEnvironmentVariable("USERPROFILE");
            if (!string.IsNullOrEmpty(home))
            {
                var fallback = Path.Combine(home, ".codex", ".sandbox-bin", "codex.exe");
                if (File.Exists(fallback)) return fallback;
            }
        }
        catch { }
        return null;
    }

    // Locate the most recently modified .jsonl under
    //   %USERPROFILE%\.claude\projects\<slug>\
    // where <slug> is Claude Code's cwd-slug convention: lowercase the drive
    // letter, turn ':' and '\' into '-'. For "D:\GitHub\WKAppBot" that's
    // "d--GitHub-WKAppBot" (observed in memory paths).
    //
    // That file is almost certainly the session the VSCode Claude Code
    // extension is writing to right now (extensions append on every message
    // turn, so its mtime is freshest). Returns the FileInfo so callers can
    // use either the session id (bare name) or the full path/content.
    // Returns null on any failure.
    static FileInfo? FindActiveClaudeSessionJsonl()
    {
        try
        {
            var home = Environment.GetEnvironmentVariable("USERPROFILE");
            if (string.IsNullOrEmpty(home)) return null;

            var cwd = Environment.CurrentDirectory;
            var chars = cwd.ToCharArray();
            if (chars.Length > 0) chars[0] = char.ToLowerInvariant(chars[0]);
            var slug = new string(chars).Replace(':', '-').Replace('\\', '-');

            var sessionDir = Path.Combine(home, ".claude", "projects", slug);
            if (!Directory.Exists(sessionDir)) return null;

            return new DirectoryInfo(sessionDir)
                .GetFiles("*.jsonl")
                .OrderByDescending(f => f.LastWriteTimeUtc)
                .FirstOrDefault();
        }
        catch { return null; }
    }

    static string? FindActiveClaudeSessionId() =>
        FindActiveClaudeSessionJsonl()?.Name is string n ? Path.GetFileNameWithoutExtension(n) : null;

    // Heuristic: is another process actively using this session file right now?
    // We try an exclusive open -- if any process (VSCode Claude Code extension,
    // another claude CLI, etc.) has it open for writing with FileShare other
    // than None, the CreateFile fails with a sharing violation. Treat that as
    // "session is live; do NOT second-attach via -r".
    //
    // False negatives (file rotated, closed but we couldn't test): we just end
    // up resuming as before. False positives (anti-virus / indexer briefly
    // opening the file): we skip resume for that one invocation, which is
    // harmless -- the user can still --resume explicitly.
    static bool IsSessionFileActive(string path)
    {
        try
        {
            using var _ = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            return false; // got exclusive write access -> nobody else holds it for write
        }
        catch (IOException) { return true; }
        catch { return false; }
    }

    // Pick the resume strategy for claude CLI:
    //   -r <id> : prefer the newest session if idle; if newest is LIVE
    //             (VSCode Code extension writing to it), fall back to the
    //             2nd-newest idle session so we preserve the previous
    //             context instead of forking cold. User: "아니면 그 직전
    //             세션이랑 연결해도 되고."
    //   null    : nothing idle to resume -- start FRESH.
    static string? PickClaudeResumeArg(bool newSession)
    {
        if (newSession) return null;
        try
        {
            var home = Environment.GetEnvironmentVariable("USERPROFILE");
            if (string.IsNullOrEmpty(home)) return null;
            var cwd = Environment.CurrentDirectory;
            var chars = cwd.ToCharArray();
            if (chars.Length > 0) chars[0] = char.ToLowerInvariant(chars[0]);
            var slug = new string(chars).Replace(':', '-').Replace('\\', '-');
            var sessionDir = Path.Combine(home, ".claude", "projects", slug);
            if (!Directory.Exists(sessionDir)) return null;

            // Walk newest -> oldest, return the first idle one.
            var ordered = new DirectoryInfo(sessionDir).GetFiles("*.jsonl")
                .OrderByDescending(f => f.LastWriteTimeUtc)
                .ToArray();
            for (int i = 0; i < ordered.Length; i++)
            {
                var f = ordered[i];
                if (IsSessionFileActive(f.FullName))
                {
                    Console.Error.WriteLine($"[CHAT] {f.Name} is in use -- trying previous session...");
                    continue;
                }
                if (i > 0)
                    Console.Error.WriteLine($"[CHAT] resuming previous session (skipped {i} live one(s)): {f.Name}");
                return $"-r {Path.GetFileNameWithoutExtension(f.Name)}";
            }
        }
        catch { }
        return null; // all sessions live (or none exist) -- fork fresh
    }

    // Write the last <tailBytes> of the user's active Claude session JSONL
    // to a temp .txt file and return its path, so AskCommand's existing
    // InlineTextFiles (<100 KB .txt/.md) logic auto-inlines it into the
    // fallback AI's prompt. The file lives under %TEMP% keyed by cwd hash so
    // repeated fallbacks overwrite rather than accumulate. 80 KB tail covers
    // roughly the last ~10 turns of a coding session without blowing the
    // downstream ask inline cap.
    static string? WriteClaudeSessionTailFile(int tailBytes = 80 * 1024)
    {
        try
        {
            var fi = FindActiveClaudeSessionJsonl();
            if (fi == null || !fi.Exists || fi.Length == 0) return null;

            long start = Math.Max(0, fi.Length - tailBytes);
            var bufLen = (int)Math.Min(tailBytes, fi.Length);
            var buf = new byte[bufLen];
            int read;
            using (var fs = fi.OpenRead())
            {
                fs.Seek(start, SeekOrigin.Begin);
                read = fs.Read(buf, 0, buf.Length);
            }
            var tail = Encoding.UTF8.GetString(buf, 0, read);
            // If we cut mid-line, drop the first partial record so the AI
            // only sees complete JSONL entries.
            if (start > 0)
            {
                int firstNl = tail.IndexOf('\n');
                if (firstNl > 0 && firstNl + 1 < tail.Length) tail = tail[(firstNl + 1)..];
            }
            if (tail.Length == 0) return null;

            // cwd-scoped filename so concurrent chat fallbacks in different
            // projects don't clobber each other.
            var cwdHash = Environment.CurrentDirectory
                .GetHashCode()
                .ToString("x8");
            var tempDir = Path.GetTempPath();
            var path = Path.Combine(tempDir, $"wkappbot-claude-tail-{cwdHash}.txt");

            var header = $"# Recent Claude Code conversation (cwd: {Environment.CurrentDirectory})\n"
                       + $"# Source: {fi.FullName}\n"
                       + $"# Tail size: {tail.Length} bytes (most recent turns)\n"
                       + $"# Format: JSONL (one Claude Code record per line)\n\n";
            File.WriteAllText(path, header + tail, Encoding.UTF8);
            return path;
        }
        catch { return null; }
    }

    static int ExecClaudeInteractive(string claudeExe, string args = "")
    {
        try
        {
            // Bare Process.Start -- same rationale as ExecOsShell: interactive shells
            // need the real terminal handles, not a CreateProcessW-hooked detached
            // console that swallows stdin.
            var psi = new ProcessStartInfo
            {
                FileName = claudeExe,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardInput = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                CreateNoWindow = false,
                WorkingDirectory = Environment.CurrentDirectory,
            };
            using var proc = Process.Start(psi);
            if (proc == null) { Console.Error.WriteLine("[CHAT] Failed to start claude"); return 1; }
            proc.WaitForExit();
            return proc.ExitCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[CHAT] interactive error: {ex.Message}");
            return 1;
        }
    }

    static (int exit, string stdout, string stderr) RunClaudePrint(string claudeExe, string question, bool printMode)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = claudeExe,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
            };
            psi.ArgumentList.Add("-p");
            psi.ArgumentList.Add(question);
            using var proc = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "CHAT-PRINT")
                          ?? AppBotPipe.Start(psi);
            if (proc == null) return (1, "", "[CHAT] Failed to start claude");
            var outSb = new StringBuilder();
            var errSb = new StringBuilder();
            proc.OutputDataReceived += (_, e) => { if (e.Data != null) outSb.AppendLine(e.Data); };
            proc.ErrorDataReceived += (_, e) => { if (e.Data != null) errSb.AppendLine(e.Data); };
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();
            return (proc.ExitCode, outSb.ToString(), errSb.ToString());
        }
        catch (Exception ex)
        {
            return (1, "", $"[CHAT] print-mode error: {ex.Message}");
        }
    }

    static readonly string[] RateLimitMarkers =
    [
        "usage limit",
        "rate limit",
        "5-hour limit",
        "session exhausted",
        "Claude is temporarily unavailable",
        "HTTP 429",
        "too many requests",
        "quota exceeded",
    ];

    static bool DetectRateLimitMarker(string text)
    {
        if (string.IsNullOrEmpty(text)) return false;
        foreach (var m in RateLimitMarkers)
            if (text.Contains(m, StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    static bool DetectLimitExitSignature(string text)
    {
        // Broader check for non-zero exits -- e.g. "limit" appearing without the exact marker phrase
        return !string.IsNullOrEmpty(text)
            && (text.Contains("limit", StringComparison.OrdinalIgnoreCase)
                || text.Contains("429", StringComparison.Ordinal));
    }

    // Default fallback AI. Triad (GPT+Gemini+Claude parallel debate) is overkill for
    // a casual chat and has known bugs + long runtimes -- bad UX for a first-response
    // path. Gemini is the current default: fastest to reply, single stream to read,
    // simplest to debug when something goes wrong. Override with --ai gpt|claude|triad.
    static string _chatFallbackAi = "gemini";

    static int AskSingleAiFallback(string question, string ai)
    {
        var preview = question.Length > 80 ? question[..77] + "..." : question;
        Console.Error.WriteLine($"[CHAT:FALLBACK] ask {ai} \"{preview}\"");

        // Per-project continuity: attach a trimmed tail of the user's active
        // Claude Code session (.jsonl) so the alternate AI gets the same
        // conversational context Claude had. Saved as a .txt tail file under
        // the ask InlineTextFiles size cap so it's inlined into the prompt
        // (kept to 80KB of the JSONL tail = roughly last 5-10 turns).
        // Returns null when no session can be found.
        var contextFile = WriteClaudeSessionTailFile();
        var attachList = new List<string>();
        if (contextFile != null) attachList.Add(contextFile);

        if (ai == "triad")
        {
            return AskTriadParallel(
                question,
                timeoutSec: 180,
                attachFiles: attachList.Count > 0 ? attachList : null,
                newSession: false,
                loopMode: false,
                loopMaxSteps: 3,
                loopRetry: 1,
                loopMaxParallel: 7,
                modelHint: null,
                noWait: false,
                debateMode: false);
        }
        // Delegate to `wkappbot ask <ai> <question> [file]` which is the same
        // code path an operator would hit typing the ask command by hand --
        // keeps the fallback in sync with any future improvements. A third
        // positional arg is read as an attachment by ParseTextAndFilesWithMarkers.
        var args = contextFile != null
            ? new[] { ai, question, contextFile }
            : new[] { ai, question };
        return AskCommand(args);
    }

    static int AskTriadFallback(string question) => AskSingleAiFallback(question, _chatFallbackAi);

    /// <summary>
    /// REPL loop for interactive-mode fallback when `claude` CLI is not installed.
    /// Uses the shared <see cref="WKAppBot.Shared.NonBlockingLineReader"/> so the
    /// user can type the NEXT question while the AI is still answering the current
    /// one -- their keystrokes land in a thread-safe queue and are consumed on the
    /// next iteration with no visible lag.
    ///
    /// Terminal-native line editing (Backspace, arrow keys, shell history) is
    /// preserved because the reader calls Console.ReadLine() under the hood.
    /// Real-time character streaming / bottom-line-pinned prompts are a future
    /// enhancement; this layer covers the common 80% cleanly.
    ///
    /// Exits on /quit, /exit, :q, EOF (Ctrl+D / Ctrl+Z+Enter), or stdin close.
    /// </summary>
    static int RunAskTriadRepl()
    {
        using var reader = new WKAppBot.Shared.NonBlockingLineReader();

        int cycles = 0, pending = 0;
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            var depthTag = reader.PendingCount > 0 ? $" (buffered={reader.PendingCount})" : "";
            Console.Write($"{_chatFallbackAi}>{depthTag} ");
            Console.ResetColor();

            var line = reader.Take();
            if (line == null)
            {
                Console.Error.WriteLine($"[CHAT] EOF -- leaving REPL after {cycles} cycle(s)");
                return 0;
            }

            var q = line.Trim();
            if (q.Length == 0) continue;

            if (q is "/quit" or "/exit" or ":q")
            {
                Console.Error.WriteLine($"[CHAT] /quit -- leaving REPL after {cycles} cycle(s)");
                return 0;
            }
            if (q is "/help" or "?")
            {
                Console.WriteLine("  /quit | /exit | :q   -- leave REPL");
                Console.WriteLine("  /help | ?            -- this help");
                Console.WriteLine("  <any question>       -- routed to ask " + _chatFallbackAi);
                Console.WriteLine("  Ctrl+Z then Enter    -- EOF (Windows)");
                Console.WriteLine();
                Console.WriteLine("  Tip: you can keep typing while the AI answers --");
                Console.WriteLine("  lines are queued and the prompt shows (buffered=N).");
                continue;
            }

            try
            {
                pending = reader.PendingCount;
                var rc = AskSingleAiFallback(q, _chatFallbackAi);
                cycles++;
                var picked = reader.PendingCount;
                var lagNote = picked > pending
                    ? $" +{picked - pending} queued during this run"
                    : "";
                Console.Error.WriteLine($"[CHAT] cycle #{cycles} done (rc={rc}){lagNote}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[CHAT] {_chatFallbackAi} error: {ex.Message} -- continuing");
            }
        }
    }
}
