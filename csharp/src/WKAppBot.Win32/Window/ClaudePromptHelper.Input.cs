using System.Drawing;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

public sealed partial class ClaudePromptHelper
{
    /// <summary>
    /// Inject text into prompt without submitting. Focusless-only.
    /// Codex: WM_CHAR to renderer. Claude Desktop: MSAA put_accValue.
    /// VS Code Claude Code: TextPattern2 (truly focusless) → clipboard paste fallback.
    /// Returns false if focusless injection not possible for this host.
    /// </summary>
    public bool InjectTextOnly(PromptInfo prompt, string text)
    {
        if (prompt.HostType == HostCodexDesktop)
            return TryPostMessageTextToChromiumRenderer(prompt, text, submit: false);
        if (prompt.HostType == HostVsCodeClaudeCode)
        {
            // Option 3: TextPattern2 truly focusless
            if (TryVSCodeTextPattern2Insert(prompt, text, submit: false))
                return true;

            Console.WriteLine("  [PROMPT:VSCODE-CC] TP2 inject failed — fallback focus-steal paste");
            using var clipGuard = new ClipboardGuard();
            if (!clipGuard.CanProceed) return false;
            SetClipboardText(text);
            var prevFg = NativeMethods.GetForegroundWindow();
            NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
            Thread.Sleep(150);
            KeyboardInput.Hotkey(new[] { "ctrl", "v" });
            Thread.Sleep(100);
            if (prevFg != IntPtr.Zero && prevFg != prompt.WindowHandle)
                NativeMethods.SmartSetForegroundWindow(prevFg);
            return true;
        }
        // Claude Desktop: MSAA put_accValue (no submit)
        return TryFocuslessInputNoSubmit(prompt, text);
    }

    /// <summary>
    /// Type text into the Claude prompt and submit with Enter.
    /// Strategy (2-tier):
    ///   1. Focusless: MSAA put_accValue via direct vtable (no focus steal!)
    ///   2. Fallback: SetForegroundWindow → Click → Paste → Enter → Restore previous foreground
    /// </summary>
    /// <summary>
    /// Type text into prompt and (by default) submit it.
    /// Pass a <see cref="PromptDeliveryContext"/> for explicit situation-aware strategy selection.
    /// Falls back to <see cref="AllowFocusSteal"/> static when ctx is null (backward compat).
    /// </summary>
    public bool TypeAndSubmit(PromptInfo prompt, string text, PromptDeliveryContext? ctx = null)
    {
        // Resolve delivery decision: context takes precedence over legacy static flag
        bool focusStealAllowed;
        if (ctx != null)
        {
            var decision = ctx.Decide();
            Console.WriteLine($"  [PROMPT] DeliveryCtx: {ctx}");
            if (decision == PromptDeliveryDecision.Skip || decision == PromptDeliveryDecision.Abort)
            {
                Console.WriteLine($"  [PROMPT] Delivery decision={decision} — abort");
                return false;
            }
            focusStealAllowed = decision == PromptDeliveryDecision.FocusSteal;
        }
        else
        {
            focusStealAllowed = AllowFocusSteal;
        }

        // === VS Code Claude Code (native extension): focus-steal + Escape + paste ===
        if (prompt.HostType == HostVsCodeClaudeCode)
            return TypeAndSubmitVSCodeClaudeCode(prompt, text, focusStealAllowed);
        if (prompt.HostType == HostCodexDesktop)
            return SubmitCodexDesktopPrompt(prompt, text);

        // === Strategy 1: Try fully focusless input ===
        if (TryFocuslessInput(prompt, text))
            return true;

        // === Strategy 2: Focus-stealing with auto-restore (minimal disruption) ===
        if (!focusStealAllowed)
        {
            Console.WriteLine("  [PROMPT] Focusless-only mode: focusless input failed, focus steal not allowed");
            return false;
        }
        using var clipGuard2 = new ClipboardGuard();
        if (!clipGuard2.CanProceed) return false;
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
    /// Option 3: TextPattern2.InsertTextAtSelection — truly focusless for VS Code Claude Code.
    /// Finds [Edit] "Message input" by ControlType+Name, then InsertTextAtSelection.
    /// Submit (if requested) via renderer WM_KEYDOWN Enter.
    /// Returns false if TextPattern2 not supported or element not found.
    /// </summary>
    private bool TryVSCodeTextPattern2Insert(PromptInfo prompt, string text, bool submit)
    {
        try
        {
            var root = _automation.FromHandle(prompt.WindowHandle);
            if (root == null) return false;

            // Filter by ControlType.Edit + Name to avoid matching text nodes containing "Message input"
            var editEl = root.FindFirst(TreeScope.Descendants, new AndCondition(
                new PropertyCondition(_automation.PropertyLibrary.Element.ControlType, ControlType.Edit),
                new PropertyCondition(_automation.PropertyLibrary.Element.Name, "Message input")));

            if (editEl == null)
            {
                Console.WriteLine("  [PROMPT:VSCODE-CC] TP2: [Edit] 'Message input' not found");
                return false;
            }

            var tp2 = editEl.Patterns.Text2;
            if (!tp2.IsSupported)
            {
                Console.WriteLine("  [PROMPT:VSCODE-CC] TP2: TextPattern2 not supported on this element");
                return false;
            }

            // FlaUI 4.0 IText2Pattern interface omits InsertTextAtSelection — call via reflection
            var insertMethod = tp2.Pattern.GetType().GetMethod("InsertTextAtSelection",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (insertMethod == null)
            {
                Console.WriteLine("  [PROMPT:VSCODE-CC] TP2: InsertTextAtSelection not in FlaUI 4.0 API");
                return false;
            }
            insertMethod.Invoke(tp2.Pattern, new object[] { text });
            Console.WriteLine($"  [PROMPT:VSCODE-CC] TP2 InsertTextAtSelection OK ({text.Length} chars, focusless!)");

            if (!submit) return true;

            // Submit via renderer WM_KEYDOWN Enter — no focus steal needed
            var rendHwnd = GetRendererHwnd(prompt.WindowHandle);
            if (rendHwnd == IntPtr.Zero)
            {
                Console.WriteLine("  [PROMPT:VSCODE-CC] TP2: renderer hwnd not found for Enter submit");
                return false;
            }

            const int WM_KEYDOWN = 0x0100, WM_KEYUP = 0x0101, VK_RETURN = 0x0D;
            uint sc = NativeMethods.MapVirtualKeyW(VK_RETURN, 0);
            IntPtr lpDown = (IntPtr)((sc << 16) | 1);
            IntPtr lpUp = (IntPtr)((sc << 16) | 1 | (1 << 30) | (1 << 31));
            NativeMethods.PostMessageW(rendHwnd, WM_KEYDOWN, (IntPtr)VK_RETURN, lpDown);
            Thread.Sleep(20);
            NativeMethods.PostMessageW(rendHwnd, WM_KEYUP, (IntPtr)VK_RETURN, lpUp);
            Console.WriteLine("  [PROMPT:VSCODE-CC] TP2 + renderer Enter submitted (focusless!)");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT:VSCODE-CC] TP2: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// VS Code Claude Code extension: TextPattern2 focusless first, then focus-steal fallback.
    /// UIA turn-form 없음 → 키보드 단축키로 입력 영역 포커싱.
    /// Escape: Claude Code 입력창 포커스 (VS Code 확장 기본 동작).
    /// 입력위치확보(Probe) 승인 후 호출되므로 포커스 전환 허용됨.
    /// </summary>
    private bool TypeAndSubmitVSCodeClaudeCode(PromptInfo prompt, string text, bool focusStealAllowed = false)
    {
        // Option 1: WM_CHAR to renderer (Codex-proven focusless path) + renderer Enter
        if (TryPostMessageTextToChromiumRenderer(prompt, text, submit: true))
        {
            Console.WriteLine("  [PROMPT:VSCODE-CC] WM_CHAR+Enter focusless OK");
            return true;
        }
        Console.WriteLine("  [PROMPT:VSCODE-CC] WM_CHAR failed — trying TP2");

        // Option 2: TextPattern2.InsertTextAtSelection (truly focusless!)
        if (TryVSCodeTextPattern2Insert(prompt, text, submit: true))
            return true;

        Console.WriteLine("  [PROMPT:VSCODE-CC] focusless all paths failed — falling back to focus-steal");

        if (!focusStealAllowed && !AllowFocusSteal)
        {
            Console.WriteLine("  [PROMPT:VSCODE-CC] Focus steal not allowed — abort");
            return false;
        }

        using var clipGuardVscc = new ClipboardGuard();
        if (!clipGuardVscc.CanProceed) return false;
        var prevForeground = NativeMethods.GetForegroundWindow();

        // 항상 타이틀바 클릭으로 윈도우 활성화 — GetForegroundWindow 결과 무관
        // 같은 프로세스(Electron) 다중 창은 API로 키보드 포커스 전환 불가 → 클릭 필수
        NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
        NativeMethods.GetWindowRect(prompt.WindowHandle, out var wr);
        var titleX = wr.Left + wr.Width / 2;
        var titleY = wr.Top + 15; // 타이틀바 중앙
        MouseInput.Click(titleX, titleY);
        Thread.Sleep(150);

        // ── Foreground verification — confirm this window actually got focus ──
        // Multiple VS Code windows share the same process; if focus steal fails,
        // keystrokes go to the wrong window. Verify before pasting.
        var actualFg = NativeMethods.GetForegroundWindow();
        if (actualFg != prompt.WindowHandle)
        {
            NativeMethods.GetWindowThreadProcessId(actualFg, out uint actualPid);
            NativeMethods.GetWindowThreadProcessId(prompt.WindowHandle, out uint targetPid);
            if (actualPid != targetPid)
            {
                // Completely wrong process — abort
                Console.WriteLine($"  [PROMPT:VSCODE-CC] Focus steal FAILED (fg=0x{actualFg:X} vs target=0x{prompt.WindowHandle:X}) — abort");
                return false;
            }
            // Same process but different window — might still be OK (VS Code internal focus)
            Console.WriteLine($"  [PROMPT:VSCODE-CC] Focus on sibling window (same process) — proceeding");
        }
        Console.WriteLine($"  [PROMPT:VSCODE-CC] Window activated via click ({titleX},{titleY})");

        // Escape → 입력창 포커스 → Paste → Enter (갭 최소화)
        KeyboardInput.PressKey("escape");
        Thread.Sleep(100);

        Console.WriteLine($"  [PROMPT:VSCODE-CC] Pasting ({text.Length} chars)");
        SetClipboardText(text);
        Thread.Sleep(30);
        KeyboardInput.Hotkey(new[] { "ctrl", "v" });
        Thread.Sleep(200);

        KeyboardInput.PressKey("enter");
        Console.WriteLine("  [PROMPT:VSCODE-CC] Submitted");

        // Restore previous foreground
        if (prevForeground != IntPtr.Zero && prevForeground != prompt.WindowHandle)
        {
            Thread.Sleep(300);
            NativeMethods.SmartSetForegroundWindow(prevForeground);
        }

        return true;
    }

    /// <summary>
    /// Codex desktop: prefer focusless WM_CHAR injection into Chromium renderer.
    /// Fallback to focus-steal paste+Enter only when explicitly allowed.
    /// </summary>
    // Ownership boundary: Codex-specific send logic stays in CodexDesktop-prefixed methods.
    // Claude-specific probing/submission should not be mixed into this path.
    private bool SubmitCodexDesktopPrompt(PromptInfo prompt, string text)
    {
        if (TryPostMessageTextToChromiumRenderer(prompt, text, submit: true))
            return true;

        if (!AllowFocusSteal)
        {
            Console.WriteLine("  [PROMPT:CODEX] Focusless-only mode: WM_CHAR path failed, focus steal not allowed");
            return false;
        }

        using var clipGuardCodex = new ClipboardGuard();
        if (!clipGuardCodex.CanProceed) return false;
        var prevForeground = NativeMethods.GetForegroundWindow();
        NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
        Thread.Sleep(120);

        var centerX = prompt.PromptRect.X + prompt.PromptRect.Width / 2;
        var centerY = prompt.PromptRect.Y + prompt.PromptRect.Height / 2;
        MouseInput.Click(centerX, centerY);
        Thread.Sleep(80);

        SetClipboardText(text);
        Thread.Sleep(50);
        KeyboardInput.Hotkey(new[] { "ctrl", "v" });
        Thread.Sleep(120);
        KeyboardInput.PressKey("enter");
        Console.WriteLine("  [PROMPT:CODEX] Fallback submit via focus-steal paste+Enter");

        if (prevForeground != IntPtr.Zero && prevForeground != prompt.WindowHandle)
        {
            Thread.Sleep(180);
            NativeMethods.SmartSetForegroundWindow(prevForeground);
        }

        return true;
    }

    /// <summary>
    /// Focusless text input for Chromium hosts using WM_CHAR + optional Enter.
    /// </summary>
    private bool TryPostMessageTextToChromiumRenderer(PromptInfo prompt, string text, bool submit)
    {
        try
        {
            // [FOCUS-GUARD] 타겟 렌더러 확인 — prompt.PromptRect와 겹치는 렌더러만 사용
            // VS Code에는 Chrome_RenderWidgetHostHWND가 다수 (에디터/사이드바/터미널 등)
            // 첫 번째가 코드 에디터일 수 있으므로 rect 기반으로 올바른 패널 선택
            var rendererHwnd = prompt.PromptRect != System.Drawing.Rectangle.Empty
                ? GetRendererHwndByRect(prompt.WindowHandle, prompt.PromptRect)
                : GetRendererHwnd(prompt.WindowHandle);

            if (rendererHwnd == IntPtr.Zero)
            {
                Console.WriteLine("  [PROMPT] WM_CHAR: renderer hwnd not found (rect-match failed)");
                return false;
            }

            // [FOCUS-GUARD] 캐럿이 PromptRect 안에 있어야 WM_CHAR 안전
            // 크롬 창 전체가 아니라 Claude 패널 rect 기준으로 판단 (에디터/터미널 방지)
            //
            // 폴백 체인:
            //   1. UIA TextPattern caret → PromptRect 포함 여부
            //   2. Win32 GetCaretPos     → PromptRect 포함 여부
            //   3. 캐럿 없음 + IsIconic  → SW_SHOWNOACTIVATE 복원 → 재시도
            //   4. 캐럿 없음 + thread focus == renderer → 안전 (백그라운드)
            //   5. 캐럿 없음 + 위 모두 실패 → FocusPromptFocusless (WM_LBUTTON) → 재시도
            //   6. 그래도 없으면 → abort
            var kbFocus = NativeMethods.GetKeyboardFocusHwnd();
            if (prompt.PromptRect != System.Drawing.Rectangle.Empty)
            {
                if (!EnsureCaretInPrompt(prompt, rendererHwnd, ref kbFocus))
                    return false;
            }
            else if (kbFocus != IntPtr.Zero && kbFocus != rendererHwnd)
                Console.WriteLine($"  [PROMPT] WM_CHAR: no PromptRect, renderer=0x{rendererHwnd:X8} kbFocus=0x{kbFocus:X8} — trying anyway");

            const int WM_CHAR = 0x0102;
            var ok = true;
            var normalized = text.Replace("\r\n", "\n");
            foreach (var ch in normalized)
            {
                if (!NativeMethods.PostMessageW(rendererHwnd, WM_CHAR, (IntPtr)ch, (IntPtr)1))
                    ok = false;
            }

            if (!ok)
            {
                Console.WriteLine("  [PROMPT] WM_CHAR: one or more character posts failed");
                return false;
            }

            Console.WriteLine($"  [PROMPT] WM_CHAR posted ({normalized.Length} chars)");

            if (!submit) return true;

            // Submit with Enter key events.
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;
            const int VK_RETURN = 0x0D;
            uint scanCode = NativeMethods.MapVirtualKeyW((uint)VK_RETURN, 0);
            IntPtr lParamDown = (IntPtr)((scanCode << 16) | 1);
            IntPtr lParamUp = (IntPtr)((scanCode << 16) | 1 | (1 << 30) | (1 << 31));
            var down = NativeMethods.PostMessageW(rendererHwnd, WM_KEYDOWN, (IntPtr)VK_RETURN, lParamDown);
            Thread.Sleep(20);
            var up = NativeMethods.PostMessageW(rendererHwnd, WM_KEYUP, (IntPtr)VK_RETURN, lParamUp);
            Console.WriteLine($"  [PROMPT] WM_CHAR submit enter down={down} up={up}");
            return down && up;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] WM_CHAR error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Try to input text into the Claude prompt WITHOUT stealing focus.
    /// Priority: MSAA put_accValue (direct vtable) → LegacyIAccessible.SetValue (UIA bridge).
    /// Returns true if text was successfully inserted AND submitted.
    /// </summary>
    private bool TryFocuslessInputNoSubmit(PromptInfo prompt, string text)
    {
        // IA2 insertText — focusless, no submit
        if (TryIA2InsertText(prompt, text))
        {
            Console.WriteLine("  [PROMPT] IA2 insert (no submit) OK");
            return true;
        }
        return false;
    }

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
    /// Type text into the prompt WITHOUT submitting (no Enter).
    /// For testing/dry-run: verify text insertion works.
    /// Tries focusless first, then focus-stealing with restore.
    /// </summary>
    public bool TypeWithoutSubmit(PromptInfo prompt, string text)
    {
        if (TryTypeWithoutSubmitFocusless(prompt, text))
            return true;

        // Fallback: focus-steal to paste text, but no Enter
        Console.WriteLine("  [PROMPT] Falling back to focus-steal paste (no submit)...");
        using var clipGuardNoSubmit = new ClipboardGuard();
        if (!clipGuardNoSubmit.CanProceed) return false;
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
    /// Focusless-only text insert (NO focus-steal fallback).
    /// Returns false if IA2/LegacyIA/TextPattern2 focusless paths all fail.
    /// </summary>
    public bool TryTypeWithoutSubmitFocusless(PromptInfo prompt, string text)
    {
        // VS Code: WM_CHAR to renderer first (Codex-proven path), then TP2 fallback
        if (prompt.HostType == HostVsCodeClaudeCode)
        {
            if (TryPostMessageTextToChromiumRenderer(prompt, text, submit: false))
            {
                Console.WriteLine("  [PROMPT:VSCODE-CC] WM_CHAR focusless OK");
                return true;
            }
            Console.WriteLine("  [PROMPT:VSCODE-CC] WM_CHAR failed — trying TP2");
            if (TryVSCodeTextPattern2Insert(prompt, text, submit: false))
                return true;
            Console.WriteLine("  [PROMPT:VSCODE-CC] focusless all paths failed");
        }

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

        return false;
    }

    /// <summary>
    /// Read current text in the prompt input area (focusless).
    /// VS Code: [Edit] "Message input" → Text pattern DocumentRange.
    /// Claude Desktop/others: turn-form inputGroup → Value pattern.
    /// Returns null if unable to read.
    /// </summary>
    public string? ReadCurrentInputText(PromptInfo prompt)
    {
        try
        {
            var root = _automation.FromHandle(prompt.WindowHandle);
            if (root == null) return null;

            // VS Code Claude Code path: [Edit] "Message input"
            if (string.Equals(prompt.HostType, HostVsCodeClaudeCode, StringComparison.OrdinalIgnoreCase)
             || string.Equals(prompt.HostType, "codex-desktop", StringComparison.OrdinalIgnoreCase))
            {
                var editEl = root.FindFirst(FlaUI.Core.Definitions.TreeScope.Descendants, new FlaUI.Core.Conditions.AndCondition(
                    new PropertyCondition(_automation.PropertyLibrary.Element.ControlType, ControlType.Edit),
                    new PropertyCondition(_automation.PropertyLibrary.Element.Name, "Message input")));
                if (editEl == null) return null;

                // Value pattern first — empty string = no content (placeholder NOT included)
                // TextPattern.GetText() returns placeholder text even when input is empty → skip
                var vp = editEl.Patterns.Value;
                if (vp.IsSupported)
                {
                    var val = vp.Pattern.Value;
                    return string.IsNullOrEmpty(val) ? null : val;
                }

                // Fallback: Text pattern (only if Value not supported)
                var tp = editEl.Patterns.Text;
                if (tp.IsSupported)
                {
                    var text = tp.Pattern.DocumentRange.GetText(-1);
                    return string.IsNullOrEmpty(text) ? null : text;
                }

                return null;
            }

            // Claude Desktop path: turn-form → inputGroup
            var turnForm2 = root.FindFirstDescendant(
                new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
            if (turnForm2 == null) return null;

            var inputGroup2 = turnForm2.FindFirstChild(
                new PropertyCondition(_automation.PropertyLibrary.Element.ControlType, ControlType.Group));
            if (inputGroup2 == null) return null;

            var valPat = inputGroup2.Patterns.Value;
            if (valPat.IsSupported)
                return valPat.Pattern.Value;

            var txtPat = inputGroup2.Patterns.Text;
            if (txtPat.IsSupported)
                return txtPat.Pattern.DocumentRange.GetText(-1);

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] ReadCurrentInputText error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Clear the prompt input area (focusless preferred, focus-steal fallback).
    /// Must be called with AllowFocusSteal=true if focus-steal fallback is needed.
    /// </summary>
    public bool ClearCurrentInput(PromptInfo prompt)
    {
        try
        {
            var root = _automation.FromHandle(prompt.WindowHandle);
            if (root == null) return false;

            // VS Code / Codex: try Value.SetValue("") on [Edit] "Message input"
            if (string.Equals(prompt.HostType, HostVsCodeClaudeCode, StringComparison.OrdinalIgnoreCase)
             || string.Equals(prompt.HostType, "codex-desktop", StringComparison.OrdinalIgnoreCase))
            {
                var editEl = root.FindFirst(FlaUI.Core.Definitions.TreeScope.Descendants, new FlaUI.Core.Conditions.AndCondition(
                    new PropertyCondition(_automation.PropertyLibrary.Element.ControlType, ControlType.Edit),
                    new PropertyCondition(_automation.PropertyLibrary.Element.Name, "Message input")));

                if (editEl != null)
                {
                    var vp = editEl.Patterns.Value;
                    if (vp.IsSupported)
                    {
                        vp.Pattern.SetValue("");
                        Console.WriteLine("  [PROMPT] ClearCurrentInput: Value.SetValue('') OK (focusless)");
                        return true;
                    }
                }

                // Fallback: focus-steal + Escape + Ctrl+A + Delete
                if (!AllowFocusSteal)
                {
                    Console.WriteLine("  [PROMPT] ClearCurrentInput: focusless failed, AllowFocusSteal=false — skip");
                    return false;
                }

                var prevFg = NativeMethods.GetForegroundWindow();
                NativeMethods.SmartSetForegroundWindow(prompt.WindowHandle);
                Thread.Sleep(150);
                KeyboardInput.PressKey("escape");
                Thread.Sleep(80);
                KeyboardInput.Hotkey(new[] { "ctrl", "a" });
                Thread.Sleep(30);
                KeyboardInput.PressKey("delete");
                Thread.Sleep(50);
                Console.WriteLine("  [PROMPT] ClearCurrentInput: focus-steal Ctrl+A+Delete OK");
                if (prevFg != IntPtr.Zero && prevFg != prompt.WindowHandle)
                    NativeMethods.SmartSetForegroundWindow(prevFg);
                return true;
            }

            // Claude Desktop: Value.SetValue("") on inputGroup
            var turnForm = root.FindFirstDescendant(
                new PropertyCondition(_automation.PropertyLibrary.Element.AutomationId, "turn-form"));
            if (turnForm == null) return false;
            var inputGroup = turnForm.FindFirstChild(
                new PropertyCondition(_automation.PropertyLibrary.Element.ControlType, ControlType.Group));
            if (inputGroup == null) return false;
            var valPat = inputGroup.Patterns.Value;
            if (valPat.IsSupported)
            {
                valPat.Pattern.SetValue("");
                Console.WriteLine("  [PROMPT] ClearCurrentInput: Value.SetValue('') on inputGroup OK");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [PROMPT] ClearCurrentInput error: {ex.Message}");
            return false;
        }
    }

    /// <summary>Public wrapper for clipboard text setting (used by NewChatCommand).</summary>
    public static void SetClipboardTextPublic(string text) => SetClipboardText(text);

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

    /// <summary>
    /// [CLIPBOARD-RESTORE] Get current clipboard text content (null if empty or non-text).
    /// Used to save/restore clipboard around focus-steal paste operations.
    /// </summary>
    private static string? GetClipboardText()
    {
        string? result = null;
        var thread = new Thread(() =>
        {
            if (!NativeMethods.OpenClipboard(IntPtr.Zero)) return;
            try
            {
                var hData = NativeMethods.GetClipboardData(13 /* CF_UNICODETEXT */);
                if (hData == IntPtr.Zero) return;
                var ptr = NativeMethods.GlobalLock(hData);
                if (ptr == IntPtr.Zero) return;
                try { result = System.Runtime.InteropServices.Marshal.PtrToStringUni(ptr); }
                finally { NativeMethods.GlobalUnlock(hData); }
            }
            finally { NativeMethods.CloseClipboard(); }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join(2000);
        return result;
    }

    /// <summary>
    /// Saves clipboard content on construction, restores it on Dispose.
    /// Use with 'using' around any SetClipboardText + paste sequence to protect user's clipboard.
    /// If save fails (clipboard locked etc.) → blocks SetClipboardText to avoid silent data loss.
    /// </summary>
    internal sealed class ClipboardGuard : IDisposable
    {
        private readonly string? _saved;
        private readonly bool _saveOk;

        public ClipboardGuard()
        {
            _saved = GetClipboardText();
            _saveOk = true; // null is valid (empty clipboard)
            Console.WriteLine(_saved != null
                ? $"  [CLIPBOARD] Saved ({_saved.Length} chars)"
                : "  [CLIPBOARD] Saved (empty)");
        }

        /// <summary>True when it's safe to proceed with clipboard injection.</summary>
        public bool CanProceed => _saveOk;

        public void Dispose()
        {
            if (!_saveOk) return;
            if (_saved != null)
            {
                SetClipboardText(_saved);
                Console.WriteLine($"  [CLIPBOARD] Restored ({_saved.Length} chars)");
            }
            // null = was empty → leave clipboard as-is (don't clear user's new copy if they pasted fast)
        }
    }
}
