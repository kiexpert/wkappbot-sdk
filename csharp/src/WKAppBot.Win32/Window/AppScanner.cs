using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

/// <summary>
/// Scans a target window and produces a structured summary:
///   1. Classify top-level children into zones (toolbar/mdi/bar/service/...)
///   2. Enumerate MDI forms with form_id + form_name + stock_code
///   3. Group forms by type for clean summary output
///   4. (--ocr) OCR unknown buttons/labels → save to Experience DB
///
/// Works with any MFC-based HTS app (LS Tuhon, Kiwoom HeroMun, NH NaMu, etc.)
/// </summary>
public static class AppScanner
{
    /// <summary>
    /// Perform a full scan of the target window.
    /// </summary>
    public static AppScanResult Scan(IntPtr hWnd)
    {
        var mainInfo = WindowInfo.FromHwnd(hWnd);

        // Get process info
        NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
        string processName = "";
        try
        {
            var proc = Process.GetProcessById((int)pid);
            processName = proc.ProcessName;
        }
        catch { }

        var result = new AppScanResult
        {
            WindowTitle = mainInfo.Title,
            WindowClass = mainInfo.ClassName,
            ProcessName = processName,
            ProcessId = (int)pid,
            Handle = hWnd,
            Rect = mainInfo.Rect,
        };

        // Classify top-level children
        var children = WindowFinder.GetChildrenZOrder(hWnd);
        IntPtr hMdiClient = IntPtr.Zero;

        foreach (var child in children)
        {
            var zone = ZoneClassifier.Classify(child, mainInfo.Rect);
            var entry = new ZoneScanEntry
            {
                Handle = child.Handle,
                ClassName = child.ClassName,
                Title = child.Title,
                ControlId = child.ControlId,
                Rect = child.Rect,
                IsVisible = child.IsVisible,
                Zone = zone,
            };

            // Scan notable children of bars (looking for Edit inputs, webviews, etc.)
            if (zone.Type == ZoneType.Bar || zone.Type == ZoneType.Toolbar)
            {
                var barChildren = WindowFinder.GetChildrenZOrder(child.Handle);
                foreach (var bc in barChildren)
                {
                    var subZone = ZoneClassifier.Classify(bc, child.Rect);
                    if (subZone.Type == ZoneType.Input || subZone.Type == ZoneType.WebView)
                    {
                        entry.NotableChildren.Add(new ZoneScanEntry
                        {
                            Handle = bc.Handle,
                            ClassName = bc.ClassName,
                            Title = bc.Title,
                            ControlId = bc.ControlId,
                            Rect = bc.Rect,
                            IsVisible = bc.IsVisible,
                            Zone = subZone,
                        });
                    }
                    // Also check one level deeper (e.g., toolbar > container > Edit)
                    var deepChildren = WindowFinder.GetChildrenZOrder(bc.Handle);
                    foreach (var dc in deepChildren)
                    {
                        var deepZone = ZoneClassifier.Classify(dc, bc.Rect);
                        if (deepZone.Type == ZoneType.Input || deepZone.Type == ZoneType.WebView)
                        {
                            entry.NotableChildren.Add(new ZoneScanEntry
                            {
                                Handle = dc.Handle,
                                ClassName = dc.ClassName,
                                Title = dc.Title,
                                ControlId = dc.ControlId,
                                Rect = dc.Rect,
                                IsVisible = dc.IsVisible,
                                Zone = deepZone,
                            });
                        }
                    }
                }
            }

            result.Zones.Add(entry);

            if (zone.Type == ZoneType.MdiContainer)
                hMdiClient = child.Handle;
        }

        // Enumerate MDI forms
        if (hMdiClient != IntPtr.Zero)
        {
            result.MdiHandle = hMdiClient;
            var formChildren = WindowFinder.GetChildrenZOrder(hMdiClient);

            foreach (var fc in formChildren)
            {
                var (formId, formName) = ZoneClassifier.ParseFormTitle(fc.Title);
                var stockCode = TryExtractStockCode(fc.Handle);

                result.Forms.Add(new FormScanEntry
                {
                    Handle = fc.Handle,
                    ClassName = fc.ClassName,
                    Title = fc.Title ?? "",
                    ControlId = fc.ControlId,
                    Rect = fc.Rect,
                    IsVisible = fc.IsVisible,
                    FormId = formId,
                    FormName = formName,
                    StockCode = stockCode,
                    DirectChildCount = CountDirectChildren(fc.Handle),
                });
            }
        }

        return result;
    }

    /// <summary>
    /// Try to extract stock code from a form window.
    /// Searches for a child with cid=3780 (known stock code input in LS Securities)
    /// or falls back to searching Edit/Afx controls with short numeric text.
    /// </summary>
    private static string? TryExtractStockCode(IntPtr hForm)
    {
        // Strategy 1: Look for cid=3780 (LS Securities pattern)
        var text = FindChildTextByCid(hForm, 3780, maxDepth: 3);
        if (!string.IsNullOrEmpty(text) && IsLikelyStockCode(text))
            return text;

        // Strategy 2: Look in direct children for Edit with short numeric text
        var children = WindowFinder.GetChildrenZOrder(hForm);
        foreach (var child in children)
        {
            // Check first-level dialog (#32770 or AfxFrameOrView)
            var innerChildren = WindowFinder.GetChildrenZOrder(child.Handle);
            foreach (var inner in innerChildren)
            {
                if (inner.ControlId == 3780)
                {
                    var t = GetWindowText(inner.Handle);
                    if (IsLikelyStockCode(t)) return t;
                }
                // One more level deep
                var deep = WindowFinder.GetChildrenZOrder(inner.Handle);
                foreach (var d in deep)
                {
                    if (d.ControlId == 3780)
                    {
                        var dt = GetWindowText(d.Handle);
                        if (IsLikelyStockCode(dt)) return dt;
                    }
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Search recursively for a child with specific cid and return its text.
    /// </summary>
    private static string? FindChildTextByCid(IntPtr hParent, int targetCid, int maxDepth, int depth = 0)
    {
        if (depth > maxDepth) return null;

        var children = WindowFinder.GetChildrenZOrder(hParent);
        foreach (var child in children)
        {
            if (child.ControlId == targetCid)
            {
                var text = GetWindowText(child.Handle);
                if (!string.IsNullOrEmpty(text)) return text;
            }

            var result = FindChildTextByCid(child.Handle, targetCid, maxDepth, depth + 1);
            if (result != null) return result;
        }
        return null;
    }

    private static string GetWindowText(IntPtr hWnd)
    {
        var sb = new StringBuilder(256);
        NativeMethods.GetWindowTextW(hWnd, sb, sb.Capacity);
        return sb.ToString();
    }

    private static bool IsLikelyStockCode(string? text)
    {
        if (string.IsNullOrEmpty(text)) return false;
        text = text.Trim();
        // Korean stock codes: 6 digits, sometimes with prefix letter(s)
        return Regex.IsMatch(text, @"^[A-Z]?\d{4,8}$");
    }

    private static int CountDirectChildren(IntPtr hParent)
    {
        int count = 0;
        var child = NativeMethods.GetWindow(hParent, NativeMethods.GW_CHILD);
        while (child != IntPtr.Zero)
        {
            count++;
            child = NativeMethods.GetWindow(child, NativeMethods.GW_HWNDNEXT);
        }
        return count;
    }

    // ── OCR Experience Learning ────────────────────────────────

    /// <summary>
    /// OCR-scan leaf controls inside each unique form type and learn their text.
    /// Captures each form → runs OCR → matches words to child controls by position.
    ///
    /// Only scans one instance per form type (e.g., one [1101] 현재가 out of 6).
    /// Focuses on Button, Static (label), and small unidentified controls.
    /// </summary>
    /// <param name="scanResult">Scan result with forms to OCR</param>
    /// <param name="expDb">Experience DB to store learned controls</param>
    /// <param name="ocrRecognizeAll">
    /// OCR function: takes Bitmap → returns (text, x, y, w, h, confidence)[]
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
        // MDI child windows live inside the parent — SetWindowPos on a child alone
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

        // Group forms by type — only OCR one instance per type
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
                // Capture the form window
                using var screenshot = ScreenCapture.CaptureWindow(form.Handle);
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
                    };

                    expDb.LearnControl(formId, form.FormName ?? formId, experience, treePath);
                    result.ControlsLearned++;

                    // ── Per-control detail capture ──
                    // --detail: always refresh screenshots + text history
                    // default: auto-capture on first encounter (no existing screenshot)
                    bool shouldCapture = ctrl.Rect.Width > 0 && ctrl.Rect.Height > 0
                        && (captureDetails || !expDb.HasScreenshot(formId, ctrl.ControlId, treePath));
                    if (shouldCapture)
                    {
                        try
                        {
                            // Crop from form bitmap (PrintWindow-safe — no other window interference)
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
                        catch { /* best-effort — don't fail scan over detail capture */ }
                    }
                }

                onProgress?.Invoke(formId, result.ControlsLearned);

                // ── Parent control screenshots (grids, tables, panels — children included) ──
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

    /// <summary>
    /// A control with its Win32 class hierarchy tree path.
    /// TreePath mirrors the actual hWnd parent-child tree as filesystem folders.
    /// Example: "#32770_59648/AfxWnd140_999/Button_3785"
    /// </summary>
    private record struct ControlWithPath(WindowInfo Info, string TreePath);

    /// <summary>
    /// Collect leaf-level controls (buttons, labels, edits, etc.) from a form.
    /// Traverses the Win32 child tree and returns controls with no children or known leaf types.
    /// </summary>
    private static List<WindowInfo> CollectLeafControls(IntPtr hForm, RECT formRect, int maxDepth)
    {
        var leafResult = new List<ControlWithPath>();
        var parentResult = new List<ControlWithPath>();
        CollectControlsRecursive(hForm, formRect, "", leafResult, parentResult, 0, maxDepth);
        return leafResult.Select(c => c.Info).ToList();
    }

    /// <summary>
    /// Collect both leaf and parent controls with tree paths.
    /// Parent controls = controls that have children and are not leaf-like (grids, tables, panels).
    /// These get a screenshot with their children included.
    /// </summary>
    private static (List<ControlWithPath> leafControls, List<ControlWithPath> parentControls) CollectAllControls(
        IntPtr hForm, RECT formRect, int maxDepth)
    {
        var leafResult = new List<ControlWithPath>();
        var parentResult = new List<ControlWithPath>();
        CollectControlsRecursive(hForm, formRect, "", leafResult, parentResult, 0, maxDepth);
        return (leafResult, parentResult);
    }

    private static void CollectControlsRecursive(
        IntPtr hParent, RECT formRect, string parentPath,
        List<ControlWithPath> leafResult, List<ControlWithPath> parentResult,
        int depth, int maxDepth)
    {
        if (depth > maxDepth) return;

        var children = WindowFinder.GetChildrenZOrder(hParent);
        foreach (var child in children)
        {
            // Skip invisible/zero-size
            if (!child.IsVisible || child.Rect.Width <= 0 || child.Rect.Height <= 0) continue;

            // Skip if outside form bounds (sometimes children have weird coords)
            if (child.Rect.Left > formRect.Right || child.Rect.Top > formRect.Bottom) continue;

            // Build tree path segment for this control
            var segment = ExperienceDb.BuildTreeSegment(child.ClassName, child.ControlId);
            var currentPath = string.IsNullOrEmpty(parentPath) ? segment : $"{parentPath}/{segment}";

            var grandChildren = WindowFinder.GetChildrenZOrder(child.Handle);

            if (grandChildren.Count == 0)
            {
                // Leaf node — this is a single control (button, label, edit, etc.)
                leafResult.Add(new ControlWithPath(child, currentPath));
            }
            else
            {
                // Has children — check if it's a known leaf-like class, otherwise recurse
                if (IsLeafLikeClass(child.ClassName))
                {
                    leafResult.Add(new ControlWithPath(child, currentPath));
                }
                else
                {
                    // Parent control: collect for screenshot (children included)
                    // Only significant-size parents (not tiny wrapper panels)
                    if (child.Rect.Width >= 30 && child.Rect.Height >= 30)
                    {
                        parentResult.Add(new ControlWithPath(child, currentPath));
                    }
                    CollectControlsRecursive(child.Handle, formRect, currentPath, leafResult, parentResult, depth + 1, maxDepth);
                }
            }
        }
    }

    /// <summary>Is this a class that should be treated as a leaf even if it has children?</summary>
    private static bool IsLeafLikeClass(string className)
    {
        // ComboBox has a child Edit + ListBox, but treat it as one control
        // SysTabControl32 has tab pages but treat the tab itself as leaf
        return className is "ComboBox" or "SysTabControl32" or "SysListView32"
            or "SysTreeView32" or "RichEdit20A" or "RichEdit20W";
    }

    /// <summary>Is this control worth OCR-scanning?</summary>
    private static bool IsOcrCandidate(WindowInfo ctrl)
    {
        var cls = ctrl.ClassName;

        // Definitely OCR: buttons, labels (Static doesn't expose text via GetWindowText sometimes)
        if (cls is "Button" or "Static") return true;

        // AfxWnd (MFC custom controls) — might be owner-drawn buttons/labels
        if (cls.StartsWith("Afx", StringComparison.Ordinal)) return true;

        // Small controls that might be buttons (unknown MFC classes)
        if (ctrl.Rect.Width > 10 && ctrl.Rect.Height > 10 &&
            ctrl.Rect.Width < 300 && ctrl.Rect.Height < 100)
            return true;

        return false;
    }

    /// <summary>
    /// Find OCR-recognized text that spatially overlaps with a control's rectangle.
    /// Converts control's screen coords to form-relative coords for matching.
    /// </summary>
    private static string? FindOverlappingOcrText(
        WindowInfo ctrl, IReadOnlyList<OcrWordInfo> ocrWords, RECT formRect)
    {
        // Control rect in form-relative coordinates (OCR coords are form-relative)
        int ctrlLeft = ctrl.Rect.Left - formRect.Left;
        int ctrlTop = ctrl.Rect.Top - formRect.Top;
        int ctrlRight = ctrlLeft + ctrl.Rect.Width;
        int ctrlBottom = ctrlTop + ctrl.Rect.Height;

        // Expand search area by a small margin (OCR bounding boxes aren't always pixel-perfect)
        int margin = 4;
        ctrlLeft -= margin;
        ctrlTop -= margin;
        ctrlRight += margin;
        ctrlBottom += margin;

        var matchedWords = new List<string>();

        foreach (var word in ocrWords)
        {
            int wordCX = word.X + word.Width / 2;
            int wordCY = word.Y + word.Height / 2;

            // Check if word center falls within control rect
            if (wordCX >= ctrlLeft && wordCX <= ctrlRight &&
                wordCY >= ctrlTop && wordCY <= ctrlBottom)
            {
                matchedWords.Add(word.Text);
            }
        }

        return matchedWords.Count > 0 ? string.Join(" ", matchedWords) : null;
    }

    /// <summary>Infer semantic role from class name and OCR text.</summary>
    private static string InferRole(string className, string ocrText)
    {
        if (className == "Button") return "button";
        if (className == "Static") return "label";
        if (className == "Edit") return "input";
        if (className == "ComboBox") return "combo";
        if (className == "SysTabControl32") return "tab";

        // Check if OCR text suggests a specific role
        var lower = ocrText.ToLowerInvariant();
        if (lower.Contains("매수") || lower.Contains("buy")) return "buy_button";
        if (lower.Contains("매도") || lower.Contains("sell")) return "sell_button";
        if (lower.Contains("조회") || lower.Contains("search") || lower.Contains("검색")) return "query_button";
        if (lower.Contains("취소") || lower.Contains("cancel")) return "cancel_button";
        if (lower.Contains("확인") || lower.Contains("ok")) return "confirm_button";
        if (lower.Contains("정정") || lower.Contains("수정")) return "modify_button";

        return "control";
    }

    // ── Form-level Text Snapshot (WM_GETTEXT) ─────────────────

    /// <summary>
    /// Collect all visible text (WM_GETTEXT) from a form's child controls, sorted by Y-coordinate.
    /// Returns text lines for puppet pattern building via ExperienceDb.AddTextSnapshot().
    ///
    /// Text collection is NEVER skipped even for DB-known controls — needed for puppet pattern diff.
    /// </summary>
    /// <param name="hForm">Form window handle</param>
    /// <param name="formRect">Form bounding rectangle (for relative Y sorting)</param>
    /// <param name="maxDepth">Max recursion depth for child traversal</param>
    public static List<string> CollectFormTextSnapshot(IntPtr hForm, RECT formRect, int maxDepth = 4)
    {
        var textItems = new List<(int y, string text)>();
        CollectTextRecursive(hForm, formRect, textItems, 0, maxDepth);

        // Sort by Y-coordinate → deduplicate adjacent identical lines
        return textItems
            .OrderBy(item => item.y)
            .Select(item => item.text)
            .Where(t => !string.IsNullOrWhiteSpace(t))
            .ToList();
    }

    private static void CollectTextRecursive(
        IntPtr hParent, RECT formRect, List<(int y, string text)> textItems,
        int depth, int maxDepth)
    {
        if (depth > maxDepth) return;

        var children = WindowFinder.GetChildrenZOrder(hParent);
        foreach (var child in children)
        {
            if (!child.IsVisible || child.Rect.Width <= 0 || child.Rect.Height <= 0) continue;

            // Collect WM_GETTEXT from this control
            var text = GetWindowText(child.Handle);
            if (!string.IsNullOrWhiteSpace(text))
            {
                int relativeY = child.Rect.Top - formRect.Top;
                textItems.Add((relativeY, text));
            }

            // Recurse into children (unless leaf-like class)
            if (!IsLeafLikeClass(child.ClassName))
            {
                CollectTextRecursive(child.Handle, formRect, textItems, depth + 1, maxDepth);
            }
        }
    }

    // ── Structural Fingerprint + OCR Keywords ──────────────────

    /// <summary>
    /// Generate a structural fingerprint from a form's control list.
    /// Each control → "{NormalizedClass}:{Cid}:{SizeBucket}:{PosBucket}" token.
    /// Tokens sorted → joined → SHA256 hash (first 16 hex chars).
    ///
    /// The fingerprint captures the STABLE structure of a form type:
    ///   - ClassName normalized (AfxWnd110/140 → "AfxWnd", Afx:hex... → "Afx:*")
    ///   - ControlId (stable per form type)
    ///   - SizeBucket (XS/S/M/L/XL based on area)
    ///   - PosBucket (TL/TC/TR/ML/MC/MR/BL/BC/BR based on relative position)
    /// </summary>
    public static (string fingerprint, string fingerprintHash) GenerateFingerprint(
        IReadOnlyList<ControlExperience> controls)
    {
        var tokens = new List<string>();

        foreach (var ctrl in controls)
        {
            var normalizedClass = NormalizeClassName(ctrl.ClassName ?? "?");
            var sizeBucket = CategorizeSizeArea(ctrl.Width * ctrl.Height);
            var posBucket = CategorizePosition(ctrl.RelativeX, ctrl.RelativeY);

            tokens.Add($"{normalizedClass}:{ctrl.ControlId}:{sizeBucket}:{posBucket}");
        }

        tokens.Sort(StringComparer.Ordinal);
        var fingerprint = string.Join("\n", tokens);

        // SHA256 → first 16 hex chars
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(fingerprint));
        var fingerprintHash = Convert.ToHexString(hashBytes)[..16].ToLowerInvariant();

        return (fingerprint, fingerprintHash);
    }

    /// <summary>
    /// Refine OCR keywords for a form type.
    /// First scan (scan_count==1): all OCR text words become keyword candidates.
    /// Subsequent scans: intersect with existing keywords → filter out dynamic data.
    /// </summary>
    public static List<string> RefineOcrKeywords(FormExperience form)
    {
        // Collect all current OCR text words from controls
        var currentWords = new HashSet<string>(StringComparer.Ordinal);
        foreach (var ctrl in form.Controls)
        {
            if (string.IsNullOrWhiteSpace(ctrl.OcrText)) continue;

            foreach (var word in SplitOcrWords(ctrl.OcrText))
            {
                if (IsKeywordCandidate(word))
                    currentWords.Add(word);
            }
        }

        if (form.OcrKeywords == null || form.OcrKeywords.Count == 0 || form.ScanCount <= 1)
        {
            // First scan: all words are candidates
            return currentWords.OrderBy(w => w, StringComparer.Ordinal).ToList();
        }

        // Subsequent scans: intersect with existing keywords
        var refined = form.OcrKeywords
            .Where(kw => currentWords.Contains(kw))
            .ToList();

        return refined;
    }

    /// <summary>
    /// Normalize class name for fingerprint comparison.
    /// MFC version numbers and session-unique hex IDs are stripped.
    /// </summary>
    internal static string NormalizeClassName(string className)
    {
        // AfxWnd110, AfxWnd140, AfxWnd70s etc. → "AfxWnd"
        if (Regex.IsMatch(className, @"^AfxWnd\d+[su]?$"))
            return "AfxWnd";

        // Afx:00E80000:b:00010005:... → "Afx:*" (session-unique identifiers)
        if (className.StartsWith("Afx:", StringComparison.Ordinal))
            return "Afx:*";

        // AfxFrameOrView, AfxMDIFrame, etc. → keep as-is (stable class names)
        return className;
    }

    /// <summary>Categorize area (width×height) into size buckets.</summary>
    internal static string CategorizeSizeArea(int area)
    {
        return area switch
        {
            < 1_000 => "XS",      // tiny icons, small buttons
            < 5_000 => "S",       // normal buttons
            < 20_000 => "M",      // large buttons, input fields
            < 100_000 => "L",     // table sections, partial charts
            _ => "XL",            // main charts, grids
        };
    }

    /// <summary>Categorize position (0.0~1.0) into 9-grid buckets.</summary>
    internal static string CategorizePosition(double relX, double relY)
    {
        var col = relX switch
        {
            < 0.333 => "L",
            < 0.666 => "C",
            _ => "R",
        };
        var row = relY switch
        {
            < 0.333 => "T",
            < 0.666 => "M",
            _ => "B",
        };
        return $"{row}{col}";
    }

    /// <summary>Split OCR text into individual words for keyword analysis.</summary>
    private static IEnumerable<string> SplitOcrWords(string ocrText)
    {
        // Split by whitespace, commas, and common separators
        return Regex.Split(ocrText, @"[\s,;:|\[\](){}]+")
            .Where(w => !string.IsNullOrWhiteSpace(w))
            .Select(w => w.Trim());
    }

    /// <summary>Is this word a good keyword candidate? Filters out dynamic data.</summary>
    private static bool IsKeywordCandidate(string word)
    {
        // Too short (single char often noise)
        if (word.Length < 2) return false;

        // Pure numbers (prices, quantities) → dynamic
        if (Regex.IsMatch(word, @"^[\d.,+\-]+$")) return false;

        // Time patterns → dynamic
        if (Regex.IsMatch(word, @"^\d{1,2}:\d{2}")) return false;

        // Stock code patterns (4-8 digits, optional letter prefix) → dynamic
        if (Regex.IsMatch(word, @"^[A-Z]?\d{4,8}$")) return false;

        // Percentage → dynamic
        if (Regex.IsMatch(word, @"^\d+\.?\d*[%t]$")) return false;

        return true;
    }

    // ── Pretty-print ─────────────────────────────────────────

    /// <summary>
    /// Format the scan result as a human-readable summary string.
    /// </summary>
    public static string FormatSummary(AppScanResult result, string? profileName = null)
    {
        var sb = new StringBuilder(2048);

        // Header
        sb.AppendLine($"=== {result.WindowTitle} — App Scan ===");
        sb.AppendLine($"Class: {result.WindowClass}  Process: {result.ProcessName} (PID:{result.ProcessId})");
        sb.AppendLine($"Size: {result.Rect.Width}x{result.Rect.Height}");
        if (profileName != null)
            sb.AppendLine($"Profile: {profileName}");
        sb.AppendLine();

        // Zones (skip forms — they're in MDI section)
        foreach (var z in result.Zones)
        {
            if (z.Zone.Type == ZoneType.Form) continue; // forms shown in MDI section

            var sizeStr = z.Rect.Width > 0 ? $"({z.Rect.Width}x{z.Rect.Height})" : "(0x0)";
            var vis = z.IsVisible ? "" : " [hidden]";
            var titleStr = !string.IsNullOrEmpty(z.Title) ? $"\"{z.Title}\"" : "";

            sb.AppendLine($"  [{z.Zone.Tag,-10}] cid={z.ControlId,-8} {sizeStr,-14} {titleStr}{vis}");

            // Notable children (inputs, webviews found inside bars)
            foreach (var nc in z.NotableChildren)
            {
                var ncTitle = !string.IsNullOrEmpty(nc.Title) ? $"\"{nc.Title}\"" : "";
                sb.AppendLine($"    [{nc.Zone.Tag,-8}] [{nc.ClassName}] cid={nc.ControlId} {ncTitle}");
            }
        }

        // MDI Forms
        if (result.Forms.Count > 0)
        {
            sb.AppendLine();
            sb.AppendLine($"── MDI Forms ({result.Forms.Count}) ────────────────────────");

            // Group by form_id
            var groups = result.Forms
                .GroupBy(f => f.FormId ?? "???")
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key);

            foreach (var g in groups)
            {
                var first = g.First();
                var formLabel = first.FormId != null
                    ? $"[{first.FormId}] {first.FormName ?? "?"}"
                    : first.Title;

                var stockCodes = g
                    .Where(f => f.StockCode != null)
                    .Select(f => f.StockCode!)
                    .Distinct()
                    .ToList();

                var stockStr = stockCodes.Count > 0
                    ? " {" + string.Join(", ", stockCodes) + "}"
                    : "";

                var countStr = g.Count() > 1 ? $" x{g.Count()}" : "";

                sb.AppendLine($"  [form] {formLabel,-30}{countStr}{stockStr}");
            }
        }

        // Count service windows
        int serviceCount = result.Zones.Count(z => z.Zone.Type == ZoneType.Service);
        if (serviceCount > 0)
        {
            sb.AppendLine();
            sb.AppendLine($"  ({serviceCount} background service windows)");
        }

        return sb.ToString();
    }
}

// ── Data models ─────────────────────────────────────────

/// <summary>
/// Complete scan result for a target window.
/// </summary>
public sealed class AppScanResult
{
    public string WindowTitle { get; init; } = "";
    public string WindowClass { get; init; } = "";
    public string ProcessName { get; init; } = "";
    public int ProcessId { get; init; }
    public IntPtr Handle { get; init; }
    public RECT Rect { get; init; }

    /// <summary>Top-level child zones</summary>
    public List<ZoneScanEntry> Zones { get; } = new();

    /// <summary>MDIClient handle (if found)</summary>
    public IntPtr MdiHandle { get; set; }

    /// <summary>MDI child forms</summary>
    public List<FormScanEntry> Forms { get; } = new();
}

/// <summary>
/// A classified top-level child zone.
/// </summary>
public sealed class ZoneScanEntry
{
    public IntPtr Handle { get; init; }
    public string ClassName { get; init; } = "";
    public string? Title { get; init; }
    public int ControlId { get; init; }
    public RECT Rect { get; init; }
    public bool IsVisible { get; init; }
    public ZoneInfo Zone { get; init; } = new(ZoneType.Unknown, "");

    /// <summary>Notable children found inside bars (Edit inputs, webviews, etc.)</summary>
    public List<ZoneScanEntry> NotableChildren { get; } = new();
}

/// <summary>
/// A scanned MDI child form.
/// </summary>
public sealed class FormScanEntry
{
    public IntPtr Handle { get; init; }
    public string ClassName { get; init; } = "";
    public string Title { get; init; } = "";
    public int ControlId { get; init; }
    public RECT Rect { get; init; }
    public bool IsVisible { get; init; }

    /// <summary>Form ID extracted from title (e.g., "1101", "0606")</summary>
    public string? FormId { get; init; }

    /// <summary>Form name extracted from title (e.g., "현재가", "키움종합차트")</summary>
    public string? FormName { get; init; }

    /// <summary>Stock code found in the form (e.g., "005930")</summary>
    public string? StockCode { get; init; }

    /// <summary>Number of direct child windows (complexity hint)</summary>
    public int DirectChildCount { get; init; }
}

/// <summary>
/// OCR word info — lightweight struct for passing OCR results from Vision layer.
/// Avoids direct dependency on WKAppBot.Vision in Win32 project.
/// </summary>
public sealed class OcrWordInfo
{
    public string Text { get; init; } = "";
    public int X { get; init; }
    public int Y { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
}

/// <summary>
/// Result summary from OCR learning pass.
/// </summary>
public sealed class OcrLearnResult
{
    public int FormsScanned { get; set; }
    public int ControlsLearned { get; set; }
    public int DetailScreenshots { get; set; }
    public int DetailTextChanges { get; set; }
    public List<string> Errors { get; } = new();

    public override string ToString()
    {
        var errStr = Errors.Count > 0 ? $", {Errors.Count} errors" : "";
        var detailStr = DetailScreenshots > 0
            ? $", {DetailScreenshots} screenshots + {DetailTextChanges} text changes"
            : "";
        return $"OCR Learning: {ControlsLearned} controls learned from {FormsScanned} form types{detailStr}{errStr}";
    }
}
