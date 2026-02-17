using System.Diagnostics;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.Core.Runner;

/// <summary>
/// Passive background element tracker.
/// Runs as a background thread during scenario execution,
/// outputting [WATCH] tagged lines when the element under the tracking point changes.
///
/// Tracking priority:
///   1. Test action point (from ActionExecutor via RuntimeContext.LastActionPoint)
///   2. Mouse cursor position (fallback when no test action point)
///
/// If the test target window is occluded, brings it to front automatically.
/// </summary>
public sealed class BackgroundWatcher : IDisposable
{
    private readonly int _intervalMs;
    private readonly object _consoleLock;
    private readonly RuntimeContext? _ctx;
    private Thread? _thread;
    private volatile bool _running;
    private UiaLocator? _uia;

    // Stats
    private int _changeCount;
    private readonly HashSet<string> _seenElements = new();
    private readonly Stopwatch _uptime = new();

    // Last state for change detection
    private string _lastElemKey = "";
    private int _lastPtX = -1, _lastPtY = -1;
    private bool _lineHasContent;

    // Track last action point to detect changes
    private DateTime _lastActionTimestamp = DateTime.MinValue;
    private bool _lastWasTestPoint;

    // Nudge signal: wakes up the background thread for immediate poll
    private readonly ManualResetEventSlim _nudgeSignal = new(false);
    // Ack signal: background thread sets this after processing a nudge
    private readonly ManualResetEventSlim _nudgeAck = new(false);

    /// <summary>
    /// Create a background watcher.
    /// </summary>
    /// <param name="intervalMs">Polling interval in ms (default: 200)</param>
    /// <param name="consoleLock">Shared lock for console output (shared with ScenarioRunner)</param>
    /// <param name="ctx">Runtime context for reading test action points. Null = mouse-only mode.</param>
    public BackgroundWatcher(int intervalMs = 200, object? consoleLock = null, RuntimeContext? ctx = null)
    {
        _intervalMs = intervalMs;
        _consoleLock = consoleLock ?? new object();
        _ctx = ctx;
    }

    /// <summary>
    /// The shared console lock. ScenarioRunner should use this same lock for [RUN] output.
    /// </summary>
    public object ConsoleLock => _consoleLock;

    /// <summary>
    /// Force an immediate watch output for the current action point.
    /// Called by ScenarioRunner after each action completes.
    /// Signals the background thread to wake up and poll immediately,
    /// then waits for acknowledgment (max 100ms) to ensure output before [RUN] continues.
    /// (UIA COM objects must be used on the thread that created them.)
    /// </summary>
    public void Nudge()
    {
        if (!_running) return;
        _nudgeAck.Reset();
        _nudgeSignal.Set();
        // Wait for background thread to process — ensures [⚡TEST] appears before [RUN] result
        // User preference: "테스트입력은 사람보다 빠를필요는 없으므로 와치출력이 완료된 이후에 해도 될것 같아요"
        // Use generous timeout (2s) to guarantee watch output completes before next step
        _nudgeAck.Wait(2000);
    }

    /// <summary>
    /// Start passive background watching.
    /// </summary>
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

    /// <summary>
    /// Stop the watcher and print summary.
    /// </summary>
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
                // Finalize any pending \r line
                if (_lineHasContent)
                    Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("[WATCH] ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Stopped. {_changeCount} changes, {_seenElements.Count} unique elements in {_uptime.Elapsed:mm\\:ss\\.f}");
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
                // Check if nudged (force immediate poll)
                bool wasNudged = _nudgeSignal.IsSet;
                if (wasNudged) _nudgeSignal.Reset();

                try
                {
                    PollOnce(force: wasNudged);
                }
                catch
                {
                    // Silently skip errors — passive mode, don't disrupt run
                }

                // Acknowledge nudge so caller can proceed
                if (wasNudged) _nudgeAck.Set();

                // Wait for interval OR nudge signal (whichever comes first)
                _nudgeSignal.Wait(_intervalMs);
            }
        }
        catch
        {
            // Thread died — that's ok for passive mode
        }
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
        // Priority: test action point > mouse cursor
        POINT pt;
        bool isTestPoint = false;
        bool isNewAction = false;  // true = new test action since last poll
        string? actionLabel = null;

        var actionPt = _ctx?.LastActionPoint;
        if (actionPt != null && actionPt.Timestamp > _lastActionTimestamp)
        {
            // New test action point available — this is the primary change signal
            pt = new POINT { X = actionPt.X, Y = actionPt.Y };
            isTestPoint = true;
            isNewAction = true;
            actionLabel = actionPt.Action;
            _lastActionTimestamp = actionPt.Timestamp;
        }
        else if (actionPt != null && _lastWasTestPoint)
        {
            // Same test point, keep tracking it (don't fall back to mouse mid-step)
            pt = new POINT { X = actionPt.X, Y = actionPt.Y };
            isTestPoint = true;
            actionLabel = actionPt.Action;
        }
        else
        {
            // No test action point — fall back to mouse cursor
            NativeMethods.GetCursorPos(out pt);
        }

        _lastWasTestPoint = isTestPoint;

        // ── Find window at point ────────────────────────────
        IntPtr hWndTarget;
        string targetClass;

        if (isTestPoint && _ctx?.MainWindowHandle != IntPtr.Zero)
        {
            // Test mode: use the test target window directly
            hWndTarget = _ctx!.MainWindowHandle;
            targetClass = WindowFinder.GetClassName(hWndTarget);

            // NOTE: UIA works even when window is occluded, no forced focus needed.
            // Vision fallback would need EnsureWindowVisible() here in the future.
        }
        else
        {
            // Mouse mode: probe from point
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
            // For test points, always query within the target window tree
            elemInfo = _uia.GetElementAtPointInWindow(pt.X, pt.Y, hWndTarget);
        }
        else
        {
            // Mouse mode: full probe with overlay detection
            var hWndTop2 = NativeMethods.WindowFromPoint(pt);
            var topClass2 = WindowFinder.GetClassName(hWndTop2);
            overlayDetected = (hWndTarget != hWndTop2);

            if (overlayDetected)
            {
                elemInfo = _uia.GetElementAtPointInWindow(pt.X, pt.Y, hWndTarget);
            }
            else
            {
                elemInfo = _uia.GetElementAtPoint(pt.X, pt.Y);

                // Check for known overlay elements
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

        // Test mode: new action timestamp = always significant change
        // (even if same element at same position — e.g. pressing Escape twice on same window center)
        // Mouse mode: only react to element/position changes
        if (!force && !isNewAction && !elementChanged && !posChanged) return;

        // Full output (with hierarchy path) when:
        //   - Nudge (force): shell-prompt style, always before/after each step
        //   - New test action: action timestamp changed = new input happened
        //   - Element changed: different UI element under point
        bool fullOutput = force || isNewAction || elementChanged;

        _lastPtX = pt.X;
        _lastPtY = pt.Y;
        var ts = DateTime.Now.ToString("HH:mm:ss.fff");

        // Build hierarchy path for full output
        string? hierarchyPath = null;
        if (fullOutput)
        {
            try
            {
                hierarchyPath = _uia?.GetHierarchyPath(pt.X, pt.Y, hWndTarget, useWindowTree: isTestPoint || overlayDetected);
            }
            catch { }
        }

        // ── Tag: always [WATCH] ──────────────────────────────
        string tag = "WATCH";
        var tagColor = ConsoleColor.DarkGray;

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

                // Finalize previous \r line
                if (_lineHasContent)
                    Console.WriteLine();

                // Line 1: combined hierarchy path
                if (!string.IsNullOrEmpty(hierarchyPath))
                {
                    Console.ForegroundColor = tagColor;
                    Console.Write($"[{tag}] ");

                    // Split at "] " to color brackets differently
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

                // Line 2: timestamp + coords + element detail (\r overwritable)
                Console.ForegroundColor = tagColor;
                Console.Write($"[{tag}] ");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{ts}  ");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"({pt.X,5},{pt.Y,5})  ");

                WriteElement(elemInfo);

                // Show action label for test points
                if (isTestPoint && actionLabel != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"  ← {actionLabel}");
                }

                Console.ResetColor();
                _lineHasContent = true;
            }
            else if (posChanged && _lineHasContent)
            {
                // Same element, position moved → \r overwrite (passive polling only)
                ClearCurrentLine();

                Console.ForegroundColor = tagColor;
                Console.Write($"[{tag}] ");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{ts}  ");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"({pt.X,5},{pt.Y,5})  ");

                WriteElement(elemInfo);

                if (isTestPoint && actionLabel != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"  ← {actionLabel}");
                }

                Console.ResetColor();
            }
        }
    }

    private static void WriteElement(ElementAtPointInfo? info)
    {
        if (info == null)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("[?]");
            return;
        }

        // Control type with color
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
