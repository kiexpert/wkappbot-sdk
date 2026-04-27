// AppBotEyeGlobalMode.EyeCard.cs -- Think Eye: per-workspace status card overlay management.
// Spawns one `wkappbot eye-card` child per VS Code window discovered via EyeParentCard,
// keeps a status JSON file fresh so the child overlay can poll it.
//
// Tag: [THINK-EYE]

using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WKAppBot.Win32.Native;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Write per-card status JSON using the same format as eye tick output.
    /// Lines are pre-formatted by the same helpers as AppBotEyeCardBuilder.
    /// </summary>
    static void WriteEyeCardStatus(EyeParentCard card)
    {
        if (card == null || string.IsNullOrEmpty(card.Cwd)) return;
        try
        {
            var lines = BuildThinkEyeLines(card);
            var hash = EyeCardHash(card.Cwd);
            var path = Path.Combine(Path.GetTempPath(), $"wkappbot_card_{hash}.json");
            var json = System.Text.Json.JsonSerializer.Serialize(new
            {
                lines,
                ts = DateTime.UtcNow.ToString("O"),
            });
            var tmp = path + ".tmp";
            File.WriteAllText(tmp, json);
            File.Move(tmp, path, overwrite: true);
        }
        catch { /* tolerate transient IO */ }
    }

    /// <summary>Generate display lines identical to eye tick card -- no time conditions.</summary>
    static string[] BuildThinkEyeLines(EyeParentCard card)
    {
        var result = new List<string>();
        var cwdTag = AbbreviateCwd(card.Cwd);
        var header = string.IsNullOrWhiteSpace(cwdTag) ? card.ParentName : FormatSlackDisplayName(card.HostType, cwdTag);
        result.Add(header);

        var (cardCtx, jsonlAge, jsonlPath, jsonlFileSize) = !string.IsNullOrEmpty(card.SessionJsonl) && File.Exists(card.SessionJsonl)
            ? GetContextInfoForJsonl(card.SessionJsonl)
            : GetContextInfoForCwdEx(card.Cwd, card.HostType);
        var ctxTag = cardCtx >= 0
            ? $" ctx={cardCtx}%" + (jsonlFileSize >= 1024 * 1024 ? $" ({jsonlFileSize / 1024 / 1024}MB)" : $" ({jsonlFileSize / 1024}KB)")
            : "";

        // 작업 + 상태 -- always show last known values, no time gate
        if (!string.IsNullOrEmpty(card.LastTag) && card.LastTag != "prompt-discovered")
        {
            result.Add($"작업: {card.LastTag}");
            result.Add($"상태: {card.LastStatus}{ctxTag}");
        }
        else
        {
            result.Add($"상태: 대기중{ctxTag}");
        }

        // 생각 -- latest user/assistant messages (same as eye tick, up to 2 lines)
        var thought = !string.IsNullOrEmpty(card.SessionJsonl) && File.Exists(card.SessionJsonl)
            ? ReadClotThoughtFromJsonl(card.SessionJsonl, card.HostType)
            : ReadClotThoughtForCwd(card.Cwd, card.HostType);
        if (!string.IsNullOrWhiteSpace(thought))
        {
            foreach (var line in thought.Split('\n'))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                result.Add($"생각: {(line.Length > 38 ? line[..38] + "…" : line)}");
            }
        }

        return result.ToArray();
    }

    static string EyeCardHash(string cwd)
    {
        var bytes = MD5.HashData(Encoding.UTF8.GetBytes(cwd.ToLowerInvariant()));
        var sb = new StringBuilder(8);
        for (int i = 0; i < 4; i++) sb.Append(bytes[i].ToString("x2"));
        return sb.ToString();
    }

    /// <summary>
    /// Ensure a single per-workspace eye-card child overlay exists for VS Code-hosted cards.
    /// No-op for non-VSCode hosts. Re-spawn if PID died.
    /// </summary>
    static void EnsureEyeCard(EyeParentCard card)
    {
        try
        {
            if (card == null || string.IsNullOrEmpty(card.Cwd)) return;
            if (string.IsNullOrEmpty(card.HostType) ||
                card.HostType.IndexOf("vscode", StringComparison.OrdinalIgnoreCase) < 0)
                return;

            // ParentPid is overloaded: for prompt-discovered cards it holds an HWND, not a PID.
            // Use CWD folder-name title match (reliable) with PID-based lookup as fallback.
            var hwnd = FindVsCodeHwndByCwd(card.Cwd);
            if (hwnd == IntPtr.Zero) hwnd = FindVsCodeHwndByPid(card.ParentPid);
            if (hwnd == IntPtr.Zero) return;

            // Status update first (cheap; runs even when child is alive)
            WriteEyeCardStatus(card);

            if (_eyeCardPids.TryGetValue(card.Cwd, out var existingPid)
                && existingPid > 0 && IsProcessAlive(existingPid))
                return;

            var exePath = Environment.ProcessPath ?? Path.Combine(AppContext.BaseDirectory, "wkappbot.exe");
            var cwdArg = card.Cwd.Replace("\"", "");
            var spawnArgs = $"eye-card --hwnd 0x{hwnd.ToInt64():X} --cwd \"{cwdArg}\"";
            var spawnCwd = string.IsNullOrEmpty(EyeCallerCwd) ? card.Cwd : EyeCallerCwd;
            var sr = AppBotPipe.Spawn(exePath, spawnArgs,
                cwd: spawnCwd,
                env: new()
                {
                    ["WKAPPBOT_PARENT_PID"] = Environment.ProcessId.ToString(),
                    ["WKAPPBOT_WORKER"] = "1",
                },
                showNoActivate: true,
                caller: "EYE-CARD");
            if (sr != null)
            {
                _eyeCardPids[card.Cwd] = sr.Pid;
                sr.Dispose();
                Console.Error.WriteLine($"[EYE-CARD] spawned pid={sr.Pid} cwd={card.Cwd} hwnd=0x{hwnd.ToInt64():X}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[EYE-CARD] EnsureEyeCard error: {ex.Message}");
        }
    }

    /// <summary>Find VS Code window matching CWD leaf folder name in its title bar.</summary>
    static IntPtr FindVsCodeHwndByCwd(string cwd)
    {
        if (string.IsNullOrEmpty(cwd)) return IntPtr.Zero;
        var leaf = Path.GetFileName(cwd.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
        if (string.IsNullOrEmpty(leaf)) return IntPtr.Zero;
        var pattern = $" - {leaf} - ";
        IntPtr found = IntPtr.Zero;
        var sbCls = new StringBuilder(128);
        var sbTitle = new StringBuilder(512);
        NativeMethods.EnumWindows((hwnd, _) =>
        {
            if (!NativeMethods.IsWindowVisible(hwnd)) return true;
            NativeMethods.GetClassNameW(hwnd, sbCls, sbCls.Capacity);
            if (!sbCls.ToString().StartsWith("Chrome_WidgetWin", StringComparison.OrdinalIgnoreCase)) return true;
            NativeMethods.GetWindowTextW(hwnd, sbTitle, sbTitle.Capacity);
            var title = sbTitle.ToString();
            if (title.IndexOf("Visual Studio Code", StringComparison.OrdinalIgnoreCase) < 0) return true;
            if (title.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) < 0) return true;
            found = hwnd;
            return false;
        }, IntPtr.Zero);
        return found;
    }

    /// <summary>Find a top-level VS Code window whose process equals the given PID (or its ancestor/descendant via WidgetWin class match).</summary>
    static IntPtr FindVsCodeHwndByPid(int pid)
    {
        if (pid <= 0) return IntPtr.Zero;
        IntPtr found = IntPtr.Zero;
        var sbCls = new StringBuilder(128);
        var sbTitle = new StringBuilder(256);
        try
        {
            NativeMethods.EnumWindows((hwnd, _) =>
            {
                NativeMethods.GetWindowThreadProcessId(hwnd, out uint wpid);
                if ((int)wpid != pid) return true;
                if (!NativeMethods.IsWindowVisible(hwnd)) return true;
                NativeMethods.GetClassNameW(hwnd, sbCls, sbCls.Capacity);
                var cls = sbCls.ToString();
                if (!cls.StartsWith("Chrome_WidgetWin", StringComparison.OrdinalIgnoreCase))
                    return true;
                NativeMethods.GetWindowTextW(hwnd, sbTitle, sbTitle.Capacity);
                var title = sbTitle.ToString();
                if (title.IndexOf("Visual Studio Code", StringComparison.OrdinalIgnoreCase) < 0)
                    return true;
                found = hwnd;
                return false;
            }, IntPtr.Zero);
        }
        catch { }
        return found;
    }

    static string BuildEyeCardTag(string cwd)
    {
        if (string.IsNullOrEmpty(cwd)) return "[?]";
        var leaf = cwd.Replace('\\', '/').TrimEnd('/');
        var idx = leaf.LastIndexOf('/');
        if (idx >= 0) leaf = leaf.Substring(idx + 1);
        // Mirror existing CWD shorthand convention in CLAUDE.md (e.g. "WG-WKAppBot")
        return $"[WG-{leaf}]";
    }

    static string BuildEyeCardEmoji(string status)
    {
        if (string.IsNullOrEmpty(status)) return "·";
        var s = status.ToLowerInvariant();
        if (s.Contains("error") || s.Contains("fail")) return "!";
        if (s.Contains("idle") || s.Contains("done")) return "·";
        if (s.Contains("wait") || s.Contains("block")) return "?";
        return ">";
    }

    /// <summary>Periodic maintenance: ensure cards, prune dead PIDs.</summary>
    static void TickEyeCards()
    {
        try
        {
            foreach (var card in _cachedCards.ToList())
                EnsureEyeCard(card);
            foreach (var k in _eyeCardPids.Keys.ToList())
                if (!IsProcessAlive(_eyeCardPids[k]))
                    _eyeCardPids.Remove(k);
        }
        catch { }
    }
}
