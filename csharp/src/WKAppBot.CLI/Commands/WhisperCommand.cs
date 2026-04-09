using System.Numerics;
using NAudio.Wave;

namespace WKAppBot.CLI;

/// <summary>
/// Whisper Keyboard Engine — Phase 1: Mel-scale FFT spectrum analyzer + 32-bit tokenizer.
///
/// Architecture (platform-portable core):
///   Mic capture (NAudio/platform) → FFT → N-band energy extraction → 32-bit token
///
/// Two modes (same 32-bit token):
///   16-band × 2bit (default) — higher spectral resolution, better phoneme discrimination
///   8-band  × 4bit (--8band) — coarser but more amplitude detail per band
///
/// Mel-scale band boundaries (log-spaced, human hearing aligned):
///   Low bands dense (formant region), high bands wide (sibilant/air)
///   Band 0 always = vocal cord detector (성대 기본주파수)
///
/// Detection logic:
///   Band0 energy HIGH + others → normal speech → LEARN mode (STT채점용)
///   Band0 energy LOW  + whisper bands active → whisper → INPUT mode
///
/// Recommendations applied from GPT/Gemini 삼두협의체:
///   - Mel/log scale bands (not linear) for better phoneme discrimination
///   - 16×2bit option for Korean ㅏ/ㅓ, ㅜ/ㅗ near-vowel separation
///   - 2-8kHz region denser (ㅅ,ㅎ,ㅍ,ㅌ sibilant/plosive)
/// </summary>
internal partial class Program
{
    // ── 16-band articulatory (default): 2bit per band = 32bit token ──
    // Band 0 = vocal cord detector (ALL humans: male 85Hz ~ child 400Hz)
    // Band 1-15 = 7 articulatory zones subdivided (all > dead zone ~389-487Hz)
    // Non-integer-multiple boundaries verified: 0 pairs within 3% of N× (Monte Carlo 1M iter)
    // Based on GPT/Gemini 삼두협의체 articulatory analysis:
    //   Lips(ㅂㅍ), Pharynx(ㅏㅓ), Velar(ㄱㅋ), OralRes(ㅣㅡ),
    //   HiFormant(ㅐㅔ), Burst(ㄷㅌㅈㅊ), Sibilant(ㅅㅆ), Breath(ㅎ)
    // Nasal(ㄴㅁㅇ) = low energy across all bands (anti-resonance)
    private static readonly (int Lo, int Hi, string Name)[] MelBands16 = [
        (   70,   389, "Vocal"),  //  0: vocal cord F0 (all humans)
        //  --- dead zone 389-487Hz: vocal cord harmonic leakage ---
        (  487,   747, "Lip1"),   //  1: lip burst low (ㅂ,ㅍ onset)
        (  747,  1022, "Lip2"),   //  2: lip resonance + ㅗ,ㅜ
        ( 1022,  1374, "Phry"),   //  3: pharynx (ㅏ,ㅓ F1)
        ( 1374,  1711, "Velr"),   //  4: velar resonance (ㄱ,ㅋ)
        ( 1711,  2105, "OrlL"),   //  5: oral cavity low (ㅣ,ㅡ onset)
        ( 2105,  2571, "OrlH"),   //  6: oral cavity high (ㅣ,ㅡ peak)
        ( 2571,  3081, "HiF1"),   //  7: high formant low (ㅐ,ㅔ)
        ( 3081,  3540, "HiF2"),   //  8: high formant high
        ( 3540,  4340, "Brs1"),   //  9: burst/friction low (ㄷ,ㅌ)
        ( 4340,  5330, "Brs2"),   // 10: burst/friction high (ㅈ,ㅊ)
        ( 5330,  6370, "Sib1"),   // 11: sibilant jet low (ㅅ)
        ( 6370,  7430, "Sib2"),   // 12: sibilant jet high (ㅆ)
        ( 7430,  8860, "Brth"),   // 13: breath (ㅎ)
        ( 8860,  9930, "Nois"),   // 14: noise detect
        ( 9930, 11030, "Gate"),   // 15: upper gate (ambient noise)
    ];

    // ── 8-band articulatory (--8band): 4bit per band = 32bit token ──
    // Band 0 = vocal cord + lips unified (성대 감지 겸 입술 저역)
    // Band 1-6 = 6 articulatory mechanisms, Band 7 = noise gate
    // Non-integer-multiple boundaries verified: 0 pairs within 3% (Monte Carlo 200K iter)
    // Nasal (ㄴ,ㅁ,ㅇ) = low energy across all bands (anti-resonance)
    private static readonly (int Lo, int Hi, string Name)[] MelBands8 = [
        (   70,  1021, "VxLip"),  // 0: vocal cord + lip burst (성대 겸 ㅂ,ㅍ,ㅗ,ㅜ)
        ( 1021,  1397, "Pharx"),  // 1: pharynx low (ㅓ ~1100Hz)
        ( 1397,  1699, "Velar"),  // 2: velar + ㅏ (~1400Hz, ㄱ,ㅋ)
        ( 1699,  2647, "OralR"),  // 3: oral cavity resonance (ㅣ,ㅡ)
        ( 2647,  3557, "HiRes"),  // 4: high formant (ㅐ,ㅔ)
        ( 3557,  4919, "Burst"),  // 5: alveolar burst+friction (ㄷ,ㅌ,ㅈ,ㅊ)
        ( 4919,  7507, "Sibil"),  // 6: sibilant jet (ㅅ,ㅆ)
        ( 7507, 11197, "Breth"),  // 7: breath + noise gate (ㅎ, >9kHz=noise)
    ];

    // FFT size: 2048 samples @ 16kHz = 128ms window, ~7.8Hz per bin
    private const int FftSize = 2048;
    private const int SampleRate = 16000;

    static int WhisperCommand(string[] args)
    {
        // Subcommand dispatch
        if (args.Length > 0 && args[0] == "study")
            return WhisperStudyCommand(args[1..]);
        if (args.Length > 0 && args[0] == "stats")
            return WhisperStudyCommand(args); // forward "stats" as-is
        if (args.Length > 0 && args[0] == "slice")
            return WhisperSliceCommand(args[1..]);
        if (args.Length > 0 && args[0] == "clean")
            return WhisperCleanCommand(args[1..]);
        if (args.Length > 0 && args[0] == "index")
            return WhisperIndexCommand(args[1..]);

        // Parse options
        bool use8Band = args.Any(a => a == "--8band");
        bool useRing = args.Any(a => a == "--ring") || args.Any(a => a == "--hud");
        bool hardKill = args.Any(a => a == "--kill");
        int deviceIndex = 0;
        float noiseGateDb = -65f;
        float killLevelDb = 0f; // 0 = auto
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "--device" && int.TryParse(args[i + 1], out var d)) deviceIndex = d;
            if (args[i] == "--gate" && float.TryParse(args[i + 1], out var g)) noiseGateDb = g;
            if (args[i] == "--kill-db" && float.TryParse(args[i + 1], out var k)) { killLevelDb = k; hardKill = true; }
        }

        var bands = use8Band ? MelBands8 : MelBands16;
        int bandCount = bands.Length;
        int bitsPerBand = use8Band ? 4 : 2;
        int maxLevel = (1 << bitsPerBand) - 1; // 15 or 3

        Console.Error.WriteLine($"[WHISPER] Mel-Scale Spectrum Analyzer + Tokenizer ({bandCount}-band × {bitsPerBand}bit = 32bit)");
        Console.Error.WriteLine("[WHISPER] Bands (Mel-scale):");
        for (int i = 0; i < bandCount; i++)
            Console.Error.WriteLine($"  B{i,2}: {bands[i].Lo,5}-{bands[i].Hi,5} Hz  ({bands[i].Name})");
        Console.Error.WriteLine("");

        // Find microphone
        int deviceCount = WaveInEvent.DeviceCount;
        if (deviceCount == 0)
        {
            Console.Error.WriteLine("[WHISPER] ERROR: No microphone found!");
            return 1;
        }

        Console.Error.WriteLine("[WHISPER] Audio devices:");
        for (int i = 0; i < deviceCount; i++)
        {
            var cap = WaveInEvent.GetCapabilities(i);
            Console.Error.WriteLine($"  [{i}] {cap.ProductName} ({cap.Channels}ch)");
        }
        Console.Error.WriteLine($"[WHISPER] Using device [{deviceIndex}]");
        if (useRing) Console.Error.WriteLine("[WHISPER] Ring HUD overlay enabled (--ring)");
        if (hardKill) Console.Error.WriteLine($"[WHISPER] DUET hard kill enabled (kill-db={killLevelDb})");
        Console.Error.WriteLine($"[WHISPER] Noise gate: {noiseGateDb} dBFS");
        Console.Error.WriteLine("[WHISPER] Press Ctrl+C to stop");
        Console.Error.WriteLine("");

        // Ring HUD overlay (--ring mode, 8-band only for now)
        WhisperRingHost? ringHost = null;
        if (useRing)
        {
            ringHost = new WhisperRingHost();
            // Bottom-right corner of primary screen
            int sx = (int)System.Windows.SystemParameters.PrimaryScreenWidth - 200;
            int sy = (int)System.Windows.SystemParameters.PrimaryScreenHeight - 230;
            ringHost.Start(sx, sy);
            Console.Error.WriteLine($"[WHISPER] Ring overlay started at ({sx},{sy})");
        }

        // Ring buffer for FFT
        var ringBuffer = new float[FftSize];
        int ringPos = 0;
        bool bufferReady = false;
        var fftLock = new object();

        // Set up microphone capture
        var waveIn = new WaveInEvent
        {
            DeviceNumber = deviceIndex,
            WaveFormat = new WaveFormat(SampleRate, 16, 1), // 16kHz mono 16bit
            BufferMilliseconds = 50,
        };

        waveIn.DataAvailable += (_, e) =>
        {
            lock (fftLock)
            {
                for (int i = 0; i < e.BytesRecorded - 1; i += 2)
                {
                    short sample = (short)(e.Buffer[i] | (e.Buffer[i + 1] << 8));
                    ringBuffer[ringPos] = sample / 32768f;
                    ringPos = (ringPos + 1) % FftSize;
                    if (ringPos == 0) bufferReady = true;
                }
            }
        };

        waveIn.StartRecording();

        // Hann window (precomputed)
        var hannWindow = new float[FftSize];
        for (int i = 0; i < FftSize; i++)
            hannWindow[i] = 0.5f * (1f - MathF.Cos(2f * MathF.PI * i / FftSize));

        // Analysis loop
        var fftBuffer = new Complex[FftSize];
        var bandEnergies = new double[bandCount];
        var prevTokens = new List<uint>();
        int frameCount = 0;

        bool useAnsi = !Console.IsOutputRedirected;
        if (useAnsi) Console.CursorVisible = false;

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        // Calculate console header lines (bands list + device info + blank)
        int headerLines = bandCount + 7;

        try
        {
            while (!cts.Token.IsCancellationRequested)
            {
                Thread.Sleep(30); // ~33fps

                if (!bufferReady) continue;

                // Copy ring buffer with Hann window
                lock (fftLock)
                {
                    for (int i = 0; i < FftSize; i++)
                    {
                        int idx = (ringPos + i) % FftSize;
                        fftBuffer[i] = new Complex(ringBuffer[idx] * hannWindow[i], 0);
                    }
                    bufferReady = false;
                }

                // In-place FFT
                Fft(fftBuffer);

                // Extract band energies
                double freqPerBin = (double)SampleRate / FftSize;
                double maxEnergy = 0;
                for (int b = 0; b < bandCount; b++)
                {
                    int binLo = (int)(bands[b].Lo / freqPerBin);
                    int binHi = (int)(bands[b].Hi / freqPerBin);
                    binLo = Math.Clamp(binLo, 0, FftSize / 2 - 1);
                    binHi = Math.Clamp(binHi, binLo + 1, FftSize / 2);

                    double energy = 0;
                    for (int k = binLo; k < binHi; k++)
                        energy += fftBuffer[k].Magnitude;
                    energy /= (binHi - binLo); // average per bin
                    bandEnergies[b] = energy;
                    if (energy > maxEnergy) maxEnergy = energy;
                }

                // Quantize to N-bit per band → pack into 32-bit token
                uint token = 0;
                var levels = new int[bandCount];
                for (int b = 0; b < bandCount; b++)
                {
                    int level = maxEnergy > 0.001
                        ? (int)(bandEnergies[b] / maxEnergy * maxLevel)
                        : 0;
                    level = Math.Clamp(level, 0, maxLevel);
                    levels[b] = level;
                    token |= (uint)level << (32 - (b + 1) * bitsPerBand);
                }

                // Detect mode
                // 8-band: Band0 (VxLip) high = vocal cord active → voiced
                // 16-band: Band0 (Vocal) high = vocal cord → voiced
                int voiceThreshold = maxLevel / 2;
                bool isVoiced = levels[0] >= voiceThreshold;
                bool isSilence = maxEnergy < 0.005;

                // Whisper: not voiced, not silent, articulatory bands active
                int whisperSum = use8Band
                    ? levels[3] + levels[4] + levels[5] + levels[6]  // 8-band: OralR+HiRes+Burst+Sibil
                    : levels[5] + levels[6] + levels[7] +            // 16-band: OrlL+OrlH+HiF1
                      levels[9] + levels[10] + levels[11];           //          Brs1+Brs2+Sib1
                int whisperThreshold = use8Band ? 4 : 3;
                bool isWhisper = !isVoiced && !isSilence && whisperSum > whisperThreshold;

                string mode = isSilence ? "QUIET" : isVoiced ? "VOICE" : isWhisper ? "WHSPR" : "NOISE";

                // Store token sequence
                if (!isSilence)
                {
                    prevTokens.Add(token);
                    if (prevTokens.Count > 100) prevTokens.RemoveAt(0);
                }

                // ── Ring HUD overlay update ──
                if (ringHost?.IsAlive == true)
                {
                    int start8 = Math.Max(0, prevTokens.Count - 6);
                    var recent = string.Join(" ", prevTokens.Skip(start8).Select(t => $"{t:X8}"));
                    // For 16-band mode, downsample to 8 bands for ring display
                    int[] ringLevels;
                    int ringMax;
                    if (use8Band)
                    {
                        ringLevels = levels;
                        ringMax = maxLevel;
                    }
                    else
                    {
                        // Map 16 bands → 8: pairs averaged, scale 0-3 → 0-15
                        ringLevels = new int[8];
                        for (int b = 0; b < 8; b++)
                        {
                            int b0 = b * 2, b1 = b * 2 + 1;
                            ringLevels[b] = (levels[b0] + levels[b1]) * 5; // 0-3 avg → 0-15ish
                        }
                        ringMax = 15;
                    }
                    ringHost.UpdateSpectrum(ringLevels, ringMax, mode, token, recent);
                }

                // ── Console visualization ──
                frameCount++;

                if (useAnsi)
                {
                    Console.SetCursorPosition(0, headerLines);
                    int barWidth = use8Band ? 15 : 20; // wider bars for 2-bit (0-3 → scale up)

                    for (int b = 0; b < bandCount; b++)
                    {
                        int barLen = use8Band ? levels[b] : levels[b] * 5; // scale: 0-3 → 0-15
                        int barMax = use8Band ? 15 : 15;
                        barLen = Math.Min(barLen, barMax);
                        string bar = new string('█', barLen) + new string('░', barMax - barLen);
                        string label = $"  B{b,2} {bands[b].Name,-5} {bands[b].Lo,5}-{bands[b].Hi,5}Hz ";

                        if (b == 0)
                            Console.ForegroundColor = levels[0] >= voiceThreshold ? ConsoleColor.Red : ConsoleColor.DarkGray;
                        else
                            Console.ForegroundColor = levels[b] > 0 ? ConsoleColor.Cyan : ConsoleColor.DarkCyan;

                        Console.Write(label);
                        Console.Write($"[{bar}]");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Error.WriteLine($" {levels[b]}  ");
                    }

                    Console.Error.WriteLine("");
                    Console.ForegroundColor = mode switch
                    {
                        "VOICE" => ConsoleColor.Yellow,
                        "WHSPR" => ConsoleColor.Green,
                        _ => ConsoleColor.DarkGray,
                    };
                    Console.Error.WriteLine($"  Token: 0x{token:X8}  Mode: [{mode}]  Frame: {frameCount}    ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("  Recent: ");
                    int start = Math.Max(0, prevTokens.Count - 8);
                    for (int i = start; i < prevTokens.Count; i++)
                        Console.Write($"{prevTokens[i]:X8} ");
                    Console.Error.WriteLine("                    ");
                    Console.ResetColor();
                }
                else
                {
                    // Pipe/redirect: simple line output
                    var bars = string.Join(" ", levels.Select(l => l.ToString("X")));
                    Console.Error.WriteLine($"[{mode}] 0x{token:X8} [{bars}] f={frameCount}");
                }
            }
        }
        finally
        {
            if (useAnsi) Console.CursorVisible = true;
            waveIn.StopRecording();
            waveIn.Dispose();
            if (ringHost != null)
            {
                ringHost.BeginFadeOut();
                Thread.Sleep(1800); // let fade-out complete
                ringHost.Dispose();
            }
        }

        Console.Error.WriteLine("");
        Console.Error.WriteLine("[WHISPER] Stopped.");
        Console.Error.WriteLine($"[WHISPER] Collected {prevTokens.Count} tokens.");
        return 0;
    }

    /// <summary>
    /// Cooley-Tukey radix-2 FFT in-place. Pure C# — no platform dependency.
    /// </summary>
    private static void Fft(Complex[] data)
    {
        int n = data.Length;

        // Bit-reversal permutation
        for (int i = 1, j = 0; i < n; i++)
        {
            int bit = n >> 1;
            for (; (j & bit) != 0; bit >>= 1)
                j ^= bit;
            j ^= bit;
            if (i < j) (data[i], data[j]) = (data[j], data[i]);
        }

        // Butterfly
        for (int len = 2; len <= n; len <<= 1)
        {
            double angle = -2.0 * Math.PI / len;
            var wn = new Complex(Math.Cos(angle), Math.Sin(angle));
            for (int i = 0; i < n; i += len)
            {
                var w = Complex.One;
                for (int j = 0; j < len / 2; j++)
                {
                    var u = data[i + j];
                    var v = data[i + j + len / 2] * w;
                    data[i + j] = u + v;
                    data[i + j + len / 2] = u - v;
                    w *= wn;
                }
            }
        }
    }
}
