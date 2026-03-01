// AppBotEyeClaudeDetector.cs — Claude Desktop UIA detection methods.
// Finds Claude Desktop window and detects its current status (executing, prompt ready, etc.)
// Extracted from AppBotEyeCommands.cs for readability.

using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Vision;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    private static bool _claudeWindowSearchLogged = false; // log once to avoid spam on repeated calls

    /// <summary>
    /// Find Claude Desktop main window by walking the parent process tree.
    /// wkappbot → ... → claude.exe (Electron) — find its main window.
    /// Much faster than scanning all 14+ claude.exe processes!
    /// </summary>
    private static IntPtr FindClaudeDesktopWindow()
    {
        bool verbose = !_claudeWindowSearchLogged; // log only on first call

        // Walk up: wkappbot.exe → parent → grandparent → ... find claude.exe
        var claudePids = new HashSet<int>();
        try
        {
            int pid = Process.GetCurrentProcess().Id;
            for (int depth = 0; depth < 10; depth++)
            {
                var parentPid = GetParentPid(pid);
                if (parentPid <= 0 || parentPid == pid) break;

                try
                {
                    var parent = Process.GetProcessById(parentPid);
                    if (verbose)
                        Console.WriteLine($"[EYE] Ancestor: {parent.ProcessName} (PID={parentPid})");
                    if (parent.ProcessName.Equals("claude", StringComparison.OrdinalIgnoreCase))
                    {
                        claudePids.Add(parentPid);
                        // Also add siblings (Electron uses multiple processes under same parent tree)
                        // Get the parent's parent — the root claude.exe
                        var rootPid = GetParentPid(parentPid);
                        if (rootPid > 0)
                        {
                            try
                            {
                                var root = Process.GetProcessById(rootPid);
                                if (root.ProcessName.Equals("claude", StringComparison.OrdinalIgnoreCase))
                                    claudePids.Add(rootPid);
                            }
                            catch { }
                        }
                        break;
                    }
                    pid = parentPid;
                }
                catch { break; }
            }
        }
        catch { }

        // If ancestor walk didn't find claude.exe, try all claude processes (slow fallback)
        if (claudePids.Count == 0)
        {
            if (verbose)
                Console.WriteLine("[EYE] Ancestor walk didn't find claude.exe, scanning all...");
            foreach (var proc in Process.GetProcessesByName("claude"))
                claudePids.Add(proc.Id);
        }

        _claudeWindowSearchLogged = true;

        // Find the visible main window (with title bar + sizable) from claude PIDs
        IntPtr found = IntPtr.Zero;
        EnumWindows((hwnd, _) =>
        {
            if (!IsWindowVisible(hwnd)) return true;

            GetWindowThreadProcessId(hwnd, out var pid);
            if (!claudePids.Contains(pid)) return true;

            // Check for WS_CAPTION (main window with title bar)
            var style = GetWindowLongW(hwnd, -16);
            if ((style & 0x00C00000) == 0) return true;

            // Verify it's a sizable window (not a small popup or notification)
            if (GetWindowRect(hwnd, out var rect))
            {
                var w = rect.Right - rect.Left;
                var h = rect.Bottom - rect.Top;
                if (w > 400 && h > 300)
                {
                    found = hwnd;
                    return false; // stop enumeration
                }
            }

            return true;
        }, IntPtr.Zero);

        return found;
    }

    /// <summary>Get parent PID using NtQueryInformationProcess (fast, no WMI dependency).</summary>
    private static int GetParentPid(int pid)
    {
        try
        {
            var handle = OpenProcess(0x0400 /* PROCESS_QUERY_INFORMATION */, false, pid);
            if (handle == IntPtr.Zero)
                handle = OpenProcess(0x1000 /* PROCESS_QUERY_LIMITED_INFORMATION */, false, pid);
            if (handle == IntPtr.Zero) return -1;

            try
            {
                var pbi = new PROCESS_BASIC_INFORMATION();
                int status = NtQueryInformationProcess(handle, 0, ref pbi, Marshal.SizeOf(pbi), out _);
                if (status == 0)
                    return (int)pbi.InheritedFromUniqueProcessId;
            }
            finally
            {
                CloseHandle(handle);
            }
        }
        catch { }
        return -1;
    }

    // ── Plan Approval Integration ──────────────────────────────────

    /// <summary>
    /// Extract plan content. Two-tier strategy:
    ///   1st: File read (~/.claude/plans/*.md) — fast, full content
    ///   2nd: UIA text collection from Claude Desktop — environment-agnostic, accessibility standard
    /// Returns (source, content) or null if not found.
    /// </summary>
    static (string source, string content)? ExtractPlanContent(IntPtr claudeHwnd)
    {
        // ── Tier 1: File read (fast, full content) ──
        try
        {
            var plansDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".claude", "plans");

            if (Directory.Exists(plansDir))
            {
                var latestFile = Directory.GetFiles(plansDir, "*.md")
                    .OrderByDescending(f => File.GetLastWriteTimeUtc(f))
                    .FirstOrDefault();

                if (latestFile != null)
                {
                    var content = File.ReadAllText(latestFile);
                    if (!string.IsNullOrWhiteSpace(content))
                        return (Path.GetFileName(latestFile), content);
                }
            }
        }
        catch { /* fall through to UIA */ }

        // ── Tier 2: UIA text collection (accessibility standard, environment-agnostic) ──
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            return null;

        try
        {
            using var automation = new UIA3Automation();
            var window = automation.FromHandle(claudeHwnd);
            if (window == null) return null;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());

            // Collect all Text elements from the plan area
            // Plan content is between the approval button area and above
            var textElements = window.FindAllDescendants(cf.ByControlType(ControlType.Text));
            if (textElements == null || textElements.Length == 0) return null;

            var planLines = new StringBuilder();
            foreach (var elem in textElements)
            {
                try
                {
                    var text = elem.Name;
                    if (string.IsNullOrEmpty(text)) continue;

                    var rect = elem.BoundingRectangle;
                    // Skip off-screen elements and tiny elements
                    if (rect.Width < 10 || rect.Height < 5) continue;
                    // Skip UI labels (very short single words that aren't plan content)
                    if (text.Length < 3 && !text.StartsWith("#")) continue;

                    planLines.AppendLine(text.Trim());
                }
                catch { continue; }
            }

            var uiaContent = planLines.ToString().Trim();
            if (uiaContent.Length > 50) // minimal threshold for valid plan content
                return ("UIA", uiaContent);
        }
        catch { }

        return null;
    }

    /// <summary>
    /// Click the plan approval button via UIA Invoke pattern.
    /// Searches for a Button containing "계획 승인" in its Name.
    /// Returns true if button was found and invoked.
    /// </summary>
    static bool ClickApproveButton(IntPtr claudeHwnd)
    {
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            return false;

        try
        {
            using var automation = new UIA3Automation();
            var window = automation.FromHandle(claudeHwnd);
            if (window == null) return false;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());

            // Find all buttons and look for approval button
            var buttons = window.FindAllDescendants(cf.ByControlType(ControlType.Button));
            if (buttons == null) return false;

            foreach (var btn in buttons)
            {
                try
                {
                    var name = btn.Name;
                    if (string.IsNullOrEmpty(name)) continue;

                    // Match "Claude의 계획 승인 및 코딩 시작" or similar patterns
                    if (name.Contains("계획 승인", StringComparison.OrdinalIgnoreCase) ||
                        name.Contains("Approve", StringComparison.OrdinalIgnoreCase))
                    {
                        // Click via UIA Invoke (focusless!)
                        if (btn.Patterns.Invoke.IsSupported)
                        {
                            btn.Patterns.Invoke.Pattern.Invoke();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"[EYE] Plan approved via UIA Invoke: \"{name}\"");
                            Console.ResetColor();
                            return true;
                        }
                    }
                }
                catch { continue; }
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] ClickApproveButton error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Type text into the plan feedback edit field and submit.
    /// Used when Slack user wants to modify the plan instead of approving.
    /// </summary>
    static bool TypePlanFeedback(IntPtr claudeHwnd, string feedback)
    {
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            return false;

        try
        {
            using var automation = new UIA3Automation();
            var window = automation.FromHandle(claudeHwnd);
            if (window == null) return false;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());

            // Find the edit field: "Claude에게 대신 수행할 작업 알려주기"
            var edits = window.FindAllDescendants(cf.ByControlType(ControlType.Edit));
            if (edits == null) return false;

            foreach (var edit in edits)
            {
                try
                {
                    var name = edit.Name;
                    if (name != null && name.Contains("대신 수행할 작업", StringComparison.OrdinalIgnoreCase))
                    {
                        // Set value via UIA Value pattern
                        if (edit.Patterns.Value.IsSupported)
                        {
                            edit.Patterns.Value.Pattern.SetValue(feedback);

                            // Submit: find and click the submit button (피드백 제출)
                            var submitBtn = window.FindFirstDescendant(
                                cf.ByControlType(ControlType.Button).And(cf.ByName("피드백 제출")));
                            if (submitBtn?.Patterns.Invoke.IsSupported == true)
                            {
                                submitBtn.Patterns.Invoke.Pattern.Invoke();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"[EYE] Plan feedback submitted: \"{feedback}\"");
                                Console.ResetColor();
                                return true;
                            }
                        }
                    }
                }
                catch { continue; }
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] TypePlanFeedback error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Check if text is a plan approval keyword.
    /// Matches: "승인", "ㅇ", "V", "ok", "approve", "ㄱㄱ", "고", "넹", etc.
    /// </summary>
    static bool IsPlanApprovalKeyword(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        var t = text.Trim().ToLowerInvariant();
        return t is "승인" or "ㅇ" or "v" or "ok" or "approve" or "yes"
            or "ㄱㄱ" or "고" or "넹" or "넵" or "ㅇㅇ" or "확인"
            or "좋아" or "진행" or "시작" or "go" or "lgtm" or "ㅎㅎ";
    }

    /// <summary>
    /// Click a permission button by matching button name via UIA.
    /// Used when Slack user clicks a permission button (Allow/Deny/etc).
    /// Returns true if button was found and invoked.
    /// </summary>
    static bool ClickPermissionButton(IntPtr claudeHwnd, string buttonText)
    {
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            return false;

        try
        {
            using var automation = new UIA3Automation();
            var window = automation.FromHandle(claudeHwnd);
            if (window == null) return false;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());
            var buttons = window.FindAllDescendants(cf.ByControlType(ControlType.Button));
            if (buttons == null) return false;

            foreach (var btn in buttons)
            {
                try
                {
                    var name = btn.Name;
                    if (string.IsNullOrEmpty(name)) continue;

                    // Exact match on button text
                    if (name.Equals(buttonText, StringComparison.OrdinalIgnoreCase))
                    {
                        if (btn.Patterns.Invoke.IsSupported)
                        {
                            btn.Patterns.Invoke.Pattern.Invoke();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"[EYE] Permission button clicked via UIA: \"{name}\"");
                            Console.ResetColor();
                            return true;
                        }
                    }
                }
                catch { continue; }
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] ClickPermissionButton error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Extract current permission button names from Claude Desktop.
    /// Returns list of button names for permission-related actions.
    /// </summary>
    static List<string> GetPermissionButtons(IntPtr claudeHwnd)
    {
        var result = new List<string>();
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            return result;

        try
        {
            using var automation = new UIA3Automation();
            var window = automation.FromHandle(claudeHwnd);
            if (window == null) return result;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());
            var buttons = window.FindAllDescendants(cf.ByControlType(ControlType.Button));
            if (buttons == null) return result;

            foreach (var btn in buttons)
            {
                try
                {
                    var name = btn.Name;
                    if (string.IsNullOrEmpty(name)) continue;

                    if (name.Contains("Allow", StringComparison.OrdinalIgnoreCase) ||
                        name.Contains("Deny", StringComparison.OrdinalIgnoreCase) ||
                        name.Contains("허용", StringComparison.OrdinalIgnoreCase) ||
                        name.Contains("거부", StringComparison.OrdinalIgnoreCase) ||
                        name.Contains("수락", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Add(name);
                    }
                }
                catch { continue; }
            }
        }
        catch { }

        return result;
    }

    // ── Claude Desktop UIA Status Detection ──────────────────────

    /// <summary>
    /// Detect Claude Desktop's current status via UIA tree inspection.
    /// Returns (status_key, display_text) tuple, or null if status unknown/idle.
    /// </summary>
    static Tuple<string, string>? DetectClaudeDesktopStatus(IntPtr claudeHwnd)
    {
        if (claudeHwnd == IntPtr.Zero || !IsWindow(claudeHwnd))
            return null;

        try
        {
            using var automation = new UIA3Automation();
            var window = automation.FromHandle(claudeHwnd);
            if (window == null) return null;

            var cf = new ConditionFactory(new UIA3PropertyLibrary());

            // 1. Check for "중단" (Stop) button → Claude is executing
            var stopButton = window.FindFirstDescendant(
                cf.ByControlType(ControlType.Button).And(cf.ByName("중단")));
            if (stopButton != null)
            {
                // Get latest status text from StatusBar
                var statusText = GetLatestStatusBarText(window, cf);
                return Tuple.Create("executing", statusText ?? "실행 중");
            }

            // 2. Check for rate limit FIRST (before turn-form!)
            // ★ Rate limit screen often still has turn-form visible → would be mis-detected as "prompt_ready"
            // Text: "You've hit your limit · resets 4pm (Asia/Seoul)"
            // ★ IMPORTANT: Must filter out AppBotEye's own overlay text!
            //   AppBotEye is a child window of Claude Desktop → its Text elements appear in FindAllDescendants
            //   If AppBotEye displays "rate limit" text → false positive self-detection loop!
            //   Solution: Search only inside "RootWebArea" Document (Claude's actual web content)
            try
            {
                // Find the RootWebArea Document first — limits search to Claude's web content only
                var rootWebArea = window.FindFirstDescendant(cf.ByAutomationId("RootWebArea"));
                var searchRoot = rootWebArea ?? window; // fallback to full window if not found
                var allTexts = searchRoot.FindAllDescendants(
                    cf.ByControlType(ControlType.Text));
                if (allTexts != null)
                {
                    foreach (var textElem in allTexts)
                    {
                        try
                        {
                            var name = textElem.Name;
                            if (string.IsNullOrEmpty(name)) continue;

                            // ★ Skip AppBotEye overlay text (prevents self-detection loop!)
                            // AppBotEye text elements are multi-line with URLs, action details, etc.
                            // Real rate limit text is a single short phrase like "You've hit your limit"
                            if (name.Contains("AppBot", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("navigate →", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("Claude:", StringComparison.OrdinalIgnoreCase) ||
                                name.Length > 200) // Real rate limit text is short
                                continue;

                            // Only match the exact Claude rate limit phrases
                            // "limit" + "reset" was too broad → matched AppBotEye overlay text!
                            if (name.Contains("hit your limit", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("You've hit your usage limit", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("한도를 초과", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("사용 한도", StringComparison.OrdinalIgnoreCase))
                            {
                                // Try to extract reset time from this element or siblings
                                var resetTime = ParseResetTime(name);
                                if (resetTime == null)
                                {
                                    foreach (var sibling in allTexts)
                                    {
                                        try
                                        {
                                            var sibName = sibling.Name;
                                            if (!string.IsNullOrEmpty(sibName))
                                            {
                                                resetTime = ParseResetTime(sibName);
                                                if (resetTime != null) break;
                                            }
                                        }
                                        catch { }
                                    }
                                }

                                var displayText = resetTime != null
                                    ? $"한도 초과 — {resetTime.Value:HH:mm}에 리셋"
                                    : "한도 초과";

                                return Tuple.Create("rate_limit", displayText);
                            }
                        }
                        catch { continue; }
                    }
                }

                // ★ 2b. OCR fallback — Claude Desktop sometimes exposes NO UIA Text elements
                // When the UIA tree is sparse (no Text children under RootWebArea),
                // fall back to OCR screenshot analysis to detect rate limit text.
                // This handles Claude Desktop updates that change accessibility structure.
                if (allTexts == null || allTexts.Length == 0)
                {
                    var ocrResult = TryDetectRateLimitViaOcr(claudeHwnd);
                    if (ocrResult != null) return ocrResult;
                }
            }
            catch { }

            // 3. Check for plan approval dialog via Button search
            var approveButton = window.FindFirstDescendant(
                cf.ByControlType(ControlType.Button).And(cf.ByName("Approve")));
            if (approveButton != null)
                return Tuple.Create("plan_approval_pending", "계획승인 대기");

            // 3.5. Check for permission/approval prompt via Button search
            try
            {
                var buttons = window.FindAllDescendants(cf.ByControlType(ControlType.Button));
                if (buttons != null && buttons.Length > 0)
                {
                    var permButtons = new List<string>();
                    foreach (var btn in buttons)
                    {
                        try
                        {
                            var name = btn.Name;
                            if (string.IsNullOrEmpty(name)) continue;

                            if (name.Contains("Allow", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("Deny", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("허용", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("거부", StringComparison.OrdinalIgnoreCase) ||
                                name.Contains("수락", StringComparison.OrdinalIgnoreCase))
                            {
                                permButtons.Add(name);
                            }
                        }
                        catch { continue; }
                    }

                    if (permButtons.Count >= 2)
                    {
                        var btnList = string.Join(" / ", permButtons);
                        return Tuple.Create("permission_prompt", $"수락 요구: [{btnList}]");
                    }
                }
            }
            catch { }

            // 4. ★ turn-form Name fallback — Electron webview buttons often aren't exposed as UIA buttons
            // turn-form Name contains concatenated text of all child elements, e.g.:
            //   " 메뉴 토글 권한 요청 Opus 4.6 중단"  → executing + permission prompt
            //   " 메뉴 토글 Opus 4.6"                 → prompt ready (normal)
            //   "계획" button visible nearby           → plan approval pending
            var turnForm = window.FindFirstDescendant(
                cf.ByAutomationId("turn-form"));
            if (turnForm != null)
            {
                try
                {
                    var rect = turnForm.BoundingRectangle;
                    if (rect.Height > 30)
                    {
                        var turnFormName = turnForm.Name ?? "";

                        // 4a. "중단" in turn-form Name → executing (Button search missed it)
                        if (turnFormName.Contains("중단"))
                        {
                            // Also check for permission prompt within executing state
                            if (turnFormName.Contains("권한 요청") || turnFormName.Contains("권한요청"))
                            {
                                return Tuple.Create("permission_prompt", "권한 요청 대기 (실행 중)");
                            }
                            var statusText = GetLatestStatusBarText(window, cf);
                            return Tuple.Create("executing", statusText ?? "실행 중");
                        }

                        // 4b. "권한 요청" in turn-form Name → permission prompt
                        if (turnFormName.Contains("권한 요청") || turnFormName.Contains("권한요청"))
                        {
                            return Tuple.Create("permission_prompt", "권한 요청 대기");
                        }

                        // 4c. Check for plan approval: look for "계획" button nearby
                        // Claude Desktop shows "계획" tab/button when in plan mode
                        try
                        {
                            var planButtons = window.FindAllDescendants(cf.ByControlType(ControlType.Button));
                            if (planButtons != null)
                            {
                                bool hasPlanButton = false;
                                bool hasApproveKorean = false;
                                foreach (var btn in planButtons)
                                {
                                    try
                                    {
                                        var name = btn.Name;
                                        if (string.IsNullOrEmpty(name)) continue;
                                        if (name.Contains("계획 승인")) hasApproveKorean = true;
                                        if (name == "계획") hasPlanButton = true;
                                    }
                                    catch { }
                                }
                                if (hasApproveKorean)
                                    return Tuple.Create("plan_approval_pending", "계획승인 대기");
                                if (hasPlanButton)
                                    return Tuple.Create("plan_mode", "계획 모드 (피드백 입력 대기)");
                            }
                        }
                        catch { }

                        // 4d. Normal prompt ready
                        return Tuple.Create("prompt_ready", "프롬프트 입력 대기");
                    }
                }
                catch { }
            }

            // 5. Idle — no special state detected
            return null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Get the most recent StatusBar text from Claude Desktop's UIA tree.
    /// Returns text like "파일 읽음", "명령 실행함", "도구 사용함".
    /// </summary>
    private static string? GetLatestStatusBarText(
        FlaUI.Core.AutomationElements.AutomationElement window, ConditionFactory cf)
    {
        try
        {
            var statusBars = window.FindAllDescendants(cf.ByControlType(ControlType.StatusBar));
            if (statusBars == null || statusBars.Length == 0) return null;

            string? latestText = null;
            int maxY = int.MinValue;

            foreach (var sb in statusBars)
            {
                try
                {
                    var rect = sb.BoundingRectangle;
                    if (rect.Y < 0 || rect.Height < 1) continue;

                    var textChild = sb.FindFirstChild(cf.ByControlType(ControlType.Text));
                    if (textChild == null) continue;

                    var text = textChild.Name;
                    if (string.IsNullOrEmpty(text)) continue;

                    if (rect.Y > maxY)
                    {
                        maxY = (int)rect.Y;
                        latestText = text;
                    }
                }
                catch { continue; }
            }

            return latestText;
        }
        catch { return null; }
    }

    // ── OCR-based rate limit detection ──────────────────────────────────────
    // Throttle: max once per 5 seconds to avoid performance overhead in the polling loop.
    private static DateTime _lastOcrRateLimitCheck = DateTime.MinValue;
    private static readonly TimeSpan OcrRateLimitInterval = TimeSpan.FromSeconds(5);

    /// <summary>
    /// OCR fallback for rate limit detection when UIA tree has no Text elements.
    /// Captures the Claude Desktop window via PrintWindow and runs OCR to find
    /// "hit your limit" / "resets Xam/pm" text.
    /// Throttled to max once per 5 seconds to avoid performance overhead.
    /// </summary>
    static Tuple<string, string>? TryDetectRateLimitViaOcr(IntPtr claudeHwnd)
    {
        // Throttle: skip if checked recently
        if (DateTime.UtcNow - _lastOcrRateLimitCheck < OcrRateLimitInterval)
            return null;
        _lastOcrRateLimitCheck = DateTime.UtcNow;

        try
        {
            // Capture window via PrintWindow (Z-order safe, focusless)
            using var bitmap = ScreenCapture.CaptureWindow(claudeHwnd);
            if (bitmap == null || ScreenCapture.IsBlankBitmap(bitmap))
                return null;

            // Run OCR — crop upper portion only (rate limit banner is at top)
            // Cropping to top 40% reduces OCR time significantly
            var cropHeight = Math.Min(bitmap.Height * 2 / 5, bitmap.Height);
            using var topCrop = bitmap.Clone(
                new Rectangle(0, 0, bitmap.Width, cropHeight),
                bitmap.PixelFormat);

            var ocr = new SimpleOcrAnalyzer();
            var ocrResult = ocr.RecognizeAll(topCrop).GetAwaiter().GetResult();
            if (ocrResult == null || string.IsNullOrEmpty(ocrResult.FullText))
                return null;

            var fullText = ocrResult.FullText;

            // Check for rate limit phrases in OCR text
            if (fullText.Contains("hit your limit", StringComparison.OrdinalIgnoreCase) ||
                fullText.Contains("hit your usage limit", StringComparison.OrdinalIgnoreCase) ||
                fullText.Contains("한도를 초과", StringComparison.OrdinalIgnoreCase) ||
                fullText.Contains("사용 한도", StringComparison.OrdinalIgnoreCase))
            {
                // Extract reset time from OCR text
                var resetTime = ParseResetTime(fullText);
                var displayText = resetTime != null
                    ? $"한도 초과 — {resetTime.Value:HH:mm}에 리셋"
                    : "한도 초과";

                return Tuple.Create("rate_limit", displayText);
            }
        }
        catch { /* OCR is best-effort fallback */ }

        return null;
    }

    /// <summary>
    /// Parse reset time from rate limit text.
    /// Patterns: "resets 4pm", "resets 4pm (Asia/Seoul)", "resets 16:00"
    /// Returns local DateTime or null.
    /// </summary>
    static DateTime? ParseResetTime(string text)
    {
        if (string.IsNullOrEmpty(text)) return null;

        // Pattern 1: "resets 4pm" or "resets 4 pm" with optional timezone
        var match = Regex.Match(text,
            @"resets?\s+(\d{1,2})\s*(am|pm)\s*(?:\(([^)]+)\))?",
            RegexOptions.IgnoreCase);
        if (match.Success)
        {
            int hour = int.Parse(match.Groups[1].Value);
            bool isPm = match.Groups[2].Value.Equals("pm", StringComparison.OrdinalIgnoreCase);
            if (isPm && hour < 12) hour += 12;
            else if (!isPm && hour == 12) hour = 0;

            // ★ DO NOT roll over to next day! The reset time is for TODAY.
            // If it already passed, return it as-is so callers can detect it's in the past.
            var resetTime = DateTime.Today.AddHours(hour);
            return resetTime;
        }

        // Pattern 2: "resets 16:00" (24-hour format)
        match = Regex.Match(text, @"resets?\s+(\d{1,2}):(\d{2})");
        if (match.Success)
        {
            int hour = int.Parse(match.Groups[1].Value);
            int min = int.Parse(match.Groups[2].Value);
            // ★ DO NOT roll over to next day!
            var resetTime = DateTime.Today.AddHours(hour).AddMinutes(min);
            return resetTime;
        }

        return null;
    }

    /// <summary>
    /// Get the parsed reset time from a rate_limit status display text.
    /// Extracts "HH:mm" from "한도 초과 — 16:00에 리셋".
    /// Returns null if not parseable.
    /// </summary>
    static DateTime? GetResetTimeFromDisplayText(string? displayText)
    {
        if (string.IsNullOrEmpty(displayText)) return null;

        var match = Regex.Match(displayText, @"(\d{1,2}:\d{2})에 리셋");
        if (match.Success && TimeOnly.TryParse(match.Groups[1].Value, out var time))
        {
            // ★ DO NOT roll over to next day — return today's time so callers can detect it's past
            var resetDt = DateTime.Today.Add(time.ToTimeSpan());
            return resetDt;
        }
        return null;
    }
}
