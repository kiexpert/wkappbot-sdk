using Microsoft.VisualBasic.FileIO;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int WhisperPhonemeLoopCommand(string[] args)
    {
        string? inDir = null;
        string? outDir = null;
        int intervalMs = 2000;
        bool move = false;
        bool dryRun = false;
        bool once = false;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--in" && i + 1 < args.Length) inDir = args[++i];
            else if (args[i] == "--out" && i + 1 < args.Length) outDir = args[++i];
            else if (args[i] == "--interval" && i + 1 < args.Length && int.TryParse(args[++i], out var ms)) intervalMs = Math.Max(250, ms);
            else if (args[i] == "--move") move = true;
            else if (args[i] == "--dry-run") dryRun = true;
            else if (args[i] == "--once") once = true;
        }

        var basePath = Path.Combine(DataDir, "profiles", "whisper_exp");
        inDir ??= Path.Combine(basePath, "slices");
        outDir ??= Path.Combine(basePath, "phoneme_db");

        if (!Directory.Exists(inDir))
            return Error($"Slices dir not found: {inDir}");

        Directory.CreateDirectory(outDir);

        Console.Error.WriteLine($"[PHONLOOP] in={inDir}");
        Console.Error.WriteLine($"[PHONLOOP] out={outDir}");
        Console.Error.WriteLine($"[PHONLOOP] interval={intervalMs}ms move={move} dry-run={dryRun} once={once}");

        var seen = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        int cycles = 0;
        int filesSeen = 0;
        int filesProcessed = 0;
        int chunksWritten = 0;
        int errors = 0;
        DateTime? pipeBrokenUntil = null;

        void Log(string text)
        {
            if (pipeBrokenUntil.HasValue && DateTime.UtcNow < pipeBrokenUntil.Value)
                return;

            try
            {
                Console.Error.WriteLine(text);
            }
            catch (IOException)
            {
                pipeBrokenUntil = DateTime.UtcNow.AddSeconds(3);
            }
        }

        while (!cts.Token.IsCancellationRequested)
        {
            cycles++;
            try
            {
                var files = Directory.GetFiles(inDir, "*.mp3", System.IO.SearchOption.TopDirectoryOnly)
                    .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                    .ToArray();

                int newFiles = 0;
                int newChunks = 0;

                foreach (var mp3 in files)
                {
                    long stamp = File.GetLastWriteTimeUtc(mp3).Ticks;
                    if (seen.TryGetValue(mp3, out var oldStamp) && oldStamp >= stamp)
                        continue;

                    seen[mp3] = stamp;
                    filesSeen++;
                    newFiles++;

                    var chunks = ExtractPhonemeChunks(mp3);
                    if (chunks.Count == 0)
                    {
                        Log($"[PHONLOOP] skip {Path.GetFileName(mp3)} (no voiced chunk)");
                        continue;
                    }

                    int wrote = 0;
                    foreach (var chunk in chunks)
                    {
                        var bucket = Path.Combine(outDir, FormatIndexCode(chunk.Code));
                        var stampText = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                        var baseName = Path.GetFileNameWithoutExtension(mp3);
                        var dest = Path.Combine(bucket, $"{FormatIndexCode(chunk.Code)}_loop_{stampText}_{baseName}_{chunk.Index:000}.mp3");

                        if (!dryRun)
                        {
                            Directory.CreateDirectory(bucket);
                            if (WritePcmChunkAsMp3(mp3, dest, chunk))
                                wrote++;
                        }
                        else
                        {
                            wrote++;
                        }
                    }

                    if (wrote > 0)
                    {
                        filesProcessed++;
                        chunksWritten += wrote;
                        newChunks += wrote;
                        Log($"[PHONLOOP] {Path.GetFileName(mp3)} -> {wrote} chunks");

                        if (move && !dryRun)
                        {
                            try
                            {
                                FileSystem.DeleteFile(mp3, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                            }
                            catch (Exception ex)
                            {
                                errors++;
                                Log($"[PHONLOOP] move error {Path.GetFileName(mp3)}: {ex.Message}");
                            }
                        }
                    }
                }

                Log($"[PHONLOOP] cycle={cycles} files={files.Length} new={newFiles} chunks={newChunks} total_files={filesProcessed} total_chunks={chunksWritten}");

                if (once)
                    break;

                Thread.Sleep(intervalMs);
            }
            catch (IOException)
            {
                pipeBrokenUntil = DateTime.UtcNow.AddSeconds(3);
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                errors++;
                Log($"[PHONLOOP] error: {ex.Message}");
                if (once)
                    break;
                Thread.Sleep(intervalMs);
            }
        }

        Log($"[PHONLOOP] done cycles={cycles} seen={filesSeen} processed={filesProcessed} chunks={chunksWritten} errors={errors}");
        return errors > 0 ? 1 : 0;
    }
}
