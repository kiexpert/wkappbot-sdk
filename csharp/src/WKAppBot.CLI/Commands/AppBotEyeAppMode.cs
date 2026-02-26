// AppBotEyeAppMode.cs — Unified App Mode polling loop with ActionState IPC + Slack integration.
// Extracted from AppBotEyeCommands.cs for readability.

using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Core.Runner;
using WKAppBot.Vision;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── Unified Mode: ActionState IPC + App Fallback ──────────────────

    /// <summary>
    /// Unified polling loop — reads ActionState IPC as primary source,
    /// falls back to cursor-based UIA tracking when stale/absent.
    /// Optionally captures PrintWindow screenshots for the target window.
    /// </summary>
    static int AppModePollingLoop(
        string? appTitle, string? appProcess,
        int width, int height, int posX, int posY,
        IntPtr claudeHwnd, int intervalMs, bool slackMode = false)
    {
        // Find initial target window (optional — ActionState may provide window info)
        var targetHwnd = IntPtr.Zero;
        if (!string.IsNullOrEmpty(appTitle) || !string.IsNullOrEmpty(appProcess))
        {
            targetHwnd = FindAppWindow(appTitle, appProcess);
            if (targetHwnd != IntPtr.Zero)
            {
                var winTitle = GetAppWindowText(targetHwnd);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[EYE] Tracking: \"{winTitle}\" (hwnd=0x{targetHwnd:X8})");
                Console.ResetColor();
            }
        }

        // Plan approval tracking (must be declared before Slack setup for lambda capture)
        bool planApprovalSentToSlack = false;
        string? pendingPlanApprovalSlackTs = null;

        // Permission prompt tracking (Allow/Deny buttons)
        bool permissionPromptSentToSlack = false;
        string? pendingPermissionSlackTs = null;
        DateTime? permissionPromptFirstSeen = null; // debounce: 3s before Slack notification

        // Slack status streaming — edit same message instead of spamming new ones
        string? slackStatusTs = null;       // current status message timestamp
        string? lastSlackStatusText = null;  // cached text for change detection

        // Restore previous status message ts from file (so restart can clean up old message)
        var statusTsFile = Path.Combine(DataDir, "runtime", "status_streaming_ts.txt");
        try
        {
            if (File.Exists(statusTsFile))
            {
                var parts = File.ReadAllText(statusTsFile).Trim().Split('|');
                if (parts.Length >= 2)
                {
                    var oldChannel = parts[0];
                    var oldTs = parts[1];
                    // Delete the old status message from previous session
                    if (!string.IsNullOrEmpty(oldTs))
                    {
                        Console.WriteLine($"[EYE] Cleaning up previous status message (ts={oldTs})");
                        // Will delete after Slack is connected (need botToken)
                        slackStatusTs = null; // don't reuse — we'll delete it below after Slack init
                        // Stash for deferred cleanup
                        Environment.SetEnvironmentVariable("_WKAPPBOT_OLD_STATUS_TS", $"{oldChannel}|{oldTs}");
                    }
                }
                File.Delete(statusTsFile);
            }
        }
        catch { }

        // ── Slack daemon integration (--slack) ──
        SlackSocketClient? slackClient = null;
        string? slackBotToken = null;
        string? slackChannel = null;
        var botUsername = BotUsername; // global static readonly, same across all CLI commands
        if (slackMode)
        {
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
                        Console.WriteLine("[EYE] Slack Socket Mode connected");
                        Console.ResetColor();

                        // Announce presence + track startup message for thread reply forwarding
                        string? startupTs = null;
                        if (!string.IsNullOrEmpty(slackChannel))
                        {
                            var startupMsg = $"AppBotEye+Slack 온라인! `{Environment.MachineName}` pid={Environment.ProcessId}";
                            var (startOk, sTs) = Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel, startupMsg, username: botUsername))
                                .GetAwaiter().GetResult();
                            if (startOk && sTs != null)
                                startupTs = sTs;
                        }

                        // Set up event handlers (pass claudeHwnd + plan/permission approval + status streaming + startup ts)
                        SetupSlackEventHandlers(slackClient, slackBotToken!, slackChannel,
                            claudeHwnd, () => pendingPlanApprovalSlackTs,
                            () => pendingPermissionSlackTs, startupTs, botUsername,
                            () => slackStatusTs, () => { slackStatusTs = null; lastSlackStatusText = null; });

                        // Block Kit button handler (plan approve/reject)
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

                                // Update original message: remove buttons, show result
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
                                // Permission button clicked — value = exact UIA button text
                                var buttonText = action.Value;
                                var clicked = ClickPermissionButton(claudeHwnd, buttonText);
                                var reply = clicked
                                    ? $":white_check_mark: \"{buttonText}\" 클릭 완료!"
                                    : $":x: \"{buttonText}\" 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                                Task.Run(async () => await SlackSendViaApi(slackBotToken!, action.Channel, reply, thread, username: botUsername))
                                    .Wait(5000);

                                // Update original message: remove buttons, show result
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

                                // Update original message: remove buttons, show rejection
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
                    Console.WriteLine($"[EYE] Slack config not found: {configPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EYE] Slack init error: {ex.Message} — continuing without Slack");
            }
        }

        // Start overlay
        using var host = new AppBotEyeHost();
        host.Start(width, height, posX, posY, claudeHwnd);
        if (targetHwnd != IntPtr.Zero)
            host.SetChromeHwnd(targetHwnd);

        Console.WriteLine($"[EYE] Unified Mode overlay active{(slackClient != null ? " + Slack" : "")} — press Ctrl+C to stop");

        // Handle Ctrl+C gracefully
        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        // Hot-reload: track EXE file timestamp
        var exePath = Process.GetCurrentProcess().MainModule?.FileName ?? "";
        var exeStartTime = File.Exists(exePath) ? File.GetLastWriteTimeUtc(exePath) : DateTime.MinValue;

        // Create UIA locator for fallback cursor tracking
        using var uia = new UiaLocator();

        int frameCount = 0;
        var sw = Stopwatch.StartNew();
        string? lastElemKey = null;
        DateTime lastActionStateMtime = DateTime.MinValue;
        ActionState? lastActionState = null;
        string? lastDisplayText = null;
        IntPtr lastActionStateHwnd = IntPtr.Zero;
        string? cachedClaudeStatusText = null; // Cached Claude Desktop status for fallback display
        bool wasRateLimited = false; // Track rate_limit -> reset transition for on_limit_reset schedules
        DateTime? rateLimitDetectedAt = null; // When rate limit was first detected (for cooldown debounce)
        DateTime? rateLimitResetTime = null;  // Parsed reset time from Claude status text
        DateTime? lastRateLimitAlertTime = null; // When last Slack alert was sent (prevent spam)
        const int RateLimitCooldownMinutes = 30; // Don't clear wasRateLimited for at least this long (prevents flapping)
        const int RateLimitAlertCooldownMinutes = 30; // Don't send Slack alert more than once per 30 min
        DateTime? lastDiagSnapshotTime = null; // Debounce for UIA detection failure snapshots
        const int DiagSnapshotCooldownMinutes = 5; // Min interval between diagnostic snapshots
        int consecutiveUiaFailures = 0; // Count consecutive null returns from DetectClaudeDesktopStatus

        // ── Startup: execute overdue schedules (PC reboot recovery) ──
        try
        {
            var overdueItems = ScheduleManager.GetDueItems();
            if (overdueItems.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[SCHEDULE] {overdueItems.Count} overdue schedule(s) from before restart");
                Console.ResetColor();
                Thread.Sleep(5000); // Wait for Claude Desktop to fully start
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

        while (host.IsAlive && !cts.IsCancellationRequested)
        {
            try
            {
                var frameSw = Stopwatch.StartNew();

                // ── 1. Read ActionState IPC (check mtime first) ──────
                bool hasValidActionState = false;
                if (frameCount % 2 == 0) // check every 2nd frame (~200ms)
                {
                    try
                    {
                        var asPath = ActionState.FilePath;
                        if (File.Exists(asPath))
                        {
                            var mtime = File.GetLastWriteTimeUtc(asPath);
                            if (mtime != lastActionStateMtime)
                            {
                                lastActionStateMtime = mtime;
                                lastActionState = ActionState.Read();
                            }
                        }
                    }
                    catch { /* best-effort */ }

                    if (lastActionState != null && !lastActionState.IsStale(30.0))
                    {
                        hasValidActionState = true;

                        // Update display text from ActionState
                        var displayText = lastActionState.ToDisplayText();
                        if (displayText != lastDisplayText)
                        {
                            lastDisplayText = displayText;
                            host.UpdateAccessibilityText(displayText);
                        }

                        // Try to find the window described by ActionState for screenshots
                        if (frameCount % 10 == 0 && !string.IsNullOrEmpty(lastActionState.WindowTitle))
                        {
                            var asHwnd = FindAppWindow(lastActionState.WindowTitle, lastActionState.ProcessName);
                            if (asHwnd != IntPtr.Zero && asHwnd != lastActionStateHwnd)
                            {
                                lastActionStateHwnd = asHwnd;
                                if (targetHwnd == IntPtr.Zero)
                                {
                                    targetHwnd = asHwnd;
                                    host.SetChromeHwnd(asHwnd);
                                }
                            }
                        }
                    }
                }

                // ── 2. ActionState stale/absent → fallback: Claude status + cursor tracking ──
                if (!hasValidActionState && frameCount % 3 == 0)
                {
                    // Detect Claude Desktop status for fallback header (every ~3 sec in fallback mode)
                    if (frameCount % 30 == 0 && claudeHwnd != IntPtr.Zero && IsWindow(claudeHwnd))
                    {
                        try
                        {
                            var status = DetectClaudeDesktopStatus(claudeHwnd);
                            cachedClaudeStatusText = status != null
                                ? $"Claude: {status.Item2}"
                                : null;
                        }
                        catch { }
                    }

                    // Build fallback header: Claude status or "(앱봇 대기 중)"
                    var fallbackHeader = !string.IsNullOrEmpty(cachedClaudeStatusText)
                        ? cachedClaudeStatusText
                        : "(앱봇 대기 중)";

                    // Re-find target if lost
                    if (targetHwnd != IntPtr.Zero && !IsWindow(targetHwnd))
                    {
                        targetHwnd = FindAppWindow(appTitle, appProcess);
                        if (targetHwnd != IntPtr.Zero)
                            host.SetChromeHwnd(targetHwnd);
                    }

                    if (targetHwnd != IntPtr.Zero)
                    {
                        try
                        {
                            var elemText = PollAppElement(uia, targetHwnd, ref lastElemKey);
                            if (elemText != null)
                            {
                                var fallbackText = $"{fallbackHeader}\n── cursor ──\n{elemText}";
                                host.UpdateAccessibilityText(fallbackText);
                            }
                        }
                        catch { /* skip */ }
                    }
                    else if (lastActionState == null)
                    {
                        // No ActionState AND no target → show status + hint
                        if (frameCount % 30 == 0)
                            host.UpdateAccessibilityText($"{fallbackHeader}\n\n커서를 앱 위에 올려주세요");
                    }
                }

                // ── 3. Screenshot: target window (if available) ──────
                var screenshotTarget = targetHwnd != IntPtr.Zero ? targetHwnd : lastActionStateHwnd;
                if (screenshotTarget != IntPtr.Zero && IsWindow(screenshotTarget))
                {
                    try
                    {
                        using var bmp = ScreenCapture.CaptureWindow(screenshotTarget);
                        if (bmp != null && !ScreenCapture.IsBlankBitmap(bmp))
                        {
                            var pngBytes = ScreenCapture.ToPngBytes(bmp);
                            if (pngBytes.Length > 0)
                                host.UpdateScreenshot(pngBytes);
                        }
                    }
                    catch { /* skip frame */ }
                }

                // ── 4. Footer update ─────────────────────────────────
                if (frameCount % 30 == 0)
                {
                    var footerHwnd = screenshotTarget;
                    if (footerHwnd != IntPtr.Zero)
                        UpdateAppFooter(host, footerHwnd);
                    else if (hasValidActionState && lastActionState != null)
                    {
                        // Show source + progress from ActionState
                        var footerText = lastActionState.Source;
                        if (!string.IsNullOrEmpty(lastActionState.Progress))
                            footerText += $" ({lastActionState.Progress})";
                        host.UpdateInfo(footerText, lastActionState.ScenarioName ?? "");
                    }
                }

                // ── 5. Claude Desktop status detection (~every 5 sec) ──
                if (frameCount % 50 == 0 && claudeHwnd != IntPtr.Zero)
                {
                    try
                    {
                        var claudeStatus = DetectClaudeDesktopStatus(claudeHwnd);

                        // Always update cached status for fallback display
                        if (claudeStatus != null)
                            cachedClaudeStatusText = $"Claude: {claudeStatus.Item2}";
                        else
                        {
                            // UIA returned null — check if rate limit should be auto-cleared
                            // (reset time passed OR cooldown expired → stop showing stale "한도 초과")
                            if (wasRateLimited)
                            {
                                var now = DateTime.Now;
                                bool cooldownPassed = rateLimitDetectedAt != null &&
                                    (now - rateLimitDetectedAt.Value).TotalMinutes >= RateLimitCooldownMinutes;
                                bool resetTimePassed = rateLimitResetTime != null && now >= rateLimitResetTime.Value;

                                if (cooldownPassed || resetTimePassed)
                                {
                                    // Rate limit expired — clear everything
                                    wasRateLimited = false;
                                    rateLimitDetectedAt = null;
                                    rateLimitResetTime = null;
                                    cachedClaudeStatusText = null;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"[EYE] Rate limit auto-cleared (null status, cooldown={cooldownPassed}, resetTime={resetTimePassed})");
                                    Console.ResetColor();

                                    // Update ActionState
                                    var currentState = lastActionState ?? new ActionState { Source = "eye" };
                                    currentState.IsRateLimited = false;
                                    currentState.RateLimitResetAt = null;
                                    currentState.ClaudeStatus = null;
                                    currentState.ClaudeStatusText = null;
                                    ActionState.Write(currentState);

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
                                // else: still in cooldown — keep cachedClaudeStatusText as "한도 초과" (but it will clear when cooldown passes)
                            }
                            else
                            {
                                cachedClaudeStatusText = null;
                            }
                        }

                        // ── UIA detection failure diagnostic snapshot ──
                        // If Claude window exists but UIA can't detect any status → something unusual
                        // (e.g. unknown dialog, crash screen, update prompt, etc.)
                        if (claudeStatus == null && IsWindow(claudeHwnd))
                        {
                            consecutiveUiaFailures++;
                            // After 3 consecutive failures (~15 sec), take diagnostic snapshot
                            if (consecutiveUiaFailures >= 3)
                            {
                                var now = DateTime.Now;
                                bool snapshotCooldownOk = lastDiagSnapshotTime == null ||
                                    (now - lastDiagSnapshotTime.Value).TotalMinutes >= DiagSnapshotCooldownMinutes;

                                if (snapshotCooldownOk)
                                {
                                    lastDiagSnapshotTime = now;
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"[EYE] Claude 창 UIA 감지 실패 ({consecutiveUiaFailures}회 연속) — 진단 스냅샷 촬영");
                                    Console.ResetColor();

                                    try
                                    {
                                        var snapDir = TakeRateLimitSnapshot(claudeHwnd, "uia_diag");
                                        if (snapDir != null)
                                        {
                                            Console.WriteLine($"[EYE] Diagnostic snapshot: {snapDir}");

                                            // Upload to Slack
                                            if (!string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                                            {
                                                var screenshotPath = Path.Combine(snapDir, "screenshot.png");
                                                if (File.Exists(screenshotPath))
                                                {
                                                    Task.Run(async () =>
                                                    {
                                                        await SlackUploadFileAsync(slackBotToken!, slackChannel!,
                                                            screenshotPath, null, "UIA 감지 실패 진단",
                                                            $":mag: Claude 창 UIA 상태 감지 실패 ({consecutiveUiaFailures}회 연속)\n" +
                                                            "중단/승인/프롬프트 어느 것도 감지 안 됨 → 알 수 없는 상태");
                                                    }).Wait(10000);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"[EYE] Diagnostic snapshot error: {ex.Message}");
                                    }
                                }
                                consecutiveUiaFailures = 0; // reset after snapshot attempt
                            }
                        }
                        else
                        {
                            consecutiveUiaFailures = 0; // reset on successful detection
                        }

                        if (claudeStatus != null)
                        {
                            // Write Claude status to ActionState for Slack to read
                            var currentState = lastActionState ?? new ActionState { Source = "eye" };
                            if (currentState.ClaudeStatus != claudeStatus.Item1 ||
                                currentState.ClaudeStatusText != claudeStatus.Item2)
                            {
                                currentState.ClaudeStatus = claudeStatus.Item1;
                                currentState.ClaudeStatusText = claudeStatus.Item2;

                                // Rate limit: write reset time to ActionState
                                // Debounce: once detected, don't clear for RateLimitCooldownMinutes
                                // (UIA text scan is intermittently unreliable → prevents flapping)
                                bool justHitRateLimit = false;
                                if (claudeStatus.Item1 == "rate_limit")
                                {
                                    justHitRateLimit = !wasRateLimited; // first detection of this rate limit
                                    wasRateLimited = true;
                                    if (rateLimitDetectedAt == null)
                                        rateLimitDetectedAt = DateTime.Now;
                                    currentState.IsRateLimited = true;
                                    var resetDt = GetResetTimeFromDisplayText(claudeStatus.Item2);
                                    if (resetDt != null)
                                        rateLimitResetTime = resetDt;
                                    currentState.RateLimitResetAt = rateLimitResetTime?.ToString("O");
                                }
                                else if (wasRateLimited)
                                {
                                    // Rate limit was active — check if we should clear it
                                    // Don't clear if: (a) cooldown hasn't passed, AND (b) reset time hasn't passed
                                    var now = DateTime.Now;
                                    bool cooldownPassed = rateLimitDetectedAt != null &&
                                        (now - rateLimitDetectedAt.Value).TotalMinutes >= RateLimitCooldownMinutes;
                                    bool resetTimePassed = rateLimitResetTime != null && now >= rateLimitResetTime.Value;

                                    if (cooldownPassed || resetTimePassed)
                                    {
                                        // Genuinely cleared — reset everything
                                        wasRateLimited = false;
                                        rateLimitDetectedAt = null;
                                        rateLimitResetTime = null;
                                        currentState.IsRateLimited = false;
                                        currentState.RateLimitResetAt = null;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"[EYE] Rate limit cleared (cooldown={cooldownPassed}, resetTime={resetTimePassed})");
                                        Console.ResetColor();

                                        // Execute on_limit_reset schedules
                                        try
                                        {
                                            Console.WriteLine("[SCHEDULE] Rate limit cleared! Checking on_limit_reset schedules...");
                                            Thread.Sleep(3000); // Let Claude Desktop settle
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
                                    else
                                    {
                                        // Still in cooldown — keep wasRateLimited = true (UIA scan probably just missed the text)
                                        currentState.IsRateLimited = true;
                                        currentState.RateLimitResetAt = rateLimitResetTime?.ToString("O");
                                    }
                                }
                                else
                                {
                                    currentState.IsRateLimited = false;
                                    currentState.RateLimitResetAt = null;
                                }

                                ActionState.Write(currentState);

                                // ★ Rate limit just detected: auto-snapshot + Slack alert (new message, not update)
                                // Alert cooldown: don't spam Slack (max once per 30 min)
                                bool alertCooldownOk = lastRateLimitAlertTime == null ||
                                    (DateTime.Now - lastRateLimitAlertTime.Value).TotalMinutes >= RateLimitAlertCooldownMinutes;
                                if (justHitRateLimit && alertCooldownOk && !string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                                {
                                    lastRateLimitAlertTime = DateTime.Now;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"[EYE] ★ Rate limit detected! {claudeStatus.Item2}");
                                    Console.ResetColor();

                                    // Auto snapshot
                                    try
                                    {
                                        var snapDir = TakeRateLimitSnapshot(claudeHwnd);
                                        if (snapDir != null)
                                        {
                                            Console.WriteLine($"[EYE] Snapshot saved: {snapDir}");
                                            // Upload screenshot to Slack
                                            var screenshotPath = Path.Combine(snapDir, "screenshot.png");
                                            if (File.Exists(screenshotPath))
                                            {
                                                Task.Run(async () =>
                                                {
                                                    await SlackUploadFileAsync(slackBotToken!, slackChannel!,
                                                        screenshotPath, null, $"Rate Limit 스냅샷",
                                                        $":warning: Rate Limit 감지! {claudeStatus.Item2}");
                                                }).Wait(10000);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"[EYE] Snapshot error: {ex.Message}");
                                    }

                                    // Send NEW Slack message (not update) for rate limit alert
                                    try
                                    {
                                        var alertMsg = $":rotating_light: *Rate Limit!* {claudeStatus.Item2}\n스냅샷 자동 저장 완료";
                                        Task.Run(async () =>
                                            await SlackSendViaApi(slackBotToken!, slackChannel!, alertMsg, username: botUsername))
                                            .Wait(5000);
                                    }
                                    catch { }
                                }

                                // Slack streaming: update Claude status (edit same message, not spam new ones)
                                if (slackClient != null && !string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                                {
                                    // Skip status streaming for permission_prompt during debounce window
                                    if (claudeStatus.Item1 == "permission_prompt" && permissionPromptFirstSeen != null
                                        && (DateTime.Now - permissionPromptFirstSeen.Value).TotalSeconds < 3.0)
                                        goto skipStatusStreaming;

                                    try
                                    {
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
                                        if (hasValidActionState && lastActionState?.Progress != null)
                                            slackText += $" | {lastActionState.ScenarioName} ({lastActionState.Progress})";

                                        bool textChanged = slackText != lastSlackStatusText;

                                        // Reuse existing status message via chat.update (no relocate spam!)
                                        // Thread reply detection (resetStatusStreaming) handles new message creation
                                        if (slackStatusTs != null && textChanged)
                                        {
                                            var localTs = slackStatusTs;
                                            Task.Run(async () => await SlackUpdateMessageAsync(
                                                slackBotToken!, slackChannel!, localTs, slackText))
                                                .Wait(3000);
                                            lastSlackStatusText = slackText;
                                        }
                                        else if (slackStatusTs == null && textChanged)
                                        {
                                            // First message — create new
                                            var (ok, ts) = Task.Run(async () =>
                                                await SlackSendViaApi(slackBotToken!, slackChannel!, slackText, username: botUsername))
                                                .GetAwaiter().GetResult();
                                            if (ok && ts != null) slackStatusTs = ts;
                                            lastSlackStatusText = slackText;
                                        }
                                    }
                                    catch { /* best-effort */ }
                                    skipStatusStreaming:

                                    // ── Plan approval: send plan content to Slack (once) ──
                                    if (claudeStatus.Item1 == "plan_approval_pending" && !planApprovalSentToSlack)
                                    {
                                        try
                                        {
                                            var plan = ExtractPlanContent(claudeHwnd);
                                            if (plan != null)
                                            {
                                                // Truncate plan if too long for Slack (max ~4000 chars)
                                                var planContent = plan.Value.content;
                                                if (planContent.Length > 3500)
                                                    planContent = planContent[..3500] + "\n\n... (truncated)";

                                                var sourceLabel = plan.Value.source == "UIA" ? "UIA" : $"`{plan.Value.source}`";
                                                var fallbackText = $":clipboard: 플랜 승인 대기 (via {sourceLabel})\n\n{planContent}";

                                                // Build Block Kit message with [수락] button
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
                                                Console.WriteLine("[EYE] Plan content not found (file + UIA both failed)");
                                                planApprovalSentToSlack = true; // don't retry
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"[EYE] Plan Slack share error: {ex.Message}");
                                        }
                                    }

                                    // Reset plan approval tracking when status changes away
                                    if (claudeStatus.Item1 != "plan_approval_pending" && planApprovalSentToSlack)
                                    {
                                        planApprovalSentToSlack = false;
                                        pendingPlanApprovalSlackTs = null;
                                    }

                                    // ── Permission prompt: send buttons to Slack (3s debounce — auto-approved vanish quickly) ──
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

                                    // Reset permission tracking when status changes away
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
                        }
                    }
                    catch { /* best-effort */ }
                }

                // ── 6. Schedule executor (~every 10 seconds) ────────────
                if (frameCount % 100 == 50)
                {
                    try
                    {
                        // 6a. Check timed schedules (once + recurring)
                        var dueItems = ScheduleManager.GetDueItems();
                        foreach (var dueItem in dueItems)
                            ExecuteScheduleItem(dueItem, slackBotToken, slackChannel);

                        // 6b. on_limit_reset trigger — wasRateLimited is managed by Section 5
                        // with debounce (won't flap). We just check if it transitioned to false.
                        // Note: Section 5 already printed "[EYE] Rate limit cleared" on transition.
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[SCHEDULE] Error: {ex.Message}");
                    }
                }

                frameCount++;

                // Log stats + Slack health
                if (frameCount % 100 == 0)
                {
                    var fps = frameCount / sw.Elapsed.TotalSeconds;
                    var mode = hasValidActionState ? "ActionState" : "Fallback";
                    var slackInfo = slackClient != null
                        ? $", Slack={slackClient.IsConnected}, msgs={slackClient.MessageCount}"
                        : "";
                    Console.WriteLine($"[EYE] frame #{frameCount}, {fps:F1} fps ({mode}{slackInfo})");
                }

                // Slack reconnect watchdog: if connected but no messages for 5 minutes, reconnect
                if (slackClient != null && frameCount % 3000 == 2999)
                {
                    if (slackClient.IsConnected && slackClient.MessageCount <= 1) // only "hello"
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

                // Hot-reload check
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

                // Frame rate control
                var elapsed = (int)frameSw.ElapsedMilliseconds;
                var delay = Math.Max(0, intervalMs - elapsed);
                if (delay > 0) Thread.Sleep(delay);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[EYE] Error: {ex.Message}");
                Console.ResetColor();
                Thread.Sleep(1000);
            }
        }

        // Cleanup Slack
        if (slackClient != null)
        {
            try
            {
                if (!string.IsNullOrEmpty(slackBotToken) && !string.IsNullOrEmpty(slackChannel))
                {
                    Task.Run(async () => await SlackSendViaApi(slackBotToken!, slackChannel!, "AppBotEye+Slack 종료", username: botUsername))
                        .Wait(3000);
                }
                slackClient.Dispose();
            }
            catch { }
        }

        Console.WriteLine($"[EYE] Stopped ({frameCount} frames, {sw.Elapsed.TotalSeconds:F1}s)");
        return 0;
    }

    /// <summary>
    /// Take a quick snapshot of Claude window for diagnostics.
    /// Returns the output directory path, or null on failure.
    /// </summary>
    static string? TakeRateLimitSnapshot(IntPtr claudeHwnd, string tag = "rate_limit")
    {
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd)) return null;

        var outDir = Path.Combine(DataDir, "output", "snapshots", $"{tag}_{DateTime.Now:yyyyMMdd_HHmmss}");
        Directory.CreateDirectory(outDir);

        // Screenshot
        try
        {
            var bmp = ScreenCapture.CaptureWindow(claudeHwnd);
            if (bmp != null && !ScreenCapture.IsBlankBitmap(bmp))
            {
                var imgPath = Path.Combine(outDir, "screenshot.png");
                bmp.Save(imgPath, System.Drawing.Imaging.ImageFormat.Png);
                bmp.Dispose();
            }
        }
        catch { }

        // UIA tree (depth=10 — Electron needs at least 8 to reach RootWebArea + buttons)
        try
        {
            using var uia = new UiaLocator();
            var tree = uia.DumpTree(claudeHwnd, 10);
            File.WriteAllText(Path.Combine(outDir, "uia_tree.txt"), tree, Encoding.UTF8);
        }
        catch { }

        // OCR
        try
        {
            var bmpForOcr = ScreenCapture.CaptureWindow(claudeHwnd);
            if (bmpForOcr != null)
            {
                var ocr = new SimpleOcrAnalyzer();
                var ocrResult = ocr.RecognizeAll(bmpForOcr).GetAwaiter().GetResult();
                if (ocrResult != null)
                    File.WriteAllText(Path.Combine(outDir, "ocr.txt"), ocrResult.FullText, Encoding.UTF8);
                bmpForOcr.Dispose();
            }
        }
        catch { }

        return outDir;
    }

    /// <summary>
    /// Set up Slack event handlers for AppBotEye-integrated Slack daemon.
    /// Handles @mentions, keyword monitoring, thread reply forwarding, and plan approval responses.
    /// RULE: User replies to bot messages are ALWAYS forwarded to Claude prompt.
    /// </summary>
    private static void SetupSlackEventHandlers(SlackSocketClient slack, string botToken, string? channel,
        IntPtr claudeHwnd = default, Func<string?>? getPlanApprovalTs = null,
        Func<string?>? getPermissionApprovalTs = null,
        string? startupTs = null, string? botUsername = null,
        Func<string?>? getStatusStreamingTs = null, Action? resetStatusStreaming = null)
    {
        // Track threads where bot is engaged (for thread reply forwarding)
        // RULE: User replies to bot messages are ALWAYS forwarded to Claude prompt.
        var activeThreads = new HashSet<string>();
        if (!string.IsNullOrEmpty(startupTs))
            activeThreads.Add(startupTs);

        // Track "전달했습니다" ack messages per thread → delete when Claude responds via this bot
        // Key: threadTs, Value: (channel, ackMessageTs)
        var pendingAcks = new Dictionary<string, (string channel, string ackTs)>();

        // Local helper: send with bot username (multi-instance identification)
        async Task<(bool ok, string? ts)> Send(string ch, string text, string? threadTs = null)
            => await SlackSendViaApi(botToken, ch, text, threadTs, username: botUsername);

        // Local helper: delete previous "전달했습니다" ack in a thread (file-based IPC)
        void DeletePendingAck(string threadKey)
        {
            // In-memory cleanup
            if (pendingAcks.TryGetValue(threadKey, out var ack))
            {
                pendingAcks.Remove(threadKey);
                Task.Run(async () => await SlackDeleteMessageAsync(botToken, ack.channel, ack.ackTs)).Wait(3000);
            }
            // File-based cleanup (for cross-process sharing with CLI)
            DeletePendingAckFromFile(botToken, threadKey);
        }

        // Local helper: send ack "전달했습니다" and track it for later deletion (memory + file)
        // Auto-deletes after 15 seconds — ack purpose is instant receipt confirmation only
        void SendAndTrackAck(string ch, string threadKey)
        {
            var (ackOk, ackTs) = Task.Run(async () => await Send(ch,
                "Claude에 전달했습니다!", threadKey)).GetAwaiter().GetResult();
            if (ackOk && ackTs != null)
            {
                pendingAcks[threadKey] = (ch, ackTs);
                activeThreads.Add(ackTs);
                // Persist to file for cross-process access (CLI can delete when replying)
                SavePendingAck(threadKey, ch, ackTs);

                // Auto-cleanup after 15s — Claude response may go to channel, not thread
                var capturedKey = threadKey;
                _ = Task.Run(async () =>
                {
                    await Task.Delay(15_000);
                    DeletePendingAck(capturedKey);
                });
            }
        }

        // Handle @mentions
        slack.OnMention += (msg) =>
        {
            var cleanText = System.Text.RegularExpressions.Regex.Replace(
                msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();
            Console.WriteLine($"[EYE][SLACK] @mention from {msg.User}: {cleanText}");

            // Track this thread so ALL follow-up messages come through
            var threadKey = msg.ThreadTs ?? msg.Timestamp;
            activeThreads.Add(threadKey);

            // ── Status message thread reply → force new channel message for next status update ──
            var statusTs = getStatusStreamingTs?.Invoke();
            if (!string.IsNullOrEmpty(statusTs) && msg.ThreadTs == statusTs && resetStatusStreaming != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[EYE][SLACK] Status thread @mention → next status will be new channel message");
                Console.ResetColor();
                resetStatusStreaming();
            }

            // ── Plan approval via Slack @mention ──
            var planTs = getPlanApprovalTs?.Invoke();
            if (!string.IsNullOrEmpty(planTs) && msg.ThreadTs == planTs && claudeHwnd != IntPtr.Zero)
            {
                if (IsPlanApprovalKeyword(cleanText))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[EYE] Plan APPROVED via Slack by {msg.User}");
                    Console.ResetColor();

                    var approved = ClickApproveButton(claudeHwnd);
                    var reply = approved
                        ? ":white_check_mark: 플랜 승인 완료! Claude가 코딩을 시작합니다."
                        : ":x: 승인 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                    DeletePendingAck(threadKey);
                    Task.Run(async () => await Send(msg.Channel, reply, threadKey)).Wait(5000);
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[EYE] Plan feedback via Slack: {cleanText}");
                    Console.ResetColor();

                    var feedbackOk = TypePlanFeedback(claudeHwnd, cleanText);
                    var reply = feedbackOk
                        ? $":pencil2: 피드백 전달 완료: \"{cleanText}\""
                        : ":x: 피드백 입력란을 찾을 수 없습니다";
                    DeletePendingAck(threadKey);
                    Task.Run(async () => await Send(msg.Channel, reply, threadKey)).Wait(5000);
                    return;
                }
            }

            // ── Permission approval via Slack @mention or thread reply ──
            var permTs = getPermissionApprovalTs?.Invoke();
            if (!string.IsNullOrEmpty(permTs) && msg.ThreadTs == permTs && claudeHwnd != IntPtr.Zero)
            {
                if (IsPlanApprovalKeyword(cleanText)) // "승인", "ㄱㄱ", "ㅇㅇ" etc
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[EYE] Permission APPROVED via Slack by {msg.User}");
                    Console.ResetColor();

                    var permButtons = GetPermissionButtons(claudeHwnd);
                    var allowBtn = permButtons.FirstOrDefault(b =>
                        b.Contains("Allow", StringComparison.OrdinalIgnoreCase) ||
                        b.Contains("허용", StringComparison.OrdinalIgnoreCase) ||
                        b.Contains("수락", StringComparison.OrdinalIgnoreCase));
                    allowBtn ??= permButtons.FirstOrDefault(); // fallback to first button

                    bool clicked = false;
                    if (allowBtn != null)
                        clicked = ClickPermissionButton(claudeHwnd, allowBtn);

                    var reply = clicked
                        ? $":white_check_mark: 권한 승인 완료! (\"{allowBtn}\")"
                        : ":x: 권한 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                    DeletePendingAck(threadKey);
                    Task.Run(async () => await Send(msg.Channel, reply, threadKey)).Wait(5000);
                    return;
                }
            }

            // ── Normal @mention: forward to Claude Code prompt ──
            var promptHelper = new ClaudePromptHelper();
            var promptInfo = promptHelper.FindPrompt();
            if (promptInfo != null)
            {
                // Build thread context (starter + previous message) for Claude
                var threadContext = "";
                var ackThread = msg.ThreadTs ?? msg.Timestamp;
                if (msg.ThreadTs != null)
                {
                    var ctx = GetThreadContext(botToken, msg.Channel, msg.ThreadTs, msg.Timestamp);
                    if (!string.IsNullOrEmpty(ctx))
                        threadContext = $"\n{ctx}\n";
                }

                var promptText = $"{cleanText}{threadContext}\n(Slack @{msg.User} — via AppBotEye+Slack)";
                promptHelper.TypeAndSubmit(promptInfo, promptText);
                Console.WriteLine("[EYE][SLACK] >> Sent to Claude prompt (with thread context)");

                DeletePendingAck(ackThread);
                SendAndTrackAck(msg.Channel, ackThread);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[EYE][SLACK] Prompt not found! Claude 상태 확인 중...");
                Console.ResetColor();

                var ackThread = msg.ThreadTs ?? msg.Timestamp;
                try
                {
                    // Check Claude Desktop status for better diagnosis
                    var claudeStatus = claudeHwnd != IntPtr.Zero ? DetectClaudeDesktopStatus(claudeHwnd) : null;
                    var statusInfo = claudeStatus != null
                        ? $"Claude 상태: {claudeStatus.Item2}"
                        : "Claude 상태: 감지 불가";

                    var diagMsg = $":warning: Claude 프롬프트를 찾을 수 없습니다!\n" +
                        $"{statusInfo}\n" +
                        $"받은 메시지: {cleanText}";
                    Task.Run(async () => await Send(msg.Channel, diagMsg, ackThread)).Wait(5000);
                }
                catch
                {
                    Task.Run(async () => await Send(msg.Channel,
                        $"(Claude 프롬프트 없음) 받은 메시지: {cleanText}", ackThread)).Wait(5000);
                }
            }
        };

        // Handle bot's own messages in threads → delete pending "전달했습니다" ack
        slack.OnSelfMessage += (msg) =>
        {
            if (string.IsNullOrEmpty(msg.ThreadTs)) return;
            // If this bot just posted a REAL response (not ack), delete the pending ack
            if (msg.Text != "Claude에 전달했습니다!")
            {
                DeletePendingAck(msg.ThreadTs);
                Console.WriteLine($"[EYE][SLACK] Deleted ack in thread {msg.ThreadTs} (bot replied)");
            }
        };

        // Handle channel messages (thread reply forwarding + keyword monitoring + plan approval)
        var keywords = new[] { "클롯", "클봇", "claude", "appbot", "wkappbot" };
        slack.OnMessage += (msg) =>
        {
            if (string.IsNullOrEmpty(msg.Text)) return;

            // Debug: log thread info for diagnosis
            Console.WriteLine($"[EYE][SLACK] MSG from={msg.User} ch={msg.Channel} thread={msg.ThreadTs ?? "(none)"} text={msg.Text[..Math.Min(msg.Text.Length, 40)]}");

            // ── Status message thread reply → force new channel message for next status update ──
            var statusTs = getStatusStreamingTs?.Invoke();
            if (!string.IsNullOrEmpty(statusTs) && msg.ThreadTs == statusTs && resetStatusStreaming != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[EYE][SLACK] Status thread reply detected → next status will be new channel message");
                Console.ResetColor();
                resetStatusStreaming();
                // Don't return — continue to forward the message to Claude prompt as well
            }

            // ── Plan approval via thread reply (no @mention needed) ──
            var planTs = getPlanApprovalTs?.Invoke();
            if (!string.IsNullOrEmpty(planTs) && msg.ThreadTs == planTs && claudeHwnd != IntPtr.Zero)
            {
                var cleanText = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();

                if (IsPlanApprovalKeyword(cleanText))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[EYE] Plan APPROVED via Slack thread by {msg.User}");
                    Console.ResetColor();

                    var approved = ClickApproveButton(claudeHwnd);
                    var reply = approved
                        ? ":white_check_mark: 플랜 승인 완료! Claude가 코딩을 시작합니다."
                        : ":x: 승인 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                    DeletePendingAck(msg.ThreadTs!);
                    Task.Run(async () => await Send(msg.Channel, reply, msg.ThreadTs)).Wait(5000);
                    return;
                }
                else if (cleanText.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[EYE] Plan feedback via Slack thread: {cleanText}");
                    Console.ResetColor();

                    var feedbackOk = TypePlanFeedback(claudeHwnd, cleanText);
                    var reply = feedbackOk
                        ? $":pencil2: 피드백 전달 완료: \"{cleanText}\""
                        : ":x: 피드백 입력란을 찾을 수 없습니다";
                    DeletePendingAck(msg.ThreadTs!);
                    Task.Run(async () => await Send(msg.Channel, reply, msg.ThreadTs)).Wait(5000);
                    return;
                }
            }

            // ── Permission approval via thread reply (no @mention needed) ──
            var permTs2 = getPermissionApprovalTs?.Invoke();
            if (!string.IsNullOrEmpty(permTs2) && msg.ThreadTs == permTs2 && claudeHwnd != IntPtr.Zero)
            {
                var cleanPerm = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();

                if (IsPlanApprovalKeyword(cleanPerm))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[EYE] Permission APPROVED via Slack thread by {msg.User}");
                    Console.ResetColor();

                    var permBtns = GetPermissionButtons(claudeHwnd);
                    var allowBtn = permBtns.FirstOrDefault(b =>
                        b.Contains("Allow", StringComparison.OrdinalIgnoreCase) ||
                        b.Contains("허용", StringComparison.OrdinalIgnoreCase) ||
                        b.Contains("수락", StringComparison.OrdinalIgnoreCase));
                    allowBtn ??= permBtns.FirstOrDefault();

                    bool clicked = false;
                    if (allowBtn != null)
                        clicked = ClickPermissionButton(claudeHwnd, allowBtn);

                    var reply = clicked
                        ? $":white_check_mark: 권한 승인 완료! (\"{allowBtn}\")"
                        : ":x: 권한 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                    DeletePendingAck(msg.ThreadTs!);
                    Task.Run(async () => await Send(msg.Channel, reply, msg.ThreadTs)).Wait(5000);
                    return;
                }
            }

            // ── Auto-track threads on bot's own messages (API check) ──
            if (msg.ThreadTs != null && !activeThreads.Contains(msg.ThreadTs))
            {
                if (IsOwnThread(botToken, msg.Channel, msg.ThreadTs))
                {
                    activeThreads.Add(msg.ThreadTs);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[EYE][SLACK] Auto-tracking thread on bot's message (ts={msg.ThreadTs})");
                    Console.ResetColor();
                }
            }

            // ── Thread reply to bot's message → ALWAYS forward to Claude prompt ──
            if (msg.ThreadTs != null && activeThreads.Contains(msg.ThreadTs))
            {
                var cleanText = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();
                if (string.IsNullOrEmpty(cleanText)) return;

                // "그만" / "stop" → stop tracking this thread
                var trimmedLower = cleanText.Trim().ToLowerInvariant();
                if (trimmedLower == "그만" || trimmedLower == "stop" || trimmedLower == "ㄱㅁ")
                {
                    activeThreads.Remove(msg.ThreadTs);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"[EYE][SLACK] << thread stopped by {msg.User} (ts={msg.ThreadTs})");
                    Console.ResetColor();
                    Task.Run(async () => await Send(msg.Channel,
                        "알겠습니다~ 이 쓰레드에서 물러납니다!", msg.ThreadTs)).Wait(5000);
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[EYE][SLACK] << thread reply from {msg.User}: {cleanText}");
                Console.ResetColor();

                var trPromptHelper = new ClaudePromptHelper();
                var trPromptInfo = trPromptHelper.FindPrompt();
                if (trPromptInfo != null)
                {
                    // Build thread context (starter + previous message) for Claude
                    var threadContext = "";
                    if (msg.ThreadTs != null)
                    {
                        var ctx = GetThreadContext(botToken, msg.Channel, msg.ThreadTs, msg.Timestamp);
                        if (!string.IsNullOrEmpty(ctx))
                            threadContext = $"\n{ctx}\n";
                    }

                    var promptText = $"{cleanText}{threadContext}\n(Slack thread reply @{msg.User} — via AppBotEye)";
                    trPromptHelper.TypeAndSubmit(trPromptInfo, promptText);
                    Console.WriteLine("[EYE][SLACK] >> Thread reply sent to Claude prompt (with context)");

                    DeletePendingAck(msg.ThreadTs!);
                    SendAndTrackAck(msg.Channel, msg.ThreadTs!);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[EYE][SLACK] >> Thread reply: Prompt not found!");
                    Console.ResetColor();

                    try
                    {
                        var trClaudeStatus = claudeHwnd != IntPtr.Zero ? DetectClaudeDesktopStatus(claudeHwnd) : null;
                        var trStatusInfo = trClaudeStatus != null
                            ? $"Claude 상태: {trClaudeStatus.Item2}"
                            : "Claude 상태: 감지 불가";
                        var diagMsg = $":warning: Claude 프롬프트를 찾을 수 없습니다!\n" +
                            $"{trStatusInfo}\n" +
                            $"스레드 답장: {cleanText}";
                        Task.Run(async () => await Send(msg.Channel, diagMsg, msg.ThreadTs)).Wait(5000);
                    }
                    catch
                    {
                        Task.Run(async () => await Send(msg.Channel,
                            $"(Claude 프롬프트 없음) 스레드 답장: {cleanText}", msg.ThreadTs)).Wait(5000);
                    }
                }
                return;
            }

            // ── Keyword monitoring → forward to Claude prompt ──
            var textLower = msg.Text.ToLowerInvariant();
            var matchedKw = keywords.FirstOrDefault(kw => textLower.Contains(kw.ToLowerInvariant()));
            if (matchedKw != null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"[EYE][SLACK] keyword \"{matchedKw}\" from {msg.User}: {msg.Text}");
                Console.ResetColor();

                // Start tracking this thread so follow-up messages also come through
                var kwThreadKey = msg.ThreadTs ?? msg.Timestamp;
                activeThreads.Add(kwThreadKey);

                var cleanKwText = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();
                if (string.IsNullOrEmpty(cleanKwText)) return;

                var kwPromptHelper = new ClaudePromptHelper();
                var kwPromptInfo = kwPromptHelper.FindPrompt();
                if (kwPromptInfo != null)
                {
                    // Build thread context (starter + previous message) for Claude
                    var threadContext = "";
                    if (msg.ThreadTs != null)
                    {
                        var ctx = GetThreadContext(botToken, msg.Channel, msg.ThreadTs, msg.Timestamp);
                        if (!string.IsNullOrEmpty(ctx))
                            threadContext = $"\n{ctx}\n";
                    }

                    var promptText = $"{cleanKwText}{threadContext}\n(Slack keyword:\"{matchedKw}\" @{msg.User} — via AppBotEye)";
                    kwPromptHelper.TypeAndSubmit(kwPromptInfo, promptText);
                    Console.WriteLine("[EYE][SLACK] >> Keyword match sent to Claude prompt (with context)");

                    DeletePendingAck(kwThreadKey);
                    SendAndTrackAck(msg.Channel, kwThreadKey);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[EYE][SLACK] >> Keyword match: Prompt not found!");
                    Console.ResetColor();

                    try
                    {
                        var kwClaudeStatus = claudeHwnd != IntPtr.Zero ? DetectClaudeDesktopStatus(claudeHwnd) : null;
                        var kwStatusInfo = kwClaudeStatus != null
                            ? $"Claude 상태: {kwClaudeStatus.Item2}"
                            : "Claude 상태: 감지 불가";
                        var diagMsg = $":warning: Claude 프롬프트를 찾을 수 없습니다!\n" +
                            $"{kwStatusInfo}\n" +
                            $"키워드 \"{matchedKw}\" 감지: {cleanKwText}";
                        Task.Run(async () => await Send(msg.Channel, diagMsg, kwThreadKey)).Wait(5000);
                    }
                    catch
                    {
                        Task.Run(async () => await Send(msg.Channel,
                            $"(Claude 프롬프트 없음) 키워드 \"{matchedKw}\" 감지: {cleanKwText}", kwThreadKey)).Wait(5000);
                    }
                }
            }
        };
    }

    /// <summary>
    /// Execute a single schedule item: resolve prompt text, find Claude prompt, type and submit.
    /// Updates schedule status to done/failed. Sends Slack notification.
    /// </summary>
    private static void ExecuteScheduleItem(ScheduleItem item, string? slackBotToken, string? slackChannel)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[SCHEDULE] Executing: {item.Id} ({item.Type})");
        Console.ResetColor();

        try
        {
            // 1. Resolve prompt text
            string? promptText = item.Prompt;
            if (!string.IsNullOrEmpty(item.PromptFile))
            {
                if (File.Exists(item.PromptFile))
                    promptText = File.ReadAllText(item.PromptFile);
                else
                {
                    ScheduleManager.UpdateStatus(item.Id, "failed", $"Prompt file not found: {item.PromptFile}");
                    ScheduleNotifySlack(slackBotToken, slackChannel,
                        $":x: 스케줄 실패 ({item.Id}): 프롬프트 파일 없음 — {item.PromptFile}");
                    return;
                }
            }

            if (string.IsNullOrEmpty(promptText))
            {
                ScheduleManager.UpdateStatus(item.Id, "failed", "Empty prompt");
                return;
            }

            // 2. Slack pre-notification
            if (item.NotifySlack)
                ScheduleNotifySlack(slackBotToken, slackChannel,
                    $":rocket: 스케줄 실행 중: {item.Id} ({item.Type})");

            // 3. Find Claude prompt and type
            var promptHelper = new ClaudePromptHelper();
            var promptInfo = promptHelper.FindPrompt();
            if (promptInfo == null)
            {
                // Retry once after 3 seconds (Claude may still be loading)
                Thread.Sleep(3000);
                promptInfo = promptHelper.FindPrompt();
            }

            if (promptInfo == null)
            {
                ScheduleManager.UpdateStatus(item.Id, "failed", "Claude prompt not found");
                ScheduleNotifySlack(slackBotToken, slackChannel,
                    $":x: 스케줄 실패 ({item.Id}): Claude 프롬프트를 찾을 수 없습니다");
                return;
            }

            // 4. Type and submit
            var suffix = $"\n\n(자동 복구 — schedule {item.Id}, {item.Type})";
            promptHelper.TypeAndSubmit(promptInfo, promptText + suffix);

            // 5. Mark done + advance recurring
            ScheduleManager.UpdateStatus(item.Id, "done");
            if (item.Type == "recurring")
                ScheduleManager.AdvanceRecurring(item.Id);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SCHEDULE] Done: {item.Id}");
            Console.ResetColor();

            if (item.NotifySlack)
                ScheduleNotifySlack(slackBotToken, slackChannel,
                    $":white_check_mark: 스케줄 완료: {item.Id} — 프롬프트 입력 완료");
        }
        catch (Exception ex)
        {
            ScheduleManager.UpdateStatus(item.Id, "failed", ex.Message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[SCHEDULE] Failed: {item.Id} — {ex.Message}");
            Console.ResetColor();
            ScheduleNotifySlack(slackBotToken, slackChannel,
                $":x: 스케줄 실패 ({item.Id}): {ex.Message}");
        }
    }

    /// <summary>Send Slack notification for schedule events (best-effort, non-blocking).</summary>
    private static void ScheduleNotifySlack(string? botToken, string? channel, string message)
    {
        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;
        try
        {
            Task.Run(async () => await SlackSendViaApi(botToken!, channel!, message, username: BotUsername)).Wait(5000);
        }
        catch { /* best-effort */ }
    }
}
