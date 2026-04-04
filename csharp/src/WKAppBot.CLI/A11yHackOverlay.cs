using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WKAppBot.CLI;

/// <summary>
/// Role: Target=neon green, Scope=semi-transparent fill (sibling/parent), Known=dashed (UIA info), Other=thin border
/// </summary>
internal enum HackBoxRole { Other, Known, Scope, Target }
internal sealed record A11yHackOverlayBox(Rect Bounds, Color Color, string? Label = null, double Thickness = 2.0, string? OcrText = null, HackBoxRole Role = HackBoxRole.Other);
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
        // DPI scale for hover too
        double hDpi = 1.0;
        var hSource = PresentationSource.FromVisual(this);
        if (hSource?.CompositionTarget != null)
            hDpi = hSource.CompositionTarget.TransformToDevice.M11;
        _hoverRect.Width = Math.Max(1, box.Bounds.Width / hDpi);
        _hoverRect.Height = Math.Max(1, box.Bounds.Height / hDpi);
        _hoverRect.Fill = new SolidColorBrush(Color.FromArgb(48, box.Color.R, box.Color.G, box.Color.B));
        _hoverRect.Stroke = new SolidColorBrush(Colors.White);
        Canvas.SetLeft(_hoverRect, box.Bounds.X / hDpi);
        Canvas.SetTop(_hoverRect, box.Bounds.Y / hDpi);
        _hoverRect.Visibility = Visibility.Visible;
    }

    public void Render(A11yHackOverlayModel model, int hoverIndex = -1)
    {
        _canvas.Children.Clear();
        _hoverRect = null; // cleared with canvas

        // DPI scale: box coordinates are physical pixels, WPF Canvas uses DIPs
        double dpiScale = 1.0;
        var source = PresentationSource.FromVisual(this);
        if (source?.CompositionTarget != null)
            dpiScale = source.CompositionTarget.TransformToDevice.M11; // e.g. 1.25 for 125%

        for (int i = 0; i < model.Boxes.Count; i++)
        {
            var box = model.Boxes[i];
            var role = box.Role;
            // Convert physical pixel bounds to DIP
            var bx = box.Bounds.X / dpiScale;
            var by = box.Bounds.Y / dpiScale;
            var bw = box.Bounds.Width / dpiScale;
            var bh = box.Bounds.Height / dpiScale;

            // Style per role:
            //   Target = neon green solid + fill + glow
            //   Scope  = semi-transparent fill + solid border (siblings/parent)
            //   Known  = dashed border, no fill (has UIA info)
            //   Other  = thin solid border, no fill
            Brush stroke; double thickness; Brush fill;
            DoubleCollection? dash; double radiusX, radiusY;
            DropShadowEffect? effect;

            switch (role)
            {
                case HackBoxRole.Target:
                    stroke = new SolidColorBrush(Color.FromRgb(0x00, 0xFF, 0x88));
                    thickness = 2.2;
                    fill = new SolidColorBrush(Color.FromArgb(28, 0x00, 0xFF, 0x88));
                    dash = null;
                    radiusX = radiusY = 3;
                    effect = new DropShadowEffect { Color = Color.FromRgb(0x00, 0xFF, 0x88), BlurRadius = 16, ShadowDepth = 0, Opacity = 0.8 };
                    break;
                case HackBoxRole.Scope:
                    stroke = new SolidColorBrush(Color.FromRgb(0x42, 0xA5, 0xF5)); // blue
                    thickness = 1.2;
                    fill = new SolidColorBrush(Color.FromArgb(22, 0x42, 0xA5, 0xF5));
                    dash = null;
                    radiusX = radiusY = 2;
                    effect = new DropShadowEffect { Color = Color.FromRgb(0x42, 0xA5, 0xF5), BlurRadius = 6, ShadowDepth = 0, Opacity = 0.3 };
                    break;
                default: // Known + Other → all dashed (analysis window scope)
                    stroke = new SolidColorBrush(Color.FromArgb(160, box.Color.R, box.Color.G, box.Color.B));
                    thickness = 0.8;
                    fill = Brushes.Transparent;
                    dash = new DoubleCollection { 3, 2 };
                    radiusX = radiusY = 0;
                    effect = null;
                    break;
            }

            var rect = new Rectangle
            {
                Width = Math.Max(1, bw),
                Height = Math.Max(1, bh),
                Stroke = stroke,
                StrokeThickness = thickness,
                Fill = fill,
                RadiusX = radiusX,
                RadiusY = radiusY,
                StrokeDashArray = dash,
                Effect = effect,
                IsHitTestVisible = false
            };
            Canvas.SetLeft(rect, bx);
            Canvas.SetTop(rect, by);
            _canvas.Children.Add(rect);

            // Label: placed at top-right of each box border (inside, overlapping border)
            if (!string.IsNullOrWhiteSpace(box.Label))
            {
                var labelFg = role switch
                {
                    HackBoxRole.Target => Color.FromRgb(0x00, 0xFF, 0x88),
                    HackBoxRole.Scope => Color.FromRgb(0x90, 0xCA, 0xF9), // light blue
                    _ => Color.FromRgb(0xB0, 0xE0, 0xE6), // light cyan
                };
                var labelBgColor = role switch
                {
                    HackBoxRole.Target => Color.FromArgb(220, 0, 30, 20),
                    HackBoxRole.Scope => Color.FromArgb(200, 0, 15, 50),
                    _ => Color.FromArgb(200, 0, 0, 60),
                };
                var labelBg = new Border
                {
                    Background = new SolidColorBrush(labelBgColor),
                    CornerRadius = new CornerRadius(2),
                    Padding = new Thickness(3, 1, 3, 1),
                    Child = new TextBlock
                    {
                        Text = box.Label,
                        Foreground = new SolidColorBrush(labelFg),
                        FontFamily = new FontFamily("Consolas"),
                        FontSize = role == HackBoxRole.Target ? 9 : 8,
                        FontWeight = role == HackBoxRole.Target ? FontWeights.Bold : FontWeights.Normal,
                    },
                    Effect = role == HackBoxRole.Target
                        ? new DropShadowEffect { Color = Color.FromRgb(0x00, 0xFF, 0x88), BlurRadius = 6, ShadowDepth = 0, Opacity = 0.5 }
                        : null,
                    IsHitTestVisible = false
                };
                labelBg.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                var labelW = labelBg.DesiredSize.Width;
                var labelH = labelBg.DesiredSize.Height;
                // Top-right inside the box, overlapping the border
                var lx = Math.Max(0, bx + bw - labelW - 2);
                var ly = Math.Max(0, by + 2);
                Canvas.SetLeft(labelBg, lx);
                Canvas.SetTop(labelBg, ly);
                _canvas.Children.Add(labelBg);
            }

            // OCR text: inside box as content, gold text, marquee scroll if overflows
            if (!string.IsNullOrWhiteSpace(box.OcrText))
            {
                var fontSize = Math.Clamp(bh * 0.45, 7, 12);
                var boxW = Math.Max(10, (bw - 4) / 2); // half-width: don't cover original text

                var ocrBlock = new TextBlock
                {
                    Text = box.OcrText,
                    Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0xD7, 0x00)), // Gold
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = fontSize,
                    TextWrapping = TextWrapping.NoWrap,
                    IsHitTestVisible = false
                };

                // Clip container to box width
                var clipCanvas = new Canvas
                {
                    Width = boxW,
                    Height = fontSize + 4,
                    ClipToBounds = true,
                    IsHitTestVisible = false
                };
                clipCanvas.Children.Add(ocrBlock);
                Canvas.SetTop(ocrBlock, 0);

                var ocrBg = new Border
                {
                    Background = new SolidColorBrush(Color.FromArgb(160, 20, 10, 0)),
                    CornerRadius = new CornerRadius(1),
                    Padding = new Thickness(2, 0, 2, 0),
                    Child = clipCanvas,
                    IsHitTestVisible = false
                };

                // Measure text width for scroll decision
                ocrBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                var textW = ocrBlock.DesiredSize.Width;

                if (textW > boxW)
                {
                    // Marquee scroll: left → right → left (ping-pong)
                    var translate = new TranslateTransform(0, 0);
                    ocrBlock.RenderTransform = translate;
                    var scrollDist = textW - boxW;
                    var duration = TimeSpan.FromSeconds(Math.Max(2, scrollDist / 30.0)); // ~30px/s
                    var anim = new DoubleAnimation
                    {
                        From = 0,
                        To = -scrollDist,
                        Duration = new Duration(duration),
                        AutoReverse = true,
                        RepeatBehavior = RepeatBehavior.Forever,
                        EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
                    };
                    translate.BeginAnimation(TranslateTransform.XProperty, anim);
                }

                ocrBg.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                var ocrH = ocrBg.DesiredSize.Height;
                // Right-aligned inside box (left half = original text untouched)
                var ox = bx + bw - boxW - 4;
                if (ox < bx) ox = bx;
                var oy = by + (bh - ocrH) / 2;
                if (oy < by + 1) oy = by + 1;
                Canvas.SetLeft(ocrBg, ox);
                Canvas.SetTop(ocrBg, oy);
                _canvas.Children.Add(ocrBg);
            }
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

/// <summary>Fixed overlay slot: Input (mouse/keyboard tracking) vs Session (AI analysis).</summary>
internal enum OverlaySlot { Input, Session }

internal sealed class A11yHackOverlayHost : IDisposable
{
    Thread? _uiThread;
    Dispatcher? _dispatcher;
    A11yHackOverlayWindow? _window;
    readonly ManualResetEventSlim _ready = new();

    // ── Singleton slots (2 fixed windows) ──
    static readonly object _slotLock = new();
    static A11yHackOverlayHost? _inputSlot;
    static A11yHackOverlayHost? _sessionSlot;

    /// <summary>
    /// Get or create overlay for the given slot. Reuses existing window (repositions).
    /// </summary>
    public static A11yHackOverlayHost? GetOrCreate(OverlaySlot slot, int screenX, int screenY, int width, int height)
    {
        lock (_slotLock)
        {
            ref var slotRef = ref (slot == OverlaySlot.Input ? ref _inputSlot : ref _sessionSlot);
            if (slotRef != null && slotRef._dispatcher != null)
            {
                // Reuse: reposition + show
                slotRef.Reposition(screenX, screenY, width, height);
                slotRef.Show();
                return slotRef;
            }

            // Create new
            try
            {
                var host = new A11yHackOverlayHost();
                host.Start(screenX, screenY, width, height);
                slotRef = host;
                return host;
            }
            catch { return null; }
        }
    }

    /// <summary>Legacy: create unmanaged overlay (backward compat for one-off analysis).</summary>
    public static A11yHackOverlayHost? TryStart(int screenX, int screenY, int width, int height)
        => GetOrCreate(OverlaySlot.Session, screenX, screenY, width, height);

    /// <summary>Reposition existing window to new screen coordinates.</summary>
    public void Reposition(int screenX, int screenY, int width, int height)
    {
        _dispatcher?.BeginInvoke(() =>
        {
            if (_window == null) return;
            _window.Width = width;
            _window.Height = height;
            ForceWindowPosition(_window, screenX, screenY, width, height);
        });
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
        // Clear singleton slot if this is a managed instance
        lock (_slotLock)
        {
            if (_inputSlot == this) _inputSlot = null;
            if (_sessionSlot == this) _sessionSlot = null;
        }
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
