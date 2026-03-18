using System.Text.Json;
using WKAppBot.Win32.Accessibility;

namespace WKAppBot.Win32.Input;

/// <summary>
/// 앱별 핫키 경험 DB — 누적 캐시.
/// - 첫 접근 시 풀스캔 → DB에 누적 (기존 항목 유지, 새것만 추가)
/// - 발동 전 Verify → 스태일 항목은 DB에서 제거
/// - 검증 통과한 항목만 발동
/// 위치: wkappbot.hq/hotkeys/<processName>.hotkeys.json
/// </summary>
public static class HotkeyExperienceDb
{
    static readonly string HqDir = Path.Combine(
        Path.GetDirectoryName(Environment.ProcessPath) ?? ".", "wkappbot.hq", "hotkeys");

    static readonly Dictionary<string, List<HotkeyDbEntry>> _cache =
        new(StringComparer.OrdinalIgnoreCase);
    static readonly HashSet<string> _sessionScanned =
        new(StringComparer.OrdinalIgnoreCase);

    static readonly JsonSerializerOptions _jsonOpts = new() { WriteIndented = true };

    // ── Load / Merge / Persist ────────────────────────────────────

    public static string GetDbPath(string processName) =>
        Path.Combine(HqDir, $"{processName}.hotkeys.json");

    /// <summary>DB 로드 (메모리 캐시 우선). 없으면 빈 리스트.</summary>
    public static List<HotkeyDbEntry> Load(string processName)
    {
        if (_cache.TryGetValue(processName, out var cached)) return cached;
        var path = GetDbPath(processName);
        if (File.Exists(path))
        {
            try
            {
                var entries = JsonSerializer.Deserialize<List<HotkeyDbEntry>>(
                    File.ReadAllText(path), _jsonOpts) ?? [];
                _cache[processName] = entries;
                Console.WriteLine($"[HOTKEY-DB] Loaded {entries.Count} ← {Path.GetFileName(path)}");
                return entries;
            }
            catch (Exception ex) { Console.WriteLine($"[HOTKEY-DB] Load error: {ex.Message}"); }
        }
        var empty = new List<HotkeyDbEntry>();
        _cache[processName] = empty;
        return empty;
    }

    /// <summary>
    /// 누적 머지 — 기존 레이블은 유지, 새 항목만 추가.
    /// 추가된 수를 반환.
    /// </summary>
    public static int Merge(string processName, IEnumerable<HotkeyDbEntry> newEntries)
    {
        var existing = Load(processName);
        var existingLabels = existing
            .Select(e => e.Label)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        int added = 0;
        foreach (var e in newEntries)
        {
            if (existingLabels.Add(e.Label)) { existing.Add(e); added++; }
        }
        if (added > 0) Persist(processName, existing);
        return added;
    }

    /// <summary>스태일 항목 제거 후 영구 저장.</summary>
    public static void RemoveStale(string processName, IEnumerable<string> staleLabels)
    {
        var existing = Load(processName);
        var staleSet = staleLabels.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var removed = existing.RemoveAll(e => staleSet.Contains(e.Label));
        if (removed > 0)
        {
            Persist(processName, existing);
            Console.WriteLine($"[HOTKEY-DB] Removed {removed} stale entries from '{processName}'");
        }
    }

    static void Persist(string processName, List<HotkeyDbEntry> entries)
    {
        Directory.CreateDirectory(HqDir);
        File.WriteAllText(GetDbPath(processName),
            JsonSerializer.Serialize(entries, _jsonOpts));
    }

    // ── Session scan tracking ─────────────────────────────────────

    public static bool IsSessionScanned(string processName) =>
        _sessionScanned.Contains(processName);

    public static void MarkSessionScanned(string processName) =>
        _sessionScanned.Add(processName);

    // ── Match ────────────────────────────────────────────────────

    /// <summary>
    /// grap 패턴으로 검색 — 커버리지 최고(가장 짧은 레이블) 우선.
    /// </summary>
    public static HotkeyDbEntry? Match(string processName, string pattern)
    {
        var entries = Load(processName);
        if (entries.Count == 0) return null;
        var matcher = PatternMatcher.Create(pattern);
        return entries
            .Where(e => matcher.IsMatch(e.Label))
            .OrderBy(e => e.Label.Length)
            .FirstOrDefault();
    }
}

/// <summary>핫키 DB 엔트리 — 레이블 + 발동 방법.</summary>
public record HotkeyDbEntry(
    string  Label,      // 표시 레이블 (& 제거)
    string  Source,     // "win32_ctrl" | "win32_menu" | "uia" | "cdp"
    string  Method,     // "menu_cmd" | "ctrl_activate" | "shortcut"
    uint    ItemId,     // menu_cmd: WM_COMMAND itemId
    string? Shortcut,   // shortcut: "Ctrl+S", "Alt+F" 등
    string? CtrlClass   // ctrl_activate: 컨트롤 클래스명
);
