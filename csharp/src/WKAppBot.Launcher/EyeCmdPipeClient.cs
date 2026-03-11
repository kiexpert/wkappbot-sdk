using System.IO.Pipes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.Launcher;

[JsonSerializable(typeof(string[]))]
internal partial class LauncherJsonContext : JsonSerializerContext { }

/// <summary>Connects to Eye's command pipe and delegates execution (zero cold-start).</summary>
internal static class EyeCmdPipeClient
{
    internal const string PipeName = "WKAppBotCmdPipe";
    internal const string EndMarker = "\x00END";

    /// <summary>
    /// Try to delegate args to the running Eye process.
    /// Returns true + sets exitCode if delegation succeeded.
    /// Returns false if Eye is not running (caller should fall through to RunCore).
    /// </summary>
    public static bool TryDelegate(string[] args, out int exitCode)
    {
        exitCode = 0;
        try
        {
            using var pipe = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut);
            pipe.Connect(300); // 300ms: Eye either answers instantly or isn't running

            var w = new StreamWriter(pipe, leaveOpen: true) { AutoFlush = true };
            var r = new StreamReader(pipe, leaveOpen: true);

            w.WriteLine(JsonSerializer.Serialize(args, LauncherJsonContext.Default.StringArray));

            string? line;
            while ((line = r.ReadLine()) != null)
            {
                if (line.StartsWith(EndMarker))
                {
                    int.TryParse(line.AsSpan(EndMarker.Length).Trim(), out exitCode);
                    break;
                }
                Console.WriteLine(line);
            }
            return true;
        }
        catch { return false; } // Eye not running or busy → caller falls through
    }
}
