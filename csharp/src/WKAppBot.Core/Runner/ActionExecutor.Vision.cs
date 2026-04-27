using WKAppBot.Core.Scenario;
using WKAppBot.Vision;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;

namespace WKAppBot.Core.Runner;

public sealed partial class ActionExecutor
{
    // -- Element location (5-tier chain) ------------------------

    /// <summary>
    /// 5-tier element locator chain:
    ///   1. UIA (AutomationId -> Name -> ControlType)
    ///   2. Vision Cache (class_path + description + window_size)
    ///   3. Simple OCR (Windows.Media.Ocr text matching -- free, offline)
    ///   4. Vision API (screenshot + Claude API -> cache result, expensive)
    ///   5. Coordinate (step.Target.X/Y)
    /// Returns (UIA element if found, locator method string).
    /// For Vision/OCR/Coordinate hits, sets step.Target.X/Y for SendInput.
    /// </summary>
    private (FlaUI.Core.AutomationElements.AutomationElement? element, string? method) LocateElement(StepDefinition step)
    {
        if (step.Target == null) return (null, null);

        // -- Tier 1: UIA (Accessibility) -- UiaLocator handles ";" OR internally --
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

        // -- Tier 2+3+4: Vision/OCR (only if enabled + description available) --
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

        // -- Tier 5: Coordinate (already set in YAML) --------
        // Returned as (null, null) -- caller checks step.Target.X/Y
        return (null, null);
    }

    /// <summary>
    /// Try Vision Cache -> Simple OCR -> Vision API (Claude) fallback chain.
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

        // -- Tier 2: Vision Cache ----------------------------
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

        // -- Capture screenshot once (shared by OCR + Vision API) --
        System.Drawing.Bitmap? bmp = null;
        string? screenshotPath = null;
        try
        {
            bmp = ScreenCapture.CaptureWindow(_ctx.MainWindowHandle, new WKAppBot.Win32.Input.CaptureOptions
            {
                RejectBlank = true,
                StepLogger = Log,
            });

            // Save debug screenshot for future review by Claude/human
            if (bmp != null)
            {
                screenshotPath = SaveVisionScreenshot(bmp, step.Target.Description);
                if (screenshotPath != null)
                    Log($"  Vision screenshot: {screenshotPath}");
            }
            else
            {
                Log("  Vision capture: blank/failed -- skipping OCR+Vision for this step");
            }
        }
        catch (Exception ex)
        {
            Log($"  Vision capture error: {ex.Message}");
        }

        // -- Tier 2.5: OcrSegmentCache -- form-level dynamic a11y tree ------
        // Build full-form segments ONCE; reuse for all element lookups.
        // Form hash detects UI changes -> auto-rebuild on mismatch.
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
                    // Cache miss or stale -- rebuild from OCR
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

                // -- Tier 3: Ask Vision AI (Gemini) -- returns OcrSegment with coords from JSON --
                // Gemini identifies element AND provides x/y/w/h directly -> no BestMatch step.
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

        // -- Tier 4: Vision API (Claude -- expensive, last resort) --
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

}
