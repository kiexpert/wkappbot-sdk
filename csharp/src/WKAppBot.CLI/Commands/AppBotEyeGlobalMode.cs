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
    static string _dirtyTickFile = "";
    static long _dirtyTickLength = -1;
    static DateTime _dirtyTickWriteUtc = DateTime.MinValue;
    static string _dirtyPromptFile = "";
    static long _dirtyPromptLength = -1;
    static DateTime _dirtyPromptWriteUtc = DateTime.MinValue;

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

                    // Announce presence
                    string? startupTs = null;
                    if (!string.IsNullOrEmpty(slackChannel))
                    {
                        var startupMsg = $"AppBotEye Global+Slack 온라인! `{Environment.MachineName}` pid={Environment.ProcessId}";
                        var (startOk, sTs) = Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel, startupMsg, username: botUsername))
                            .GetAwaiter().GetResult();
                        if (startOk && sTs != null)
                            startupTs = sTs;
                    }

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

        int frameCount = 0;
        while (host.IsAlive && !cts.IsCancellationRequested)
        {
            // ── Core tick: read ticks + sessions ──
            var forceFull = ShouldForceFullLoad();
            var (tickDirty, promptDirty) = CheckGlobalDirtyFlags();
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
                                var now = DateTime.Now;
                                bool cooldownPassed = rateLimitDetectedAt != null &&
                                    (now - rateLimitDetectedAt.Value).TotalMinutes >= RateLimitCooldownMinutes;
                                bool resetTimePassed = rateLimitResetTime != null && now >= rateLimitResetTime.Value;

                                if (cooldownPassed || resetTimePassed)
                                {
                                    wasRateLimited = false;
                                    rateLimitDetectedAt = null;
                                    rateLimitResetTime = null;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"[EYE] Rate limit cleared (cooldown={cooldownPassed}, resetTime={resetTimePassed})");
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

                            // Slack status streaming (edit same message)
                            if (slackClient != null && !string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                            {
                                try
                                {
                                    // Skip status streaming for permission_prompt during debounce window (auto-approved vanish quickly)
                                    if (claudeStatus.Item1 == "permission_prompt" && permissionPromptFirstSeen != null
                                        && (DateTime.Now - permissionPromptFirstSeen.Value).TotalSeconds < 3.0)
                                        goto skipStatusStreaming;

                                    var statusEmoji = claudeStatus.Item1 switch
                                    {
                                        "executing" => ":gear:",
                                        "plan_approval_pending" => ":clipboard:",
                                        "plan_mode" => ":memo:",
                                        "permission_prompt" => ":lock:",
                                        "prompt_ready" => ":speech_balloon:",
                                        "rate_limit" => ":warning:",
                                        _ => ":robot_face:"
                                    };
                                    var slackText = $"{statusEmoji} Claude: {claudeStatus.Item2}";
                                    bool textChanged = slackText != lastSlackStatusText;

                                    if (slackStatusTs != null && textChanged)
                                    {
                                        // Check if our status message is still the latest in channel
                                        var latestTs = Task.Run(async () =>
                                            await GetChannelLatestMessageTs(slackBotToken!, slackChannel!))
                                            .GetAwaiter().GetResult();

                                        if (latestTs == slackStatusTs)
                                        {
                                            // Still latest → chat.update (reuse)
                                            var localTs = slackStatusTs;
                                            Task.Run(async () => await SlackUpdateMessageAsync(
                                                slackBotToken!, slackChannel!, localTs, slackText))
                                                .Wait(3000);
                                        }
                                        else
                                        {
                                            // Not latest → delete old + create new (relocate to bottom)
                                            var oldTs = slackStatusTs;
                                            Task.Run(async () => await SlackDeleteMessageAsync(
                                                slackBotToken!, slackChannel!, oldTs)).Wait(3000);
                                            var (ok, ts) = Task.Run(async () =>
                                                await SlackSendViaApi(slackBotToken!, slackChannel!, slackText, username: botUsername))
                                                .GetAwaiter().GetResult();
                                            if (ok && ts != null)
                                            {
                                                slackStatusTs = ts;
                                                try { File.WriteAllText(statusTsFile, ts); } catch { }
                                            }
                                        }
                                        lastSlackStatusText = slackText;
                                    }
                                    else if (slackStatusTs == null && textChanged)
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
                            // Claude status null — auto-clear stale rate limit
                            if (wasRateLimited)
                            {
                                var now = DateTime.Now;
                                bool cooldownPassed = rateLimitDetectedAt != null &&
                                    (now - rateLimitDetectedAt.Value).TotalMinutes >= RateLimitCooldownMinutes;
                                bool resetTimePassed = rateLimitResetTime != null && now >= rateLimitResetTime.Value;
                                if (cooldownPassed || resetTimePassed)
                                {
                                    wasRateLimited = false;
                                    rateLimitDetectedAt = null;
                                    rateLimitResetTime = null;
                                    cachedClaudeStatusText = null;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"[EYE] Rate limit auto-cleared (null status)");
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
            if (frameCount % 50 == 0 && exeStartTime != DateTime.MinValue)
            {
                try
                {
                    if (File.GetLastWriteTimeUtc(exePath) != exeStartTime)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[EYE] Hot-reload: EXE changed — shutting down");
                        Console.ResetColor();
                        break;
                    }
                }
                catch { }
            }

            // ── Stats logging ──
            if (frameCount % 100 == 0 && frameCount > 0)
            {
                var slackInfo = slackClient != null
                    ? $", Slack={slackClient.IsConnected}, msgs={slackClient.MessageCount}"
                    : "";
                Console.WriteLine($"[EYE] frame #{frameCount} ({(slackClient != null ? "Socket+API" : "API-only")}{slackInfo})");
            }

            frameCount++;
            Thread.Sleep(Math.Max(100, intervalMs));
        }

        // ── Cleanup ──
        WKAppBot.Win32.Native.NativeMethods.SetThreadExecutionState(
            WKAppBot.Win32.Native.NativeMethods.ES_CONTINUOUS);

        if (slackClient != null)
        {
            try
            {
                if (!string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                {
                    Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!, "AppBotEye Global+Slack 종료", username: botUsername))
                        .Wait(3000);
                }
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
            _cachedCards = ReadEyeCards(staleSeconds: 180);
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
        Console.WriteLine($"[EYE_TICK] mode={mode} tick={swTick.ElapsedMilliseconds}ms(read={tickRead}ms,parse={tickParse}ms,activity=0ms) " +
                          $"prompt={swPrompt.ElapsedMilliseconds}ms(stat={promptDiag.StatMs}ms,read={promptDiag.ReadMs}ms,scan={promptDiag.ScanMs}ms,parse={promptDiag.ParseMs}ms,norm={promptDiag.NormMs}ms,cache={promptDiag.CacheMs}ms) " +
                          $"schedule={swSchedule.ElapsedMilliseconds}ms total={swTotal.ElapsedMilliseconds}ms");
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

    static (bool tickDirty, bool promptDirty) CheckGlobalDirtyFlags()
    {
        bool tickDirty = false;
        bool promptDirty = false;

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

        return (tickDirty, promptDirty);
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
            var cards = ReadEyeCards(staleSeconds: 180);
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

                var replyCmd = $"wkappbot slack reply \"response\" --channel {channel} --thread {ts}";
                // Include context from last processed message for conversation flow
                var contextPrefix = (contextLine != null && forwarded == 0) ? $"{contextLine}\n\n" : "";
                var promptText = $"{contextPrefix}{cleanText}\n\n(Slack @{user} — reply: {replyCmd})";

                // Re-find prompt each time (window may shift)
                var fresh = helper.FindPrompt();
                if (fresh == null)
                {
                    Console.WriteLine("[EYE_TICK] WARNING: Lost prompt — stopping forward");
                    break;
                }

                var ok = helper.TypeAndSubmit(fresh, promptText);
                if (ok)
                {
                    forwarded++;
                    latestTs = ts;
                    Console.WriteLine($"[EYE_TICK] >> Slack @{user}: {cleanText}");
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

                    var replyCmd = $"wkappbot slack reply \"response\" --channel {channel} --thread {threadTs}";
                    var promptText = $"[쓰레드 시작] 클롣: {cleanParent}\n\n@{rUser}: {cleanReply}\n\n(Slack thread reply — {replyCmd})";

                    var fresh = helper.FindPrompt();
                    if (fresh == null)
                    {
                        Console.WriteLine("[EYE_TICK] WARNING: Lost prompt — stopping thread reply forward");
                        return;
                    }

                    var ok = helper.TypeAndSubmit(fresh, promptText);
                    if (ok)
                    {
                        threadReplies++;
                        latestReplyTs = rTs;
                        Console.WriteLine($"[EYE_TICK] >> Thread @{rUser}: {cleanReply}");
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

        var (a11y, act, fallback) = ReadLatestActionTriplet();
        if (!string.IsNullOrWhiteSpace(a11y)) sb.AppendLine($"엑빌: {a11y}");
        if (!string.IsNullOrWhiteSpace(act)) sb.AppendLine($"액션: {act}");
        if (!string.IsNullOrWhiteSpace(fallback)) sb.AppendLine($"폴백: {fallback}");

        if (!string.IsNullOrWhiteSpace(prompt))
            sb.AppendLine($"최근 생각: {prompt}");

        if (latest != null)
        {
            var (progress, done, next, block) = BuildKroStatus3(latest);
            sb.AppendLine($"크로 진행: {progress}");
            sb.AppendLine($"크로 완료: {done}");
            sb.AppendLine($"크로 예정: {next}");

            var plans = ExtractRecentPlanItems(maxItems: 3);
            if (plans.Count > 0)
            {
                for (int i = 0; i < plans.Count; i++)
                    sb.AppendLine($"- —:— {plans[i]}");
                if (_lastPlanItemsCache.Count > plans.Count)
                    sb.AppendLine($"- —:— 그 외 {_lastPlanItemsCache.Count - plans.Count}건...");
            }

            if (!string.IsNullOrWhiteSpace(block))
                sb.AppendLine($"크로 이슈: {block}");
            sb.AppendLine("----");
        }

        if (cards.Count == 0)
            sb.AppendLine("(active parent cards: 0)");
        else
        {
            foreach (var c in cards.OrderByDescending(x => x.LastTsUtc).Take(6))
            {
                var age = Math.Max(0, (int)(DateTime.UtcNow - c.LastTsUtc).TotalSeconds);
                var head = string.IsNullOrWhiteSpace(c.ParentTitle) ? $"{c.ParentName}:{c.ParentPid}" : $"{c.ParentTitle} ({c.ParentName}:{c.ParentPid})";
                sb.AppendLine($"[{head}] {c.LastTag} {c.LastStatus} -{age}s");
            }
        }

        return sb.ToString().TrimEnd();
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

    static List<EyeParentCard> ReadEyeCards(int staleSeconds = 25)
    {
        var cards = new Dictionary<int, EyeParentCard>();
        var path = EyeTicksPath;
        if (!File.Exists(path)) return cards.Values.ToList();

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

                if (!cards.TryGetValue(ppid, out var c) || tsUtc > c.LastTsUtc)
                {
                    cards[ppid] = new EyeParentCard
                    {
                        ParentPid = ppid,
                        ParentName = pname,
                        ParentTitle = ptitle,
                        LastTag = t.Tag,
                        LastStatus = t.Status,
                        LastTsUtc = tsUtc
                    };
                }
            }
            catch { }
        }

        return cards.Values.Where(c => (now - c.LastTsUtc).TotalSeconds <= staleSeconds).ToList();
    }
}
