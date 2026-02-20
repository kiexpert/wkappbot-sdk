using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WKAppBot.CLI;

/// <summary>
/// "Claude's Eye" — semi-transparent WPF overlay that shows what the WebBot sees.
/// Programmatic UI (no XAML). Runs on a dedicated STA thread.
///
/// Features:
/// - Semi-transparent dark overlay with cyan border
/// - CDP screenshot as full background (behind text)
/// - Accessibility text overlay with larger font + auto-scroll animation
/// - Click image → restore Chrome window
/// - Cloaking: fades out after inactivity, fades in on new content
/// - Drag to move, always on top, never steals focus
/// </summary>
internal sealed class MiniViewOverlay : Window
{
    private readonly Image _image;
    private readonly TextBlock _urlText;
    private readonly TextBlock _timeText;
    private readonly TextBlock _axText; // accessibility text display (overlay on image)
    private readonly Canvas _scrollCanvas; // canvas for scroll animation
    private readonly Ellipse _dot;
    private DateTime _lastUpdate = DateTime.MinValue;
    private DispatcherTimer? _cloakTimer;
    private DispatcherTimer? _scrollTimer; // auto-scroll timer
    private bool _isCloaked;
    private IntPtr _chromeHwnd;
    private double _scrollOffset; // current scroll Y offset
    private bool _needsScroll; // text exceeds visible area

    // Cloaking thresholds
    private const double ActiveOpacity = 0.85;
    private const double DimOpacity = 0.3;
    private const int DimAfterSeconds = 5;
    private const int CloakAfterSeconds = 10;

    // Scroll settings
    private const double ScrollSpeed = 0.5; // pixels per tick
    private const double ScrollPauseTop = 60; // ticks to pause at top
    private const double ScrollPauseBottom = 120; // ticks to pause at bottom
    private double _scrollPauseCount;
    private bool _scrollingDown = true;

    public MiniViewOverlay(int width = 320, int height = 220)
    {
        // Window properties — semi-transparent overlay
        Title = "Claude's Eye";
        Width = width;
        Height = height;
        WindowStyle = WindowStyle.None;
        AllowsTransparency = true;
        Background = Brushes.Transparent;
        Topmost = true;
        ShowInTaskbar = false;
        ShowActivated = false;
        ResizeMode = ResizeMode.NoResize;
        Opacity = ActiveOpacity;

        // Build visual tree programmatically (no XAML needed)
        var root = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(0xDD, 0x1A, 0x1A, 0x2E)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(0x4F, 0xC3, 0xF7)), // cyan
            BorderThickness = new Thickness(1.5),
            CornerRadius = new CornerRadius(6),
            Padding = new Thickness(0),
            ClipToBounds = true,
        };

        var mainGrid = new Grid();
        mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(22) });  // header
        mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // content area
        mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(18) });  // footer

        // ── Header: [●] Claude's Eye  [✕] ──
        var header = new DockPanel { Margin = new Thickness(6, 2, 6, 0) };
        header.MouseLeftButtonDown += (_, _) => DragMove(); // drag to move

        _dot = new Ellipse
        {
            Width = 8, Height = 8,
            Fill = new SolidColorBrush(Color.FromRgb(0x4C, 0xAF, 0x50)), // green
            Margin = new Thickness(0, 0, 5, 0),
            VerticalAlignment = VerticalAlignment.Center,
        };
        DockPanel.SetDock(_dot, Dock.Left);

        var titleText = new TextBlock
        {
            Text = "Claude's Eye",
            Foreground = new SolidColorBrush(Color.FromRgb(0x4F, 0xC3, 0xF7)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 11,
            FontWeight = FontWeights.Bold,
            VerticalAlignment = VerticalAlignment.Center,
        };
        DockPanel.SetDock(titleText, Dock.Left);

        var closeBtn = new TextBlock
        {
            Text = " \u2715 ", // ✕
            Foreground = new SolidColorBrush(Color.FromRgb(0x88, 0x88, 0x88)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 12,
            VerticalAlignment = VerticalAlignment.Center,
            Cursor = Cursors.Hand,
        };
        closeBtn.MouseEnter += (_, _) => closeBtn.Foreground = Brushes.White;
        closeBtn.MouseLeave += (_, _) => closeBtn.Foreground = new SolidColorBrush(Color.FromRgb(0x88, 0x88, 0x88));
        closeBtn.MouseLeftButtonDown += (_, _) => Dispatcher.InvokeShutdown();
        DockPanel.SetDock(closeBtn, Dock.Right);

        header.Children.Add(_dot);
        header.Children.Add(titleText);
        header.Children.Add(closeBtn);
        Grid.SetRow(header, 0);

        // ── Content area: Background image + text overlay ──
        var contentGrid = new Grid();

        // Background: CDP screenshot (fills entire content area, dimmed)
        _image = new Image
        {
            Stretch = Stretch.Uniform,
            Opacity = 0.35, // dimmed background — text is the star
            Margin = new Thickness(2),
            Cursor = Cursors.Hand,
        };
        _image.MouseLeftButtonDown += OnImageClick;
        contentGrid.Children.Add(_image);

        // Foreground: Accessibility text overlay with semi-transparent dark backdrop
        var textOverlay = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(0x88, 0x10, 0x10, 0x20)), // darker backdrop
            CornerRadius = new CornerRadius(4),
            Margin = new Thickness(6, 4, 6, 4),
            Padding = new Thickness(6, 4, 6, 4),
            ClipToBounds = true,
            Cursor = Cursors.Hand,
        };
        textOverlay.MouseLeftButtonDown += OnImageClick;

        // Canvas for scrolling text (clips to parent border)
        _scrollCanvas = new Canvas { ClipToBounds = true };

        _axText = new TextBlock
        {
            Text = "connecting...",
            Foreground = new SolidColorBrush(Color.FromRgb(0x80, 0xFF, 0x80)), // light green
            FontFamily = new FontFamily("Consolas"),
            FontSize = 13, // bigger text for readability
            TextWrapping = TextWrapping.Wrap,
            LineHeight = 18,
        };
        Canvas.SetLeft(_axText, 0);
        Canvas.SetTop(_axText, 0);
        _scrollCanvas.Children.Add(_axText);
        textOverlay.Child = _scrollCanvas;
        contentGrid.Children.Add(textOverlay);

        Grid.SetRow(contentGrid, 1);

        // ── Footer: URL + timestamp ──
        var footer = new DockPanel { Margin = new Thickness(6, 0, 6, 2) };

        _timeText = new TextBlock
        {
            Text = "",
            Foreground = new SolidColorBrush(Color.FromRgb(0x66, 0x66, 0x66)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 9,
            VerticalAlignment = VerticalAlignment.Center,
        };
        DockPanel.SetDock(_timeText, Dock.Right);

        _urlText = new TextBlock
        {
            Text = "connecting...",
            Foreground = new SolidColorBrush(Color.FromRgb(0x88, 0x88, 0x88)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 9,
            TextTrimming = TextTrimming.CharacterEllipsis,
            VerticalAlignment = VerticalAlignment.Center,
        };

        footer.Children.Add(_timeText);
        footer.Children.Add(_urlText);
        Grid.SetRow(footer, 2);

        mainGrid.Children.Add(header);
        mainGrid.Children.Add(contentGrid);
        mainGrid.Children.Add(footer);
        root.Child = mainGrid;
        Content = root;

        // Wire up canvas size change to update text width
        _scrollCanvas.SizeChanged += (_, args) =>
        {
            if (args.NewSize.Width > 0)
                _axText.Width = args.NewSize.Width;
        };

        // Start cloaking timer + scroll timer
        SetupCloakTimer();
        SetupScrollTimer();
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        // Set WS_EX_NOACTIVATE so clicking the overlay doesn't steal focus from other windows
        var helper = new WindowInteropHelper(this);
        var exStyle = GetWindowLongPtr(helper.Handle, GWL_EXSTYLE);
        SetWindowLongPtr(helper.Handle, GWL_EXSTYLE, new IntPtr(exStyle.ToInt64() | WS_EX_NOACTIVATE));
    }

    /// <summary>Update the screenshot thumbnail from CDP PNG data.</summary>
    /// <remarks>Does NOT reset _lastUpdate — screenshots arrive continuously while WebBot is alive.
    /// Cloaking is based on content changes (URL/text), not screenshot freshness.</remarks>
    public void UpdateScreenshot(byte[] pngData)
    {
        try
        {
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(pngData);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.DecodePixelWidth = (int)Width; // scale down for performance
            bi.EndInit();
            bi.Freeze(); // thread-safe!
            _image.Source = bi;
            // Note: no _lastUpdate here — cloaking tracks content changes, not screenshot refresh
        }
        catch { /* best effort */ }
    }

    /// <summary>Update footer info (URL + domain display).</summary>
    public void UpdateInfo(string? url, string? title)
    {
        try
        {
            if (url != null)
            {
                // Show domain only for compact display
                if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
                    _urlText.Text = uri.Host;
                else
                    _urlText.Text = url.Length > 40 ? url[..37] + "..." : url;
            }
            _lastUpdate = DateTime.Now;
        }
        catch { /* best effort */ }
    }

    /// <summary>Update the accessibility text display with scroll reset.</summary>
    public void UpdateAccessibilityText(string text)
    {
        try
        {
            if (_axText.Text == text) return; // no change — don't reset scroll

            _axText.Text = text;
            _lastUpdate = DateTime.Now;

            // Reset scroll to top when text changes
            _scrollOffset = 0;
            _scrollPauseCount = ScrollPauseTop; // brief pause at top before scrolling
            _scrollingDown = true;
            Canvas.SetTop(_axText, 0);

            // Measure text to determine if scrolling is needed
            _axText.Measure(new Size(_scrollCanvas.ActualWidth > 0 ? _scrollCanvas.ActualWidth : Width - 30, double.PositiveInfinity));
            _needsScroll = _axText.DesiredSize.Height > (_scrollCanvas.ActualHeight > 0 ? _scrollCanvas.ActualHeight : 120);

            // Set text width to match canvas for proper wrapping
            if (_scrollCanvas.ActualWidth > 0)
                _axText.Width = _scrollCanvas.ActualWidth;

            if (_isCloaked) Uncloak();
        }
        catch { /* best effort */ }
    }

    /// <summary>Set the Chrome window handle for click-to-restore.</summary>
    public void SetChromeHwnd(IntPtr hwnd)
    {
        _chromeHwnd = hwnd;
    }

    // ── Click image → restore Chrome window ──

    private void OnImageClick(object sender, MouseButtonEventArgs e)
    {
        if (_chromeHwnd != IntPtr.Zero && IsWindow(_chromeHwnd))
        {
            ShowWindow(_chromeHwnd, SW_RESTORE);
            SetForegroundWindow(_chromeHwnd);
        }
    }

    // ── Cloaking (fade out/in) ──

    private void SetupCloakTimer()
    {
        _cloakTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _cloakTimer.Tick += OnCloakTick;
        _cloakTimer.Start();
    }

    private void SetupScrollTimer()
    {
        _scrollTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(33) }; // ~30fps
        _scrollTimer.Tick += OnScrollTick;
        _scrollTimer.Start();
    }

    private void OnScrollTick(object? sender, EventArgs e)
    {
        if (!_needsScroll) return;

        var canvasHeight = _scrollCanvas.ActualHeight;
        if (canvasHeight <= 0) return;

        // Measure actual text height
        _axText.Measure(new Size(_scrollCanvas.ActualWidth > 0 ? _scrollCanvas.ActualWidth : Width - 30, double.PositiveInfinity));
        var textHeight = _axText.DesiredSize.Height;
        if (textHeight <= canvasHeight)
        {
            _needsScroll = false;
            Canvas.SetTop(_axText, 0);
            return;
        }

        var maxScroll = textHeight - canvasHeight;

        // Handle pause at top/bottom
        if (_scrollPauseCount > 0)
        {
            _scrollPauseCount--;
            return;
        }

        // Scroll
        if (_scrollingDown)
        {
            _scrollOffset += ScrollSpeed;
            if (_scrollOffset >= maxScroll)
            {
                _scrollOffset = maxScroll;
                _scrollingDown = false;
                _scrollPauseCount = ScrollPauseBottom; // longer pause at bottom
            }
        }
        else
        {
            _scrollOffset -= ScrollSpeed * 2; // scroll back up faster
            if (_scrollOffset <= 0)
            {
                _scrollOffset = 0;
                _scrollingDown = true;
                _scrollPauseCount = ScrollPauseTop;
            }
        }

        Canvas.SetTop(_axText, -_scrollOffset);
    }

    private void OnCloakTick(object? sender, EventArgs e)
    {
        if (_lastUpdate == DateTime.MinValue) return; // no content yet

        var elapsed = (DateTime.Now - _lastUpdate).TotalSeconds;

        if (elapsed >= CloakAfterSeconds && !_isCloaked)
        {
            // Full cloak — fade to invisible
            var anim = new DoubleAnimation(Opacity, 0.0, TimeSpan.FromSeconds(3));
            anim.Completed += (_, _) =>
            {
                _isCloaked = true;
                Visibility = Visibility.Hidden;
            };
            BeginAnimation(OpacityProperty, anim);
        }
        else if (elapsed >= DimAfterSeconds && elapsed < CloakAfterSeconds && Opacity > DimOpacity + 0.05)
        {
            // Dim — reduce opacity
            var anim = new DoubleAnimation(Opacity, DimOpacity, TimeSpan.FromSeconds(2));
            BeginAnimation(OpacityProperty, anim);
        }
    }

    private void Uncloak()
    {
        _isCloaked = false;
        Visibility = Visibility.Visible;
        BeginAnimation(OpacityProperty, null); // stop any running animation
        var anim = new DoubleAnimation(Opacity, ActiveOpacity, TimeSpan.FromSeconds(0.5));
        BeginAnimation(OpacityProperty, anim);
    }

    // ── Win32 P/Invoke ──

    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_NOACTIVATE = 0x08000000;
    private const int SW_RESTORE = 9;

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool IsWindow(IntPtr hWnd);
}

/// <summary>
/// Manages the STA thread for the MiniView WPF overlay window.
/// Bridge between CLI main thread and WPF Dispatcher thread.
/// When ownerHwnd is set, the overlay follows its parent window (e.g. Claude Desktop).
/// </summary>
internal sealed class MiniViewHost : IDisposable
{
    private Thread? _uiThread;
    private MiniViewOverlay? _window;
    private Dispatcher? _dispatcher;
    private readonly ManualResetEventSlim _ready = new();

    public bool IsAlive => _uiThread?.IsAlive == true;

    /// <summary>Start the overlay window on a dedicated STA thread.</summary>
    /// <param name="ownerHwnd">Optional parent window handle (e.g. Claude Desktop) — overlay follows it.</param>
    public void Start(int width = 320, int height = 220, int x = -1, int y = -1, IntPtr ownerHwnd = default)
    {
        _uiThread = new Thread(() =>
        {
            _window = new MiniViewOverlay(width, height);

            // Position: default to bottom-right, above the prompt area
            if (x >= 0 && y >= 0)
            {
                _window.Left = x;
                _window.Top = y;
            }
            else
            {
                // Place above the terminal/prompt area (bottom-right, offset up)
                var screen = SystemParameters.WorkArea;
                _window.Left = screen.Right - width - 10;
                _window.Top = screen.Bottom - height - 80; // above taskbar + prompt
            }

            // Set owner window — overlay follows Claude Desktop (minimize/restore together)
            if (ownerHwnd != IntPtr.Zero)
            {
                try
                {
                    var helper = new WindowInteropHelper(_window);
                    helper.Owner = ownerHwnd;
                    // Owner window means: overlay shows on top of Claude Desktop,
                    // minimizes/restores with it, but retains its own Topmost + transparency
                }
                catch { /* best effort — continue without owner */ }
            }

            _dispatcher = Dispatcher.CurrentDispatcher;
            _ready.Set();
            _window.Show();
            Dispatcher.Run(); // message loop — blocks until InvokeShutdown()
        });
        _uiThread.SetApartmentState(ApartmentState.STA);
        _uiThread.IsBackground = true;
        _uiThread.Name = "MiniView-STA";
        _uiThread.Start();
        _ready.Wait(5000);
    }

    /// <summary>Push a new CDP screenshot to the overlay.</summary>
    public void UpdateScreenshot(byte[] pngData)
    {
        _dispatcher?.BeginInvoke(() => _window?.UpdateScreenshot(pngData));
    }

    /// <summary>Update URL/title info in the footer.</summary>
    public void UpdateInfo(string? url, string? title)
    {
        _dispatcher?.BeginInvoke(() => _window?.UpdateInfo(url, title));
    }

    /// <summary>Update accessibility text in the overlay.</summary>
    public void UpdateAccessibilityText(string text)
    {
        _dispatcher?.BeginInvoke(() => _window?.UpdateAccessibilityText(text));
    }

    /// <summary>Set the Chrome window handle for click-to-restore.</summary>
    public void SetChromeHwnd(IntPtr hwnd)
    {
        _dispatcher?.BeginInvoke(() => _window?.SetChromeHwnd(hwnd));
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
