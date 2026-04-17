// AppBotPipe.HotSwap.cs -- Hot-swap staging/promotion entry points for AppBotPipe.
// Linked via csproj: <Compile Include="..\..\Shared\AppBotPipe.HotSwap.cs" Link="Shared\AppBotPipe.HotSwap.cs" />
//
//   AppBotPipe.PromoteNewExeIfPending(exePath) -- rename <exe>.new.exe -> <exe>
//   AppBotPipe.TryRenameSwap(exePath)          -- same, returns whether a swap happened
//
// Both Launcher and Core need this: Launcher promotes Core before spawn
// (pre-UAC path), Core promotes itself before restart (Eye hot-swap).

using System.IO;

internal static partial class AppBotPipe
{
    /// <summary>
    /// If <paramref name="exePath"/>.new.exe exists, rename it over <paramref name="exePath"/>.
    /// Returns true if a rename happened (caller should re-resolve the exe path if it cached it).
    /// Silently returns false on any failure -- caller proceeds with the existing binary.
    /// </summary>
    public static bool PromoteNewExeIfPending(string exePath)
    {
        try
        {
            if (string.IsNullOrEmpty(exePath)) return false;
            var dir = Path.GetDirectoryName(exePath) ?? ".";
            var name = Path.GetFileNameWithoutExtension(exePath);
            var ext = Path.GetExtension(exePath);
            var newPath = Path.Combine(dir, name + ".new" + ext);
            if (!File.Exists(newPath)) return false;

            // Windows cannot replace a locked file. Best-effort: delete existing first.
            if (File.Exists(exePath))
            {
                try { File.Delete(exePath); } catch { return false; }
            }
            File.Move(newPath, exePath);
            return true;
        }
        catch { return false; }
    }

    /// <summary>
    /// Alias for <see cref="PromoteNewExeIfPending(string)"/>. Preserved for callers
    /// that historically used this name (e.g. hack workers).
    /// </summary>
    public static bool TryRenameSwap(string exePath) => PromoteNewExeIfPending(exePath);
}
