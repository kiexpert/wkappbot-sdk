using NAudio.Wave;
using Microsoft.VisualBasic.FileIO;

namespace WKAppBot.CLI;

// wkappbot whisper clean — analyze _unknown/ MP3s offline, trash noise, keep speech
//
// Usage:
//   wkappbot whisper clean [--threshold N] [--dry-run] [--wav-dir]
//
// For each MP3 in _unknown/ without _stt_ tag:
//   - Decode PCM, compute RMS-based voice activity per 64ms frame
//   - If voice ratio < threshold → Recycle Bin
//   - If voice ratio >= threshold → keep in _unknown/ (wait for Gemini)
//
// For each MP3 in wav/ root (old files without _stt_ tag):
//   - Same analysis
//   - If speech → move to _unknown/ for Gemini
//   - If noise → Recycle Bin
//
//   --threshold N   voice frame ratio % to consider speech (default: 15)
//   --dry-run       print decisions without moving/deleting
//   --wav-dir       also scan wav/ root (old recordings)

internal partial class Program
{
    static int WhisperCleanCommand(string[] args)
    {
        int  threshold = 15;   // % of frames that must be voice to keep
        bool dryRun    = false;
        bool scanWav   = false;

        for (int i = 0; i < args.Length; i++)
        {
            if      (args[i] == "--threshold" && i + 1 < args.Length) int.TryParse(args[++i], out threshold);
            else if (args[i] == "--dry-run")  dryRun  = true;
            else if (args[i] == "--wav-dir")  scanWav = true;
        }

        var basePath   = Path.Combine(DataDir, "profiles", "whisper_exp");
        var unknownDir = Path.Combine(basePath, "wav", "_unknown");
        var wavDir     = Path.Combine(basePath, "wav");

        Console.WriteLine($"[CLEAN] threshold={threshold}% voice frames, dry-run={dryRun}");

        int trashed = 0, kept = 0, movedToUnknown = 0, errors = 0;

        // ── Scan _unknown/: no _stt_ tag → analyze → trash or keep ──
        if (Directory.Exists(unknownDir))
        {
            var files = Directory.GetFiles(unknownDir, "*.mp3")
                .Where(f => !Path.GetFileName(f).Contains("_stt_"))
                .OrderBy(f => f)
                .ToArray();

            Console.WriteLine($"[CLEAN] _unknown/: {files.Length} untagged files");

            foreach (var mp3 in files)
            {
                try
                {
                    var (voicePct, durationMs) = AnalyzeSpeechRatio(mp3);
                    var name = Path.GetFileName(mp3);

                    if (voicePct >= threshold)
                    {
                        kept++;
                        Console.WriteLine($"[CLEAN] KEEP  {name} ({voicePct:F0}% voice, {durationMs}ms)");
                    }
                    else
                    {
                        trashed++;
                        Console.WriteLine($"[CLEAN] TRASH {name} ({voicePct:F0}% voice, {durationMs}ms)");
                        if (!dryRun)
                            FileSystem.DeleteFile(mp3, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    }
                }
                catch (Exception ex)
                {
                    errors++;
                    Console.WriteLine($"[CLEAN] ERROR {Path.GetFileName(mp3)}: {ex.Message}");
                }
            }
        }

        // ── Scan wav/ root: speech without tag → move to _unknown/ ──
        if (scanWav && Directory.Exists(wavDir))
        {
            var files = Directory.GetFiles(wavDir, "*.mp3")
                .Where(f => !Path.GetFileName(f).Contains("_stt_"))
                .OrderBy(f => f)
                .ToArray();

            Console.WriteLine($"[CLEAN] wav/: {files.Length} untagged files");
            Directory.CreateDirectory(unknownDir);

            foreach (var mp3 in files)
            {
                try
                {
                    var (voicePct, durationMs) = AnalyzeSpeechRatio(mp3);
                    var name = Path.GetFileName(mp3);

                    if (voicePct >= threshold)
                    {
                        movedToUnknown++;
                        var dest = Path.Combine(unknownDir, name);
                        Console.WriteLine($"[CLEAN] →_unknown {name} ({voicePct:F0}% voice)");
                        if (!dryRun && !File.Exists(dest))
                            File.Move(mp3, dest);
                    }
                    else
                    {
                        trashed++;
                        Console.WriteLine($"[CLEAN] TRASH {name} ({voicePct:F0}% voice, {durationMs}ms)");
                        if (!dryRun)
                            FileSystem.DeleteFile(mp3, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    }
                }
                catch (Exception ex)
                {
                    errors++;
                    Console.WriteLine($"[CLEAN] ERROR {Path.GetFileName(mp3)}: {ex.Message}");
                }
            }
        }

        Console.WriteLine($"\n[CLEAN] Done — kept={kept} trashed={trashed} →_unknown={movedToUnknown} errors={errors}");
        return errors > 0 ? 1 : 0;
    }

    /// <summary>
    /// Decode MP3, compute RMS-based voice activity ratio.
    /// Returns (voicePercent 0-100, totalDurationMs).
    /// A frame = 64ms block at 16kHz (1024 samples).
    /// Frame is "voice" if RMS > -50dBFS.
    /// </summary>
    static (float voicePct, int durationMs) AnalyzeSpeechRatio(string mp3Path, float voiceGateDb = -50f)
    {
        const int sampleRate   = 16000;
        const int blockSamples = 1024;  // 64ms at 16kHz

        int totalFrames = 0, voiceFrames = 0;

        using var reader = new Mp3FileReader(mp3Path);
        var monoFormat = new WaveFormat(sampleRate, 16, 1);
        using var resampler = new MediaFoundationResampler(reader, monoFormat) { ResamplerQuality = 10 };

        var buf = new byte[blockSamples * 2]; // 16bit mono
        int read;
        while ((read = resampler.Read(buf, 0, buf.Length)) > 0)
        {
            int samples = read / 2;
            float rmsSum = 0f;
            for (int i = 0; i < read - 1; i += 2)
            {
                float s = (short)(buf[i] | (buf[i + 1] << 8)) / 32768f;
                rmsSum += s * s;
            }
            float rmsDb = 10f * MathF.Log10(rmsSum / samples + 1e-10f);
            totalFrames++;
            if (rmsDb > voiceGateDb) voiceFrames++;
        }

        int durationMs = totalFrames > 0 ? totalFrames * 64 : 0;
        float pct = totalFrames > 0 ? voiceFrames * 100f / totalFrames : 0f;
        return (pct, durationMs);
    }
}
