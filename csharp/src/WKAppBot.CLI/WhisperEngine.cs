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
    private const int FftSize = 512;       // 32ms window — optimal for Korean phoneme detection (10-30ms)
    private const int HalfFft = FftSize / 2;
    private const int SampleRate = 16000;
    private const float PreEmphasis = 0.97f; // high-freq boost for consonant clarity
    private const float DefaultNoiseGateDb = -72f;  // dBFS noise gate (lowered for distant voice detection)

    private WaveInEvent? _waveIn;
    private Thread? _analysisThread;
    private CancellationTokenSource? _cts;

    // Ring buffers — stereo (L/R) for DUET spatial analysis
    private readonly float[] _ringL = new float[FftSize];
    private readonly float[] _ringR = new float[FftSize];
    private readonly float[] _ringBuffer = new float[FftSize]; // mono mix (backward compat)
    private int _ringPos;
    private volatile bool _bufferReady;
    private readonly object _fftLock = new();
    private bool _isStereo; // true if mic provides 2ch

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
    private double[] _prevOrigMag = new double[HalfFft]; // previous frame FFT magnitudes (for DUET kill gate)

    // Syllable-window sound code confirmation: N consecutive same code → confirmed
    private ushort _scRunCode;    // current running sound code
    private int _scRunCount;      // consecutive frames with same code
    private ushort _confirmedSc;  // last confirmed (stable) sound code

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

        // Try stereo first, fallback to mono
        _isStereo = false;
        try
        {
            var testWave = new WaveInEvent { DeviceNumber = deviceIndex, WaveFormat = new WaveFormat(SampleRate, 16, 2) };
            testWave.Dispose();
            _isStereo = true;
        }
        catch { /* mono fallback */ }

        int channels = _isStereo ? 2 : 1;
        _waveIn = new WaveInEvent
        {
            DeviceNumber = deviceIndex,
            WaveFormat = new WaveFormat(SampleRate, 16, channels),
            BufferMilliseconds = 50,
        };

        _waveIn.DataAvailable += (_, e) =>
        {
            lock (_fftLock)
            {
                int bytesPerSample = channels * 2; // 16bit × channels
                for (int i = 0; i + bytesPerSample - 1 < e.BytesRecorded; i += bytesPerSample)
                {
                    float left = (short)(e.Buffer[i] | (e.Buffer[i + 1] << 8)) / 32768f;
                    float right = _isStereo
                        ? (short)(e.Buffer[i + 2] | (e.Buffer[i + 3] << 8)) / 32768f
                        : left;
                    float mono = (left + right) * 0.5f;

                    _ringL[_ringPos] = left;
                    _ringR[_ringPos] = right;
                    _ringBuffer[_ringPos] = mono;
                    _ringPos = (_ringPos + 1) % FftSize;
                    _bufferReady = true;
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

    // ── DUET source tracking: cached source directions across frames ──
    private readonly List<SourceInfo> _sourcesCache = new();
    private const int MaxSources = 8;
    private const double SourceMergeAngle = 0.15; // radians (~8.6°) — merge if within this
    private const int WarmInterval = 5; // reflection hunt + cache expiry every N frames

    private void AnalysisLoop()
    {
        var fftBuffer = new Complex[FftSize];   // mono (backward compat pipeline)
        var fftL = new Complex[FftSize];        // stereo left
        var fftR = new Complex[FftSize];        // stereo right
        var bandEnergies = new double[BandCount];
        var ranked = new int[BandCount];
        var ct = _cts!.Token;

        // Per-bin spatial features
        var binILD = new double[HalfFft];   // inter-channel level difference
        var binIPD = new double[HalfFft];   // inter-channel phase difference
        var binEnergy = new double[HalfFft]; // magnitude sum

        while (!ct.IsCancellationRequested)
        {
            try
            {
            Thread.Sleep(10); // ~100fps — 10ms sliding window for Korean phoneme capture
            if (!_bufferReady) continue;

            // ── Noise gate: compute RMS (dBFS), skip if below threshold ──
            float rmsSum;
            lock (_fftLock)
            {
                rmsSum = 0f;
                for (int i = 0; i < FftSize; i++)
                {
                    float s = _ringBuffer[(_ringPos + i) % FftSize];
                    rmsSum += s * s;
                }
            }
            float rmsDb = 10f * MathF.Log10(rmsSum / FftSize + 1e-10f);
            // Periodic diagnostic: every 3000 frames (~30s) log rmsDb even if QUIET
            if (_frameCount % 3000 == 0)
                Console.WriteLine($"[WHISPER:DIAG] frame={_frameCount} rmsDb={rmsDb:F1} gate={NoiseGateDb:F0} stereo={_isStereo} src={_sourcesCache.Count}");
            if (rmsDb < this.NoiseGateDb)
            {
                _frameCount++;
                OnFrame?.Invoke(new WhisperFrame
                {
                    Levels = new int[BandCount],
                    MaxLevel = MaxLevel,
                    Mode = "QUIET",
                    Token = 0,
                    SoundCode = 0,
                    RecentTokens = "",
                    FrameCount = _frameCount,
                    RmsDb = rmsDb,
                    IsStereo = _isStereo,
                });
                continue;
            }

            // Copy ring buffers with pre-emphasis + Hann window
            lock (_fftLock)
            {
                for (int i = 0; i < FftSize; i++)
                {
                    int idx = (_ringPos + i) % FftSize;
                    float monoSample = _ringBuffer[idx];
                    float lSample = _ringL[idx];
                    float rSample = _ringR[idx];
                    if (i > 0)
                    {
                        int prevIdx = (_ringPos + i - 1) % FftSize;
                        monoSample -= PreEmphasis * _ringBuffer[prevIdx];
                        lSample -= PreEmphasis * _ringL[prevIdx];
                        rSample -= PreEmphasis * _ringR[prevIdx];
                    }
                    fftBuffer[i] = new Complex(monoSample * _hannWindow[i], 0);
                    fftL[i] = new Complex(lSample * _hannWindow[i], 0);
                    fftR[i] = new Complex(rSample * _hannWindow[i], 0);
                }
                _bufferReady = false;
            }

            // FFT — mono + stereo L/R
            Fft(fftBuffer);
            if (_isStereo) { Fft(fftL); Fft(fftR); }

            // Preserve original mono magnitudes BEFORE DUET zeroing
            // (DUET kills noise bins → rawEnergies must use original spectrum for mode detection)
            var origMag = new double[HalfFft];
            for (int k = 0; k < HalfFft; k++)
                origMag[k] = fftBuffer[k].Magnitude;

            // ── DUET spatial analysis: per-bin ILD + IPD ──
            int killedBins = 0;
            int voiceClusters = 0, noiseClusters = 0;
            if (_isStereo)
            {
                // Step 1: compute per-bin spatial features
                for (int k = 1; k < HalfFft; k++)
                {
                    double magL = fftL[k].Magnitude;
                    double magR = fftR[k].Magnitude;
                    binEnergy[k] = magL + magR;
                    binILD[k] = (magL + 1e-10) / (magR + 1e-10);
                    var cross = fftL[k] * Complex.Conjugate(fftR[k]);
                    binIPD[k] = Math.Atan2(cross.Imaginary, cross.Real);
                }

                // Step 2: DUET-style direction clustering
                var frameSources = ClusterSources(binIPD, binILD, binEnergy);

                // Step 3+4: 집중감시 우선 → 분류 → 킬
                // 이미 알려진 음성방향은 스펙트럼 계산 건너뜀 (빠르고 정확)
                var killed = new HashSet<int>();
                for (int ci = 0; ci < frameSources.Count; ci++)
                {
                    if (killed.Contains(ci)) continue;
                    var src = frameSources[ci];

                    // ① 캐시 히트 체크 (최우선 — 방향만으로 즉시 판정, 스펙트럼 불필요)
                    double trackedConf = GetTrackedVoiceConfidence(src.Angle);
                    if (trackedConf >= 1.0) // 집중감시방향 → 즉시 보호
                    {
                        src.IsVoiceLike = true;
                        // Warm: 5프레임마다 프로파일 계산 (음악 감지용 stability 추적)
                        if (_frameCount % WarmInterval == 0)
                            src.SpectralProfile = ComputeSpectralProfile(src.BinIndices, fftBuffer);
                        voiceClusters++;
                        TrackSource(src);
                        // TrackSource가 음악으로 강등했으면 킬로 전환
                        if (IsSuspectedMusic(src.Angle))
                        {
                            voiceClusters--;
                            noiseClusters++;
                            // fall through to ③ kill
                        }
                        else
                        {
                            if (!EnableHardKill) continue;
                        }
                    }
                    else if (trackedConf <= 0.0) // 척살집중대상 → 즉시 킬
                    {
                        src.IsVoiceLike = false;
                        noiseClusters++;
                        TrackSource(src);
                        // fall through to ③ kill
                    }
                    else
                    {
                        // ② 신규/불확실 방향 → 스펙트럼 분석 후 판정
                        src.SpectralProfile = ComputeSpectralProfile(src.BinIndices, fftBuffer);
                        src.IsVoiceLike = IsVoiceLikeCluster(src.SpectralProfile)
                                          || trackedConf > 0.6;
                        TrackSource(src);

                        if (src.IsVoiceLike)
                        {
                            voiceClusters++;
                            if (!EnableHardKill) continue;
                        }
                        else
                        {
                            noiseClusters++;
                        }
                    }

                    // ③ 킬원음: hard zero kill bins (skip weak bins — preserve distant voice)
                    foreach (int k in src.BinIndices)
                    {
                        // Kill gate: skip if bin energy dropped to <20% of previous frame
                        // Weak/fading bins are likely distant voice remnants, not active noise
                        double curMag = fftBuffer[k].Magnitude;
                        if (curMag < _prevOrigMag[k] * KillMercy) continue;
                        fftBuffer[k] = Complex.Zero;
                        fftBuffer[FftSize - k] = Complex.Zero;
                        killedBins++;
                    }
                    killed.Add(ci);
                }

                // ── Warm path (every 5 frames): reflection hunt + cache maintenance ──
                if (_frameCount % WarmInterval == 0)
                {
                    // Reflection hunt: find weaker clusters with similar spectral shape to killed ones
                    // Snapshot killed set — we add new entries during iteration
                    var killedSnapshot = killed.ToArray();
                    foreach (int ki in killedSnapshot)
                    {
                        var src = frameSources[ki];
                        if (src.SpectralProfile == null) continue;
                        for (int ri = 0; ri < frameSources.Count; ri++)
                        {
                            if (killed.Contains(ri)) continue;
                            var candidate = frameSources[ri];
                            if (candidate.SpectralProfile == null) continue;
                            if (candidate.TotalEnergy >= src.TotalEnergy) continue;
                            double sim = CosineSimilarity(src.SpectralProfile, candidate.SpectralProfile);
                            if (sim > 0.85)
                            {
                                foreach (int k in candidate.BinIndices)
                                {
                                    double curMag = fftBuffer[k].Magnitude;
                                    if (curMag < _prevOrigMag[k] * KillMercy) continue;
                                    fftBuffer[k] = Complex.Zero;
                                    fftBuffer[FftSize - k] = Complex.Zero;
                                    killedBins++;
                                }
                                killed.Add(ri);
                                TrackSource(candidate);
                            }
                        }
                    }

                    // Expire stale sources (unseen for 50 frames = ~0.5s)
                    // 집중감시방향(EverVoice)은 더 오래 유지 (600 frames ≈ 1분)
                    _sourcesCache.RemoveAll(s =>
                        _frameCount - s.LastSeenFrame > (s.EverVoice ? 600 : 50));
                }
            }

            // Extract raw band energies from ORIGINAL spectrum (not DUET-modified)
            double freqPerBin = (double)SampleRate / FftSize;
            var rawEnergies = new double[BandCount];
            var postDuetEnergies = new double[BandCount]; // after DUET kill = voice estimate
            for (int b = 0; b < BandCount; b++)
            {
                int binLo = Math.Clamp((int)(Bands[b].Lo / freqPerBin), 0, FftSize / 2 - 1);
                int binHi = Math.Clamp((int)(Bands[b].Hi / freqPerBin), binLo + 1, FftSize / 2);

                double energy = 0, postEnergy = 0;
                for (int k = binLo; k < binHi; k++)
                {
                    energy += origMag[k];                  // pre-DUET (all sounds)
                    postEnergy += fftBuffer[k].Magnitude;  // post-DUET (noise killed)
                }
                int binCount = binHi - binLo;
                rawEnergies[b] = energy / binCount;
                postDuetEnergies[b] = postEnergy / binCount;
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
            var voiceLevels = _isStereo ? new int[BandCount] : null; // post-DUET voice levels
            for (int b = 0; b < BandCount; b++)
            {
                double clean = Math.Max(0, rawEnergies[b] - _noiseAvg[b]);
                double midClean = Math.Max(_signalMid[b] - _noiseAvg[b], 0.001);
                int level = (int)(clean / midClean * 7); // mid → 7, 2×mid → 14~15
                level = Math.Clamp(level, 0, MaxLevel);
                levels[b] = level;
                token |= (uint)level << (32 - (b + 1) * BitsPerBand);
                if (clean > maxEnergy) maxEnergy = clean;

                // Post-DUET voice levels: same quantization but from filtered spectrum
                if (voiceLevels != null)
                {
                    double vClean = Math.Max(0, postDuetEnergies[b] - _noiseAvg[b]);
                    int vLevel = (int)(vClean / midClean * 7);
                    voiceLevels[b] = Math.Clamp(vLevel, 0, MaxLevel);
                }
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

            // Syllable-window confirmation: same code N frames in a row → confirmed
            if (soundCode != 0 && SyllableFrames > 0)
            {
                if (soundCode == _scRunCode)
                    _scRunCount++;
                else
                    { _scRunCode = soundCode; _scRunCount = 1; }

                if (_scRunCount >= SyllableFrames)
                    _confirmedSc = soundCode;
                // else: keep previous _confirmedSc (don't clear on transients)
            }
            else if (soundCode == 0)
            {
                _scRunCount = 0; // silence resets run
            }

            if (!isSilence)
            {
                _recentTokens.Add(token);
                if (_recentTokens.Count > 100) _recentTokens.RemoveAt(0);
            }

            _frameCount++;

            // Save current magnitudes for next frame's DUET kill gate
            Array.Copy(origMag, _prevOrigMag, HalfFft);

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
                RmsDb = rmsDb,
                IsStereo = _isStereo,
                KilledBins = killedBins,
                TrackedSources = _sourcesCache.Count,
                VoiceClusters = voiceClusters,
                NoiseClusters = noiseClusters,
                VoiceLevels = voiceLevels,
                ConfirmedSoundCode = _confirmedSc,
            });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WHISPER:CRASH] AnalysisLoop exception: {ex.GetType().Name}: {ex.Message}");
                Console.WriteLine($"[WHISPER:CRASH] {ex.StackTrace}");
                // Don't rethrow — keep the loop alive
            }
        }
    }

    public void Dispose()
    {
        Stop();
        _cts?.Dispose();
    }

    /// <summary>Whether stereo DUET spatial analysis is active.</summary>
    public bool IsStereo => _isStereo;

    /// <summary>Enable DUET hard kill (directional noise source removal). Default OFF.</summary>
    public bool EnableHardKill { get; set; }

    /// <summary>Noise gate in dBFS. Below this = QUIET (skip FFT). Default -65.</summary>
    public float NoiseGateDb { get; set; } = DefaultNoiseGateDb;

    /// <summary>Kill level in dBFS. Sources above this get hard-killed (when EnableHardKill=true). Default 0 = auto (noiseAvg×10).</summary>
    public float KillLevelDb { get; set; }

    /// <summary>
    /// Kill mercy ratio (0.0~1.0). Skip killing a bin if its current energy
    /// dropped below this fraction of the previous frame's energy.
    /// Preserves fading/distant voice remnants from over-kill.
    /// 0.0 = no mercy (kill everything), 0.2 = spare bins below 20% of prev frame. Default 0.2.
    /// </summary>
    public double KillMercy { get; set; } = 0.2;

    /// <summary>
    /// Syllable window: consecutive frames with same sound code required for confirmation.
    /// 3 frames × 64ms(1024 FFT) = ~192ms ≈ one Korean syllable (초성+중성+종성).
    /// 0 = no confirmation (emit raw codes). Default 3.
    /// </summary>
    public int SyllableFrames { get; set; } = 3;

    /// <summary>Last confirmed (stable) sound code. 0 if not yet confirmed.</summary>
    public ushort ConfirmedSoundCode => _confirmedSc;

    /// <summary>Band info for UI display.</summary>
    public static (int Lo, int Hi, string Name)[] GetBands() => Bands;

    // ── DUET clustering: group TF bins by IPD direction ──

    private List<SourceCluster> ClusterSources(double[] ipd, double[] ild, double[] energy)
    {
        // Simple histogram-based clustering on IPD
        // IPD range: -π to π, quantize into 32 bins (~11° each)
        const int NumDirBins = 32;
        var dirBins = new List<int>[NumDirBins];
        var dirEnergy = new double[NumDirBins];
        for (int d = 0; d < NumDirBins; d++) dirBins[d] = new List<int>();

        for (int k = 1; k < HalfFft; k++)
        {
            if (energy[k] < 1e-8) continue; // skip silence bins
            // Map IPD [-π,π] → [0, NumDirBins)
            int dir = (int)((ipd[k] + Math.PI) / (2 * Math.PI) * NumDirBins);
            dir = Math.Clamp(dir, 0, NumDirBins - 1);
            dirBins[dir].Add(k);
            dirEnergy[dir] += energy[k];
        }

        // Merge adjacent bins into clusters, sort by energy descending
        var clusters = new List<SourceCluster>();
        for (int d = 0; d < NumDirBins; d++)
        {
            if (dirBins[d].Count == 0) continue;
            double angle = (d + 0.5) / NumDirBins * 2 * Math.PI - Math.PI;

            // Try merge with existing cluster if angle is close
            bool merged = false;
            foreach (var c in clusters)
            {
                if (Math.Abs(c.Angle - angle) < SourceMergeAngle ||
                    Math.Abs(c.Angle - angle + 2 * Math.PI) < SourceMergeAngle ||
                    Math.Abs(c.Angle - angle - 2 * Math.PI) < SourceMergeAngle)
                {
                    c.BinIndices.AddRange(dirBins[d]);
                    c.TotalEnergy += dirEnergy[d];
                    merged = true;
                    break;
                }
            }
            if (!merged)
            {
                clusters.Add(new SourceCluster
                {
                    Angle = angle,
                    BinIndices = new List<int>(dirBins[d]),
                    TotalEnergy = dirEnergy[d],
                });
            }
        }

        // Sort descending by energy — peel strongest first
        clusters.Sort((a, b) => b.TotalEnergy.CompareTo(a.TotalEnergy));
        return clusters;
    }

    private double GetBreathEnergyThreshold()
    {
        // If explicit kill level set (dBFS), convert to linear energy
        if (KillLevelDb < 0)
            return Math.Pow(10, KillLevelDb / 10.0); // dBFS → linear

        // Auto: noise floor average × 10 — only kill clearly loud sources
        double noiseSum = 0;
        for (int b = 0; b < BandCount; b++) noiseSum += _noiseAvg[b];
        return Math.Max(noiseSum / BandCount * 10.0, 0.01);
    }

    private void TrackSource(SourceCluster src)
    {
        // Update or add to source cache (frame-to-frame tracking)
        double voiceVote = src.IsVoiceLike ? 1.0 : 0.0;
        foreach (var cached in _sourcesCache)
        {
            double angleDiff = Math.Abs(cached.Angle - src.Angle);
            if (angleDiff > Math.PI) angleDiff = 2 * Math.PI - angleDiff;
            if (angleDiff < SourceMergeAngle)
            {
                cached.Angle = cached.Angle * 0.8 + src.Angle * 0.2;
                cached.Energy = src.TotalEnergy;
                cached.FramesSeen++;
                cached.LastSeenFrame = _frameCount;
                // Voice confidence EMA — 0.1 alpha ≈ 0.5s convergence at 100fps
                cached.VoiceConfidence += (voiceVote - cached.VoiceConfidence) * 0.1;
                // Spectral stability: cosine similarity between consecutive profiles
                if (src.SpectralProfile != null && cached.LastProfile != null)
                {
                    double sim = CosineSimilarity(cached.LastProfile, src.SpectralProfile);
                    cached.SpectralStability += (sim - cached.SpectralStability) * 0.15; // α=0.15, ~0.3s convergence
                }
                if (src.SpectralProfile != null)
                    cached.LastProfile = src.SpectralProfile;
                // 음악 의심: 스펙트럼 안정 + 충분한 관찰 → 음성처럼 보여도 음악
                // Music has stable harmonics (sim ~0.9), voice changes formants (sim ~0.5)
                if (cached.SpectralStability > 0.85 && cached.FramesSeen > 15)
                {
                    if (!cached.SuspectedMusic)
                        LogDecisionChange(cached, "MUSIC", $"stability={cached.SpectralStability:F2} frames={cached.FramesSeen}");
                    cached.SuspectedMusic = true;
                    cached.EverVoice = false;       // 집중감시 해제
                    cached.ConfirmedNoise = true;   // 척살집중 전환
                }
                // 음악→음성 복귀: stability가 떨어지면 (사람이 말하기 시작) 음악 의심 해제
                else if (cached.SuspectedMusic && cached.SpectralStability < 0.7)
                {
                    LogDecisionChange(cached, "VOICE_BACK", $"stability={cached.SpectralStability:F2}");
                    cached.SuspectedMusic = false;
                    cached.ConfirmedNoise = false;  // 척살 해제 → 재분석 대상
                }
                // 집중감시: 한 번이라도 음성 → 방향 기억 (단, 음악 의심 시 제외)
                if (src.IsVoiceLike && !cached.SuspectedMusic)
                {
                    if (!cached.EverVoice)
                        LogDecisionChange(cached, "VOICE", $"conf={cached.VoiceConfidence:F2}");
                    cached.EverVoice = true; cached.ConfirmedNoise = false;
                }
                // 척살집중: EMA 안정 + 충분한 관찰 → 소음 확정
                if (!cached.EverVoice && cached.VoiceConfidence < 0.2 && cached.FramesSeen > 10)
                {
                    if (!cached.ConfirmedNoise)
                        LogDecisionChange(cached, "KILL", $"conf={cached.VoiceConfidence:F2} frames={cached.FramesSeen}");
                    cached.ConfirmedNoise = true;
                }
                return;
            }
        }

        if (_sourcesCache.Count < MaxSources)
        {
            _sourcesCache.Add(new SourceInfo
            {
                Angle = src.Angle,
                Energy = src.TotalEnergy,
                FramesSeen = 1,
                LastSeenFrame = _frameCount,
                VoiceConfidence = voiceVote,
                EverVoice = src.IsVoiceLike,
            });
        }
    }

    /// <summary>Get voice confidence for a tracked source near the given angle.
    /// 집중감시 (EverVoice) → 1.0, 척살집중 (ConfirmedNoise) → 0.0.</summary>
    private double GetTrackedVoiceConfidence(double angle)
    {
        foreach (var cached in _sourcesCache)
        {
            double diff = Math.Abs(cached.Angle - angle);
            if (diff > Math.PI) diff = 2 * Math.PI - diff;
            if (diff < SourceMergeAngle)
                return cached.EverVoice ? 1.0
                     : cached.ConfirmedNoise ? 0.0
                     : cached.VoiceConfidence;
        }
        return 0.5; // unknown → neutral
    }

    /// <summary>Log decision changes for parameter tuning. Outputs angle, energy, confidence, stability.</summary>
    private void LogDecisionChange(SourceInfo src, string newState, string detail)
    {
        var angleDeg = src.Angle * 180.0 / Math.PI;
        Console.WriteLine($"[DUET] {newState} @{angleDeg:F0}° E={src.Energy:F1} {detail}");
    }

    /// <summary>Check if a tracked source near the angle is suspected music.</summary>
    private bool IsSuspectedMusic(double angle)
    {
        foreach (var cached in _sourcesCache)
        {
            double diff = Math.Abs(cached.Angle - angle);
            if (diff > Math.PI) diff = 2 * Math.PI - diff;
            if (diff < SourceMergeAngle)
                return cached.SuspectedMusic;
        }
        return false;
    }

    /// <summary>Currently tracked directional sources (각음원). Returns snapshot (thread-safe).</summary>
    public IReadOnlyList<SourceInfo> TrackedSources
    {
        get { try { return _sourcesCache.ToList(); } catch { return Array.Empty<SourceInfo>(); } }
    }

    // ── Voice/Noise classifier for 각음원 clusters (킬원음 판별) ──

    /// <summary>
    /// Classify a direction cluster as voice-like or noise (킬원음).
    /// Voice: low-band dominant (formants in 70-3557Hz) + peaked spectrum (harmonics).
    /// Noise/keyboard: flat spectrum or high-band dominant.
    /// </summary>
    private static bool IsVoiceLikeCluster(double[]? profile)
    {
        if (profile == null || profile.Length < BandCount) return false;

        // Criterion 1: Voice Band Dominance
        // Bands 0-4 (70-3557Hz) = vocal cord + formants F1-F3
        // Bands 5-7 (3557-11197Hz) = sibilant/breath/noise
        double voiceSum = 0, noiseSum = 0;
        for (int b = 0; b < profile.Length; b++)
        {
            if (b < 5) voiceSum += profile[b];
            else noiseSum += profile[b];
        }
        double total = voiceSum + noiseSum;
        if (total < 1e-6) return false;
        bool lowBandDominant = voiceSum / total > 0.55;

        // Criterion 2: Spectral Peakiness (voice has formant peaks, noise/click is flat)
        // max(profile) / mean(profile) — voice > 1.3, broadband impulse ≈ 1.0
        double max = 0, sum = 0;
        for (int b = 0; b < profile.Length; b++)
        {
            sum += profile[b];
            if (profile[b] > max) max = profile[b];
        }
        double mean = sum / profile.Length;
        bool hasPeaks = mean > 1e-6 && max / mean > 1.3;

        return lowBandDominant && hasPeaks;
    }

    // ── Reflection detection helpers ──

    private double[] ComputeSpectralProfile(List<int> binIndices, Complex[] fft)
    {
        // Map cluster's bins into 8-band energy profile (same bands as main pipeline)
        double freqPerBin = (double)SampleRate / FftSize;
        var profile = new double[BandCount];
        var counts = new int[BandCount];
        foreach (int k in binIndices)
        {
            double freq = k * freqPerBin;
            for (int b = 0; b < BandCount; b++)
            {
                if (freq >= Bands[b].Lo && freq < Bands[b].Hi)
                {
                    profile[b] += fft[k].Magnitude;
                    counts[b]++;
                    break;
                }
            }
        }
        // Normalize by bin count + log scale
        for (int b = 0; b < BandCount; b++)
            profile[b] = Math.Log(1 + (counts[b] > 0 ? profile[b] / counts[b] : 0));
        return profile;
    }

    private static double CosineSimilarity(double[] a, double[] b)
    {
        double dot = 0, magA = 0, magB = 0;
        for (int i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            magA += a[i] * a[i];
            magB += b[i] * b[i];
        }
        double denom = Math.Sqrt(magA) * Math.Sqrt(magB);
        return denom > 1e-10 ? dot / denom : 0;
    }

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

/// <summary>Intermediate: group of TF bins sharing a direction (각음원 = 角音源, directional source cluster).</summary>
internal sealed class SourceCluster
{
    public double Angle { get; set; }
    public List<int> BinIndices { get; set; } = [];
    public double TotalEnergy { get; set; }
    /// <summary>8-band energy profile for reflection matching (log-scale).</summary>
    public double[]? SpectralProfile { get; set; }
    /// <summary>Per-frame voice classification result.</summary>
    public bool IsVoiceLike { get; set; }
}

/// <summary>Tracked directional source across frames (각음원 추적).</summary>
internal sealed class SourceInfo
{
    public double Angle { get; set; }            // IPD-based direction (radians)
    public double Energy { get; set; }           // latest energy
    public int FramesSeen { get; set; }          // how many frames this source appeared
    public int LastSeenFrame { get; set; }       // for staleness detection
    /// <summary>Voice confidence EMA: 0.0=킬원음(noise/각음원 제거대상), 1.0=음성(voice).</summary>
    public double VoiceConfidence { get; set; }
    /// <summary>집중감시방향: 한 번이라도 음성 판정 → true (만료 전까지 음성 우선 보호).</summary>
    public bool EverVoice { get; set; }
    /// <summary>척살집중대상: VoiceConfidence가 0.2 이하로 안정 → true (즉시 킬, 분석 건너뜀).</summary>
    public bool ConfirmedNoise { get; set; }
    /// <summary>Previous frame's spectral profile — for stability comparison.</summary>
    public double[]? LastProfile { get; set; }
    /// <summary>Spectral stability EMA (cosine similarity between consecutive profiles).
    /// Music ~0.9 (stable harmonics), voice ~0.5 (changing formants), clicks ~0.2 (random).</summary>
    public double SpectralStability { get; set; } = 0.5;
    /// <summary>음악 의심: SpectralStability > 0.85 AND FramesSeen > 15 → voice→noise 강등.</summary>
    public bool SuspectedMusic { get; set; }
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
    /// <summary>RMS level in dBFS (noise gate reference).</summary>
    public float RmsDb { get; init; }
    // DUET spatial analysis
    public bool IsStereo { get; init; }
    public int KilledBins { get; init; }      // how many bins were hard-zeroed this frame
    public int TrackedSources { get; init; }  // number of tracked directional sources
    public int VoiceClusters { get; init; }   // voice-classified clusters (preserved)
    public int NoiseClusters { get; init; }   // noise/킬원음 clusters (killed)
    /// <summary>Post-DUET band levels (noise bins zeroed). null if mono/no DUET.</summary>
    public int[]? VoiceLevels { get; init; }
    /// <summary>Confirmed sound code (stable across SyllableFrames). 0 if not yet confirmed.</summary>
    public ushort ConfirmedSoundCode { get; init; }
}
