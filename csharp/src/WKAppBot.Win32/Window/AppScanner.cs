using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

/// <summary>
/// Scans a target window and produces a structured summary:
///   1. Classify top-level children into zones (toolbar/mdi/bar/service/...)
///   2. Enumerate MDI forms with form_id + form_name + stock_code
///   3. Group forms by type for clean summary output
///   4. (--ocr) OCR unknown buttons/labels -> save to Experience DB
///
/// Works with any MFC-based HTS app (LS Tuhon, Kiwoom HeroMun, NH NaMu, etc.)
/// </summary>
public static partial class AppScanner
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
}
