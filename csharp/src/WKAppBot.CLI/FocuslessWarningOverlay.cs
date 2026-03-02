using System.Collections.Concurrent;
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
/// 포커스리스 클릭 시도 후 전경이 의도치 않게 변경됐을 때 알림.
/// 타겟 윈도우 소유 WPF 오버레이 — ShowActivated=false + WS_EX_NOACTIVATE.
/// "다시 알림" 체크박스(기본 체크) — 언체크 후 확인 → 해당 프로세스 알림 억제.
/// 확인 안 누르면 5초 후 자동 닫힘 (알림 계속).
///
/// Design: "⚠ 포커스리스 시도했으나 의도치 않게 포커스됨"
/// Tag: [FL] [READINESS]
/// </summary>
internal sealed class FocuslessWarningWindow : Window
{
    private readonly IntPtr _ownerHwnd;
    private readonly string _processName;
    private readonly CheckBox _alertCheck;

    public event Action<bool>? Dismissed; // true = 다시 알림, false = 알림 억제

    public FocuslessWarningWindow(IntPtr ownerHwnd, string detail, string processName)
    {
        _ownerHwnd = ownerHwnd;
        _processName = processName;

        Title = "FocuslessWarning";
        Width = 440;
        Height = 140;
        WindowStyle = WindowStyle.None;
        AllowsTransparency = true;
        Background = Brushes.Transparent;
        Topmost = true;
        ShowInTaskbar = false;
        ShowActivated = false; // CRITICAL: never steal focus
        ResizeMode = ResizeMode.NoResize;

        // ── Visual tree ──
        var outerBorder = new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(0xF0, 0x2D, 0x1A, 0x00)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(0xFF, 0xA0, 0x00)), // amber
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(8),
            Padding = new Thickness(16, 10, 16, 10),
        };

        var stack = new StackPanel { VerticalAlignment = VerticalAlignment.Center };

        // Header
        stack.Children.Add(new TextBlock
        {
            Text = "[FL] ⚠ 의도치 않은 포커스 변경",
            Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0xC1, 0x07)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 12,
            FontWeight = FontWeights.Bold,
            Margin = new Thickness(0, 0, 0, 4),
        });

        // Detail message
        stack.Children.Add(new TextBlock
        {
            Text = $"포커스리스 클릭 성공, 그러나 앱({processName})이 전경으로 올라옴\n→ {detail}",
            Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0xE0, 0xB2)),
            FontFamily = new FontFamily("맑은 고딕"),
            FontSize = 11,
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 0, 0, 8),
        });

        // Bottom row: checkbox + button
        var bottomRow = new DockPanel { LastChildFill = false };

        // "다시 알림" checkbox (기본 체크)
        _alertCheck = new CheckBox
        {
            Content = "Don't show again",
            IsChecked = false, // 기본: 언체크 (= 계속 알림)
            Foreground = new SolidColorBrush(Color.FromRgb(0xCC, 0xCC, 0xCC)),
            FontFamily = new FontFamily("맑은 고딕"),
            FontSize = 11,
            VerticalAlignment = VerticalAlignment.Center,
        };
        DockPanel.SetDock(_alertCheck, Dock.Left);
        bottomRow.Children.Add(_alertCheck);

        // 확인 button
        var btnBorder = new Border
        {
            Background = new SolidColorBrush(Color.FromRgb(0x55, 0x55, 0x55)),
            CornerRadius = new CornerRadius(4),
            Padding = new Thickness(16, 4, 16, 4),
            Cursor = Cursors.Hand,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(10, 0, 0, 0),
        };
        var btnText = new TextBlock
        {
            Text = "확인",
            Foreground = Brushes.White,
            FontFamily = new FontFamily("맑은 고딕"),
            FontSize = 11,
        };
        btnBorder.Child = btnText;
        btnBorder.MouseEnter += (_, _) =>
            btnBorder.Background = new SolidColorBrush(Color.FromRgb(0x77, 0x77, 0x77));
        btnBorder.MouseLeave += (_, _) =>
            btnBorder.Background = new SolidColorBrush(Color.FromRgb(0x55, 0x55, 0x55));
        btnBorder.MouseLeftButtonUp += (_, _) => OnDismiss();
        DockPanel.SetDock(btnBorder, Dock.Right);
        bottomRow.Children.Add(btnBorder);

        stack.Children.Add(bottomRow);
        outerBorder.Child = stack;
        Content = outerBorder;

        // 확인 버튼 눌러야만 닫힘 (자동 닫힘 없음)
        Loaded += (_, _) => PlayWarningTone();
    }

    // ── Warning tone: 짧은 하행 2음 경고 (G5→D5, ~200ms) ──
    private static void PlayWarningTone()
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                const int rate = 22050;
                (double hz, int ms)[] melody =
                {
                    (783.99, 90),   // G5 — 높은 경고음
                    (587.33, 130),  // D5 — 하행 (단3도 아래, 긴장감)
                };

                var pcm = new System.Collections.Generic.List<short>();
                foreach (var (hz, ms) in melody)
                {
                    int n = rate * ms / 1000;
                    for (int i = 0; i < n; i++)
                    {
                        double t = (double)i / rate;
                        double env = Math.Min(1.0, Math.Min(i / 44.0, (n - i) / (n * 0.3)));
                        double wave = Math.Sin(2 * Math.PI * hz * t)
                                    + 0.2 * Math.Sin(4 * Math.PI * hz * t);
                        pcm.Add((short)(6000 * env * wave));
                    }
                    // 15ms gap
                    for (int i = 0; i < rate * 15 / 1000; i++)
                        pcm.Add(0);
                }

                using var wav = new MemoryStream();
                using var bw = new BinaryWriter(wav);
                int dataLen = pcm.Count * 2;
                bw.Write(Encoding.ASCII.GetBytes("RIFF"));
                bw.Write(36 + dataLen);
                bw.Write(Encoding.ASCII.GetBytes("WAVEfmt "));
                bw.Write(16);
                bw.Write((short)1);   // PCM
                bw.Write((short)1);   // mono
                bw.Write(rate);
                bw.Write(rate * 2);
                bw.Write((short)2);   // block align
                bw.Write((short)16);  // bits
                bw.Write(Encoding.ASCII.GetBytes("data"));
                bw.Write(dataLen);
                foreach (var s in pcm) bw.Write(s);

                wav.Position = 0;
                using var player = new SoundPlayer(wav);
                player.PlaySync();
            }
            catch { /* audio not available */ }
        });
    }

    private void OnDismiss()
    {
        // "Don't show again" 체크됨 → suppress=true (alertAgain=false)
        bool dontShowAgain = _alertCheck.IsChecked == true;
        Dismissed?.Invoke(!dontShowAgain); // true=계속알림, false=억제
        Dispatcher.InvokeShutdown();
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        var helper = new WindowInteropHelper(this);

        // Set owner to target app's main window (cross-process Win32 ownership)
        if (_ownerHwnd != IntPtr.Zero)
            SetWindowLongPtr(helper.Handle, GWL_HWNDPARENT, _ownerHwnd);

        // WS_EX_NOACTIVATE: clicking won't steal focus from any app
        var exStyle = GetWindowLongPtr(helper.Handle, GWL_EXSTYLE);
        SetWindowLongPtr(helper.Handle, GWL_EXSTYLE,
            new IntPtr(exStyle.ToInt64() | WS_EX_NOACTIVATE));
    }

    // ── Win32 P/Invoke ──
    private const int GWL_EXSTYLE = -20;
    private const int GWL_HWNDPARENT = -8;
    private const int WS_EX_NOACTIVATE = 0x08000000;

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
}

/// <summary>
/// Fire-and-forget STA host for FocuslessWarningWindow.
/// 프로세스별 알림 억제 관리 (다시 알림 체크박스로 제어).
/// Tag: [FL] [READINESS]
/// </summary>
internal static class FocuslessWarningOverlay
{
    // 프로세스별 알림 억제 Set (언체크+확인 시 추가)
    private static readonly ConcurrentDictionary<string, bool> _suppressed = new();

    /// <summary>
    /// 포커스리스 경고 알림 표시 (fire-and-forget, non-blocking).
    /// 해당 프로세스의 알림이 억제된 상태면 표시하지 않음.
    /// </summary>
    public static void Show(IntPtr ownerHwnd, string? detail, string? processName)
    {
        var procName = processName ?? "unknown";

        // 알림 억제 체크
        if (_suppressed.ContainsKey(procName))
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  [FL] Warning suppressed for {procName}");
            Console.ResetColor();
            return;
        }

        var detailText = detail ?? "(unknown)";
        var thread = new Thread(() =>
        {
            try
            {
            var window = new FocuslessWarningWindow(ownerHwnd, detailText, procName);

            // 알림 해제 콜백
            window.Dismissed += (alertAgain) =>
            {
                if (!alertAgain)
                {
                    _suppressed[procName] = true;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"  [FL] Warning suppressed for {procName} (user choice)");
                    Console.ResetColor();
                }
            };

            // Position: 타겟 윈도우 상단에 표시
            try
            {
                if (ownerHwnd != IntPtr.Zero && GetWindowRect(ownerHwnd, out var rect))
                {
                    window.Left = rect.Left + 20;
                    window.Top = rect.Top + 20;
                }
                else
                {
                    window.Left = 100;
                    window.Top = 100;
                }
            }
            catch
            {
                window.Left = 100;
                window.Top = 100;
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  [FL] Popup showing at ({window.Left},{window.Top}) owner=0x{ownerHwnd:X}");
            Console.ResetColor();

            window.Show();
            Dispatcher.Run();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  [FL] Popup FAILED: {ex.GetType().Name}: {ex.Message}");
                Console.ResetColor();
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.IsBackground = false; // MUST be foreground: keep process alive until popup closes (5s auto)
        thread.Name = "FocuslessWarning-STA";
        thread.Start();
    }

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left, Top, Right, Bottom;
    }
}
