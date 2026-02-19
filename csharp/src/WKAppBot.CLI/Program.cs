using System.Text;
using WKAppBot.Core.Scenario;
using WKAppBot.Core.Runner;

namespace WKAppBot.CLI;

/// <summary>
/// CLI entry point and command dispatcher.
/// Commands are implemented in partial class files under Commands/ and Helpers/.
///
/// File layout:
///   Program.cs                          — Main, DataDir, run, validate (this file)
///   Commands/UsageCommand.cs            — PrintUsage, Error, GetArgValue
///   Commands/InspectionCommands.cs      — inspect, focus, watch, capture + watch helpers
///   Commands/ChartCommands.cs           — chart-analyze, tooltip-probe
///   Commands/HtsStressCommand.cs        — hts-stress
///   Commands/ScanCommands.cs            — scan, form-dump
///   Commands/AutomationCommands.cs      — click, do, SmartClickButton, CheckDialogGone
///   Commands/DialogCommands.cs          — dismiss, dialog-click, toolbar-ocr, TryHandleBlocker
///   Helpers/ControlHelpers.cs           — FindAllChildrenFlat, FindMfcCombos, DumpFormTree,
///                                         BuildClassPath, notice/dialog content helpers,
///                                         InteractiveButtonLearn, GenerateLearnedHandler
/// </summary>
internal partial class Program
{
    /// <summary>
    /// Runtime data directory: {exe_dir}/wkappbot.hq/
    /// All logs, profiles, output go here — keeps SDK/bin clean.
    /// HQ = HeadQuarters (본부: 경험치 축적 + 작전 기록 + 전과 보고)
    /// </summary>
    static readonly string DataDir = Path.Combine(
        Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".", "wkappbot.hq");

    static int Main(string[] args)
    {
        // Force UTF-8 output (Windows 949 codepage breaks Korean in non-Korean terminals)
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Enable DPI awareness
        try { WKAppBot.Win32.Native.NativeMethods.SetProcessDpiAwareness(2); } catch { }

        // Auto-log: tee all console output to file
        var exePath = Environment.ProcessPath ?? "wkappbot.exe";
        var exeName = Path.GetFileName(exePath);
        var logDir = Path.Combine(DataDir, "logs");
        var logFile = Path.Combine(logDir, $"{exeName}.out-{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        using var tee = new TeeTextWriter(Console.Out, logFile);
        Console.SetOut(tee);

        try
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return 1;
            }

            var command = args[0].ToLowerInvariant();
            var restArgs = args.Skip(1).ToArray();

            return command switch
            {
                "run" => RunCommand(restArgs),
                "validate" => ValidateCommand(restArgs),
                "inspect" => InspectCommand(restArgs),
                "focus" => FocusCommand(restArgs),
                "watch" => WatchCommand(restArgs),
                "capture" => CaptureCommand(restArgs),
                "hts-stress" => HtsStressCommand(restArgs),
                "scan" => ScanCommand(restArgs),
                "click" => ClickCommand(restArgs),
                "form-dump" => FormDumpCommand(restArgs),
                "do" => DoCommand(restArgs),
                "dismiss" => DismissCommand(restArgs),
                "toolbar-ocr" => ToolbarOcrCommand(restArgs),
                "chart-analyze" => ChartAnalyzeCommand(restArgs),
                "tooltip-probe" => TooltipProbeCommand(restArgs),
                "ocr" => OcrCommand(restArgs),
                "web" => WebCommand(restArgs),
                "slack" => SlackCommand(restArgs),
                "dialog-click" => DialogClickCommand(restArgs),
                "--help" or "-h" or "help" => PrintUsage(),
                _ => Error($"Unknown command: {command}")
            };
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            return 1;
        }
        finally
        {
            Console.SetOut(tee.OriginalConsole);
            Console.WriteLine($"Log saved: {tee.LogPath}");
        }
    }

    // ── run ────────────────────────────────────────────────────

    static int RunCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot run <scenario.yaml> [-v|--verbose] [--no-watch] [--report <dir>]");

        string scenarioPath = args[0];
        bool verbose = args.Contains("-v") || args.Contains("--verbose");
        bool noWatch = args.Contains("--no-watch");
        int watchMs = int.TryParse(GetArgValue(args, "--watch-interval"), out var wiv) ? wiv : 200;
        string? reportDir = GetArgValue(args, "--report");

        // Parse scenario
        var doc = ScenarioParser.Load(scenarioPath);
        Console.WriteLine($"Loaded: {doc.Scenario.Name} ({doc.Steps.Count} steps)");

        // Run with passive background watcher (default: on)
        var runner = new ScenarioRunner(verbose, watch: !noWatch, watchIntervalMs: watchMs);
        var result = runner.Run(doc);

        // TODO: Generate HTML report if reportDir specified

        return result.IsSuccess ? 0 : 1;
    }

    // ── validate ───────────────────────────────────────────────

    static int ValidateCommand(string[] args)
    {
        if (args.Length == 0)
            return Error("Usage: appbot validate <scenario.yaml>");

        string path = args[0];
        if (ScenarioParser.TryValidate(path, out string? error))
        {
            var doc = ScenarioParser.Load(path);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"VALID: {doc.Scenario.Name}");
            Console.ResetColor();
            Console.WriteLine($"  Steps: {doc.Steps.Count}");
            Console.WriteLine($"  App: {doc.App.Launch}");
            if (doc.Teardown != null)
                Console.WriteLine($"  Teardown: {doc.Teardown.Count} steps");
            return 0;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"INVALID: {error}");
            Console.ResetColor();
            return 1;
        }
    }
}
