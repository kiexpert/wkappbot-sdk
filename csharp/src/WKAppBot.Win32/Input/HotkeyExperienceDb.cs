using System.Text.Json;
using WKAppBot.Win32.Accessibility;

namespace WKAppBot.Win32.Input;

/// <summary>
/// 앱별 핫키 경험 DB -- 누적 캐시.
/// - 첫 접근 시 풀스캔 -> DB에 누적 (기존 항목 유지, 새것만 추가)
/// - 발동 전 Verify -> 스태일 항목은 DB에서 제거
/// - 검증 통과한 항목만 발동
/// - EXE 버전 키: 앱 업데이트 시 자동 무효화 (WM_COMMAND ItemId 변경 대응)
/// 위치: wkappbot.hq/hotkeys/<processName>.hotkeys.json
/// </summary>
public static class HotkeyExperienceDb
{
    static readonly string HqDir = Path.Combine(
        Path.GetDirectoryName(Environment.ProcessPath) ?? ".", "wkappbot.hq", "hotkeys");

    static readonly Dictionary<string, List<HotkeyDbEntry>> _cache =
        new(StringComparer.OrdinalIgnoreCase);
    // version cache: processName -> last-known EXE version used when DB was loaded/written
    static readonly Dictionary<string, string> _versionCache =
        new(StringComparer.OrdinalIgnoreCase);
    static readonly HashSet<string> _sessionScanned =
        new(StringComparer.OrdinalIgnoreCase);

    static readonly JsonSerializerOptions _jsonOpts = new() { WriteIndented = true };

    // -- EXE version helper ----------------------------------------

    /// <summary>
    /// PID -> EXE FileVersion string (e.g. "120.0.6099.71").
    /// Returns null on any failure (access denied, no MainModule, etc.).
    /// </summary>
    public static string? GetExeVersion(uint pid)
    {
        try
        {
            var proc = System.Diagnostics.Process.GetProcessById((int)pid);
            var fn = proc.MainModule?.FileName;
            if (string.IsNullOrEmpty(fn)) return null;
            return System.Diagnostics.FileVersionInfo.GetVersionInfo(fn).FileVersion;
        }
        catch { return null; }
    }

    // -- Load / Merge / Persist ------------------------------------

    public static string GetDbPath(string processName) =>
        Path.Combine(HqDir, $"{processName}.hotkeys.json");

    /// <summary>
    /// DB 로드 (메모리 캐시 우선). 없으면 빈 리스트.
    /// exeVersion이 제공되고 파일 버전과 불일치하면 캐시 무효화 -> 재스캔 유도.
    /// </summary>
    public static List<HotkeyDbEntry> Load(string processName, string? exeVersion = null)
    {
        // Memory cache hit -- but check version invalidation first
        if (_cache.TryGetValue(processName, out var cached))
        {
            if (exeVersion != null &&
                _versionCache.TryGetValue(processName, out var cachedVer) &&
                !string.Equals(cachedVer, exeVersion, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[HOTKEY-DB] Version changed ({cachedVer} -> {exeVersion}) -- invalidating '{processName}' cache");
                _cache.Remove(processName);
                _versionCache.Remove(processName);
                _sessionScanned.Remove(processName);
                // Fall through to file load
            }
            else
                return cached;
        }

        var path = GetDbPath(processName);
        if (File.Exists(path))
        {
            try
            {
                var file = JsonSerializer.Deserialize<HotkeyDbFile>(
                    File.ReadAllText(path), _jsonOpts);
                var entries = file?.Entries ?? [];
                var storedVer = file?.ExeVersion;

                // Version mismatch -> discard file contents, start fresh
                if (exeVersion != null && storedVer != null &&
                    !string.Equals(storedVer, exeVersion, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"[HOTKEY-DB] EXE updated ({storedVer} -> {exeVersion}) -- discarding '{processName}' DB (will rescan)");
                    entries = [];
                }
                else
                    Console.WriteLine($"[HOTKEY-DB] Loaded {entries.Count} <- {Path.GetFileName(path)}{(storedVer != null ? $" v{storedVer}" : "")}");

                _cache[processName] = entries;
                if (exeVersion != null) _versionCache[processName] = exeVersion;
                return entries;
            }
            catch (Exception ex) { Console.WriteLine($"[HOTKEY-DB] Load error: {ex.Message}"); }
        }

        var empty = new List<HotkeyDbEntry>();
        _cache[processName] = empty;
        if (exeVersion != null) _versionCache[processName] = exeVersion;
        return empty;
    }

    /// <summary>
    /// 누적 머지 -- 기존 레이블은 유지, 새 항목만 추가.
    /// 추가된 수를 반환.
    /// </summary>
    public static int Merge(string processName, IEnumerable<HotkeyDbEntry> newEntries, string? exeVersion = null)
    {
        var existing = Load(processName, exeVersion);
        var existingLabels = existing
            .Select(e => e.Label)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        int added = 0;
        foreach (var e in newEntries)
        {
            if (existingLabels.Add(e.Label)) { existing.Add(e); added++; }
        }
        if (added > 0) Persist(processName, existing, exeVersion);
        return added;
    }

    /// <summary>스태일 항목 제거 후 영구 저장.</summary>
    public static void RemoveStale(string processName, IEnumerable<string> staleLabels, string? exeVersion = null)
    {
        var existing = Load(processName, exeVersion);
        var staleSet = staleLabels.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var removed = existing.RemoveAll(e => staleSet.Contains(e.Label));
        if (removed > 0)
        {
            Persist(processName, existing, exeVersion);
            Console.WriteLine($"[HOTKEY-DB] Removed {removed} stale entries from '{processName}'");
        }
    }

    static void Persist(string processName, List<HotkeyDbEntry> entries, string? exeVersion = null)
    {
        // Resolve version from memory cache if not provided
        if (exeVersion == null) _versionCache.TryGetValue(processName, out exeVersion);
        Directory.CreateDirectory(HqDir);
        var file = new HotkeyDbFile(exeVersion, entries);
        File.WriteAllText(GetDbPath(processName),
            JsonSerializer.Serialize(file, _jsonOpts));
    }

    // -- Session scan tracking ------------------------------------─

    public static bool IsSessionScanned(string processName) =>
        _sessionScanned.Contains(processName);

    public static void MarkSessionScanned(string processName) =>
        _sessionScanned.Add(processName);

    // -- Match ----------------------------------------------------

    /// <summary>
    /// grap 패턴으로 검색 -- 커버리지 최고(가장 짧은 레이블) 우선.
    /// exeVersion이 제공되면 Load 시 버전 검증 포함.
    /// </summary>
    public static HotkeyDbEntry? Match(string processName, string pattern, string? exeVersion = null)
    {
        var entries = Load(processName, exeVersion);
        if (entries.Count == 0) return null;
        var matcher = PatternMatcher.Create(pattern);
        return entries
            .Where(e => matcher.IsMatch(e.Label))
            .OrderBy(e => e.Label.Length)
            .FirstOrDefault();
    }
}

/// <summary>핫키 DB 파일 래퍼 -- 버전 + 엔트리 리스트.</summary>
public record HotkeyDbFile(
    string? ExeVersion,         // EXE FileVersion at scan time -- null = legacy file (no version)
    List<HotkeyDbEntry> Entries
);

/// <summary>핫키 DB 엔트리 -- 레이블 + 발동 방법.</summary>
public record HotkeyDbEntry(
    string  Label,      // 표시 레이블 (& 제거)
    string  Source,     // "win32_ctrl" | "win32_menu" | "uia" | "cdp"
    string  Method,     // "menu_cmd" | "ctrl_activate" | "shortcut"
    uint    ItemId,     // menu_cmd: WM_COMMAND itemId
    string? Shortcut,   // shortcut: "Ctrl+S", "Alt+F" 등
    string? CtrlClass   // ctrl_activate: 컨트롤 클래스명
);
