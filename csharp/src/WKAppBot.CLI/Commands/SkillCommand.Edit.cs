namespace WKAppBot.CLI;

// partial class: wkappbot skill edit / delete -- project-skill mutations
internal partial class Program
{
    // -- Edit (partial update) ----------------------------------------------------

    static int SkillEditCommand(string[] args)
    {
        if (args.Length == 0 || args[0].StartsWith('-'))
        {
            Console.WriteLine("Usage: wkappbot skill edit <id> [--title X] [--desc X] [--tags t1,t2] [--steps s1|s2]");
            Console.WriteLine("                           [--step N --content X]   edit single step by 1-based index");
            Console.WriteLine("                           [--add-step X]           append a new step");
            Console.WriteLine("  Partial update -- only specified fields are changed. Version auto-bumped.");
            Console.WriteLine("  Note: to rename the file slug, use delete + contribute --id <new-slug>.");
            return 1;
        }

        var id = args[0];
        string? title = null, desc = null, stepContent = null;
        List<string>? steps = null, tags = null;
        var addRequirements = new List<SkillRequirement>();
        int stepIndex = -1;   // 1-based; -1 = not set
        bool addStep  = false;
        bool force    = args.Contains("--i-acknowledge-encoding-risk-notified-willkim-and-take-responsibility-for-token-waste");

        for (int i = 1; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--title"     when i + 1 < args.Length: title       = args[++i]; break;
                case "--desc"      when i + 1 < args.Length: desc        = args[++i]; break;
                case "--steps"     when i + 1 < args.Length: steps       = [.. args[++i].Split('|')]; break;
                case "--tags"      when i + 1 < args.Length: tags        = [.. args[++i].Split(',')]; break;
                case "--step"      when i + 1 < args.Length:
                    if (int.TryParse(args[++i], out var si)) stepIndex = si; break;
                case "--content"        when i + 1 < args.Length: stepContent = args[++i]; break;
                case "--add-step"       when i + 1 < args.Length: stepContent = args[++i]; addStep = true; break;
                case "--add-requirement" when i + 1 < args.Length:
                {
                    var raw = args[++i];
                    var sep = raw.IndexOf(" => ", StringComparison.Ordinal);
                    if (sep < 0) { Console.Error.WriteLine($"[SKILL] --add-requirement must use ' => ' separator: \"{raw}\""); return 1; }
                    addRequirements.Add(new SkillRequirement { Cmd = raw[..sep].Trim(), Expect = raw[(sep + 4)..].Trim() });
                    break;
                }
                case "--i-acknowledge-encoding-risk-notified-willkim-and-take-responsibility-for-token-waste": break;
            }
        }

        // Encoding risk check on edited content -- deferred below until we
        // know the target skill's app so Korean-UI apps (hts-*, heroes*, ...)
        // can opt into their relaxed threshold.
        var editedContent = string.Join(" ", new[] { title, desc, stepContent }.Concat(steps ?? []).Where(s => s != null)!);

        bool hasSingleStep = stepIndex > 0 && stepContent != null;
        if (title == null && desc == null && steps == null && tags == null && !hasSingleStep && !addStep && addRequirements.Count == 0)
        {
            Console.WriteLine("[SKILL] Nothing to edit -- specify at least one of: --title, --desc, --tags, --steps, --step N --content X, --add-step X, --add-requirement \"cmd => expected\"");
            return 1;
        }

        if (!Directory.Exists(ProjectSkillsDir))
        {
            Console.WriteLine("[SKILL] No project skills directory. Run from project root.");
            return 1;
        }

        string? skillPath = null;
        SkillRecord? skill = null;
        foreach (var f in Directory.GetFiles(ProjectSkillsDir, "*.skill.json", SearchOption.AllDirectories))
        {
            var s = SkillRecord.Load(f);
            if (s != null && s.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            { skill = s; skillPath = f; break; }
        }

        if (skill == null || skillPath == null)
        {
            // Copy-on-edit: skill exists in HqSkillsDir but not in this repo's project skills.
            // Fork it into ProjectSkillsDir so the suggester can add requirements without
            // needing to manually copy first. Sync will propagate the updated version (higher
            // version number) back to HqSkillsDir and to all other repos.
            SkillRecord? hqSkill = null;
            string? hqPath = null;
            if (Directory.Exists(HqSkillsDir))
            {
                foreach (var f in Directory.EnumerateFiles(HqSkillsDir, "*.skill.json", SearchOption.AllDirectories))
                {
                    var s = SkillRecord.Load(f);
                    if (s != null && s.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                    { hqSkill = s; hqPath = f; break; }
                }
            }
            if (hqSkill == null || hqPath == null)
            {
                Console.Error.WriteLine($"[SKILL] Not found: {id}");
                return 1;
            }
            // Fork: copy to ProjectSkillsDir preserving sub-dir (app namespace)
            var forkDir = Path.Combine(ProjectSkillsDir, hqSkill.App ?? "wkappbot-core");
            Directory.CreateDirectory(forkDir);
            skillPath = Path.Combine(forkDir, Path.GetFileName(hqPath));
            File.Copy(hqPath, skillPath, overwrite: false);
            skill = hqSkill;
            Console.WriteLine($"[SKILL] forked {id} from HQ → {skillPath} (edit here, sync propagates back)");
        }

        // Run encoding check now that we know the target skill's app.
        if (!force && !string.IsNullOrEmpty(editedContent) && HasEncodingRisk(editedContent, "skill edit", skill.App)) return 1;

        if (title != null) skill.Title = title;
        if (desc  != null) skill.Desc  = desc;
        if (steps != null) skill.Steps = steps;
        if (tags  != null) skill.Tags  = tags;

        // --step N --content X : edit single step by 1-based index
        if (hasSingleStep)
        {
            skill.Steps ??= [];
            int idx = stepIndex - 1;
            if (idx < 0 || idx >= skill.Steps.Count)
                return Error($"--step {stepIndex} out of range (skill has {skill.Steps.Count} step(s))");
            var old = skill.Steps[idx];
            skill.Steps[idx] = stepContent!;
            Console.WriteLine($"[SKILL] step {stepIndex}: \"{old}\" -> \"{stepContent}\"");
        }

        // --add-step X : append new step
        if (addStep && stepContent != null)
        {
            skill.Steps ??= [];
            skill.Steps.Add(stepContent);
            Console.WriteLine($"[SKILL] added step {skill.Steps.Count}: \"{stepContent}\"");
        }

        // --add-requirement "cmd => expected" : append behavioral contract
        if (addRequirements.Count > 0)
        {
            skill.Requirements ??= [];
            foreach (var req in addRequirements)
            {
                bool dup = skill.Requirements.Any(r => string.Equals(r.Cmd, req.Cmd, StringComparison.OrdinalIgnoreCase));
                if (dup)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[SKILL] FAILED: duplicate requirement cmd: \"{req.Cmd}\"");
                    Console.ResetColor();
                    return 1;
                }
                skill.Requirements.Add(req);
                Console.WriteLine($"[SKILL] added requirement: \"{req.Cmd} => {req.Expect}\"");
            }
            // Guard: hard-fail if total < 3 after edit.
            if (skill.Requirements.Count < 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[SKILL] FAILED: requirements total {skill.Requirements.Count}/3 -- must have at least 3.");
                Console.WriteLine("  Add more: wkappbot skill edit <id> --add-requirement \"wkappbot <cmd> => <expected>\"");
                Console.ResetColor();
                return 1;
            }
        }

        skill.Updated = DateTime.UtcNow;
        skill.Version = BumpVersion(skill.Version);

        skill.Save(skillPath);
        // stdout, not stderr -- see SkillContributeCommand for the rationale
        // (IocpPipeRelay buffers+drops stderr on exit=0).
        Console.WriteLine($"[SKILL] Updated: [{skill.App}] {skill.Title} (id={skill.Id}, v{skill.Version})");
        try { var (n, _, _) = SyncSkillsFromPeerRepos(); if (n > 0) Console.Error.WriteLine($"[SKILL:SYNC] +{n} from peers"); }
        catch { }
        return 0;
    }

    // -- Delete ------------------------------------------------------------------─

    static int SkillDeleteCommand(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: wkappbot skill delete <id>");
            return 1;
        }

        if (!Directory.Exists(ProjectSkillsDir))
        {
            Console.WriteLine("[SKILL] No project skills directory.");
            return 1;
        }

        var id = args[0];
        foreach (var f in Directory.GetFiles(ProjectSkillsDir, "*.skill.json", SearchOption.AllDirectories))
        {
            var s = SkillRecord.Load(f);
            if (s == null || !s.Id.Equals(id, StringComparison.OrdinalIgnoreCase)) continue;
            File.Delete(f);
            Console.Error.WriteLine($"[SKILL] Deleted: [{s.App}] {s.Title} ({id})");
            return 0;
        }

        Console.Error.WriteLine($"[SKILL] Not found in project skills: {id}");
        return 1;
    }
}
