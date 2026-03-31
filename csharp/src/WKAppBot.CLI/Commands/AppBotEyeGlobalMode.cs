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
    sealed class EyeParentCard
    {
        public int ParentPid { get; set; }
        public string ParentName { get; set; } = "";
        public string ParentTitle { get; set; } = "";
        public string HostType { get; set; } = "";     // vscode, claude-desktop, codex, cursor, terminal
        public string LastTag { get; set; } = "";
        public string LastStatus { get; set; } = "";
        public DateTime LastTsUtc { get; set; }
        public string Cwd { get; set; } = "";    // working directory for display
    }

    // ClaudeInstanceState + _instanceStates → moved to AppBotEyeClaudeStatusStreamer.cs

    sealed class PromptDiag
    {
        public long StatMs { get; set; }
        public long ReadMs { get; set; }
        public long ScanMs { get; set; }
        public long ParseMs { get; set; }
        public long NormMs { get; set; }
        public long CacheMs { get; set; }
        public string Source { get; set; } = "none";
        public DateTime FileWriteUtc { get; set; } = DateTime.MinValue; // session file mtime
    }

    static DateTime _lastTickActivityUtc = DateTime.MinValue;
    static DateTime _lastAiActivityUtc = DateTime.MinValue;
    static DateTime _lastAutoGogoUtc = DateTime.MinValue;
    static DateTime _lastKeepAwakeUtc = DateTime.MinValue;
    static string _lastPromptSource = "none";

    static System.Windows.Forms.Form? _screenBlankForm;
    static ScreenSaverOverlay? _screenSaver;

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

    // ── Recovered status positions from Slack (username → (ts, text)) ──
    static readonly Dictionary<string, (string ts, string text)> _recoveredStatusByUsername = new();

    // ── Eye status message ts (앱봇아이 — edited in place, never idle-deleted) ──
    static string? _eyeStatusTs;
    static string? _eyeSummaryReplyTs; // thread reply for card summary
    static string _lastPostedSummary = ""; // change detection: only post when summary differs

    // ── Time-based loop timers ──
    static DateTime _lastWatchdogRefresh = DateTime.MinValue;
    static DateTime _lastEyeStatusEdit = DateTime.MinValue;
    // _lastEyeRebumpCheck removed — rebump disabled (keep initial message position fixed)
    static DateTime _lastCcaAnalysis = DateTime.MinValue;
    static DateTime _lastZoomCleanup = DateTime.MinValue;

    // ── CCA live analysis cache ──
    static string _cachedCcaSummary = "";

    // ── Dead card + health check ──
    static readonly HashSet<int> _reportedDeadPids = new();          // pids we've already alerted for
    static readonly Dictionary<int, string> _cardHealthCache = new(); // pid → "ok"/"slow"/"dead"
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

    // ── FSW hybrid: event-driven dirty flags (set by FileSystemWatcher callbacks) ──
    static volatile bool _fswTickDirty;
    static volatile bool _fswPromptDirty;
    static volatile string? _fswPromptChangedFile; // last changed file name for filtering
    static volatile bool _fswExeDirty; // hot-swap: exe binary changed
    static volatile bool _slackRetiring; // hot-swap retiring: stop DrainSlackQueue, keep EnqueueSlackRoute
    static volatile bool _fswClaudeJsonlDirty; // reserved (FSW removed — kept to avoid refactor)
    static FileSystemWatcher? _fswTick;
    static FileSystemWatcher? _fswPrompt;
    static FileSystemWatcher? _fswExe;

    // ── WhisperRing watchdog ──
    static int _whisperRingPid = 0;       // PID of spawned WhisperRing process (0 = not spawned)
    static int _whisperRingX = 0;
    static int _whisperRingY = 0;
    static DateTime _lastWhisperRingCheck = DateTime.MinValue;
    static FileSystemWatcher? _fswExeNew; // hot-swap: .new.exe staged file
    static FileSystemWatcher? _fswMcp;
    static FileSystemWatcher? _fswClaudeJsonl; // Claude Code projects JSONL (~/.claude/projects/**/*.jsonl)
    static readonly HashSet<string> _mcpTabsOpened = new(StringComparer.OrdinalIgnoreCase);

    // ── Memory tracking ──
    static long _prevWorkingSetMB;
    static long _peakWorkingSetMB;

    // Named mutex: signals GlobalMode Eye is running (no WPF window to detect otherwise)
    // LaunchAppBotEyeIfNeededCore checks this to prevent duplicate Eye spawns.
    static System.Threading.Mutex? _eyeRunMutex;

    // ── SupplementCardsFromPrompts: 1s cooldown after last scan ──
    // Per-hwnd cache in ClaudePromptHelper makes FindAllPrompts fast for known windows (no UIA rescan).
    // 1s cooldown avoids redundant EnumWindows calls in back-to-back ticks.
    static List<ClaudePromptHelper.PromptInfo>? _cachedAllPrompts;
    /// <summary>Cached appbot master prompt — always-on relay target (WKAppBot VS Code).</summary>
    internal static ClaudePromptHelper.PromptInfo? CachedAppbotMasterPrompt;
    static DateTime _lastFindAllPromptsAt = DateTime.MinValue;

    // ── Eye IPC cache: updated each tick so eye tick IPC queries get instant response ──
    static string _cachedIpcSummary = "";
    static string _cachedIpcPromptPreview = "";
    static DateTime _cachedIpcUpdatedAt = DateTime.MinValue;

    /// <summary>Eye's working directory — set once at startup, used for all child process spawns.</summary>
    internal static string EyeCallerCwd { get; private set; } = "";

    static int EyeGlobalPollingLoop(int width, int height, int posX, int posY, int intervalMs, string callerCwd, bool elevated = false, int replacePid = 0)
    {
        EyeCallerCwd = callerCwd; // store for DrainSlackQueueIfNeeded and HoverAnalyzer
        if (posX < 0 || posY < 0)
        {
            var (x, y) = GetRightmostMonitorAnchor(width, height);
            posX = x;
            posY = y;
        }

        // ── Duplicate Eye guard (no kill — mutex-based yielding) ──
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
                    Console.Error.WriteLine($"[EYE:WARN] ⚠ Existing Eye detected (PID={proc.Id}) — yielding via mutex (no kill)");
                }
            }
        }
        else
        {
            // Hot-swap 세대교체: 자식(나)이 부모(replacePid)를 인수인계 받는다.
            // 부모가 자연 은퇴하면 OK. 좀비(10초 이상 안 죽음)면 패륜 — 자식이 부모만 kill.
            _ = Task.Run(async () =>
            {
                // 30s warning → 10min final grace (triad debate may be running through Eye's pipe)
                await Task.Delay(30_000);
                try
                {
                    using var check = Process.GetProcessById(replacePid);
                    if (!check.HasExited)
                        Console.Error.WriteLine($"[EYE:WARN] ⏰ Parent Eye PID={replacePid} still alive after 30s — will force-kill in 10min if zombie");
                }
                catch { /* already gone */ }

                await Task.Delay(570_000); // remaining 9.5min
                try
                {
                    using var parent = Process.GetProcessById(replacePid);
                    if (!parent.HasExited)
                    {
                        Console.Error.WriteLine($"[EYE:WARN] ⚠ PATRICIDE: Parent Eye PID={replacePid} didn't retire in 10min — child PID={Environment.ProcessId} force-killing parent");
                        parent.Kill();
                        EyeColor(ConsoleColor.Yellow);
                        Console.WriteLine($"[EYE:HOT-SWAP] Parent Eye (PID={replacePid}) force-retired by child");
                        EyeResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"[EYE:HOT-SWAP] Parent Eye (PID={replacePid}) retired gracefully — good succession");
                    }
                }
                catch { /* already exited — happy path, 효도 성공 */ }
            });
        }

        // Acquire named mutex — signals to other processes that GlobalMode Eye is running
        _eyeRunMutex = new System.Threading.Mutex(true, @"Global\WKAppBotEyeGlobal", out _);

        // Tee Eye console output to temp log file → Eye FSW가 apbot-mcp WT 탭으로 표시
        // 파일 기반이므로 WT 탭 닫아도 Eye 동작에 무영향 (브로큰파이프 없음)
        var eyeLogFile = Path.Combine(Path.GetTempPath(), $"wkappbot-eye-{Environment.ProcessId}.log");
        Console.SetOut(new TeeTextWriter(Console.Out, eyeLogFile, moveToOldOnDispose: false));

        // Thread-local console routing: command threads → pipe StringWriter, Eye threads → real console
        Console.SetOut(new ThreadRoutingWriter(Console.Out));
        // Start command pipe server — Launcher delegates commands here (zero cold-start)
        PulseStep.Init("eye-startup");
        EyeCmdPipeServer.StartServer();
        PulseStep.Mark("pipe-server");

        // ── Early Slack connect (parallel with MCP+WPF startup) ──
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

        // Start MCP worker subprocess — all a11y/UIA operations route through this process
        // Eye stays lean (~80MB), UIA memory (~600MB) stays in the worker
        EyeMcpClient.Start();
        PulseStep.Mark("mcp-client");

        using var host = new AppBotEyeHost();
        host.Start(width, height, posX, posY, ownerHwnd: IntPtr.Zero);
        host.UpdateInfo("global", $"WK AppBot Global Eye {DateTime.Now:HH:mm:ss}");
        host.UpdateAccessibilityText(string.Empty);

        PulseStep.Mark("host-started");
        Console.WriteLine("[EYE] Global monitor active — press Ctrl+C to stop");

        // ── Windows Task Scheduler: dual watchdog structure ──
        // 1. Permanent 10-min watchdog (Eye always comes back even if killed)
        // 2. Precise one-shot retry task synced to actual queue (if items exist)
        PulseStep.Mark("watchdog");
        EnsureEyeWatchdogTask();
        RouteRetryQueue.ScheduleRetryTask();

        // ★ Default: pure focusless mode — Eye will not steal foreground focus
        // AllowFocusSteal is temporarily enabled for handoff nudges only
        ClaudePromptHelper.AllowFocusSteal = false;
        Console.WriteLine("[EYE] Focusless mode (AllowFocusSteal=OFF by default)");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        // ── Eye pipe server (always: admin UIA proxy + eye tick IPC) ──
        _ = Task.Run(() => ElevatedEyeServer.ListenAsync(cts.Token));
        Console.WriteLine($"[EYE] Eye pipe server started (elevated={elevated})");

        PulseStep.Mark("eye-pipe-server");
        // ── Auto a11y hack on InputReadiness probe success ──
        SetupAutoHackOnProbe();
        // ── Mouse CCA: 1s interval → UIA element + CCA + Visual MD → Slack thread reply ──
        PulseStep.Mark("workers-init");
        StartMouseCcaWorker(cts.Token);
        // ── Keyboard Focus Chain: 1s interval → focused element + parent chain → Slack thread reply ──
        // FocusChain now handled inside unified MouseCcaWorker (same server process)

        if (_lastTickActivityUtc == DateTime.MinValue) _lastTickActivityUtc = DateTime.UtcNow;
        if (_lastAiActivityUtc == DateTime.MinValue) _lastAiActivityUtc = DateTime.UtcNow;

        // ── Find Claude Desktop window (for plan approval UIA clicks) ──
        // Stored in a field so the getter closure below always returns the current value.
        // Re-fetched automatically when stale (Electron restart / window recreation).
        IntPtr claudeHwnd = FindClaudeDesktopWindow();
        if (claudeHwnd != IntPtr.Zero)
        {
            EyeColor(ConsoleColor.Cyan);
            Console.WriteLine($"[EYE] Found Claude Desktop (hwnd=0x{claudeHwnd:X8})");
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
                        Console.WriteLine($"[EYE] Claude Desktop re-detected (hwnd=0x{claudeHwnd:X8})");
                }
            }
            return claudeHwnd;
        }

        // ── Slack status streaming (per-instance state → AppBotEyeClaudeStatusStreamer.cs) ──
        var statusTsFile = Path.Combine(DataDir, "runtime", "status_streaming_ts.txt");

        // Previous status message ts — will be deleted after Slack connects
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

        // ── Claude status tracking ──
        string? cachedClaudeStatusText = null;

        // ── Slack Socket Mode daemon (always ON) ──
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
                        // Reuse the early-started client — await completion (usually already done)
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
                    Console.WriteLine($"[EYE] Slack Socket Mode connected — workspace={workspace} channel={channelName} app={maskApp} bot={maskBot}");
                    EyeResetColor();

                    // Startup: collect stale status messages (reply_count==0) — do NOT delete yet.
                    // Deletion happens after first new post succeeds (PostOrUpdateAiStatusAsync),
                    // so the old message stays visible until the new one appears — no gap.
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
                                                Console.WriteLine($"[EYE] Recovered status ts for '{username}': {ts}");
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
                            Console.WriteLine($"[EYE] Recovered {_recoveredStatusByUsername.Count} status position(s) from Slack");
                        if (_staleStatusTsOnStartup.Count > 0)
                        {
                            Console.WriteLine($"[EYE] {_staleStatusTsOnStartup.Count} stale status message(s) pending — will sweep after first post or 20s timer");
                            // Independent timer: sweep even if first ticks are all idle (no new post fired)
                            _ = SweepStaleOnStartupAsync(slackBotToken!, slackChannel!);
                        }
                        try { File.WriteAllText(statusTsFile, ""); } catch { }
                        previousStatusTs = null;
                    }
                    string? startupTs = null;

                    // Set up event handlers (Slack → Claude prompt forwarding, plan/permission approval, status streaming)
                    SetupSlackEventHandlers(slackClient, slackBotToken!, slackChannel,
                        GetCurrentClaudeHwnd, GetAnyPlanApprovalTs,
                        GetAnyPermissionTs, startupTs, botUsername,
                        GetAnyInstanceSlackStatusTs, () => ResetAllInstancesSlackStatus(statusTsFile),
                        callerCwd);

                    // Block Kit button handler (plan approve/reject, permission buttons)
                    slackClient.OnBlockAction += (action) =>
                    {
                        EyeColor(ConsoleColor.Cyan);
                        Console.WriteLine($"[EYE][SLACK] Button: {action.ActionId}={action.Value} by {action.UserName}");
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
                                    ? $":white_check_mark: *플랜 승인됨* — by {action.UserName}"
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
                                    ? $":white_check_mark: *\"{buttonText}\" 처리됨* — by {action.UserName}"
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
                                var updateText = $":no_entry_sign: *플랜 거절됨* — by {action.UserName}";
                                Task.Run(async () => await SlackRespondViaUrl(action.ResponseUrl, updateText))
                                    .Wait(3000);
                            }
                        }
                    };
                }
                else
                {
                    Console.WriteLine("[EYE] Slack config missing tokens — Slack disabled");
                }
            }
            else
            {
                Console.WriteLine("[EYE] Slack config not found — Slack disabled");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] Slack init error: {ex.Message} — continuing without Slack");
        }

        // ── Startup: execute overdue schedules (PC reboot recovery) ──
        try
        {
            var overdueItems = ScheduleManager.GetDueItems();
            if (overdueItems.Count > 0)
            {
                EyeColor(ConsoleColor.Yellow);
                Console.WriteLine($"[SCHEDULE] {overdueItems.Count} overdue schedule(s) from before restart");
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
            Console.WriteLine($"[SCHEDULE] Startup recovery error: {ex.Message}");
        }

        // ── Hot-reload: track EXE timestamp ──
        var exePath = Process.GetCurrentProcess().MainModule?.FileName ?? "";
        var exeStartTime = File.Exists(exePath) ? File.GetLastWriteTimeUtc(exePath) : DateTime.MinValue;
        bool hotReloadTriggered = false;

        var keepAwakeSw = Stopwatch.StartNew();
        WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
            WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS |
            WKAppBot.Win32.Native.NativeMethods.ES_SYSTEM_REQUIRED |
            WKAppBot.Win32.Native.NativeMethods.ES_DISPLAY_REQUIRED);
        _lastKeepAwakeUtc = DateTime.UtcNow;

        // ── FSW hybrid: event-driven file change detection ──
        InitFileWatchers();
        // Eye 자체 콘솔 탭 오픈 (apbot-mcp 창 최초 생성 + 위치 고정)
        EyeOpenConsoleWtTab(eyeLogFile);

        // ── Whisper Ring: separate process (WPF + audio model → memory isolated) ──
        var eyeStartTime = DateTime.UtcNow;
        try
        {
            var wrPath = Environment.ProcessPath ?? "wkappbot";
            _whisperRingX = Math.Max(0, posX - 190);
            _whisperRingY = posY;
            var wr = AppBotPipe.Spawn(wrPath, $"whisper-ring {_whisperRingX} {_whisperRingY}",
                cwd: callerCwd,
                env: new() { ["WKAPPBOT_PARENT_PID"] = Environment.ProcessId.ToString(), ["WKAPPBOT_WORKER"] = "1" },
                caller: "EYE-WHISPER");
            if (wr != null) { _whisperRingPid = wr.Pid; wr.Dispose(); }
            PulseStep.Mark("whisper-spawned");
            Console.WriteLine($"[EYE] Whisper Ring spawned pid={_whisperRingPid}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] Whisper Ring spawn failed: {ex.Message}");
        }

        // ── Screen Saver: separate process (WPF isolation → Eye stays lightweight) ──
        try
        {
            var ssPath = Environment.ProcessPath ?? "wkappbot";
            AppBotPipe.Spawn(ssPath, "screensaver",
                cwd: callerCwd,
                env: new() { ["WKAPPBOT_PARENT_PID"] = Environment.ProcessId.ToString(), ["WKAPPBOT_WORKER"] = "1" },
                caller: "EYE-SCREENSAVER");
            PulseStep.Mark("screensaver-spawned");
            Console.WriteLine("[EYE] ScreenSaver spawned as separate process");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] ScreenSaver spawn failed: {ex.Message}");
        }

        // ── Context usage monitor (per-card) ──
        // Track last warned MB + JSONL path per CWD.
        // Path change = new session (ctime-new file) → reset MB counter so new session gets fresh warnings.
        var contextWarnedPcts = new Dictionary<string, (int mb, string? path)>(StringComparer.OrdinalIgnoreCase);

        // ── Duplicate Eye self-close: Z-order check every ~10s ──
        // EnumWindows enumerates top-level windows top-to-bottom (Z-order).
        // First "WK AppBot Eye" window found = topmost. If that's not me → I'm behind → self-close.
        IntPtr myEyeHwnd = IntPtr.Zero;
        int duplicateCheckFrame = 0;

        PulseStep.Done("ready");
        int frameCount = 0;
        while (host.IsAlive && !cts.IsCancellationRequested)
        {
            // ScreenSaver now runs as separate process — no Tick() needed in Eye

            // ── Duplicate Eye check (every 100 frames ≈ 10s) ──
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
                        Console.WriteLine($"[EYE] Not topmost Eye (top=0x{firstEyeHwnd:X} me=0x{myEyeHwnd:X}) — self-closing");
                        EyeResetColor();
                        cts.Cancel();
                        break;
                    }
                }
            }

            // ── Core tick: read ticks + sessions ──
            var forceFull = ShouldForceFullLoad();
            var (tickDirty, promptDirty) = CheckGlobalDirtyFlags(forceFull);
            if (!TryRunOneGlobalTick(host, timeoutMs: 3000, forceFull, tickDirty, promptDirty))
            {
                if (frameCount < 3)
                {
                    Console.WriteLine($"[EYE] tick timeout (>3s) on frame {frameCount} — startup grace, continuing");
                }
                else
                {
                    Console.WriteLine("[EYE] tick timeout (>3s) - self terminate + restart");
                    hotReloadTriggered = true; // trigger self-restart via hot-swap path
                    break;
                }
            }

            // ── First frame: announce Eye startup to Slack with card summary ──
            if (frameCount == 0 && !string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
            {
                try
                {
                    var summary = _cachedIpcSummary;
                    var startMsg = $":large_green_circle: Eye started (PID={Environment.ProcessId})";
                    // Proactively reuse existing Eye status message (by username, no emoji check)
                    var adoptTs = FindLastMessageTsByAuthor(slackBotToken, slackChannel, null, "앱봇아이");
                    if (adoptTs != null)
                    {
                        // Reuse existing Eye status ts — update in place, no new post
                        _eyeStatusTs = adoptTs;
                        Console.Error.WriteLine($"[EYE] Startup reuse ts={adoptTs}");
                        SlackUpdateMessageAsync(slackBotToken, slackChannel, adoptTs, startMsg).GetAwaiter().GetResult();
                        // Recover thread replies (1=summary, 2=mouse CCA, 3=focus chain) — await to prevent race
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
                                if (r1 != null) { _eyeSummaryReplyTs = r1; Console.WriteLine($"[EYE] Restored summary reply ts={r1}"); }
                                // r2/r3 (mouse CCA, focus chain) handled by UnifiedMouseFocusLoop on startup
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        // No existing status found → post new (expected on first launch)
                        Console.Error.WriteLine("[EYE] Startup: no existing 앱봇아이 msg found → posting new");
                        var (eyeOk, eyeTs) = SlackSendViaApi(slackBotToken, slackChannel, startMsg, username: "앱봇아이").GetAwaiter().GetResult();
                        if (eyeOk && eyeTs != null)
                        {
                            _eyeStatusTs = eyeTs;
                            // Post card summary as first thread reply
                            if (summary.Length > 0)
                            {
                                var (replyOk, replyTs) = SlackSendViaApi(slackBotToken, slackChannel, "```\n" + summary + "\n```", threadTs: eyeTs, username: "앱봇아이").GetAwaiter().GetResult();
                                if (replyOk && replyTs != null) _eyeSummaryReplyTs = replyTs;
                            }
                        }
                    }
                }
                catch { }
            }

            // ── Hot-swap blue-green: first render OK → old Eye exits on its own (return 0) ──
            if (replacePid > 0 && frameCount == 0)
            {
                EyeColor(ConsoleColor.Magenta);
                Console.WriteLine($"[EYE:HOT-SWAP] First render OK — old Eye (PID={replacePid}) exiting on its own");
                EyeResetColor();
                replacePid = 0;
                // Old Eye does return 0 right after Process.Start — 1s should be enough
                Thread.Sleep(1000);
                TryDeleteOldExes();
            }

            // ── Hot-swap cleanup: try delete .old.exe every ~10s (non-blocking fallback) ──
            if (frameCount % 100 == 50)
                TryDeleteOldExes();

            // ── Claude Desktop status detection (~every 1 sec) ──
            // Per-instance JSONL size watermark inside RunClaudeStatusTick handles dedup.
            if (frameCount % 10 == 0)
            {
                cachedClaudeStatusText = RunClaudeStatusTick(
                    ref claudeHwnd, slackBotToken, slackChannel, botUsername,
                    slackClient, statusTsFile, contextWarnedPcts);

                // ── Drain Slack message file queue ──
                DrainSlackQueue();
            }

            // ── Stale zoom overlay cleanup (every 60s, kill zooms older than 60s) ──
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
                        Console.WriteLine($"[EYE] Cleaned {cleaned} stale zoom overlay(s)");
                }
                catch { }

                // ── LGDisplayExtensionWnd rogue topmost overlay guard ──
                // This LG monitor software occasionally pops a full-screen topmost black overlay
                // without user interaction. Auto-close + Slack alert for forensics.
                try
                {
                    var lgHwnd = NativeMethods.FindWindowW("HwndWrapper[LGDisplayExtension.exe;;", null);
                    if (lgHwnd != IntPtr.Zero
                        && (NativeMethods.GetWindowLongW(lgHwnd, -20) & 0x8) != 0) // WS_EX_TOPMOST
                    {
                        var fgBuf = new System.Text.StringBuilder(256);
                        NativeMethods.GetWindowTextW(NativeMethods.GetForegroundWindow(), fgBuf, fgBuf.Capacity);
                        var fgTitle = fgBuf.ToString();
                        Console.WriteLine($"[EYE][GUARD] LGDisplayExtensionWnd topmost! fg=\"{fgTitle}\"");

                        // Step 1: instant transparency
                        var exStyle = NativeMethods.GetWindowLongW(lgHwnd, -20);
                        NativeMethods.SetWindowLongW(lgHwnd, -20, exStyle | NativeMethods.WS_EX_LAYERED);
                        NativeMethods.SetLayeredWindowAttributes(lgHwnd, 0, 0, NativeMethods.LWA_ALPHA);
                        // Step 2: WM_CLOSE
                        NativeMethods.PostMessageW(lgHwnd, 0x0010, IntPtr.Zero, IntPtr.Zero);
                        // Step 3: verify + kill
                        Thread.Sleep(500);
                        if (NativeMethods.IsWindow(lgHwnd))
                        {
                            NativeMethods.GetWindowThreadProcessId(lgHwnd, out uint lgPid);
                            if (lgPid > 0) try { Process.GetProcessById((int)lgPid).Kill(); } catch { }
                            Console.WriteLine($"[EYE][GUARD] WM_CLOSE ignored → killed process");
                        }

                        if (!string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                        {
                            var result = NativeMethods.IsWindow(lgHwnd) ? "킬 완료" : "닫기 완료";
                            var alertMsg = $":warning: *LGDisplayExtension* 검은 장막 감지 → {result}\n포그라운드: `{fgTitle}`";
                            Task.Run(async () =>
                            {
                                try { await SlackSendViaApi(slackBotToken, slackChannel, alertMsg, username: "앱봇아이"); }
                                catch { }
                            });
                        }
                    }
                }
                catch { }
            }

            // ── WhisperRing watchdog: respawn if process died (every 60s) ──
            if (_whisperRingPid > 0 && (DateTime.UtcNow - _lastWhisperRingCheck).TotalSeconds >= 60)
            {
                _lastWhisperRingCheck = DateTime.UtcNow;
                try
                {
                    bool alive = false;
                    try { Process.GetProcessById(_whisperRingPid); alive = true; } catch { }
                    if (!alive)
                    {
                        Console.WriteLine($"[EYE] WhisperRing pid={_whisperRingPid} died — respawning");
                        var wrPath = Environment.ProcessPath ?? "wkappbot";
                        var wr = AppBotPipe.Spawn(wrPath, $"whisper-ring {_whisperRingX} {_whisperRingY}",
                            cwd: callerCwd,
                            env: new() { ["WKAPPBOT_PARENT_PID"] = Environment.ProcessId.ToString(), ["WKAPPBOT_WORKER"] = "1" },
                            caller: "EYE-WHISPER-RESPAWN");
                        if (wr != null) { _whisperRingPid = wr.Pid; wr.Dispose(); }
                        Console.WriteLine($"[EYE] WhisperRing respawned pid={_whisperRingPid}");
                    }
                }
                catch { }
            }

            // ── Screensaver kill: user active → kill screensaver process (every 5s) ──
            // Screensaver self-exit is unreliable — Eye enforces kill from outside.
            if (frameCount % 50 == 25) // every 5s at 100ms interval
            {
                try
                {
                    var userIdleMs = NativeMethods.GetUserIdleMs();
                    if (userIdleMs < 3000) // user active
                    {
                        foreach (var p in Process.GetProcessesByName("wkappbot-core"))
                        {
                            try
                            {
                                var cmd = WKAppBot.Win32.Native.NativeMethods.GetProcessCommandLine(p.Id);
                                if (cmd != null && cmd.Contains("screensaver"))
                                {
                                    p.Kill();
                                    Console.WriteLine($"[EYE] Killed screensaver (PID={p.Id}) — user active");
                                }
                            }
                            catch { }
                        }
                    }
                }
                catch { }
            }

            // ── Periodic GC: every 5 min, reduce memory pressure from CCA/UIA workers ──
            if (frameCount % 3000 == 1500) // ~5 min at 100ms interval
            {
                var memBefore = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
                GC.Collect(2, GCCollectionMode.Optimized, blocking: false);
                // Log only if memory is high
                if (memBefore > 1024)
                    Console.WriteLine($"[EYE] GC triggered: {memBefore}MB (non-blocking gen2)");
            }

            // ── Schedule executor + Route retry (~every 10 seconds) ──
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
                    Console.WriteLine($"[SCHEDULE] Error: {ex.Message}");
                }

                // Route retry queue: re-dispatch failed route attempts
                try
                {
                    var retryItems = RouteRetryQueue.GetDueItems();
                    foreach (var item in retryItems)
                    {
                        EyeColor(ConsoleColor.Cyan);
                        Console.WriteLine($"[RETRY] Re-dispatching route: {item[..Math.Min(item.Length, 80)]}...");
                        EyeResetColor();
                        EyeCmdPipeServer.DispatchBg(["slack", "route", item]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[RETRY] Route retry error: {ex.Message}");
                }
            }

            // ── Keep-awake ──
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

            // ── Slack reconnect watchdog (~every 5 min) ──
            if (slackClient != null && frameCount % 3000 == 2999)
            {
                if (slackClient.IsConnected && slackClient.MessageCount <= 1)
                {
                    EyeColor(ConsoleColor.Yellow);
                    Console.WriteLine("[EYE][SLACK] No events received in 5+ minutes — attempting reconnect...");
                    EyeResetColor();
                    try
                    {
                        slackClient.ReconnectAsync().GetAwaiter().GetResult();
                        Console.WriteLine("[EYE][SLACK] Reconnected successfully");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[EYE][SLACK] Reconnect failed: {ex.Message}");
                    }
                }
            }

            // ── Hot-swap: FSW-driven instant detection + blue-green restart ──
            // FSW flag checked every frame (~100ms) — no 5s polling delay
            if (_fswExeDirty && exeStartTime != DateTime.MinValue)
            {
                _fswExeDirty = false; // consume flag
                try
                {
                    var exeDir = Path.GetDirectoryName(exePath) ?? "";
                    var exeName = Path.GetFileNameWithoutExtension(exePath);
                    var newExePath = Path.Combine(exeDir, $"{exeName}.new.exe");
                    // Timestamped old name — avoids collision with locked ghost-magnifier .old.exe
                    var oldExePath = Path.Combine(exeDir, $"{exeName}.old-{DateTime.Now:yyyyMMdd-HHmm}.exe");

                    if (File.Exists(newExePath))
                    {
                        // Skip if .new.exe is identical to running exe (same mtime + size = no actual rebuild)
                        var newInfo = new FileInfo(newExePath);
                        var curInfo = new FileInfo(exePath);
                        if (newInfo.LastWriteTimeUtc == curInfo.LastWriteTimeUtc && newInfo.Length == curInfo.Length)
                        {
                            Console.WriteLine($"[EYE:HOT-SWAP] .new.exe identical (mtime={newInfo.LastWriteTimeUtc:HH:mm:ss}, size={newInfo.Length}) — skipping");
                            try { File.Delete(newExePath); } catch { }
                            continue;
                        }

                        // .new.exe staged — rename-swap (running exe CAN be renamed on Windows!)
                        EyeColor(ConsoleColor.Magenta);
                        Console.WriteLine("[EYE:HOT-SWAP] .new.exe detected — rename-swap");
                        EyeResetColor();
                        try
                        {
                            // No pre-delete needed — timestamped name is always unique
                            File.Move(exePath, oldExePath);    // running exe → .old-YYYYMMDD-HHmm.exe
                            File.Move(newExePath, exePath);     // .new.exe → wkappbot.exe
                            Console.WriteLine($"[EYE:HOT-SWAP] swap OK (.exe→{Path.GetFileName(oldExePath)}, .new→.exe)");
                            _slackRetiring = true; // stop draining queue — new Eye will take over
                            EyeCmdPipeServer.StopAccepting(); // stop accepting new pipe connections immediately
                            DisableEyeWatchdogTask(); // new Eye will re-enable it
                            hotReloadTriggered = true;
                            break;
                        }
                        catch (Exception swapEx)
                        {
                            Console.WriteLine($"[EYE:HOT-SWAP] swap failed ({swapEx.Message})");
                            // Rollback if partial (exe was moved but .new rename failed)
                            if (!File.Exists(exePath) && File.Exists(oldExePath))
                                try { File.Move(oldExePath, exePath); } catch { }
                        }
                    }
                    else if (File.GetLastWriteTimeUtc(exePath) != exeStartTime)
                    {
                        // Direct overwrite succeeded (exe wasn't locked somehow)
                        EyeColor(ConsoleColor.Magenta);
                        Console.WriteLine("[EYE:HOT-SWAP] EXE timestamp changed — binary updated!");
                        EyeResetColor();
                        _slackRetiring = true;
                        EyeCmdPipeServer.StopAccepting(); // stop accepting new pipe connections immediately
                        DisableEyeWatchdogTask(); // new Eye will re-enable it
                        hotReloadTriggered = true;
                        break;
                    }
                }
                catch { }
            }

            // ── Slack socket health check (~every 10 min = 6000 frames @ 100ms) ──
            if (frameCount % 6000 == 0 && frameCount > 0 && slackClient != null)
            {
                try
                {
                    var connAge = (DateTime.UtcNow - slackClient.LastConnectedUtc).TotalMinutes;
                    if (!slackClient.IsConnected || connAge >= 10)
                    {
                        EyeColor(ConsoleColor.Yellow);
                        Console.WriteLine($"[EYE][SLACK] Health check: connected={slackClient.IsConnected} age={connAge:F0}m → force reconnect");
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
                                Console.WriteLine($"[EYE][SLACK] Periodic reconnect failed: {ex.Message}");
                            }
                        }).Wait(10000);
                    }
                    else
                    {
                        Console.WriteLine($"[EYE][SLACK] Health OK: connected={slackClient.IsConnected} age={connAge:F0}m msgs={slackClient.MessageCount} reconnects={slackClient.ReconnectCount}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[EYE][SLACK] Health check error: {ex.Message}");
                }
            }

            // ── Watchdog refresh + Slack heartbeat (every 1 min, time-based) ──
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
                            using var hc = new HttpClient();
                            hc.DefaultRequestHeaders.Authorization =
                                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", slackBotToken);
                            var resp = hc.GetStringAsync("https://slack.com/api/auth.test").GetAwaiter().GetResult();
                            slackAlive = resp.Contains("\"ok\":true");
                            if (!slackAlive) Console.WriteLine($"[EYE] Slack auth.test failed: {resp[..Math.Min(100, resp.Length)]}");
                        }
                        catch (Exception ex) { slackAlive = false; Console.WriteLine($"[EYE] Slack probe failed: {ex.Message}"); }
                    }
                }
                if (slackClient != null && !slackClient.IsConnected)
                {
                    Console.WriteLine("[EYE] Slack disconnected — attempting reconnect...");
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
                    catch (Exception ex) { Console.WriteLine($"[EYE] Slack reconnect failed: {ex.Message}"); }
                }
            }

            // ── Periodic GC (~every 5 min = 3000 frames @ 100ms) ──
            if (frameCount % 3000 == 0 && frameCount > 0)
            {
                var beforeMB = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
                GC.Collect(2, GCCollectionMode.Optimized);
                GC.WaitForPendingFinalizers();
                GC.Collect(2, GCCollectionMode.Optimized);
                var afterMB = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
                EyeColor(ConsoleColor.DarkGray);
                Console.WriteLine($"[EYE][GC] Gen2 collect: {beforeMB}MB → {afterMB}MB (freed {beforeMB - afterMB}MB)");
                EyeResetColor();
            }

            // ── Stats logging ──
            if (frameCount % 100 == 0 && frameCount > 0)
            {
                var slackInfo = slackClient != null
                    ? $", Slack={slackClient.IsConnected}, msgs={slackClient.MessageCount}, reconn={slackClient.ReconnectCount}"
                    : "";
                Console.WriteLine($"[EYE] frame #{frameCount} ({(slackClient != null ? "Socket+API" : "API-only")}{slackInfo})");
            }

            // ── Eye status edit (change-based: only when card summary differs, throttled 1s) ──
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

                        // Always update in-place — keep initial message position fixed
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

            frameCount++;

            // Prevent GC of critical objects used by Slack event handler closures
            GC.KeepAlive(slackClient);
            GC.KeepAlive(slackBotToken);
            GC.KeepAlive(slackChannel);
            Thread.Sleep(Math.Max(100, intervalMs));
        }

        // ── Shutdown announcement ──
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

        // ── Cleanup ──
        WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
            WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS);

        // Whisper Ring runs as separate process — exits on its own

        // ScreenSaver runs as separate process — exits on its own

        // ── Cleanup all WPF windows owned by this process (prevent zombie) ──
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
            if (closed > 0) Console.WriteLine($"[EYE] Closed {closed} lingering WPF window(s)");
        }
        catch { }

        // ── Cleanup FSW watchers ──
        DisposeFileWatchers();

        if (slackClient != null)
        {
            try
            {
                // Shutdown — no Slack announcement (reduces channel spam on hot-reload restarts)
                slackClient.Dispose();
            }
            catch { }
        }

        // ── Hot-swap: launch new Eye FIRST (instant), then graceful cleanup ──
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
                Console.WriteLine($"[EYE:HOT-SWAP] Launching new Eye: {Path.GetFileName(exePath)} {argsStr}");
                EyeResetColor();

                var newProc = AppBotPipe.Spawn(exePath, argsStr, callerCwd,
                    redirectStdIn: true, redirectStdOut: true, redirectStdErr: true,
                    caller: "EYE-HOTSWAP");
                if (newProc != null)
                {
                    // Close pipe ends immediately — new Eye writes to its own AllocConsole (hidden).
                    try { newProc.StdIn?.Close(); } catch { }
                    try { newProc.StdOut?.Close(); } catch { }
                    try { newProc.StdErr?.Close(); } catch { }
                }
                if (newProc != null)
                {
                    // Wait for new Eye's window to appear (pipe server is up by then) before closing old Eye.
                    // Both old+new Eye can listen on the same named pipe (MaxAllowedServerInstances) — no gap.
                    Console.WriteLine($"[EYE:HOT-SWAP] New Eye PID={newProc.Pid} — waiting for window...");
                    var hsw = System.Diagnostics.Stopwatch.StartNew();
                    var warnAt = 9.0;
                    while (true)
                    {
                        if (newProc.HasExited || hsw.Elapsed.TotalSeconds >= 30) break;
                        if (hsw.Elapsed.TotalSeconds >= warnAt)
                        {
                            Console.WriteLine($"[EYE:HOT-SWAP] still waiting for new Eye message loop... ({warnAt:F0}s)");
                            warnAt += 9.0;
                        }
                        Thread.Sleep(200);
                    }
                    Console.WriteLine($"[EYE:HOT-SWAP] New Eye responding ({hsw.Elapsed.TotalMilliseconds:F0}ms) — freeing old Eye windows");
                    try { host.Dispose(); } catch { } // free WPF "WK AppBot Eye" window first (prevents duplicate detection)
                    TryHideConsoleWindow(); // hide console too
                    Console.WriteLine("[EYE:HOT-SWAP] Stopping MCP worker...");
                    EyeMcpClient.Stop();
                    Console.WriteLine("[EYE:HOT-SWAP] Draining active pipe connections...");
                    EyeCmdPipeServer.StopAcceptingAndWaitForDrain();
                    Console.WriteLine("[EYE:HOT-SWAP] Pipe drain complete — old Eye exiting");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EYE:HOT-SWAP] Re-launch failed: {ex.Message}");
            }
        }

        // ── Graceful shutdown ──
        cts.Cancel();

        Console.WriteLine("[EYE:HOT-SWAP] Old Eye shutting down");
        return 0;
    }

}
