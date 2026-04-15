using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// wkappbot update [--check] [--force] [--repo owner/repo]
    /// Download latest successful build artifact from GitHub Actions and hot-swap binaries.
    ///
    /// Flow:
    ///   1. Read bin/build_info.json for installed run_id
    ///   2. gh run list → latest successful build.yml run
    ///   3. gh run download → temp dir
    ///   4. Stage wkappbot-core.exe.new  → Eye picks up and hot-swaps
    ///   5. Stage wkappbot.exe.new        → TryRenameSwap at end (self-swap)
    ///   6. Copy wkappbot.hq/ assets + DLLs
    ///   7. Write updated build_info.json
    /// </summary>
    static int UpdateCommand(string[] args)
    {
        if (args.Length > 0 && args[0] is "-h" or "--help" or "help")
        {
            Console.WriteLine("Usage: wkappbot update [options]");
            Console.WriteLine("  Download latest GitHub Actions build and hot-swap binaries.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --check        Show whether an update is available (no download)");
            Console.WriteLine("  --force        Download even if already up to date");
            Console.WriteLine("  --repo <r>     GitHub repo (default: kiexpert/WKAppBot)");
            Console.WriteLine();
            Console.WriteLine("Prerequisites: gh CLI authenticated (gh auth login)");
            return 0;
        }

        bool checkOnly = args.Contains("--check");
        bool force = args.Contains("--force");
        var repoIdx = Array.IndexOf(args, "--repo");
        const string defaultRepo = "kiexpert/WKAppBot";
        var repo = repoIdx >= 0 && repoIdx + 1 < args.Length ? args[repoIdx + 1] : defaultRepo;
        var binDir = Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".";
        var buildInfoPath = Path.Combine(binDir, "build_info.json");

        // Read installed version
        string? currentRunId = null;
        string? currentSha = null;
        if (File.Exists(buildInfoPath))
        {
            try
            {
                var node = JsonNode.Parse(File.ReadAllText(buildInfoPath));
                currentRunId = node?["run_id"]?.GetValue<string>();
                currentSha = node?["sha"]?.GetValue<string>();
            }
            catch { }
        }
        Console.Error.WriteLine($"[UPDATE] Installed: {currentRunId ?? "unknown"} (sha:{currentSha?[..7] ?? "???????"})" );

        // Verify gh CLI
        if (!TryRunGh(["auth", "status"], out _))
        {
            Console.Error.WriteLine("[UPDATE] gh CLI not authenticated. Run: gh auth login");
            return 1;
        }

        // Query latest successful build
        if (!TryRunGh(["run", "list", "--repo", repo, "--workflow", "build.yml",
                       "--status", "success", "--limit", "1",
                       "--json", "databaseId,headSha,createdAt", "--jq", ".[0]"], out var runJson)
            || string.IsNullOrWhiteSpace(runJson))
        {
            Console.Error.WriteLine("[UPDATE] No successful build found on GitHub Actions.");
            return 1;
        }

        JsonNode? runNode;
        try { runNode = JsonNode.Parse(runJson.Trim()); }
        catch { Console.Error.WriteLine($"[UPDATE] Failed to parse run info: {runJson}"); return 1; }

        var latestRunId = runNode?["databaseId"]?.GetValue<long>().ToString();
        var latestSha = runNode?["headSha"]?.GetValue<string>();
        var createdAt = runNode?["createdAt"]?.GetValue<string>();
        Console.Error.WriteLine($"[UPDATE] Latest:    {latestRunId} (sha:{latestSha?[..7] ?? "?"}) @ {createdAt}");

        if (!force && latestRunId == currentRunId)
        {
            Console.WriteLine("[UPDATE] Already up to date.");
            return 0;
        }
        if (checkOnly)
        {
            Console.WriteLine($"[UPDATE] Update available: {currentRunId ?? "unknown"} → {latestRunId}");
            return 0;
        }

        // Download artifact to temp dir
        var tmpDir = Path.Combine(Path.GetTempPath(), $"wkappbot-update-{latestRunId}");
        if (Directory.Exists(tmpDir)) { try { Directory.Delete(tmpDir, true); } catch { } }
        Directory.CreateDirectory(tmpDir);

        Console.Error.WriteLine($"[UPDATE] Downloading run {latestRunId} artifacts...");
        var artifactName = $"wkappbot-bin-{latestRunId}";
        bool downloaded = TryRunGh(["run", "download", latestRunId!, "--repo", repo,
                                     "--name", artifactName, "--dir", tmpDir], out _);
        if (!downloaded)
        {
            // Fallback: download all artifacts for the run (picks up whichever bin artifact exists)
            Console.Error.WriteLine($"[UPDATE] Artifact '{artifactName}' not found — retrying without name filter...");
            downloaded = TryRunGh(["run", "download", latestRunId!, "--repo", repo, "--dir", tmpDir], out _);
        }
        if (!downloaded)
        {
            Console.Error.WriteLine("[UPDATE] Download failed. Check: gh run list --workflow build.yml");
            try { Directory.Delete(tmpDir, true); } catch { }
            return 1;
        }

        // Locate wkappbot.exe in downloaded structure (may be in a subdir named after the artifact)
        var downloadedBinDir = FindExeDir(tmpDir, "wkappbot.exe");
        if (downloadedBinDir == null)
        {
            Console.Error.WriteLine($"[UPDATE] wkappbot.exe not found in downloaded artifact ({tmpDir})");
            try { Directory.Delete(tmpDir, true); } catch { }
            return 1;
        }
        Console.Error.WriteLine($"[UPDATE] Artifact extracted to: {downloadedBinDir}");

        // Stage wkappbot-core.exe as .new.exe — Eye hot-swaps automatically
        var srcCore = Path.Combine(downloadedBinDir, "wkappbot-core.exe");
        if (File.Exists(srcCore))
        {
            var stageCore = Path.Combine(binDir, "wkappbot-core.exe.new");
            File.Copy(srcCore, stageCore, overwrite: true);
            Console.Error.WriteLine("[UPDATE] wkappbot-core.exe.new staged — Eye will hot-swap.");
        }

        // Stage wkappbot.exe as .new.exe — self-swapped at end of this command
        var srcLauncher = Path.Combine(downloadedBinDir, "wkappbot.exe");
        if (File.Exists(srcLauncher))
        {
            var stageLauncher = Path.Combine(binDir, "wkappbot.exe.new");
            File.Copy(srcLauncher, stageLauncher, overwrite: true);
        }

        // Update wkappbot.hq/ (skills, handlers, experiences)
        var srcHq = Path.Combine(downloadedBinDir, "wkappbot.hq");
        if (Directory.Exists(srcHq))
        {
            var destHq = Path.Combine(binDir, "wkappbot.hq");
            UpdateCopyDirectory(srcHq, destHq);
            Console.Error.WriteLine("[UPDATE] wkappbot.hq/ updated.");
        }

        // Copy non-exe support files (DLLs, config, etc.)
        foreach (var file in Directory.GetFiles(downloadedBinDir))
        {
            if (Path.GetExtension(file).Equals(".exe", StringComparison.OrdinalIgnoreCase)) continue;
            var dest = Path.Combine(binDir, Path.GetFileName(file));
            try { File.Copy(file, dest, overwrite: true); } catch { }
        }

        // Write updated build_info.json
        var newInfo = JsonSerializer.Serialize(new
        {
            run_id = latestRunId,
            sha = latestSha,
            updated_at = DateTime.UtcNow.ToString("O")
        });
        File.WriteAllText(buildInfoPath, newInfo);

        Console.WriteLine($"[UPDATE] Done: {currentRunId ?? "unknown"} → {latestRunId}");

        // Cleanup temp
        try { Directory.Delete(tmpDir, true); } catch { }

        // Self-swap wkappbot.exe — must be the last operation (renames the running process)
        var selfPath = Environment.ProcessPath;
        var stagedSelf = Path.Combine(binDir, "wkappbot.exe.new");
        if (!string.IsNullOrEmpty(selfPath) && File.Exists(stagedSelf))
        {
            Console.WriteLine("[UPDATE] Applying wkappbot.exe self-swap...");
            var swapResult = TryRenameSwap(selfPath, "UPDATE");
            Console.WriteLine(swapResult == HotSwapResult.Swapped
                ? "[UPDATE] wkappbot.exe updated — new binary active on next run."
                : $"[UPDATE] wkappbot.exe self-swap result: {swapResult}");
        }

        return 0;
    }

    /// <summary>Run gh CLI and capture stdout. Returns true on exit code 0.</summary>
    static bool TryRunGh(string[] args, out string output)
    {
        try
        {
            var psi = new ProcessStartInfo("gh")
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = false,
            };
            foreach (var a in args) psi.ArgumentList.Add(a);
            using var proc = Process.Start(psi)!;
            output = proc.StandardOutput.ReadToEnd().Trim();
            proc.WaitForExit();
            return proc.ExitCode == 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[UPDATE] gh error: {ex.Message}");
            output = "";
            return false;
        }
    }

    /// <summary>Find directory containing the target exe, searching recursively under root.</summary>
    static string? FindExeDir(string root, string exeName)
    {
        if (File.Exists(Path.Combine(root, exeName))) return root;
        foreach (var sub in Directory.GetDirectories(root, "*", SearchOption.AllDirectories))
            if (File.Exists(Path.Combine(sub, exeName))) return sub;
        return null;
    }

    /// <summary>Recursively copy src → dest, overwriting existing files.</summary>
    static void UpdateCopyDirectory(string src, string dest)
    {
        Directory.CreateDirectory(dest);
        foreach (var file in Directory.GetFiles(src))
            try { File.Copy(file, Path.Combine(dest, Path.GetFileName(file)), overwrite: true); } catch { }
        foreach (var dir in Directory.GetDirectories(src))
            UpdateCopyDirectory(dir, Path.Combine(dest, Path.GetFileName(dir)));
    }
}
