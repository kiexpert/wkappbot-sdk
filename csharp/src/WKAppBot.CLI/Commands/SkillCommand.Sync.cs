// SkillCommand.Sync.cs -- Periodic skill sync from peer repos into global HQ installed dir.
//
// CONFLICT POLICY (version-based, not mtime-based):
//   src.version > dest.version  → copy (peer has newer skill)
//   src.version == dest.version → keep dest; WARN if content differs (split-brain)
//   src.version < dest.version  → skip (HQ already has newer version)
//   dest missing                → copy (first install)
//
// Why version > mtime:
//   mtime changes on every File.Copy, so after syncing from peer, dest.mtime is "now"
//   which always beats future peer edits that haven't bumped the version. Version field
//   is explicitly incremented by skill contribute/edit and reflects intentional changes.
//
// Split-brain detection:
//   Same version, different content = two repos edited the skill independently from the
//   same base. Log WARN with both sources so the user can decide which to keep.
//
// Eye runs SyncSkillsFromPeerRepos() every 30 min; `wkappbot skill sync` is the manual trigger.
// contribute/edit/resolve also call it immediately so changes propagate without waiting.

using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Sync .skill.json files from peer repo skills/ dirs into HqSkillsDir.
    /// Resolution: version field wins; same version + different content = WARN split-brain.
    /// </summary>
    static (int installed, int upToDate, int conflicts) SyncSkillsFromPeerRepos(bool verbose = false)
    {
        var root = ProjectRoot.Find();
        var parentDir = Path.GetDirectoryName(root.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
        if (!Directory.Exists(parentDir)) return (0, 0, 0);

        int installed = 0, upToDate = 0, conflicts = 0;

        foreach (var repoDir in Directory.GetDirectories(parentDir))
        {
            if (string.Equals(repoDir, root, StringComparison.OrdinalIgnoreCase)) continue;

            var repoSkillsDir = Path.Combine(repoDir, "skills");
            if (!Directory.Exists(repoSkillsDir)) continue;

            var repoName = Path.GetFileName(repoDir);
            foreach (var src in Directory.EnumerateFiles(repoSkillsDir, "*.skill.json", SearchOption.AllDirectories))
            {
                var rel  = Path.GetRelativePath(repoSkillsDir, src);
                var dest = Path.Combine(HqSkillsDir, rel);

                if (File.Exists(dest))
                {
                    var srcVer  = ReadSkillVersion(src);
                    var destVer = ReadSkillVersion(dest);
                    int cmp = CompareSkillVersions(srcVer, destVer);

                    if (cmp < 0)
                    {
                        // dest has a newer version -- skip
                        upToDate++;
                        continue;
                    }
                    if (cmp == 0)
                    {
                        // Same version: check for split-brain (same version, different bytes)
                        if (!FileBytesEqual(src, dest))
                        {
                            conflicts++;
                            Console.Error.WriteLine(
                                $"[SKILL:SYNC] WARN split-brain {rel} v{srcVer}: " +
                                $"HQ and {repoName} differ at same version -- " +
                                $"keeping HQ copy. Run `wkappbot skill sync --resolve {Path.GetFileNameWithoutExtension(src).Replace(".skill","")}` to choose.");
                        }
                        else
                        {
                            upToDate++;
                        }
                        continue;
                    }
                    // cmp > 0: src is newer version → fall through to copy
                }

                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
                    File.Copy(src, dest, overwrite: true);
                    installed++;
                    var id = Path.GetFileNameWithoutExtension(src).Replace(".skill", "");
                    if (verbose)
                        Console.WriteLine($"[SKILL:SYNC] +{id} v{ReadSkillVersion(dest)} from {repoName}");
                    else
                        Console.Error.WriteLine($"[SKILL:SYNC] installed {rel} from {repoName}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"[SKILL:SYNC] failed {rel}: {ex.Message}");
                }
            }
        }

        if (verbose || conflicts > 0)
        {
            var msg = $"[SKILL:SYNC] done -- installed={installed} up-to-date={upToDate}";
            if (conflicts > 0) msg += $" CONFLICTS={conflicts} (check stderr for details)";
            Console.WriteLine(msg);
        }
        return (installed, upToDate, conflicts);
    }

    static string ReadSkillVersion(string path)
    {
        try
        {
            var node = JsonNode.Parse(File.ReadAllText(path));
            return node?["version"]?.GetValue<string>() ?? "0.0";
        }
        catch { return "0.0"; }
    }

    /// <summary>Compare "major.minor" version strings. Returns -1/0/+1.</summary>
    static int CompareSkillVersions(string a, string b)
    {
        static (int maj, int min) Parse(string v)
        {
            var parts = v.Split('.');
            int.TryParse(parts.ElementAtOrDefault(0), out int maj);
            int.TryParse(parts.ElementAtOrDefault(1), out int min);
            return (maj, min);
        }
        var (aMaj, aMin) = Parse(a);
        var (bMaj, bMin) = Parse(b);
        if (aMaj != bMaj) return aMaj.CompareTo(bMaj);
        return aMin.CompareTo(bMin);
    }

    static bool FileBytesEqual(string p1, string p2)
    {
        try
        {
            var b1 = File.ReadAllBytes(p1);
            var b2 = File.ReadAllBytes(p2);
            return b1.SequenceEqual(b2);
        }
        catch { return false; }
    }

    static int SkillSyncCommand(string[] args)
    {
        Console.WriteLine("[SKILL:SYNC] scanning peer repos...");
        SyncSkillsFromPeerRepos(verbose: true);
        return 0;
    }
}
