// AppBotEyePromptInfo.cs — Per-prompt-window CWD/display-name/last-output resolution.
// Single source of truth for all "hwnd → who is this? what are they doing?" queries.
//
// Key API:
//   GetPromptDisplayInfo(hwnd)  → (displayName, lastLine, cwdLabel)
//   GetLastOutputLine(text)     → last meaningful line from JSONL text
//   ReadClotThoughtForCwd(cwd)  → most recent assistant output from JSONL session
//   ExtractCwdFromVsCodeTitle() → "... - WKAppBot - Visual Studio Code" → W:\GitHub\WKAppBot
//   GetContextInfoForCwdEx()    → JSONL size → context usage %

// FlaUI removed from Eye — ExtractCwdFromCodexWindow routed through MCP
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── JSONL last-output cache ──────────────────────────────────────────────
    // Key: CWD path. Value: (file, preview, fileLen, fileMtime)
    static readonly Dictionary<string, (string file, string? preview, long len, DateTime mtime)>
        _clotThoughtCache = new();

    // ── VS Code title → CWD ─────────────────────────────────────────────────

    /// <summary>
    /// Extract full CWD path from VS Code window title.
    /// Pattern: "filename - FolderName - Visual Studio Code"
    ///          "# Heading — FolderName - Visual Studio Code"
    /// Strategy: strip VSCode suffix → take last " - " segment → search known roots.
    /// Fallback: reverse-map from ~/.claude/projects/ dir names.
    /// </summary>
    static string? ExtractCwdFromVsCodeTitle(string title)
    {
        var vscIdx = title.IndexOf(" - Visual Studio Code", StringComparison.OrdinalIgnoreCase);
        if (vscIdx < 0) return null;
        var withoutSuffix = title[..vscIdx];
        var lastDash = withoutSuffix.LastIndexOf(" - ", StringComparison.Ordinal);
        var folderName = lastDash >= 0 ? withoutSuffix[(lastDash + 3)..].Trim() : withoutSuffix.Trim();
        if (string.IsNullOrEmpty(folderName)) return null;
        return ResolveProjectFolderToPath(folderName);
    }

    // ── Codex window CWD extraction ─────────────────────────────────────────

    /// <summary>
    /// Extract project folder name from Codex (OpenAI) desktop window UIA tree.
    ///
    /// [KNOWHOW] Codex CWD 추출 방법:
    ///   Codex는 Electron 앱 — 타이틀은 항상 "Codex" (폴더 정보 없음).
    ///   UIA inspect 결과: aid="app-header-portal-main" 그룹 내 두 번째 Button이
    ///   현재 열린 프로젝트명을 가짐. 예: [Button] "WKAppBot".
    ///   이 버튼 텍스트 → searchRoots에서 실제 폴더 경로로 역매핑.
    ///
    ///   UIA path: RootWebArea → Group[aid=app-header-portal-main] → Button[1] (project name)
    ///   (Button[0]은 스레드 제목, Button[1]이 프로젝트명)
    /// </summary>
    static string? ExtractCwdFromCodexWindow(IntPtr hwnd)
    {
        try
        {
            // Route through MCP to avoid FlaUI loading in Eye
            var grap = $"*hwnd={hwnd.ToInt64():X}*#app-header-portal-main";
            var (output, code) = EyeMcpClient.CallAsync(
                ["a11y", "inspect", grap, "--depth", "3"], timeoutMs: 5_000).GetAwaiter().GetResult();
            if (code != 0 || string.IsNullOrWhiteSpace(output)) return null;

            // Parse inspect output for Button name (project folder)
            foreach (var line in output.Split('\n'))
            {
                if (!line.Contains("[Button]", StringComparison.OrdinalIgnoreCase)) continue;
                var q1 = line.IndexOf('"');
                var q2 = q1 >= 0 ? line.IndexOf('"', q1 + 1) : -1;
                if (q1 >= 0 && q2 > q1)
                {
                    var folderName = line.Substring(q1 + 1, q2 - q1 - 1);
                    if (!string.IsNullOrWhiteSpace(folderName))
                        return ResolveProjectFolderToPath(folderName);
                }
            }
            return null;
        }
        catch { return null; }
    }

    /// <summary>Resolve a project folder name to a full path via searchRoots + ~/.claude/projects/ reverse-map.</summary>
    static string? ResolveProjectFolderToPath(string folderName)
    {
        var searchRoots = new[] { @"W:\GitHub", @"W:\HTS_Project", @"C:\Users\edenc\projects" };
        foreach (var root in searchRoots)
        {
            var candidate = Path.Combine(root, folderName);
            if (Directory.Exists(candidate)) return candidate;
        }
        // Reverse-map from ~/.claude/projects/ dir names
        var cpDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".claude", "projects");
        if (Directory.Exists(cpDir))
        {
            var suffix = "-" + folderName.Replace('_', '-');
            foreach (var dir in Directory.GetDirectories(cpDir))
            {
                var dirName = Path.GetFileName(dir);
                if (dirName != null && dirName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
                {
                    var reversed = dirName.Replace("--", ":\\", StringComparison.Ordinal).Replace('-', '\\');
                    return reversed;
                }
            }
        }
        return null;
    }

    // ── JSONL context usage ──────────────────────────────────────────────────

    /// <summary>Get context usage % and JSONL age for a specific CWD's session.</summary>
    static (int pct, TimeSpan? age) GetContextInfoForCwd(string? cwd)
    {
        var (pct, age, _, _) = GetContextInfoForCwdEx(cwd);
        return (pct, age);
    }

    /// <summary>
    /// Extended: also returns JSONL path and file size (for context monitor dedup).
    /// Estimate: 40MB = 100% context. JSONL ÷ 40MB = ctx%.
    /// </summary>
    static (int pct, TimeSpan? age, string? jsonlPath, long fileSize) GetContextInfoForCwdEx(string? cwd)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return (-1, null, null, 0);
        try
        {
            var projName = cwd.Replace(':', '-').Replace('\\', '-').Replace('/', '-').Replace('_', '-');
            var projDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".claude", "projects", projName);
            if (!Directory.Exists(projDir)) return (-1, null, null, 0);

            string? latestFile = null;
            DateTime latestMtime = DateTime.MinValue;
            foreach (var jsonl in Directory.GetFiles(projDir, "*.jsonl"))
            {
                try
                {
                    var mtime = File.GetLastWriteTimeUtc(jsonl);
                    if (mtime > latestMtime) { latestMtime = mtime; latestFile = jsonl; }
                }
                catch { }
            }
            if (latestFile == null) return (-1, null, null, 0);
            var size = new FileInfo(latestFile).Length;
            var pct = size > 0 ? (int)(size / (1024.0 * 1024.0) / 40.0 * 100) : -1;
            var age = DateTime.UtcNow - latestMtime;
            return (pct, age, latestFile, size);
        }
        catch { return (-1, null, null, 0); }
    }

    // ── JSONL last-output reading ────────────────────────────────────────────

    /// <summary>
    /// Read most recent assistant output from Claude Code session JSONL for a CWD.
    /// CWD → ~/.claude/projects/{mapped-name}/*.jsonl (most recently modified).
    /// Reads only last 8–32 KB (tail) — session files grow to 35MB+.
    /// Per-CWD cache: skips re-read when file size/mtime unchanged.
    /// </summary>
    static string? ReadClotThoughtForCwd(string? cwd)
    {
        if (string.IsNullOrWhiteSpace(cwd)) return "";
        try
        {
            var projName = cwd.Replace(':', '-').Replace('\\', '-').Replace('/', '-').Replace('_', '-');
            var projDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".claude", "projects", projName);
            if (!Directory.Exists(projDir)) return "";

            string? latestFile = null;
            DateTime latestMtime = DateTime.MinValue;
            foreach (var jsonl in Directory.GetFiles(projDir, "*.jsonl"))
            {
                var mtime = File.GetLastWriteTimeUtc(jsonl);
                if (mtime > latestMtime) { latestMtime = mtime; latestFile = jsonl; }
            }
            if (latestFile == null) return "";

            var fi = new FileInfo(latestFile);
            if (_clotThoughtCache.TryGetValue(cwd, out var cached) &&
                cached.file == latestFile && cached.len == fi.Length && cached.mtime == fi.LastWriteTimeUtc)
                return cached.preview;

            // Lightweight tail read: 8KB first, 32KB fallback
            string? selected = null;
            foreach (var tailSize in new[] { 8 * 1024, 32 * 1024 })
            {
                var lines = ReadTailLinesShared(latestFile, tailSize);
                string bestAssistant = "", bestUser = "";
                for (int i = lines.Length - 1; i >= 0; i--)
                {
                    var line = lines[i];
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (!line.Contains("\"role\"", StringComparison.OrdinalIgnoreCase)) continue;
                    if (TryExtractRoleAndText(line, out var role, out var text))
                    {
                        text = NormalizePrompt(text);
                        if (string.IsNullOrWhiteSpace(text)) continue;
                        if (role == "assistant" && string.IsNullOrWhiteSpace(bestAssistant))
                            bestAssistant = text;
                        if (role == "user" && string.IsNullOrWhiteSpace(bestUser))
                            bestUser = text;
                        if (!string.IsNullOrWhiteSpace(bestAssistant) && !string.IsNullOrWhiteSpace(bestUser))
                            break;
                    }
                }
                // Show both: user prompt + assistant thought
                if (!string.IsNullOrWhiteSpace(bestAssistant) && !string.IsNullOrWhiteSpace(bestUser))
                    selected = $"💬 {bestUser}\n🤖 {bestAssistant}";
                else if (!string.IsNullOrWhiteSpace(bestAssistant))
                    selected = bestAssistant;
                else if (!string.IsNullOrWhiteSpace(bestUser))
                    selected = $"💬 {bestUser}";
                if (!string.IsNullOrWhiteSpace(selected)) break;
            }
            _clotThoughtCache[cwd] = (latestFile, selected, fi.Length, fi.LastWriteTimeUtc);
            return selected;
        }
        catch { return ""; }
    }

    // ── Per-prompt display info ──────────────────────────────────────────────

    /// <summary>
    /// 최근 AI 출력에서 의미 있는 마지막 한 줄 추출 (4자 이상, 최대 120자).
    /// ReadClotThoughtForCwd() 결과나 ai.status 후보 텍스트에 공통 적용.
    /// </summary>
    internal static string GetLastOutputLine(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return "(empty)";
        var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var last = lines.LastOrDefault(l => l.Length >= 4) ?? text.Trim();
        return last.Length > 120 ? last[..120] + "…" : last;
    }

    /// <summary>
    /// hwnd 하나로 (displayName, lastLine, cwdLabel) 한 방에 반환.
    /// 프창 순회 시 표준 정보 조회 단일 진입점.
    ///
    /// [KNOWHOW] CWD 해석 삽질 역사 (후배 클롣들이 반복하지 말 것!)
    ///
    ///   문제1 — PID 기반 CWD는 다중 VS Code 창에서 틀림:
    ///     두 VS Code 창(WKAppBot, lucy_securepad)이 같은 PID(Code.exe 37252)를 공유.
    ///     _cachedCards.FirstOrDefault(c => c.ParentPid == pid) → 첫 번째 카드가 걸림
    ///     → 항상 한쪽 CWD만 나와서 대화명이 뒤바뀌는 버그 발생.
    ///     ★ 해결: VS Code는 반드시 창 타이틀에서 CWD 추출 (ExtractCwdFromVsCodeTitle) 우선!
    ///
    ///   문제2 — GetAllCachedPrompts()에 VS Code/Codex가 안 들어가 있었음:
    ///     ClaudePromptHelper.FindAllPrompts()가 VS Code/Codex 항목을 _turnFormCache에
    ///     추가하지 않아서 GetAllCachedPrompts()가 빈 리스트 반환.
    ///     → claudeInstances 비어있어 상태 스트리밍 전혀 안 됨.
    ///     ★ 해결: FindAllPrompts()에서 vscode-claudecode/codex-desktop도 _turnFormCache에 추가.
    ///
    ///   문제3 — 대화명 fallback이 "최근 업데이트된 카드" → 엉뚱한 인스턴스 선택:
    ///     GetBotUsernameFromCachedCards() step 2가 최근 카드를 고르는데,
    ///     lucy_securepad가 더 최근에 tick되면 WKAppBot 명령에서도 lucy 이름으로 전송됨.
    ///     ★ 해결: callerHwnd 힌트(step 0) → 타이틀 기반 CWD 매칭(step 1b) → 직접 빌드(step 1c)
    ///             callerCwd 모를 때만 최근 카드 fallback(step 2) 허용.
    ///
    ///   문제4 — Slack 스레드 라우팅: 작성자 이름 파싱으로 창 매칭 실패:
    ///     "클롣[WG-lucy_securepad]" → cwdTag 추출 → 타이틀 Contains → 불안정.
    ///     ★ 해결: threadTs == state.SlackStatusTs 역방향 조회 (FindHwndBySlackStatusTs)
    ///             hwnd를 직접 찾아 라우팅 — 프창핸들이 결정적 정보!
    ///
    /// displayName : Slack 대화명 (예: "클롣[WG-WKAppBot]")
    /// lastLine    : JSONL 마지막 assistant 출력 한 줄
    /// cwdLabel    : 약식 CWD (예: "WG-WKAppBot")
    /// </summary>
    internal static (string displayName, string lastLine, string cwdLabel) GetPromptDisplayInfo(IntPtr hwnd)
    {
        // 1. PromptInfo from cache
        var pi = FindAllPromptsViaMcp()
            .FirstOrDefault(p => p.WindowHandle == hwnd);

        // 2. CWD: 호스트 타입별 추출 우선순위
        //   VS Code → 타이틀 우선 (PID 공유 문제, 문제1 참조)
        //   Codex   → UIA app-header-portal-main 버튼 (타이틀은 항상 "Codex")
        //   기타    → PID 카드 fallback
        string? cwd = null;
        if (pi?.HostType == "vscode-claudecode" && !string.IsNullOrEmpty(pi.WindowTitle))
            cwd = ExtractCwdFromVsCodeTitle(pi.WindowTitle);
        else if (pi?.HostType == "codex-desktop")
            cwd = ExtractCwdFromCodexWindow(hwnd);
        if (string.IsNullOrEmpty(cwd))
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out var pid);
            var card = _cachedCards.FirstOrDefault(c => c.ParentPid == (int)pid);
            cwd = card?.Cwd;
        }

        // 3. CwdLabel (시스템/설치폴더 → 태그 없음)
        var cwdLabel = (!string.IsNullOrEmpty(cwd) && !IsSystemOrInstallDirectory(cwd))
            ? AbbreviateCwd(cwd) : "";

        // 4. Display name
        string displayName;
        if (pi != null)
        {
            var prefix = pi.HostType?.Contains("codex") == true ? SlackCodexPrefix : SlackClaudePrefix;
            displayName = (pi.HostType == "claude-desktop" || string.IsNullOrEmpty(cwdLabel))
                ? prefix : $"{prefix}[{cwdLabel}]";
        }
        else
        {
            displayName = !string.IsNullOrEmpty(cwdLabel)
                ? $"{SlackClaudePrefix}[{cwdLabel}]" : SlackClaudePrefix;
        }

        // 5. Last output line from JSONL
        var lastLine = GetLastOutputLine(ReadClotThoughtForCwd(cwd));

        return (displayName, lastLine, cwdLabel);
    }
}
