using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

// -- Readiness interop: GetLastInputInfo -----------------------------------
internal static class CalloutInputNative
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    [DllImport("user32.dll")]
    public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

    [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
    public static extern bool SetForegroundWindowRaw(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    public static extern IntPtr MonitorFromPoint(System.Drawing.Point pt, uint dwFlags);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT { public int Left, Top, Right, Bottom; }

    [StructLayout(LayoutKind.Sequential)]
    public struct MONITORINFO
    {
        public uint cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;
    }

    [DllImport("user32.dll")]
    public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    [DllImport("user32.dll")]
    public static extern int GetSystemMetrics(int nIndex); // 0=SM_CXSCREEN, 1=SM_CYSCREEN

    public static uint GetLastInputTick()
    {
        var lii = new LASTINPUTINFO { cbSize = (uint)Marshal.SizeOf<LASTINPUTINFO>() };
        GetLastInputInfo(ref lii);
        return lii.dwTime;
    }
}

/// <summary>
/// Comic-style speech bubble callout near a target UIA element.
/// Tail tip points precisely at target BoundingRect center.
/// Body appears toward mouse cursor (user's attention direction).
///
/// Usage: CalloutInputOverlay.Show(content, contentType, targetRect, timeoutSeconds)
/// Returns: true = confirmed, false = cancelled/timeout
/// </summary>
internal sealed class CalloutInputWindow : Window
{
    /// <summary>
    /// Text to display in readiness callout (set by A11yCommand before readiness.Probe).
    /// Consumed by UserInputWaitAdapter.WaitForUserYield. Cleared after the targets loop.
    /// Enables single-callout behavior: readiness probe shows typed text instead of triggering
    /// a second pre-callout inside the per-target loop.
    /// </summary>
    internal static string? PendingContent = null;
    /// <summary>Tail tip rect: caret / text-end / left+20 position within the element.
    /// Used for CalcCalloutGeometry tip coordinates. Set by A11yCommand before readiness.Probe().</summary>
    internal static System.Drawing.Rectangle PendingTargetRect = default;
    /// <summary>Full BoundingRectangle of the target a11y element.
    /// Used for body placement (body right MUST be left of ElementRect.Left) and screenshot crop.</summary>
    internal static System.Drawing.Rectangle PendingElementRect = default;
    /// <summary>
    /// Action to execute when user presses O (confirm). Callout fires this directly via Task.Run,
    /// then closes -- no round-trip back to A11yCommand needed.
    /// Signature: () => bool (true = success). Set to null to use legacy approved-return flow.
    /// Supports: type injection, appbot script/prompt delivery, any input action.
    /// </summary>
    internal static Func<bool>? PendingAction = null;
    /// <summary>Set to true when PendingAction was fired by OnConfirm. A11yCommand checks this
    /// to skip the legacy yield-confirmed type path (avoids double-execution).</summary>
    internal static volatile bool ActionExecuted = false;

    // -- Active callout stack (for multi-window stacking) --
    static readonly System.Collections.Concurrent.ConcurrentBag<WeakReference<CalloutInputWindow>> _activeStack = new();

    /// <summary>
    /// Show callout without blocking (fire-and-forget).
    /// Stacks alongside existing callouts. Outputs grap to stdout.
    /// PendingAction fires on approval without round-trip to caller.
    /// </summary>
    public static void ShowAsync(
        string content, CalloutContentType contentType,
        System.Drawing.Rectangle targetScreenRect,
        Func<bool>? pendingAction = null,
        string? grapHint = null,
        int timeoutSeconds = 30)
    {
        // Push existing callouts up before showing new one
        PushCalloutsUp((int)(BodyH + 20 + 5)); // BodyH + tail/padding estimate + gap

        var thread = new System.Threading.Thread(() =>
        {
            var win = new CalloutInputWindow(content, contentType, targetScreenRect, timeoutSeconds, null);
            if (pendingAction != null) PendingAction = pendingAction;
            _activeStack.Add(new WeakReference<CalloutInputWindow>(win));
            win.Closed += (_, _) => { RearrangeStack(null); };
            win.ShowDialog();
        });
        thread.SetApartmentState(System.Threading.ApartmentState.STA);
        thread.IsBackground = true;
        thread.Name = "Callout-Async-STA";
        thread.Start();

        // Print grap to stdout so caller (Claude, scripts) can reference this callout
        var hwndHex = "(pending)";
        System.Console.WriteLine($"# CALLOUT action={contentType} grap={grapHint ?? "(none)"} timeout={timeoutSeconds}s -- awaiting user approval");
    }

    /// <summary>
    /// Called periodically: if this window overlaps another callout, slide it down until clear.
    /// Cross-process: uses EnumWindows so all callout processes self-organise.
    /// </summary>
    static void AvoidOverlapWithOtherCallouts(CalloutInputWindow self)
    {
        try
        {
            var myHwnd = new WindowInteropHelper(self).Handle;
            if (myHwnd == IntPtr.Zero) return;
            NativeMethods.GetWindowRect(myHwnd, out var myRect);
            const int gap = 5;

            foreach (var (hwnd, ox, oy, ow, oh) in FindAllCalloutWindows())
            {
                if (hwnd == myHwnd) continue;
                // Check overlap
                bool overlapX = myRect.Left < ox + ow && myRect.Right  > ox;
                bool overlapY = myRect.Top  < oy + oh && myRect.Bottom > oy;
                if (!overlapX || !overlapY) continue;

                // We overlap: slide ourselves down to be gap below the other window
                double targetY = oy + oh + gap;
                if (Math.Abs(self.Top - targetY) > 2)
                {
                    var anim = new System.Windows.Media.Animation.DoubleAnimation(
                        targetY, TimeSpan.FromMilliseconds(200))
                    { EasingFunction = new System.Windows.Media.Animation.CubicEase
                        { EasingMode = System.Windows.Media.Animation.EasingMode.EaseOut } };
                    self.BeginAnimation(TopProperty, anim);
                }
                break; // handle one at a time; next tick handles the rest
            }
        }
        catch { }
    }

    /// <summary>Push all existing callout windows up by pushBy pixels (cross-process).</summary>
    static void PushCalloutsUp(int pushBy)
    {
        try
        {
            foreach (var (hwnd, x, y, w, h) in FindAllCalloutWindows())
                NativeMethods.SetWindowPos(hwnd, IntPtr.Zero,
                    x, y - pushBy, w, h,
                    0x0010 | 0x0004); // SWP_NOACTIVATE | SWP_NOZORDER
        }
        catch { }
    }

    /// <summary>
    /// Find all callout windows across ALL processes via EnumWindows (cross-process coordination).
    /// Returns sorted by current Top position (topmost first).
    /// </summary>
    static List<(IntPtr hwnd, int x, int y, int w, int h)> FindAllCalloutWindows()
    {
        var result = new List<(IntPtr, int, int, int, int)>();
        var cls = new System.Text.StringBuilder(256);
        NativeMethods.EnumWindows((hwnd, _) =>
        {
            cls.Clear();
            NativeMethods.GetClassNameW(hwnd, cls, cls.Capacity);
            var c = cls.ToString();
            if ((c.StartsWith("HwndWrapper") && c.Contains("callout", StringComparison.OrdinalIgnoreCase))
                || c.Contains("CalloutInput", StringComparison.OrdinalIgnoreCase))
            {
                if (NativeMethods.IsWindowVisible(hwnd))
                {
                    NativeMethods.GetWindowRect(hwnd, out var r);
                    result.Add((hwnd, r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top));
                }
            }
            return true;
        }, IntPtr.Zero);
        return result.OrderBy(r => r.Item3).ToList(); // sort by Y (Top)
    }

    /// <summary>
    /// Rearrange all callout windows (cross-process) so they stack without overlap.
    /// Each window moves itself if needed. Called on Show and Close.
    /// </summary>
    static void RearrangeStack(CalloutInputWindow? self)
    {
        try
        {
            var all = FindAllCalloutWindows();
            if (all.Count < 2) return;
            double gap = 8;
            double nextTop = all[0].y;
            for (int i = 0; i < all.Count; i++)
            {
                double targetTop = i == 0 ? all[0].y : nextTop;
                if (Math.Abs(all[i].y - targetTop) > 4)
                {
                    // Animate only our own window; other processes move themselves on next show/close
                    if (self != null)
                    {
                        var myHwnd = new WindowInteropHelper(self).Handle;
                        if (myHwnd == all[i].hwnd)
                        {
                            var anim = new System.Windows.Media.Animation.DoubleAnimation(
                                targetTop, TimeSpan.FromMilliseconds(300))
                            { EasingFunction = new System.Windows.Media.Animation.CubicEase
                                { EasingMode = System.Windows.Media.Animation.EasingMode.EaseOut } };
                            self.BeginAnimation(TopProperty, anim);
                        }
                        else
                        {
                            // Move other-process window immediately via SetWindowPos
                            NativeMethods.SetWindowPos(all[i].hwnd, IntPtr.Zero,
                                all[i].x, (int)targetTop, 0, 0,
                                0x0002 | 0x0001 | 0x0010); // SWP_NOMOVE? no, apply pos | NOSIZE | NOACTIVATE
                        }
                    }
                }
                nextTop = targetTop + all[i].h + gap;
            }
        }
        catch { }
    }

    // -- Layout constants --
    private const double BodyW        = 380;
    private const double BodyH        = 240; // grows with content
    private const double BodyRadius   = 14;
    private const double TailWidth    = 18;  // base width of tail triangle
    private const double TailOverlap  = 6;   // tail base overlaps body edge so seam is hidden
    private const double Margin       = 12;  // screen edge margin
    private const double RingRadius   = 50;  // target highlight ring radius (px)

    private const int MinDisplayMs = 3000;

    private readonly Canvas        _canvas;
    private TextBlock              _confirmText = null!;
    private readonly DispatcherTimer _timer;
    private int                    _remaining;
    private bool                   _confirmed;
    private readonly System.Diagnostics.Stopwatch _shownSw = System.Diagnostics.Stopwatch.StartNew();

    // -- Readiness approval fields (transplanted from UserInputWaitWindow) --
    private readonly IntPtr _targetHwnd;
    private readonly IntPtr _prevFg;
    private readonly int    _resetSeconds;
    private uint            _lastInputTick;
    private bool            _focusAcquired;
    private bool            _deniedByUser;

    public bool Confirmed     => _confirmed;
    /// <summary>Focus was actually transferred to target (verified via GetForegroundWindow).</summary>
    public bool FocusAcquired => _focusAcquired;
    /// <summary>True if user explicitly clicked the deny button.</summary>
    public bool DeniedByUser  => _deniedByUser;

    // ── Public factory ───────────────────────────────────────────────────────

    /// <summary>
    /// Show the callout synchronously on an STA thread.
    /// Returns true if user confirmed, false if cancelled or timed out.
    /// </summary>
    // Kill any stale callout windows before showing a new one (zombie guard)
    static void CloseStaleCallouts()
    {
        try
        {
            var sb = new System.Text.StringBuilder(256);
            NativeMethods.EnumWindows((hwnd, _) =>
            {
                sb.Clear();
                NativeMethods.GetClassNameW(hwnd, sb, sb.Capacity);
                var cls = sb.ToString();
                if (cls.Contains("CalloutInput", StringComparison.OrdinalIgnoreCase) ||
                    (cls.StartsWith("HwndWrapper") && cls.Contains("callout", StringComparison.OrdinalIgnoreCase)))
                {
                    NativeMethods.SendMessageW(hwnd, 0x0010 /*WM_CLOSE*/, IntPtr.Zero, IntPtr.Zero);
                }
                return true;
            }, IntPtr.Zero);
        }
        catch { }
    }

    public static bool Show(
        string content,
        CalloutContentType contentType,
        System.Drawing.Rectangle targetScreenRect,
        int timeoutSeconds = 0,        // 0 = wait indefinitely
        System.Drawing.Bitmap? imageBitmap = null)
    {
        CloseStaleCallouts(); // zombie guard: kill any leftover callout windows
        bool result = false;
        var thread = new System.Threading.Thread(() =>
        {
            var win = new CalloutInputWindow(
                content, contentType, targetScreenRect, timeoutSeconds, imageBitmap);
            win.ShowDialog(); // CloseAfterMinimum() inside guarantees >=3s display
            result = win.Confirmed;
        });
        thread.SetApartmentState(System.Threading.ApartmentState.STA);
        thread.IsBackground = true;
        thread.Start();
        thread.Join();
        return result;
    }

    /// <summary>
    /// Type-delegation callout: request user approval before typing.
    /// Shows slide-in animation, shadow cards for queue depth, grap + delegation notice to stdout.
    /// </summary>
    public static bool ShowForTypeAction(
        string textToType,
        System.Drawing.Rectangle targetRect,
        int queueDepth = 0)
    {
        bool result = false;
        // Build content: show what will be typed + queue badge
        var preview = textToType.Length > 60 ? textToType[..57] + "..." : textToType;
        var queueTag = queueDepth > 0 ? $"\n▶ {queueDepth} more queued" : "";
        var content  = $"타이핑 위임 요청:\n「{preview}」{queueTag}";

        var thread = new System.Threading.Thread(() =>
        {
            var win = new CalloutInputWindow(content, CalloutContentType.Text, targetRect, 0, null);
            win._queueDepth = queueDepth;

            // Slide-in: X offset +200→0, opacity 0→1 (200ms EaseOut)
            win.RenderTransform = new System.Windows.Media.TranslateTransform(200, 0);
            win.Opacity = 0;
            win.Loaded += (_, _) =>
            {
                // Output grap + delegation notice AFTER window appears (hwnd available)
                var wih   = new WindowInteropHelper(win);
                var hwnd  = wih.Handle;
                var grap  = $"{{hwnd:0x{hwnd:X},proc:'wkappbot',title:'*callout*'}}";
                System.Console.WriteLine($"# CALLOUT-TYPE text=\"{preview.Replace("\"","\\\"")}\" queue={queueDepth} hwnd=0x{hwnd:X}");
                System.Console.WriteLine($"# TYPING DELEGATED grap={grap}");
                System.Console.WriteLine($"#   confirm: wkappbot a11y click \"{grap}#But*확인*\"");
                System.Console.WriteLine($"#   cancel:  wkappbot a11y close \"{grap}\"");
                System.Console.Out.Flush();

                var sb = new System.Windows.Media.Animation.Storyboard();
                var slideX = new System.Windows.Media.Animation.DoubleAnimation(200, 0, TimeSpan.FromMilliseconds(200))
                    { EasingFunction = new System.Windows.Media.Animation.CubicEase
                        { EasingMode = System.Windows.Media.Animation.EasingMode.EaseOut } };
                System.Windows.Media.Animation.Storyboard.SetTarget(slideX, win);
                System.Windows.Media.Animation.Storyboard.SetTargetProperty(slideX,
                    new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                var fadeIn = new System.Windows.Media.Animation.DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(200));
                System.Windows.Media.Animation.Storyboard.SetTarget(fadeIn, win);
                System.Windows.Media.Animation.Storyboard.SetTargetProperty(fadeIn, new PropertyPath(OpacityProperty));
                sb.Children.Add(slideX); sb.Children.Add(fadeIn);
                sb.Begin();
            };
            win.ShowDialog();
            result = win.Confirmed;
        });
        thread.SetApartmentState(System.Threading.ApartmentState.STA);
        thread.IsBackground = true;
        thread.Name = "Callout-Type-STA";
        thread.Start();
        thread.Join();
        return result;
    }

    private int _queueDepth = 0; // shadow card count

    /// <summary>
    /// Readiness variant: show the callout as a focus-yield approval prompt.
    /// Transplanted logic from UserInputWaitWindow:
    ///  - countdown resets to <paramref name="resetSeconds"/> on user activity
    ///  - auto-approves when countdown hits 0
    ///  - SmartSetForegroundWindow(targetHwnd) on confirm -> FocusAcquired
    ///  - deny button sets DeniedByUser=true, restores prevFg
    /// Returns: (approved, focusAcquired, deniedByUser).
    /// </summary>
    public static (bool approved, bool focusAcquired, bool deniedByUser) ShowForReadiness(
        string content,
        System.Drawing.Rectangle targetScreenRect,
        int timeoutSeconds,
        IntPtr targetHwnd,
        int resetSeconds = 30)
    {
        bool approved = false;
        bool focusAcquired = false;
        bool deniedByUser = false;

        CloseStaleCallouts(); // zombie guard
        PushCalloutsUp((int)(BodyH + 20 + 5)); // push existing callouts up

        // Capture prevFg on calling thread (STA worker cannot see the original foreground).
        var prevFg = CalloutInputNative.GetForegroundWindow();

        var thread = new System.Threading.Thread(() =>
        {
            var win = new CalloutInputWindow(
                content, CalloutContentType.Text, targetScreenRect, timeoutSeconds,
                imageBitmap: null, targetHwnd: targetHwnd, resetSeconds: resetSeconds, prevFg: prevFg);
            win.ShowDialog();
            approved      = win.Confirmed;
            focusAcquired = win.FocusAcquired;
            deniedByUser  = win.DeniedByUser;
        });
        thread.SetApartmentState(System.Threading.ApartmentState.STA);
        thread.IsBackground = true;
        thread.Start();
        // 5-minute hard cap matches legacy UserInputWaitOverlay safety timeout.
        thread.Join(5 * 60 * 1000);
        return (approved, focusAcquired, deniedByUser);
    }

    // ── Constructor ──────────────────────────────────────────────────────────

    private CalloutInputWindow(
        string content,
        CalloutContentType contentType,
        System.Drawing.Rectangle targetRect,
        int timeoutSeconds,
        System.Drawing.Bitmap? imageBitmap,
        IntPtr targetHwnd = default,
        int resetSeconds = 0,
        IntPtr prevFg = default)
    {
        _remaining     = timeoutSeconds > 0 ? timeoutSeconds : -1;
        _targetHwnd    = targetHwnd;
        _prevFg        = prevFg;
        _resetSeconds  = resetSeconds;
        _lastInputTick = CalloutInputNative.GetLastInputTick();

        // Window setup: transparent, no chrome, never activates
        Title            = "AppBotCallout";
        WindowStyle      = WindowStyle.None;
        AllowsTransparency = true;
        Background       = Brushes.Transparent;
        Topmost          = true;
        ShowInTaskbar    = false;
        ShowActivated    = false;
        ResizeMode       = ResizeMode.NoResize;

        // ── Geometry: call extracted helper ─────────────────────────────────
        var geo = CalcCalloutGeometry(targetRect);
        double scale     = geo.Scale;
        double tipX      = geo.TipX;      double tipY      = geo.TipY;
        double bodyX     = geo.BodyX;     double bodyY     = geo.BodyY;
        bool   vertical  = geo.Vertical;
        bool   mouseAbove= geo.MouseAbove;
        bool   mouseLeft = geo.MouseLeft;
        double minX      = geo.MinX;      double minY      = geo.MinY;
        double canvasW   = geo.CanvasW;   double canvasH   = geo.CanvasH;

        Console.Error.WriteLine($"[CALLOUT:GEO] target=({targetRect.X},{targetRect.Y} {targetRect.Width}x{targetRect.Height})");
        Console.Error.WriteLine($"[CALLOUT:GEO] scale={scale:F2} tip=({tipX:F0},{tipY:F0}) body=({bodyX:F0},{bodyY:F0}) vert={vertical} mouseAbove={mouseAbove} mouseLeft={mouseLeft}");
        Console.Error.WriteLine($"[CALLOUT:GEO] window Left={minX:F0} Top={minY:F0} W={canvasW:F0} H={canvasH:F0}");

        // Window placement = canvas origin in screen DIP
        Left = minX; Top = minY;
        Width = canvasW; Height = canvasH;

        // Self-correction: if window ends up off-screen (e.g. MDI-local coords), animate back in
        Loaded += (_, _) =>
        {
            var target = ClampToNearestMonitor(Left, Top, Width, Height);
            if (Math.Abs(target.x - Left) > 2 || Math.Abs(target.y - Top) > 2)
            {
                Console.Error.WriteLine($"[CALLOUT:CLAMP] Off-screen ({Left:F0},{Top:F0}) → ({target.x:F0},{target.y:F0})");
                var ax = new System.Windows.Media.Animation.DoubleAnimation(target.x, TimeSpan.FromMilliseconds(250));
                var ay = new System.Windows.Media.Animation.DoubleAnimation(target.y, TimeSpan.FromMilliseconds(250));
                ax.EasingFunction = ay.EasingFunction = new System.Windows.Media.Animation.CubicEase { EasingMode = System.Windows.Media.Animation.EasingMode.EaseOut };
                BeginAnimation(LeftProperty, ax);
                BeginAnimation(TopProperty, ay);
            }
        };

        // Convert absolute DIP coords to canvas-local coords
        double localBodyX = bodyX - minX;
        double localBodyY = bodyY - minY;
        double localTipX  = tipX  - minX;
        double localTipY  = tipY  - minY;

        // ── Visual tree: rings, shadow, tail, body, content ──────────────────
        _canvas = new Canvas { Width = canvasW, Height = canvasH };

        // Halo rings at tail tip (outer faint -> inner bright)
        _canvas.Children.Add(BuildRing(localTipX, localTipY, RingRadius * 2.0,
            Color.FromArgb(0x33, 0x00, 0xDD, 0x33), 2.0));
        _canvas.Children.Add(BuildRing(localTipX, localTipY, RingRadius * 1.4,
            Color.FromArgb(0x66, 0x00, 0xDD, 0x33), 2.5));
        _canvas.Children.Add(BuildRing(localTipX, localTipY, RingRadius,
            Color.FromArgb(0xAA, 0x00, 0xFF, 0x41), 3.0));

        // Shadow (offset from body)
        var shadow = BuildBodyRect(localBodyX + 3, localBodyY + 3, BodyW, BodyH,
            new SolidColorBrush(Color.FromArgb(0x40, 0, 0, 0)), null);
        _canvas.Children.Add(shadow);

        // Tail path (drawn before body so body edge overlaps seam)
        var tail = BuildTailPath(localBodyX, localBodyY, BodyW, BodyH,
            localTipX, localTipY, vertical, mouseAbove, mouseLeft);
        _canvas.Children.Add(tail);

        // Body rounded rect
        var body = BuildBodyRect(localBodyX, localBodyY, BodyW, BodyH,
            new SolidColorBrush(Color.FromArgb(0xF2, 0x04, 0x10, 0x04)),
            new SolidColorBrush(Color.FromRgb(0x00, 0xDD, 0x33)));
        _canvas.Children.Add(body);

        // Content inside body
        BuildContent(_canvas, content, contentType, imageBitmap,
            localBodyX, localBodyY, BodyW, BodyH);

        Content = _canvas;

        // ── Countdown + confirm/cancel ───────────────────────────────────────
        // (buttons are built inside BuildContent)
        if (_remaining > 0)
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _timer.Tick += (_, _) =>
            {
                // Overlap check: every tick, push ourselves down if another callout overlaps us
                AvoidOverlapWithOtherCallouts(this);

                // Readiness mode (resetSeconds > 0): reset countdown on user activity.
                if (_resetSeconds > 0)
                {
                    uint currentTick = CalloutInputNative.GetLastInputTick();
                    if (currentTick != _lastInputTick)
                    {
                        _lastInputTick = currentTick;
                        _remaining = _resetSeconds;
                        UpdateConfirmText();
                        return;
                    }
                }

                _remaining--;
                UpdateConfirmText();
                if (_remaining <= 0)
                {
                    _timer.Stop();
                    OnAutoApprove();
                }
            };
            _timer.Start();
        }
        else
        {
            _timer = new DispatcherTimer(); // unused
        }

        // Readiness mode (resetSeconds>0): allow activation so buttons receive clicks.
        // Preview-only mode (resetSeconds==0): NOACTIVATE so focus isn't stolen while typing.
        SourceInitialized += (_, _) =>
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            int ex = NativeMethods.GetWindowLongW(hwnd, NativeMethods.GWL_EXSTYLE);
            int flags = 0x00000080; // WS_EX_TOOLWINDOW always
            if (_resetSeconds == 0) flags |= 0x08000000; // WS_EX_NOACTIVATE only for preview
            NativeMethods.SetWindowLongW(hwnd, NativeMethods.GWL_EXSTYLE, ex | flags);
        };

        KeyDown += (_, e) => { if (e.Key == Key.Escape) { _confirmed = false; CloseAfterMinimum(); } };

        // Play attention chime + auto screenshot on load (readiness mode only)
        if (_resetSeconds > 0)
            Loaded += (_, _) => { PlayChimeThenSpeak(); TakeElementScreenshotAsync(); };
    }

    // -- Chime + TTS announce (ported from UserInputWaitOverlay) --

    private static void PlayChimeThenSpeak()
    {
        System.Threading.ThreadPool.QueueUserWorkItem(_ => { PlayChimeSync(); PlaySpeakAnnounce(); });
    }

    private static void PlaySpeakAnnounce()
    {
        try { AppBotPipe.Spawn("wkappbot", "speak \"포커스 양보가 필요합니다\" --bg", Environment.CurrentDirectory, caller: "CALLOUT"); }
        catch { }
    }

    private static void PlayChimeSync()
    {
        try
        {
            const int rate = 22050;
            (double hz, int ms)[] melody =
            {
                (523.25, 100), (659.25, 100), (783.99, 100), (1046.50, 220),
            };
            var pcm = new System.Collections.Generic.List<short>();
            foreach (var (hz, ms) in melody)
            {
                int n = rate * ms / 1000;
                for (int i = 0; i < n; i++)
                {
                    double t = (double)i / rate;
                    double env = Math.Min(1.0, Math.Min(i / 66.0, (n - i) / (n * 0.4)));
                    double wave = Math.Sin(2 * Math.PI * hz * t) + 0.15 * Math.Sin(4 * Math.PI * hz * t);
                    pcm.Add((short)(5500 * env * wave));
                }
                for (int i = 0; i < rate * 25 / 1000; i++) pcm.Add(0);
            }
            using var wav = new System.IO.MemoryStream();
            using var bw = new System.IO.BinaryWriter(wav);
            int dataLen = pcm.Count * 2;
            bw.Write(System.Text.Encoding.ASCII.GetBytes("RIFF")); bw.Write(36 + dataLen);
            bw.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt ")); bw.Write(16);
            bw.Write((short)1); bw.Write((short)1); bw.Write(rate); bw.Write(rate * 2);
            bw.Write((short)2); bw.Write((short)16);
            bw.Write(System.Text.Encoding.ASCII.GetBytes("data")); bw.Write(dataLen);
            foreach (var s in pcm) bw.Write(s);
            wav.Position = 0;
            using var player = new System.Media.SoundPlayer(wav);
            player.PlaySync();
        }
        catch { }
    }

    private static void TakeElementScreenshotAsync()
    {
        var captureRect = PendingElementRect.IsEmpty
            ? System.Drawing.Rectangle.Empty
            : PendingElementRect;
        System.Threading.ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                System.Drawing.Rectangle crop;
                if (captureRect.IsEmpty)
                {
                    // Full primary screen fallback
                    int sw = CalloutInputNative.GetSystemMetrics(0);
                    int sh = CalloutInputNative.GetSystemMetrics(1);
                    crop = new System.Drawing.Rectangle(0, 0, sw, sh);
                }
                else
                {
                    // Element + 30px padding on each side
                    const int pad = 30;
                    crop = new System.Drawing.Rectangle(
                        Math.Max(0, captureRect.X - pad),
                        Math.Max(0, captureRect.Y - pad),
                        captureRect.Width  + pad * 2,
                        captureRect.Height + pad * 2);
                }
                using var bmp = new System.Drawing.Bitmap(crop.Width, crop.Height);
                using var g   = System.Drawing.Graphics.FromImage(bmp);
                g.CopyFromScreen(crop.X, crop.Y, 0, 0, crop.Size);
                var dir  = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var path = System.IO.Path.Combine(dir, $"wkappbot-{DateTime.Now:yyyyMMdd-HHmmss}.png");
                bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                Console.Error.WriteLine($"[CALLOUT:SS] {path} ({crop.Width}x{crop.Height})");
            }
            catch (Exception ex) { Console.Error.WriteLine($"[CALLOUT:SS] failed: {ex.Message}"); }
        });
    }

    // Delays actual Close() until MinDisplayMs elapsed so the bubble stays readable
    private void CloseAfterMinimum()
    {
        var remaining = MinDisplayMs - (int)_shownSw.ElapsedMilliseconds;
        if (remaining <= 0) { Close(); return; }
        var t = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(remaining) };
        t.Tick += (_, _) => { t.Stop(); Close(); };
        t.Start();
    }

    // ── Shape builders ───────────────────────────────────────────────────────

    private static UIElement BuildRing(double cx, double cy, double r, Color color,
        double strokeThickness, bool fill = false)
    {
        var brush = new SolidColorBrush(color);
        var el = new Ellipse
        {
            Width           = r * 2,
            Height          = r * 2,
            Fill            = fill ? brush : Brushes.Transparent,
            Stroke          = fill ? null : brush,
            StrokeThickness = strokeThickness,
        };
        Canvas.SetLeft(el, cx - r);
        Canvas.SetTop(el, cy - r);
        return el;
    }

    private static Border BuildBodyRect(double x, double y, double w, double h,
        Brush bg, Brush? border)
    {
        var b = new Border
        {
            Width           = w,
            Height          = h,
            Background      = bg,
            CornerRadius    = new CornerRadius(BodyRadius),
            BorderBrush     = border,
            BorderThickness = border != null ? new Thickness(1.5) : new Thickness(0),
        };
        Canvas.SetLeft(b, x);
        Canvas.SetTop(b, y);
        return b;
    }

    private static System.Windows.Shapes.Path BuildTailPath(
        double bodyX, double bodyY, double bodyW, double bodyH,
        double tipX, double tipY,
        bool vertical, bool mouseAbove, bool mouseLeft)
    {
        // Tail base: two points on the body edge, centered on the tip axis
        double bx1, by1, bx2, by2;
        double half = TailWidth / 2;
        if (vertical)
        {
            double edgeY = mouseAbove ? bodyY + bodyH - TailOverlap : bodyY + TailOverlap;
            bx1 = bodyX + bodyW / 2 - half;
            by1 = edgeY;
            bx2 = bodyX + bodyW / 2 + half;
            by2 = edgeY;
        }
        else
        {
            double edgeX = mouseLeft ? bodyX + bodyW - TailOverlap : bodyX + TailOverlap;
            bx1 = edgeX;
            by1 = bodyY + bodyH / 2 - half;
            bx2 = edgeX;
            by2 = bodyY + bodyH / 2 + half;
        }

        var geo = new PathGeometry();
        var fig = new PathFigure { StartPoint = new Point(bx1, by1), IsClosed = true };
        fig.Segments.Add(new LineSegment(new Point(bx2, by2), true));
        fig.Segments.Add(new LineSegment(new Point(tipX, tipY), true));
        geo.Figures.Add(fig);

        return new System.Windows.Shapes.Path
        {
            Data   = geo,
            Fill   = new SolidColorBrush(Color.FromArgb(0xF2, 0x04, 0x10, 0x04)),
            Stroke = new SolidColorBrush(Color.FromRgb(0x00, 0xDD, 0x33)),
            StrokeThickness = 1.5,
            StrokeLineJoin  = PenLineJoin.Round,
        };
    }

    private void BuildContent(Canvas canvas, string content, CalloutContentType type,
        System.Drawing.Bitmap? image,
        double bx, double by, double bw, double bh)
    {
        double pad = 10;
        double innerW = bw - pad * 2;

        // No header -- content fills the box
        double contentTop = by + pad;
        double contentH   = bh - pad * 2 - 70; // reserve bottom for circle buttons

        if (type == CalloutContentType.Image && image != null)
        {
            // Image thumbnail
            var bmp = ConvertBitmap(image);
            var img = new System.Windows.Controls.Image
            {
                Source  = bmp,
                Width   = Math.Min(innerW, image.Width),
                Height  = Math.Min(contentH, image.Height),
                Stretch = Stretch.Uniform,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            Canvas.SetLeft(img, bx + pad);
            Canvas.SetTop(img, contentTop);
            canvas.Children.Add(img);
        }
        else
        {
            // Text / Script: scrollable display (TextBlock in ScrollViewer -- not an editable TextBox)
            var tb = new TextBlock
            {
                Text        = content,
                Foreground  = new SolidColorBrush(Color.FromRgb(0x00, 0xFF, 0x41)),
                FontFamily  = new FontFamily("Consolas"),
                FontSize    = 16,
                TextWrapping = TextWrapping.Wrap,
                Padding     = new Thickness(6, 4, 6, 4),
            };
            var sv = new ScrollViewer
            {
                Width  = innerW,
                Height = contentH,
                Background              = new SolidColorBrush(Color.FromArgb(0x60, 0x00, 0x08, 0x01)),
                BorderBrush             = new SolidColorBrush(Color.FromRgb(0x00, 0x55, 0x11)),
                BorderThickness         = new Thickness(1),
                VerticalScrollBarVisibility   = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                Content = tb,
            };
            Canvas.SetLeft(sv, bx + pad);
            Canvas.SetTop(sv, contentTop);
            canvas.Children.Add(sv);
        }

        // -- Button row: big green confirm circle + small red X deny circle --
        // No cancel button (Esc key still cancels). Layout centered horizontally
        // with confirm on the left and deny (readiness only) on the right.
        const double ConfirmDiameter = 56;
        const double DenyDiameter    = 36;
        const double BtnGap          = 12;
        double btnY   = by + bh - pad - ConfirmDiameter;
        bool hasDeny   = _resetSeconds > 0;

        // Bottom-right cluster: O at far right, X immediately left of it.
        double confirmCx = bx + bw - pad - ConfirmDiameter / 2;
        double confirmCy = btnY + ConfirmDiameter / 2;

        // Confirm circle: big green ellipse, countdown text centered over it.
        var confirmCircle = new Ellipse
        {
            Width  = ConfirmDiameter,
            Height = ConfirmDiameter,
            Fill   = new SolidColorBrush(Color.FromRgb(0x00, 0xDD, 0x33)),
            Stroke = new SolidColorBrush(Color.FromRgb(0x00, 0xFF, 0x41)),
            StrokeThickness = 2,
            Cursor = Cursors.Hand,
        };
        Canvas.SetLeft(confirmCircle, confirmCx - ConfirmDiameter / 2);
        Canvas.SetTop (confirmCircle, btnY);
        confirmCircle.MouseLeftButtonDown += (_, _) => OnConfirm();
        confirmCircle.MouseEnter += (_, _) =>
            confirmCircle.Fill = new SolidColorBrush(Color.FromRgb(0x22, 0xEE, 0x55));
        confirmCircle.MouseLeave += (_, _) =>
            confirmCircle.Fill = new SolidColorBrush(Color.FromRgb(0x00, 0xDD, 0x33));
        canvas.Children.Add(confirmCircle);

        _confirmText = new TextBlock
        {
            Foreground = Brushes.Black,
            FontFamily = new FontFamily("맑은 고딕"),
            FontWeight = FontWeights.Bold,
            FontSize   = 13,
            IsHitTestVisible = false, // let clicks pass to the ellipse
            TextAlignment = TextAlignment.Center,
            Width  = ConfirmDiameter,
            Height = ConfirmDiameter,
            VerticalAlignment = VerticalAlignment.Center,
        };
        // Vertically center text inside the circle via padding.
        _confirmText.Padding = new Thickness(0, (ConfirmDiameter - 18) / 2, 0, 0);
        UpdateConfirmText();
        Canvas.SetLeft(_confirmText, confirmCx - ConfirmDiameter / 2);
        Canvas.SetTop (_confirmText, btnY);
        canvas.Children.Add(_confirmText);

        // Deny circle (readiness mode only): small dark-red ellipse with X glyph.
        if (hasDeny)
        {
            double denyCx = confirmCx - ConfirmDiameter / 2 - BtnGap - DenyDiameter / 2;
            double denyTop = confirmCy - DenyDiameter / 2; // vertically align with confirm center

            var denyCircle = new Ellipse
            {
                Width  = DenyDiameter,
                Height = DenyDiameter,
                Fill   = new SolidColorBrush(Color.FromRgb(0x99, 0x22, 0x22)),
                Stroke = new SolidColorBrush(Color.FromRgb(0xCC, 0x33, 0x33)),
                StrokeThickness = 1.5,
                Cursor = Cursors.Hand,
            };
            Canvas.SetLeft(denyCircle, denyCx - DenyDiameter / 2);
            Canvas.SetTop (denyCircle, denyTop);
            denyCircle.MouseLeftButtonDown += (_, _) => OnDeny();
            denyCircle.MouseEnter += (_, _) =>
                denyCircle.Fill = new SolidColorBrush(Color.FromRgb(0xCC, 0x33, 0x33));
            denyCircle.MouseLeave += (_, _) =>
                denyCircle.Fill = new SolidColorBrush(Color.FromRgb(0x99, 0x22, 0x22));
            canvas.Children.Add(denyCircle);

            var denyGlyph = new TextBlock
            {
                Text       = "✕",
                Foreground = Brushes.White,
                FontFamily = new FontFamily("Segoe UI Symbol"),
                FontWeight = FontWeights.Bold,
                FontSize   = 18,
                IsHitTestVisible = false,
                TextAlignment = TextAlignment.Center,
                Width  = DenyDiameter,
                Height = DenyDiameter,
            };
            denyGlyph.Padding = new Thickness(0, (DenyDiameter - 22) / 2, 0, 0);
            Canvas.SetLeft(denyGlyph, denyCx - DenyDiameter / 2);
            Canvas.SetTop (denyGlyph, denyTop);
            canvas.Children.Add(denyGlyph);
        }
    }

    // ── Readiness approval handlers (transplanted from UserInputWaitWindow) ──

    private void OnConfirm()
    {
        if (_confirmed) return;
        _confirmed = true;
        _timer.Stop();

        // Dump callstack so we can identify what triggered this callout
        try
        {
            var frames = new System.Diagnostics.StackTrace(fNeedFileInfo: false).GetFrames()
                .Select(f => f.GetMethod())
                .Where(m => m?.DeclaringType?.Namespace?.StartsWith("WKAppBot") == true)
                .Select(m => Program.CleanAsyncMethodName(m!.DeclaringType!.Name, m.Name))
                .Where(s => s != null)
                .Distinct().Take(8).ToArray();
            Console.Error.WriteLine($"[CALLOUT:OnConfirm] stack: {string.Join(" <- ", frames)}");
        }
        catch { }

        // If caller pre-bound a PendingAction, execute on background thread immediately
        // (don't block the WPF UI thread -- idle gate + action run off-thread, close dispatched back).
        var action = PendingAction;
        PendingAction = null;
        if (action != null)
        {
            ActionExecuted = true;
            var capturedHwnd = _targetHwnd;
            // Close IMMEDIATELY (not CloseAfterMinimum) so focus shift happens before SetForegroundWindowRaw
            Dispatcher.BeginInvoke(() => Close());
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(200); // wait for callout to fully close + focus to settle
                    if (capturedHwnd != IntPtr.Zero)
                    {
                        try { CalloutInputNative.SetForegroundWindowRaw(capturedHwnd); } catch { }
                        Thread.Sleep(300);
                        // Idle gate: wait until user idle >= 500ms (max 30s)
                        var gw = System.Diagnostics.Stopwatch.StartNew();
                        while (gw.ElapsedMilliseconds < 30_000)
                        {
                            uint lastTick = CalloutInputNative.GetLastInputTick();
                            uint curTick  = (uint)Environment.TickCount;
                            if (curTick - lastTick >= 500) break;
                            Thread.Sleep(50);
                        }
                    }
                    bool ok = action();
                    Console.Error.WriteLine($"[CALLOUT:ACTION] result={ok}");
                }
                catch (Exception ex) { Console.Error.WriteLine($"[CALLOUT:ACTION] failed: {ex.Message}"); }
            });
            return;
        }

        // Legacy flow: just transfer foreground and return approved to caller
        if (_targetHwnd != IntPtr.Zero)
        {
            try { CalloutInputNative.SetForegroundWindowRaw(_targetHwnd); } catch { }
            Thread.Sleep(80);
            _focusAcquired = CalloutInputNative.GetForegroundWindow() == _targetHwnd;
        }
        CloseAfterMinimum();
    }

    private void OnDeny()
    {
        if (_confirmed) return;
        _confirmed     = true;
        _deniedByUser  = true;
        _timer.Stop();
        // Restore original foreground (user said no -- don't let another app steal it)
        var restore = _prevFg != IntPtr.Zero ? _prevFg : _targetHwnd;
        if (restore != IntPtr.Zero)
        {
            try { CalloutInputNative.SetForegroundWindowRaw(restore); } catch { }
        }
        Close(); // X = immediate close, no minimum delay
    }

    private void OnAutoApprove()
    {
        if (_confirmed) return;
        _confirmed = true;

        Console.Error.WriteLine("[CALLOUT:OnAutoApprove] auto-approved");

        // If PendingAction is bound, same as OnConfirm: background thread, close immediately.
        var action = PendingAction;
        PendingAction = null;
        if (action != null)
        {
            ActionExecuted = true;
            var capturedHwnd2 = _targetHwnd;
            Dispatcher.BeginInvoke(() => Close()); // immediate close so focus shift precedes SetForegroundWindowRaw
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(200);
                    if (capturedHwnd2 != IntPtr.Zero)
                    {
                        try { CalloutInputNative.SetForegroundWindowRaw(capturedHwnd2); } catch { }
                        Thread.Sleep(300);
                        var gw2 = System.Diagnostics.Stopwatch.StartNew();
                        while (gw2.ElapsedMilliseconds < 30_000)
                        {
                            uint lastTick2 = CalloutInputNative.GetLastInputTick();
                            uint curTick2  = (uint)Environment.TickCount;
                            if (curTick2 - lastTick2 >= 500) break;
                            Thread.Sleep(50);
                        }
                    }
                    bool ok = action();
                    Console.Error.WriteLine($"[CALLOUT:ACTION] auto result={ok}");
                }
                catch (Exception ex) { Console.Error.WriteLine($"[CALLOUT:ACTION] auto failed: {ex.Message}"); }
            });
            return;
        }

        // Legacy: user idle -> safer to grab foreground (weaker entitlement gate).
        if (_targetHwnd != IntPtr.Zero)
        {
            try { NativeMethods.SmartSetForegroundWindow(_targetHwnd); }
            catch { }
            Thread.Sleep(80);
            _focusAcquired = CalloutInputNative.GetForegroundWindow() == _targetHwnd;
        }
        CloseAfterMinimum();
    }

    private void UpdateConfirmText()
    {
        if (_confirmText == null) return;
        _confirmText.Text = _remaining > 0 ? $"입력 ({_remaining})" : "입력";
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static BitmapSource ConvertBitmap(System.Drawing.Bitmap bmp)
    {
        var handle = bmp.GetHbitmap();
        try
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                handle, IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
        finally
        {
            DeleteObject(handle);
        }
    }

    private static double GetSystemDpi()
    {
        using var g = System.Drawing.Graphics.FromHwnd(IntPtr.Zero);
        return g.DpiX;
    }

    // ── Geometry helper (extracted from constructor -- reusable + testable) ────

    internal record CalloutGeo(
        double Scale,
        double TipX, double TipY,
        double BodyX, double BodyY,
        bool Vertical, bool MouseAbove, bool MouseLeft,
        double MinX, double MinY, double CanvasW, double CanvasH);

    /// <summary>
    /// Calculates callout window layout from target screen rect.
    /// Body placed upper-left of the target element; tail tip at caret/center.
    /// Cross-monitor: body stays on the same monitor as the target.
    /// </summary>
    /// <summary>Clamp window to nearest monitor's work area. Returns adjusted (x, y) in WPF DIPs.</summary>
    /// <remarks>
    /// (x, y, w, h) are WPF DIPs (Window.Left/Top space). System.Windows.Forms.Screen.WorkingArea
    /// returns physical pixels under PerMonitor DPI awareness, so we must convert to DIPs before
    /// comparing — otherwise multi-monitor MDI HTS targets get clamped against a wrong-scale
    /// rectangle and end up off-screen.
    /// </remarks>
    static (double x, double y) ClampToNearestMonitor(double x, double y, double w, double h)
    {
        double scale = GetSystemDpi() / 96.0;

        // Primary work area is already in DIPs via SystemParameters
        double wa_l = SystemParameters.WorkArea.Left;
        double wa_t = SystemParameters.WorkArea.Top;
        double wa_r = SystemParameters.WorkArea.Right;
        double wa_b = SystemParameters.WorkArea.Bottom;

        // Try to find the monitor nearest to the window's center.
        // Forms.Screen.WorkingArea is physical px → divide by scale to compare in DIPs.
        foreach (var screen in System.Windows.Forms.Screen.AllScreens)
        {
            var wa = screen.WorkingArea;
            double sl = wa.Left / scale, st = wa.Top / scale;
            double sr = wa.Right / scale, sb = wa.Bottom / scale;
            double cx = x + w / 2, cy = y + h / 2;
            double dist = Math.Sqrt(Math.Pow(cx - (sl + (sr - sl) / 2), 2) +
                                    Math.Pow(cy - (st + (sb - st) / 2), 2));
            if (dist < Math.Sqrt(Math.Pow(cx - (wa_l + (wa_r - wa_l) / 2), 2) +
                                 Math.Pow(cy - (wa_t + (wa_b - wa_t) / 2), 2)))
            {
                wa_l = sl; wa_t = st; wa_r = sr; wa_b = sb;
            }
        }

        double nx = Math.Max(wa_l, Math.Min(x, wa_r - w));
        double ny = Math.Max(wa_t, Math.Min(y, wa_b - h));
        return (nx, ny);
    }

    internal static CalloutGeo CalcCalloutGeometry(System.Drawing.Rectangle targetRect)
    {
        double dpi   = GetSystemDpi();
        double scale = dpi / 96.0;

        // Tail tip = center of target element in logical (DIP) coords
        double tipX = (targetRect.Left + targetRect.Width  / 2.0) / scale;
        double tipY = (targetRect.Top  + targetRect.Height / 2.0) / scale;

        // Fixed upper-left placement: body to the upper-left of the target element.
        // Tail goes from body lower-right area down to tip.
        bool vertical   = true;
        bool mouseLeft  = false;
        bool mouseAbove = true;

        // Body placement: position so that the confirm (O) button is exactly 150px above cursor.
        // confirmCenter = (bodyX + BodyW - ConfirmEdgeOffset, bodyY + BodyH - ConfirmEdgeOffset)
        // Target confirmCenter = (mouseX, mouseY - 150)  → body coords by back-calculation.
        const double ConfirmEdgeOffset = 38.0; // pad(10) + ConfirmDiameter/2(28)
        const double ConfirmAboveMouse = 150.0;
        NativeMethods.GetCursorPos(out var cursorPt);
        double mouseX = cursorPt.X / scale;
        double mouseY = cursorPt.Y / scale;
        // confirm button center → (mouseX - 150, mouseY - 150): upper-left diagonal from cursor
        double bodyX = mouseX - ConfirmAboveMouse - BodyW + ConfirmEdgeOffset;
        double bodyY = mouseY - ConfirmAboveMouse - BodyH + ConfirmEdgeOffset;

        // Clamp body to virtual screen bounds.
        // SystemParameters.VirtualScreen{Left,Top,Width,Height} are already in WPF DIPs
        // (primary-monitor DPI logical space — same space as Window.Left/Top). Do NOT divide
        // by scale again; otherwise on >96 DPI primaries or multi-monitor setups (very common
        // with elevated HTS apps on a secondary high-DPI monitor) the bounds collapse and
        // bodyX/bodyY get clamped off-screen for MDI children.
        double scrW = SystemParameters.VirtualScreenWidth;
        double scrH = SystemParameters.VirtualScreenHeight;
        double scrL = SystemParameters.VirtualScreenLeft;
        double scrT = SystemParameters.VirtualScreenTop;
        // Clamp body to the SAME MONITOR as the target element (not full virtual screen).
        // Use tipX to identify which monitor contains the target.
        // monL/monR are in DIP space; default to virtual-screen extents (already DIP).
        double monL = scrL, monR = scrL + scrW;
        double monT = scrT, monB = scrT + scrH;
        try
        {
            // Find monitor containing tip point via Win32 (MonitorFromPoint takes physical px).
            var tipPt = new System.Drawing.Point((int)(tipX * scale), (int)(tipY * scale));
            var hMon = CalloutInputNative.MonitorFromPoint(tipPt, 2 /*MONITOR_DEFAULTTONEAREST*/);
            if (hMon != IntPtr.Zero)
            {
                var mi = new CalloutInputNative.MONITORINFO { cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<CalloutInputNative.MONITORINFO>() };
                if (CalloutInputNative.GetMonitorInfo(hMon, ref mi))
                {
                    // MONITORINFO.rcWork is physical pixels → divide by primary-DPI scale to
                    // get WPF DIPs (Window.Left/Top space).
                    monL = mi.rcWork.Left   / scale;
                    monR = mi.rcWork.Right  / scale;
                    monT = mi.rcWork.Top    / scale;
                    monB = mi.rcWork.Bottom / scale;
                }
            }
        }
        catch { }
        Console.Error.WriteLine($"[CALLOUT:GEO] monL={monL:F0} monR={monR:F0} monT={monT:F0} monB={monB:F0} mouse=({mouseX:F0},{mouseY:F0}) bodyX_before_clamp={bodyX:F0} bodyY_before_clamp={bodyY:F0}");
        bodyX = Math.Max(monL + Margin, Math.Min(bodyX, monR - BodyW - Margin));
        bodyY = Math.Max(monT + Margin, Math.Min(bodyY, monB - BodyH - Margin));

        // Cross-monitor fix: if cursor-derived body landed on a different monitor than the
        // target tip (common when MDI HTS lives on a secondary monitor while user works on
        // the primary), pull body back onto the tip's monitor right above the target.
        bool bodyOnDifferentMonitor = bodyX + BodyW / 2 < monL || bodyX + BodyW / 2 > monR
                                   || bodyY + BodyH / 2 < monT || bodyY + BodyH / 2 > monB;
        if (bodyOnDifferentMonitor)
        {
            // Place body above the tip -- same monitor, short tail pointing down
            bodyX = tipX - BodyW / 2;
            bodyY = tipY - BodyH - TailOverlap;
            vertical   = true;
            mouseAbove = true;
            mouseLeft  = false;
            bodyX = Math.Max(monL + Margin, Math.Min(bodyX, monR - BodyW - Margin));
            bodyY = Math.Max(monT + Margin, Math.Min(bodyY, monB - BodyH - Margin));
        }

        // Anti-overlap: ensure body doesn't cover the target element.
        // If body overlaps target rect, push body above (or below if above is off-screen).
        double tgtL = targetRect.Left   / scale, tgtR = (targetRect.Left + targetRect.Width)  / scale;
        double tgtT = targetRect.Top    / scale, tgtB = (targetRect.Top  + targetRect.Height) / scale;
        bool bodyOverlapsTarget = bodyX < tgtR && bodyX + BodyW > tgtL
                                && bodyY < tgtB && bodyY + BodyH > tgtT;
        if (bodyOverlapsTarget)
        {
            // Prefer placing body above the target; fall back to below if off-screen.
            // Clamp against the target's monitor (monT/monB), not the global virtual screen,
            // so MDI children on a secondary monitor don't push the body across monitors.
            double aboveY = tgtT - BodyH - Margin;
            double belowY = tgtB + Margin;
            bodyY = aboveY >= monT + Margin ? aboveY : belowY;
            // Re-sync flags for tail direction
            vertical   = true;
            mouseAbove = bodyY < tgtT; // body above → tail points down
            bodyX = Math.Max(monL + Margin, Math.Min(bodyX, monR - BodyW - Margin));
            bodyY = Math.Max(monT + Margin, Math.Min(bodyY, monB - BodyH - Margin));
        }

        // Clamp tail tip: max 100px from nearest body edge (prevents tail stretching across screen)
        const double MaxTailLength = 100.0;
        double clampPtX = Math.Max(bodyX, Math.Min(tipX, bodyX + BodyW));
        double clampPtY = Math.Max(bodyY, Math.Min(tipY, bodyY + BodyH));
        double tdx = tipX - clampPtX, tdy = tipY - clampPtY;
        double tlen = Math.Sqrt(tdx * tdx + tdy * tdy);
        if (tlen > MaxTailLength && tlen > 0)
        {
            tipX = clampPtX + tdx * MaxTailLength / tlen;
            tipY = clampPtY + tdy * MaxTailLength / tlen;
        }

        // Canvas bounding box: encompasses body rect + tail tip + rings
        double minX = Math.Min(bodyX, tipX - RingRadius) - 4;
        double minY = Math.Min(bodyY, tipY - RingRadius) - 4;
        double maxX = Math.Max(bodyX + BodyW, tipX + RingRadius) + 4;
        double maxY = Math.Max(bodyY + BodyH, tipY + RingRadius) + 4;

        return new CalloutGeo(scale, tipX, tipY, bodyX, bodyY,
            vertical, mouseAbove, mouseLeft,
            minX, minY, maxX - minX, maxY - minY);
    }

    [DllImport("gdi32.dll")] private static extern bool DeleteObject(IntPtr hObject);
}

// ── Content type enum ────────────────────────────────────────────────────────

internal enum CalloutContentType { Text, Script, Image, Clipboard }

// ── Static show helper ───────────────────────────────────────────────────────

internal static class CalloutInputOverlay
{
    /// <summary>
    /// Show a callout for type/paste input confirmation.
    /// targetRect: screen coordinates of the target UIA element BoundingRect.
    /// Returns true if user confirmed.
    /// </summary>
    public static bool Show(
        string content,
        CalloutContentType contentType = CalloutContentType.Text,
        System.Drawing.Rectangle targetRect = default,
        int timeoutSeconds = 0,
        System.Drawing.Bitmap? image = null)
        => CalloutInputWindow.Show(content, contentType, targetRect, timeoutSeconds, image);

    /// <summary>
    /// Readiness-focused callout: countdown with user-activity reset, confirm/deny,
    /// and SmartSetForegroundWindow on approval. Replacement for UserInputWaitOverlay.Show.
    /// </summary>
    public static (bool approved, bool focusAcquired, bool deniedByUser) ShowForReadiness(
        string content,
        System.Drawing.Rectangle targetRect,
        int timeoutSeconds,
        IntPtr targetHwnd,
        int resetSeconds = 30)
        => CalloutInputWindow.ShowForReadiness(content, targetRect, timeoutSeconds, targetHwnd, resetSeconds);

    /// <summary>
    /// Type-delegation callout: shows queued type request with queue depth badge.
    /// Outputs exact grap of callout window + delegation notice to stdout.
    /// Returns true if confirmed.
    /// </summary>
    public static bool ShowForTypeAction(
        string textToType,
        System.Drawing.Rectangle targetRect,
        int queueDepth = 0)
        => CalloutInputWindow.ShowForTypeAction(textToType, targetRect, queueDepth);
}
