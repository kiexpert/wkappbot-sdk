using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
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

    sealed class PromptDiag
    {
        public long StatMs { get; set; }
        public long ReadMs { get; set; }
        public long ScanMs { get; set; }
        public long ParseMs { get; set; }
        public long NormMs { get; set; }
        public long CacheMs { get; set; }
        public string Source { get; set; } = "none";
    }

    static DateTime _lastTickActivityUtc = DateTime.MinValue;
    static DateTime _lastAiActivityUtc = DateTime.MinValue;
    static DateTime _lastAutoGogoUtc = DateTime.MinValue;
    static DateTime _lastKeepAwakeUtc = DateTime.MinValue;
    static string _lastPromptSource = "none";

    static string _lastPromptSessionFile = "";
    static int _lastPromptLineIndex = -1;
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
    static List<EyeParentCard> _cachedCards = new();
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
    static FileSystemWatcher? _fswTick;
    static FileSystemWatcher? _fswPrompt;

    // ── Memory tracking ──
    static long _prevWorkingSetMB;
    static long _peakWorkingSetMB;

    static int EyeGlobalPollingLoop(int width, int height, int posX, int posY, int intervalMs)
    {
        if (posX < 0 || posY < 0)
        {
            var (x, y) = GetRightmostMonitorAnchor(width, height);
            posX = x;
            posY = y;
        }

        using var host = new AppBotEyeHost();
        host.Start(width, height, posX, posY, ownerHwnd: IntPtr.Zero);
        host.UpdateInfo("global", $"WK AppBot Global Eye {DateTime.Now:HH:mm:ss}");
        host.UpdateAccessibilityText(string.Empty);

        Console.WriteLine("[EYE] Global monitor active — press Ctrl+C to stop");

        // ★ Force focusless: Eye should NEVER steal foreground focus
        ClaudePromptHelper.ForceFocusless = true;
        Console.WriteLine("[EYE] ForceFocusless=ON (prompt delivery will not steal focus)");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        if (_lastTickActivityUtc == DateTime.MinValue) _lastTickActivityUtc = DateTime.UtcNow;
        if (_lastAiActivityUtc == DateTime.MinValue) _lastAiActivityUtc = DateTime.UtcNow;

        // ── Find Claude Desktop window (for plan approval UIA clicks) ──
        IntPtr claudeHwnd = FindClaudeDesktopWindow();
        if (claudeHwnd != IntPtr.Zero)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[EYE] Found Claude Desktop (hwnd=0x{claudeHwnd:X8})");
            Console.ResetColor();
        }

        // ── Plan/Permission approval tracking ──
        bool planApprovalSentToSlack = false;
        string? pendingPlanApprovalSlackTs = null;
        bool permissionPromptSentToSlack = false;
        string? pendingPermissionSlackTs = null;
        DateTime? permissionPromptFirstSeen = null; // debounce: 3s before Slack notification

        // ── Slack status streaming ──
        string? slackStatusTs = null;
        string? lastSlackStatusText = null;
        string? lastExecutingText = null; // remember last "executing" status for prompt_ready transition
        string? idleAfterText = null; // persisted: what was the last task before going idle
        bool idleMessageSent = false; // whether idle status message has been posted to Slack
        var statusTsFile = Path.Combine(DataDir, "runtime", "status_streaming_ts.txt");

        // Restore previous status message ts (reuse on restart instead of creating new)
        try
        {
            if (File.Exists(statusTsFile))
            {
                var savedTs = File.ReadAllText(statusTsFile).Trim();
                if (!string.IsNullOrEmpty(savedTs))
                {
                    slackStatusTs = savedTs;
                    Console.WriteLine($"[EYE] Restored previous status message (ts={savedTs})");
                }
            }
        }
        catch { }

        // ── Claude status tracking ──
        string? cachedClaudeStatusText = null;
        bool wasRateLimited = false;
        DateTime? rateLimitDetectedAt = null;
        DateTime? rateLimitResetTime = null;
        DateTime? lastRateLimitAlertTime = null;
        const int RateLimitCooldownMinutes = 30;
        const int RateLimitAlertCooldownMinutes = 30;

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

                if (!string.IsNullOrEmpty(appToken) && !string.IsNullOrEmpty(slackBotToken))
                {
                    slackClient = new SlackSocketClient();
                    slackClient.ConnectAsync(appToken, slackBotToken).GetAwaiter().GetResult();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[EYE] Slack Socket Mode connected (GlobalMode)");
                    Console.ResetColor();

                    // Startup — no Slack announcement (reduces channel spam on hot-reload restarts)
                    string? startupTs = null;

                    // Set up event handlers (Slack → Claude prompt forwarding, plan/permission approval, status streaming)
                    SetupSlackEventHandlers(slackClient, slackBotToken!, slackChannel,
                        claudeHwnd, () => pendingPlanApprovalSlackTs,
                        () => pendingPermissionSlackTs, startupTs, botUsername,
                        () => slackStatusTs, () => {
                            slackStatusTs = null; lastSlackStatusText = null;
                            try { File.WriteAllText(statusTsFile, ""); } catch { }
                        });

                    // Block Kit button handler (plan approve/reject, permission buttons)
                    slackClient.OnBlockAction += (action) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[EYE][SLACK] Button: {action.ActionId}={action.Value} by {action.UserName}");
                        Console.ResetColor();

                        var thread = action.MessageTs ?? pendingPlanApprovalSlackTs;

                        if (action.ActionId == "plan_approve" && claudeHwnd != IntPtr.Zero)
                        {
                            var approved = ClickApproveButton(claudeHwnd);
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
                        else if (action.ActionId.StartsWith("perm_") && claudeHwnd != IntPtr.Zero)
                        {
                            var buttonText = action.Value;
                            var clicked = ClickPermissionButton(claudeHwnd, buttonText);
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
                        else if (action.ActionId == "plan_reject" && claudeHwnd != IntPtr.Zero)
                        {
                            var feedbackOk = TypePlanFeedback(claudeHwnd, "이 플랜을 거절합니다. 다시 검토해주세요.");
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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[SCHEDULE] {overdueItems.Count} overdue schedule(s) from before restart");
                Console.ResetColor();
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

        var keepAwakeSw = Stopwatch.StartNew();
        WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
            WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS |
            WKAppBot.Win32.Native.NativeMethods.ES_SYSTEM_REQUIRED |
            WKAppBot.Win32.Native.NativeMethods.ES_DISPLAY_REQUIRED);
        _lastKeepAwakeUtc = DateTime.UtcNow;

        // ── FSW hybrid: event-driven file change detection ──
        InitFileWatchers();

        // ── Context usage monitor + auto-relay ──
        bool contextWarning90Sent = false, contextWarning95Sent = false;
        bool contextRelayPromptSent = false;   // 90%: "준비해!" prompt delivered
        bool contextRelayExecuted = false;     // 95%: new chat opened + handoff pasted
        string? lastContextJsonlPath = null;
        const double ContextLimitMB = 40.0; // empirical: sessions hit limit ~40MB JSONL

        int frameCount = 0;
        while (host.IsAlive && !cts.IsCancellationRequested)
        {
            // ── Core tick: read ticks + sessions ──
            var forceFull = ShouldForceFullLoad();
            var (tickDirty, promptDirty) = CheckGlobalDirtyFlags(forceFull);
            if (!TryRunOneGlobalTick(host, timeoutMs: 3000, forceFull, tickDirty, promptDirty))
            {
                Console.WriteLine("[EYE] tick timeout (>3s) - self terminate");
                break;
            }

            // ── Claude Desktop status detection (~every 5 sec) ──
            if (frameCount % 50 == 0)
            {
                // Re-find Claude window if lost
                if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
                    claudeHwnd = FindClaudeDesktopWindow();

                if (claudeHwnd != IntPtr.Zero)
                {
                    try
                    {
                        var claudeStatus = DetectClaudeDesktopStatus(claudeHwnd);

                        if (claudeStatus != null)
                        {
                            // Remember last executing text for idle display
                            if (claudeStatus.Item1 == "executing")
                                lastExecutingText = claudeStatus.Item2;

                            cachedClaudeStatusText = $"Claude: {claudeStatus.Item2}";

                            // Rate limit detection
                            bool justHitRateLimit = false;
                            if (claudeStatus.Item1 == "rate_limit")
                            {
                                justHitRateLimit = !wasRateLimited;
                                wasRateLimited = true;
                                if (rateLimitDetectedAt == null)
                                    rateLimitDetectedAt = DateTime.Now;
                                var resetDt = GetResetTimeFromDisplayText(claudeStatus.Item2);
                                if (resetDt != null)
                                    rateLimitResetTime = resetDt;
                            }
                            else if (wasRateLimited)
                            {
                                // ★ 다른 상태(executing/prompt_ready/permission 등) 감지 = 리밋 해제!
                                // 시간 조건 없이 즉시 해제 (유저 요청: "다른 상태가 감지되면 리밋이 풀린 것")
                                bool stateChanged = true; // claudeStatus is NOT rate_limit → 상태 변화
                                var now = DateTime.Now;
                                bool cooldownPassed = rateLimitDetectedAt != null &&
                                    (now - rateLimitDetectedAt.Value).TotalMinutes >= RateLimitCooldownMinutes;
                                bool resetTimePassed = rateLimitResetTime != null && now >= rateLimitResetTime.Value;

                                if (stateChanged || cooldownPassed || resetTimePassed)
                                {
                                    wasRateLimited = false;
                                    rateLimitDetectedAt = null;
                                    rateLimitResetTime = null;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"[EYE] Rate limit cleared (stateChanged={stateChanged}, newState={claudeStatus.Item1}, cooldown={cooldownPassed}, resetTime={resetTimePassed})");
                                    Console.ResetColor();

                                    // Execute on_limit_reset schedules
                                    try
                                    {
                                        Console.WriteLine("[SCHEDULE] Rate limit cleared! Checking on_limit_reset schedules...");
                                        Thread.Sleep(3000);
                                        var resetItems = ScheduleManager.GetOnLimitResetItems();
                                        foreach (var resetItem in resetItems)
                                        {
                                            ExecuteScheduleItem(resetItem, slackBotToken, slackChannel);
                                            Thread.Sleep(2000);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"[SCHEDULE] on_limit_reset error: {ex.Message}");
                                    }
                                }
                            }

                            // Rate limit alert to Slack (new message, not update)
                            bool alertCooldownOk = lastRateLimitAlertTime == null ||
                                (DateTime.Now - lastRateLimitAlertTime.Value).TotalMinutes >= RateLimitAlertCooldownMinutes;
                            if (justHitRateLimit && alertCooldownOk && !string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                            {
                                lastRateLimitAlertTime = DateTime.Now;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"[EYE] ★ Rate limit detected! {claudeStatus.Item2}");
                                Console.ResetColor();
                                try
                                {
                                    var alertMsg = $":rotating_light: *Rate Limit!* {claudeStatus.Item2}";
                                    Task.Run(async () =>
                                        await SlackSendViaApi(slackBotToken!, slackChannel!, alertMsg, username: botUsername))
                                        .Wait(5000);
                                }
                                catch { }
                            }

                            // ── Context usage monitor (~every 5s, same as Claude status) ──
                            try
                            {
                                var claudeProjectsDir = Path.Combine(
                                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                    ".claude", "projects");
                                if (Directory.Exists(claudeProjectsDir))
                                {
                                    // Find most recently modified .jsonl (= active session)
                                    // Refresh() forces OS-level re-read (no .NET FileInfo attr cache)
                                    var latest = Directory.EnumerateFiles(claudeProjectsDir, "*.jsonl", SearchOption.AllDirectories)
                                        .Select(f => { try { var fi = new FileInfo(f); fi.Refresh(); return fi; } catch { return null; } })
                                        .Where(fi => fi != null && fi.Length > 0)
                                        .OrderByDescending(fi => fi!.LastWriteTimeUtc)
                                        .FirstOrDefault();
                                    if (latest != null)
                                    {
                                        // Staleness check: if JSONL hasn't been written in 2+ min,
                                        // it's an old/abandoned session — skip context monitor.
                                        // Prevents: eye restart → detects old 95% JSONL → false relay
                                        var staleMinutes = (DateTime.UtcNow - latest.LastWriteTimeUtc).TotalMinutes;
                                        if (staleMinutes > 2.0)
                                        {
                                            // Stale JSONL — likely old session, new chat has no history yet
                                            // Reset all flags so we don't carry stale state
                                            if (contextWarning90Sent || contextWarning95Sent)
                                            {
                                                Console.WriteLine($"[EYE] JSONL stale ({staleMinutes:F0}min) — skipping context monitor (new chat?)");
                                                contextWarning90Sent = false;
                                                contextWarning95Sent = false;
                                                contextRelayPromptSent = false;
                                                contextRelayExecuted = false;
                                                lastContextJsonlPath = null;
                                            }
                                            goto skipContextMonitor;
                                        }

                                        var sizeMB = latest.Length / (1024.0 * 1024.0);
                                        var pct = (int)(sizeMB / ContextLimitMB * 100);
                                        // Reset warnings if session changed
                                        if (lastContextJsonlPath != latest.FullName)
                                        {
                                            lastContextJsonlPath = latest.FullName;
                                            contextWarning90Sent = false;
                                            contextWarning95Sent = false;
                                            contextRelayPromptSent = false;
                                            contextRelayExecuted = false;
                                        }
                                        if (pct >= 95 && !contextRelayExecuted && !string.IsNullOrEmpty(slackBotToken))
                                        {
                                            // Send 95% Slack warning once
                                            if (!contextWarning95Sent)
                                            {
                                                contextWarning95Sent = true;
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine($"[EYE] 🚨 Context 95%! ({sizeMB:F1}MB/{ContextLimitMB}MB) — auto-relay 대기 중...");
                                                Console.ResetColor();
                                                Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!,
                                                    $":rotating_light: *컨텍스트 95%!* ({sizeMB:F1}/{ContextLimitMB}MB) 릴레이 대기 중 (클롣 대기상태 + 인수인계 섹션 필요)",
                                                    username: botUsername)).Wait(3000);
                                            }

                                            // ── Auto-relay preconditions ──
                                            // 1. Claude must be idle (prompt_ready) — not executing/planning
                                            // 2. CLAUDE.md must have handoff section (written by current Claude)
                                            bool claudeIsIdle = claudeStatus?.Item1 == "prompt_ready";
                                            bool handoffExists = HasHandoffSectionInClaudeMd();

                                            if (!claudeIsIdle || !handoffExists)
                                            {
                                                // Not ready yet — retry next cycle (every 5s)
                                                if (frameCount % 250 == 0) // log every ~25s to avoid spam
                                                    Console.WriteLine($"[EYE] Auto-relay waiting: idle={claudeIsIdle}, handoff={handoffExists}");
                                            }
                                            else
                                            {
                                                contextRelayExecuted = true;
                                                try
                                                {
                                                    // 1. Build handoff prompt from current session's JSONL
                                                    var handoffPrompt = BuildHandoffPrompt(latest.FullName);
                                                    Console.WriteLine($"[EYE] Handoff prompt built ({handoffPrompt.Length} chars)");

                                                    // 2. Open new chat (Ctrl+N) + verify JSONL change
                                                    using var relayHelper = new ClaudePromptHelper();
                                                    var newChatOk = relayHelper.OpenNewChat();

                                                    // 3. Always write handoff to MEMORY.md as durable backup
                                                    //    → even if schedule fails, next session reads MEMORY.md
                                                    var handoffSection = BuildHandoffSection(latest.FullName, handoffPrompt);
                                                    WriteHandoffToClaudeMd(handoffSection);

                                                    if (newChatOk)
                                                    {
                                                        // 4. Schedule handoff prompt for 1 minute later
                                                        //    → Eye tick will execute it after new chat fully initializes
                                                        var scheduleId = ScheduleManager.Add(new ScheduleItem
                                                        {
                                                            Type = "once",
                                                            ExecuteAt = DateTime.Now.AddMinutes(1).ToString("O"),
                                                            Prompt = handoffPrompt,
                                                            Status = "pending",
                                                            CreatedBy = "auto-relay",
                                                            NotifySlack = true
                                                        });
                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine($"[EYE] ✅ New chat opened! Handoff scheduled (id={scheduleId}, +1min) + CLAUDE.md written");
                                                        Console.ResetColor();
                                                        Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!,
                                                            $":sparkles: *새 채팅 열림!* 핸드오프 프롬프트 1분 후 자동 입력 예약됨 (id={scheduleId})\nCLAUDE.md에도 인수인계 섹션 작성됨 :memo:",
                                                            username: botUsername)).Wait(3000);
                                                    }
                                                    else
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("[EYE] ❌ Failed to open new chat! Handoff written to CLAUDE.md");
                                                        Console.ResetColor();
                                                        Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!,
                                                            $":x: 새 채팅 열기 실패! 수동으로 새 채팅을 열어주세요.\n:memo: CLAUDE.md에 인수인계 섹션이 작성되어 있으니, 새 세션이 자동으로 읽습니다.",
                                                            username: botUsername)).Wait(3000);
                                                    }
                                                }
                                                catch (Exception relayEx)
                                                {
                                                    Console.WriteLine($"[EYE] Auto-relay error: {relayEx.Message}");
                                                    Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!,
                                                        $":x: 자동 릴레이 에러: {relayEx.Message}",
                                                        username: botUsername)).Wait(3000);
                                                }
                                            }
                                        }
                                        else if (pct >= 90 && !contextWarning90Sent && !string.IsNullOrEmpty(slackBotToken))
                                        {
                                            contextWarning90Sent = true;
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine($"[EYE] ⚠️ Context 90%! ({sizeMB:F1}MB/{ContextLimitMB}MB) — 핸드오프 준비 프롬프트 전달!");
                                            Console.ResetColor();
                                            Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!,
                                                $":warning: *컨텍스트 90% 소진!* ({sizeMB:F1}/{ContextLimitMB}MB) 핸드오프 프롬프트 전달 중...",
                                                username: botUsername)).Wait(3000);

                                            // ── Deliver "prepare handoff" prompt to current Claude session ──
                                            if (!contextRelayPromptSent)
                                            {
                                                contextRelayPromptSent = true;
                                                try
                                                {
                                                    using var prepHelper = new ClaudePromptHelper();
                                                    var prompt = prepHelper.FindPrompt();
                                                    if (prompt == null)
                                                    {
                                                        Thread.Sleep(2000);
                                                        prompt = prepHelper.FindPrompt();
                                                    }
                                                    if (prompt != null)
                                                    {
                                                        var prepText = @"[CONTEXT_90%] 컨텍스트 90% 소진! 핸드오프를 준비해주세요.

지금까지 이 세션에서 한 작업을 요약하고, 다음 세션에서 이어갈 수 있는 핸드오프 프롬프트를 작성해주세요.
작업 요약 + 미완성 작업 + 핵심 파일 목록을 포함해주세요.

⚠️ 95%에 도달하면 앱봇의 눈이 자동으로 새 채팅을 열고 핸드오프 프롬프트를 입력합니다!
빠르게 현재 작업을 마무리하고 슬랙으로도 현황을 알려주세요.

(auto-relay by AppBotEye context monitor)";
                                                        prepHelper.TypeAndSubmit(prompt, prepText);
                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine("[EYE] ✅ Handoff preparation prompt delivered!");
                                                        Console.ResetColor();
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("[EYE] WARNING: Could not find Claude prompt for handoff prep");
                                                    }
                                                }
                                                catch (Exception prepEx)
                                                {
                                                    Console.WriteLine($"[EYE] Handoff prep error: {prepEx.Message}");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch { /* best-effort */ }
                            skipContextMonitor:;

                            // Slack status streaming (edit same message)
                            if (slackClient != null && !string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                            {
                                try
                                {
                                    // Never show permission_prompt in status stream
                                    if (claudeStatus.Item1 == "permission_prompt")
                                        goto skipStatusStreaming;

                                    // ── Build status text ──
                                    // Note: idle detection is handled by spinner pixel check below, NOT by prompt_ready.
                                    // prompt_ready from Claude Desktop is unreliable (KRO keeps "executing" active).
                                    string slackText;
                                    if (claudeStatus.Item1 == "prompt_ready")
                                    {
                                        // Skip — spinner detection handles idle below
                                        goto skipStatusStreaming;
                                    }
                                    else
                                    {
                                        // Active: reset idle ONLY if spinner is animating again (proves something is working)
                                        // Without this check: KRO keeps claudeStatus="executing" → ping-pong idle↔active
                                        if (idleMessageSent)
                                        {
                                            // DetectSpinnerIdle returns true=idle. If still idle → skip active status.
                                            // If spinner started again (not idle) → allow active status to proceed.
                                            if (DetectSpinnerIdle(claudeHwnd)) goto skipStatusStreaming;
                                            // Spinner is animating again → activity resumed!
                                            ResetSpinnerDetection();
                                            idleAfterText = null; // clear for next idle cycle
                                        }
                                        idleMessageSent = false;
                                        var statusEmoji = claudeStatus.Item1 switch
                                        {
                                            "executing" => ":gear:",
                                            "plan_approval_pending" => ":clipboard:",
                                            "plan_mode" => ":memo:",
                                            "rate_limit" => ":warning:",
                                            _ => ":robot_face:"
                                        };
                                        slackText = $"{statusEmoji} Claude: {claudeStatus.Item2}";
                                    }

                                    if (slackText == lastSlackStatusText) goto skipStatusStreaming;

                                    // ── Always: delete old → post new (fresh timestamp) ──
                                    if (slackStatusTs != null)
                                    {
                                        var oldTs = slackStatusTs;
                                        Task.Run(async () => await SlackDeleteMessageAsync(
                                            slackBotToken!, slackChannel!, oldTs)).Wait(3000);
                                        slackStatusTs = null;
                                    }
                                    {
                                        var (ok, ts) = Task.Run(async () =>
                                            await SlackSendViaApi(slackBotToken!, slackChannel!, slackText, username: botUsername))
                                            .GetAwaiter().GetResult();
                                        if (ok && ts != null)
                                        {
                                            slackStatusTs = ts;
                                            try { File.WriteAllText(statusTsFile, ts); } catch { }
                                        }
                                        lastSlackStatusText = slackText;
                                    }
                                }
                                catch { /* best-effort */ }
                                skipStatusStreaming:

                                // ── Spinner pixel idle: if Claude Desktop prompt area stops animating → idle ──
                                // Samples pixels above turn-form every tick (~1s).
                                // 2 consecutive identical hashes = no animation = idle.
                                if (!idleMessageSent
                                    && !string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                                {
                                    try
                                    {
                                        if (DetectSpinnerIdle(claudeHwnd))
                                        {
                                            // Spinner stopped → Claude idle!
                                            if (idleAfterText == null)
                                            {
                                                if (lastExecutingText != null)
                                                    idleAfterText = lastExecutingText;
                                                lastExecutingText = null;
                                            }
                                            var idleSuffix = idleAfterText != null ? $" after: {idleAfterText}" : "";
                                            var idleMsg = $":zzz: Idle{idleSuffix}";
                                            // Delete old status → post new idle message
                                            if (slackStatusTs != null)
                                            {
                                                var oldTs = slackStatusTs;
                                                Task.Run(async () => await SlackDeleteMessageAsync(
                                                    slackBotToken!, slackChannel!, oldTs)).Wait(3000);
                                                slackStatusTs = null;
                                            }
                                            var (idleOk, idleTs) = Task.Run(async () =>
                                                await SlackSendViaApi(slackBotToken!, slackChannel!, idleMsg, username: botUsername))
                                                .GetAwaiter().GetResult();
                                            if (idleOk && idleTs != null)
                                            {
                                                slackStatusTs = idleTs;
                                                try { File.WriteAllText(statusTsFile, idleTs); } catch { }
                                            }
                                            lastSlackStatusText = idleMsg;
                                            idleMessageSent = true;
                                            Console.WriteLine($"[EYE] Spinner idle: {idleMsg}");
                                        }
                                    }
                                    catch { }
                                }

                                // Plan approval → Slack
                                if (claudeStatus.Item1 == "plan_approval_pending" && !planApprovalSentToSlack)
                                {
                                    try
                                    {
                                        var plan = ExtractPlanContent(claudeHwnd);
                                        if (plan != null)
                                        {
                                            var planContent = plan.Value.content;
                                            if (planContent.Length > 3500)
                                                planContent = planContent[..3500] + "\n\n... (truncated)";
                                            var sourceLabel = plan.Value.source == "UIA" ? "UIA" : $"`{plan.Value.source}`";
                                            var fallbackText = $":clipboard: 플랜 승인 대기 (via {sourceLabel})\n\n{planContent}";
                                            var blocks = BuildPlanApprovalBlocks(planContent, sourceLabel);
                                            var (sendOk, sendTs) = Task.Run(async () =>
                                                await SlackSendBlocksViaApi(slackBotToken!, slackChannel!, fallbackText, blocks))
                                                .GetAwaiter().GetResult();
                                            if (sendOk)
                                            {
                                                pendingPlanApprovalSlackTs = sendTs;
                                                planApprovalSentToSlack = true;
                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                Console.WriteLine($"[EYE] Plan sent to Slack via {plan.Value.source} (ts={sendTs})");
                                                Console.ResetColor();
                                            }
                                        }
                                        else
                                        {
                                            planApprovalSentToSlack = true; // don't retry
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"[EYE] Plan Slack share error: {ex.Message}");
                                    }
                                }
                                if (claudeStatus.Item1 != "plan_approval_pending" && planApprovalSentToSlack)
                                {
                                    planApprovalSentToSlack = false;
                                    pendingPlanApprovalSlackTs = null;
                                }

                                // Permission prompt → Slack (3s debounce: auto-approved prompts vanish quickly)
                                if (claudeStatus.Item1 == "permission_prompt" && !permissionPromptSentToSlack)
                                {
                                    if (permissionPromptFirstSeen == null)
                                        permissionPromptFirstSeen = DateTime.Now;

                                    if ((DateTime.Now - permissionPromptFirstSeen.Value).TotalSeconds >= 3.0)
                                    {
                                        try
                                        {
                                            var permButtons = GetPermissionButtons(claudeHwnd);
                                            if (permButtons.Count >= 2)
                                            {
                                                var btnList = string.Join(" / ", permButtons);
                                                var fallbackText = $":lock: 수락 요구: [{btnList}]";
                                                var blocks = BuildPermissionBlocks(permButtons, claudeStatus.Item2);
                                                var (sendOk, sendTs) = Task.Run(async () =>
                                                    await SlackSendBlocksViaApi(slackBotToken!, slackChannel!, fallbackText, blocks))
                                                    .GetAwaiter().GetResult();
                                                if (sendOk)
                                                {
                                                    pendingPermissionSlackTs = sendTs;
                                                    permissionPromptSentToSlack = true;
                                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                                    Console.WriteLine($"[EYE] Permission buttons sent to Slack: [{btnList}] (ts={sendTs})");
                                                    Console.ResetColor();
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"[EYE] Permission Slack share error: {ex.Message}");
                                        }
                                    }
                                }
                                if (claudeStatus.Item1 != "permission_prompt")
                                {
                                    permissionPromptFirstSeen = null; // reset debounce
                                    if (permissionPromptSentToSlack)
                                    {
                                        permissionPromptSentToSlack = false;
                                        pendingPermissionSlackTs = null;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Claude status null — tick-file idle will handle Slack status
                            cachedClaudeStatusText = null;

                            // Claude status null (idle) — ★ 리밋 중에 idle 감지 = 리밋 해제!
                            if (wasRateLimited)
                            {
                                // idle 상태 = Claude가 정상 동작 중 → 리밋 즉시 해제
                                {
                                    wasRateLimited = false;
                                    rateLimitDetectedAt = null;
                                    rateLimitResetTime = null;
                                    cachedClaudeStatusText = null;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"[EYE] Rate limit cleared (idle status detected — limit lifted)");
                                    Console.ResetColor();

                                    try
                                    {
                                        var resetItems = ScheduleManager.GetOnLimitResetItems();
                                        foreach (var resetItem in resetItems)
                                        {
                                            ExecuteScheduleItem(resetItem, slackBotToken, slackChannel);
                                            Thread.Sleep(2000);
                                        }
                                    }
                                    catch { }
                                }
                            }
                            else
                            {
                                cachedClaudeStatusText = null;
                            }
                        }
                    }
                    catch { /* best-effort */ }
                }
                // ★ FIX: claudeHwnd lost — still update Slack to idle if needed
                else if (!string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel)
                    && slackStatusTs != null
                    && lastSlackStatusText != null
                    && !lastSlackStatusText.Contains("프롬프트 대기")
                    && !lastSlackStatusText.Contains("창 없음"))
                {
                    try
                    {
                        var idleText = ":zzz: Claude 창 없음 — 프롬프트 대기";
                        Task.Run(async () => await SlackUpdateMessageAsync(
                            slackBotToken!, slackChannel!, slackStatusTs, idleText))
                            .Wait(3000);
                        lastSlackStatusText = idleText;
                        cachedClaudeStatusText = null;
                        Console.WriteLine($"[EYE] Claude window lost → Slack idle");
                    }
                    catch { }
                }
            }

            // ── Schedule executor (~every 10 seconds) ──
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
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[EYE][SLACK] No events received in 5+ minutes — attempting reconnect...");
                    Console.ResetColor();
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

            // ── Hot-reload check (~every 5 sec) ──
            // Detects: (1) EXE timestamp change (direct overwrite), (2) .new.exe staged (locked EXE fallback)
            if (frameCount % 50 == 0 && exeStartTime != DateTime.MinValue)
            {
                try
                {
                    var newExePath = Path.Combine(Path.GetDirectoryName(exePath) ?? "", "wkappbot.new.exe");
                    if (File.Exists(newExePath))
                    {
                        // .new.exe staged by PostPublish (EXE was locked) — swap and restart
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[EYE] Hot-reload: wkappbot.new.exe detected — swapping");
                        Console.ResetColor();
                        bool swapped = false;
                        try
                        {
                            File.Delete(exePath);
                            File.Move(newExePath, exePath);
                            Console.WriteLine("[EYE] Hot-reload: EXE swapped successfully");
                            swapped = true;
                        }
                        catch (Exception swapEx)
                        {
                            Console.WriteLine($"[EYE] Hot-reload: swap failed ({swapEx.Message})");
                            // Delete .new.exe to prevent infinite restart loop
                            try { File.Delete(newExePath); } catch { }
                            Console.WriteLine("[EYE] Hot-reload: deleted .new.exe to prevent restart loop — continuing");
                        }
                        if (swapped) break; // only restart if swap succeeded
                    }
                    else if (File.GetLastWriteTimeUtc(exePath) != exeStartTime)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[EYE] Hot-reload: EXE changed — shutting down");
                        Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[EYE][SLACK] Health check: connected={slackClient.IsConnected} age={connAge:F0}m → force reconnect");
                        Console.ResetColor();
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

            // ── Periodic GC (~every 5 min = 3000 frames @ 100ms) ──
            if (frameCount % 3000 == 0 && frameCount > 0)
            {
                var beforeMB = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
                GC.Collect(2, GCCollectionMode.Optimized);
                GC.WaitForPendingFinalizers();
                GC.Collect(2, GCCollectionMode.Optimized);
                var afterMB = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[EYE][GC] Gen2 collect: {beforeMB}MB → {afterMB}MB (freed {beforeMB - afterMB}MB)");
                Console.ResetColor();
            }

            // ── Stats logging ──
            if (frameCount % 100 == 0 && frameCount > 0)
            {
                var slackInfo = slackClient != null
                    ? $", Slack={slackClient.IsConnected}, msgs={slackClient.MessageCount}, reconn={slackClient.ReconnectCount}"
                    : "";
                Console.WriteLine($"[EYE] frame #{frameCount} ({(slackClient != null ? "Socket+API" : "API-only")}{slackInfo})");
            }

            frameCount++;
            Thread.Sleep(Math.Max(100, intervalMs));
        }

        // ── Cleanup ──
        WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
            WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS);

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
        }
        swTick.Stop();

        var swPrompt = Stopwatch.StartNew();
        var promptDiag = new PromptDiag();
        var promptPreview = _cachedPromptPreview;
        if (forceFull || promptDirty || string.IsNullOrWhiteSpace(promptPreview))
        {
            promptPreview = ReadLatestOpenClawPromptPreview(promptDiag);
            _cachedPromptPreview = promptPreview;
        }
        else
        {
            promptDiag.Source = "sessions-cache";
            promptDiag.CacheMs = 1;
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
        host.UpdateAccessibilityText(BuildEyeSummary(cards, latest, promptPreview));

        swTotal.Stop();

        var mode = forceFull ? "full-1s" : (tickDirty || promptDirty ? "dirty" : "idle");
        // Memory delta tracking
        var proc = Process.GetCurrentProcess();
        var wsMB = proc.WorkingSet64 / (1024 * 1024);
        var deltaMB = wsMB - _prevWorkingSetMB;
        if (wsMB > _peakWorkingSetMB) _peakWorkingSetMB = wsMB;
        _prevWorkingSetMB = wsMB;
        var memSuffix = deltaMB != 0 ? $" mem={wsMB}MB({(deltaMB >= 0 ? "+" : "")}{deltaMB}) peak={_peakWorkingSetMB}MB" : "";

        Console.WriteLine($"[EYE_TICK] mode={mode} tick={swTick.ElapsedMilliseconds}ms(read={tickRead}ms,parse={tickParse}ms,activity=0ms) " +
                          $"prompt={swPrompt.ElapsedMilliseconds}ms(stat={promptDiag.StatMs}ms,read={promptDiag.ReadMs}ms,scan={promptDiag.ScanMs}ms,parse={promptDiag.ParseMs}ms,norm={promptDiag.NormMs}ms,cache={promptDiag.CacheMs}ms) " +
                          $"schedule={swSchedule.ElapsedMilliseconds}ms total={swTotal.ElapsedMilliseconds}ms{memSuffix}");
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
        if (tickDirty) _fswTickDirty = false;    // consume flag
        if (promptDirty) _fswPromptDirty = false; // consume flag

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
                _fswPrompt.Changed += (_, _) => _fswPromptDirty = true;
                _fswPrompt.Created += (_, _) => _fswPromptDirty = true;
                _fswPrompt.Renamed += (_, _) => _fswPromptDirty = true;
                Console.WriteLine($"[EYE][FSW] Prompt watcher: {sessionsDir}/*.jsonl");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE][FSW] Prompt watcher init failed: {ex.Message}");
        }
    }

    static void DisposeFileWatchers()
    {
        try { if (_fswTick != null) { _fswTick.EnableRaisingEvents = false; _fswTick.Dispose(); _fswTick = null; } } catch { }
        try { if (_fswPrompt != null) { _fswPrompt.EnableRaisingEvents = false; _fswPrompt.Dispose(); _fswPrompt = null; } } catch { }
        Console.WriteLine("[EYE][FSW] Watchers disposed");
    }

    static int EyeTickCommand(string[] args)
    {
        try
        {
            if (_lastTickActivityUtc == DateTime.MinValue) _lastTickActivityUtc = DateTime.UtcNow;
            if (_lastAiActivityUtc == DateTime.MinValue) _lastAiActivityUtc = DateTime.UtcNow;
            var swTotal = Stopwatch.StartNew();
            long tickRead = 0, tickParse = 0;
            var latest = ReadLatestTick(out tickRead, out tickParse);
            var promptDiag = new PromptDiag();
            var prompt = ReadLatestOpenClawPromptPreview(promptDiag);
            var cards = ReadEyeCards(staleSeconds: 86400); // 24 hours
            _lastPromptSource = promptDiag.Source;

            Console.WriteLine("[EYE] one-shot tick");
            Console.WriteLine($"[EYE_TICK] tick={tickRead + tickParse}ms(read={tickRead}ms,parse={tickParse}ms,activity=0ms) " +
                              $"prompt={promptDiag.StatMs + promptDiag.ReadMs + promptDiag.ScanMs + promptDiag.ParseMs + promptDiag.NormMs + promptDiag.CacheMs}ms(stat={promptDiag.StatMs}ms,read={promptDiag.ReadMs}ms,scan={promptDiag.ScanMs}ms,parse={promptDiag.ParseMs}ms,norm={promptDiag.NormMs}ms,cache={promptDiag.CacheMs}ms) schedule=0ms total={swTotal.ElapsedMilliseconds}ms");
            Console.WriteLine($"[EYE_TICK] hint promptLine={_lastPromptLineIndex} tickLine={_lastEyeTickLineIndex}");
            Console.WriteLine($"[EYE_TICK] cards={cards.Count} promptSource={promptDiag.Source}");
            if (!string.IsNullOrWhiteSpace(prompt))
                Console.WriteLine($"[EYE_TICK] recent={prompt}");

            var plans = ExtractRecentPlanItems(maxItems: 3);
            if (plans.Count > 0)
            {
                for (int i = 0; i < plans.Count; i++)
                    Console.WriteLine($"[EYE_PLAN] —:— {plans[i]}");
                if (_lastPlanItemsCache.Count > plans.Count)
                    Console.WriteLine($"[EYE_PLAN] —:— 그 외 {_lastPlanItemsCache.Count - plans.Count}건...");
            }

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

            // ── Build final card display (same as eye overlay) and print ──
            var summary = BuildEyeSummary(cards, latest, prompt);
            Console.WriteLine($"[EYE_TICK] ── card display ──");
            foreach (var line in summary.Split('\n'))
                Console.WriteLine($"[EYE_TICK] {line.TrimEnd('\r')}");

            // ── Slack inbox check + forward to Claude prompt ──
            EyeTickForwardSlackInbox();

            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE_TICK] error={ex.Message}");
            return 1;
        }
    }

    /// <summary>
    /// EyeTick one-shot: check Slack for new messages via conversations.history API
    /// and forward them to Claude prompt. Uses slack_last_ts.txt to track position.
    /// Also checks slack_inbox.jsonl as fallback (from standalone slack listen).
    /// </summary>
    static void EyeTickForwardSlackInbox()
    {
        try
        {
            // Load Slack config
            var configPath = Path.Combine(DataDir, "profiles", "slack_exp", "webhook.json");
            if (!File.Exists(configPath))
            {
                Console.WriteLine("[EYE_TICK] slack=no_config");
                return;
            }

            var json = System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(configPath));
            var botToken = json?["bot_token"]?.GetValue<string>();
            var channel = json?["channel"]?.GetValue<string>();
            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
            {
                Console.WriteLine("[EYE_TICK] slack=missing_token_or_channel");
                return;
            }

            // Get bot user ID to filter self-messages
            var botUserId = SlackGetBotUserId(botToken);

            // Get last processed timestamp
            var lastTs = LoadLastTs(channel);
            var lastTsDouble = 0.0;
            if (!string.IsNullOrEmpty(lastTs))
                double.TryParse(lastTs, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out lastTsDouble);

            // Fetch latest 20 messages (no oldest param — Slack API oldest returns ascending order,
            // causing limit to clip old messages instead of new ones. Filter by ts in code instead.)
            var messages = SlackFetchHistoryAsync(botToken, channel, limit: 20)
                .GetAwaiter().GetResult();

            if (messages.Count == 0)
            {
                Console.WriteLine("[EYE_TICK] slack=no_new_messages");
                return;
            }

            // Thread reply check uses ClaudePromptHelper — create early for shared use
            using var helper = new ClaudePromptHelper();

            // Separate: context message (last processed) vs new messages (after lastTs)
            string? contextLine = null; // last processed message for conversation context
            var newMsgs = new List<(string user, string text, string ts)>();
            foreach (var msg in messages)
            {
                var user = msg["user"]?.GetValue<string>() ?? "";
                var text = msg["text"]?.GetValue<string>() ?? "";
                var ts = msg["ts"]?.GetValue<string>() ?? "";
                var subtype = msg["subtype"]?.GetValue<string>();

                // Skip subtypes (bot_message, channel_join, etc.)
                if (!string.IsNullOrEmpty(subtype)) continue;
                if (string.IsNullOrWhiteSpace(text)) continue;

                // Parse ts for comparison
                double.TryParse(ts, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out var tsDouble);

                // Message at lastTs = context (already processed, include for conversation flow)
                if (ts == lastTs)
                {
                    var ctxClean = System.Text.RegularExpressions.Regex.Replace(text, @"<@[A-Z0-9]+>\s*", "").Trim();
                    var who = user == botUserId ? "클롣" : $"@{user}";
                    contextLine = $"[직전 대화] {who}: {ctxClean}";
                    continue;
                }

                // Skip messages older than or equal to lastTs (already processed)
                if (lastTsDouble > 0 && tsDouble <= lastTsDouble) continue;

                // Skip bot's own messages (for new messages only)
                if (user == botUserId) continue;

                newMsgs.Add((user, text, ts));
            }

            if (newMsgs.Count == 0)
            {
                Console.WriteLine("[EYE_TICK] slack=no_new_messages");
                // Still check thread replies even if no new channel messages
                EyeTickCheckThreadReplies(messages, botToken, channel, botUserId, helper);
                return;
            }

            // Reverse so oldest first (API returns newest first)
            newMsgs.Reverse();

            Console.WriteLine($"[EYE_TICK] slack={newMsgs.Count} new message(s) — forwarding to Claude prompt...");

            // Find Claude prompt
            var promptInfo = helper.FindPrompt();
            if (promptInfo == null)
            {
                Console.WriteLine("[EYE_TICK] WARNING: Claude prompt not found — will retry next tick");
                return;
            }

            int forwarded = 0;
            string? latestTs = null;
            foreach (var (user, text, ts) in newMsgs)
            {
                // Clean @mention tags
                var cleanText = System.Text.RegularExpressions.Regex.Replace(text, @"<@[A-Z0-9]+>\s*", "").Trim();
                if (string.IsNullOrWhiteSpace(cleanText)) continue;

                // Include context from last processed message for conversation flow
                var contextPrefix = (contextLine != null && forwarded == 0) ? $"{contextLine}\n\n" : "";
                var promptText = $"{contextPrefix}{cleanText}\n\n{SlackReplySuffix(user, ts)}";

                // Re-find prompt each time (window may shift)
                var fresh = helper.FindPrompt();
                if (fresh == null)
                {
                    Console.WriteLine("[EYE_TICK] WARNING: Lost prompt — stopping forward");
                    break;
                }

                Console.WriteLine($"[EYE_TICK] [FORWARD] Slack @{user} → Claude prompt");
                var ok = helper.TypeAndSubmit(fresh, promptText);
                if (ok)
                {
                    forwarded++;
                    latestTs = ts;
                    Console.WriteLine($"[EYE_TICK] [DELIVERED] Slack @{user}: {cleanText}");

                    // Send "전달했습니다" ack — deleted when slack reply is sent
                    try
                    {
                        var ackText = $"Claude에 전달했습니다! (thread={ts})";
                        var (ackOk, ackTs) = SlackSendViaApi(botToken, channel, ackText, ts, username: BotUsername)
                            .GetAwaiter().GetResult();
                        if (ackOk && ackTs != null)
                            SavePendingAck(ts, channel, ackTs);
                    }
                    catch { }

                    if (newMsgs.Count > 1)
                        Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine($"[EYE_TICK] WARNING: TypeAndSubmit failed for @{user}");
                }
            }

            // Save last processed timestamp
            if (latestTs != null)
            {
                SaveLastTs(channel, latestTs);
                Console.WriteLine($"[EYE_TICK] slack forwarded={forwarded}/{newMsgs.Count} — lastTs updated");
            }

            // ── Thread reply detection ──
            // Check recent bot messages for user thread replies
            EyeTickCheckThreadReplies(messages, botToken, channel, botUserId, helper);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE_TICK] slack error: {ex.Message}");
        }
    }

    /// <summary>
    /// Check recent bot messages for new thread replies from users.
    /// Responds when: (1) parent is from bot (클롣) → any user reply, or (2) @mention in reply.
    /// </summary>
    static void EyeTickCheckThreadReplies(List<System.Text.Json.Nodes.JsonNode> channelMessages,
        string botToken, string channel, string? botUserId, ClaudePromptHelper helper)
    {
        try
        {
            // Find bot messages with replies (max 5 threads to check)
            var botThreads = new List<(string threadTs, string parentText)>();
            foreach (var msg in channelMessages)
            {
                var user = msg["user"]?.GetValue<string>() ?? "";
                var botId = msg["bot_id"]?.GetValue<string>();
                var subtype = msg["subtype"]?.GetValue<string>();
                var replyCount = msg["reply_count"]?.GetValue<int>() ?? 0;
                var ts = msg["ts"]?.GetValue<string>() ?? "";
                var text = msg["text"]?.GetValue<string>() ?? "";

                if (replyCount <= 0) continue;

                // Skip threads older than 1 hour — prevents old message flood on Eye restart
                if (!string.IsNullOrEmpty(ts))
                {
                    if (double.TryParse(ts, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out var tsEpoch))
                    {
                        var ageSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - tsEpoch;
                        if (ageSeconds > 3600)
                        {
                            // Silently skip — old thread, no need to scan replies
                            continue;
                        }
                    }
                }

                // Is this a bot message? (either user==botUserId or has bot_id or subtype==bot_message)
                bool isBotMsg = user == botUserId
                    || !string.IsNullOrEmpty(botId)
                    || subtype == "bot_message";
                if (!isBotMsg) continue;

                botThreads.Add((ts, text));
                if (botThreads.Count >= 5) break;
            }

            if (botThreads.Count == 0) return;

            int threadReplies = 0;
            foreach (var (threadTs, parentText) in botThreads)
            {
                // Load per-thread last reply ts
                var threadKey = $"{channel}_t_{threadTs}";
                var lastReplyTs = LoadLastTs(threadKey);
                double lastReplyTsDouble = 0;
                if (!string.IsNullOrEmpty(lastReplyTs))
                    double.TryParse(lastReplyTs, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out lastReplyTsDouble);

                // Fetch thread replies
                var replies = SlackFetchRepliesAsync(botToken, channel, threadTs, limit: 10)
                    .GetAwaiter().GetResult();

                string? latestReplyTs = null;
                foreach (var reply in replies)
                {
                    var rTs = reply["ts"]?.GetValue<string>() ?? "";
                    var rUser = reply["user"]?.GetValue<string>() ?? "";
                    var rText = reply["text"]?.GetValue<string>() ?? "";
                    var rBotId = reply["bot_id"]?.GetValue<string>();
                    var rSubtype = reply["subtype"]?.GetValue<string>();

                    // Skip parent message itself
                    if (rTs == threadTs) continue;
                    // Skip bot's own replies
                    if (rUser == botUserId || !string.IsNullOrEmpty(rBotId) || rSubtype == "bot_message") continue;
                    // Skip empty
                    if (string.IsNullOrWhiteSpace(rText)) continue;

                    // Skip already processed
                    double.TryParse(rTs, System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture, out var rTsDouble);
                    if (lastReplyTsDouble > 0 && rTsDouble <= lastReplyTsDouble) continue;

                    // Check: @mention tag → always respond
                    bool hasMention = !string.IsNullOrEmpty(botUserId) && rText.Contains($"<@{botUserId}>");

                    // For non-mention replies, parent must be from bot (already filtered above)
                    // Both cases pass — bot thread reply OR @mention

                    // Clean text
                    var cleanReply = System.Text.RegularExpressions.Regex.Replace(rText, @"<@[A-Z0-9]+>\s*", "").Trim();
                    if (string.IsNullOrWhiteSpace(cleanReply)) continue;

                    // Build context: bot's original message (truncated)
                    var cleanParent = System.Text.RegularExpressions.Regex.Replace(parentText, @"<@[A-Z0-9]+>\s*", "").Trim();
                    if (cleanParent.Length > 80) cleanParent = cleanParent[..80] + "…";

                    var promptText = $"[쓰레드 시작] {cleanParent}\n\n{cleanReply}\n\n{SlackReplySuffix(rUser, threadTs, "thread reply")}";

                    var fresh = helper.FindPrompt();
                    if (fresh == null)
                    {
                        Console.WriteLine("[EYE_TICK] WARNING: Lost prompt — stopping thread reply forward");
                        return;
                    }

                    Console.WriteLine($"[EYE_TICK] [FORWARD] Thread @{rUser} → Claude prompt");
                    var ok = helper.TypeAndSubmit(fresh, promptText);
                    if (ok)
                    {
                        threadReplies++;
                        latestReplyTs = rTs;
                        Console.WriteLine($"[EYE_TICK] [DELIVERED] Thread @{rUser}: {cleanReply}");

                        // Send "전달했습니다" ack — deleted when slack reply is sent
                        try
                        {
                            var ackText = $"Claude에 전달했습니다! (thread={threadTs})";
                            var (ackOk, ackTs) = SlackSendViaApi(botToken, channel, ackText, threadTs, username: BotUsername)
                                .GetAwaiter().GetResult();
                            if (ackOk && ackTs != null)
                                SavePendingAck(threadTs, channel, ackTs);
                        }
                        catch { }

                        Thread.Sleep(2000);
                    }
                }

                // Update per-thread last reply ts
                if (latestReplyTs != null)
                    SaveLastTs(threadKey, latestReplyTs);
            }

            if (threadReplies > 0)
                Console.WriteLine($"[EYE_TICK] thread_replies={threadReplies} forwarded");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE_TICK] thread check error: {ex.Message}");
        }
    }

    static string BuildEyeSummary(List<EyeParentCard> cards, EyeTick? latest, string prompt)
    {
        var sb = new StringBuilder();

        // ── Action triplet (always on top — real-time accessibility probe) ──
        var (a11y, act, fallback) = ReadLatestActionTriplet();
        if (!string.IsNullOrWhiteSpace(a11y)) sb.AppendLine($"엑빌: {a11y}");
        if (!string.IsNullOrWhiteSpace(act)) sb.AppendLine($"액션: {act}");
        if (!string.IsNullOrWhiteSpace(fallback)) sb.AppendLine($"폴백: {fallback}");

        if (!string.IsNullOrWhiteSpace(prompt))
        {
            // Truncate to ~60 chars to keep overlay compact — cards must stay visible
            var truncated = prompt.Length > 60 ? prompt[..60] + "..." : prompt;
            sb.AppendLine($"최근 생각: {truncated}");
        }

        // ── Build KRO section text (rendered inline with cards by recency) ──
        // KRO timestamp = time when prompt CONTENT last changed (not file mtime, not eye tick time)
        // File mtime can update without content change; eye tick is CLI's time, not KRO's.
        string kroBlock = "";
        DateTime kroTsUtc = DateTime.MinValue;
        if (latest != null)
        {
            // KRO timestamp from file-based card cache (content-change detection)
            var kroContent = prompt ?? "";
            kroTsUtc = CardCacheGetTimestamp("kro", kroContent);

            var kroSb = new StringBuilder();
            // KRO's home is ~/.openclaw/, not the CLI command's CWD
            var openClawDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw");
            var kroCwd = AbbreviateCwd(openClawDir);
            var (progress, done, next, block) = BuildKroStatus3(latest);
            kroSb.AppendLine($"크로 진행: {progress}" + (string.IsNullOrWhiteSpace(kroCwd) ? "" : $" [{kroCwd}]"));
            kroSb.AppendLine($"크로 완료: {done}");
            kroSb.AppendLine($"크로 예정: {next}");

            var plans = ExtractRecentPlanItems(maxItems: 3);
            if (plans.Count > 0)
            {
                for (int i = 0; i < plans.Count; i++)
                    kroSb.AppendLine($"- —:— {plans[i]}");
                if (_lastPlanItemsCache.Count > plans.Count)
                    kroSb.AppendLine($"- —:— 그 외 {_lastPlanItemsCache.Count - plans.Count}건...");
            }

            if (!string.IsNullOrWhiteSpace(block))
                kroSb.AppendLine($"크로 이슈: {block}");
            kroBlock = kroSb.ToString().TrimEnd();
        }

        // ── Apply CardCache to 클롣 cards — timestamp reflects content change, not tick time ──
        // If tag/status hasn't changed, the card keeps its old timestamp (won't jump to top)
        foreach (var c in cards)
        {
            var clotContent = $"{c.LastTag}|{c.LastStatus}";
            var cwdAbbrev = AbbreviateCwd(c.Cwd);
            var clotKey = !string.IsNullOrEmpty(cwdAbbrev) ? $"clot_{cwdAbbrev}" : $"clot_pid{c.ParentPid}";
            var cachedTs = CardCacheGetTimestamp(clotKey, clotContent);
            if (cachedTs == DateTime.MinValue)
            {
                // First encounter — seed with tick timestamp (new card should appear at correct position)
                cachedTs = c.LastTsUtc;
                _cardCache[clotKey] = (clotContent, cachedTs);
                CardCacheSave(clotKey, clotContent, cachedTs);
            }
            c.LastTsUtc = cachedTs;
        }

        // ── Sort ALL cards (including KRO) by recency — newest on top ──
        // KRO = Claude Code itself (~/.openclaw/), cards = individual CLI commands (each with own CWD)
        // They share the same Claude Desktop host but are separate entities — no dedup needed
        // Hide KRO if its tick is older than 24h (stale — KRO not active)
        bool kroStale = kroTsUtc != DateTime.MinValue && (DateTime.UtcNow - kroTsUtc).TotalHours > 24;
        bool hasKro = !string.IsNullOrWhiteSpace(kroBlock) && !kroStale;
        var sortedCards = cards.OrderByDescending(x => x.LastTsUtc).Take(6).ToList();
        bool kroRendered = !hasKro;  // if no KRO or stale, mark as already rendered

        if (sortedCards.Count == 0 && !hasKro)
        {
            sb.AppendLine("(active cards: 0)");
        }
        else
        {
            // Interleave: render each item in time order (newest first)
            int cardIdx = 0;
            while (cardIdx < sortedCards.Count || !kroRendered)
            {
                bool renderKroNow = false;
                if (!kroRendered)
                {
                    if (cardIdx >= sortedCards.Count)
                        renderKroNow = true;
                    else if (kroTsUtc > sortedCards[cardIdx].LastTsUtc)
                        renderKroNow = true;
                }

                if (renderKroNow)
                {
                    sb.AppendLine(kroBlock);
                    sb.AppendLine("----");
                    kroRendered = true;
                }
                else if (cardIdx < sortedCards.Count)
                {
                    var c = sortedCards[cardIdx++];
                    var age = Math.Max(0, (int)(DateTime.UtcNow - c.LastTsUtc).TotalSeconds);
                    var cwdTag = AbbreviateCwd(c.Cwd);
                    var ageText = age < 60 ? $"{age}초 전" : age < 3600 ? $"{age / 60}분 전" : $"{age / 3600}시간 전";

                    // Header: [CWD] or process info
                    var header = string.IsNullOrWhiteSpace(cwdTag)
                        ? (string.IsNullOrWhiteSpace(c.ParentTitle) ? $"{c.ParentName}:{c.ParentPid}" : c.ParentTitle)
                        : cwdTag;
                    sb.AppendLine($"[{header}] {c.ParentName}:{c.ParentPid}");
                    // If last tick is older than 30s, show idle state instead of stale command info
                    if (age > 30)
                    {
                        sb.AppendLine($"클롣 상태: 대기중 ({ageText})");
                    }
                    else
                    {
                        sb.AppendLine($"클롣 작업: {c.LastTag}");
                        sb.AppendLine($"클롣 상태: {c.LastStatus} ({ageText})");
                    }
                    sb.AppendLine("----");
                }
                else
                {
                    break;
                }
            }
        }

        return sb.ToString().TrimEnd();
    }

    /// <summary>
    /// Abbreviate a working directory path for compact display.
    /// Drive letter + first letter of each intermediate folder + "-" + leaf folder.
    /// e.g. "W:\GitHub\WKAppBot" → "WG-WKAppBot"
    ///      "W:\HTS-Project\Source\Main\MainLib" → "WHSM-MainLib"
    ///      "W:\VIGSOne" → "W-VIGSOne"
    /// </summary>
    static string AbbreviateCwd(string? cwd)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return "";
        var path = cwd.Replace('\\', '/').TrimEnd('/');
        var parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) return "";
        var drive = parts[0].TrimEnd(':').ToUpperInvariant();  // "W:" → "W"
        if (parts.Length <= 1) return drive;
        var leaf = parts[^1];
        // Middle folders → take first char of each as uppercase initial
        var initials = "";
        for (int i = 1; i < parts.Length - 1; i++)
            if (parts[i].Length > 0) initials += char.ToUpperInvariant(parts[i][0]);
        return $"{drive}{initials}-{leaf}";
    }

    /// <summary>
    /// File-based card cache: stores each card's content + last-changed timestamp.
    /// Content-change detection: timestamp only updates when content actually differs.
    /// Survives process restarts (one-shot eye tick, eye restart, etc.)
    /// Files: wkappbot.hq/runtime/card_cache/{cardKey}.json
    /// </summary>
    static string GetCardCacheDir()
    {
        if (string.IsNullOrEmpty(_cardCacheDir))
        {
            _cardCacheDir = Path.Combine(DataDir, "runtime", "card_cache");
            Directory.CreateDirectory(_cardCacheDir);
        }
        return _cardCacheDir;
    }

    /// <summary>
    /// Get the cached changedUtc for a card, updating if content changed.
    /// Returns DateTime.MinValue if card has never been seen.
    /// </summary>
    static DateTime CardCacheGetTimestamp(string cardKey, string currentContent)
    {
        // Memory cache first
        if (_cardCache.TryGetValue(cardKey, out var cached))
        {
            if (cached.content == currentContent)
                return cached.changedUtc;
            // Content changed — update
            var now = DateTime.UtcNow;
            _cardCache[cardKey] = (currentContent, now);
            CardCacheSave(cardKey, currentContent, now);
            return now;
        }

        // Cold start — load from disk
        var diskEntry = CardCacheLoad(cardKey);
        if (diskEntry.HasValue)
        {
            if (diskEntry.Value.content == currentContent)
            {
                _cardCache[cardKey] = diskEntry.Value;
                return diskEntry.Value.changedUtc;
            }
            // Disk content differs from current — content changed
            var now = DateTime.UtcNow;
            _cardCache[cardKey] = (currentContent, now);
            CardCacheSave(cardKey, currentContent, now);
            return now;
        }

        // Never seen — first encounter, use MinValue (card goes to bottom)
        _cardCache[cardKey] = (currentContent, DateTime.MinValue);
        CardCacheSave(cardKey, currentContent, DateTime.MinValue);
        return DateTime.MinValue;
    }

    static void CardCacheSave(string cardKey, string content, DateTime changedUtc)
    {
        try
        {
            var file = Path.Combine(GetCardCacheDir(), $"{cardKey}.json");
            var json = JsonSerializer.Serialize(new { content, changedUtc = changedUtc.ToString("O") });
            var tmp = file + ".tmp";
            File.WriteAllText(tmp, json);
            File.Move(tmp, file, overwrite: true);
        }
        catch { }
    }

    static (string content, DateTime changedUtc)? CardCacheLoad(string cardKey)
    {
        try
        {
            var file = Path.Combine(GetCardCacheDir(), $"{cardKey}.json");
            if (!File.Exists(file)) return null;
            var node = JsonNode.Parse(File.ReadAllText(file));
            var content = node?["content"]?.GetValue<string>() ?? "";
            var tsStr = node?["changedUtc"]?.GetValue<string>() ?? "";
            if (DateTime.TryParse(tsStr, null, System.Globalization.DateTimeStyles.RoundtripKind, out var ts))
                return (content, ts.ToUniversalTime());
            return (content, DateTime.MinValue);
        }
        catch { return null; }
    }

    static (int x, int y) GetRightmostMonitorAnchor(int width, int height)
    {
        try
        {
            var screens = Screen.AllScreens;
            if (screens != null && screens.Length > 0)
            {
                var rightMost = screens.OrderByDescending(s => s.Bounds.Right).First();
                var x = rightMost.Bounds.Right - width - 10;
                var y = rightMost.Bounds.Top + 10;
                return (x, y);
            }
        }
        catch { }

        return (1530, 110);
    }

    static EyeTick? ReadLatestTick(out long readMs, out long parseMs)
    {
        readMs = 0; parseMs = 0;
        var path = EyeTicksPath;
        if (!File.Exists(path)) return null;

        var swRead = Stopwatch.StartNew();
        var lines = ReadTailLinesShared(path, 64 * 1024);
        swRead.Stop();
        readMs = swRead.ElapsedMilliseconds;

        var swParse = Stopwatch.StartNew();
        var start = _lastEyeTickFile == path ? Math.Max(0, _lastEyeTickLineIndex - 1) : 0;
        for (int i = lines.Length - 1; i >= start; i--)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            try
            {
                var t = JsonSerializer.Deserialize<EyeTick>(lines[i]);
                if (t != null)
                {
                    _lastEyeTickFile = path;
                    _lastEyeTickLineIndex = i;
                    swParse.Stop();
                    parseMs = swParse.ElapsedMilliseconds;
                    return t;
                }
            }
            catch { }
        }
        swParse.Stop();
        parseMs = swParse.ElapsedMilliseconds;
        return null;
    }

    static string ReadLatestOpenClawPromptPreview(PromptDiag diag)
    {
        var sw = Stopwatch.StartNew();
        var sessionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions");
        if (!Directory.Exists(sessionsDir)) return "";

        var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl")
            .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
            .FirstOrDefault();
        diag.StatMs = sw.ElapsedMilliseconds;
        if (string.IsNullOrWhiteSpace(latestFile)) return "";

        var fi = new FileInfo(latestFile);
        if (latestFile == _lastPromptSessionFile && fi.Length == _lastPromptSessionLength && fi.LastWriteTimeUtc == _lastPromptSessionWriteUtc)
        {
            diag.CacheMs = 1;
            diag.Source = "sessions-cache";
            return _lastPromptPreviewCache;
        }

        string selected = "";
        int selectedLine = -1;
        var windows = new[] { 64 * 1024, 128 * 1024, 256 * 1024 };

        foreach (var tailBytes in windows)
        {
            sw.Restart();
            var lines = ReadTailLinesShared(latestFile, tailBytes);
            diag.ReadMs += sw.ElapsedMilliseconds;

            sw.Restart();
            string bestAssistant = "";
            string bestUser = "";
            int bestLine = -1;
            var start = latestFile == _lastPromptSessionFile ? Math.Max(0, _lastPromptLineIndex - 2) : 0;
            if (start >= lines.Length) start = 0;

            for (int i = lines.Length - 1; i >= start; i--)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (!line.Contains("\"role\"", StringComparison.OrdinalIgnoreCase)) continue;
                if (!line.Contains("assistant", StringComparison.OrdinalIgnoreCase) && !line.Contains("user", StringComparison.OrdinalIgnoreCase)) continue;

                var parseStart = Stopwatch.StartNew();
                if (TryExtractRoleAndText(line, out var role, out var text))
                {
                    parseStart.Stop();
                    diag.ParseMs += parseStart.ElapsedMilliseconds;

                    var nsw = Stopwatch.StartNew();
                    text = NormalizePrompt(text);
                    nsw.Stop();
                    diag.NormMs += nsw.ElapsedMilliseconds;
                    if (string.IsNullOrWhiteSpace(text)) continue;

                    if (role == "assistant" && string.IsNullOrWhiteSpace(bestAssistant))
                    {
                        bestAssistant = text;
                        bestLine = i;
                        break;
                    }
                    if (role == "user" && string.IsNullOrWhiteSpace(bestUser))
                    {
                        bestUser = text;
                        if (bestLine < 0) bestLine = i;
                    }
                }
                else
                {
                    parseStart.Stop();
                    diag.ParseMs += parseStart.ElapsedMilliseconds;
                }
            }
            diag.ScanMs += sw.ElapsedMilliseconds;

            selected = !string.IsNullOrWhiteSpace(bestAssistant) ? bestAssistant : bestUser;
            selectedLine = bestLine;
            if (!string.IsNullOrWhiteSpace(selected)) break;
        }

        sw.Restart();
        _lastPromptSessionFile = latestFile;
        _lastPromptLineIndex = selectedLine;
        _lastPromptPreviewCache = selected;
        _lastPromptSessionLength = fi.Length;
        _lastPromptSessionWriteUtc = fi.LastWriteTimeUtc;
        diag.CacheMs = sw.ElapsedMilliseconds;
        diag.Source = string.IsNullOrWhiteSpace(selected) ? "none" : "sessions";
        return selected;
    }

    static bool TryExtractRoleAndText(string jsonLine, out string role, out string text)
    {
        role = "";
        text = "";
        try
        {
            using var doc = JsonDocument.Parse(jsonLine);
            var root = doc.RootElement;
            if (!root.TryGetProperty("message", out var msg)) return false;
            if (!msg.TryGetProperty("role", out var roleEl)) return false;
            role = roleEl.GetString() ?? "";
            if (!msg.TryGetProperty("content", out var content) || content.ValueKind != JsonValueKind.Array) return false;

            foreach (var c in content.EnumerateArray())
            {
                if (c.TryGetProperty("type", out var typeEl))
                {
                    var type = (typeEl.GetString() ?? "").ToLowerInvariant();
                    if (type is "text" or "output_text" or "input_text" or "summary_text")
                    {
                        if (c.TryGetProperty("text", out var txtEl))
                        {
                            text = txtEl.GetString() ?? "";
                            if (!string.IsNullOrWhiteSpace(text)) return true;
                        }
                    }
                }

                if (c.TryGetProperty("text", out var txtAny))
                {
                    text = txtAny.GetString() ?? "";
                    if (!string.IsNullOrWhiteSpace(text)) return true;
                }

                if (c.TryGetProperty("summary", out var summary) && summary.ValueKind == JsonValueKind.Array)
                {
                    foreach (var s in summary.EnumerateArray())
                    {
                        if (s.TryGetProperty("text", out var st))
                        {
                            text = st.GetString() ?? "";
                            if (!string.IsNullOrWhiteSpace(text)) return true;
                        }
                    }
                }
            }
            return false;
        }
        catch { return false; }
    }

    static string NormalizePrompt(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "";
        var t = s.Replace("\r", " ").Replace("\n", " ").Trim();
        if (t.Equals("NO_REPLY", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Contains("send ㄱㄱ", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Contains("telegram send ㄱㄱ", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Equals("ㄱㄱ", StringComparison.OrdinalIgnoreCase)) return "";
        if (t.Length > 160) t = t[..160] + "...";
        return t;
    }

    static string CompressPlanTitle(string s, int maxLen = 34)
    {
        var t = s.Replace("\r", " ").Replace("\n", " ").Trim();
        if (t.Length > maxLen) t = t[..maxLen] + "...";
        return t;
    }

    static List<string> ExtractPlanOutlineItems(string text, int maxItems)
    {
        var items = new List<string>();
        if (string.IsNullOrWhiteSpace(text)) return items;

        var lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
                        .Select(x => x.Trim())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToList();

        bool inBlock = false;
        for (int i = 0; i < lines.Count; i++)
        {
            var ln = lines[i];
            if (ln.Equals("[KRO_PLAN_BEGIN]", StringComparison.OrdinalIgnoreCase)) { inBlock = true; continue; }
            if (ln.Equals("[KRO_PLAN_END]", StringComparison.OrdinalIgnoreCase)) break;

            if (ln.StartsWith("PLAN:", StringComparison.OrdinalIgnoreCase))
            {
                var head = ln[5..].Trim();
                if (!string.IsNullOrWhiteSpace(head))
                {
                    // support single-line style: PLAN: title - item1 - item2 ...
                    var parts = head.Split(" - ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    if (parts.Length > 0)
                    {
                        items.Add(CompressPlanTitle(parts[0]));
                        for (int p = 1; p < parts.Length && items.Count < maxItems; p++)
                            items.Add(CompressPlanTitle(parts[p]));
                    }
                    else
                    {
                        items.Add(CompressPlanTitle(head));
                    }
                }
                inBlock = true;
                continue;
            }

            if (inBlock && (ln.StartsWith("-") || ln.StartsWith("•") || ln.StartsWith("1)") || ln.StartsWith("2)") || ln.StartsWith("3)")))
            {
                var body = ln.TrimStart('-', '•', ' ').Trim();
                if (!string.IsNullOrWhiteSpace(body)) items.Add(CompressPlanTitle(body));
                if (items.Count >= maxItems) break;
            }
        }

        return items.Take(maxItems).ToList();
    }

    static List<string> ExtractRecentPlanItems(int maxItems = 3)
    {
        try
        {
            var sessionsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".openclaw", "agents", "main", "sessions");
            if (!Directory.Exists(sessionsDir)) return new List<string>();

            var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl")
                .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
                .FirstOrDefault();
            if (string.IsNullOrWhiteSpace(latestFile)) return new List<string>();

            var fi = new FileInfo(latestFile);
            if (latestFile == _lastPlanSessionFile && fi.Length == _lastPlanSessionLength && fi.LastWriteTimeUtc == _lastPlanSessionWriteUtc)
                return _lastPlanItemsCache.Take(maxItems).ToList();

            var lines = ReadTailLinesShared(latestFile, 256 * 1024);
            var start = latestFile == _lastPlanSessionFile ? Math.Max(0, _lastPlanLineIndex - 2) : 0;
            if (start >= lines.Length) start = 0;

            var outItems = new List<string>();
            int foundLine = -1;
            for (int i = lines.Length - 1; i >= start; i--)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (!line.Contains("assistant", StringComparison.OrdinalIgnoreCase)) continue;
                if (!TryExtractRoleAndText(line, out var role, out var text)) continue;
                if (!string.Equals(role, "assistant", StringComparison.OrdinalIgnoreCase)) continue;

                // 1) preferred: explicit plan-format outline
                var outline = ExtractPlanOutlineItems(text, maxItems);
                if (outline.Count > 0)
                {
                    // newest valid plan block wins: do not mix with older plans
                    outItems.Clear();
                    foreach (var it in outline.Take(maxItems))
                        outItems.Add(it);
                    foundLine = i;
                    break;
                }

                // 2) strict mode: no heuristic promotion
                // only explicit plan-format outputs are allowed for EYE_PLAN
                continue;
            }

            _lastPlanSessionFile = latestFile;
            _lastPlanLineIndex = foundLine;
            _lastPlanSessionLength = fi.Length;
            _lastPlanSessionWriteUtc = fi.LastWriteTimeUtc;
            _lastPlanItemsCache = outItems;

            return outItems.Take(maxItems).ToList();
        }
        catch
        {
            return new List<string>();
        }
    }

    static (string a11y, string act, string fallback) ReadLatestActionTriplet()
    {
        try
        {
            var logDir = Path.Combine(DataDir, "logs");
            if (!Directory.Exists(logDir)) return ("", "", "");

            var files = Directory.GetFiles(logDir, "*.txt", SearchOption.TopDirectoryOnly)
                .Where(p => !Path.GetFileName(p).Contains(".eye."))
                .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
                .Take(3)
                .ToList();

            string a11y = "", act = "", fallback = "";
            foreach (var f in files)
            {
                var lines = ReadTailLinesShared(f, 32 * 1024);
                for (int i = lines.Length - 1; i >= 0; i--)
                {
                    var ln = lines[i].Trim();
                    if (string.IsNullOrWhiteSpace(a11y) && ln.StartsWith("[A11Y]")) a11y = ln[6..].Trim();
                    if (string.IsNullOrWhiteSpace(act) && ln.StartsWith("[ACT]")) act = ln[5..].Trim();
                    if (string.IsNullOrWhiteSpace(fallback) && ln.StartsWith("[FALLBACK]")) fallback = ln[10..].Trim();
                    if (!string.IsNullOrWhiteSpace(a11y) && !string.IsNullOrWhiteSpace(act) && !string.IsNullOrWhiteSpace(fallback))
                        break;
                }
                if (!string.IsNullOrWhiteSpace(a11y) && !string.IsNullOrWhiteSpace(act) && !string.IsNullOrWhiteSpace(fallback))
                    break;
            }

            a11y = CompressPlanTitle(a11y, 46);
            act = CompressPlanTitle(act, 46);
            fallback = CompressPlanTitle(fallback, 46);
            return (a11y, act, fallback);
        }
        catch
        {
            return ("", "", "");
        }
    }

    static string[] ReadTailLinesShared(string path, int tailBytes)
    {
        try
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            var len = fs.Length;
            var start = Math.Max(0, len - tailBytes);
            fs.Seek(start, SeekOrigin.Begin);
            using var sr = new StreamReader(fs, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
            var txt = sr.ReadToEnd();
            if (start > 0)
            {
                var idx = txt.IndexOf('\n');
                if (idx >= 0 && idx + 1 < txt.Length)
                    txt = txt[(idx + 1)..];
            }
            return txt.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        }
        catch { return Array.Empty<string>(); }
    }

    // Commands to skip when building idle-after text (meta + communication noise)
    static readonly HashSet<string> _idleSkipCommands = new(StringComparer.OrdinalIgnoreCase)
    { "eye", "slack", "tick" };

    /// <summary>
    /// Get age (seconds since last modification) of the most recent Claude Code session JSONL.
    /// Claude Code writes to ~/.claude/projects/{project}/*.jsonl during ALL activity
    /// (file reads, edits, builds, tool calls) — not just wkappbot commands.
    /// Returns 9999 if no session file found.
    /// </summary>
    static double GetClaudeCodeSessionAge()
    {
        try
        {
            var claudeDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".claude", "projects");
            if (!Directory.Exists(claudeDir)) return 9999;

            // Find most recently modified .jsonl across all projects
            DateTime latestUtc = DateTime.MinValue;
            foreach (var projDir in Directory.GetDirectories(claudeDir))
            {
                try
                {
                    foreach (var jsonl in Directory.GetFiles(projDir, "*.jsonl"))
                    {
                        var mtime = File.GetLastWriteTimeUtc(jsonl);
                        if (mtime > latestUtc) latestUtc = mtime;
                    }
                }
                catch { }
            }

            if (latestUtc == DateTime.MinValue) return 9999;
            return (DateTime.UtcNow - latestUtc).TotalSeconds;
        }
        catch { return 9999; }
    }

    /// <summary>
    /// Read the last meaningful tick's tag from eye_ticks.jsonl.
    /// Skips meta tags AND communication commands (slack, eye) to find actual work.
    /// Returns like "publish/build" or "inspect" — shown as idle-after text.
    /// </summary>
    static string? GetLastTickTag()
    {
        try
        {
            if (!File.Exists(EyeTicksPath)) return null;
            var lines = ReadTailLinesShared(EyeTicksPath, 8192); // ~last 30 ticks (may need deeper scan)
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                var t = System.Text.Json.JsonSerializer.Deserialize<EyeTick>(lines[i]);
                if (t == null) continue;
                // Skip meta tags (eye, snapshot, validate, help)
                if (IsMetaTag(t.Tag)) continue;
                // Skip communication commands (slack send/reply, eye tick)
                if (_idleSkipCommands.Contains(t.Command ?? "")) continue;
                var tag = t.Tag ?? "";
                // Include command for context: "publish/build" or "run/click"
                if (!string.IsNullOrEmpty(t.Command) && !tag.StartsWith(t.Command))
                    return $"{t.Command}/{tag}";
                return tag;
            }
        }
        catch { }
        return null;
    }

    static (string progress, string done, string next, string block) BuildKroStatus3(EyeTick t)
    {
        var report = $"{t.Command}/{t.Tag}";
        var status = (t.Status ?? "").ToLowerInvariant();
        var aiRecent = (DateTime.UtcNow - _lastAiActivityUtc).TotalSeconds <= 45;

        if (status == "start")
            return (report, "작업 시작", "응답 처리 중", "");

        if (status.StartsWith("step:"))
        {
            if (aiRecent)
                return (report, "최근 대화 수신", "응답 처리 중", "");
            return (report, "중간 단계", "상태 갱신 대기", "");
        }

        if (status.StartsWith("end:"))
        {
            var codeText = status[4..].Trim();
            if (int.TryParse(codeText, out var code) && code == 0)
            {
                if (aiRecent)
                    return ("응답 처리 중", "최근 대화 수신", "후속 입력 대기", "");
                return (report, "작업 완료", "다음 작업 대기", "");
            }
            return (report, "오류 종료", "에러 분석/폴백", $"종료코드 {codeText}");
        }

        if (aiRecent)
            return (report, "최근 대화 수신", "응답 처리 중", "");

        return (report, "상태 대기", "상태 갱신 대기", "");
    }

    // Meta tags: diagnostic/overhead commands that shouldn't hide meaningful work in cards
    // e.g. "eye tick" checks status, "snapshot" captures state — not the real work itself
    static readonly HashSet<string> _metaTags = new(StringComparer.OrdinalIgnoreCase)
    { "eye", "snapshot", "eye tick", "validate", "help" };

    static bool IsMetaTag(string? tag) =>
        !string.IsNullOrWhiteSpace(tag) && _metaTags.Contains(tag!);

    static List<EyeParentCard> ReadEyeCards(int staleSeconds = 86400)
    {
        // KEY = normalized CWD (folder-based grouping: same folder = one card, survives PID restart)
        var cards = new Dictionary<string, EyeParentCard>(StringComparer.OrdinalIgnoreCase);
        var path = EyeTicksPath;
        if (!File.Exists(path)) return cards.Values.ToList();

        // 2MB tail: enough for ~24h of ticks (each tick ~200 bytes, tick every ~10s = ~1.7MB/day)
        var lines = ReadTailLinesShared(path, 64 * 1024);
        var now = DateTime.UtcNow;

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;
            try
            {
                var t = JsonSerializer.Deserialize<EyeTick>(line);
                if (t == null) continue;
                if (!DateTime.TryParse(t.Ts, out var tsLocal)) continue;
                var tsUtc = tsLocal.ToUniversalTime();
                var ppid = t.HostPid > 0 ? t.HostPid : (t.ParentPid > 0 ? t.ParentPid : t.Pid);
                var pname = !string.IsNullOrWhiteSpace(t.HostName) ? t.HostName : (string.IsNullOrWhiteSpace(t.ParentName) ? "unknown" : t.ParentName);
                var ptitle = t.HostTitle ?? "";

                // CWD-based key: normalize to lowercase forward-slash, trimmed
                var cwdRaw = (t.Cwd ?? "").Replace('\\', '/').ToLowerInvariant().TrimEnd('/');
                var cardKey = string.IsNullOrEmpty(cwdRaw) ? $"pid_{ppid}" : cwdRaw;

                // Always update timestamp with latest tick
                // But for tag/status: meta tags (eye, snapshot) don't overwrite meaningful work tags
                if (!cards.TryGetValue(cardKey, out var c))
                {
                    // First tick for this CWD — always accept
                    cards[cardKey] = new EyeParentCard
                    {
                        ParentPid = ppid,
                        ParentName = pname,
                        ParentTitle = ptitle,
                        LastTag = t.Tag,
                        LastStatus = t.Status,
                        LastTsUtc = tsUtc,
                        Cwd = t.Cwd ?? "",
                    };
                }
                else if (tsUtc > c.LastTsUtc)
                {
                    // Newer tick — always update timestamp and process info
                    c.LastTsUtc = tsUtc;
                    c.ParentPid = ppid; // latest PID for this CWD
                    c.ParentName = pname;
                    c.ParentTitle = ptitle;
                    if (!string.IsNullOrWhiteSpace(t.Cwd)) c.Cwd = t.Cwd;

                    // Only update tag/status if:
                    // 1. New tick is non-meta (real work always wins), OR
                    // 2. Existing tag is also meta (meta→meta is fine)
                    if (!IsMetaTag(t.Tag) || IsMetaTag(c.LastTag))
                    {
                        c.LastTag = t.Tag;
                        c.LastStatus = t.Status;
                    }
                }
            }
            catch { }
        }

        return cards.Values.Where(c => (now - c.LastTsUtc).TotalSeconds <= staleSeconds).ToList();
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

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[EYE] ✅ CLAUDE.md 인수인계 섹션 작성 완료 ({handoffSection.Length} chars)");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] CLAUDE.md 인수인계 작성 실패: {ex.Message}");
        }
    }
}
