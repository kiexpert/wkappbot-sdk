namespace WKAppBot.CLI;

internal partial class Program
{
    static int WhisperPhonemeLoopCommand(string[] args)
    {
        Console.Error.WriteLine("[PHONLOOP] disabled in stable restore build");
        Console.Error.WriteLine("[PHONLOOP] missing helper pipeline was excluded to recover stable build");
        return 0;
    }
}
