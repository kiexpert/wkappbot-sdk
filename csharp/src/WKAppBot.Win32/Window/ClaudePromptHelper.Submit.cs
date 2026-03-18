using System.Drawing;
using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

public sealed partial class ClaudePromptHelper
{
    /// <summary>
    /// Try to submit the prompt focuslessly using multiple strategies:
    ///   1. UIA: Invoke pattern on Submit/Send button (most reliable!)
    ///   2. MSAA: Walk tree to find Send button → accDoDefaultAction
    ///   3. PostMessage VK_RETURN to Chrome renderer hwnd
    ///   4. Fallback: brief focus steal for Enter key
    /// </summary>
    /// <summary>
    /// Unified submit logic: find "제출" button → check state → invoke → verify → retry.
    /// Returns true only if submit is VERIFIED (button disappeared or "중단" appeared).
    /// </summary>
    private bool TryFocuslessSubmit(PromptInfo prompt, AutomationElement turnForm)
    {
        Thread.Sleep(500); // Wait for UI to settle + React to detect DOM change from put_accValue

        // === Strategy 1: UIA Invoke with verify (fully focusless) ===
        if (TrySubmitWithVerify(turnForm, "UIA"))
            return true;

        // === Strategy 2: MSAA accDoDefaultAction ===
        if (TryMsaaSubmit(prompt))
        {
            Console.WriteLine("  [PROMPT] MSAA submit fired, verifying...");
            if (VerifySubmitSuccess(turnForm))
            {
                Console.WriteLine("  [PROMPT] Submitted via MSAA (verified, focusless!)");
                return true;
            }
            Console.WriteLine("  [PROMPT] MSAA submit fired but NOT verified");
        }

        // === Strategy 3: PostMessage VK_RETURN ===
        if (TryPostMessageEnter(prompt))
        {
            Console.WriteLine("  [PROMPT] PostMessage VK_RETURN fired, verifying...");
            if (VerifySubmitSuccess(turnForm))
            {
                Console.WriteLine("  [PROMPT] Submitted via PostMessage VK_RETURN (verified, focusless!)");
                return true;
            }
            Console.WriteLine("  [PROMPT] PostMessage submit fired but NOT verified");
        }

        // === Strategy 4: brief focus + nudge React + Enter ===
        // put_accValue sets DOM text but React may not see it (no input event fired).
        // Brief focus + Space+Backspace triggers React's onChange, then Enter submits.
        if (!AllowFocusSteal)
        {
            Console.WriteLine("  [PROMPT] Focusless-only mode: focusless submit failed, focus steal not allowed");
            return false;
        }
        Console.WriteLine("  [PROMPT] Focusless strategies exhausted, trying brief focus + React nudge...");
        var prevFg = NativeMethods.GetForegroundWindow();
        NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
        Thread.Sleep(200);

        // Nudge React: End → Space → Backspace (triggers input event without changing text)
        KeyboardInput.PressKey("end");
        Thread.Sleep(50);
        KeyboardInput.PressKey("space");
        Thread.Sleep(50);
        KeyboardInput.PressKey("backspace");
        Thread.Sleep(200); // Let React process the input event

        // Now submit with Enter (retry 3 times)
        for (int retry = 0; retry < 3; retry++)
        {
            KeyboardInput.PressKey("enter");
            Thread.Sleep(500);

            // After each Enter, check if submit succeeded
            try
            {
                var checkResult = CheckSubmitButton(turnForm);
                if (checkResult == SubmitButtonState.Gone || checkResult == SubmitButtonState.StopAppeared)
                {
                    Console.WriteLine($"  [PROMPT] Submitted via focus+Enter (retry {retry + 1}, verified!)");
                    RestoreFocus(prevFg, prompt.WindowHandle);
                    return true;
                }
            }
            catch { }
        }

        RestoreFocus(prevFg, prompt.WindowHandle);
        Console.WriteLine("  [PROMPT] Submit FAILED (focus+Enter, 3x retry, unverified)");
        return false; // 3x Enter failed verification — likely rate-limited or UI blocked
    }

    /// <summary>
    /// Find submit button, check its enabled state, invoke, verify, retry up to 3 times.
    /// Returns true only if submit is VERIFIED (button gone or "중단" appeared).
    /// </summary>
    private bool TrySubmitWithVerify(AutomationElement turnForm, string tag)
    {
        for (int attempt = 0; attempt < 3; attempt++)
        {
            if (attempt > 0) Thread.Sleep(500 * attempt);
            try
            {
                var btn = FindSubmitButton(turnForm);
                if (btn == null)
                {
                    if (attempt < 2) Console.WriteLine($"  [PROMPT] [{tag}] Submit button not found (attempt {attempt + 1}/3)");
                    continue;
                }

                // Check if button is enabled/invokable
                if (!btn.Patterns.Invoke.IsSupported)
                {
                    Console.WriteLine($"  [PROMPT] [{tag}] Button \"{btn.Name}\" found but Invoke not supported");
                    continue;
                }
                if (!btn.IsEnabled)
                {
                    Console.WriteLine($"  [PROMPT] [{tag}] Button \"{btn.Name}\" found but DISABLED (React may not see text yet)");
                    Thread.Sleep(300); // Extra wait for React
                    continue;
                }

                // Invoke the button
                btn.Patterns.Invoke.Pattern.Invoke();
                Console.WriteLine($"  [PROMPT] [{tag}] Invoked \"{btn.Name}\" (attempt {attempt + 1})");

                // Verify: wait up to 2 seconds for submit to take effect
                for (int check = 0; check < 4; check++)
                {
                    Thread.Sleep(500);
                    var state = CheckSubmitButton(turnForm);
                    if (state == SubmitButtonState.Gone || state == SubmitButtonState.StopAppeared)
                    {
                        Console.WriteLine($"  [PROMPT] [{tag}] Submit VERIFIED ({state}, attempt {attempt + 1}, fully focusless!)");
                        return true;
                    }
                }

                // Button still visible after 2s — submit likely failed, re-press
                Console.WriteLine($"  [PROMPT] [{tag}] Submit NOT verified after invoke (attempt {attempt + 1}), retrying...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [PROMPT] [{tag}] Error (attempt {attempt + 1}): {ex.Message}");
            }
        }

        Console.WriteLine($"  [PROMPT] [{tag}] All 3 attempts exhausted without verification");
        return false;
    }

    private enum SubmitButtonState { Visible, Gone, StopAppeared }

    /// <summary>
    /// Check if submit succeeded by examining button state.
    /// Gone = button disappeared (submit consumed), StopAppeared = Claude is processing.
    /// </summary>
    private SubmitButtonState CheckSubmitButton(AutomationElement turnForm)
    {
        try
        {
            var elements = turnForm.FindAllDescendants();
            bool submitVisible = false;
            foreach (var el in elements)
            {
                if (el.ControlType != ControlType.Button) continue;
                var name = (el.Name ?? "").ToLowerInvariant();
                if (name.Contains("중단") || name.Contains("stop"))
                    return SubmitButtonState.StopAppeared;
                if (name.Contains("제출") || name.Contains("submit") || name.Contains("send") || name.Contains("전송"))
                    submitVisible = true;
            }
            return submitVisible ? SubmitButtonState.Visible : SubmitButtonState.Gone;
        }
        catch { return SubmitButtonState.Gone; } // Element stale → likely submitted
    }

    /// <summary>
    /// Find the submit/send button in turn-form descendants.
    /// </summary>
    private AutomationElement? FindSubmitButton(AutomationElement turnForm)
    {
        try
        {
            foreach (var el in turnForm.FindAllDescendants())
            {
                if (el.ControlType != ControlType.Button) continue;
                var name = (el.Name ?? "").ToLowerInvariant();
                var aid = (el.AutomationId ?? "").ToLowerInvariant();
                if (name.Contains("중단") || name.Contains("stop") || name.Contains("cancel")) continue;
                if (name.Contains("send") || name.Contains("submit") || name.Contains("전송") ||
                    name.Contains("제출") || aid.Contains("send") || aid.Contains("submit"))
                    return el;
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Verify submit success: wait up to 2s for button to disappear or "중단" to appear.
    /// </summary>
    public bool VerifySubmitAccepted(PromptInfo prompt, int timeoutMs = 1500)
    {
        try
        {
            var root = _automation.FromHandle(prompt.WindowHandle);
            if (root == null) return false;

            var turnForm = root.FindFirstDescendant(
                new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
            if (turnForm == null) return false;

            var checks = Math.Max(1, timeoutMs / 250);
            for (int i = 0; i < checks; i++)
            {
                Thread.Sleep(250);
                var state = CheckSubmitButton(turnForm);
                if (state == SubmitButtonState.Gone || state == SubmitButtonState.StopAppeared)
                    return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    private bool VerifySubmitSuccess(AutomationElement turnForm)
    {
        for (int i = 0; i < 4; i++)
        {
            Thread.Sleep(500);
            var state = CheckSubmitButton(turnForm);
            if (state == SubmitButtonState.Gone || state == SubmitButtonState.StopAppeared)
                return true;
        }
        return false;
    }

    private void RestoreFocus(IntPtr prevFg, IntPtr promptHwnd)
    {
        if (prevFg != IntPtr.Zero && prevFg != promptHwnd)
        {
            Thread.Sleep(200);
            NativeMethods.SmartSetForegroundWindow(prevFg);
            Console.WriteLine("  [PROMPT] Focus restored");
        }
    }

    /// <summary>
    /// Try to submit via MSAA: find Send button in MSAA tree → accDoDefaultAction.
    /// Note: accDoDefaultAction("활성화") on contentEditable only activates/focuses the element,
    /// it does NOT submit. So we must find the actual Send button.
    /// </summary>
    private bool TryMsaaSubmit(PromptInfo prompt)
    {
        try
        {
            var centerX = prompt.PromptRect.X + prompt.PromptRect.Width / 2;
            var centerY = prompt.PromptRect.Y + prompt.PromptRect.Height / 2;
            var pt = new Native.POINT(centerX, centerY);

            int hr = AccessibleObjectFromPoint(pt, out object? acc, out object? _childId);
            if (hr != 0 || acc == null) return false;

            try
            {
                if (acc is not IAccessibleVtbl selfV) return false;

                // Navigate up to turn-form level (grandparent → great-grandparent)
                // and search the MSAA tree for a Send/Submit button
                selfV.get_accParent(out object? parentObj);
                if (parentObj is not IAccessibleVtbl parentV) return false;

                // Walk up several levels to cover the turn-form area
                IAccessibleVtbl searchRoot = parentV;
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        searchRoot.get_accParent(out object? upper);
                        if (upper is IAccessibleVtbl upperV)
                            searchRoot = upperV;
                        else
                            break;
                    }
                    catch { break; }
                }

                Console.WriteLine("  [PROMPT] MSAA submit: Searching for Send button in MSAA tree...");
                if (TryFindAndClickSubmitButton(searchRoot, 0, 8))
                    return true;

                Console.WriteLine("  [PROMPT] MSAA submit: No Send button found in MSAA tree");
                return false;
            }
            finally
            {
                try { Marshal.ReleaseComObject(acc); } catch { }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] MSAA submit error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Recursively search MSAA tree for a button element and trigger its default action.
    /// Looks for ROLE_SYSTEM_PUSHBUTTON(43) or ROLE_SYSTEM_BUTTONDROPDOWN(56).
    /// Also accepts elements with "press"/"누르기" action but NOT generic "상위 개체 클릭".
    /// Skips stop/cancel/중단 buttons.
    /// </summary>
    private bool TryFindAndClickSubmitButton(IAccessibleVtbl node, int depth, int maxDepth)
    {
        if (depth > maxDepth) return false;

        try
        {
            node.get_accRole(0, out object? roleObj);
            int role = roleObj is int r ? r : -1;

            node.get_accName(0, out string? name);
            string nameLower = (name ?? "").ToLowerInvariant();

            string? action = null;
            try { node.get_accDefaultAction(0, out action); } catch { }
            string actionLower = (action ?? "").ToLowerInvariant();

            // ROLE_SYSTEM_PUSHBUTTON=43, ROLE_SYSTEM_BUTTONDROPDOWN=56, ROLE_SYSTEM_LINK=30
            bool isButtonRole = role == 43 || role == 56 || role == 30;
            // Accept press/click actions but NOT "상위 개체 클릭" (generic parent click = false positive)
            bool hasClickAction = (actionLower.Contains("press") || actionLower.Contains("누르기")) &&
                                  !actionLower.Contains("상위");
            // For "클릭" action, only accept actual button roles (not GROUPING=20)
            if (!hasClickAction && isButtonRole && actionLower.Contains("클릭"))
                hasClickAction = true;

            if (isButtonRole || hasClickAction)
            {
                // Skip stop/cancel/중단/collapse/expand/menu/model selector buttons
                if (nameLower.Contains("중단") || nameLower.Contains("stop") || nameLower.Contains("cancel") ||
                    nameLower.Contains("collapse") || nameLower.Contains("expand") ||
                    nameLower.Contains("접기") || nameLower.Contains("펼치기") ||
                    nameLower.Contains("메뉴") || nameLower.Contains("menu") ||
                    nameLower.Contains("opus") || nameLower.Contains("sonnet") || nameLower.Contains("haiku") ||
                    nameLower.Contains("권한") || nameLower.Contains("permission"))
                {
                    // Don't return false — continue to children
                }
                else
                {
                    Console.WriteLine($"  [PROMPT] MSAA button: \"{name}\" action=\"{action}\" role={role}(0x{role:X2}) depth={depth}");

                    if (action != null)
                    {
                        int hr = node.accDoDefaultAction(0);
                        Console.WriteLine($"  [PROMPT] MSAA submit: accDoDefaultAction on \"{name}\" hr=0x{hr:X8}");
                        if (hr == 0) return true;
                    }
                }
            }

            // Recurse into children
            node.get_accChildCount(out int childCount);
            for (int i = 1; i <= Math.Min(childCount, 30); i++)
            {
                try
                {
                    int hr = node.get_accChild(i, out object? child);
                    if (hr == 0 && child is IAccessibleVtbl childV)
                    {
                        if (TryFindAndClickSubmitButton(childV, depth + 1, maxDepth))
                            return true;
                    }
                }
                catch { }
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Try to submit by posting VK_RETURN to the Chrome renderer child window.
    /// Chrome_RenderWidgetHostHWND is the actual input handler inside the Electron window.
    /// </summary>
    private bool TryPostMessageEnter(PromptInfo prompt)
    {
        try
        {
            var rendererHwnd = GetRendererHwnd(prompt.WindowHandle);

            if (rendererHwnd == IntPtr.Zero)
            {
                Console.WriteLine("  [PROMPT] PostMessage: Chrome_RenderWidgetHostHWND not found");
                return false;
            }

            Console.WriteLine($"  [PROMPT] PostMessage: Found renderer hwnd=0x{rendererHwnd:X}");

            // Post WM_KEYDOWN + WM_KEYUP for VK_RETURN
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;
            const int VK_RETURN = 0x0D;

            // lParam for VK_RETURN: repeat=1, scancode=0x1C, extended=0
            uint scanCode = NativeMethods.MapVirtualKeyW((uint)VK_RETURN, 0);
            IntPtr lParamDown = (IntPtr)((scanCode << 16) | 1); // repeat=1
            IntPtr lParamUp = (IntPtr)((scanCode << 16) | 1 | (1 << 30) | (1 << 31)); // prev=down, transition=release

            bool ok1 = NativeMethods.PostMessageW(rendererHwnd, WM_KEYDOWN, (IntPtr)VK_RETURN, lParamDown);
            Thread.Sleep(30);
            bool ok2 = NativeMethods.PostMessageW(rendererHwnd, WM_KEYUP, (IntPtr)VK_RETURN, lParamUp);

            Console.WriteLine($"  [PROMPT] PostMessage: VK_RETURN down={ok1} up={ok2}");

            if (ok1 && ok2)
            {
                Thread.Sleep(200); // Wait for submit to register
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] PostMessage error: {ex.Message}");
            return false;
        }
    }

    public SubmitState ProbeSubmitState(PromptInfo prompt)
    {
        try
        {
            var root = _automation.FromHandle(prompt.WindowHandle);
            if (root == null) return new SubmitState(false, false, false, "");

            var turnForm = root.FindFirstDescendant(
                new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
            if (turnForm == null) return new SubmitState(false, false, false, "");

            var btn = FindSubmitButton(turnForm);
            if (btn == null) return new SubmitState(true, false, false, "");

            return new SubmitState(true, true, btn.IsEnabled, btn.Name ?? "");
        }
        catch
        {
            return new SubmitState(false, false, false, "");
        }
    }

    public bool SubmitExistingInput(PromptInfo prompt)
    {
        try
        {
            var root = _automation.FromHandle(prompt.WindowHandle);
            var turnForm = root?.FindFirstDescendant(
                new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));

            if (turnForm != null)
                return TryFocuslessSubmit(prompt, turnForm);

            if (!AllowFocusSteal) return false;

            var prevForeground = NativeMethods.GetForegroundWindow();
            NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
            Thread.Sleep(120);
            KeyboardInput.PressKey("enter");
            Thread.Sleep(250);
            if (prevForeground != IntPtr.Zero && prevForeground != prompt.WindowHandle)
            {
                Thread.Sleep(120);
                NativeMethods.SmartSetForegroundWindow(prevForeground);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
}
