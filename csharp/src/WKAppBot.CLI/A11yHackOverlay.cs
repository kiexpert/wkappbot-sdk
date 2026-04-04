using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WKAppBot.CLI;

internal sealed record A11yHackOverlayBox(Rect Bounds, Color Color, string? Label = null, double Thickness = 2.0);
internal sealed record A11yHackOverlayPreview(BitmapSource Image, double Width, double Height, string? Header = null);
internal sealed record A11yHackOverlayModel(IReadOnlyList<A11yHackOverlayBox> Boxes, A11yHackOverlayPreview? Preview = null);

internal sealed class A11yHackOverlayWindow : Window
{
    readonly Canvas _canvas;
    Rectangle? _hoverRect;  // single reusable hover highlight — no re-render needed

    public A11yHackOverlayWindow(int width, int height)
    {
        Title = "A11yHackOverlay";
        Width = width;
        Height = height;
        WindowStyle = WindowStyle.None;
        AllowsTransparency = true;
        Background = Brushes.Transparent;
        Topmost = true;
        ShowInTaskbar = false;
        ShowActivated = false;
        ResizeMode = ResizeMode.NoResize;
        Opacity = 0.92;
        _canvas = new Canvas { Background = Brushes.Transparent, IsHitTestVisible = false };
        Content = _canvas;
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        var helper = new WindowInteropHelper(this);
        var exStyle = GetWindowLongPtr(helper.Handle, GWL_EXSTYLE);
        SetWindowLongPtr(helper.Handle, GWL_EXSTYLE, new IntPtr(exStyle.ToInt64() | WS_EX_NOACTIVATE | WS_EX_TRANSPARENT));
    }

    /// <summary>Move/show/hide the hover highlight without re-rendering the full canvas.</summary>
    public void SetHover(A11yHackOverlayModel model, int hoverIndex)
    {
        if (hoverIndex < 0 || hoverIndex >= model.Boxes.Count)
        {
            // Hide hover
            if (_hoverRect != null) _hoverRect.Visibility = Visibility.Collapsed;
            return;
        }
        var box = model.Boxes[hoverIndex];
        if (_hoverRect == null)
        {
            _hoverRect = new Rectangle
            {
                Stroke = Brushes.White,
                StrokeThickness = 3,
                IsHitTestVisible = false,
                RadiusX = 0, RadiusY = 0,
            };
            _canvas.Children.Add(_hoverRect);
        }
        _hoverRect.Width = Math.Max(1, box.Bounds.Width);
        _hoverRect.Height = Math.Max(1, box.Bounds.Height);
        _hoverRect.Fill = new SolidColorBrush(Color.FromArgb(48, box.Color.R, box.Color.G, box.Color.B));
        _hoverRect.Stroke = new SolidColorBrush(Colors.White);
        Canvas.SetLeft(_hoverRect, box.Bounds.X);
        Canvas.SetTop(_hoverRect, box.Bounds.Y);
        _hoverRect.Visibility = Visibility.Visible;
    }

    public void Render(A11yHackOverlayModel model, int hoverIndex = -1)
    {
        _canvas.Children.Clear();
        _hoverRect = null; // cleared with canvas

        var labelRows = new List<Border>();
        for (int i = 0; i < model.Boxes.Count; i++)
        {
            var box = model.Boxes[i];
            bool isTarget = i == 0;
            var stroke = new SolidColorBrush(box.Color);
            var rect = new Rectangle
            {
                Width = Math.Max(1, box.Bounds.Width),
                Height = Math.Max(1, box.Bounds.Height),
                Stroke = stroke,
                StrokeThickness = isTarget ? Math.Max(1.2, box.Thickness + 0.3) : box.Thickness,
                Fill = Brushes.Transparent,
                RadiusX = 0,
                RadiusY = 0,
                StrokeDashArray = isTarget ? null : new DoubleCollection { 4, 2 },
                Effect = isTarget
                    ? new DropShadowEffect
                    {
                        Color = box.Color,
                        BlurRadius = 8,
                        ShadowDepth = 0,
                        Opacity = 0.6
                    }
                    : new DropShadowEffect
                    {
                        Color = box.Color,
                        BlurRadius = 5,
                        ShadowDepth = 0,
                        Opacity = 0.25
                    },
                IsHitTestVisible = false
            };
            Canvas.SetLeft(rect, box.Bounds.X);
            Canvas.SetTop(rect, box.Bounds.Y);
            _canvas.Children.Add(rect);

            if (!string.IsNullOrWhiteSpace(box.Label))
            {
                var labelBg = new Border
                {
                    Background = new SolidColorBrush(isTarget
                        ? Color.FromArgb(220, 7, 26, 24)
                        : Color.FromArgb(188, 8, 10, 14)),
                    BorderBrush = stroke,
                    BorderThickness = new Thickness(isTarget ? 1.5 : 1),
                    CornerRadius = new CornerRadius(isTarget ? 5 : 3),
                    Padding = new Thickness(isTarget ? 6 : 4, 2, isTarget ? 6 : 4, 2),
                    Effect = new DropShadowEffect
                    {
                        Color = box.Color,
                        BlurRadius = isTarget ? 12 : 6,
                        ShadowDepth = 0,
                        Opacity = isTarget ? 0.75 : 0.35
                    },
                    Child = new TextBlock
                    {
                        Text = box.Label,
                        Foreground = isTarget ? Brushes.White : Brushes.Gainsboro,
                        FontFamily = new FontFamily("Consolas"),
                        FontSize = isTarget ? 10.5 : 10,
                        FontWeight = isTarget ? FontWeights.Bold : FontWeights.SemiBold
                    },
                    IsHitTestVisible = false
                };
                labelRows.Add(labelBg);
            }
        }

        const double left = 8;
        double top = 8;
        foreach (var row in labelRows)
        {
            row.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            var desired = row.DesiredSize;
            Canvas.SetLeft(row, left);
            Canvas.SetTop(row, top);
            _canvas.Children.Add(row);
            top += desired.Height + 4;
        }
    }

    // Kept for later reuse: the callout/preview panel can be re-enabled without
    // rebuilding the visual design. The default renderer intentionally stays box-only.
    void RenderPreview(A11yHackOverlayPreview preview)
    {
        var panel = new Border
        {
            Width = preview.Width,
            Height = preview.Height,
            Background = new SolidColorBrush(Color.FromArgb(224, 7, 12, 16)),
            BorderBrush = new LinearGradientBrush(
                Color.FromRgb(0x4D, 0xF3, 0xC4),
                Color.FromRgb(0x42, 0xA5, 0xF5),
                0),
            BorderThickness = new Thickness(1.6),
            CornerRadius = new CornerRadius(8),
            Padding = new Thickness(6),
            Effect = new DropShadowEffect
            {
                Color = Color.FromRgb(0x22, 0xD3, 0xEE),
                BlurRadius = 18,
                ShadowDepth = 0,
                Opacity = 0.45
            },
            IsHitTestVisible = false
        };

        var stack = new StackPanel
        {
            Orientation = Orientation.Vertical,
            IsHitTestVisible = false
        };

        if (!string.IsNullOrWhiteSpace(preview.Header))
        {
            stack.Children.Add(new TextBlock
            {
                Text = preview.Header,
                Foreground = Brushes.Aquamarine,
                FontFamily = new FontFamily("Consolas"),
                FontWeight = FontWeights.Bold,
                FontSize = 10.5,
                Margin = new Thickness(0, 0, 0, 4)
            });
        }

        stack.Children.Add(new Image
        {
            Source = preview.Image,
            Width = preview.Width - 8,
            Height = preview.Height - 24,
            Stretch = Stretch.Fill,
            SnapsToDevicePixels = true,
            Effect = new DropShadowEffect
            {
                Color = Colors.Black,
                BlurRadius = 6,
                ShadowDepth = 0,
                Opacity = 0.25
            },
            IsHitTestVisible = false
        });

        panel.Child = stack;
        Canvas.SetLeft(panel, 8);
        Canvas.SetTop(panel, 8);
        _canvas.Children.Add(panel);
    }

    const int GWL_EXSTYLE = -20;
    const int WS_EX_NOACTIVATE = 0x08000000;
    const int WS_EX_TRANSPARENT = 0x00000020;

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
    static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
}

internal sealed class A11yHackOverlayHost : IDisposable
{
    Thread? _uiThread;
    Dispatcher? _dispatcher;
    A11yHackOverlayWindow? _window;
    readonly ManualResetEventSlim _ready = new();

    public static A11yHackOverlayHost? TryStart(int screenX, int screenY, int width, int height)
    {
        try
        {
            var host = new A11yHackOverlayHost();
            host.Start(screenX, screenY, width, height);
            return host;
        }
        catch
        {
            return null;
        }
    }

    void Start(int screenX, int screenY, int width, int height)
    {
        _uiThread = new Thread(() =>
        {
            _window = new A11yHackOverlayWindow(width, height)
            {
                Left = screenX,
                Top = screenY
            };
            _dispatcher = Dispatcher.CurrentDispatcher;
            _ready.Set();
            _window.Show();
            ForceWindowPosition(_window, screenX, screenY, width, height);
            Dispatcher.Run();
        });
        _uiThread.SetApartmentState(ApartmentState.STA);
        _uiThread.IsBackground = true;
        _uiThread.Name = "A11yHackOverlay-STA";
        _uiThread.Start();
        _ready.Wait(3000);
    }

    A11yHackOverlayModel? _lastModel;
    int _screenX, _screenY;
    DispatcherTimer? _hoverTimer;

    public void Update(A11yHackOverlayModel model)
    {
        _lastModel = model;
        _dispatcher?.BeginInvoke(() => _window?.Render(model));
    }

    public void Update(IReadOnlyList<A11yHackOverlayBox> boxes)
    {
        Update(new A11yHackOverlayModel(boxes));
    }

    /// <summary>
    /// Start mouse hover tracking: polls cursor position at ~60fps,
    /// highlights the segment under the cursor with a bright fill.
    /// </summary>
    public void StartHoverTracking(int screenX, int screenY)
    {
        _screenX = screenX;
        _screenY = screenY;
        _dispatcher?.BeginInvoke(() =>
        {
            _hoverTimer = new DispatcherTimer(DispatcherPriority.Input)
            {
                Interval = TimeSpan.FromMilliseconds(16)
            };
            int lastHoverIdx = -1;
            POINT lastPt = default;
            _hoverTimer.Tick += (_, _) =>
            {
                if (_lastModel == null || _window == null) return;
                GetCursorPos(out var pt);

                // Skip rendering entirely while user is actively typing/moving
                // (only show hover after 200ms idle — instant feel without interrupting work)
                var idleMs = WKAppBot.Win32.Native.NativeMethods.GetUserIdleMs();
                if (idleMs < 200)
                {
                    if (lastHoverIdx >= 0) { lastHoverIdx = -1; _window.SetHover(_lastModel, -1); }
                    lastPt = pt;
                    return;
                }

                // No cursor movement since last check → skip
                if (pt.X == lastPt.X && pt.Y == lastPt.Y && lastHoverIdx >= 0) return;
                lastPt = pt;

                double localX = pt.X - _screenX;
                double localY = pt.Y - _screenY;

                int hoverIdx = -1;
                for (int i = _lastModel.Boxes.Count - 1; i >= 0; i--)
                {
                    var b = _lastModel.Boxes[i].Bounds;
                    if (localX >= b.X && localX <= b.X + b.Width &&
                        localY >= b.Y && localY <= b.Y + b.Height)
                    { hoverIdx = i; break; }
                }

                if (hoverIdx == lastHoverIdx) return;
                lastHoverIdx = hoverIdx;
                _window.SetHover(_lastModel, hoverIdx);
            };
            _hoverTimer.Start();
        });
    }

    public void StopHoverTracking()
    {
        _dispatcher?.BeginInvoke(() => { _hoverTimer?.Stop(); _hoverTimer = null; });
    }

    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    struct POINT { public int X, Y; }

    public void Hide()
    {
        _dispatcher?.BeginInvoke(() => { if (_window != null) _window.Visibility = Visibility.Collapsed; });
    }

    public void Show()
    {
        _dispatcher?.BeginInvoke(() => { if (_window != null) _window.Visibility = Visibility.Visible; });
    }

    public void Dispose()
    {
        try
        {
            StopHoverTracking();
            _dispatcher?.InvokeShutdown();
            _uiThread?.Join(1500);
        }
        catch { }
        _ready.Dispose();
    }

    static void ForceWindowPosition(Window window, int x, int y, int w, int h)
    {
        try
        {
            var helper = new WindowInteropHelper(window);
            if (helper.Handle != IntPtr.Zero)
                SetWindowPos(helper.Handle, HWND_TOPMOST, x, y, w, h, SWP_NOACTIVATE);
        }
        catch { }
    }

    static readonly IntPtr HWND_TOPMOST = new(-1);
    const uint SWP_NOACTIVATE = 0x0010;

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
}
