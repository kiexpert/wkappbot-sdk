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
    static volatile bool _fswClaudeJsonlDirty; // reserved (FSW removed — kept to avoid refactor)
    static FileSystemWatcher? _fswTick;
    static FileSystemWatcher? _fswPrompt;
    static FileSystemWatcher? _fswExe;
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
    static DateTime _lastFindAllPromptsAt = DateTime.MinValue;

    // ── Eye IPC cache: updated each tick so eye tick IPC queries get instant response ──
    static string _cachedIpcSummary = "";
    static string _cachedIpcPromptPreview = "";
    static DateTime _cachedIpcUpdatedAt = DateTime.MinValue;

    static int EyeGlobalPollingLoop(int width, int height, int posX, int posY, int intervalMs, bool elevated = false, int replacePid = 0)
    {
        if (posX < 0 || posY < 0)
        {
            var (x, y) = GetRightmostMonitorAnchor(width, height);
            posX = x;
            posY = y;
        }

        // ── Kill duplicate Eye processes ──
        // Sweep all wkappbot/wkappbot-core processes that have an Eye window or hold the Eye mutex.
        // This ensures only one Eye runs at a time regardless of how many were spawned.
        if (replacePid == 0)
        {
            int myPid = Environment.ProcessId;
            var names = new[] { "wkappbot", "wkappbot-core" };
            foreach (var name in names)
            foreach (var proc in Process.GetProcessesByName(name))
            {
                if (proc.Id == myPid) continue;
                bool isEye = false;
                try
                {
                    // Detect Eye: has "WK AppBot Eye" window owned by this PID
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
                if (!isEye) continue;
                try
                {
                    proc.Kill();
                    proc.WaitForExit(3000);
                    EyeColor(ConsoleColor.Yellow);
                    Console.WriteLine($"[EYE] Killed duplicate Eye (PID={proc.Id})");
                    EyeResetColor();
                }
                catch { /* already exited or access denied */ }
                finally { proc.Dispose(); }
            }
        }
        else
        {
            // Hot-swap start: old Eye should self-exit (cts.Cancel), but if it goes zombie,
            // force-kill after 1 minute. Fire-and-forget — does not block startup.
            _ = Task.Run(async () =>
            {
                await Task.Delay(60_000);
                try
                {
                    using var old = Process.GetProcessById(replacePid);
                    if (!old.HasExited)
                    {
                        old.Kill();
                        EyeColor(ConsoleColor.Yellow);
                        Console.WriteLine($"[EYE:HOT-SWAP] Force-killed zombie old Eye (PID={replacePid}) after 1min grace");
                        EyeResetColor();
                    }
                }
                catch { /* already exited — happy path */ }
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
        EyeCmdPipeServer.StartServer();

        using var host = new AppBotEyeHost();
        host.Start(width, height, posX, posY, ownerHwnd: IntPtr.Zero);
        host.UpdateInfo("global", $"WK AppBot Global Eye {DateTime.Now:HH:mm:ss}");
        host.UpdateAccessibilityText(string.Empty);

        Console.WriteLine("[EYE] Global monitor active — press Ctrl+C to stop");

        // ── Windows Task Scheduler: dual watchdog structure ──
        // 1. Permanent 10-min watchdog (Eye always comes back even if killed)
        // 2. Precise one-shot retry task synced to actual queue (if items exist)
        EnsureEyeWatchdogTask(); // initial: eye tick in 5 min (pushed forward every ~1 min in loop)
        RouteRetryQueue.ScheduleRetryTask(); // precise one-shot retry task

        // ★ Default: pure focusless mode — Eye will not steal foreground focus
        // AllowFocusSteal is temporarily enabled for handoff nudges only
        ClaudePromptHelper.AllowFocusSteal = false;
        Console.WriteLine("[EYE] Focusless mode (AllowFocusSteal=OFF by default)");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        // ── Eye pipe server (always: admin UIA proxy + eye tick IPC) ──
        _ = Task.Run(() => ElevatedEyeServer.ListenAsync(cts.Token));
        Console.WriteLine($"[EYE] Eye pipe server started (elevated={elevated})");

        // ── Auto a11y hack on InputReadiness probe success ──
        SetupAutoHackOnProbe();
        // ── Mouse CCA: 1s interval → UIA element + CCA + Visual MD → Slack thread reply ──
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
                    slackClient = new SlackSocketClient();
                    slackClient.ConnectAsync(appToken, slackBotToken).GetAwaiter().GetResult();
                    EyeColor(ConsoleColor.Green);
                    Console.WriteLine("[EYE] Slack Socket Mode connected (GlobalMode)");
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
                                        var statusRx = System.Text.RegularExpressions.Regex.Match(text,
                                            @"^(:zzz:|:runner:|:gear:|:clipboard:|:memo:|:warning:|:robot_face:)");
                                        if (statusRx.Success)
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
                        GetAnyInstanceSlackStatusTs, () => ResetAllInstancesSlackStatus(statusTsFile));

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

        // ── Whisper Spectrum Ring (always-on mic → radial HUD overlay) ──
        var eyeStartTime = DateTime.UtcNow; // gate: auto-study allowed only after 10 min
        WhisperEngine? whisperEngine = null;
        WhisperRingHost? whisperRing = null;
        WhisperExperienceDb? whisperExp = null;
        try
        {
            whisperEngine = new WhisperEngine();
            if (whisperEngine.Start())
            {
                whisperRing = new WhisperRingHost();
                // Position: left of Eye window (Eye is at top-right corner)
                int ringX = Math.Max(0, posX - 190);
                int ringY = posY;
                whisperRing.Start(ringX, ringY);

                // Experience DB: token logging + STT auto-labeling
                whisperExp = new WhisperExperienceDb();
                whisperExp.StartLogging();
                bool sttOk = whisperExp.StartStt();

                // Auto-study: when _unknown/ reaches 10 files, run study in background
                // Gate: skip for first 2 min after Eye starts, then enforce 2-min minimum interval
                var expRef = whisperExp; // capture for closure
                DateTime lastStudyTime = DateTime.MinValue;
                const double StudyGateMinutes = 2.0; // was 10 — 5x faster now
                whisperExp.OnAutoStudyNeeded += (count) =>
                {
                    var now = DateTime.UtcNow;
                    if ((now - eyeStartTime).TotalMinutes < StudyGateMinutes)
                    {
                        Console.WriteLine($"[WHISPER] Auto-study deferred (Eye started {(now - eyeStartTime).TotalMinutes:F1} min ago, wait {StudyGateMinutes} min)");
                        expRef.NotifyAutoStudyDone();
                        return;
                    }
                    if ((now - lastStudyTime).TotalMinutes < StudyGateMinutes)
                    {
                        Console.WriteLine($"[WHISPER] Auto-study deferred (last study {(now - lastStudyTime).TotalMinutes:F1} min ago, wait {StudyGateMinutes} min)");
                        expRef.NotifyAutoStudyDone();
                        return;
                    }
                    lastStudyTime = now;
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        try
                        {
                            EyeColor(ConsoleColor.Magenta);
                            Console.WriteLine($"[WHISPER] Auto-study triggered: {count} files in _unknown/");
                            EyeResetColor();
                            WhisperStudyCommand(["--batch", count.ToString()]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[WHISPER] Auto-study error: {ex.Message}");
                        }
                        finally
                        {
                            expRef.NotifyAutoStudyDone();
                        }
                    });
                };

                whisperEngine.OnFrame += (frame) =>
                {
                    if (whisperRing.IsAlive)
                    {
                        var (lastStt, lastSttTicks, lastSttMode, _) = whisperExp?.GetStatus() ?? (null, 0, "QUIET", 0);
                        long ageTicks = lastStt != null ? DateTime.UtcNow.Ticks - lastSttTicks : long.MaxValue;
                        int segFrames = whisperExp?.SegmentFrames ?? 0;
                        whisperRing.UpdateSpectrum(frame.Levels, frame.MaxLevel,
                            frame.Mode, frame.Token, frame.RecentTokens, lastStt, ageTicks, lastSttMode,
                            segFrames, frame.SoundCode, frame.VoiceLevels);
                    }
                    whisperExp?.LogFrame(frame);
                };

                // Mic PCM → parallel MP3 recording for Gemini STT
                // Align channel count FIRST so LameMP3FileWriter uses correct WaveFormat
                whisperExp?.SetMicChannels(whisperEngine.Channels);
                whisperEngine.OnMicData += (buf, len) => whisperExp?.WriteMicData(buf, len);

                // Mic segment ready → move to _unknown/ for batch Gemini STT (no real-time processing)
                whisperExp!.OnMicSegmentReady += (mp3Path) =>
                {
                    try
                    {
                        var unknownDir = Path.Combine(Path.GetDirectoryName(mp3Path)!, "..", "_unknown");
                        Directory.CreateDirectory(unknownDir);
                        var dest = Path.Combine(unknownDir, Path.GetFileName(mp3Path));
                        File.Move(mp3Path, dest);
                    }
                    catch { /* best effort */ }
                };

                EyeColor(ConsoleColor.Magenta);
                Console.WriteLine($"[EYE] Whisper Ring started at ({ringX},{ringY})");
                Console.WriteLine($"[EYE] Whisper ExpDB: logging=ON stt={( sttOk ? "ON" : "OFF" )}");
                EyeResetColor();
            }
            else
            {
                EyeColor(ConsoleColor.DarkGray);
                Console.WriteLine("[EYE] Whisper Ring skipped (no microphone)");
                EyeResetColor();
                whisperEngine.Dispose();
                whisperEngine = null;
            }
        }
        catch (Exception ex)
        {
            EyeColor(ConsoleColor.DarkGray);
            Console.WriteLine($"[EYE] Whisper Ring init failed: {ex.Message}");
            EyeResetColor();
            whisperEngine?.Dispose();
            whisperEngine = null;
        }

        // ── Screen Saver: separate process (WPF isolation → Eye stays lightweight) ──
        try
        {
            var ssPath = Environment.ProcessPath ?? "wkappbot";
            var ssPsi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = ssPath,
                Arguments = "screensaver",
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            System.Diagnostics.Process.Start(ssPsi);
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
                    var startMsg = $"🟢 Eye started (PID={Environment.ProcessId})";
                    var (eyeOk, eyeTs) = SlackSendViaApi(slackBotToken, slackChannel, startMsg, username: "앱봇아이").GetAwaiter().GetResult();
                    if (eyeOk && eyeTs != null)
                    {
                        _eyeStatusTs = eyeTs;
                        // Post card summary as thread reply
                        if (summary.Length > 0)
                        {
                            var (replyOk, replyTs) = SlackSendViaApi(slackBotToken, slackChannel, summary, threadTs: eyeTs, username: "앱봇아이").GetAwaiter().GetResult();
                            if (replyOk && replyTs != null) _eyeSummaryReplyTs = replyTs;
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
                cachedClaudeStatusText = RunClaudeStatusTick(
                    ref claudeHwnd, slackBotToken, slackChannel, botUsername,
                    slackClient, statusTsFile, contextWarnedPcts);

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

            // ── Watchdog refresh (every 1 min, time-based) ──
            if ((DateTime.UtcNow - _lastWatchdogRefresh).TotalSeconds >= 60)
            {
                _lastWatchdogRefresh = DateTime.UtcNow;
                EnsureEyeWatchdogTask();
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
                        _ = SlackUpdateMessageAsync(slackBotToken!, slackChannel!, _eyeStatusTs, mainMsg);
                        // Update summary thread reply
                        if (summary.Length > 0 && _eyeSummaryReplyTs != null)
                            _ = SlackUpdateMessageAsync(slackBotToken!, slackChannel!, _eyeSummaryReplyTs, summary);
                        else if (summary.Length > 0 && _eyeSummaryReplyTs == null && _eyeStatusTs != null)
                        {
                            _ = Task.Run(async () =>
                            {
                                var (ok, ts) = await SlackSendViaApi(slackBotToken!, slackChannel!, summary, threadTs: _eyeStatusTs, username: "앱봇아이");
                                if (ok && ts != null) _eyeSummaryReplyTs = ts;
                            });
                        }
                    }
                    catch { }
                }
            }

            frameCount++;
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
                    SlackSendViaApi(slackBotToken, slackChannel, msg, username: "앱봇아이").GetAwaiter().GetResult();
            }
            catch { }
        }

        // ── Cleanup ──
        WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
            WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS);

        // ── Cleanup Whisper Ring + ExpDB ──
        if (whisperEngine != null)
        {
            whisperExp?.Stop();
            whisperEngine.Dispose();
            whisperRing?.BeginFadeOut();
            Thread.Sleep(1200);
            whisperRing?.Dispose();
        }

        // ScreenSaver runs as separate process — exits on its own

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

                var psi = new ProcessStartInfo
                {
                    FileName = exePath,  // always original path (rename-swap already done)
                    Arguments = argsStr,
                    UseShellExecute        = false, // inherit admin token from parent
                    CreateNoWindow         = true,  // Eye is a daemon — no console window
                    RedirectStandardInput  = true,  // detach from parent's stdin/stdout/stderr
                    RedirectStandardOutput = true,
                    RedirectStandardError  = true,
                };
                var newProc = Process.Start(psi);
                if (newProc != null)
                {
                    // Close pipe ends immediately — new Eye writes to its own AllocConsole (hidden).
                    try { newProc.StandardInput.Close(); } catch { }
                    try { newProc.StandardOutput.Close(); } catch { }
                    try { newProc.StandardError.Close(); } catch { }
                }
                if (newProc != null)
                {
                    // Wait for new Eye's window to appear (pipe server is up by then) before closing old Eye.
                    // Both old+new Eye can listen on the same named pipe (MaxAllowedServerInstances) — no gap.
                    Console.WriteLine($"[EYE:HOT-SWAP] New Eye PID={newProc.Id} — waiting for window...");
                    var hsw = System.Diagnostics.Stopwatch.StartNew();
                    var warnAt = 9.0;
                    while (true)
                    {
                        newProc.Refresh();
                        if (newProc.HasExited || newProc.Responding) break;
                        if (hsw.Elapsed.TotalSeconds >= warnAt)
                        {
                            Console.WriteLine($"[EYE:HOT-SWAP] still waiting for new Eye message loop... ({warnAt:F0}s)");
                            warnAt += 9.0;
                        }
                        Thread.Sleep(200);
                    }
                    Console.WriteLine($"[EYE:HOT-SWAP] New Eye responding ({hsw.Elapsed.TotalMilliseconds:F0}ms) — hiding old Eye window");
                    TryHideConsoleWindow(); // hide immediately so new Eye window is unobscured
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

    static bool TryRunOneGlobalTick(AppBotEyeHost host, int timeoutMs, bool forceFull, bool tickDirty, bool promptDirty)
    {
        try
        {
            var t = Task.Run(() => RunOneGlobalTick(host, forceFull, tickDirty, promptDirty));
            return t.Wait(timeoutMs);
        }
        catch { return false; }
    }

    static void RunOneGlobalTick(AppBotEyeHost host, bool forceFull, bool tickDirty, bool promptDirty)
    {
        var swTotal = Stopwatch.StartNew();

        var swTick = Stopwatch.StartNew();
        long tickRead = 0, tickParse = 0;
        var latest = _cachedLatestTick;
        if (forceFull || tickDirty || latest == null)
        {
            latest = ReadLatestTick(out tickRead, out tickParse);
            _cachedLatestTick = latest;
            _cachedCards = ReadEyeCards(staleSeconds: 86400); // 24 hours
            SupplementCardsFromPrompts(_cachedCards);
            CheckAndReportDeadCards(_cachedCards);
        }
        swTick.Stop();

        var swPrompt = Stopwatch.StartNew();
        var promptDiag = new PromptDiag();
        var promptPreview = _cachedPromptPreview;
        if (forceFull || promptDirty || string.IsNullOrWhiteSpace(promptPreview))
        {
            promptPreview = ReadLatestOpenClawPromptPreview(promptDiag);
            _cachedPromptPreview = promptPreview;
            _cachedPromptFileWriteUtc = promptDiag.FileWriteUtc; // cache mtime for kro sort
        }
        else
        {
            promptDiag.Source = "sessions-cache";
            promptDiag.CacheMs = 1;
            promptDiag.FileWriteUtc = _cachedPromptFileWriteUtc; // restore cached mtime
        }
        swPrompt.Stop();

        var swSchedule = Stopwatch.StartNew();
        // placeholder for future schedule diagnostics
        swSchedule.Stop();

        if (latest != null)
        {
            _lastTickActivityUtc = DateTime.UtcNow;
            if ((latest.Status ?? "").Contains("step:", StringComparison.OrdinalIgnoreCase) ||
                (latest.Status ?? "").Contains("done", StringComparison.OrdinalIgnoreCase))
                _lastAiActivityUtc = DateTime.UtcNow;
        }
        if (!string.IsNullOrWhiteSpace(promptPreview))
            _lastAiActivityUtc = DateTime.UtcNow;

        _lastPromptSource = promptDiag.Source;

        var cards = _cachedCards;

        host.UpdateInfo("global", $"WK AppBot Global Eye {DateTime.Now:HH:mm:ss}");
        var eyeSummary = BuildEyeSummary(cards, latest, promptPreview, promptDiag.FileWriteUtc);

        // CCA live analysis removed from Eye (v4.8) — now in analyze-hack server process.
        // Eye only does read-only operations: card summary, UIA status, context %.
        // No Bitmap/CCA/FlaUI in Eye → memory savings ~500MB+.

        host.UpdateAccessibilityText(eyeSummary);
        // Update IPC cache so eye tick one-shot gets instant response
        _cachedIpcSummary = eyeSummary;
        _cachedIpcPromptPreview = promptPreview ?? "";
        _cachedIpcUpdatedAt = DateTime.UtcNow;

        swTotal.Stop();

        var mode = forceFull ? "full-1s" : (tickDirty || promptDirty ? "dirty" : "idle");
        // Memory delta tracking
        var proc = Process.GetCurrentProcess();
        var wsMB = proc.WorkingSet64 / (1024 * 1024);
        var deltaMB = wsMB - _prevWorkingSetMB;
        if (wsMB > _peakWorkingSetMB) _peakWorkingSetMB = wsMB;
        _prevWorkingSetMB = wsMB;
        var memSuffix = deltaMB != 0 ? $" mem={wsMB}MB({(deltaMB >= 0 ? "+" : "")}{deltaMB}) peak={_peakWorkingSetMB}MB" : "";

        var ctxSuffix = _lastContextPct >= 0 ? $" ctx={_lastContextPct}%" : "";
        Console.WriteLine($"[EYE_TICK] mode={mode} tick={swTick.ElapsedMilliseconds}ms(read={tickRead}ms,parse={tickParse}ms,activity=0ms) " +
                          $"prompt={swPrompt.ElapsedMilliseconds}ms(stat={promptDiag.StatMs}ms,read={promptDiag.ReadMs}ms,scan={promptDiag.ScanMs}ms,parse={promptDiag.ParseMs}ms,norm={promptDiag.NormMs}ms,cache={promptDiag.CacheMs}ms) " +
                          $"schedule={swSchedule.ElapsedMilliseconds}ms total={swTotal.ElapsedMilliseconds}ms{memSuffix}{ctxSuffix}");
        Console.WriteLine($"[EYE_TICK] hint promptLine={_lastPromptLineIndex} tickLine={_lastEyeTickLineIndex}");

        var execIdle = (DateTime.UtcNow - _lastTickActivityUtc).TotalSeconds;
        var aiIdle = (DateTime.UtcNow - _lastAiActivityUtc).TotalSeconds;
        var cooldown = _lastAutoGogoUtc == DateTime.MinValue ? 9999 : (DateTime.UtcNow - _lastAutoGogoUtc).TotalSeconds;
        var armed = execIdle >= 60 && aiIdle >= 60 && cooldown >= 600;
        Console.WriteLine($"[EYE_GUARD] armed={(armed ? 1 : 0)} execIdle={execIdle:F0}s aiIdle={aiIdle:F0}s cooldown={cooldown:F0}s");

        var latestAge = -1.0;
        if (latest != null && DateTime.TryParse(latest.Ts, out var ts))
            latestAge = (DateTime.UtcNow - ts.ToUniversalTime()).TotalSeconds;
        var keepAge = _lastKeepAwakeUtc == DateTime.MinValue ? -1 : (DateTime.UtcNow - _lastKeepAwakeUtc).TotalSeconds;
        Console.WriteLine($"[EYE_LOOP] keepAwakeAge={(keepAge < 0 ? "n/a" : keepAge.ToString("F0") + "s")} promptSource={_lastPromptSource} latestTickAge={(latestAge < 0 ? "n/a" : latestAge.ToString("F0") + "s")}");
    }

    static bool ShouldForceFullLoad()
    {
        var now = DateTime.UtcNow;
        if (_lastForceFullLoadUtc == DateTime.MinValue || (now - _lastForceFullLoadUtc).TotalMilliseconds >= 1000)
        {
            _lastForceFullLoadUtc = now;
            return true;
        }
        return false;
    }

    /// <summary>
    /// FSW hybrid dirty check:
    /// - Fast path: consume volatile FSW flags (set by FileSystemWatcher callbacks, ~0ms)
    /// - Slow path: FileInfo triple-check (only on forceFull=true, every 1s safety net)
    /// This eliminates 100ms polling overhead while keeping reliability.
    /// </summary>
    static (bool tickDirty, bool promptDirty) CheckGlobalDirtyFlags(bool forceFull = false)
    {
        // ── Fast path: FSW event-driven flags (instant, no I/O) ──
        bool tickDirty = _fswTickDirty;
        bool promptDirty = _fswPromptDirty;
        var promptChangedFile = _fswPromptChangedFile;
        if (tickDirty) _fswTickDirty = false;    // consume flag
        if (promptDirty) _fswPromptDirty = false; // consume flag

        // Filter: skip prompt dirty if the changed file isn't the one we're tracking
        if (promptDirty && !string.IsNullOrEmpty(promptChangedFile) && !string.IsNullOrEmpty(_dirtyPromptFile))
        {
            var trackedName = Path.GetFileName(_dirtyPromptFile);
            if (!string.Equals(promptChangedFile, trackedName, StringComparison.OrdinalIgnoreCase))
            {
                promptDirty = false; // irrelevant file change — skip
            }
        }

        // ── Slow path: FileInfo poll (only on 1s safety-net intervals) ──
        // Catches edge cases: FSW buffer overflow, network drives, watcher init failure
        if (forceFull)
        {
            try
            {
                var tickPath = EyeTicksPath;
                if (File.Exists(tickPath))
                {
                    var fi = new FileInfo(tickPath);
                    if (_dirtyTickFile != tickPath || _dirtyTickLength != fi.Length || _dirtyTickWriteUtc != fi.LastWriteTimeUtc)
                    {
                        tickDirty = true;
                        _dirtyTickFile = tickPath;
                        _dirtyTickLength = fi.Length;
                        _dirtyTickWriteUtc = fi.LastWriteTimeUtc;
                    }
                }
            }
            catch { }

            try
            {
                var sessionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions");
                if (Directory.Exists(sessionsDir))
                {
                    var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl")
                        .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
                        .FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(latestFile) && File.Exists(latestFile))
                    {
                        var fi = new FileInfo(latestFile);
                        if (_dirtyPromptFile != latestFile || _dirtyPromptLength != fi.Length || _dirtyPromptWriteUtc != fi.LastWriteTimeUtc)
                        {
                            promptDirty = true;
                            _dirtyPromptFile = latestFile;
                            _dirtyPromptLength = fi.Length;
                            _dirtyPromptWriteUtc = fi.LastWriteTimeUtc;
                        }
                    }
                }
            }
            catch { }
        }

        return (tickDirty, promptDirty);
    }

    /// <summary>
    /// FSW hybrid: create FileSystemWatchers for tick file + OpenClaw sessions dir.
    /// Events set volatile dirty flags → 100ms loop picks them up instantly (no FileInfo poll).
    /// 1s full-load safety net unchanged.
    /// </summary>
    static void InitFileWatchers()
    {
        // ── 1. Tick file watcher (eye_ticks.jsonl) ──
        try
        {
            var tickPath = EyeTicksPath;
            var tickDir = Path.GetDirectoryName(tickPath);
            var tickFile = Path.GetFileName(tickPath);
            if (tickDir != null && Directory.Exists(tickDir))
            {
                _fswTick = new FileSystemWatcher(tickDir)
                {
                    Filter = tickFile,
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
                    EnableRaisingEvents = true
                };
                _fswTick.Changed += (_, _) => _fswTickDirty = true;
                _fswTick.Created += (_, _) => _fswTickDirty = true;
                Console.WriteLine($"[EYE][FSW] Tick watcher: {tickDir}/{tickFile}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE][FSW] Tick watcher init failed: {ex.Message}");
        }

        // ── 2. OpenClaw sessions watcher (*.jsonl) ──
        try
        {
            var sessionsDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".openclaw", "agents", "main", "sessions");
            if (Directory.Exists(sessionsDir))
            {
                _fswPrompt = new FileSystemWatcher(sessionsDir)
                {
                    Filter = "*.jsonl",
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName,
                    EnableRaisingEvents = true
                };
                _fswPrompt.Changed += (_, e) => { _fswPromptChangedFile = e.Name; _fswPromptDirty = true; };
                _fswPrompt.Created += (_, e) => { _fswPromptChangedFile = e.Name; _fswPromptDirty = true; };
                _fswPrompt.Renamed += (_, e) => { _fswPromptChangedFile = e.Name; _fswPromptDirty = true; };
                Console.WriteLine($"[EYE][FSW] Prompt watcher: {sessionsDir}/*.jsonl");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE][FSW] Prompt watcher init failed: {ex.Message}");
        }

        // Claude Code JSONL: 1s polling only — FSW removed (per-instance watermark sufficient)

        // ── 4. EXE file watcher (hot-swap: instant binary change detection) ──
        try
        {
            var selfExe = Environment.ProcessPath ?? "";
            var exeDir = Path.GetDirectoryName(selfExe);
            var exeFile = Path.GetFileName(selfExe);
            if (exeDir != null && Directory.Exists(exeDir) && !string.IsNullOrEmpty(exeFile))
            {
                _fswExe = new FileSystemWatcher(exeDir)
                {
                    Filter = exeFile,
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.CreationTime,
                    EnableRaisingEvents = true
                };
                _fswExe.Changed += (_, _) => _fswExeDirty = true;
                _fswExe.Created += (_, _) => _fswExeDirty = true;
                // Also watch for .new.exe (staged swap)
                _fswExeNew = new FileSystemWatcher(exeDir)
                {
                    Filter = Path.GetFileNameWithoutExtension(exeFile) + ".new.exe",
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.Size,
                    EnableRaisingEvents = true
                };
                _fswExeNew.Changed += (_, _) => _fswExeDirty = true;
                _fswExeNew.Created += (_, _) => _fswExeDirty = true;
                // Startup: .new.exe may already exist (pre-dated swap from before restart)
                var newExeOnStart = Path.Combine(exeDir, Path.GetFileNameWithoutExtension(exeFile) + ".new.exe");
                if (File.Exists(newExeOnStart))
                {
                    Console.WriteLine("[EYE][FSW] .new.exe already present at startup — triggering hot-swap");
                    _fswExeDirty = true;
                }
                Console.WriteLine($"[EYE][FSW] Hot-swap watcher: {exeDir}/{exeFile}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE][FSW] Hot-swap watcher init failed: {ex.Message}");
        }

        // ── 4. 앱봇관리 log file watcher (temp dir) ──
        // Eye/MCP가 wkappbot-*.log 생성 → Eye가 감지해서 apbot-mcp WT 탭 자동 오픈
        try
        {
            var tempDir = Path.GetTempPath();
            // Eye 재시작 시 기존 로그 파일 처리:
            // - PID 살아있는 것 → 탭 오픈
            // - 죽은 세션 로그 → 삭제 (탭 폭발 방지)
            foreach (var f in Directory.GetFiles(tempDir, "wkappbot-*.log"))
            {
                var stem  = Path.GetFileNameWithoutExtension(f);
                var parts = stem.Split('-');
                // wkappbot-eye-{pid} or wkappbot-mcp-{pid} → 마지막 파트가 숫자이면 PID
                if (int.TryParse(parts[^1], out var logPid))
                {
                    bool alive = false;
                    try { using var p = Process.GetProcessById(logPid); alive = !p.HasExited; } catch { }
                    if (!alive) { try { File.Delete(f); } catch { } continue; }
                }
                EyeOpenConsoleWtTab(f);
            }

            _fswMcp = new FileSystemWatcher(tempDir)
            {
                Filter                = "wkappbot-*.log",
                IncludeSubdirectories = false,
                NotifyFilter          = NotifyFilters.FileName | NotifyFilters.CreationTime,
                EnableRaisingEvents   = true
            };
            _fswMcp.Created += (_, e) =>
            {
                if (e.FullPath != null)
                    Task.Run(() => EyeOpenConsoleWtTab(e.FullPath));
            };
            Console.WriteLine($"[EYE][FSW] Console watcher: {tempDir}wkappbot-*.log");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE][FSW] Console watcher init failed: {ex.Message}");
        }
    }

    /// <summary>
    /// 앱봇관리 로그 파일 생성 감지 시 apbot-mcp WT 탭 오픈.
    /// 파일명 규칙:
    ///   wkappbot-eye-{pid}.log              → [앱봇관리] Eye
    ///   wkappbot-mcp-{session}.log          → [앱봇관리] MCP
    ///   wkappbot-mcp-tool-{name}-{id}.log   → [앱봇관리] {name} #{id}
    /// </summary>
    static void EyeOpenConsoleWtTab(string logFilePath)
    {
        // wt.exe 자동 탭 오픈 비활성화 — 포커스 간섭 없이 필요할 때 수동으로 열어서 사용
        return;
        try
        {
            var fileName = Path.GetFileNameWithoutExtension(logFilePath);
            lock (_mcpTabsOpened)
            {
                if (!_mcpTabsOpened.Add(fileName)) return; // 이미 탭 열었음
            }

            string wtTitle;
            int watchPid = 0;
            if (fileName.StartsWith("wkappbot-eye-", StringComparison.Ordinal))
                wtTitle = "앱봇아이";
            else
            {
                // wkappbot-mcp-{sessionPid}.log → 대화명 조회
                var sessionPart = fileName["wkappbot-mcp-".Length..];
                if (int.TryParse(sessionPart, out var sessionPid))
                {
                    wtTitle   = ResolveDisplayNameByPid(sessionPid);
                    watchPid  = sessionPid;
                }
                else wtTitle = "MCP";
            }

            // 프로세스 종료 감시 — 킬당해도 로그에 찍힘
            if (watchPid > 0)
            {
                var logPath = logFilePath;
                _ = Task.Run(async () =>
                {
                    try
                    {
                        using var p = Process.GetProcessById(watchPid);
                        await p.WaitForExitAsync();
                        var msg = p.ExitCode == 0
                            ? "[MCP] Server stopped (stdin EOF)"
                            : $"[MCP] 강제 종료됨 (exit={p.ExitCode}) ㅋ";
                        await File.AppendAllTextAsync(logPath, msg + "\n");
                    }
                    catch { }
                });
            }

            // 이미 같은 로그파일을 감시 중인 powershell.exe가 있으면 탭 중복 방지
            if (IsAlreadyWatchingLog(logFilePath))
            {
                Console.WriteLine($"[EYE][CONSOLE] 이미 탭 열려있음 (PS 감시중): {wtTitle}");
                return;
            }

            var logEsc = logFilePath.Replace("'", "''");
            var psCmd  = $"Get-Content -Wait -Path '{logEsc}'";
            // [FOCUS-GUARD] GuardedStart: wt.exe (Windows Terminal) 실행 — 포커스 강탈 자동 감지+복원
            var wtProc = WKAppBot.Win32.Input.ProcessLaunchGuard.GuardedStart(new ProcessStartInfo
            {
                FileName        = "wt.exe",
                Arguments       = $"-w {McpWtWindowName} new-tab --title \"{wtTitle}\" -- powershell -NoProfile -NonInteractive -NoExit -Command \"{psCmd}\"",
                UseShellExecute = true,
            }, "AppBotEye:wt");
            // 첫 탭 or apbot-mcp 창 없을 때만 위치 고정
            ScheduleWtWindowPosition(900, wtProc?.Id ?? 0);
            Console.WriteLine($"[EYE][CONSOLE] WT 탭 오픈: {wtTitle}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE][CONSOLE] WT 탭 오픈 실패: {ex.Message}");
        }
    }

    /// <summary>WMI로 powershell.exe 중 같은 로그파일을 Get-Content -Wait 중인 프로세스가 있는지 확인.</summary>
    static bool IsAlreadyWatchingLog(string logFilePath)
    {
        try
        {
            var normalized = logFilePath.Replace("\\", "/").ToLowerInvariant();
            using var searcher = new System.Management.ManagementObjectSearcher(
                "SELECT CommandLine FROM Win32_Process WHERE Name='powershell.exe'");
            foreach (System.Management.ManagementObject mo in searcher.Get())
            {
                var cmd = mo["CommandLine"]?.ToString() ?? "";
                if (cmd.Replace("\\", "/").IndexOf(normalized, StringComparison.OrdinalIgnoreCase) >= 0
                    && cmd.Contains("Get-Content", StringComparison.OrdinalIgnoreCase))
                    return true;
            }
        }
        catch { }
        return false;
    }

    static void TryDeleteOldExes()
    {
        try
        {
            var myExe = Environment.ProcessPath ?? "";
            var exeDir = Path.GetDirectoryName(myExe) ?? "";
            var exeName = Path.GetFileNameWithoutExtension(myExe);
            // Delete all timestamped old exes: wkappbot.old-*.exe
            foreach (var oldExe in Directory.GetFiles(exeDir, $"{exeName}.old-*.exe"))
            {
                try
                {
                    File.Delete(oldExe);
                    Console.WriteLine($"[EYE:HOT-SWAP] Cleaned up {Path.GetFileName(oldExe)}");
                }
                catch { } // still locked — 10s polling will retry
            }
        }
        catch { }
    }

    static void DisposeFileWatchers()
    {
        try { if (_fswTick != null)   { _fswTick.EnableRaisingEvents   = false; _fswTick.Dispose();   _fswTick   = null; } } catch { }
        try { if (_fswPrompt != null) { _fswPrompt.EnableRaisingEvents = false; _fswPrompt.Dispose(); _fswPrompt = null; } } catch { }
        try { if (_fswExe != null)    { _fswExe.EnableRaisingEvents    = false; _fswExe.Dispose();    _fswExe    = null; } } catch { }
        try { if (_fswExeNew != null) { _fswExeNew.EnableRaisingEvents = false; _fswExeNew.Dispose(); _fswExeNew = null; } } catch { }
        try { if (_fswMcp != null)    { _fswMcp.EnableRaisingEvents    = false; _fswMcp.Dispose();    _fswMcp    = null; } } catch { }
        Console.WriteLine("[EYE][FSW] Watchers disposed");
    }


    // EyeTickCommand + BuildIpcTickResponse → AppBotEyeTickCommand.cs

    // EyeTickForwardSlackInbox + EyeTickCheckThreadReplies → AppBotEyeSlackMonitor.cs

    // BuildEyeSummary + AbbreviateCwd + CardCache + GetRightmostMonitorAnchor → AppBotEyeCardBuilder.cs

    // ReadLatestTick + ReadLatestOpenClawPromptPreview + TryExtractRoleAndText + NormalizePrompt
    // CompressPlanTitle + ExtractPlanOutlineItems + ExtractRecentPlanItems
    // ReadLatestActionTriplet + ReadTailLinesShared → AppBotEyeJsonlParser.cs

    // _idleSkipCommands + GetClaudeCodeSessionAge + GetLastTickTag + BuildKroStatus3
    // IsMetaTag + CheckAndReportDeadCards + SupplementCardsFromPrompts + ReadEyeCards → AppBotEyeHealthCheck.cs

    /// <summary>
    /// Extract the CWD from a JSONL file by reading its first few lines.
    /// Returns null if CWD not found.
    /// </summary>
    static string? ExtractCwdFromJsonl(string jsonlPath)
    {
        try
        {
            using var sr = new StreamReader(jsonlPath);
            for (int i = 0; i < 20 && !sr.EndOfStream; i++)
            {
                var line = sr.ReadLine();
                if (line == null) break;
                // Look for "cwd":"W:\\GitHub\\WKAppBot" pattern
                var m = System.Text.RegularExpressions.Regex.Match(line, "\"cwd\":\"([^\"]+)\"");
                if (m.Success)
                {
                    return m.Groups[1].Value.Replace("\\\\", "\\");
                }
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Build a handoff prompt for a new chat session.
    /// Reads the tail of the current JSONL session to extract recent context,
    /// then constructs a continuation prompt in English (token-efficient) with Korean response instruction.
    /// </summary>
    static string BuildHandoffPrompt(string jsonlPath)
    {
        // Extract last few user/assistant messages from JSONL for context
        var recentMessages = new List<string>();
        try
        {
            // Read last ~50KB of the file (tail) to find recent conversation
            using var fs = new FileStream(jsonlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            var tailSize = Math.Min(fs.Length, 50 * 1024);
            fs.Seek(-tailSize, SeekOrigin.End);
            using var sr = new StreamReader(fs, Encoding.UTF8);

            // Skip partial first line
            if (fs.Position > 0) sr.ReadLine();

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var node = JsonNode.Parse(line);
                    if (node == null) continue;
                    var role = node["role"]?.GetValue<string>();
                    var type = node["type"]?.GetValue<string>();

                    // Capture human/assistant summary messages
                    if (role == "human" || role == "user")
                    {
                        var content = node["content"]?.ToString() ?? "";
                        if (content.Length > 200) content = content[..200] + "...";
                        if (!string.IsNullOrWhiteSpace(content))
                            recentMessages.Add($"[USER] {content}");
                    }
                    else if (role == "assistant")
                    {
                        var content = node["content"]?.ToString() ?? "";
                        if (content.Length > 300) content = content[..300] + "...";
                        if (!string.IsNullOrWhiteSpace(content))
                            recentMessages.Add($"[ASSISTANT] {content}");
                    }
                }
                catch { /* skip unparseable lines */ }
            }
        }
        catch (Exception ex)
        {
            recentMessages.Add($"(Failed to read session: {ex.Message})");
        }

        // Keep only the last ~10 messages for context
        if (recentMessages.Count > 10)
            recentMessages = recentMessages.Skip(recentMessages.Count - 10).ToList();

        var contextBlock = recentMessages.Count > 0
            ? string.Join("\n", recentMessages)
            : "(no recent messages extracted)";

        // Build handoff prompt (English for token efficiency, Korean response requested)
        var handoffPrompt = $@"This is an AUTO-RELAY from AppBotEye. The previous session hit 95% context limit and was automatically handed off.

## Instructions
1. Read CLAUDE.md and MEMORY.md first to understand the project
2. Continue the work from where the previous session left off
3. Reply in Korean (한국어로 답변해주세요)
4. Send a Slack message to let the user know you're continuing: `wkappbot slack send ""새 채팅에서 이어갑니다! (auto-relay) 🔄""`
5. Check recent Slack messages for any user instructions: look at eye tick data

## Recent conversation context from previous session:
```
{contextBlock}
```

## Session info
- Previous JSONL: {Path.GetFileName(jsonlPath)}
- Relay timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
- Relay reason: context 95% limit reached

Please start by reading CLAUDE.md, then summarize what you understand about the pending work and continue.

(auto-relay by AppBotEye context monitor)";

        return handoffPrompt;
    }

    /// <summary>Overload: includes active card data + plan files for richer handoff.</summary>
    static string BuildHandoffPrompt(string jsonlPath, List<EyeParentCard> cards, double sizeMB, double limitMB)
    {
        var sb = new StringBuilder();
        sb.AppendLine("이전 세션이 컨텍스트 90%에 도달하여 자동 인수인계합니다.");
        sb.AppendLine("CLAUDE.md와 MEMORY.md를 먼저 읽고 이어서 작업해주세요.");
        sb.AppendLine();

        // Active cards = what clots were working on
        if (cards.Count > 0)
        {
            sb.AppendLine("## 활성 클롣 카드:");
            foreach (var c in cards.Take(5))
            {
                var cwd = AbbreviateCwd(c.Cwd);
                var info = !string.IsNullOrWhiteSpace(cwd) ? cwd : c.ParentName;
                sb.AppendLine($"- [{info}] {c.LastTag}: {c.LastStatus}");
            }
            sb.AppendLine();
        }

        // Plan files
        try
        {
            var plansDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".claude", "plans");
            if (Directory.Exists(plansDir))
            {
                var recentPlan = Directory.GetFiles(plansDir, "*.md")
                    .OrderByDescending(f => File.GetLastWriteTimeUtc(f))
                    .FirstOrDefault();
                if (recentPlan != null)
                {
                    var age = (DateTime.UtcNow - File.GetLastWriteTimeUtc(recentPlan)).TotalHours;
                    if (age < 24)
                        sb.AppendLine($"## 미완료 플랜: {Path.GetFileName(recentPlan)} ({age:F0}시간 전)");
                }
            }
        }
        catch { }

        sb.AppendLine();
        sb.AppendLine($"세션: {Path.GetFileName(jsonlPath)} ({sizeMB:F1}/{limitMB}MB)");
        sb.AppendLine($"시간: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine();
        sb.AppendLine("⚡ 중요: 채팅방 제목을 참신하고 재밌게 지어주세요! 매번 '인수인계'같은 뻔한 제목 금지!");
        sb.AppendLine("슬랙으로 인수인계 완료 알림 보내주세요: wkappbot slack send '새 세션에서 이어갑니다 🔄'");

        return sb.ToString().Trim();
    }

    /// <summary>
    /// Build a handoff section to write into CLAUDE.md.
    /// Written in Korean so the user can also read it ("나도 보게 ㅋ").
    /// The next Claude session reads CLAUDE.md on startup and sees this section.
    /// </summary>
    static string BuildHandoffSection(string jsonlPath, string handoffPrompt)
    {
        // Extract recent context summary from JSONL
        var recentLines = new List<string>();
        try
        {
            using var fs = new FileStream(jsonlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            var tailSize = Math.Min(fs.Length, 30 * 1024);
            fs.Seek(-tailSize, SeekOrigin.End);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            if (fs.Position > 0) sr.ReadLine(); // skip partial

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var node = JsonNode.Parse(line);
                    if (node == null) continue;
                    var role = node["role"]?.GetValue<string>();
                    if (role == "human" || role == "user")
                    {
                        var c = node["content"]?.ToString() ?? "";
                        if (c.Length > 150) c = c[..150] + "...";
                        if (!string.IsNullOrWhiteSpace(c)) recentLines.Add($"  - **User**: {c}");
                    }
                    else if (role == "assistant")
                    {
                        var c = node["content"]?.ToString() ?? "";
                        if (c.Length > 150) c = c[..150] + "...";
                        if (!string.IsNullOrWhiteSpace(c)) recentLines.Add($"  - **Claude**: {c}");
                    }
                }
                catch { }
            }
        }
        catch { recentLines.Add("  - (failed to read session)"); }

        // Keep last 8 messages
        if (recentLines.Count > 8)
            recentLines = recentLines.Skip(recentLines.Count - 8).ToList();

        var contextSummary = recentLines.Count > 0
            ? string.Join("\n", recentLines)
            : "  - (no recent messages)";

        return $@"
## 🔄 Handoff — {DateTime.Now:yyyy-MM-dd HH:mm}

> **Previous session hit 95% context limit and was auto-relayed by AppBotEye.**
> Read this section, continue the work, then DELETE this section when done.

### Previous Session
- **JSONL**: `{Path.GetFileName(jsonlPath)}`
- **Relay time**: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
- **Reason**: context 95% limit (auto-relay by AppBotEye)

### Recent Conversation
{contextSummary}

### TODO for new session
1. Read CLAUDE.md + MEMORY.md to understand project state
2. Run `wkappbot slack send ""New chat continuing! (auto-relay) 🔄""` to notify user
3. Check recent Slack messages for user instructions
4. Continue work from where previous session left off
5. **DELETE this handoff section** after processing

";
    }

    /// <summary>
    /// Check if CLAUDE.md contains a handoff section (auto-relay precondition).
    /// </summary>
    static bool HasHandoffSectionInClaudeMd()
    {
        const string claudeMdPath = @"W:\GitHub\WKAppBot\CLAUDE.md";
        const string handoffMarker = "## \U0001f504 Handoff";
        try
        {
            if (!File.Exists(claudeMdPath)) return false;
            var content = File.ReadAllText(claudeMdPath, Encoding.UTF8);
            return content.Contains(handoffMarker, StringComparison.Ordinal);
        }
        catch { return false; }
    }

    /// <summary>
    /// Write handoff section to CLAUDE.md (at the end, before roadmap section).
    /// If a previous handoff section exists, replace it.
    /// CLAUDE.md path: W:/GitHub/WKAppBot/CLAUDE.md
    /// </summary>
    static void WriteHandoffToClaudeMd(string handoffSection)
    {
        const string claudeMdPath = @"W:\GitHub\WKAppBot\CLAUDE.md";
        const string handoffMarker = "## 🔄 Handoff";
        const string handoffEndMarker = "## "; // next section starts

        try
        {
            if (!File.Exists(claudeMdPath))
            {
                Console.WriteLine("[EYE] CLAUDE.md not found! Cannot write handoff.");
                return;
            }

            var content = File.ReadAllText(claudeMdPath, Encoding.UTF8);

            // Remove existing handoff section if present
            var handoffIdx = content.IndexOf(handoffMarker, StringComparison.Ordinal);
            if (handoffIdx >= 0)
            {
                // Find end of handoff section (next ## heading or end of file)
                var afterHandoff = content.IndexOf("\n" + handoffEndMarker, handoffIdx + handoffMarker.Length, StringComparison.Ordinal);
                if (afterHandoff >= 0)
                    content = content[..handoffIdx] + content[(afterHandoff + 1)..];
                else
                    content = content[..handoffIdx]; // handoff was at end
            }

            // Append handoff section at the end
            content = content.TrimEnd() + "\n" + handoffSection;

            // Atomic write
            var tmpPath = claudeMdPath + ".tmp";
            File.WriteAllText(tmpPath, content, Encoding.UTF8);
            File.Move(tmpPath, claudeMdPath, overwrite: true);

            EyeColor(ConsoleColor.Green);
            Console.WriteLine($"[EYE] ✅ CLAUDE.md 인수인계 섹션 작성 완료 ({handoffSection.Length} chars)");
            EyeResetColor();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] CLAUDE.md 인수인계 작성 실패: {ex.Message}");
        }
    }
}
