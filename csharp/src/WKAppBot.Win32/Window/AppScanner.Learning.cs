using System.Drawing;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

public static partial class AppScanner
{
    // -- OCR Experience Learning --------------------------------

    /// <summary>
    /// OCR-scan leaf controls inside each unique form type and learn their text.
    /// Captures each form -> runs OCR -> matches words to child controls by position.
    ///
    /// Only scans one instance per form type (e.g., one [1101] 현재가 out of 6).
    /// Focuses on Button, Static (label), and small unidentified controls.
    /// </summary>
    /// <param name="scanResult">Scan result with forms to OCR</param>
    /// <param name="expDb">Experience DB to store learned controls</param>
    /// <param name="ocrRecognizeAll">
    /// OCR function: takes Bitmap -> returns (text, x, y, w, h, confidence)[]
    /// Injected to avoid WKAppBot.Vision dependency in Win32 project.
    /// </param>
    /// <param name="onProgress">Optional progress callback (formId, controlCount)</param>
    /// <param name="captureDetails">If true, save per-control screenshots + text history</param>
    public static async Task<OcrLearnResult> LearnFormsWithOcr(
        AppScanResult scanResult,
        ExperienceDb expDb,
        Func<Bitmap, Task<IReadOnlyList<OcrWordInfo>>> ocrRecognizeAll,
        Action<string, int>? onProgress = null,
        bool captureDetails = false)
    {
        var result = new OcrLearnResult();

        // Bring the main window to front before capturing MDI children.
        // MDI child windows live inside the parent -- SetWindowPos on a child alone
        // won't help if another app (editor, browser) covers the parent.
        // SWP_NOACTIVATE avoids stealing focus from the user.
        var originalFg = NativeMethods.GetForegroundWindow();
        bool needsRestore = originalFg != IntPtr.Zero && originalFg != scanResult.Handle;
        NativeMethods.SetWindowPos(
            scanResult.Handle, NativeMethods.HWND_TOP,
            0, 0, 0, 0,
            NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE |
            NativeMethods.SWP_SHOWWINDOW | NativeMethods.SWP_NOACTIVATE);
        Thread.Sleep(150); // let it render on top

        try
        {

        // Group forms by type -- only OCR one instance per type
        var formGroups = scanResult.Forms
            .Where(f => f.FormId != null && f.IsVisible && f.Rect.Width > 50 && f.Rect.Height > 50)
            .GroupBy(f => f.FormId!)
            .ToList();

        foreach (var group in formGroups)
        {
            var formId = group.Key;
            var form = group.First(); // pick first visible instance
            result.FormsScanned++;

            try
            {
                // Minimized window? Capture the cute taskbar icon as evidence
                if (NativeMethods.IsIconic(form.Handle))
                {
                    try
                    {
                        NativeMethods.GetWindowRect(form.Handle, out var iconRect);
                        if (iconRect.Width > 0 && iconRect.Height > 0)
                        {
                            using var iconBmp = ScreenCapture.CaptureScreenRegion(
                                iconRect.Left, iconRect.Top, iconRect.Width, iconRect.Height);
                            var failDir = Path.Combine(expDb.ExpDir, $"form_{formId}");
                            if (!Directory.Exists(failDir)) Directory.CreateDirectory(failDir);
                            ScreenCapture.SaveToFile(iconBmp, Path.Combine(failDir, ".fail-iconic.png"));
                        }
                    }
                    catch { /* best-effort */ }
                    continue;
                }

                // Capture the form window (skip blank captures -- protect DB from bad data)
                using var screenshot = ScreenCapture.CaptureWindow(form.Handle, new CaptureOptions
                {
                    RejectBlank = true,
                });
                if (screenshot == null)
                {
                    // Capture actual screen region to show what's covering the form area
                    try
                    {
                        using var screenBmp = ScreenCapture.CaptureScreenRegion(
                            form.Rect.Left, form.Rect.Top, form.Rect.Width, form.Rect.Height);
                        var failDir = Path.Combine(expDb.ExpDir, $"form_{formId}");
                        if (!Directory.Exists(failDir)) Directory.CreateDirectory(failDir);
                        ScreenCapture.SaveToFile(screenBmp, Path.Combine(failDir, ".fail.png"));
                    }
                    catch { /* best-effort */ }
                    continue;
                }
                if (screenshot.Width < 10 || screenshot.Height < 10) continue;

                // Run OCR on the form
                var ocrWords = await ocrRecognizeAll(screenshot);
                if (ocrWords.Count == 0) continue;

                // Get all child controls of this form (leaf + parent)
                var (leafControls, parentControls) = CollectAllControls(form.Handle, form.Rect, maxDepth: 4);

                // Match OCR words to controls by spatial overlap
                foreach (var cwp in leafControls)
                {
                    var ctrl = cwp.Info;
                    var treePath = cwp.TreePath;

                    // Only interested in buttons, labels, and small controls
                    if (!IsOcrCandidate(ctrl)) continue;

                    // Find OCR words that overlap with this control's rect
                    var matchedText = FindOverlappingOcrText(ctrl, ocrWords, form.Rect);

                    // Also collect WM_GETTEXT (works for Edit/ComboBox where OCR is unreliable)
                    var wmText = GetWindowText(ctrl.Handle);

                    // Need at least one text source to learn this control
                    if (string.IsNullOrWhiteSpace(matchedText) && string.IsNullOrWhiteSpace(wmText))
                        continue;

                    // Calculate relative position (0.0~1.0 within form)
                    double relX = form.Rect.Width > 0
                        ? (double)(ctrl.Rect.Left - form.Rect.Left) / form.Rect.Width : 0;
                    double relY = form.Rect.Height > 0
                        ? (double)(ctrl.Rect.Top - form.Rect.Top) / form.Rect.Height : 0;

                    // Use OCR text as primary (visual), WM_GETTEXT as supplementary
                    var primaryText = !string.IsNullOrWhiteSpace(matchedText) ? matchedText : wmText!;

                    // Learn this control (with tree path)
                    var experience = new ControlExperience
                    {
                        ControlId = ctrl.ControlId,
                        ClassName = ctrl.ClassName,
                        Role = InferRole(ctrl.ClassName, primaryText),
                        OcrText = matchedText,
                        OcrConfidence = !string.IsNullOrWhiteSpace(matchedText) ? 0.8 : 0.0,
                        WmGetText = !string.IsNullOrWhiteSpace(wmText) ? wmText : null,
                        Width = ctrl.Rect.Width,
                        Height = ctrl.Rect.Height,
                        RelativeX = Math.Round(relX, 4),
                        RelativeY = Math.Round(relY, 4),
                        HitCount = 1,
                        Style = ctrl.Style,
                        ExStyle = ctrl.ExStyle,
                    };

                    expDb.LearnControl(formId, form.FormName ?? formId, experience, treePath);
                    result.ControlsLearned++;

                    // -- Per-control detail capture --
                    // --detail: always refresh screenshots + text history
                    // default: auto-capture on first encounter (no existing screenshot)
                    bool shouldCapture = ctrl.Rect.Width > 0 && ctrl.Rect.Height > 0
                        && (captureDetails || !expDb.HasScreenshot(formId, ctrl.ControlId, treePath));
                    if (shouldCapture)
                    {
                        try
                        {
                            // Crop from form bitmap (PrintWindow-safe -- no other window interference)
                            int cropX = ctrl.Rect.Left - form.Rect.Left;
                            int cropY = ctrl.Rect.Top - form.Rect.Top;
                            using var ctrlBmp = ScreenCapture.CropRegion(
                                screenshot, cropX, cropY,
                                ctrl.Rect.Width, ctrl.Rect.Height);
                            expDb.SaveControlScreenshot(formId, ctrl.ControlId, ctrlBmp, treePath);
                            result.DetailScreenshots++;

                            // Append text history (dedup: only if text changed)
                            if (expDb.AppendTextHistory(formId, ctrl.ControlId, matchedText, wmText, treePath))
                                result.DetailTextChanges++;
                        }
                        catch { /* best-effort -- don't fail scan over detail capture */ }
                    }
                }

                onProgress?.Invoke(formId, result.ControlsLearned);

                // -- Parent control screenshots (grids, tables, panels -- children included) --
                // Parent controls are treated as regular controls for screenshot purposes.
                // They get exactly one screenshot showing their full content with all children.
                foreach (var pwp in parentControls)
                {
                    var parent = pwp.Info;
                    var parentTreePath = pwp.TreePath;

                    bool shouldCaptureParent = parent.Rect.Width > 0 && parent.Rect.Height > 0
                        && (captureDetails || !expDb.HasScreenshot(formId, parent.ControlId, parentTreePath));
                    if (!shouldCaptureParent) continue;

                    try
                    {
                        int pCropX = parent.Rect.Left - form.Rect.Left;
                        int pCropY = parent.Rect.Top - form.Rect.Top;
                        using var parentBmp = ScreenCapture.CropRegion(
                            screenshot, pCropX, pCropY,
                            parent.Rect.Width, parent.Rect.Height);
                        expDb.SaveControlScreenshot(formId, parent.ControlId, parentBmp, parentTreePath);
                        result.DetailScreenshots++;

                        // Also record WM_GETTEXT for parent (often has meaningful text like "통", "Chart Window")
                        var parentWmText = GetWindowText(parent.Handle);
                        if (!string.IsNullOrWhiteSpace(parentWmText))
                            expDb.AppendTextHistory(formId, parent.ControlId, null, parentWmText, parentTreePath);

                        // Ensure parent is registered in experience DB as a control
                        if (expDb.GetControl(formId, parent.ControlId) == null)
                        {
                            double pRelX = form.Rect.Width > 0
                                ? (double)(parent.Rect.Left - form.Rect.Left) / form.Rect.Width : 0;
                            double pRelY = form.Rect.Height > 0
                                ? (double)(parent.Rect.Top - form.Rect.Top) / form.Rect.Height : 0;

                            expDb.LearnControl(formId, form.FormName ?? formId, new ControlExperience
                            {
                                ControlId = parent.ControlId,
                                ClassName = parent.ClassName,
                                Role = "container",
                                WmGetText = parentWmText,
                                Width = parent.Rect.Width,
                                Height = parent.Rect.Height,
                                RelativeX = Math.Round(pRelX, 4),
                                RelativeY = Math.Round(pRelY, 4),
                                HitCount = 1,
                                Style = parent.Style,
                                ExStyle = parent.ExStyle,
                            }, parentTreePath);
                        }
                    }
                    catch { /* best-effort */ }
                }

                // Generate structural fingerprint from learned controls
                var formExp = expDb.GetForm(formId);
                if (formExp != null && formExp.Controls.Count > 0)
                {
                    var (fp, fpHash) = GenerateFingerprint(formExp.Controls);
                    formExp.Fingerprint = fp;
                    formExp.FingerprintHash = fpHash;

                    // Refine OCR keywords (intersection across multiple scans)
                    formExp.OcrKeywords = RefineOcrKeywords(formExp);
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add($"[{formId}] {ex.Message}");
            }
        }

        return result;

        } // end try
        finally
        {
            // Restore original foreground window Z-order (best-effort)
            if (needsRestore)
            {
                NativeMethods.SetWindowPos(
                    originalFg, NativeMethods.HWND_TOP,
                    0, 0, 0, 0,
                    NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE |
                    NativeMethods.SWP_SHOWWINDOW | NativeMethods.SWP_NOACTIVATE);
            }
        }
    }

    // -- Quick Touch Controls (lightweight ExperienceDb accumulation) --

    /// <summary>
    /// Quick per-control experience accumulation WITHOUT OCR.
    /// Uses PrintWindow bitmap + WM_GETTEXT only -- much lighter than LearnFormsWithOcr.
    /// Called from snapshot/capture commands to accumulate experience on every encounter.
    /// </summary>
    /// <param name="scanResult">AppScanResult from Scan()</param>
    /// <param name="expDb">Experience DB to store learned controls</param>
    /// <param name="maxDepth">Max tree depth for control collection (default: 4)</param>
    /// <returns>(forms touched, controls touched, screenshots captured)</returns>
    public static (int forms, int controls, int screenshots) QuickTouchControls(
        AppScanResult scanResult, ExperienceDb expDb, int maxDepth = 4)
    {
        int formCount = 0, controlCount = 0, screenshotCount = 0;

        var formGroups = scanResult.Forms
            .Where(f => f.FormId != null && f.IsVisible && f.Rect.Width > 50 && f.Rect.Height > 50)
            .GroupBy(f => f.FormId!)
            .ToList();

        foreach (var group in formGroups)
        {
            var formId = group.Key;
            var form = group.First();

            try
            {
                // Skip minimized forms
                if (NativeMethods.IsIconic(form.Handle)) continue;

                // Capture form bitmap via PrintWindow (Z-order safe)
                using var formBmp = ScreenCapture.CaptureWindow(form.Handle, new CaptureOptions
                {
                    RejectBlank = true,
                    StepLogger = s => Console.Error.WriteLine(s),
                });
                if (formBmp == null) continue;

                var formRect = form.Rect;
                formCount++;

                // Collect leaf + parent controls
                var (leafControls, parentControls) = CollectAllControls(form.Handle, formRect, maxDepth);

                // Touch each control (screenshot on first encounter + WM_GETTEXT text history)
                foreach (var ctrl in leafControls)
                {
                    try
                    {
                        var ctrlRect = new System.Drawing.Rectangle(
                            ctrl.Info.Rect.Left, ctrl.Info.Rect.Top,
                            ctrl.Info.Rect.Width, ctrl.Info.Rect.Height);
                        var fRect = new System.Drawing.Rectangle(
                            formRect.Left, formRect.Top,
                            formRect.Right - formRect.Left, formRect.Bottom - formRect.Top);

                        // WM_GETTEXT for text history (best-effort)
                        string? wmText = null;
                        try { wmText = NativeMethods.GetWindowTextW(ctrl.Info.Handle); }
                        catch { }

                        bool captured = expDb.TouchControl(
                            formId, ctrl.Info.ControlId,
                            formBmp, fRect, ctrlRect,
                            ocrText: null, wmText: wmText,
                            treePath: ctrl.TreePath);

                        controlCount++;
                        if (captured) screenshotCount++;
                    }
                    catch { /* best-effort per control */ }
                }

                // Parent controls (containers) -- screenshot only, no text
                foreach (var parent in parentControls)
                {
                    try
                    {
                        var ctrlRect = new System.Drawing.Rectangle(
                            parent.Info.Rect.Left, parent.Info.Rect.Top,
                            parent.Info.Rect.Width, parent.Info.Rect.Height);
                        var fRect = new System.Drawing.Rectangle(
                            formRect.Left, formRect.Top,
                            formRect.Right - formRect.Left, formRect.Bottom - formRect.Top);

                        expDb.TouchControl(
                            formId, parent.Info.ControlId,
                            formBmp, fRect, ctrlRect,
                            treePath: parent.TreePath);

                        controlCount++;
                    }
                    catch { /* best-effort per control */ }
                }
            }
            catch { /* best-effort per form */ }
        }

        // Save all accumulated experience
        try { expDb.SaveAll(); } catch { }

        return (formCount, controlCount, screenshotCount);
    }
}
