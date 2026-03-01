using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

/// <summary>
/// Find and interact with the Claude Code prompt input field.
/// Supports: Claude Desktop (Electron), VS Code Claude Code Extension.
///
/// Strategy:
///   1. Find the parent process of wkappbot.exe (claude.exe or code.exe)
///   2. Walk up to the main window of that process
///   3. Find the prompt input via UIA: aid="turn-form" → child Group "입력하세요"
///   4. Insert text via MSAA put_accValue (focusless!)
///   5. Submit via UIA Invoke on "제출" button (focusless!) — NO focus steal needed!
///
/// Key Discoveries (2026-02-26):
///   1. Direct MSAA vtable put_accValue() on the contentEditable's parent MSAA element
///      (name="입력하세요", ROLE_SYSTEM_GROUPING=20) returns S_OK and successfully inserts text,
///      even though FlaUI's UIA LegacyIAccessible.SetValue returns E_NOTIMPL for the same element.
///      The difference: direct MSAA vtable call bypasses the UIA-to-MSAA proxy bridge.
///   2. The submit button in Claude Desktop is named "제출" (Korean for "submit") with
///      UIA Invoke pattern. accDoDefaultAction("활성화") on contentEditable is a false positive
///      — it only activates/focuses the element, doesn't submit.
///   3. MSAA tree walk pitfall: GROUPING(role=20) with action "상위 개체 클릭" is NOT a button!
/// </summary>
public sealed class ClaudePromptHelper : IDisposable
{
    private readonly UIA3Automation _automation;

    public ClaudePromptHelper()
    {
        _automation = new UIA3Automation();
    }

    /// <summary>
    /// Result of a prompt search.
    /// </summary>
    public record PromptInfo(
        IntPtr WindowHandle,
        string WindowTitle,
        string ProcessName,
        Rectangle PromptRect,
        string HostType // "claude-desktop" | "vscode" | "unknown"
    );

    /// <summary>
    /// Find the Claude Code prompt input by walking the process tree.
    /// Strategy: current process → parent → grandparent ... → find Electron/VSCode main window → UIA search.
    /// </summary>
    public PromptInfo? FindPrompt()
    {
        // Strategy 1: Walk up process tree from current wkappbot.exe
        var ancestors = GetAncestorProcesses();
        foreach (var (pid, name) in ancestors)
        {
            Console.WriteLine($"  [PROMPT] Checking ancestor: {name} (PID={pid})");

            if (name.Equals("claude", StringComparison.OrdinalIgnoreCase))
            {
                // Claude Desktop (Electron app)
                var result = FindClaudeDesktopPrompt(pid);
                if (result != null) return result;
            }
            else if (name.Equals("code", StringComparison.OrdinalIgnoreCase) ||
                     name.Equals("code - insiders", StringComparison.OrdinalIgnoreCase))
            {
                // VS Code with Claude Code extension
                var result = FindVSCodePrompt(pid);
                if (result != null) return result;
            }
        }

        // Strategy 2: Enumerate all visible windows from claude.exe processes
        Console.WriteLine("  [PROMPT] Ancestor walk failed, scanning all claude.exe windows...");
        foreach (var proc in Process.GetProcessesByName("claude"))
        {
            var result = FindClaudeDesktopPrompt(proc.Id);
            if (result != null) return result;
        }

        // Strategy 3: Enumerate all VS Code windows
        Console.WriteLine("  [PROMPT] Scanning VS Code windows...");
        foreach (var proc in Process.GetProcessesByName("Code"))
        {
            var result = FindVSCodePrompt(proc.Id);
            if (result != null) return result;
        }

        return null;
    }

    /// <summary>
    /// Type text into the Claude prompt and submit with Enter.
    /// Strategy (2-tier):
    ///   1. Focusless: MSAA put_accValue via direct vtable (no focus steal!)
    ///   2. Fallback: SetForegroundWindow → Click → Paste → Enter → Restore previous foreground
    /// </summary>
    public bool TypeAndSubmit(PromptInfo prompt, string text)
    {
        // === Strategy 1: Try fully focusless input ===
        if (TryFocuslessInput(prompt, text))
            return true;

        // === Strategy 2: Focus-stealing with auto-restore (minimal disruption) ===
        var prevForeground = NativeMethods.GetForegroundWindow();
        Console.WriteLine($"  [PROMPT] Activating: {prompt.WindowTitle} (will restore focus)");
        NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
        Thread.Sleep(200);

        // Click the prompt area to focus the contentEditable div
        var centerX = prompt.PromptRect.X + prompt.PromptRect.Width / 2;
        var centerY = prompt.PromptRect.Y + prompt.PromptRect.Height / 2;
        MouseInput.Click(centerX, centerY);
        Thread.Sleep(150);

        // Clear existing text
        KeyboardInput.Hotkey(new[] { "ctrl", "a" });
        Thread.Sleep(30);
        KeyboardInput.PressKey("delete");
        Thread.Sleep(30);

        // Paste via clipboard (fast, supports multiline + unicode)
        Console.WriteLine($"  [PROMPT] Pasting ({text.Length} chars)");
        SetClipboardText(text);
        Thread.Sleep(50);
        KeyboardInput.Hotkey(new[] { "ctrl", "v" });
        Thread.Sleep(200);

        // Submit with Enter
        KeyboardInput.PressKey("enter");
        Console.WriteLine("  [PROMPT] Submitted");

        // Restore previous foreground window (minimize user disruption)
        if (prevForeground != IntPtr.Zero && prevForeground != prompt.WindowHandle)
        {
            Thread.Sleep(300); // Brief pause to let Claude register the submit
            NativeMethods.SmartSetForegroundWindow(prevForeground);
            Console.WriteLine("  [PROMPT] Focus restored to previous window");
        }

        return true;
    }

    /// <summary>
    /// Try to input text into the Claude prompt WITHOUT stealing focus.
    /// Priority: MSAA put_accValue (direct vtable) → LegacyIAccessible.SetValue (UIA bridge).
    /// Returns true if text was successfully inserted AND submitted.
    /// </summary>
    private bool TryFocuslessInput(PromptInfo prompt, string text)
    {
        try
        {
            // === IA2 attempt: IAccessibleEditableText.insertText (the dream!) ===
            if (TryIA2InsertText(prompt, text))
            {
                // Text inserted focuslessly! Now submit.
                Console.WriteLine("  [PROMPT] IA2 text insertion succeeded! Submitting...");
                var root2 = _automation.FromHandle(prompt.WindowHandle);
                var tf2 = root2?.FindFirstDescendant(
                    new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
                if (tf2 != null)
                    return TryFocuslessSubmit(prompt, tf2);
                // Fallback: brief focus for Enter
                var prevFg = NativeMethods.GetForegroundWindow();
                NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
                Thread.Sleep(100);
                KeyboardInput.PressKey("enter");
                Thread.Sleep(100);
                if (prevFg != IntPtr.Zero && prevFg != prompt.WindowHandle)
                    NativeMethods.SmartSetForegroundWindow(prevFg);
                Console.WriteLine("  [PROMPT] Submitted (IA2 text + brief focus Enter)");
                return true;
            }

            // === LegacyIAccessible attempt (known to fail on Electron contentEditable) ===
            Console.WriteLine("  [PROMPT] Trying focusless input (LegacyIAccessible)...");
            var root = _automation.FromHandle(prompt.WindowHandle);
            if (root == null) return false;

            var turnForm = root.FindFirstDescendant(
                new PropertyCondition(
                    _automation.PropertyLibrary.Element.AutomationId,
                    "turn-form"));
            if (turnForm == null)
            {
                Console.WriteLine("  [PROMPT] Focusless: turn-form not found");
                return false;
            }

            // Find the input group (contentEditable div)
            var inputGroup = turnForm.FindFirstChild(
                new PropertyCondition(
                    _automation.PropertyLibrary.Element.ControlType,
                    ControlType.Group));
            if (inputGroup == null)
            {
                var children = turnForm.FindAllChildren();
                inputGroup = children.FirstOrDefault(c =>
                    c.ControlType == ControlType.Group &&
                    c.BoundingRectangle.Width > 100);
            }
            if (inputGroup == null)
            {
                Console.WriteLine("  [PROMPT] Focusless: inputGroup not found");
                return false;
            }

            // Try LegacyIAccessible.SetValue
            var legacy = inputGroup.Patterns.LegacyIAccessible;
            if (!legacy.IsSupported)
            {
                Console.WriteLine("  [PROMPT] Focusless: LegacyIA not supported");
                return false;
            }

            // Method 1: LegacyIAccessible.SetValue
            try
            {
                legacy.Pattern.SetValue(text);
                Console.WriteLine($"  [PROMPT] LegacyIA.SetValue succeeded! ({text.Length} chars, focusless!)");
                // If we get here, text was set — try to submit and return
                return TryFocuslessSubmit(prompt, turnForm);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [PROMPT] LegacyIA.SetValue failed: {ex.Message}");
            }

            // NOTE: LegacyIAccessible.SetValue returns E_NOTIMPL for Electron contentEditable
            // because FlaUI's UIA→MSAA bridge adds extra validation.
            // Direct MSAA vtable put_accValue works! (handled above via TryIA2InsertText)
            // PostMessage WM_PASTE also doesn't work (Chrome ignores when not foreground).

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] Focusless error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Try to submit the prompt focuslessly using multiple strategies:
    ///   1. UIA: Invoke pattern on Submit/Send button (most reliable!)
    ///   2. MSAA: Walk tree to find Send button → accDoDefaultAction
    ///   3. PostMessage VK_RETURN to Chrome renderer hwnd
    ///   4. Fallback: brief focus steal for Enter key
    /// </summary>
    private bool TryFocuslessSubmit(PromptInfo prompt, AutomationElement turnForm)
    {
        Thread.Sleep(200); // Wait a bit for UI to settle after text insertion

        // === Strategy 1: UIA Invoke on Submit/Send button (best — fully focusless!) ===
        // Retry up to 3 times with increasing delay (button may not appear immediately)
        for (int attempt = 0; attempt < 3; attempt++)
        {
            if (attempt > 0) Thread.Sleep(300 * attempt);
            try
            {
                var allElements = turnForm.FindAllDescendants();
                foreach (var el in allElements)
                {
                    if (el.ControlType != ControlType.Button) continue;
                    var name = (el.Name ?? "").ToLowerInvariant();
                    var aid = (el.AutomationId ?? "").ToLowerInvariant();
                    // Skip stop/cancel/중단 buttons
                    if (name.Contains("중단") || name.Contains("stop") || name.Contains("cancel")) continue;
                    // Match submit/send buttons (Claude Desktop uses "제출" = Korean for "submit")
                    if (name.Contains("send") || name.Contains("submit") || name.Contains("전송") ||
                        name.Contains("제출") ||
                        aid.Contains("send") || aid.Contains("submit"))
                    {
                        if (el.Patterns.Invoke.IsSupported)
                        {
                            el.Patterns.Invoke.Pattern.Invoke();
                            Console.WriteLine($"  [PROMPT] Submitted via UIA Invoke on \"{el.Name}\" (attempt {attempt + 1}, fully focusless!)");

                            // Retry 2 more times at 1s intervals (user: "가끔 실패하는데 두번 더 누르면")
                            // Safe: if first Invoke worked, button disappears → retry no-ops silently
                            for (int extra = 1; extra <= 2; extra++)
                            {
                                Thread.Sleep(1000);
                                try
                                {
                                    // Re-find button (UIA element may become stale)
                                    var freshElements = turnForm.FindAllDescendants();
                                    bool found = false;
                                    foreach (var fe in freshElements)
                                    {
                                        if (fe.ControlType != ControlType.Button) continue;
                                        var fn = (fe.Name ?? "").ToLowerInvariant();
                                        if (fn.Contains("중단") || fn.Contains("stop")) break; // Submit already worked → "중단" appeared
                                        if ((fn.Contains("send") || fn.Contains("submit") || fn.Contains("전송") || fn.Contains("제출")) &&
                                            fe.Patterns.Invoke.IsSupported)
                                        {
                                            fe.Patterns.Invoke.Pattern.Invoke();
                                            Console.WriteLine($"  [PROMPT] Submit re-pressed ({extra}/2, button still visible)");
                                            found = true;
                                            break;
                                        }
                                    }
                                    if (!found) break; // Button gone or "중단" appeared → submit succeeded
                                }
                                catch { break; } // Element stale → submit likely succeeded
                            }
                            return true;
                        }
                    }
                }
                if (attempt < 2) Console.WriteLine($"  [PROMPT] Submit button not found (attempt {attempt + 1}/3), retrying...");
            }
            catch { }
        }

        // === Strategy 2: MSAA accDoDefaultAction on Send button ===
        if (TryMsaaSubmit(prompt))
        {
            Console.WriteLine("  [PROMPT] Submitted via MSAA (fully focusless!)");
            return true;
        }

        // === Strategy 3: PostMessage VK_RETURN to Chrome renderer hwnd ===
        if (TryPostMessageEnter(prompt))
        {
            Console.WriteLine("  [PROMPT] Submitted via PostMessage VK_RETURN (focusless!)");
            return true;
        }

        // === Strategy 4: brief focus steal for Enter key only (retry 3 times) ===
        Console.WriteLine("  [PROMPT] Text set focuslessly! Brief focus for Enter key (3x)...");
        var prevFg = NativeMethods.GetForegroundWindow();
        NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
        Thread.Sleep(200);
        for (int retry = 0; retry < 3; retry++)
        {
            KeyboardInput.PressKey("enter");
            Thread.Sleep(300);
        }
        if (prevFg != IntPtr.Zero && prevFg != prompt.WindowHandle)
        {
            NativeMethods.SmartSetForegroundWindow(prevFg);
            Console.WriteLine("  [PROMPT] Focus restored after Enter");
        }
        Console.WriteLine("  [PROMPT] Submitted (text=focusless, Enter=brief focus, 3x retry)");
        return true;
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
            // Find Chrome_RenderWidgetHostHWND child window
            var rendererHwnd = NativeMethods.FindWindowExW(
                prompt.WindowHandle, IntPtr.Zero,
                "Chrome_RenderWidgetHostHWND", null);

            if (rendererHwnd == IntPtr.Zero)
            {
                // Try intermediate child: Chrome_WidgetWin_1 → Chrome_RenderWidgetHostHWND
                var widgetWin = NativeMethods.FindWindowExW(
                    prompt.WindowHandle, IntPtr.Zero,
                    "Chrome_WidgetWin_1", null);
                if (widgetWin != IntPtr.Zero)
                {
                    rendererHwnd = NativeMethods.FindWindowExW(
                        widgetWin, IntPtr.Zero,
                        "Chrome_RenderWidgetHostHWND", null);
                }
            }

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

    /// <summary>
    /// Type text into the prompt WITHOUT submitting (no Enter).
    /// For testing/dry-run: verify text insertion works.
    /// Tries focusless first, then focus-stealing with restore.
    /// </summary>
    public bool TypeWithoutSubmit(PromptInfo prompt, string text)
    {
        // Try IA2 focusless text insertion first
        if (TryIA2InsertText(prompt, text))
        {
            Console.WriteLine($"  [PROMPT] IA2 insertText succeeded! ({text.Length} chars, focusless, no submit)");
            return true;
        }

        // Try focusless text insertion via LegacyIAccessible
        try
        {
            Console.WriteLine("  [PROMPT] TypeWithoutSubmit: trying LegacyIA focusless...");
            var root = _automation.FromHandle(prompt.WindowHandle);
            if (root != null)
            {
                var turnForm = root.FindFirstDescendant(
                    new PropertyCondition(
                        _automation.PropertyLibrary.Element.AutomationId,
                        "turn-form"));
                if (turnForm != null)
                {
                    var inputGroup = turnForm.FindFirstChild(
                        new PropertyCondition(
                            _automation.PropertyLibrary.Element.ControlType,
                            ControlType.Group));
                    if (inputGroup == null)
                    {
                        var children = turnForm.FindAllChildren();
                        inputGroup = children.FirstOrDefault(c =>
                            c.ControlType == ControlType.Group &&
                            c.BoundingRectangle.Width > 100);
                    }

                    if (inputGroup != null)
                    {
                        var legacy = inputGroup.Patterns.LegacyIAccessible;
                        if (legacy.IsSupported)
                        {
                            try
                            {
                                legacy.Pattern.SetValue(text);
                                Console.WriteLine($"  [PROMPT] LegacyIA.SetValue succeeded! ({text.Length} chars, focusless, no submit)");
                                return true;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"  [PROMPT] LegacyIA.SetValue failed: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("  [PROMPT] LegacyIA not supported on inputGroup");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] Focusless probe error: {ex.Message}");
        }

        // Fallback: focus-steal to paste text, but no Enter
        Console.WriteLine("  [PROMPT] Falling back to focus-steal paste (no submit)...");
        var prevForeground = NativeMethods.GetForegroundWindow();
        NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
        Thread.Sleep(200);

        var centerX = prompt.PromptRect.X + prompt.PromptRect.Width / 2;
        var centerY = prompt.PromptRect.Y + prompt.PromptRect.Height / 2;
        MouseInput.Click(centerX, centerY);
        Thread.Sleep(150);

        KeyboardInput.Hotkey(new[] { "ctrl", "a" });
        Thread.Sleep(30);
        KeyboardInput.PressKey("delete");
        Thread.Sleep(30);

        Console.WriteLine($"  [PROMPT] Pasting ({text.Length} chars, no submit)");
        SetClipboardText(text);
        Thread.Sleep(50);
        KeyboardInput.Hotkey(new[] { "ctrl", "v" });
        Thread.Sleep(200);

        // NO Enter key — dry run!
        Console.WriteLine("  [PROMPT] Text pasted (no Enter, dry-run)");

        // Restore previous foreground
        if (prevForeground != IntPtr.Zero && prevForeground != prompt.WindowHandle)
        {
            Thread.Sleep(200);
            NativeMethods.SmartSetForegroundWindow(prevForeground);
            Console.WriteLine("  [PROMPT] Focus restored");
        }

        return true;
    }

    /// <summary>
    /// Find prompt in Claude Desktop (Electron) window.
    /// UIA path: Window → ... → Document "Claude" aid="RootWebArea" → Group aid="turn-form" → Group "입력하세요"
    /// </summary>
    private PromptInfo? FindClaudeDesktopPrompt(int processId)
    {
        // Find all visible top-level windows from this PID
        var windows = FindWindowsByProcessId(processId);
        foreach (var hWnd in windows)
        {
            try
            {
                var root = _automation.FromHandle(hWnd);
                if (root == null) continue;

                // Look for the turn-form group (the prompt container)
                var turnForm = root.FindFirstDescendant(
                    new PropertyCondition(
                        _automation.PropertyLibrary.Element.AutomationId,
                        "turn-form"));

                if (turnForm == null) continue;

                // Found the prompt container! Get the input group
                // It's a child Group with name "입력하세요" or similar placeholder
                var inputGroup = turnForm.FindFirstChild(
                    new PropertyCondition(
                        _automation.PropertyLibrary.Element.ControlType,
                        ControlType.Group));

                if (inputGroup == null)
                {
                    // Try finding by contentEditable behavior — the first child Group
                    var children = turnForm.FindAllChildren();
                    inputGroup = children.FirstOrDefault(c =>
                        c.ControlType == ControlType.Group &&
                        c.BoundingRectangle.Width > 100);
                }

                if (inputGroup == null) continue;

                var rect = inputGroup.BoundingRectangle;
                var title = WindowFinder.GetWindowText(hWnd);

                Console.WriteLine($"  [PROMPT] Found Claude Desktop prompt: aid=turn-form at ({rect.X},{rect.Y} {rect.Width}x{rect.Height})");

                return new PromptInfo(
                    hWnd,
                    title,
                    "claude",
                    new Rectangle(rect.X, rect.Y, rect.Width, rect.Height),
                    "claude-desktop"
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [PROMPT] Error checking hWnd {hWnd}: {ex.Message}");
            }
        }
        return null;
    }

    /// <summary>
    /// Find prompt in VS Code with Claude Code extension.
    /// The Claude Code panel in VS Code may have different UIA structure.
    /// </summary>
    private PromptInfo? FindVSCodePrompt(int processId)
    {
        var windows = FindWindowsByProcessId(processId);
        foreach (var hWnd in windows)
        {
            try
            {
                var root = _automation.FromHandle(hWnd);
                if (root == null) continue;

                // VS Code Claude Code panel — look for turn-form or similar
                var turnForm = root.FindFirstDescendant(
                    new PropertyCondition(
                        _automation.PropertyLibrary.Element.AutomationId,
                        "turn-form"));

                if (turnForm != null)
                {
                    var inputGroup = turnForm.FindFirstChild(
                        new PropertyCondition(
                            _automation.PropertyLibrary.Element.ControlType,
                            ControlType.Group));

                    if (inputGroup != null)
                    {
                        var rect = inputGroup.BoundingRectangle;
                        var title = WindowFinder.GetWindowText(hWnd);

                        Console.WriteLine($"  [PROMPT] Found VS Code Claude prompt: aid=turn-form at ({rect.X},{rect.Y} {rect.Width}x{rect.Height})");

                        return new PromptInfo(
                            hWnd,
                            title,
                            "Code",
                            new Rectangle(rect.X, rect.Y, rect.Width, rect.Height),
                            "vscode"
                        );
                    }
                }
            }
            catch { }
        }
        return null;
    }

    /// <summary>
    /// Get ancestor process chain: current → parent → grandparent → ...
    /// Stops when parent is not accessible or reaches PID 0/1.
    /// </summary>
    private static List<(int Pid, string Name)> GetAncestorProcesses()
    {
        var result = new List<(int, string)>();
        try
        {
            var current = Process.GetCurrentProcess();
            var pid = current.Id;

            // Walk up the tree
            for (int i = 0; i < 10; i++) // max 10 levels
            {
                var parentPid = GetParentProcessId(pid);
                if (parentPid <= 0 || parentPid == pid) break;

                try
                {
                    var parent = Process.GetProcessById(parentPid);
                    result.Add((parent.Id, parent.ProcessName));
                    pid = parent.Id;
                }
                catch
                {
                    break;
                }
            }
        }
        catch { }
        return result;
    }

    /// <summary>
    /// Get parent process ID using NtQueryInformationProcess or WMI fallback.
    /// </summary>
    private static int GetParentProcessId(int pid)
    {
        try
        {
            // Use WMI-like approach via Process
            // On Windows, Process has no direct ParentId. Use handle + NtQueryInformationProcess.
            // Simpler: use PerformanceCounter or WMI
            // Simplest portable approach: read /proc on WSL or use dotnet API
            var proc = Process.GetProcessById(pid);

            // .NET 8+ approach - try reflection for ParentId (not available in all frameworks)
            // Fallback: use command-line tool
            var psi = new ProcessStartInfo
            {
                FileName = "wmic",
                Arguments = $"process where processid={pid} get parentprocessid /value",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using var wmic = Process.Start(psi);
            if (wmic == null) return -1;
            var output = wmic.StandardOutput.ReadToEnd();
            wmic.WaitForExit(3000);

            // Parse "ParentProcessId=12345\r\n"
            var match = System.Text.RegularExpressions.Regex.Match(output, @"ParentProcessId=(\d+)");
            if (match.Success)
                return int.Parse(match.Groups[1].Value);
        }
        catch { }
        return -1;
    }

    /// <summary>
    /// Find all visible top-level windows belonging to a process.
    /// </summary>
    private static List<IntPtr> FindWindowsByProcessId(int processId)
    {
        var result = new List<IntPtr>();
        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;

            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            if (pid == (uint)processId)
                result.Add(hWnd);

            return true;
        }, IntPtr.Zero);
        return result;
    }

    /// <summary>
    /// Set clipboard text using STA thread (Win32 clipboard requires STA).
    /// </summary>
    private static void SetClipboardText(string text)
    {
        // Clipboard operations require STA thread
        var thread = new Thread(() =>
        {
            // P/Invoke clipboard: OpenClipboard → EmptyClipboard → SetClipboardData → CloseClipboard
            if (!NativeMethods.OpenClipboard(IntPtr.Zero)) return;
            try
            {
                NativeMethods.EmptyClipboard();
                var hGlobal = System.Runtime.InteropServices.Marshal.StringToHGlobalUni(text);
                NativeMethods.SetClipboardData(13 /* CF_UNICODETEXT */, hGlobal);
                // Don't free hGlobal — clipboard takes ownership
            }
            finally
            {
                NativeMethods.CloseClipboard();
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join(3000);
    }

    // ═══════════════════════════════════════════════════════════════
    // MSAA/IA2 COM Interop for focusless text insertion
    //
    // Discovery (2026-02-26):
    //   Chromium's contentEditable has ROLE_SYSTEM_GROUPING (not ROLE_TEXT),
    //   so IAccessibleEditableText is NOT available. However, direct MSAA vtable
    //   put_accValue() on the parent element (name="입력하세요") works!
    //
    //   Why it works when FlaUI's LegacyIAccessible.SetValue returns E_NOTIMPL:
    //   FlaUI goes through UIA→MSAA proxy bridge which adds validation.
    //   Direct MSAA COM vtable call bypasses the proxy entirely.
    //
    // MSAA tree structure (Claude Desktop prompt):
    //   grandparent: GROUPING, name=null
    //     └─ parent: GROUPING, name="입력하세요"  ← put_accValue target!
    //          └─ self: GROUPING, name=null       ← AccessibleObjectFromPoint hit
    //               ├─ child: GROUPING or TEXT
    //               └─ child: WHITESPACE, name="\n"
    // ═══════════════════════════════════════════════════════════════

    [DllImport("oleacc.dll")]
    private static extern int AccessibleObjectFromPoint(
        Native.POINT pt,
        [MarshalAs(UnmanagedType.Interface)] out object ppacc,
        [MarshalAs(UnmanagedType.Struct)] out object pvarChild);

    // OLE IServiceProvider — used to QueryService for IA2 from IAccessible
    [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IOleServiceProvider
    {
        [PreserveSig]
        int QueryService(ref Guid guidService, ref Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject);
    }

    // IAccessibleEditableText — the IA2 interface for text input
    // vtable order MUST match the IA2 IDL exactly!
    [ComImport, Guid("A59AA09A-7011-4b65-939D-32B1FB5547E3"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAccessibleEditableText
    {
        [PreserveSig]
        int copyText(int startOffset, int endOffset);
        [PreserveSig]
        int deleteText(int startOffset, int endOffset);
        [PreserveSig]
        int insertText(int offset, [MarshalAs(UnmanagedType.BStr)] string text);
        [PreserveSig]
        int cutText(int startOffset, int endOffset);
        [PreserveSig]
        int pasteText(int offset);
        [PreserveSig]
        int replaceText(int startOffset, int endOffset, [MarshalAs(UnmanagedType.BStr)] string text);
        [PreserveSig]
        int setAttributes(int startOffset, int endOffset, [MarshalAs(UnmanagedType.BStr)] string attributes);
    }

    // IAccessible via vtable — for MSAA tree navigation (role probing + child access)
    // Chromium doesn't support IDispatch late binding → must use vtable (InterfaceIsIUnknown)
    // Vtable order: IUnknown(3) + IDispatch(4) + IAccessible(21) = 28 slots
    [ComImport, Guid("618736E0-3C3D-11CF-810C-00AA00389B71"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAccessibleVtbl
    {
        // ── IDispatch (4 slots, never called — consuming vtable positions) ──
        void _QueryInfoCount_slot();
        void _GetTypeInfo_slot();
        void _GetIDsOfNames_slot();
        void _Invoke_slot();

        // ── IAccessible (21 methods) ──
        [PreserveSig]
        int get_accParent([MarshalAs(UnmanagedType.IDispatch)] out object? ppdispParent);
        [PreserveSig]
        int get_accChildCount(out int pcountChildren);
        [PreserveSig]
        int get_accChild([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.IDispatch)] out object? ppdispChild);
        [PreserveSig]
        int get_accName([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszName);
        [PreserveSig]
        int get_accValue([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszValue);
        [PreserveSig]
        int get_accDescription([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszDescription);
        [PreserveSig]
        int get_accRole([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.Struct)] out object? pvarRole);
        [PreserveSig]
        int get_accState([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.Struct)] out object? pvarState);
        [PreserveSig]
        int get_accHelp([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszHelp);
        [PreserveSig]
        int get_accHelpTopic([MarshalAs(UnmanagedType.BStr)] out string? pszHelpFile,
            [MarshalAs(UnmanagedType.Struct)] object varChild, out int pidTopic);
        [PreserveSig]
        int get_accKeyboardShortcut([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszKeyboardShortcut);
        [PreserveSig]
        int get_accFocus([MarshalAs(UnmanagedType.Struct)] out object? pvarChild);
        [PreserveSig]
        int get_accSelection([MarshalAs(UnmanagedType.Struct)] out object? pvarChildren);
        [PreserveSig]
        int get_accDefaultAction([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] out string? pszDefaultAction);
        [PreserveSig]
        int accSelect(int flagsSelect, [MarshalAs(UnmanagedType.Struct)] object varChild);
        [PreserveSig]
        int accLocation(out int pxLeft, out int pyTop, out int pcxWidth, out int pcyHeight,
            [MarshalAs(UnmanagedType.Struct)] object varChild);
        [PreserveSig]
        int accNavigate(int navDir, [MarshalAs(UnmanagedType.Struct)] object varStart,
            [MarshalAs(UnmanagedType.Struct)] out object? pvarEndUpAt);
        [PreserveSig]
        int accHitTest(int xLeft, int yTop, [MarshalAs(UnmanagedType.Struct)] out object? pvarChild);
        [PreserveSig]
        int accDoDefaultAction([MarshalAs(UnmanagedType.Struct)] object varChild);
        [PreserveSig]
        int put_accName([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] string szName);
        [PreserveSig]
        int put_accValue([MarshalAs(UnmanagedType.Struct)] object varChild,
            [MarshalAs(UnmanagedType.BStr)] string szValue);
    }

    private static readonly Guid IID_IAccessible = new("618736E0-3C3D-11CF-810C-00AA00389B71");
    private static readonly Guid IID_IAccessible2 = new("E89F726E-C4F4-4c19-BB19-B647D7FA8478");
    private static readonly Guid IID_IAccessibleEditableText = new("A59AA09A-7011-4b65-939D-32B1FB5547E3");

    /// <summary>
    /// Try to insert text focuslessly using MSAA put_accValue (direct vtable).
    /// Priority 1: put_accValue on parent MSAA element (proven working on Claude Desktop)
    /// Priority 2: IAccessibleEditableText.insertText (IA2, not available for contentEditable)
    /// Strategy: AccessibleObjectFromPoint → navigate to parent → put_accValue
    /// </summary>
    private bool TryIA2InsertText(PromptInfo prompt, string text)
    {
        try
        {
            Console.WriteLine("  [PROMPT] Trying IA2 IAccessibleEditableText.insertText()...");

            var centerX = prompt.PromptRect.X + prompt.PromptRect.Width / 2;
            var centerY = prompt.PromptRect.Y + prompt.PromptRect.Height / 2;
            var pt = new Native.POINT(centerX, centerY);

            int hr = AccessibleObjectFromPoint(pt, out object? acc, out object? childId);
            if (hr != 0 || acc == null)
            {
                Console.WriteLine($"  [PROMPT] IA2: AccessibleObjectFromPoint failed (hr=0x{hr:X8})");
                return false;
            }

            var childIdStr = childId switch
            {
                int id => id == 0 ? "CHILDID_SELF" : $"child={id}",
                _ => $"type={childId?.GetType().Name ?? "null"}"
            };
            Console.WriteLine($"  [PROMPT] IA2: Got MSAA object at ({centerX},{centerY}), {childIdStr}");

            try
            {
                // Probe MSAA role/name/children for diagnostics
                ProbeAccessibleInfo(acc, "  [PROMPT] IA2 [self]");

                // === Priority 1: Try put_accValue on parent (the "입력하세요" contentEditable) ===
                // This works through direct MSAA vtable, bypassing UIA bridge that returns E_NOTIMPL
                if (acc is IAccessibleVtbl selfForParent)
                {
                    selfForParent.get_accParent(out object? parentForValue);
                    if (parentForValue is IAccessibleVtbl parentVtbl)
                    {
                        string? parentName = null;
                        try { parentVtbl.get_accName(0, out parentName); } catch { }
                        Console.WriteLine($"  [PROMPT] IA2: Trying put_accValue on parent (\"{parentName}\")...");
                        try
                        {
                            int pvHr = parentVtbl.put_accValue(0, text);
                            if (pvHr == 0)
                            {
                                Console.WriteLine($"  [PROMPT] IA2: put_accValue SUCCEEDED! ({text.Length} chars, focusless!)");
                                return true;
                            }
                            Console.WriteLine($"  [PROMPT] IA2: put_accValue returned hr=0x{pvHr:X8}");
                        }
                        catch (Exception pvEx)
                        {
                            Console.WriteLine($"  [PROMPT] IA2: put_accValue error: {pvEx.Message}");
                        }

                        // Also try on self
                        try
                        {
                            int pvHr2 = selfForParent.put_accValue(0, text);
                            if (pvHr2 == 0)
                            {
                                Console.WriteLine($"  [PROMPT] IA2: put_accValue on self SUCCEEDED! ({text.Length} chars)");
                                return true;
                            }
                        }
                        catch { }
                    }
                }

                // === Priority 2: Try IAccessibleEditableText ===
                if (TryInsertOnObject(acc, text, "self"))
                    return true;

                // Navigate to parent and try IAccessibleEditableText there
                if (acc is IAccessibleVtbl accV)
                {
                    try
                    {
                        accV.get_accParent(out object? parent);
                        if (parent != null)
                        {
                            ProbeAccessibleInfo(parent, "  [PROMPT] IA2 [parent]");
                            if (TryInsertOnObject(parent, text, "parent"))
                                return true;

                            // Try grandparent too (contentEditable might be 2 levels up)
                            if (parent is IAccessibleVtbl parentV)
                            {
                                try
                                {
                                    parentV.get_accParent(out object? grandparent);
                                    if (grandparent != null)
                                    {
                                        ProbeAccessibleInfo(grandparent, "  [PROMPT] IA2 [grandparent]");
                                        if (TryInsertOnObject(grandparent, text, "grandparent"))
                                            return true;
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  [PROMPT] IA2: Parent navigation error: {ex.Message}");
                    }

                    // Try children of the current element
                    try
                    {
                        accV.get_accChildCount(out int childCount);
                        if (childCount > 0)
                        {
                            Console.WriteLine($"  [PROMPT] IA2: Probing {childCount} children...");
                            for (int i = 1; i <= Math.Min(childCount, 10); i++)
                            {
                                try
                                {
                                    int hr2 = accV.get_accChild(i, out object? child);
                                    if (hr2 == 0 && child != null)
                                    {
                                        ProbeAccessibleInfo(child, $"  [PROMPT] IA2 [child {i}]");
                                        if (TryInsertOnObject(child, text, $"child[{i}]"))
                                            return true;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"  [PROMPT] IA2 [child {i}]: get_accChild hr=0x{hr2:X8} (simple element or null)");
                                    }
                                }
                                catch (Exception cex)
                                {
                                    Console.WriteLine($"  [PROMPT] IA2 [child {i}]: error ({cex.Message})");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  [PROMPT] IA2: Child navigation error: {ex.Message}");
                    }
                }

                // Deep recursive search — walk entire subtree from parent for IAccessibleEditableText
                if (acc is IAccessibleVtbl selfV)
                {
                    selfV.get_accParent(out object? searchRoot);
                    if (searchRoot is IAccessibleVtbl rootV)
                    {
                        rootV.get_accParent(out object? gp);
                        if (gp != null) searchRoot = gp; // start from grandparent for wider coverage
                    }
                    if (searchRoot != null)
                    {
                        Console.WriteLine("  [PROMPT] IA2: Deep recursive search for IAccessibleEditableText...");
                        if (TryInsertRecursive(searchRoot, text, 0, 5))
                            return true;
                    }
                }

                Console.WriteLine("  [PROMPT] IA2: No focusless insertion method found");
                return false;
            }
            finally
            {
                try { Marshal.ReleaseComObject(acc); } catch { }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] IA2 error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Log MSAA role, name, childCount for diagnostics using vtable-bound IAccessible.
    /// </summary>
    private static void ProbeAccessibleInfo(object acc, string prefix)
    {
        if (acc is not IAccessibleVtbl v)
        {
            Console.WriteLine($"{prefix}: not IAccessibleVtbl (type={acc.GetType().Name})");
            return;
        }
        try
        {
            int childCount = 0;
            v.get_accChildCount(out childCount);

            object? role = null;
            try { v.get_accRole(0 /* CHILDID_SELF */, out role); } catch { }

            string? name = null;
            try { v.get_accName(0, out name); } catch { }

            // MSAA role constants: 42=ROLE_SYSTEM_TEXT, 9=ROLE_SYSTEM_CLIENT,
            // 20=ROLE_SYSTEM_GROUPING, 15=ROLE_SYSTEM_DOCUMENT, 14=ROLE_SYSTEM_WINDOW
            var roleStr = role is int r ? $"role={r} (0x{r:X2})" : $"role={role}";
            Console.WriteLine($"{prefix}: {roleStr}, name=\"{name ?? "(null)"}\", children={childCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{prefix}: probe failed ({ex.Message})");
        }
    }

    /// <summary>
    /// Recursively walk the MSAA tree to find any element supporting IAccessibleEditableText.
    /// </summary>
    private bool TryInsertRecursive(object acc, string text, int depth, int maxDepth)
    {
        if (depth > maxDepth) return false;
        var indent = new string(' ', depth * 2);

        if (acc is IAccessibleVtbl v)
        {
            // Check role
            object? role = null;
            string? name = null;
            try { v.get_accRole(0, out role); } catch { }
            try { v.get_accName(0, out name); } catch { }
            var roleInt = role is int r ? r : -1;

            // Log only interesting roles (not GROUPING 20)
            if (roleInt != 20)
                Console.WriteLine($"  [PROMPT] IA2 {indent}depth={depth}: role={roleInt} (0x{roleInt:X2}), name=\"{name ?? ""}\"");

            // Try IAccessibleEditableText on this element
            if (TryInsertOnObject(acc, text, $"depth{depth}"))
                return true;

            // Navigate to children
            v.get_accChildCount(out int childCount);
            for (int i = 1; i <= Math.Min(childCount, 20); i++)
            {
                try
                {
                    int hr = v.get_accChild(i, out object? child);
                    if (hr == 0 && child != null)
                    {
                        if (TryInsertRecursive(child, text, depth + 1, maxDepth))
                            return true;
                    }
                }
                catch { }
            }
        }
        return false;
    }

    /// <summary>
    /// Try all approaches to get IAccessibleEditableText and call insertText on a COM object.
    /// Tries: Direct QI → QueryService(IA2)→QI → QueryService(IAccessibleEditableText)
    /// </summary>
    private bool TryInsertOnObject(object acc, string text, string label)
    {
        int hr;

        // Approach 1: Direct QI for IAccessibleEditableText
        if (acc is IAccessibleEditableText directEdit)
        {
            Console.WriteLine($"  [PROMPT] IA2 [{label}]: Direct QI succeeded! Calling insertText...");
            hr = directEdit.insertText(0, text);
            if (hr == 0)
            {
                Console.WriteLine($"  [PROMPT] IA2: insertText on {label} SUCCEEDED! ({text.Length} chars, fully focusless!)");
                return true;
            }
            Console.WriteLine($"  [PROMPT] IA2 [{label}]: insertText returned hr=0x{hr:X8}");
        }

        // Approach 2: QueryService → IAccessible2 → QI for IAccessibleEditableText
        if (acc is IOleServiceProvider sp)
        {
            var guidIAcc = IID_IAccessible;
            var guidIA2 = IID_IAccessible2;
            var guidET = IID_IAccessibleEditableText;

            hr = sp.QueryService(ref guidIAcc, ref guidIA2, out object? ia2Obj);
            if (hr == 0 && ia2Obj != null)
            {
                if (ia2Obj is IAccessibleEditableText ia2Edit)
                {
                    Console.WriteLine($"  [PROMPT] IA2 [{label}]: IA2→EditableText QI succeeded! Calling insertText...");
                    hr = ia2Edit.insertText(0, text);
                    if (hr == 0)
                    {
                        Console.WriteLine($"  [PROMPT] IA2: insertText via IA2 on {label} SUCCEEDED! ({text.Length} chars)");
                        return true;
                    }
                    Console.WriteLine($"  [PROMPT] IA2 [{label}]: insertText returned hr=0x{hr:X8}");
                }

                // Approach 3: QueryService directly for IAccessibleEditableText
                hr = sp.QueryService(ref guidIAcc, ref guidET, out object? etObj);
                if (hr == 0 && etObj is IAccessibleEditableText serviceEdit)
                {
                    Console.WriteLine($"  [PROMPT] IA2 [{label}]: ServiceProvider→EditableText succeeded! Calling insertText...");
                    hr = serviceEdit.insertText(0, text);
                    if (hr == 0)
                    {
                        Console.WriteLine($"  [PROMPT] IA2: insertText via SP on {label} SUCCEEDED! ({text.Length} chars)");
                        return true;
                    }
                    Console.WriteLine($"  [PROMPT] IA2 [{label}]: insertText returned hr=0x{hr:X8}");
                }

                try { Marshal.ReleaseComObject(ia2Obj); } catch { }
            }
        }

        return false;
    }

    /// <summary>
    /// Open a new chat in Claude Desktop via UIA sidebar "새 대화" button (fully Focusless!).
    /// Flow: Toggle sidebar open → Invoke "새 대화" Hyperlink → verify new JSONL → Toggle sidebar closed.
    /// Falls back to Ctrl+N (focus steal) if UIA approach fails.
    /// Verification: checks that a new JSONL session file was created (= new chat confirmed).
    /// </summary>
    public bool OpenNewChat(PromptInfo? currentPrompt = null)
    {
        try
        {
            // Find Claude Desktop window
            IntPtr claudeHwnd;
            AutomationElement? claudeRoot = null;
            if (currentPrompt != null)
            {
                claudeHwnd = currentPrompt.WindowHandle;
            }
            else
            {
                var found = FindPrompt();
                if (found == null)
                {
                    Console.WriteLine("  [PROMPT] OpenNewChat: Claude Desktop window not found");
                    return false;
                }
                claudeHwnd = found.WindowHandle;
            }

            // Snapshot current JSONL for change detection
            var (beforePath, beforeCreation) = GetLatestJsonlInfo();
            Console.WriteLine($"  [PROMPT] OpenNewChat: Before JSONL = {Path.GetFileName(beforePath ?? "(none)")} created={beforeCreation:HH:mm:ss}");

            // === Strategy 1: UIA Focusless — sidebar toggle + "새 대화" button ===
            bool uiaTriggered = false;
            try
            {
                using var automation = new FlaUI.UIA3.UIA3Automation();
                claudeRoot = automation.FromHandle(claudeHwnd);
                if (claudeRoot != null)
                {
                    // Step 1: Find and click "사이드바 열기" toggle button
                    var sidebarBtn = claudeRoot.FindAllDescendants()
                        .FirstOrDefault(e => e.ControlType == ControlType.Button &&
                                            (e.Name ?? "").Contains("사이드바"));
                    if (sidebarBtn != null && sidebarBtn.Patterns.Toggle.IsSupported)
                    {
                        var toggleState = sidebarBtn.Patterns.Toggle.Pattern.ToggleState;
                        Console.WriteLine($"  [PROMPT] OpenNewChat: Sidebar toggle found (state={toggleState})");

                        // Open sidebar if closed
                        if (toggleState == FlaUI.Core.Definitions.ToggleState.Off)
                        {
                            sidebarBtn.Patterns.Toggle.Pattern.Toggle();
                            Console.WriteLine("  [PROMPT] OpenNewChat: Sidebar opened (focusless!)");
                            Thread.Sleep(500); // Wait for sidebar animation
                        }

                        // Step 2: Find "새 대화" hyperlink in sidebar and Invoke
                        var newChatLink = claudeRoot.FindAllDescendants()
                            .FirstOrDefault(e => e.ControlType == ControlType.Hyperlink &&
                                                (e.Name ?? "").Contains("새 대화"));
                        if (newChatLink != null && newChatLink.Patterns.Invoke.IsSupported)
                        {
                            newChatLink.Patterns.Invoke.Pattern.Invoke();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("  [PROMPT] OpenNewChat: '새 대화' clicked (fully focusless!)");
                            Console.ResetColor();
                            uiaTriggered = true;
                        }
                        else
                        {
                            Console.WriteLine("  [PROMPT] OpenNewChat: '새 대화' hyperlink not found in sidebar");
                        }

                        // Step 3: Close sidebar back (cleanup)
                        Thread.Sleep(300);
                        // Re-find sidebar button (DOM may have changed)
                        sidebarBtn = claudeRoot.FindAllDescendants()
                            .FirstOrDefault(e => e.ControlType == ControlType.Button &&
                                                (e.Name ?? "").Contains("사이드바"));
                        if (sidebarBtn?.Patterns.Toggle.IsSupported == true)
                        {
                            var newState = sidebarBtn.Patterns.Toggle.Pattern.ToggleState;
                            if (newState == FlaUI.Core.Definitions.ToggleState.On)
                            {
                                sidebarBtn.Patterns.Toggle.Pattern.Toggle();
                                Console.WriteLine("  [PROMPT] OpenNewChat: Sidebar closed back (focusless!)");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  [PROMPT] OpenNewChat: UIA strategy failed: {ex.Message}");
            }

            // === Strategy 2: Ctrl+N fallback (requires focus steal) ===
            if (!uiaTriggered)
            {
                Console.WriteLine("  [PROMPT] OpenNewChat: Falling back to Ctrl+N (focus steal)...");
                var prevForeground = NativeMethods.GetForegroundWindow();
                NativeMethods.SmartSetForegroundWindow(claudeHwnd);
                Thread.Sleep(300);
                KeyboardInput.Hotkey(new[] { "ctrl", "n" });
                Console.WriteLine("  [PROMPT] OpenNewChat: Sent Ctrl+N");
                Thread.Sleep(300);
                if (prevForeground != IntPtr.Zero && prevForeground != claudeHwnd)
                    NativeMethods.SmartSetForegroundWindow(prevForeground);
            }

            // Wait and verify: new JSONL file should appear (= new session)
            // Detection: new file path OR same path but newer CreationTime
            // Minimum size: new JSONL should have some initial content (>100 bytes)
            const long MinNewJsonlBytes = 100;
            bool newChatConfirmed = false;
            for (int retry = 0; retry < 15; retry++)  // 15 retries × 500ms = 7.5s max
            {
                Thread.Sleep(500);
                var (afterPath, afterCreation) = GetLatestJsonlInfo();
                if (afterPath == null) continue;

                // Check 1: different file path = definitely new chat
                // Check 2: same path but newer creation time = recycled name (unlikely but safe)
                bool isNewFile = afterPath != beforePath ||
                                 (afterCreation > beforeCreation && (afterCreation - beforeCreation).TotalSeconds > 2);

                if (isNewFile)
                {
                    var afterSize = 0L;
                    try { afterSize = new FileInfo(afterPath).Length; } catch { }

                    if (afterSize >= MinNewJsonlBytes)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"  [PROMPT] OpenNewChat: New JSONL confirmed! {Path.GetFileName(afterPath)} ({afterSize}B, created={afterCreation:HH:mm:ss})");
                        Console.ResetColor();
                        newChatConfirmed = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"  [PROMPT] OpenNewChat: New JSONL found but too small ({afterSize}B < {MinNewJsonlBytes}B), waiting...");
                    }
                }
            }

            if (!newChatConfirmed)
            {
                // Fallback: check if prompt exists (less reliable but better than nothing)
                var newPrompt = FindPrompt();
                if (newPrompt != null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("  [PROMPT] OpenNewChat: No new JSONL confirmed, but prompt found (assuming OK)");
                    Console.ResetColor();
                    return true;
                }
                Console.WriteLine("  [PROMPT] OpenNewChat: FAILED — no new JSONL and no prompt found");
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] OpenNewChat error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Get the most recently created JSONL file in ~/.claude/projects/
    /// Returns (path, creationTime) for new chat detection.
    /// Uses CreationTimeUtc to distinguish genuinely new files from merely updated ones.
    /// </summary>
    internal static (string? path, DateTime creationUtc) GetLatestJsonlInfo()
    {
        try
        {
            var claudeProjectsDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".claude", "projects");
            if (!Directory.Exists(claudeProjectsDir)) return (null, DateTime.MinValue);

            var latest = Directory.EnumerateFiles(claudeProjectsDir, "*.jsonl", SearchOption.AllDirectories)
                .Select(f => { try { return new FileInfo(f); } catch { return null; } })
                .Where(fi => fi != null)
                .OrderByDescending(fi => fi!.CreationTimeUtc)
                .FirstOrDefault();

            if (latest == null) return (null, DateTime.MinValue);
            return (latest.FullName, latest.CreationTimeUtc);
        }
        catch { return (null, DateTime.MinValue); }
    }

    /// <summary>Backward compat wrapper — returns just the path.</summary>
    private static string? GetLatestJsonlPath()
        => GetLatestJsonlInfo().path;

    /// <summary>
    /// Complete auto-relay: open new chat and type handoff prompt immediately.
    /// NOTE: Prefer Schedule-based relay (OpenNewChat + ScheduleManager.Add) for reliability.
    /// This method is kept as a manual/fallback option.
    /// </summary>
    public bool AutoRelay(string handoffPrompt)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("[RELAY] Starting auto-relay to new chat...");
        Console.ResetColor();

        // 1. Open new chat
        if (!OpenNewChat())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[RELAY] Failed to open new chat!");
            Console.ResetColor();
            return false;
        }

        // 2. Wait a bit for new chat to stabilize
        Thread.Sleep(1000);

        // 3. Find prompt in the new chat
        var newPrompt = FindPrompt();
        if (newPrompt == null)
        {
            // Retry
            Thread.Sleep(2000);
            newPrompt = FindPrompt();
        }
        if (newPrompt == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[RELAY] Failed to find prompt in new chat!");
            Console.ResetColor();
            return false;
        }

        // 4. Type handoff prompt
        var ok = TypeAndSubmit(newPrompt, handoffPrompt);
        if (ok)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[RELAY] ✅ Auto-relay complete! New chat started with handoff prompt.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[RELAY] Failed to type handoff prompt!");
            Console.ResetColor();
        }
        return ok;
    }

    public void Dispose()
    {
        _automation.Dispose();
    }
}
