// AppBotEyeSlackHandlers.cs — Shared Slack event handlers + schedule executor for AppBotEye.
// Used by EyeGlobalPollingLoop (GlobalMode) — the only Eye mode.
// Kept as separate partial class file for readability (SetupSlackEventHandlers is ~440 lines).

using System.Text.Json.Nodes;
using WKAppBot.Core.Runner;
using WKAppBot.WebBot;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

internal partial class Program
{
    /// <summary>현재 실행 중인 exe 경로 (설치 위치 무관).</summary>
    static readonly string ExePath = (Environment.ProcessPath ?? "wkappbot").Replace('\\', '/');

    /// <summary>
    /// Build the "(Slack ... — wkappbot slack reply ...)" suffix appended to every forwarded prompt.
    /// ONE place to change the format — never duplicate this string elsewhere!
    /// </summary>
    static string SlackReplySuffix(string user, string replyTs, string? label = null)
    {
        var displayName = ResolveSlackDisplayName(user);
        var tag = string.IsNullOrEmpty(label) ? $"Slack @{displayName}" : $"Slack {label} @{displayName}";
        return $"({tag} → \"{ExePath}\" slack reply \"MUST reply your answer here\" --msg {replyTs})";
    }

    /// <summary>채널 브로드캐스트용 suffix — reply 대신 send (채널에 답장).</summary>
    static string SlackSendSuffix(string user)
    {
        var displayName = ResolveSlackDisplayName(user);
        return $"(Slack @{displayName} → \"{ExePath}\" slack send \"퐁~! 지금 뭐뭐 완료하고 머머 하고있습니다~!\")";
    }

    /// <summary>Per-target delivery result for ack message.</summary>
    record DeliveryResult(string ShortName, bool Sent);

    /// <summary>Full Slack display name for a prompt window (e.g. "클롣[WG-WKAppBot]", "코덳앱[WG-WKAppBot]").</summary>
    static string ExtractProjectName(ClaudePromptHelper.PromptInfo pi)
        => GetPromptDisplayInfo(pi.WindowHandle).displayName;

    /// <summary>
    /// CWD 기반으로 "내 창" 프롬프트를 정확히 찾는다.
    /// FindAllPrompts → CWD 폴더명이 윈도우 타이틀에 포함된 창 우선 → fallback: FindPrompt.
    /// 동료 창이 전경에 있어도 정확한 창을 찾아 전달.
    /// </summary>
    // Ownership boundary: this resolver picks the Eye instance's own target prompt.
    // Keep workspace-scoped selection in one function to avoid split logic edits.
    /// <summary>
    /// "폴더 태그 없음" fallback: 앱봇 실행 워크스페이스(e.g. WKAppBot)와
    /// 타이틀이 일치하는 모든 프롬프트 창 반환.
    /// → 포워딩 대상이 명확하지 않을 때 같은 프로젝트의 모든 AI에게 브로드캐스트.
    /// 단일 프롬프트 버전은 첫 번째 요소를 쓰면 됨.
    /// </summary>
    static DateTime _lastAppbotVsCodeSpawnAt = DateTime.MinValue;

    /// <summary>
    /// Spawn appbot VS Code window if not already open (60s cooldown).
    /// If pendingMessage is provided, delivers it to the window once Claude Code is ready
    /// (polls up to 20s for the prompt input to appear, then types + submits).
    /// Reusable from anywhere: routing fallback, newchat, homework, etc.
    /// </summary>
    static void TrySpawnAppbotVsCode(string? pendingMessage = null)
    {
        var cooldown = TimeSpan.FromSeconds(60);
        if (DateTime.UtcNow - _lastAppbotVsCodeSpawnAt < cooldown) return;
        _lastAppbotVsCodeSpawnAt = DateTime.UtcNow;

        var appbotDir = Environment.CurrentDirectory; // "W:\GitHub\WKAppBot"
        if (string.IsNullOrWhiteSpace(appbotDir) || IsSystemOrInstallDirectory(appbotDir)) return;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[EYE][SLACK] 앱봇 창 없음 — VS Code 스폰: {appbotDir}");
        Console.ResetColor();

        try { System.Diagnostics.Process.Start("code.exe", $"\"{appbotDir}\""); }
        catch (Exception ex) { Console.WriteLine($"[EYE][SLACK] VS Code 스폰 실패: {ex.Message}"); return; }

        if (string.IsNullOrWhiteSpace(pendingMessage)) return;

        // Background: poll until the prompt window is ready, then deliver the message
        var msg = pendingMessage;
        System.Threading.Tasks.Task.Run(() =>
        {
            const int maxWaitSec = 20;
            const int pollMs = 1000;
            ClaudePromptHelper.PromptInfo? target = null;

            for (int i = 0; i < maxWaitSec; i++)
            {
                System.Threading.Thread.Sleep(pollMs);
                try
                {
                    using var ph = new ClaudePromptHelper();
                    var all = ph.FindAllPrompts();
                    var cwdFolder = Path.GetFileName(Environment.CurrentDirectory);
                    target = all.FirstOrDefault(p =>
                        p.WindowTitle.Contains(cwdFolder, StringComparison.OrdinalIgnoreCase));
                    if (target != null) break;
                }
                catch { }
            }

            if (target == null)
            {
                Console.WriteLine("[EYE][SLACK] VS Code 스폰 후 창 미발견 — 메시지 전달 포기");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[EYE][SLACK] VS Code 준비 완료 — 대기 메시지 전달: {msg[..Math.Min(60, msg.Length)]}...");
            Console.ResetColor();
            try
            {
                using var ph = new ClaudePromptHelper();
                ClaudePromptHelper.AllowFocusSteal = true;
                ph.TypeAndSubmit(target, msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EYE][SLACK] 메시지 전달 실패: {ex.Message}");
            }
        });
    }

    static List<ClaudePromptHelper.PromptInfo> ResolveWorkspaceScopedPrompts(ClaudePromptHelper promptHelper)
    {
        var cwdFolder = Path.GetFileName(Environment.CurrentDirectory); // "WKAppBot"
        var allPrompts = promptHelper.FindAllPrompts();

        if (allPrompts.Count == 0)
        {
            Console.WriteLine("[EYE][SLACK] ResolveWorkspaceScopedPrompts: no prompts found at all");
            TrySpawnAppbotVsCode();
            var last = promptHelper.FindPrompt();
            return last != null ? new List<ClaudePromptHelper.PromptInfo> { last } : new List<ClaudePromptHelper.PromptInfo>();
        }

        if (allPrompts.Count == 1)
            return allPrompts;

        // CWD 폴더명으로 윈도우 타이틀 매칭 — 여러 개 가능 (VS Code + Codex 동시에 같은 폴더 열어둔 경우)
        var myPrompts = allPrompts
            .Where(p => p.WindowTitle.Contains(cwdFolder, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (myPrompts.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[EYE][SLACK] FindMyPrompts: CWD \"{cwdFolder}\" → {myPrompts.Count} match(es) (broadcast to all in workspace)");
            Console.ResetColor();
            return myPrompts;
        }

        // CWD 타이틀 매칭 실패 → cached appbot hwnd 우선, 없으면 첫 번째
        if (_cachedAppbotOwnHwnd != IntPtr.Zero && IsWindow(_cachedAppbotOwnHwnd))
        {
            var cached = allPrompts.FirstOrDefault(p => p.WindowHandle == _cachedAppbotOwnHwnd);
            if (cached != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[EYE][SLACK] FindMyPrompts: no CWD title match for \"{cwdFolder}\", using cached appbot hwnd 0x{_cachedAppbotOwnHwnd:X}");
                Console.ResetColor();
                return new List<ClaudePromptHelper.PromptInfo> { cached };
            }
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[EYE][SLACK] FindMyPrompts: no CWD match for \"{cwdFolder}\" in {allPrompts.Count} prompts, using first");
        Console.ResetColor();
        return new List<ClaudePromptHelper.PromptInfo> { allPrompts[0] };
    }

    static ClaudePromptHelper.PromptInfo? ResolveWorkspaceScopedPrompt(ClaudePromptHelper promptHelper)
        => ResolveWorkspaceScopedPrompts(promptHelper).FirstOrDefault();

    static bool EnableOwnerRecentAuthorRouting() => true;

    static List<ClaudePromptHelper.PromptInfo> ResolveThreadScopedPromptsByOwnerAndRecent(
        ClaudePromptHelper promptHelper,
        string botToken,
        string channel,
        string threadTs,
        string? ownBotUsername,
        List<ClaudePromptHelper.PromptInfo> allPrompts)
    {
        var (owner, recentAuthors, allAuthors) = LoadThreadAuthors(botToken, channel, threadTs);

        // NOTE: do NOT gate on owner == ownBotUsername here.
        // Normal user pings have owner = user, not the bot.
        // Thread-based bot routing uses participant presence, not ownership.

        if (allAuthors.Count == 0)
        {
            var own = ResolveWorkspaceScopedPrompts(promptHelper);
            if (own.Count > 0)
            {
                Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: no authors found, broadcast to {own.Count} workspace prompt(s)");
                return own;
            }
            Console.WriteLine("[EYE][SLACK] FindPromptsForThread: no authors found, fallback to all prompts");
            return allPrompts;
        }

        var hostTarget = ResolveAuthorHostTarget(allAuthors);
        var cwdTags = ExtractAuthorWorkspaceTags(allAuthors);

        Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: owner=\"{owner ?? "(none)"}\" recent={recentAuthors.Count} hostTarget={hostTarget} tags=[{string.Join(", ", cwdTags)}]");

        // cwdTags.Count == 0 means bot username has no [CWD] tag (e.g. "클롣" with no workspace).
        // In that case we route only to own workspace, NOT all prompts.
        if (cwdTags.Count == 0)
        {
            var ownOnly = ResolveWorkspaceScopedPrompts(promptHelper);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: no CWD tag in authors [{string.Join(", ", allAuthors)}] → own workspace only ({ownOnly.Count})");
            Console.ResetColor();
            return ownOnly;
        }

        Console.WriteLine($"[EYE][SLACK] PromptCandidates: total={allPrompts.Count}");
        foreach (var p in allPrompts)
        {
            var hostMatch = MatchesHostTarget(p, hostTarget);
            var searchTitle = GetPromptSearchableTitle(p);
            var cwdMatch = cwdTags.Any(tag => searchTitle.Contains(tag, StringComparison.OrdinalIgnoreCase));
            var selected = hostMatch && cwdMatch;
            var st = promptHelper.ProbeSubmitState(p);
            var shortTitle = p.WindowTitle.Length > 72 ? p.WindowTitle[..69] + "..." : p.WindowTitle;
            Console.WriteLine(
                $"  [CAND] hwnd=0x{p.WindowHandle:X} host={p.HostType} selected={selected} " +
                $"hostMatch={hostMatch} cwdMatch={cwdMatch} folder=\"{searchTitle}\" turnForm={st.TurnFormFound} submit={st.SubmitFound}/{st.SubmitEnabled}");
            Console.WriteLine($"         title=\"{shortTitle}\"");
        }

        var matched = allPrompts
            .Where(p => MatchesHostTarget(p, hostTarget))
            .Where(p => cwdTags.Any(tag => GetPromptSearchableTitle(p).Contains(tag, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        if (matched.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: {matched.Count}/{allPrompts.Count} prompts matched (authors: {string.Join(", ", allAuthors)})");
            Console.ResetColor();
            return matched;
        }

        var myFallback = ResolveWorkspaceScopedPrompts(promptHelper);
        if (myFallback.Count > 0)
        {
            Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: no match for [{string.Join(", ", allAuthors)}], broadcast to {myFallback.Count} workspace prompt(s)");
            return myFallback;
        }
        Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: no prompt match for authors [{string.Join(", ", allAuthors)}], skip routing");
        return new List<ClaudePromptHelper.PromptInfo>();
    }

    static (string? owner, List<string> recentAuthors, List<string> allAuthors) LoadThreadAuthors(string botToken, string channel, string threadTs)
    {
        string? owner = null;
        var recent = new List<string>();
        var all = new List<string>();

        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {botToken}");
            var url = $"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit=40&inclusive=true";
            var resp = http.GetAsync(url).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var json = System.Text.Json.Nodes.JsonNode.Parse(body);
            if (json?["ok"]?.GetValue<bool>() != true)
                return (owner, recent, all);

            var msgs = json["messages"]?.AsArray() ?? new System.Text.Json.Nodes.JsonArray();
            if (msgs.Count == 0) return (owner, recent, all);

            string? ReadAuthor(System.Text.Json.Nodes.JsonNode? m)
            {
                var username = m?["username"]?.GetValue<string>();
                if (!string.IsNullOrWhiteSpace(username)) return username.Trim();
                var user = m?["user"]?.GetValue<string>();
                if (!string.IsNullOrWhiteSpace(user)) return user.Trim();
                return null;
            }

            var starter = msgs.FirstOrDefault(m => string.Equals(m?["ts"]?.GetValue<string>(), threadTs, StringComparison.Ordinal))
                          ?? msgs[0];

            owner = ReadAuthor(starter);
            if (!string.IsNullOrWhiteSpace(owner))
                all.Add(owner);

            foreach (var m in msgs.Where(m => !ReferenceEquals(m, starter)).TakeLast(7))
            {
                var a = ReadAuthor(m);
                if (string.IsNullOrWhiteSpace(a)) continue;
                recent.Add(a);
                all.Add(a);
            }

            all = all
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
        catch { }

        return (owner, recent, all);
    }

    static string ResolveAuthorHostTarget(IEnumerable<string> authors)
    {
        var hasCodex = authors.Any(a =>
            a.Contains("codex", StringComparison.OrdinalIgnoreCase) ||
            a.Contains("코뎃", StringComparison.OrdinalIgnoreCase) ||
            a.Contains("코덱", StringComparison.OrdinalIgnoreCase));
        var hasClaude = authors.Any(a =>
            a.Contains("claude", StringComparison.OrdinalIgnoreCase) ||
            a.Contains("클롣", StringComparison.OrdinalIgnoreCase) ||
            a.Contains("클봇", StringComparison.OrdinalIgnoreCase));

        if (hasCodex && !hasClaude) return "codex";
        if (!hasCodex && hasClaude) return "claude";
        return "any";
    }

    static HashSet<string> ExtractAuthorWorkspaceTags(IEnumerable<string> authors)
    {
        var tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var a in authors)
        {
            if (string.IsNullOrWhiteSpace(a)) continue;
            var start = a.IndexOf('[');
            var end = a.IndexOf(']');
            if (start < 0 || end <= start) continue;
            var tag = a[(start + 1)..end].Trim();
            if (string.IsNullOrWhiteSpace(tag)) continue;
            tags.Add(tag);
            var lastDash = tag.LastIndexOf('-');
            if (lastDash >= 0 && lastDash < tag.Length - 1)
                tags.Add(tag[(lastDash + 1)..]);
        }
        return tags;
    }

    /// <summary>
    /// VS Code 타이틀에서 작업 폴더명만 추출.
    /// "채팅제목 - 작업폴더 - Visual Studio Code" → "작업폴더"
    /// 다른 호스트는 전체 타이틀 그대로 반환.
    /// </summary>
    static string GetPromptSearchableTitle(ClaudePromptHelper.PromptInfo p)
    {
        if (p.HostType == "vscode-claudecode")
        {
            var vscIdx = p.WindowTitle.IndexOf(" - Visual Studio Code", StringComparison.OrdinalIgnoreCase);
            if (vscIdx > 0)
            {
                var withoutSuffix = p.WindowTitle[..vscIdx];
                var lastDash = withoutSuffix.LastIndexOf(" - ", StringComparison.Ordinal);
                return lastDash >= 0 ? withoutSuffix[(lastDash + 3)..].Trim() : withoutSuffix.Trim();
            }
        }
        return p.WindowTitle;
    }

    static bool MatchesHostTarget(ClaudePromptHelper.PromptInfo prompt, string hostTarget)
    {
        if (hostTarget == "codex")
            return string.Equals(prompt.HostType, "codex-desktop", StringComparison.OrdinalIgnoreCase);
        if (hostTarget == "claude")
            return !string.Equals(prompt.HostType, "codex-desktop", StringComparison.OrdinalIgnoreCase);
        return true;
    }

    // Legacy alias kept for merge compatibility with older branches.
    static ClaudePromptHelper.PromptInfo? FindMyPrompt(ClaudePromptHelper promptHelper) =>
        ResolveWorkspaceScopedPrompt(promptHelper);

    /// <summary>
    /// threadTs → 해당 상태메시지를 올린 hwnd 역방향 조회.
    /// _instanceStates[hwnd].SlackStatusTs == threadTs → hwnd 직접 반환.
    /// Eye 재시작 후에도 SlackStatusTs는 window prop에서 복원되므로 신뢰도 높음.
    /// </summary>
    static IntPtr FindHwndBySlackStatusTs(string threadTs)
    {
        foreach (var (hwnd, state) in _instanceStates)
            if (state.SlackStatusTs == threadTs) return hwnd;
        return IntPtr.Zero;
    }

    /// <summary>
    /// 쓰레드에 응답한 클롣 대화명을 조회 → 해당 프롬프트들만 반환.
    /// Priority 0: threadTs == SlackStatusTs → hwnd 직접 매칭 (가장 정확).
    /// 쓰레드 메시지의 bot username ("클롣 [WKAppBot]") → 대괄호 안 폴더명 추출 → 프롬프트 매칭.
    /// 매칭 결과가 없으면 전체 프롬프트 반환 (fallback).
    /// </summary>
    // Ownership boundary: this resolver maps one Slack thread to target prompt windows.
    static List<ClaudePromptHelper.PromptInfo> ResolveThreadScopedPrompts(
        ClaudePromptHelper promptHelper, string botToken, string channel, string threadTs, string? ownBotUsername = null)
    {
        var allPrompts = promptHelper.FindAllPrompts();
        if (allPrompts.Count <= 1)
            return allPrompts;

        // Priority 0: threadTs == SlackStatusTs → hwnd 직접 역조회 (프창핸들이 결정적 정보)
        var directHwnd = FindHwndBySlackStatusTs(threadTs);
        if (directHwnd != IntPtr.Zero)
        {
            var directPrompt = ClaudePromptHelper.GetAllCachedPrompts()
                .FirstOrDefault(p => p.WindowHandle == directHwnd);
            if (directPrompt != null)
            {
                Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: direct hwnd=0x{directHwnd:X} for threadTs={threadTs}");
                return new List<ClaudePromptHelper.PromptInfo> { directPrompt };
            }
        }

        if (EnableOwnerRecentAuthorRouting())
            return ResolveThreadScopedPromptsByOwnerAndRecent(promptHelper, botToken, channel, threadTs, ownBotUsername, allPrompts);

        // 쓰레드 전체 조회 → 봇 대화명(username) 수집
        var botUsernames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Authorization", $"Bearer {botToken}");
            // 시작 메시지(1) + 최근 7개 = 충분히 참가 클롣 파악
            var url = $"https://slack.com/api/conversations.replies?channel={channel}&ts={threadTs}&limit=40&inclusive=true";
            var resp = http.GetAsync(url).GetAwaiter().GetResult();
            var body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var json = System.Text.Json.Nodes.JsonNode.Parse(body);
            if (json?["ok"]?.GetValue<bool>() == true)
            {
                foreach (var m in json["messages"]?.AsArray() ?? new System.Text.Json.Nodes.JsonArray())
                {
                    var uname = m?["username"]?.GetValue<string>();
                    if (!string.IsNullOrEmpty(uname))
                        botUsernames.Add(uname);
                }
            }
        }
        catch { }

        // Strict guard: if this Eye instance has a bot username, only route threads
        // that explicitly include the same username. Prevents cross-bot misrouting.
        if (!string.IsNullOrWhiteSpace(ownBotUsername) && botUsernames.Count > 0)
        {
            if (!botUsernames.Contains(ownBotUsername))
            {
                Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: skip thread {threadTs} (owner mismatch: expected \"{ownBotUsername}\", found [{string.Join(", ", botUsernames)}])");
                return new List<ClaudePromptHelper.PromptInfo>();
            }
            botUsernames = new HashSet<string>(new[] { ownBotUsername }, StringComparer.OrdinalIgnoreCase);
        }

        if (botUsernames.Count == 0)
        {
            // 참가 봇 없음 (원본 메시지 삭제됐거나 처음 도착한 쓰레드)
            // → 전체 브로드캐스트 대신 Eye 본인 CWD 창에만 전달
            var myPrompt = ResolveWorkspaceScopedPrompt(promptHelper);
            if (myPrompt != null)
            {
                Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: no bot usernames — delivering to own CWD window only");
                return new List<ClaudePromptHelper.PromptInfo> { myPrompt };
            }
            Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: no bot usernames, own CWD not found — sending to all");
            return new List<ClaudePromptHelper.PromptInfo>();
        }

        // "클롣 [WG-WKAppBot]" → "WG-WKAppBot" 추출 + leaf name도 추가
        // AbbreviateCwd("W:\GitHub\WKAppBot") = "WG-WKAppBot" → leaf = "WKAppBot"
        // 윈도우 타이틀엔 원본 폴더명("WKAppBot")만 있으므로 leaf도 매칭 시도
        var cwdNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var uname in botUsernames)
        {
            var start = uname.IndexOf('[');
            var end = uname.IndexOf(']');
            if (start >= 0 && end > start)
            {
                var tag = uname[(start + 1)..end];
                cwdNames.Add(tag);
                // Also add leaf part: "WG-WKAppBot" → "WKAppBot"
                var lastDash = tag.LastIndexOf('-');
                if (lastDash >= 0 && lastDash < tag.Length - 1)
                    cwdNames.Add(tag[(lastDash + 1)..]);
            }
        }

        // 프롬프트 윈도우 타이틀에서 매칭
        // GetPromptSearchableTitle: VS Code "채팅제목 - 작업폴더 - Visual Studio Code" → "작업폴더"
        // 채팅 제목에 다른 프로젝트명이 포함돼도 오매칭 안 됨 (GetPromptSearchableTitle로 통일)
        var matched = new List<ClaudePromptHelper.PromptInfo>();
        foreach (var p in allPrompts)
        {
            var titleToSearch = GetPromptSearchableTitle(p);
            Console.WriteLine($"[EYE][SLACK] title-match: 0x{p.WindowHandle:X} host={p.HostType} folder=\"{titleToSearch}\" cwds=[{string.Join(", ", cwdNames)}]");
            foreach (var cwd in cwdNames)
            {
                if (titleToSearch.Contains(cwd, StringComparison.OrdinalIgnoreCase))
                {
                    matched.Add(p);
                    break;
                }
            }
        }

        if (matched.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: {matched.Count}/{allPrompts.Count} prompts matched (usernames: {string.Join(", ", botUsernames)})");
            Console.ResetColor();
            return matched;
        }

        // 봇 username은 있는데 프롬프트 매칭 안 됨 → 같은 워크스페이스의 모든 AI에게 브로드캐스트
        var myFallback = ResolveWorkspaceScopedPrompts(promptHelper);
        if (myFallback.Count > 0)
        {
            Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: no match for [{string.Join(", ", botUsernames)}] — broadcast to {myFallback.Count} workspace prompt(s)");
            return myFallback;
        }
        Console.WriteLine($"[EYE][SLACK] FindPromptsForThread: no prompt match for usernames [{string.Join(", ", botUsernames)}], skip routing");
        return new List<ClaudePromptHelper.PromptInfo>();
    }

    // Legacy alias kept for merge compatibility with older branches.
    static List<ClaudePromptHelper.PromptInfo> FindPromptsForThread(
        ClaudePromptHelper promptHelper, string botToken, string channel, string threadTs) =>
        ResolveThreadScopedPrompts(promptHelper, botToken, channel, threadTs);

    /// <summary>
    /// 위치확보 후 프롬프트 입력. 모든 자동입력 전에 Probe 호출.
    /// 슬랙 요청 = AutoApproveYield (승인만 자동, 돋보기+확보는 정상).
    /// 최소화 창 → SW_SHOWNOACTIVATE 포커스리스 리스토어 후 재시도.
    /// slackMsgTs: dedup용 Slack 메시지 ts — 프롬프트 창 Win32 Prop에 저장, Eye 재시작에도 생존.
    /// </summary>
    static bool ProbeAndSubmit(ClaudePromptHelper promptHelper, ClaudePromptHelper.PromptInfo prompt, string text, string? slackMsgTs = null)
    {
        var shortTitle = prompt.WindowTitle.Length > 40
            ? prompt.WindowTitle[..37] + "..." : prompt.WindowTitle;

        // ── Dedup: check if this message was already forwarded to this prompt window ──
        if (slackMsgTs != null && prompt.WindowHandle != IntPtr.Zero)
        {
            var stored = NativeMethods.GetPropW(prompt.WindowHandle, "WKAppBot_LastSlackTs");
            if (stored != IntPtr.Zero)
            {
                var storedTs = stored.ToInt64();
                // Parse Slack ts integer part (seconds since epoch)
                var dotIdx = slackMsgTs.IndexOf('.');
                var tsSecStr = dotIdx > 0 ? slackMsgTs[..dotIdx] : slackMsgTs;
                if (long.TryParse(tsSecStr, out var msgTsSec) && msgTsSec <= storedTs)
                {
                    Console.WriteLine($"  [SLACK→PROMPT] DEDUP: ts={slackMsgTs} already forwarded to 0x{prompt.WindowHandle:X} (stored={storedTs})");
                    return false;
                }
            }
        }

        Console.WriteLine($"  [SLACK→PROMPT] 위치확보 시작: 0x{prompt.WindowHandle:X} \"{shortTitle}\"");

        var readiness = CreateInputReadiness();
        var report = readiness.Probe(new InputReadinessRequest
        {
            TargetHwnd = prompt.WindowHandle,
            IntendedAction = "key", // 키보드 입력 → 항상 포커스 필요
            AutoApproveYield = true, // 슬랙 요청 → 양보 자동승인
            SkipKnowhow = true, // 프롬프트는 노하우 불필요
            EnsureInputPosition = () => promptHelper.EnsureCaretInPrompt(prompt),
        });

        // ── 최소화 감지 → 포커스리스 리스토어 후 재 Probe ──
        if (report.FormIconic)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  [SLACK→PROMPT] 최소화 감지 → 포커스리스 리스토어 시도");
            Console.ResetColor();
            report.Zoom?.Dispose();

            // SW_SHOWNOACTIVATE: 포커스 안 뺏고 원래 위치에 복원
            var wp = new NativeMethods.WINDOWPLACEMENT();
            wp.length = System.Runtime.InteropServices.Marshal.SizeOf(wp);
            if (NativeMethods.GetWindowPlacement(prompt.WindowHandle, ref wp))
            {
                wp.showCmd = NativeMethods.SW_SHOWNOACTIVATE;
                NativeMethods.SetWindowPlacement(prompt.WindowHandle, ref wp);
                Console.WriteLine($"  [SLACK→PROMPT] 포커스리스 리스토어 완료 (SW_SHOWNOACTIVATE)");
                Thread.Sleep(300); // UI 갱신 대기

                // 재 Probe
                report = readiness.Probe(new InputReadinessRequest
                {
                    TargetHwnd = prompt.WindowHandle,
                    IntendedAction = "key",
                    AutoApproveYield = true,
                    SkipKnowhow = true,
                    EnsureInputPosition = () => promptHelper.EnsureCaretInPrompt(prompt),
                });
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  [SLACK→PROMPT] GetWindowPlacement 실패");
                Console.ResetColor();
                return false;
            }
        }

        // ── 위치확보 결과 요약 ──
        var issues = new List<string>();
        if (report.ActiveBlocker != null)
            issues.Add($"blocker={report.ActiveBlocker.Title}");
        if (report.ElevationMismatch)
            issues.Add("elevation-mismatch");
        if (!report.FormVisible)
            issues.Add("not-visible");
        if (!report.FormEnabled)
            issues.Add("not-enabled");
        if (report.FormIconic)
            issues.Add("still-minimized");
        if (report.UserYieldRequested && !report.UserYieldConfirmed)
            issues.Add("yield-denied");
        if (report.UserYieldConfirmed && !report.UserYieldFocusAcquired)
            issues.Add("focus-acquire-failed");

        if (issues.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  [SLACK→PROMPT] 위치확보 실패: {string.Join(", ", issues)}");
            Console.ResetColor();
            report.Zoom?.Dispose();
            return false;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  [SLACK→PROMPT] 위치확보 OK — 입력 시작");
        Console.ResetColor();

        bool result = false;
        report.Zoom?.Dispose();

        // ── 기존 입력 내용 확인 ──
        var existingInput = promptHelper.ReadCurrentInputText(prompt)?.Trim();
        if (!string.IsNullOrEmpty(existingInput))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  [SLACK→PROMPT] 기존 입력 내용 감지 ({existingInput.Length}자): \"{existingInput[..Math.Min(60, existingInput.Length)]}...\"");
            Console.ResetColor();

            // 승인창으로 덮어쓰기 확인 요청
            var preview = existingInput.Length > 40 ? existingInput[..40] + "…" : existingInput;
            var actionInfo = $"기존: \"{preview}\"\n→ 슬랙 메시지로 교체 후 전달";
            var idleMs = NativeMethods.GetUserIdleMs();
            var yieldTimeout = 3; // Slack routing = user-initiated → always 3s auto-approve
            Console.WriteLine($"  [SLACK→PROMPT] idle={idleMs}ms → yieldTimeout={yieldTimeout}s");
            var (approved, _, deniedByUser) = UserInputWaitOverlay.Show(
                prompt.WindowHandle, userIdleMs: idleMs, timeoutSeconds: yieldTimeout,
                positionHwnd: prompt.WindowHandle, noSound: true,
                actionInfo: actionInfo);

            if (!approved || deniedByUser)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  [SLACK→PROMPT] 덮어쓰기 거부 — 전달 취소");
                Console.ResetColor();
                return false;
            }
            Console.WriteLine($"  [SLACK→PROMPT] 덮어쓰기 승인 — 기존 내용 저장 후 전달");
        }

        var isCodexPrompt = string.Equals(prompt.HostType, "codex-desktop", StringComparison.OrdinalIgnoreCase);
        var isVsCodePrompt = string.Equals(prompt.HostType, "vscode-claudecode", StringComparison.OrdinalIgnoreCase);
        if (isCodexPrompt || isVsCodePrompt)
        {
            if (isCodexPrompt)
                Console.WriteLine("  [WARN] codex-desktop: stage-check is limited, using legacy submit path");
            // Probe() already confirmed user yield — allow focus-steal as last resort
            var prevAllow = ClaudePromptHelper.AllowFocusSteal;
            try
            {
                ClaudePromptHelper.AllowFocusSteal = true;
                // 기존 내용이 있으면 먼저 지우기 (안 지우면 append됨)
                if (!string.IsNullOrEmpty(existingInput))
                {
                    var cleared = promptHelper.ClearCurrentInput(prompt);
                    Console.WriteLine($"  [SLACK→PROMPT] ClearCurrentInput={cleared}");
                    Thread.Sleep(50);
                }
                result = promptHelper.TypeAndSubmit(prompt, text);
            }
            finally { ClaudePromptHelper.AllowFocusSteal = prevAllow; }

            // ── 전달 완료 후 기존 내용 복구 ──
            if (result && !string.IsNullOrEmpty(existingInput))
            {
                Thread.Sleep(300); // 전송 처리 대기
                Console.WriteLine($"  [SLACK→PROMPT] 기존 입력 내용 복구 시도 ({existingInput.Length}자)");
                try
                {
                    ClaudePromptHelper.AllowFocusSteal = true;
                    var restored = promptHelper.TypeWithoutSubmit(prompt, existingInput);
                    Console.WriteLine(restored
                        ? $"  [SLACK→PROMPT] 기존 입력 복구 완료"
                        : $"  [WARN] 기존 입력 복구 실패 (Claude 응답 처리 중일 수 있음)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  [WARN] 기존 입력 복구 예외: {ex.Message}");
                }
                finally { ClaudePromptHelper.AllowFocusSteal = prevAllow; }
            }
        }
        else for (int attempt = 1; attempt <= 3; attempt++)
        {
            Console.WriteLine($"  [SLACK->PROMPT] stage attempt {attempt}/3");

            var typed = promptHelper.TypeWithoutSubmit(prompt, text);
            if (!typed)
            {
                Console.WriteLine("  [WARN] stage=type result=failed");
                continue;
            }

            var before = promptHelper.ProbeSubmitState(prompt);
            Console.WriteLine($"  [SLACK->PROMPT] before turnForm={before.TurnFormFound} submit={before.SubmitFound} enabled={before.SubmitEnabled} name=\"{before.SubmitName}\"");
            if (!before.TurnFormFound || !before.SubmitFound || !before.SubmitEnabled)
            {
                Console.WriteLine("  [WARN] stage=pre-submit-check result=failed");
                continue;
            }

            var prevAllowFocusSteal = ClaudePromptHelper.AllowFocusSteal;
            try
            {
                ClaudePromptHelper.AllowFocusSteal = attempt > 1;
                var submitted = promptHelper.SubmitExistingInput(prompt);
                if (!submitted)
                {
                    Console.WriteLine("  [WARN] stage=submit result=failed");
                    continue;
                }

                var accepted = promptHelper.VerifySubmitAccepted(prompt, 2200);
                var after = promptHelper.ProbeSubmitState(prompt);
                Console.WriteLine($"  [SLACK->PROMPT] after turnForm={after.TurnFormFound} submit={after.SubmitFound} enabled={after.SubmitEnabled} name=\"{after.SubmitName}\" accepted={accepted}");
                if (accepted)
                {
                    result = true;
                    break;
                }

                Console.WriteLine("  [WARN] stage=post-submit-check result=failed");
            }
            finally
            {
                ClaudePromptHelper.AllowFocusSteal = prevAllowFocusSteal;
            }
        }

        // ── Store forwarded ts on prompt window (survives Eye restart) ──
        if (result && slackMsgTs != null && prompt.WindowHandle != IntPtr.Zero)
        {
            var dotIdx = slackMsgTs.IndexOf('.');
            var tsSecStr = dotIdx > 0 ? slackMsgTs[..dotIdx] : slackMsgTs;
            if (long.TryParse(tsSecStr, out var msgTsSec))
            {
                NativeMethods.SetPropW(prompt.WindowHandle, "WKAppBot_LastSlackTs", new IntPtr(msgTsSec));
                Console.WriteLine($"  [SLACK→PROMPT] Stored ts={msgTsSec} on 0x{prompt.WindowHandle:X}");
            }
        }

        return result;
    }

    /// <summary>
    /// Set up Slack event handlers for AppBotEye-integrated Slack daemon.
    /// Handles @mentions, keyword monitoring, thread reply forwarding, and plan approval responses.
    /// RULE: User replies to bot messages are ALWAYS forwarded to Claude prompt.
    /// </summary>
    private static void SetupSlackEventHandlers(SlackSocketClient slack, string botToken, string? channel,
        Func<IntPtr>? getClaudeHwnd = null, Func<string?>? getPlanApprovalTs = null,
        Func<string?>? getPermissionApprovalTs = null,
        string? startupTs = null, string? botUsername = null,
        Func<string?>? getStatusStreamingTs = null, Action? resetStatusStreaming = null)
    {
        // Re-detect Claude Desktop window on every event call (handles Electron restart)
        IntPtr GetClaudeHwnd() => getClaudeHwnd?.Invoke() ?? IntPtr.Zero;

        // Track threads where bot is engaged (for thread reply forwarding)
        // RULE: User replies to bot messages are ALWAYS forwarded to Claude prompt.
        var activeThreads = new HashSet<string>();
        // Dedup: messages already forwarded by OnMention → skip in OnMessage thread handler
        var handledByMention = new HashSet<string>();
        if (!string.IsNullOrEmpty(startupTs))
            activeThreads.Add(startupTs);

        // Restore active threads from pending_acks.json (survives Eye restart)
        var savedAcks = LoadPendingAcks();
        foreach (var threadTs in savedAcks.Keys)
            activeThreads.Add(threadTs);
        if (savedAcks.Count > 0)
            Console.WriteLine($"[EYE][SLACK] Restored {savedAcks.Count} active thread(s) from pending_acks.json");

        // Track "전달했습니다" ack messages per thread → delete when Claude responds via this bot
        // Key: threadTs, Value: (channel, ackMessageTs)
        var pendingAcks = new Dictionary<string, (string channel, string ackTs)>();
        foreach (var kv in savedAcks)
            pendingAcks[kv.Key] = (kv.Value.Channel, kv.Value.AckTs);

        // Local helper: send with bot username (multi-instance identification)
        async Task<(bool ok, string? ts)> Send(string ch, string text, string? threadTs = null)
            => await SlackSendViaApi(botToken, ch, text, threadTs, username: botUsername);

        static bool IsTruthy(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            var v = value.Trim().ToLowerInvariant();
            return v is "1" or "true" or "yes" or "on";
        }

        var envPromptNotFoundToChannel = Environment.GetEnvironmentVariable("WKAPPBOT_PROMPT_NOT_FOUND_TO_CHANNEL");
        bool promptNotFoundAlsoToChannel = string.IsNullOrWhiteSpace(envPromptNotFoundToChannel)
            ? true
            : IsTruthy(envPromptNotFoundToChannel);
        if (string.IsNullOrWhiteSpace(envPromptNotFoundToChannel))
        {
            try
            {
                var cfgPath = Path.Combine(DataDir, "profiles", "slack_exp", "webhook.json");
                if (File.Exists(cfgPath))
                {
                    var node = JsonNode.Parse(File.ReadAllText(cfgPath));
                    var opt = node?["prompt_not_found_to_channel"];
                    if (opt != null)
                    {
                        try { promptNotFoundAlsoToChannel = opt.GetValue<bool>(); }
                        catch { promptNotFoundAlsoToChannel = IsTruthy(opt.ToString()); }
                    }
                }
            }
            catch { }
        }
        Console.WriteLine($"[EYE][SLACK] prompt_not_found_to_channel={(promptNotFoundAlsoToChannel ? "ON" : "OFF")}");

        void SendPromptNotFound(string ch, string text, string? threadTs, string originTs)
        {
            Task.Run(async () => await Send(ch, text, threadTs)).Wait(5000);
            if (!promptNotFoundAlsoToChannel) return;

            var brief = $":warning: AI prompt not found! (origin ts={originTs})";
            Task.Run(async () => await Send(ch, brief, null)).Wait(5000);
        }

        // Local helper: delete previous "전달했습니다" ack in a thread (file-based IPC)
        void DeletePendingAck(string threadKey)
        {
            // In-memory cleanup
            if (pendingAcks.TryGetValue(threadKey, out var ack))
            {
                pendingAcks.Remove(threadKey);
                Task.Run(async () => await SlackDeleteMessageAsync(botToken, ack.channel, ack.ackTs, guardThreadStarter: false)).Wait(3000);
            }
            // File-based cleanup (for cross-process sharing with CLI)
            DeletePendingAckFromFile(botToken, threadKey);
        }

        // Local helper: send ack "전달했습니다" and track it for later deletion
        // Deleted when slack reply is sent (not auto-deleted — stays visible until response)
        const string AckUsername = "앱봇아이";
        void SendAndTrackAck(string ch, string threadKey, int promptCount = 1, int totalAttempted = 0,
            List<DeliveryResult>? results = null)
        {
            string ackText;
            if (totalAttempted > 0 && promptCount < totalAttempted)
                ackText = $":warning: 전달 {promptCount}/{totalAttempted} (partial failure)";
            else if (promptCount > 1)
                ackText = $"{promptCount}곳에 전달했습니다!";
            else if (results?.Count == 1)
                ackText = $"{results[0].ShortName}에 전달했습니다! {(results[0].Sent ? ":white_check_mark:" : ":x:")}";
            else
                ackText = "전달했습니다!";

            // Append per-target status for multi-target
            if (results != null && results.Count > 1)
            {
                var lines = results.Select(r =>
                    $"• {r.ShortName} {(r.Sent ? ":white_check_mark:" : ":x:")}");
                ackText += "\n" + string.Join("\n", lines);
            }

            // Keep old ack visible until the new ack is confirmed.
            // If posting fails, do not delete the previous ack.
            var (ackOk, ackTs) = Task.Run(async () =>
            {
                var first = await SlackSendViaApi(botToken, ch, ackText, threadKey, username: AckUsername);
                if (first.ok) return first;

                // Keep sender identity deterministic for ACK messages.
                Console.WriteLine("[EYE][SLACK] Ack send failed with fixed ack username");
                return first;
            }).GetAwaiter().GetResult();
            if (ackOk && ackTs != null)
            {
                if (pendingAcks.TryGetValue(threadKey, out var oldAck))
                    Task.Run(async () => await SlackDeleteMessageAsync(botToken, oldAck.channel, oldAck.ackTs, guardThreadStarter: false)).Wait(3000);

                pendingAcks[threadKey] = (ch, ackTs);
                activeThreads.Add(ackTs);
                // Persist to file for cross-process access (CLI can delete when replying)
                SavePendingAck(threadKey, ch, ackTs);
            }
            else
            {
                Console.WriteLine($"[EYE][SLACK] Ack send failed, preserving previous ack (thread={threadKey})");
            }
        }

        // Handle @mentions
        slack.OnMention += (msg) =>
        {
            var cleanText = System.Text.RegularExpressions.Regex.Replace(
                msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();
            Console.WriteLine($"[EYE][SLACK] @mention from {msg.User}: {cleanText}");

            // Track this thread so ALL follow-up messages come through
            var threadKey = msg.ThreadTs ?? msg.Timestamp;
            activeThreads.Add(threadKey);

            // ── Status message thread reply → force new channel message for next status update ──
            var statusTs = getStatusStreamingTs?.Invoke();
            if (!string.IsNullOrEmpty(statusTs) && msg.ThreadTs == statusTs && resetStatusStreaming != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[EYE][SLACK] Status thread @mention → next status will be new channel message");
                Console.ResetColor();
                resetStatusStreaming();
            }

            // ── Plan approval via Slack @mention ──
            var planTs = getPlanApprovalTs?.Invoke();
            if (!string.IsNullOrEmpty(planTs) && msg.ThreadTs == planTs && GetClaudeHwnd() != IntPtr.Zero)
            {
                if (IsPlanApprovalKeyword(cleanText))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[EYE] Plan APPROVED via Slack by {msg.User}");
                    Console.ResetColor();

                    var approved = ClickApproveButton(GetClaudeHwnd());
                    var reply = approved
                        ? ":white_check_mark: 플랜 승인 완료! Claude가 코딩을 시작합니다."
                        : ":x: 승인 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                    DeletePendingAck(threadKey);
                    Task.Run(async () => await Send(msg.Channel, reply, threadKey)).Wait(5000);
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[EYE] Plan feedback via Slack: {cleanText}");
                    Console.ResetColor();

                    var feedbackOk = TypePlanFeedback(GetClaudeHwnd(), cleanText);
                    var reply = feedbackOk
                        ? $":pencil2: 피드백 전달 완료: \"{cleanText}\""
                        : ":x: 피드백 입력란을 찾을 수 없습니다";
                    DeletePendingAck(threadKey);
                    Task.Run(async () => await Send(msg.Channel, reply, threadKey)).Wait(5000);
                    return;
                }
            }

            // ── Permission approval via Slack @mention or thread reply ──
            var permTs = getPermissionApprovalTs?.Invoke();
            if (!string.IsNullOrEmpty(permTs) && msg.ThreadTs == permTs && GetClaudeHwnd() != IntPtr.Zero)
            {
                if (IsPlanApprovalKeyword(cleanText)) // "승인", "ㄱㄱ", "ㅇㅇ" etc
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[EYE] Permission APPROVED via Slack by {msg.User}");
                    Console.ResetColor();

                    var permButtons = GetPermissionButtons(GetClaudeHwnd());
                    var allowBtn = permButtons.FirstOrDefault(b =>
                        b.Contains("Allow", StringComparison.OrdinalIgnoreCase) ||
                        b.Contains("허용", StringComparison.OrdinalIgnoreCase) ||
                        b.Contains("수락", StringComparison.OrdinalIgnoreCase));
                    allowBtn ??= permButtons.FirstOrDefault(); // fallback to first button

                    bool clicked = false;
                    if (allowBtn != null)
                        clicked = ClickPermissionButton(GetClaudeHwnd(), allowBtn);

                    var reply = clicked
                        ? $":white_check_mark: 권한 승인 완료! (\"{allowBtn}\")"
                        : ":x: 권한 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                    DeletePendingAck(threadKey);
                    Task.Run(async () => await Send(msg.Channel, reply, threadKey)).Wait(5000);
                    return;
                }
            }

            // ── Normal @mention: broadcast to ALL prompts (멘션=폴더구분 없음) ──
            ClaudePromptHelper.AllowFocusSteal = true; // fallback path용
            var promptHelper = new ClaudePromptHelper();
            var allMentionPrompts = promptHelper.FindAllPrompts();
            if (allMentionPrompts.Count > 0)
            {
                // Build thread context (starter + previous message) for Claude
                var threadContext = "";
                var ackThread = msg.ThreadTs ?? msg.Timestamp;
                if (msg.ThreadTs != null)
                {
                    var ctx = GetThreadContext(botToken, msg.Channel, msg.ThreadTs, msg.Timestamp);
                    if (!string.IsNullOrEmpty(ctx))
                        threadContext = $"\n{ctx}\n";
                }

                var replyThread = msg.ThreadTs ?? msg.Timestamp;
                int mentionSent = 0;
                var mentionResults = new List<DeliveryResult>();
                foreach (var pi in allMentionPrompts)
                {
                    try
                    {
                        var promptText = $"{cleanText}{threadContext}\n{SlackReplySuffix(msg.User, replyThread)}";
                        var ok = ProbeAndSubmit(promptHelper, pi, promptText, msg.Timestamp);
                        mentionResults.Add(new DeliveryResult(ExtractProjectName(pi), ok));
                        if (ok) mentionSent++;
                    }
                    catch (Exception ex)
                    {
                        mentionResults.Add(new DeliveryResult(ExtractProjectName(pi), false));
                        Console.WriteLine($"[EYE][SLACK] Mention broadcast error: {ex.Message}");
                    }
                }
                handledByMention.Add(msg.Timestamp); // dedup: OnMessage won't re-forward
                Console.WriteLine($"[EYE][SLACK] >> Mention broadcast: {mentionSent}/{allMentionPrompts.Count} prompts");

                SendAndTrackAck(msg.Channel, ackThread, mentionSent, allMentionPrompts.Count, mentionResults);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[EYE][SLACK] Prompt not found! 새 채팅 폴백 시도...");
                Console.ResetColor();

                var ackThread = msg.ThreadTs ?? msg.Timestamp;

                // ★ New-chat fallback: if Claude Desktop window exists, try click+paste
                if (GetClaudeHwnd() != IntPtr.Zero)
                {
                    var replyThread = msg.ThreadTs ?? msg.Timestamp;
                    var threadContext = "";
                    if (msg.ThreadTs != null)
                    {
                        var ctx = GetThreadContext(botToken, msg.Channel, msg.ThreadTs, msg.Timestamp);
                        if (!string.IsNullOrEmpty(ctx)) threadContext = $"\n{ctx}\n";
                    }
                    var promptText = $"{cleanText}{threadContext}\n{SlackReplySuffix(msg.User, replyThread)}";

                    if (promptHelper.TryNewChatInput(GetClaudeHwnd(), promptText))
                    {
                        Console.WriteLine("[EYE][SLACK] >> New-chat fallback SUCCESS!");
                        handledByMention.Add(msg.Timestamp);
                        SendAndTrackAck(msg.Channel, ackThread);
                        return; // skip diagnosis
                    }
                    Console.WriteLine("[EYE][SLACK] New-chat fallback FAILED, running diagnosis...");
                }

                try
                {
                    // Comprehensive diagnosis
                    var claudeStatus = GetClaudeHwnd() != IntPtr.Zero ? DetectClaudeDesktopStatus(GetClaudeHwnd()) : null;
                    var statusInfo = claudeStatus != null
                        ? $"AI status: {claudeStatus.Item2}"
                        : "AI status: unavailable";

                    string diagDetail;
                    try { diagDetail = promptHelper.DiagnosePromptSearch(); }
                    catch (Exception dex) { diagDetail = $"(진단 실패: {dex.Message})"; }

                    Console.WriteLine(diagDetail);
                    try
                    {
                        var diagPath = Path.Combine(DataDir, "logs", $"prompt_diag_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                        File.WriteAllText(diagPath, $"{statusInfo}\n{diagDetail}\n받은 메시지: {cleanText}");
                        Console.WriteLine($"[EYE] Diagnosis saved: {diagPath}");
                    }
                    catch { }

                    var slackMsg = $":warning: AI prompt not found!\n" +
                        $"{statusInfo}\n" +
                        $"받은 메시지: {cleanText}\n" +
                        $"```\n{(diagDetail.Length > 800 ? diagDetail.Substring(0, 800) + "\n...(truncated)" : diagDetail)}\n```";
                    SendPromptNotFound(msg.Channel, slackMsg, ackThread, msg.Timestamp);
                }
                catch
                {
                    Task.Run(async () => await Send(msg.Channel,
                        $"(Claude 프롬프트 없음) 받은 메시지: {cleanText}", ackThread)).Wait(5000);
                }
            }
        };

        // Handle bot's own messages in threads → delete pending "전달했습니다" ack
        slack.OnSelfMessage += (msg) =>
        {
            if (string.IsNullOrEmpty(msg.ThreadTs)) return;
            // Skip ack messages (posted by "클롣아이") — never trigger self-deletion
            if (msg.Username == AckUsername) return;
            // Only delete ack if the replying session matches THIS Eye's botUsername
            // (prevents other sessions' replies from deleting our ack too early)
            if (!string.IsNullOrEmpty(msg.Username) && !string.IsNullOrEmpty(botUsername)
                && !msg.Username.Equals(botUsername, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[EYE][SLACK] Ack preserved: reply from \"{msg.Username}\" ≠ \"{botUsername}\"");
                return;
            }
            DeletePendingAck(msg.ThreadTs);
            Console.WriteLine($"[EYE][SLACK] Deleted ack in thread {msg.ThreadTs} (bot replied)");
        };

        // Handle channel messages (thread reply forwarding + keyword monitoring + plan approval)
        var keywords = new[] { "클롣", "클롯", "클봇", "claude", "appbot", "wkappbot" };
        slack.OnMessage += (msg) =>
        {
            if (string.IsNullOrEmpty(msg.Text)) return;
            // Fire-and-forget: don't block WebSocket receive thread
            // (InputReadiness approval can take 30s+ → next messages would stall)
            _ = Task.Run(() =>
            {

            // Debug: log thread info for diagnosis
            Console.WriteLine($"[EYE][SLACK] MSG from={msg.User} ch={msg.Channel} thread={msg.ThreadTs ?? "(none)"} text={msg.Text[..Math.Min(msg.Text.Length, 40)]}");

            // ── Status message thread reply → force new channel message for next status update ──
            var statusTs = getStatusStreamingTs?.Invoke();
            if (!string.IsNullOrEmpty(statusTs) && msg.ThreadTs == statusTs && resetStatusStreaming != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[EYE][SLACK] Status thread reply detected → next status will be new channel message");
                Console.ResetColor();
                resetStatusStreaming();
                // Don't return — continue to forward the message to Claude prompt as well
            }

            // ── Plan approval via thread reply (no @mention needed) ──
            var planTs = getPlanApprovalTs?.Invoke();
            if (!string.IsNullOrEmpty(planTs) && msg.ThreadTs == planTs && GetClaudeHwnd() != IntPtr.Zero)
            {
                var cleanText = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();

                if (IsPlanApprovalKeyword(cleanText))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[EYE] Plan APPROVED via Slack thread by {msg.User}");
                    Console.ResetColor();

                    var approved = ClickApproveButton(GetClaudeHwnd());
                    var reply = approved
                        ? ":white_check_mark: 플랜 승인 완료! Claude가 코딩을 시작합니다."
                        : ":x: 승인 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                    DeletePendingAck(msg.ThreadTs!);
                    Task.Run(async () => await Send(msg.Channel, reply, msg.ThreadTs)).Wait(5000);
                    return;
                }
                else if (cleanText.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[EYE] Plan feedback via Slack thread: {cleanText}");
                    Console.ResetColor();

                    var feedbackOk = TypePlanFeedback(GetClaudeHwnd(), cleanText);
                    var reply = feedbackOk
                        ? $":pencil2: 피드백 전달 완료: \"{cleanText}\""
                        : ":x: 피드백 입력란을 찾을 수 없습니다";
                    DeletePendingAck(msg.ThreadTs!);
                    Task.Run(async () => await Send(msg.Channel, reply, msg.ThreadTs)).Wait(5000);
                    return;
                }
            }

            // ── Permission approval via thread reply (no @mention needed) ──
            var permTs2 = getPermissionApprovalTs?.Invoke();
            if (!string.IsNullOrEmpty(permTs2) && msg.ThreadTs == permTs2 && GetClaudeHwnd() != IntPtr.Zero)
            {
                var cleanPerm = System.Text.RegularExpressions.Regex.Replace(
                    msg.Text, @"<@[A-Z0-9]+>\s*", "").Trim();

                if (IsPlanApprovalKeyword(cleanPerm))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[EYE] Permission APPROVED via Slack thread by {msg.User}");
                    Console.ResetColor();

                    var permBtns = GetPermissionButtons(GetClaudeHwnd());
                    var allowBtn = permBtns.FirstOrDefault(b =>
                        b.Contains("Allow", StringComparison.OrdinalIgnoreCase) ||
                        b.Contains("허용", StringComparison.OrdinalIgnoreCase) ||
                        b.Contains("수락", StringComparison.OrdinalIgnoreCase));
                    allowBtn ??= permBtns.FirstOrDefault();

                    bool clicked = false;
                    if (allowBtn != null)
                        clicked = ClickPermissionButton(GetClaudeHwnd(), allowBtn);

                    var reply = clicked
                        ? $":white_check_mark: 권한 승인 완료! (\"{allowBtn}\")"
                        : ":x: 권한 버튼을 찾을 수 없습니다 (이미 처리되었거나 화면이 변경됨)";
                    DeletePendingAck(msg.ThreadTs!);
                    Task.Run(async () => await Send(msg.Channel, reply, msg.ThreadTs)).Wait(5000);
                    return;
                }
            }

            // ── Dedup: already forwarded by OnMention → skip ──
            if (handledByMention.Remove(msg.Timestamp))
            {
                Console.WriteLine($"[EYE][SLACK] DEDUP skip (already by OnMention): {msg.Text[..Math.Min(msg.Text.Length, 30)]}");
                return;
            }

            // ── Dispatch to route worker (fire-and-forget) ──
            Console.WriteLine($"[EYE][SLACK] → DispatchBg slack route: {msg.Text[..Math.Min(msg.Text.Length, 40)]}");
            // slack route handles: thread reply / keyword / catch-all → inject + ack
            var routeJson = System.Text.Json.JsonSerializer.Serialize(new
            {
                text = msg.Text, user = msg.User, ts = msg.Timestamp,
                threadTs = msg.ThreadTs, channel = msg.Channel
            });
            EyeCmdPipeServer.DispatchBg(["slack", "route", routeJson]);
            }); // end Task.Run — WebSocket thread now free for next message
        };
    }

    /// <summary>
    /// Execute a single schedule item: resolve prompt text, find Claude prompt, type and submit.
    /// Updates schedule status to done/failed. Sends Slack notification.
    /// </summary>
    internal static void ExecuteScheduleItem(ScheduleItem item, string? slackBotToken, string? slackChannel)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[SCHEDULE] Executing: {item.Id} ({item.Type})");
        Console.ResetColor();

        try
        {
            // 0. Shell command mode
            if (!string.IsNullOrEmpty(item.Command))
            {
                if (item.NotifySlack)
                    ScheduleNotifySlack(slackBotToken, slackChannel,
                        $":rocket: 스케줄 커맨드 실행: `{item.Command}`");
                var psi = new System.Diagnostics.ProcessStartInfo("cmd.exe", $"/c {item.Command}")
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = item.Cwd ?? Environment.CurrentDirectory,
                };
                using var proc = System.Diagnostics.Process.Start(psi)!;
                var stdout = proc.StandardOutput.ReadToEnd();
                var stderr = proc.StandardError.ReadToEnd();
                proc.WaitForExit(30_000);
                var output = (stdout + stderr).Trim();
                Console.WriteLine($"[SCHEDULE:CMD] exit={proc.ExitCode} output={output.Length}ch");
                ScheduleManager.UpdateStatus(item.Id, proc.ExitCode == 0 ? "done" : "failed", output.Length > 0 ? output[..Math.Min(200, output.Length)] : null);
                if (item.NotifySlack)
                    ScheduleNotifySlack(slackBotToken, slackChannel,
                        $"{(proc.ExitCode == 0 ? ":white_check_mark:" : ":x:")} 커맨드 완료 (exit={proc.ExitCode}){(output.Length > 0 ? $"\n```{output[..Math.Min(300, output.Length)]}```" : "")}");
                if (item.Type == "recurring") ScheduleManager.AdvanceRecurring(item.Id);
                return;
            }

            // 1. Resolve prompt text
            string? promptText = item.Prompt;
            if (!string.IsNullOrEmpty(item.PromptFile))
            {
                if (File.Exists(item.PromptFile))
                    promptText = File.ReadAllText(item.PromptFile);
                else
                {
                    ScheduleManager.UpdateStatus(item.Id, "failed", $"Prompt file not found: {item.PromptFile}");
                    ScheduleNotifySlack(slackBotToken, slackChannel,
                        $":x: 스케줄 실패 ({item.Id}): 프롬프트 파일 없음 — {item.PromptFile}");
                    return;
                }
            }

            if (string.IsNullOrEmpty(promptText))
            {
                ScheduleManager.UpdateStatus(item.Id, "failed", "Empty prompt");
                return;
            }

            // 2. Slack pre-notification
            if (item.NotifySlack)
                ScheduleNotifySlack(slackBotToken, slackChannel,
                    $":rocket: 스케줄 실행 중: {item.Id} ({item.Type})");

            // 3. Find Claude prompt and type
            var promptHelper = new ClaudePromptHelper();
            var promptInfo = ResolveWorkspaceScopedPrompt(promptHelper);
            if (promptInfo == null)
            {
                // Retry once after 3 seconds (Claude may still be loading)
                Thread.Sleep(3000);
                promptInfo = ResolveWorkspaceScopedPrompt(promptHelper);
            }

            if (promptInfo == null)
            {
                ScheduleManager.UpdateStatus(item.Id, "failed", "Claude prompt not found");
                ScheduleNotifySlack(slackBotToken, slackChannel,
                    $":x: 스케줄 실패 ({item.Id}): Claude 프롬프트를 찾을 수 없습니다");
                return;
            }

            // 4. 위치확보 + Type and submit
            ClaudePromptHelper.AllowFocusSteal = true; // schedule = auto request
            var suffix = $"\n\n(자동 복구 — schedule {item.Id}, {item.Type})";
            ProbeAndSubmit(promptHelper, promptInfo, promptText + suffix);

            // 5. Mark done + advance recurring
            ScheduleManager.UpdateStatus(item.Id, "done");
            if (item.Type == "recurring")
                ScheduleManager.AdvanceRecurring(item.Id);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SCHEDULE] Done: {item.Id}");
            Console.ResetColor();

            if (item.NotifySlack)
                ScheduleNotifySlack(slackBotToken, slackChannel,
                    $":white_check_mark: 스케줄 완료: {item.Id} — 프롬프트 입력 완료");
        }
        catch (Exception ex)
        {
            ScheduleManager.UpdateStatus(item.Id, "failed", ex.Message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[SCHEDULE] Failed: {item.Id} — {ex.Message}");
            Console.ResetColor();
            ScheduleNotifySlack(slackBotToken, slackChannel,
                $":x: 스케줄 실패 ({item.Id}): {ex.Message}");
        }
    }

    /// <summary>Send Slack notification for schedule events (best-effort, non-blocking).</summary>
    private static void ScheduleNotifySlack(string? botToken, string? channel, string message)
    {
        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel)) return;
        try
        {
            Task.Run(async () => await SlackSendViaApi(botToken!, channel!, message, username: BotUsername)).Wait(5000);
        }
        catch { /* best-effort */ }
    }
}
