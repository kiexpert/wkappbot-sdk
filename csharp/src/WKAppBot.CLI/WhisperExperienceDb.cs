using System.Speech.AudioFormat;
using System.Speech.Recognition;
using System.Text.Json;
using NAudio.Lame;
using NAudio.Wave;

namespace WKAppBot.CLI;

/// <summary>
/// Whisper Experience DB — token JSONL logging + Windows STT auto-labeling.
/// Logs every non-silence frame with 32-bit token, mode, levels, and optional STT label.
///
/// Architecture:
///   WhisperEngine.OnFrame → WhisperExperienceDb.LogFrame()
///   Windows STT (parallel) → recognized text tagged to recent token window
///
/// Storage: wkappbot.hq/profiles/whisper_exp/
///   tokens_{date}.jsonl  — per-frame token log
///   stt_{date}.jsonl     — STT recognition results with token window
/// </summary>
internal sealed class WhisperExperienceDb : IDisposable
{
    private readonly string _basePath;
    private StreamWriter? _tokenWriter;
    private StreamWriter? _sttWriter;
    private string _currentDate = "";
    private readonly object _writeLock = new();

    // STT engine + WASAPI loopback (system audio → STT for learning)
    private SpeechRecognitionEngine? _sttEngine;
    private Thread? _sttThread;
    private volatile bool _sttRunning;
    private volatile string? _lastSttResult;
    private long _lastSttTicks;
    private volatile string _lastSttMode = "QUIET"; // mode at recognition time
    private WasapiLoopbackCapture? _loopback;
    private SttPipeStream? _sttPipe;

    // Recent token window for STT alignment
    private readonly List<(long ticks, uint token, string mode)> _tokenWindow = new();
    private const int TokenWindowSize = 60; // ~2 seconds at 33fps
    private volatile string _currentMode = "QUIET"; // updated every LogFrame

    // MP3 segment recording (voice activity → sentence-level MP3 files)
    private string? _wavDir;
    private LameMP3FileWriter? _mp3Writer;
    private string? _wavPath; // current segment file path
    private volatile bool _isRecording;
    private int _quietFrames;
    private int _segmentFrames; // voice frames in current segment (for min-length gate)
    private const int PauseFrames = 33;      // ~330ms silence = ".." (comma, short breath)
    private const int SentenceEndFrames = 99; // ~1s silence = "." (period, sentence end → cut)
    private const int MinSegmentFrames  = 100; // ~3 seconds — skip tiny bursts for Gemini
    private const int MinVoicedFrames   = 9;   // ~90ms  — fewer → discard file entirely
    private readonly object _wavLock = new();
    private volatile string? _segmentSttLabel; // STT draft label for current segment

    // Mic MP3 segment recording (parallel to loopback MP3, for Gemini STT)
    private LameMP3FileWriter? _micMp3Writer;
    private string? _micMp3Path;
    private int _micChannels = 1; // set by SetMicChannels to match WhisperEngine capture format
    private readonly object _micWavLock = new();

    // Sound code accumulator for segment filename
    private readonly List<ushort> _segmentSoundCodes = new();

    // Gemini STT callback: fires when mic segment is ready for transcription
    /// <summary>Fired when a mic voice segment MP3 is ready. Handler should send to Gemini.</summary>
    public event Action<string>? OnMicSegmentReady; // arg = mp3 file path

    // Auto-study trigger: fires when _unknown/ reaches threshold
    private int _autoStudyThreshold = 10;
    private volatile bool _autoStudyRunning;
    /// <summary>Fired when _unknown/ folder reaches threshold. Handler should run study in background.</summary>
    public event Action<int>? OnAutoStudyNeeded; // arg = file count

    public bool IsLogging => _tokenWriter != null;
    public bool IsSttActive => _sttRunning;
    public int SegmentFrames => _segmentFrames;

    public WhisperExperienceDb(string? basePath = null)
    {
        _basePath = basePath ?? Path.Combine(
            AppContext.BaseDirectory, "wkappbot.hq", "profiles", "whisper_exp");
        Directory.CreateDirectory(_basePath);
    }

    /// <summary>Start JSONL token logging.</summary>
    public void StartLogging()
    {
        EnsureWriter();
    }

    /// <summary>Start Windows STT fed by WASAPI loopback (system audio → STT for learning).</summary>
    public bool StartStt()
    {
        if (_sttRunning) return true;

        try
        {
            // Prefer English recognizer (YouTube content) — Korean segments → _unknown/ → Gemini handles
            var recognizers = SpeechRecognitionEngine.InstalledRecognizers();
            var chosen = recognizers.FirstOrDefault(r => r.Culture.Name.StartsWith("en"))
                      ?? recognizers.FirstOrDefault(r => r.Culture.Name.StartsWith("ko"))
                      ?? recognizers.First();
            Console.WriteLine($"[WHISPER] STT recognizer: {chosen.Description} ({chosen.Culture.Name})");
            _sttEngine = new SpeechRecognitionEngine(chosen);

            _sttEngine.LoadGrammar(new DictationGrammar());

            // WASAPI loopback → resample to 16kHz mono 16bit → pipe to STT
            _loopback = new WasapiLoopbackCapture();
            _sttPipe = new SttPipeStream();
            var srcFormat = _loopback.WaveFormat;

            _loopback.DataAvailable += (_, e) =>
            {
                // Stereo resample (48kHz float → 16kHz 16bit stereo) for WAV recording
                var src1 = new RawSourceWaveStream(new MemoryStream(e.Buffer, 0, e.BytesRecorded), srcFormat);
                using var stereoResampler = new MediaFoundationResampler(src1, new WaveFormat(16000, 16, 2))
                {
                    ResamplerQuality = 30,
                };
                var stereoBuf = new byte[e.BytesRecorded];
                int stereoRead = stereoResampler.Read(stereoBuf, 0, stereoBuf.Length);
                if (stereoRead > 0)
                    WriteWavData(stereoBuf, stereoRead);

                // Mono resample (48kHz float → 16kHz 16bit mono) for STT engine
                var src2 = new RawSourceWaveStream(new MemoryStream(e.Buffer, 0, e.BytesRecorded), srcFormat);
                using var monoResampler = new MediaFoundationResampler(src2, new WaveFormat(16000, 16, 1))
                {
                    ResamplerQuality = 30,
                };
                var monoBuf = new byte[e.BytesRecorded];
                int monoRead = monoResampler.Read(monoBuf, 0, monoBuf.Length);
                if (monoRead > 0)
                    _sttPipe.Write(monoBuf, 0, monoRead);
            };

            var sttFormat = new SpeechAudioFormatInfo(16000, AudioBitsPerSample.Sixteen, AudioChannel.Mono);
            _sttEngine.SetInputToAudioStream(_sttPipe, sttFormat);

            WireSttEvents();

            _loopback.StartRecording();

            _sttRunning = true;
            _sttThread = new Thread(() =>
            {
                try { _sttEngine.RecognizeAsync(RecognizeMode.Multiple); }
                catch (Exception ex)
                {
                    _sttRunning = false;
                    Console.Error.WriteLine($"[WHISPER] STT RecognizeAsync failed: {ex.Message}");
                }
            })
            {
                IsBackground = true,
                Name = "WhisperSTT",
            };
            _sttThread.Start();
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[WHISPER] STT loopback failed: {ex.Message}, falling back to mic");
            // Fallback: mic input
            try { _loopback?.Dispose(); } catch { }
            _loopback = null;
            _sttPipe = null;
            return StartSttMic();
        }
    }

    /// <summary>Fallback: STT from default microphone.</summary>
    private bool StartSttMic()
    {
        try
        {
            if (_sttEngine == null)
            {
                var recognizers = SpeechRecognitionEngine.InstalledRecognizers();
                var chosen = recognizers.FirstOrDefault(r => r.Culture.Name.StartsWith("en"))
                          ?? recognizers.FirstOrDefault(r => r.Culture.Name.StartsWith("ko"))
                          ?? recognizers.First();
                _sttEngine = new SpeechRecognitionEngine(chosen);
            }

            if (_sttEngine.Grammars.Count == 0)
                _sttEngine.LoadGrammar(new DictationGrammar());
            _sttEngine.SetInputToDefaultAudioDevice();

            WireSttEvents();

            _sttRunning = true;
            _sttThread = new Thread(() =>
            {
                try { _sttEngine.RecognizeAsync(RecognizeMode.Multiple); }
                catch (Exception ex)
                {
                    _sttRunning = false;
                    Console.Error.WriteLine($"[WHISPER] STT-Mic RecognizeAsync failed: {ex.Message}");
                }
            })
            {
                IsBackground = true,
                Name = "WhisperSTT-Mic",
            };
            _sttThread.Start();
            return true;
        }
        catch
        {
            _sttEngine?.Dispose();
            _sttEngine = null;
            return false;
        }
    }

    private bool _eventsWired;
    private void WireSttEvents()
    {
        if (_eventsWired || _sttEngine == null) return;
        _eventsWired = true;

        _sttEngine.SpeechRecognized += (_, e) =>
        {
            Console.Error.WriteLine($"[WHISPER:STT] recognized: \"{e.Result.Text}\" conf={e.Result.Confidence:F2}");
            if (e.Result.Confidence < 0.3f) return;

            var result = new SttResult
            {
                Ticks = DateTime.UtcNow.Ticks,
                Text = e.Result.Text,
                Confidence = e.Result.Confidence,
                TokenWindow = GetRecentTokens(),
            };

            _lastSttResult = e.Result.Text;
            _lastSttTicks = result.Ticks;
            _lastSttMode = _currentMode;
            if (_isRecording) _segmentSttLabel = e.Result.Text; // label WAV with STT draft

            WriteSttResult(result);
        };

        _sttEngine.SpeechRecognitionRejected += (_, e) =>
        {
            var topAlt = e.Result.Alternates.Count > 0 ? e.Result.Alternates[0] : null;
            Console.Error.WriteLine($"[WHISPER:STT] rejected: alts={e.Result.Alternates.Count} top=\"{topAlt?.Text}\" conf={topAlt?.Confidence:F2}");
            if (e.Result.Alternates.Count > 0)
            {
                var best = e.Result.Alternates[0];
                if (best.Confidence >= 0.15f)
                {
                    var result = new SttResult
                    {
                        Ticks = DateTime.UtcNow.Ticks,
                        Text = $"?{best.Text}",
                        Confidence = best.Confidence,
                        TokenWindow = GetRecentTokens(),
                    };

                    _lastSttResult = result.Text;
                    _lastSttTicks = result.Ticks;
                    _lastSttMode = _currentMode;

                    WriteSttResult(result);
                }
            }
        };
    }

    /// <summary>Log a single frame (call from WhisperEngine.OnFrame).</summary>
    public void LogFrame(WhisperFrame frame)
    {
        _currentMode = frame.Mode;

        // WAV segment recording: only VOICE/WHSPR are speech — everything else is silence
        bool isSpeech = frame.Mode == "VOICE" || frame.Mode == "WHSPR";
        if (!isSpeech)
        {
            if (_isRecording)
            {
                _quietFrames++;
                // ".." comma: short breath between syllables → insert pause marker (once)
                if (_quietFrames == PauseFrames)
                    _segmentSoundCodes.Add(0); // 0 = pause marker → "-0-" in filename
                // "." period: long silence → sentence end → cut!
                if (_quietFrames >= SentenceEndFrames)
                {
                    Console.WriteLine($"[WHISPER:.] speech={_segmentFrames}f({_segmentFrames * 10}ms) pause={_quietFrames}f mode={frame.Mode}");
                    StopWavSegment();
                    _segmentFrames = 0;
                }
            }
            if (frame.Mode == "QUIET") return; // skip silence for token log
        }
        else
        {
            _quietFrames = 0;
            if (!_isRecording)
                StartWavSegment(frame.Mode);
            _segmentFrames++;
            _segmentSoundCodes.Add(frame.SoundCode);

            // Long sentence warning: 9s (~300 frames) → debug dump (once per sentence)
            if (_segmentFrames == 300)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[WHISPER:LONG] 9s+ sentence! frames={_segmentFrames} mode={frame.Mode} sc=0x{frame.SoundCode:X4} energy={frame.MaxEnergy:F4}");
                Console.WriteLine($"[WHISPER:LONG] unique_codes={_segmentSoundCodes.Distinct().Count()}/{_segmentSoundCodes.Count} quiet_streak={_quietFrames}");
                Console.ResetColor();
            }
        }

        var ticks = DateTime.UtcNow.Ticks;

        // Track token window for STT alignment
        lock (_tokenWindow)
        {
            _tokenWindow.Add((ticks, frame.Token, frame.Mode));
            while (_tokenWindow.Count > TokenWindowSize)
                _tokenWindow.RemoveAt(0);
        }

        // Write token JSONL
        EnsureWriter();
        var entry = new TokenEntry
        {
            T = ticks,
            Tk = frame.Token,
            M = frame.Mode,
            L = frame.Levels,
            E = Math.Round(frame.MaxEnergy, 6),
        };

        lock (_writeLock)
        {
            _tokenWriter?.WriteLine(JsonSerializer.Serialize(entry,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }
    }

    /// <summary>Flush and rotate writers if date changed.</summary>
    public void Flush()
    {
        lock (_writeLock)
        {
            _tokenWriter?.Flush();
            _sttWriter?.Flush();
        }
    }

    private string[] GetRecentTokens()
    {
        lock (_tokenWindow)
        {
            return _tokenWindow
                .TakeLast(20)
                .Select(t => $"{t.token:X8}:{t.mode[0]}")
                .ToArray();
        }
    }

    private void EnsureWriter()
    {
        var date = DateTime.Now.ToString("yyyyMMdd");
        if (date == _currentDate && _tokenWriter != null) return;

        lock (_writeLock)
        {
            _tokenWriter?.Flush();
            _tokenWriter?.Dispose();
            _sttWriter?.Flush();
            _sttWriter?.Dispose();

            _currentDate = date;
            _tokenWriter = new StreamWriter(
                Path.Combine(_basePath, $"tokens_{date}.jsonl"), append: true)
            { AutoFlush = false };
            _sttWriter = new StreamWriter(
                Path.Combine(_basePath, $"stt_{date}.jsonl"), append: true)
            { AutoFlush = false };
        }
    }

    private void StartWavSegment(string mode)
    {
        lock (_wavLock)
        {
            if (_isRecording) return;
            _wavDir ??= Path.Combine(_basePath, "wav");
            Directory.CreateDirectory(_wavDir);

            var tag = mode == "WHSPR" ? "W" : "V";
            _wavPath = Path.Combine(_wavDir, $"{tag}.mp3");
            _segmentSttLabel = null; // reset label for new segment
            _segmentFrames = 0; // reset frame counter
            _segmentSoundCodes.Clear();
            try
            {
                var waveFormat = new WaveFormat(16000, 16, 2); // stereo
                _mp3Writer = new LameMP3FileWriter(_wavPath, waveFormat, 64); // 64kbps — good for speech
                _isRecording = true;
            }
            catch { _mp3Writer = null; _wavPath = null; }
        }
        StartMicSegment(); // parallel mic recording for Gemini STT
    }

    private void StopWavSegment()
    {
        int frames = _segmentFrames;
        lock (_wavLock)
        {
            _isRecording = false;
            // Append 0.5s silence padding for clear sentence boundary
            try
            {
                var silence = new byte[32000]; // 0.5s at 16kHz 16bit stereo = 32000 bytes
                _mp3Writer?.Write(silence, 0, silence.Length);
            }
            catch { }
            try { _mp3Writer?.Dispose(); } catch { }
            _mp3Writer = null;

            // Too short: < MinVoicedFrames → discard file entirely (incomplete phoneme)
            if (frames < MinVoicedFrames && _wavPath != null)
            {
                try { File.Delete(_wavPath); } catch { }
                _wavPath = null;
            }

            // Rename with sound code sequence: 3A1F-5C2E 7D04_20260309_1443_V.mp3
            // '-' between token transitions, ' ' for pause (code=0)
            if (_wavPath != null)
            {
                try
                {
                    if (_segmentSoundCodes.Count > 0)
                    {
                        // Deduplicate consecutive identical sound codes (RLE)
                        var deduped = new List<ushort> { _segmentSoundCodes[0] };
                        for (int i = 1; i < _segmentSoundCodes.Count; i++)
                            if (_segmentSoundCodes[i] != deduped[^1])
                                deduped.Add(_segmentSoundCodes[i]);

                        // Sample up to 8 evenly-spaced from deduped sequence
                        var sampled = new List<ushort>();
                        int step = Math.Max(1, deduped.Count / 8);
                        for (int i = 0; i < deduped.Count && sampled.Count < 8; i += step)
                            sampled.Add(deduped[i]);

                        // Build code string: '-' between non-zero tokens, ' ' for pause (0)
                        var sbCode = new System.Text.StringBuilder();
                        bool lastCode = false;
                        foreach (var c in sampled)
                        {
                            if (c == 0) { sbCode.Append(' '); lastCode = false; }
                            else { if (lastCode) sbCode.Append('-'); sbCode.Append(FormatSoundCode(c, forFile: true)); lastCode = true; }
                        }
                        var scStr = sbCode.ToString().Trim();

                        var dir = Path.GetDirectoryName(_wavPath)!;
                        var origName = Path.GetFileName(_wavPath);
                        // MAX_PATH safety: trim if path would exceed 240 chars
                        var maxScLen = 240 - dir.Length - 1 - 1 - origName.Length;
                        if (maxScLen > 0 && scStr.Length > maxScLen)
                            scStr = scStr[..maxScLen].TrimEnd('-').TrimEnd();
                        var newName = $"{scStr}_{origName}";
                        var newPath = Path.Combine(dir, newName);
                        if (!File.Exists(newPath))
                            File.Move(_wavPath, newPath);
                        // Human-readable scroll: same format (- / space)
                        var sbRead = new System.Text.StringBuilder();
                        bool lastR = false;
                        foreach (var c in sampled)
                        {
                            if (c == 0) { sbRead.Append(' '); lastR = false; }
                            else { if (lastR) sbRead.Append('-'); sbRead.Append(FormatSoundCode(c)); lastR = true; }
                        }
                        Console.WriteLine($"[WHISPER] {newName}  ({deduped.Count} codes, {_segmentSoundCodes.Count} frames)");
                        Console.WriteLine($"[WHISPER] {sbRead}");
                    }
                }
                catch { /* rename is best-effort */ }
            }
            _wavPath = null;
        }

        // Stop mic segment + fire Gemini event (only for meaningful sentences)
        if (frames >= MinSegmentFrames)
            StopMicSegment();
        else
        {
            // Too short — discard mic segment silently
            lock (_micWavLock)
            {
                try { _micMp3Writer?.Dispose(); } catch { }
                _micMp3Writer = null;
                if (_micMp3Path != null) try { File.Delete(_micMp3Path); } catch { }
                _micMp3Path = null;
            }
        }

        // Check auto-study trigger
        CheckAutoStudyTrigger();
    }

    private void CheckAutoStudyTrigger()
    {
        if (_autoStudyRunning || OnAutoStudyNeeded == null) return;
        try
        {
            var unknownDir = Path.Combine(_basePath, "wav", "_unknown");
            if (!Directory.Exists(unknownDir)) return;
            var count = Directory.GetFiles(unknownDir, "*.mp3").Length;
            if (count >= _autoStudyThreshold)
            {
                _autoStudyRunning = true;
                OnAutoStudyNeeded.Invoke(count);
            }
        }
        catch { /* best effort */ }
    }

    /// <summary>Call when auto-study batch completes (success or fail).</summary>
    public void NotifyAutoStudyDone() => _autoStudyRunning = false;

    /// <summary>Format sound code as octal digits (no leading zeros). 0 = pause marker.
    /// If band 0 (VxLip/성대음) is rank 1 → encode ranks 2,3,4 (>>3, leading "0" strips naturally).
    /// Otherwise → encode ranks 1,2,3 (>>6).</summary>
    private static string FormatSoundCode(ushort sc, bool forFile = false)
    {
        if (sc == 0) return forFile ? "0" : "..";
        bool vocalFirst = ((sc >> 12) & 7) == 0; // band 0 is rank 1?
        int val = vocalFirst ? sc >> 3 : sc >> 6;
        return Convert.ToString(val, 8);
    }

    private static string SanitizeFileName(string text)
    {
        // Keep first ~40 chars, replace invalid chars with underscore, trim
        if (text.Length > 40) text = text[..40];
        var invalid = Path.GetInvalidFileNameChars();
        var sb = new System.Text.StringBuilder(text.Length);
        foreach (var c in text)
            sb.Append(invalid.Contains(c) || c == '?' ? '_' : c == ' ' ? '_' : c);
        return sb.ToString().Trim('_');
    }

    /// <summary>Write resampled audio to MP3 segment (called from loopback callback).</summary>
    internal void WriteWavData(byte[] buffer, int count)
    {
        if (!_isRecording || count <= 0) return;
        lock (_wavLock)
        {
            try { _mp3Writer?.Write(buffer, 0, count); }
            catch { /* ignore write errors */ }
        }
    }

    /// <summary>Write mic PCM data to mic MP3 segment (16kHz 16bit mono from WhisperEngine).</summary>
    internal void WriteMicData(byte[] buffer, int count)
    {
        if (!_isRecording || count <= 0) return;
        lock (_micWavLock)
        {
            try { _micMp3Writer?.Write(buffer, 0, count); }
            catch { /* ignore write errors */ }
        }
    }

    /// <summary>Call once after WhisperEngine.Start() to align mic MP3 channel count.</summary>
    internal void SetMicChannels(int channels) => _micChannels = Math.Max(1, channels);

    private void StartMicSegment()
    {
        lock (_micWavLock)
        {
            if (_micMp3Writer != null) return;
            _wavDir ??= Path.Combine(_basePath, "wav");
            var micDir = Path.Combine(_wavDir, "_mic");
            Directory.CreateDirectory(micDir);
            var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            _micMp3Path = Path.Combine(micDir, $"mic_{stamp}.mp3");
            try
            {
                var waveFormat = new WaveFormat(16000, 16, _micChannels);
                _micMp3Writer = new LameMP3FileWriter(_micMp3Path, waveFormat, 64);
            }
            catch { _micMp3Writer = null; _micMp3Path = null; }
        }
    }

    private void StopMicSegment()
    {
        string? completedPath;
        lock (_micWavLock)
        {
            // Append 0.3s silence for clean ending
            try { _micMp3Writer?.Write(new byte[9600], 0, 9600); } catch { }
            try { _micMp3Writer?.Dispose(); } catch { }
            _micMp3Writer = null;
            completedPath = _micMp3Path;
            _micMp3Path = null;
        }
        // Fire event for Gemini transcription
        if (completedPath != null && File.Exists(completedPath))
        {
            var fi = new FileInfo(completedPath);
            if (fi.Length > 2000) // skip tiny segments (< ~0.1s)
                OnMicSegmentReady?.Invoke(completedPath);
            else
                try { File.Delete(completedPath); } catch { }
        }
    }

    private void WriteSttResult(SttResult result)
    {
        EnsureWriter();
        lock (_writeLock)
        {
            _sttWriter?.WriteLine(JsonSerializer.Serialize(result,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            _sttWriter?.Flush(); // STT events are rare, flush immediately
        }
    }

    public void Stop()
    {
        _sttRunning = false;
        StopWavSegment(); // close any active WAV segment
        try { _loopback?.StopRecording(); } catch { }
        try { _loopback?.Dispose(); } catch { }
        _loopback = null;
        try { _sttPipe?.Close(); } catch { }
        _sttPipe = null;
        try { _sttEngine?.RecognizeAsyncCancel(); } catch { }
        try { _sttEngine?.Dispose(); } catch { }
        _sttEngine = null;

        Flush();
        lock (_writeLock)
        {
            _tokenWriter?.Dispose();
            _tokenWriter = null;
            _sttWriter?.Dispose();
            _sttWriter = null;
        }
    }

    public void Dispose() => Stop();

    /// <summary>Get stats for display.</summary>
    public (string? lastStt, long lastSttTicks, string lastSttMode, int windowSize) GetStatus()
        => (_lastSttResult, _lastSttTicks, _lastSttMode, _tokenWindow.Count);

    // ── JSON models ──

    private sealed class TokenEntry
    {
        public long T { get; set; }      // UTC ticks
        public uint Tk { get; set; }     // 32-bit token
        public string M { get; set; } = ""; // mode
        public int[] L { get; set; } = []; // levels[8]
        public double E { get; set; }    // maxEnergy
    }

    private sealed class SttResult
    {
        public long Ticks { get; set; }
        public string Text { get; set; } = "";
        public float Confidence { get; set; }
        public string[] TokenWindow { get; set; } = []; // recent tokens at recognition time
    }
}

/// <summary>
/// Circular buffer Stream for piping WASAPI loopback audio to SpeechRecognitionEngine.
/// Write side (loopback callback) never blocks. Read side (STT engine) blocks when empty.
/// </summary>
internal sealed class SttPipeStream : Stream
{
    private readonly byte[] _buffer = new byte[64 * 1024]; // 64KB ring
    private int _writePos;
    private int _readPos;
    private int _count;
    private readonly object _lock = new();
    private bool _closed;

    public override bool CanRead => true;
    public override bool CanSeek => false;
    public override bool CanWrite => true;
    public override long Length => _count;
    public override long Position { get => 0; set { } }

    public override int Read(byte[] buffer, int offset, int count)
    {
        lock (_lock)
        {
            while (_count == 0 && !_closed)
                Monitor.Wait(_lock, 100);

            if (_count == 0 && _closed) return 0;

            int toRead = Math.Min(count, _count);
            int firstChunk = Math.Min(toRead, _buffer.Length - _readPos);
            Array.Copy(_buffer, _readPos, buffer, offset, firstChunk);
            if (toRead > firstChunk)
                Array.Copy(_buffer, 0, buffer, offset + firstChunk, toRead - firstChunk);

            _readPos = (_readPos + toRead) % _buffer.Length;
            _count -= toRead;
            return toRead;
        }
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        lock (_lock)
        {
            if (_closed) return;

            // Drop oldest data if buffer full (prefer freshness over completeness)
            int space = _buffer.Length - _count;
            if (count > space)
            {
                int drop = count - space;
                _readPos = (_readPos + drop) % _buffer.Length;
                _count -= drop;
            }

            int toWrite = Math.Min(count, _buffer.Length);
            int srcOffset = offset + count - toWrite; // take the latest bytes if trimmed
            int firstChunk = Math.Min(toWrite, _buffer.Length - _writePos);
            Array.Copy(buffer, srcOffset, _buffer, _writePos, firstChunk);
            if (toWrite > firstChunk)
                Array.Copy(buffer, srcOffset + firstChunk, _buffer, 0, toWrite - firstChunk);

            _writePos = (_writePos + toWrite) % _buffer.Length;
            _count += toWrite;
            Monitor.PulseAll(_lock);
        }
    }

    public override void Close()
    {
        lock (_lock)
        {
            _closed = true;
            Monitor.PulseAll(_lock);
        }
        base.Close();
    }

    public override void Flush() { }
    public override long Seek(long offset, SeekOrigin origin) => 0;
    public override void SetLength(long value) { }
}
