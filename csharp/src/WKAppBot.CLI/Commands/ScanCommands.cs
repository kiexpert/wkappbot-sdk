using System.Text;
using WKAppBot.Core.Runner;
using WKAppBot.Vision;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: scan + form-dump commands
internal partial class Program
{
    // ── scan ──────────────────────────────────────────────────

    static int ScanCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot scan <window-title> [--save] [--ocr] [--detail] [--depth N]");

        string title = args[0];
        bool save = args.Contains("--save");
        bool useOcr = args.Contains("--ocr");
        bool captureDetails = args.Contains("--detail");
        int depth = 1; // default: form level only
        var depthStr = GetArgValue(args, "--depth");
        if (depthStr != null) int.TryParse(depthStr, out depth);

        // Find target window
        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Window not found: \"{title}\"");
            Console.ResetColor();
            return 1;
        }

        var win = windows[0];
        Console.WriteLine($"Target: [{win.Handle:X8}] \"{win.Title}\" ({win.ClassName}) {win.Rect.Width}x{win.Rect.Height}");
        Console.WriteLine();

        // Check for existing profile
        var profileStore = new ProfileStore();
        var match = profileStore.FindByMatch(win.ClassName, "");
        string? profileName = null;
        AppProfile? existingProfile = null;

        // Get process name for profile matching
        NativeMethods.GetWindowThreadProcessId(win.Handle, out uint pid);
        string processName = "";
        try { processName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }

        if (match == null && !string.IsNullOrEmpty(processName))
            match = profileStore.FindByMatch("", processName);

        if (match != null)
        {
            profileName = match.Value.name;
            existingProfile = match.Value.profile;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Profile: {profileName} (matched, scan #{existingProfile.ScanCount + 1})");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Profile: (new — use --save to create)");
            Console.ResetColor();
        }
        Console.WriteLine();

        // Run scan
        var scanResult = AppScanner.Scan(win.Handle);

        // Print summary
        var summary = AppScanner.FormatSummary(scanResult, profileName);
        Console.Write(summary);

        // Determine profile name early (needed for OCR + exp DB)
        if (profileName == null && (save || useOcr))
            profileName = ProfileStore.GenerateProfileName(scanResult);

        // Experience DB
        var expDir = profileName != null
            ? Path.Combine(profileStore.ProfileDir, $"{profileName}_exp")
            : null;
        ExperienceDb? expDb = expDir != null ? new ExperienceDb(expDir) : null;

        // ── OCR learning pass (--ocr) ──
        if (useOcr && expDb != null)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("── OCR Learning ────────────────────────");
            Console.ResetColor();

            // Initialize Simple OCR
            SimpleOcrAnalyzer? simpleOcr = null;
            try
            {
                var ocrLangs = SimpleOcrAnalyzer.GetAvailableLanguages();
                var primaryLang = ocrLangs.Contains("ko") ? "ko" : ocrLangs.FirstOrDefault() ?? "en-US";
                simpleOcr = new SimpleOcrAnalyzer(primaryLanguage: primaryLang, confidenceThreshold: 0.5f);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  OCR init failed: {ex.Message}");
                Console.ResetColor();
            }

            if (simpleOcr != null)
            {
                try
                {
                    // Bridge: SimpleOcrAnalyzer.RecognizeAll → OcrWordInfo[]
                    // (avoids WKAppBot.Vision dependency in Win32 project)
                    async Task<IReadOnlyList<OcrWordInfo>> OcrBridge(System.Drawing.Bitmap bmp)
                    {
                        var ocrFull = await simpleOcr.RecognizeAll(bmp);
                        return ocrFull.Words.Select(w => new OcrWordInfo
                        {
                            Text = w.Text,
                            X = w.X,
                            Y = w.Y,
                            Width = w.Width,
                            Height = w.Height,
                        }).ToArray();
                    }

                    var ocrResult = AppScanner.LearnFormsWithOcr(
                        scanResult, expDb, OcrBridge,
                        onProgress: (formId, count) =>
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($"\r  Scanning [{formId}]... {count} controls learned");
                            Console.ResetColor();
                        },
                        captureDetails: captureDetails
                    ).GetAwaiter().GetResult();

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"  {ocrResult}");
                    Console.ResetColor();

                    foreach (var err in ocrResult.Errors)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  Warning: {err}");
                        Console.ResetColor();
                    }

                    // ── Control Detail Cache (auto on first encounter, always on --detail) ──
                    if (ocrResult.DetailScreenshots > 0)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("── Control Detail Cache ────────────────");
                        Console.ResetColor();

                        // Per-form stats
                        foreach (var formGroup2 in scanResult.Forms
                            .Where(f => f.FormId != null && f.IsVisible)
                            .GroupBy(f => f.FormId!))
                        {
                            var fid = formGroup2.Key;
                            var fExp = expDb.GetForm(fid);
                            if (fExp == null) continue;
                            var ctrlDir = Path.Combine(expDb.ExpDir, $"form_{fid}", "controls");
                            if (Directory.Exists(ctrlDir))
                            {
                                var cidDirs = Directory.GetDirectories(ctrlDir);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"  [{fid}] {fExp.Controls.Count} controls → {cidDirs.Length} screenshots saved");
                                Console.ResetColor();
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"  Total: {ocrResult.DetailScreenshots} screenshots, {ocrResult.DetailTextChanges} text history entries");
                        Console.ResetColor();
                    }

                    // ── Text Snapshot Collection (Puppet Pattern) ──
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("── Text Snapshot (Puppet Pattern) ─────");
                    Console.ResetColor();

                    int snapshotCount = 0;
                    var formGroupsForSnapshot = scanResult.Forms
                        .Where(f => f.FormId != null && f.IsVisible && f.Rect.Width > 50 && f.Rect.Height > 50)
                        .GroupBy(f => f.FormId!)
                        .ToList();

                    foreach (var snapGroup in formGroupsForSnapshot)
                    {
                        var snapFormId = snapGroup.Key;
                        var snapForm = snapGroup.First();

                        try
                        {
                            var textLines = AppScanner.CollectFormTextSnapshot(
                                snapForm.Handle, snapForm.Rect, maxDepth: 4);

                            if (textLines.Count > 0)
                            {
                                expDb.AddTextSnapshot(snapFormId, textLines);
                                snapshotCount++;

                                var formExp = expDb.GetForm(snapFormId);
                                if (formExp?.PuppetPattern != null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"  [{snapFormId}] Puppet pattern built ({textLines.Count} lines, scan #{formExp.PuppetScanCount})");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine($"  [{snapFormId}] {textLines.Count} text lines collected (need 1 more scan for pattern)");
                                    Console.ResetColor();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"  [{snapFormId}] Snapshot failed: {ex.Message}");
                            Console.ResetColor();
                        }
                    }

                    if (snapshotCount > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"  {snapshotCount} text snapshots collected");
                        Console.ResetColor();
                    }

                    // Save experience DB (includes text snapshots + puppet patterns)
                    expDb.SaveAll();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"  Experience saved: {expDir}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  OCR error: {ex.Message}");
                    Console.ResetColor();
                }
                finally
                {
                    simpleOcr.Dispose();
                }
            }
        }

        // Print experience DB stats (with fingerprint + keywords)
        if (expDb != null)
        {
            Console.WriteLine();
            Console.WriteLine(expDb.GetStatsString());

            // Show fingerprint + keywords for each form type
            foreach (var (formId, formExp) in expDb.GetAllForms())
            {
                var ctrlCount = formExp.Controls.Count;
                var fpStr = formExp.FingerprintHash != null ? $"fp={formExp.FingerprintHash}" : "fp=(none)";
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"  [{formId}] {formExp.FormName}: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{ctrlCount} controls, ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(fpStr);

                if (formExp.OcrKeywords != null && formExp.OcrKeywords.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    var kwList = string.Join(", ", formExp.OcrKeywords.Take(15));
                    var more = formExp.OcrKeywords.Count > 15 ? $" +{formExp.OcrKeywords.Count - 15} more" : "";
                    Console.WriteLine($"         keywords: [{kwList}]{more}");
                }
                if (formExp.PuppetPattern != null)
                {
                    var pLines = formExp.PuppetPattern.Split('\n');
                    int totalLines = pLines.Length;
                    int dynamicLines = pLines.Count(l => l.Trim() == "{*}");
                    int fixedLines = totalLines - dynamicLines;
                    int wildcards = pLines.Sum(l => l.Split("{*}").Length - 1);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"         puppet: {fixedLines} fixed + {dynamicLines} dynamic lines, {wildcards} fields (scan #{formExp.PuppetScanCount})");
                }
                Console.ResetColor();
            }
        }

        // ── Save profile ──
        if (save)
        {
            AppProfile profile;
            if (existingProfile != null)
            {
                // Update existing profile
                profile = existingProfile;
                profile.ScanCount++;
                profile.UpdatedAt = DateTime.UtcNow;

                // Merge new form types
                var newFormGroups = scanResult.Forms
                    .Where(f => f.FormId != null)
                    .GroupBy(f => f.FormId!);

                foreach (var g in newFormGroups)
                {
                    if (!profile.FormTypes.ContainsKey(g.Key))
                    {
                        var first = g.First();
                        profile.FormTypes[g.Key] = new FormTypeDefinition
                        {
                            Name = first.FormName ?? g.Key,
                            FrameClass = first.ClassName,
                            ScanCount = 1,
                        };
                    }
                    else
                    {
                        profile.FormTypes[g.Key].ScanCount++;
                    }

                    // Copy fingerprint hash from experience DB
                    var formExp = expDb?.GetForm(g.Key);
                    if (formExp?.FingerprintHash != null)
                        profile.FormTypes[g.Key].FingerprintHash = formExp.FingerprintHash;
                }
            }
            else
            {
                // Create new profile from scan (with fingerprint hashes from experience DB)
                profile = ProfileStore.FromScanResult(scanResult, expDb);
            }

            profileStore.Save(profileName!, profile);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Profile saved: {Path.Combine(profileStore.ProfileDir, profileName + ".json")}");
            Console.ResetColor();
        }

        // ── ActionState IPC: share scan info with AppBotEye ──
        try
        {
            var formCount = scanResult.Forms.Count;
            ActionState.Write(new ActionState
            {
                Source = "scan",
                WindowTitle = win.Title,
                ActionName = "scan",
                ActionDetail = $"Scan: {formCount} forms found" + (save ? " (saved)" : ""),
                Status = "pass",
            });
        }
        catch { /* best-effort */ }

        return 0;
    }

    // ── form-dump ─────────────────────────────────────────────

    static int FormDumpCommand(string[] args)
    {
        if (args.Length < 2)
            return Error("Usage: appbot form-dump <window-title> <form-id> [--depth N]");

        string title = args[0];
        string targetFormId = args[1];
        int maxDepth = int.TryParse(GetArgValue(args, "--depth"), out var d) ? d : 6;

        var windows = WindowFinder.FindByTitle(title);
        if (windows.Count == 0) return Error($"Window not found: \"{title}\"");
        var win = windows[0];

        var scanResult = AppScanner.Scan(win.Handle);
        var targetForm = scanResult.Forms.FirstOrDefault(f => f.FormId == targetFormId);
        if (targetForm == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Form [{targetFormId}] not found.");
            Console.ResetColor();
            foreach (var f in scanResult.Forms.Where(f => f.FormId != null).GroupBy(f => f.FormId!))
                Console.WriteLine($"  [{f.First().FormId}] {f.First().FormName}");
            return 1;
        }

        Console.WriteLine($"Form: [{targetForm.FormId}] {targetForm.FormName} ({targetForm.Rect.Width}x{targetForm.Rect.Height})");
        Console.WriteLine();

        DumpFormTree(targetForm.Handle, 0, maxDepth);
        return 0;
    }
}
