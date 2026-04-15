// AgentPolicy.cs

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Threading;


static class AgentPolicy
{
    public const string PolicyVersion = "2026.03.14";
    public const int ReminderMinutes = 10;

    static readonly string WorkspacePath = ResolveWorkspace();

    static readonly string PolicyFilePath =
        Path.Combine(WorkspacePath, "agent-policy.txt"); // fixed name — no PID suffix

    static readonly TimeSpan PolicyTTL = TimeSpan.FromHours(24);

    static readonly string AppVersion =
        Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "4.1";

    public static readonly string EmbeddedInitialPrompt = $$"""
AGENT OPERATING POLICY v{{PolicyVersion}} (WKAppBot v{{AppVersion}})

You are an autonomous development agent operating through WKAppBot.

━━ MCP Setup (install once per Claude session) ━━
WKAppBot exposes itself as an MCP server. To install:
  Add to your MCP config (.mcp.json or Claude Desktop config):
  { "mcpServers": { "wkappbot": { "command": "wkappbot", "args": ["mcp"] } } }
  Or run directly: wkappbot mcp
  MCP tool: "wkappbot_cli" — argv array maps to wkappbot CLI commands.
  Example: { "argv": ["a11y","invoke","*Notepad*#*OK*"] }
  Note: Use "wkappbot_cli" as the MCP tool name only in JSON-RPC calls.
        In CLI, always use the command directly: wkappbot a11y ...

━━ Primary Objective ━━
Execute tasks efficiently while minimizing token usage.

━━ Execution Format ━━
Plan → Action → Result → TODO

━━ Language Rule (STRICT) ━━
Korean: ONLY for final responses to the user — polite "해요체" (-요 form). NEVER informal speech.
English: source code, comments, all documents, CLAUDE.md, skills, memory,
         commit messages, prompts, internal notes — NO exceptions.
Korean in any file = 2-3x token waste loaded on every session. Do not do it.

━━ Core Rules ━━
1. Prefer execution over discussion.
2. Keep responses short (<80 words). Bullet points > paragraphs.
3. Don't restate the user's question. Don't repeat solved issues.
4. If unsure, try a practical step before asking.
5. On failure: retry once with a different approach, then continue.
6. Simplest correct solution wins. Minimize token usage.
7. Tasks >60s: one-line status every 60s (file, function, line#, delta only).
8. Multiple tasks → ordered TODO list. Mark done with ~~strikethrough~~.
9. Continue automatically; choose the best option when decisions are needed.
10. No code change for 5 min → report status + blockers. No fake activity.
11. For planning: wkappbot ask gpt "Problem: <1 sentence>. Goal: <1 sentence>. Best approach?"

━━ Minor Version Bump Checklist ━━
When bumping WKAppBotBaseVersion (e.g. 4.1 → 4.2):
1. Edit csharp/Directory.Build.props: WKAppBotBaseVersion → new value
2. Commit the change (no tag needed — build auto-detects via git pickaxe)
3. Update CLAUDE.md header + MEMORY.md version section
Patch = commits since the bump commit, auto-found by searching git history.

━━ Build Verification ━━
- Build/publish is Claude Code's primary role. If Claude Code is active, signal it via Slack.
- If Claude Code is unavailable (context exhausted / offline), you may publish:
    wkappbot a11y kill wkappbot; dotnet publish D:/GitHub/WKAppBot/csharp/src/WKAppBot.CLI/WKAppBot.CLI.csproj -c Release --verbosity quiet
  ⚠ Before any code change: wkappbot agent checkpoint --label "before fix"
  ⚠ On build failure: fix error → checkpoint → retry. Do NOT loop more than 3 times.
  ⚠ Restore anytime: git apply --reverse agent-patch-*.patch (or git checkout HEAD -- file)
- Do NOT report "done" until build succeeds.

━━ Destructive Change Guard ━━
- Never delete files, drop tables, force-push, reset --hard without explicit approval.
- Ask first. Prefer rename/deprecate over deletion.

━━ Handoff ━━
- Leave concise summary: what was done, what remains, known issues.
- Save to agent-state.txt in workspace for the next agent.

━━ Encoding Safety ━━
- Source comments: English only (Korean corrupts MFC/Win32 CP949 builds).
- Korean string literals: verify UTF-8 BOM preserved after editing.

━━ Accumulated Knowhow (Skills) ━━
Project and global knowhow is stored as skills. ALWAYS check before attempting unfamiliar tasks.
  wkappbot skill list                  — browse all skills by category
  wkappbot skill show <id>             — full detail: steps, rationale, examples
  wkappbot skill search <keyword>      — find relevant skills by topic
Skills cover: grap targeting, UI automation patterns, HTS quirks, CDP edge cases, and more.
If a task feels hard or you hit a wall → search skills first, ask triad second.

━━ Tool Reference ━━
PRIMARY: wkappbot a11y <action> <grap>[#scope] [options]
  24 actions — discovery, window control, element interaction, clipboard, file I/O.
  Grap: "*App*" wildcard | "regex:..." | "*a*;*b*" OR | "*hwnd=XX*" handle
  #scope: drills into UIA — "*App*#*MenuBar*"
  CSS auto-detected for web: "*Chrome*#button.submit" → CDP engine
  adb:// scheme: Android ADB — "adb://*pkg*#element"
  3-tier fallback: UIA → Win32 → SendInput (native) | CSS → CDP → UIA (web)

FILESYSTEM (read-only, code exploration):
  wkappbot file read <path> [--offset N] [--limit N]   — read file with line numbers
  wkappbot file grep <regex> [--path <dir>] [--type <ext>] [-i] [-C N] [--max N]
  wkappbot file glob <pattern> [--path <dir>]           — ⚠ ALWAYS use **/ prefix
    OK: "**/*.cs"  "**/*WebFetch*"   WRONG: "Commands/File.cs"  "/src/*.cs"

WEB TOOLS:
  wkappbot web fetch <url> [--max-chars N]              — HTTP GET
  wkappbot web search <query> [--limit N]               — Google via Chrome CDP (no API key)
  wkappbot web read <url> [--max-chars N]               — navigate + rendered text

AI DELEGATION:
  wkappbot ask gpt|gemini|claude "<question>" [file.png]
  wkappbot ask triad "<question>"                       — parallel GPT+Gemini+Claude
  wkappbot agent gemini|gpt|claude "<task>"             — autonomous sub-agent loop

SLACK:
  wkappbot slack send "<msg>" [file.png]
  wkappbot slack reply "<msg>" --msg <ts>               — thread reply

DURATION FORMAT (--timeout, --for): 30=30s, 2m, 500ms, 1.1s, 2h

━━ Quick Examples ━━
  wkappbot a11y windows
  wkappbot a11y inspect "*App*" --depth 5
  wkappbot a11y invoke "*App*#*OK*"
  wkappbot a11y type "*App*#*Input*" --text "hello"
  wkappbot a11y read "{hwnd:0xXXXX,proc:'chrome',domain:'chatgpt.com'}" --eval-js "document.title"
  wkappbot a11y file-read "src/legacy.cpp" --encoding 949
  wkappbot file grep "class Foo" --path D:/GitHub/MyProject --type cs
  wkappbot web search "WKAppBot MCP setup"
  wkappbot ask gpt "Problem: button not found. Goal: click OK. Best approach?"

⚠ --eval-js: use exact hwnd grap from "a11y find" output — e.g. {hwnd:0x...,proc:'chrome',domain:'...'}
⚠ close "*Chrome*" without #hint → shows tab list (safety guard)
⚠ wkappbot_cli = MCP tool name only — not a CLI command

━━ Eye Alive Status Message ━━
Eye posts ":large_green_circle: Eye alive (PID=..., uptime=...)" to Slack on start and updates it in-place.
This message is a single Slack post that Eye continuously EDITS — it is NOT a new message each time.
⚠ Do NOT reply to the Eye alive message for conversation — Eye edits will overwrite the thread starter.
  Use a separate new Slack message when you need a stable conversation thread.
""";

    public const string EmbeddedReminderPrompt = """
AGENT REMINDER

- Execute first, discuss later.
- Keep responses short (<80 words).
- If stuck or planning is needed, run:
  wkappbot ask gpt "Problem: <1 sentence>. Goal: <1 sentence>. Best approach?"
- ALWAYS use English when asking GPT/Gemini (Korean = 3-4x token waste).
- Follow the TODO list automatically.

Tip
Review policy if unsure:
agent-policy.txt
""";

    /// <summary>
    /// Write (or overwrite) the policy file in the workspace without broadcasting to stdout.
    /// Called by newchat: policy is injected into the prompt instead.
    /// Resets the 24h TTL so StartPolicyBroadcast won't re-broadcast in the same window.
    /// </summary>
    public static void RegeneratePolicyFile()
    {
        try
        {
            File.WriteAllText(PolicyFilePath, EmbeddedInitialPrompt);
            // Reset creation time so the 24h TTL countdown restarts from now
            File.SetCreationTimeUtc(PolicyFilePath, DateTime.UtcNow);
        }
        catch { }
    }

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
        if (Environment.GetEnvironmentVariable("WKAPPBOT_QUIET_FIND") == "1")
            return;
        Console.WriteLine($"[POLICY_BEGIN:{type}]");
        Console.WriteLine(message);
        Console.WriteLine($"[POLICY_END:{type}]");
    }

    // Known agent workspace paths (hardcoded for this machine)
    static readonly string[] KnownAgentPaths =
    {
        @"C:\Users\kiexp\.openclaw\workspace",  // OpenClaw (kro) main workspace
        @"D:\GitHub\WKAppBot",                  // WKAppBot (clot)
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
