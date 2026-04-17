using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

// partial class: wkappbot speak "text" -- Windows TTS 카라오케 오버레이
internal partial class Program
{
    internal record SpeakMarker(int CharPos, int CharCount, TimeSpan AudioPos);

    static int SpeakCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot speak \"text\" [--bg] [--mouse] [--target <grap>]");
            Console.WriteLine("  Windows TTS 음성 출력 + 카라오케 오버레이");
            Console.WriteLine("  --bg: 백그라운드 실행 (즉시 리턴)");
            Console.WriteLine("  --size N: 글자 크기 px (기본 32, 활성 1.5배)");
            Console.WriteLine("  --mouse: 마우스 커서 위치에 오버레이 표시");
            Console.WriteLine("  --target <grap>: 지정 윈도우 위에 오버레이 표시");
            return 1;
        }

        var argList = args.ToList();
        bool bg = argList.Remove("--bg");
        bool atMouse = argList.Remove("--mouse");

        // --target <grap> 파싱
        string? targetGrap = null;
        int targetIdx = argList.IndexOf("--target");
        if (targetIdx >= 0 && targetIdx + 1 < argList.Count)
        {
            targetGrap = argList[targetIdx + 1];
            argList.RemoveRange(targetIdx, 2);
        }

        // --size N 파싱 (px 단위)
        double sizePx = 0;
        int sizeIdx = argList.IndexOf("--size");
        if (sizeIdx >= 0 && sizeIdx + 1 < argList.Count)
        {
            double.TryParse(argList[sizeIdx + 1], out sizePx);
            sizePx = Math.Clamp(sizePx, 16, 200);
            argList.RemoveRange(sizeIdx, 2);
        }
        if (bg)
        {
            var exe = Environment.ProcessPath ?? "wkappbot";
            var textArgs = string.Join(" ", argList.Select(a => $"\"{a}\""));
            var sizeArg = sizePx > 0 ? $" --size {sizePx}" : "";
            var mouseArg = atMouse ? " --mouse" : "";
            var targetArg = targetGrap != null ? $" --target \"{targetGrap}\"" : "";
            AppBotPipe.Spawn(exe, $"speak {textArgs}{sizeArg}{mouseArg}{targetArg}", Environment.CurrentDirectory, caller: "SPEAK");
            return 0;
        }

        var text = string.Join(" ", argList).Replace("\\!", "!").Replace("\\?", "?");
        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("[SPEAK] 텍스트가 비어있습니다.");
            return 1;
        }

        // Serialize concurrent speak calls -- only one TTS at a time.
        // Without this, parallel callers produce overlapping audio.
        using var speakMutex = new System.Threading.Mutex(false, "Global\\WKAppBotSpeak");
        try { speakMutex.WaitOne(); }
        catch (System.Threading.AbandonedMutexException) { /* previous speaker crashed -- take over */ }

        // 1) TTS WAV 렌더링 + 타이밍 마커 수집
        var markers = new List<SpeakMarker>();
        var tmpFile = RenderTtsWav(text, markers, gain: 3.0f);

        // 2) 공유 Stopwatch -- 오디오 재생 직전에 Start
        var sharedStopwatch = new Stopwatch();

        // 3) 오버레이 위치 결정
        System.Drawing.Point? mousePos = null;
        IntPtr targetHwnd = IntPtr.Zero;
        if (atMouse)
        {
            if (NativeMethods.GetCursorPos(out var pt))
                mousePos = new System.Drawing.Point(pt.X, pt.Y);
        }
        if (targetGrap != null)
        {
            try
            {
                var wins = WKAppBot.Win32.Window.WindowFinder.FindWindows(targetGrap);
                if (wins.Count > 0) targetHwnd = wins[0].Handle;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[SPEAK] --target 윈도우 탐색 실패: {ex.Message}");
            }
        }

        var (myHwnd, panelRect) = FindMyWindow();
        var overlayThread = ShowSpeakOverlay(text, markers, sharedStopwatch, sizePx,
            targetHwnd != IntPtr.Zero ? targetHwnd : myHwnd, panelRect, mousePos);

        // 4) 오디오 재생 -- Stopwatch 시작과 동시에
        try
        {
            var player = new System.Media.SoundPlayer(tmpFile);
            player.Load(); // WAV 미리 버퍼링 -> PlaySync 즉시 재생
            sharedStopwatch.Start(); // 오버레이와 동기화 포인트!
            player.PlaySync();
            player.Dispose();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SPEAK] TTS 실패: {ex.Message}");
        }
        try { File.Delete(tmpFile); } catch { }

        if (overlayThread.IsAlive)
            overlayThread.Join();

        Console.Error.WriteLine($"[SPEAK] {text}");
        return 0;
    }

    // -- 내 클롣 창 찾기 --

    static (IntPtr hwnd, System.Drawing.Rectangle? panelRect) FindMyWindow()
    {
        try
        {
            var cwd = Environment.CurrentDirectory;
            var folderName = Path.GetFileName(cwd);
            if (string.IsNullOrEmpty(folderName)) return (IntPtr.Zero, null);

            IntPtr found = IntPtr.Zero;
            NativeMethods.EnumWindows((NativeMethods.EnumWindowsProc)((hwnd, _) =>
            {
                if (!NativeMethods.IsWindowVisible(hwnd)) return true;
                var title = NativeMethods.GetWindowTextW(hwnd);
                if (title.Contains(folderName, StringComparison.OrdinalIgnoreCase)
                    && title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase))
                {
                    found = hwnd;
                    return false;
                }
                return true;
            }), IntPtr.Zero);

            if (found == IntPtr.Zero) return (IntPtr.Zero, null);

            try
            {
                using var automation = new FlaUI.UIA3.UIA3Automation();
                var root = automation.FromHandle(found);
                var claudeEl = root.FindFirstDescendant(cf =>
                    cf.ByName("Claude").Or(cf.ByName("claude")));
                if (claudeEl != null)
                {
                    var br = claudeEl.BoundingRectangle;
                    if (br.Width > 50 && br.Height > 50)
                        return (found, br);
                }
            }
            catch { }

            return (found, null);
        }
        catch { return (IntPtr.Zero, null); }
    }

    // -- TTS -> WAV + 게인 증폭 + 타이밍 수집 --

    static string RenderTtsWav(string text, List<SpeakMarker> markers, float gain = 3.0f)
    {
        var synth = new SpeechSynthesizer();
        synth.Volume = 100;
        synth.Rate = 1;
        foreach (var voice in synth.GetInstalledVoices())
        {
            if (voice.VoiceInfo.Culture.Name.StartsWith("ko"))
            {
                synth.SelectVoice(voice.VoiceInfo.Name);
                break;
            }
        }

        synth.SpeakProgress += (_, e) =>
        {
            markers.Add(new SpeakMarker(e.CharacterPosition, e.CharacterCount, e.AudioPosition));
        };

        var tmpFile = Path.Combine(Path.GetTempPath(), "wkappbot_speak.wav");
        synth.SetOutputToWaveFile(tmpFile);
        synth.Speak(text);
        synth.SetOutputToNull();
        synth.Dispose();

        // WAV 게인 증폭
        var wav = File.ReadAllBytes(tmpFile);
        int dataOffset = 44;
        for (int i = 0; i < wav.Length - 4; i++)
        {
            if (wav[i] == 'd' && wav[i + 1] == 'a' && wav[i + 2] == 't' && wav[i + 3] == 'a')
            {
                dataOffset = i + 8;
                break;
            }
        }
        for (int i = dataOffset; i < wav.Length - 1; i += 2)
        {
            short sample = (short)(wav[i] | (wav[i + 1] << 8));
            int amplified = (int)(sample * gain);
            amplified = Math.Clamp(amplified, short.MinValue, short.MaxValue);
            wav[i] = (byte)(amplified & 0xFF);
            wav[i + 1] = (byte)((amplified >> 8) & 0xFF);
        }
        File.WriteAllBytes(tmpFile, wav);
        return tmpFile;
    }

    // -- 카라오케 오버레이 --

    static Thread ShowSpeakOverlay(string text, List<SpeakMarker> markers,
        Stopwatch sharedStopwatch, double sizePx = 1.0, IntPtr ownerHwnd = default,
        System.Drawing.Rectangle? panelRect = null, System.Drawing.Point? mousePos = null)
    {
        var thread = new Thread(() =>
        {
            try
            {
                var window = new SpeakOverlayWindow(text, markers, sharedStopwatch, sizePx, ownerHwnd, panelRect, mousePos);
                window.Show();
                Dispatcher.Run();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[SPEAK] Overlay failed: {ex.Message}");
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.IsBackground = true;
        thread.Name = "SpeakOverlay-STA";
        thread.Start();
        return thread;
    }
}

/// <summary>
/// 카라오케 오버레이: 발음 중인 글자가 크게 + 노란색.
/// 한 줄 가로 스크롤, 현재 글자가 항상 중앙.
/// </summary>
internal sealed class SpeakOverlayWindow : Window
{
    private readonly IntPtr _ownerHwnd;
    private readonly System.Drawing.Rectangle? _panelRect;
    private readonly System.Drawing.Point? _mousePos;
    private readonly List<Program.SpeakMarker> _markers;
    private readonly Stopwatch _stopwatch; // 메인 스레드와 공유
    private readonly ScrollViewer _scrollViewer;
    private readonly StackPanel _textPanel;
    private int _currentMarkerIndex = -1;
    private DispatcherTimer? _karaokeTimer;
    private double _scrollTarget;
    private double _scrollCurrent;

    private static readonly SolidColorBrush WaitBrush = new(Color.FromRgb(200, 205, 215));        // 대기: 소프트 실버
    private static readonly SolidColorBrush ActiveBrush = new(Color.FromRgb(255, 200, 60));     // 발음 중: 골드
    private static readonly SolidColorBrush DoneBrush = new(Color.FromRgb(80, 175, 255));       // 완료: 스카이블루
    private readonly double NormalSize;
    private readonly double ActiveSize;

    public SpeakOverlayWindow(string text, List<Program.SpeakMarker> markers,
        Stopwatch sharedStopwatch, double sizePx = 1.0, IntPtr ownerHwnd = default,
        System.Drawing.Rectangle? panelRect = null, System.Drawing.Point? mousePos = null)
    {
        _ownerHwnd = ownerHwnd;
        _panelRect = panelRect;
        _mousePos = mousePos;
        _markers = markers;
        _stopwatch = sharedStopwatch;
        NormalSize = sizePx > 0 ? sizePx : 32;
        ActiveSize = NormalSize * 1.5;

        Title = "SpeakOverlay";
        WindowStyle = WindowStyle.None;
        AllowsTransparency = true;
        Background = Brushes.Transparent;
        Topmost = true;
        ShowActivated = false;
        ShowInTaskbar = false;

        Width = 700;
        SizeToContent = SizeToContent.Height;

        var border = new Border
        {
            Background = Brushes.Transparent,
            Padding = new Thickness(28, 18, 28, 18),
        };

        _textPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
        };

        _scrollViewer = new ScrollViewer
        {
            HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
            VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
            MaxWidth = 640,
        };
        _scrollViewer.Content = _textPanel;

        var fontFamily = new FontFamily("Malgun Gothic");
        var textShadow = new DropShadowEffect
        {
            Color = Colors.Black,
            BlurRadius = 12,
            ShadowDepth = 0,
            Opacity = 0.9,
        };
        foreach (char c in text)
        {
            _textPanel.Children.Add(new TextBlock
            {
                Text = c.ToString(),
                Foreground = WaitBrush,
                FontSize = NormalSize,
                FontWeight = FontWeights.ExtraBold,
                FontFamily = fontFamily,
                Effect = textShadow,
                VerticalAlignment = VerticalAlignment.Bottom,
            });
        }

        border.Child = _scrollViewer;
        Content = border;

        SourceInitialized += (_, _) =>
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            var exStyle = GetWindowLongW(hwnd, -20);
            SetWindowLongW(hwnd, -20, exStyle | 0x08000000); // WS_EX_NOACTIVATE

            // Physical pixel coords from GetWindowRect -> divide by DPI scale for WPF Left/Top (DIP)
            uint dpi_ = GetDpiForWindow(hwnd);
            double scale_ = dpi_ > 0 ? dpi_ / 96.0 : 1.0;

            if (_mousePos.HasValue)
            {
                // Mouse cursor position -- already in physical pixels -> convert to DIP
                Left = _mousePos.Value.X / scale_ - Width / 2;
                Top  = _mousePos.Value.Y / scale_ - 80;
            }
            else if (_ownerHwnd != IntPtr.Zero && NativeMethods.GetWindowRect(_ownerHwnd, out var ownerRect))
            {
                // Center on target window (ownerRect is physical pixels -> DIP)
                double cx = (ownerRect.Left + ownerRect.Right) / 2.0 / scale_;
                double cy = (ownerRect.Top  + ownerRect.Bottom) / 2.0 / scale_;
                Left = cx - Width / 2;
                Top  = cy - 50;
            }
            else if (_panelRect.HasValue && _panelRect.Value.Width > 50)
            {
                var pr = _panelRect.Value;
                Left = pr.X / scale_ + (pr.Width / scale_ - Width) / 2;
                Top  = pr.Y / scale_ + (pr.Height / scale_ - 100) / 2;
            }
            else
            {
                Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
                Top = (SystemParameters.PrimaryScreenHeight - 100) / 2;
            }
        };

        Loaded += (_, _) =>
        {
            // 30ms 폴링으로 공유 Stopwatch 추적 (메인 스레드가 Start 호출할 때까지 대기)
            _karaokeTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(30) };
            _karaokeTimer.Tick += KaraokeTick;
            _karaokeTimer.Start();

            // 안전망: 최대 30초 후 자동 닫기
            var safetyTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(30) };
            safetyTimer.Tick += (_, _) => { safetyTimer.Stop(); BeginFadeOut(); };
            safetyTimer.Start();
        };
    }

    private void KaraokeTick(object? sender, EventArgs e)
    {
        // 이징 스크롤: 매 프레임 목표 위치로 20% 접근
        if (Math.Abs(_scrollTarget - _scrollCurrent) > 0.5)
        {
            _scrollCurrent += (_scrollTarget - _scrollCurrent) * 0.2;
            _scrollViewer.ScrollToHorizontalOffset(_scrollCurrent);
        }

        // Stopwatch가 아직 시작 안 됐으면 대기
        if (!_stopwatch.IsRunning && _stopwatch.ElapsedMilliseconds == 0) return;

        if (_markers.Count == 0) return;
        var elapsed = _stopwatch.Elapsed;

        // 마지막 마커 지나고 1.5초 후 페이드아웃
        if (_currentMarkerIndex == _markers.Count - 1)
        {
            var lastEnd = _markers[^1].AudioPos + TimeSpan.FromSeconds(1.5);
            if (elapsed > lastEnd)
            {
                _karaokeTimer?.Stop();
                BeginFadeOut();
                return;
            }
        }

        // 현재 마커 찾기
        int newIndex = _currentMarkerIndex;
        for (int i = Math.Max(0, _currentMarkerIndex + 1); i < _markers.Count; i++)
        {
            if (_markers[i].AudioPos <= elapsed)
                newIndex = i;
            else
                break;
        }

        if (newIndex == _currentMarkerIndex) return;

        // 이전 글자 -> 발음 완료 색상
        if (_currentMarkerIndex >= 0)
        {
            SetChars(_currentMarkerIndex, NormalSize, DoneBrush);
        }

        _currentMarkerIndex = newIndex;

        // 현재 발음 글자 -> 크게 + 노란색
        SetChars(_currentMarkerIndex, ActiveSize, ActiveBrush);

        // 가로 스크롤 타겟 설정 (이징은 KaraokeTick에서 매 프레임 처리)
        var curM = _markers[_currentMarkerIndex];
        if (curM.CharPos < _textPanel.Children.Count)
        {
            var targetTb = (TextBlock)_textPanel.Children[curM.CharPos];
            var pos = targetTb.TranslatePoint(new Point(0, 0), _textPanel);
            _scrollTarget = Math.Max(0, pos.X - _scrollViewer.ViewportWidth / 2 + targetTb.ActualWidth / 2);
        }
    }

    private static readonly DropShadowEffect NormalShadow = new()
    {
        Color = Colors.Black, BlurRadius = 12, ShadowDepth = 0, Opacity = 0.9,
    };
    private static readonly DropShadowEffect GlowShadow = new()
    {
        Color = Color.FromRgb(255, 220, 80), BlurRadius = 18, ShadowDepth = 0, Opacity = 0.7,
    };

    private void SetChars(int markerIndex, double size, SolidColorBrush brush)
    {
        var cur = _markers[markerIndex];
        int start = cur.CharPos;
        int end = markerIndex + 1 < _markers.Count
            ? _markers[markerIndex + 1].CharPos
            : _textPanel.Children.Count;
        bool isActive = (brush == ActiveBrush);
        for (int c = start; c < end && c < _textPanel.Children.Count; c++)
        {
            var tb = (TextBlock)_textPanel.Children[c];
            tb.FontSize = size;
            tb.Foreground = brush;
            tb.Effect = isActive ? GlowShadow : NormalShadow;
        }
    }

    private void BeginFadeOut()
    {
        var fade = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(500));
        fade.Completed += (_, _) =>
        {
            Close();
            Dispatcher.InvokeShutdown();
        };
        BeginAnimation(OpacityProperty, fade);
    }

    [DllImport("user32.dll")] private static extern int GetWindowLongW(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")] private static extern int SetWindowLongW(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll")] private static extern uint GetDpiForWindow(IntPtr hWnd);
}
