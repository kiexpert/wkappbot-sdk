using System.IO;
using System.Text;

namespace WKAppBot.CLI;

internal static class AppBotEyeWatcher
{
    private static FileSystemWatcher? _tickWatcher;
    private static FileSystemWatcher? _sessionWatcher;
    private static FileSystemWatcher? _logWatcher;

    private static volatile bool _tickDirty;
    private static volatile bool _promptDirty;
    private static volatile bool _logDirty;
    private static volatile string? _promptChangedFile;

    private static string _dirtyTickFile = "";
    private static long _dirtyTickLength = -1;
    private static DateTime _dirtyTickWriteUtc = DateTime.MinValue;
    private static string _dirtyPromptFile = "";
    private static long _dirtyPromptLength = -1;
    private static DateTime _dirtyPromptWriteUtc = DateTime.MinValue;

    public static void Initialize()
    {
        InitTickWatcher();
        InitSessionWatcher();
        InitLogWatcher();
    }

    public static void Dispose()
    {
        TryDispose(ref _tickWatcher);
        TryDispose(ref _sessionWatcher);
        TryDispose(ref _logWatcher);
    }

    public static (bool tickDirty, bool promptDirty) CheckDirtyFlags(bool forceFull = false)
    {
        bool tickDirty = _tickDirty;
        bool promptDirty = _promptDirty;
        _tickDirty = false;
        _promptDirty = false;

        if (forceFull)
        {
            EnsureLatestTick();
            EnsureLatestPrompt();
        }

        return (tickDirty, promptDirty);
    }

    /// <summary>Optional: expose log dirty flag for other consumers.</summary>
    public static bool ConsumeLogDirty()
    {
        var value = _logDirty;
        _logDirty = false;
        return value;
    }

    private static void InitTickWatcher()
    {
        try
        {
            var tickPath = Program.EyeTicksPath;
            var tickDir = Path.GetDirectoryName(tickPath);
            var tickFile = Path.GetFileName(tickPath);
            if (!string.IsNullOrEmpty(tickDir) && Directory.Exists(tickDir))
            {
                _tickWatcher = new FileSystemWatcher(tickDir)
                {
                    Filter = tickFile,
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
                    EnableRaisingEvents = true
                };
                _tickWatcher.Changed += (_, _) => _tickDirty = true;
                _tickWatcher.Created += (_, _) => _tickDirty = true;
                Console.WriteLine($"[EYE][FSW] Tick watcher: {tickDir}/{tickFile}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE][FSW] Tick watcher init failed: {ex.Message}");
        }
    }

    private static void InitSessionWatcher()
    {
        try
        {
            var sessionsDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".openclaw", "agents", "main", "sessions");
            if (Directory.Exists(sessionsDir))
            {
                _sessionWatcher = new FileSystemWatcher(sessionsDir)
                {
                    Filter = "*.jsonl",
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName,
                    EnableRaisingEvents = true
                };
                _sessionWatcher.Changed += (_, e) => { if (e.Name is { } n) OnSessionChanged(n); };
                _sessionWatcher.Created += (_, e) => { if (e.Name is { } n) OnSessionChanged(n); };
                _sessionWatcher.Renamed += (_, e) => { if (e.Name is { } n) OnSessionChanged(n); };
                Console.WriteLine($"[EYE][FSW] Prompt watcher: {sessionsDir}/*.jsonl");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE][FSW] Prompt watcher init failed: {ex.Message}");
        }
    }

    private static void InitLogWatcher()
    {
        try
        {
            var logDir = Path.Combine(Program.DataDir, "logs");
            if (Directory.Exists(logDir))
            {
                _logWatcher = new FileSystemWatcher(logDir)
                {
                    Filter = "*.log",
                    IncludeSubdirectories = false,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName,
                    EnableRaisingEvents = true
                };
                _logWatcher.Changed += (_, _) => _logDirty = true;
                _logWatcher.Created += (_, _) => _logDirty = true;
                Console.WriteLine($"[EYE][FSW] Logs watcher: {logDir}/*.log");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE][FSW] Logs watcher init failed: {ex.Message}");
        }
    }

    private static void EnsureLatestTick()
    {
        try
        {
            var tickPath = Program.EyeTicksPath;
            if (File.Exists(tickPath))
            {
                var fi = new FileInfo(tickPath);
                if (_dirtyTickFile != tickPath || _dirtyTickLength != fi.Length || _dirtyTickWriteUtc != fi.LastWriteTimeUtc)
                {
                    _tickDirty = true;
                    _dirtyTickFile = tickPath;
                    _dirtyTickLength = fi.Length;
                    _dirtyTickWriteUtc = fi.LastWriteTimeUtc;
                }
            }
        }
        catch { }
    }

    private static void EnsureLatestPrompt()
    {
        try
        {
            var sessionsDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".openclaw", "agents", "main", "sessions");
            if (!Directory.Exists(sessionsDir))
                return;

            var latestFile = Directory.GetFiles(sessionsDir, "*.jsonl")
                .OrderByDescending(p => File.GetLastWriteTimeUtc(p))
                .FirstOrDefault();
            if (string.IsNullOrWhiteSpace(latestFile) || !File.Exists(latestFile))
                return;

            var fi = new FileInfo(latestFile);
            if (_dirtyPromptFile != latestFile || _dirtyPromptLength != fi.Length || _dirtyPromptWriteUtc != fi.LastWriteTimeUtc)
            {
                _promptDirty = true;
                _dirtyPromptFile = latestFile;
                _dirtyPromptLength = fi.Length;
                _dirtyPromptWriteUtc = fi.LastWriteTimeUtc;
            }
        }
        catch { }
    }

    private static void OnSessionChanged(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return;

        var trackedName = Path.GetFileName(_dirtyPromptFile);
        if (string.IsNullOrWhiteSpace(trackedName) || string.Equals(fileName, trackedName, StringComparison.OrdinalIgnoreCase))
        {
            _promptChangedFile = fileName;
            _promptDirty = true;
        }
    }

    private static void TryDispose(ref FileSystemWatcher? watcher)
    {
        try
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
            }
        }
        catch { }
        finally { watcher = null; }
    }
}
