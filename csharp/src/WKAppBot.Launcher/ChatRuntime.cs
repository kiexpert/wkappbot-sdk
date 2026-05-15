using System.Text;
using System.Text.RegularExpressions;

namespace WKAppBot.Launcher;

internal abstract class ChatCliProvider
{
    protected ChatCliProvider(string name, params string[] limitPatterns)
    {
        Name = name;
        LimitMatchers = limitPatterns
            .Select(p => new Regex(p, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled))
            .ToArray();
    }

    public string Name { get; }
    protected Regex[] LimitMatchers { get; }

    public bool MatchesLimit(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        return LimitMatchers.Any(rx => rx.IsMatch(text));
    }
}

internal sealed class ClaudeCliProvider : ChatCliProvider
{
    public ClaudeCliProvider() : base(
        "claude",
        @"usage\s+limit",
        @"monthly\s+usage\s+limit",
        @"rate\s+limit",
        @"5[-\s]?hour\s+limit",
        @"session\s+exhausted",
        @"quota\s+(?:exceeded|limit|reached)",
        @"too\s+many\s+requests",
        @"you'?ve\s+hit\s+your\s+usage\s+limit",
        @"upgrade\s+to\s+pro",
        @"purchase\s+more\s+credits",
        @"HTTP\s*429|\b429\b",
        @"temporarily\s+unavailable")
    {
    }
}

internal sealed class CodexCliProvider : ChatCliProvider
{
    public CodexCliProvider() : base(
        "codex",
        @"usage\s+limit",
        @"monthly\s+usage\s+limit",
        @"rate\s+limit",
        @"quota\s+(?:exceeded|limit|reached)",
        @"purchase\s+more\s+credits",
        @"upgrade\s+to\s+pro",
        @"try\s+again\s+at",
        @"HTTP\s*429|\b429\b")
    {
    }
}

internal sealed class GeminiCliProvider : ChatCliProvider
{
    public GeminiCliProvider() : base(
        "gemini",
        @"usage\s+limit",
        @"rate\s+limit",
        @"quota\s+(?:exceeded|limit|reached)",
        @"temporarily\s+unavailable",
        @"try\s+again\s+later")
    {
    }
}

internal sealed class GeminiWebProvider : ChatCliProvider
{
    public GeminiWebProvider() : base("gemini-web", @"usage\s+limit", @"rate\s+limit", @"quota\s+")
    {
    }
}

internal static class ChatCliRegistry
{
    private static readonly ChatCliProvider[] Providers =
    {
        new ClaudeCliProvider(),
        new CodexCliProvider(),
        new GeminiCliProvider(),
        new GeminiWebProvider(),
    };

    public static ChatCliProvider Resolve(string? name)
    {
        if (string.IsNullOrWhiteSpace(name)) return Providers[0];
        var normalized = name.Trim().ToLowerInvariant();
        return normalized switch
        {
            "claude" => Providers[0],
            "codex" => Providers[1],
            "gemini" or "gemini-cli" => Providers[2],
            "gemini-web" => Providers[3],
            _ => Providers[0],
        };
    }
}

internal sealed record ChatHandoffState(
    string SourceCli,
    string NextCli,
    string Reason,
    string? Prompt,
    DateTimeOffset CreatedUtc);

internal sealed record ChatHotSwapState(
    string SourceCli,
    string NextCli,
    string CleanupPolicy,
    int PreviousPid,
    string PreviousCore,
    string NextCore,
    DateTimeOffset CreatedUtc);

internal static class ChatHandoffStore
{
    private static string RuntimeDir
    {
        get
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? Environment.CurrentDirectory) ?? ".";
            return Path.Combine(exeDir, "wkappbot.hq", "runtime");
        }
    }

    private static string FilePath => Path.Combine(RuntimeDir, "chat-limit-handoff.txt");

    private static string Encode(string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

    private static string Decode(string value)
        => Encoding.UTF8.GetString(Convert.FromBase64String(value));

    public static void Stage(ChatHandoffState state)
    {
        Directory.CreateDirectory(RuntimeDir);
        var lines = new[]
        {
            "v=1",
            $"sourceCli={Encode(state.SourceCli)}",
            $"nextCli={Encode(state.NextCli)}",
            $"reason={Encode(state.Reason)}",
            $"prompt={Encode(state.Prompt ?? string.Empty)}",
            $"createdUtc={state.CreatedUtc.UtcDateTime:O}",
        };
        File.WriteAllLines(FilePath, lines, Encoding.UTF8);
    }

    public static bool TryConsume(out ChatHandoffState? state)
    {
        state = null;
        try
        {
            if (!File.Exists(FilePath)) return false;
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var rawLine in File.ReadAllLines(FilePath, Encoding.UTF8))
            {
                if (string.IsNullOrWhiteSpace(rawLine)) continue;
                var line = rawLine.Trim();
                if (line.StartsWith("#", StringComparison.Ordinal)) continue;
                var idx = line.IndexOf('=');
                if (idx <= 0) continue;
                var key = line[..idx].Trim();
                var value = line[(idx + 1)..].Trim();
                map[key] = value;
            }

            if (!map.TryGetValue("sourceCli", out var sourceCliB64)) return false;
            if (!map.TryGetValue("nextCli", out var nextCliB64)) return false;
            if (!map.TryGetValue("reason", out var reasonB64)) return false;
            if (!map.TryGetValue("createdUtc", out var createdUtcText)) return false;

            var prompt = map.TryGetValue("prompt", out var promptB64) && !string.IsNullOrEmpty(promptB64)
                ? Decode(promptB64)
                : null;

            if (!DateTimeOffset.TryParse(createdUtcText, null, System.Globalization.DateTimeStyles.RoundtripKind, out var createdUtc))
                return false;

            state = new ChatHandoffState(
                Decode(sourceCliB64),
                Decode(nextCliB64),
                Decode(reasonB64),
                prompt,
                createdUtc);
            try { File.Delete(FilePath); } catch { }
            return state != null;
        }
        catch
        {
            return false;
        }
    }
}

internal static class ChatHotSwapStore
{
    private static string RuntimeDir
    {
        get
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? Environment.CurrentDirectory) ?? ".";
            return Path.Combine(exeDir, "wkappbot.hq", "runtime");
        }
    }

    private static string FilePath => Path.Combine(RuntimeDir, "chat-hotswap-state.txt");

    private static string Encode(string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    private static string Decode(string value) => Encoding.UTF8.GetString(Convert.FromBase64String(value));

    public static void Stage(ChatHotSwapState state)
    {
        Directory.CreateDirectory(RuntimeDir);
        var lines = new[]
        {
            "v=1",
            $"sourceCli={Encode(state.SourceCli)}",
            $"nextCli={Encode(state.NextCli)}",
            $"cleanupPolicy={Encode(state.CleanupPolicy)}",
            $"previousPid={state.PreviousPid}",
            $"previousCore={Encode(state.PreviousCore)}",
            $"nextCore={Encode(state.NextCore)}",
            $"createdUtc={state.CreatedUtc.UtcDateTime:O}",
        };
        File.WriteAllLines(FilePath, lines, Encoding.UTF8);
    }

    public static bool TryConsume(out ChatHotSwapState? state)
    {
        state = null;
        try
        {
            if (!File.Exists(FilePath)) return false;

            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var rawLine in File.ReadAllLines(FilePath, Encoding.UTF8))
            {
                if (string.IsNullOrWhiteSpace(rawLine)) continue;
                var line = rawLine.Trim();
                if (line.StartsWith("#", StringComparison.Ordinal)) continue;
                var idx = line.IndexOf('=');
                if (idx <= 0) continue;
                map[line[..idx].Trim()] = line[(idx + 1)..].Trim();
            }

            if (!map.TryGetValue("sourceCli", out var sourceCliB64)) return false;
            if (!map.TryGetValue("nextCli", out var nextCliB64)) return false;
            if (!map.TryGetValue("cleanupPolicy", out var cleanupPolicyB64)) return false;
            if (!map.TryGetValue("previousPid", out var previousPidText)) return false;
            if (!map.TryGetValue("previousCore", out var previousCoreB64)) return false;
            if (!map.TryGetValue("nextCore", out var nextCoreB64)) return false;
            if (!map.TryGetValue("createdUtc", out var createdUtcText)) return false;
            if (!int.TryParse(previousPidText, out var previousPid)) return false;
            if (!DateTimeOffset.TryParse(createdUtcText, null, System.Globalization.DateTimeStyles.RoundtripKind, out var createdUtc))
                return false;

            state = new ChatHotSwapState(
                Decode(sourceCliB64),
                Decode(nextCliB64),
                Decode(cleanupPolicyB64),
                previousPid,
                Decode(previousCoreB64),
                Decode(nextCoreB64),
                createdUtc);

            try { File.Delete(FilePath); } catch { }
            return true;
        }
        catch
        {
            return false;
        }
    }
}

internal static class ChatHotSwapPolicy
{
    public static string Resolve()
    {
        var raw = Environment.GetEnvironmentVariable("WKAPPBOT_CHAT_HOTSWAP_POLICY");
        if (string.IsNullOrWhiteSpace(raw)) return "new-core-decides";
        return raw.Trim().ToLowerInvariant() switch
        {
            "new-core-decides" => "new-core-decides",
            "launcher-cleans" => "launcher-cleans",
            "old-core-drains-first" => "old-core-drains-first",
            _ => "new-core-decides",
        };
    }
}

partial class Program
{
    static bool TryGetChatProviderToken(string[] args, out string providerToken)
    {
        providerToken = "";
        if (args.Length < 2) return false;
        var token = args[1].Trim().ToLowerInvariant();
        if (token is "claude" or "codex" or "gemini" or "gemini-web")
        {
            providerToken = token;
            return true;
        }
        return false;
    }

    static bool IsChatHeadlessRequest(string[] args)
        => Console.IsInputRedirected
           || args.Any(a => a is "-p" or "--prompt" or "--print");

    static string[] GetChatPayloadArgs(string[] args, bool hasProviderToken)
    {
        if (args.Length <= 1) return Array.Empty<string>();
        return hasProviderToken
            ? args.Skip(2).ToArray()
            : args.Skip(1).ToArray();
    }

    static string BuildCommandLine(string exe, string[] args)
    {
        static string Quote(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "\"\"";
            var needsQuotes = value.Any(char.IsWhiteSpace) || value.Contains('"') || value.Contains('\\');
            if (!needsQuotes)
                return value;

            var sb = new StringBuilder();
            sb.Append('"');
            var backslashes = 0;
            foreach (var ch in value)
            {
                if (ch == '\\')
                {
                    backslashes++;
                    continue;
                }

                if (ch == '"')
                {
                    sb.Append('\\', backslashes * 2 + 1);
                    sb.Append('"');
                    backslashes = 0;
                    continue;
                }

                if (backslashes > 0)
                {
                    sb.Append('\\', backslashes);
                    backslashes = 0;
                }
                sb.Append(ch);
            }

            if (backslashes > 0)
                sb.Append('\\', backslashes * 2);

            sb.Append('"');
            return sb.ToString();
        }

        var parts = new List<string> { Quote(exe) };
        parts.AddRange(args.Select(Quote));
        return string.Join(" ", parts);
    }

    static string GetNextChatProviderAfterLimit(string provider)
        => provider.Trim().ToLowerInvariant() switch
        {
            "claude" => "codex",
            "codex" => "gemini",
            "gemini" => "gemini-web",
            "gemini-web" => "claude",
            _ => "codex",
        };

    static bool TryParseSlashSwitch(string line, out string nextCli, out string? prompt)
    {
        nextCli = "";
        prompt = null;

        var trimmed = line.Trim();
        if (!trimmed.StartsWith("/", StringComparison.Ordinal)) return false;
        if (trimmed.Length <= 1) return false;

        var body = trimmed[1..].Trim();
        if (string.IsNullOrWhiteSpace(body)) return false;

        var parts = body.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
        var token = parts[0].Trim().ToLowerInvariant();
        if (token is not ("claude" or "codex" or "gemini" or "gemini-web"))
            return false;

        nextCli = token;
        if (parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1]))
            prompt = parts[1].Trim();
        return true;
    }

    static string? GetProviderCliLinkName(string provider)
        => provider.ToLowerInvariant() switch
        {
            "codex" => "codex.exe",
            "claude" => "claude.cmd",
            "gemini" => "gemini.cmd",
            _ => null
        };

    /// <summary>
    /// Locate the on-disk entrypoint for a chat provider CLI by probing the user's
    /// PATH for the well-known executable name. Returns the absolute path when found,
    /// or null when the CLI is not installed. Stub-grade implementation -- callers
    /// already handle null/missing-file with a user-facing error.
    /// </summary>
    static string? ResolveProviderCliTarget(string provider)
    {
        var linkName = GetProviderCliLinkName(provider);
        if (string.IsNullOrWhiteSpace(linkName)) return null;
        try
        {
            // 1. Adjacent to wkappbot.exe (typical when a wrapper .cmd ships with us).
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? Environment.CurrentDirectory);
            if (!string.IsNullOrWhiteSpace(exeDir))
            {
                var adjacent = Path.Combine(exeDir, linkName);
                if (File.Exists(adjacent)) return adjacent;
            }
            // 2. PATH lookup -- match the link name verbatim (codex.exe / claude.cmd / gemini.cmd).
            var pathEnv = Environment.GetEnvironmentVariable("PATH") ?? "";
            foreach (var seg in pathEnv.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    var candidate = Path.Combine(seg.Trim('"'), linkName);
                    if (File.Exists(candidate)) return candidate;
                }
                catch { }
            }
        }
        catch { }
        return null;
    }

    /// <summary>
    /// Ensure a CLI alias for the chat provider is reachable. Stub: no-op when the
    /// resolver already finds the provider on disk; callers tolerate the no-alias
    /// case (they fall back to the resolved entrypoint).
    /// </summary>
    static void EnsureProviderCliAlias(string provider)
    {
        // Real implementation would create a launcher-side .cmd shim that forwards
        // to ResolveProviderCliTarget so `provider` works as a bare command.
        // For now we rely on the user's existing PATH alias (codex / claude / gemini).
        _ = provider;
    }

    static int RunChatProviderOneShot(string providerName, string[] providerArgs)
    {
        try
        {
            if (providerName.Equals("gemini-web", StringComparison.OrdinalIgnoreCase))
            {
                Console.Error.WriteLine("[LAUNCHER] chat: gemini-web does not support headless pipe mode");
                return 1;
            }

            var linkName = GetProviderCliLinkName(providerName);
            if (linkName == null)
            {
                Console.Error.WriteLine($"[LAUNCHER] chat: unsupported provider for one-shot mode: {providerName}");
                return 1;
            }

            var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? Environment.CurrentDirectory) ?? ".";
            var linkPath = Path.Combine(exeDir, linkName);
            if (!File.Exists(linkPath))
            {
                var target = ResolveProviderCliTarget(providerName);
                if (string.IsNullOrWhiteSpace(target) || !File.Exists(target))
                {
                    Console.Error.WriteLine($"[LAUNCHER] chat: provider entrypoint not found for {providerName}");
                    return 1;
                }
                linkPath = target;
            }
            var officialTarget = ResolveProviderCliTarget(providerName);
            if (!string.IsNullOrWhiteSpace(officialTarget) && File.Exists(officialTarget))
                linkPath = officialTarget;

            static bool TryBuildProviderLaunchPlan(string providerName, string resolvedEntrypoint, string[] providerArgs, out string fileName, out string[] launchArgs, out string error)
            {
                fileName = "";
                launchArgs = Array.Empty<string>();
                error = "";

                var provider = providerName.Trim().ToLowerInvariant();
                var entryDir = Path.GetDirectoryName(resolvedEntrypoint) ?? ".";

                switch (provider)
                {
                    case "codex":
                        fileName = resolvedEntrypoint;
                        launchArgs = providerArgs;
                        return true;

                    case "claude":
                    {
                        var directExe = resolvedEntrypoint.EndsWith(".cmd", StringComparison.OrdinalIgnoreCase)
                            ? Path.Combine(entryDir, "node_modules", "@anthropic-ai", "claude-code", "bin", "claude.exe")
                            : resolvedEntrypoint;
                        if (File.Exists(directExe))
                        {
                            fileName = directExe;
                            launchArgs = providerArgs.Where(a => !a.Equals("--skip-trust", StringComparison.OrdinalIgnoreCase)).ToArray();
                            return true;
                        }
                        error = $"claude executable not found for {resolvedEntrypoint}";
                        return false;
                    }

                    case "gemini":
                    {
                        var bundleJs = Path.Combine(entryDir, "node_modules", "@google", "gemini-cli", "bundle", "gemini.js");
                        if (!File.Exists(bundleJs))
                        {
                            error = $"gemini bundle not found for {resolvedEntrypoint}";
                            return false;
                        }

                        var localNode = Path.Combine(entryDir, "node.exe");
                        fileName = File.Exists(localNode) ? localNode : "node";
                        launchArgs = new[] { bundleJs }.Concat(providerArgs).ToArray();
                        return true;
                    }

                    case "gemini-web":
                        error = "gemini-web does not support headless pipe mode";
                        return false;

                    default:
                        error = $"unsupported provider for one-shot mode: {providerName}";
                        return false;
                }
            }

            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = linkPath,
                UseShellExecute = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                RedirectStandardInput = false,
                CreateNoWindow = false,
                WorkingDirectory = Environment.CurrentDirectory,
            };
            if (!TryBuildProviderLaunchPlan(providerName, linkPath, providerArgs, out var launchFile, out var launchArgs, out var launchError))
            {
                Console.Error.WriteLine($"[LAUNCHER] chat: {launchError}");
                return 1;
            }

            psi.FileName = launchFile;
            foreach (var a in launchArgs)
                psi.ArgumentList.Add(a);

            using var proc = System.Diagnostics.Process.Start(psi);
            if (proc == null)
            {
                Console.Error.WriteLine($"[LAUNCHER] chat: failed to start {providerName}");
                return 1;
            }
            proc.WaitForExit();
            return proc.ExitCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCHER] chat one-shot error: {ex.Message}");
            return 1;
        }
    }

    static int DumpChatRoutingProbe()
    {
        try
        {
            var requested = Environment.GetEnvironmentVariable("WKAPPBOT_CHAT_PROVIDER");
            var provider = ChatCliRegistry.Resolve(requested);
            EnsureProviderCliAlias(provider.Name);
            var hotSwapPolicy = ChatHotSwapPolicy.Resolve();
            var session = Environment.GetEnvironmentVariable("WKAPPBOT_CHAT_SESSION") == "1" ? "1" : "0";
            var bufferedPrompt = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WKAPPBOT_CHAT_BUFFERED_PROMPT"))
                ? "0"
                : "1";

            Console.WriteLine($"provider={provider.Name}");
            Console.WriteLine($"requested={requested ?? ""}");
            Console.WriteLine($"chat_session={session}");
            Console.WriteLine($"buffered_prompt={bufferedPrompt}");
            Console.WriteLine($"hot_swap_policy={hotSwapPolicy}");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCHER] chat probe error: {ex.Message}");
            return 1;
        }
    }

    static int DumpChatSlashSwitchProbe()
    {
        try
        {
            var line = Environment.GetEnvironmentVariable("WKAPPBOT_CHAT_PROBE_LINE") ?? "";
            if (TryParseSlashSwitch(line, out var nextCli, out var prompt))
            {
                Console.WriteLine("switch=1");
                Console.WriteLine($"line={line}");
                Console.WriteLine($"next_cli={nextCli}");
                Console.WriteLine($"prompt={prompt ?? ""}");
            }
            else
            {
                Console.WriteLine("switch=0");
                Console.WriteLine($"line={line}");
            }

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCHER] chat slash probe error: {ex.Message}");
            return 1;
        }
    }

    static int RunChatInteractiveSession(string[] args)
    {
        try
        {
            var requested = Environment.GetEnvironmentVariable("WKAPPBOT_CHAT_PROVIDER");
            var provider = ChatCliRegistry.Resolve(requested);
            var hasProviderToken = TryGetChatProviderToken(args, out var providerToken);
            if (hasProviderToken)
                provider = ChatCliRegistry.Resolve(providerToken);

            var headless = IsChatHeadlessRequest(args);
            var providerArgs = GetChatPayloadArgs(args, hasProviderToken);

            if (headless)
            {
                Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_PROVIDER", provider.Name);
                Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_SESSION", "1");
                return RunChatProviderOneShot(provider.Name, providerArgs);
            }

            while (true)
            {
                if (ChatHotSwapStore.TryConsume(out var pendingHotSwap) && pendingHotSwap != null)
                {
                    Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_HOTSWAP_POLICY", pendingHotSwap.CleanupPolicy);
                    Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_HOTSWAP_PREVIOUS_PID", pendingHotSwap.PreviousPid.ToString());
                    Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_HOTSWAP_PREVIOUS_CORE", pendingHotSwap.PreviousCore);
                    Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_HOTSWAP_NEXT_CORE", pendingHotSwap.NextCore);
                    Console.Error.WriteLine($"[LAUNCHER] chat hot-swap metadata loaded -> policy={pendingHotSwap.CleanupPolicy} oldPid={pendingHotSwap.PreviousPid}");
                }

                if (ChatHandoffStore.TryConsume(out var pending) && pending != null)
                {
                    provider = ChatCliRegistry.Resolve(pending.NextCli);
                    Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_BUFFERED_PROMPT", pending.Prompt ?? "");
                    Console.Error.WriteLine($"[LAUNCHER] chat handoff_pending -> provider={provider.Name} reason={pending.Reason}");
                }

                EnsureProviderCliAlias(provider.Name);
                Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_PROVIDER", provider.Name);
                Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_SESSION", "1");

                var code = RunCoreInheritedStdio(args, provider.Name);
                if (code != 126 && code != 99)
                    return code;

                Prof(code == 126
                    ? "chat: limit-triggered handoff restart"
                    : "chat: hot-swap restart via launched core");
                if (code == 99)
                    Console.Error.WriteLine("[LAUNCHER] chat: Core hot-swapped, restarting session with new binary...");
                continue;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCHER] chat router error: {ex.Message}");
            return 1;
        }
    }

    /// <summary>
    /// Spawn Core with full stdio inheritance -- no pipe redirection, no ConPTY
    /// decoupling. Used exclusively by interactive commands like `chat` where the
    /// child process must see the user's actual terminal so its own subprocess
    /// (claude CLI, cmd.exe, bash, ...) can read keystrokes and render output live.
    ///
    /// MCP-style hot-swap loop: if Core exits with code 99 (hot-swap signal), the
    /// Launcher re-resolves the core binary path (Core was just swapped on disk)
    /// and respawns without the user noticing -- same terminal, same session, new
    /// binary. Lets `chat` sessions survive Core updates instead of dying whenever
    /// the user publishes a new build mid-conversation.
    ///
    /// Accepts the LPC/MSYS2 deadlock risk because the command is BY DEFINITION
    /// running inside an interactive console session; the conditions that trigger
    /// the deadlock (non-interactive ConPTY + single-file AppHost) don't apply.
    /// </summary>
    static int RunCoreInheritedStdio(string[] args, string providerName)
    {
        try
        {
            var core = ResolveCoreExe();
            if (!System.IO.File.Exists(core))
            {
                Console.Error.WriteLine($"[LAUNCHER] wkappbot-core.exe not found at: {core}");
                return 1;
            }

            var childHandle = IntPtr.Zero;
            var limitTriggered = false;
            var hotSwapTriggered = false;
            var slashTriggered = false;
            var coreWatcherCts = new CancellationTokenSource();
            var runningCore = core;
            var tail = new StringBuilder();
            var provider = ChatCliRegistry.Resolve(providerName);

            var coreWatchThread = new Thread(() =>
            {
                try
                {
                    while (!coreWatcherCts.IsCancellationRequested)
                    {
                        Thread.Sleep(250);
                        if (childHandle == IntPtr.Zero) continue;
                        var resolved = ResolveCoreExe();
                        if (!string.Equals(Path.GetFullPath(resolved), Path.GetFullPath(runningCore), StringComparison.OrdinalIgnoreCase))
                        {
                            hotSwapTriggered = true;
                            var cleanupPolicy = ChatHotSwapPolicy.Resolve();
                            var sourceCli = Environment.GetEnvironmentVariable("WKAPPBOT_CHAT_PROVIDER") ?? provider.Name;
                            var hotSwap = new ChatHotSwapState(
                                sourceCli,
                                sourceCli,
                                cleanupPolicy,
                                System.Diagnostics.Process.GetCurrentProcess().Id,
                                runningCore,
                                resolved,
                                DateTimeOffset.UtcNow);
                            ChatHotSwapStore.Stage(hotSwap);
                            Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_HOTSWAP_POLICY", cleanupPolicy);
                            Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_HOTSWAP_PREVIOUS_PID", hotSwap.PreviousPid.ToString());
                            Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_HOTSWAP_PREVIOUS_CORE", runningCore);
                            Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_HOTSWAP_NEXT_CORE", resolved);
                            Console.Error.WriteLine($"[LAUNCHER] chat: newer core detected ({Path.GetFileName(resolved)}); restarting session with new binary...");
                            try { WKAppBot.Shared.PseudoConsoleRunner.TryTerminateProcess(childHandle, 0); } catch { }
                            return;
                        }
                    }
                }
                catch { }
            })
            { IsBackground = true, Name = "chat-core-watch" };
            coreWatchThread.Start();

            try
            {
                var exit = WKAppBot.Shared.PseudoConsoleRunner.Run(
                    runningCore,
                    BuildCommandLine(runningCore, args),
                    cwd: Environment.CurrentDirectory,
                    onProcessStarted: h => childHandle = h,
                    onLineReady: line =>
                    {
                        if (slashTriggered || string.IsNullOrWhiteSpace(line))
                            return null;

                        if (!TryParseSlashSwitch(line, out var nextCli, out var prompt))
                            return null;

                        slashTriggered = true;
                        ChatHandoffStore.Stage(new ChatHandoffState(
                            provider.Name,
                            nextCli,
                            "slash-switch",
                            string.IsNullOrWhiteSpace(prompt) ? null : prompt,
                            DateTimeOffset.UtcNow));
                        Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_BUFFERED_PROMPT", prompt ?? "");
                        Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_LIMIT_REASON", "slash-switch");
                        Console.Error.WriteLine($"[LAUNCHER] chat slash switch -> handoff_pending provider={nextCli}");
                        return () =>
                        {
                            try { if (childHandle != IntPtr.Zero) WKAppBot.Shared.PseudoConsoleRunner.TryTerminateProcess(childHandle, 126); } catch { }
                        };
                    },
                    onOutputText: text =>
                    {
                        if (limitTriggered) return;
                        if (string.IsNullOrWhiteSpace(text)) return;
                        tail.Append(text);
                        if (tail.Length > 8192)
                            tail.Remove(0, tail.Length - 4096);

                        var snapshot = tail.ToString();
                        if (!provider.MatchesLimit(snapshot))
                            return;

                        limitTriggered = true;
                        var nextCli = GetNextChatProviderAfterLimit(provider.Name);
                        var prompt = Environment.GetEnvironmentVariable("WKAPPBOT_CHAT_BUFFERED_PROMPT");
                        var reason = snapshot.Trim();
                        ChatHandoffStore.Stage(new ChatHandoffState(
                            provider.Name,
                            nextCli,
                            reason,
                            string.IsNullOrWhiteSpace(prompt) ? null : prompt,
                            DateTimeOffset.UtcNow));
                        Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_LIMIT_REASON", reason);
                        Environment.SetEnvironmentVariable("WKAPPBOT_CHAT_BUFFERED_PROMPT", prompt ?? "");
                        Console.Error.WriteLine($"[LAUNCHER] chat limit detected -> handoff_pending provider={nextCli}");
                        try { WKAppBot.Shared.PseudoConsoleRunner.TryTerminateProcess(childHandle, 126); } catch { }
                    },
                    mirrorToTerminal: true);

                if (limitTriggered || slashTriggered)
                    return 126;

                if (hotSwapTriggered)
                    return 99;

                return exit;
            }
            finally
            {
                try { coreWatcherCts.Cancel(); } catch { }
                try { coreWatchThread.Join(1000); } catch { }
                try { coreWatcherCts.Dispose(); } catch { }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[LAUNCHER] chat inherited-stdio spawn error: {ex.Message}");
            return 1;
        }
    }
}
