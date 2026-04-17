using System.Diagnostics;
using System.Speech.Recognition;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using NAudio.Wave;
using NAudio.Lame;

namespace WKAppBot.CLI;

/// <summary>
/// whisper study -- batch MP3 audio segments -> Gemini transcription with word-level timing.
///
/// Architecture:
///   1. Scan whisper_exp/wav/ for MP3 files (prioritize _unknown/)
///   2. Concat N files into a multi-minute batch file (NAudio)
///   3. Send to Gemini with karaoke prompt -> word-level JSON with timing + speaker
///   4. Parse response -> move files to correct label folders
///   5. Save analysis to study_{date}.jsonl
///
/// Usage:
///   wkappbot whisper study [--batch N] [--engine gemini] [--dry-run]
///   wkappbot whisper stats
/// </summary>
internal partial class Program
{
    static int WhisperStudyCommand(string[] args)
    {
        if (args.Length > 0 && args[0] == "stats")
            return WhisperStatsCommand();
        if (args.Length > 0 && args[0] == "prune-simple")
            return WhisperPruneSimpleCommand(args[1..]);

        // Parse options
        int      batchSize = 10;
        TimeSpan timeout   = TimeSpan.Zero; // zero = single-shot; use --for for loop duration
        bool     dryRun    = false;
        string   engine    = "gemini";
        for (int i = 0; i < args.Length; i++)
        {
            if      (args[i] == "--batch"  && i + 1 < args.Length) int.TryParse(args[++i], out batchSize);
            else if (args[i] == "--for"    && i + 1 < args.Length) timeout = ParseDuration(args[++i]);
            else if (args[i] == "--dry-run")  dryRun  = true;
            else if (args[i] == "--engine" && i + 1 < args.Length) engine = args[++i].ToLowerInvariant();
        }
        batchSize = Math.Clamp(batchSize, 1, 50);

        var basePath = Path.Combine(DataDir, "profiles", "whisper_exp");
        var wavDir   = Path.Combine(basePath, "wav");
        if (!Directory.Exists(wavDir))
        {
            Console.WriteLine("[STUDY] No wav/ directory found. Run whisper first to record audio segments.");
            return 1;
        }

        // -- Ctrl+C -> smooth shutdown after current batch --
        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true; // don't kill process immediately
            cts.Cancel();
            Console.WriteLine("\n[STUDY] Ctrl+C -- finishing current batch then exiting...");
        };

        var deadline   = timeout > TimeSpan.Zero ? DateTime.UtcNow.Add(timeout) : DateTime.MaxValue;
        int totalBatch = 0, totalMoved = 0;
        if (timeout > TimeSpan.Zero)
            Console.Error.WriteLine($"[STUDY] Loop mode -- timeout={timeout}, batch={batchSize}, engine={engine}");

        while (!cts.IsCancellationRequested)
        {
            // -- Timeout check --
            var remaining = deadline - DateTime.UtcNow;
            if (remaining <= TimeSpan.Zero)
            {
                Console.WriteLine("[STUDY] Timeout reached.");
                break;
            }
            if (timeout > TimeSpan.Zero)
                Console.Error.WriteLine($"[STUDY] Remaining: {(int)remaining.TotalMinutes}m{remaining.Seconds:D2}s");

            // -- Collect files --
            var mp3Files = CollectMp3Files(wavDir, batchSize);
            if (mp3Files.Count == 0)
            {
                if (timeout > TimeSpan.Zero && !cts.IsCancellationRequested)
                {
                    Console.WriteLine("[STUDY] No files -- waiting 30s for new recordings...");
                    for (int w = 0; w < 30 && !cts.IsCancellationRequested && DateTime.UtcNow < deadline; w++)
                        Thread.Sleep(1000);
                    continue;
                }
                Console.WriteLine("[STUDY] No MP3 files to study.");
                break;
            }

            Console.Error.WriteLine($"[STUDY] Batch #{totalBatch + 1}: {mp3Files.Count} files");
            foreach (var f in mp3Files)
                Console.WriteLine($"  {Path.GetRelativePath(wavDir, f)}");

            // -- STT re-labeling (offline) --
            Console.Error.WriteLine($"[STUDY] STT re-labeling...");
            var sttLabels = SttRelabelFiles(mp3Files);
            int sttOk = sttLabels.Values.Count(v => !string.IsNullOrEmpty(v.Text));
            Console.Error.WriteLine($"[STUDY] STT recognized {sttOk}/{mp3Files.Count}");

            // -- Concat -> batch MP3 (size-capped: select files by cumulative size) --
            const long MaxBatchBytes  = 2 * 1024 * 1024; // 2MB total -- safe for Gemini
            const long MaxSingleBytes = 1 * 1024 * 1024; // 1MB per file -- skip oversized singles
            {
                long cumSize = 0;
                var capped = new List<string>();
                int skipped = 0;
                foreach (var f in mp3Files)
                {
                    var sz = new FileInfo(f).Length;
                    if (sz > MaxSingleBytes) { skipped++; continue; } // single file too large -> skip
                    if (capped.Count > 0 && cumSize + sz > MaxBatchBytes) break;
                    capped.Add(f);
                    cumSize += sz;
                }
                if (skipped > 0 || capped.Count < mp3Files.Count)
                    Console.Error.WriteLine($"[STUDY] Size cap: {mp3Files.Count} -> {capped.Count} files ({cumSize / 1024}KB, skipped_large={skipped})");
                if (capped.Count == 0)
                {
                    Console.WriteLine("[STUDY] All files oversized -- skipping batch.");
                    if (timeout == TimeSpan.Zero) break;
                    continue;
                }
                mp3Files = capped;
            }
            var batchFile  = Path.Combine(basePath, $"study_batch_{DateTime.Now:yyyyMMdd_HHmmss}.mp3");
            var segmentMap = ConcatMp3Files(mp3Files, batchFile);
            if (segmentMap == null || segmentMap.Count == 0)
            {
                Console.WriteLine("[STUDY] Failed to concat MP3 files -- skipping batch.");
                break;
            }
            Console.Error.WriteLine($"[STUDY] Batch file: {new FileInfo(batchFile).Length / 1024}KB, {segmentMap.Count} segments");

            // -- Prompt --
            var prompt = BuildKaraokePrompt(segmentMap);
            Console.Error.WriteLine($"[STUDY] Prompt: {prompt.Length} chars");

            if (dryRun)
            {
                Console.WriteLine("[STUDY] Dry run -- prompt:");
                Console.WriteLine(prompt);
                try { File.Delete(batchFile); } catch { }
                break; // dry-run always single-shot
            }

            // -- AI query -- multi-tier fallback cascade --
            // Tier 1: normal ask -> Tier 2: fresh session -> Tier 3: copyright dismiss + retry
            // -> Tier 4: close tab + new tab -> give up + Slack notify
            bool success = false;
            string? response = null;
            string? failReason = null;

            var tiers = new (string name, bool fresh, bool closeTab)[]
            {
                ("normal", false, false),
                ("fresh-session", true, false),
                ("new-tab", true, true),
            };

            foreach (var (tierName, fresh, closeTab) in tiers)
            {
                Console.Error.WriteLine($"[STUDY] Tier [{tierName}] -> {engine}...");

                if (closeTab)
                {
                    // Close existing Gemini tab to force completely fresh state
                    try
                    {
                        Console.Error.WriteLine($"[STUDY] Closing existing {engine} tab...");
                        A11yCommand(["close", $"*Chrome*#{engine}*", "--force"]);
                        Thread.Sleep(2000);
                    }
                    catch { }
                }

                (success, response) = AskAiForStudy(batchFile, prompt, engine, freshSession: fresh);
                if (success && !string.IsNullOrWhiteSpace(response))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Error.WriteLine($"[STUDY] Tier [{tierName}] SUCCESS -- {response.Length} chars");
                    Console.ResetColor();
                    break;
                }

                failReason = tierName;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Error.WriteLine($"[STUDY] Tier [{tierName}] FAILED -- trying next tier...");
                Console.ResetColor();
                Thread.Sleep(1000);
            }

            if (!success || string.IsNullOrWhiteSpace(response))
            {
                Console.Error.WriteLine($"[STUDY] All tiers exhausted -- keeping batch file for retry.");
                // Notify via Slack with failure details
                try
                {
                    var slackCfg = File.Exists(SlackConfigPath) ? JsonNode.Parse(File.ReadAllText(SlackConfigPath)) : null;
                    var slackToken = slackCfg?["bot_token"]?.GetValue<string>();
                    var slackCh = slackCfg?["channel"]?.GetValue<string>();
                    if (!string.IsNullOrEmpty(slackToken) && !string.IsNullOrEmpty(slackCh))
                    {
                        var debugDir = Path.Combine(DataDir, "logs");
                        var latestDebug = Directory.Exists(debugDir)
                            ? Directory.GetFiles(debugDir, $"study_fail_{engine}_*.log")
                                .OrderByDescending(f => f).FirstOrDefault() : null;
                        var detail = latestDebug != null ? $"\nDebug: `{Path.GetFileName(latestDebug)}`" : "";
                        SlackSendViaApi(slackToken, slackCh,
                            $":x: Whisper study `{engine}` ALL TIERS FAILED: `{Path.GetFileName(batchFile)}`\nTried: normal -> fresh-session -> new-tab{detail}",
                            username: "앱봇위스퍼").GetAwaiter().GetResult();
                    }
                }
                catch { }
                if (timeout == TimeSpan.Zero) break; else continue;
            }
            Console.Error.WriteLine($"[STUDY] Response: {response.Length} chars");

            // -- Parse (retry with fresh tab if response looks like UI artifact) --
            var studyResults = ParseStudyResponse(response, segmentMap);
            if (studyResults == null || studyResults.Count == 0)
            {
                // "AnalysisAnalysis..." = stale Gemini tab state -> retry with fresh session
                bool looksLikeUiArtifact = !response.Contains('[') || response.StartsWith("Analysis");
                if (looksLikeUiArtifact && engine == "gemini")
                {
                    Console.WriteLine("[STUDY] UI artifact detected -- retrying with fresh Gemini tab...");
                    (success, response) = AskAiForStudy(batchFile, prompt, engine, freshSession: true);
                    if (success && !string.IsNullOrWhiteSpace(response))
                        studyResults = ParseStudyResponse(response, segmentMap);
                }
            }
            if (studyResults == null || studyResults.Count == 0)
            {
                Console.WriteLine("[STUDY] Failed to parse response. Raw:");
                Console.WriteLine(response?.Length > 2000 ? response[..2000] + "..." : response);
                SaveRawResponse(basePath, response ?? "", segmentMap);
                if (timeout == TimeSpan.Zero) break;
                continue; // loop mode: skip bad batch and try next
            }
            Console.Error.WriteLine($"[STUDY] Parsed {studyResults.Count} segments");

            // -- Move to label folders --
            int moved = 0, quarantined = 0;
            var quarantineDir = Path.Combine(wavDir, "_quarantine");
            foreach (var result in studyResults)
            {
                if (result.SourceFile == null || !File.Exists(result.SourceFile)) continue;

                var transcript = result.Transcript;
                sttLabels.TryGetValue(result.SourceFile, out var sttFb);
                if (string.IsNullOrWhiteSpace(transcript))
                    transcript = sttFb?.Text;

                // -- Cross-model agreement gate --------------------------------
                // If Gemini confidence low OR Gemini/STT disagree significantly -> quarantine
                string? quarantineReason = null;
                if (result.AudioType == "speech" || result.AudioType == "mixed")
                {
                    if (result.Confidence < 0.35 && !string.IsNullOrWhiteSpace(result.Transcript))
                        quarantineReason = $"low_conf={result.Confidence:F2}";
                    else if (!string.IsNullOrWhiteSpace(result.Transcript)
                          && !string.IsNullOrWhiteSpace(sttFb?.Text)
                          && sttFb.Confidence >= 0.3f)
                    {
                        var agree = WordJaccard(result.Transcript, sttFb.Text);
                        if (agree < 0.15)
                            quarantineReason = $"disagree={agree:F2}(gemini=\"{result.Transcript[..Math.Min(20,result.Transcript.Length)]}\" stt=\"{sttFb.Text[..Math.Min(20,sttFb.Text.Length)]}\")";
                    }
                }

                if (quarantineReason != null)
                {
                    Directory.CreateDirectory(quarantineDir);
                    var qDest = Path.Combine(quarantineDir, Path.GetFileName(result.SourceFile));
                    if (!File.Exists(qDest))
                    {
                        try { File.Move(result.SourceFile, qDest); quarantined++; } catch { continue; }
                        Console.WriteLine($"  [Q] _quarantine  {Path.GetFileName(result.SourceFile)}  ({quarantineReason})");
                        DropCompanionJson(qDest, result, sttLabels);
                    }
                    continue;
                }

                string label;
                if (result.AudioType != "speech" && result.AudioType != "mixed")
                    label = $"_{result.AudioType}";
                else
                {
                    label = SanitizeFolderName(transcript ?? "_unknown");
                    if (string.IsNullOrWhiteSpace(label)) label = "_unknown";
                }

                var currentFolder = Path.GetFileName(Path.GetDirectoryName(result.SourceFile)) ?? "";
                string destPath;
                // -- Sound code display --
                string scTag = "";
                try
                {
                    var (sc, _) = ExtractFirstSyllableCodeEx(result.SourceFile);
                    if (sc != 0) scTag = $" [{FormatIndexCode(sc)}]";
                }
                catch { }

                if (currentFolder == label)
                {
                    destPath = result.SourceFile;
                    Console.WriteLine($"  [=] {label}{scTag}  {Path.GetFileName(result.SourceFile)}");
                }
                else
                {
                    var targetDir = Path.Combine(wavDir, label);
                    Directory.CreateDirectory(targetDir);
                    destPath = Path.Combine(targetDir, Path.GetFileName(result.SourceFile));
                    if (File.Exists(destPath)) continue;
                    try
                    {
                        File.Move(result.SourceFile, destPath);
                        moved++;
                        Console.WriteLine($"  [->] [{label}]{scTag}  {Path.GetFileName(result.SourceFile)}");
                    }
                    catch (Exception ex) { Console.WriteLine($"  [!] Move failed: {ex.Message}"); continue; }
                }
                DropCompanionJson(destPath, result, sttLabels);
            }
            Console.Error.WriteLine($"[STUDY] Moved {moved}/{studyResults.Count}  quarantined={quarantined}");
            totalMoved += moved;

            // -- Phoneme map + self-heal --
            int mapped = BuildPhonemeMap(basePath, studyResults);
            if (mapped > 0) Console.Error.WriteLine($"[STUDY] Phoneme map: {mapped} new entries");
            SaveStudyResults(basePath, studyResults);
            SelfHeal(basePath, wavDir);

            // -- Auto-slice -> phoneme_db --
            var todayStr = DateTime.Now.ToString("yyyyMMdd");
            int sliced2 = WhisperSliceCommand(["--date", todayStr]);
            if (sliced2 == 0)
            {
                WhisperIndexCommand(["--move"]);
                // Delete source MP3s from labeled folders (sliced -> phoneme_db, no longer needed)
                // Keep: _unknown (pending), _requeue (needs retry), _mic (raw recordings)
                // Delete: _noise, _music, _instrument, and any word-label folders (no _ prefix)
                var keepFolders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                    { "_unknown", "_requeue", "_mic" };
                foreach (var labelDir in Directory.GetDirectories(wavDir)
                             .Where(d => !keepFolders.Contains(Path.GetFileName(d))))
                {
                    foreach (var f in Directory.GetFiles(labelDir))
                        try { File.Delete(f); } catch { }
                }
                Console.WriteLine("[STUDY] Auto-slice+index done -> phoneme_db updated");
            }

            // -- Cleanup --
            try { File.Delete(batchFile); } catch { }
            foreach (var f in mp3Files)
                if (!File.Exists(f)) continue; else try { File.Delete(f); } catch { }

            var micDir2 = Path.Combine(wavDir, "_mic");
            if (Directory.Exists(micDir2))
            {
                int purged = 0;
                foreach (var f in Directory.GetFiles(micDir2, "*.mp3"))
                    if (new FileInfo(f).Length == 0) { try { File.Delete(f); purged++; } catch { } }
                if (purged > 0) Console.Error.WriteLine($"[STUDY] Purged {purged} empty mic files");
            }

            // -- Delete empty subdirectories --
            int emptyDirs = 0;
            foreach (var dir in Directory.GetDirectories(wavDir, "*", SearchOption.AllDirectories)
                         .OrderByDescending(d => d.Length)) // deepest first
            {
                try
                {
                    if (!Directory.EnumerateFileSystemEntries(dir).Any())
                    {
                        Directory.Delete(dir);
                        emptyDirs++;
                    }
                }
                catch { }
            }
            if (emptyDirs > 0) Console.Error.WriteLine($"[STUDY] Removed {emptyDirs} empty folder(s)");

            totalBatch++;
            if (timeout == TimeSpan.Zero) break; // single-shot mode -- done after one batch
        }

        Console.Error.WriteLine($"[STUDY] Done -- {totalBatch} batch(es), {totalMoved} files moved total");
        return 0;
    }

    /// <summary>Collect MP3 files, prioritizing: no companion JSON > _unknown/ > other folders.</summary>
    static List<string> CollectMp3Files(string wavDir, int maxCount)
    {
        // Gather all MP3s from all directories
        var allMp3s = new List<string>();

        // _unknown/ folder
        var unknownDir = Path.Combine(wavDir, "_unknown");
        if (Directory.Exists(unknownDir))
            allMp3s.AddRange(Directory.GetFiles(unknownDir, "*.mp3"));

        // Other subfolders (skip system folders: _mic, _quarantine, _noise already excluded)
        foreach (var subDir in Directory.GetDirectories(wavDir).OrderBy(d => d))
        {
            var name = Path.GetFileName(subDir);
            if (name == "_unknown" || name == "_mic" || name == "_quarantine" || name == "_noise") continue;
            allMp3s.AddRange(Directory.GetFiles(subDir, "*.mp3"));
        }

        // Root wav/ folder
        allMp3s.AddRange(Directory.GetFiles(wavDir, "*.mp3")
            .Where(f => !Path.GetFileName(f).StartsWith("study_batch_")));

        // Shuffle for diversity: avoid sending same files every batch.
        // Prioritize: no companion JSON (never analyzed), then shuffle within each group.
        var rng = new Random();
        var valid = allMp3s.Where(f => new FileInfo(f).Length > 0).ToList();
        var neverAnalyzed = valid.Where(f => !File.Exists(Path.ChangeExtension(f, ".json"))).ToList();
        var analyzed = valid.Where(f => File.Exists(Path.ChangeExtension(f, ".json"))).ToList();

        // Shuffle both groups
        for (int i = neverAnalyzed.Count - 1; i > 0; i--) { int j = rng.Next(i + 1); (neverAnalyzed[i], neverAnalyzed[j]) = (neverAnalyzed[j], neverAnalyzed[i]); }
        for (int i = analyzed.Count - 1; i > 0; i--) { int j = rng.Next(i + 1); (analyzed[i], analyzed[j]) = (analyzed[j], analyzed[i]); }

        // Never-analyzed first, then analyzed (re-study candidates), take maxCount
        return neverAnalyzed.Concat(analyzed).Take(maxCount).ToList();
    }

    /// <summary>
    /// Concat MP3 files into single file with 1-second silence between segments.
    /// Returns list of (startMs, durationMs, sourceFile) for each segment.
    /// </summary>
    static List<StudySegmentMap>? ConcatMp3Files(List<string> files, string outputPath)
    {
        var segments = new List<StudySegmentMap>();
        try
        {
            var stereoFormat = new WaveFormat(16000, 16, 2);
            using var mp3Writer = new LameMP3FileWriter(outputPath, stereoFormat, 64);

            int currentMs = 0;
            var silenceBytes = new byte[32000]; // 0.5s at 16kHz 16bit stereo

            for (int i = 0; i < files.Count; i++)
            {
                // Add silence separator between segments
                if (i > 0)
                {
                    mp3Writer.Write(silenceBytes, 0, silenceBytes.Length);
                    mp3Writer.Write(silenceBytes, 0, silenceBytes.Length); // 1.0s total silence
                    currentMs += 1000;
                }

                var startMs = currentMs;
                try
                {
                    using var reader = new Mp3FileReader(files[i]);
                    // Resample to match output format
                    using var resampler = new MediaFoundationResampler(reader, stereoFormat)
                    {
                        ResamplerQuality = 30,
                    };

                    var buf = new byte[8192];
                    int totalBytes = 0;
                    int read;
                    while ((read = resampler.Read(buf, 0, buf.Length)) > 0)
                    {
                        mp3Writer.Write(buf, 0, read);
                        totalBytes += read;
                    }

                    // Calculate duration: bytes / (sampleRate * channels * bytesPerSample)
                    var durationMs = (int)(totalBytes / (16000.0 * 2 * 2) * 1000);
                    currentMs += durationMs;

                    segments.Add(new StudySegmentMap
                    {
                        Index = i + 1,
                        StartMs = startMs,
                        DurationMs = durationMs,
                        SourceFile = files[i],
                    });
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[STUDY] Skip {Path.GetFileName(files[i])}: {ex.Message}");
                }
            }

            return segments;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[STUDY] Concat failed: {ex.Message}");
            return null;
        }
    }

    /// <summary>Build the English karaoke prompt for Gemini.</summary>
    static string BuildKaraokePrompt(List<StudySegmentMap> segments)
    {
        var sb = new StringBuilder();
        sb.AppendLine("You are a professional audio transcription and analysis tool.");
        sb.AppendLine("I'm building a karaoke system for language learning.");
        sb.AppendLine();
        sb.AppendLine("This audio file contains multiple short speech segments separated by ~1 second silence gaps.");
        sb.AppendLine("Segments are PRIMARILY Korean, but may contain English, Japanese, or other languages mixed in.");
        sb.AppendLine("Some segments may be NON-SPEECH. Classify each segment's audio_type:");
        sb.AppendLine("  \"speech\" = human voice, \"music\" = song/BGM, \"instrument\" = piano/guitar/etc,");
        sb.AppendLine("  \"noise\" = keyboard/click/fan/ambient, \"mixed\" = speech+music overlap.");
        sb.AppendLine("For non-speech: set confidence=0, transcript=\"(noise)\"/\"(music)\"/\"(instrument)\".");
        sb.AppendLine();
        sb.AppendLine("Segment timing map (approximate):");
        foreach (var seg in segments)
        {
            var endMs = seg.StartMs + seg.DurationMs;
            sb.AppendLine($"  Segment {seg.Index}: {seg.StartMs}ms ~ {endMs}ms (file: {Path.GetFileName(seg.SourceFile)})");
        }
        sb.AppendLine();
        sb.AppendLine("For EACH segment, provide a JSON object with:");
        sb.AppendLine("1. Full transcript of the speech");
        sb.AppendLine("2. Word-level timing (start_ms relative to segment start, duration_ms)");
        sb.AppendLine("3. Speaker identification (speaker_1, speaker_2, etc. -- different voices get different IDs)");
        sb.AppendLine("4. L/R stereo phase analysis if noticeable (phase_lr_deg: estimated degrees, 0=center)");
        sb.AppendLine("5. Confidence score (0.0~1.0) -- how confident you are in the transcript accuracy");
        sb.AppendLine("6. Detected language code (e.g. \"en\", \"ko\", \"ja\", \"zh\")");
        sb.AppendLine("7. silence_before_ms -- milliseconds of silence/noise BEFORE the first word starts speaking");
        sb.AppendLine("8. audio_type -- one of: speech, music, instrument, noise, mixed");
        sb.AppendLine();
        sb.AppendLine("Respond with ONLY a JSON array, no markdown fences, no explanation:");
        sb.AppendLine("[");
        sb.AppendLine("  {");
        sb.AppendLine("    \"segment\": 1,");
        sb.AppendLine("    \"transcript\": \"Hello, how are you today?\",");
        sb.AppendLine("    \"speaker\": \"speaker_1\",");
        sb.AppendLine("    \"confidence\": 0.95,");
        sb.AppendLine("    \"language\": \"en\",");
        sb.AppendLine("    \"silence_before_ms\": 120,");
        sb.AppendLine("    \"audio_type\": \"speech\",");
        sb.AppendLine("    \"words\": [");
        sb.AppendLine("      {\"word\": \"Hello\", \"start_ms\": 120, \"dur_ms\": 350, \"phase_lr_deg\": 0.0},");
        sb.AppendLine("      {\"word\": \"how\", \"start_ms\": 520, \"dur_ms\": 200, \"phase_lr_deg\": 0.0}");
        sb.AppendLine("    ]");
        sb.AppendLine("  }");
        sb.AppendLine("]");
        return sb.ToString();
    }

    /// <summary>
    /// Send batch audio to AI (Gemini or GPT) via `ask` command and capture response.
    /// Reuses existing AskCommand infrastructure (persona, tab routing, file attachment, streaming).
    /// </summary>
    static (bool success, string? response) AskAiForStudy(string audioFile, string prompt, string engine = "gemini", bool freshSession = false)
    {
        // Write prompt to temp file (too long for command line arg)
        var promptFile = Path.Combine(Path.GetTempPath(), $"whisper_study_prompt_{DateTime.Now:HHmmss}.txt");
        File.WriteAllText(promptFile, prompt);

        try
        {
            // Capture console output from AskCommand
            var originalOut = Console.Out;
            var capture = new StringWriter();
            var tee = new StudyCaptureWriter(originalOut, capture); // write to both console and capture
            Console.SetOut(tee);

            try
            {
                // Call AskCommand directly: ask <engine> "prompt text" audioFile.mp3 --timeout 120
                // File args are auto-detected by AskCommand (existing file -> attachment)
                var askArgs = new List<string> { engine };

                // Add audio file first (auto-detected as attachment by AskCommand)
                if (File.Exists(audioFile))
                    askArgs.Add(audioFile);

                // Add prompt text lines (each line = separate arg for AskCommand)
                foreach (var line in prompt.Split('\n'))
                {
                    var trimmed = line.TrimEnd('\r');
                    if (trimmed.Length > 0)
                        askArgs.Add(trimmed);
                }

                askArgs.Add("--timeout");
                askArgs.Add("120");
                askArgs.Add("--target-tag");
                askArgs.Add($"{engine}-whisper"); // dedicated tab: never shares with triad/normal ask tabs
                if (freshSession)
                    askArgs.Add("--new-session"); // clear stale tab context on retry

                Console.Error.WriteLine($"[STUDY] Calling: ask {engine} <{askArgs.Count - 2} lines> {Path.GetFileName(audioFile)} --timeout 120 --target-tag {engine}-whisper{(freshSession ? " --new-session" : "")}");
                _suppressSlackSession.Value = true; // internal sub-call -- no Slack channel noise
                AskCommand(askArgs.ToArray());
            }
            finally
            {
                Console.SetOut(originalOut);
            }

            // Extract AI answer from captured output
            var output = capture.ToString();
            var response = ExtractGeminiAnswer(output);

            // Debug logging: why did it fail?
            if (response == null)
            {
                var outputLen = output.Length;
                var hasBegin = output.Contains("[ASK_FULL_ANSWER_BEGIN]");
                var hasEnd = output.Contains("[ASK_FULL_ANSWER_END]");
                var hasError = output.Contains("[ASK] Error:");
                var hasCopyright = output.Contains("저작권") || output.Contains("copyright") || output.Contains("대답이 중지");
                var hasTimeout = output.Contains("timed out") || output.Contains("Timeout");
                var hasBlank = output.Contains("BLANK") || output.Contains("blank page");
                var lastLines = string.Join("\n", output.Split('\n').TakeLast(5).Select(l => l.Trim()));

                Console.Error.WriteLine($"[STUDY-DEBUG] {engine} extraction FAILED:");
                Console.Error.WriteLine($"  output={outputLen}ch begin={hasBegin} end={hasEnd}");
                Console.Error.WriteLine($"  error={hasError} copyright={hasCopyright} timeout={hasTimeout} blank={hasBlank}");
                Console.Error.WriteLine($"  last5lines: {lastLines[..Math.Min(lastLines.Length, 300)]}");

                // Save full output for post-mortem
                try
                {
                    var debugPath = Path.Combine(DataDir, "logs",
                        $"study_fail_{engine}_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                    File.WriteAllText(debugPath, output);
                    Console.Error.WriteLine($"  saved: {debugPath}");
                }
                catch { }
            }

            return (response != null, response);
        }
        finally
        {
            try { File.Delete(promptFile); } catch { }
        }
    }

    /// <summary>Extract Gemini answer text from captured ask command output.</summary>
    static string? ExtractGeminiAnswer(string output)
    {
        // Prefer full answer markers (not truncated)
        const string beginMarker = "[ASK_FULL_ANSWER_BEGIN]";
        const string endMarker = "[ASK_FULL_ANSWER_END]";
        var beginIdx = output.IndexOf(beginMarker);
        var endIdx = output.IndexOf(endMarker);
        if (beginIdx >= 0 && endIdx > beginIdx)
        {
            var answer = output[(beginIdx + beginMarker.Length)..endIdx].Trim();
            if (answer.Length > 0) return answer;
        }

        // Fallback: truncated display output
        const string marker = "-- Gemini 답변 --";
        var idx = output.IndexOf(marker);
        if (idx < 0) return null;

        var answerStart = idx + marker.Length;
        while (answerStart < output.Length && (output[answerStart] == '\r' || output[answerStart] == '\n'))
            answerStart++;

        if (answerStart >= output.Length) return null;
        var fallback = output[answerStart..].Trim();
        if (fallback.EndsWith("... (truncated)"))
            fallback = fallback[..^"... (truncated)".Length].Trim();

        return fallback.Length > 0 ? fallback : null;
    }

    /// <summary>
    /// Run Windows STT (SpeechRecognitionEngine) on each MP3 file individually.
    /// Converts MP3->WAV in temp, runs synchronous Recognize(), returns per-file labels.
    /// Offline processing = no YouTube audio interference.
    /// </summary>
    static Dictionary<string, SttLabel> SttRelabelFiles(List<string> mp3Files)
    {
        var results = new Dictionary<string, SttLabel>(StringComparer.OrdinalIgnoreCase);

        // Pick best recognizer (English preferred for YouTube content)
        var recognizers = SpeechRecognitionEngine.InstalledRecognizers();
        var chosen = recognizers.FirstOrDefault(r => r.Culture.Name.StartsWith("en"))
                  ?? recognizers.FirstOrDefault(r => r.Culture.Name.StartsWith("ko"))
                  ?? recognizers.FirstOrDefault();
        if (chosen == null)
        {
            Console.WriteLine("[STT] No speech recognizer available");
            return results;
        }
        Console.Error.WriteLine($"[STT] Using: {chosen.Description} ({chosen.Culture.Name})");

        for (int fi = 0; fi < mp3Files.Count; fi++)
        {
            var mp3Path = mp3Files[fi];
            var fileName = Path.GetFileName(mp3Path);
            string? wavTemp = null;
            try
            {
                // Convert MP3 -> WAV temp file (SpeechRecognitionEngine requires WAV)
                wavTemp = Path.Combine(Path.GetTempPath(), $"stt_{Guid.NewGuid():N}.wav");
                using (var reader = new Mp3FileReader(mp3Path))
                {
                    // Resample to 16kHz mono (optimal for speech recognition)
                    var monoFormat = new WaveFormat(16000, 16, 1);
                    using var resampler = new MediaFoundationResampler(reader, monoFormat)
                    {
                        ResamplerQuality = 30,
                    };
                    WaveFileWriter.CreateWaveFile(wavTemp, resampler);
                }

                // Run synchronous recognition
                using var engine = new SpeechRecognitionEngine(chosen);
                engine.LoadGrammar(new DictationGrammar());
                engine.SetInputToWaveFile(wavTemp);

                var result = engine.Recognize(TimeSpan.FromSeconds(30));
                if (result != null && result.Confidence >= 0.3f)
                {
                    // Embed STT result in filename: seg_001.mp3 -> seg_001_hello_how_are_you.mp3
                    var sttTag = SanitizeSttTag(result.Text);
                    var newPath = mp3Path;
                    if (!string.IsNullOrEmpty(sttTag))
                    {
                        var dir = Path.GetDirectoryName(mp3Path) ?? "";
                        var nameNoExt = Path.GetFileNameWithoutExtension(mp3Path);
                        // Don't re-tag if already tagged (avoid stacking)
                        if (!nameNoExt.Contains("_stt_"))
                        {
                            var newName = $"{nameNoExt}_stt_{sttTag}.mp3";
                            newPath = Path.Combine(dir, newName);
                            try
                            {
                                File.Move(mp3Path, newPath);
                                // Update mp3Files list entry for downstream (concat, move)
                                mp3Files[fi] = newPath;
                            }
                            catch { newPath = mp3Path; } // rename failed, keep original
                        }
                    }
                    results[newPath] = new SttLabel(result.Text, result.Confidence);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"  [STT] {Path.GetFileName(newPath)}: \"{result.Text}\" ({result.Confidence:P0})");
                    Console.ResetColor();
                }
                else
                {
                    results[mp3Path] = new SttLabel("", 0f);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"  [STT] {fileName}: (no recognition)");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                results[mp3Path] = new SttLabel("", 0f);
                Console.WriteLine($"  [STT] {fileName}: error -- {ex.Message}");
            }
            finally
            {
                if (wavTemp != null) try { File.Delete(wavTemp); } catch { }
            }
        }

        return results;
    }

    /// <summary>Drop companion JSON with full analysis next to an MP3 file.</summary>
    static void DropCompanionJson(string mp3Path, StudyResult result, Dictionary<string, SttLabel> sttLabels)
    {
        try
        {
            var jsonPath = Path.ChangeExtension(mp3Path, ".json");
            var sttEntry = (result.SourceFile != null && sttLabels.TryGetValue(result.SourceFile, out var sttE))
                ? sttE : new SttLabel("", 0f);

            int mp3DurationMs = 0;
            try
            {
                using var dur = new Mp3FileReader(mp3Path);
                mp3DurationMs = (int)dur.TotalTime.TotalMilliseconds;
            }
            catch { }

            var companion = new
            {
                source = Path.GetFileName(result.SourceFile ?? mp3Path),
                transcript = result.Transcript,
                confidence = result.Confidence,
                language = result.Language,
                silenceBeforeMs = result.SilenceBeforeMs,
                stt = sttEntry.Text,
                sttConfidence = Math.Round(sttEntry.Confidence, 3),
                speaker = result.Speaker,
                durationMs = mp3DurationMs,
                words = result.Words.Select(w => new { w.Word, w.StartMs, w.DurMs, w.PhaseLrDeg }),
                segmentStartMs = result.SegmentStartMs,
                segmentDurationMs = result.SegmentDurationMs,
                analyzedAt = DateTime.Now.ToString("o"),
            };
            File.WriteAllText(jsonPath, JsonSerializer.Serialize(companion,
                new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [!] JSON write failed: {ex.Message}");
        }
    }

    /// <summary>TextWriter that writes to two targets simultaneously (for output capture).</summary>
    sealed class StudyCaptureWriter : TextWriter
    {
        private readonly TextWriter _a, _b;
        public StudyCaptureWriter(TextWriter a, TextWriter b) { _a = a; _b = b; }
        public override Encoding Encoding => _a.Encoding;
        public override void Write(char value) { _a.Write(value); _b.Write(value); }
        public override void Write(string? value) { _a.Write(value); _b.Write(value); }
        public override void WriteLine(string? value) { _a.WriteLine(value); _b.WriteLine(value); }
        public override void Flush() { _a.Flush(); _b.Flush(); }
    }

    /// <summary>Parse Gemini's JSON response into study results.</summary>
    static List<StudyResult>? ParseStudyResponse(string response, List<StudySegmentMap> segments)
    {
        // Extract JSON array from response (might be wrapped in markdown fences)
        var json = response;
        var jsonStart = json.IndexOf('[');
        var jsonEnd = json.LastIndexOf(']');
        if (jsonStart < 0 || jsonEnd < 0 || jsonEnd <= jsonStart)
        {
            Console.WriteLine("[STUDY] No JSON array found in response");
            return null;
        }
        json = json[jsonStart..(jsonEnd + 1)];

        try
        {
            JsonArray? arr = null;
            try { arr = JsonSerializer.Deserialize<JsonArray>(json); }
            catch
            {
                // Truncated JSON recovery: find last complete object "}" before the end
                // and close the array there
                int lastClose = json.LastIndexOf('}');
                if (lastClose > 0)
                {
                    var repaired = json[..(lastClose + 1)] + "]";
                    try
                    {
                        arr = JsonSerializer.Deserialize<JsonArray>(repaired);
                        Console.Error.WriteLine($"[STUDY] JSON repaired: truncated at char {lastClose}, recovered {arr?.Count ?? 0} segment(s)");
                    }
                    catch (Exception ex2)
                    {
                        Console.Error.WriteLine($"[STUDY] JSON parse error: {ex2.Message}");
                    }
                }
            }
            if (arr == null) return null;

            var results = new List<StudyResult>();
            foreach (var node in arr)
            {
                var segIndex = node?["segment"]?.GetValue<int>() ?? 0;
                var transcript = node?["transcript"]?.GetValue<string>() ?? "";
                var speaker = node?["speaker"]?.GetValue<string>() ?? "speaker_1";
                var confidence = node?["confidence"]?.GetValue<double>() ?? 0;
                var language = node?["language"]?.GetValue<string>() ?? "";
                var audioType = node?["audio_type"]?.GetValue<string>() ?? "speech";
                var silenceBeforeMs = node?["silence_before_ms"]?.GetValue<int>() ?? 0;

                // Match to source file
                var seg = segments.FirstOrDefault(s => s.Index == segIndex);
                if (seg == null && segIndex > 0 && segIndex <= segments.Count)
                    seg = segments[segIndex - 1];

                var words = new List<StudyWord>();
                if (node?["words"] is JsonArray wordsArr)
                {
                    foreach (var w in wordsArr)
                    {
                        words.Add(new StudyWord
                        {
                            Word = w?["word"]?.GetValue<string>() ?? "",
                            StartMs = w?["start_ms"]?.GetValue<int>() ?? 0,
                            DurMs = w?["dur_ms"]?.GetValue<int>() ?? 0,
                            PhaseLrDeg = w?["phase_lr_deg"]?.GetValue<double>() ?? 0,
                        });
                    }
                }

                results.Add(new StudyResult
                {
                    Segment = segIndex,
                    Transcript = transcript,
                    Speaker = speaker,
                    Confidence = confidence,
                    Language = language,
                    AudioType = audioType,
                    SilenceBeforeMs = silenceBeforeMs,
                    Words = words,
                    SourceFile = seg?.SourceFile,
                    SegmentStartMs = seg?.StartMs ?? 0,
                    SegmentDurationMs = seg?.DurationMs ?? 0,
                });
            }

            return results;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[STUDY] JSON parse error: {ex.Message}");
            return null;
        }
    }

    /// <summary>Save study results to JSONL.</summary>
    static void SaveStudyResults(string basePath, List<StudyResult> results)
    {
        var jsonlPath = Path.Combine(basePath, $"study_{DateTime.Now:yyyyMMdd}.jsonl");
        using var writer = new StreamWriter(jsonlPath, append: true);
        var opts = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        foreach (var r in results)
        {
            writer.WriteLine(JsonSerializer.Serialize(r, opts));
        }
        Console.Error.WriteLine($"[STUDY] Results saved to {jsonlPath}");
    }

    /// <summary>Save raw response for debugging.</summary>
    static void SaveRawResponse(string basePath, string response, List<StudySegmentMap> segments)
    {
        var rawPath = Path.Combine(basePath, $"study_raw_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        var sb = new StringBuilder();
        sb.AppendLine("=== Segment Map ===");
        foreach (var seg in segments)
            sb.AppendLine($"  {seg.Index}: {seg.StartMs}ms, {seg.DurationMs}ms, {seg.SourceFile}");
        sb.AppendLine("\n=== Raw Response ===");
        sb.AppendLine(response);
        File.WriteAllText(rawPath, sb.ToString());
        Console.Error.WriteLine($"[STUDY] Raw response saved to {rawPath}");
    }

    /// <summary>Sanitize STT text for embedding in filename (short, filesystem-safe).</summary>
    static string SanitizeSttTag(string text)
    {
        text = text.Trim().ToLowerInvariant();
        if (text.Length > 40) text = text[..40]; // keep it short
        var invalid = Path.GetInvalidFileNameChars();
        var sb = new StringBuilder(text.Length);
        foreach (var c in text)
            sb.Append(invalid.Contains(c) || c == ' ' ? '_' : c);
        var result = sb.ToString().Trim('_');
        while (result.Contains("__")) result = result.Replace("__", "_");
        return result;
    }

    /// <summary>
    /// Jaccard similarity between word sets of two transcripts (case-insensitive).
    /// Returns 0.0 (no overlap) to 1.0 (identical). Handles short/empty strings gracefully.
    /// </summary>
    static double WordJaccard(string a, string b)
    {
        var wa = new HashSet<string>(a.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries));
        var wb = new HashSet<string>(b.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries));
        if (wa.Count == 0 && wb.Count == 0) return 1.0;
        if (wa.Count == 0 || wb.Count == 0) return 0.0;
        int intersection = wa.Count(w => wb.Contains(w));
        int union = wa.Union(wb).Count();
        return union == 0 ? 0.0 : (double)intersection / union;
    }

    static string SanitizeFolderName(string text)
    {
        // Use first meaningful phrase as folder name (max 60 chars)
        text = text.Trim();
        if (text.Length > 60) text = text[..60];
        var invalid = Path.GetInvalidFileNameChars();
        var sb = new StringBuilder(text.Length);
        foreach (var c in text)
            sb.Append(invalid.Contains(c) ? '_' : c == ' ' ? '_' : c);
        var result = sb.ToString().Trim('_').ToLowerInvariant();
        // Collapse multiple underscores
        while (result.Contains("__")) result = result.Replace("__", "_");
        return result.Length == 0 ? "_unknown" : result;
    }

    // -- Prune-simple subcommand --

    /// <summary>
    /// Delete _unknown/ MP3s with simple/trivial token sequences in their filenames.
    /// "Simple" = very few tokens (noise/silence) OR highly repetitive (noise loop).
    ///   --min-tokens N   : delete files with fewer than N tokens (default 7)
    ///   --max-unique N   : delete files where unique token count ≤ N AND total ≥ 5 (default 3)
    ///   --dry-run        : show what would be deleted without deleting
    /// </summary>
    static int WhisperPruneSimpleCommand(string[] args)
    {
        int  minTokens = 7;
        int  maxUnique = 3;
        bool dryRun    = false;
        for (int i = 0; i < args.Length; i++)
        {
            if      (args[i] == "--min-tokens" && i + 1 < args.Length) int.TryParse(args[++i], out minTokens);
            else if (args[i] == "--max-unique" && i + 1 < args.Length) int.TryParse(args[++i], out maxUnique);
            else if (args[i] == "--dry-run") dryRun = true;
        }

        var wavDir = Path.Combine(DataDir, "profiles", "whisper_exp", "wav", "_unknown");
        if (!Directory.Exists(wavDir))
        {
            Console.WriteLine("[PRUNE] _unknown/ not found.");
            return 1;
        }

        var files = Directory.GetFiles(wavDir, "*.mp3");
        Console.Error.WriteLine($"[PRUNE] Scanning {files.Length} files in _unknown/ (min-tokens={minTokens}, max-unique={maxUnique}{(dryRun ? ", DRY-RUN" : "")})");

        int deleted = 0, kept = 0;
        foreach (var f in files)
        {
            var nameNoExt = Path.GetFileNameWithoutExtension(f);
            // Strip trailing _V or _W suffix
            if (nameNoExt.EndsWith("_V") || nameNoExt.EndsWith("_W"))
                nameNoExt = nameNoExt[..^2];

            // Parse tokens: split on '-' and ' ' (pause marker), ignore empty
            var tokens = nameNoExt.Split(new[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                  .Where(t => int.TryParse(t, out _))
                                  .ToList();
            int total  = tokens.Count;
            int unique = tokens.Distinct().Count();

            bool tooShort    = total < minTokens;
            bool tooRepetive = total >= 5 && unique <= maxUnique;

            if (tooShort || tooRepetive)
            {
                var reason = tooShort ? $"tokens={total}<{minTokens}" : $"unique={unique}<={maxUnique} of {total}";
                Console.WriteLine($"  [{(dryRun ? "DRY" : "DEL")}] {Path.GetFileName(f)}  ({reason})");
                if (!dryRun)
                    try { File.Delete(f); deleted++; } catch (Exception ex) { Console.WriteLine($"    WARN: {ex.Message}"); }
                else
                    deleted++;
            }
            else
                kept++;
        }

        Console.Error.WriteLine($"[PRUNE] Done -- {(dryRun ? "would delete" : "deleted")} {deleted}, kept {kept}");
        return 0;
    }

    // -- Stats subcommand --


    static int WhisperStatsCommand()
    {
        var basePath = Path.Combine(DataDir, "profiles", "whisper_exp");
        var wavDir = Path.Combine(basePath, "wav");
        if (!Directory.Exists(wavDir))
        {
            Console.WriteLine("[STATS] No wav/ directory found.");
            return 0;
        }

        Console.WriteLine("[STATS] Whisper Study Statistics");
        Console.WriteLine(new string('─', 60));

        int totalFiles = 0;
        long totalSize = 0;
        var folders = new List<(string name, int count, long size)>();

        foreach (var dir in Directory.GetDirectories(wavDir).OrderBy(d => d))
        {
            var mp3s = Directory.GetFiles(dir, "*.mp3");
            if (mp3s.Length == 0) continue;
            var dirSize = mp3s.Sum(f => new FileInfo(f).Length);
            folders.Add((Path.GetFileName(dir), mp3s.Length, dirSize));
            totalFiles += mp3s.Length;
            totalSize += dirSize;
        }

        // Root files
        var rootMp3s = Directory.GetFiles(wavDir, "*.mp3")
            .Where(f => !Path.GetFileName(f).StartsWith("study_batch_")).ToArray();
        if (rootMp3s.Length > 0)
        {
            var rootSize = rootMp3s.Sum(f => new FileInfo(f).Length);
            folders.Insert(0, ("(root)", rootMp3s.Length, rootSize));
            totalFiles += rootMp3s.Length;
            totalSize += rootSize;
        }

        foreach (var (name, count, size) in folders)
        {
            var marker = name == "_unknown" ? " <-" : "";
            Console.WriteLine($"  {name,-40} {count,4} files  {size / 1024,6}KB{marker}");
        }
        Console.WriteLine(new string('─', 60));
        Console.WriteLine($"  {"Total",-40} {totalFiles,4} files  {totalSize / 1024,6}KB");

        // Study results
        var jsonlFiles = Directory.GetFiles(basePath, "study_*.jsonl");
        if (jsonlFiles.Length > 0)
        {
            Console.WriteLine();
            Console.WriteLine("[STATS] Study sessions:");
            foreach (var jf in jsonlFiles.OrderByDescending(f => f))
            {
                var lines = File.ReadAllLines(jf).Length;
                Console.WriteLine($"  {Path.GetFileName(jf)}: {lines} entries");
            }
        }

        return 0;
    }

    // -- JSON models --

    record SttLabel(string Text, float Confidence);

    sealed class StudySegmentMap
    {
        public int Index { get; set; }
        public int StartMs { get; set; }
        public int DurationMs { get; set; }
        public string SourceFile { get; set; } = "";
    }

    sealed class StudyResult
    {
        public int Segment { get; set; }
        public string Transcript { get; set; } = "";
        public string Speaker { get; set; } = "speaker_1";
        public double Confidence { get; set; }
        public string Language { get; set; } = "";
        public string AudioType { get; set; } = "speech"; // speech/music/instrument/noise/mixed
        public int SilenceBeforeMs { get; set; }
        public List<StudyWord> Words { get; set; } = new();
        public string? SourceFile { get; set; }
        public int SegmentStartMs { get; set; }
        public int SegmentDurationMs { get; set; }
    }

    sealed class StudyWord
    {
        public string Word { get; set; } = "";
        public int StartMs { get; set; }
        public int DurMs { get; set; }
        public double PhaseLrDeg { get; set; }
    }

    // -- Sound code -> 초성 phoneme mapping --
    // MP3 filename: "45012-63547-..._W.mp3" -> first code = 45012
    // Transcript: "카드결제" -> first char '카' -> 초성 'ㅋ'
    // Append mapping: {code: 45012, cho: "ㅋ", transcript: "카드결제", ts: ...}

    /// <summary>Korean 초성 (initial consonant) table -- index into Unicode Hangul Jamo.</summary>
    private static readonly string[] Choseong =
    [
        "ㄱ","ㄲ","ㄴ","ㄷ","ㄸ","ㄹ","ㅁ","ㅂ","ㅃ","ㅅ",
        "ㅆ","ㅇ","ㅈ","ㅉ","ㅊ","ㅋ","ㅌ","ㅍ","ㅎ"
    ];

    /// <summary>Extract 초성 from a Korean syllable char. Returns null for non-Korean.</summary>
    static string? GetChoseong(char c)
    {
        if (c < '\uAC00' || c > '\uD7A3') return null;
        int index = (c - '\uAC00') / (21 * 28);
        return index < Choseong.Length ? Choseong[index] : null;
    }

    /// <summary>Parse first sound code from MP3 filename (before first '-' or '_').</summary>
    static ushort ParseFirstSoundCode(string mp3Path)
    {
        var name = Path.GetFileNameWithoutExtension(mp3Path);
        // Format: "45012-63547-23456-0_20260310_154642_559_W"
        var dash = name.IndexOf('-');
        var token = dash > 0 ? name[..dash] : name;
        return ushort.TryParse(token, out var code) ? code : (ushort)0;
    }

    /// <summary>Build phoneme map: first sound code -> 초성 of transcript's first Korean char.</summary>
    static int BuildPhonemeMap(string basePath, List<StudyResult> results)
    {
        var mapPath = Path.Combine(basePath, "phoneme_map.jsonl");
        int count = 0;

        try
        {
            using var writer = new StreamWriter(mapPath, append: true, encoding: System.Text.Encoding.UTF8);
            foreach (var result in results)
            {
                if (result.SourceFile == null || string.IsNullOrWhiteSpace(result.Transcript))
                    continue;

                var code = ParseFirstSoundCode(result.SourceFile);
                if (code == 0) continue;

                // Find first Korean character in transcript (null = non-speech)
                string? cho = null;
                string syllable = "";
                foreach (char c in result.Transcript)
                {
                    cho = GetChoseong(c);
                    if (cho != null) { syllable = c.ToString(); break; }
                }
                // Non-Korean transcript -> check if English letter (first alpha char)
                string? firstAlpha = null;
                if (cho == null)
                {
                    foreach (char c in result.Transcript)
                        if (char.IsLetter(c)) { firstAlpha = c.ToString().ToLowerInvariant(); break; }
                }

                var entry = new
                {
                    sc = code,
                    cho,                     // null = non-Korean (noise, English, etc.)
                    syllable,                // "카", "" if non-Korean
                    alpha = firstAlpha,      // "h" for "hello", null if Korean
                    type = result.AudioType, // speech/music/instrument/noise/mixed
                    lang = result.Language,   // ko/en/ja/zh/...
                    transcript = result.Transcript.Length > 20
                        ? result.Transcript[..20] : result.Transcript,
                    confidence = Math.Round(result.Confidence, 2),
                    t = DateTime.UtcNow.Ticks,
                };
                writer.WriteLine(JsonSerializer.Serialize(entry));
                count++;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[STUDY] Phoneme map error: {ex.Message}");
        }
        return count;
    }

    // -- Self-Healing Pipeline --
    // After each study batch: reconcile conflicts, requeue low-confidence, migrate noise.
    // Architecture: append-only phoneme_map.jsonl -> majority vote -> canonical label.

    static void SelfHeal(string basePath, string wavDir)
    {
        var mapPath = Path.Combine(basePath, "phoneme_map.jsonl");
        if (!File.Exists(mapPath)) return;

        try
        {
            // 1. Load all phoneme_map entries, group by sound code
            var entries = new List<PhonemeEntry>();
            foreach (var line in File.ReadLines(mapPath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var doc = JsonSerializer.Deserialize<JsonNode>(line);
                    if (doc == null) continue;
                    entries.Add(new PhonemeEntry
                    {
                        Sc = (ushort)(doc["sc"]?.GetValue<int>() ?? 0),
                        Cho = doc["cho"]?.GetValue<string>(),
                        Type = doc["type"]?.GetValue<string>() ?? "speech",
                        Transcript = doc["transcript"]?.GetValue<string>() ?? "",
                        Confidence = doc["confidence"]?.GetValue<double>() ?? 0,
                    });
                }
                catch { }
            }

            if (entries.Count == 0) return;

            var grouped = entries.Where(e => e.Sc > 0).GroupBy(e => e.Sc).ToList();
            int conflicts = 0, requeued = 0, noiseMigrated = 0;

            // 2. Conflict detection + majority vote
            var canonical = new Dictionary<ushort, string>(); // sc -> winning label
            foreach (var group in grouped)
            {
                var labels = group.Select(e => e.Cho ?? e.Type ?? "_unknown").ToList();
                var best = labels.GroupBy(l => l)
                    .OrderByDescending(g => g.Count())
                    .First();
                double winRate = (double)best.Count() / labels.Count;

                if (labels.Distinct().Count() > 1)
                    conflicts++;

                // Majority vote: ≥60% -> canonical
                if (winRate >= 0.6)
                    canonical[group.Key] = best.Key;
            }

            // 3. Low-confidence requeue: files with avg confidence < 0.3 AND ≥3 observations
            var requeueDir = Path.Combine(wavDir, "_requeue");
            foreach (var group in grouped)
            {
                if (group.Count() < 3) continue;
                double avgConf = group.Average(e => e.Confidence);
                if (avgConf >= 0.3) continue;

                // Find files in their current folders that match this sound code
                foreach (var subDir in Directory.GetDirectories(wavDir))
                {
                    var dirName = Path.GetFileName(subDir);
                    if (dirName.StartsWith("_")) continue; // skip system folders

                    foreach (var mp3 in Directory.GetFiles(subDir, "*.mp3"))
                    {
                        var code = ParseFirstSoundCode(mp3);
                        if (code != group.Key) continue;

                        Directory.CreateDirectory(requeueDir);
                        var dest = Path.Combine(requeueDir, Path.GetFileName(mp3));
                        if (!File.Exists(dest))
                        {
                            try { File.Move(mp3, dest); requeued++; } catch { }
                            // Move companion JSON too
                            var jsonSrc = Path.ChangeExtension(mp3, ".json");
                            var jsonDst = Path.ChangeExtension(dest, ".json");
                            if (File.Exists(jsonSrc))
                                try { File.Move(jsonSrc, jsonDst); } catch { }
                        }
                    }
                }
            }

            // 4. Noise auto-migration: sound codes with ≥3 observations, avg confidence < 0.3,
            //    AND no clear speech winner -> move to _noise/
            var noiseDir = Path.Combine(wavDir, "_noise");
            foreach (var group in grouped)
            {
                if (group.Count() < 3) continue;
                double avgConf = group.Average(e => e.Confidence);
                bool allNonSpeech = group.All(e => e.Type != "speech");
                if (avgConf < 0.3 && allNonSpeech)
                {
                    // Move any files with this code from non-system folders to _noise/
                    foreach (var subDir in Directory.GetDirectories(wavDir))
                    {
                        var dirName = Path.GetFileName(subDir);
                        if (dirName.StartsWith("_")) continue;

                        foreach (var mp3 in Directory.GetFiles(subDir, "*.mp3"))
                        {
                            var code = ParseFirstSoundCode(mp3);
                            if (code != group.Key) continue;

                            Directory.CreateDirectory(noiseDir);
                            var dest = Path.Combine(noiseDir, Path.GetFileName(mp3));
                            if (!File.Exists(dest))
                            {
                                try { File.Move(mp3, dest); noiseMigrated++; } catch { }
                                var jsonSrc = Path.ChangeExtension(mp3, ".json");
                                var jsonDst = Path.ChangeExtension(dest, ".json");
                                if (File.Exists(jsonSrc))
                                    try { File.Move(jsonSrc, jsonDst); } catch { }
                            }
                        }
                    }
                }
            }

            // 5. Quarantine re-promotion: files quarantined >2 batches ago (companion JSON age)
            //    -> move back to _unknown/ for re-study (second chance with fresh context)
            var quarantineDir2 = Path.Combine(wavDir, "_quarantine");
            int rePromoted = 0;
            if (Directory.Exists(quarantineDir2))
            {
                var unknownDir2 = Path.Combine(wavDir, "_unknown");
                var cutoff = DateTime.Now - TimeSpan.FromHours(1); // re-study after 1h
                foreach (var mp3 in Directory.GetFiles(quarantineDir2, "*.mp3"))
                {
                    var jsonPath = Path.ChangeExtension(mp3, ".json");
                    var refTime = File.Exists(jsonPath) ? File.GetLastWriteTime(jsonPath) : File.GetLastWriteTime(mp3);
                    if (refTime > cutoff) continue; // too recent -- skip

                    Directory.CreateDirectory(unknownDir2);
                    var dest = Path.Combine(unknownDir2, Path.GetFileName(mp3));
                    if (File.Exists(dest)) continue;
                    try
                    {
                        File.Move(mp3, dest); rePromoted++;
                        if (File.Exists(jsonPath)) try { File.Delete(jsonPath); } catch { }
                    }
                    catch { }
                }
            }

            if (conflicts > 0 || requeued > 0 || noiseMigrated > 0 || rePromoted > 0)
                Console.Error.WriteLine($"[HEAL] conflicts={conflicts} requeued={requeued} noise={noiseMigrated} canonical={canonical.Count} quarantine_requeue={rePromoted}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[HEAL] Error: {ex.Message}");
        }
    }

    sealed class PhonemeEntry
    {
        public ushort Sc { get; set; }
        public string? Cho { get; set; }
        public string? Type { get; set; }
        public string Transcript { get; set; } = "";
        public double Confidence { get; set; }
    }
}
