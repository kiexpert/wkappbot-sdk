using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace WKAppBot.Win32.Window;

/// <summary>
/// Shared approval keyword policy used by prompt and dialog acceptance paths.
/// The policy is persisted under bin/wkappbot.hq/profiles/approval-policy.json
/// so accepted patterns remain global across sessions.
/// </summary>
public static class ApprovalPolicy
{
    private static readonly Lazy<ApprovalPolicyData> _policy = new(LoadPolicy, LazyThreadSafetyMode.ExecutionAndPublication);
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
    };

    public static string PolicyPath => Path.Combine(
        AppContext.BaseDirectory,
        "wkappbot.hq",
        "profiles",
        "approval-policy.json");

    public static bool AutoApproveAll => _policy.Value.AutoApproveAll;
    public static bool IsAcceptCommand(string text) => MatchesExact(text, _policy.Value.AcceptKeywords);
    public static bool IsPlanApprovalKeyword(string text) => MatchesExact(text, _policy.Value.PlanApprovalKeywords);
    public static bool ContainsApprovalContext(string text) => ContainsAny(text, _policy.Value.ApprovalContextKeywords);
    public static bool ContainsApprovalButtonText(string text) => ContainsAny(text, _policy.Value.ApprovalButtonKeywords);

    private static ApprovalPolicyData LoadPolicy()
    {
        var defaults = ApprovalPolicyData.CreateDefaults();

        try
        {
            if (!File.Exists(PolicyPath))
            {
                EnsureDefaultPolicyFile(defaults);
                return defaults;
            }

            var json = File.ReadAllText(PolicyPath, Encoding.UTF8);
            var loaded = JsonSerializer.Deserialize<ApprovalPolicyData>(json, JsonOptions);
            if (loaded == null)
                return defaults;

            return defaults.Merge(loaded);
        }
        catch
        {
            return defaults;
        }
    }

    private static void EnsureDefaultPolicyFile(ApprovalPolicyData defaults)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(PolicyPath)!);
            var json = JsonSerializer.Serialize(defaults, JsonOptions);
            File.WriteAllText(PolicyPath, json + Environment.NewLine, Encoding.UTF8);
        }
        catch
        {
            // Best effort only. The in-memory defaults still work if the file cannot be written.
        }
    }

    private static bool MatchesExact(string text, IReadOnlyCollection<string> keywords)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        var normalized = Normalize(text);
        foreach (var keyword in keywords)
        {
            if (string.Equals(normalized, Normalize(keyword), StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    private static bool ContainsAny(string text, IReadOnlyCollection<string> keywords)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;
        var normalized = Normalize(text);
        foreach (var keyword in keywords)
        {
            var needle = Normalize(keyword);
            if (needle.Length == 0) continue;
            if (normalized.Contains(needle, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    private static string Normalize(string text) => text.Trim().ToLowerInvariant();

    public sealed class ApprovalPolicyData
    {
        public List<string>? AcceptKeywords { get; set; }
        public List<string>? PlanApprovalKeywords { get; set; }
        public List<string>? ApprovalContextKeywords { get; set; }
        public List<string>? ApprovalButtonKeywords { get; set; }
        public bool AutoApproveAll { get; set; }

        public static ApprovalPolicyData CreateDefaults() => new()
        {
            AutoApproveAll = true,
            AcceptKeywords = new List<string>
            {
                "수락", "승인", "ㅅㄹ", "accept", "approve", "yes", "ㅇㅇ",
                "ok", "okay", "confirm", "continue", "proceed",
            },
            PlanApprovalKeywords = new List<string>
            {
                "승인", "ㅇ", "v", "ok", "okay", "approve", "yes",
                "ㄱㄱ", "고", "넹", "넵", "ㅇㅇ", "확인",
                "좋아", "진행", "시작", "go", "lgtm", "ㅎㅎ",
                "continue", "proceed", "allow", "confirm",
            },
            ApprovalContextKeywords = new List<string>
            {
                "file edit approval",
                "retry without sandbox",
                "approval",
                "permission",
                "approve",
                "accept",
                "allow",
                "yes",
                "수락",
                "허용",
            },
            ApprovalButtonKeywords = new List<string>
            {
                "approve",
                "accept",
                "allow",
                "yes",
                "confirm",
                "continue",
                "proceed",
                "수락",
                "허용",
                "승인",
            },
        };

        public ApprovalPolicyData Merge(ApprovalPolicyData overrides)
        {
            return new ApprovalPolicyData
            {
                AutoApproveAll = AutoApproveAll || overrides.AutoApproveAll,
                AcceptKeywords = MergeList(AcceptKeywords, overrides.AcceptKeywords),
                PlanApprovalKeywords = MergeList(PlanApprovalKeywords, overrides.PlanApprovalKeywords),
                ApprovalContextKeywords = MergeList(ApprovalContextKeywords, overrides.ApprovalContextKeywords),
                ApprovalButtonKeywords = MergeList(ApprovalButtonKeywords, overrides.ApprovalButtonKeywords),
            };
        }

        private static List<string> MergeList(IReadOnlyCollection<string>? defaults, IReadOnlyCollection<string>? overrides)
        {
            var merged = new List<string>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            void AddRange(IReadOnlyCollection<string>? values)
            {
                if (values == null) return;
                foreach (var value in values)
                {
                    if (string.IsNullOrWhiteSpace(value)) continue;
                    var trimmed = value.Trim();
                    if (seen.Add(trimmed))
                        merged.Add(trimmed);
                }
            }

            AddRange(defaults);
            AddRange(overrides);
            return merged;
        }
    }
}
