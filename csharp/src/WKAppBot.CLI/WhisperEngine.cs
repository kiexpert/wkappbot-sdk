using System.Numerics;
using NAudio.Wave;

namespace WKAppBot.CLI;

/// <summary>
/// Whisper Keyboard Engine — standalone FFT spectrum analyzer + 32-bit tokenizer.
/// Can run headless (no console) as a subsystem inside AppBotEye or standalone via CLI.
///
/// Usage:
///   var engine = new WhisperEngine();
///   engine.Start();
///   engine.OnFrame += (data) => { /* update UI */ };
///   engine.Stop();
/// </summary>
internal sealed class WhisperEngine : IDisposable
{
    // ── 8-band articulatory: 4bit per band = 32bit token ──
    // Non-integer-multiple boundaries verified: 0 pairs within 3% (Monte Carlo 200K iter)
    private static readonly (int Lo, int Hi, string Name)[] Bands = [
        (   70,  1021, "VxLip"),  // 0: vocal cord + lip burst
        ( 1021,  1397, "Pharx"),  // 1: pharynx low
        ( 1397,  1699, "Velar"),  // 2: velar + ㅏ
        ( 1699,  2647, "OralR"),  // 3: oral cavity resonance
        ( 2647,  3557, "HiRes"),  // 4: high formant
        ( 3557,  4919, "Burst"),  // 5: alveolar burst+friction
        ( 4919,  7507, "Sibil"),  // 6: sibilant jet
        ( 7507, 11197, "Breth"),  // 7: breath + noise gate
    ];

    private const int BandCount = 8;
    private const int BitsPerBand = 4;
    private const int MaxLevel = 15;
    private const int FftSize = 2048;
    private const int SampleRate = 16000;

    private WaveInEvent? _waveIn;
    private Thread? _analysisThread;
    private CancellationTokenSource? _cts;

    // Ring buffer
    private readonly float[] _ringBuffer = new float[FftSize];
    private int _ringPos;
    private volatile bool _bufferReady;
    private readonly object _fftLock = new();

    // Hann window (precomputed)
    private readonly float[] _hannWindow;

    // ── Method B: Noise Average + Signal Mid ──
    // Noise = 10s rolling EMA of all frames (per-band)
    // Signal Mid = rolling EMA of frames where energy > 2× noise (per-band)
    // Quantize: energy / mid × 7 → 0~15 (mid = level 7~8)
    private readonly double[] _noiseAvg = new double[BandCount];   // 10s EMA (all frames)
    private readonly double[] _signalMid = new double[BandCount];  // EMA of >2×noise frames only
    private int _warmupFrames;
    private const int WarmupWindow = 30;          // ~1s seed period
    private const double NoiseAlpha = 0.01;       // ~3s at 33fps (speech excluded → fast OK)
    private const double SignalAlpha = 0.02;       // faster adapt for speech reference
    private const double NoiseGateMultiplier = 2.0; // frames > noise×2 = signal

    // State
    private readonly List<uint> _recentTokens = new();
    private int _frameCount;
    private bool _inWhisper; // hysteresis state for WHSPR detection
    private double[] _prevBandEnergies = new double[BandCount]; // for spectral flux

    /// <summary>Fired on each analysis frame (~33fps).</summary>
    public event Action<WhisperFrame>? OnFrame;

    /// <summary>Fired on each mic audio buffer (raw 16kHz 16bit mono PCM).</summary>
    public event Action<byte[], int>? OnMicData;

    /// <summary>Current device index (default 0).</summary>
    public int DeviceIndex { get; set; }

    public bool IsRunning => _analysisThread?.IsAlive == true;

    public WhisperEngine()
    {
        _hannWindow = new float[FftSize];
        for (int i = 0; i < FftSize; i++)
            _hannWindow[i] = 0.5f * (1f - MathF.Cos(2f * MathF.PI * i / FftSize));
    }

    /// <summary>Start microphone capture and analysis loop.</summary>
    public bool Start(int deviceIndex = 0)
    {
        if (IsRunning) return true;

        DeviceIndex = deviceIndex;
        if (WaveInEvent.DeviceCount == 0) return false;

        _cts = new CancellationTokenSource();
        _ringPos = 0;
        _bufferReady = false;

        _waveIn = new WaveInEvent
        {
            DeviceNumber = deviceIndex,
            WaveFormat = new WaveFormat(SampleRate, 16, 1),
            BufferMilliseconds = 50,
        };

        _waveIn.DataAvailable += (_, e) =>
        {
            lock (_fftLock)
            {
                for (int i = 0; i < e.BytesRecorded - 1; i += 2)
                {
                    short sample = (short)(e.Buffer[i] | (e.Buffer[i + 1] << 8));
                    _ringBuffer[_ringPos] = sample / 32768f;
                    _ringPos = (_ringPos + 1) % FftSize;
                    _bufferReady = true; // sliding window: always ready
                }
            }
            // Forward raw mic PCM for parallel recording
            OnMicData?.Invoke(e.Buffer, e.BytesRecorded);
        };

        try { _waveIn.StartRecording(); }
        catch { return false; }

        _analysisThread = new Thread(AnalysisLoop)
        {
            IsBackground = true,
            Name = "WhisperEngine",
        };
        _analysisThread.Start();
        return true;
    }

    /// <summary>Stop capture and analysis.</summary>
    public void Stop()
    {
        _cts?.Cancel();
        try { _waveIn?.StopRecording(); } catch { }
        _analysisThread?.Join(2000);
        try { _waveIn?.Dispose(); } catch { }
        _waveIn = null;
    }

    private void AnalysisLoop()
    {
        var fftBuffer = new Complex[FftSize];
        var bandEnergies = new double[BandCount];
        var ranked = new int[BandCount];
        var ct = _cts!.Token;

        while (!ct.IsCancellationRequested)
        {
            Thread.Sleep(10); // ~100fps — 10ms sliding window for Korean phoneme capture
            if (!_bufferReady) continue;

            // Copy ring buffer with Hann window
            lock (_fftLock)
            {
                for (int i = 0; i < FftSize; i++)
                {
                    int idx = (_ringPos + i) % FftSize;
                    fftBuffer[i] = new Complex(_ringBuffer[idx] * _hannWindow[i], 0);
                }
                _bufferReady = false;
            }

            // FFT
            Fft(fftBuffer);

            // Extract raw band energies
            double freqPerBin = (double)SampleRate / FftSize;
            var rawEnergies = new double[BandCount];
            for (int b = 0; b < BandCount; b++)
            {
                int binLo = Math.Clamp((int)(Bands[b].Lo / freqPerBin), 0, FftSize / 2 - 1);
                int binHi = Math.Clamp((int)(Bands[b].Hi / freqPerBin), binLo + 1, FftSize / 2);

                double energy = 0;
                for (int k = binLo; k < binHi; k++)
                    energy += fftBuffer[k].Magnitude;
                rawEnergies[b] = energy / (binHi - binLo);
            }

            // ── Method B: Noise Average + Signal Mid quantization ──
            // Stage 1: Update noise average (10s EMA, all frames)
            if (_warmupFrames < WarmupWindow)
            {
                // Seed period: initialize with first frames
                for (int b = 0; b < BandCount; b++)
                {
                    if (_warmupFrames == 0)
                    {
                        _noiseAvg[b] = rawEnergies[b];
                        _signalMid[b] = rawEnergies[b] * 3; // initial guess: 3× noise
                    }
                    else
                    {
                        _noiseAvg[b] += (rawEnergies[b] - _noiseAvg[b]) * 0.1; // fast seed
                    }
                }
                _warmupFrames++;
            }
            else
            {
                // Noise: only update on quiet frames (< 2× noise) — speech doesn't pollute noise
                // Signal mid: only update on loud frames (≥ 2× noise) — noise doesn't dilute signal
                for (int b = 0; b < BandCount; b++)
                {
                    double gate = _noiseAvg[b] * NoiseGateMultiplier;
                    if (rawEnergies[b] < gate)
                        _noiseAvg[b] += (rawEnergies[b] - _noiseAvg[b]) * NoiseAlpha;
                    else
                        _signalMid[b] += (rawEnergies[b] - _signalMid[b]) * SignalAlpha;
                    _signalMid[b] = Math.Max(_signalMid[b], _noiseAvg[b] * 2.5);
                }
            }

            // Stage 3: Quantize — mid maps to level ~7, scale 0~15
            double maxEnergy = 0;
            uint token = 0;
            var levels = new int[BandCount];
            for (int b = 0; b < BandCount; b++)
            {
                double clean = Math.Max(0, rawEnergies[b] - _noiseAvg[b]);
                double midClean = Math.Max(_signalMid[b] - _noiseAvg[b], 0.001);
                int level = (int)(clean / midClean * 7); // mid → 7, 2×mid → 14~15
                level = Math.Clamp(level, 0, MaxLevel);
                levels[b] = level;
                token |= (uint)level << (32 - (b + 1) * BitsPerBand);
                if (clean > maxEnergy) maxEnergy = clean;
            }

            // ── 음코드 (SoundCode): top 5 bands by energy, 3bit index each = 15bit ──
            // Band indices sorted by clean energy descending → pack top 5 into ushort
            // MSB first: rank1(bit14-12) | rank2(bit11-9) | rank3(bit8-6) | rank4(bit5-3) | rank5(bit2-0)
            var cleanEnergies = new double[BandCount];
            for (int b = 0; b < BandCount; b++)
                cleanEnergies[b] = Math.Max(0, rawEnergies[b] - _noiseAvg[b]);

            for (int b = 0; b < BandCount; b++) ranked[b] = b;
            // Simple insertion sort (8 elements, descending by cleanEnergies)
            for (int i = 1; i < BandCount; i++)
            {
                int key = ranked[i];
                double keyVal = cleanEnergies[key];
                int j = i - 1;
                while (j >= 0 && cleanEnergies[ranked[j]] < keyVal)
                {
                    ranked[j + 1] = ranked[j];
                    j--;
                }
                ranked[j + 1] = key;
            }
            ushort soundCode = 0;
            for (int r = 0; r < 5; r++)
                soundCode |= (ushort)(ranked[r] << (12 - r * 3));

            // Mode detection
            bool isSilence = maxEnergy < 0.001;

            // Upper energy gate: if maxEnergy exceeds whisper ceiling, it's external noise
            // (car horn, YouTube, music, etc.) — whisper physically can't produce this much energy.
            // Ceiling = 4× signal mid average (whisper rarely exceeds 2× mid)
            double midAvg = 0;
            for (int b = 0; b < BandCount; b++) midAvg += _signalMid[b];
            midAvg /= BandCount;
            bool isTooLoud = !isSilence && maxEnergy > midAvg * 4;

            // Spectral Flux: sum of positive energy changes across bands
            // High flux = speech (non-stationary), low flux = fan/hum (stationary)
            double spectralFlux = 0;
            for (int b = 0; b < BandCount; b++)
            {
                double diff = rawEnergies[b] - _prevBandEnergies[b];
                if (diff > 0) spectralFlux += diff;
                _prevBandEnergies[b] = rawEnergies[b];
            }

            int voiceThreshold = MaxLevel / 2;
            bool isVoiced = levels[0] >= voiceThreshold;
            int whisperSum = levels[3] + levels[4] + levels[5] + levels[6];
            // Hysteresis: start requires >8, sustain requires >4
            int whisperGate = _inWhisper ? 4 : 8;
            // Spectral flux gate: low flux = stationary noise, not speech
            // Only block if flux is very low (< 1% of midAvg) — don't over-filter
            bool hasFlux = spectralFlux > midAvg * 0.01;
            bool isWhisper = !isVoiced && !isSilence && !isTooLoud && whisperSum > whisperGate && hasFlux;
            _inWhisper = isWhisper;

            string mode = isSilence ? "QUIET"
                : isTooLoud ? "LOUD"
                : isVoiced ? "VOICE"
                : isWhisper ? "WHSPR"
                : "NOISE";

            // Gate sound code: need meaningful energy above noise floor
            // Mode alone isn't enough — weak WHSPR with low energy = random band rankings
            // Require at least one band with clean energy ≥ 30% of its signal mid
            bool hasStrongBand = false;
            for (int b = 0; b < BandCount; b++)
            {
                double clean = cleanEnergies[b];
                double mid = Math.Max(_signalMid[b] - _noiseAvg[b], 0.001);
                if (clean >= mid * 0.3) { hasStrongBand = true; break; }
            }
            if (!hasStrongBand || (mode != "VOICE" && mode != "WHSPR"))
                soundCode = 0;

            if (!isSilence)
            {
                _recentTokens.Add(token);
                if (_recentTokens.Count > 100) _recentTokens.RemoveAt(0);
            }

            _frameCount++;

            // Build recent tokens string
            int start = Math.Max(0, _recentTokens.Count - 6);
            var recent = string.Join(" ", _recentTokens.Skip(start).Select(t => $"{t:X8}"));

            // Fire event
            OnFrame?.Invoke(new WhisperFrame
            {
                Levels = levels,
                MaxLevel = MaxLevel,
                Mode = mode,
                Token = token,
                SoundCode = soundCode,
                RecentTokens = recent,
                FrameCount = _frameCount,
                MaxEnergy = maxEnergy,
                NoiseFloor = (double[])_noiseAvg.Clone(),
                IsCalibrating = _warmupFrames < WarmupWindow,
            });
        }
    }

    public void Dispose()
    {
        Stop();
        _cts?.Dispose();
    }

    /// <summary>Band info for UI display.</summary>
    public static (int Lo, int Hi, string Name)[] GetBands() => Bands;

    // ── Cooley-Tukey radix-2 FFT ──
    private static void Fft(Complex[] data)
    {
        int n = data.Length;
        for (int i = 1, j = 0; i < n; i++)
        {
            int bit = n >> 1;
            for (; (j & bit) != 0; bit >>= 1) j ^= bit;
            j ^= bit;
            if (i < j) (data[i], data[j]) = (data[j], data[i]);
        }
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

/// <summary>One frame of whisper analysis data.</summary>
internal sealed class WhisperFrame
{
    public int[] Levels { get; init; } = [];
    public int MaxLevel { get; init; }
    public string Mode { get; init; } = "QUIET";
    public uint Token { get; init; }
    /// <summary>15-bit sound code: top 5 bands by energy, 3-bit index each (MSB=rank1).</summary>
    public ushort SoundCode { get; init; }
    public string RecentTokens { get; init; } = "";
    public int FrameCount { get; init; }
    public double MaxEnergy { get; init; }
    public double[] NoiseFloor { get; init; } = [];
    public bool IsCalibrating { get; init; }
}
