using System.Diagnostics;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    sealed record VsCodeWindowCandidate(IntPtr Hwnd, string Title, string? WorkspaceCwd);

    static int FileOpenCommand(string[] args)
    {
        string? spec = null;
        string? path = null;
        int? line = null;
        int? col = null;

        for (int i = 0; i < args.Length; i++)
        {
            if ((args[i] is "--path" or "--file") && i + 1 < args.Length) path = args[++i];
            else if (args[i] == "--line" && i + 1 < args.Length && int.TryParse(args[++i], out var parsedLine)) line = parsedLine;
            else if ((args[i] is "--col" or "--column") && i + 1 < args.Length && int.TryParse(args[++i], out var parsedCol)) col = parsedCol;
            else if (!args[i].StartsWith("--")) spec ??= args[i];
        }

        if (spec != null)
            ParseFileOpenSpec(spec, ref path, ref line, ref col);

        if (string.IsNullOrWhiteSpace(path))
            return Error("Usage: file open <path>[:line[:col]]");

        var fullPath = ResolveFileOpenPath(path!);
        if (string.IsNullOrWhiteSpace(fullPath) || !File.Exists(fullPath))
            return Error($"File not found: {path}");

        var target = FindResponsibleVsCodeWindow(fullPath);
        var gotoSpec = BuildCodeGotoSpec(fullPath, line, col);
        var workingDir = target?.WorkspaceCwd
            ?? Path.GetDirectoryName(fullPath)
            ?? FileDefaultDir();

        if (target != null)
        {
            NativeMethods.SmartSetForegroundWindow(target.Hwnd);
            Thread.Sleep(150);
            Console.WriteLine($"[FILE] open target -> hwnd=0x{target.Hwnd:X} cwd={target.WorkspaceCwd ?? "?"}");
        }
        else
        {
            Console.WriteLine("[FILE] open target -> no matching VS Code window, falling back to code.exe");
        }

        return LaunchVsCodeGoto(gotoSpec, workingDir);
    }

    static void ParseFileOpenSpec(string spec, ref string? path, ref int? line, ref int? col)
    {
        var remaining = spec.Trim();
        int? parsedCol = null;
        int? parsedLine = null;

        for (int pass = 0; pass < 2; pass++)
        {
            var idx = remaining.LastIndexOf(':');
            if (idx <= 1) break; // preserve drive root like C:\
            var suffix = remaining[(idx + 1)..];
            if (!int.TryParse(suffix, out var num)) break;
            if (parsedCol == null) parsedCol = num;
            else if (parsedLine == null) parsedLine = num;
            remaining = remaining[..idx];
        }

        path ??= remaining;
        if (line == null && parsedLine != null) line = parsedLine;
        else if (line == null && parsedCol != null) line = parsedCol;

        if (parsedLine != null && parsedCol != null && col == null) col = parsedCol;
    }

    static string ResolveFileOpenPath(string rawPath)
    {
        if (Path.IsPathRooted(rawPath))
            return Path.GetFullPath(rawPath);
        return Path.GetFullPath(Path.Combine(FileDefaultDir(), rawPath));
    }

    static string BuildCodeGotoSpec(string path, int? line, int? col)
    {
        if (line == null) return path;
        if (col == null) return $"{path}:{line}";
        return $"{path}:{line}:{col}";
    }

    static VsCodeWindowCandidate? FindResponsibleVsCodeWindow(string filePath)
    {
        var candidates = new List<VsCodeWindowCandidate>();

        NativeMethods.EnumWindows((hWnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            if (WindowFinder.GetClassName(hWnd) != "Chrome_WidgetWin_1") return true;

            NativeMethods.GetWindowThreadProcessId(hWnd, out uint pid);
            string procName;
            try { procName = Process.GetProcessById((int)pid).ProcessName; }
            catch { return true; }
            if (!procName.Equals("Code", StringComparison.OrdinalIgnoreCase)) return true;

            var title = WindowFinder.GetWindowText(hWnd);
            if (!title.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase)) return true;

            candidates.Add(new VsCodeWindowCandidate(hWnd, title, ExtractCwdFromVsCodeTitle(title)));
            return true;
        }, IntPtr.Zero);

        var matches = candidates
            .Where(c => !string.IsNullOrWhiteSpace(c.WorkspaceCwd) && IsPathUnderRoot(filePath, c.WorkspaceCwd!))
            .OrderByDescending(c => c.WorkspaceCwd!.Length)
            .ThenBy(c => c.Title.Length)
            .ToList();

        return matches.FirstOrDefault();
    }

    static bool IsPathUnderRoot(string path, string root)
    {
        try
        {
            var fullPath = Path.GetFullPath(path).TrimEnd('\\');
            var fullRoot = Path.GetFullPath(root).TrimEnd('\\');
            return fullPath.Equals(fullRoot, StringComparison.OrdinalIgnoreCase)
                || fullPath.StartsWith(fullRoot + "\\", StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    static int LaunchVsCodeGoto(string gotoSpec, string workingDir)
    {
        foreach (var exe in EnumerateVsCodeCliCandidates())
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = $"--reuse-window --goto \"{gotoSpec}\"",
                    WorkingDirectory = workingDir,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                using var proc = AppBotPipe.StartTracked(psi, workingDir, "FILE-OPEN");
                Console.WriteLine($"[FILE] open OK -> {gotoSpec}");
                return 0;
            }
            catch
            {
                // try next candidate
            }
        }

        return Error("VS Code CLI not found (tried code.exe / code.cmd)");
    }

    static IEnumerable<string> EnumerateVsCodeCliCandidates()
    {
        yield return "code.exe";
        yield return "code.cmd";

        var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        if (!string.IsNullOrWhiteSpace(localAppData))
        {
            yield return Path.Combine(localAppData, "Programs", "Microsoft VS Code", "bin", "code.cmd");
            yield return Path.Combine(localAppData, "Programs", "Microsoft VS Code", "Code.exe");
        }
    }
}
