// AgentPolicy.cs

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;


static class AgentPolicy
{
    public const string PolicyVersion = "2026.03.06";
    public const int ReminderMinutes = 10;

    static readonly string WorkspacePath = ResolveWorkspace();

    static readonly string PolicyFilePath =
        Path.Combine(WorkspacePath, $"agent-policy-{Environment.ProcessId}.txt");

    static readonly TimeSpan PolicyTTL = TimeSpan.FromHours(24);

    public const string EmbeddedInitialPrompt = """
AGENT OPERATING POLICY v2026.03.06

You are an autonomous development agent operating through WKAppBot.

Primary Objective
Execute tasks efficiently while minimizing token usage.

Execution Format
Plan
Action
Result
TODO

Response Language
All reports and questions must be written in Korean using polite "-요" style.
Do not mix other languages unless required.

Core Rules
1. Prefer execution over discussion.
2. Keep prompts short and precise.
3. Avoid repeating solved questions.
4. Do not restate the user's question unless needed.
5. Default response length <80 words unless requested.
6. Use bullet points or numbered lists instead of paragraphs.
7. If unsure, try a practical step before asking.
8. Do not stop after the first failure; try alternative approaches.
9. Prefer the simplest correct solution and minimize token usage.
10. If tasks >60s, report a one-line status every 60s including file, function, and line number.
11. Progress reports must include only the delta since the previous report.
12. If no delta exists, report the last action and elapsed seconds.
13. If waiting for input or no next step exists, notify the user.
14. If multiple tasks appear, extract them into an ordered TODO list.
15. Mark completed TODO items with strikethrough (~~done~~).
16. Continue automatically using the TODO list; choose the best practical option if decisions are required.
17. If no code change occurs for 5 minutes, report current status and blockers. Do NOT make unnecessary code changes just to show activity.
18. For planning or complex problems run:
wkappbot ask gpt "Problem: <one sentence>. Goal: <one sentence>. Best approach?"

Build Verification
- After every code change, run: dotnet build (or the project-specific build command).
- Do NOT report "done" until the build succeeds.
- If the build fails, fix it before moving on.

Destructive Change Guard
- Never delete files, drop tables, force-push, or reset --hard without explicit user approval.
- If a destructive action seems necessary, ask first.
- Prefer additive changes (rename, deprecate) over deletion.

Handoff Quality
- When handing off work to another agent or session, leave a concise summary:
  What was done, what remains, and any known issues.
- Write the summary in the workspace (e.g., agent-state.txt) so the next agent can read it.

Encoding Safety
- Source code comments: English only (Korean causes encoding corruption in MFC/Win32 builds).
- String literals with Korean: always verify UTF-8 BOM is preserved after editing.
- If a file's encoding looks wrong, fix it before making other changes.

Tool Usage
- WKAppBot is the command gateway. The standard command is `a11y`.
- `a11y` is the ONE universal command: 24 actions covering discovery (inspect, windows, screenshot, ocr),
  window control (close, minimize, maximize, restore, focus, move, resize),
  element interaction (invoke, click, toggle, expand, collapse, select, scroll, type, set-value, set-range),
  and query (find, read, highlight).
- Grap pattern targets windows: wildcards "*App*", regex "regex:...", OR "*a*;*b*", handle "*hwnd=XX*".
- #scope drills into UIA elements: "*App*#*MenuBar*". CSS auto-detected for web views: "*Chrome*#button.submit".
- 3-tier fallback: UIA -> Win32 -> SendInput (native), CSS -> CDP -> UIA (web views).
- Use tools only when needed.
- Do not repeatedly call the same tool without new information.
- Explore files, commands, and logs before asking.
- Prefer modifying existing files instead of rewriting large sections.

Loop Prevention
- Check if work is already done before editing or asking again.
- If a tool fails, retry once with a different approach, then continue locally.

Planning Delegation
Use:
wkappbot ask gpt "Problem: <one sentence>. Goal: <one sentence>. Constraints: <optional>. Best approach?"

Guidelines
- Question length ≈30 words.
- Ask for short numbered plans.
- Reuse existing plans.
- Prefer answers <80 words.

Startup Confirmation
After loading this policy, confirm with ONE short line only:
"Policy loaded. Ready."

Do not summarize the policy.
""";

    public const string EmbeddedReminderPrompt = """
AGENT REMINDER

- Execute first, discuss later.
- Keep responses short (<80 words).
- If stuck or planning is needed, run:
  wkappbot ask gpt "Problem: <1 sentence>. Goal: <1 sentence>. Best approach?"
- Follow the TODO list automatically.

Tip
Review policy if unsure:
agent-policy.txt
""";

    public static void StartPolicyBroadcast()
    {
        DateTime now = DateTime.UtcNow;

        // ── 이니셜 판단: 파일 생성시간(CreationTime) 기준 ──
        // 파일 없음 / 생성 24시간 경과 / 생성시간 미래 → INITIAL 강제 출력
        if (!File.Exists(PolicyFilePath))
        {
            BroadcastInitial();
        }
        else
        {
            DateTime createdUtc = File.GetCreationTimeUtc(PolicyFilePath);
            DateTime modifiedUtc = File.GetLastWriteTimeUtc(PolicyFilePath);

            if (createdUtc > now || now - createdUtc > PolicyTTL)
            {
                // 생성 24시간 경과 or 미래 → 폴리시 재생성 (INITIAL)
                BroadcastInitial();
            }
            else if (now - modifiedUtc > TimeSpan.FromMinutes(ReminderMinutes))
            {
                // 마지막 수정(=리마인더) 후 10분 경과 → 리마인더 append
                BroadcastReminder(now);
            }
            // else: 최근에 출력됨 → 스킵 (노이즈 방지)
        }
    }

    static void BroadcastInitial()
    {
        Broadcast("INITIAL", EmbeddedInitialPrompt);

        try
        {
            File.WriteAllText(PolicyFilePath, EmbeddedInitialPrompt);
        }
        catch { }
    }

    static void BroadcastReminder(DateTime now)
    {
        Broadcast("REMINDER", EmbeddedReminderPrompt);

        try
        {
            // Append → 파일 변경시간 갱신 (다음 호출 시 "언제 리마인드했나" 판단 기준)
            File.AppendAllText(PolicyFilePath,
                $"\n--- REMINDER @ {now:yyyy-MM-dd HH:mm:ss} UTC ---\n");
        }
        catch { }
    }

    static void Broadcast(string type, string message)
    {
        Console.WriteLine($"[POLICY_BEGIN:{type}]");
        Console.WriteLine(message);
        Console.WriteLine($"[POLICY_END:{type}]");
    }

    // Known agent workspace paths (hardcoded for this machine)
    static readonly string[] KnownAgentPaths =
    {
        @"C:\Users\edenc\.openclaw\workspace",  // OpenClaw (kro) main workspace
        @"W:\GitHub\WKAppBot",                  // WKAppBot (clot)
    };

    static string ResolveWorkspace()
    {
        // 1️⃣ explicit override
        string? env = Environment.GetEnvironmentVariable("WK_WORKSPACE");
        if (!string.IsNullOrWhiteSpace(env) && Directory.Exists(env))
            return Path.GetFullPath(env);

        // 1.5️⃣ known agent paths — check if our process tree contains a matching agent
        try
        {
            int checkPid = System.Diagnostics.Process.GetCurrentProcess().Id;
            for (int d = 0; d < 5 && checkPid > 0; d++)
            {
                var pi = QueryProcess(checkPid);
                if (pi == null) break;
                if (IsAiAgentProcess(pi.Name, pi.CommandLine))
                {
                    foreach (var known in KnownAgentPaths)
                    {
                        if (Directory.Exists(known) &&
                            (pi.CommandLine ?? "").IndexOf(
                                Path.GetFileName(known), StringComparison.OrdinalIgnoreCase) >= 0)
                            return known;
                    }
                }
                checkPid = pi.ParentProcessId;
            }
        }
        catch { }

        try
        {
            int pid = System.Diagnostics.Process.GetCurrentProcess().Id;
            int depth = 0;
            const int maxDepth = 10;
            string? bestNonVcs = null; // agent dir without VCS — usable as fallback

            while (pid > 0 && depth < maxDepth)
            {
                depth++;
                var info = QueryProcess(pid);
                if (info == null) break;

                // 2️⃣ agent process CWD — if ancestor is an AI agent, use its working directory
                if (IsAiAgentProcess(info.Name, info.CommandLine))
                {
                    string? agentCwd = GetProcessCwd(info.ParentProcessId)
                        ?? ExtractWorkDirFromCommandLine(info.CommandLine);

                    if (!string.IsNullOrWhiteSpace(agentCwd) && Directory.Exists(agentCwd))
                    {
                        if (IsVcsWorkspace(agentCwd))
                            return Path.GetFullPath(agentCwd!);

                        // Non-VCS agent dir (e.g. OpenClaw) — remember as fallback
                        bestNonVcs ??= Path.GetFullPath(agentCwd!);
                    }
                }

                // 3️⃣ workspace path inside command line (VSCode / agents often pass it)
                string? cmdDir = ExtractWorkDirFromCommandLine(info.CommandLine);
                if (!string.IsNullOrWhiteSpace(cmdDir) && IsVcsWorkspace(cmdDir))
                    return Path.GetFullPath(cmdDir!);

                // 4️⃣ fallback to executable directory of ancestor
                if (!string.IsNullOrWhiteSpace(info.ExecutablePath))
                {
                    string? dir = Path.GetDirectoryName(info.ExecutablePath);
                    if (!string.IsNullOrWhiteSpace(dir) && IsVcsWorkspace(dir))
                        return dir!;
                }

                pid = info.ParentProcessId;
            }

            // 5️⃣ agent dir without VCS is still better than CWD
            if (bestNonVcs != null)
                return bestNonVcs;
        }
        catch
        {
        }

        // 6️⃣ final fallback — current directory
        return Directory.GetCurrentDirectory();
    }

    /// <summary>Try to get process CWD via WMI (not always available).</summary>
    static string? GetProcessCwd(int pid)
    {
        try
        {
            // WMI Win32_Process doesn't expose CWD directly.
            // Fall back to reading /proc-style info via NtQueryInformationProcess if needed.
            // For now, try the parent shell's command line for --folder or path arguments.
            var info = QueryProcess(pid);
            if (info == null) return null;
            return ExtractWorkDirFromCommandLine(info.CommandLine);
        }
        catch { return null; }
    }

    /// <summary>Extract a directory path from a command line string (quoted or --folder arg).</summary>
    static string? ExtractWorkDirFromCommandLine(string? cmd)
    {
        if (string.IsNullOrWhiteSpace(cmd)) return null;

        // Check for --folder "path" pattern (VSCode, Claude Code)
        int folderIdx = cmd.IndexOf("--folder", StringComparison.OrdinalIgnoreCase);
        if (folderIdx >= 0)
        {
            string after = cmd.Substring(folderIdx + 8).TrimStart();
            if (after.StartsWith("\""))
            {
                int end = after.IndexOf('"', 1);
                if (end > 1)
                {
                    string p = after.Substring(1, end - 1);
                    if (Directory.Exists(p)) return Path.GetFullPath(p);
                }
            }
            else
            {
                string? p = after.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                if (!string.IsNullOrEmpty(p) && Directory.Exists(p)) return Path.GetFullPath(p);
            }
        }

        // Fallback: scan quoted segments for directory paths
        var parts = cmd.Split('"');
        foreach (var p in parts)
        {
            string trimmed = p.Trim();
            if (trimmed.Length >= 3 && trimmed.Contains(Path.DirectorySeparatorChar) && Directory.Exists(trimmed))
                return Path.GetFullPath(trimmed);
        }

        return null;
    }

    static readonly string[] VcsMarkers = { ".git", ".svn", ".hg", ".bzr", ".vscode" };

    static bool IsVcsWorkspace(string? dir)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dir) || !Directory.Exists(dir))
                return false;

            foreach (var marker in VcsMarkers)
            {
                if (Directory.Exists(Path.Combine(dir, marker)))
                    return true;
            }
            return false;
        }
        catch { return false; }
    }

    static bool IsAiAgentProcess(string? name, string? cmd)
    {
        if (string.IsNullOrEmpty(name)) return false;

        name = name.ToLowerInvariant();
        cmd = (cmd ?? "").ToLowerInvariant();

        return
            name.Contains("openclaw") ||
            name.Contains("codex") ||
            name.Contains("claude") ||
            name.Contains("agent") ||
            cmd.Contains("openclaw") ||
            cmd.Contains("codex") ||
            cmd.Contains("claude") ||
            cmd.Contains("wkappbot");
    }

    class ProcInfo
    {
        public int ParentProcessId;
        public string? Name;
        public string? CommandLine;
        public string? ExecutablePath;
        public string? CurrentDirectory;
    }

    static ProcInfo? QueryProcess(int pid)
    {
        try
        {
            using var searcher = new ManagementObjectSearcher(
                $"SELECT ProcessId, ParentProcessId, Name, CommandLine, ExecutablePath FROM Win32_Process WHERE ProcessId={pid}");

            foreach (ManagementObject mo in searcher.Get())
            {
                var info = new ProcInfo();

                info.ParentProcessId = Convert.ToInt32(mo["ParentProcessId"]);
                info.Name = mo["Name"]?.ToString();
                info.CommandLine = mo["CommandLine"]?.ToString();
                info.ExecutablePath = mo["ExecutablePath"]?.ToString();

                if (!string.IsNullOrEmpty(info.ExecutablePath))
                    info.CurrentDirectory = Path.GetDirectoryName(info.ExecutablePath);

                return info;
            }
        }
        catch { }

        return null;
    }}