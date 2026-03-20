using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

/// <summary>
/// Sunset screensaver: gradual color-shifting overlay covering all monitors.
/// Click-through (WS_EX_TRANSPARENT) — does not block any input.
/// 10s → golden hour, 1min → red sunset, 2min → purple twilight, 3min → night sky.
/// Runs on a dedicated STA thread managed by Eye global loop.
/// </summary>
internal sealed class ScreenSaverOverlay : IDisposable
{
    private const uint FadeStartMs  = 10_000;      // 10s idle → fade begins
    private const uint FadeEndMs    = 180_000;     // 3min idle → max opacity
    private const double MaxOpacity = 0.95;        // 95% opaque at full fade

    // Sunset color stops: (progress 0.0-1.0, color, opacity at this stage)
    private static readonly (double t, Color color)[] SunsetStops =
    {
        (0.00, Color.FromRgb(0xFF, 0xB3, 0x47)),  // golden hour — warm amber
        (0.15, Color.FromRgb(0xFF, 0x6B, 0x35)),  // orange glow
        (0.35, Color.FromRgb(0xFF, 0x45, 0x00)),  // red sunset
        (0.55, Color.FromRgb(0xC7, 0x15, 0x85)),  // magenta horizon
        (0.75, Color.FromRgb(0x4B, 0x00, 0x82)),  // purple twilight
        (0.90, Color.FromRgb(0x0D, 0x00, 0x3D)),  // deep indigo
        (1.00, Color.FromRgb(0x00, 0x00, 0x10)),  // night sky
    };

    private Thread? _thread;
    private Dispatcher? _dispatcher;
    private Window? _window;
    private readonly ManualResetEventSlim _ready = new(false);
    private volatile bool _isVisible;
    private volatile bool _disposed;
    private double _currentOpacity;
    private double _lastT = -1;  // last color progress (avoid redundant updates)

    // Win32 extended styles for click-through
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

    private const int SM_XVIRTUALSCREEN  = 76;
    private const int SM_YVIRTUALSCREEN  = 77;
    private const int SM_CXVIRTUALSCREEN = 78;
    private const int SM_CYVIRTUALSCREEN = 79;

    public void Start()
    {
        if (_thread != null) return;
        _thread = new Thread(UiThread) { IsBackground = true, Name = "ScreenSaver" };
        _thread.SetApartmentState(ApartmentState.STA);
        _thread.Start();
        _ready.Wait(3000);
    }

    private void UiThread()
    {
        // Virtual screen bounds (all monitors combined)
        int vx = GetSystemMetrics(SM_XVIRTUALSCREEN);
        int vy = GetSystemMetrics(SM_YVIRTUALSCREEN);
        int vw = GetSystemMetrics(SM_CXVIRTUALSCREEN);
        int vh = GetSystemMetrics(SM_CYVIRTUALSCREEN);

        _window = new Window
        {
            WindowStyle = WindowStyle.None,
            AllowsTransparency = true,
            Background = new SolidColorBrush(SunsetStops[0].color),
            Opacity = 0,
            Topmost = true,
            ShowInTaskbar = false,
            ShowActivated = false,
            ResizeMode = ResizeMode.NoResize,
            Left = vx,
            Top = vy,
            Width = vw,
            Height = vh,
            Title = "WKAppBot ScreenSaver",
        };

        // Stars + moon canvas (drawn when night falls)
        var canvas = new System.Windows.Controls.Canvas();
        _window.Content = canvas;

        // Pre-generate stars (random positions, sizes, opacities)
        var rng = new Random(42); // fixed seed = same constellation every time
        for (int si = 0; si < 120; si++)
        {
            double sx = rng.NextDouble() * vw;
            double sy = rng.NextDouble() * vh;
            double size = rng.NextDouble() * 2.5 + 0.5;
            double starAlpha = rng.NextDouble() * 0.6 + 0.4;
            var star = new System.Windows.Shapes.Ellipse
            {
                Width = size, Height = size,
                Fill = new SolidColorBrush(Color.FromArgb((byte)(starAlpha * 255), 255, 255, 240)),
                Opacity = 0, // hidden until night
                Tag = "star",
            };
            System.Windows.Controls.Canvas.SetLeft(star, sx);
            System.Windows.Controls.Canvas.SetTop(star, sy);
            canvas.Children.Add(star);
        }

        // Moon (crescent-like: two overlapping circles)
        var moonX = vw * 0.82;
        var moonY = vh * 0.12;
        var moon = new System.Windows.Shapes.Ellipse
        {
            Width = 40, Height = 40,
            Fill = new RadialGradientBrush(
                Color.FromArgb(220, 255, 255, 230),
                Color.FromArgb(60, 200, 200, 180)),
            Opacity = 0,
            Tag = "moon",
        };
        System.Windows.Controls.Canvas.SetLeft(moon, moonX);
        System.Windows.Controls.Canvas.SetTop(moon, moonY);
        canvas.Children.Add(moon);

        // Moon glow (larger, softer)
        var moonGlow = new System.Windows.Shapes.Ellipse
        {
            Width = 80, Height = 80,
            Fill = new RadialGradientBrush(
                Color.FromArgb(30, 255, 255, 200),
                Color.FromArgb(0, 255, 255, 200)),
            Opacity = 0,
            Tag = "moon",
        };
        System.Windows.Controls.Canvas.SetLeft(moonGlow, moonX - 20);
        System.Windows.Controls.Canvas.SetTop(moonGlow, moonY - 20);
        canvas.Children.Add(moonGlow);

        _window.SourceInitialized += (_, _) =>
        {
            var helper = new WindowInteropHelper(_window);
            var hwnd = helper.Handle;

            // Extended style: click-through + no-activate + tool window + layered
            var exStyle = GetWindowLongPtr(hwnd, GWL_EXSTYLE);
            exStyle = (IntPtr)((long)exStyle | WS_EX_TRANSPARENT | WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW | WS_EX_LAYERED);
            SetWindowLongPtr(hwnd, GWL_EXSTYLE, exStyle);

            // Position exactly over all monitors (physical pixels)
            SetWindowPos(hwnd, (IntPtr)HWND_TOPMOST, vx, vy, vw, vh, SWP_NOACTIVATE);
        };

        // Start hidden
        _window.Visibility = Visibility.Hidden;
        _window.Show();
        _window.Visibility = Visibility.Hidden;

        _dispatcher = Dispatcher.CurrentDispatcher;
        _ready.Set();

        Dispatcher.Run();
    }

    /// <summary>
    /// Called from Eye loop every frame (~100ms).
    /// Gradually fades in: 10s idle → 0%, 3min idle → 95%. Input → instant hide.
    /// </summary>
    public void Tick()
    {
        if (_disposed || _dispatcher == null || _window == null) return;

        var idleMs = NativeMethods.GetUserIdleMs();

        if (idleMs >= FadeStartMs)
        {
            // Linear interpolation: 0% at FadeStartMs → MaxOpacity at FadeEndMs
            double t = Math.Min(1.0, (double)(idleMs - FadeStartMs) / (FadeEndMs - FadeStartMs));
            double targetOpacity = t * MaxOpacity;

            // Only update if opacity changed meaningfully (avoid spamming dispatcher)
            if (Math.Abs(targetOpacity - _currentOpacity) >= 0.005)
            {
                _currentOpacity = targetOpacity;
                bool wasHidden = !_isVisible;
                _isVisible = true;

                _dispatcher.BeginInvoke(() =>
                {
                    if (_window == null) return;
                    if (wasHidden)
                    {
                        _window.Visibility = Visibility.Visible;
                        var helper = new WindowInteropHelper(_window);
                        SetWindowPos(helper.Handle, (IntPtr)HWND_TOPMOST, 0, 0, 0, 0,
                            SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
                    }
                    _window.Opacity = targetOpacity;

                    // Sunset color transition
                    if (Math.Abs(t - _lastT) >= 0.01)
                    {
                        _lastT = t;
                        var color = InterpolateSunset(t);
                        _window.Background = new SolidColorBrush(color);

                        // Stars + moon: fade in during twilight→night (t > 0.7)
                        if (_window.Content is System.Windows.Controls.Canvas canvas)
                        {
                            double nightAlpha = t > 0.7 ? (t - 0.7) / 0.3 : 0; // 0→1 during 70%→100%
                            foreach (System.Windows.UIElement child in canvas.Children)
                            {
                                if (child is System.Windows.Shapes.Ellipse el)
                                {
                                    var tag = el.Tag as string;
                                    if (tag == "star")
                                        el.Opacity = nightAlpha * (0.4 + 0.6 * Math.Sin(DateTime.Now.Ticks / 30_000_000.0 + el.Width * 100)); // twinkle!
                                    else if (tag == "moon")
                                        el.Opacity = nightAlpha;
                                }
                            }
                        }
                    }
                });

                if (wasHidden)
                    Console.WriteLine("[EYE] ScreenSaver fade start (user idle ≥10s)");
            }
        }
        else if (_isVisible)
        {
            _isVisible = false;
            _currentOpacity = 0;
            _dispatcher.Invoke(() =>
            {
                if (_window == null) return;
                _window.Opacity = 0;
                _window.Visibility = Visibility.Hidden;
            });
            Console.WriteLine("[EYE] ScreenSaver OFF (user input detected)");
        }
    }

    /// <summary>Interpolate between sunset color stops based on progress t (0.0-1.0).</summary>
    private static Color InterpolateSunset(double t)
    {
        t = Math.Clamp(t, 0, 1);
        // Find the two stops surrounding t
        for (int i = 0; i < SunsetStops.Length - 1; i++)
        {
            var (t0, c0) = SunsetStops[i];
            var (t1, c1) = SunsetStops[i + 1];
            if (t >= t0 && t <= t1)
            {
                double f = (t - t0) / (t1 - t0); // local blend factor
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

        try
        {
            _dispatcher?.InvokeShutdown();
            _thread?.Join(2000);
        }
        catch { }

        _ready.Dispose();
    }
}
