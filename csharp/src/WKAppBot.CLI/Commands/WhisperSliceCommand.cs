using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

// wkappbot whisper slice -- cut STT-tagged segments into per-word MP3 slices
// Reads stt_*.jsonl, extracts word timestamps, cuts with ffmpeg.
//
// Usage:
//   wkappbot whisper slice [--date YYYYMMDD] [--out <dir>] [--min-dur N] [--all]
//
// Output: slices/<word>_<seq>.mp3
//   --date YYYYMMDD  process only this date's stt file (default: today)
//   --all            process all stt_*.jsonl files
//   --out <dir>      output directory (default: whisper_exp/slices/)
//   --min-dur N      skip words shorter than N ms (default: 150)
//   --ffmpeg <path>  ffmpeg executable path (default: ffmpeg)

internal partial class Program
{
    static int WhisperSliceCommand(string[] args)
    {
        string? dateFilter = null;
        string? outDir    = null;
        string  ffmpeg    = "ffmpeg";
        int     minDurMs  = 150;
        bool    allDates  = false;

        for (int i = 0; i < args.Length; i++)
        {
            if      (args[i] == "--date"   && i + 1 < args.Length) dateFilter = args[++i];
            else if (args[i] == "--out"    && i + 1 < args.Length) outDir     = args[++i];
            else if (args[i] == "--ffmpeg" && i + 1 < args.Length) ffmpeg     = args[++i];
            else if (args[i] == "--min-dur"&& i + 1 < args.Length) int.TryParse(args[++i], out minDurMs);
            else if (args[i] == "--all")   allDates = true;
        }

        var basePath = Path.Combine(DataDir, "profiles", "whisper_exp");
        outDir ??= Path.Combine(basePath, "slices");
        Directory.CreateDirectory(outDir);

        // Collect study JSONL files to process (study_*.jsonl = Gemini-tagged STT data)
        var jsonlFiles = new List<string>();
        if (allDates)
        {
            jsonlFiles.AddRange(Directory.GetFiles(basePath, "study_*.jsonl").OrderBy(f => f));
        }
        else
        {
            var date = dateFilter ?? DateTime.Now.ToString("yyyyMMdd");
            var f = Path.Combine(basePath, $"study_{date}.jsonl");
            if (!File.Exists(f))
                return Error($"Study file not found: {f}\nTip: use --all to process all dates, or --date YYYYMMDD");
            jsonlFiles.Add(f);
        }

        Console.Error.WriteLine($"[SLICE] Output: {outDir}");
        Console.Error.WriteLine($"[SLICE] Processing {jsonlFiles.Count} JSONL file(s), min-dur={minDurMs}ms");

        int totalWords = 0, sliced = 0, skipped = 0, errors = 0;
        int seq = 1;

        // Load existing slice count to avoid overwriting
        seq = Directory.GetFiles(outDir, "*.mp3").Length + 1;

        foreach (var jsonlPath in jsonlFiles)
        {
            Console.Error.WriteLine($"[SLICE] {Path.GetFileName(jsonlPath)}");
            var lines = File.ReadAllLines(jsonlPath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                JsonNode? entry;
                try { entry = JsonNode.Parse(line); } catch { continue; }

                var sourceFile = entry?["sourceFile"]?.GetValue<string>();
                if (string.IsNullOrEmpty(sourceFile) || !File.Exists(sourceFile))
                {
                    skipped++;
                    continue;
                }

                var words = entry?["words"]?.AsArray();
                if (words == null || words.Count == 0) continue;

                foreach (var w in words)
                {
                    totalWords++;
                    var word   = w?["word"]?.GetValue<string>() ?? "";
                    var startMs = w?["startMs"]?.GetValue<int>() ?? 0;
                    var durMs   = w?["durMs"]?.GetValue<int>() ?? 0;

                    if (string.IsNullOrWhiteSpace(word) || durMs < minDurMs)
                    {
                        skipped++;
                        continue;
                    }

                    // Sanitize word for filename
                    var safeName = SanitizeSliceName(word);
                    if (string.IsNullOrEmpty(safeName)) { skipped++; continue; }

                    var outFile = Path.Combine(outDir, $"{safeName}_{seq:D4}.mp3");
                    seq++;

                    // ffmpeg: extract word slice
                    // -ss before -i = fast seek (keyframe), accurate enough for word-level
                    double startSec = startMs / 1000.0;
                    double durSec   = durMs   / 1000.0;

                    var result = RunFfmpeg(ffmpeg,
                        $"-y -ss {startSec:F3} -i \"{sourceFile}\" -t {durSec:F3} -c:a libmp3lame -q:a 4 \"{outFile}\"");

                    if (result == 0)
                    {
                        sliced++;
                        Console.Error.WriteLine($"[SLICE] [{seq - 1:D4}] {word} ({durMs}ms) -> {Path.GetFileName(outFile)}");
                    }
                    else
                    {
                        errors++;
                        Console.Error.WriteLine($"[SLICE] FAIL [{seq - 1:D4}] {word} -- ffmpeg exit={result}");
                    }
                }
            }
        }

        Console.WriteLine($"\n[SLICE] Done -- sliced={sliced} skipped={skipped} errors={errors} total_words={totalWords}");
        Console.Error.WriteLine($"[SLICE] Output: {outDir}");
        return errors > 0 ? 1 : 0;
    }

    static int RunFfmpeg(string ffmpegExe, string arguments)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName  = ffmpegExe,
                Arguments = arguments,
                UseShellExecute        = false,
                CreateNoWindow         = true,
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
            };
            using var proc = AppBotPipe.StartTracked(psi, psi.WorkingDirectory.Length > 0 ? psi.WorkingDirectory : Environment.CurrentDirectory, "WHISPER-FFMPEG");
            if (proc == null) return -1;
            proc.StandardOutput.ReadToEnd(); // drain
            proc.StandardError.ReadToEnd();  // drain
            proc.WaitForExit(30_000);
            return proc.ExitCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[SLICE] ffmpeg error: {ex.Message}");
            return -1;
        }
    }

    static string SanitizeSliceName(string word)
    {
        var invalid = Path.GetInvalidFileNameChars();
        var sb = new System.Text.StringBuilder();
        foreach (var c in word)
            if (!invalid.Contains(c) && c != ' ') sb.Append(c);
        return sb.ToString().Trim();
    }
}
