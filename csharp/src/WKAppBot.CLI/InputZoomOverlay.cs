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
/// "Input Zoom Overlay" — semi-transparent WPF magnifier that shows a 2x enlarged
/// live view of the target control during input automation.
/// Positioned directly on top of the control, like a magnifying glass.
///
/// Design: "돋보기 중계방송" — see what the bot types in real-time!
/// Tag: [ZOOM]
/// </summary>
internal sealed class InputZoomWindow : Window
{
    private readonly Image _controlImage;
    private readonly TextBlock _headerText;
    private readonly TextBlock _statusText;
    private readonly Border _outerBorder;

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
        Opacity = 0.98; // nearly opaque — input text must be clearly visible

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

    /// <summary>Update the magnified control image from PNG data.</summary>
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

    // ── Win32 P/Invoke ──
    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_NOACTIVATE = 0x08000000;

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
}

/// <summary>
/// Manages the STA thread for the InputZoom WPF overlay window.
/// Bridge between CLI main thread and WPF Dispatcher thread.
/// Short-lived: created before input, disposed after input completes.
/// </summary>
internal sealed class InputZoomHost : IDisposable
{
    private Thread? _uiThread;
    private InputZoomWindow? _window;
    private Dispatcher? _dispatcher;
    private readonly ManualResetEventSlim _ready = new();

    public bool IsAlive => _uiThread?.IsAlive == true;

    /// <summary>Start the zoom overlay on a dedicated STA thread.</summary>
    public void Start(int screenX, int screenY, int width, int height)
    {
        _uiThread = new Thread(() =>
        {
            _window = new InputZoomWindow(width, height);
            _window.Left = screenX;
            _window.Top = screenY;

            _dispatcher = Dispatcher.CurrentDispatcher;
            _ready.Set();
            _window.Show();
            Dispatcher.Run(); // message loop — blocks until InvokeShutdown()
        });
        _uiThread.SetApartmentState(ApartmentState.STA);
        _uiThread.IsBackground = true;
        _uiThread.Name = "InputZoom-STA";
        _uiThread.Start();
        _ready.Wait(3000);
    }

    /// <summary>Push a new control capture frame.</summary>
    public void UpdateImage(byte[] pngData)
    {
        _dispatcher?.BeginInvoke(() => _window?.UpdateImage(pngData));
    }

    /// <summary>Update the header label.</summary>
    public void UpdateHeader(string text)
    {
        _dispatcher?.BeginInvoke(() => _window?.UpdateHeader(text));
    }

    /// <summary>Update the status line.</summary>
    public void UpdateStatus(string text)
    {
        _dispatcher?.BeginInvoke(() => _window?.UpdateStatus(text));
    }

    /// <summary>Show pass/fail result with colored border.</summary>
    public void ShowResult(bool pass, string text)
    {
        _dispatcher?.BeginInvoke(() => _window?.ShowResult(pass, text));
    }

    /// <summary>Begin fade-out animation, then auto-close.</summary>
    public void BeginFadeOut(int delayMs = 3000, int fadeDurationMs = 800)
    {
        _dispatcher?.BeginInvoke(() => _window?.BeginFadeOut(delayMs, fadeDurationMs));
    }

    public void Dispose()
    {
        try
        {
            _dispatcher?.InvokeShutdown();
            _uiThread?.Join(3000);
        }
        catch { /* best effort */ }
        _ready.Dispose();
    }
}
