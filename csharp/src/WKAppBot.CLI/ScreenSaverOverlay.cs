using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

/// <summary>
/// Sunset screensaver: per-monitor overlay windows with independent X-ray control.
/// Click-through (WS_EX_TRANSPARENT) — does not block any input.
/// 10s → golden hour, 1min → red sunset, 2min → purple twilight, 3min → night sky.
/// Each monitor gets its own Window — allows independent transparency/X-ray per monitor.
/// </summary>
internal sealed class ScreenSaverOverlay : IDisposable
{
    private const uint FadeStartMs  = 10_000;
    private const uint FadeEndMs    = 180_000;
    private const double MaxOpacity = 0.99;

    private static readonly (double t, Color color)[] SunsetStops =
    {
        (0.00, Color.FromRgb(0xFF, 0xB3, 0x47)),
        (0.15, Color.FromRgb(0xFF, 0x6B, 0x35)),
        (0.35, Color.FromRgb(0xFF, 0x45, 0x00)),
        (0.55, Color.FromRgb(0xC7, 0x15, 0x85)),
        (0.75, Color.FromRgb(0x4B, 0x00, 0x82)),
        (0.90, Color.FromRgb(0x0D, 0x00, 0x3D)),
        (1.00, Color.FromRgb(0x00, 0x00, 0x10)),
    };

    /// <summary>Per-monitor window state.</summary>
    private sealed class MonitorWindow
    {
        public Window Window = null!;
        public int X, Y, W, H;
        public bool XRayActive; // independent X-ray per monitor
        public double LastT;    // per-monitor visual throttle (was global _lastT → only first monitor updated)
    }

    private Thread? _thread;
    private Dispatcher? _dispatcher;
    private readonly List<MonitorWindow> _monitors = new();
    private readonly ManualResetEventSlim _ready = new(false);
    private volatile bool _isVisible;
    private volatile bool _disposed;
    private double _currentOpacity;
    private DateTime _lastTopmostUtc = DateTime.MinValue;
    /// <summary>Set when user input detected while visible — process should self-terminate.</summary>
    public volatile bool ShouldExit;
    // _lastT removed — now per-monitor (MonitorWindow.LastT)

    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_TOOLWINDOW   = 0x00000080;
    private const int WS_EX_NOACTIVATE   = 0x08000000;
    private const int WS_EX_TRANSPARENT  = 0x00000020;
    private const int WS_EX_LAYERED      = 0x00080000;
    private const int HWND_TOPMOST = -1;
    private const uint SWP_NOMOVE  = 0x0002;
    private const uint SWP_NOSIZE  = 0x0001;
    private const uint SWP_NOACTIVATE = 0x0010;

    [DllImport("user32.dll")] private static extern int GetSystemMetrics(int nIndex);
    [DllImport("user32.dll")] private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
    [DllImport("user32.dll")] private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")] private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("user32.dll")] private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")] private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
    [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    private const uint LWA_ALPHA = 0x02;
    private const uint WM_CLOSE = 0x0010;
    [DllImport("user32.dll")] private static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam, uint fuFlags, uint uTimeout, out IntPtr lpdwResult);

    // EnumDisplayMonitors for per-monitor enumeration
    private delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);
    [DllImport("user32.dll")] private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);
    [StructLayout(LayoutKind.Sequential)] private struct RECT { public int Left, Top, Right, Bottom; }

    public void Start()
    {
        if (_thread != null) return;
        _thread = new Thread(UiThread) { IsBackground = true, Name = "ScreenSaver" };
        _thread.SetApartmentState(ApartmentState.STA);
        _thread.Start();
        _ready.Wait(3000);

        // Per-window watchdog: each monitor gets its own thread.
        // If one window's Win32 call blocks, others still vanish independently.
        for (int mi = 0; mi < _monitors.Count; mi++)
        {
            var mwin = _monitors[mi];
            var idx = mi;
            new Thread(() =>
            {
                IntPtr hwnd = IntPtr.Zero;
                // Wait for HWND (window created on STA thread)
                for (int retry = 0; retry < 20 && hwnd == IntPtr.Zero; retry++)
                {
                    Thread.Sleep(250);
                    try { _dispatcher?.Invoke(() => hwnd = new WindowInteropHelper(mwin.Window).Handle); } catch { }
                }
                while (!_disposed)
                {
                    Thread.Sleep(500);
                    if (_isVisible && NativeMethods.GetUserIdleMs() < 3000)
                    {
                        Console.WriteLine($"[SS:GUARD:{idx}] User input! 9-layer kill on monitor {idx} (hwnd=0x{hwnd:X})");
                        if (hwnd != IntPtr.Zero)
                        {
                            try { SetLayeredWindowAttributes(hwnd, 0, 0, LWA_ALPHA); } catch { }  // 1) alpha=0
                            try { ShowWindow(hwnd, 0); } catch { }                                // 2) SW_HIDE
                            try { SetWindowPos(hwnd, (IntPtr)(-2), -32000, -32000, 1, 1, SWP_NOACTIVATE); } catch { } // 3) un-topmost + offscreen
                            try { SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero); } catch { } // 4) WM_CLOSE
                        }
                    }
                }
            }) { IsBackground = true, Name = $"SS-Guard-{idx}" }.Start();
        }
        // Master watchdog: force-kills entire process (last resort)
        new Thread(() =>
        {
            while (!_disposed)
            {
                Thread.Sleep(500);
                if (_isVisible && NativeMethods.GetUserIdleMs() < 3000)
                {
                    _isVisible = false;
                    Console.WriteLine("[SS:MASTER] Force terminating process!");
                    try { Environment.Exit(0); } catch { }
                    Thread.Sleep(200);
                    try { System.Diagnostics.Process.GetCurrentProcess().Kill(); } catch { }
                }
                // Dispatcher liveness ping: send WM_NULL to STA thread's message pump.
                // If Dispatcher is frozen/deadlocked, SendMessageTimeout returns 0 → kill.
                if (_isVisible && _monitors.Count > 0)
                {
                    try
                    {
                        var hwnd = IntPtr.Zero;
                        try { _dispatcher?.Invoke(() => hwnd = new WindowInteropHelper(_monitors[0].Window).Handle); } catch { }
                        if (hwnd != IntPtr.Zero)
                        {
                            var ok = SendMessageTimeout(hwnd, 0 /*WM_NULL*/, IntPtr.Zero, IntPtr.Zero,
                                0x0002 /*SMTO_ABORTIFHUNG*/, 100, out _);
                            if (ok == IntPtr.Zero)
                            {
                                Console.WriteLine("[SS:MASTER] Dispatcher frozen (WM_NULL timeout 100ms) — force kill!");
                                foreach (var mwin in _monitors)
                                {
                                    try
                                    {
                                        var h = new WindowInteropHelper(mwin.Window).Handle;
                                        ShowWindow(h, 0);
                                        SetWindowPos(h, (IntPtr)(-2), -32000, -32000, 1, 1, SWP_NOACTIVATE);
                                    }
                                    catch { }
                                }
                                try { Environment.Exit(0); } catch { }
                                try { System.Diagnostics.Process.GetCurrentProcess().Kill(); } catch { }
                            }
                        }
                    }
                    catch { }
                }
            }
        }) { IsBackground = true, Name = "SS-Master" }.Start();
    }

    private void UiThread()
    {
        // Enumerate monitors
        var monitorRects = new List<(int x, int y, int w, int h)>();
        EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, (IntPtr hMon, IntPtr hdc, ref RECT rect, IntPtr data) =>
        {
            monitorRects.Add((rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top));
            return true;
        }, IntPtr.Zero);

        if (monitorRects.Count == 0)
        {
            // Fallback: single virtual screen
            int vx = GetSystemMetrics(76), vy = GetSystemMetrics(77);
            int vw = GetSystemMetrics(78), vh = GetSystemMetrics(79);
            monitorRects.Add((vx, vy, vw, vh));
        }

        Console.WriteLine($"[SCREENSAVER] {monitorRects.Count} monitor(s) detected");

        var wallpaperPath = GetDesktopWallpaperPath();
        BitmapImage? wallpaperBmp = null;
        if (!string.IsNullOrEmpty(wallpaperPath) && File.Exists(wallpaperPath))
        {
            try
            {
                wallpaperBmp = new BitmapImage();
                wallpaperBmp.BeginInit();
                wallpaperBmp.UriSource = new Uri(wallpaperPath);
                wallpaperBmp.CacheOption = BitmapCacheOption.OnLoad;
                wallpaperBmp.EndInit();
                wallpaperBmp.Freeze();
            }
            catch { wallpaperBmp = null; }
        }

        // Create one window per monitor
        var rng = new Random(42);
        for (int mi = 0; mi < monitorRects.Count; mi++)
        {
            var (mx, my, mw, mh) = monitorRects[mi];
            var mwin = new MonitorWindow { X = mx, Y = my, W = mw, H = mh };

            mwin.Window = new Window
            {
                WindowStyle = WindowStyle.None,
                AllowsTransparency = true,
                Background = new SolidColorBrush(SunsetStops[0].color),
                Opacity = 0,
                Topmost = true,
                ShowInTaskbar = false,
                ShowActivated = false,
                ResizeMode = ResizeMode.NoResize,
                Left = mx, Top = my, Width = mw, Height = mh,
                Title = $"WKAppBot ScreenSaver #{mi}",
            };

            var grid = new System.Windows.Controls.Grid();
            mwin.Window.Content = grid;

            // Layer 0: wallpaper
            if (wallpaperBmp != null)
            {
                grid.Children.Add(new System.Windows.Controls.Image
                {
                    Source = wallpaperBmp, Stretch = System.Windows.Media.Stretch.UniformToFill, Tag = "desktop",
                });
            }

            // Layer 1: sunset overlay
            grid.Children.Add(new System.Windows.Controls.Border
            {
                Background = new SolidColorBrush(SunsetStops[0].color), Opacity = 0, Tag = "sunset",
            });

            // Layer 2: sky canvas (stars, moon, saturn, waves)
            var canvas = new System.Windows.Controls.Canvas { Tag = "sky" };
            grid.Children.Add(canvas);

            // Stars (per-monitor positions)
            for (int si = 0; si < 40; si++) // 40 per monitor (was 120 total)
            {
                double sx = rng.NextDouble() * mw;
                double sy = rng.NextDouble() * mh;
                double size = rng.NextDouble() * 2.5 + 0.5;
                double starAlpha = rng.NextDouble() * 0.6 + 0.4;
                var star = new System.Windows.Shapes.Ellipse
                {
                    Width = size, Height = size,
                    Fill = new SolidColorBrush(Color.FromArgb((byte)(starAlpha * 255), 255, 255, 240)),
                    Opacity = 0, Tag = "star",
                };
                System.Windows.Controls.Canvas.SetLeft(star, sx);
                System.Windows.Controls.Canvas.SetTop(star, sy);
                canvas.Children.Add(star);
            }

            // Moon (only on primary/first monitor)
            if (mi == 0)
            {
                double moonX = mw * 0.82, moonY = mh * 0.12;
                canvas.Children.Add(CreateMoon(moonX, moonY));
                canvas.Children.Add(CreateMoonGlow(moonX, moonY));
                foreach (var el in CreateSaturn(rng, mw, mh)) canvas.Children.Add(el);
            }

            // Waves (per-monitor)
            for (int wl = 0; wl < 3; wl++)
            {
                canvas.Children.Add(new System.Windows.Shapes.Path
                {
                    Stroke = Brushes.Transparent,
                    Fill = new SolidColorBrush(Color.FromArgb(
                        (byte)(40 + wl * 20), (byte)(20 + wl * 15), (byte)(40 + wl * 20), (byte)(80 + wl * 30))),
                    Opacity = 0, Tag = $"wave{wl}",
                });
            }

            mwin.Window.SourceInitialized += (_, _) =>
            {
                var helper = new WindowInteropHelper(mwin.Window);
                var hwnd = helper.Handle;
                var exStyle = GetWindowLongPtr(hwnd, GWL_EXSTYLE);
                exStyle = (IntPtr)((long)exStyle | WS_EX_TRANSPARENT | WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW | WS_EX_LAYERED);
                SetWindowLongPtr(hwnd, GWL_EXSTYLE, exStyle);
                SetWindowPos(hwnd, (IntPtr)HWND_TOPMOST, mwin.X, mwin.Y, mwin.W, mwin.H, SWP_NOACTIVATE);
            };

            mwin.Window.Visibility = Visibility.Hidden;
            mwin.Window.Show();
            mwin.Window.Visibility = Visibility.Hidden;

            _monitors.Add(mwin);
        }

        _dispatcher = Dispatcher.CurrentDispatcher;
        _ready.Set();
        Dispatcher.Run();
    }

    private static System.Windows.Shapes.Ellipse CreateMoon(double x, double y)
    {
        var moon = new System.Windows.Shapes.Ellipse
        {
            Width = 40, Height = 40,
            Fill = new RadialGradientBrush(Color.FromArgb(220, 255, 255, 230), Color.FromArgb(60, 200, 200, 180)),
            Opacity = 0, Tag = "moon",
        };
        System.Windows.Controls.Canvas.SetLeft(moon, x);
        System.Windows.Controls.Canvas.SetTop(moon, y);
        return moon;
    }

    private static System.Windows.Shapes.Ellipse CreateMoonGlow(double x, double y)
    {
        var glow = new System.Windows.Shapes.Ellipse
        {
            Width = 80, Height = 80,
            Fill = new RadialGradientBrush(Color.FromArgb(30, 255, 255, 200), Color.FromArgb(0, 255, 255, 200)),
            Opacity = 0, Tag = "moon",
        };
        System.Windows.Controls.Canvas.SetLeft(glow, x - 20);
        System.Windows.Controls.Canvas.SetTop(glow, y - 20);
        return glow;
    }

    private static System.Windows.UIElement[] CreateSaturn(Random rng, int mw, int mh)
    {
        double sx = mw * 0.18, sy = mh * 0.22;
        var body = new System.Windows.Shapes.Ellipse
        {
            Width = 18, Height = 16,
            Fill = new RadialGradientBrush(Color.FromArgb(200, 210, 180, 120), Color.FromArgb(150, 180, 150, 80)),
            Opacity = 0, Tag = "star",
        };
        System.Windows.Controls.Canvas.SetLeft(body, sx);
        System.Windows.Controls.Canvas.SetTop(body, sy);
        var ring = new System.Windows.Shapes.Ellipse
        {
            Width = 36, Height = 8,
            Stroke = new SolidColorBrush(Color.FromArgb(160, 210, 190, 130)),
            StrokeThickness = 1.5, Fill = Brushes.Transparent,
            Opacity = 0, Tag = "star",
            RenderTransform = new RotateTransform(-20, 18, 4),
        };
        System.Windows.Controls.Canvas.SetLeft(ring, sx - 9);
        System.Windows.Controls.Canvas.SetTop(ring, sy + 4);
        return [body, ring];
    }

    // Overload to add array of elements to canvas
    private static void AddRange(System.Windows.Controls.Canvas c, System.Windows.UIElement[] items)
    { foreach (var i in items) c.Children.Add(i); }

    public void Tick()
    {
        if (_disposed || _dispatcher == null || _monitors.Count == 0) return;

        var idleMs = NativeMethods.GetUserIdleMs();

        if (idleMs >= FadeStartMs)
        {
            double t = Math.Min(1.0, (double)(idleMs - FadeStartMs) / (FadeEndMs - FadeStartMs));
            double targetOpacity = t * MaxOpacity;

            bool wasHidden = !_isVisible;
            if (wasHidden)
            {
                // First show: opacity=0, then fade in naturally via subsequent ticks
                _currentOpacity = 0;
                _isVisible = true;
                _lastTopmostUtc = DateTime.UtcNow;
                _dispatcher.BeginInvoke(() =>
                {
                    foreach (var mwin in _monitors)
                    {
                        mwin.Window.Opacity = 0;
                        mwin.Window.Visibility = Visibility.Visible;
                        var helper = new WindowInteropHelper(mwin.Window);
                        SetWindowPos(helper.Handle, (IntPtr)HWND_TOPMOST, 0, 0, 0, 0,
                            SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
                    }
                });
                Console.WriteLine("[EYE] ScreenSaver fade start (user idle ≥10s)");
            }
            else if (Math.Abs(targetOpacity - _currentOpacity) >= 0.005)
            {
                _currentOpacity = targetOpacity;

                _dispatcher.BeginInvoke(() =>
                {
                    foreach (var mwin in _monitors)
                    {
                        if (mwin.XRayActive) continue;
                        mwin.Window.Opacity = targetOpacity;
                        UpdateMonitorVisuals(mwin, t);

                        // Re-topmost at most every 5 minutes (avoid blocking other windows)
                        if ((DateTime.UtcNow - _lastTopmostUtc).TotalMinutes >= 5)
                        {
                            var helper = new WindowInteropHelper(mwin.Window);
                            SetWindowPos(helper.Handle, (IntPtr)HWND_TOPMOST, 0, 0, 0, 0,
                                SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
                        }
                    }
                    if ((DateTime.UtcNow - _lastTopmostUtc).TotalMinutes >= 5)
                        _lastTopmostUtc = DateTime.UtcNow;
                });
            }
        }
        else if (_isVisible)
        {
            // ── 9-layer user input safety: IMMEDIATELY vanish on ANY user activity ──
            // 1) State flags
            _isVisible = false;
            _currentOpacity = 0;
            ShouldExit = true;
            // 2-6) Sync Invoke: opacity=0, hidden, un-topmost, all monitors
            _dispatcher.Invoke(() =>
            {
                foreach (var mwin in _monitors)
                {
                    mwin.XRayActive = false;
                    // 2) Opacity zero
                    mwin.Window.Opacity = 0;
                    // 3) Visibility hidden
                    mwin.Window.Visibility = Visibility.Hidden;
                    // 4) Remove topmost (HWND_NOTOPMOST = -2)
                    var helper = new WindowInteropHelper(mwin.Window);
                    SetWindowPos(helper.Handle, (IntPtr)(-2), 0, 0, 0, 0,
                        SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
                    // 5) Move off-screen as extra safety
                    SetWindowPos(helper.Handle, IntPtr.Zero, -32000, -32000, 1, 1, SWP_NOACTIVATE);
                }
            });
            // 7) Log
            Console.WriteLine("[EYE] ScreenSaver OFF + ShouldExit (user input detected)");
            // 8) Process self-terminate (no lingering)
            try { Environment.Exit(0); } catch { }
            // 9) Force kill if Exit didn't work
            try { System.Diagnostics.Process.GetCurrentProcess().Kill(); } catch { }
        }
    }

    private void UpdateMonitorVisuals(MonitorWindow mwin, double t)
    {
        // Per-monitor throttle: skip if this monitor's visuals haven't changed enough
        if (Math.Abs(t - mwin.LastT) < 0.01) return;
        mwin.LastT = t;

        var color = InterpolateSunset(t);
        if (mwin.Window.Content is not System.Windows.Controls.Grid grid) return;

        foreach (System.Windows.UIElement child in grid.Children)
        {
            if (child is System.Windows.Controls.Border border && border.Tag as string == "sunset")
            {
                border.Background = new SolidColorBrush(color);
                border.Opacity = 0.5 + t * 0.45;
                break;
            }
        }

        var canvas = grid.Children.OfType<System.Windows.Controls.Canvas>().FirstOrDefault();
        if (canvas == null) return;

        double nightAlpha = t > 0.7 ? (t - 0.7) / 0.3 : 0;
        double waveAlpha = t > 0.3 ? Math.Min(1.0, (t - 0.3) / 0.3) : 0;
        double now = DateTime.Now.Ticks / 10_000_000.0;

        foreach (System.Windows.UIElement child in canvas.Children)
        {
            if (child is System.Windows.Shapes.Ellipse el)
            {
                var tag = el.Tag as string;
                if (tag == "star")
                    el.Opacity = nightAlpha * (0.4 + 0.6 * Math.Sin(now * 1.5 + el.Width * 100));
                else if (tag == "moon")
                    el.Opacity = nightAlpha;
            }
            else if (child is System.Windows.Shapes.Path wavePath)
            {
                var tag = wavePath.Tag as string ?? "";
                if (!tag.StartsWith("wave")) continue;
                int layer = tag.Length > 4 && char.IsDigit(tag[4]) ? tag[4] - '0' : 0;

                wavePath.Opacity = waveAlpha;
                double speed = 0.4 + layer * 0.15;
                double amp = 8 + layer * 4;
                double freq = 0.008 - layer * 0.001;
                double baseY = canvas.ActualHeight * 0.85 + layer * 20;
                double w = Math.Max(1, canvas.ActualWidth);

                var geo = new StreamGeometry();
                using (var ctx = geo.Open())
                {
                    ctx.BeginFigure(new System.Windows.Point(0, canvas.ActualHeight), true, true);
                    ctx.LineTo(new System.Windows.Point(0, baseY + amp * Math.Sin(now * speed)), true, false);
                    for (double px = 10; px <= w; px += 10)
                        ctx.LineTo(new System.Windows.Point(px, baseY + amp * Math.Sin(px * freq + now * speed)), true, false);
                    ctx.LineTo(new System.Windows.Point(w, canvas.ActualHeight), true, false);
                }
                geo.Freeze();
                wavePath.Data = geo;
            }
        }
    }

    /// <summary>Toggle X-ray (transparency) for the monitor containing the given screen point.</summary>
    public void ToggleXRay(int screenX, int screenY)
    {
        if (_dispatcher == null) return;
        _dispatcher.BeginInvoke(() =>
        {
            foreach (var mwin in _monitors)
            {
                if (screenX >= mwin.X && screenX < mwin.X + mwin.W &&
                    screenY >= mwin.Y && screenY < mwin.Y + mwin.H)
                {
                    mwin.XRayActive = !mwin.XRayActive;
                    if (mwin.XRayActive)
                    {
                        mwin.Window.Opacity = 0;
                        mwin.Window.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        // X-ray OFF: restore visibility + current opacity
                        mwin.Window.Visibility = Visibility.Visible;
                        mwin.Window.Opacity = _currentOpacity;
                    }
                    Console.WriteLine($"[SCREENSAVER] X-ray {(mwin.XRayActive ? "ON" : "OFF")} for monitor at ({mwin.X},{mwin.Y} {mwin.W}x{mwin.H})");
                    break;
                }
            }
        });
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern bool SystemParametersInfoW(uint uiAction, uint uiParam, System.Text.StringBuilder pvParam, uint fWinIni);

    private static string? GetDesktopWallpaperPath()
    {
        var sb = new System.Text.StringBuilder(260);
        return SystemParametersInfoW(0x0073, 260, sb, 0) ? sb.ToString() : null;
    }

    private static Color InterpolateSunset(double t)
    {
        t = Math.Clamp(t, 0, 1);
        for (int i = 0; i < SunsetStops.Length - 1; i++)
        {
            var (t0, c0) = SunsetStops[i];
            var (t1, c1) = SunsetStops[i + 1];
            if (t >= t0 && t <= t1)
            {
                double f = (t - t0) / (t1 - t0);
                return Color.FromRgb(
                    (byte)(c0.R + f * (c1.R - c0.R)),
                    (byte)(c0.G + f * (c1.G - c0.G)),
                    (byte)(c0.B + f * (c1.B - c0.B)));
            }
        }
        return SunsetStops[^1].color;
    }

    public bool IsVisible => _isVisible;

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _isVisible = false;
        try { _dispatcher?.InvokeShutdown(); _thread?.Join(2000); } catch { }
        _ready.Dispose();
    }
}
