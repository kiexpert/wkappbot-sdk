namespace WKAppBot.CLI;

// partial class: PrintUsage + Error + GetArgValue (shared utilities)
internal partial class Program
{
    static int PrintUsage()
    {
        Console.WriteLine(@"
WKAppBot - Windows App Automation Test Framework
YAML scenario-driven UI automation for Windows desktop apps.

Usage:
  wkappbot <command> [options]

Scenario Commands:
  run <scenario.yaml> [-v] [--no-watch] [--watch-interval N]
      Run a test scenario with passive [WATCH] background tracker.
  validate <scenario.yaml>
      Validate a YAML scenario file (syntax + structure check).

Inspection Commands:
  inspect <window-title> [--depth N] [--win32]
      Dump UI Automation tree of a window (by title substring match).
  focus [--title <text>] [--delay N] [--depth N] [--win32] [-b]
      Inspect the currently focused window (countdown + UIA/Win32 dump).
  watch [--duration N] [--live] [--win32] [--interval N] [--save file]
      Real-time element tracking under mouse cursor (Ctrl+C to stop).
  capture <window-title> [-o output.png] [--form <id>]
      Capture a screenshot of a window or specific MDI child form.

App Scanning Commands:
  scan <window-title> [--save] [--ocr] [--detail] [--depth N]
      Scan window structure: auto-classify zones, MDI forms, Experience DB.
      --save: Save profile + experience data. --ocr: OCR learning.
      --detail: Per-control screenshots + text history (opt-in).
  form-dump <window-title> <form-id> [--depth N]
      Recursively dump all controls in an MDI child form.

Automation Commands:
  click <window-title> <form-id> [button-text] [--combo N INDEX] [options]
      Find an MDI form and click a button (with MFC combo selection).
  do <window-title> <form-id> <button-text> [--delay N] [--confirm] [--no-dismiss]
      Full automation: combos + button click + dialog handling + notice dismiss.
      Integrates Experience DB for smart click strategy ordering.
  dismiss <window-title> [--force] [keyword1] [keyword2] ...
      Auto-dismiss MDI child notice/popup windows matching keywords.
      Default keywords: 공지, 인사, 안내, 알림, POP-UP.
      Reads content via OCR and classifies importance (critical = no close).

Dialog & Toolbar Commands:
  dialog-click <dialog-title> [button-index]
      Click a button in a top-level dialog using physical mouse click.
  toolbar-ocr <window-title> [--click ""text""] [--save]
      OCR-scan MFC toolbar panes and show recognized button text.
      --click: Click the region matching text. --save: Save toolbar screenshots.

Chart Analysis Commands:
  chart-analyze <window-title|image.png> [--form <id>] [--candles N] [options]
      Extract OHLC + volume candlestick data from chart screenshots.
      3 strategies: body-first, column-scan, hts_style (auto-selected).
      --tooltip: Y-axis recalibration via mouse hover (Phase B).
      --debug: Save debug overlay image with detection visualization.
  tooltip-probe <process-name> [--capture]
      Enumerate tooltip windows for a process (diagnostic tool).

HTS Stress Test Commands:
  hts-stress <form.xmf> [-n 20] [--pattern repeat|memory|ctx-only] [options]
      HTS MDI stress test with memory tracking (3 patterns).
      --process <name>: Target process (default: HTSRun).

Run Options:
  --no-watch          Disable passive background element watcher
  --watch-interval N  Watcher polling interval in ms (default: 200)

Focus Options:
  --title <text>  Find window by title (skip countdown)
  --delay N       Seconds to wait before capturing focus (default: 3)
  --depth N       UIA tree depth (default: 6)
  --win32         Show Win32 child windows list
  -b, --buttons   Show all clickable buttons with AutomationId

Watch Options:
  --duration N    Stop after N seconds (default: unlimited)
  --live          Single-line overwrite mode (instead of scroll history)
  --win32         Also show Win32 hwnd/class under cursor
  --interval N    Polling interval in ms (default: 150)
  --save <file>   Save log to specific file

General Options:
  -v, --verbose   Verbose output
  --report <dir>  Generate HTML report in directory
  -h, --help      Show this help message

Data Directory:
  Runtime data (profiles, logs, handlers, output) stored in:
  {exe_dir}/wkappbot.hq/
");
        return 0;
    }

    static int Error(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine(msg);
        Console.ResetColor();
        return 1;
    }

    static string? GetArgValue(string[] args, string flag)
    {
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == flag) return args[i + 1];
        }
        return null;
    }
}
