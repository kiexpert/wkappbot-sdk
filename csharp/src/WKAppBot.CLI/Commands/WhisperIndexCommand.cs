using System.Numerics;
using Microsoft.VisualBasic.FileIO;
using NAudio.Wave;

namespace WKAppBot.CLI;

// wkappbot whisper index — extract first-syllable sound token from sliced MP3s → phoneme DB
//
// For each MP3 in slices/:
//   1. Decode to 16kHz mono PCM
//   2. FFT band analysis (same algorithm as WhisperEngine)
//   3. First voiced syllable → dominant soundCode
//   4. Format code: vocaled (band0=rank1) → ranks 2,3,4; else → ranks 1,2,3 (octal, no leading zeros)
//   5. Files with < MinVoicedFrames voiced frames → Recycle Bin (incomplete/ambiguous)
//   6. Create folder: phoneme_db/<code>/
//   7. Copy/move file as <word>_<seq>.mp3
//
// Usage:
//   wkappbot whisper index [--in <slices-dir>] [--out <db-dir>] [--move] [--dry-run]

internal partial class Program
{
    // Band definitions — identical to WhisperEngine.Bands (must stay in sync)
    private static readonly (int Lo, int Hi)[] IndexBands =
    [
        (   70,  1021),   // 0: VxLip — vocal cord + lip burst
        ( 1021,  1397),   // 1: Pharx
        ( 1397,  1699),   // 2: Velar
        ( 1699,  2647),   // 3: OralR
        ( 2647,  3557),   // 4: HiRes
        ( 3557,  4919),   // 5: Burst
        ( 4919,  7507),   // 6: Sibil
        ( 7507, 11197),   // 7: Breth
    ];

    private const int IdxFftSize    = 512;
    private const int IdxSampleRate = 16000;
    private const int IdxBandCount  = 8;
    private const float IdxPreEmph  = 0.97f;

    // Frames with soundCode != 0 that belong to the first syllable
    private const int IdxSilenceGap    = 6;  // consecutive silence frames → syllable ended
    private const int MinVoicedFrames  = 9;  // < 9 voiced frames (~90ms) → too short → Recycle Bin

    static int WhisperIndexCommand(string[] args)
    {
        string? inDir   = null;
        string? outDir  = null;
        bool    move    = false;
        bool    dryRun  = false;

        for (int i = 0; i < args.Length; i++)
        {
            if      (args[i] == "--in"  && i + 1 < args.Length) inDir  = args[++i];
            else if (args[i] == "--out" && i + 1 < args.Length) outDir = args[++i];
            else if (args[i] == "--move")    move   = true;
            else if (args[i] == "--dry-run") dryRun = true;
        }

        var basePath = Path.Combine(DataDir, "profiles", "whisper_exp");
        inDir  ??= Path.Combine(basePath, "slices");
        outDir ??= Path.Combine(basePath, "phoneme_db");

        if (!Directory.Exists(inDir))
            return Error($"Slices dir not found: {inDir}");

        var files = Directory.GetFiles(inDir, "*.mp3").OrderBy(f => f).ToArray();
        Console.Error.WriteLine($"[INDEX] Input : {inDir}");
        Console.Error.WriteLine($"[INDEX] Output: {outDir}");
        Console.Error.WriteLine($"[INDEX] Files : {files.Length}  move={move} dry-run={dryRun}");

        int ok = 0, skipped = 0, trashed = 0, errors = 0;

        foreach (var mp3 in files)
        {
            try
            {
                var name = Path.GetFileNameWithoutExtension(mp3);

                // Word = filename up to last _NNNN suffix (e.g., "안녕_0001" → "안녕")
                var word = ExtractWordFromSliceName(name);

                // FSA trie: extract per-syllable code sequence → each code = one folder level
                var (syllables, totalVoiced) = ExtractSyllableSequence(mp3);
                if (syllables.Count == 0)
                {
                    skipped++;
                    Console.Error.WriteLine($"[INDEX] SKIP  {name} (no voiced frame)");
                    continue;
                }

                // Too few voiced frames → incomplete/ambiguous → Recycle Bin
                if (totalVoiced < MinVoicedFrames)
                {
                    trashed++;
                    Console.Error.WriteLine($"[INDEX] TRASH {name} ({totalVoiced} frames < {MinVoicedFrames})");
                    if (!dryRun)
                        FileSystem.DeleteFile(mp3, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    continue;
                }

                // Build trie path: phoneme_db/<code0>/<code1>/<code2>/...
                var destDir = syllables.Aggregate(outDir,
                    (dir, sc) => Path.Combine(dir, FormatIndexCode(sc)));

                var pathStr = string.Join("/", syllables.Select(FormatIndexCode));

                // Avoid collisions
                var destFile = Path.Combine(destDir, $"{word}{Path.GetExtension(mp3)}");
                if (File.Exists(destFile))
                {
                    var match = System.Text.RegularExpressions.Regex.Match(name, @"_(\d+)$");
                    var seq   = match.Success ? match.Groups[1].Value : "dup";
                    destFile  = Path.Combine(destDir, $"{word}_{seq}{Path.GetExtension(mp3)}");
                }

                ok++;
                Console.Error.WriteLine($"[INDEX] {pathStr}/  {word}  ← {name}  ({syllables.Count} syllables)");

                if (!dryRun)
                {
                    Directory.CreateDirectory(destDir);
                    if (move)
                        File.Move(mp3, destFile, overwrite: true);
                    else
                        File.Copy(mp3, destFile, overwrite: true);
                }
            }
            catch (Exception ex)
            {
                errors++;
                Console.Error.WriteLine($"[INDEX] ERROR {Path.GetFileName(mp3)}: {ex.Message}");
            }
        }

        Console.WriteLine($"\n[INDEX] Done — ok={ok} trashed={trashed} skipped={skipped} errors={errors}");
        Console.Error.WriteLine($"[INDEX] DB   : {outDir}");
        return errors > 0 ? 1 : 0;
    }

    /// <summary>
    /// Decode MP3, run band FFT analysis, return (dominant soundCode, voiced frame count).
    /// code=0 if no voiced frame found.
    /// </summary>
    static (ushort code, int voicedCount) ExtractFirstSyllableCodeEx(string mp3Path)
    {
        const int HalfFft = IdxFftSize / 2;

        // Precompute Hann window
        var hann = new float[IdxFftSize];
        for (int i = 0; i < IdxFftSize; i++)
            hann[i] = 0.5f * (1f - MathF.Cos(2f * MathF.PI * i / IdxFftSize));

        // Decode MP3 → 16kHz mono float samples
        var pcm = new List<float>(IdxSampleRate * 3);
        using (var reader = new Mp3FileReader(mp3Path))
        {
            var fmt = new WaveFormat(IdxSampleRate, 16, 1);
            using var rs = new MediaFoundationResampler(reader, fmt) { ResamplerQuality = 6 };
            var buf = new byte[IdxFftSize * 2];
            int n;
            while ((n = rs.Read(buf, 0, buf.Length)) > 0)
                for (int i = 0; i < n - 1; i += 2)
                    pcm.Add((short)(buf[i] | (buf[i + 1] << 8)) / 32768f);
        }

        if (pcm.Count < IdxFftSize) return (0, 0);

        // Pass 1: collect raw band energies for noise floor estimation
        int frameCount = (pcm.Count - IdxFftSize) / (IdxFftSize / 2) + 1;
        var allRaw = new double[frameCount][];
        var fftBuf = new Complex[IdxFftSize];
        int fi = 0;

        for (int off = 0; off + IdxFftSize <= pcm.Count; off += IdxFftSize / 2, fi++)
        {
            for (int i = 0; i < IdxFftSize; i++)
            {
                float s = pcm[off + i];
                if (i > 0) s -= IdxPreEmph * pcm[off + i - 1];
                fftBuf[i] = new Complex(s * hann[i], 0);
            }
            WhisperEngine.Fft(fftBuf);

            var raw = new double[IdxBandCount];
            for (int b = 0; b < IdxBandCount; b++)
            {
                int loK = IndexBands[b].Lo * IdxFftSize / IdxSampleRate;
                int hiK = IndexBands[b].Hi * IdxFftSize / IdxSampleRate;
                for (int k = Math.Max(1, loK); k < Math.Min(HalfFft, hiK); k++)
                    raw[b] += fftBuf[k].Magnitude;
            }
            allRaw[fi] = raw;
        }
        frameCount = fi;

        // Noise floor = 20th-percentile of each band's raw energy (quiet frames)
        var noise = new double[IdxBandCount];
        for (int b = 0; b < IdxBandCount; b++)
        {
            var sorted = allRaw.Take(frameCount).Select(r => r[b]).OrderBy(v => v).ToArray();
            int idx20  = Math.Max(0, (int)(sorted.Length * 0.20) - 1);
            noise[b]   = sorted[idx20];
        }

        // Pass 2: find first voiced syllable, collect its sound codes
        var syllableCodes = new List<ushort>();
        bool inSyllable   = false;
        int  silenceCount = 0;
        var  ranked       = new int[IdxBandCount];

        for (int f = 0; f < frameCount; f++)
        {
            var raw   = allRaw[f];
            var clean = new double[IdxBandCount];
            for (int b = 0; b < IdxBandCount; b++)
                clean[b] = Math.Max(0, raw[b] - noise[b]);

            // Sort bands by clean energy descending (insertion sort — 8 elements)
            for (int b = 0; b < IdxBandCount; b++) ranked[b] = b;
            for (int i = 1; i < IdxBandCount; i++)
            {
                int k = ranked[i]; double kv = clean[k]; int j = i - 1;
                while (j >= 0 && clean[ranked[j]] < kv) { ranked[j + 1] = ranked[j]; j--; }
                ranked[j + 1] = k;
            }

            // Pack top-5 into 15-bit soundCode (Gray-encoded band indices)
            ushort sc = 0;
            for (int r = 0; r < 5; r++)
            {
                int idx  = ranked[r];
                int gray = idx ^ (idx >> 1);
                sc |= (ushort)(gray << (12 - r * 3));
            }

            // Gate: need meaningful signal — rank-1 band must be significantly above noise
            double maxClean = clean[ranked[0]];
            double maxNoise = noise[ranked[0]];
            if (maxClean < maxNoise * 1.5) sc = 0; // below 1.5× noise → silence

            if (sc != 0)
            {
                if (!inSyllable) inSyllable = true;
                silenceCount = 0;
                syllableCodes.Add(sc);
            }
            else if (inSyllable)
            {
                silenceCount++;
                if (silenceCount >= IdxSilenceGap)
                    break; // first syllable ended
            }
        }

        if (syllableCodes.Count == 0) return (0, 0);

        // Dominant code = most frequent in syllable window
        var dominant = syllableCodes
            .GroupBy(c => c)
            .OrderByDescending(g => g.Count())
            .First().Key;
        return (dominant, syllableCodes.Count);
    }

    /// <summary>
    /// Extract RLE-deduplicated token sequence from MP3 for FSA trie path.
    /// Returns (tokenList, totalVoicedFrames).
    /// Each entry = one distinct token (consecutive duplicates collapsed).
    /// </summary>
    static (List<ushort> syllables, int totalVoiced) ExtractSyllableSequence(string mp3Path, int maxSyllables = 8)
    {
        const int HalfFft = IdxFftSize / 2;

        var hann = new float[IdxFftSize];
        for (int i = 0; i < IdxFftSize; i++)
            hann[i] = 0.5f * (1f - MathF.Cos(2f * MathF.PI * i / IdxFftSize));

        var pcm = new List<float>(IdxSampleRate * 3);
        using (var reader = new Mp3FileReader(mp3Path))
        {
            var fmt = new WaveFormat(IdxSampleRate, 16, 1);
            using var rs = new MediaFoundationResampler(reader, fmt) { ResamplerQuality = 6 };
            var buf = new byte[IdxFftSize * 2];
            int n;
            while ((n = rs.Read(buf, 0, buf.Length)) > 0)
                for (int i = 0; i < n - 1; i += 2)
                    pcm.Add((short)(buf[i] | (buf[i + 1] << 8)) / 32768f);
        }
        if (pcm.Count < IdxFftSize) return ([], 0);

        // Pass 1: raw band energies for noise floor
        int frameCount = (pcm.Count - IdxFftSize) / (IdxFftSize / 2) + 1;
        var allRaw = new double[frameCount][];
        var fftBuf = new Complex[IdxFftSize];
        int fi = 0;
        for (int off = 0; off + IdxFftSize <= pcm.Count; off += IdxFftSize / 2, fi++)
        {
            for (int i = 0; i < IdxFftSize; i++)
            {
                float s = pcm[off + i];
                if (i > 0) s -= IdxPreEmph * pcm[off + i - 1];
                fftBuf[i] = new Complex(s * hann[i], 0);
            }
            WhisperEngine.Fft(fftBuf);
            var raw = new double[IdxBandCount];
            for (int b = 0; b < IdxBandCount; b++)
            {
                int loK = IndexBands[b].Lo * IdxFftSize / IdxSampleRate;
                int hiK = IndexBands[b].Hi * IdxFftSize / IdxSampleRate;
                for (int k = Math.Max(1, loK); k < Math.Min(HalfFft, hiK); k++)
                    raw[b] += fftBuf[k].Magnitude;
            }
            allRaw[fi] = raw;
        }
        frameCount = fi;

        var noise = new double[IdxBandCount];
        for (int b = 0; b < IdxBandCount; b++)
        {
            var sorted = allRaw.Take(frameCount).Select(r => r[b]).OrderBy(v => v).ToArray();
            int idx20  = Math.Max(0, (int)(sorted.Length * 0.20) - 1);
            noise[b]   = sorted[idx20];
        }

        // Pass 2: RLE-deduplicate tokens → FSA trie path (one folder per distinct token)
        var tokens      = new List<ushort>();
        int totalVoiced = 0;
        ushort lastSc   = 0;
        var    ranked   = new int[IdxBandCount];

        for (int f = 0; f < frameCount; f++)
        {
            var raw   = allRaw[f];
            var clean = new double[IdxBandCount];
            for (int b = 0; b < IdxBandCount; b++)
                clean[b] = Math.Max(0, raw[b] - noise[b]);

            for (int b = 0; b < IdxBandCount; b++) ranked[b] = b;
            for (int i = 1; i < IdxBandCount; i++)
            {
                int k = ranked[i]; double kv = clean[k]; int j = i - 1;
                while (j >= 0 && clean[ranked[j]] < kv) { ranked[j + 1] = ranked[j]; j--; }
                ranked[j + 1] = k;
            }
            ushort sc = 0;
            for (int r = 0; r < 5; r++)
            {
                int idx  = ranked[r];
                int gray = idx ^ (idx >> 1);
                sc |= (ushort)(gray << (12 - r * 3));
            }
            if (clean[ranked[0]] < noise[ranked[0]] * 1.5) sc = 0;

            if (sc != 0)
            {
                totalVoiced++;
                if (sc != lastSc) // RLE: skip consecutive duplicates
                {
                    if (tokens.Count < maxSyllables) tokens.Add(sc);
                    lastSc = sc;
                }
            }
            else
            {
                lastSc = 0; // silence resets RLE — next voiced token always added
            }
        }

        return (tokens, totalVoiced);
    }

    /// <summary>
    /// Extract word from slice filename. "안녕_0001" → "안녕", "hello_0042" → "hello".
    /// </summary>
    static string ExtractWordFromSliceName(string nameNoExt)
    {
        var m = System.Text.RegularExpressions.Regex.Match(nameNoExt, @"^(.+)_\d{3,}$");
        return m.Success ? m.Groups[1].Value : nameNoExt;
    }

    /// <summary>
    /// Format soundCode as octal folder name.
    /// Band 0 (VxLip) rank 1 → encode ranks 2,3,4 (>>3); else ranks 1,2,3 (>>6).
    /// </summary>
    static string FormatIndexCode(ushort sc)
    {
        if (sc == 0) return "0";
        bool vocalFirst = ((sc >> 12) & 7) == 0;
        int  val        = vocalFirst ? sc >> 3 : sc >> 6;
        return Convert.ToString(val, 8);
    }
}
