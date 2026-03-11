namespace WKAppBot.CLI;

/// <summary>Public entry point for WKAppBot.Launcher's fallback DLL/EXE invocation.</summary>
public static class CoreEntry
{
    public static int Run(string[] args) => Program.Main(args);
}
