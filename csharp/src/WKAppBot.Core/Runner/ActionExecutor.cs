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
/// 4-tier Chain of Responsibility:
///   1. UIA (Accessibility) — fast, reliable
///   2. Vision Cache — cached API results (경험치!)
///   3. Vision API — Claude screenshot analysis (expensive, cached)
///   4. Coordinate — raw x,y fallback
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

    public ActionExecutor(RuntimeContext ctx, bool verbose = false,
                          VisionCache? visionCache = null, VisionAnalyzer? visionAnalyzer = null)
    {
        _ctx = ctx;
        _verbose = verbose;
        _uia = new UiaLocator();
        _visionCache = visionCache;
        _visionAnalyzer = visionAnalyzer;
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

                default:
                    throw new InvalidOperationException($"Unknown action: {step.Action}");
            }

            if (result.Status == StepStatus.Pass || result.Status == 0)
                result.Status = StepStatus.Pass;
        }
        catch (Exception ex)
        {
            result.Status = StepStatus.Fail;
            result.Message = ex.Message;
        }

        sw.Stop();
        result.ElapsedMs = sw.Elapsed.TotalMilliseconds;
        return result;
    }

    // ── Click actions ──────────────────────────────────────────

    private string DoClick(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
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
                // SendInput path: focus required
                EnsureFocus();
                MouseInput.Click(center.Value.x, center.Value.y);
                result.ActionDetail = $"Click {elemDesc} ({center.Value.x},{center.Value.y}) ({method})";
                Log($"  Clicked at ({center.Value.x},{center.Value.y}) via UIA ({method})");
                return method!;
            }
        }

        if (step.Target?.X != null && step.Target?.Y != null)
        {
            _ctx.SetActionPoint(step.Target.X.Value, step.Target.Y.Value, step.Name, "click", "coordinate");
            // SendInput path: focus required
            EnsureFocus();
            MouseInput.Click(step.Target.X.Value, step.Target.Y.Value);
            result.ActionDetail = $"Click ({step.Target.X},{step.Target.Y}) (coordinate)";
            Log($"  Clicked at ({step.Target.X},{step.Target.Y}) via coordinate");
            return "coordinate";
        }

        throw new InvalidOperationException(
            $"Cannot locate element for click: {step.Target?.AutomationId ?? step.Target?.Name ?? "(no target)"}");
    }

    private string DoDoubleClick(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        if (element != null)
        {
            var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
            var center = UiaLocator.GetCenter(element);
            if (center != null)
            {
                _ctx.SetActionPoint(center.Value.x, center.Value.y, step.Name, "double_click", elemDesc);
                EnsureFocus();  // SendInput required
                MouseInput.DoubleClick(center.Value.x, center.Value.y);
                result.ActionDetail = $"DblClick {elemDesc} ({center.Value.x},{center.Value.y}) ({method})";
                return method!;
            }
        }

        if (step.Target?.X != null && step.Target?.Y != null)
        {
            _ctx.SetActionPoint(step.Target.X.Value, step.Target.Y.Value, step.Name, "double_click", "coordinate");
            EnsureFocus();  // SendInput required
            MouseInput.DoubleClick(step.Target.X.Value, step.Target.Y.Value);
            result.ActionDetail = $"DblClick ({step.Target.X},{step.Target.Y}) (coordinate)";
            return "coordinate";
        }

        throw new InvalidOperationException("Cannot locate element for double_click");
    }

    private string DoRightClick(StepDefinition step, StepResult result)
    {
        var (element, method) = LocateElement(step);
        if (element != null)
        {
            var elemDesc = step.Target?.AutomationId ?? step.Target?.Name ?? "?";
            var center = UiaLocator.GetCenter(element);
            if (center != null)
            {
                _ctx.SetActionPoint(center.Value.x, center.Value.y, step.Name, "right_click", elemDesc);
                EnsureFocus();  // SendInput required
                MouseInput.RightClick(center.Value.x, center.Value.y);
                result.ActionDetail = $"RightClick {elemDesc} ({center.Value.x},{center.Value.y}) ({method})";
                return method!;
            }
        }

        if (step.Target?.X != null && step.Target?.Y != null)
        {
            _ctx.SetActionPoint(step.Target.X.Value, step.Target.Y.Value, step.Name, "right_click", "coordinate");
            EnsureFocus();  // SendInput required
            MouseInput.RightClick(step.Target.X.Value, step.Target.Y.Value);
            result.ActionDetail = $"RightClick ({step.Target.X},{step.Target.Y}) (coordinate)";
            return "coordinate";
        }

        throw new InvalidOperationException("Cannot locate element for right_click");
    }

    // ── Text / Keyboard ────────────────────────────────────────

    private void DoTypeText(StepDefinition step, StepResult result)
    {
        var text = _ctx.Resolve(step.Params!.Text);
        SetActionPointToWindowCenter(step.Name, "type_text", $"type \"{text}\"");

        // Focusless attempt: UIA Value pattern (no focus needed!)
        if (_ctx.PreferFocusless && step.Target != null)
        {
            try
            {
                var (element, _) = LocateElement(step);
                if (element?.Patterns.Value.IsSupported == true)
                {
                    element.Patterns.Value.Pattern.SetValue(text);
                    result.ActionDetail = $"Type \"{text}\" (UIA Value, focusless)";
                    Log($"  Typed via UIA Value: \"{text}\" (focusless)");
                    return;
                }
            }
            catch { /* UIA Value not available — fall through to SendInput */ }
        }

        // SendInput path: focus required
        EnsureFocus();
        KeyboardInput.TypeText(text);
        result.ActionDetail = $"Type \"{text}\"";
        Log($"  Typed: \"{text}\"");
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
        int clicks = direction.ToLowerInvariant() == "up" ? amount : -amount;
        EnsureFocus();  // SendInput required
        MouseInput.Scroll(clicks);
        result.ActionDetail = $"Scroll {direction} {amount}";
        Log($"  Scrolled {direction} {amount}");
    }

    // ── Element location (4-tier chain) ────────────────────────

    /// <summary>
    /// 4-tier element locator chain:
    ///   1. UIA (AutomationId → Name → ControlType)
    ///   2. Vision Cache (class_path + description + window_size)
    ///   3. Vision API (screenshot + Claude API → cache result)
    ///   4. Coordinate (step.Target.X/Y)
    /// Returns (UIA element if found, locator method string).
    /// For Vision/Coordinate hits, sets step.Target.X/Y for SendInput.
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

        if (element != null) return (element, method);

        // ── Tier 2+3: Vision (only if enabled + description available) ──
        if (_ctx.VisionEnabled && !string.IsNullOrEmpty(step.Target.Description))
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

        // ── Tier 4: Coordinate (already set in YAML) ────────
        // Returned as (null, null) — caller checks step.Target.X/Y
        return (null, null);
    }

    /// <summary>
    /// Try Vision Cache → Vision API fallback.
    /// Returns absolute screen coordinates if found.
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

        // ── Tier 3: Vision API (Claude) ─────────────────────
        if (_visionAnalyzer != null)
        {
            try
            {
                Log($"  Vision API: querying for \"{step.Target.Description}\"...");

                // Capture window screenshot (focusless!)
                using var bmp = ScreenCapture.CaptureWindow(_ctx.MainWindowHandle);
                var location = _visionAnalyzer.FindElement(bmp, step.Target.Description)
                    .GetAwaiter().GetResult();

                if (location != null)
                {
                    // Convert screenshot-relative coords to absolute screen coords
                    int absX = rect.Left + location.CenterX;
                    int absY = rect.Top + location.CenterY;

                    // Cache the result (경험치 축적!)
                    if (_visionCache != null)
                    {
                        var entry = VisionCacheEntry.FromAbsolute(
                            classPath, step.Target.Description,
                            winW, winH, absX, absY, rect.Left, rect.Top,
                            location.Width, location.Height,
                            location.Confidence, location.Label, location.ControlType);

                        _visionCache.Put(classPath, step.Target.Description, winW, winH, entry);
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

        return null;
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
