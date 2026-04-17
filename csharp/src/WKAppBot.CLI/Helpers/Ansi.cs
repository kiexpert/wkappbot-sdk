namespace WKAppBot.CLI;

/// <summary>
/// ANSI escape code helpers for colorized terminal output.
/// All methods return empty string when stdout is redirected (pipe/file).
/// </summary>
internal static class Ansi
{
    // Lazy-evaluate: check once per process whether colors are supported.
    static readonly bool _enabled = !Console.IsOutputRedirected
        && Environment.GetEnvironmentVariable("NO_COLOR") == null
        && Environment.GetEnvironmentVariable("TERM") != "dumb";

    // -- Foreground colors ----------------------------------------------------─
    public static string Green(string s)   => _enabled ? $"\x1b[92m{s}\x1b[0m" : s;
    public static string Red(string s)     => _enabled ? $"\x1b[91m{s}\x1b[0m" : s;
    public static string Yellow(string s)  => _enabled ? $"\x1b[93m{s}\x1b[0m" : s;
    public static string Cyan(string s)    => _enabled ? $"\x1b[96m{s}\x1b[0m" : s;
    public static string DimCyan(string s) => _enabled ? $"\x1b[36m{s}\x1b[0m" : s;
    public static string Bold(string s)    => _enabled ? $"\x1b[1m{s}\x1b[0m"  : s;
    public static string Dim(string s)     => _enabled ? $"\x1b[2m{s}\x1b[0m"  : s;
    public static string Magenta(string s) => _enabled ? $"\x1b[95m{s}\x1b[0m" : s;

    // -- Composite helpers ----------------------------------------------------─

    /// Format a [MARK] badge: [OK]=green, [MISS]=red, [?]=dim
    public static string Mark(string mark) => mark switch
    {
        "OK"   => _enabled ? $"\x1b[92m[OK]\x1b[0m"   : "[OK]",
        "MISS" => _enabled ? $"\x1b[91m[MISS]\x1b[0m" : "[MISS]",
        _      => _enabled ? $"\x1b[2m[{mark}]\x1b[0m" : $"[{mark}]",
    };

    /// Section header (━━━ CURSOR ━━━...)
    public static string Header(string text) => _enabled ? $"\x1b[36m{text}\x1b[0m" : text;

    /// # TARGET / # FOCUS prefix line
    public static string TargetLine(string text) => _enabled ? $"\x1b[1;96m{text}\x1b[0m" : text;
}
