using WKAppBot.Core.Scenario;
using WKAppBot.Win32.Input;

namespace WKAppBot.Core.Runner;

public sealed partial class ActionExecutor
{
    // -- Text / Keyboard ----------------------------------------

    private void DoTypeText(StepDefinition step, StepResult result)
    {
        var text = _ctx.Resolve(step.Params!.Text);
        SetActionPointToWindowCenter(step.Name, "type_text", $"type \"{text}\"");

        FlaUI.Core.AutomationElements.AutomationElement? element = null;

        // -- Tier 1: UIA Value pattern (focusless -- no focus needed!) --
        if (_ctx.PreferFocusless && step.Target != null)
        {
            try
            {
                (element, _) = LocateElement(step);
                EnsureInputReady(element, step.Action);
                BeginZoomForElement(element, step);
                if (element?.Patterns.Value.IsSupported == true)
                {
                    element.Patterns.Value.Pattern.SetValue(text);
                    result.ActionDetail = $"Type \"{text}\" (UIA Value, focusless)";
                    Log($"  Typed via UIA Value: \"{text}\" (focusless)");
                    return;
                }
            }
            catch { /* UIA Value not available -- fall through */ }
        }

        // -- Tier 2: WM_CHAR PostMessage (cross-process, queued to target) --
        // Best for MFC CMaskEdit, owner-drawn inputs (Gemini recommendation)
        var hWnd = GetTargetHwnd(step, element);
        if (hWnd != IntPtr.Zero)
        {
            try
            {
                // Set focus to target window first (WM_CHAR needs focus on the control)
                WKAppBot.Win32.Native.NativeMethods.SetFocus(hWnd);
                Thread.Sleep(50);

                if (KeyboardInput.TypeTextViaWmChar(hWnd, text))
                {
                    // Verify: read back text to confirm
                    Thread.Sleep(100);
                    var readBack = WKAppBot.Win32.Native.NativeMethods.GetWindowTextW(hWnd);
                    if (!string.IsNullOrEmpty(readBack) && readBack.Contains(text.Replace(",", "")))
                    {
                        result.ActionDetail = $"Type \"{text}\" (WM_CHAR)";
                        Log($"  Typed via WM_CHAR: \"{text}\"");
                        return;
                    }
                    // WM_CHAR sent but verification failed -- try next tier
                    Log($"  WM_CHAR sent but verify failed (got \"{readBack}\"), trying next...");
                }
            }
            catch { /* WM_CHAR failed -- fall through */ }
        }

        // -- Tier 3: EM_REPLACESEL (Edit control text insertion) --
        if (hWnd != IntPtr.Zero)
        {
            try
            {
                // Select all existing text first, then replace
                WKAppBot.Win32.Native.NativeMethods.SendMessageW(hWnd,
                    0x00B1 /*EM_SETSEL*/, IntPtr.Zero, (IntPtr)(-1)); // select all
                if (KeyboardInput.TypeTextViaEmReplaceSel(hWnd, text))
                {
                    Thread.Sleep(50);
                    var readBack = WKAppBot.Win32.Native.NativeMethods.GetWindowTextW(hWnd);
                    if (!string.IsNullOrEmpty(readBack) && readBack.Contains(text.Replace(",", "")))
                    {
                        result.ActionDetail = $"Type \"{text}\" (EM_REPLACESEL)";
                        Log($"  Typed via EM_REPLACESEL: \"{text}\"");
                        return;
                    }
                    Log($"  EM_REPLACESEL sent but verify failed (got \"{readBack}\"), trying next...");
                }
            }
            catch { /* EM_REPLACESEL failed -- fall through */ }
        }

        // -- Tier 4: SendInput Unicode (physical input, most reliable, needs focus) --
        EnsureFocus();
        // Pass hWnd so TypeText can check focus per-character and restore on drift
        KeyboardInput.TypeText(text, hWnd);
        result.ActionDetail = $"Type \"{text}\" (SendInput)";
        Log($"  Typed via SendInput: \"{text}\"");
    }

    /// <summary>
    /// Get the native window handle for the target element.
    /// Used by WM_CHAR/EM_REPLACESEL tiers.
    /// </summary>
    private IntPtr GetTargetHwnd(StepDefinition step, FlaUI.Core.AutomationElements.AutomationElement? element)
    {
        try
        {
            // Try UIA element's native window handle first
            if (element != null)
            {
                var hwndProp = element.Properties.NativeWindowHandle;
                if (hwndProp.IsSupported)
                    return hwndProp.Value;
            }

            // Try locating element if not already done
            if (element == null && step.Target != null)
            {
                var (el, _) = LocateElement(step);
                if (el != null)
                {
                    var hwndProp = el.Properties.NativeWindowHandle;
                    if (hwndProp.IsSupported)
                        return hwndProp.Value;
                }
            }
        }
        catch { }

        return IntPtr.Zero;
    }

    private void DoPressKey(StepDefinition step, StepResult result)
    {
        var key = _ctx.Resolve(step.Params!.Key)!;
        SetActionPointToWindowCenter(step.Name, "press_key", $"key:{key}");
        EnsureFocus();  // SendInput required
        KeyboardInput.PressKey(key);
        result.ActionDetail = $"Press {key}";
        Log($"  Pressed: {key}");
    }

    private void DoHotkey(StepDefinition step, StepResult result)
    {
        var keys = step.Params!.Keys!;
        SetActionPointToWindowCenter(step.Name, "hotkey", $"hotkey:{string.Join("+", keys)}");
        EnsureFocus();  // SendInput required
        KeyboardInput.Hotkey(keys);
        result.ActionDetail = $"Hotkey {string.Join("+", keys)}";
        Log($"  Hotkey: {string.Join("+", keys)}");
    }
}
