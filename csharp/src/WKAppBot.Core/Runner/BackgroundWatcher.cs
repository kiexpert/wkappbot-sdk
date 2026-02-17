using System.Diagnostics;
using System.Drawing;
using WKAppBot.Vision;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.Core.Runner;

/// <summary>
/// Passive background element tracker with optional OCR measurement.
///
/// Output layout (stable LEFT → volatile RIGHT):
///
/// Line 1: [WATCH] [ClassPath] UIA name hierarchy path
/// Line 2: [WATCH] [Type] "Name" aid="id" (patterns)
/// Line 3: [WATCH] OCR="text" conf=0.95★  UIA↔OCR=90%  watch/001.png    (nudge only)
/// Line 4: [WATCH] ← action  (x,y) HH:mm:ss.fff                        (volatile, \r)
///
/// OCR runs only on nudge (force=true) to avoid CPU waste.
/// Screenshots saved to {VisionCacheDir}/watch/ on each nudge.
/// </summary>
public sealed class BackgroundWatcher : IDisposable
{
    private readonly int _intervalMs;
    private readonly object _consoleLock;
    private readonly RuntimeContext? _ctx;
    private readonly SimpleOcrAnalyzer? _ocr;
    private Thread? _thread;
    private volatile bool _running;
    private UiaLocator? _uia;

    // Stats
    private int _changeCount;
    private int _ocrRunCount;
    private readonly HashSet<string> _seenElements = new();
    private readonly Stopwatch _uptime = new();

    // Last state for change detection
    private string _lastElemKey = "";
    private int _lastPtX = -1, _lastPtY = -1;
    private bool _lineHasContent;

    // Cache the stable prefix so \r can just redraw coords+timestamp
    private string _stablePrefix = "";

    // Track last action point to detect changes
    private DateTime _lastActionTimestamp = DateTime.MinValue;
    private bool _lastWasTestPoint;

    // Nudge signal + ack
    private readonly ManualResetEventSlim _nudgeSignal = new(false);
    private readonly ManualResetEventSlim _nudgeAck = new(false);

    /// <param name="ocr">Optional OCR analyzer. When provided + OcrPreview, runs OCR on nudge.</param>
    public BackgroundWatcher(
        int intervalMs = 200,
        object? consoleLock = null,
        RuntimeContext? ctx = null,
        SimpleOcrAnalyzer? ocr = null)
    {
        _intervalMs = intervalMs;
        _consoleLock = consoleLock ?? new object();
        _ctx = ctx;
        _ocr = ocr;
    }

    public object ConsoleLock => _consoleLock;

    public void Nudge()
    {
        if (!_running) return;
        _nudgeAck.Reset();
        _nudgeSignal.Set();
        _nudgeAck.Wait(2000);
    }

    public void Start()
    {
        if (_running) return;
        _running = true;
        _uptime.Start();

        _thread = new Thread(WatchLoop)
        {
            IsBackground = true,
            Name = "WKAppBot.BackgroundWatcher",
            Priority = ThreadPriority.BelowNormal
        };
        _thread.Start();
    }

    public void Stop(bool printSummary = true)
    {
        if (!_running) return;
        _running = false;
        _uptime.Stop();
        _thread?.Join(1000);

        if (printSummary && _changeCount > 0)
        {
            lock (_consoleLock)
            {
                if (_lineHasContent)
                    Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("[WATCH] ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                var ocrInfo = _ocrRunCount > 0 ? $", {_ocrRunCount} OCR scans" : "";
                Console.WriteLine($"Stopped. {_changeCount} changes, {_seenElements.Count} unique elements{ocrInfo} in {_uptime.Elapsed:mm\\:ss\\.f}");
                Console.ResetColor();
            }
        }
    }

    private void WatchLoop()
    {
        try
        {
            _uia = new UiaLocator();

            while (_running)
            {
                bool wasNudged = _nudgeSignal.IsSet;
                if (wasNudged) _nudgeSignal.Reset();

                try { PollOnce(force: wasNudged); }
                catch { }

                if (wasNudged) _nudgeAck.Set();

                _nudgeSignal.Wait(_intervalMs);
            }
        }
        catch { }
        finally
        {
            _uia?.Dispose();
            _uia = null;
        }
    }

    private void PollOnce(bool force = false)
    {
        if (_uia == null) return;

        // ── Determine tracking point ────────────────────────
        POINT pt;
        bool isTestPoint = false;
        bool isNewAction = false;
        string? actionLabel = null;

        var actionPt = _ctx?.LastActionPoint;
        if (actionPt != null && actionPt.Timestamp > _lastActionTimestamp)
        {
            pt = new POINT { X = actionPt.X, Y = actionPt.Y };
            isTestPoint = true;
            isNewAction = true;
            actionLabel = actionPt.Action;
            _lastActionTimestamp = actionPt.Timestamp;
        }
        else if (actionPt != null && _lastWasTestPoint)
        {
            pt = new POINT { X = actionPt.X, Y = actionPt.Y };
            isTestPoint = true;
            actionLabel = actionPt.Action;
        }
        else
        {
            NativeMethods.GetCursorPos(out pt);
        }

        _lastWasTestPoint = isTestPoint;

        // ── Find window at point ────────────────────────────
        IntPtr hWndTarget;
        string targetClass;

        if (isTestPoint && _ctx?.MainWindowHandle != IntPtr.Zero)
        {
            hWndTarget = _ctx!.MainWindowHandle;
            targetClass = WindowFinder.GetClassName(hWndTarget);
        }
        else
        {
            var hWndTop = NativeMethods.WindowFromPoint(pt);
            var topClass = WindowFinder.GetClassName(hWndTop);
            hWndTarget = NativeMethods.FindRealWindowFromPoint(pt, hWndTop, topClass);
            targetClass = (hWndTarget == hWndTop) ? topClass : WindowFinder.GetClassName(hWndTarget);
        }

        // ── Get UIA element at point ────────────────────────
        ElementAtPointInfo? elemInfo;
        bool overlayDetected = false;

        if (isTestPoint)
        {
            elemInfo = _uia.GetElementAtPointInWindow(pt.X, pt.Y, hWndTarget);
        }
        else
        {
            var hWndTop2 = NativeMethods.WindowFromPoint(pt);
            overlayDetected = (hWndTarget != hWndTop2);

            if (overlayDetected)
            {
                elemInfo = _uia.GetElementAtPointInWindow(pt.X, pt.Y, hWndTarget);
            }
            else
            {
                elemInfo = _uia.GetElementAtPoint(pt.X, pt.Y);

                if (elemInfo != null && IsOverlayElement(elemInfo.AutomationId, targetClass))
                {
                    overlayDetected = true;
                    var nextHwnd = NativeMethods.GetWindow(hWndTarget, NativeMethods.GW_HWNDNEXT);
                    int skip = 0;
                    while (nextHwnd != IntPtr.Zero && skip < 30)
                    {
                        skip++;
                        if (NativeMethods.IsWindowVisible(nextHwnd))
                        {
                            NativeMethods.GetWindowRect(nextHwnd, out var rc);
                            if (NativeMethods.PtInRect(ref rc, pt))
                            {
                                hWndTarget = nextHwnd;
                                targetClass = WindowFinder.GetClassName(nextHwnd);
                                elemInfo = _uia.GetElementAtPointInWindow(pt.X, pt.Y, nextHwnd);
                                break;
                            }
                        }
                        nextHwnd = NativeMethods.GetWindow(nextHwnd, NativeMethods.GW_HWNDNEXT);
                    }
                }
            }
        }

        // ── Change detection ────────────────────────────────
        string elemKey = elemInfo != null
            ? $"{elemInfo.ControlType}|{elemInfo.AutomationId}|{elemInfo.Name}|0x{hWndTarget:X8}"
            : $"?|0x{hWndTarget:X8}";

        bool elementChanged = (elemKey != _lastElemKey);
        bool posChanged = (pt.X != _lastPtX || pt.Y != _lastPtY);

        if (!force && !isNewAction && !elementChanged && !posChanged) return;

        bool fullOutput = force || isNewAction || elementChanged;

        _lastPtX = pt.X;
        _lastPtY = pt.Y;
        var ts = DateTime.Now.ToString("HH:mm:ss.fff");

        // Build hierarchy path
        string? hierarchyPath = null;
        if (fullOutput)
        {
            try
            {
                hierarchyPath = _uia?.GetHierarchyPath(pt.X, pt.Y, hWndTarget, useWindowTree: isTestPoint || overlayDetected);
            }
            catch { }
        }

        // ── OCR: only on nudge (force) + OcrPreview enabled ──
        OcrMatchResult? ocrResult = null;
        string? screenshotPath = null;
        bool doOcr = force && _ocr != null && _ctx?.OcrPreview == true && hWndTarget != IntPtr.Zero;

        if (doOcr)
        {
            try
            {
                using var screenshot = ScreenCapture.CaptureWindow(hWndTarget);

                // Save screenshot
                var watchDir = Path.Combine(_ctx!.VisionCacheDir.Replace("/entries", ""), "watch");
                if (!Directory.Exists(watchDir)) Directory.CreateDirectory(watchDir);

                var safeName = (elemInfo?.AutomationId ?? elemInfo?.Name ?? "unknown")
                    .Replace(" ", "_").Replace("/", "_").Replace("\\", "_");
                if (safeName.Length > 30) safeName = safeName[..30];
                screenshotPath = Path.Combine(watchDir, $"{_ocrRunCount:D3}_{safeName}.png");
                ScreenCapture.SaveToFile(screenshot, screenshotPath);

                // Run OCR if element has text to compare
                var searchText = elemInfo?.Name;
                if (!string.IsNullOrEmpty(searchText))
                {
                    ocrResult = _ocr!.FindElement(screenshot, searchText).GetAwaiter().GetResult();
                }

                _ocrRunCount++;
            }
            catch { /* OCR errors are non-fatal in watch mode */ }
        }

        // ── Output ──────────────────────────────────────────
        lock (_consoleLock)
        {
            if (fullOutput)
            {
                if (elementChanged)
                {
                    _lastElemKey = elemKey;
                    _changeCount++;
                    _seenElements.Add(elemKey);
                }

                if (_lineHasContent)
                    Console.WriteLine();

                // Line 1: [WATCH] [ClassPath] hierarchy
                if (!string.IsNullOrEmpty(hierarchyPath))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[WATCH] ");

                    var bracketEnd = hierarchyPath.IndexOf("] ");
                    if (bracketEnd >= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(hierarchyPath[..(bracketEnd + 1)]);
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write(hierarchyPath[(bracketEnd + 1)..]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write(hierarchyPath);
                    }

                    if (overlayDetected && !isTestPoint)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"  !! overlay");
                    }
                    Console.WriteLine();
                }

                // Line 2: [WATCH] [Type] "Name" aid="id" (patterns)
                WriteStablePart(elemInfo);

                // Line 3: [WATCH] OCR results (nudge only)
                if (ocrResult != null || screenshotPath != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[WATCH] ");

                    if (ocrResult != null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"OCR=\"{ocrResult.MatchedText}\"");

                        var confColor = ocrResult.Confidence >= 0.9 ? ConsoleColor.Green
                                      : ocrResult.Confidence >= 0.7 ? ConsoleColor.Yellow
                                      : ConsoleColor.Red;
                        Console.ForegroundColor = confColor;
                        Console.Write($" conf={ocrResult.Confidence:F2}");
                        if (ocrResult.Confidence >= 0.9) Console.Write("\u2605"); // ★

                        // UIA↔OCR match rate
                        if (elemInfo?.Name != null)
                        {
                            double matchRate = CalculateTextMatchRate(elemInfo.Name, ocrResult.MatchedText);
                            var mrColor = matchRate >= 0.9 ? ConsoleColor.Green
                                        : matchRate >= 0.7 ? ConsoleColor.Yellow
                                        : ConsoleColor.Red;
                            Console.ForegroundColor = mrColor;
                            Console.Write($"  UIA\u2194OCR={matchRate:P0}");
                            if (matchRate >= 0.9) Console.Write("\u2605");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("OCR=(no match)");
                    }

                    if (screenshotPath != null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"  {screenshotPath}");
                    }

                    Console.WriteLine();
                }

                // Line 4 (volatile): ← action  (x,y) timestamp
                WriteActionLine(isTestPoint, actionLabel);
                WriteVolatilePart(pt, ts);

                Console.ResetColor();
                _lineHasContent = true;
            }
            else if (posChanged && _lineHasContent)
            {
                ClearCurrentLine();
                WriteStablePrefixRaw();
                WriteVolatilePart(pt, ts);
                Console.ResetColor();
            }
        }
    }

    /// <summary>
    /// Write element stable part (Line 2) and cache for \r redraws.
    /// </summary>
    private void WriteStablePart(ElementAtPointInfo? info)
    {
        var sb = new System.Text.StringBuilder();
        sb.Append("[WATCH] ");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("[WATCH] ");

        WriteElement(info);
        sb.Append(GetElementPlain(info));

        Console.WriteLine();
        // _stablePrefix used for Line 4 \r redraws
    }

    /// <summary>
    /// Write action + pad for volatile (Line 4 left side), cache for \r.
    /// </summary>
    private void WriteActionLine(bool isTestPoint, string? actionLabel)
    {
        var sb = new System.Text.StringBuilder();
        sb.Append("[WATCH] ");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("[WATCH] ");

        if (isTestPoint && actionLabel != null)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"\u2190 {actionLabel}  ");
            sb.Append($"<- {actionLabel}  ");
        }

        _stablePrefix = sb.ToString();
    }

    /// <summary>
    /// Redraw cached stable prefix for \r overwrites.
    /// </summary>
    private void WriteStablePrefixRaw()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(_stablePrefix);
    }

    /// <summary>
    /// Write volatile part: coordinates + timestamp (rightmost, \r updatable).
    /// </summary>
    private static void WriteVolatilePart(POINT pt, string ts)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write($"({pt.X,5},{pt.Y,5})");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($" {ts}");
    }

    /// <summary>
    /// Calculate text match rate between UIA name and OCR text.
    /// </summary>
    private static double CalculateTextMatchRate(string uiaText, string ocrText)
    {
        if (string.IsNullOrEmpty(uiaText) || string.IsNullOrEmpty(ocrText)) return 0;

        var a = uiaText.ToLowerInvariant().Trim();
        var b = ocrText.ToLowerInvariant().Trim();

        if (a == b) return 1.0;
        if (a.Contains(b) || b.Contains(a)) return 0.9;

        // Dice coefficient
        var setA = new HashSet<char>(a.Where(c => !char.IsWhiteSpace(c)));
        var setB = new HashSet<char>(b.Where(c => !char.IsWhiteSpace(c)));
        if (setA.Count == 0 || setB.Count == 0) return 0;
        int intersection = setA.Intersect(setB).Count();
        return 2.0 * intersection / (setA.Count + setB.Count);
    }

    private static void WriteElement(ElementAtPointInfo? info)
    {
        if (info == null)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("[?]");
            return;
        }

        Console.ForegroundColor = info.ControlType switch
        {
            "Button" => ConsoleColor.Yellow,
            "Edit" => ConsoleColor.Cyan,
            "Text" => ConsoleColor.Gray,
            "List" or "ListItem" or "DataGrid" or "DataItem" => ConsoleColor.Magenta,
            "ComboBox" => ConsoleColor.Blue,
            "CheckBox" or "RadioButton" => ConsoleColor.DarkYellow,
            "Window" or "Pane" => ConsoleColor.DarkGray,
            _ => ConsoleColor.White
        };
        Console.Write($"[{info.ControlType}]");

        if (!string.IsNullOrEmpty(info.Name))
        {
            Console.ForegroundColor = ConsoleColor.White;
            var display = info.Name.Length > 25 ? info.Name[..25] + "..." : info.Name;
            Console.Write($" \"{display}\"");
        }

        if (!string.IsNullOrEmpty(info.AutomationId))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($" aid=\"{info.AutomationId}\"");
        }

        if (info.Patterns.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($" ({string.Join(",", info.Patterns)})");
        }
    }

    private static string GetElementPlain(ElementAtPointInfo? info)
    {
        if (info == null) return "[?]";

        var sb = new System.Text.StringBuilder();
        sb.Append($"[{info.ControlType}]");

        if (!string.IsNullOrEmpty(info.Name))
        {
            var display = info.Name.Length > 25 ? info.Name[..25] + "..." : info.Name;
            sb.Append($" \"{display}\"");
        }

        if (!string.IsNullOrEmpty(info.AutomationId))
            sb.Append($" aid=\"{info.AutomationId}\"");

        if (info.Patterns.Count > 0)
            sb.Append($" ({string.Join(",", info.Patterns)})");

        return sb.ToString();
    }

    private static bool IsOverlayElement(string? automationId, string className)
    {
        if (className.StartsWith("Chrome_WidgetWin") &&
            (automationId == "BTN_BKGRND" || automationId == ""))
            return true;
        return false;
    }

    private static void ClearCurrentLine()
    {
        int w;
        try { w = Console.WindowWidth; } catch { w = 120; }
        Console.Write("\r" + new string(' ', Math.Max(w - 1, 80)) + "\r");
    }

    public void Dispose()
    {
        Stop(printSummary: false);
        _nudgeSignal.Dispose();
        _nudgeAck.Dispose();
    }
}
