using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ClaudeInstanceState + _instanceStates -> moved to AppBotEyeClaudeStatusStreamer.cs

    static DateTime _lastTickActivityUtc = DateTime.MinValue;
    static DateTime _lastAiActivityUtc = DateTime.MinValue;
    static DateTime _lastAutoGogoUtc = DateTime.MinValue;
    static DateTime _lastKeepAwakeUtc = DateTime.MinValue;
    static string _lastPromptSource = "none";

    static System.Windows.Forms.Form? _screenBlankForm;
#pragma warning disable CS0169
    static ScreenSaverOverlay? _screenSaver;
#pragma warning restore CS0169

    static string _lastPromptSessionFile = "";
    static int _lastPromptLineIndex = -1;
    static int _lastContextPct = -1;  // context usage % for tick display
    static string _lastPromptPreviewCache = "";
    static long _lastPromptSessionLength = -1;
    static DateTime _lastPromptSessionWriteUtc = DateTime.MinValue;

    static string _lastEyeTickFile = "";
    static int _lastEyeTickLineIndex = -1;

    static string _lastPlanSessionFile = "";
    static int _lastPlanLineIndex = -1;
    static long _lastPlanSessionLength = -1;
    static DateTime _lastPlanSessionWriteUtc = DateTime.MinValue;
    static List<string> _lastPlanItemsCache = new();

    static EyeTick? _cachedLatestTick;
    static string _cachedPromptPreview = "";
    static DateTime _cachedPromptFileWriteUtc = DateTime.MinValue;
    static List<EyeParentCard> _cachedCards = new();

    // -- Recovered status positions from Slack (username -> (ts, text)) --
    static readonly Dictionary<string, (string ts, string text)> _recoveredStatusByUsername = new();

    // -- Eye status message ts (앱봇아이 -- edited in place, never idle-deleted) --
    static string? _eyeStatusTs;
    static string? _eyeSummaryReplyTs; // thread reply for card summary
    static string _lastPostedSummary = ""; // change detection: only post when summary differs

    // -- Time-based loop timers --
    static DateTime _lastWatchdogRefresh = DateTime.MinValue;
    static DateTime _lastEyeStatusEdit = DateTime.MinValue;
    // _lastEyeRebumpCheck removed -- rebump disabled (keep initial message position fixed)
    static DateTime _lastCcaAnalysis = DateTime.MinValue;
    static DateTime _lastZoomCleanup = DateTime.MinValue;
    static DateTime _lastSkillAuditUtc = DateTime.MinValue;

    // -- CCA live analysis cache --
#pragma warning disable CS0414
    static string _cachedCcaSummary = "";
#pragma warning restore CS0414

    // -- Dead card + health check --
    static readonly HashSet<int> _reportedDeadPids = new();          // pids we've already alerted for
    static readonly Dictionary<int, string> _cardHealthCache = new(); // pid -> "ok"/"slow"/"dead"
    static string? _eyeBotToken;    // set once Slack creds are loaded
    static string? _eyeChannel;
    static DateTime _lastForceFullLoadUtc = DateTime.MinValue;
    // Card cache: content + changedUtc per card, persisted to disk
    static string _cardCacheDir = "";
    static Dictionary<string, (string content, DateTime changedUtc)> _cardCache = new();
    static string _dirtyTickFile = "";
    static long _dirtyTickLength = -1;
    static DateTime _dirtyTickWriteUtc = DateTime.MinValue;
    static string _dirtyPromptFile = "";
    static long _dirtyPromptLength = -1;
    static DateTime _dirtyPromptWriteUtc = DateTime.MinValue;

    // -- FSW hybrid: event-driven dirty flags (set by FileSystemWatcher callbacks) --
    static volatile bool _fswTickDirty;
    static volatile bool _fswPromptDirty;
    static volatile string? _fswPromptChangedFile; // last changed file name for filtering
    static volatile bool _fswExeDirty; // hot-swap: exe binary changed
    static volatile bool _pendingSwapWhileAdmin; // hot-swap deferred -- admin proxy was busy
    static string? _lastFailedSwapStamp; // mtime stamp of a previously-failed swap -- skip until newer binary
    internal static volatile bool _eyeShutdownRequested; // graceful shutdown via CMD pipe (eye shutdown)
    static volatile bool _slackRetiring; // hot-swap retiring: stop DrainSlackQueue, keep EnqueueSlackRoute
#pragma warning disable CS0169
    static volatile bool _fswClaudeJsonlDirty; // reserved (FSW removed -- kept to avoid refactor)
#pragma warning restore CS0169
    static FileSystemWatcher? _fswTick;
    static FileSystemWatcher? _fswPrompt;
    static FileSystemWatcher? _fswExe;

    // -- WhisperRing watchdog --
    static int _whisperRingPid = 0;       // PID of spawned WhisperRing process (0 = not spawned)
    static int _whisperRingX = 0;
    static int _whisperRingY = 0;
    static DateTime _lastWhisperRingCheck = DateTime.MinValue;
    static FileSystemWatcher? _fswExeNew; // hot-swap: .new.exe staged file
    static FileSystemWatcher? _fswMcp;
#pragma warning disable CS0169
    static FileSystemWatcher? _fswClaudeJsonl; // Claude Code projects JSONL (~/.claude/projects/**/*.jsonl)
#pragma warning restore CS0169
    static readonly HashSet<string> _mcpTabsOpened = new(StringComparer.OrdinalIgnoreCase);
    static readonly HashSet<string> _knownLgOverlayProcesses = new(StringComparer.OrdinalIgnoreCase)
    {
        "LGDisplayExtension",
        "LGHotkeyExtension",
        "LGSmartAssistantExtension",
        "LGSmartAssistant",
        "LGLivelyWallpaper",
        "LG PC Care",
    };
    static DateTime _lastLgOverlayGuardUtc = DateTime.MinValue;
    static nint _lastLgOverlayHwnd;

    // -- Memory tracking --
    static long _prevWorkingSetMB;
    static long _peakWorkingSetMB;

    // Named mutex: signals GlobalMode Eye is running (no WPF window to detect otherwise)
    // LaunchAppBotEyeIfNeededCore checks this to prevent duplicate Eye spawns.
    static System.Threading.Mutex? _eyeRunMutex;

    // -- SupplementCardsFromPrompts: 1s cooldown after last scan --
    // Per-hwnd cache in ClaudePromptHelper makes FindAllPrompts fast for known windows (no UIA rescan).
    // 1s cooldown avoids redundant EnumWindows calls in back-to-back ticks.
    static List<ClaudePromptHelper.PromptInfo>? _cachedAllPrompts;
    /// <summary>Cached appbot master prompt -- always-on relay target (WKAppBot VS Code).</summary>
    internal static ClaudePromptHelper.PromptInfo? CachedAppbotMasterPrompt;

    static bool TryGuardLgOverlay(string logPrefix, string? slackBotToken = null, string? slackChannel = null)
    {
        try
        {
            if (_lastLgOverlayGuardUtc != DateTime.MinValue &&
                (DateTime.UtcNow - _lastLgOverlayGuardUtc).TotalSeconds < 5)
                return false;

            var lgHwnd = FindLgOverlayWindow();
            if (lgHwnd == IntPtr.Zero) return false;

            _lastLgOverlayGuardUtc = DateTime.UtcNow;
            _lastLgOverlayHwnd = lgHwnd;

            var fgBuf = new StringBuilder(256);
            NativeMethods.GetWindowTextW(NativeMethods.GetForegroundWindow(), fgBuf, fgBuf.Capacity);
            var fgTitle = fgBuf.ToString();

            NativeMethods.GetWindowThreadProcessId(lgHwnd, out uint lgPid);
            string procName = "";
            try { procName = lgPid > 0 ? Process.GetProcessById((int)lgPid).ProcessName : ""; } catch { }

            Console.WriteLine($"{logPrefix} LG overlay topmost! pid={lgPid} proc={procName} fg=\"{fgTitle}\"");

            ApplyLgOverlayNeutralize(lgHwnd, logPrefix);

            NativeMethods.PostMessageW(lgHwnd, 0x0010, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine($"{logPrefix} -> WM_CLOSE sent");

            Thread.Sleep(500);
            string result;
            if (NativeMethods.IsWindow(lgHwnd))
            {
                Console.WriteLine($"{logPrefix} WM_CLOSE ignored -> applying shrink fallback");
                ApplyLgOverlayFallbackLayout(lgHwnd, logPrefix);
                Thread.Sleep(250);
                result = "shrunk";

                if (NativeMethods.IsWindow(lgHwnd))
                {
                    result = "kill";
                    Console.WriteLine($"{logPrefix} shrink fallback insufficient -> killing process");
                }

                if (lgPid > 0)
                {
                    try
                    {
                        if (result == "kill")
                        {
                            Process.GetProcessById((int)lgPid).Kill();
                            Console.WriteLine($"{logPrefix} Kill OK (pid={lgPid})");
                        }
                    }
                    catch (Exception kex)
                    {
                        result = $"kill-failed: {kex.Message}";
                        Console.WriteLine($"{logPrefix} Kill failed: {kex.Message}");
                    }
                }
            }
            else
            {
                result = "closed";
                Console.WriteLine($"{logPrefix} overlay closed OK");
            }

            if (!string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
            {
                var alertMsg = $":warning: *{(string.IsNullOrEmpty(procName) ? "LG overlay" : procName)}* screen-cover detected -> {result}\n포그라운드: `{fgTitle}`";
                Task.Run(async () =>
                {
                    try { await SlackSendViaApi(slackBotToken, slackChannel, alertMsg, username: "앱봇아이"); }
                    catch { }
                });
            }

            return true;
        }
        catch { return false; }
    }

    static IntPtr FindLgOverlayWindow()
    {
        IntPtr best = IntPtr.Zero;
        long bestArea = 0;
        var virtualScreen = SystemInformation.VirtualScreen;

        NativeMethods.EnumWindows((hWnd, _) =>
        {
            try
            {
                if (!NativeMethods.IsWindowVisible(hWnd)) return true;

                int exStyle = NativeMethods.GetWindowLongW(hWnd, -20);
                if ((exStyle & 0x8) == 0) return true; // WS_EX_TOPMOST

                NativeMethods.GetWindowRect(hWnd, out var rc);
                int w = Math.Max(0, rc.Right - rc.Left);
                int h = Math.Max(0, rc.Bottom - rc.Top);
                if (w < 600 || h < 300) return true; // ignore tiny LG helper popups

                long area = (long)w * h;
                long minCoverArea = (long)virtualScreen.Width * virtualScreen.Height / 3;
                if (area < minCoverArea) return true; // only large screen-cover candidates

                NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                if (pid == 0) return true;

                string procName;
                try { procName = Process.GetProcessById((int)pid).ProcessName; }
                catch { return true; }
                if (!_knownLgOverlayProcesses.Contains(procName)) return true;

                if (area > bestArea)
                {
                    bestArea = area;
                    best = hWnd;
                }
            }
            catch { }
            return true;
        }, IntPtr.Zero);

        return best;
    }

    static void ApplyLgOverlayNeutralize(IntPtr hWnd, string logPrefix)
    {
        var exStyle = NativeMethods.GetWindowLongW(hWnd, -20);
        var neutralExStyle = exStyle | NativeMethods.WS_EX_LAYERED | NativeMethods.WS_EX_TRANSPARENT;
        NativeMethods.SetWindowLongW(hWnd, -20, neutralExStyle);
        NativeMethods.SetLayeredWindowAttributes(hWnd, 0, 0, NativeMethods.LWA_ALPHA);
        Console.WriteLine($"{logPrefix} -> transparent + click-through");
    }

    static void ApplyLgOverlayFallbackLayout(IntPtr hWnd, string logPrefix)
    {
        try
        {
            NativeMethods.ShowWindow(hWnd, NativeMethods.SW_SHOWMINNOACTIVE);
            Console.WriteLine($"{logPrefix} -> minimize(no-activate)");
            if (!NativeMethods.IsIconic(hWnd))
            {
                NativeMethods.ShowWindow(hWnd, 6); // SW_MINIMIZE
                Console.WriteLine($"{logPrefix} -> force iconic minimize");
            }
        }
        catch { }

        try
        {
            NativeMethods.SetWindowPos(
                hWnd,
                new IntPtr(-2), // HWND_NOTOPMOST
                -32000, -32000, 1, 1,
                0x0010); // SWP_NOACTIVATE
            Console.WriteLine($"{logPrefix} -> offscreen shrink");
        }
        catch { }
    }
    static DateTime _lastFindAllPromptsAt = DateTime.MinValue;

    // -- Eye IPC cache: updated each tick so eye tick IPC queries get instant response --
    static string _cachedIpcSummary = "";
    static string _cachedIpcPromptPreview = "";
    static DateTime _cachedIpcUpdatedAt = DateTime.MinValue;

    /// <summary>Eye's working directory -- set once at startup, used for all child process spawns.</summary>
    internal static string EyeCallerCwd { get; private set; } = "";

    /// <summary>Correct EyeCallerCwd when a caller provides a more accurate (parent) CWD.</summary>
    internal static void SetEyeCallerCwd(string cwd) => EyeCallerCwd = cwd;

    static int EyeGlobalPollingLoop(int width, int height, int posX, int posY, int intervalMs, string callerCwd, bool elevated = false, int replacePid = 0)
    {
        EyeCallerCwd = callerCwd; // store for DrainSlackQueueIfNeeded and HoverAnalyzer
        var eyeDiagFile = Path.Combine(GetEyeLogDir(), $"eye.diag.pid={Environment.ProcessId}.log");
        var hasAnchor = posX >= 0 && posY >= 0;
        void EyeDiag(string step)
        {
            try
            {
                Directory.CreateDirectory(GetEyeLogDir());
                File.AppendAllText(eyeDiagFile, $"{DateTime.Now:HH:mm:ss.fff} {step}{Environment.NewLine}");
            }
            catch { }
        }
        EyeDiag("global-enter");
        if (posX < 0 || posY < 0)
        {
            var (x, y) = GetRightmostMonitorAnchor(width, height);
            posX = x;
            posY = y;
        }

        // -- Duplicate Eye guard (no kill -- mutex-based yielding) --
        // If another Eye with a window already exists and we're NOT a hot-swap replacement,
        // log a warning and let the mutex guard in AppBotEyeCommand handle it.
        // The alive-mutex 5s wait ensures exactly one Eye survives without killing.
        if (replacePid == 0)
        {
            int myPid = Environment.ProcessId;
            var names = new[] { "wkappbot", "wkappbot-core" };
            foreach (var name in names)
            foreach (var proc in Process.GetProcessesByName(name))
            {
                if (proc.Id == myPid) { proc.Dispose(); continue; }
                bool isEye = false;
                try
                {
                    var sb2 = new System.Text.StringBuilder(256);
                    NativeMethods.EnumWindows((hwnd, _) =>
                    {
                        NativeMethods.GetWindowTextW(hwnd, sb2, sb2.Capacity);
                        if (sb2.ToString() == "WK AppBot Eye")
                        {
                            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
                            if ((int)pid == proc.Id) isEye = true;
                        }
                        return !isEye;
                    }, IntPtr.Zero);
                }
                catch { }
                proc.Dispose();
                if (isEye)
                {
                    Console.Error.WriteLine($"[EYE:WARN] ! Existing Eye detected (PID={proc.Id}) -- yielding via mutex (no kill)");
                }
            }
        }
        else
        {
            // Hot-swap 세대교체: 자식(나)이 부모(replacePid)를 인수인계 받는다.
            // 부모가 자연 은퇴하면 OK. 좀비(10초 이상 안 죽음)면 패륜 -- 자식이 부모만 kill.
            _ = Task.Run(async () =>
            {
                // 30s warning -> 10min final grace (triad debate may be running through Eye's pipe)
                await Task.Delay(30_000);
                try
                {
                    using var check = Process.GetProcessById(replacePid);
                    if (!check.HasExited)
                        Console.Error.WriteLine($"[EYE:WARN] ⏰ Parent Eye PID={replacePid} still alive after 30s -- will force-kill in 10min if zombie");
                }
                catch { /* already gone */ }

                await Task.Delay(570_000); // remaining 9.5min
                try
                {
                    using var parent = Process.GetProcessById(replacePid);
                    if (!parent.HasExited)
                    {
                        Console.Error.WriteLine($"[EYE:WARN] ! PATRICIDE: Parent Eye PID={replacePid} didn't retire in 10min -- child PID={Environment.ProcessId} force-killing parent");
                        parent.Kill();
                        EyeColor(ConsoleColor.Yellow);
                        Console.Error.WriteLine($"[EYE:HOT-SWAP] Parent Eye (PID={replacePid}) force-retired by child");
                        EyeResetColor();
                    }
                    else
                    {
                        Console.Error.WriteLine($"[EYE:HOT-SWAP] Parent Eye (PID={replacePid}) retired gracefully -- good succession");
                    }
                }
                catch { /* already exited -- happy path, 효도 성공 */ }
            });
        }

        // Acquire named mutex -- signals to other processes that GlobalMode Eye is running
        _eyeRunMutex = new System.Threading.Mutex(true, @"Global\WKAppBotEyeGlobal", out _);

        // Tee Eye console output to temp log file -> Eye FSW가 apbot-mcp WT 탭으로 표시
        // 파일 기반이므로 WT 탭 닫아도 Eye 동작에 무영향 (브로큰파이프 없음)
        var eyeLogFile = Path.Combine(Path.GetTempPath(), $"wkappbot-eye-{Environment.ProcessId}.log");
        Console.SetOut(new TeeTextWriter(Console.Out, eyeLogFile, moveToOldOnDispose: false));

        // Thread-local console routing: command threads -> pipe StringWriter, Eye threads -> real console
        Console.SetOut(new ThreadRoutingWriter(Console.Out));
        // Start command pipe server -- Launcher delegates commands here (zero cold-start)
        PulseStep.Init("eye-startup");
        EyeCmdPipeServer.StartServer();
        PulseStep.Mark("pipe-server");
        EyeDiag("pipe-server");

        // -- Early Slack connect (parallel with MCP+WPF startup) --
        // Slack ConnectAsync takes 1-3s (WebSocket handshake). Start it NOW so it runs
        // concurrently with MCP worker spawn + WPF host init + schtasks watchdog.
        SlackSocketClient? _earlySlackClient = null;
        string? _earlyAppToken = null, _earlyBotToken = null, _earlyChannel = null;
        Task? _slackConnectBgTask = null;
        {
            var earlyCfg = Path.Combine(DataDir, "profiles", "slack_exp", "webhook.json");
            if (File.Exists(earlyCfg))
            {
                try
                {
                    var earlyJson = JsonNode.Parse(File.ReadAllText(earlyCfg));
                    _earlyAppToken = earlyJson?["app_token"]?.GetValue<string>();
                    _earlyBotToken = earlyJson?["bot_token"]?.GetValue<string>();
                    _earlyChannel  = earlyJson?["channel"]?.GetValue<string>();
                    if (!string.IsNullOrEmpty(_earlyAppToken) && !string.IsNullOrEmpty(_earlyBotToken))
                    {
                        _earlySlackClient = new SlackSocketClient();
                        var capturedClient = _earlySlackClient;
                        var capturedApp = _earlyAppToken;
                        var capturedBot = _earlyBotToken;
                        _slackConnectBgTask = Task.Run(async () =>
                        {
                            try { await capturedClient.ConnectAsync(capturedApp, capturedBot); }
                            catch (Exception ex) { Console.Error.WriteLine($"[EYE] Slack early-connect failed: {ex.Message}"); }
                        });
                        Console.WriteLine("[EYE] Slack: connecting in background (parallel with startup)...");
                    }
                }
                catch (Exception ex) { Console.Error.WriteLine($"[EYE] Slack early-config read failed: {ex.Message}"); }
            }
        }
        PulseStep.Mark("slack-connect-started");

        // Start MCP worker subprocess -- all a11y/UIA operations route through this process
        // Eye stays lean (~80MB), UIA memory (~600MB) stays in the worker
        EyeMcpClient.Start();
        PulseStep.Mark("mcp-client");
        EyeDiag("mcp-client");

        using var host = new AppBotEyeHost();
        EyeDiag("host-start-call");
        host.Start(width, height, posX, posY, ownerHwnd: IntPtr.Zero);
        EyeDiag("host-start-return");
        host.UpdateInfo("global", $"WK AppBot Global Eye {DateTime.Now:HH:mm:ss}");
        EyeDiag("host-update-info");
        host.UpdateAccessibilityText(string.Empty);
        EyeDiag("host-update-empty");
        // Initial border color reflects admin Eye state at startup -- otherwise the
        // window briefly shows blue before the first tick flips it to amber.
        host.SetElevatedBorder(ElevatedEyeClient.Ping(100));

        PulseStep.Mark("host-started");
        EyeDiag("host-started");
        Console.WriteLine("[EYE] Global monitor active -- press Ctrl+C to stop");

        // -- Windows Task Scheduler: dual watchdog structure --
        // 1. Permanent 10-min watchdog (Eye always comes back even if killed)
        // 2. Precise one-shot retry task synced to actual queue (if items exist)
        PulseStep.Mark("watchdog");
        EyeDiag("watchdog-start");
        _ = Task.Run(() =>
        {
            try
            {
                EnsureEyeWatchdogTask();
                EnsureEyeGuardianForWindow(host.GetWindowHandle());
            }
            finally { EyeDiag("watchdog-done"); }
        });
        RouteRetryQueue.ScheduleRetryTask();
        EyeDiag("route-retry-done");
        CheckPreviousCrash();
        EyeDiag("crash-check-done");
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            try
            {
                var crashedDir = Path.Combine(GetEyeLogDir(), "_crashed");
                Directory.CreateDirectory(crashedDir);
                File.WriteAllText(
                    Path.Combine(crashedDir, $"crash-{DateTime.Now:yyyyMMdd-HHmmss}.pid={Environment.ProcessId}.txt"),
                    e.ExceptionObject?.ToString() ?? "unknown");
            }
            catch { }
        };


        // ★ Default: pure focusless mode -- Eye will not steal foreground focus
        // AllowFocusSteal is temporarily enabled for handoff nudges only
        ClaudePromptHelper.AllowFocusSteal = false;
        EyeDiag("focusless-set");
        Console.WriteLine("[EYE] Focusless mode (AllowFocusSteal=OFF by default)");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        // -- Eye pipe server (always: admin UIA proxy + eye tick IPC) --
        _ = Task.Run(() => ElevatedEyeServer.ListenAsync(cts.Token));
        EyeDiag("eye-pipe-started");
        Console.Error.WriteLine($"[EYE] Eye pipe server started (elevated={elevated})");

        PulseStep.Mark("eye-pipe-server");
        // -- Auto a11y hack on InputReadiness probe success --
        // Disabled by default: the hover-driven auto-hack fires on every
        // InputReadiness probe (i.e. on virtually every keystroke / click),
        // which stacks up a11y scans while the user is actively typing and
        // was the dominant source of "focus keeps getting stolen while I
        // type" complaints. Re-enable per-session with WKAPPBOT_EYE_AUTOHACK=1
        // when you actually want the live segment database populated.
        if (Environment.GetEnvironmentVariable("WKAPPBOT_EYE_AUTOHACK") == "1")
        {
            SetupAutoHackOnProbe();
            EyeDiag("autohack-hooked");
        }
        else
        {
            EyeDiag("autohack-disabled");
            Console.Error.WriteLine("[EYE] Auto-hack hover worker disabled (set WKAPPBOT_EYE_AUTOHACK=1 to enable)");
        }
        // -- Mouse CCA: 1s interval -> UIA element + CCA + Visual MD -> Slack thread reply --
        PulseStep.Mark("workers-init");
        StartMouseCcaWorker(cts.Token);
        EyeDiag("mouse-worker-started");
        // -- Keyboard Focus Chain: 1s interval -> focused element + parent chain -> Slack thread reply --
        // FocusChain now handled inside unified MouseCcaWorker (same server process)

        if (_lastTickActivityUtc == DateTime.MinValue) _lastTickActivityUtc = DateTime.UtcNow;
        if (_lastAiActivityUtc == DateTime.MinValue) _lastAiActivityUtc = DateTime.UtcNow;

        // -- Find Claude Desktop window (for plan approval UIA clicks) --
        // Stored in a field so the getter closure below always returns the current value.
        // Re-fetched automatically when stale (Electron restart / window recreation).
        IntPtr claudeHwnd = FindClaudeDesktopWindow();
        EyeDiag($"claude-hwnd=0x{claudeHwnd:X}");
        if (claudeHwnd != IntPtr.Zero)
        {
            EyeColor(ConsoleColor.Cyan);
            Console.Error.WriteLine($"[EYE] Found Claude Desktop (hwnd=0x{claudeHwnd:X8})");
            EyeResetColor();
        }
        // Getter: re-detects if hwnd gone (Claude Desktop restarted or Electron recreated window).
        IntPtr GetCurrentClaudeHwnd()
        {
            if (claudeHwnd != IntPtr.Zero && !WKAppBot.Win32.Native.NativeMethods.IsWindow(claudeHwnd))
            {
                var fresh = FindClaudeDesktopWindow();
                if (fresh != claudeHwnd)
                {
                    claudeHwnd = fresh;
                    if (claudeHwnd != IntPtr.Zero)
                        Console.Error.WriteLine($"[EYE] Claude Desktop re-detected (hwnd=0x{claudeHwnd:X8})");
                }
            }
            return claudeHwnd;
        }

        // -- Slack status streaming (per-instance state -> AppBotEyeClaudeStatusStreamer.cs) --
        var statusTsFile = Path.Combine(DataDir, "runtime", "status_streaming_ts.txt");

        // Previous status message ts -- will be deleted after Slack connects
        string? previousStatusTs = null;
        try
        {
            if (File.Exists(statusTsFile))
            {
                var savedTs = File.ReadAllText(statusTsFile).Trim();
                if (!string.IsNullOrEmpty(savedTs))
                    previousStatusTs = savedTs;
            }
        }
        catch { }

        // -- Claude status tracking --
        string? cachedClaudeStatusText = null;

        // -- Slack Socket Mode daemon (always ON) --
        SlackSocketClient? slackClient = null;
        string? slackBotToken = null;
        string? slackChannel = null;
        var botUsername = BotUsername;

        try
        {
            var configPath = Path.Combine(DataDir, "profiles", "slack_exp", "webhook.json");
            if (File.Exists(configPath))
            {
                var json = JsonNode.Parse(File.ReadAllText(configPath));
                var appToken = json?["app_token"]?.GetValue<string>();
                slackBotToken = json?["bot_token"]?.GetValue<string>();
                slackChannel = json?["channel"]?.GetValue<string>();
                _eyeBotToken = slackBotToken;
                _eyeChannel = slackChannel;

                if (!string.IsNullOrEmpty(appToken) && !string.IsNullOrEmpty(slackBotToken))
                {
                    if (_earlySlackClient != null && _slackConnectBgTask != null)
                    {
                        // Reuse the early-started client -- await completion (usually already done)
                        _slackConnectBgTask.GetAwaiter().GetResult();
                        slackClient = _earlySlackClient;
                    }
                    else
                    {
                        slackClient = new SlackSocketClient();
                        slackClient.ConnectAsync(appToken, slackBotToken).GetAwaiter().GetResult();
                    }
                    EyeColor(ConsoleColor.Green);
                    PulseStep.Mark("slack-connected");
                    var workspace = json?["workspace"]?.GetValue<string>() ?? "?";
                    var channelName = json?["channel_name"]?.GetValue<string>() ?? slackChannel;
                    var maskApp = appToken.Length > 12 ? appToken[..8] + "..." + appToken[^4..] : "***";
                    var maskBot = slackBotToken.Length > 12 ? slackBotToken[..8] + "..." + slackBotToken[^4..] : "***";
                    Console.Error.WriteLine($"[EYE] Slack Socket Mode connected -- workspace={workspace} channel={channelName} app={maskApp} bot={maskBot}");
                    EyeResetColor();

                    // Startup: collect stale status messages (reply_count==0) -- do NOT delete yet.
                    // Deletion happens after first new post succeeds (PostOrUpdateAiStatusAsync),
                    // so the old message stays visible until the new one appears -- no gap.
                    {
                        _staleStatusTsOnStartup.Clear();
                        _recoveredStatusByUsername.Clear();
                        try
                        {
                            using var http = new HttpClient();
                            http.DefaultRequestHeaders.Authorization =
                                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", slackBotToken);
                            var histUrl = $"https://slack.com/api/conversations.history?channel={slackChannel}&limit=30";
                            var histResp = http.GetStringAsync(histUrl).GetAwaiter().GetResult();
                            var histJson = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.Nodes.JsonNode>(histResp);
                            if (histJson?["ok"]?.GetValue<bool>() == true && histJson["messages"] is System.Text.Json.Nodes.JsonArray msgs)
                            {
                                foreach (var m in msgs)
                                {
                                    var text = m?["text"]?.GetValue<string>() ?? "";
                                    var botId = m?["bot_id"]?.GetValue<string>();
                                    var subtype = m?["subtype"]?.GetValue<string>();
                                    if (botId != null || subtype == "bot_message")
                                    {
                                        if (IsStatusEmoji(text))
                                        {
                                            var ts = m?["ts"]?.GetValue<string>();
                                            var replyCount = m?["reply_count"]?.GetValue<int>() ?? 0;
                                            var username = m?["username"]?.GetValue<string>() ?? "";

                                            // Recover: remember latest status ts per username (first hit = newest)
                                            if (ts != null && !string.IsNullOrEmpty(username)
                                                && !_recoveredStatusByUsername.ContainsKey(username))
                                            {
                                                _recoveredStatusByUsername[username] = (ts, text);
                                                Console.Error.WriteLine($"[EYE] Recovered status ts for '{username}': {ts}");
                                            }

                                            // Stale: collect reply-less messages for sweep
                                            // BUT keep the latest per username (already in _recoveredStatusByUsername)
                                            if (ts != null && replyCount == 0)
                                            {
                                                bool isLatestForUser = !string.IsNullOrEmpty(username)
                                                    && _recoveredStatusByUsername.TryGetValue(username, out var latest)
                                                    && latest.ts == ts;
                                                if (!isLatestForUser)
                                                    _staleStatusTsOnStartup.Add(ts);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch { }
                        if (_recoveredStatusByUsername.Count > 0)
                            Console.Error.WriteLine($"[EYE] Recovered {_recoveredStatusByUsername.Count} status position(s) from Slack");
                        if (_staleStatusTsOnStartup.Count > 0)
                        {
                            Console.Error.WriteLine($"[EYE] {_staleStatusTsOnStartup.Count} stale status message(s) pending -- will sweep after first post or 20s timer");
                            // Independent timer: sweep even if first ticks are all idle (no new post fired)
                            _ = SweepStaleOnStartupAsync(slackBotToken!, slackChannel!);
                        }
                        try { File.WriteAllText(statusTsFile, ""); } catch { }
                        previousStatusTs = null;
                    }
                    string? startupTs = null;

                    // Set up event handlers (Slack -> Claude prompt forwarding, plan/permission approval, status streaming)
                    SetupSlackEventHandlers(slackClient, slackBotToken!, slackChannel,
                        GetCurrentClaudeHwnd, GetAnyPlanApprovalTs,
                        GetAnyPermissionTs, startupTs, botUsername,
                        GetAnyInstanceSlackStatusTs, () => ResetAllInstancesSlackStatus(statusTsFile),
                        callerCwd);

                    // Block Kit button handler (plan approve/reject, permission buttons)
                    slackClient.OnBlockAction += (action) =>
                    {
                        EyeColor(ConsoleColor.Cyan);
                        Console.Error.WriteLine($"[EYE][SLACK] Button: {action.ActionId}={action.Value} by {action.UserName}");
                        EyeResetColor();

                        var thread = action.MessageTs ?? GetAnyPlanApprovalTs();

                        var cwHwnd = GetCurrentClaudeHwnd(); // re-detect if stale
                        if (action.ActionId == "plan_approve" && cwHwnd != IntPtr.Zero)
                        {
                            var approved = ClickApproveButton(cwHwnd);
                            var reply = approved
                                ? ":white_check_mark: 플랜 승인 완료! Claude가 코딩을 시작합니다."
                                : ":x: 승인 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                            Task.Run(async () => await SlackSendViaApi(slackBotToken!, action.Channel, reply, thread, username: botUsername))
                                .Wait(5000);

                            if (!string.IsNullOrEmpty(action.ResponseUrl))
                            {
                                var updateText = approved
                                    ? $":white_check_mark: *플랜 승인됨* -- by {action.UserName}"
                                    : ":warning: 승인 시도했으나 버튼을 찾지 못함";
                                Task.Run(async () => await SlackRespondViaUrl(action.ResponseUrl, updateText))
                                    .Wait(3000);
                            }
                        }
                        else if (action.ActionId.StartsWith("perm_") && cwHwnd != IntPtr.Zero)
                        {
                            var buttonText = action.Value;
                            var clicked = ClickPermissionButton(cwHwnd, buttonText);
                            var reply = clicked
                                ? $":white_check_mark: \"{buttonText}\" 클릭 완료!"
                                : $":x: \"{buttonText}\" 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                            Task.Run(async () => await SlackSendViaApi(slackBotToken!, action.Channel, reply, thread, username: botUsername))
                                .Wait(5000);

                            if (!string.IsNullOrEmpty(action.ResponseUrl))
                            {
                                var updateText = clicked
                                    ? $":white_check_mark: *\"{buttonText}\" 처리됨* -- by {action.UserName}"
                                    : $":warning: \"{buttonText}\" 버튼을 찾지 못함";
                                Task.Run(async () => await SlackRespondViaUrl(action.ResponseUrl, updateText))
                                    .Wait(3000);
                            }
                        }
                        else if (action.ActionId == "plan_reject" && cwHwnd != IntPtr.Zero)
                        {
                            var feedbackOk = TypePlanFeedback(cwHwnd, "이 플랜을 거절합니다. 다시 검토해주세요.");
                            var reply = feedbackOk
                                ? ":no_entry_sign: 플랜 거절 피드백을 Claude에 전달했습니다."
                                : ":x: 피드백 입력란을 찾을 수 없습니다";
                            Task.Run(async () => await SlackSendViaApi(slackBotToken!, action.Channel, reply, thread, username: botUsername))
                                .Wait(5000);

                            if (!string.IsNullOrEmpty(action.ResponseUrl))
                            {
                                var updateText = $":no_entry_sign: *플랜 거절됨* -- by {action.UserName}";
                                Task.Run(async () => await SlackRespondViaUrl(action.ResponseUrl, updateText))
                                    .Wait(3000);
                            }
                        }
                    };
                }
                else
                {
                    Console.WriteLine("[EYE] Slack config missing tokens -- Slack disabled");
                }
            }
            else
            {
                Console.WriteLine("[EYE] Slack config not found -- Slack disabled");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE] Slack init error: {ex.Message} -- continuing without Slack");
        }

        // -- Startup: execute overdue schedules (PC reboot recovery) --
        try
        {
            var overdueItems = ScheduleManager.GetDueItems();
            if (overdueItems.Count > 0)
            {
                EyeColor(ConsoleColor.Yellow);
                Console.Error.WriteLine($"[SCHEDULE] {overdueItems.Count} overdue schedule(s) from before restart");
                EyeResetColor();
                Thread.Sleep(5000);
                foreach (var item in overdueItems)
                {
                    ExecuteScheduleItem(item, slackBotToken, slackChannel);
                    Thread.Sleep(2000);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SCHEDULE] Startup recovery error: {ex.Message}");
        }

        // -- Hot-reload: track EXE timestamp --
        var exePath = Process.GetCurrentProcess().MainModule?.FileName ?? "";
        var exeStartTime = File.Exists(exePath) ? File.GetLastWriteTimeUtc(exePath) : DateTime.MinValue;
        bool hotReloadTriggered = false;

        var keepAwakeSw = Stopwatch.StartNew();
        WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
            WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS |
            WKAppBot.Win32.Native.NativeMethods.ES_SYSTEM_REQUIRED |
            WKAppBot.Win32.Native.NativeMethods.ES_DISPLAY_REQUIRED);
        _lastKeepAwakeUtc = DateTime.UtcNow;

        // -- FSW hybrid: event-driven file change detection --
        InitFileWatchers();

        // Startup gentle-swap: if .new.exe already exists (published while Eye was down), apply immediately.
        // Uses shared TryRenameSwap -- Swapped -> enter cleanup mode (hotReloadTriggered before main loop).
        {
            var startupSwap = TryRenameSwap(exePath, "EYE:STARTUP");
            if (startupSwap == HotSwapResult.Swapped)
            {
                Console.WriteLine("[EYE:STARTUP] Swap applied -- entering cleanup mode (will re-launch with new binary)");
                hotReloadTriggered = true;
                // Skip main loop entirely -- go straight to blue-green re-launch + shutdown.
            }
        }

        // Eye 자체 콘솔 탭 오픈 (apbot-mcp 창 최초 생성 + 위치 고정)
        EyeOpenConsoleWtTab(eyeLogFile);

        // -- Whisper Ring: separate process (WPF + audio model -> memory isolated) --
        var eyeStartTime = DateTime.UtcNow;
        try
        {
            var wrPath = Environment.ProcessPath ?? "wkappbot";
            _whisperRingX = Math.Max(0, posX - 190); // WhisperRing 180px wide + 10px gap
            _whisperRingY = posY;
            var wr = AppBotPipe.Spawn(wrPath, $"whisper-ring {_whisperRingX} {_whisperRingY}",
                cwd: callerCwd,
                env: new() { ["WKAPPBOT_PARENT_PID"] = Environment.ProcessId.ToString(), ["WKAPPBOT_WORKER"] = "1" },
                showNoActivate: true,
                caller: "EYE-WHISPER");
            if (wr != null) { _whisperRingPid = wr.Pid; wr.Dispose(); }
            PulseStep.Mark("whisper-spawned");
            Console.Error.WriteLine($"[EYE] Whisper Ring spawned pid={_whisperRingPid}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE] Whisper Ring spawn failed: {ex.Message}");
        }

        // -- Screen Saver: separate process (WPF isolation -> Eye stays lightweight) --
        try
        {
            var ssPath = Environment.ProcessPath ?? "wkappbot";
            AppBotPipe.Spawn(ssPath, "screensaver",
                cwd: callerCwd,
                env: new() { ["WKAPPBOT_PARENT_PID"] = Environment.ProcessId.ToString(), ["WKAPPBOT_WORKER"] = "1" },
                showNoActivate: true,
                caller: "EYE-SCREENSAVER");
            PulseStep.Mark("screensaver-spawned");
            Console.WriteLine("[EYE] ScreenSaver spawned as separate process");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE] ScreenSaver spawn failed: {ex.Message}");
        }

        // -- Claude Proxy: auto-start on port 7788 (ANTHROPIC_BASE_URL passthrough + limit handoff) --
        try
        {
            var proxyPath = Environment.ProcessPath ?? "wkappbot";
            AppBotPipe.Spawn(proxyPath, "claude-proxy --inject-context",
                cwd: callerCwd,
                env: new() { ["WKAPPBOT_PARENT_PID"] = Environment.ProcessId.ToString(), ["WKAPPBOT_WORKER"] = "1" },
                caller: "EYE-CLAUDE-PROXY");
            Console.Error.WriteLine($"[EYE] Claude proxy spawned on port {ProxyDefaultPort}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE] Claude proxy spawn failed: {ex.Message}");
        }

        // -- Context usage monitor (per-card) --
        // Track last warned MB + JSONL path per CWD.
        // Path change = new session (ctime-new file) -> reset MB counter so new session gets fresh warnings.
        var contextWarnedPcts = new Dictionary<string, (int mb, string? path)>(StringComparer.OrdinalIgnoreCase);

        // -- Duplicate Eye self-close: Z-order check every ~10s --
        // EnumWindows enumerates top-level windows top-to-bottom (Z-order).
        // First "WK AppBot Eye" window found = topmost. If that's not me -> I'm behind -> self-close.
        IntPtr myEyeHwnd = IntPtr.Zero;
        int duplicateCheckFrame = 0;

        PulseStep.Done("ready");
        EyeDiag("loop-ready");
        int frameCount = 0;
        Console.WriteLine("[EYE_LOOP] entering main loop");
        while (host.IsAlive && !cts.IsCancellationRequested && !hotReloadTriggered)
        {
            if (frameCount < 3) EyeDiag($"frame-{frameCount}-start");
            if (frameCount < 3) Console.Error.WriteLine($"[EYE_LOOP] frame={frameCount} start");
            // ScreenSaver now runs as separate process -- no Tick() needed in Eye

            // -- Idle self-swap: Eye's job is relay + monitor, not heavy
            //    compute. If a .new.exe is sitting on disk and the pipes are
            //    quiet, it's a good moment to swap quietly without disrupting
            //    callers. Checked once every ~50 frames (~5s). FSW based
            //    triggers still work; this is a backstop that catches files
            //    dropped in while Eye was unwatched or on a flaky FSW.
            if (!_fswExeDirty && (frameCount % 50 == 0))
            {
                try
                {
                    var binDir  = AppContext.BaseDirectory;
                    var liveExe = Path.Combine(binDir, "wkappbot-core.exe");
                    var newExe  = Path.Combine(binDir, "wkappbot-core.new.exe");
                    bool pipeIdle = !ElevatedEyeServer.IsBusy; // admin proxy quiet
                    if (pipeIdle && File.Exists(liveExe) && File.Exists(newExe))
                    {
                        var live = new FileInfo(liveExe);
                        var next = new FileInfo(newExe);
                        if ((next.LastWriteTimeUtc - live.LastWriteTimeUtc).TotalSeconds >= 60)
                        {
                            Console.WriteLine($"[EYE:HOT-SWAP] idle self-swap trigger (.new.exe is {((next.LastWriteTimeUtc - live.LastWriteTimeUtc).TotalMinutes):F0}m newer)");
                            _fswExeDirty = true;
                        }
                    }
                }
                catch { /* diagnostic probe -- never block the loop */ }
            }

            // -- Duplicate Eye check (every 100 frames ≈ 10s) --
            if (++duplicateCheckFrame >= 100)
            {
                duplicateCheckFrame = 0;
                if (myEyeHwnd == IntPtr.Zero) myEyeHwnd = host.GetWindowHandle();
                if (myEyeHwnd != IntPtr.Zero)
                {
                    IntPtr firstEyeHwnd = IntPtr.Zero;
                    var sbDup = new System.Text.StringBuilder(256);
                    NativeMethods.EnumWindows((hwnd, _) =>
                    {
                        NativeMethods.GetWindowTextW(hwnd, sbDup, sbDup.Capacity);
                        if (sbDup.ToString() == "WK AppBot Eye")
                        {
                            firstEyeHwnd = hwnd; // first hit = topmost Eye in Z-order
                            return false; // stop enumeration
                        }
                        return true;
                    }, IntPtr.Zero);

                    if (firstEyeHwnd != IntPtr.Zero && firstEyeHwnd != myEyeHwnd)
                    {
                        EyeColor(ConsoleColor.Yellow);
                        Console.Error.WriteLine($"[EYE] Not topmost Eye (top=0x{firstEyeHwnd:X} me=0x{myEyeHwnd:X}) -- self-closing");
                        EyeResetColor();
                        cts.Cancel();
                        break;
                    }
                }
            }

            // -- Core tick: read ticks + sessions --
            var forceFull = ShouldForceFullLoad();
            var (tickDirty, promptDirty) = CheckGlobalDirtyFlags(forceFull);
            if (frameCount < 3) EyeDiag($"frame-{frameCount}-before-tick forceFull={forceFull} tickDirty={tickDirty} promptDirty={promptDirty}");
            if (frameCount < 3) Console.Error.WriteLine($"[EYE_LOOP] frame={frameCount} before-global-tick forceFull={forceFull} tickDirty={tickDirty} promptDirty={promptDirty}");
            // Skip tick when hot-swap is imminent -- reduces detection latency from ~3s to ~100ms
            if (!_fswExeDirty && !TryRunOneGlobalTick(host, timeoutMs: 3000, forceFull, tickDirty, promptDirty))
            {
                if (frameCount < 3) EyeDiag($"frame-{frameCount}-tick-timeout");
                Console.Error.WriteLine($"[EYE_LOOP] frame={frameCount} global-tick-timeout");
                if (frameCount < 3)
                {
                    Console.Error.WriteLine($"[EYE] tick timeout (>3s) on frame {frameCount} -- startup grace, continuing");
                }
                else
                {
                    Console.WriteLine("[EYE] tick timeout (>3s) - self terminate + restart");
                    hotReloadTriggered = true; // trigger self-restart via hot-swap path
                    break;
                }
            }
            else if (!_fswExeDirty && frameCount < 3)
            {
                EyeDiag($"frame-{frameCount}-tick-ok");
                Console.Error.WriteLine($"[EYE_LOOP] frame={frameCount} global-tick-ok");
            }

            // -- First frame: announce Eye startup to Slack with card summary --
            if (frameCount == 0 && !string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
            {
                try
                {
                    var summary = _cachedIpcSummary;
                    var startMsg = $":large_green_circle: Eye started (PID={Environment.ProcessId})";
                    // Proactively reuse existing Eye status message (by username, no emoji check)
                    var adoptTs = FindEyeStatusTsForReuse(slackBotToken, slackChannel, "앱봇아이");
                    if (adoptTs != null)
                    {
                        // Reuse existing Eye status ts -- update in place, no new post
                        _eyeStatusTs = adoptTs;
                        Console.Error.WriteLine($"[EYE] Startup reuse ts={adoptTs}");
                        SlackUpdateMessageAsync(slackBotToken, slackChannel, adoptTs, startMsg).GetAwaiter().GetResult();
                        // Recover thread replies (1=summary, 2=mouse CCA, 3=focus chain) -- await to prevent race
                        try
                        {
                            var replies = SlackGetThreadRepliesAsync(slackBotToken, slackChannel, adoptTs).GetAwaiter().GetResult();
                            if (replies != null)
                            {
                                var children = replies
                                    .Where(r => r?["ts"]?.GetValue<string>() != adoptTs
                                           && r?["username"]?.GetValue<string>() == "앱봇아이")
                                    .ToList();
                                var r1 = children.Count > 0 ? children[0]?["ts"]?.GetValue<string>() : null;
                                if (r1 != null) { _eyeSummaryReplyTs = r1; Console.Error.WriteLine($"[EYE] Restored summary reply ts={r1}"); }
                                // Restore 앱봇아이 slot ts directly (not deferred to UnifiedMouseFocusLoop -- avoids duplicate creation)
                                var ccaReply = replies.FirstOrDefault(r => r?["ts"]?.GetValue<string>() != adoptTs
                                    && r?["username"]?.GetValue<string>() == "앱봇아이");
                                var r2 = ccaReply?["ts"]?.GetValue<string>();
                                RestoreHoverReplyTs(r2, null);
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        // No existing status found -> post new (expected on first launch)
                        Console.Error.WriteLine("[EYE] Startup: no existing 앱봇아이 msg found -> posting new");
                        var (eyeOk, eyeTs) = SlackSendViaApi(slackBotToken, slackChannel, startMsg, username: "앱봇아이").GetAwaiter().GetResult();
                        if (eyeOk && eyeTs != null)
                        {
                            _eyeStatusTs = eyeTs;
                            // Always post card placeholder as first thread reply [0]
                            // -- guarantees 앱봇아이 always lands at [1], not [0]
                            var cardContent = summary.Length > 0
                                ? "```\n" + summary + "\n```"
                                : "_(loading...)_";
                            var (replyOk, replyTs) = SlackSendViaApi(slackBotToken, slackChannel, cardContent, threadTs: eyeTs, username: "앱봇아이").GetAwaiter().GetResult();
                            if (replyOk && replyTs != null) _eyeSummaryReplyTs = replyTs;
                        }
                    }
                }
                catch { }
            }

            // -- Hot-swap blue-green: first render OK -> old Eye exits on its own (return 0) --
            if (replacePid > 0 && frameCount == 0)
            {
                EyeColor(ConsoleColor.Magenta);
                Console.Error.WriteLine($"[EYE:HOT-SWAP] First render OK -- old Eye (PID={replacePid}) exiting on its own");
                EyeResetColor();
                replacePid = 0;
                // Old Eye does return 0 right after Process.Start -- 1s should be enough
                Thread.Sleep(1000);
                TryDeleteOldExes();
            }

            // -- Hot-swap cleanup: try delete .old.exe every ~10s (non-blocking fallback) --
            if (frameCount % 100 == 50)
                TryDeleteOldExes();

            // -- Claude Desktop status detection (~every 1 sec) --
            // Per-instance JSONL size watermark inside RunClaudeStatusTick handles dedup.
            if (frameCount % 10 == 0)
            {
                cachedClaudeStatusText = RunClaudeStatusTick(
                    ref claudeHwnd, slackBotToken, slackChannel, botUsername,
                    slackClient, statusTsFile, contextWarnedPcts);

                // -- Drain Slack message file queue --
                DrainSlackQueue();
            }

            // -- Skill audit (once per day -- detect stale source_refs, prompt agent via newchat) --
            if ((DateTime.UtcNow - _lastSkillAuditUtc).TotalHours >= 24)
            {
                _lastSkillAuditUtc = DateTime.UtcNow;
                try
                {
                    var cwd = _cachedCards.FirstOrDefault(c => !string.IsNullOrEmpty(c.Cwd))?.Cwd
                              ?? Environment.CurrentDirectory;
                    var issues = RunSkillAuditSilent(cwd);
                    if (issues.Count > 0)
                    {
                        var verifyLines = string.Join("\n", issues.Select(id => $"  wkappbot skill verify {id}"));
                        var prompt = $"[SKILL AUDIT] {issues.Count} skill(s) have stale source_refs.\n"
                                   + verifyLines
                                   + "\nRun `wkappbot skill audit` for details, then fix each skill's source_refs with correct file paths and line numbers.";
                        var cwdFolder = Path.GetFileName(cwd) ?? "";
                        // --when-idle 5s: wait until agent input is visible (not busy with a tool call)
                        EyeMcpClient.CallFireAndForget(["prompt", "send", cwdFolder, prompt, "--when-idle", "5s", "--timeout", "5m"]);
                        Console.Error.WriteLine($"[EYE] Skill audit: {issues.Count} stale skill(s) -- agent prompted via prompt send (--when-idle 5s)");
                    }
                    else
                        Console.Error.WriteLine($"[EYE] Skill audit: all refs OK");
                }
                catch (Exception ex) { Console.Error.WriteLine($"[EYE] Skill audit error: {ex.Message}"); }
            }

            // -- Stale zoom overlay cleanup (every 60s, kill zooms older than 60s) --
            if ((DateTime.UtcNow - _lastZoomCleanup).TotalSeconds >= 60)
            {
                _lastZoomCleanup = DateTime.UtcNow;
                try
                {
                    int cleaned = 0;
                    NativeMethods.EnumWindows((hWnd, _) =>
                    {
                        var buf = new System.Text.StringBuilder(64);
                        NativeMethods.GetWindowTextW(hWnd, buf, buf.Capacity);
                        var title = buf.ToString();
                        if (title is "InputZoom" or "InputHighlight")
                        {
                            NativeMethods.PostMessageW(hWnd, 0x0010, IntPtr.Zero, IntPtr.Zero); // WM_CLOSE
                            cleaned++;
                        }
                        return true;
                    }, IntPtr.Zero);
                    if (cleaned > 0)
                        Console.Error.WriteLine($"[EYE] Cleaned {cleaned} stale zoom overlay(s)");
                }
                catch { }

                // -- LG rogue topmost overlay guard --
                // LG Smart Assistant family occasionally pops a large topmost screen-cover window.
                // Handle by process+window heuristic instead of a single fixed class name.
                TryGuardLgOverlay("[EYE][GUARD]", slackBotToken, slackChannel);
            }

            // -- WhisperRing watchdog: respawn if died + reposition on monitor change (every 60s) --
            if (_whisperRingPid > 0 && (DateTime.UtcNow - _lastWhisperRingCheck).TotalSeconds >= 60)
            {
                _lastWhisperRingCheck = DateTime.UtcNow;
                try
                {
                    // Recalculate position from current monitors (handles monitor add/remove)
                    var (anchorX, anchorY) = GetRightmostMonitorAnchor(width, height);
                    int newWrX = Math.Max(0, anchorX - 190); // Eye 바로 왼쪽 10px gap (whisperRing 180px wide)
                    int newWrY = anchorY;

                    bool alive = false;
                    try { Process.GetProcessById(_whisperRingPid); alive = true; } catch { }

                    bool posChanged = newWrX != _whisperRingX || newWrY != _whisperRingY;
                    if (!alive || posChanged)
                    {
                        if (posChanged && alive)
                        {
                            // Kill old WhisperRing at stale position
                            try { Process.GetProcessById(_whisperRingPid).Kill(); } catch { }
                            Console.Error.WriteLine($"[EYE] WhisperRing monitor changed ({_whisperRingX},{_whisperRingY})->({newWrX},{newWrY}) -- respawning");
                        }
                        else
                            Console.Error.WriteLine($"[EYE] WhisperRing pid={_whisperRingPid} died -- respawning");

                        _whisperRingX = newWrX;
                        _whisperRingY = newWrY;
                        var wrPath = Environment.ProcessPath ?? "wkappbot";
                        var wr = AppBotPipe.Spawn(wrPath, $"whisper-ring {_whisperRingX} {_whisperRingY}",
                            cwd: callerCwd,
                            env: new() { ["WKAPPBOT_PARENT_PID"] = Environment.ProcessId.ToString(), ["WKAPPBOT_WORKER"] = "1" },
                            showNoActivate: true,
                            caller: "EYE-WHISPER-RESPAWN");
                        if (wr != null) { _whisperRingPid = wr.Pid; wr.Dispose(); }
                        Console.Error.WriteLine($"[EYE] WhisperRing respawned pid={_whisperRingPid} at ({_whisperRingX},{_whisperRingY})");
                    }
                }
                catch { }
            }

            // -- Screensaver lifecycle: kill when active, respawn when idle (every 5s) --
            if (frameCount % 50 == 25) // every 5s at 100ms interval
            {
                try
                {
                    var userIdleMs = NativeMethods.GetUserIdleMs();
                    bool ssAlive = false;
                    foreach (var p in Process.GetProcessesByName("wkappbot-core"))
                    {
                        try
                        {
                            var cmd = WKAppBot.Win32.Native.NativeMethods.GetProcessCommandLine(p.Id);
                            if (cmd != null && cmd.Contains("screensaver"))
                            {
                                if (userIdleMs < 3000) // user active -> kill
                                {
                                    p.Kill();
                                    Console.Error.WriteLine($"[EYE] Killed screensaver (PID={p.Id}) -- user active");
                                }
                                else
                                    ssAlive = true;
                            }
                        }
                        catch { }
                    }
                    // Respawn screensaver when idle ≥10s and no screensaver process alive
                    if (!ssAlive && userIdleMs >= 10_000)
                    {
                        var ssPath = Environment.ProcessPath ?? "wkappbot";
                        AppBotPipe.Spawn(ssPath, "screensaver",
                            cwd: callerCwd,
                            env: new() { ["WKAPPBOT_PARENT_PID"] = Environment.ProcessId.ToString(), ["WKAPPBOT_WORKER"] = "1" },
                            showNoActivate: true,
                            caller: "EYE-SCREENSAVER-RESPAWN");
                        Console.WriteLine("[EYE] ScreenSaver respawned (idle ≥10s)");
                    }
                }
                catch { }
            }

            // -- Periodic GC: every 5 min, reduce memory pressure from CCA/UIA workers --
            if (frameCount % 3000 == 1500) // ~5 min at 100ms interval
            {
                var memBefore = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
                GC.Collect(2, GCCollectionMode.Optimized, blocking: false);
                // Log only if memory is high
                if (memBefore > 1024)
                    Console.Error.WriteLine($"[EYE] GC triggered: {memBefore}MB (non-blocking gen2)");
            }

            // -- Schedule executor + Route retry (~every 10 seconds) --
            if (frameCount % 100 == 50)
            {
                try
                {
                    var dueItems = ScheduleManager.GetDueItems();
                    foreach (var dueItem in dueItems)
                        ExecuteScheduleItem(dueItem, slackBotToken, slackChannel);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[SCHEDULE] Error: {ex.Message}");
                }

                // Route retry queue: re-dispatch failed route attempts
                try
                {
                    var retryItems = RouteRetryQueue.GetDueItems();
                    foreach (var item in retryItems)
                    {
                        EyeColor(ConsoleColor.Cyan);
                        Console.Error.WriteLine($"[RETRY] Re-dispatching route: {item[..Math.Min(item.Length, 80)]}...");
                        EyeResetColor();
                        EyeCmdPipeServer.DispatchBg(["slack", "route", item]);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[RETRY] Route retry error: {ex.Message}");
                }
            }

            // -- Keep-awake --
            if (keepAwakeSw.ElapsedMilliseconds >= 60_000)
            {
                keepAwakeSw.Restart();
                WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
                    WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS |
                    WKAppBot.Win32.Native.NativeMethods.ES_SYSTEM_REQUIRED |
                    WKAppBot.Win32.Native.NativeMethods.ES_DISPLAY_REQUIRED);
                _lastKeepAwakeUtc = DateTime.UtcNow;
                Console.WriteLine("[EYE] keep-awake tick");
            }

            // -- Slack reconnect watchdog (~every 5 min) --
            if (slackClient != null && frameCount % 3000 == 2999)
            {
                if (slackClient.IsConnected && slackClient.MessageCount <= 1)
                {
                    EyeColor(ConsoleColor.Yellow);
                    Console.WriteLine("[EYE][SLACK] No events received in 5+ minutes -- attempting reconnect...");
                    EyeResetColor();
                    try
                    {
                        slackClient.ReconnectAsync().GetAwaiter().GetResult();
                        Console.WriteLine("[EYE][SLACK] Reconnected successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"[EYE][SLACK] Reconnect failed: {ex.Message}");
                    }
                }
            }

            // -- Graceful shutdown (eye shutdown command via CMD pipe) --
            if (_eyeShutdownRequested)
            {
                Console.WriteLine("[EYE] Graceful shutdown requested -- exiting");
                WriteEyeCleanExit();
                break;
            }

            // -- Hot-swap: FSW-driven instant detection + blue-green restart --
            // FSW flag checked every frame (~100ms) -- no 5s polling delay
            // Re-trigger pending swap once admin proxy is no longer busy (MCP pattern)
            if (_pendingSwapWhileAdmin && !ElevatedEyeServer.IsBusy)
            {
                _pendingSwapWhileAdmin = false;
                _fswExeDirty = true;
                Console.WriteLine("[EYE:HOT-SWAP] Admin proxy idle -- resuming deferred swap");
            }
            if (_fswExeDirty && exeStartTime != DateTime.MinValue)
            {
                // Defer swap if admin proxy is handling a request (MCP admin-first pattern)
                if (ElevatedEyeServer.IsBusy)
                {
                    _fswExeDirty = false;
                    _pendingSwapWhileAdmin = true;
                    Console.WriteLine("[EYE:HOT-SWAP] Admin proxy busy -- deferring swap");
                    goto AfterHotSwap;
                }
                _fswExeDirty = false; // consume flag
                try
                {
                    // Same-stamp guard: skip if this binary already failed once (MCP pattern)
                    var currentStamp = File.Exists(exePath)
                        ? File.GetLastWriteTimeUtc(exePath).Ticks.ToString() : null;
                    if (currentStamp != null && currentStamp == _lastFailedSwapStamp)
                    {
                        Console.Error.WriteLine($"[EYE:HOT-SWAP] Previously-failed stamp ({currentStamp}) -- waiting for newer binary");
                        goto AfterHotSwap;
                    }

                    var swapResult = TryRenameSwap(exePath, "EYE:HOT-SWAP");
                    if (swapResult == HotSwapResult.Swapped)
                    {
                        _lastFailedSwapStamp = null; // clear on success
                        _slackRetiring = true; // stop draining queue -- new Eye will take over
                        EyeCmdPipeServer.StopAccepting(); // stop accepting new pipe connections immediately
                        EnsureEyeGuardianForWindow(host.GetWindowHandle()); // re-launch guardian for new Eye
                        hotReloadTriggered = true;
                        break;
                    }
                    else if (swapResult == HotSwapResult.NoNewExe && File.GetLastWriteTimeUtc(exePath) != exeStartTime)
                    {
                        // Direct overwrite (exe wasn't locked) -- timestamp changed
                        _lastFailedSwapStamp = null;
                        EyeColor(ConsoleColor.Magenta);
                        Console.WriteLine("[EYE:HOT-SWAP] EXE timestamp changed -- binary updated!");
                        EyeResetColor();
                        _slackRetiring = true;
                        EyeCmdPipeServer.StopAccepting();
                        EnsureEyeGuardianForWindow(host.GetWindowHandle());
                        hotReloadTriggered = true;
                        break;
                    }
                    else if (swapResult == HotSwapResult.Failed)
                    {
                        // Record failed stamp -- don't retry until a newer binary arrives
                        _lastFailedSwapStamp = currentStamp;
                        Console.Error.WriteLine($"[EYE:HOT-SWAP] Swap failed -- recording stamp {currentStamp}");
                    }
                    // Identical: continue main loop (no restart needed)
                }
                catch { }
            }
            AfterHotSwap:;

            // -- Slack socket health check (~every 10 min = 6000 frames @ 100ms) --
            if (frameCount % 6000 == 0 && frameCount > 0 && slackClient != null)
            {
                try
                {
                    var connAge = (DateTime.UtcNow - slackClient.LastConnectedUtc).TotalMinutes;
                    if (!slackClient.IsConnected || connAge >= 10)
                    {
                        EyeColor(ConsoleColor.Yellow);
                        Console.Error.WriteLine($"[EYE][SLACK] Health check: connected={slackClient.IsConnected} age={connAge:F0}m -> force reconnect");
                        EyeResetColor();
                        Task.Run(async () =>
                        {
                            try
                            {
                                await slackClient.ReconnectAsync();
                                Console.WriteLine("[EYE][SLACK] Periodic reconnect OK");
                            }
                            catch (Exception ex)
                            {
                                Console.Error.WriteLine($"[EYE][SLACK] Periodic reconnect failed: {ex.Message}");
                            }
                        }).Wait(10000);
                    }
                    else
                    {
                        Console.Error.WriteLine($"[EYE][SLACK] Health OK: connected={slackClient.IsConnected} age={connAge:F0}m msgs={slackClient.MessageCount} reconnects={slackClient.ReconnectCount}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[EYE][SLACK] Health check error: {ex.Message}");
                }
            }

            // -- Watchdog refresh + Slack heartbeat (every 1 min, time-based) --
            if ((DateTime.UtcNow - _lastWatchdogRefresh).TotalSeconds >= 60)
            {
                _lastWatchdogRefresh = DateTime.UtcNow;
                EnsureEyeWatchdogTask();

                // Slack Socket heartbeat: auth.test API call + reconnect if dead
                if (slackClient != null && !string.IsNullOrEmpty(slackBotToken))
                {
                    bool slackAlive = slackClient.IsConnected;
                    // Active probe: call auth.test to verify token + server reachability
                    if (slackAlive)
                    {
                        try
                        {
                            using var hc = new HttpClient { Timeout = TimeSpan.FromSeconds(8) };
                            hc.DefaultRequestHeaders.Authorization =
                                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", slackBotToken);
                            var resp = hc.GetStringAsync("https://slack.com/api/auth.test").GetAwaiter().GetResult();
                            slackAlive = resp.Contains("\"ok\":true");
                            if (!slackAlive) Console.Error.WriteLine($"[EYE] Slack auth.test failed: {resp[..Math.Min(100, resp.Length)]}");
                        }
                        catch (Exception ex) { slackAlive = false; Console.Error.WriteLine($"[EYE] Slack probe failed: {ex.Message}"); }
                    }
                }
                if (slackClient != null && !slackClient.IsConnected)
                {
                    Console.WriteLine("[EYE] Slack disconnected -- attempting reconnect...");
                    try
                    {
                        slackClient.Dispose();
                        slackClient = new SlackSocketClient();
                        string? appTok = null;
                        try
                        {
                            var cfg = Path.Combine(DataDir, "profiles", "slack_exp", "webhook.json");
                            if (File.Exists(cfg))
                                appTok = System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(cfg))?["app_token"]?.GetValue<string>();
                        }
                        catch { }
                        if (appTok != null && slackBotToken != null)
                        {
                            slackClient.ConnectAsync(appTok, slackBotToken).GetAwaiter().GetResult();
                            SetupSlackEventHandlers(slackClient, slackBotToken!, slackChannel,
                                GetCurrentClaudeHwnd, GetAnyPlanApprovalTs,
                                GetAnyPermissionTs, _eyeStatusTs, botUsername,
                                GetAnyInstanceSlackStatusTs, () => ResetAllInstancesSlackStatus(statusTsFile));
                            Console.WriteLine("[EYE] Slack reconnected!");
                        }
                    }
                    catch (Exception ex) { Console.Error.WriteLine($"[EYE] Slack reconnect failed: {ex.Message}"); }
                }
            }

            // -- Periodic GC (~every 5 min = 3000 frames @ 100ms) --
            if (frameCount % 3000 == 0 && frameCount > 0)
            {
                var beforeMB = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
                GC.Collect(2, GCCollectionMode.Optimized);
                GC.WaitForPendingFinalizers();
                GC.Collect(2, GCCollectionMode.Optimized);
                var afterMB = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
                EyeColor(ConsoleColor.DarkGray);
                Console.Error.WriteLine($"[EYE][GC] Gen2 collect: {beforeMB}MB -> {afterMB}MB (freed {beforeMB - afterMB}MB)");
                EyeResetColor();
            }

            // -- Stats logging --
            if (frameCount % 100 == 0 && frameCount > 0)
            {
                var slackInfo = slackClient != null
                    ? $", Slack={slackClient.IsConnected}, msgs={slackClient.MessageCount}, reconn={slackClient.ReconnectCount}"
                    : "";
                Console.Error.WriteLine($"[EYE] frame #{frameCount} ({(slackClient != null ? "Socket+API" : "API-only")}{slackInfo})");
            }

            // -- Eye status edit (change-based: only when card summary differs, throttled 1s) --
            if (_eyeStatusTs != null && !string.IsNullOrEmpty(slackBotToken)
                && (DateTime.UtcNow - _lastEyeStatusEdit).TotalSeconds >= 1.0)
            {
                var summary = _cachedIpcSummary;
                if (summary != _lastPostedSummary)
                {
                    _lastEyeStatusEdit = DateTime.UtcNow;
                    _lastPostedSummary = summary;
                    try
                    {
                        var uptime = DateTime.UtcNow - eyeStartTime;
                        var memMB = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
                        var mainMsg = $"🟢 Eye alive (PID={Environment.ProcessId}, uptime={uptime.TotalMinutes:F0}m, mem={memMB}MB, frame={frameCount})";

                        // Always update in-place -- keep initial message position fixed
                        _ = SlackUpdateMessageAsync(slackBotToken!, slackChannel!, _eyeStatusTs, mainMsg);

                        // Update summary thread reply
                        if (summary.Length > 0 && _eyeSummaryReplyTs != null)
                            _ = SlackUpdateMessageAsync(slackBotToken!, slackChannel!, _eyeSummaryReplyTs, "```\n" + summary + "\n```");
                        else if (summary.Length > 0 && _eyeSummaryReplyTs == null && _eyeStatusTs != null)
                        {
                            _ = Task.Run(async () =>
                            {
                                var (ok, ts) = await SlackSendViaApi(slackBotToken!, slackChannel!, "```\n" + summary + "\n```", threadTs: _eyeStatusTs, username: "앱봇아이");
                                if (ok && ts != null) _eyeSummaryReplyTs = ts;
                            });
                        }
                    }
                    catch { }
                }
            }

            // -- Self-drift guard: keep Eye's own console anchored unless the user is active --
            if (hasAnchor && frameCount % Math.Max(1, 10_000 / Math.Max(1, intervalMs)) == 0
                && !FocusSafe.ShouldYieldToActiveUser(out _))
            {
                try
                {
                    var hConsole = GetConsoleWindow();
                    if (hConsole != IntPtr.Zero && NativeMethods.GetWindowRect(hConsole, out var rc))
                    {
                        var dx = rc.Left - posX;
                        var dy = rc.Top - posY;
                        if (Math.Abs(dx) > 100 || Math.Abs(dy) > 100)
                        {
                            Console.Error.WriteLine($"[EYE:POS] drift X={dx} Y={dy} -- restoring to ({posX}, {posY})");
                            NativeMethods.SetWindowPos(hConsole, IntPtr.Zero, posX, posY, width, height, 0x0004 | NativeMethods.SWP_NOACTIVATE);
                        }
                    }
                }
                catch { }
            }

            frameCount++;

            // Prevent GC of critical objects used by Slack event handler closures
            GC.KeepAlive(slackClient);
            GC.KeepAlive(slackBotToken);
            GC.KeepAlive(slackChannel);
            Thread.Sleep(Math.Max(100, intervalMs));
        }

        // -- Shutdown announcement --
        if (!string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
        {
            try
            {
                var uptime = DateTime.UtcNow - eyeStartTime;
                var reason = hotReloadTriggered ? "hot-swap" : "shutdown";
                var msg = $"🔴 Eye stopped (PID={Environment.ProcessId}, uptime={uptime.TotalMinutes:F0}m, reason={reason}, frames={frameCount})";
                // Edit main message to show stopped status
                if (_eyeStatusTs != null)
                    SlackUpdateMessageAsync(slackBotToken, slackChannel, _eyeStatusTs, msg).GetAwaiter().GetResult();
                else
                {
                    var (stopOk, _) = SlackSendViaApi(slackBotToken, slackChannel, msg, username: "앱봇아이").GetAwaiter().GetResult();
                    // message_limit fallback handled inside SlackSendViaApi (appends to last 앱봇아이 message)
                }
            }
            catch { }
        }

        // -- Cleanup --
        WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
            WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS);

        // Whisper Ring runs as separate process -- exits on its own

        // ScreenSaver runs as separate process -- exits on its own

        // -- Cleanup all WPF windows owned by this process (prevent zombie) --
        try
        {
            int closed = 0;
            NativeMethods.EnumWindows((hWnd, _) =>
            {
                NativeMethods.GetWindowThreadProcessId(hWnd, out uint wpid);
                if ((int)wpid == Environment.ProcessId)
                {
                    var buf = new System.Text.StringBuilder(64);
                    NativeMethods.GetWindowTextW(hWnd, buf, buf.Capacity);
                    var title = buf.ToString();
                    // Close WPF windows that keep the process alive
                    if (title is "FocuslessWarning" or "InputZoom" or "InputHighlight"
                        or "UserInputWaitOverlay" or "Hidden Window")
                    {
                        NativeMethods.PostMessageW(hWnd, 0x0010, IntPtr.Zero, IntPtr.Zero); // WM_CLOSE
                        closed++;
                    }
                }
                return true;
            }, IntPtr.Zero);
            if (closed > 0) Console.Error.WriteLine($"[EYE] Closed {closed} lingering WPF window(s)");
        }
        catch { }

        // -- Cleanup FSW watchers --
        DisposeFileWatchers();

        if (slackClient != null)
        {
            try
            {
                // Shutdown -- no Slack announcement (reduces channel spam on hot-reload restarts)
                slackClient.Dispose();
            }
            catch { }
        }

        // -- Hot-swap: launch new Eye FIRST (instant), then graceful cleanup --
        if (hotReloadTriggered && File.Exists(exePath))
        {
            try
            {
                // Reconstruct original args + add --replace-pid for blue-green handoff
                var origArgs = Environment.GetCommandLineArgs().Skip(1).ToList();
                // Remove any previous --replace-pid
                for (int ri = origArgs.Count - 1; ri >= 0; ri--)
                    if (origArgs[ri] == "--replace-pid" && ri + 1 < origArgs.Count)
                    { origArgs.RemoveAt(ri + 1); origArgs.RemoveAt(ri); }
                origArgs.Add("--replace-pid");
                origArgs.Add(Environment.ProcessId.ToString());
                var argsStr = string.Join(" ", origArgs.Select(a =>
                    a.Contains(' ') ? $"\"{a}\"" : a));

                EyeColor(ConsoleColor.Magenta);
                Console.Error.WriteLine($"[EYE:HOT-SWAP] Launching new Eye: {Path.GetFileName(exePath)} {argsStr}");
                EyeResetColor();

                PulseStep.Init("HOT-SWAP");
                var newProc = AppBotPipe.Spawn(exePath, argsStr, callerCwd,
                    redirectStdIn: true, redirectStdOut: true, redirectStdErr: true,
                    caller: "EYE-HOTSWAP");
                PulseStep.Line("spawn");
                if (newProc != null)
                {
                    // Close pipe ends immediately -- new Eye writes to its own AllocConsole (hidden).
                    try { newProc.StdIn?.Close(); } catch { }
                    try { newProc.StdOut?.Close(); } catch { }
                    try { newProc.StdErr?.Close(); } catch { }
                }
                PulseStep.Mark("pipes-closed");
                if (newProc != null)
                {
                    // Wait for new Eye's window to appear (pipe server is up by then) before closing old Eye.
                    // Both old+new Eye can listen on the same named pipe (MaxAllowedServerInstances) -- no gap.
                    Console.Error.WriteLine($"[EYE:HOT-SWAP] New Eye PID={newProc.Pid} -- waiting for window...");
                    var hsw = System.Diagnostics.Stopwatch.StartNew();
                    var warnAt = 9.0;
                    while (true)
                    {
                        if (newProc.HasExited || hsw.Elapsed.TotalSeconds >= 30) break;
                        if (hsw.Elapsed.TotalSeconds >= warnAt)
                        {
                            Console.Error.WriteLine($"[EYE:HOT-SWAP] still waiting for new Eye message loop... ({warnAt:F0}s)");
                            warnAt += 9.0;
                        }
                        Thread.Sleep(200);
                    }
                    PulseStep.Line($"new-eye-ready ({hsw.Elapsed.TotalMilliseconds:F0}ms)");
                    // Kill old WhisperRing -- new Eye will spawn its own
                    if (_whisperRingPid > 0) { try { Process.GetProcessById(_whisperRingPid).Kill(); } catch { } }
                    PulseStep.Mark("whisper-killed");
                    try { host.Dispose(); } catch { } // free WPF "WK AppBot Eye" window first (prevents duplicate detection)
                    PulseStep.Mark("wpf-disposed");
                    TryHideConsoleWindow(); // hide console too
                    PulseStep.Mark("console-hidden");
                    EyeMcpClient.Stop();
                    PulseStep.Line("mcp-stopped");
                    EyeCmdPipeServer.StopAcceptingAndWaitForDrain();
                    PulseStep.Line("pipe-drained");
                    Console.WriteLine("[EYE:HOT-SWAP] Pipe drain complete -- old Eye exiting");
                }
                PulseStep.Finish("done");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[EYE:HOT-SWAP] Re-launch failed: {ex.Message}");
            }
        }

        // -- Graceful shutdown --
        cts.Cancel();

        WriteEyeCleanExit();
        Console.WriteLine("[EYE:HOT-SWAP] Old Eye shutting down");
        return 0;
    }

    // -- Hot-swap: reusable rename-swap function ----------------------------------
    // Called from: Eye main loop, Eye startup gentle-swap, `wkappbot hotswap` command.
    // Thread-safe: callers serialize externally (Eye uses _fswExeDirty flag).

    internal enum HotSwapResult { NoNewExe, Identical, Swapped, Failed }

    /// <summary>
    /// Check for {exeName}.new.exe next to exePath and apply rename-swap.
    /// Returns: NoNewExe (nothing to do), Identical (deleted, no-op), Swapped (success), Failed (rollback).
    /// All steps logged to stdout for transparency.
    /// </summary>
    internal static HotSwapResult TryRenameSwap(string exePath, string tag = "HOT-SWAP")
    {
        if (string.IsNullOrEmpty(exePath) || !File.Exists(exePath))
            return HotSwapResult.NoNewExe;

        var exeDir = Path.GetDirectoryName(exePath) ?? "";
        var exeName = Path.GetFileNameWithoutExtension(exePath);
        var newExePath = Path.Combine(exeDir, $"{exeName}.new.exe");

        if (!File.Exists(newExePath))
            return HotSwapResult.NoNewExe;

        var newInfo = new FileInfo(newExePath);
        var curInfo = new FileInfo(exePath);

        // Identical check: same mtime + size = no actual rebuild -> delete .new.exe
        if (newInfo.LastWriteTimeUtc == curInfo.LastWriteTimeUtc && newInfo.Length == curInfo.Length)
        {
            Console.Error.WriteLine($"[{tag}] .new.exe identical (mtime={newInfo.LastWriteTimeUtc:HH:mm:ss}, size={newInfo.Length}) -- deleting");
            try { File.Delete(newExePath); } catch { }
            return HotSwapResult.Identical;
        }

        Console.Error.WriteLine($"[{tag}] .new.exe detected (new={newInfo.Length}b/{newInfo.LastWriteTimeUtc:HH:mm:ss}, cur={curInfo.Length}b/{curInfo.LastWriteTimeUtc:HH:mm:ss}) -- rename-swap");

        // Step 1: running exe -> .old (Windows allows renaming running exe)
        var oldExePath = Path.Combine(exeDir, $"{exeName}.old-{DateTime.Now:yyyyMMdd-HHmm}.exe");
        bool step1 = false;
        try { File.Move(exePath, oldExePath); step1 = true; }
        catch (Exception ex) { Console.Error.WriteLine($"[{tag}] step1 rename->.old failed: {ex.Message}"); }

        // Step 2: .new.exe -> exe (retry up to 4× for deploy file lock)
        bool step2 = false;
        if (step1)
        {
            for (int r = 0; r < 4 && !step2; r++)
            {
                if (r > 0) { Console.Error.WriteLine($"[{tag}] step2 retry {r}/3 (file locked -- waiting 1s)"); Thread.Sleep(1000); }
                try { File.Move(newExePath, exePath); step2 = true; }
                catch (Exception ex) { if (r == 3) Console.Error.WriteLine($"[{tag}] step2 .new->.exe failed: {ex.Message}"); }
            }
            if (!step2)
            {
                Console.Error.WriteLine($"[{tag}] rollback: .old->.exe");
                try { File.Move(oldExePath, exePath); } catch { }
            }
        }

        if (step1 && step2)
        {
            Console.Error.WriteLine($"[{tag}] swap OK (.exe->{Path.GetFileName(oldExePath)}, .new->.exe)");
            return HotSwapResult.Swapped;
        }
        return HotSwapResult.Failed;
    }

}
