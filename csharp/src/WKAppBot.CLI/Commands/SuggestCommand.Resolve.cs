// SuggestCommand.Resolve.cs -- suggest resolve + regression tests + recovery + AutoRegisterBug
// Split from SuggestCommand.cs for maintainability (~810 lines)

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>suggest resolve &lt;ts_prefix&gt; "note" -- mark done in JSONL + Slack reply.</summary>
    static int SuggestResolveCommand(string[] args)
    {
        if (args.Length < 1 || args[0] is "-h" or "--help")
        {
            Console.WriteLine("Usage: wkappbot suggest resolve <ts> \"note\"");
            Console.WriteLine("  ts  : ISO timestamp prefix (e.g. 2026-03-17T05) or full ts");
            Console.WriteLine("  note: resolution summary (English preferred)");
            return 0;
        }

        // -- Resolve guard: require confirmation flag + evidence script + skill --
        const string ConfirmFlag = "--i-completed-the-code-and-built-successfully-and-deployed-and-tested-with-real-scenarios-and-confirmed-meaningful-results-and-have-evidence-and-willkim-allowed-this-script";
        string? evidenceFile = null;
        string? skillId = null;
        string? commitHash = null;
        string? classNameArg = null;
        string? forceReason = null;
        bool hasConfirm = false;
        bool coResolveConfirmFlag = false; // --confirm: second-party 2-of-2 confirmation path
        for (int ei = 0; ei < args.Length; ei++)
        {
            if (args[ei] == ConfirmFlag && ei + 1 < args.Length)
            {
                hasConfirm = true;
                evidenceFile = args[ei + 1];
                continue;
            }
            if (args[ei] == "--skill" && ei + 1 < args.Length)
            {
                skillId = args[ei + 1];
                continue;
            }
            if (args[ei] == "--commit" && ei + 1 < args.Length)
            {
                commitHash = args[++ei];
                continue;
            }
            if (args[ei] == "--class" && ei + 1 < args.Length)
            {
                classNameArg = args[++ei];
                continue;
            }
            if (args[ei] == "--force" && ei + 1 < args.Length)
            {
                forceReason = args[++ei];
                continue;
            }
            if (args[ei] == "--confirm")
            {
                coResolveConfirmFlag = true;
                continue;
            }
        }

        if (!hasConfirm || evidenceFile == null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  RESOLVE GUARD -- no resolve without test evidence!");
            Console.WriteLine();
            Console.WriteLine("  Required: confirmation flag + test script/log file + skill id");
            Console.WriteLine("  The evidence file is uploaded to Slack as proof of testing.");
            Console.WriteLine("  The skill ensures the fix becomes reusable knowledge for future sessions.");
            Console.WriteLine();
            Console.WriteLine($"  wkappbot suggest resolve <ts> \"note\" {ConfirmFlag} test_result.sh --skill <id>");
            Console.WriteLine($"  Allowed evidence scripts: .sh, .ps1, .cmd");
            Console.ResetColor();
            return 1;
        }

        if (string.IsNullOrEmpty(skillId))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  RESOLVE GUARD -- --skill <id> is required (no bypass).");
            Console.WriteLine();
            Console.WriteLine("  Every resolved fix needs a skill entry so future AI/Claude sessions");
            Console.WriteLine("  can reuse the institutional memory. Even CHORE / informational items");
            Console.WriteLine("  should capture *why* they were closed. Create/update one via:");
            Console.WriteLine("    wkappbot skill contribute --app <app> --title ... --desc ... --steps ...");
            Console.WriteLine("    wkappbot skill edit <id> --step N --content ...");
            Console.ResetColor();
            return 1;
        }

        // -- Skill guard step 1 (exists + displayable via `skill read`) --
        // We intentionally validate against what `skill read` prints rather
        // than the raw JSON fields: that output IS the user contract. If
        // the schema grows or the display changes, validation follows.
        // Guard loads the record for recency (not displayed), then invokes
        // SkillShowCommand in-process with stdout captured so later steps
        // score the same text a human would see. No bypass -- every resolve
        // must carry a fresh, displayable, command-coupled skill.
        SkillRecord? guardSkill = null;
        string? guardSkillReadOutput = null;
        try
        {
            foreach (var dir in new[] { ProjectSkillsDir, Path.Combine(AppContext.BaseDirectory, "wkappbot.hq", "skills") })
            {
                if (!Directory.Exists(dir)) continue;
                foreach (var f in Directory.EnumerateFiles(dir, "*.skill.json", SearchOption.AllDirectories))
                {
                    var s = SkillRecord.Load(f);
                    if (s != null && s.Id.Equals(skillId, StringComparison.OrdinalIgnoreCase))
                    { guardSkill = s; break; }
                }
                if (guardSkill != null) break;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Skill guard -- load error: {ex.Message}");
            Console.ResetColor();
            return 1;
        }

        if (guardSkill == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Skill guard FAILED: skill id '{skillId}' not found in project or HQ skills.");
            Console.WriteLine($"  Create with: wkappbot skill contribute --app <app> --title ... --desc ... --id {skillId}");
            Console.ResetColor();
            return 1;
        }

        var skillAge = DateTime.UtcNow - guardSkill.LastActivity;
        if (skillAge.TotalDays > 7)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Skill guard FAILED: '{skillId}' last-activity is {(int)skillAge.TotalDays}d old (>7d).");
            Console.WriteLine($"  Update it to reflect this fix: wkappbot skill edit {skillId} --step N --content ... (or --add-step)");
            Console.ResetColor();
            return 1;
        }

        // Capture `skill read <id> --developer` output. --developer forces
        // all steps to render (project skills are already full-display,
        // but HQ skills from other projects would hide detail without it).
        // We drive the actual command so any future display-layer change
        // is reflected in validation automatically.
        {
            var prevOut = Console.Out;
            using var captured = new StringWriter();
            try
            {
                Console.SetOut(captured);
                var rc = SkillShowCommand(new[] { skillId, "--developer" });
                if (rc != 0)
                {
                    Console.SetOut(prevOut);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  Skill guard FAILED: `skill read {skillId}` returned exit={rc}.");
                    Console.ResetColor();
                    return 1;
                }
            }
            finally { Console.SetOut(prevOut); }
            guardSkillReadOutput = captured.ToString();
        }

        if (!guardSkillReadOutput.Contains($"ID     : {skillId}", StringComparison.OrdinalIgnoreCase))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Skill guard FAILED: `skill read` output missing canonical ID line for '{skillId}'.");
            Console.WriteLine($"  Output preview: {guardSkillReadOutput[..Math.Min(200, guardSkillReadOutput.Length)]}");
            Console.ResetColor();
            return 1;
        }

        Console.WriteLine($"[RESOLVE] Skill: {guardSkill.Id} (app={guardSkill.App}, v{guardSkill.Version ?? "?"}, updated {skillAge.TotalHours:F1}h ago, skill-read OK)");

        // Pre-load resolve target to check for merge record's affectedCommands
        string[]? mergeAffectedCmds = null;
        {
            var preArgs = args.Where(a => a != ConfirmFlag && a != evidenceFile).ToArray();
            var preTsPrefix = preArgs.Length > 0 ? preArgs[0] : "";
            var preJsonlPath = Path.Combine(Path.Combine(AppContext.BaseDirectory, "wkappbot.hq"), "suggestions.jsonl");
            if (!string.IsNullOrEmpty(preTsPrefix) && File.Exists(preJsonlPath))
            {
                foreach (var line in File.ReadAllLines(preJsonlPath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    try
                    {
                        var node = JsonSerializer.Deserialize<JsonNode>(line);
                        var entryTs = node?["ts"]?.GetValue<string>() ?? "";
                        var entrySlackTs = node?["slack_ts"]?.GetValue<string>() ?? "";
                        if (entryTs.StartsWith(preTsPrefix, StringComparison.OrdinalIgnoreCase) ||
                            entrySlackTs.StartsWith(preTsPrefix, StringComparison.OrdinalIgnoreCase))
                        {
                            // Check affectedCommands (merge record or explicit field override on any record)
                            {
                                var cmds = node?["affectedCommands"]?.AsArray()
                                    .Select(c => c?.GetValue<string>())
                                    .Where(c => !string.IsNullOrWhiteSpace(c))
                                    .ToArray();
                                if (cmds?.Length > 0)
                                {
                                    mergeAffectedCmds = cmds!;
                                    Console.WriteLine($"[RESOLVE] affectedCommands: [{string.Join(", ", mergeAffectedCmds)}] -- using for CMD guard");
                                }
                            }
                            // Non-merge: extract mentioned commands from suggest text
                            if (mergeAffectedCmds == null)
                            {
                                var sugText = node?["text"]?.GetValue<string>() ?? "";
                                // Word-boundary check: match "wkappbot {k}", "a11y {k}", "--{k}" only when
                                // the next char is non-alphanumeric (prevents "commands" matching key "com").
                                static bool HasCmdToken(string text, string prefix, string key)
                                {
                                    var pat = $"{prefix}{key}";
                                    int idx = text.IndexOf(pat, StringComparison.OrdinalIgnoreCase);
                                    if (idx < 0) return false;
                                    int end = idx + pat.Length;
                                    return end >= text.Length || !char.IsLetterOrDigit(text[end]);
                                }
                                var mentioned = CommandHelpMap.Keys
                                    .Where(k => HasCmdToken(sugText, "wkappbot ", k)
                                             || HasCmdToken(sugText, "a11y ", k)
                                             || HasCmdToken(sugText, "--", k))
                                    .Distinct().ToArray();
                                if (mentioned.Length > 0)
                                {
                                    mergeAffectedCmds = mentioned;
                                    Console.WriteLine($"[RESOLVE] Suggest mentions commands: [{string.Join(", ", mentioned)}] -- using for CMD guard");
                                }
                            }
                            break;
                        }
                    }
                    catch { }
                }
            }
        }

        // -- Skill guard step 2: content depth (parsed from `skill read`) --
        // Source of truth = the same text a human would see via
        // `wkappbot skill read <id> --developer`. Future display format
        // changes automatically flow into validation without touching this
        // code.
        if (guardSkill != null && guardSkillReadOutput != null)
        {
            var skillLines = guardSkillReadOutput.Split('\n').Select(l => l.TrimEnd('\r')).ToArray();
            // Desc line: starts with "  Desc   : " (note: App/ID/Tags follow
            // identical prefix; parse first Desc-prefixed line).
            string descText = "";
            foreach (var line in skillLines)
            {
                if (line.StartsWith("  Desc   : ", StringComparison.Ordinal))
                { descText = line.Substring("  Desc   : ".Length); break; }
            }
            if (descText.Length < 50)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  Skill guard FAILED: `skill read` shows desc = '{descText}' ({descText.Length} chars < 50).");
                Console.WriteLine($"  Extend via: wkappbot skill edit {guardSkill.Id} --desc \"<longer explanation>\"");
                Console.ResetColor();
                return 1;
            }

            // Steps: each step line matches "    {N}. {content}" (4 leading
            // spaces, a digit run, a dot). We count only lines that also
            // carry real content beyond the number.
            var stepRegex = new System.Text.RegularExpressions.Regex(@"^    \d+\. \S");
            int stepCount = skillLines.Count(l => stepRegex.IsMatch(l));
            if (stepCount < 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  Skill guard FAILED: `skill read` shows {stepCount} step(s) (<3). Need at least 3 concrete how-to steps.");
                Console.WriteLine($"  Add via: wkappbot skill edit {guardSkill.Id} --add-step \"<actionable instruction>\"");
                Console.ResetColor();
                return 1;
            }
            Console.WriteLine($"[RESOLVE] Skill display depth OK (desc={descText.Length} chars, {stepCount} steps)");
        }

        // -- Skill guard step 3: core-command coupling --
        // Prefer a matching skill id, but allow a bridge skill when the
        // rendered `skill read` body already shows a concrete command usage
        // (e.g. "wkappbot <cmd>" / "a11y <cmd>" / "--<cmd>"). This keeps
        // cross-cutting skills (Codex guidance that naturally belongs to
        // ask/skill/eye workflows) from being blocked by a too-narrow id
        // naming rule. The looser substring `exampleFound` check below is
        // a separate safety net -- both must still agree.
        if (guardSkill != null && guardSkillReadOutput != null && mergeAffectedCmds is { Length: > 0 })
        {
            var tokens = mergeAffectedCmds
                .SelectMany(c => c.Split(new[] { ' ', '-', '_', '/' }, StringSplitOptions.RemoveEmptyEntries))
                .Where(t => t.Length >= 2)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();
            bool idCovers = tokens.Any(tok => guardSkill.Id.Contains(tok, StringComparison.OrdinalIgnoreCase));
            bool bodyCovers = mergeAffectedCmds.Any(cmd =>
                !string.IsNullOrWhiteSpace(cmd)
                && (
                    guardSkillReadOutput.Contains($"wkappbot {cmd}", StringComparison.OrdinalIgnoreCase) ||
                    guardSkillReadOutput.Contains($"a11y {cmd}", StringComparison.OrdinalIgnoreCase) ||
                    guardSkillReadOutput.Contains($"--{cmd}", StringComparison.OrdinalIgnoreCase)
                ));
            if (!idCovers && !bodyCovers)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  Skill guard FAILED: skill id '{guardSkill.Id}' does not contain any core-command token from affectedCommands [{string.Join(", ", mergeAffectedCmds)}].");
                Console.WriteLine($"  Rename / re-create the skill, or add a concrete command usage to `skill read` output.");
                Console.WriteLine($"  Preferred tokens: {string.Join(", ", tokens)}");
                Console.ResetColor();
                return 1;
            }

            // Concrete-example check against the rendered output (not raw
            // fields): catches cases where desc+steps never mention the
            // command even if tags do.
            bool exampleFound = mergeAffectedCmds.Any(cmd =>
                !string.IsNullOrWhiteSpace(cmd)
                && guardSkillReadOutput.Contains(cmd, StringComparison.OrdinalIgnoreCase));
            if (!exampleFound)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  Skill guard FAILED: `skill read {guardSkill.Id}` never prints any affectedCommand phrase from [{string.Join(", ", mergeAffectedCmds)}].");
                Console.WriteLine($"  Add a concrete step referencing the command: wkappbot skill edit {guardSkill.Id} --add-step \"... {mergeAffectedCmds[0]} ...\"");
                Console.ResetColor();
                return 1;
            }

            Console.WriteLine($"[RESOLVE] Skill content OK (id covers, `skill read` body references affectedCommand)");
        }

        // -- Evidence file checks (deferred until after skill guard so a weak
        // skill short-circuits before the user burns time on evidence). --
        if (!File.Exists(evidenceFile))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Evidence file not found: {evidenceFile}");
            Console.ResetColor();
            return 1;
        }

        var evidenceExt = Path.GetExtension(evidenceFile).ToLowerInvariant();
        var allowedEvidenceExts = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".sh", ".ps1", ".cmd", ".bat"
        };
        if (!allowedEvidenceExts.Contains(evidenceExt))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  Unsupported evidence extension: {evidenceExt}");
            Console.WriteLine($"     Allowed: .sh, .ps1, .cmd");
            Console.ResetColor();
            return 1;
        }

        var evidenceName = Path.GetFileNameWithoutExtension(evidenceFile);
        var evidenceParts = evidenceName.Split('-');
        if (evidenceParts.Length < 3 || !evidenceParts[0].Equals("test", StringComparison.OrdinalIgnoreCase))
        {
            // Relaxed: warn instead of block. CMD guard already validates script content;
            // rigid filename pattern adds friction without safety benefit.
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  [WARN] Evidence filename doesn't follow test-{{cmd}}-{{subcmd}}-* convention.");
            Console.WriteLine($"     Got: {Path.GetFileName(evidenceFile)} (accepted, but consider renaming)");
            Console.ResetColor();
            // Normalize parts for experience DB path: use "misc" + stem if no proper structure
            evidenceParts = evidenceParts.Length >= 3 ? evidenceParts : new[] { "test", "misc", evidenceName };
        }

        Console.WriteLine($"[RESOLVE] Evidence: {evidenceFile} ({new FileInfo(evidenceFile).Length} bytes)");

        // Validate cmd/subcmd against known CLI commands
        {
            var cmdCheck = evidenceParts.Length > 1 ? evidenceParts[1] : "";
            if (!string.IsNullOrEmpty(cmdCheck) && !CommandHelpMap.ContainsKey(cmdCheck))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  [WARN] Unknown command \"{cmdCheck}\" in evidence filename. Known: {string.Join(", ", CommandHelpMap.Keys.Order())}");
                Console.ResetColor();
            }
        }

        // Show existing test scripts for this cmd/subcmd
        {
            var cmd1 = evidenceParts.Length > 1 ? evidenceParts[1] : "";
            var sub1 = evidenceParts.Length > 2 ? evidenceParts[2] : "";
            var existingDir = Path.Combine(DataDir, "experience", "tests", cmd1, sub1);
            if (Directory.Exists(existingDir))
            {
                var existing = Directory.GetFiles(existingDir).Select(Path.GetFileName).ToArray();
                if (existing.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"  Existing tests for {cmd1}/{sub1}: {string.Join(", ", existing)}");
                    Console.ResetColor();
                }
            }
        }

        // Verify script content: command-name coupling + option validation
        try
        {
            var cmd2 = evidenceParts.Length > 1 ? evidenceParts[1] : "";
            var subcmd2 = evidenceParts.Length > 2 ? evidenceParts[2] : "";
            var scriptContent = File.ReadAllText(evidenceFile);
            var expectedCmd = $"{cmd2} {subcmd2}".Trim();
            if (ShouldEnforceEvidenceCommandCoupling(cmd2, subcmd2)
                && !string.IsNullOrEmpty(expectedCmd)
                && !scriptContent.Contains(expectedCmd, StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  Script doesn't contain \"{expectedCmd}\" -- filename says test-{cmd2}-{subcmd2} but command not used!");
                Console.ResetColor();
                return 1;
            }

            // Option validation: filename segments after cmd-subcmd that look like options (e.g. "eval-js")
            // test-a11y-click-eval-js.sh -> must contain "--eval-js" in script
            for (int pi = 3; pi < evidenceParts.Length; pi++)
            {
                var seg = evidenceParts[pi];
                // Skip common non-option description words
                if (seg.Length < 2) continue;
                var optionFlag = $"--{seg}";
                if (scriptContent.Contains(optionFlag, StringComparison.OrdinalIgnoreCase))
                    continue; // option found in script -- OK
                // Check if it's a known CLI option (heuristic: contains hyphen = likely option)
                if (seg.Contains('-') || seg.All(c => char.IsLetter(c) || c == '-'))
                {
                    // Only warn, don't block -- description segments may not be options
                    if (seg.Contains('-')) // multi-word segments like "eval-js" are definitely options
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  [WARN] Filename mentions \"{seg}\" but \"{optionFlag}\" not found in evidence script");
                        Console.ResetColor();
                    }
                }
            }
        }
        catch { }

        // Duplicate guard: backup existing similar tests
        try
        {
            var cmd3 = evidenceParts.Length > 1 ? evidenceParts[1] : "misc";
            var sub3 = evidenceParts.Length > 2 ? evidenceParts[2] : "general";
            var existDir = Path.Combine(DataDir, "experience", "tests", cmd3, sub3);
            if (Directory.Exists(existDir))
            {
                var newLines = File.ReadAllLines(evidenceFile).Where(l => l.Trim().Length > 0).ToArray();
                foreach (var existing in Directory.GetFiles(existDir))
                {
                    var oldLines = File.ReadAllLines(existing).Where(l => l.Trim().Length > 0).ToArray();
                    if (oldLines.Length == 0 || newLines.Length == 0) continue;
                    int overlap = newLines.Count(nl => oldLines.Any(ol => ol.Trim() == nl.Trim()));
                    double ratio = (double)overlap / Math.Max(newLines.Length, oldLines.Length);
                    if (ratio > 0.5)
                    {
                        var bakTs = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                        var bakName = $"{Path.GetFileNameWithoutExtension(existing)}.bak-{bakTs}{Path.GetExtension(existing)}";
                        var bakPath = Path.Combine(existDir, bakName);
                        File.Copy(existing, bakPath);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  Evidence similar to existing {Path.GetFileName(existing)} ({ratio:P0} overlap)");
                        Console.WriteLine($"     Backed up: {bakName}");
                        Console.ResetColor();
                    }
                }
            }
        }
        catch { }

        // Run evidence script
        var ext = evidenceExt;
        var shell = ext switch
        {
            ".sh"  => "bash",
            ".ps1" => "powershell -NoProfile -ExecutionPolicy Bypass -File",
            ".bat" or ".cmd" => "cmd /c",
            ".py"  => "python",
            ".js"  => "node",
            _ => null
        };
        if (shell != null)
        {
            Console.WriteLine($"[RESOLVE] Running evidence script ({ext} -> {shell.Split(' ')[0]})...");
            try
            {
                var resolvedShell = shell;
                if (resolvedShell == "bash" && File.Exists(@"C:\Program Files\Git\usr\bin\bash.exe"))
                    resolvedShell = @"C:\Program Files\Git\usr\bin\bash.exe";
                // Don't split file paths -- ProcessStartInfo.FileName accepts paths with spaces directly.
                // Only split "verb args" forms like "bash -l" where the verb is not an existing path.
                var shellParts = resolvedShell.Contains(' ') && !resolvedShell.Contains('"') && !File.Exists(resolvedShell)
                    ? resolvedShell.Split(' ', 2)
                    : new[] { resolvedShell };
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = shellParts[0],
                    Arguments = (shellParts.Length > 1 ? shellParts[1] + " " : "") + $"\"{evidenceFile}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                };
                psi.EnvironmentVariables["WKAPPBOT_WORKER"] = "1";

                // CMD execution guard: capture OutputDebugString
                var dbgCapture = new System.Text.StringBuilder();
                var dbgListener = new DbgViewListener();
                dbgListener.MessageReceived += (pid, msg) => { lock (dbgCapture) dbgCapture.AppendLine(msg); };
                bool dbgStarted = dbgListener.TryStart();

                var proc = AppBotPipe.StartTracked(psi, psi.WorkingDirectory.Length > 0 ? psi.WorkingDirectory : Environment.CurrentDirectory, "SUGGEST");
                var stdoutTask = Task.Run(() => proc?.StandardOutput.ReadToEnd() ?? "");
                var stderrTask = Task.Run(() => proc?.StandardError.ReadToEnd() ?? "");
                proc?.WaitForExit(120_000);
                if (proc != null && !proc.HasExited) proc.Kill();
                var stdout = stdoutTask.GetAwaiter().GetResult();
                var stderr = stderrTask.GetAwaiter().GetResult();
                proc?.WaitForExit(); // drain async I/O before ExitCode (required by .NET)
                var exitCode = proc?.ExitCode ?? 1;

                dbgListener.Dispose();
                var capturedDbg = dbgCapture.ToString();

                Console.WriteLine(stdout);
                if (!string.IsNullOrWhiteSpace(stderr)) Console.WriteLine(stderr);

                if (exitCode != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  Evidence script FAILED (exit={exitCode}). Fix the test before resolving!");
                    Console.ResetColor();
                    BackupEvidenceOnFailure(evidenceFile, mergeAffectedCmds);
                    return 1;
                }

                var scriptBaseName = Path.GetFileNameWithoutExtension(evidenceFile);
                var scriptParts = scriptBaseName.Split('-');

                // CMD pattern: merge affectedCommands take priority over filename convention
                string? expectedCmdPattern = null;
                if (mergeAffectedCmds != null && mergeAffectedCmds.Length > 0)
                {
                    // Use merge's affectedCommands -- any one matching is sufficient
                    expectedCmdPattern = string.Join("|", mergeAffectedCmds);
                }
                else if (scriptParts.Length >= 3 && ShouldEnforceEvidenceCommandCoupling(scriptParts[1], scriptParts[2]))
                {
                    expectedCmdPattern = $"{scriptParts[1]}-{scriptParts[2]}";
                }

                var dbgLines = capturedDbg.Split('\n');
                var cmdCount = dbgLines.Count(l => l.Contains("[CMD]"));
                bool cmdPatternHit;
                if (expectedCmdPattern == null)
                    cmdPatternHit = true;
                else if (mergeAffectedCmds != null)
                {
                    // "all" = any command counts; otherwise any affectedCommand must appear in [CMD] lines
                    bool isAllCommands = mergeAffectedCmds.Any(cmd => cmd.Equals("all", StringComparison.OrdinalIgnoreCase));
                    cmdPatternHit = isAllCommands
                        ? cmdCount > 0
                        : mergeAffectedCmds.Any(cmd =>
                            dbgLines.Any(l => l.Contains($"cmd={cmd}") || l.Contains($"-{cmd}>")
                                          || l.Contains($"[CMD] {cmd} ") || l.Contains($"[CMD] {cmd}\t")));
                }
                else
                    cmdPatternHit = dbgLines.Any(l => l.Contains($"-{expectedCmdPattern}>"));

                if (!dbgStarted)
                    Console.WriteLine("  [RESOLVE] CMD guard: dbg listener unavailable -- skipped");
                else if (cmdCount == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  CMD execution guard FAILED: no [CMD] entries in debug output.");
                    Console.WriteLine("  -> evidence 스크립트 안에서 핵심 wkappbot 명령을 실제로 실행해야 합니다.");
                    Console.WriteLine("  -> 예: WKAPPBOT_WORKER=1 timeout 8 D:/SDK/bin/wkappbot.exe a11y click \"*App*\" --eval-js \"...\"");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("  핵심 명령줄이 무엇인지 evidence 파일명 또는 스크립트에 포함시키세요:");
                    Console.WriteLine($"  파일명: test-{{cmd}}-{{subcmd}}-{{option}}.sh  (예: test-a11y-click-eval-js.sh)");
                    Console.WriteLine($"  스크립트: wkappbot {{cmd}} {{subcmd}} ... --{{option}} 실제 실행");
                    Console.ResetColor();
                    return 1;
                }
                else if (!cmdPatternHit)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (mergeAffectedCmds != null)
                    {
                        Console.WriteLine($"  CMD execution guard FAILED: none of merge affectedCommands [{string.Join(", ", mergeAffectedCmds)}] found in debug output.");
                        Console.WriteLine($"  -> evidence에서 [{string.Join(" 또는 ", mergeAffectedCmds)}] 명령을 실행하세요.");
                    }
                    else
                    {
                        Console.WriteLine($"  CMD execution guard FAILED: expected '{expectedCmdPattern}' command not found.");
                        Console.WriteLine($"  -> evidence에서 'wkappbot {expectedCmdPattern} ...' 명령을 실행하세요.");
                    }
                    Console.ResetColor();
                    return 1;
                }
                else
                {
                    var guardSource = mergeAffectedCmds != null ? "merge affectedCommands" : "filename";
                    Console.WriteLine($"  [RESOLVE] CMD execution verified: {cmdCount} command(s) captured, pattern '{expectedCmdPattern}' confirmed ({guardSource})");

                    // Validate captured commands are registered in CommandHelpMap (구라 검출 + help 강제)
                    foreach (var dbgLine in dbgLines.Where(l => l.Contains("[CMD]")))
                    {
                        var cmdIdx = dbgLine.IndexOf("cmd=", StringComparison.Ordinal);
                        if (cmdIdx < 0) continue;
                        var cmdRest = dbgLine[(cmdIdx + 4)..].Trim();
                        var cmdWord = cmdRest.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? "";
                        if (!string.IsNullOrEmpty(cmdWord) && !CommandHelpMap.ContainsKey(cmdWord))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"  RESOLVE BLOCKED: command \"{cmdWord}\" is not in CommandHelpMap.");
                            Console.WriteLine($"  -> CommandHelp.cs의 CommandHelpMap에 \"{cmdWord}\" help 텍스트를 먼저 추가하세요.");
                            Console.WriteLine($"  -> 그래야 --help 자동 지원 + regression 테스트 카테고리가 올바르게 동작합니다.");
                            Console.ResetColor();
                            return 1;
                        }
                        // Help quality check: too short = probably placeholder
                        if (!string.IsNullOrEmpty(cmdWord) && CommandHelpMap.TryGetValue(cmdWord, out var helpText))
                        {
                            var helpLen = helpText.Trim().Length;
                            if (helpLen < 30)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"  [WARN] \"{cmdWord}\" help is too short ({helpLen} chars) -- consider adding usage examples");
                                Console.ResetColor();
                            }
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  Evidence script PASSED (exit=0)");
                Console.ResetColor();
                // Suggest registering as skill regression script
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"  [TIP] To link as skill regression: wkappbot skill contribute --id <skill-id> --regression-script \"{Path.GetFullPath(evidenceFile)}\"");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  Evidence script execution error: {ex.Message}");
                Console.ResetColor();
                return 1;
            }
        }
        else
            Console.WriteLine($"[RESOLVE] Non-executable evidence ({ext}) -- skip execution, upload only");

        // Regression test (merge affectedCommands determines test folder)
        if (RunRegressionTests(evidenceFile, mergeAffectedCmds) != 0) return 1;

        // Strip flags + their values so positional args (ts, note) line up cleanly.
        // Filtered: ConfirmFlag + its value, --skill/--commit/--class/--force + values, --confirm.
        {
            var keep = new List<string>(args.Length);
            for (int fi = 0; fi < args.Length; fi++)
            {
                var a = args[fi];
                if (a == ConfirmFlag || a == evidenceFile) continue;
                if (a == "--skill"  && fi + 1 < args.Length) { fi++; continue; }
                if (a == "--commit" && fi + 1 < args.Length) { fi++; continue; }
                if (a == "--class"  && fi + 1 < args.Length) { fi++; continue; }
                if (a == "--force"  && fi + 1 < args.Length) { fi++; continue; }
                if (a == "--confirm") continue;
                keep.Add(a);
            }
            args = keep.ToArray();
        }

        var tsPrefix = args[0];
        var note = args.Length >= 2 ? string.Join(" ", args[1..]) : "resolved";

        var hqDir = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq");
        var jsonlPath = Path.Combine(hqDir, "suggestions.jsonl");

        // Git root anchored to the WKAppBot repo (walked up from hqDir).
        // Used by commit guard + source size guard so git commands run in the right repo
        // regardless of launcher CWD (which reflects the foreground window's workspace).
        static string FindGitRoot(string startDir)
        {
            var dir = startDir;
            while (!string.IsNullOrEmpty(dir))
            {
                if (Directory.Exists(Path.Combine(dir, ".git"))) return dir;
                dir = Path.GetDirectoryName(dir) ?? "";
            }
            return startDir;
        }
        var resolverGitRoot = FindGitRoot(hqDir);
        // suggesterGitRoot: resolved after suggest entry is loaded (from entry's cwd field).
        // Allows commit guard to also search the suggester's repo (e.g. WkAutoQuant commits).
        string? _suggesterGitRootLazy = null;
        var historyPath = Path.Combine(hqDir, "suggestions_history.jsonl");

        if (!File.Exists(jsonlPath))
        {
            Console.WriteLine("[RESOLVE] suggestions.jsonl not found");
            return 1;
        }

        var lines = File.ReadAllLines(jsonlPath).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        int matchIdx = -1;
        JsonNode? matchEntry = null;

        for (int i = 0; i < lines.Count; i++)
        {
            try
            {
                var node = JsonSerializer.Deserialize<JsonNode>(lines[i]);
                var entryTs = node?["ts"]?.GetValue<string>() ?? "";
                var entrySlackTs = node?["slack_ts"]?.GetValue<string>() ?? "";
                if (entryTs.StartsWith(tsPrefix, StringComparison.OrdinalIgnoreCase) ||
                    entrySlackTs.StartsWith(tsPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    matchIdx = i;
                    matchEntry = node;
                    break;
                }
            }
            catch { }
        }

        if (matchIdx < 0 || matchEntry == null)
        {
            Console.WriteLine($"[RESOLVE] No suggestion found with ts prefix: {tsPrefix}");
            return 1;
        }

        var entryText = matchEntry["text"]?.GetValue<string>() ?? "";
        var slackTs = matchEntry["slack_ts"]?.GetValue<string>();
        // Resolve suggester's git root now that we have the entry cwd.
        var _entryCwd = matchEntry["cwd"]?.GetValue<string>() ?? "";
        _suggesterGitRootLazy = !string.IsNullOrEmpty(_entryCwd) ? FindGitRoot(_entryCwd) : resolverGitRoot;
        var from = matchEntry["from"]?.GetValue<string>() ?? "unknown";
        var _previewLines = entryText.Split('\n');
        var preview = _previewLines[0].Trim();
        if (preview.Equals("--title", StringComparison.OrdinalIgnoreCase) && _previewLines.Length > 1)
            preview = _previewLines[1].Trim();
        if (preview.Length > 80) preview = preview[..80] + "...";
        Console.WriteLine($"[RESOLVE] Found: [{from}] {preview}");

        // Read requirements from the suggest entry (must have >= 3).
        var requirements = new List<SkillRequirement>();
        var reqArr = matchEntry["requirements"]?.AsArray();
        if (reqArr != null)
        {
            foreach (var r in reqArr)
            {
                var raw = r?.GetValue<string>() ?? "";
                var sep = raw.IndexOf(" => ", StringComparison.Ordinal);
                if (sep > 0)
                    requirements.Add(new SkillRequirement { Cmd = raw[..sep].Trim(), Expect = raw[(sep + 4)..].Trim() });
            }
        }
        bool isAutoSuggest = entryText.StartsWith("[BUG-AUTO]", StringComparison.OrdinalIgnoreCase)
            || entryText.StartsWith("[CDP-FALLBACK]", StringComparison.OrdinalIgnoreCase)
            || entryText.StartsWith("[AI-NEWS]", StringComparison.OrdinalIgnoreCase)
            || entryText.StartsWith("[HN]", StringComparison.OrdinalIgnoreCase)
            || entryText.StartsWith("[YT:", StringComparison.OrdinalIgnoreCase);
        if (!isAutoSuggest && requirements.Count < 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[RESOLVE] FAILED: suggest has only {requirements.Count} requirement(s) — need >= 3.");
            Console.WriteLine("  Re-file the suggest with at least 3 --requirement flags:");
            Console.WriteLine("    wkappbot suggest \"title\" --requirement \"wkappbot <cmd> => <expected>\" ...");
            Console.ResetColor();
            return 1;
        }

        // -- 2-of-2 Co-Resolve gate ----------------------------------------------
        // Skill + evidence + regression + requirements all passed. Now check the
        // cross-author guard: a different party than the original author must
        // either (a) write the 1/2 half-resolve and stop, or (b) complete the
        // 2/2 if the first half is already on file. See SuggestCommand.CoResolve.cs.
        var coResolveOutcome = CheckCoResolveStatus(
            jsonlPath, lines, matchIdx, matchEntry, tsPrefix, note,
            skillId, forceReason, coResolveConfirmFlag, slackTs);
        if (coResolveOutcome == null) return 1; // hard error logged by helper
        if (coResolveOutcome.Decision == CoResolveDecision.Skip)
            return 0;
        if (coResolveOutcome.Decision == CoResolveDecision.WroteHalf)
        {
            // 1/2 written + Slack notice posted by helper. Do NOT move to history.
            Console.WriteLine($"[RESOLVE:HALF] suggest stays in suggestions.jsonl until {from} confirms with --confirm");
            return 0;
        }
        // Decision is Proceed / ForceProceed / CompleteSecondHalf -- fall through.

        // -- Commit guard: (1) keyword match  (2) time guard  (3) file coupling --
        if (!string.IsNullOrEmpty(commitHash))
        {
            string RunGitIn(string gitArgs, string workDir)
            {
                var psi = new System.Diagnostics.ProcessStartInfo("git", gitArgs)
                {
                    UseShellExecute = false, RedirectStandardOutput = true,
                    RedirectStandardError = true, CreateNoWindow = true,
                    WorkingDirectory = workDir,
                };
                var p = System.Diagnostics.Process.Start(psi)!;
                var output = p.StandardOutput.ReadToEnd();
                p.WaitForExit(10_000);
                return p.ExitCode == 0 ? output : "";
            }
            // Run git in resolver repo first; fall back to suggester repo (cross-repo commits)
            string RunGit(string gitArgs)
            {
                var r = RunGitIn(gitArgs, resolverGitRoot);
                if (!string.IsNullOrWhiteSpace(r)) return r;
                var sRoot = _suggesterGitRootLazy ?? resolverGitRoot;
                if (!string.Equals(sRoot, resolverGitRoot, StringComparison.OrdinalIgnoreCase))
                    return RunGitIn(gitArgs, sRoot);
                return r;
            }

            var shortHash = commitHash[..Math.Min(8, commitHash.Length)];

            // Fetch message + timestamp in one log call: "%cI|||%B"
            var logRaw = RunGit($"log -1 --format=%cI|||%B {commitHash}");
            if (string.IsNullOrWhiteSpace(logRaw))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[RESOLVE] Commit guard FAILED: `git log {shortHash}` returned nothing.");
                var _sr = _suggesterGitRootLazy ?? resolverGitRoot;
                Console.WriteLine($"  Checked: {resolverGitRoot}" + (_sr != resolverGitRoot ? $" + {_sr}" : ""));
                Console.ResetColor();
                return 1;
            }
            var sepIdx = logRaw.IndexOf("|||", StringComparison.Ordinal);
            var commitTimeStr = sepIdx > 0 ? logRaw[..sepIdx].Trim() : "";
            var commitMsg     = sepIdx > 0 ? logRaw[(sepIdx + 3)..].Trim() : logRaw.Trim();
            // Extend keyword search to full diff (BUG-AUTO titles may have keywords only in code comments)
            var commitDiff = RunGit($"show {commitHash}");
            var commitFull = commitMsg + "\n" + commitDiff;

            // (1) Keyword match: 90% of suggest title words must appear in commit message or diff
            // Skip keyword guard when --force is set (e.g. AI-NEWS informational evaluations)
            bool keywordGuardEnabled = string.IsNullOrEmpty(forceReason);
            var stopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "a","an","the","and","or","but","for","nor","so","yet","in","on","at",
                "to","of","by","as","is","it","its","this","that","with","from","be",
                "was","are","has","have","not","when","via","also","both","into",
                "wkappbot","suggest","feature","bug","fix","feat","chore","docs","test","refactor",
            };
            // Suggests filed with --title flag have format "--title\nActual Title\n--desc\n..."
            // Skip the "--title" marker line and use the next non-flag line as the title.
            var titleLines = entryText.Split('\n');
            var titleLine = titleLines[0].Trim();
            if (titleLine.Equals("--title", StringComparison.OrdinalIgnoreCase) && titleLines.Length > 1)
                titleLine = titleLines[1].Trim();
            var titleWords = System.Text.RegularExpressions.Regex
                .Split(titleLine, @"[^A-Za-z0-9]+")
                .Where(w => w.Length >= 4 && !stopWords.Contains(w)
                    && !System.Text.RegularExpressions.Regex.IsMatch(w, @"^[0-9A-Fa-f]{4,}$") // skip hex addresses (hwnd, pid)
                    && !w.StartsWith("0x", StringComparison.OrdinalIgnoreCase))                // skip 0xHEX runtime values
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();
            var missing = titleWords.Where(w => !commitFull.Contains(w, StringComparison.OrdinalIgnoreCase)).ToArray();
            if (keywordGuardEnabled && titleWords.Length > 0 && (double)missing.Length / titleWords.Length > 0.1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[RESOLVE] Commit guard FAILED (keyword): {missing.Length}/{titleWords.Length} title keywords missing from commit {shortHash}");
                Console.WriteLine($"  Missing: {string.Join(", ", missing)}");
                Console.WriteLine($"  Commit : {commitMsg.Split('\n')[0]}");
                Console.ResetColor();
                return 1;
            }
            if (keywordGuardEnabled)
                Console.WriteLine($"[RESOLVE] Commit keyword OK ({shortHash}): [{string.Join(", ", titleWords)}]");
            else
                Console.WriteLine($"[RESOLVE] Commit keyword skipped (--force): {shortHash}");

            // (2) Time guard: commit must be newer than the suggest
            var suggestTimeStr = matchEntry["ts"]?.GetValue<string>() ?? "";
            if (!string.IsNullOrEmpty(commitTimeStr) && !string.IsNullOrEmpty(suggestTimeStr)
                && DateTime.TryParse(commitTimeStr, null, System.Globalization.DateTimeStyles.RoundtripKind, out var commitTime)
                && DateTime.TryParse(suggestTimeStr, null, System.Globalization.DateTimeStyles.RoundtripKind, out var suggestTime))
            {
                if (commitTime < suggestTime)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[RESOLVE] Commit guard FAILED (time): commit {commitTime:u} predates suggest {suggestTime:u}");
                    Console.WriteLine($"  A commit cannot fix a bug that didn't exist yet. Use the correct commit hash.");
                    Console.ResetColor();
                    return 1;
                }
                Console.WriteLine($"[RESOLVE] Commit time OK: {commitTime:u} > suggest {suggestTime:u}");
            }

            // (3) File coupling: PascalCase class names / *.cs filenames from suggest must appear in git show --stat
            if (!string.IsNullOrEmpty(forceReason)) goto SkipCouplingGuard;
            var showStat = RunGit($"show --stat {commitHash}");
            var classPattern = new System.Text.RegularExpressions.Regex(@"\b([A-Z][a-zA-Z]{4,})\b");
            var filePattern  = new System.Text.RegularExpressions.Regex(@"\b(\w+\.(cs|json|sh|yaml|yml|ps1))\b");
            var couplingCandidates = classPattern.Matches(titleLine)
                .Concat(filePattern.Matches(entryText))
                .Select(m => m.Groups[1].Value)
                .Where(w => !stopWords.Contains(w))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();
            if (couplingCandidates.Length > 0 && !string.IsNullOrWhiteSpace(showStat))
            {
                var coupled = couplingCandidates.Where(c => showStat.Contains(c, StringComparison.OrdinalIgnoreCase)).ToArray();
                if (coupled.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[RESOLVE] Commit guard FAILED (file coupling): none of [{string.Join(", ", couplingCandidates)}] appear in changed files of {shortHash}");
                    Console.WriteLine($"  Changed: {string.Join(", ", showStat.Split('\n').Where(l => l.TrimStart().StartsWith(" ")).Select(l => l.Trim().Split(' ')[0]).Take(5))}");
                    Console.ResetColor();
                    return 1;
                }
                Console.WriteLine($"[RESOLVE] File coupling OK: [{string.Join(", ", coupled)}] found in commit diff");
            }
            SkipCouplingGuard:;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[RESOLVE] FAILED: --commit <hash> is required.");
            Console.WriteLine("  Provide the git commit hash that fixes this suggest.");
            Console.WriteLine("  Example: --commit $(git rev-parse HEAD)");
            Console.ResetColor();
            return 1;
        }

        // -- Source size guard: block resolve if core class file exceeds 400-line soft cap --
        // Checks --class <ClassName> explicitly + all .cs files touched by --commit.
        {
            const int LineCap = 400;
            var filesToCheck = new List<(string label, string path)>();

            if (string.IsNullOrEmpty(classNameArg))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[RESOLVE] FAILED: --class <ClassName> is required.");
                Console.WriteLine("  Provide the core class you modified. This enforces the 400-line source size rule.");
                Console.WriteLine("  For skill-only / doc-only commits with no .cs changes use: --class skill-only");
                Console.ResetColor();
                return 1;
            }

            // skill-only / doc-only commit: --class skill-only bypasses git grep (no .cs changed)
            bool skillOnlyBypass = string.Equals(classNameArg, "skill-only", StringComparison.OrdinalIgnoreCase);

            if (!skillOnlyBypass)
            {
                var psiG = new System.Diagnostics.ProcessStartInfo("git", $"grep -l \"class {classNameArg}\"")
                { UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true,
                  WorkingDirectory = resolverGitRoot };
                var pg = System.Diagnostics.Process.Start(psiG)!;
                var grepOut = pg.StandardOutput.ReadToEnd();
                pg.WaitForExit(10_000);
                foreach (var line in grepOut.Split('\n'))
                {
                    var rel = line.Trim().Replace('/', Path.DirectorySeparatorChar);
                    var abs = Path.IsPathRooted(rel) ? rel : Path.Combine(resolverGitRoot, rel);
                    if (rel.EndsWith(".cs", StringComparison.OrdinalIgnoreCase) && File.Exists(abs))
                        filesToCheck.Add(($"--class {classNameArg}", abs));
                }
                if (!filesToCheck.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[RESOLVE] Source size guard FAILED: --class {classNameArg} found no .cs file (class not committed or typo?)");
                    Console.WriteLine("  If this commit touches no .cs files (skill/doc only), use: --class skill-only");
                    Console.ResetColor();
                    return 1;
                }
            }
            else
            {
                Console.WriteLine($"[RESOLVE] Source size guard: skill-only bypass (no .cs changes attested)");
            }

            if (!string.IsNullOrEmpty(commitHash))
            {
                var psiD = new System.Diagnostics.ProcessStartInfo("git", $"diff-tree --no-commit-id -r --name-only {commitHash}")
                { UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true,
                  WorkingDirectory = resolverGitRoot };
                var pd = System.Diagnostics.Process.Start(psiD)!;
                var diffOut = pd.StandardOutput.ReadToEnd();
                pd.WaitForExit(10_000);
                foreach (var line in diffOut.Split('\n'))
                {
                    var rel = line.Trim().Replace('/', Path.DirectorySeparatorChar);
                    var abs = Path.IsPathRooted(rel) ? rel : Path.Combine(resolverGitRoot, rel);
                    if (!rel.EndsWith(".cs", StringComparison.OrdinalIgnoreCase) || !File.Exists(abs)) continue;
                    if (!filesToCheck.Any(f => string.Equals(f.path, abs, StringComparison.OrdinalIgnoreCase)))
                        filesToCheck.Add(("commit", abs));
                }
            }

            if (filesToCheck.Count > 0)
            {
                var sized = filesToCheck
                    .Select(f => (f.label, f.path, lines: File.ReadAllLines(f.path).Length))
                    .ToArray();
                // Only the explicitly specified --class file is a hard failure.
                // Other commit-touched files that were already oversized are warnings --
                // requiring a split of every pre-existing large file in a batch commit
                // would make the guard unusable in practice.
                var classOversized = sized
                    .Where(f => f.label.StartsWith("--class") && f.lines > LineCap)
                    .ToArray();
                var commitOversized = sized
                    .Where(f => f.label == "commit" && f.lines > LineCap)
                    .ToArray();
                if (classOversized.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[RESOLVE] Source size guard FAILED: --class file exceeds {LineCap}-line cap:");
                    foreach (var (lbl, fpath, lcount) in classOversized)
                        Console.WriteLine($"  {Path.GetFileName(fpath)} = {lcount} lines (+{lcount - LineCap} over)");
                    Console.WriteLine($"  Split before resolving:");
                    Console.WriteLine($"    wkappbot ask codex --agent \"split {classOversized[0].path} into partial classes under {LineCap} lines\"");
                    Console.ResetColor();
                    return 1;
                }
                if (commitOversized.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"[RESOLVE] Source size WARNING: {commitOversized.Length} commit file(s) exceed {LineCap}-line cap (pre-existing, not blocking):");
                    foreach (var (lbl, fpath, lcount) in commitOversized)
                        Console.WriteLine($"  {Path.GetFileName(fpath)} = {lcount} lines (+{lcount - LineCap} over) -- schedule split");
                    Console.ResetColor();
                }
                Console.WriteLine($"[RESOLVE] Source size OK: {filesToCheck.Count} file(s) checked");
            }
        }

        var resolverChannelForHistory = AbbreviateCwd(Environment.CurrentDirectory);
        if (string.IsNullOrEmpty(resolverChannelForHistory)) resolverChannelForHistory = "unknown";
        var resolvedObj = new Dictionary<string, object?>
        {
            ["ts"] = matchEntry["ts"]?.GetValue<string>(),
            ["from"] = from,
            ["cwd"] = matchEntry["cwd"]?.GetValue<string>(),
            ["text"] = entryText,
            ["files"] = matchEntry["files"]?.AsArray().Select(f => f?.GetValue<string>()).ToArray(),
            ["slack_ts"] = slackTs,
            ["review_status"] = "done",
            ["review_note"] = note,
            ["review_ts"] = DateTime.UtcNow.ToString("o"),
            ["review_by"] = resolverChannelForHistory,
            ["evidence_file"] = Path.GetFileName(evidenceFile),
            // Co-resolve attestation: when the resolve completed via 2-of-2,
            // record the first half (different party + their note + ts) so
            // the audit trail in suggestions_history.jsonl shows both signers.
            ["co_resolve"] = coResolveOutcome.Decision == CoResolveDecision.CompleteSecondHalf
                ? new Dictionary<string, object?>
                {
                    ["mode"] = "2of2",
                    ["first_half_by"]   = coResolveOutcome.FirstHalfBy,
                    ["first_half_note"] = coResolveOutcome.FirstHalfNote,
                    ["first_half_at"]   = coResolveOutcome.FirstHalfAt,
                    ["second_half_by"]  = resolverChannelForHistory,
                    ["second_half_note"] = note,
                }
                : coResolveOutcome.Decision == CoResolveDecision.ForceProceed
                ? new Dictionary<string, object?>
                {
                    ["mode"] = "force",
                    ["force_by"]     = resolverChannelForHistory,
                    ["force_reason"] = coResolveOutcome.ForceReason,
                }
                : null,
        };
        var resolvedJson = JsonSerializer.Serialize(resolvedObj);

        // Safeguard 1: pre-op snapshot of suggestions.jsonl before destructive write.
        BackupSuggestFile("resolve");

        File.AppendAllText(historyPath, resolvedJson + Environment.NewLine);
        lines.RemoveAt(matchIdx);
        File.WriteAllLines(jsonlPath, lines);
        Console.WriteLine($"[RESOLVE] Moved to history: {note}");

        // Auto-commit: ts prefix capped at 10 chars per spec (e.g. "2026-04-26").
        var resolvedTsRaw = matchEntry["ts"]?.GetValue<string>() ?? tsPrefix;
        var resolvedTsShort = resolvedTsRaw.Length >= 10 ? resolvedTsRaw[..10] : resolvedTsRaw;
        try { GitCommitSuggestFiles("resolve", resolvedTsShort); }
        catch (InvalidOperationException) { return 1; }

        // Copy evidence to experience DB (always filename-based path for consistency with help examples)
        try
        {
            var parts = Path.GetFileNameWithoutExtension(evidenceFile).Split('-');
            var cmd = parts.Length > 1 ? parts[1] : "misc";
            var subcmd = parts.Length > 2 ? parts[2] : "general";
            var testsDir = Path.Combine(hqDir, "experience", "tests", cmd, subcmd);
            Directory.CreateDirectory(testsDir);
            var destName = Path.GetFileName(evidenceFile);
            var destPath = Path.Combine(testsDir, destName);
            if (File.Exists(destPath))
            {
                var existingHash = Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(File.ReadAllBytes(destPath)))[..8];
                var newHash = Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(File.ReadAllBytes(evidenceFile)))[..8];
                if (existingHash != newHash)
                {
                    var ts = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                    var destExt2 = Path.GetExtension(destName);
                    destName = $"{Path.GetFileNameWithoutExtension(destName)}-{ts}{destExt2}";
                    destPath = Path.Combine(testsDir, destName);
                }
            }
            File.Copy(evidenceFile, destPath, overwrite: true);
            Console.WriteLine($"[RESOLVE] Evidence -> experience/tests/{cmd}/{subcmd}/{destName}");

            var manifestPath2 = Path.Combine(testsDir, ".manifest");
            var realName = Path.GetFileName(evidenceFile);
            var manifestLines = File.Exists(manifestPath2) ? File.ReadAllLines(manifestPath2).ToList() : new List<string>();
            if (!manifestLines.Any(l => l.Trim() == realName))
            {
                manifestLines.Add(realName);
                File.WriteAllLines(manifestPath2, manifestLines);
            }
        }
        catch (Exception ex) { Console.WriteLine($"[RESOLVE] Evidence copy failed: {ex.Message}"); }

        // -- Auto-append evidence path to the skill as a new step --
        // Skills accumulate verification trails this way: each resolve that
        // targeted them adds a "Verified <timestamp>: <relative evidence path>"
        // line. Future readers see at a glance which test scripts exercised
        // the fix.
        try
        {
            if (guardSkill != null && !string.IsNullOrEmpty(guardSkill.FilePath) && File.Exists(guardSkill.FilePath))
            {
                var relEvidence = Path.GetFileName(evidenceFile);
                try
                {
                    var evidenceAbs = Path.GetFullPath(evidenceFile);
                    var hqAbs = Path.GetFullPath(hqDir);
                    if (evidenceAbs.StartsWith(hqAbs, StringComparison.OrdinalIgnoreCase))
                        relEvidence = Path.GetRelativePath(hqAbs, evidenceAbs).Replace('\\', '/');
                }
                catch { }

                var verifiedStep = $"Verified {DateTime.UtcNow:yyyy-MM-dd}: {relEvidence} (resolve ts={tsPrefix})";
                var fresh = SkillRecord.Load(guardSkill.FilePath);
                if (fresh != null)
                {
                    fresh.Steps ??= new List<string>();
                    // De-dupe by exact evidence filename (same test run again shouldn't double-log)
                    bool already = fresh.Steps.Any(s =>
                        s.Contains("Verified", StringComparison.OrdinalIgnoreCase)
                        && s.Contains(relEvidence, StringComparison.OrdinalIgnoreCase));
                    if (!already)
                    {
                        fresh.Steps.Add(verifiedStep);
                        fresh.Updated = DateTime.UtcNow;
                        fresh.Version = BumpVersion(fresh.Version);
                        fresh.Save(guardSkill.FilePath);
                        Console.WriteLine($"[RESOLVE] Skill step appended: {guardSkill.Id} +-> \"{verifiedStep}\"");
                    }
                    else
                    {
                        Console.WriteLine($"[RESOLVE] Skill step skipped (evidence already logged): {relEvidence}");
                    }

                    // Force-write requirements from the suggest entry into the skill.
                    // Also promotes the first requirement's cmd as PrimaryCmd on the skill.
                    if (requirements.Count > 0)
                    {
                        fresh = SkillRecord.Load(guardSkill.FilePath)!;
                        fresh.Requirements ??= new List<SkillRequirement>();
                        var resolvePrompt = note;
                        bool reqChanged = false;
                        foreach (var req in requirements)
                        {
                            req.Prompt ??= resolvePrompt;
                            bool dup = fresh.Requirements.Any(r =>
                                string.Equals(r.Cmd, req.Cmd, StringComparison.OrdinalIgnoreCase));
                            if (!dup) { fresh.Requirements.Add(req); reqChanged = true; }
                        }
                        // First requirement becomes the skill's primary command line.
                        if (string.IsNullOrEmpty(fresh.PrimaryCmd))
                        {
                            fresh.PrimaryCmd = requirements[0].Cmd;
                            reqChanged = true;
                        }
                        if (reqChanged)
                        {
                            fresh.Updated = DateTime.UtcNow;
                            fresh.Version = BumpVersion(fresh.Version);
                            fresh.Save(guardSkill.FilePath);
                            Console.WriteLine($"[RESOLVE] Skill requirements written: {requirements.Count} contract(s) -> {guardSkill.Id}");
                            Console.WriteLine($"[RESOLVE] PrimaryCmd: {fresh.PrimaryCmd}");
                            foreach (var req in requirements)
                                Console.WriteLine($"  $ {req.Cmd}");
                        }
                    }
                }
            }
        }
        catch (Exception ex) { Console.WriteLine($"[RESOLVE] Skill step append failed: {ex.Message}"); }

        try
        {
            var recoveryZip = CreateResolveRecoveryZip(hqDir, tsPrefix, evidenceFile);
            if (!string.IsNullOrWhiteSpace(recoveryZip))
                Console.WriteLine($"[RESOLVE] Recovery ZIP: {recoveryZip}");
        }
        catch (Exception ex) { Console.WriteLine($"[RESOLVE] Recovery ZIP failed: {ex.Message}"); }

        // Slack reply -- route to SUGGESTER's channel (the thread lives there),
        // not the resolver's. Routing key = suggest entry's `cwd` (the exact
        // project root where the suggest was filed). See SuggestCommand.SlackRouting.cs.
        if (!string.IsNullOrEmpty(slackTs))
        {
            var suggestCwd = matchEntry["cwd"]?.GetValue<string>() ?? "";
            var slackChannelId = matchEntry["slack_channel"]?.GetValue<string>() ?? "";
            var resolveCfg = LoadSlackConfig();
            var (botToken, channel) = ResolveReplyTarget(suggestCwd, slackChannelId, resolveCfg, "[RESOLVE:SLACK]");
            if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(channel))
            {
                var replyText = $":white_check_mark: *RESOLVED* -- {note}\n:page_facing_up: Evidence: `{Path.GetFileName(evidenceFile)}`";
                if (coResolveOutcome.Decision == CoResolveDecision.CompleteSecondHalf)
                {
                    replyText = BuildCoResolveDoneMessage(coResolveOutcome, resolverChannelForHistory, note)
                              + $"\n:page_facing_up: Evidence: `{Path.GetFileName(evidenceFile)}`";
                    Console.WriteLine($"[RESOLVE:DONE] 2/2 complete");
                }
                var resolverName = GetSuggestBypassUsername();
                var (textOk, _) = SlackSendViaApi(botToken, channel, replyText,
                    threadTs: slackTs, username: resolverName, replyBroadcast: true).GetAwaiter().GetResult();
                if (textOk) Console.WriteLine($"[RESOLVE] Slack reply sent to thread {slackTs}");
                else Console.WriteLine($"[RESOLVE] Slack reply failed");

                try
                {
                    SlackUploadFileAsync(botToken, channel, evidenceFile,
                        title: Path.GetFileName(evidenceFile), threadTs: slackTs).GetAwaiter().GetResult();
                    Console.WriteLine($"[RESOLVE] Evidence uploaded to thread");
                }
                catch (Exception ex) { Console.WriteLine($"[RESOLVE] Evidence upload failed: {ex.Message}"); }
            }
            else Console.WriteLine("[RESOLVE] No Slack config -- Slack reply skipped");
        }
        else Console.WriteLine("[RESOLVE] No slack_ts recorded -- Slack reply skipped");

        // Auto-install project skills so newly created/updated skills become available
        // and Trigger A (NotifyNewSkillsMatchingHistory) fires for matching users.
        try { SkillInstallCommand([]); }
        catch (Exception ex) { Console.WriteLine($"[RESOLVE] skill install failed: {ex.Message}"); }

        // Suggest resolved = skill publicly confirmed -- sync to all peer repo installs immediately.
        try { var (n, _, _) = SyncSkillsFromPeerRepos(); if (n > 0) Console.Error.WriteLine($"[SKILL:SYNC] +{n} from peers after resolve"); }
        catch { }

        return 0;
    }

    static bool ShouldEnforceEvidenceCommandCoupling(string cmd, string subcmd)
    {
        if (string.IsNullOrWhiteSpace(cmd) || string.IsNullOrWhiteSpace(subcmd))
            return false;
        static bool IsAsciiWord(string s) => s.All(ch => char.IsLetter(ch) || ch == '_');
        if (!IsAsciiWord(cmd) || !IsAsciiWord(subcmd)) return false;
        var known = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "a11y", "ask", "file", "web", "slack", "suggest", "prompt", "windows",
            "input", "focus", "screen", "eye", "whisper", "grap", "logcat", "mcp",
            "newchat", "zoom", "timeout", "debate", "launcher", "findprompts"
        };
        return known.Contains(cmd);
    }

    static string? CreateResolveRecoveryZip(string hqDir, string tsPrefix, string evidenceFile)
    {
        try
        {
            var binDir = AppContext.BaseDirectory;
            var coreExe = Path.Combine(binDir, "wkappbot-core.exe");
            var launcherExe = Path.Combine(binDir, "wkappbot.exe");
            if (!File.Exists(coreExe) && !File.Exists(launcherExe)) return null;

            var recoveryDir = Path.Combine(hqDir, "recovery");
            Directory.CreateDirectory(recoveryDir);

            var version = System.Diagnostics.FileVersionInfo
                .GetVersionInfo(coreExe).ProductVersion
                ?? System.Diagnostics.FileVersionInfo.GetVersionInfo(coreExe).FileVersion
                ?? "unknown";
            var stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var safeTs = tsPrefix.Replace(":", "").Replace("/", "_").Replace("\\", "_");
            var zipPath = Path.Combine(recoveryDir, $"wkappbot-recovery-v{version}-{stamp}-{safeTs}.zip");

            using var archive = System.IO.Compression.ZipFile.Open(zipPath, System.IO.Compression.ZipArchiveMode.Create);
            AddRecoveryEntry(archive, coreExe, "bin/wkappbot-core.exe");
            AddRecoveryEntry(archive, launcherExe, "bin/wkappbot.exe");
            AddRecoveryEntry(archive, evidenceFile, $"evidence/{Path.GetFileName(evidenceFile)}");

            var manifest = new StringBuilder();
            manifest.AppendLine($"version={version}");
            manifest.AppendLine($"resolved_at={DateTime.UtcNow:O}");
            manifest.AppendLine($"resolve_ts_prefix={tsPrefix}");
            manifest.AppendLine($"core={Path.GetFileName(coreExe)}");
            manifest.AppendLine($"launcher={Path.GetFileName(launcherExe)}");
            manifest.AppendLine($"evidence={Path.GetFileName(evidenceFile)}");

            var entry = archive.CreateEntry("manifest.txt");
            using var writer = new StreamWriter(entry.Open(), Encoding.UTF8);
            writer.Write(manifest.ToString());
            return zipPath;
        }
        catch { return null; }
    }

    static void AddRecoveryEntry(System.IO.Compression.ZipArchive archive, string path, string entryName)
    {
        if (!File.Exists(path)) return;
        System.IO.Compression.ZipFileExtensions.CreateEntryFromFile(
            archive, path, entryName, System.IO.Compression.CompressionLevel.Optimal);
    }

    static int RunRegressionTests(string evidenceFile, string[]? mergeAffectedCmds = null)
    {
        try
        {
            var parts = Path.GetFileNameWithoutExtension(evidenceFile).Split('-');
            var fileCmd = parts.Length > 1 ? parts[1] : "misc";
            var fileSubcmd = parts.Length > 2 ? parts[2] : "general";

            // Collect all test directories to check (both filename-based and merge-based)
            var hqDir = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq");
            var testDirs = new List<string>();
            // 1. Filename-based path (always)
            testDirs.Add(Path.Combine(hqDir, "experience", "tests", fileCmd, fileSubcmd));
            // 2. Merge affectedCommands path (if different)
            if (mergeAffectedCmds != null)
            {
                foreach (var ac in mergeAffectedCmds)
                {
                    var mergeDir = Path.Combine(hqDir, "experience", "tests", ac, fileCmd);
                    if (!testDirs.Contains(mergeDir, StringComparer.OrdinalIgnoreCase))
                        testDirs.Add(mergeDir);
                }
            }
            // Primary dir = filename-based (consistent storage + help examples)
            var cmd = fileCmd; var subcmd = fileSubcmd;
            var testsDir = Path.Combine(hqDir, "experience", "tests", cmd, subcmd);
            if (!testDirs.Any(d => Directory.Exists(d))) return 0;

            // Deleted test detection
            var manifestPath = Path.Combine(testsDir, ".manifest");
            if (File.Exists(manifestPath))
            {
                var manifest = File.ReadAllLines(manifestPath)
                    .Select(l => l.Trim())
                    .Where(l => l.Length > 0 && !l.StartsWith('#'))
                    .ToList();
                var missing = manifest.Where(name => !File.Exists(Path.Combine(testsDir, name))
                    && !Directory.GetFiles(testsDir).Any(f => Path.GetFileName(f).StartsWith(
                        Path.GetFileNameWithoutExtension(name) + ".bak-"))).ToList();
                if (missing.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("  EXPERIENCE DB test deletion detected!");
                    Console.ResetColor();

                    int restored = 0;
                    foreach (var m in missing.ToList())
                    {
                        var stem = Path.GetFileNameWithoutExtension(m);
                        var baks = Directory.GetFiles(testsDir)
                            .Where(f => Path.GetFileName(f).StartsWith(stem + ".bak-")
                                     || Path.GetFileName(f).StartsWith(m + ".bak-"))
                            .OrderByDescending(File.GetLastWriteTimeUtc)
                            .ToList();
                        if (baks.Count > 0)
                        {
                            File.Copy(baks[0], Path.Combine(testsDir, m), overwrite: true);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"  Auto-restored: {m} <- {Path.GetFileName(baks[0])}");
                            Console.ResetColor();
                            missing.Remove(m);
                            restored++;
                        }
                    }

                    if (missing.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  Cannot restore {missing.Count} (no backup):");
                        foreach (var m in missing) Console.WriteLine($"    x {m}");
                        Console.ResetColor();
                        BackupEvidenceOnFailure(evidenceFile, mergeAffectedCmds);
                        return 1;
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  {restored} test(s) restored from backup -- retry to pass");
                    Console.ResetColor();
                    BackupEvidenceOnFailure(evidenceFile, mergeAffectedCmds);
                    return 1;
                }
            }

            var evidenceAbs = Path.GetFullPath(evidenceFile);
            // Collect scripts from ALL test directories (filename-based + merge-based)
            var scripts = testDirs
                .Where(Directory.Exists)
                .SelectMany(d => Directory.GetFiles(d))
                .Where(f =>
                {
                    var ext2 = Path.GetExtension(f).ToLowerInvariant();
                    return ext2 is ".sh" or ".ps1" or ".cmd" or ".bat";
                })
                .Where(f => !Path.GetFileName(f).Contains(".bak-"))
                .Where(f => !string.Equals(Path.GetFullPath(f), evidenceAbs, StringComparison.OrdinalIgnoreCase))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(f => f)
                .ToArray();
            if (scripts.Length == 0) return 0;

            Console.WriteLine($"\n[REGRESSION] Running {scripts.Length} existing test(s) for {cmd}/{subcmd}...");
            int passed = 0, failed = 0;
            var failures = new List<(string name, string path)>();
            foreach (var script in scripts)
            {
                var name = Path.GetFileName(script);
                Console.Write($"  {name}... ");
                try { Console.Out.Flush(); } catch { }
                try
                {
                    var ext2 = Path.GetExtension(script).ToLowerInvariant();
                    var runner = ext2 switch
                    {
                        // ProcessStartInfo.FileName accepts paths with spaces directly -- no quoting needed.
                        ".sh" => File.Exists(@"C:\Program Files\Git\usr\bin\bash.exe")
                            ? @"C:\Program Files\Git\usr\bin\bash.exe"
                            : "bash",
                        ".ps1" => "powershell.exe",
                        ".cmd" or ".bat" => "cmd.exe",
                        _ => null
                    };
                    if (runner == null)
                        throw new InvalidOperationException($"Unsupported regression script: {script}");

                    var runnerArgs = ext2 switch
                    {
                        ".sh" => $"\"{script}\"",
                        ".ps1" => $"-NoProfile -ExecutionPolicy Bypass -File \"{script}\"",
                        ".cmd" or ".bat" => $"/c \"{script}\"",
                        _ => throw new InvalidOperationException()
                    };

                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = runner,
                        Arguments = runnerArgs,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                    };
                    psi.EnvironmentVariables["WKAPPBOT_WORKER"] = "1";
                    var proc = AppBotPipe.StartTracked(psi, psi.WorkingDirectory.Length > 0 ? psi.WorkingDirectory : Environment.CurrentDirectory, "SUGGEST");
                    var rOut = Task.Run(() => {
                        string? line;
                        while ((line = proc?.StandardOutput.ReadLine()) != null)
                        { Console.WriteLine($"    {line}"); try { Console.Out.Flush(); } catch { } }
                    });
                    var rErr = Task.Run(() => {
                        string? line;
                        while ((line = proc?.StandardError.ReadLine()) != null)
                            Console.WriteLine($"    {line}");
                    });
                    proc?.WaitForExit(60_000);
                    rOut.Wait(3_000); rErr.Wait(1_000);
                    var exitCode = proc?.ExitCode ?? 1;
                    if (exitCode == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("PASS");
                        Console.ResetColor();
                        passed++;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"FAIL (exit={exitCode})");
                        Console.ResetColor();
                        failed++;
                        failures.Add((name, script));
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ERROR ({ex.Message})");
                    Console.ResetColor();
                    failed++;
                    failures.Add((name, script));
                }
            }

            if (failed == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[REGRESSION] All {passed} regression test(s) passed");
                Console.ResetColor();
                return 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[REGRESSION] {failed}/{scripts.Length} regression test(s) FAILED -- resolve BLOCKED");
                Console.ResetColor();
                Console.WriteLine($"  Fix the regression in code, or edit the failing test if the check is outdated.");
                Console.WriteLine($"  Edit commands:");
                foreach (var (name, path) in failures)
                    Console.WriteLine($"    wkappbot file edit \"old\" \"new\" \"{path.Replace('\\', '/')}\"");

                try
                {
                    var regCfg = LoadSlackConfig();
                    var botToken = regCfg?["bot_token"]?.GetValue<string>();
                    var channel  = regCfg?["channel"]?.GetValue<string>();
                    if (!string.IsNullOrEmpty(botToken) && !string.IsNullOrEmpty(channel))
                    {
                        var senderName = GetSuggestBypassUsername();
                        var sb = new System.Text.StringBuilder();
                        sb.AppendLine($":x: *[REGRESSION BLOCKED]* `{cmd}/{subcmd}` -- {failed}/{scripts.Length} test(s) failed:");
                        foreach (var (name, path) in failures)
                            sb.AppendLine($"* `{name}` -- `wkedit \"old\" \"new\" \"{path.Replace('\\', '/')}\"` to fix");
                        sb.Append("_Fix code bug or update outdated test via wkedit, then re-resolve_");
                        SlackSendViaApi(botToken, channel, sb.ToString(), username: senderName).GetAwaiter().GetResult();
                    }
                }
                catch { }
                BackupEvidenceOnFailure(evidenceFile, mergeAffectedCmds);
                return 1;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[REGRESSION] Error running regression tests: {ex.Message}");
        }
        return 0;
    }

    static void BackupEvidenceOnFailure(string evidenceFile, string[]? _mergeAffectedCmds = null)
    {
        try
        {
            var parts = Path.GetFileNameWithoutExtension(evidenceFile).Split('-');
            var cmd = parts.Length > 1 ? parts[1] : "misc";
            var subcmd = parts.Length > 2 ? parts[2] : "general";
            var hqDir = Path.Combine(AppContext.BaseDirectory, "wkappbot.hq");
            var testsDir = Path.Combine(hqDir, "experience", "tests", cmd, subcmd);
            Directory.CreateDirectory(testsDir);

            var caller = Environment.GetEnvironmentVariable("WKAPPBOT_LOOP_CALLER") ?? "resolve";
            var ts = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            var origName = Path.GetFileName(evidenceFile);
            var bakName = $"{origName}.bak-{ts}-{caller}.txt";
            var bakPath = Path.Combine(testsDir, bakName);
            File.Copy(evidenceFile, bakPath, overwrite: true);
            Console.WriteLine($"  [RESOLVE] Evidence backed up -> experience/tests/{cmd}/{subcmd}/{bakName}");
            Console.WriteLine($"  [RESOLVE] Edit hint: wkappbot file edit \"old\" \"new\" \"{bakPath.Replace('\\', '/')}\"");
        }
        catch (Exception ex) { Console.WriteLine($"  [RESOLVE] Backup failed: {ex.Message}"); }
    }

    /// <summary>
    /// Append a [BUG-AUTO] entry to suggestions.jsonl. Deduplicates within 1 hour by text prefix.
    /// </summary>
    internal static void AutoRegisterBug(string text, string[]? args = null, string? callerCwd = null, string? logFile = null)
    {
        try
        {
            var suggPath = Path.Combine(DataDir, "suggestions.jsonl");
            var prefix = text[..Math.Min(80, text.Length)];
            var nowIso = DateTime.UtcNow.ToString("o");

            // Delta-comment mode: when the same-prefix record exists anywhere
            // in the backlog (not just the recent window) we annotate the
            // existing entry instead of creating a new one. That keeps the
            // backlog flat + makes `freq/hr` naturally rise via seenCount
            // rather than via spam. If prefix doesn't match anything, fall
            // through to normal append.
            if (File.Exists(suggPath))
            {
                var lines = File.ReadAllLines(suggPath);
                int matchIdx = -1;
                JsonNode? matchNode = null;
                for (int i = lines.Length - 1; i >= 0 && i >= lines.Length - 500; i--)
                {
                    var line = lines[i];
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    try
                    {
                        var n = JsonNode.Parse(line);
                        var nodeText = n?["text"]?.GetValue<string>() ?? "";
                        // Match direct text prefix OR a merge-record's title prefix
                        var title = n?["title"]?.GetValue<string>() ?? "";
                        if (nodeText.StartsWith(prefix, StringComparison.Ordinal)
                            || title.StartsWith(prefix, StringComparison.Ordinal))
                        {
                            matchIdx = i;
                            matchNode = n;
                            break;
                        }
                    }
                    catch { }
                }

                if (matchIdx >= 0 && matchNode != null)
                {
                    // Bump seenCount + lastSeen + append compact comment
                    var obj = matchNode.AsObject();
                    int prev = 1;
                    if (obj.TryGetPropertyValue("seenCount", out var sc) && sc is JsonValue v && v.TryGetValue<int>(out var ci))
                        prev = ci;
                    obj["seenCount"] = prev + 1;
                    obj["lastSeen"]  = nowIso;

                    JsonArray comments;
                    if (obj.TryGetPropertyValue("comments", out var cExisting) && cExisting is JsonArray arr)
                        comments = arr;
                    else
                    {
                        comments = new JsonArray();
                        obj["comments"] = comments;
                    }
                    // Keep only the last 20 comments to prevent unbounded growth
                    while (comments.Count >= 20) comments.RemoveAt(0);
                    comments.Add(new JsonObject
                    {
                        ["ts"] = nowIso,
                        ["cwd"] = callerCwd ?? Environment.CurrentDirectory,
                        ["log"] = logFile ?? "",
                        ["snippet"] = text.Length > 120 ? text[..117] + "..." : text,
                    });

                    lines[matchIdx] = matchNode.ToJsonString();
                    File.WriteAllLines(suggPath, lines);
                    Console.WriteLine($"[BUG-AUTO] delta-comment on existing ({prev + 1}x): {prefix}...");
                    return;
                }
            }

            var entry = JsonSerializer.Serialize(new
            {
                ts     = DateTime.UtcNow.ToString("o"),
                from   = "bug-auto",
                cwd    = callerCwd ?? Environment.CurrentDirectory,
                text,
                files  = logFile != null ? new[] { logFile } : Array.Empty<string>(),
                status = "pending",
                tag    = "bug-auto",
                seenCount = 1,
            });
            File.AppendAllText(suggPath, entry + "\n");
            Console.WriteLine($"[BUG-AUTO] registered: {prefix}...");
        }
        catch { }
    }
}
