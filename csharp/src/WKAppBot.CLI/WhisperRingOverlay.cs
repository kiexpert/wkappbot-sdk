using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WpfPath = System.Windows.Shapes.Path;
using WpfEllipse = System.Windows.Shapes.Ellipse;
using System.Windows.Threading;

namespace WKAppBot.CLI;

/// <summary>
/// "Whisper Spectrum Ring" — radial HUD overlay for real-time FFT visualization.
/// 8 arc segments around a circle, each representing a frequency band.
/// Center shows mode (VOICE/WHISPER/QUIET/NOISE) + current token hex.
/// Recent tokens scroll along the bottom.
///
/// Design: Cybernetic ring — compact, always-on-top, non-interactive.
/// Tag: [WHISPER]
/// </summary>
internal sealed class WhisperRingWindow : Window
{
    private const int BandCount = 8;
    private const double RingRadius = 52;     // outer ring radius
    private const double RingThickness = 14;  // max arc thickness at full energy
    private const double GapAngle = 4;        // degrees gap between arcs

    private readonly Canvas _canvas;
    private readonly WpfPath[] _arcPaths = new WpfPath[BandCount];
    private readonly TextBlock _modeText;
    private readonly TextBlock _tokenText;
    private readonly TextBlock _recentText;
    private readonly TextBlock _sttText;    // STT recognized word(s)
    private readonly WpfEllipse _coreGlow;
    private readonly WpfEllipse _coreDot;
    private readonly Canvas _pointerLayer;                      // pointer-only layer (blur without affecting text)
    private readonly System.Windows.Shapes.Line _pointer;      // energy centroid pointer (current)
    private readonly WpfEllipse _pointerDot;                   // pointer tip dot (current)

    // Ghost trail — 4 afterimage lines that fade+blur like dissipating smoke
    private const int GhostCount = 4;
    private readonly System.Windows.Shapes.Line[] _ghosts = new System.Windows.Shapes.Line[GhostCount];
    private readonly WpfEllipse[] _ghostDots = new WpfEllipse[GhostCount];
    private readonly double[] _ghostAge = new double[GhostCount]; // 0=fresh, 1=gone
    private int _ghostCursor = 0;                                 // ring buffer index

    // Band colors — warm-to-cool gradient matching articulatory zones
    private static readonly Color[] BandColors = [
        Color.FromRgb(0xFF, 0x44, 0x44), // 0 VxLip — red (vocal cord)
        Color.FromRgb(0xFF, 0x88, 0x33), // 1 Pharx — orange
        Color.FromRgb(0xFF, 0xCC, 0x22), // 2 Velar — yellow
        Color.FromRgb(0x44, 0xFF, 0x88), // 3 OralR — green
        Color.FromRgb(0x33, 0xCC, 0xFF), // 4 HiRes — cyan
        Color.FromRgb(0x44, 0x88, 0xFF), // 5 Burst — blue
        Color.FromRgb(0x88, 0x44, 0xFF), // 6 Sibil — purple
        Color.FromRgb(0xCC, 0x44, 0xFF), // 7 Breth — magenta
    ];

    // Mode colors
    private static readonly SolidColorBrush ModeBrushVoice = new(Color.FromRgb(0xFF, 0xCC, 0x22));
    private static readonly SolidColorBrush ModeBrushWhisper = new(Color.FromRgb(0x44, 0xFF, 0x88));
    private static readonly SolidColorBrush ModeBrushQuiet = new(Color.FromRgb(0x55, 0x55, 0x66));
    private static readonly SolidColorBrush ModeBrushNoise = new(Color.FromRgb(0xFF, 0x66, 0x44));

    static WhisperRingWindow()
    {
        ModeBrushVoice.Freeze();
        ModeBrushWhisper.Freeze();
        ModeBrushQuiet.Freeze();
        ModeBrushNoise.Freeze();
    }

    public WhisperRingWindow()
    {
        const double size = 180;
        Title = "WhisperRing";
        Width = size;
        Height = size + 22; // extra for recent tokens text
        WindowStyle = WindowStyle.None;
        AllowsTransparency = true;
        Background = Brushes.Transparent;
        Topmost = true;
        ShowInTaskbar = false;
        ShowActivated = false;
        ResizeMode = ResizeMode.NoResize;
        Opacity = 0.88;

        var root = new Grid();

        // Main canvas for ring
        _canvas = new Canvas
        {
            Width = size,
            Height = size,
        };

        double cx = size / 2, cy = size / 2;

        // Background circle (dark, subtle)
        var bgCircle = new WpfEllipse
        {
            Width = RingRadius * 2 + 8,
            Height = RingRadius * 2 + 8,
            Fill = new SolidColorBrush(Color.FromArgb(0xDD, 0x12, 0x12, 0x1E)),
            Stroke = new SolidColorBrush(Color.FromArgb(0x44, 0x88, 0x88, 0xAA)),
            StrokeThickness = 1,
        };
        Canvas.SetLeft(bgCircle, cx - RingRadius - 4);
        Canvas.SetTop(bgCircle, cy - RingRadius - 4);
        _canvas.Children.Add(bgCircle);

        // Core glow (pulsing center dot)
        _coreGlow = new WpfEllipse
        {
            Width = 28,
            Height = 28,
            Fill = new RadialGradientBrush(
                Color.FromArgb(0x44, 0x44, 0xFF, 0x88),
                Color.FromArgb(0x00, 0x44, 0xFF, 0x88)),
        };
        Canvas.SetLeft(_coreGlow, cx - 14);
        Canvas.SetTop(_coreGlow, cy - 14);
        _canvas.Children.Add(_coreGlow);

        _coreDot = new WpfEllipse
        {
            Width = 8,
            Height = 8,
            Fill = new SolidColorBrush(Color.FromRgb(0x44, 0xFF, 0x88)),
        };
        Canvas.SetLeft(_coreDot, cx - 4);
        Canvas.SetTop(_coreDot, cy - 4);
        _canvas.Children.Add(_coreDot);

        // ── Pointer layer (separate Canvas so blur only affects needles) ──
        _pointerLayer = new Canvas { Width = size, Height = size };

        // Ghost trail lines (oldest first, rendered behind current pointer)
        for (int g = 0; g < GhostCount; g++)
        {
            _ghosts[g] = new System.Windows.Shapes.Line
            {
                X1 = cx, Y1 = cy, X2 = cx, Y2 = cy,
                StrokeThickness = 2,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                Opacity = 0,
                Effect = new System.Windows.Media.Effects.BlurEffect { Radius = 0 },
            };
            _pointerLayer.Children.Add(_ghosts[g]);

            _ghostDots[g] = new WpfEllipse
            {
                Width = 6, Height = 6, Opacity = 0,
                Effect = new System.Windows.Media.Effects.BlurEffect { Radius = 0 },
            };
            _pointerLayer.Children.Add(_ghostDots[g]);

            _ghostAge[g] = 1.0; // start fully aged (invisible)
        }

        // Current pointer (on top of ghosts)
        _pointer = new System.Windows.Shapes.Line
        {
            X1 = cx, Y1 = cy, X2 = cx, Y2 = cy,
            Stroke = new SolidColorBrush(Color.FromArgb(0xCC, 0xFF, 0xFF, 0xFF)),
            StrokeThickness = 2,
            StrokeStartLineCap = PenLineCap.Round,
            StrokeEndLineCap = PenLineCap.Round,
            Opacity = 0,
        };
        _pointerLayer.Children.Add(_pointer);

        _pointerDot = new WpfEllipse
        {
            Width = 6, Height = 6,
            Fill = new SolidColorBrush(Color.FromRgb(0xFF, 0xFF, 0xFF)),
            Opacity = 0,
        };
        _pointerLayer.Children.Add(_pointerDot);

        _canvas.Children.Add(_pointerLayer);

        // Create 8 arc segment paths
        double arcSpan = (360.0 - BandCount * GapAngle) / BandCount;
        for (int i = 0; i < BandCount; i++)
        {
            var path = new WpfPath
            {
                StrokeThickness = 3, // minimum visible thickness
                Stroke = new SolidColorBrush(Color.FromArgb(0x66, BandColors[i].R, BandColors[i].G, BandColors[i].B)),
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    Color = BandColors[i],
                    BlurRadius = 6,
                    ShadowDepth = 0,
                    Opacity = 0.5,
                },
            };
            _arcPaths[i] = path;
            _canvas.Children.Add(path);

            // Set initial arc geometry
            double startAngle = i * (arcSpan + GapAngle) - 90; // start from top
            UpdateArc(i, startAngle, arcSpan, 3, cx, cy);
        }

        // Mode text (center)
        _modeText = new TextBlock
        {
            Text = "QUIET",
            Foreground = ModeBrushQuiet,
            FontFamily = new FontFamily("Consolas"),
            FontSize = 11,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
        };
        Canvas.SetLeft(_modeText, cx - 28);
        Canvas.SetTop(_modeText, cy - 22);
        _canvas.Children.Add(_modeText);

        // Token hex (center, below mode)
        _tokenText = new TextBlock
        {
            Text = "00000000",
            Foreground = new SolidColorBrush(Color.FromRgb(0x99, 0x99, 0xAA)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 9,
            TextAlignment = TextAlignment.Center,
        };
        Canvas.SetLeft(_tokenText, cx - 28);
        Canvas.SetTop(_tokenText, cy + 6);
        _canvas.Children.Add(_tokenText);

        // STT text (inside ring, below token hex)
        _sttText = new TextBlock
        {
            Text = "",
            Foreground = Brushes.White,
            FontFamily = new FontFamily("Malgun Gothic, Consolas"),
            FontSize = 13,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
            Opacity = 0,
        };
        Canvas.SetLeft(_sttText, cx - 50);
        Canvas.SetTop(_sttText, cy + 18);
        _sttText.Width = 100;
        _canvas.Children.Add(_sttText);

        root.Children.Add(_canvas);

        // Recent tokens (bottom bar)
        _recentText = new TextBlock
        {
            Text = "",
            Foreground = new SolidColorBrush(Color.FromRgb(0x66, 0x66, 0x77)),
            FontFamily = new FontFamily("Consolas"),
            FontSize = 8,
            TextAlignment = TextAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom,
            Margin = new Thickness(4, 0, 4, 2),
        };
        root.Children.Add(_recentText);

        Content = root;
    }

    /// <summary>Update a single arc segment geometry.</summary>
    private void UpdateArc(int bandIndex, double startAngleDeg, double sweepDeg, double thickness, double cx, double cy)
    {
        double r = RingRadius;
        double startRad = startAngleDeg * Math.PI / 180;
        double endRad = (startAngleDeg + sweepDeg) * Math.PI / 180;

        var startPoint = new Point(cx + r * Math.Cos(startRad), cy + r * Math.Sin(startRad));
        var endPoint = new Point(cx + r * Math.Cos(endRad), cy + r * Math.Sin(endRad));
        bool isLargeArc = sweepDeg > 180;

        var figure = new PathFigure { StartPoint = startPoint, IsClosed = false };
        figure.Segments.Add(new ArcSegment
        {
            Point = endPoint,
            Size = new Size(r, r),
            IsLargeArc = isLargeArc,
            SweepDirection = SweepDirection.Clockwise,
        });

        var geometry = new PathGeometry();
        geometry.Figures.Add(figure);

        _arcPaths[bandIndex].Data = geometry;
        _arcPaths[bandIndex].StrokeThickness = Math.Max(2, thickness);
    }

    /// <summary>
    /// Update all bands + mode from whisper engine data.
    /// Called from WhisperRingHost on the WPF dispatcher thread.
    /// </summary>
    public void UpdateSpectrum(int[] levels, int maxLevel, string mode, uint token, string recentTokens,
        string? sttText = null, long sttAgeTicks = long.MaxValue, string sttMode = "QUIET")
    {
        double cx = _canvas.Width / 2, cy = _canvas.Height / 2;
        double arcSpan = (360.0 - BandCount * GapAngle) / BandCount;

        for (int i = 0; i < BandCount && i < levels.Length; i++)
        {
            double startAngle = i * (arcSpan + GapAngle) - 90;
            double energy = maxLevel > 0 ? (double)levels[i] / maxLevel : 0;

            // Thickness: 2px (silent) to RingThickness (full energy)
            double thickness = 2 + energy * (RingThickness - 2);

            // Color intensity: dim when low, bright when high
            byte alpha = (byte)(0x44 + (int)(energy * 0xBB));
            var color = BandColors[i];
            _arcPaths[i].Stroke = new SolidColorBrush(Color.FromArgb(alpha, color.R, color.G, color.B));

            // Glow intensity
            if (_arcPaths[i].Effect is System.Windows.Media.Effects.DropShadowEffect glow)
                glow.Opacity = 0.2 + energy * 0.8;

            UpdateArc(i, startAngle, arcSpan, thickness, cx, cy);
        }

        // ── Energy centroid pointer (articulatory position indicator) ──
        // Weighted average of band angles by energy → shows dominant articulation point
        double totalEnergy = 0;
        double vecX = 0, vecY = 0;
        for (int i = 0; i < BandCount && i < levels.Length; i++)
        {
            double energy = maxLevel > 0 ? (double)levels[i] / maxLevel : 0;
            double midAngle = (i * (arcSpan + GapAngle) + arcSpan / 2 - 90) * Math.PI / 180;
            vecX += energy * Math.Cos(midAngle);
            vecY += energy * Math.Sin(midAngle);
            totalEnergy += energy;
        }

        if (totalEnergy > 0.3 && mode != "QUIET")
        {
            double angle = Math.Atan2(vecY, vecX);
            double magnitude = Math.Sqrt(vecX * vecX + vecY * vecY) / totalEnergy;
            double pointerLen = 10 + magnitude * (RingRadius - 16);

            double tipX = cx + pointerLen * Math.Cos(angle);
            double tipY = cy + pointerLen * Math.Sin(angle);

            // ── Spawn ghost: snapshot current pointer position before moving ──
            if (_pointer.Opacity > 0.1)
            {
                var g = _ghosts[_ghostCursor];
                var gd = _ghostDots[_ghostCursor];
                g.X1 = _pointer.X1; g.Y1 = _pointer.Y1;
                g.X2 = _pointer.X2; g.Y2 = _pointer.Y2;
                g.Stroke = _pointer.Stroke;  // inherit color
                g.Opacity = _pointer.Opacity * 0.6;
                gd.Fill = _pointerDot.Fill;
                Canvas.SetLeft(gd, Canvas.GetLeft(_pointerDot));
                Canvas.SetTop(gd, Canvas.GetTop(_pointerDot));
                gd.Opacity = 0.5;
                _ghostAge[_ghostCursor] = 0; // fresh
                _ghostCursor = (_ghostCursor + 1) % GhostCount;
            }

            // ── Move current pointer ──
            _pointer.X1 = cx; _pointer.Y1 = cy;
            _pointer.X2 = tipX; _pointer.Y2 = tipY;

            int domBand = 0;
            for (int i = 1; i < BandCount && i < levels.Length; i++)
                if (levels[i] > levels[domBand]) domBand = i;
            var pColor = BandColors[domBand];
            _pointer.Stroke = new SolidColorBrush(Color.FromArgb(0xCC, pColor.R, pColor.G, pColor.B));

            Canvas.SetLeft(_pointerDot, tipX - 3);
            Canvas.SetTop(_pointerDot, tipY - 3);
            _pointerDot.Fill = new SolidColorBrush(pColor);

            _pointer.Opacity = 0.7 + magnitude * 0.3;
            _pointerDot.Opacity = 1.0;
        }
        else
        {
            // Current pointer fades
            _pointer.Opacity = Math.Max(0, _pointer.Opacity - 0.08);
            _pointerDot.Opacity = Math.Max(0, _pointerDot.Opacity - 0.08);
        }

        // ── Age all ghosts: thicken + blur + fade = smoke dissipation ──
        for (int g = 0; g < GhostCount; g++)
        {
            if (_ghostAge[g] >= 1.0) continue; // already gone
            _ghostAge[g] += 0.06; // ~0.55s full fade @ 30fps (slower for more visible spread)
            if (_ghostAge[g] >= 1.0) _ghostAge[g] = 1.0;

            double age = _ghostAge[g]; // 0=fresh → 1=gone
            double fade = 1.0 - age;
            _ghosts[g].Opacity = fade * 0.6;
            _ghostDots[g].Opacity = fade * 0.5;

            // Thicken as ghost ages — gives blur more "material" to spread
            _ghosts[g].StrokeThickness = 2 + age * 10;       // 2px → 12px
            _ghostDots[g].Width = _ghostDots[g].Height = 6 + age * 14; // 6px → 20px

            // Blur expands as ghost ages — smoke spreading effect
            if (_ghosts[g].Effect is System.Windows.Media.Effects.BlurEffect gb1) gb1.Radius = age * 18;
            if (_ghostDots[g].Effect is System.Windows.Media.Effects.BlurEffect gb2) gb2.Radius = age * 16;
        }

        // Mode indicator
        _modeText.Text = mode;
        _modeText.Foreground = mode switch
        {
            "VOICE" => ModeBrushVoice,
            "WHSPR" => ModeBrushWhisper,
            "NOISE" => ModeBrushNoise,
            _ => ModeBrushQuiet,
        };

        // Core dot color matches mode
        var coreColor = mode switch
        {
            "VOICE" => Color.FromRgb(0xFF, 0xCC, 0x22),
            "WHSPR" => Color.FromRgb(0x44, 0xFF, 0x88),
            "NOISE" => Color.FromRgb(0xFF, 0x66, 0x44),
            _ => Color.FromRgb(0x44, 0x44, 0x55),
        };
        _coreDot.Fill = new SolidColorBrush(coreColor);
        (_coreGlow.Fill as RadialGradientBrush)?.GradientStops.Clear();
        if (_coreGlow.Fill is RadialGradientBrush rg)
        {
            rg = new RadialGradientBrush(
                Color.FromArgb(0x44, coreColor.R, coreColor.G, coreColor.B),
                Color.FromArgb(0x00, coreColor.R, coreColor.G, coreColor.B));
            _coreGlow.Fill = rg;
        }

        // Token hex — cyan for readability
        _tokenText.Text = $"0x{token:X8}";
        _tokenText.Foreground = mode == "WHSPR"
            ? new SolidColorBrush(Color.FromRgb(0x88, 0xFF, 0xBB))
            : new SolidColorBrush(Color.FromRgb(0x33, 0xCC, 0xFF));

        // Recent tokens
        _recentText.Text = recentTokens;

        // Bottom text: mode label (primary) / STT word (override) / clock (quiet)
        double sttAgeSec = sttAgeTicks / (double)TimeSpan.TicksPerSecond;
        bool hasStt = !string.IsNullOrEmpty(sttText) && sttAgeSec < 5.0;

        if (hasStt)
        {
            // STT recognized → show word briefly (fades over 5s)
            _sttText.Text = sttText!.Length > 12 ? sttText[..12] : sttText;
            _sttText.Opacity = Math.Max(0, 1.0 - sttAgeSec / 5.0);
            _sttText.Foreground = Brushes.White;
        }
        else if (mode == "QUIET")
        {
            // Quiet → clock
            _sttText.Text = DateTime.Now.ToString("HH:mm:ss");
            _sttText.Foreground = new SolidColorBrush(Color.FromRgb(0x33, 0xCC, 0xFF));
            _sttText.Opacity = 0.7;
        }
        else
        {
            // Active sound, no recent STT → clock (mode already shown by ring colors)
            _sttText.Text = DateTime.Now.ToString("HH:mm:ss");
            _sttText.Foreground = new SolidColorBrush(Color.FromRgb(0x33, 0xCC, 0xFF));
            _sttText.Opacity = 0.5;
        }
    }

    /// <summary>Fade out and close.</summary>
    public void BeginFadeOut(int delayMs = 1000, int fadeDurationMs = 600)
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

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        var helper = new WindowInteropHelper(this);

        // WS_EX_NOACTIVATE | WS_EX_TRANSPARENT: click-through + no focus steal
        var exStyle = GetWindowLongPtr(helper.Handle, GWL_EXSTYLE);
        SetWindowLongPtr(helper.Handle, GWL_EXSTYLE,
            new IntPtr(exStyle.ToInt64() | WS_EX_NOACTIVATE | WS_EX_TRANSPARENT));
    }

    public void EnsureTopmost()
    {
        try
        {
            var helper = new WindowInteropHelper(this);
            if (helper.Handle != IntPtr.Zero)
                SetWindowPos(helper.Handle, HWND_TOPMOST, 0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        }
        catch { /* best effort */ }
    }

    // Win32
    private const int GWL_EXSTYLE = -20;
    private const int WS_EX_NOACTIVATE = 0x08000000;
    private const int WS_EX_TRANSPARENT = 0x00000020;
    private static readonly IntPtr HWND_TOPMOST = new(-1);
    private const uint SWP_NOMOVE = 0x0002;
    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOACTIVATE = 0x0010;

    [DllImport("user32.dll", EntryPoint = "GetWindowLongPtrW")]
    private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, uint uFlags);
}

/// <summary>
/// STA thread host for WhisperRingWindow.
/// Bridge between whisper engine thread and WPF dispatcher.
/// </summary>
internal sealed class WhisperRingHost : IDisposable
{
    private Thread? _uiThread;
    private WhisperRingWindow? _window;
    private Dispatcher? _dispatcher;
    private readonly ManualResetEventSlim _ready = new();

    public bool IsAlive => _uiThread?.IsAlive == true;

    /// <summary>Start the ring overlay on a dedicated STA thread.</summary>
    public void Start(int screenX, int screenY)
    {
        _uiThread = new Thread(() =>
        {
            _window = new WhisperRingWindow();
            _window.Left = screenX;
            _window.Top = screenY;
            _dispatcher = Dispatcher.CurrentDispatcher;
            _ready.Set();
            _window.Show();

            // Force exact pixel position
            try
            {
                var helper = new WindowInteropHelper(_window);
                if (helper.Handle != IntPtr.Zero)
                    SetWindowPos(helper.Handle, HWND_TOPMOST,
                        screenX, screenY, (int)_window.Width, (int)_window.Height,
                        SWP_NOACTIVATE);
            }
            catch { }

            Dispatcher.Run();
        });
        _uiThread.SetApartmentState(ApartmentState.STA);
        _uiThread.IsBackground = true;
        _uiThread.Name = "WhisperRing-STA";
        _uiThread.Start();
        _ready.Wait(3000);
    }

    /// <summary>Push new spectrum data to the ring overlay.</summary>
    public void UpdateSpectrum(int[] levels, int maxLevel, string mode, uint token, string recentTokens,
        string? sttText = null, long sttAgeTicks = long.MaxValue, string sttMode = "QUIET")
    {
        _dispatcher?.BeginInvoke(() =>
        {
            _window?.UpdateSpectrum(levels, maxLevel, mode, token, recentTokens, sttText, sttAgeTicks, sttMode);
            _window?.EnsureTopmost();
        });
    }

    /// <summary>Fade out and close.</summary>
    public void BeginFadeOut(int delayMs = 1000)
    {
        if (_uiThread != null) try { _uiThread.IsBackground = false; } catch { }
        _dispatcher?.BeginInvoke(() => _window?.BeginFadeOut(delayMs));
    }

    public void Dispose()
    {
        try
        {
            _dispatcher?.InvokeShutdown();
            _uiThread?.Join(3000);
        }
        catch { }
        _ready.Dispose();
    }

    private static readonly IntPtr HWND_TOPMOST = new(-1);
    private const uint SWP_NOACTIVATE = 0x0010;
    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, uint uFlags);
}
