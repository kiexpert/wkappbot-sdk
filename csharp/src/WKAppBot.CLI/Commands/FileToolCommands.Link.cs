using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    // file link <target> <linkname>   -- hard link (same volume, files only)
    // file symlink <target> <linkname> -- symbolic link (files or directories)
    static int FileLinkCommand(string[] args, bool symbolic)
    {
        string? target = null, linkName = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] is "--target" or "--src" && i + 1 < args.Length) target = args[++i];
            else if (args[i] is "--link" or "--dest" or "--name" && i + 1 < args.Length) linkName = args[++i];
            else if (!args[i].StartsWith('-')) { (target, linkName) = target == null ? (args[i], linkName) : (target, args[i]); }
        }
        if (target == null || linkName == null)
        {
            Console.Error.WriteLine($"Usage: wkappbot file {(symbolic ? "symlink" : "link")} <target> <linkname>");
            Console.Error.WriteLine(symbolic
                ? "  Creates a symbolic link. Works for files and directories."
                : "  Creates a hard link. Files only, same volume.");
            return 1;
        }
        target = Path.GetFullPath(target, FileDefaultDir());
        linkName = Path.GetFullPath(linkName, FileDefaultDir());
        if (File.Exists(linkName) || Directory.Exists(linkName))
        {
            Console.Error.WriteLine($"[FILE:{(symbolic ? "SYMLINK" : "LINK")}] ERROR: link path already exists: {linkName}");
            return 1;
        }
        try
        {
            if (symbolic)
            {
                FileSystemInfo info = Directory.Exists(target)
                    ? Directory.CreateSymbolicLink(linkName, target)
                    : File.CreateSymbolicLink(linkName, target);
                Console.WriteLine($"[FILE:SYMLINK] {info.FullName} -> {target}");
            }
            else
            {
                // Hard link via P/Invoke (no .NET 8 BCL wrapper for CreateHardLinkW)
                if (!NativeMethods.CreateHardLink(linkName, target, IntPtr.Zero))
                {
                    int err = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                    Console.Error.WriteLine($"[FILE:LINK] CreateHardLink failed: Win32 error {err} (may need same volume or admin rights)");
                    return 1;
                }
                Console.WriteLine($"[FILE:LINK] {linkName} => {target} (hard link)");
            }
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[FILE:{(symbolic ? "SYMLINK" : "LINK")}] {ex.GetType().Name}: {ex.Message}");
            return 1;
        }
    }
}
