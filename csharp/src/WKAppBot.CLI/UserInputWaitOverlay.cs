using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace WKAppBot.CLI;

/// <summary>
/// Non-focus-stealing WPF overlay that asks user to yield focus.
/// Countdown timer on confirm button — timeout = fail.
/// Owner set to target app's main window (stays above target, hides with it).
///
/// Design: "사용자님의 포커스 양보가 필요합니다"
/// Tag: [READINESS] [ZOOM]
/// </summary>
internal sealed class UserInputWaitWindow : Window
{
    private readonly TextBlock _buttonText;
    private readonly Border _buttonBorder;
    private readonly DispatcherTimer _countdownTimer;
    private readonly IntPtr _ownerHwnd;
    private int _remainingSeconds;
    private bool _confirmed;

    public event Action? Confirmed;
    public event Action? TimedOut;

    public UserInputWaitWindow(IntPtr ownerHwnd, uint userIdleMs, int timeoutSeconds)
    {
        _ownerHwnd = ownerHwnd;
        _remainingSeconds = timeoutSeconds;

        Title = "UserInputWait";
        Width = 400;
        Height = 210;
        WindowStyle = WindowStyle.None;
        AllowsTransparency = true;
        Background = Brushes.Transparent;
        Topmost = true;
        ShowInTaskbar = false;
        ShowActivated = false; // CRITICAL: never steal focus
        ResizeMode = ResizeMode.NoResize;

        // ── Build visual tree ──
        var outerBorder = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(0xF5, 0x1A, 0x1A, 0x2E)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(0xFF, 0xA0, 0x00)), // amber
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(8),
            Padding = new Thickness(20, 14, 20, 14),
        };

        var stack = new StackPanel { VerticalAlignment = VerticalAlignment.Center };

        // Header
        stack.Children.Add(new TextBlock
        {
            Text = "[READINESS] 포커스 양보 대기",
            Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0xA0, 0x00)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 11,
            Margin = new Thickness(0, 0, 0, 10),
        });

        // Main message
        stack.Children.Add(new TextBlock
        {
            Text = "사용자님의 포커스 양보가 필요합니다",
            Foreground = Brushes.White,
            FontFamily = new FontFamily("맑은 고딕"),
            FontSize = 15,
            FontWeight = FontWeights.Bold,
            Margin = new Thickness(0, 0, 0, 6),
        });

        // Detail
        stack.Children.Add(new TextBlock
        {
            Text = "자동화가 포커스를 확보해야 합니다.\n현재 작업을 잠시 멈추고 확인을 눌러주세요.",
            Foreground = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA)),
            FontFamily = new FontFamily("맑은 고딕"),
            FontSize = 11,
            Margin = new Thickness(0, 0, 0, 8),
            TextWrapping = TextWrapping.Wrap,
        });

        // Idle info
        stack.Children.Add(new TextBlock
        {
            Text = $"마지막 입력: {userIdleMs / 1000.0:F1}초 전",
            Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0xD5, 0x4F)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 10,
            FontStyle = FontStyles.Italic,
            Margin = new Thickness(0, 0, 0, 12),
        });

        // Confirm button (Border + TextBlock for clean look, no ugly chrome)
        _buttonBorder = new Border
        {
            Background = new SolidColorBrush(Color.FromRgb(0xFF, 0xA0, 0x00)),
            CornerRadius = new CornerRadius(4),
            Padding = new Thickness(28, 8, 28, 8),
            HorizontalAlignment = HorizontalAlignment.Center,
            Cursor = Cursors.Hand,
        };
        _buttonText = new TextBlock
        {
            Text = $"확인 ({_remainingSeconds})",
            Foreground = new SolidColorBrush(Color.FromRgb(0x1A, 0x1A, 0x2E)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 13,
            FontWeight = FontWeights.Bold,
            HorizontalAlignment = HorizontalAlignment.Center,
        };
        _buttonBorder.Child = _buttonText;
        _buttonBorder.MouseLeftButtonDown += (_, _) => OnConfirm();
        _buttonBorder.MouseEnter += (_, _) =>
            _buttonBorder.Background = new SolidColorBrush(Color.FromRgb(0xFF, 0xB8, 0x40));
        _buttonBorder.MouseLeave += (_, _) =>
            _buttonBorder.Background = new SolidColorBrush(Color.FromRgb(0xFF, 0xA0, 0x00));

        stack.Children.Add(_buttonBorder);
        outerBorder.Child = stack;
        Content = outerBorder;

        // Countdown timer
        _countdownTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _countdownTimer.Tick += OnCountdownTick;
        _countdownTimer.Start();

        // 정중한 요청 차임벨 (창 표시 시 재생)
        Loaded += (_, _) => PlayChime();
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        var helper = new WindowInteropHelper(this);

        // Set owner to target app's main window (cross-process Win32 ownership)
        // Effect: alert stays above target, hides when target minimized
        if (_ownerHwnd != IntPtr.Zero)
            SetWindowLongPtr(helper.Handle, GWL_HWNDPARENT, _ownerHwnd);

        // WS_EX_NOACTIVATE: clicking won't steal focus from any app
        var exStyle = GetWindowLongPtr(helper.Handle, GWL_EXSTYLE);
        SetWindowLongPtr(helper.Handle, GWL_EXSTYLE,
            new IntPtr(exStyle.ToInt64() | WS_EX_NOACTIVATE));
    }

    private void OnConfirm()
    {
        if (_confirmed) return;
        _confirmed = true;
        _countdownTimer.Stop();

        // 핵심: 유저가 우리 창을 클릭했으므로 foreground right 보유 중!
        // 즉시 타겟 윈도우에 포커스 이전 → 호출자가 EnsureFocus 스킵 가능
        if (_ownerHwnd != IntPtr.Zero)
            SetForegroundWindow(_ownerHwnd);
        Thread.Sleep(80); // Windows가 포커스 전환 처리할 시간

        Confirmed?.Invoke();
        Dispatcher.InvokeShutdown();
    }

    private void OnCountdownTick(object? sender, EventArgs e)
    {
        _remainingSeconds--;
        if (_remainingSeconds <= 0)
        {
            _countdownTimer.Stop();
            _buttonText.Text = "시간 초과";
            _buttonBorder.Background = new SolidColorBrush(Color.FromRgb(0xFF, 0x40, 0x40));
            _buttonBorder.MouseEnter -= null; // disable hover
            TimedOut?.Invoke();
            // Brief delay then auto-close
            var close = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(800) };
            close.Tick += (_, _) => { close.Stop(); Dispatcher.InvokeShutdown(); };
            close.Start();
        }
        else
        {
            _buttonText.Text = $"확인 ({_remainingSeconds})";
            // Gradually amber → red as time runs out (last 5 seconds)
            if (_remainingSeconds <= 5)
            {
                byte g = (byte)(0xA0 * _remainingSeconds / 5);
                _buttonBorder.Background = new SolidColorBrush(Color.FromRgb(0xFF, g, 0x00));
            }
        }
    }

    // ── Chime: 정중한 요청 멜로디 (C5→E5→G5→C6 메이저 아르페지오) ──

    private static void PlayChime()
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                const int rate = 22050;
                // Ascending major arpeggio — polite "attention please~" chime
                (double hz, int ms)[] melody =
                {
                    (523.25, 100),  // C5
                    (659.25, 100),  // E5
                    (783.99, 100),  // G5
                    (1046.50, 220), // C6 — longer, resolving note
                };

                var pcm = new System.Collections.Generic.List<short>();
                foreach (var (hz, ms) in melody)
                {
                    int n = rate * ms / 1000;
                    for (int i = 0; i < n; i++)
                    {
                        double t = (double)i / rate;
                        // Smooth envelope: quick attack (3ms), gentle release (40%)
                        double env = Math.Min(1.0, Math.Min(i / 66.0, (n - i) / (n * 0.4)));
                        // Sine + soft overtone for warmth
                        double wave = Math.Sin(2 * Math.PI * hz * t)
                                    + 0.15 * Math.Sin(4 * Math.PI * hz * t);
                        pcm.Add((short)(5500 * env * wave));
                    }
                    // 25ms gap between notes
                    for (int i = 0; i < rate * 25 / 1000; i++)
                        pcm.Add(0);
                }

                // Build WAV in memory (16-bit mono PCM)
                using var wav = new MemoryStream();
                using var bw = new BinaryWriter(wav);
                int dataLen = pcm.Count * 2;
                bw.Write(Encoding.ASCII.GetBytes("RIFF"));
                bw.Write(36 + dataLen);
                bw.Write(Encoding.ASCII.GetBytes("WAVEfmt "));
                bw.Write(16);         // fmt chunk size
                bw.Write((short)1);   // PCM format
                bw.Write((short)1);   // mono
                bw.Write(rate);       // sample rate
                bw.Write(rate * 2);   // byte rate
                bw.Write((short)2);   // block align
                bw.Write((short)16);  // bits per sample
                bw.Write(Encoding.ASCII.GetBytes("data"));
                bw.Write(dataLen);
                foreach (var s in pcm) bw.Write(s);

                wav.Position = 0;
                using var player = new SoundPlayer(wav);
                player.PlaySync();
            }
            catch { /* audio not available — silent fallback */ }
        });
    }

    // ── Win32 P/Invoke ──
    private const int GWL_EXSTYLE = -20;
    private const int GWL_HWNDPARENT = -8;
    private const int WS_EX_NOACTIVATE = 0x08000000;

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
}

/// <summary>
/// STA thread host for UserInputWaitWindow.
/// Blocks calling thread until user confirms or countdown expires.
/// Tag: [READINESS]
/// </summary>
internal static class UserInputWaitOverlay
{
    /// <summary>
    /// Show the yield dialog and block until resolved.
    /// Returns true if user clicked confirm, false if timed out.
    /// </summary>
    public static bool Show(IntPtr ownerHwnd, uint userIdleMs, int timeoutSeconds)
    {
        bool confirmed = false;
        var done = new ManualResetEventSlim(false);

        var thread = new Thread(() =>
        {
            var window = new UserInputWaitWindow(ownerHwnd, userIdleMs, timeoutSeconds);

            // Position: top-center of primary screen (visible but non-intrusive)
            var screenW = SystemParameters.PrimaryScreenWidth;
            window.Left = (screenW - window.Width) / 2;
            window.Top = 120;

            window.Confirmed += () => { confirmed = true; done.Set(); };
            window.TimedOut += () => { confirmed = false; done.Set(); };

            window.Show();
            Dispatcher.Run(); // blocks until InvokeShutdown()
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.IsBackground = true;
        thread.Name = "UserInputWait-STA";
        thread.Start();

        // Block caller until confirmed or timeout (+2s safety margin)
        done.Wait((timeoutSeconds + 3) * 1000);
        done.Dispose();

        return confirmed;
    }
}
