using System.Diagnostics;
using WKAppBot.Core.Scenario;
using WKAppBot.Vision;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;
using FlaUI.Core.AutomationElements;

namespace WKAppBot.Core.Runner;

/// <summary>
/// Executes individual step actions.
/// 5-tier Chain of Responsibility:
///   1. UIA (Accessibility) — fast, reliable
///   2. Vision Cache — cached results (경험치!)
///   3. Simple OCR — Windows.Media.Ocr text matching (free, fast, offline)
///   4. Vision API — Claude screenshot analysis (expensive, cached)
///   5. Coordinate — raw x,y fallback
///
/// Focusless-First principle:
///   - UIA Invoke (click), UIA Value (type) → no focus needed, user undisturbed
///   - SendInput (mouse/keyboard) → EnsureFocus required
/// </summary>
public sealed class ActionExecutor : IDisposable
{
    private readonly UiaLocator _uia;
    private readonly RuntimeContext _ctx;
    private readonly bool _verbose;

    // Vision tier (optional — only when vision_enabled: true)
    private VisionCache? _visionCache;
    private VisionAnalyzer? _visionAnalyzer;
    private SimpleOcrAnalyzer? _simpleOcr;
    private OcrSegmentCache? _segmentCache;  // Tier 2.5: form-level dynamic a11y tree

    // [ZOOM] Overlay factory — set by CLI layer (ClickZoomAdapter)
    // Parameters: (screenRect, formHandle, actionName, label) → IActionZoom?
    private IActionZoom? _currentZoom;

    /// <summary>
    /// Optional zoom overlay factory. Set by CLI layer to enable visual feedback.
    /// Parameters: (System.Drawing.Rectangle screenRect, IntPtr formHandle, string action, string label)
    /// </summary>
    public Func<System.Drawing.Rectangle, IntPtr, string, string, IActionZoom?>? CreateZoom { get; set; }

    /// <summary>
    /// Optional Vision AI ask delegate — CLI wires this to Gemini/Claude CDP for blob identification.
    ///
    /// Called when OcrSegmentCache has no text match AND Vision API isn't configured.
    /// Parameters: (formScreenshot, elementDescription)
    /// Returns: OcrSegment with x/y/w/h from Gemini JSON, or null on failure.
    ///
    /// Coordinates come directly from Gemini JSON — no BestMatch step needed.
    /// Result is used to:
    ///   1. Save blob crop: {pixelHash}={label}.png
    ///   2. Teach OcrSegmentCache (source="gemini") for future lookups
    /// </summary>
    public Func<System.Drawing.Bitmap, string, Task<OcrSegment?>>? AskVisionFn { get; set; }

    public ActionExecutor(RuntimeContext ctx, bool verbose = false,
                          VisionCache? visionCache = null, VisionAnalyzer? visionAnalyzer = null,
                          SimpleOcrAnalyzer? simpleOcr = null,
                          OcrSegmentCache? segmentCache = null)
    {
        _ctx = ctx;
        _verbose = verbose;
        _uia = new UiaLocator();
        _visionCache = visionCache;
        _visionAnalyzer = visionAnalyzer;
        _segmentCache = segmentCache;
        _simpleOcr = simpleOcr;
    }

    // ── [READINESS] Pre-action input readiness check ────────────

    /// <summary>
    /// Optional InputReadiness instance. Set by CLI layer to enable pre-action readiness checks.
    /// When set, every a11y action triggers: readiness check → zoom → actual call.
    /// </summary>
    public InputReadiness? Readiness { get; set; }

    /// <summary>
    /// Lightweight readiness check before a11y action:
    ///   1. Minimized → focusless restore (SW_SHOWNOACTIVATE)
    ///   2. Blocker detection (~5ms, QuickMode)
    /// Does NOT do full Probe — keeps action latency minimal.
    /// </summary>
    private void EnsureInputReady(FlaUI.Core.AutomationElements.AutomationElement? element, string action)
    {
        var mainHwnd = _ctx.MainWindowHandle;
        if (mainHwnd == IntPtr.Zero) return;

        // Step 1: Minimized → focusless restore
        if (NativeMethods.IsIconic(mainHwnd))
        {
            Log($"  [READINESS] Window minimized — restoring (focusless)");
            NativeMethods.ShowWindow(mainHwnd, NativeMethods.SW_SHOWNOACTIVATE);
            Thread.Sleep(200); // wait for restore animation
        }

        // Step 2: Blocker detection (quick — ~5ms)
        if (Readiness != null)
        {
            var blocker = Readiness.DetectBlocker(mainHwnd);
            if (blocker != null)
            {
                Log($"  [READINESS] Blocker: {blocker.ClassName} \"{blocker.Title}\" — attempting dismiss");
                var (handled, _) = Readiness.BlockerHandler?.TryHandle(mainHwnd, blocker)
                                   ?? (false, false);
                if (!handled)
                    Log($"  [READINESS] Blocker not handled — continuing anyway");
            }
        }
    }

    // ── [ZOOM] Overlay helper ───────────────────────────────

    /// <summary>
    /// Start zoom overlay for the located element (if factory is set).
    /// Sets _currentZoom field — Execute() manages ShowPass/ShowFail/Dispose.
    /// </summary>
    private void BeginZoomForElement(FlaUI.Core.AutomationElements.AutomationElement? element, StepDefinition step)
    {
        if (CreateZoom == null || element == null) return;
        try
        {
            var rect = element.Properties.BoundingRectangle.ValueOrDefault;
            if (rect.Width <= 0 || rect.Height <= 0) return;

            var hwnd = IntPtr.Zero;
            try { hwnd = element.Properties.NativeWindowHandle.ValueOrDefault; } catch { }

            var screenRect = new System.Drawing.Rectangle(
                (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
            var formHandle = hwnd != IntPtr.Zero ? hwnd : _ctx.MainWindowHandle;
            var label = step.Target?.AutomationId ?? step.Target?.Name ?? step.Name;

            _currentZoom = CreateZoom(screenRect, formHandle, step.Action, label);
        }
        catch { /* zoom is non-critical — never block automation */ }
    }

    // ── Smart Focus ("위치확보") ─────────────────────────────

    /// <summary>
    /// Ensure the target window has focus before SendInput.
    /// Called only when focusless path is not available.
    ///
    /// Flow:
    ///   1. Already focused? → return (0ms)
    ///   2. Alert (beep + flash) → wait alertDelay for user to switch back
    ///   3. Force focus (AttachThreadInput trick) → retry loop
    ///   4. Timeout → throw exception
    /// </summary>
    private void EnsureFocus()
    {
        if (!_ctx.FocusCheck) return;
        if (_ctx.MainWindowHandle == IntPtr.Zero) return;

        // Quick check — most common case
        if (NativeMethods.IsWindowForeground(_ctx.MainWindowHandle))
            return;

        // Smart multi-phase recovery
        var (success, method) = WindowFinder.SmartBringToFront(
            _ctx.MainWindowHandle,
            timeoutSec: _ctx.FocusTimeout,
            retryDelaySec: _ctx.FocusRetryDelay,
            alertDelaySec: _ctx.FocusAlertDelay,
            consoleLock: _ctx.ConsoleLock);

        if (!success)
        {
            throw new InvalidOperationException(
                $"Focus recovery failed after {_ctx.FocusTimeout:F1}s — " +
                "please switch to the test window or disable focus_check");
        }
    }

    /// <summary>
    /// Execute a step and return result.
    /// </summary>
    public StepResult Execute(StepDefinition step)
    {
        var sw = Stopwatch.StartNew();
        var result = new StepResult
        {
            StepName = step.Name,
            Action = step.Action
        };

        _currentZoom = null;
        try
        {
            switch (step.Action)
            {
                case "click":
                    result.LocatorMethod = DoClick(step, result);
                    break;

                case "double_click":
                    result.LocatorMethod = DoDoubleClick(step, result);
                    break;

                case "right_click":
                    result.LocatorMethod = DoRightClick(step, result);
                    break;

                case "type_text":
                    DoTypeText(step, result);
                    break;

                case "press_key":
                    DoPressKey(step, result);
                    break;

                case "hotkey":
                    DoHotkey(step, result);
                    break;

                case "wait":
                    DoWait(step, result);
                    break;

                case "assert":
                    DoAssert(step, result);
                    break;

                case "screenshot":
                    DoScreenshot(step, result);
                    break;

                case "scroll":
                    DoScroll(step, result);
                    break;

                // ── UIA Pattern actions (all focusless!) ──────────────
                case "toggle":
                    DoToggle(step, result);
                    break;

                case "expand":
                    DoExpandCollapse(step, result, expand: true);
                    break;

                case "collapse":
                    DoExpandCollapse(step, result, expand: false);
                    break;

                case "select":
                    DoSelect(step, result);
                    break;

                case "set_range":
                    DoSetRange(step, result);
                    break;

                case "window_close":
                    DoWindowAction(step, result, "close");
                    break;

                case "window_minimize":
                    DoWindowAction(step, result, "minimize");
                    break;

                case "window_maximize":
                    DoWindowAction(step, result, "maximize");
                    break;

                default:
                    throw new InvalidOperationException($"Unknown action: {step.Action}");
            }

            if (result.Status == StepStatus.Pass || result.Status == 0)
                result.Status = StepStatus.Pass;

            // [ZOOM] Show success result
            _currentZoom?.ShowPass(result.ActionDetail ?? "done");
        }
        catch (Exception ex)
        {
            result.Status = StepStatus.Fail;
            result.Message = ex.Message;

            // [ZOOM] Show failure result
            _currentZoom?.ShowFail(ex.Message);
        }
        finally
        {
            _currentZoom?.Dispose();
            _currentZoom = null;
        }

        sw.Stop();
        result.ElapsedMs = sw.Elapsed.TotalMilliseconds;
        return result;
    }

    // ── Click actions ──────────────────────────────────────────

    private string DoClick(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element != null)
        {
            var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
            var center = UiaLocator.GetCenter(element);
            if (center != null)
                _ctx.SetActionPoint(center.Value.x, center.Value.y, step.Name, "click", elemDesc);

            // Focusless path: UIA Invoke (no focus needed — user undisturbed!)
            if (_ctx.PreferFocusless && UiaLocator.TryInvoke(element))
            {
                result.ActionDetail = $"Click {elemDesc} (Invoke, {method}, focusless)";
                Log($"  Invoked via UIA ({method}, focusless)");
                return method!;
            }

            if (center != null)
            {
                // SendInput path: pre-verify → focus → post-verify → click
                var verify = VerifiedEnsureFocus(center.Value.x, center.Value.y,
                    expectedAid: step.Target?.AutomationId);
                MouseInput.Click(center.Value.x, center.Value.y);
                result.ActionDetail = $"Click {elemDesc} ({center.Value.x},{center.Value.y}) ({method}, {verify})";
                Log($"  Clicked at ({center.Value.x},{center.Value.y}) via UIA ({method}, {verify})");
                return method!;
            }
        }

        if (step.Target?.X != null && step.Target?.Y != null)
        {
            _ctx.SetActionPoint(step.Target.X.Value, step.Target.Y.Value, step.Name, "click", "coordinate");
            // SendInput path: pre-verify → focus → post-verify → click
            var verify = VerifiedEnsureFocus(step.Target.X.Value, step.Target.Y.Value);
            MouseInput.Click(step.Target.X.Value, step.Target.Y.Value);
            result.ActionDetail = $"Click ({step.Target.X},{step.Target.Y}) (coordinate, {verify})";
            Log($"  Clicked at ({step.Target.X},{step.Target.Y}) via coordinate ({verify})");
            return "coordinate";
        }

        throw new InvalidOperationException(
            $"Cannot locate element for click: {step.Target?.AutomationId ?? step.Target?.Name ?? "(no target)"}");
    }

    private string DoDoubleClick(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element != null)
        {
            var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
            var center = UiaLocator.GetCenter(element);
            if (center != null)
            {
                _ctx.SetActionPoint(center.Value.x, center.Value.y, step.Name, "double_click", elemDesc);
                var verify = VerifiedEnsureFocus(center.Value.x, center.Value.y,
                    expectedAid: step.Target?.AutomationId);
                MouseInput.DoubleClick(center.Value.x, center.Value.y);
                result.ActionDetail = $"DblClick {elemDesc} ({center.Value.x},{center.Value.y}) ({method}, {verify})";
                return method!;
            }
        }

        if (step.Target?.X != null && step.Target?.Y != null)
        {
            _ctx.SetActionPoint(step.Target.X.Value, step.Target.Y.Value, step.Name, "double_click", "coordinate");
            var verify = VerifiedEnsureFocus(step.Target.X.Value, step.Target.Y.Value);
            MouseInput.DoubleClick(step.Target.X.Value, step.Target.Y.Value);
            result.ActionDetail = $"DblClick ({step.Target.X},{step.Target.Y}) (coordinate, {verify})";
            return "coordinate";
        }

        throw new InvalidOperationException("Cannot locate element for double_click");
    }

    private string DoRightClick(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element != null)
        {
            var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
            var center = UiaLocator.GetCenter(element);
            if (center != null)
            {
                _ctx.SetActionPoint(center.Value.x, center.Value.y, step.Name, "right_click", elemDesc);
                var verify = VerifiedEnsureFocus(center.Value.x, center.Value.y,
                    expectedAid: step.Target?.AutomationId);
                MouseInput.RightClick(center.Value.x, center.Value.y);
                result.ActionDetail = $"RightClick {elemDesc} ({center.Value.x},{center.Value.y}) ({method}, {verify})";
                return method!;
            }
        }

        if (step.Target?.X != null && step.Target?.Y != null)
        {
            _ctx.SetActionPoint(step.Target.X.Value, step.Target.Y.Value, step.Name, "right_click", "coordinate");
            var verify = VerifiedEnsureFocus(step.Target.X.Value, step.Target.Y.Value);
            MouseInput.RightClick(step.Target.X.Value, step.Target.Y.Value);
            result.ActionDetail = $"RightClick ({step.Target.X},{step.Target.Y}) (coordinate, {verify})";
            return "coordinate";
        }

        throw new InvalidOperationException("Cannot locate element for right_click");
    }

    // ── Text / Keyboard ────────────────────────────────────────

    private void DoTypeText(StepDefinition step, StepResult result)
    {
        var text = _ctx.Resolve(step.Params!.Text);
        SetActionPointToWindowCenter(step.Name, "type_text", $"type \"{text}\"");

        FlaUI.Core.AutomationElements.AutomationElement? element = null;

        // ── Tier 1: UIA Value pattern (focusless — no focus needed!) ──
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
            catch { /* UIA Value not available — fall through */ }
        }

        // ── Tier 2: WM_CHAR PostMessage (cross-process, queued to target) ──
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
                    // WM_CHAR sent but verification failed — try next tier
                    Log($"  WM_CHAR sent but verify failed (got \"{readBack}\"), trying next...");
                }
            }
            catch { /* WM_CHAR failed — fall through */ }
        }

        // ── Tier 3: EM_REPLACESEL (Edit control text insertion) ──
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
            catch { /* EM_REPLACESEL failed — fall through */ }
        }

        // ── Tier 4: SendInput Unicode (physical input, most reliable, needs focus) ──
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

    // ── Wait ───────────────────────────────────────────────────

    private void DoWait(StepDefinition step, StepResult result)
    {
        var seconds = step.Params?.Seconds ?? 1.0;
        Thread.Sleep((int)(seconds * 1000));
        result.ActionDetail = $"Wait {seconds}s";
        Log($"  Waited {seconds}s");
    }

    // ── Assert ─────────────────────────────────────────────────

    private void DoAssert(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        if (element == null)
            throw new InvalidOperationException("Cannot locate element for assert");

        // Record action point at assert target element
        var center = UiaLocator.GetCenter(element);
        if (center != null)
        {
            var elemDesc = $"{step.Target?.AutomationId ?? step.Target?.Name ?? "?"}";
            _ctx.SetActionPoint(center.Value.x, center.Value.y, step.Name, "assert", elemDesc);
        }

        var actualText = UiaLocator.GetText(element) ?? "";
        var expected = _ctx.Resolve(step.Params!.Expected);
        var assertType = step.Params.Type!;

        Log($"  Assert {assertType}: actual=\"{actualText}\" expected=\"{expected}\"");

        bool pass = assertType switch
        {
            "text_contains" => actualText.Contains(expected, StringComparison.OrdinalIgnoreCase),
            "text_equals" => actualText.Equals(expected, StringComparison.OrdinalIgnoreCase),
            "text_starts_with" => actualText.StartsWith(expected, StringComparison.OrdinalIgnoreCase),
            "text_not_empty" => !string.IsNullOrWhiteSpace(actualText),
            _ => throw new InvalidOperationException($"Unknown assert type: {assertType}")
        };

        result.Status = pass ? StepStatus.Pass : StepStatus.Fail;
        result.Message = pass
            ? $"OK: \"{actualText}\" {assertType} \"{expected}\""
            : $"FAIL: \"{actualText}\" does not {assertType} \"{expected}\"";
        result.LocatorMethod = method;
        result.ActionDetail = $"Assert {assertType} \"{expected}\" on {step.Target?.AutomationId ?? step.Target?.Name ?? "?"}";
        if (pass) result.ActionDetail += $" → \"{actualText}\"";
    }

    // ── Screenshot ─────────────────────────────────────────────

    private void DoScreenshot(StepDefinition step, StepResult result)
    {
        var filename = step.Params?.Filename ?? $"step_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var outputDir = Path.Combine("output", "screenshots");
        var fullPath = Path.Combine(outputDir, filename);

        // screenshot: set action point to window center
        SetActionPointToWindowCenter(step.Name, "screenshot", filename);

        using var bmp = ScreenCapture.CaptureWindow(_ctx.MainWindowHandle);
        ScreenCapture.SaveToFile(bmp, fullPath);

        result.ScreenshotPath = fullPath;
        result.Status = StepStatus.Pass;
        result.Message = $"Saved: {fullPath}";
        result.ActionDetail = $"Screenshot → {filename}";
        Log($"  Screenshot: {fullPath}");
    }

    // ── Scroll ─────────────────────────────────────────────────

    private void DoScroll(StepDefinition step, StepResult result)
    {
        var direction = step.Params?.Direction ?? "down";
        var amount = step.Params?.Amount ?? 3;

        // ── Try UIA Scroll pattern first (focusless!) ──
        if (_ctx.PreferFocusless && step.Target != null)
        {
            try
            {
                var (element, method) = LocateElement(step);
                EnsureInputReady(element, step.Action);
                BeginZoomForElement(element, step);
                if (element != null)
                {
                    var hAmount = FlaUI.Core.Definitions.ScrollAmount.NoAmount;
                    var vAmount = FlaUI.Core.Definitions.ScrollAmount.NoAmount;

                    switch (direction.ToLowerInvariant())
                    {
                        case "down":
                            vAmount = amount > 3
                                ? FlaUI.Core.Definitions.ScrollAmount.LargeIncrement
                                : FlaUI.Core.Definitions.ScrollAmount.SmallIncrement;
                            break;
                        case "up":
                            vAmount = amount > 3
                                ? FlaUI.Core.Definitions.ScrollAmount.LargeDecrement
                                : FlaUI.Core.Definitions.ScrollAmount.SmallDecrement;
                            break;
                        case "right":
                            hAmount = FlaUI.Core.Definitions.ScrollAmount.SmallIncrement;
                            break;
                        case "left":
                            hAmount = FlaUI.Core.Definitions.ScrollAmount.SmallDecrement;
                            break;
                    }

                    if (UiaLocator.TryScroll(element, hAmount, vAmount))
                    {
                        var elemDesc = step.Target.AutomationId ?? step.Target.Name ?? "?";
                        result.ActionDetail = $"Scroll {direction} {amount} on {elemDesc} (UIA Scroll, {method}, focusless)";
                        Log($"  Scrolled {direction} via UIA Scroll ({method}, focusless)");
                        return;
                    }
                }
            }
            catch { /* UIA Scroll not available — fall through to SendInput */ }
        }

        // ── Fallback: SendInput mouse wheel (requires focus) ──
        int clicks = direction.ToLowerInvariant() == "up" ? amount : -amount;
        EnsureFocus();
        MouseInput.Scroll(clicks);
        result.ActionDetail = $"Scroll {direction} {amount}";
        Log($"  Scrolled {direction} {amount}");
    }

    // ── UIA Pattern actions (all focusless!) ─────────────────────

    /// <summary>
    /// Toggle a checkbox or toggle button.
    /// Focusless via UIA Toggle pattern, click fallback.
    /// </summary>
    private void DoToggle(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element == null)
            throw new InvalidOperationException($"Cannot locate element for toggle: {step.Target?.AutomationId ?? step.Target?.Name ?? "(no target)"}");

        var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";

        // Read current state first
        var beforeState = UiaLocator.GetToggleState(element);
        var stateStr = beforeState?.ToString() ?? "?";

        // If specific state requested, use TrySetToggle
        if (step.Params?.Checked != null)
        {
            if (UiaLocator.TrySetToggle(element, step.Params.Checked.Value))
            {
                var afterState = UiaLocator.GetToggleState(element);
                result.ActionDetail = $"Toggle {elemDesc} → {(step.Params.Checked.Value ? "ON" : "OFF")} ({method}, focusless)";
                Log($"  Toggle set to {(step.Params.Checked.Value ? "ON" : "OFF")} via UIA ({method}, focusless) [{stateStr} → {afterState}]");
                return;
            }
        }
        else
        {
            // Just toggle (no specific target state)
            if (UiaLocator.TryToggle(element))
            {
                var afterState = UiaLocator.GetToggleState(element);
                result.ActionDetail = $"Toggle {elemDesc} ({method}, focusless) [{stateStr} → {afterState}]";
                Log($"  Toggled via UIA ({method}, focusless) [{stateStr} → {afterState}]");
                return;
            }
        }

        // Fallback: click the element
        var center = UiaLocator.GetCenter(element);
        if (center != null)
        {
            var verify = VerifiedEnsureFocus(center.Value.x, center.Value.y,
                expectedAid: step.Target?.AutomationId);
            MouseInput.Click(center.Value.x, center.Value.y);
            result.ActionDetail = $"Toggle {elemDesc} ({center.Value.x},{center.Value.y}) ({method}, click fallback, {verify})";
            Log($"  Toggled via click fallback ({verify})");
            return;
        }

        throw new InvalidOperationException($"Toggle failed: no UIA Toggle pattern and no click target for {elemDesc}");
    }

    /// <summary>
    /// Expand or collapse a tree node, combo box, or group.
    /// Focusless via UIA ExpandCollapse pattern.
    /// </summary>
    private void DoExpandCollapse(StepDefinition step, StepResult result, bool expand)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element == null)
            throw new InvalidOperationException($"Cannot locate element for {(expand ? "expand" : "collapse")}");

        var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
        var action = expand ? "Expand" : "Collapse";

        var beforeState = UiaLocator.GetExpandCollapseState(element);

        bool success = expand
            ? UiaLocator.TryExpand(element)
            : UiaLocator.TryCollapse(element);

        if (success)
        {
            var afterState = UiaLocator.GetExpandCollapseState(element);
            result.ActionDetail = $"{action} {elemDesc} ({method}, focusless) [{beforeState} → {afterState}]";
            Log($"  {action}ed via UIA ({method}, focusless) [{beforeState} → {afterState}]");
            return;
        }

        // Fallback: click + arrow key for expand, click for collapse
        var center = UiaLocator.GetCenter(element);
        if (center != null)
        {
            var verify = VerifiedEnsureFocus(center.Value.x, center.Value.y,
                expectedAid: step.Target?.AutomationId);
            MouseInput.Click(center.Value.x, center.Value.y);
            result.ActionDetail = $"{action} {elemDesc} ({center.Value.x},{center.Value.y}) ({method}, click fallback, {verify})";
            return;
        }

        throw new InvalidOperationException($"{action} failed for {elemDesc}");
    }

    /// <summary>
    /// Select an item in a list, combo, or tab.
    /// Focusless via UIA SelectionItem pattern.
    /// Params: itemText (find child by name), or target directly identifies the item.
    /// </summary>
    private void DoSelect(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element == null)
            throw new InvalidOperationException("Cannot locate element for select");

        var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";

        // If itemText specified, find the child item to select
        if (step.Params?.ItemText != null)
        {
            // Search children for the item with matching name
            var itemText = step.Params.ItemText;
            AutomationElement? item = null;

            try
            {
                var children = element.FindAllChildren();
                foreach (var child in children)
                {
                    try
                    {
                        if (string.Equals(child.Name, itemText, StringComparison.OrdinalIgnoreCase))
                        {
                            item = child;
                            break;
                        }
                    }
                    catch { }
                }
            }
            catch { }

            if (item != null && UiaLocator.TrySelect(item))
            {
                result.ActionDetail = $"Select \"{itemText}\" in {elemDesc} ({method}, focusless)";
                Log($"  Selected \"{itemText}\" via UIA SelectionItem ({method}, focusless)");
                return;
            }

            throw new InvalidOperationException($"Cannot select item \"{itemText}\" in {elemDesc}");
        }

        // Direct selection (target is the item itself)
        if (UiaLocator.TrySelect(element))
        {
            result.ActionDetail = $"Select {elemDesc} ({method}, focusless)";
            Log($"  Selected via UIA SelectionItem ({method}, focusless)");
            return;
        }

        // Fallback: click
        var center = UiaLocator.GetCenter(element);
        if (center != null)
        {
            var verify = VerifiedEnsureFocus(center.Value.x, center.Value.y,
                expectedAid: step.Target?.AutomationId);
            MouseInput.Click(center.Value.x, center.Value.y);
            result.ActionDetail = $"Select {elemDesc} ({center.Value.x},{center.Value.y}) ({method}, click fallback, {verify})";
            return;
        }

        throw new InvalidOperationException($"Select failed for {elemDesc}");
    }

    /// <summary>
    /// Set a range value (slider, progress bar, spinner).
    /// Focusless via UIA RangeValue pattern.
    /// </summary>
    private void DoSetRange(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        EnsureInputReady(element, step.Action);
        BeginZoomForElement(element, step);
        if (element == null)
            throw new InvalidOperationException("Cannot locate element for set_range");

        var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
        var value = step.Params?.Value ?? throw new InvalidOperationException("set_range requires params.value");

        var before = UiaLocator.GetRangeValueInfo(element);
        if (UiaLocator.TrySetRangeValue(element, value))
        {
            var after = UiaLocator.GetRangeValueInfo(element);
            result.ActionDetail = $"SetRange {elemDesc} → {value} ({method}, focusless) [{before?.Value} → {after?.Value}]";
            Log($"  Set range to {value} via UIA RangeValue ({method}, focusless)");
            return;
        }

        throw new InvalidOperationException($"SetRange failed for {elemDesc} (value={value}, range={before})");
    }

    /// <summary>
    /// Close, minimize, or maximize a window.
    /// Focusless via UIA Window pattern.
    /// </summary>
    private void DoWindowAction(StepDefinition step, StepResult result, string action)
    {
        var (element, method) = LocateElement(step);
        if (element == null)
            throw new InvalidOperationException($"Cannot locate element for window_{action}");

        var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";

        bool success = action switch
        {
            "close" => UiaLocator.TryWindowClose(element),
            "minimize" => UiaLocator.TrySetWindowState(element,
                FlaUI.Core.Definitions.WindowVisualState.Minimized),
            "maximize" => UiaLocator.TrySetWindowState(element,
                FlaUI.Core.Definitions.WindowVisualState.Maximized),
            _ => false
        };

        if (success)
        {
            result.ActionDetail = $"Window {action} {elemDesc} ({method}, focusless)";
            Log($"  Window {action} via UIA ({method}, focusless)");
            return;
        }

        // Fallback for close: Alt+F4
        if (action == "close")
        {
            EnsureFocus();
            KeyboardInput.Hotkey(new[] { "alt", "F4" });
            result.ActionDetail = $"Window close {elemDesc} (Alt+F4 fallback)";
            Log($"  Window close via Alt+F4 fallback");
            return;
        }

        throw new InvalidOperationException($"Window {action} failed for {elemDesc}");
    }

    // ── Element location (5-tier chain) ────────────────────────

    /// <summary>
    /// 5-tier element locator chain:
    ///   1. UIA (AutomationId → Name → ControlType)
    ///   2. Vision Cache (class_path + description + window_size)
    ///   3. Simple OCR (Windows.Media.Ocr text matching — free, offline)
    ///   4. Vision API (screenshot + Claude API → cache result, expensive)
    ///   5. Coordinate (step.Target.X/Y)
    /// Returns (UIA element if found, locator method string).
    /// For Vision/OCR/Coordinate hits, sets step.Target.X/Y for SendInput.
    /// </summary>
    private (AutomationElement? element, string? method) LocateElement(StepDefinition step)
    {
        if (step.Target == null) return (null, null);

        // ── Tier 1: UIA (Accessibility) ─────────────────────
        var (element, method) = _uia.FindElement(
            _ctx.MainWindowHandle,
            step.Target.AutomationId,
            step.Target.Name,
            step.Target.ControlType);

        if (element != null)
        {
            // OCR Preview: even if UIA succeeds, run OCR for benchmarking/data collection
            if (_ctx.OcrPreview && _simpleOcr != null && !string.IsNullOrEmpty(step.Target.Description))
            {
                RunOcrPreview(step);
            }
            return (element, method);
        }

        // ── Tier 2+3+4: Vision/OCR (only if enabled + description available) ──
        if ((_ctx.VisionEnabled || _ctx.OcrPreview) && !string.IsNullOrEmpty(step.Target.Description))
        {
            var visionCoords = TryVisionLocate(step);
            if (visionCoords != null)
            {
                // Set coordinates on the step target for SendInput
                step.Target.X = visionCoords.Value.x;
                step.Target.Y = visionCoords.Value.y;
                return (null, visionCoords.Value.method);
            }
        }

        // ── Tier 5: Coordinate (already set in YAML) ────────
        // Returned as (null, null) — caller checks step.Target.X/Y
        return (null, null);
    }

    /// <summary>
    /// Try Vision Cache → Simple OCR → Vision API (Claude) fallback chain.
    /// Returns absolute screen coordinates if found.
    /// Saves debug screenshots to vision_cache_dir for future Claude review.
    /// </summary>
    private (int x, int y, string method)? TryVisionLocate(StepDefinition step)
    {
        if (step.Target?.Description == null) return null;

        var rect = WindowFinder.GetWindowRect(_ctx.MainWindowHandle);
        int winW = rect.Width;
        int winH = rect.Height;

        // Get class path for cache key
        string classPath;
        try
        {
            classPath = WindowFinder.GetWindowHierarchyPathAtPoint(
                _ctx.MainWindowHandle, rect.Left + winW / 2, rect.Top + winH / 2);
        }
        catch { classPath = "unknown"; }

        // ── Tier 2: Vision Cache ────────────────────────────
        if (_visionCache != null)
        {
            var cached = _visionCache.Get(classPath, step.Target.Description, winW, winH);
            if (cached != null)
            {
                var (absX, absY) = cached.ToAbsolute(rect.Left, rect.Top, winW, winH);
                Log($"  Vision cache hit: ({absX},{absY}) conf={cached.Confidence:F2} hits={cached.HitCount}");
                return (absX, absY, $"vision_cache, conf={cached.Confidence:F2}");
            }
        }

        // ── Capture screenshot once (shared by OCR + Vision API) ──
        System.Drawing.Bitmap? bmp = null;
        string? screenshotPath = null;
        try
        {
            bmp = ScreenCapture.CaptureWindow(_ctx.MainWindowHandle);

            // Save debug screenshot for future review by Claude/human
            screenshotPath = SaveVisionScreenshot(bmp, step.Target.Description);
            if (screenshotPath != null)
                Log($"  Vision screenshot: {screenshotPath}");
        }
        catch (Exception ex)
        {
            Log($"  Vision capture error: {ex.Message}");
        }

        // ── Tier 2.5: OcrSegmentCache — form-level dynamic a11y tree ──────
        // Build full-form segments ONCE; reuse for all element lookups.
        // Form hash detects UI changes → auto-rebuild on mismatch.
        if (_simpleOcr != null && bmp != null)
        {
            try
            {
                var formHash = SimpleOcrAnalyzer.ComputeFormHash(bmp);

                // Check disk cache first
                var cachedEntry = _segmentCache?.LoadIfFresh(classPath, formHash, winW, winH);
                List<OcrSegment>? segments = cachedEntry?.Segments;

                if (segments == null)
                {
                    // Cache miss or stale — rebuild from OCR
                    Log($"  OcrSeg: building segments (hash={formHash[..8]})...");
                    segments = _simpleOcr.SegmentAll(bmp).GetAwaiter().GetResult();

                    if (_segmentCache != null)
                    {
                        _segmentCache.Save(classPath, winW, winH, new OcrSegmentCacheEntry
                        {
                            FormHash = formHash,
                            BuildAt = DateTime.UtcNow,
                            WindowWidth = winW,
                            WindowHeight = winH,
                            Segments = segments
                        });
                        Log($"  OcrSeg: cached {segments.Count} segments");
                    }
                }

                var match = OcrSegmentCache.BestMatch(segments, step.Target.Description);
                if (match != null)
                {
                    int absX = rect.Left + (int)(match.RelX * winW);
                    int absY = rect.Top + (int)(match.RelY * winH);

                    // Cross-populate VisionCache for fast per-element hits next time
                    if (_visionCache != null)
                    {
                        var entry = VisionCacheEntry.FromAbsolute(
                            classPath, step.Target.Description,
                            winW, winH, absX, absY, rect.Left, rect.Top,
                            (int)(match.RelW * winW), (int)(match.RelH * winH),
                            match.Confidence, match.Text, match.ControlType ?? "OcrSeg");
                        _visionCache.Put(classPath, step.Target.Description, winW, winH, entry);
                    }

                    var cacheTag = cachedEntry != null ? "ocr_seg_cache" : "ocr_seg";
                    Log($"  {cacheTag}: found \"{match.Text}\" at ({absX},{absY}) conf={match.Confidence:F2} src={match.Source}");
                    return (absX, absY, $"{cacheTag}, conf={match.Confidence:F2}, \"{match.Text}\"");
                }

                Log($"  OcrSeg: \"{step.Target.Description}\" not found in {segments.Count} segments");

                // ── Tier 3: Ask Vision AI (Gemini) — returns OcrSegment with coords from JSON ──
                // Gemini identifies element AND provides x/y/w/h directly → no BestMatch step.
                if (AskVisionFn != null)
                {
                    try
                    {
                        Log($"  VisionAsk: asking Gemini to identify '{step.Target.Description}'...");
                        var seg = AskVisionFn(bmp, step.Target.Description).GetAwaiter().GetResult();
                        if (seg != null)
                        {
                            Log($"  VisionAsk: Gemini found '{seg.Text}' at ({seg.RelX:F2},{seg.RelY:F2})");
                            int absX = rect.Left + (int)(seg.RelX * winW);
                            int absY = rect.Top + (int)(seg.RelY * winH);

                            // Teach segment cache with Gemini-provided coords
                            if (_segmentCache != null)
                            {
                                var formHash2 = SimpleOcrAnalyzer.ComputeFormHash(bmp);
                                _segmentCache.LearnSegment(classPath, formHash2, winW, winH, seg);

                                // Save blob crop: {pixelHash}={label}.png
                                int cx = Math.Max(0, (int)((seg.RelX - seg.RelW / 2) * bmp.Width));
                                int cy = Math.Max(0, (int)((seg.RelY - seg.RelH / 2) * bmp.Height));
                                int cw = Math.Min(Math.Max((int)(seg.RelW * bmp.Width), 4), bmp.Width - cx);
                                int ch = Math.Min(Math.Max((int)(seg.RelH * bmp.Height), 4), bmp.Height - cy);
                                if (cw > 4 && ch > 4)
                                {
                                    try
                                    {
                                        using var crop = bmp.Clone(new System.Drawing.Rectangle(cx, cy, cw, ch), bmp.PixelFormat);
                                        var blobPath = _segmentCache.SaveBlob(crop, seg.Text);
                                        if (blobPath != null) Log($"  VisionAsk: blob saved {Path.GetFileName(blobPath)}");
                                    }
                                    catch { }
                                }
                            }

                            if (_visionCache != null)
                            {
                                var entry = VisionCacheEntry.FromAbsolute(
                                    classPath, step.Target.Description,
                                    winW, winH, absX, absY, rect.Left, rect.Top,
                                    (int)(seg.RelW * winW), (int)(seg.RelH * winH),
                                    seg.Confidence, seg.Text, "Gemini");
                                _visionCache.Put(classPath, step.Target.Description, winW, winH, entry);
                            }

                            return (absX, absY, $"vision_ask, conf={seg.Confidence:F2}, \"{seg.Text}\"");
                        }
                    }
                    catch (Exception ex) { Log($"  VisionAsk error: {ex.Message}"); }
                }
            }
            catch (Exception ex)
            {
                Log($"  OcrSeg error: {ex.Message}");
            }
        }

        // ── Tier 4: Vision API (Claude — expensive, last resort) ──
        if (_visionAnalyzer != null && bmp != null)
        {
            try
            {
                Log($"  Vision API: querying Claude for \"{step.Target.Description}\"...");

                var location = _visionAnalyzer.FindElement(bmp, step.Target.Description)
                    .GetAwaiter().GetResult();

                if (location != null)
                {
                    int absX = rect.Left + location.CenterX;
                    int absY = rect.Top + location.CenterY;

                    // Cross-populate both caches (경험치 축적!)
                    if (_visionCache != null)
                    {
                        var entry = VisionCacheEntry.FromAbsolute(
                            classPath, step.Target.Description,
                            winW, winH, absX, absY, rect.Left, rect.Top,
                            location.Width, location.Height,
                            location.Confidence, location.Label, location.ControlType);
                        _visionCache.Put(classPath, step.Target.Description, winW, winH, entry);
                    }

                    // Also teach OcrSegmentCache so this element is found next time without Vision API
                    if (_segmentCache != null && bmp != null)
                    {
                        var formHash = SimpleOcrAnalyzer.ComputeFormHash(bmp);
                        var seg = new OcrSegment
                        {
                            Text = location.Label ?? step.Target.Description,
                            RelX = winW > 0 ? (double)(absX - rect.Left) / winW : 0,
                            RelY = winH > 0 ? (double)(absY - rect.Top) / winH : 0,
                            RelW = winW > 0 ? (double)location.Width / winW : 0,
                            RelH = winH > 0 ? (double)location.Height / winH : 0,
                            Confidence = location.Confidence,
                            Source = "vision_api",
                            ControlType = location.ControlType
                        };
                        _segmentCache.LearnSegment(classPath, formHash, winW, winH, seg);

                        // Save element crop as {pixelHash}={label}.png for pixel-based fast lookup
                        if (location.Width > 4 && location.Height > 4)
                        {
                            try
                            {
                                int cx = location.CenterX - location.Width / 2;
                                int cy = location.CenterY - location.Height / 2;
                                int cw = Math.Min(location.Width, bmp.Width - Math.Max(0, cx));
                                int ch = Math.Min(location.Height, bmp.Height - Math.Max(0, cy));
                                cx = Math.Max(0, cx); cy = Math.Max(0, cy);
                                if (cw > 0 && ch > 0)
                                {
                                    using var crop = bmp.Clone(
                                        new System.Drawing.Rectangle(cx, cy, cw, ch), bmp.PixelFormat);
                                    var blobPath = _segmentCache.SaveBlob(crop,
                                        location.Label ?? step.Target.Description);
                                    if (blobPath != null)
                                        Log($"  Vision API: blob saved {Path.GetFileName(blobPath)}");
                                }
                            }
                            catch { /* best-effort */ }
                        }
                    }

                    Log($"  Vision API: found at ({absX},{absY}) conf={location.Confidence:F2}");
                    return (absX, absY, $"vision_api, conf={location.Confidence:F2}");
                }
                else
                {
                    Log($"  Vision API: element not found or low confidence");
                }
            }
            catch (Exception ex)
            {
                Log($"  Vision API error: {ex.Message}");
            }
        }

        bmp?.Dispose();
        return null;
    }

    /// <summary>
    /// Save a debug screenshot for Vision fallback analysis.
    /// Returns the saved file path, or null if saving failed.
    /// Future Claude sessions can review these to understand OCR/Vision failures.
    /// </summary>
    private string? SaveVisionScreenshot(System.Drawing.Bitmap bmp, string description)
    {
        try
        {
            var dir = Path.Combine(_ctx.VisionCacheDir, "screenshots");
            Directory.CreateDirectory(dir);

            // Sanitize description for filename
            var safeName = string.Join("_",
                description.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries))
                .Replace(' ', '_');
            if (safeName.Length > 40) safeName = safeName[..40];

            var timestamp = DateTime.Now.ToString("HHmmss_fff");
            var filename = $"{timestamp}_{safeName}.png";
            var path = Path.Combine(dir, filename);

            bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            return path;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// OCR Preview: run OCR in background even when UIA succeeds.
    /// Compares UIA text vs OCR text for accuracy benchmarking.
    /// Does NOT affect actual element location — purely informational.
    /// </summary>
    private void RunOcrPreview(StepDefinition step)
    {
        try
        {
            using var bmp = ScreenCapture.CaptureWindow(_ctx.MainWindowHandle);
            var screenshotPath = SaveVisionScreenshot(bmp, step.Target!.Description!);

            // Get UIA text for comparison
            string? uiaText = null;
            try
            {
                var (uiaElem, _) = _uia.FindElement(
                    _ctx.MainWindowHandle,
                    step.Target.AutomationId,
                    step.Target.Name,
                    step.Target.ControlType);
                if (uiaElem != null)
                    uiaText = uiaElem.Properties.Name.ValueOrDefault;
            }
            catch { /* ignore */ }

            // Run OCR full recognition
            var fullResult = _simpleOcr!.RecognizeAll(bmp).GetAwaiter().GetResult();

            // Run OCR element matching
            var ocrMatch = _simpleOcr.FindElement(bmp, step.Target.Description!)
                .GetAwaiter().GetResult();

            if (ocrMatch != null)
            {
                // Calculate UIA vs OCR match rate
                string matchInfo = "";
                if (!string.IsNullOrEmpty(uiaText))
                {
                    var rate = CalculateTextMatchRate(uiaText, ocrMatch.MatchedText);
                    matchInfo = rate >= 0.9
                        ? $" | UIA\u2194OCR={rate:P0} \u2605"
                        : $" | UIA\u2194OCR={rate:P0}";
                }

                Log($"  [OCR PREVIEW] found \"{ocrMatch.MatchedText}\" ({ocrMatch.X},{ocrMatch.Y}) conf={ocrMatch.Confidence:F2} [{ocrMatch.MatchType}]{matchInfo}");
            }
            else
            {
                var textSnippet = fullResult.FullText.Length > 80
                    ? fullResult.FullText[..80] + "..."
                    : fullResult.FullText;
                Log($"  [OCR PREVIEW] no match for \"{step.Target.Description}\" | OCR saw: \"{textSnippet.Replace('\n', ' ')}\"");
            }

            // Summary line
            Log($"  [OCR PREVIEW] {fullResult.Words.Count} words | UIA=\"{uiaText ?? "(none)"}\" | img={screenshotPath ?? "n/a"}");
        }
        catch (Exception ex)
        {
            Log($"  [OCR PREVIEW] error: {ex.Message}");
        }
    }

    /// <summary>
    /// Calculate similarity between UIA text and OCR text (0.0~1.0).
    /// </summary>
    private static double CalculateTextMatchRate(string uiaText, string ocrText)
    {
        var a = uiaText.Trim().ToLowerInvariant();
        var b = ocrText.Trim().ToLowerInvariant();

        if (a == b) return 1.0;
        if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b)) return 0;

        // Contains check
        if (a.Contains(b) || b.Contains(a))
        {
            double shorter = Math.Min(a.Length, b.Length);
            double longer = Math.Max(a.Length, b.Length);
            return shorter / longer;
        }

        // Character overlap (Dice coefficient)
        var setA = new HashSet<char>(a.Where(c => !char.IsWhiteSpace(c)));
        var setB = new HashSet<char>(b.Where(c => !char.IsWhiteSpace(c)));
        if (setA.Count == 0 || setB.Count == 0) return 0;

        int intersection = setA.Intersect(setB).Count();
        return 2.0 * intersection / (setA.Count + setB.Count);
    }

    // ── Pre/Post Action Verification ────────────────────────

    // ── Pre/Post Action Verification ────────────────────────

    /// <summary>
    /// Snapshot of element + overlay state at a screen coordinate.
    /// </summary>
    private sealed class TargetSnapshot
    {
        public string ControlType { get; init; } = "";
        public string Name { get; init; } = "";
        public string AutomationId { get; init; } = "";
        public int BoundsX { get; init; }
        public int BoundsY { get; init; }
        public int BoundsW { get; init; }
        public int BoundsH { get; init; }
        public bool PointInsideBounds { get; init; }
        public bool OverlayDetected { get; init; }
        public string? OverlayClass { get; init; }
        public int X { get; init; }
        public int Y { get; init; }
        public DateTime Timestamp { get; init; }
    }

    /// <summary>
    /// Take a UIA + overlay snapshot at screen coordinates.
    ///
    /// Checks:
    ///   1. UIA element at (x,y) — identity and properties
    ///   2. Point inside element BoundingRectangle — 좌표가 렉트 안쪽인지
    ///   3. WindowFromPoint — 방해하는 창이 없는지 (overlay detection)
    /// </summary>
    private TargetSnapshot? SnapshotAt(int x, int y)
    {
        try
        {
            // UIA element at point
            var info = _uia.GetElementAtPoint(x, y);
            if (info == null) return null;

            // Point inside bounding rect?
            bool inside = x >= info.BoundsX && x < info.BoundsX + info.BoundsW
                       && y >= info.BoundsY && y < info.BoundsY + info.BoundsH;

            // Overlay detection: WindowFromPoint should return our target window
            bool overlayDetected = false;
            string? overlayClass = null;

            if (_ctx.MainWindowHandle != IntPtr.Zero)
            {
                var pt = new POINT { X = x, Y = y };
                var topWnd = NativeMethods.WindowFromPoint(pt);
                if (topWnd != IntPtr.Zero)
                {
                    // Walk up to top-level to compare with our main window
                    var topLevel = topWnd;
                    IntPtr parent;
                    while ((parent = NativeMethods.GetParent(topLevel)) != IntPtr.Zero)
                        topLevel = parent;

                    if (topLevel != _ctx.MainWindowHandle)
                    {
                        overlayDetected = true;
                        overlayClass = WindowFinder.GetClassName(topWnd);
                    }
                }
            }

            return new TargetSnapshot
            {
                ControlType = info.ControlType,
                Name = info.Name,
                AutomationId = info.AutomationId,
                BoundsX = info.BoundsX, BoundsY = info.BoundsY,
                BoundsW = info.BoundsW, BoundsH = info.BoundsH,
                PointInsideBounds = inside,
                OverlayDetected = overlayDetected,
                OverlayClass = overlayClass,
                X = x, Y = y,
                Timestamp = DateTime.Now
            };
        }
        catch { return null; }
    }

    /// <summary>
    /// Pre/Post verification around EnsureFocus + SendInput.
    ///
    /// Flow:
    ///   1. Pre-snap: UIA + overlay at target coords (before focus change)
    ///   2. EnsureFocus (may cause window switch)
    ///   3. Post-snap: UIA + overlay at target coords (after focus)
    ///   4. Compare:
    ///      - Same element? (type + automationId)
    ///      - Point inside element rect? (좌표가 렉트 안)
    ///      - No overlay? (방해 창 없음)
    ///      → All OK → "verify=OK"
    ///      → Warning → "verify=WARN(...)" + [VERIFY] log
    ///
    /// Never blocks execution — only logs warnings.
    /// </summary>
    private string VerifiedEnsureFocus(int x, int y, string? expectedName = null, string? expectedAid = null)
    {
        // Pre-snap (before focus)
        var pre = SnapshotAt(x, y);

        // Focus
        EnsureFocus();

        // Post-snap (after focus)
        var post = SnapshotAt(x, y);

        // ── Analyze results ──
        var warnings = new List<string>();

        if (pre == null && post == null)
            return "verify=skip(no_element)";

        // Use post-snap (after focus = actual state at click time)
        var snap = post ?? pre;

        // Check 1: Overlay — 방해하는 창 없나?
        if (snap!.OverlayDetected)
        {
            warnings.Add($"overlay({snap.OverlayClass})");
            LogVerify($"Overlay detected at ({x},{y}): class=\"{snap.OverlayClass}\" blocking target window");
        }

        // Check 2: Point inside bounds — 좌표가 렉트 안인가?
        if (!snap.PointInsideBounds)
        {
            warnings.Add("outside_bounds");
            LogVerify($"Click point ({x},{y}) outside element rect: [{snap.ControlType}]\"{snap.Name}\" at ({snap.BoundsX},{snap.BoundsY} {snap.BoundsW}x{snap.BoundsH})");
        }

        // Check 3: Expected element match
        if (expectedAid != null && !string.IsNullOrEmpty(snap.AutomationId) && snap.AutomationId != expectedAid)
        {
            // Pattern matching support: check if expectedAid is a pattern
            if (PatternMatcher.IsPattern(expectedAid))
            {
                var matcher = PatternMatcher.Create(expectedAid);
                if (!matcher.IsMatch(snap.AutomationId))
                {
                    warnings.Add($"aid_mismatch");
                    LogVerify($"Element mismatch: expected aid=\"{expectedAid}\" but found aid=\"{snap.AutomationId}\" [{snap.ControlType}]");
                }
            }
            else
            {
                warnings.Add($"aid_mismatch");
                LogVerify($"Element mismatch: expected aid=\"{expectedAid}\" but found aid=\"{snap.AutomationId}\" [{snap.ControlType}]");
            }
        }

        // Check 4: Pre vs Post stability
        if (pre != null && post != null)
        {
            bool sameElement = pre.ControlType == post.ControlType
                && pre.AutomationId == post.AutomationId;

            if (!sameElement)
            {
                warnings.Add("element_changed");
                LogVerify($"Element changed after focus: [{pre.ControlType}]\"{pre.Name}\" → [{post.ControlType}]\"{post.Name}\"");
            }
            else if (pre.Name != post.Name)
            {
                // Same element, name changed — likely state change (not a warning, just info)
                Log($"  [VERIFY] Name changed: \"{pre.Name}\" → \"{post.Name}\" (state change)");
            }
        }

        if (warnings.Count == 0)
            return "verify=OK";

        return $"verify=WARN({string.Join(",", warnings)})";
    }

    private void LogVerify(string msg)
    {
        var consoleLock = _ctx.ConsoleLock ?? new object();
        lock (consoleLock)
        {
            int w;
            try { w = Console.WindowWidth; } catch { w = 120; }
            Console.Write("\r" + new string(' ', Math.Max(w - 1, 80)) + "\r");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("[VERIFY] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Set action point to the center of the test target window.
    /// Used for non-positional actions (type_text, hotkey, etc.) that affect the focused window.
    /// </summary>
    private void SetActionPointToWindowCenter(string stepName, string action, string? desc = null)
    {
        try
        {
            var rect = WKAppBot.Win32.Window.WindowFinder.GetWindowRect(_ctx.MainWindowHandle);
            int cx = rect.Left + rect.Width / 2;
            int cy = rect.Top + rect.Height / 2;
            _ctx.SetActionPoint(cx, cy, stepName, action, desc);
        }
        catch { /* ignore — non-critical */ }
    }

    private void Log(string msg)
    {
        if (_verbose) Console.WriteLine(msg);
    }

    public void Dispose()
    {
        _uia.Dispose();
    }
}
