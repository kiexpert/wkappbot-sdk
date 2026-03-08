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
/// Countdown timer — auto-approves when user idle, resets on user activity.
/// Owner set to target app's main window (stays above target, hides with it).
///
/// Design: "사용자님의 포커스 양보가 필요합니다"
/// - User clicks "확인" → immediate confirm (foreground right guaranteed)
/// - User idle until timeout → auto-approve (try SetForegroundWindow)
/// - User active (mouse/keyboard) → reset timer to 30s
/// Tag: [READINESS] [ZOOM]
/// </summary>
internal sealed class UserInputWaitWindow : Window
{
    private readonly TextBlock _buttonText;
    private readonly Border _buttonBorder;
    private readonly TextBlock _idleText;
    private readonly DispatcherTimer _countdownTimer;
    private readonly IntPtr _ownerHwnd;
    private readonly int _resetSeconds;
    private int _remainingSeconds;
    private uint _lastInputTick;
    private bool _confirmed;
    private bool _focusAcquired;

    /// <summary>Focus was actually transferred to target (verified).</summary>
    public bool FocusAcquired => _focusAcquired;

    public event Action? Confirmed;

    public bool UseChime { get; set; } // true = 차임벨, false(기본) = 카라오케

    public UserInputWaitWindow(IntPtr ownerHwnd, uint userIdleMs, int timeoutSeconds, int resetSeconds = 30)
    {
        _ownerHwnd = ownerHwnd;
        _remainingSeconds = timeoutSeconds;
        _resetSeconds = resetSeconds;
        _lastInputTick = GetLastInputTick();

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
            Text = "자동화가 포커스를 확보해야 합니다.\n입력이 없으면 자동으로 진행합니다.",
            Foreground = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA)),
            FontFamily = new FontFamily("맑은 고딕"),
            FontSize = 11,
            Margin = new Thickness(0, 0, 0, 8),
            TextWrapping = TextWrapping.Wrap,
        });

        // Idle info (dynamic — updates on user activity detection)
        _idleText = new TextBlock
        {
            Text = $"마지막 입력: {userIdleMs / 1000.0:F1}초 전",
            Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0xD5, 0x4F)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 10,
            FontStyle = FontStyles.Italic,
            Margin = new Thickness(0, 0, 0, 12),
        };
        stack.Children.Add(_idleText);

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

        // 음성 안내: 카라오케(기본) or 차임벨(UseChime)
        Loaded += (_, _) => { if (UseChime) PlayChime(); else PlaySpeakAnnounce(); };
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
        {
            SetForegroundWindow(_ownerHwnd);
            Thread.Sleep(80); // Windows가 포커스 전환 처리할 시간
            _focusAcquired = true; // 클릭 = foreground right 보장
        }

        Confirmed?.Invoke();
        Dispatcher.InvokeShutdown();
    }

    private void OnAutoApprove()
    {
        if (_confirmed) return;
        _confirmed = true;
        _countdownTimer.Stop();

        _buttonText.Text = "자동 진행!";
        _buttonBorder.Background = new SolidColorBrush(Color.FromRgb(0x40, 0xC0, 0x40));

        // User idle → safe to grab focus (no foreground right, but no contention)
        if (_ownerHwnd != IntPtr.Zero)
        {
            SetForegroundWindow(_ownerHwnd);
            Thread.Sleep(80);
            _focusAcquired = GetForegroundWindow() == _ownerHwnd;
        }

        Confirmed?.Invoke();
        // Brief green flash then close
        var close = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(600) };
        close.Tick += (_, _) => { close.Stop(); Dispatcher.InvokeShutdown(); };
        close.Start();
    }

    private void OnCountdownTick(object? sender, EventArgs e)
    {
        // ── Check user activity via GetLastInputInfo ──
        uint currentTick = GetLastInputTick();
        if (currentTick != _lastInputTick)
        {
            // User active → reset timer to _resetSeconds
            _lastInputTick = currentTick;
            _remainingSeconds = _resetSeconds;
            _idleText.Text = "유저 활동 감지 — 대기 연장";
            _buttonText.Text = $"확인 ({_remainingSeconds})";
            _buttonBorder.Background = new SolidColorBrush(Color.FromRgb(0xFF, 0xA0, 0x00));
            return;
        }

        _remainingSeconds--;
        if (_remainingSeconds <= 0)
        {
            OnAutoApprove();
        }
        else if (_remainingSeconds <= 5)
        {
            // Auto-approve approaching → amber→green transition
            _buttonText.Text = $"자동 진행 ({_remainingSeconds})";
            byte r = (byte)(0xFF - (0xFF - 0x40) * (5 - _remainingSeconds) / 5);
            byte g = (byte)(0xA0 + (0xC0 - 0xA0) * (5 - _remainingSeconds) / 5);
            _buttonBorder.Background = new SolidColorBrush(Color.FromRgb(r, g, 0x00));
        }
        else
        {
            _buttonText.Text = $"확인 ({_remainingSeconds})";
        }
    }

    // ── Speak: 카라오케 음성 안내 (기본) ──

    private static void PlaySpeakAnnounce()
    {
        try
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "wkappbot",
                Arguments = "speak \"포커스 양보가 필요합니다\" --bg",
                UseShellExecute = false,
                CreateNoWindow = true
            });
        }
        catch { /* best effort — fallback to silent */ }
    }

    // ── Chime: 정중한 요청 멜로디 (C5→E5→G5→C6 메이저 아르페지오, 옵션) ──

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

    // ── GetLastInputInfo: user activity detection ──

    private static uint GetLastInputTick()
    {
        var lii = new LASTINPUTINFO { cbSize = (uint)Marshal.SizeOf<LASTINPUTINFO>() };
        GetLastInputInfo(ref lii);
        return lii.dwTime;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    [DllImport("user32.dll")]
    private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

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

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
}

/// <summary>
/// STA thread host for UserInputWaitWindow.
/// Blocks calling thread until user confirms or auto-approved on idle timeout.
/// Tag: [READINESS]
/// </summary>
internal static class UserInputWaitOverlay
{
    /// <summary>
    /// Show the yield dialog and block until resolved.
    /// Returns (approved, focusAcquired):
    /// - Manual click: approved=true, focusAcquired=true (foreground right from click)
    /// - Auto-approve (idle): approved=true, focusAcquired=verified via GetForegroundWindow
    /// - Safety timeout (5min hard cap): approved=false, focusAcquired=false
    /// </summary>
    public static (bool approved, bool focusAcquired) Show(IntPtr ownerHwnd, uint userIdleMs, int timeoutSeconds,
                                                            IntPtr positionHwnd = default)
    {
        bool approved = false;
        bool focusAcquired = false;
        var done = new ManualResetEventSlim(false);

        // positionHwnd: 위치 기준 (타겟 컨트롤/폼), ownerHwnd: Win32 소유자 (메인 윈도우)
        var posHwnd = positionHwnd != IntPtr.Zero ? positionHwnd : ownerHwnd;

        var thread = new Thread(() =>
        {
            var window = new UserInputWaitWindow(ownerHwnd, userIdleMs, timeoutSeconds);

            // Position: centered inside target window (multi-monitor safe)
            if (posHwnd != IntPtr.Zero && GetWindowRect(posHwnd, out var posRect)
                && posRect.right - posRect.left > 0)
            {
                int posW = posRect.right - posRect.left;
                int posH = posRect.bottom - posRect.top;
                // Center overlay on target window (upper 1/3 for visibility)
                window.Left = posRect.left + (posW - window.Width) / 2;
                window.Top = posRect.top + (posH - window.Height) / 3;

                // Clamp to virtual screen bounds
                int vsLeft = GetSystemMetrics(76);  // SM_XVIRTUALSCREEN
                int vsTop  = GetSystemMetrics(77);  // SM_YVIRTUALSCREEN
                int vsW    = GetSystemMetrics(78);  // SM_CXVIRTUALSCREEN
                int vsH    = GetSystemMetrics(79);  // SM_CYVIRTUALSCREEN
                if (window.Left < vsLeft) window.Left = vsLeft;
                if (window.Top  < vsTop)  window.Top  = vsTop;
                if (window.Left + window.Width > vsLeft + vsW)
                    window.Left = vsLeft + vsW - window.Width;
                if (window.Top + window.Height > vsTop + vsH)
                    window.Top = vsTop + vsH - window.Height;
            }
            else
            {
                // Fallback: primary screen center
                var screenW = SystemParameters.PrimaryScreenWidth;
                window.Left = (screenW - window.Width) / 2;
                window.Top = 120;
            }

            window.Confirmed += () =>
            {
                approved = true;
                focusAcquired = window.FocusAcquired;
                done.Set();
            };

            window.Show();
            Dispatcher.Run(); // blocks until InvokeShutdown()
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.IsBackground = true;
        thread.Name = "UserInputWait-STA";
        thread.Start();

        // Block caller — 5 minute hard cap (user activity resets can extend beyond initial timeout)
        done.Wait(5 * 60 * 1000);
        done.Dispose();

        return (approved, focusAcquired);
    }

    // P/Invoke for multi-monitor positioning
    [StructLayout(LayoutKind.Sequential)]
    private struct RECT { public int left, top, right, bottom; }

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);
}
