// SuggestCommand.AddRequirement.cs -- wkappbot suggest add-requirement <ts> "cmd => expected"
//
// Allows any repo (suggester OR maintainer) to add behavioral requirements to a pending suggest.
// Optionally links a skill and updates it immediately (--skill <id>).
//
// Flow:
//   1. Find suggest by ts prefix in suggestions.jsonl
//   2. Append requirement to the entry's requirements array
//   3. Write back with git commit
//   4. If --skill <id>: add the same requirement to the skill via skill edit
//      (triggers copy-on-edit fork if skill not in caller's ProjectSkillsDir)

using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    static int SuggestAddRequirementCommand(string[] args)
    {
        // Usage: suggest add-requirement <ts> "cmd => expected" [--skill <id>]
        if (args.Length < 2 || args[0] is "-h" or "--help")
        {
            Console.WriteLine("Usage: wkappbot suggest add-requirement <ts> \"cmd => expected\" [--skill <id>]");
            Console.WriteLine("  ts  : suggest timestamp prefix (e.g. 2026-04-26T10)");
            Console.WriteLine("  req : behavioral contract in 'cmd => expected' format");
            Console.WriteLine("  --skill <id>: also add the requirement to the named skill immediately");
            return 1;
        }

        var tsPrefix  = args[0];
        var reqRaw    = args[1];
        string? skillId = null;
        for (int i = 2; i < args.Length; i++)
            if (args[i] == "--skill" && i + 1 < args.Length) skillId = args[++i];

        // Parse "cmd => expected"
        var sep = reqRaw.IndexOf(" => ", StringComparison.Ordinal);
        if (sep < 0)
        {
            Console.Error.WriteLine("[ADD-REQ] Format must be: \"wkappbot <cmd> => <expected output>\"");
            return 1;
        }
        var reqCmd    = reqRaw[..sep].Trim();
        var reqExpect = reqRaw[(sep + 4)..].Trim();

        // -- Update suggestions.jsonl --
        var jsonlPath = Path.Combine(DataDir, "suggestions.jsonl");
        if (!File.Exists(jsonlPath))
        { Console.Error.WriteLine("[ADD-REQ] suggestions.jsonl not found"); return 1; }

        var lines    = File.ReadAllLines(jsonlPath);
        bool updated = false;
        string? matchedTs = null;

        for (int i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            JsonNode? node;
            try { node = JsonNode.Parse(lines[i]); } catch { continue; }
            if (node is not JsonObject entry) continue;

            var ts = entry["ts"]?.GetValue<string>() ?? "";
            if (!ts.StartsWith(tsPrefix, StringComparison.OrdinalIgnoreCase)) continue;
            if (entry["resolved_at"] != null)
            { Console.Error.WriteLine($"[ADD-REQ] suggest {ts} is already resolved -- use skill edit instead"); return 1; }

            // Build or extend the requirements array
            var reqs = entry["requirements"] as JsonArray ?? [];
            // Check for duplicate cmd
            foreach (var r in reqs)
            {
                if (r is JsonObject ro && string.Equals(ro["cmd"]?.GetValue<string>(), reqCmd, StringComparison.OrdinalIgnoreCase))
                { Console.Error.WriteLine($"[ADD-REQ] duplicate requirement cmd already exists: \"{reqCmd}\""); return 1; }
            }

            var newReq = new JsonObject
            {
                ["cmd"]    = reqCmd,
                ["expect"] = reqExpect
            };
            reqs.Add(newReq);
            entry["requirements"] = reqs;

            lines[i]   = entry.ToJsonString();
            matchedTs  = ts;
            updated    = true;
            Console.WriteLine($"[ADD-REQ] added to suggest {ts}: \"{reqCmd} => {reqExpect}\"");
            break;
        }

        if (!updated)
        { Console.Error.WriteLine($"[ADD-REQ] no open suggest found matching ts prefix '{tsPrefix}'"); return 1; }

        // Write back with backup
        var bak = jsonlPath + ".bak";
        File.Copy(jsonlPath, bak, overwrite: true);
        File.WriteAllLines(jsonlPath, lines);

        // Git commit
        var tsShort = matchedTs!.Length >= 19 ? matchedTs[..19] : matchedTs;
        try { GitCommitSuggestFiles("add-req", tsShort[..Math.Min(16, tsShort.Length)]); }
        catch (Exception ex) { Console.Error.WriteLine($"[ADD-REQ] git commit failed: {ex.Message}"); }

        // -- Optionally update linked skill --
        if (!string.IsNullOrEmpty(skillId))
        {
            Console.WriteLine($"[ADD-REQ] updating skill {skillId}...");
            var rc = SkillEditCommand(new[] { skillId, "--add-requirement", $"{reqCmd} => {reqExpect}" });
            if (rc != 0)
                Console.Error.WriteLine($"[ADD-REQ] skill edit returned {rc} -- requirement added to suggest but not to skill");
            else
                Console.WriteLine($"[ADD-REQ] skill {skillId} updated");
        }

        return 0;
    }
}
