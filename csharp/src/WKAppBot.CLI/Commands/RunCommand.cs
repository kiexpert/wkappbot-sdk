// RunCommand.cs -- wkappbot exec <key>: AppBot-managed exe launcher
//
// Generic "launch + watch + stream loading state" for any registered
// exe. HTS apps (hero4/heroglobal/...) ship as built-in presets but any
// exe can be registered via --register.
//
// Usage:
//   wkappbot exec hero4                 launch Hero4 HTS (preset)
//   wkappbot exec heroglobal            launch HeroGlobal
//   wkappbot exec xingq                 launch eBEST xingQ
//   wkappbot exec kiwoom                launch Kiwoom OpenAPI
//   wkappbot exec list                  list available keys + registered paths
//   wkappbot exec --register <key> <exe-path> [--title <pattern>]
//                                       register a custom exe
//
// Default: stdin/stdout/stderr piped (relay-friendly for CLI children).
// Add --no-pipe for GUI apps that benefit from Minimized+focusless launch.
//
// Also reachable as `wkappbot run <key>` (polymorphic -- see AppBotProgramInfra).
//
// Profiles persisted at {hq}/profiles/run_profiles.json. First launch of a
// known key (hero4/etc.) auto-scans Program Files if the profile is missing.
//
// Loading state stream (stderr): launch -> first-window -> blocker dismiss ->
// main-window-ready. Stdout reserved for the final status line so callers
// can pipe `wkappbot run hero4 | grep OK` for scripting.

using System.Text.Json;
using System.Text.Json.Serialization;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    static readonly Dictionary<string, ExecProfile> _builtInExecProfiles = new(StringComparer.OrdinalIgnoreCase)
    {
        ["hero4"]       = new("Hero4",       "Hero4.exe",       new[] { "*영웅문4*", "*Hero4*" }),
        ["heroglobal"]  = new("HeroGlobal",  "Heroglobal.exe",  new[] { "*영웅문 Global*", "*HeroGlobal*" }),
        ["xingq"]       = new("xingQ",       "xingQ.exe",       new[] { "*xingQ*", "*이베스트*" }),
        ["kiwoom"]      = new("Kiwoom Open", "opstarter.exe",   new[] { "*KOA Studio*", "*Open API*" }),
        ["tuhon"]       = new("Tuhon",       "Tuhon.exe",       new[] { "*투혼*", "*Tuhon*" }),
        // Test presets: sanity-check the exec pipeline (READY probe, a11y
        // diff stream, --stdin-inject) without needing an HTS installed.
        ["notepad"]     = new("Notepad",     "notepad.exe",     new[] { "*Notepad*", "*메모장*" }),
        ["calc"]        = new("Calculator",  "calc.exe",        new[] { "*Calculator*", "*계산기*" }),
        // Coding agents -- reuse the exec pipeline (stdin-inject, --watch,
        // loading-trace) for local AI CLIs. codex.exe lives at an unusual
        // path (%USERPROFILE%\.codex\.sandbox-bin\) so ScanForExe has a
        // custom fast-path; see ScanForExe below.
        ["codex"]       = new("Codex CLI",   "codex.exe",       new[] { "*codex*", "*Codex*" }),
    };

    internal sealed record ExecProfile(string DisplayName, string ExeName, string[] MainTitlePatterns);

    sealed class RunProfileStore
    {
        [JsonPropertyName("paths")]
        public Dictionary<string, string> Paths { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    }

    static int ExecCommand(string[] args)
    {
        if (args.Length == 0 || args[0] is "--help" or "-h")
            return ExecUsage();
        if (args[0].Equals("list", StringComparison.OrdinalIgnoreCase))
            return ExecList();
        if (args[0] == "--register")
        {
            if (args.Length < 3) return ExecUsage();
            return ExecRegister(args[1], args[2]);
        }

        var key = args[0].ToLowerInvariant();
        ExecProfile? profile = null;
        string? exePath = null;

        if (_builtInExecProfiles.TryGetValue(key, out var builtIn))
        {
            profile = builtIn;
            // Resolve exe path: saved profile > Program Files scan > error out
            var store = LoadRunProfileStore();
            exePath = store.Paths.TryGetValue(key, out var saved) && File.Exists(saved) ? saved : null;
            if (exePath == null)
            {
                exePath = ScanForExe(profile.ExeName);
                if (exePath != null)
                {
                    store.Paths[key] = exePath;
                    SaveRunProfileStore(store);
                    Console.Error.WriteLine($"[EXEC] Discovered + saved: {key} -> {exePath}");
                }
            }
            if (exePath == null || !File.Exists(exePath))
            {
                Console.Error.WriteLine($"[EXEC] {profile.DisplayName} exe not found. Register explicitly:");
                Console.Error.WriteLine($"  wkappbot run --register {key} \"C:\\Path\\To\\{profile.ExeName}\"");
                return 1;
            }
        }
        else
        {
            // Direct-path or PATH-resolved fallback: args[0] is either a raw exe
            // path (e.g. "C:/bin/foo.exe") or a bare name (e.g. "notepad") that
            // RunCommand already resolved via TryResolveExecutable before
            // delegating here. Build an ad-hoc ExecProfile so the rest of the
            // pipeline (launch, watch, loading-trace) still applies.
            exePath = TryResolveExecutable(args[0]);
            if (exePath == null || !File.Exists(exePath))
            {
                Console.Error.WriteLine($"[EXEC] Unknown key: {key}. Not a preset, not a registered path, not a resolvable executable.");
                Console.Error.WriteLine("  Try `wkappbot exec list` or `--register`, or pass an absolute .exe path.");
                return 1;
            }
            var display = Path.GetFileNameWithoutExtension(exePath);
            profile = new ExecProfile(display, Path.GetFileName(exePath), new[] { "*" + display + "*" });
            Console.Error.WriteLine($"[EXEC] ad-hoc launch: {exePath} (no preset)");
        }

        // Flag parse (must happen before the "already running" adopt branch
        // uses stdinInject).
        bool pipeIO = !args.Any(a => a == "--no-pipe");
        bool keepWatching = args.Any(a => a == "--watch");
        bool stdinInject = args.Any(a => a == "--stdin-inject");
        if (stdinInject) { pipeIO = false; keepWatching = true; }

        // Already running? Match by process name (not title) -- WindowFinder
        // walks a UIA subtree fallback for Code/Claude/Codex/Chrome windows
        // that false-positives on VS Code tabs containing words like "Notepad".
        var exeBase = Path.GetFileNameWithoutExtension(profile.ExeName);
        var running = System.Diagnostics.Process.GetProcessesByName(exeBase);
        System.Diagnostics.Process? adopted = null;
        if (running.Length > 0)
        {
            if (stdinInject)
            {
                // Adopt existing instance -- --stdin-inject's point is often
                // to feed data into an app that's already up (HTS, editor,
                // notepad). Bailing would defeat the use case.
                adopted = running[0];
                Console.Error.WriteLine($"[EXEC] --stdin-inject: adopting existing {profile.DisplayName} pid={adopted.Id}");
                foreach (var p in running.Skip(1)) { try { p.Dispose(); } catch { } }
            }
            else
            {
                var first = running[0];
                IntPtr h = IntPtr.Zero;
                string title = "";
                try { h = first.MainWindowHandle; title = first.MainWindowTitle ?? ""; } catch { }
                Console.Error.WriteLine($"[EXEC] {profile.DisplayName} already running: pid={first.Id} hwnd=0x{h.ToInt64():X8} \"{title}\"");
                Console.WriteLine($"OK {profile.DisplayName} already-running pid={first.Id} hwnd=0x{h.ToInt64():X8}");
                foreach (var p in running) { try { p.Dispose(); } catch { } }
                return 0;
            }
        }
        TimeSpan? readyTimeout = null;
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "--timeout")
            {
                readyTimeout = ParseDuration(args[i + 1]);
                break;
            }
        }
        return LaunchAndWatch(profile, exePath, pipeIO, keepWatching, readyTimeout, stdinInject, adopted);
    }

    // -- Launch + progress watch ---------------------------------------------

    static int LaunchAndWatch(ExecProfile profile, string exePath, bool pipeIO, bool keepWatching = false, TimeSpan? readyTimeout = null, bool stdinInject = false, System.Diagnostics.Process? adoptedProc = null)
    {
        System.Diagnostics.Process? proc = adoptedProc;
        if (proc != null)
        {
            Console.Error.WriteLine($"[EXEC] Adopted existing {profile.DisplayName} pid={proc.Id}{(stdinInject ? " (stdin->focus inject)" : "")}");
        }
        else try
        {
            Console.Error.WriteLine($"[EXEC] Launching {profile.DisplayName}: {exePath}{(pipeIO ? " (stdio piped)" : "")}{(stdinInject ? " (stdin->focus inject)" : "")}");
            // Two launch shapes:
            //   default: UseShellExecute=true + WindowStyle.Minimized so GUI apps
            //   (HTS etc.) come up in the taskbar without stealing foreground.
            //   --pipe:  UseShellExecute=false + CreateNoWindow=true + redirect
            //   stdin/stdout/stderr so CLI children can be relayed to the caller.
            //   Pipe mode sacrifices the minimize trick but is only useful for
            //   tools that actually have a stdio interface.
            System.Diagnostics.ProcessStartInfo psi;
            if (stdinInject)
            {
                // Inject mode: bypass AppBotPipe.Start's hidden/minimized
                // policy and bypass UseShellExecute=true's UWP-broker stub
                // (which would exit immediately, leaving the real Notepad
                // pid unreachable). Spawn() + SW_SHOWNOACTIVATE gives us a
                // visible-but-no-focus-steal window and a real pid.
                var cwd = Path.GetDirectoryName(exePath) ?? Environment.CurrentDirectory;
                var sr = AppBotPipe.Spawn(exePath, "", cwd, showNoActivate: true, caller: "EXEC");
                if (sr == null)
                {
                    Console.Error.WriteLine("[EXEC] Launch failed (AppBotPipe.Spawn returned null)");
                    return 1;
                }
                try { proc = System.Diagnostics.Process.GetProcessById(sr.Pid); }
                catch
                {
                    Console.Error.WriteLine($"[EXEC] Launch OK (pid={sr.Pid}) but GetProcessById failed -- cannot track lifecycle");
                    return 1;
                }
                Console.Error.WriteLine($"[EXEC] Process spawned: pid={proc.Id} (visible, no-activate)");
                // Skip the pipeIO relay and common psi-path fall-through
                goto LaunchDone;
            }
            else if (pipeIO)
            {
                psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = exePath,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = System.Text.Encoding.UTF8,
                    StandardErrorEncoding  = System.Text.Encoding.UTF8,
                    WorkingDirectory = Path.GetDirectoryName(exePath) ?? Environment.CurrentDirectory,
                };
            }
            else
            {
                psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = exePath,
                    UseShellExecute = true,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized,
                    WorkingDirectory = Path.GetDirectoryName(exePath) ?? Environment.CurrentDirectory,
                };
            }
            proc = AppBotPipe.Start(psi);
            if (proc == null)
            {
                Console.Error.WriteLine("[EXEC] Launch failed (AppBotPipe.Start returned null)");
                return 1;
            }
            Console.Error.WriteLine($"[EXEC] Process spawned: pid={proc.Id}");
        LaunchDone:

            if (pipeIO)
            {
                // child stdout -> our stdout (thread)
                _ = System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        var buf = new char[4096];
                        int n;
                        while ((n = proc.StandardOutput.Read(buf, 0, buf.Length)) > 0)
                            Console.Out.Write(buf, 0, n);
                    }
                    catch { }
                });
                // child stderr -> our stderr (thread)
                _ = System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        var buf = new char[4096];
                        int n;
                        while ((n = proc.StandardError.Read(buf, 0, buf.Length)) > 0)
                            Console.Error.Write(buf, 0, n);
                    }
                    catch { }
                });
                // our stdin -> child stdin (thread)
                _ = System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        var buf = new char[4096];
                        int n;
                        while ((n = Console.In.Read(buf, 0, buf.Length)) > 0)
                        {
                            proc.StandardInput.Write(buf, 0, n);
                            proc.StandardInput.Flush();
                        }
                        try { proc.StandardInput.Close(); } catch { }
                    }
                    catch { }
                });
                Console.Error.WriteLine($"[EXEC] stdio relay active: stdin->child, child->stdout/stderr");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EXEC] Launch error: {ex.Message}");
            return 1;
        }

        // -- Per-second watch loop ------------------------------------------
        // Each tick prints: elapsed, current top-title seen for this pid, a
        // WM_NULL response-time probe (quick measure of UI thread liveness),
        // and a trailing list of recent dialog messages.
        // READY = main title matches a target pattern AND pid-owned window
        // list is stable for >=2s AND last response-time <500ms.
        var watchStart = DateTime.UtcNow;
        // Default 3-minute ready wait fits cold-start HTS; callers can override
        // with --timeout 1s etc to claim control back quickly even if READY
        // hasn't been declared yet.
        var watchBudget = readyTimeout ?? TimeSpan.FromMinutes(3);
        IntPtr mainHwnd = IntPtr.Zero;
        int dismissedBlockers = 0;
        var recentDialogs = new LinkedList<string>(); // dedupe + last-3 log
        string? lastStableSignature = null;
        DateTime stableSince = DateTime.MinValue;
        int lastResponseMs = -1;
        // Output-rate limiter: once the app is steady (post-READY, same title,
        // same resp bucket, no new blockers) skip the per-tick status line and
        // only emit a heartbeat every 30s so the narrator stays quiet until
        // something interesting happens.
        string? lastLoggedTitle = null;
        int lastLoggedRespBucket = -2;
        int lastLoggedBlockers = -1;
        DateTime lastStatusLogAt = DateTime.MinValue;
        int tickCount = 0;
        // Track a11y nodes observed so far so we can stream "newly-appeared"
        // entries on each tick. Limited to depth-2 children of the current
        // top window -- we're not competing with `a11y hack`, just giving
        // an expansion trace while the HTS warms up its UI.
        var seenA11y = new HashSet<string>(StringComparer.Ordinal);
        FlaUI.UIA3.UIA3Automation? uia = null;
        try { uia = new FlaUI.UIA3.UIA3Automation(); } catch { /* UIA unavailable -- fall back silently */ }

        // --stdin-inject: background reader drains Console.In line-by-line into
        // a queue that the watch tick services. Keeps stdin from blocking the
        // loop while still letting `echo ... | wkappbot exec` feed the child.
        var stdinQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();
        var injectState = new InjectState();
        if (stdinInject)
        {
            new System.Threading.Thread(() =>
            {
                try
                {
                    string? line;
                    while ((line = Console.In.ReadLine()) != null)
                    {
                        if (line.Length > 0) stdinQueue.Enqueue(line);
                    }
                }
                catch { }
            }) { IsBackground = true, Name = "exec-stdin-inject-reader" }.Start();
            Console.Error.WriteLine("[EXEC] --stdin-inject: stdin lines will be typed into focused edit fields (guarded by pid+ControlType+IsPassword)");
        }

        // Startup grace: give the process 800ms before first probe so the
        // window handle has a chance to be created.
        Thread.Sleep(800);

        while (DateTime.UtcNow - watchStart < watchBudget)
        {
            var tickStart = DateTime.UtcNow;

            bool procGone = false;
            try { procGone = proc?.HasExited == true; } catch { procGone = !IsProcessAlive(proc?.Id ?? 0); }
            if (procGone)
            {
                // Ctrl+C from stdin asked us to close the app. Exit clean --
                // this is the expected lifecycle end, not a crash. Never
                // re-attach after an explicit close request.
                if (injectState.CloseRequested)
                {
                    Console.Error.WriteLine($"[EXEC] Child closed via Ctrl+C stdin signal -- exiting");
                    Console.WriteLine($"OK {profile.DisplayName} closed-via-ctrl-c");
                    return 0;
                }
                // UWP-broker re-attach: Win11's System32\notepad.exe (and many
                // other packaged apps) is an App Execution Alias redirector.
                // The pid we launched is the broker stub; it forks the real
                // app process and exits. In --stdin-inject mode we need the
                // real pid -- find it by exe name.
                if (stdinInject)
                {
                    var exeBase2 = Path.GetFileNameWithoutExtension(profile.ExeName);
                    var adopt = System.Diagnostics.Process.GetProcessesByName(exeBase2)
                        .Where(p => { try { return p.Id != proc?.Id; } catch { return true; } })
                        .OrderByDescending(p => { try { return p.StartTime; } catch { return DateTime.MinValue; } })
                        .FirstOrDefault();
                    if (adopt != null)
                    {
                        Console.Error.WriteLine($"[EXEC] Re-attach: broker stub exited, adopting real pid={adopt.Id} ({exeBase2})");
                        try { proc?.Dispose(); } catch { }
                        proc = adopt;
                        continue;
                    }
                }
                Console.Error.WriteLine($"[EXEC] Process exited before main window appeared");
                Console.WriteLine($"FAIL {profile.DisplayName} process-exited-early");
                return 1;
            }

            // Enumerate windows owned by this pid (not just title matches).
            // A splash window may not match the main-title pattern but is
            // still a visible loading surface worth reporting.
            var pidWindows = EnumerateProcessWindows((uint)proc!.Id);

            // Blocker dismiss pass over every pid-owned window
            var readiness = CreateInputReadiness();
            foreach (var w in pidWindows)
            {
                var blocker = readiness.DetectBlocker(w.Handle);
                if (blocker != null)
                {
                    var msg = $"{blocker.ClassName} \"{blocker.Title}\"";
                    if (recentDialogs.Contains(msg) == false)
                    {
                        recentDialogs.AddLast(msg);
                        if (recentDialogs.Count > 3) recentDialogs.RemoveFirst();
                    }
                    Console.Error.WriteLine($"[EXEC] Blocker: {msg} -- dismissing");
                    var (handled, _) = readiness.BlockerHandler?.TryHandle(w.Handle, blocker) ?? (false, false);
                    if (handled) dismissedBlockers++;
                }
            }

            // Main-window probe by title pattern
            WindowInfo? main = null;
            foreach (var pattern in profile.MainTitlePatterns)
            {
                var hit = pidWindows
                    .FirstOrDefault(w => w.Rect.Width > 300 && w.Rect.Height > 200 &&
                        (PatternMatch(w.Title, pattern) || PatternMatch(w.ClassName, pattern)));
                if (hit != null) { main = hit; break; }
            }

            // If we haven't found main yet, pick the largest visible window
            // as "current top" for display purposes.
            var topForDisplay = main
                ?? pidWindows.OrderByDescending(w => w.Rect.Width * w.Rect.Height).FirstOrDefault();

            // WM_NULL response-time probe on the top window: 1000ms timeout.
            // Fast response = UI thread alive; slow/timeout = still loading.
            if (topForDisplay != null && NativeMethods.IsWindow(topForDisplay.Handle))
            {
                var rsw = System.Diagnostics.Stopwatch.StartNew();
                var ok = NativeMethods.SendMessageTimeoutW(
                    topForDisplay.Handle, 0x0000 /*WM_NULL*/, IntPtr.Zero, IntPtr.Zero,
                    0x0002 /*SMTO_ABORTIFHUNG*/, 1000, out _);
                rsw.Stop();
                lastResponseMs = ok != IntPtr.Zero ? (int)rsw.ElapsedMilliseconds : -1;
            }

            // Stability check: pid window-set fingerprint
            var signature = string.Join(",", pidWindows
                .Where(w => w.Rect.Width > 100)
                .OrderBy(w => w.Handle.ToInt64())
                .Select(w => $"{w.Handle:X}:{w.Title}"));
            if (signature != lastStableSignature)
            {
                lastStableSignature = signature;
                stableSince = DateTime.UtcNow;
            }
            var stableSec = (DateTime.UtcNow - stableSince).TotalSeconds;

            // -- Per-tick status line (delta-aware) --
            // Print on: first 3 ticks (warmup), title change, response-bucket
            // flip (fast<500ms / slow>=500ms / hung=-1), blocker-count change,
            // or 30s heartbeat since last status log. Steady-state is silent.
            tickCount++;
            var elapsed = (DateTime.UtcNow - watchStart).TotalSeconds;
            var titlePreview = topForDisplay?.Title;
            if (string.IsNullOrWhiteSpace(titlePreview)) titlePreview = topForDisplay?.ClassName ?? "(no window)";
            if (titlePreview.Length > 50) titlePreview = titlePreview[..47] + "...";
            var respStr = lastResponseMs < 0 ? "hung" : $"{lastResponseMs}ms";
            int respBucket = lastResponseMs < 0 ? -1 : (lastResponseMs < 500 ? 0 : 1);
            bool titleChanged = titlePreview != lastLoggedTitle;
            bool respChanged = respBucket != lastLoggedRespBucket;
            bool blockersChanged = dismissedBlockers != lastLoggedBlockers;
            bool heartbeat = (DateTime.UtcNow - lastStatusLogAt).TotalSeconds >= 30;
            bool warmup = tickCount <= 3;
            bool shouldLog = warmup || titleChanged || respChanged || blockersChanged || heartbeat;
            if (shouldLog)
            {
                var dlgTail = recentDialogs.Count == 0 ? "" : $" dlg=[{string.Join("|", recentDialogs.TakeLast(2))}]";
                var hb = heartbeat && !titleChanged && !respChanged && !blockersChanged ? " hb" : "";
                Console.Error.WriteLine($"[EXEC] +{elapsed:F0}s win=\"{titlePreview}\" resp={respStr} blockers={dismissedBlockers}{dlgTail}{hb}");
                lastLoggedTitle = titlePreview;
                lastLoggedRespBucket = respBucket;
                lastLoggedBlockers = dismissedBlockers;
                lastStatusLogAt = DateTime.UtcNow;
            }

            // -- Newly-appeared a11y nodes, emitted in grap form --------------
            // Each new node prints as `<window-grap>#<ControlType>_<Aid|Name>`
            // so the line is copy-pasteable directly into subsequent
            // `wkappbot a11y <action> <grap>` commands. Only walks two levels
            // deep -- cheap, and HTS MFC UIA trees rarely expose more than a
            // flat control list under the main window.
            if (uia != null && topForDisplay != null && NativeMethods.IsWindow(topForDisplay.Handle)
                && !FocusSafe.ShouldYieldToActiveUser(out _))
            {
                try
                {
                    var winGrap = BuildCompactWinGrap(topForDisplay.Handle);
                    var rootEl = uia.FromHandle(topForDisplay.Handle);
                    foreach (var nodeSuffix in EnumA11yDepth2Suffixes(rootEl))
                    {
                        var grap = $"{winGrap}#{nodeSuffix}";
                        if (seenA11y.Add(grap))
                            Console.Error.WriteLine($"[EXEC:A11Y] {grap}");
                    }
                }
                catch { /* UIA on a transitioning window can throw -- skip this tick */ }
            }

            // READY condition: main title matched + last response fast +
            // window set stable for >=2s. Main title has to match; a random
            // splash won't pass the MainTitlePatterns filter.
            if (main != null && lastResponseMs >= 0 && lastResponseMs < 500 && stableSec >= 2 && mainHwnd == IntPtr.Zero)
            {
                mainHwnd = main.Handle;
                Console.Error.WriteLine($"[EXEC] {profile.DisplayName} READY after {elapsed:F1}s (response={lastResponseMs}ms, stable {stableSec:F0}s, blockers={dismissedBlockers})");
                Console.WriteLine($"OK {profile.DisplayName} ready hwnd=0x{mainHwnd:X8} elapsed={elapsed:F1}s resp={lastResponseMs}ms blockers={dismissedBlockers}");
                if (!keepWatching) return 0;
                // --watch: fall through to continue streaming a11y diffs
                // until the child process itself exits. READY is reported as
                // a milestone, but the monitor session lives on.
                Console.Error.WriteLine($"[EXEC] --watch: staying attached until pid={proc!.Id} exits");
                // Expand budget to child's natural lifetime
                watchBudget = TimeSpan.FromDays(7);
                watchStart = DateTime.UtcNow;
            }

            // Sleep remainder of the second. When --stdin-inject is on, slice
            // the sleep into 150ms chunks so keyboard focus is probed near
            // real-time (user expectation: type a line, see it appear in the
            // focused edit within ~200ms).
            var took = (DateTime.UtcNow - tickStart).TotalMilliseconds;
            var remaining = (int)Math.Max(0, 1000 - took);
            if (stdinInject && uia != null && remaining > 0)
            {
                var fastDeadline = DateTime.UtcNow.AddMilliseconds(remaining);
                while (DateTime.UtcNow < fastDeadline)
                {
                    var slice = Math.Min(150, (int)(fastDeadline - DateTime.UtcNow).TotalMilliseconds);
                    if (slice <= 0) break;
                    Thread.Sleep(slice);
                    TryInjectStdinToFocus(uia, proc!, stdinQueue, injectState);
                    if (injectState.CloseRequested) break;
                }
            }
            else if (remaining > 0)
            {
                Thread.Sleep(remaining);
            }
        }

        if (mainHwnd != IntPtr.Zero && keepWatching)
        {
            // watch-mode exit path: child process died, we stayed around to
            // log it. Not a failure -- the launch itself succeeded.
            Console.Error.WriteLine($"[EXEC] --watch: child pid exited, monitor session closing");
            Console.WriteLine($"OK {profile.DisplayName} watched-to-exit");
            return 0;
        }
        // --timeout + --watch: budget elapsed without READY but user wants to
        // keep watching. Emit a partial-OK and extend the budget so we keep
        // logging until the child dies.
        if (keepWatching && proc?.HasExited == false)
        {
            Console.Error.WriteLine($"[EXEC] Timeout {watchBudget.TotalSeconds:F0}s elapsed (READY not yet reached) -- --watch stays attached to pid={proc.Id}");
            Console.WriteLine($"OK {profile.DisplayName} not-yet-ready pid={proc.Id} (watch continues)");
            watchBudget = TimeSpan.FromDays(7);
            watchStart = DateTime.UtcNow;
            goto KeepWatching;
        }
        Console.Error.WriteLine($"[EXEC] Timeout after {watchBudget.TotalSeconds:F0}s (main window never stabilized) -- requesting CloseMainWindow so we exit together with the orphan child");
        try { proc?.CloseMainWindow(); } catch { }
        Console.WriteLine($"FAIL {profile.DisplayName} timeout (child close requested)");
        return 1;
    KeepWatching:
        while (proc != null && !proc.HasExited)
        {
            if (injectState.CloseRequested) break;
            if (stdinInject && uia != null)
            {
                Thread.Sleep(150);
                TryInjectStdinToFocus(uia, proc, stdinQueue, injectState);
            }
            else
            {
                Thread.Sleep(1000);
            }
        }
        Console.Error.WriteLine($"[EXEC] --watch: child pid exited after extended watch");
        Console.WriteLine($"OK {profile.DisplayName} watched-to-exit");
        return 0;
    }

    // Mutable inject state shared between watch loop and inject probe.
    // Kept as a class so the probe can flip CloseRequested without a ref
    // parameter (simpler static-method signatures).
    sealed class InjectState
    {
        public string? LastInjected;
        public bool CloseRequested;
    }

    // --stdin-inject probe: if the child has a focused editable element, pop
    // one queued stdin line and drop it in via ValuePattern.
    //
    // Safety ladder (fail closed on any uncertainty):
    //   1. UIA reachable?
    //   2. User idle >= FocusSafe threshold? (don't steal while they type)
    //   3. Focused element's ProcessId == our child? (don't type into other apps)
    //   4. ControlType ∈ {Edit, Document}? (not a button, not a list)
    //   5. IsPassword == false? (never inject credentials)
    //   6. Last-injected dedup so a re-echoed line doesn't double-fire
    static void TryInjectStdinToFocus(
        FlaUI.UIA3.UIA3Automation uia,
        System.Diagnostics.Process proc,
        System.Collections.Concurrent.ConcurrentQueue<string> queue,
        InjectState state)
    {
        if (queue.IsEmpty) return;

        // Control-character fast path (must run before the focus ladder so
        // Ctrl+C still works even if the user is actively typing in another
        // app). Single-char lines carrying ASCII control codes map to app
        // control actions, not text injection.
        if (queue.TryPeek(out var peek) && peek.Length == 1)
        {
            var ch = peek[0];
            if (ch == '\x03') // Ctrl+C from stdin -- request close of adopted app
            {
                queue.TryDequeue(out _);
                Console.Error.WriteLine("[EXEC:INJECT] Ctrl+C from stdin -- CloseMainWindow on adopted pid");
                try { proc.CloseMainWindow(); } catch { }
                state.CloseRequested = true;
                return;
            }
            if (ch == '\x1A') // Ctrl+Z from stdin -- forward as undo keystroke
            {
                // Undo has to go to whatever edit the user (or we) last touched,
                // so ownership check still applies: only fire when focus is in
                // our child process. Otherwise we'd undo in the user's real app.
                try
                {
                    var focusedZ = uia.FocusedElement();
                    uint fpid = 0;
                    try { fpid = (uint)(focusedZ?.Properties.ProcessId.ValueOrDefault ?? 0); } catch { }
                    if (focusedZ == null || fpid != (uint)proc.Id)
                    {
                        Console.Error.WriteLine($"[EXEC:INJECT] Ctrl+Z skip: focus pid={fpid} != child pid={proc.Id}");
                        return;
                    }
                    queue.TryDequeue(out _);
                    Console.Error.WriteLine($"[EXEC:INJECT] Ctrl+Z -> undo on child pid={proc.Id}");
                    try { WKAppBot.Win32.Input.KeyboardInput.Hotkey(new[] { "ctrl", "z" }); } catch { }
                }
                catch { }
                return;
            }
            // Ctrl+A (\x01) intentionally unhandled: ValuePattern.SetValue
            // already replaces the full control value on every injection,
            // so an explicit select-all keystroke would be redundant.
        }

        if (FocusSafe.ShouldYieldToActiveUser(out _)) return;

        try
        {
            var focused = uia.FocusedElement();
            if (focused == null) return;

            uint focusedPid = 0;
            try { focusedPid = (uint)focused.Properties.ProcessId.ValueOrDefault; }
            catch { return; }
            if (focusedPid != (uint)proc.Id) return;

            var ct = focused.Properties.ControlType.ValueOrDefault;
            bool isEditable = ct == FlaUI.Core.Definitions.ControlType.Edit
                           || ct == FlaUI.Core.Definitions.ControlType.Document;
            if (!isEditable) return;

            bool isPassword = false;
            try { isPassword = focused.Properties.IsPassword.ValueOrDefault; } catch { }
            if (isPassword)
            {
                Console.Error.WriteLine("[EXEC:INJECT] BLOCKED password field");
                return;
            }

            if (!queue.TryDequeue(out var line)) return;
            if (line == state.LastInjected) return;

            // Same-content skip: if the focused control already has this text
            // verbatim, don't bother SetValue-ing again. Avoids redundant
            // cursor-resets when a downstream process re-emits the same line.
            try
            {
                var currentVal = focused.Patterns.Value.PatternOrDefault?.Value.ValueOrDefault;
                if (currentVal == line)
                {
                    state.LastInjected = line;
                    return;
                }
            }
            catch { /* can't read current value -- proceed with SetValue */ }

            bool injected = false;
            try
            {
                var pat = focused.Patterns.Value.PatternOrDefault;
                if (pat != null && !pat.IsReadOnly.ValueOrDefault)
                {
                    pat.SetValue(line);
                    injected = true;
                }
            }
            catch { }

            if (!injected)
            {
                Console.Error.WriteLine($"[EXEC:INJECT] skip: no usable ValuePattern on ControlType={ct}");
                return;
            }

            var grap = BuildFocusedGrap(focused);
            Console.Error.WriteLine($"[EXEC:INJECT] {grap} len={line.Length}");
            state.LastInjected = line;
        }
        catch
        {
            // UIA transient on a redrawing window -- drop the tick, keep state
            return;
        }
    }

    // Walk UIA ancestors of a focused element, stopping at the first
    // HWND-backed ancestor (the top-level window) and building the full
    // grap path in the form: `{winGrap}#Seg1/Seg2/.../Focused`.
    // Silent fail-over: if a parent lookup throws, return whatever we have.
    static string BuildFocusedGrap(FlaUI.Core.AutomationElements.AutomationElement focused)
    {
        var segments = new List<string>();
        IntPtr windowHwnd = IntPtr.Zero;
        var el = focused;
        int safety = 0;
        while (el != null && safety++ < 20)
        {
            IntPtr h = IntPtr.Zero;
            try { h = (IntPtr)el.Properties.NativeWindowHandle.ValueOrDefault; } catch { }
            if (h != IntPtr.Zero && windowHwnd == IntPtr.Zero && safety > 1)
            {
                // First HWND ancestor -- this is the window root. Stop here;
                // don't include it in the child-path segments.
                windowHwnd = h;
                break;
            }
            var tag = NodeTag(el);
            if (tag != null) segments.Insert(0, tag);
            try { el = el.Parent; } catch { break; }
        }
        var path = segments.Count > 0 ? string.Join("/", segments) : "?";
        if (windowHwnd != IntPtr.Zero)
        {
            var winGrap = BuildCompactWinGrap(windowHwnd);
            return $"{winGrap}#{path}";
        }
        return $"#{path}";
    }

    // Walk UIA depth-2 and yield each node as a grap suffix:
    //   ControlType_Aid          if AutomationId is set
    //   ControlType_Name         fallback to Name (truncated + sanitized)
    //   ControlType_ClassName    last-resort when nothing identifies the node
    // Parent path (grandparent) is inlined via a leading segment when level 2
    // so users see "Pane_Main/Button_OK" rather than just "Button_OK".
    static IEnumerable<string> EnumA11yDepth2Suffixes(FlaUI.Core.AutomationElements.AutomationElement root)
    {
        FlaUI.Core.AutomationElements.AutomationElement[] children;
        try { children = root.FindAllChildren(); }
        catch { yield break; }

        foreach (var child in children)
        {
            var childTag = NodeTag(child);
            if (childTag != null) yield return childTag;

            FlaUI.Core.AutomationElements.AutomationElement[] grand;
            try { grand = child.FindAllChildren(); }
            catch { continue; }

            foreach (var g in grand)
            {
                var gTag = NodeTag(g);
                if (gTag != null && childTag != null)
                    yield return $"{childTag}/{gTag}";
                else if (gTag != null)
                    yield return gTag;
            }
        }
    }

    static string? NodeTag(FlaUI.Core.AutomationElements.AutomationElement el)
    {
        try
        {
            string ctrl = "?";
            try { ctrl = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
            var aid = el.Properties.AutomationId.ValueOrDefault ?? "";
            var name = el.Properties.Name.ValueOrDefault ?? "";
            var cls = "";
            try { cls = el.Properties.ClassName.ValueOrDefault ?? ""; } catch { }

            string ident =
                !string.IsNullOrWhiteSpace(aid) ? aid
              : !string.IsNullOrWhiteSpace(name) ? TrimForTag(name)
              : !string.IsNullOrWhiteSpace(cls) ? cls
              : "?";
            if (ident == "?" && ctrl == "?") return null;
            return $"{ctrl}_{ident}";
        }
        catch { return null; }
    }

    static string TrimForTag(string s)
    {
        var t = s.Trim();
        if (t.Length > 30) t = t[..27] + "...";
        // Replace characters that break grap parsing
        return t.Replace('/', '_').Replace('#', '_');
    }

    // Enumerate all top-level windows owned by a given pid.
    static List<WindowInfo> EnumerateProcessWindows(uint pid)
    {
        var results = new List<WindowInfo>();
        NativeMethods.EnumWindows((h, _) =>
        {
            if (!NativeMethods.IsWindowVisible(h)) return true;
            NativeMethods.GetWindowThreadProcessId(h, out uint p);
            if (p == pid) results.Add(WindowInfo.FromHwnd(h));
            return true;
        }, IntPtr.Zero);
        return results;
    }

    static bool PatternMatch(string? text, string pattern)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern)) return false;
        var core = pattern.Trim('*');
        if (core.Length == 0) return true;
        return text.Contains(core, StringComparison.OrdinalIgnoreCase);
    }

    // -- Helpers -------------------------------------------------------------

    /// <summary>
    /// Resolve a user-provided executable reference to an absolute exe path.
    /// Accepts: absolute/relative paths that exist, bare names found on %PATH%
    /// with any %PATHEXT% extension (default .exe). Returns null if nothing
    /// resolves -- caller should fall through to a usage error rather than
    /// pretending the arg is a preset key.
    /// Used by RunCommand polymorphic dispatch and ExecCommand ad-hoc fallback
    /// so `wkappbot run notepad` or `wkappbot exec C:/bin/foo.exe` both work.
    /// </summary>
    static string? TryResolveExecutable(string arg)
    {
        if (string.IsNullOrWhiteSpace(arg)) return null;

        // Direct path (absolute or with separator) -- verify + normalize.
        if (arg.Contains('/') || arg.Contains('\\') || Path.IsPathRooted(arg))
        {
            if (File.Exists(arg)) return Path.GetFullPath(arg);
            // Also try appending .exe for rooted bare filename
            if (!arg.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                var withExe = arg + ".exe";
                if (File.Exists(withExe)) return Path.GetFullPath(withExe);
            }
            return null;
        }

        // Bare name -- walk %PATH% and %PATHEXT%
        var pathEnv = Environment.GetEnvironmentVariable("PATH") ?? "";
        var pathExtEnv = Environment.GetEnvironmentVariable("PATHEXT") ?? ".EXE";
        var extensions = pathExtEnv.Split(';', StringSplitOptions.RemoveEmptyEntries);
        bool alreadyHasExt = Path.HasExtension(arg);

        foreach (var dir in pathEnv.Split(';', StringSplitOptions.RemoveEmptyEntries))
        {
            var trimmed = dir.Trim().Trim('"');
            if (string.IsNullOrEmpty(trimmed)) continue;
            // Exact-as-given first
            var direct = Path.Combine(trimmed, arg);
            if (File.Exists(direct)) return Path.GetFullPath(direct);
            // Then try with each PATHEXT extension (skip if user already provided one)
            if (!alreadyHasExt)
            {
                foreach (var ext in extensions)
                {
                    var candidate = Path.Combine(trimmed, arg + ext);
                    if (File.Exists(candidate)) return Path.GetFullPath(candidate);
                }
            }
        }
        return null;
    }

    static string? ScanForExe(string exeName)
    {
        // Well-known system exes (notepad/calc) -- direct System32 lookup
        // before the deep scan.
        var systemDir = Environment.SystemDirectory;
        if (!string.IsNullOrEmpty(systemDir))
        {
            var sys = Path.Combine(systemDir, exeName);
            if (File.Exists(sys)) return sys;
        }

        // Codex CLI lives under %USERPROFILE%\.codex\.sandbox-bin\ (sideloaded
        // install by the Codex VSCode extension, not on PATH by default).
        if (string.Equals(exeName, "codex.exe", StringComparison.OrdinalIgnoreCase))
        {
            var home = Environment.GetEnvironmentVariable("USERPROFILE");
            if (!string.IsNullOrEmpty(home))
            {
                var codexFallback = Path.Combine(home, ".codex", ".sandbox-bin", "codex.exe");
                if (File.Exists(codexFallback)) return codexFallback;
            }
        }

        // Common HTS install roots. Ordered by likelihood.
        var roots = new[]
        {
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
            @"C:\",
        };
        foreach (var root in roots)
        {
            if (string.IsNullOrEmpty(root) || !Directory.Exists(root)) continue;
            try
            {
                // Limit scan depth: HTS installers rarely nest beyond 2 levels
                foreach (var sub in Directory.EnumerateDirectories(root))
                {
                    var direct = Path.Combine(sub, exeName);
                    if (File.Exists(direct)) return direct;
                    try
                    {
                        foreach (var sub2 in Directory.EnumerateDirectories(sub))
                        {
                            var deep = Path.Combine(sub2, exeName);
                            if (File.Exists(deep)) return deep;
                        }
                    }
                    catch { /* ACL / link -- skip */ }
                }
            }
            catch { /* ACL -- skip */ }
        }
        return null;
    }

    static string RunProfileStorePath()
        => Path.Combine(DataDir, "profiles", "run_profiles.json");

    static RunProfileStore LoadRunProfileStore()
    {
        try
        {
            var path = RunProfileStorePath();
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<RunProfileStore>(json) ?? new RunProfileStore();
            }
        }
        catch { }
        return new RunProfileStore();
    }

    static void SaveRunProfileStore(RunProfileStore store)
    {
        try
        {
            var path = RunProfileStorePath();
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, JsonSerializer.Serialize(store, new JsonSerializerOptions { WriteIndented = true }));
        }
        catch (Exception ex) { Console.Error.WriteLine($"[EXEC] Save profile error: {ex.Message}"); }
    }

    static int ExecRegister(string key, string exePath)
    {
        if (!File.Exists(exePath))
        {
            Console.Error.WriteLine($"[EXEC] Exe not found: {exePath}");
            return 1;
        }
        var store = LoadRunProfileStore();
        store.Paths[key.ToLowerInvariant()] = Path.GetFullPath(exePath);
        SaveRunProfileStore(store);
        Console.WriteLine($"[EXEC] Registered: {key} -> {store.Paths[key.ToLowerInvariant()]}");
        return 0;
    }

    static int ExecList()
    {
        var store = LoadRunProfileStore();
        Console.WriteLine("Built-in HTS profiles:");
        foreach (var (key, profile) in _builtInExecProfiles)
        {
            var saved = store.Paths.TryGetValue(key, out var p) ? p : "(not registered)";
            Console.WriteLine($"  {key,-12} {profile.DisplayName,-16} {saved}");
        }
        if (store.Paths.Any(kv => !_builtInExecProfiles.ContainsKey(kv.Key)))
        {
            Console.WriteLine();
            Console.WriteLine("Custom registrations:");
            foreach (var (k, v) in store.Paths.Where(kv => !_builtInExecProfiles.ContainsKey(kv.Key)))
                Console.WriteLine($"  {k,-12} {v}");
        }
        return 0;
    }

    static int ExecUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  wkappbot exec <key>                    Launch managed exe + relay stdio (default: piped)");
        Console.WriteLine("  wkappbot exec <key> --no-pipe          GUI mode: UseShellExecute + Minimized, no stdio relay");
        Console.WriteLine("  wkappbot exec <key> --watch            Stay attached: stream a11y diffs until child exits");
        Console.WriteLine("  wkappbot exec <key> --timeout 1s       Cap the READY wait; caller gets control back early");
        Console.WriteLine("  wkappbot exec <key> --timeout 1s --watch  Short-wait + keep logging in background until child exits");
        Console.WriteLine("  wkappbot exec list                     List built-in keys + registered paths");
        Console.WriteLine("  wkappbot exec --register <key> <exe>   Register a custom exe or override an auto-discovered path");
        Console.WriteLine("  wkappbot run <key>                     Polymorphic alias -- same as `exec <key>` when not a scenario file");
        Console.WriteLine();
        Console.WriteLine("Stages streamed to stderr during launch:");
        Console.WriteLine("  [EXEC] Launching ... / [EXEC] Process spawned / [EXEC] Blocker: ... dismissing");
        Console.WriteLine("  [EXEC] Main window visible / [EXEC] READY after Ns");
        Console.WriteLine("Stdout: single OK / FAIL line for scripting.");
        return 1;
    }
}
