namespace WKAppBot.CLI;

// partial class: wkappbot skill verify / audit -- self-heal ref/regression checks
internal partial class Program
{
    // -- Verify ------------------------------------------------------------------─

    static int SkillVerifyCommand(string[] args)
    {
        if (args.Length == 0) { Console.WriteLine("Usage: wkappbot skill verify <id> [--skip-evidence]"); return 1; }
        bool skipEvidence = args.Any(a => a == "--skip-evidence");
        var skill = FindSkill(args[0]);
        if (skill == null) { Console.Error.WriteLine($"[SKILL] Not found: {args[0]}"); return 1; }
        var (ok, missing, stale) = RunVerify(skill, verbose: true);
        var regResult = RunSkillRegressionScript(skill);
        if (regResult == -1)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Error.WriteLine($"[SKILL] No regression script registered for '{skill.Id}'.");
            Console.Error.WriteLine($"  -> To add one: wkappbot skill contribute --id {skill.Id} --regression-script <test-{skill.App}-*.sh>");
            Console.ResetColor();
        }

        // Replay the Verified-step evidence scripts accumulated via resolves.
        // Each `suggest resolve` appends a "Verified YYYY-MM-DD: <path>" step;
        // re-running those scripts is the ultimate self-heal check -- if a
        // regression sneaks back in, the same test that proved the original
        // fix will now fail and skill verify surfaces it immediately.
        int evidencePass = 0, evidenceFail = 0, evidenceMissing = 0;
        if (!skipEvidence)
        {
            (evidencePass, evidenceFail, evidenceMissing) = RunVerifiedEvidenceScripts(skill);
        }

        return (missing + stale > 0 || regResult > 0 || evidenceFail > 0 || evidenceMissing > 0) ? 1 : 0;
    }

    // -- Evidence replay: re-run each "Verified ..." step's script -----------
    static (int pass, int fail, int missing) RunVerifiedEvidenceScripts(SkillRecord skill)
    {
        if (skill.Steps == null || skill.Steps.Count == 0) return (0, 0, 0);
        var verifiedRegex = new System.Text.RegularExpressions.Regex(
            @"^Verified\s+\d{4}-\d{2}-\d{2}:\s*(?<path>\S[^(]*?)(?:\s*\(resolve ts=.*)?$",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        var hqDir = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq");

        int pass = 0, fail = 0, missing = 0;
        var entries = new List<(string relPath, string absPath)>();
        foreach (var step in skill.Steps)
        {
            var m = verifiedRegex.Match(step.Trim());
            if (!m.Success) continue;
            var rel = m.Groups["path"].Value.Trim();
            var abs = Path.IsPathRooted(rel) ? rel : Path.Combine(hqDir, rel);
            entries.Add((rel, abs));
        }
        if (entries.Count == 0) return (0, 0, 0);

        Console.Error.WriteLine($"[SKILL] Replaying {entries.Count} Verified evidence script(s)...");
        foreach (var (rel, abs) in entries)
        {
            if (!File.Exists(abs))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"    ❌ EVIDENCE MISSING: {rel}");
                Console.ResetColor();
                missing++;
                continue;
            }
            var ext = Path.GetExtension(abs).ToLowerInvariant();
            var psi = ext switch
            {
                ".sh"  => new System.Diagnostics.ProcessStartInfo("bash", $"\"{abs}\"")
                            { UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true },
                ".ps1" => new System.Diagnostics.ProcessStartInfo("powershell", $"-NoProfile -ExecutionPolicy Bypass -File \"{abs}\"")
                            { UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true },
                ".cmd" or ".bat" => new System.Diagnostics.ProcessStartInfo("cmd", $"/c \"{abs}\"")
                            { UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true },
                _ => null
            };
            if (psi == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"    ⚠️  unsupported ext {ext}: {rel}");
                Console.ResetColor();
                missing++;
                continue;
            }
            var sw = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                using var proc = AppBotPipe.StartTracked(psi, Path.GetDirectoryName(abs) ?? Environment.CurrentDirectory, "SKILL-VERIFY");
                if (proc == null) { fail++; continue; }
                if (!proc.WaitForExit(300_000)) // 5-min cap per script
                {
                    try { proc.Kill(entireProcessTree: true); } catch { }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"    ❌ TIMEOUT {sw.ElapsedMilliseconds}ms: {rel}");
                    Console.ResetColor();
                    fail++;
                    continue;
                }
                sw.Stop();
                if (proc.ExitCode == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"    ✅ PASS ({sw.ElapsedMilliseconds}ms): {rel}");
                    Console.ResetColor();
                    pass++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"    ❌ FAIL exit={proc.ExitCode} ({sw.ElapsedMilliseconds}ms): {rel}");
                    Console.ResetColor();
                    fail++;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"    ❌ EXEC ERROR: {rel}: {ex.Message}");
                Console.ResetColor();
                fail++;
            }
        }

        Console.Error.WriteLine($"[SKILL] Evidence replay: {pass} pass, {fail} fail, {missing} missing");
        return (pass, fail, missing);
    }

    // -- Audit --------------------------------------------------------------------

    static int SkillAuditCommand(string[] args)
    {
        string? appFilter = null;
        for (int i = 0; i < args.Length; i++)
            if (args[i] == "--app" && i + 1 < args.Length) appFilter = args[++i];

        var skills = LoadAllSkills(appFilter);
        if (skills.Count == 0) { Console.WriteLine("[SKILL] No skills to audit."); return 0; }

        int totalOk = 0, totalIssues = 0, noRefs = 0;
        var issueIds = new List<string>();

        Console.Error.WriteLine($"[SKILL] Auditing {skills.Count} skill(s)...");
        foreach (var skill in skills.OrderBy(s => s.App).ThenBy(s => s.Id))
        {
            if (skill.SourceRefs == null || skill.SourceRefs.Count == 0) { noRefs++; continue; }
            var (ok, missing, stale) = RunVerify(skill, verbose: false);
            if (missing + stale > 0)
            {
                Console.WriteLine($"  ! [{skill.App}] {skill.Id}:");
                RunVerify(skill, verbose: true);
                issueIds.Add(skill.Id);
                totalIssues++;
            }
            else totalOk++;
        }

        Console.WriteLine();
        Console.Error.WriteLine($"[SKILL] Audit: {totalOk} ok, {totalIssues} stale/missing, {noRefs} without refs");
        if (issueIds.Count > 0)
        {
            Console.WriteLine("  -> Fix: wkappbot skill read <id>  then  wkappbot skill contribute ...");
            foreach (var id in issueIds) Console.WriteLine($"    wkappbot skill read {id}");
        }
        return totalIssues > 0 ? 1 : 0;
    }

    // Returns (ok, missing, stale). When verbose=true prints per-ref status.
    static (int ok, int missing, int stale) RunVerify(SkillRecord skill, bool verbose)
    {
        if (skill.SourceRefs == null || skill.SourceRefs.Count == 0)
        {
            if (verbose) Console.Error.WriteLine($"[SKILL] {skill.Id}: no source_refs (nothing to verify)");
            return (0, 0, 0);
        }

        var cwd = EyeCmdPipeServer.CallerCwd.Value ?? Environment.CurrentDirectory;
        int ok = 0, missing = 0, stale = 0;

        foreach (var r in skill.SourceRefs)
        {
            var absPath = Path.IsPathRooted(r.File) ? r.File : Path.Combine(cwd, r.File);
            if (!File.Exists(absPath))
            {
                if (verbose) Console.WriteLine($"    ❌ FILE MISSING: {r.File}");
                missing++; continue;
            }
            if (string.IsNullOrEmpty(r.Pattern))
            {
                if (verbose) Console.WriteLine($"    ✅ {r.File} -- file exists");
                ok++; continue;
            }

            string[] lines;
            try { lines = File.ReadAllLines(absPath); }
            catch { missing++; continue; }

            int foundLine = -1;
            for (int i = 0; i < lines.Length; i++)
                if (lines[i].Contains(r.Pattern, StringComparison.Ordinal)) { foundLine = i + 1; break; }

            if (foundLine > 0)
            {
                if (verbose) Console.WriteLine($"    ✅ {r.File}:{foundLine} -- \"{r.Pattern}\"");
                ok++;
            }
            else
            {
                if (verbose)
                {
                    Console.WriteLine($"    ! PATTERN NOT FOUND: \"{r.Pattern}\" in {r.File}");
                    if (r.Line.HasValue)
                    {
                        int start = Math.Max(0, r.Line.Value - 2);
                        int count = Math.Min(5, lines.Length - start);
                        Console.WriteLine($"    (was line {r.Line}, now reads:)");
                        foreach (var l in lines.Skip(start).Take(count)) Console.WriteLine($"      | {l.TrimEnd()}");
                    }
                }
                stale++;
            }
        }

        // Regression script existence check
        if (!string.IsNullOrEmpty(skill.RegressionScript))
        {
            var scriptAbs = Path.IsPathRooted(skill.RegressionScript)
                ? skill.RegressionScript
                : Path.Combine(DataDir, skill.RegressionScript);
            if (!File.Exists(scriptAbs))
            {
                if (verbose) Console.WriteLine($"    ❌ REGRESSION SCRIPT MISSING: {skill.RegressionScript}");
                missing++;
            }
            else if (verbose)
                Console.Error.WriteLine($"[SKILL] {skill.Id}: regression script ✅ {Path.GetFileName(scriptAbs)}");
        }

        if (verbose && missing + stale == 0)
            Console.Error.WriteLine($"[SKILL] {skill.Id}: ✅ all {ok} ref(s) OK");
        return (ok, missing, stale);
    }

    // Runs the skill's regression script (if any). Returns 0=pass, 1=fail, -1=no script.
    static int RunSkillRegressionScript(SkillRecord skill)
    {
        if (string.IsNullOrEmpty(skill.RegressionScript)) return -1;
        var scriptAbs = Path.IsPathRooted(skill.RegressionScript)
            ? skill.RegressionScript
            : Path.Combine(DataDir, skill.RegressionScript);
        if (!File.Exists(scriptAbs))
        {
            Console.Error.WriteLine($"[SKILL] Regression script missing: {skill.RegressionScript}");
            return 1;
        }

        var ext = Path.GetExtension(scriptAbs).ToLowerInvariant();
        var (runner, runnerArgs) = ext switch
        {
            ".sh"  => (File.Exists(@"C:\Program Files\Git\usr\bin\bash.exe")
                        ? @"C:\Program Files\Git\usr\bin\bash.exe" : "bash",
                       $"\"{scriptAbs}\""),
            ".ps1" => ("powershell.exe", $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptAbs}\""),
            ".cmd" or ".bat" => ("cmd.exe", $"/c \"{scriptAbs}\""),
            _ => (null as string, null as string)
        };
        if (runner == null)
        {
            Console.Error.WriteLine($"[SKILL] Unsupported regression script type: {ext}");
            return 1;
        }

        Console.Error.WriteLine($"[SKILL] Running regression: {Path.GetFileName(scriptAbs)}");
        var psi = new System.Diagnostics.ProcessStartInfo
        {
            FileName = runner, Arguments = runnerArgs,
            UseShellExecute = false, CreateNoWindow = true,
            RedirectStandardOutput = true, RedirectStandardError = true,
        };
        psi.EnvironmentVariables["WKAPPBOT_WORKER"] = "1";
        var proc = AppBotPipe.StartTracked(psi, Environment.CurrentDirectory, "SKILL");
        var rOut = Task.Run(() => { string? l; while ((l = proc?.StandardOutput.ReadLine()) != null) Console.WriteLine($"  {l}"); });
        var rErr = Task.Run(() => { string? l; while ((l = proc?.StandardError.ReadLine()) != null) Console.Error.WriteLine($"  {l}"); });
        proc?.WaitForExit(60_000);
        rOut.Wait(3_000); rErr.Wait(1_000);
        var exit = proc?.ExitCode ?? 1;
        if (exit == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Error.WriteLine($"[SKILL] Regression PASS");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"[SKILL] Regression FAIL (exit={exit})");
            Console.ResetColor();
        }
        return exit == 0 ? 0 : 1;
    }
}
