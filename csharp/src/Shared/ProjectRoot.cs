// ProjectRoot.cs -- per-repo identity helper, shared between WKAppBot.CLI and WKAppBot.Launcher.
// Linked via csproj: <Compile Include="..\Shared\ProjectRoot.cs" Link="Shared\ProjectRoot.cs" />
//
// Multi-repo deployment: a single Windows host may host several cloned repos
// of WKAppBot at once. Each repo gets its own data directory (.wkappbot/hq/)
// and its own pair of named pipes (wkappbot_elevated_<hash>, wkappbot_eye_ipc_<hash>).
// The 8-character hash is derived from the canonicalized project root path so
// two clones at different paths never collide on pipe names or HQ data.
//
// Both CLI (managed) and Launcher (AOT, no CLI reference) use this; keep the
// implementation dependency-free (no Newtonsoft, no Core types, no logging).

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WKAppBot.CLI;

/// <summary>Locates the project root and computes a per-repo identity hash.</summary>
internal static class ProjectRoot
{
    private static string? _root;
    private static string? _hash8;

    /// <summary>
    /// Finds the project root by walking up from startPath (or CurrentDirectory if null)
    /// looking for .git, .wkappbot, or CLAUDE.md markers. Falls back to exe directory.
    /// </summary>
    public static string Find(string? startPath = null)
    {
        if (_root != null) return _root;
        _root = Detect(startPath ?? Directory.GetCurrentDirectory())
             ?? Detect(Path.GetDirectoryName(AppContext.BaseDirectory) ?? ".")
             ?? Path.GetDirectoryName(Environment.ProcessPath ?? ".") ?? ".";
        return _root;
    }

    /// <summary>8-character MD5 prefix of the canonicalized project root path.</summary>
    public static string Hash8()
    {
        if (_hash8 != null) return _hash8;
        var root = Find();
        var bytes = MD5.HashData(Encoding.UTF8.GetBytes(root.ToLowerInvariant()));
        _hash8 = Convert.ToHexString(bytes)[..8].ToLowerInvariant();
        return _hash8;
    }

    private static string? Detect(string startPath)
    {
        var dir = new DirectoryInfo(startPath);
        while (dir != null)
        {
            if (dir.GetFileSystemInfos(".git").Length > 0
             || dir.GetFileSystemInfos(".wkappbot").Length > 0
             || dir.GetFiles("CLAUDE.md").Length > 0)
                return dir.FullName;
            dir = dir.Parent;
        }
        return null;
    }
}
