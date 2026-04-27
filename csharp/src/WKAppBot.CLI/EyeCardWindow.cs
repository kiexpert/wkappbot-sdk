// EyeCardWindow.cs -- Per-workspace Eye status overlay (upper-right of VS Code window)
// Code-only WPF window (no XAML). Tracks parent VS Code HWND, repositions every 200ms,
// reads status JSON every 3s. No focus steal: WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW.
//
// Tag: [EYE-CARD]

using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WKAppBot.CLI;

internal sealed class EyeCardWindow : Window
{
    private readonly IntPtr _vsHwnd;
    private readonly string _statusFilePath;
    private readonly TextBlock[] _tbLines; // up to 3 lines
    private DispatcherTimer? _posTimer;
    private DispatcherTimer? _statusTimer;
    private bool _hovering;
    private double _baseOpacity = 1.0; // 1.0 when VS Code focused, 0.5 when not

    public EyeCardWindow(IntPtr vsHwnd, string statusFilePath)
    {
        _vsHwnd = vsHwnd;
        _statusFilePath = statusFilePath;

        Title = "EyeCard";
        Width = 244;
        SizeToContent = SizeToContent.Height; // auto-fit vertically to line count
        WindowStyle = WindowStyle.None;
        ResizeMode = ResizeMode.NoResize;
        ShowInTaskbar = false;
        ShowActivated = false;
        Topmost = false; // Z-order managed manually: always just above _vsHwnd
        AllowsTransparency = true;
        Background = Brushes.Transparent;

        var border = new Border
        {
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            BorderThickness = new Thickness(0),
        };
        var stack = new StackPanel { Margin = new Thickness(5, 4, 5, 4) };
        var fg = new SolidColorBrush(Color.FromRgb(0x00, 0xFF, 0x41));
        _tbLines = new TextBlock[5];
        for (int i = 0; i < 3; i++)
        {
            _tbLines[i] = new TextBlock
            {
                Foreground = fg,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 10.5,
                Opacity = i == 0 ? 1.0 : 0.82,
                TextTrimming = TextTrimming.CharacterEllipsis,
                Visibility = i == 0 ? Visibility.Visible : Visibility.Collapsed,
            };
            stack.Children.Add(_tbLines[i]);
        }
        _tbLines[0].Text = "…";
        border.Child = stack;
        Content = border;

        Loaded += OnLoaded;
        // No MouseEnter/MouseLeave: opacity=0 breaks WPF hit-test causing flicker loop.
        // Cursor position is polled in the reposition timer instead.
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // Apply WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW
        var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        if (hwnd != IntPtr.Zero)
        {
            int ex = GetWindowLong(hwnd, GWL_EXSTYLE);
            ex |= WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW | WS_EX_TRANSPARENT;
            SetWindowLong(hwnd, GWL_EXSTYLE, ex);
            // Clear TOPMOST flag -- Z-order tracked per vsHwnd instead
            SetWindowPos(hwnd, HWND_NOTOPMOST, 0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        }

        _posTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
        _posTimer.Tick += (_, _) => Reposition();
        _posTimer.Start();

        _statusTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
        _statusTimer.Tick += (_, _) => RefreshStatus();
        _statusTimer.Start();

        Reposition();
        RefreshStatus();
    }

    private void AnimateOpacity(double to, int durationMs)
    {
        var anim = new DoubleAnimation(to, TimeSpan.FromMilliseconds(durationMs))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        BeginAnimation(OpacityProperty, anim);
    }

    private void Reposition()
    {
        if (!IsWindow(_vsHwnd)) { Close(); return; }
        if (IsIconic(_vsHwnd)) { Hide(); return; } else Show();
        if (!GetWindowRect(_vsHwnd, out var rect)) return;
        // Sanity: if rect is zero/tiny the HWND is not a real visible window -- close
        if (rect.Right - rect.Left < 100 || rect.Bottom - rect.Top < 50) { Close(); return; }

        int sw = GetSystemMetrics(SM_CXSCREEN);
        int sh = GetSystemMetrics(SM_CYSCREEN);
        int w = (int)ActualWidth;
        int h = (int)ActualHeight;

        int x = rect.Right - (w + 5);
        int y = rect.Top + 5;
        if (x < 0) x = 0;
        if (y < 0) y = 0;
        if (sw > 0 && x + w > sw) x = Math.Max(0, sw - w);
        if (sh > 0 && y + h > sh) y = Math.Max(0, sh - h);

        var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        if (hwnd != IntPtr.Zero)
        {
            // Place card just above vsHwnd in Z-order: find who's currently above vsHwnd
            var above = GetWindow(_vsHwnd, GW_HWNDPREV);
            var insertAfter = (above != IntPtr.Zero && above != hwnd) ? above : HWND_TOP;
            SetWindowPos(hwnd, insertAfter, x, y, w, h, SWP_NOACTIVATE | SWP_SHOWWINDOW);
        }

        // Opacity logic -- polled here to avoid MouseEnter/Leave flicker when opacity=0
        // Compare by PID, not HWND: VS Code focus lands on child Chrome windows, not _vsHwnd itself
        var fg = GetForegroundWindow();
        GetWindowThreadProcessId(_vsHwnd, out uint vsPid);
        GetWindowThreadProcessId(fg, out uint fgPid);
        bool focused = vsPid != 0 && fgPid == vsPid;
        _baseOpacity = focused ? 1.0 : 0.5;

        GetCursorPos(out var cur);
        bool nowHovering = cur.X >= x && cur.X <= x + w && cur.Y >= y && cur.Y <= y + h;

        double target = nowHovering
            ? (focused ? 0.0 : 1.0)  // focused+hover=hide, unfocused+hover=show
            : _baseOpacity;

        if (Math.Abs(Opacity - target) > 0.02)
        {
            bool wasHovering = _hovering;
            _hovering = nowHovering;
            int ms = (!wasHovering && nowHovering) ? 150   // entering: fast
                   : (wasHovering && !nowHovering)  ? 300  // leaving: gentle
                   : 400;                                   // focus change: slow
            AnimateOpacity(target, durationMs: ms);
        }
        else
        {
            _hovering = nowHovering;
        }
    }

    private void RefreshStatus()
    {
        try
        {
            string[]? lines = null;
            if (File.Exists(_statusFilePath))
            {
                var fi = new FileInfo(_statusFilePath);
                if ((DateTime.UtcNow - fi.LastWriteTimeUtc).TotalSeconds <= 30)
                {
                    using var doc = JsonDocument.Parse(File.ReadAllText(_statusFilePath));
                    var root = doc.RootElement;
                    if (root.TryGetProperty("lines", out var arr) && arr.ValueKind == JsonValueKind.Array)
                        lines = arr.EnumerateArray().Select(e => e.GetString() ?? "").ToArray();
                }
            }
            lines ??= new[] { "…", "idle" };

            // Update TextBlocks and auto-resize height
            int count = Math.Min(lines.Length, _tbLines.Length);
            for (int i = 0; i < _tbLines.Length; i++)
            {
                if (i < count)
                {
                    _tbLines[i].Text = lines[i];
                    _tbLines[i].Visibility = Visibility.Visible;
                }
                else
                {
                    _tbLines[i].Visibility = Visibility.Collapsed;
                }
            }
            // SizeToContent=Height handles vertical sizing automatically
        }
        catch { /* tolerate transient parse / IO errors */ }
    }

    // --- P/Invoke ---
    [StructLayout(LayoutKind.Sequential)]
    private struct RECT { public int Left, Top, Right, Bottom; }

    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_NOACTIVATE  = 0x08000000;
    private const int WS_EX_TOOLWINDOW  = 0x00000080;
    private const int WS_EX_TRANSPARENT = 0x00000020; // click-through when invisible
    private const int SM_CXSCREEN = 0;
    private const int SM_CYSCREEN = 1;
    private static readonly IntPtr HWND_TOP      = new(0);
    private static readonly IntPtr HWND_TOPMOST  = new(-1);
    private static readonly IntPtr HWND_NOTOPMOST = new(-2);
    private const uint GW_HWNDPREV   = 3; // window just above in Z-order
    private const uint SWP_NOMOVE    = 0x0002;
    private const uint SWP_NOSIZE    = 0x0001;
    private const uint SWP_NOACTIVATE = 0x0010;
    private const uint SWP_SHOWWINDOW = 0x0040;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    [DllImport("user32.dll")]
    private static extern bool IsWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    private static extern bool IsIconic(IntPtr hWnd);
    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")]
    private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);
    [StructLayout(LayoutKind.Sequential)]
    private struct POINT { public int X, Y; }
}
