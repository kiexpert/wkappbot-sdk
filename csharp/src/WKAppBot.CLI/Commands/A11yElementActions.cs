using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

// Element-level a11y action implementations (partial class of Program)
// Split from A11yCommand.cs per ~400 line file size policy
internal partial class Program
{
    // -- Highlight: show zoom/highlight overlay on target element --
    static bool A11yHighlight(AutomationElement el, IntPtr hwnd, int durationMs = 3000)
    {
        var rect = GetBoundingRect(el);
        if (rect == null)
        {
            Console.Error.WriteLine("[A11Y] highlight — no BoundingRect available");
            return false;
        }

        var name = el.Properties.Name.ValueOrDefault ?? "";
        var type = "?";
        try { type = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
        var aid = el.Properties.AutomationId.ValueOrDefault ?? "";

        // Try element hwnd first (better capture), fall back to screen rect
        var elHwnd = GetElementHwnd(el);
        ClickZoomHelper? zoom;
        if (elHwnd != IntPtr.Zero)
            zoom = ClickZoomHelper.Begin(elHwnd, hwnd, "a11y-highlight", $"{type} \"{name}\"");
        else
            zoom = ClickZoomHelper.BeginFromRect(rect.Value, hwnd, "a11y-highlight", $"{type} \"{name}\"");

        if (zoom == null)
        {
            // Fallback: just print position info without overlay
            Console.WriteLine($"[A11Y] highlight — overlay failed, element at ({rect.Value.X},{rect.Value.Y}) {rect.Value.Width}x{rect.Value.Height}");
            return true;
        }

        var label = !string.IsNullOrEmpty(name) ? $"\"{name}\"" : (aid != "" ? $"aid={aid}" : "(unnamed)");
        zoom.UpdateStatus($"[{type}] {label}");
        Console.WriteLine($"[A11Y] highlight — [{type}] {label} at ({rect.Value.X},{rect.Value.Y}) {rect.Value.Width}x{rect.Value.Height}");

        // Show for duration then fade out
        zoom.ShowPass($"{type} {label}");
        Thread.Sleep(durationMs);
        zoom.Dispose();
        return true;
    }

    // -- Find: list Win32 child windows + UIA children tree (MUD game "look" command) --
    static bool A11yFind(AutomationElement root, IntPtr hwnd, int depth)
    {
        bool hasOutput = false;

        // Part 1: Win32 child windows
        var win32Children = WindowFinder.GetChildrenZOrder(hwnd);
        if (win32Children.Count > 0)
        {
            hasOutput = true;
            Console.WriteLine($"[A11Y] Win32 children ({win32Children.Count}):");
            foreach (var child in win32Children)
            {
                var r = child.Rect;
                var w = r.Right - r.Left;
                var h = r.Bottom - r.Top;
                var vis = NativeMethods.IsWindowVisible(child.Handle) ? "" : " [hidden]";
                Console.WriteLine($"[A11Y]   [{child.ClassName}] \"{child.Title}\" hwnd={child.Handle:X8} cid={child.ControlId} {w}x{h}{vis}");
            }
        }

        // Part 2: UIA children tree
        try
        {
            var children = root.FindAllChildren();
            if (children.Length > 0)
            {
                hasOutput = true;
                Console.WriteLine($"[A11Y] UIA children ({children.Length}):");
                foreach (var child in children)
                {
                    DumpUiaElement(child, 1, depth);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] UIA tree error: {ex.Message}");
        }

        if (!hasOutput)
            Console.WriteLine("[A11Y] find — no children found");

        return hasOutput;
    }

    /// <summary>
    /// Recursively dump UIA element with detail: Type, Name, AutomationId, Rect, Patterns.
    /// </summary>
    static void DumpUiaElement(AutomationElement el, int level, int maxDepth)
    {
        if (level > maxDepth) return;

        var indent = new string(' ', level * 2);
        try
        {
            var name = el.Properties.Name.ValueOrDefault ?? "";
            var aid = el.Properties.AutomationId.ValueOrDefault ?? "";
            var type = "?";
            try { type = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }

            // BoundingRect
            var rectStr = "";
            try
            {
                var r = el.Properties.BoundingRectangle.ValueOrDefault;
                if (r.Width > 0)
                    rectStr = $" ({(int)r.X},{(int)r.Y} {(int)r.Width}x{(int)r.Height})";
            }
            catch { }

            // Supported patterns (compact)
            var patterns = GetSupportedPatternNames(el);
            var patStr = patterns.Count > 0 ? $" ({string.Join(",", patterns)})" : "";

            // NativeWindowHandle
            var nhStr = "";
            var nh = GetElementHwnd(el);
            if (nh != IntPtr.Zero)
                nhStr = $" hwnd={nh:X8}";

            // Label: prefer name, then aid
            var label = !string.IsNullOrEmpty(name) ? $"\"{name}\"" : "";
            var aidStr = !string.IsNullOrEmpty(aid) ? $" aid=\"{aid}\"" : "";

            Console.WriteLine($"[A11Y] {indent}[{type}] {label}{aidStr}{nhStr}{rectStr}{patStr}");

            // Recurse children
            if (level < maxDepth)
            {
                try
                {
                    foreach (var child in el.FindAllChildren())
                        DumpUiaElement(child, level + 1, maxDepth);
                }
                catch { }
            }
        }
        catch { /* COM timeout on some elements */ }
    }

    /// <summary>Get compact list of supported UIA pattern names.</summary>
    static List<string> GetSupportedPatternNames(AutomationElement el)
    {
        var names = new List<string>();
        try { if (el.Patterns.Invoke.IsSupported) names.Add("Invoke"); } catch { }
        try { if (el.Patterns.Value.IsSupported) names.Add("Value"); } catch { }
        try { if (el.Patterns.Toggle.IsSupported) names.Add("Toggle"); } catch { }
        try { if (el.Patterns.SelectionItem.IsSupported) names.Add("Select"); } catch { }
        try { if (el.Patterns.ExpandCollapse.IsSupported) names.Add("Expand"); } catch { }
        try { if (el.Patterns.Scroll.IsSupported) names.Add("Scroll"); } catch { }
        try { if (el.Patterns.RangeValue.IsSupported) names.Add("Range"); } catch { }
        try { if (el.Patterns.Window.IsSupported) names.Add("Window"); } catch { }
        try { if (el.Patterns.Transform.IsSupported) names.Add("Transform"); } catch { }
        try { if (el.Patterns.Grid.IsSupported) names.Add("Grid"); } catch { }
        try { if (el.Patterns.LegacyIAccessible.IsSupported) names.Add("LegacyIA"); } catch { }
        return names;
    }

    // FLUTTERVIEW child = direct Flutter input pipeline (focusless, ~0ms) — no class whitelist needed
    static IntPtr FindFlutterViewChild(IntPtr hwnd) =>
        NativeMethods.FindWindowExW(hwnd, IntPtr.Zero, "FLUTTERVIEW", null);

    // TODO: when nullMs >= 100 (window lagging), skip verbose UIA tree/prop output in inspect/find
    //       GetNullMs() is already available — just gate heavy output behind nullMs < 100 check

    // Hollow invoke detection via Win32 SetProp/GetProp:
    //   GetPropW(elHwnd, InvokeHollowProp) != 0  →  UIA Invoke is hollow for this hwnd
    //   Prop is auto-cleaned when the window closes — no stale data.
    //   On hollow confirmed: SetProp stamps the hwnd + fires ActionApi.OnInvokeHollow → knowhow

    // WM_NULL baseline cache: pre-measured during read/find, reused by invoke (TTL = 3s)
    static readonly System.Collections.Concurrent.ConcurrentDictionary<IntPtr, (long nullMs, long ticks)> _nullCache = new();
    static readonly long NullCacheTtlTicks = 3 * System.Diagnostics.Stopwatch.Frequency; // 3 seconds

    /// <summary>Send WM_NULL to hwnd and cache the roundtrip time. Call during read/find so invoke gets it for free.</summary>
    internal static void PreheatWindow(IntPtr hwnd)
    {
        if (hwnd == IntPtr.Zero) return;
        var sw = System.Diagnostics.Stopwatch.StartNew();
        NativeMethods.SendMessageTimeoutW(hwnd, NativeMethods.WM_NULL,
            IntPtr.Zero, IntPtr.Zero, NativeMethods.SMTO_ABORTIFHUNG, 100, out _);
        sw.Stop();
        _nullCache[hwnd] = (sw.ElapsedMilliseconds, System.Diagnostics.Stopwatch.GetTimestamp());
    }

    static long GetNullMs(IntPtr hwnd)
    {
        if (hwnd != IntPtr.Zero && _nullCache.TryGetValue(hwnd, out var entry))
        {
            if (System.Diagnostics.Stopwatch.GetTimestamp() - entry.ticks < NullCacheTtlTicks)
                return entry.nullMs; // fresh cache
        }
        // Cache miss or stale — measure now
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var ret = NativeMethods.SendMessageTimeoutW(hwnd, NativeMethods.WM_NULL,
            IntPtr.Zero, IntPtr.Zero, NativeMethods.SMTO_ABORTIFHUNG, 100, out _);
        sw.Stop();
        var ms = sw.ElapsedMilliseconds;
        _nullCache[hwnd] = (ms, System.Diagnostics.Stopwatch.GetTimestamp());
        return ret == IntPtr.Zero ? 100 : ms; // timeout → treat as 100ms (lagging)
    }

    // Returns true = confirmed hollow (no paint after 50ms), false = paint detected (real invoke)
    static bool ConfirmHollow(IntPtr elHwnd)
    {
        if (elHwnd == IntPtr.Zero) return true;
        NativeMethods.ValidateRect(elHwnd, IntPtr.Zero);   // clear any pre-existing dirty region
        Thread.Sleep(50);                                   // let message pump process results
        bool hasPaint = NativeMethods.GetUpdateRect(elHwnd, IntPtr.Zero, false);
        return !hasPaint; // no paint = hollow confirmed
    }

    // -- Invoke: FLUTTERVIEW Tier0 -> UIA Invoke -> BM_CLICK -> WM_LBUTTON fallback --
    static bool A11yInvoke(AutomationElement el, IntPtr hwnd)
    {
        var elHwnd = GetElementHwnd(el);

        // Tier 0: FLUTTERVIEW direct PostMessage — bypasses Win32 routing, focusless, ~0ms
        if (elHwnd != IntPtr.Zero)
        {
            var flutterView = FindFlutterViewChild(elHwnd);
            if (flutterView != IntPtr.Zero)
            {
                Console.WriteLine("[A11Y] invoke — Tier0 FLUTTERVIEW (focusless direct)");
                return PostClickToFlutterView(el, flutterView);
            }
        }

        // Tier 1: UIA Invoke (focusless)
        // Skip if hwnd was previously confirmed hollow (Win32 prop, auto-cleared on window close)
        bool propHollow = elHwnd != IntPtr.Zero &&
                          NativeMethods.GetPropW(elHwnd, ActionApi.InvokeHollowProp) != IntPtr.Zero;
        if (propHollow)
        {
            Console.WriteLine("[A11Y] invoke — UIA Invoke skipped (prop=hollow) → next tier");
        }
        else
        {
            // WM_NULL baseline: no-op roundtrip measures system responsiveness.
            // If invoke ≈ null_ms → hollow stub. If null_ms ≥ 100ms → window is lagging, skip invoke entirely.
            long nullMs = 0;
            if (elHwnd != IntPtr.Zero)
            {
                nullMs = GetNullMs(elHwnd); // cached from read/find, or measures now
                if (nullMs >= 100)
                {
                    Console.WriteLine($"[A11Y] invoke — window lagging (WM_NULL={nullMs}ms) → skip UIA Invoke");
                    goto tier2;
                }
            }

            var prevFg = NativeMethods.GetForegroundWindow();
            var sw = System.Diagnostics.Stopwatch.StartNew();
            bool invoked = UiaLocator.TryInvoke(el);
            sw.Stop();
            long invokeMs = sw.ElapsedMilliseconds;

            if (invoked)
            {
                // Restore focus if stolen (even hollow stubs can activate target window)
                var curFg = NativeMethods.GetForegroundWindow();
                if (curFg != prevFg && prevFg != IntPtr.Zero)
                {
                    NativeMethods.SetForegroundWindow(prevFg);
                    Console.WriteLine($"[A11Y] invoke — UIA Invoke focus restored ({curFg:X8}→{prevFg:X8})");
                }

                if (invokeMs > 1000)
                {
                    Console.WriteLine($"[A11Y] invoke — UIA Invoke ⚠ BLOCKED ({invokeMs}ms, null={nullMs}ms)");
                    return true; // best effort
                }

                // Suspicious if invoke is no faster than a no-op WM_NULL (+2ms margin)
                bool suspiciousTiming = invokeMs <= nullMs + 2;
                Console.WriteLine($"[A11Y] invoke — UIA Invoke {invokeMs}ms (null={nullMs}ms){(suspiciousTiming ? " ⚠ suspicious" : "")}");

                if (suspiciousTiming)
                {
                    // Confirm via paint detection: ValidateRect → invoke → Sleep(50) → GetUpdateRect
                    bool hollow = elHwnd != IntPtr.Zero && ConfirmHollow(elHwnd);
                    if (hollow)
                    {
                        NativeMethods.SetPropW(elHwnd, ActionApi.InvokeHollowProp, (IntPtr)1);
                        var clsB = new System.Text.StringBuilder(64);
                        NativeMethods.GetClassNameW(elHwnd, clsB, clsB.Capacity);
                        ActionApi.OnInvokeHollow?.Invoke(elHwnd, clsB.ToString());
                        Console.WriteLine($"[A11Y] invoke — UIA Invoke ✗ hollow confirmed (no paint) → prop stamped, trying next tier");
                    }
                    else
                    {
                        Console.WriteLine($"[A11Y] invoke — UIA Invoke ✓ real (paint detected)");
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        // Tier 2: BM_CLICK (focusless)
        tier2:
        if (elHwnd != IntPtr.Zero)
        {
            NativeMethods.PostMessageW(elHwnd, 0x00F5 /* BM_CLICK */, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine("[A11Y] invoke — Win32 BM_CLICK");
            return true;
        }

        // Tier 3: WM_LBUTTON / SendInput (via A11yClick)
        return A11yClick(el, hwnd);
    }

    // -- Click: BoundingRect center -> SendInput (requires focus) --
    static bool A11yClick(AutomationElement el, IntPtr hwnd)
    {
        var rect = GetBoundingRect(el);
        if (rect == null)
        {
            Console.Error.WriteLine("[A11Y] click — no BoundingRect available");
            return false;
        }

        int cx = (rect.Value.Left + rect.Value.Right) / 2;
        int cy = (rect.Value.Top + rect.Value.Bottom) / 2;

        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                var defAction = legacy.Pattern.DefaultAction.ValueOrDefault;
                if (!string.IsNullOrEmpty(defAction))
                {
                    legacy.Pattern.DoDefaultAction();
                    Console.WriteLine($"[A11Y] click — LegacyIA DoDefaultAction at ({cx},{cy})");
                    return true;
                }
            }
        }
        catch { }

        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            // FLUTTERVIEW child? Route directly (bypasses Win32 routing)
            var flutterView = FindFlutterViewChild(elHwnd);
            if (flutterView != IntPtr.Zero)
            {
                Console.WriteLine($"[A11Y] click — FLUTTERVIEW direct at ({cx},{cy})");
                return PostClickToFlutterView(el, flutterView);
            }

            var pt = new POINT { X = cx, Y = cy };
            NativeMethods.ScreenToClient(elHwnd, ref pt);
            var lParam = (IntPtr)(pt.X | (pt.Y << 16));
            NativeMethods.PostMessageW(elHwnd, NativeMethods.WM_LBUTTONDOWN, (IntPtr)1, lParam);
            Thread.Sleep(50);
            NativeMethods.PostMessageW(elHwnd, NativeMethods.WM_LBUTTONUP, IntPtr.Zero, lParam);
            Console.WriteLine($"[A11Y] click — Win32 WM_LBUTTON at ({cx},{cy})");
            return true;
        }

        NativeMethods.SetCursorPos(cx, cy);
        Thread.Sleep(30);
        var inputs = new INPUT[2];
        inputs[0].type = INPUT.INPUT_MOUSE;
        inputs[0].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_LEFTDOWN;
        inputs[1].type = INPUT.INPUT_MOUSE;
        inputs[1].u.mi.dwFlags = MouseFlags.MOUSEEVENTF_LEFTUP;
        NativeMethods.SendInput(2, inputs, System.Runtime.InteropServices.Marshal.SizeOf<INPUT>());
        Console.WriteLine($"[A11Y] click — SendInput at ({cx},{cy})");
        return true;
    }

    // Send WM_LBUTTON to FLUTTER_VIEW child at element's center (screen→client conversion)
    static bool PostClickToFlutterView(AutomationElement el, IntPtr flutterView)
    {
        var rect = GetBoundingRect(el);
        int cx, cy;
        if (rect != null)
        {
            cx = (rect.Value.Left + rect.Value.Right) / 2;
            cy = (rect.Value.Top + rect.Value.Bottom) / 2;
        }
        else
        {
            Console.Error.WriteLine("[A11Y] flutter-click — no BoundingRect, using FLUTTERVIEW center");
            NativeMethods.GetClientRect(flutterView, out var fb);
            var fp = new POINT { X = (fb.Left + fb.Right) / 2, Y = (fb.Top + fb.Bottom) / 2 };
            NativeMethods.ClientToScreen(flutterView, ref fp);
            cx = fp.X; cy = fp.Y;
        }

        // Clamp to largest inscribed circle of BoundingRect — handles rounded-corner buttons.
        // Circle center = rect center, radius = min(w,h)/2 - margin.
        // If (cx,cy) outside circle → pull toward center along same direction vector.
        if (rect != null)
        {
            const int margin = 4;
            double rcx = (rect.Value.Left + rect.Value.Right)  / 2.0;
            double rcy = (rect.Value.Top  + rect.Value.Bottom) / 2.0;
            double radius = Math.Min(rect.Value.Width, rect.Value.Height) / 2.0 - margin;
            if (radius > 0)
            {
                double dx = cx - rcx, dy = cy - rcy;
                double dist = Math.Sqrt(dx * dx + dy * dy);
                if (dist > radius)
                {
                    int origX = cx, origY = cy;
                    cx = (int)Math.Round(rcx + dx / dist * radius);
                    cy = (int)Math.Round(rcy + dy / dist * radius);
                    Console.WriteLine($"[A11Y] flutter-click — clamped to inscribed circle ({origX},{origY})→({cx},{cy}) r={radius:F0}");
                }
            }
        }

        // Convert screen → FLUTTERVIEW client coords
        var pt = new POINT { X = cx, Y = cy };
        NativeMethods.ScreenToClient(flutterView, ref pt);

        var lParam = (IntPtr)(pt.X | (pt.Y << 16));
        NativeMethods.PostMessageW(flutterView, NativeMethods.WM_LBUTTONDOWN, (IntPtr)1, lParam);
        Thread.Sleep(50);
        NativeMethods.PostMessageW(flutterView, NativeMethods.WM_LBUTTONUP, IntPtr.Zero, lParam);
        Console.WriteLine($"[A11Y] flutter-click — FLUTTER_VIEW WM_LBUTTON at screen({cx},{cy}) client({pt.X},{pt.Y})");
        return true;
    }

    // -- Toggle: UIA Toggle -> BM_CLICK fallback --
    static bool A11yToggle(AutomationElement el, IntPtr hwnd)
    {
        var before = UiaLocator.GetToggleState(el);
        if (before != null)
            Console.WriteLine($"[A11Y] toggle state before: {before}");

        if (UiaLocator.TryToggle(el))
        {
            var after = UiaLocator.GetToggleState(el);
            Console.WriteLine($"[A11Y] toggle — UIA Toggle (now: {after})");
            return true;
        }

        Console.WriteLine("[A11Y] toggle — UIA not supported, falling back to invoke");
        return A11yInvoke(el, hwnd);
    }

    // -- Expand/Collapse: UIA ExpandCollapse --
    static bool A11yExpand(AutomationElement el)
    {
        if (UiaLocator.TryExpand(el))
        {
            Console.WriteLine("[A11Y] expand — UIA ExpandCollapse");
            return true;
        }
        Console.Error.WriteLine("[A11Y] expand — not supported on this element");
        return false;
    }

    static bool A11yCollapse(AutomationElement el)
    {
        if (UiaLocator.TryCollapse(el))
        {
            Console.WriteLine("[A11Y] collapse — UIA ExpandCollapse");
            return true;
        }
        Console.Error.WriteLine("[A11Y] collapse — not supported on this element");
        return false;
    }

    // -- Select: UIA SelectionItem --
    static bool A11ySelectItem(AutomationElement el)
    {
        if (UiaLocator.TrySelect(el))
        {
            Console.WriteLine("[A11Y] select — UIA SelectionItem");
            return true;
        }
        Console.WriteLine("[A11Y] select — UIA not supported, falling back to invoke");
        return UiaLocator.TryInvoke(el);
    }

    // -- Scroll: UIA Scroll -> WM_VSCROLL/WM_HSCROLL fallback --
    static bool A11yScrollAction(AutomationElement el, IntPtr hwnd, string direction, string amount)
    {
        var hAmount = ScrollAmount.NoAmount;
        var vAmount = ScrollAmount.NoAmount;

        var scrollAmt = amount == "large" ? ScrollAmount.LargeIncrement : ScrollAmount.SmallIncrement;
        var scrollAmtNeg = amount == "large" ? ScrollAmount.LargeDecrement : ScrollAmount.SmallDecrement;

        switch (direction)
        {
            case "down": vAmount = scrollAmt; break;
            case "up": vAmount = scrollAmtNeg; break;
            case "right": hAmount = scrollAmt; break;
            case "left": hAmount = scrollAmtNeg; break;
            default: return Error($"Invalid scroll direction: {direction}") != 0 ? false : false;
        }

        if (UiaLocator.TryScroll(el, hAmount, vAmount))
        {
            Console.WriteLine($"[A11Y] scroll {direction} ({amount}) — UIA Scroll");
            return true;
        }

        var elHwnd = GetElementHwnd(el);
        if (elHwnd == IntPtr.Zero) elHwnd = hwnd;

        uint msg = (direction == "left" || direction == "right") ? 0x0114u : 0x0115u;
        int wParam = (direction == "down" || direction == "right") ? 1 : 0;
        if (amount == "large")
            wParam = (direction == "down" || direction == "right") ? 3 : 2;

        NativeMethods.PostMessageW(elHwnd, msg, (IntPtr)wParam, IntPtr.Zero);
        Console.WriteLine($"[A11Y] scroll {direction} ({amount}) — Win32 WM_{(msg == 0x0114u ? "H" : "V")}SCROLL");
        return true;
    }

    // Terminal window classes that use ConPTY — WM_CHAR doesn't reach stdin, need SendInput
    static readonly HashSet<string> TerminalClasses = new(StringComparer.OrdinalIgnoreCase)
    {
        "CASCADIA_HOSTING_WINDOW_CLASS", // Windows Terminal
        "ConsoleWindowClass",            // Classic conhost.exe
        "PseudoConsoleWindow",           // ConPTY
        "VirtualConsoleClass",           // misc
    };

    // -- Type: UIA Value -> WM_CHAR -> LegacyIA SetValue --
    static bool A11yType(AutomationElement el, IntPtr hwnd, string text)
    {
        // Check if target is a terminal window — WM_CHAR bypasses ConPTY stdin
        var winClass = WindowFinder.GetClassName(hwnd);
        bool isTerminal = TerminalClasses.Contains(winClass);

        if (!isTerminal)
        {
            try
            {
                var vp = el.Patterns.Value;
                if (vp.IsSupported && !vp.Pattern.IsReadOnly.Value)
                {
                    vp.Pattern.SetValue(text);
                    Console.WriteLine($"[A11Y] type — UIA Value.SetValue ({text.Length} chars)");
                    return true;
                }
            }
            catch { }

            var elHwnd = GetElementHwnd(el);
            if (elHwnd != IntPtr.Zero)
            {
                foreach (char c in text)
                    NativeMethods.PostMessageW(elHwnd, NativeMethods.WM_CHAR, (IntPtr)c, IntPtr.Zero);
                Console.WriteLine($"[A11Y] type — Win32 WM_CHAR ({text.Length} chars)");
                return true;
            }
        }
        else
        {
            Console.WriteLine($"[A11Y] type — terminal ({winClass}): skipping WM_CHAR (ConPTY), using SendInput");
        }

        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                legacy.Pattern.SetValue(text);
                Console.WriteLine($"[A11Y] type — LegacyIA SetValue ({text.Length} chars)");
                return true;
            }
        }
        catch { }

        // Tier 4: SendKeys keystroke fallback (requires focus)
        try
        {
            Console.WriteLine("[A11Y] type — focusless failed, falling back to SendKeys (focus required)");
            NativeMethods.SetForegroundWindow(hwnd);
            Thread.Sleep(100);
            // Pass hwnd for per-token mid-input focus check+restore
            WKAppBot.Win32.Input.KeyboardInput.SendKeys(text, hwnd);
            Console.WriteLine($"[A11Y] type — SendKeys keystroke ({text.Length} chars)");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[A11Y] type — SendKeys failed: {ex.Message}");
        }

        Console.Error.WriteLine("[A11Y] type — no input method available");
        return false;
    }

    // -- SetValue: UIA Value -> WM_SETTEXT -> LegacyIA --
    static bool A11ySetValue(AutomationElement el, IntPtr hwnd, string text)
    {
        try
        {
            var vp = el.Patterns.Value;
            if (vp.IsSupported && !vp.Pattern.IsReadOnly.Value)
            {
                vp.Pattern.SetValue(text);
                Console.WriteLine("[A11Y] set-value — UIA Value.SetValue");
                return true;
            }
        }
        catch { }

        var elHwnd = GetElementHwnd(el);
        if (elHwnd != IntPtr.Zero)
        {
            NativeMethods.SendMessageW(elHwnd, NativeMethods.WM_SETTEXT, IntPtr.Zero, text);
            Console.WriteLine("[A11Y] set-value — Win32 WM_SETTEXT");
            return true;
        }

        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                legacy.Pattern.SetValue(text);
                Console.WriteLine("[A11Y] set-value — LegacyIA SetValue");
                return true;
            }
        }
        catch { }

        Console.Error.WriteLine("[A11Y] set-value — no method available");
        return false;
    }

    // -- Read: dump element's accessible state (TTS friendly) --
    static bool A11yRead(AutomationElement el)
    {
        var lines = new List<string>();

        var name = el.Properties.Name.ValueOrDefault;
        if (!string.IsNullOrEmpty(name))
            lines.Add($"Name: {name}");

        try
        {
            var ct = el.Properties.ControlType.ValueOrDefault;
            lines.Add($"Type: {ct}");
        }
        catch { }

        var aid = el.Properties.AutomationId.ValueOrDefault;
        if (!string.IsNullOrEmpty(aid))
            lines.Add($"AutomationId: {aid}");

        try
        {
            var vp = el.Patterns.Value;
            if (vp.IsSupported)
            {
                var val = vp.Pattern.Value.ValueOrDefault ?? "";
                var ro = vp.Pattern.IsReadOnly.ValueOrDefault;
                lines.Add($"Value: \"{val}\"{(ro ? " (readonly)" : "")}");
            }
        }
        catch { }

        var toggle = UiaLocator.GetToggleState(el);
        if (toggle != null)
            lines.Add($"ToggleState: {toggle}");

        var sel = UiaLocator.IsSelected(el);
        if (sel != null)
            lines.Add($"IsSelected: {sel}");

        var ec = UiaLocator.GetExpandCollapseState(el);
        if (ec != null)
            lines.Add($"ExpandState: {ec}");

        var range = UiaLocator.GetRangeValueInfo(el);
        if (range != null)
            lines.Add($"Range: {range.Value} ({range.Minimum}..{range.Maximum}, step={range.SmallChange}{(range.IsReadOnly ? ", readonly" : "")})");

        try
        {
            var r = el.Properties.BoundingRectangle.ValueOrDefault;
            if (r.Width > 0)
                lines.Add($"Rect: ({(int)r.X},{(int)r.Y}) {(int)r.Width}x{(int)r.Height}");
        }
        catch { }

        try
        {
            var legacy = el.Patterns.LegacyIAccessible;
            if (legacy.IsSupported)
            {
                var desc = legacy.Pattern.Description.ValueOrDefault;
                if (!string.IsNullOrEmpty(desc))
                    lines.Add($"Description: {desc}");
                var help = legacy.Pattern.Help.ValueOrDefault;
                if (!string.IsNullOrEmpty(help))
                    lines.Add($"Help: {help}");
                var defAction = legacy.Pattern.DefaultAction.ValueOrDefault;
                if (!string.IsNullOrEmpty(defAction))
                    lines.Add($"DefaultAction: {defAction}");
            }
        }
        catch { }

        try
        {
            var children = el.FindAllChildren();
            if (children.Length > 0)
            {
                lines.Add($"Children: {children.Length}");
                foreach (var child in children.Take(10))
                {
                    try
                    {
                        var cn = child.Properties.Name.ValueOrDefault ?? "";
                        var cct = "?";
                        try { cct = child.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
                        var caid = child.Properties.AutomationId.ValueOrDefault ?? "";
                        var label = !string.IsNullOrEmpty(cn) ? $"\"{cn}\"" : (caid != "" ? $"aid={caid}" : "(unnamed)");
                        lines.Add($"  [{cct}] {label}");
                    }
                    catch { }
                }
                if (children.Length > 10)
                    lines.Add($"  ... +{children.Length - 10} more");
            }
        }
        catch { }

        if (lines.Count == 0)
        {
            Console.Error.WriteLine("[A11Y] read — no accessible information available");
            return false;
        }

        foreach (var line in lines)
            Console.WriteLine($"[A11Y] {line}");
        return true;
    }

    // -- SetRange: UIA RangeValue --
    static bool A11ySetRange(AutomationElement el, double value)
    {
        var info = UiaLocator.GetRangeValueInfo(el);
        if (info != null)
            Console.WriteLine($"[A11Y] range: {info.Minimum}..{info.Maximum} (current={info.Value}, step={info.SmallChange})");

        if (UiaLocator.TrySetRangeValue(el, value))
        {
            Console.WriteLine($"[A11Y] set-range — UIA RangeValue = {value}");
            return true;
        }

        Console.Error.WriteLine("[A11Y] set-range — RangeValue not supported on this element");
        return false;
    }

    // ═══ Element Helpers ═══

    /// <summary>
    /// Ensure target element's tab is active. Walks up UIA parent chain looking for
    /// unselected TabItem ancestors → auto-select them (focusless UIA SelectionItem).
    /// Returns true if a tab was activated (caller may need to re-resolve scope).
    /// </summary>
    static bool EnsureTabActive(AutomationElement el)
    {
        try
        {
            var current = el;
            for (int i = 0; i < 20; i++) // max 20 levels up
            {
                AutomationElement? parent;
                try { parent = current.Parent; } catch { break; }
                if (parent == null) break;

                try
                {
                    var ct = parent.Properties.ControlType.ValueOrDefault;
                    if (ct == ControlType.TabItem)
                    {
                        // Check if this TabItem is selected
                        try
                        {
                            var selPat = parent.Patterns.SelectionItem;
                            if (selPat.IsSupported && !selPat.Pattern.IsSelected.Value)
                            {
                                var tabName = parent.Properties.Name.ValueOrDefault ?? "(unnamed)";
                                selPat.Pattern.Select();
                                Console.WriteLine($"[A11Y] tab activated: \"{tabName}\"");
                                Thread.Sleep(200);
                                return true;
                            }
                        }
                        catch { }
                    }
                }
                catch { }

                current = parent;
            }
        }
        catch { }
        return false;
    }

    static IntPtr GetElementHwnd(AutomationElement el)
    {
        try
        {
            var nh = el.Properties.NativeWindowHandle.ValueOrDefault;
            if (nh != IntPtr.Zero) return nh;
        }
        catch { }
        return IntPtr.Zero;
    }

    static System.Drawing.Rectangle? GetBoundingRect(AutomationElement el)
    {
        try
        {
            var r = el.Properties.BoundingRectangle.ValueOrDefault;
            if (r.Width > 0 && r.Height > 0)
                return new System.Drawing.Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);
        }
        catch { }
        return null;
    }
}
