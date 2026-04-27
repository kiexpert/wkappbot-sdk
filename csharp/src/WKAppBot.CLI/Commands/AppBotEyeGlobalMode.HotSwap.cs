namespace WKAppBot.CLI;

// Reusable hot-swap (rename-swap) helper.
// Called from: Eye main loop, Eye startup gentle-swap, `wkappbot hotswap` command.
// Thread-safe: callers serialize externally (Eye uses _fswExeDirty flag).
// Extracted from AppBotEyeGlobalMode.cs to keep the root partial under the 400-line cap.
internal partial class Program
{
    internal enum HotSwapResult { NoNewExe, Identical, Swapped, Failed }

    /// <summary>
    /// Check for {exeName}.new.exe next to exePath and apply rename-swap.
    /// Returns: NoNewExe (nothing to do), Identical (deleted, no-op), Swapped (success), Failed (rollback).
    /// All steps logged to stdout for transparency.
    /// </summary>
    internal static HotSwapResult TryRenameSwap(string exePath, string tag = "HOT-SWAP")
    {
        if (string.IsNullOrEmpty(exePath) || !File.Exists(exePath))
            return HotSwapResult.NoNewExe;

        var exeDir = Path.GetDirectoryName(exePath) ?? "";
        var exeName = Path.GetFileNameWithoutExtension(exePath);
        var newExePath = Path.Combine(exeDir, $"{exeName}.new.exe");

        if (!File.Exists(newExePath))
            return HotSwapResult.NoNewExe;

        var newInfo = new FileInfo(newExePath);
        var curInfo = new FileInfo(exePath);

        // Identical check: same mtime + size = no actual rebuild -> delete .new.exe
        if (newInfo.LastWriteTimeUtc == curInfo.LastWriteTimeUtc && newInfo.Length == curInfo.Length)
        {
            Console.Error.WriteLine($"[{tag}] .new.exe identical (mtime={newInfo.LastWriteTimeUtc:HH:mm:ss}, size={newInfo.Length}) -- deleting");
            try { File.Delete(newExePath); } catch { }
            return HotSwapResult.Identical;
        }

        Console.Error.WriteLine($"[{tag}] .new.exe detected (new={newInfo.Length}b/{newInfo.LastWriteTimeUtc:HH:mm:ss}, cur={curInfo.Length}b/{curInfo.LastWriteTimeUtc:HH:mm:ss}) -- rename-swap");

        // Step 1: running exe -> .old (Windows allows renaming running exe)
        var oldExePath = Path.Combine(exeDir, $"{exeName}.old-{DateTime.Now:yyyyMMdd-HHmm}.exe");
        bool step1 = false;
        try { File.Move(exePath, oldExePath); step1 = true; }
        catch (Exception ex) { Console.Error.WriteLine($"[{tag}] step1 rename->.old failed: {ex.Message}"); }

        // Step 2: .new.exe -> exe (retry up to 4× for deploy file lock)
        bool step2 = false;
        if (step1)
        {
            for (int r = 0; r < 4 && !step2; r++)
            {
                if (r > 0) { Console.Error.WriteLine($"[{tag}] step2 retry {r}/3 (file locked -- waiting 1s)"); Thread.Sleep(1000); }
                try { File.Move(newExePath, exePath); step2 = true; }
                catch (Exception ex) { if (r == 3) Console.Error.WriteLine($"[{tag}] step2 .new->.exe failed: {ex.Message}"); }
            }
            if (!step2)
            {
                Console.Error.WriteLine($"[{tag}] rollback: .old->.exe");
                try { File.Move(oldExePath, exePath); } catch { }
            }
        }

        if (step1 && step2)
        {
            Console.Error.WriteLine($"[{tag}] swap OK (.exe->{Path.GetFileName(oldExePath)}, .new->.exe)");
            return HotSwapResult.Swapped;
        }
        return HotSwapResult.Failed;
    }
}
