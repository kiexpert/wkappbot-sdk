using System.Text;

namespace WKAppBot.Launcher;

partial class Program
{
    // -- Encoding recovery ----------------------------------------------------
    /// <summary>
    /// Recover Korean (and other multibyte) args corrupted by bash/MSYS2.
    /// GetCommandLineA() -> raw system-codepage bytes -> decode with system encoding.
    /// Compare each arg: if Unicode version has replacement chars (U+FFFD) or
    /// differs from ANSI-decoded version, use the ANSI version.
    /// </summary>
    static string[] TryRecoverEncodingFromAnsiCommandLine(string[] args)
    {
        try
        {
            var ansiPtr = GetCommandLineA();
            if (ansiPtr == IntPtr.Zero) return args;

            // Read raw ANSI command line as bytes
            var ansiStr = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ansiPtr);
            if (string.IsNullOrEmpty(ansiStr)) return args;

            // Parse ANSI command line into args (same logic as Windows CommandLineToArgvW but for ANSI)
            var ansiArgs = ParseCommandLineArgs(ansiStr);
            if (ansiArgs.Length == 0) return args;

            // Skip argv[0] (exe path) -- compare only actual args
            var unicodeAll = Environment.GetCommandLineArgs();
            if (unicodeAll.Length <= 1) return args;

            var unicodeArgs = unicodeAll[1..]; // skip exe
            if (ansiArgs.Length <= 0) return args;

            // Also decode using system codepage for comparison
            // Marshal.PtrToStringAnsi uses system ANSI codepage (CP949 on Korean Windows)
            bool anyRecovered = false;
            var result = new string[args.Length];
            Array.Copy(args, result, args.Length);

            for (int i = 0; i < result.Length && i < ansiArgs.Length; i++)
            {
                var uArg = result[i];
                var aArg = ansiArgs[i];

                // Check if Unicode version has corruption indicators
                bool hasReplacementChar = uArg.Contains('\uFFFD');
                bool hasMojibake = false;

                // Detect common mojibake: high bytes in Latin range that shouldn't be there
                // e.g. "í\u0085\u008c스í\u008a\u00b8" instead of "테스트"
                if (!hasReplacementChar && uArg.Length > 0)
                {
                    int suspiciousChars = 0;
                    foreach (var c in uArg)
                    {
                        if (c >= 0x80 && c <= 0xFF) suspiciousChars++;
                    }
                    // If >30% of chars are in the suspicious range, likely mojibake
                    hasMojibake = suspiciousChars > 0 && (double)suspiciousChars / uArg.Length > 0.3;
                }

                if ((hasReplacementChar || hasMojibake) && aArg != uArg && aArg.Length > 0)
                {
                    result[i] = aArg;
                    anyRecovered = true;
                    Console.Error.WriteLine($"[ENCODING] Recovered arg[{i}]: \"{uArg}\" -> \"{aArg}\" (system codepage)");
                }
            }

            if (anyRecovered)
                Console.Error.WriteLine($"[ENCODING] {result.Length} arg(s) checked, recovery applied (codepage={GetConsoleCP()})");

            return result;
        }
        catch
        {
            return args; // any failure -> use original args
        }
    }

    /// <summary>Simple command line parser (handles quoted args).</summary>
    static string[] ParseCommandLineArgs(string cmdLine)
    {
        var args = new System.Collections.Generic.List<string>();
        int i = 0;

        // Skip executable path (first arg)
        if (i < cmdLine.Length)
        {
            if (cmdLine[i] == '"')
            {
                i++; while (i < cmdLine.Length && cmdLine[i] != '"') i++; i++; // skip closing quote
            }
            else
            {
                while (i < cmdLine.Length && cmdLine[i] != ' ') i++;
            }
            while (i < cmdLine.Length && cmdLine[i] == ' ') i++; // skip spaces
        }

        // Parse remaining args
        while (i < cmdLine.Length)
        {
            if (cmdLine[i] == '"')
            {
                i++; // skip opening quote
                int start = i;
                while (i < cmdLine.Length && cmdLine[i] != '"') i++;
                args.Add(cmdLine[start..i]);
                if (i < cmdLine.Length) i++; // skip closing quote
            }
            else
            {
                int start = i;
                while (i < cmdLine.Length && cmdLine[i] != ' ') i++;
                args.Add(cmdLine[start..i]);
            }
            while (i < cmdLine.Length && cmdLine[i] == ' ') i++;
        }

        return args.ToArray();
    }
}
