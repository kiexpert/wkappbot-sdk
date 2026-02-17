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

    public ActionExecutor(RuntimeContext ctx, bool verbose = false,
                          VisionCache? visionCache = null, VisionAnalyzer? visionAnalyzer = null,
                          SimpleOcrAnalyzer? simpleOcr = null)
    {
        _ctx = ctx;
        _verbose = verbose;
        _uia = new UiaLocator();
        _visionCache = visionCache;
        _visionAnalyzer = visionAnalyzer;
        _simpleOcr = simpleOcr;
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

        // ── Tier 3: Simple OCR (Windows.Media.Ocr — free, offline) ──
        if (_simpleOcr != null && bmp != null)
        {
            try
            {
                Log($"  Simple OCR: searching for \"{step.Target.Description}\"...");

                var ocrMatch = _simpleOcr.FindElement(bmp, step.Target.Description)
                    .GetAwaiter().GetResult();

                if (ocrMatch != null)
                {
                    int absX = rect.Left + ocrMatch.X;
                    int absY = rect.Top + ocrMatch.Y;

                    // Cache the result (경험치 축적!)
                    if (_visionCache != null)
                    {
                        var entry = VisionCacheEntry.FromAbsolute(
                            classPath, step.Target.Description,
                            winW, winH, absX, absY, rect.Left, rect.Top,
                            ocrMatch.Width, ocrMatch.Height,
                            ocrMatch.Confidence, ocrMatch.MatchedText, "OcrText");

                        _visionCache.Put(classPath, step.Target.Description, winW, winH, entry);
                    }

                    Log($"  Simple OCR: found \"{ocrMatch.MatchedText}\" at ({absX},{absY}) conf={ocrMatch.Confidence:F2} [{ocrMatch.MatchType}]");
                    return (absX, absY, $"simple_ocr, conf={ocrMatch.Confidence:F2}, \"{ocrMatch.MatchedText}\"");
                }
                else
                {
                    Log($"  Simple OCR: no matching text found");
                }
            }
            catch (Exception ex)
            {
                Log($"  Simple OCR error: {ex.Message}");
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
