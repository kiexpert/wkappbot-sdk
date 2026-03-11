using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WKAppBot.CLI;

/// <summary>
/// Adaptive zoom mode selection based on control size and visibility.
/// </summary>
internal enum ZoomMode
{
    /// <summary>Small control (w &lt; 200 AND h &lt; 60): 3x magnifier overlay with header/status.</summary>
    Magnifier,
    /// <summary>Large control + foreground (visible): highlight box border only (cyan 3px).</summary>
    HighlightBox,
    /// <summary>Large control + obscured (behind other windows): 1:1 relay overlay (no magnification).</summary>
    Relay,
}

/// <summary>
/// "Input Zoom Overlay" — semi-transparent WPF magnifier that shows a 2x enlarged
/// live view of the target control during input automation.
/// Positioned directly on top of the control, like a magnifying glass.
///
/// Design: "돋보기 중계방송" — see what the bot types in real-time!
/// Tag: [ZOOM]
///
/// Adaptive mode for large controls (e.g., Notepad text area):
///   - Small control (w&lt;200 AND h&lt;60): current 3x magnifier overlay
///   - Large control + foreground: highlight box only (cyan 3px border around control)
///   - Large control + obscured: 1:1 overlay (no magnification, just PrintWindow relay)
/// </summary>
internal sealed class InputZoomWindow : Window
{
    private readonly Image _controlImage;
    private readonly TextBlock _headerText;
    private readonly TextBlock _statusText;
    private readonly Border _outerBorder;
    private bool _firstFrameReceived;

    public InputZoomWindow(int width, int height)
    {
        // Window properties — semi-transparent overlay, never steals focus
        Title = "InputZoom";
        Width = width;
        Height = height;
        WindowStyle = WindowStyle.None;
        AllowsTransparency = true;
        Background = Brushes.Transparent;
        Topmost = true;
        ShowInTaskbar = false;
        ShowActivated = false; // CRITICAL: never steal focus from target app
        ResizeMode = ResizeMode.NoResize;
        Opacity = 0; // Start fully transparent — reveal on first frame load

        // Build visual tree programmatically (no XAML)
        _outerBorder = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(0xF5, 0x1A, 0x1A, 0x2E)), // near-opaque dark bg
            BorderBrush = new SolidColorBrush(Color.FromRgb(0x4F, 0xC3, 0xF7)), // cyan
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(4),
            Padding = new Thickness(4),
            ClipToBounds = true,
        };

        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(16) });                   // header
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // image
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(18) });                   // status

        // Header: "[ZOOM] input_m10"
        _headerText = new TextBlock
        {
            Text = "[ZOOM]",
            Foreground = new SolidColorBrush(Color.FromRgb(0x4F, 0xC3, 0xF7)), // cyan
            FontFamily = new FontFamily("Consolas"),
            FontSize = 10,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(2, 0, 0, 0),
        };
        Grid.SetRow(_headerText, 0);

        // Image: 2x magnified control capture (NearestNeighbor for pixel-sharp scaling)
        _controlImage = new Image
        {
            Stretch = Stretch.Uniform,
            Margin = new Thickness(2),
        };
        RenderOptions.SetBitmapScalingMode(_controlImage, BitmapScalingMode.NearestNeighbor);
        Grid.SetRow(_controlImage, 1);

        // Status: "Typing: 3/6  \"005___\""
        _statusText = new TextBlock
        {
            Text = "Ready",
            Foreground = new SolidColorBrush(Color.FromRgb(0xCC, 0xCC, 0xCC)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 10,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(2, 0, 0, 0),
        };
        Grid.SetRow(_statusText, 2);

        grid.Children.Add(_headerText);
        grid.Children.Add(_controlImage);
        grid.Children.Add(_statusText);
        _outerBorder.Child = grid;
        Content = _outerBorder;
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        var helper = new WindowInteropHelper(this);

        // WS_EX_NOACTIVATE: clicking the overlay won't steal focus from target app
        var exStyle = GetWindowLongPtr(helper.Handle, GWL_EXSTYLE);
        SetWindowLongPtr(helper.Handle, GWL_EXSTYLE,
            new IntPtr(exStyle.ToInt64() | WS_EX_NOACTIVATE));
    }

    /// <summary>Update the magnified control image from PNG data. Reveals overlay on first frame.</summary>
    public void UpdateImage(byte[] pngData)
    {
        try
        {
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(pngData);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            // NO DecodePixelWidth — keep full resolution for pixel-perfect 2x magnification
            bi.EndInit();
            bi.Freeze(); // thread-safe
            _controlImage.Source = bi;

            // Reveal on first frame — was fully transparent until now
            if (!_firstFrameReceived)
            {
                _firstFrameReceived = true;
                Opacity = 0.77;
            }
        }
        catch { /* best effort */ }
    }

    /// <summary>Update the header label (e.g., method name).</summary>
    public void UpdateHeader(string text)
    {
        _headerText.Text = text;
    }

    /// <summary>Update the status line (e.g., "Typing: 3/6").</summary>
    public void UpdateStatus(string text)
    {
        _statusText.Text = text;
    }

    /// <summary>Show pass/fail result with colored border and status text.</summary>
    public void ShowResult(bool pass, string text)
    {
        try
        {
            var color = pass
                ? Color.FromRgb(0x4C, 0xAF, 0x50)  // green
                : Color.FromRgb(0xFF, 0xA0, 0x00);  // amber (uncertain, not error)
            _outerBorder.BorderBrush = new SolidColorBrush(color);
            _statusText.Foreground = new SolidColorBrush(color);
            _statusText.Text = text;
            _statusText.FontWeight = FontWeights.Bold;
        }
        catch { /* best effort */ }
    }

    /// <summary>Fade out and auto-close after delay.</summary>
    public void BeginFadeOut(int delayMs = 3000, int fadeDurationMs = 800)
    {
        var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(delayMs) };
        timer.Tick += (_, _) =>
        {
            timer.Stop();
            var anim = new DoubleAnimation(Opacity, 0.0, TimeSpan.FromMilliseconds(fadeDurationMs));
            anim.Completed += (_, _) => Dispatcher.InvokeShutdown();
            BeginAnimation(OpacityProperty, anim);
        };
        timer.Start();
    }

    /// <summary>Force Win32 HWND_TOPMOST to stay above dropdowns and other popups.</summary>
    public void EnsureTopmost()
    {
        try
        {
            var helper = new WindowInteropHelper(this);
            if (helper.Handle != IntPtr.Zero)
                SetWindowPos(helper.Handle, HWND_TOPMOST, 0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        }
        catch { /* best effort */ }
    }

    // ── Win32 P/Invoke ──
    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_NOACTIVATE = 0x08000000;
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private const uint SWP_NOMOVE = 0x0002;
    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOACTIVATE = 0x0010;

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, uint uFlags);
}

/// <summary>
/// "Input Highlight Box" — transparent WPF window with just a colored border
/// placed exactly on the target control. Used for large, foreground-visible controls
/// where magnification is unnecessary (e.g., Notepad text area, Excel cells).
///
/// Design: "여기에 입력 중!" — simple highlight border around the target control.
/// </summary>
internal sealed class InputHighlightWindow : Window
{
    private readonly Border _outerBorder;
    private readonly TextBlock _statusText;

    public InputHighlightWindow(int width, int height)
    {
        Title = "InputHighlight";
        Width = width;
        Height = height;
        WindowStyle = WindowStyle.None;
        AllowsTransparency = true;
        Background = Brushes.Transparent;
        Topmost = true;
        ShowInTaskbar = false;
        ShowActivated = false; // CRITICAL: never steal focus from target app
        ResizeMode = ResizeMode.NoResize;
        Opacity = 0.77; // 77% opaque — lucky seven, clear enough to read

        // Just a border — transparent interior, no image/header
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(18) }); // status bar

        _outerBorder = new Border
        {
            Background = Brushes.Transparent, // see-through — control is visible underneath
            BorderBrush = new SolidColorBrush(Color.FromRgb(0x4F, 0xC3, 0xF7)), // cyan
            BorderThickness = new Thickness(3),
            CornerRadius = new CornerRadius(2),
        };
        Grid.SetRow(_outerBorder, 0);

        // Small status tag at bottom — semi-transparent dark bg
        var statusBorder = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(0xDD, 0x1A, 0x1A, 0x2E)),
            CornerRadius = new CornerRadius(0, 0, 2, 2),
            Padding = new Thickness(4, 1, 4, 1),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom,
        };
        _statusText = new TextBlock
        {
            Text = "[ZOOM] Ready",
            Foreground = new SolidColorBrush(Color.FromRgb(0x4F, 0xC3, 0xF7)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 10,
        };
        statusBorder.Child = _statusText;
        Grid.SetRow(statusBorder, 1);

        grid.Children.Add(_outerBorder);
        grid.Children.Add(statusBorder);
        Content = grid;
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        var helper = new WindowInteropHelper(this);
        // WS_EX_NOACTIVATE | WS_EX_TRANSPARENT: click-through + no focus steal
        var exStyle = GetWindowLongPtr(helper.Handle, GWL_EXSTYLE);
        SetWindowLongPtr(helper.Handle, GWL_EXSTYLE,
            new IntPtr(exStyle.ToInt64() | WS_EX_NOACTIVATE | WS_EX_TRANSPARENT));
    }

    public void UpdateStatus(string text) => _statusText.Text = text;

    public void ShowResult(bool pass, string text)
    {
        try
        {
            var color = pass
                ? Color.FromRgb(0x4C, 0xAF, 0x50)  // green
                : Color.FromRgb(0xFF, 0xA0, 0x00);  // amber
            _outerBorder.BorderBrush = new SolidColorBrush(color);
            _statusText.Foreground = new SolidColorBrush(color);
            _statusText.Text = text;
            _statusText.FontWeight = FontWeights.Bold;
        }
        catch { /* best effort */ }
    }

    public void BeginFadeOut(int delayMs = 2000, int fadeDurationMs = 600)
    {
        var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(delayMs) };
        timer.Tick += (_, _) =>
        {
            timer.Stop();
            var anim = new DoubleAnimation(Opacity, 0.0, TimeSpan.FromMilliseconds(fadeDurationMs));
            anim.Completed += (_, _) => Dispatcher.InvokeShutdown();
            BeginAnimation(OpacityProperty, anim);
        };
        timer.Start();
    }

    /// <summary>Force Win32 HWND_TOPMOST to stay above dropdowns and other popups.</summary>
    public void EnsureTopmost()
    {
        try
        {
            var helper = new WindowInteropHelper(this);
            if (helper.Handle != IntPtr.Zero)
                SetWindowPos(helper.Handle, HWND_TOPMOST, 0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        }
        catch { /* best effort */ }
    }

    // ── Win32 P/Invoke ──
    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_NOACTIVATE = 0x08000000;
    private const int WS_EX_TRANSPARENT = 0x00000020;
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private const uint SWP_NOMOVE = 0x0002;
    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOACTIVATE = 0x0010;

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, uint uFlags);
}

/// <summary>
/// Manages the STA thread for InputZoom/Highlight/Relay overlay windows.
/// Bridge between CLI main thread and WPF Dispatcher thread.
/// Short-lived: created before input, disposed after input completes.
///
/// Supports 3 modes:
///   Magnifier — 3x magnifier overlay (small controls)
///   HighlightBox — border-only overlay (large + foreground)
///   Relay — 1:1 relay overlay (large + obscured)
/// </summary>
internal sealed class InputZoomHost : IDisposable
{
    private Thread? _uiThread;
    private InputZoomWindow? _zoomWindow;         // Magnifier or Relay mode
    private InputHighlightWindow? _highlightWindow; // HighlightBox mode
    private Dispatcher? _dispatcher;
    private readonly ManualResetEventSlim _ready = new();
    private DispatcherTimer? _refreshTimer;
    private Func<byte[]?>? _captureFunc;

    public ZoomMode Mode { get; private set; }
    public bool IsAlive => _uiThread?.IsAlive == true;

    /// <summary>Start the overlay on a dedicated STA thread.</summary>
    public void Start(int screenX, int screenY, int width, int height, ZoomMode mode = ZoomMode.Magnifier)
    {
        Mode = mode;
        _uiThread = new Thread(() =>
        {
            if (mode == ZoomMode.HighlightBox)
            {
                _highlightWindow = new InputHighlightWindow(width, height);
                _highlightWindow.Left = screenX;
                _highlightWindow.Top = screenY;
                _dispatcher = Dispatcher.CurrentDispatcher;
                _ready.Set();
                _highlightWindow.Show();
                // Fix DPI mismatch: WPF Left/Top = DIPs, but we have physical pixels.
                // Use Win32 SetWindowPos to force exact physical pixel position.
                ForceWindowPosition(_highlightWindow, screenX, screenY, width, height);
            }
            else
            {
                _zoomWindow = new InputZoomWindow(width, height);
                _zoomWindow.Left = screenX;
                _zoomWindow.Top = screenY;
                _dispatcher = Dispatcher.CurrentDispatcher;
                _ready.Set();
                _zoomWindow.Show();
                // Fix DPI mismatch: force exact physical pixel position via Win32
                ForceWindowPosition(_zoomWindow, screenX, screenY, width, height);
            }
            Dispatcher.Run(); // message loop — blocks until InvokeShutdown()
        });
        _uiThread.SetApartmentState(ApartmentState.STA);
        _uiThread.IsBackground = true;
        _uiThread.Name = mode == ZoomMode.HighlightBox ? "InputHL-STA" : "InputZoom-STA";
        _uiThread.Start();
        _ready.Wait(3000);
    }

    /// <summary>
    /// Force exact physical pixel position via Win32 SetWindowPos.
    /// WPF Window.Left/Top use device-independent pixels (DIPs) which differ from
    /// physical pixels on high-DPI monitors, causing zoom overlays to appear offset.
    /// This bypasses WPF's DPI conversion and positions at exact screen coordinates.
    /// </summary>
    private static void ForceWindowPosition(Window window, int x, int y, int w, int h)
    {
        try
        {
            var helper = new System.Windows.Interop.WindowInteropHelper(window);
            if (helper.Handle != IntPtr.Zero)
            {
                SetWindowPos(helper.Handle, HWND_TOPMOST, x, y, w, h, SWP_NOACTIVATE);
            }
        }
        catch { /* best effort — WPF Left/Top is fallback */ }
    }

    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private const uint SWP_NOACTIVATE = 0x0010;
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, uint uFlags);

    /// <summary>Push a new control capture frame (Magnifier/Relay only).</summary>
    public void UpdateImage(byte[] pngData)
    {
        if (Mode == ZoomMode.HighlightBox) return; // no image in highlight mode
        _dispatcher?.BeginInvoke(() => _zoomWindow?.UpdateImage(pngData));
    }

    /// <summary>Update the header label (Magnifier/Relay only).</summary>
    public void UpdateHeader(string text)
    {
        if (Mode == ZoomMode.HighlightBox) return;
        _dispatcher?.BeginInvoke(() => _zoomWindow?.UpdateHeader(text));
    }

    /// <summary>Update the status line.</summary>
    public void UpdateStatus(string text)
    {
        if (Mode == ZoomMode.HighlightBox)
            _dispatcher?.BeginInvoke(() => _highlightWindow?.UpdateStatus(text));
        else
            _dispatcher?.BeginInvoke(() => _zoomWindow?.UpdateStatus(text));
    }

    /// <summary>Show pass/fail result with colored border.</summary>
    public void ShowResult(bool pass, string text)
    {
        if (Mode == ZoomMode.HighlightBox)
            _dispatcher?.BeginInvoke(() => _highlightWindow?.ShowResult(pass, text));
        else
            _dispatcher?.BeginInvoke(() => _zoomWindow?.ShowResult(pass, text));
    }

    /// <summary>
    /// Start periodic live capture (Magnifier/Relay: image refresh + TOPMOST,
    /// HighlightBox: TOPMOST only). Interval default 500ms.
    /// </summary>
    private int _tickCount;
    public void StartLiveCapture(Func<byte[]?>? captureFunc = null, int intervalMs = 200)
    {
        _captureFunc = captureFunc;
        _tickCount = 0;
        bool _capturing = false;
        _dispatcher?.BeginInvoke(() =>
        {
            _refreshTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(intervalMs) };
            _refreshTimer.Tick += (_, _) =>
            {
                _tickCount++;
                try
                {
                    if (Mode != ZoomMode.HighlightBox && _captureFunc != null)
                    {
                        if (_capturing) return; // skip if previous capture still running
                        _capturing = true;
                        var func = _captureFunc; // local copy for closure safety
                        // Capture on ThreadPool (off UI thread!) → push result to WPF
                        System.Threading.ThreadPool.QueueUserWorkItem(_ =>
                        {
                            try
                            {
                                var png = func?.Invoke();
                                if (png != null)
                                    _dispatcher?.BeginInvoke(() =>
                                    {
                                        _zoomWindow?.UpdateImage(png);
                                        _zoomWindow?.EnsureTopmost();
                                    });
                            }
                            catch { /* best effort */ }
                            finally { _capturing = false; }
                        });
                    }
                    else
                    {
                        _highlightWindow?.EnsureTopmost();
                    }
                }
                catch { /* best effort */ }
            };
            _refreshTimer.Start();
        });
    }
    /// <summary>Number of timer ticks fired (for diagnostics).</summary>
    public int TickCount => _tickCount;

    /// <summary>Stop the periodic live capture timer.</summary>
    public void StopLiveCapture()
    {
        _dispatcher?.BeginInvoke(() => { _refreshTimer?.Stop(); _refreshTimer = null; });
    }

    /// <summary>Force Win32 TOPMOST on the overlay window.</summary>
    public void EnsureTopmost()
    {
        if (Mode == ZoomMode.HighlightBox)
            _dispatcher?.BeginInvoke(() => _highlightWindow?.EnsureTopmost());
        else
            _dispatcher?.BeginInvoke(() => _zoomWindow?.EnsureTopmost());
    }

    /// <summary>
    /// Close all InputZoom/InputHighlight overlay windows from OTHER processes.
    /// Call at startup to kill ghost zooms left by previous invocations that died mid-fade.
    /// Uses FindWindowExW (kernel title index, no messaging) — fast and non-blocking.
    /// </summary>
    public static void CloseAllGhosts()
    {
        int myPid = Environment.ProcessId;
        foreach (var title in new[] { "InputZoom", "InputHighlight" })
        {
            var hWnd = IntPtr.Zero;
            while (true)
            {
                hWnd = WKAppBot.Win32.Native.NativeMethods.FindWindowExW(IntPtr.Zero, hWnd, null, title);
                if (hWnd == IntPtr.Zero) break;
                try
                {
                    WKAppBot.Win32.Native.NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
                    if ((int)pid != myPid)
                        WKAppBot.Win32.Native.NativeMethods.PostMessageW(
                            hWnd, WKAppBot.Win32.Native.NativeMethods.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }
                catch { }
            }
        }
    }

    /// <summary>Begin fade-out animation, then auto-close.
    /// Promotes thread to foreground so the overlay survives after main thread exits.</summary>
    public void BeginFadeOut(int delayMs = 3000, int fadeDurationMs = 800)
    {
        // Promote to foreground thread — keeps process alive until fade completes.
        // Without this, IsBackground=true kills the overlay when main thread exits.
        if (_uiThread != null) try { _uiThread.IsBackground = false; } catch { }

        if (Mode == ZoomMode.HighlightBox)
            _dispatcher?.BeginInvoke(() => _highlightWindow?.BeginFadeOut(
                Math.Min(delayMs, 2000), Math.Min(fadeDurationMs, 600)));
        else
            _dispatcher?.BeginInvoke(() => _zoomWindow?.BeginFadeOut(delayMs, fadeDurationMs));
    }

    public void Dispose()
    {
        try
        {
            _dispatcher?.BeginInvoke(() => { _refreshTimer?.Stop(); _refreshTimer = null; });
            _dispatcher?.InvokeShutdown();
            _uiThread?.Join(3000);
        }
        catch { /* best effort */ }
        _ready.Dispose();
    }
}
