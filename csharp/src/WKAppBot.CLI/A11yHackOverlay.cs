using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal enum HackBoxRole { Scope, Target, Known, Cached }

internal sealed record A11yHackOverlayBox(
    Rect Bounds,
    string? Label = null,
    string? OcrText = null,
    HackBoxRole Role = HackBoxRole.Scope);

internal sealed record A11yHackOverlayModel(IReadOnlyList<A11yHackOverlayBox> Boxes);

/// <summary>Fixed overlay slot: Input (mouse/keyboard tracking) vs Session (AI analysis).</summary>
internal enum OverlaySlot { Input, Session }

// ── Window ──────────────────────────────────────────────────────────────

internal sealed class A11yHackOverlayWindow : Window
{
    readonly Canvas _canvas;
    Rectangle? _hoverRect;

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
        Opacity = 0.5;
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

    double GetDpiScale()
    {
        var src = PresentationSource.FromVisual(this);
        return src?.CompositionTarget?.TransformToDevice.M11 ?? 1.0;
    }

    public void SetHover(A11yHackOverlayModel model, int hoverIndex)
    {
        if (hoverIndex < 0 || hoverIndex >= model.Boxes.Count)
        {
            if (_hoverRect != null) _hoverRect.Visibility = Visibility.Collapsed;
            return;
        }
        var box = model.Boxes[hoverIndex];
        if (_hoverRect == null)
        {
            _hoverRect = new Rectangle
            {
                Stroke = Brushes.White, StrokeThickness = 3,
                IsHitTestVisible = false,
            };
            _canvas.Children.Add(_hoverRect);
        }
        var dpi = GetDpiScale();
        _hoverRect.Width = Math.Max(1, box.Bounds.Width / dpi);
        _hoverRect.Height = Math.Max(1, box.Bounds.Height / dpi);
        _hoverRect.Fill = new SolidColorBrush(Color.FromArgb(48, 0x42, 0xA5, 0xF5));
        Canvas.SetLeft(_hoverRect, box.Bounds.X / dpi);
        Canvas.SetTop(_hoverRect, box.Bounds.Y / dpi);
        _hoverRect.Visibility = Visibility.Visible;
    }

    public void ClearCanvas()
    {
        _canvas.Children.Clear();
        _hoverRect = null;
    }

    public void Render(A11yHackOverlayModel model)
    {
        ClearCanvas();
        var dpi = GetDpiScale();

        foreach (var box in model.Boxes)
        {
            var bx = box.Bounds.X / dpi;
            var by = box.Bounds.Y / dpi;
            var bw = box.Bounds.Width / dpi;
            var bh = box.Bounds.Height / dpi;

            // ── Box style: Target(neon) / Scope(blue fill) / Known(green dashed) ──
            var role = box.Role;
            Brush stroke; double thick; Brush fill;
            DoubleCollection? dash; double rx, ry; Effect? fx;
            switch (role)
            {
                case HackBoxRole.Target:
                    stroke = new SolidColorBrush(Color.FromRgb(0x00, 0xFF, 0x88));
                    thick = 2.2; fill = Brushes.Transparent;
                    dash = null; rx = ry = 3;
                    fx = new DropShadowEffect { Color = Color.FromRgb(0x00, 0xFF, 0x88), BlurRadius = 16, ShadowDepth = 0, Opacity = 0.8 };
                    break;
                case HackBoxRole.Scope: // parent chain — 2x thick neon dashed
                    stroke = new SolidColorBrush(Color.FromRgb(0x00, 0xFF, 0x88));
                    thick = 2.4; fill = Brushes.Transparent;
                    dash = new DoubleCollection { 4, 2 }; rx = ry = 2;
                    fx = new DropShadowEffect { Color = Color.FromRgb(0x00, 0xFF, 0x88), BlurRadius = 8, ShadowDepth = 0, Opacity = 0.4 };
                    break;
                case HackBoxRole.Cached: // experience DB hit — amber dashed, no fill
                    stroke = new SolidColorBrush(Color.FromArgb(180, 0xFF, 0xA5, 0x00));
                    thick = 1.0; fill = Brushes.Transparent;
                    dash = new DoubleCollection { 2, 2 }; rx = ry = 1;
                    fx = null;
                    break;
                default: // Known — system a11y dashed green, no fill
                    stroke = new SolidColorBrush(Color.FromArgb(220, 0x32, 0xCD, 0x32));
                    thick = 1.2; fill = Brushes.Transparent;
                    dash = new DoubleCollection { 3, 2 }; rx = ry = 1;
                    fx = null;
                    break;
            }
            var rect = new Rectangle
            {
                Width = Math.Max(1, bw), Height = Math.Max(1, bh),
                Stroke = stroke, StrokeThickness = thick, Fill = fill,
                RadiusX = rx, RadiusY = ry, StrokeDashArray = dash,
                Effect = fx, IsHitTestVisible = false
            };
            Canvas.SetLeft(rect, bx);
            Canvas.SetTop(rect, by);
            _canvas.Children.Add(rect);

            // ── Label (top-right) ──
            if (!string.IsNullOrWhiteSpace(box.Label))
            {
                var (fg, bg2) = role switch
                {
                    HackBoxRole.Target => (Color.FromRgb(0x00, 0xFF, 0x88), Color.FromArgb(220, 0, 30, 20)),
                    HackBoxRole.Scope => (Color.FromRgb(0x90, 0xCA, 0xF9), Color.FromArgb(200, 0, 15, 50)),
                    HackBoxRole.Cached => (Color.FromRgb(0xFF, 0xD7, 0x80), Color.FromArgb(180, 40, 20, 0)), // amber
                    _ => (Color.FromRgb(0xB0, 0xE0, 0xB0), Color.FromArgb(180, 0, 30, 0)),
                };
                var labelBg = new Border
                {
                    Background = new SolidColorBrush(bg2),
                    CornerRadius = new CornerRadius(2),
                    Padding = new Thickness(3, 1, 3, 1),
                    Child = new TextBlock
                    {
                        Text = box.Label,
                        Foreground = new SolidColorBrush(fg),
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
                Canvas.SetLeft(labelBg, Math.Max(0, bx + bw - labelBg.DesiredSize.Width - 2));
                Canvas.SetTop(labelBg, Math.Max(0, by + 2));
                _canvas.Children.Add(labelBg);
            }

            // ── OCR text (inside box, right half, gold, marquee) ──
            if (!string.IsNullOrWhiteSpace(box.OcrText))
            {
                var fontSize = Math.Clamp(bh * 0.45, 7, 12);
                var halfW = Math.Max(10, (bw - 4) / 2);

                var ocrBlock = new TextBlock
                {
                    Text = box.OcrText,
                    Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0xD7, 0x00)),
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = fontSize,
                    TextWrapping = TextWrapping.NoWrap,
                    IsHitTestVisible = false
                };
                var clip = new Canvas { Width = halfW, Height = fontSize + 4, ClipToBounds = true, IsHitTestVisible = false };
                clip.Children.Add(ocrBlock);

                var ocrBg = new Border
                {
                    Background = new SolidColorBrush(Color.FromArgb(160, 20, 10, 0)),
                    CornerRadius = new CornerRadius(1),
                    Padding = new Thickness(2, 0, 2, 0),
                    Child = clip,
                    IsHitTestVisible = false
                };

                ocrBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                if (ocrBlock.DesiredSize.Width > halfW)
                {
                    var dist = ocrBlock.DesiredSize.Width - halfW;
                    var tr = new TranslateTransform();
                    ocrBlock.RenderTransform = tr;
                    tr.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation
                    {
                        From = 0, To = -dist,
                        Duration = new Duration(TimeSpan.FromSeconds(Math.Max(2, dist / 30.0))),
                        AutoReverse = true, RepeatBehavior = RepeatBehavior.Forever,
                        EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
                    });
                }

                ocrBg.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                var ox = bx + bw - halfW - 4;
                if (ox < bx) ox = bx;
                var oy = by + (bh - ocrBg.DesiredSize.Height) / 2;
                if (oy < by + 1) oy = by + 1;
                Canvas.SetLeft(ocrBg, ox);
                Canvas.SetTop(ocrBg, oy);
                _canvas.Children.Add(ocrBg);
            }
        }
    }

    const int GWL_EXSTYLE = -20;
    const int WS_EX_NOACTIVATE = 0x08000000;
    const int WS_EX_TRANSPARENT = 0x00000020;

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
    static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
}

// ── Host ────────────────────────────────────────────────────────────────

internal sealed class A11yHackOverlayHost : IDisposable
{
    Thread? _uiThread;
    Dispatcher? _dispatcher;
    A11yHackOverlayWindow? _window;
    readonly ManualResetEventSlim _ready = new();
    A11yHackOverlayModel? _lastModel;
    int _screenX, _screenY;
    DispatcherTimer? _hoverTimer;

    // ── Singleton slots ──
    static readonly object _slotLock = new();
    static A11yHackOverlayHost? _inputSlot;
    static A11yHackOverlayHost? _sessionSlot;

    public static A11yHackOverlayHost? TryGetSlot(OverlaySlot slot)
    {
        lock (_slotLock)
            return slot == OverlaySlot.Input ? _inputSlot : _sessionSlot;
    }

    public static A11yHackOverlayHost? GetOrCreate(OverlaySlot slot, int screenX, int screenY, int width, int height)
    {
        lock (_slotLock)
        {
            ref var s = ref (slot == OverlaySlot.Input ? ref _inputSlot : ref _sessionSlot);
            if (s != null && s._dispatcher != null)
            {
                // Clear stale content first, then move+resize to new target
                var host = s;
                host.StopHoverTracking();
                host._dispatcher?.BeginInvoke(() => host._window?.ClearCanvas());
                host.Reposition(screenX, screenY, width, height);
                host.Show();
                return host;
            }
            try
            {
                var host = new A11yHackOverlayHost();
                host.Start(screenX, screenY, width, height);
                s = host;
                return host;
            }
            catch { return null; }
        }
    }

    public static A11yHackOverlayHost? TryStart(int screenX, int screenY, int width, int height)
        => GetOrCreate(OverlaySlot.Session, screenX, screenY, width, height);

    public void Reposition(int screenX, int screenY, int width, int height)
    {
        _screenX = screenX;
        _screenY = screenY;
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
        _screenX = screenX;
        _screenY = screenY;
        _uiThread = new Thread(() =>
        {
            _window = new A11yHackOverlayWindow(width, height) { Left = screenX, Top = screenY };
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

    public void Update(A11yHackOverlayModel model)
    {
        _lastModel = model;
        _dispatcher?.BeginInvoke(() => _window?.Render(model));
    }

    public void Update(IReadOnlyList<A11yHackOverlayBox> boxes)
        => Update(new A11yHackOverlayModel(boxes));

    public void StartHoverTracking(int screenX, int screenY)
    {
        _screenX = screenX;
        _screenY = screenY;
        _dispatcher?.BeginInvoke(() =>
        {
            _hoverTimer = new DispatcherTimer(DispatcherPriority.Input) { Interval = TimeSpan.FromMilliseconds(16) };
            int lastIdx = -1;
            POINT lastPt = default;
            _hoverTimer.Tick += (_, _) =>
            {
                if (_lastModel == null || _window == null) return;
                NativeMethods.GetCursorPos(out var pt);
                if (NativeMethods.GetUserIdleMs() < 200)
                {
                    if (lastIdx >= 0) { lastIdx = -1; _window.SetHover(_lastModel, -1); }
                    lastPt = pt;
                    return;
                }
                if (pt.X == lastPt.X && pt.Y == lastPt.Y && lastIdx >= 0) return;
                lastPt = pt;

                double lx = pt.X - _screenX, ly = pt.Y - _screenY;
                int idx = -1;
                for (int i = _lastModel.Boxes.Count - 1; i >= 0; i--)
                {
                    var b = _lastModel.Boxes[i].Bounds;
                    if (lx >= b.X && lx <= b.X + b.Width && ly >= b.Y && ly <= b.Y + b.Height)
                    { idx = i; break; }
                }
                if (idx == lastIdx) return;
                lastIdx = idx;
                _window.SetHover(_lastModel, idx);
            };
            _hoverTimer.Start();
        });
    }

    public void StopHoverTracking()
        => _dispatcher?.BeginInvoke(() => { _hoverTimer?.Stop(); _hoverTimer = null; });

    public void Hide()
        => _dispatcher?.BeginInvoke(() =>
        {
            if (_window == null) return;
            _window.ClearCanvas(); // stop animations, free WPF resources
            _window.Visibility = Visibility.Collapsed;
        });

    public void Show()
        => _dispatcher?.BeginInvoke(() => { if (_window != null) _window.Visibility = Visibility.Visible; });

    public void Dispose()
    {
        lock (_slotLock)
        {
            if (_inputSlot == this) _inputSlot = null;
            if (_sessionSlot == this) _sessionSlot = null;
        }
        try { StopHoverTracking(); _dispatcher?.InvokeShutdown(); _uiThread?.Join(1500); } catch { }
        _ready.Dispose();
    }

    static void ForceWindowPosition(Window window, int x, int y, int w, int h)
    {
        try
        {
            var hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd != IntPtr.Zero) SetWindowPos(hwnd, HWND_TOPMOST, x, y, w, h, SWP_NOACTIVATE);
        }
        catch { }
    }

    static readonly IntPtr HWND_TOPMOST = new(-1);
    const uint SWP_NOACTIVATE = 0x0010;

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
}
