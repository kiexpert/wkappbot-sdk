using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

/// <summary>
/// Security screensaver: 90% opaque black overlay covering all monitors.
/// Click-through (WS_EX_TRANSPARENT) — does not block any input.
/// Shows after 10s user idle; hides instantly on any keyboard/mouse input.
/// Runs on a dedicated STA thread managed by Eye global loop.
/// </summary>
internal sealed class ScreenSaverOverlay : IDisposable
{
    private const uint IdleThresholdMs = 10_000;   // 10 seconds idle → show
    private const double OverlayOpacity = 0.95;    // 95% opaque

    private Thread? _thread;
    private Dispatcher? _dispatcher;
    private Window? _window;
    private readonly ManualResetEventSlim _ready = new(false);
    private volatile bool _isVisible;
    private volatile bool _disposed;

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
            Background = new SolidColorBrush(Colors.Black),
            Opacity = OverlayOpacity,
            Topmost = true,
            ShowInTaskbar = false,
            ShowActivated = false,
            ResizeMode = ResizeMode.NoResize,
            // Use device-independent coords — will fix with SetWindowPos below
            Left = vx,
            Top = vy,
            Width = vw,
            Height = vh,
            Title = "WKAppBot ScreenSaver",
        };

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
    /// Checks user idle time and shows/hides the overlay accordingly.
    /// </summary>
    public void Tick()
    {
        if (_disposed || _dispatcher == null || _window == null) return;

        var idleMs = NativeMethods.GetUserIdleMs();

        if (idleMs >= IdleThresholdMs && !_isVisible)
        {
            _isVisible = true;
            _dispatcher.BeginInvoke(() =>
            {
                if (_window == null) return;
                _window.Visibility = Visibility.Visible;

                // Re-assert topmost on show
                var helper = new WindowInteropHelper(_window);
                SetWindowPos(helper.Handle, (IntPtr)HWND_TOPMOST, 0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
            });
            Console.WriteLine("[EYE] ScreenSaver ON (user idle ≥10s)");
        }
        else if (idleMs < IdleThresholdMs && _isVisible)
        {
            _isVisible = false;
            _dispatcher.BeginInvoke(() =>
            {
                if (_window != null) _window.Visibility = Visibility.Hidden;
            });
            Console.WriteLine("[EYE] ScreenSaver OFF (user input detected)");
        }
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
