// wkwrap.cs -- source for wkwrap.exe, the real-PE shim of the wkwrap universal
// symlink-resolver (skill: wkwrap). A PE is needed because .NET
// Process.Start(UseShellExecute=false) cannot launch a .cmd/.ps1 directly, so any
// caller that spawns a tool by name needs a true executable on the path. This exe
// is a thin forwarder: it derives its OWN invocation basename (e.g. "codex" when
// invoked via a codex.exe copy/symlink) and hands it + all args to wkwrap.ps1,
// inheriting stdio and the child's exit code. Build (no csproj, single file):
//   C:/Windows/Microsoft.NET/Framework64/v4.0.30319/csc.exe /nologo /target:exe \
//     /out:wkwrap.exe wkwrap.cs
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

internal static class WkWrap
{
    private static int Main(string[] args)
    {
        // Real on-disk location of this exe -> where wkwrap.ps1 lives beside it.
        string realExe = Process.GetCurrentProcess().MainModule.FileName;
        string dir = Path.GetDirectoryName(realExe);
        string ps1 = Path.Combine(dir, "wkwrap.ps1");

        // Invocation name (argv0) preserves the symlink/copy name (e.g. codex.exe);
        // fall back to the real exe name if argv0 is unavailable.
        string invoked = Environment.GetCommandLineArgs().Length > 0
            ? Environment.GetCommandLineArgs()[0]
            : realExe;
        string self = Path.GetFileNameWithoutExtension(
            string.IsNullOrEmpty(invoked) ? realExe : invoked);

        var sb = new StringBuilder();
        sb.Append("-NoProfile -NonInteractive -ExecutionPolicy Bypass -File ");
        sb.Append(Quote(ps1));
        sb.Append(' ').Append(Quote(self));
        foreach (string a in args)
        {
            sb.Append(' ').Append(Quote(a));
        }

        var psi = new ProcessStartInfo("powershell.exe", sb.ToString())
        {
            UseShellExecute = false // inherit stdin/stdout/stderr = passthrough
        };

        try
        {
            using (Process p = Process.Start(psi))
            {
                p.WaitForExit();
                return p.ExitCode;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("[wkwrap.exe] failed to launch wkwrap.ps1: " + ex.Message);
            return 127;
        }
    }

    // Minimal CommandLineToArgvW-compatible quoting.
    private static string Quote(string s)
    {
        if (s.Length > 0 && s.IndexOfAny(new[] { ' ', '\t', '"' }) < 0)
        {
            return s;
        }
        return "\"" + s.Replace("\"", "\\\"") + "\"";
    }
}
