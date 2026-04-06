using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>
    /// Extract the CWD from a JSONL file by reading its first few lines.
    /// Returns null if CWD not found.
    /// </summary>
    static string? ExtractCwdFromJsonl(string jsonlPath)
    {
        try
        {
            using var sr = new StreamReader(jsonlPath);
            for (int i = 0; i < 20 && !sr.EndOfStream; i++)
            {
                var line = sr.ReadLine();
                if (line == null) break;
                // Look for "cwd":"W:\\GitHub\\WKAppBot" pattern
                var m = System.Text.RegularExpressions.Regex.Match(line, "\"cwd\":\"([^\"]+)\"");
                if (m.Success)
                {
                    return m.Groups[1].Value.Replace("\\\\", "\\");
                }
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Build a handoff prompt for a new chat session.
    /// Reads the tail of the current JSONL session to extract recent context,
    /// then constructs a continuation prompt in English (token-efficient) with Korean response instruction.
    /// </summary>
    static string BuildHandoffPrompt(string jsonlPath)
    {
        // Extract last few user/assistant messages from JSONL for context
        var recentMessages = new List<string>();
        try
        {
            // Read last ~50KB of the file (tail) to find recent conversation
            using var fs = new FileStream(jsonlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            var tailSize = Math.Min(fs.Length, 50 * 1024);
            fs.Seek(-tailSize, SeekOrigin.End);
            using var sr = new StreamReader(fs, Encoding.UTF8);

            // Skip partial first line
            if (fs.Position > 0) sr.ReadLine();

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var node = JsonNode.Parse(line);
                    if (node == null) continue;
                    var role = node["role"]?.GetValue<string>();
                    var type = node["type"]?.GetValue<string>();

                    // Capture human/assistant summary messages
                    if (role == "human" || role == "user")
                    {
                        var content = node["content"]?.ToString() ?? "";
                        if (content.Length > 200) content = content[..200] + "...";
                        if (!string.IsNullOrWhiteSpace(content))
                            recentMessages.Add($"[USER] {content}");
                    }
                    else if (role == "assistant")
                    {
                        var content = node["content"]?.ToString() ?? "";
                        if (content.Length > 300) content = content[..300] + "...";
                        if (!string.IsNullOrWhiteSpace(content))
                            recentMessages.Add($"[ASSISTANT] {content}");
                    }
                }
                catch { /* skip unparseable lines */ }
            }
        }
        catch (Exception ex)
        {
            recentMessages.Add($"(Failed to read session: {ex.Message})");
        }

        // Keep only the last ~10 messages for context
        if (recentMessages.Count > 10)
            recentMessages = recentMessages.Skip(recentMessages.Count - 10).ToList();

        var contextBlock = recentMessages.Count > 0
            ? string.Join("\n", recentMessages)
            : "(no recent messages extracted)";

        // Build handoff prompt (English for token efficiency, Korean response requested)
        var handoffPrompt = $@"This is an AUTO-RELAY from AppBotEye. The previous session hit 95% context limit and was automatically handed off.

## Instructions
1. Read CLAUDE.md and MEMORY.md first to understand the project
2. Continue the work from where the previous session left off
3. Reply in Korean (한국어로 답변해주세요)
4. Send a Slack message to let the user know you're continuing: `wkappbot slack send ""새 채팅에서 이어갑니다! (auto-relay) 🔄""`
5. Check recent Slack messages for any user instructions: look at eye tick data

## Recent conversation context from previous session:
```
{contextBlock}
```

## Session info
- Previous JSONL: {Path.GetFileName(jsonlPath)}
- Relay timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
- Relay reason: context 95% limit reached

Please start by reading CLAUDE.md, then summarize what you understand about the pending work and continue.

(auto-relay by AppBotEye context monitor)";

        return handoffPrompt;
    }

    /// <summary>Overload: includes active card data + plan files for richer handoff.</summary>
    static string BuildHandoffPrompt(string jsonlPath, List<EyeParentCard> cards, double sizeMB, double limitMB)
    {
        var sb = new StringBuilder();
        sb.AppendLine("이전 세션이 컨텍스트 90%에 도달하여 자동 인수인계합니다.");
        sb.AppendLine("CLAUDE.md와 MEMORY.md를 먼저 읽고 이어서 작업해주세요.");
        sb.AppendLine();

        // Active cards = what clots were working on
        if (cards.Count > 0)
        {
            sb.AppendLine("## 활성 클롣 카드:");
            foreach (var c in cards.Take(5))
            {
                var cwd = AbbreviateCwd(c.Cwd);
                var info = !string.IsNullOrWhiteSpace(cwd) ? cwd : c.ParentName;
                sb.AppendLine($"- [{info}] {c.LastTag}: {c.LastStatus}");
            }
            sb.AppendLine();
        }

        // Plan files
        try
        {
            var plansDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".claude", "plans");
            if (Directory.Exists(plansDir))
            {
                var recentPlan = Directory.GetFiles(plansDir, "*.md")
                    .OrderByDescending(f => File.GetLastWriteTimeUtc(f))
                    .FirstOrDefault();
                if (recentPlan != null)
                {
                    var age = (DateTime.UtcNow - File.GetLastWriteTimeUtc(recentPlan)).TotalHours;
                    if (age < 24)
                        sb.AppendLine($"## 미완료 플랜: {Path.GetFileName(recentPlan)} ({age:F0}시간 전)");
                }
            }
        }
        catch { }

        // Available skills (project + HQ)
        try
        {
            var skillLines = BuildSkillSummary(cards.FirstOrDefault()?.Cwd);
            if (skillLines != null)
            {
                sb.AppendLine("## 📚 참고 스킬 (작업 중 아래 스킬들을 참고서로 활용하세요):");
                sb.AppendLine("관련 작업 전에 `wkappbot skill show <id>` 로 상세 내용 먼저 확인하면 삽질 방지!");
                sb.Append(skillLines);
                sb.AppendLine("→ `wkappbot skill search <키워드>` 로 관련 스킬 검색 가능");
                sb.AppendLine();
            }
        }
        catch { }

        sb.AppendLine();
        sb.AppendLine($"세션: {Path.GetFileName(jsonlPath)} ({sizeMB:F1}/{limitMB}MB)");
        sb.AppendLine($"시간: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine();
        sb.AppendLine("⚡ 중요: 채팅방 제목을 참신하고 재밌게 지어주세요! 매번 '인수인계'같은 뻔한 제목 금지!");
        sb.AppendLine("슬랙으로 인수인계 완료 알림 보내주세요: wkappbot slack send '새 세션에서 이어갑니다 🔄'");

        return sb.ToString().Trim();
    }

    // Returns a compact skill list (app-grouped) for handoff prompt, or null if no skills.
    static string? BuildSkillSummary(string? cwd)
    {
        var sb = new StringBuilder();
        var dirs = new List<string>();

        // Project skills dir (callerCwd/skills/)
        var projCwd = cwd ?? Environment.CurrentDirectory;
        var projSkills = Path.Combine(projCwd, "skills");
        if (Directory.Exists(projSkills)) dirs.Add(projSkills);

        // HQ skills dir
        var hqSkills = Path.Combine(DataDir, "skills");
        if (Directory.Exists(hqSkills)) dirs.Add(hqSkills);

        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var byApp = new SortedDictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var dir in dirs)
        {
            foreach (var f in Directory.GetFiles(dir, "*.skill.json", SearchOption.AllDirectories))
            {
                try
                {
                    var text = File.ReadAllText(f);
                    var doc = System.Text.Json.JsonDocument.Parse(text).RootElement;
                    var id    = doc.TryGetProperty("id",    out var idEl)    ? idEl.GetString()    ?? "" : "";
                    var app   = doc.TryGetProperty("app",   out var appEl)   ? appEl.GetString()   ?? "" : "";
                    var title = doc.TryGetProperty("title", out var titleEl) ? titleEl.GetString() ?? "" : "";
                    if (string.IsNullOrEmpty(id) || !seen.Add(id)) continue;
                    if (!byApp.TryGetValue(app, out var list)) byApp[app] = list = [];
                    list.Add($"  • {id} — {title}");
                }
                catch { }
            }
        }

        if (byApp.Count == 0) return null;

        foreach (var (app, items) in byApp)
        {
            sb.AppendLine($"[{app}]");
            foreach (var item in items) sb.AppendLine(item);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Build a handoff section to write into CLAUDE.md.
    /// Written in Korean so the user can also read it ("나도 보게 ㅋ").
    /// The next Claude session reads CLAUDE.md on startup and sees this section.
    /// </summary>
    static string BuildHandoffSection(string jsonlPath, string handoffPrompt)
    {
        // Extract recent context summary from JSONL
        var recentLines = new List<string>();
        try
        {
            using var fs = new FileStream(jsonlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
            var tailSize = Math.Min(fs.Length, 30 * 1024);
            fs.Seek(-tailSize, SeekOrigin.End);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            if (fs.Position > 0) sr.ReadLine(); // skip partial

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var node = JsonNode.Parse(line);
                    if (node == null) continue;
                    var role = node["role"]?.GetValue<string>();
                    if (role == "human" || role == "user")
                    {
                        var c = node["content"]?.ToString() ?? "";
                        if (c.Length > 150) c = c[..150] + "...";
                        if (!string.IsNullOrWhiteSpace(c)) recentLines.Add($"  - **User**: {c}");
                    }
                    else if (role == "assistant")
                    {
                        var c = node["content"]?.ToString() ?? "";
                        if (c.Length > 150) c = c[..150] + "...";
                        if (!string.IsNullOrWhiteSpace(c)) recentLines.Add($"  - **Claude**: {c}");
                    }
                }
                catch { }
            }
        }
        catch { recentLines.Add("  - (failed to read session)"); }

        // Keep last 8 messages
        if (recentLines.Count > 8)
            recentLines = recentLines.Skip(recentLines.Count - 8).ToList();

        var contextSummary = recentLines.Count > 0
            ? string.Join("\n", recentLines)
            : "  - (no recent messages)";

        return $@"
## 🔄 Handoff — {DateTime.Now:yyyy-MM-dd HH:mm}

> **Previous session hit 95% context limit and was auto-relayed by AppBotEye.**
> Read this section, continue the work, then DELETE this section when done.

### Previous Session
- **JSONL**: `{Path.GetFileName(jsonlPath)}`
- **Relay time**: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
- **Reason**: context 95% limit (auto-relay by AppBotEye)

### Recent Conversation
{contextSummary}

### TODO for new session
1. Read CLAUDE.md + MEMORY.md to understand project state
2. Run `wkappbot slack send ""New chat continuing! (auto-relay) 🔄""` to notify user
3. Check recent Slack messages for user instructions
4. Continue work from where previous session left off
5. **DELETE this handoff section** after processing

";
    }

    /// <summary>
    /// Check if CLAUDE.md contains a handoff section (auto-relay precondition).
    /// </summary>
    static bool HasHandoffSectionInClaudeMd()
    {
        const string claudeMdPath = @"W:\GitHub\WKAppBot\CLAUDE.md";
        const string handoffMarker = "## \U0001f504 Handoff";
        try
        {
            if (!File.Exists(claudeMdPath)) return false;
            var content = File.ReadAllText(claudeMdPath, Encoding.UTF8);
            return content.Contains(handoffMarker, StringComparison.Ordinal);
        }
        catch { return false; }
    }

    /// <summary>
    /// Write handoff section to CLAUDE.md (at the end, before roadmap section).
    /// If a previous handoff section exists, replace it.
    /// CLAUDE.md path: W:/GitHub/WKAppBot/CLAUDE.md
    /// </summary>
    static void WriteHandoffToClaudeMd(string handoffSection)
    {
        const string claudeMdPath = @"W:\GitHub\WKAppBot\CLAUDE.md";
        const string handoffMarker = "## 🔄 Handoff";
        const string handoffEndMarker = "## "; // next section starts

        try
        {
            if (!File.Exists(claudeMdPath))
            {
                Console.WriteLine("[EYE] CLAUDE.md not found! Cannot write handoff.");
                return;
            }

            var content = File.ReadAllText(claudeMdPath, Encoding.UTF8);

            // Remove existing handoff section if present
            var handoffIdx = content.IndexOf(handoffMarker, StringComparison.Ordinal);
            if (handoffIdx >= 0)
            {
                // Find end of handoff section (next ## heading or end of file)
                var afterHandoff = content.IndexOf("\n" + handoffEndMarker, handoffIdx + handoffMarker.Length, StringComparison.Ordinal);
                if (afterHandoff >= 0)
                    content = content[..handoffIdx] + content[(afterHandoff + 1)..];
                else
                    content = content[..handoffIdx]; // handoff was at end
            }

            // Append handoff section at the end
            content = content.TrimEnd() + "\n" + handoffSection;

            // Atomic write
            var tmpPath = claudeMdPath + ".tmp";
            File.WriteAllText(tmpPath, content, Encoding.UTF8);
            File.Move(tmpPath, claudeMdPath, overwrite: true);

            EyeColor(ConsoleColor.Green);
            Console.WriteLine($"[EYE] ✅ CLAUDE.md 인수인계 섹션 작성 완료 ({handoffSection.Length} chars)");
            EyeResetColor();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EYE] CLAUDE.md 인수인계 작성 실패: {ex.Message}");
        }
    }
}
