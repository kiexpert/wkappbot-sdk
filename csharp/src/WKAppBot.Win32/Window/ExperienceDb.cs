using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.Win32.Window;

/// <summary>
/// Experience DB — per-form-type control knowledge learned from scanning + OCR.
/// Stored as JSON files: {exe_dir}/profiles/{profile_name}_exp/form_{form_id}.json
///
/// Key idea: "DB에 등록된 컨트롤이 나올 때까지 트리를 타고 들어간다"
///   - Scanner traverses the Win32 tree downward
///   - When it hits a known control (cid match in experience DB) → STRUCTURE skip (서브트리 스킵)
///   - BUT text collection (WM_GETTEXT) is NEVER skipped — needed for puppet pattern diff
///   - Known control's role, OCR text, etc. are immediately available
///   - Unknown controls → OCR if requested → learn and store
///
/// Per-control detail cache:
///   data/experience/{profile}/form_{formId}/controls/cid_{N}/
///     - info.json: class, role, relativeXY, click strategy stats
///     - latest.png: latest control screenshot (BitBlt, focusless)
///     - text_history.jsonl: text change log ({"ts":..., "v":...} per line, append-only)
///     - snapshots/: screenshots on text change only (disk-efficient)
///
/// Puppet pattern (포펫 패턴):
///   FormExperience.PuppetPattern — layout-preserving text fingerprint
///   Dynamic values → {*}, fixed labels → literal text
///   Built by diffing TextSnapshots across multiple scans
///   Use case: form identification from image-only (remote RDP), state detection, assert generation
///
/// Each form type (e.g., "1101" 현재가) has its own experience file
/// with all discovered controls and their learned properties.
/// </summary>
public sealed class ExperienceDb
{
    private readonly string _expDir;
    private readonly Dictionary<string, FormExperience> _forms = new();

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public ExperienceDb(string expDir)
    {
        _expDir = expDir;
        LoadAll();
    }

    public string ExpDir => _expDir;

    /// <summary>Number of known form types</summary>
    public int FormTypeCount => _forms.Count;

    /// <summary>Total number of known controls across all form types</summary>
    public int TotalControlCount => _forms.Values.Sum(f => f.Controls.Count);

    /// <summary>Last update time across all form types</summary>
    public DateTime? LastUpdated => _forms.Values
        .Select(f => f.UpdatedAt)
        .DefaultIfEmpty(DateTime.MinValue)
        .Max();

    // ── Query ────────────────────────────────────────────

    /// <summary>
    /// Get the experience for a specific form type.
    /// Returns null if this form type hasn't been learned yet.
    /// </summary>
    public FormExperience? GetForm(string formId)
    {
        return _forms.TryGetValue(formId, out var exp) ? exp : null;
    }

    /// <summary>
    /// Get a specific control's experience within a form.
    /// </summary>
    public ControlExperience? GetControl(string formId, int controlId)
    {
        if (!_forms.TryGetValue(formId, out var form)) return null;
        return form.Controls.FirstOrDefault(c => c.ControlId == controlId);
    }

    /// <summary>
    /// Check if a control ID is known in any form type.
    /// Used by scanner to decide "should I stop here or go deeper?"
    /// </summary>
    public bool IsKnownControl(string formId, int controlId)
    {
        if (!_forms.TryGetValue(formId, out var form)) return false;
        return form.Controls.Any(c => c.ControlId == controlId);
    }

    /// <summary>
    /// Get all known form types.
    /// </summary>
    public IReadOnlyDictionary<string, FormExperience> GetAllForms() => _forms;

    // ── Learn (update) ───────────────────────────────────

    /// <summary>
    /// Learn or update a control within a form type.
    /// Merges with existing knowledge (doesn't overwrite OCR text if already known).
    /// </summary>
    public void LearnControl(string formId, string formName, ControlExperience control)
    {
        if (!_forms.TryGetValue(formId, out var form))
        {
            form = new FormExperience
            {
                FormId = formId,
                FormName = formName,
                LearnedAt = DateTime.UtcNow,
            };
            _forms[formId] = form;
        }

        var existing = form.Controls.FirstOrDefault(c => c.ControlId == control.ControlId);
        if (existing != null)
        {
            // Merge: update OCR text only if new has higher confidence
            if (control.OcrText != null &&
                (existing.OcrText == null || control.OcrConfidence > existing.OcrConfidence))
            {
                existing.OcrText = control.OcrText;
                existing.OcrConfidence = control.OcrConfidence;
            }

            // Update WM_GETTEXT (always take latest — it reflects current state)
            if (control.WmGetText != null) existing.WmGetText = control.WmGetText;

            // Always update class and size info
            if (control.ClassName != null) existing.ClassName = control.ClassName;
            if (control.Width > 0) existing.Width = control.Width;
            if (control.Height > 0) existing.Height = control.Height;
            if (control.RelativeX > 0) existing.RelativeX = control.RelativeX;
            if (control.RelativeY > 0) existing.RelativeY = control.RelativeY;

            existing.HitCount++;
        }
        else
        {
            form.Controls.Add(control);
        }

        form.UpdatedAt = DateTime.UtcNow;
        form.ScanCount++;
    }

    /// <summary>
    /// Record a successful action on a control.
    /// </summary>
    public void RecordSuccess(string formId, int controlId)
    {
        var ctrl = GetControl(formId, controlId);
        if (ctrl != null)
        {
            ctrl.SuccessCount++;
            ctrl.HitCount++;
        }
    }

    /// <summary>
    /// Record a failed action on a control.
    /// </summary>
    public void RecordFailure(string formId, int controlId)
    {
        var ctrl = GetControl(formId, controlId);
        if (ctrl != null)
        {
            ctrl.FailCount++;
            ctrl.HitCount++;
        }
    }

    // ── Click Strategy Recording (Phase 7) ─────────────

    /// <summary>
    /// Record a click strategy attempt result for a specific control.
    /// SmartClickButton calls this after each strategy attempt.
    /// TODO: implement in Phase 7
    /// </summary>
    /// <param name="formId">Form type ID</param>
    /// <param name="controlId">Control ID (cid)</param>
    /// <param name="strategy">Strategy name: "bm_click", "wm_lbutton", "send_input"</param>
    /// <param name="success">Whether the strategy successfully clicked the control</param>
    public void RecordClickStrategy(string formId, int controlId, string strategy, bool success)
    {
        var ctrl = GetControl(formId, controlId);
        if (ctrl == null) return;

        ctrl.ClickStrategies ??= new Dictionary<string, ClickStrategyStats>();
        if (!ctrl.ClickStrategies.TryGetValue(strategy, out var stats))
        {
            stats = new ClickStrategyStats();
            ctrl.ClickStrategies[strategy] = stats;
        }

        if (success) stats.Success++;
        else stats.Fail++;
    }

    /// <summary>
    /// Get the best click strategy order for a control based on recorded stats.
    /// Returns strategies sorted by success rate (highest first).
    /// Falls back to default order if no data available.
    /// TODO: wire up in SmartClickButton (Phase 7)
    /// </summary>
    public IReadOnlyList<string> GetBestClickOrder(string formId, int controlId)
    {
        var defaultOrder = new[] { "bm_click", "wm_lbutton", "send_input" };
        var ctrl = GetControl(formId, controlId);
        if (ctrl?.ClickStrategies == null || ctrl.ClickStrategies.Count == 0)
            return defaultOrder;

        return ctrl.ClickStrategies
            .OrderByDescending(kv => kv.Value.SuccessRate)
            .ThenByDescending(kv => kv.Value.Success + kv.Value.Fail) // prefer more data
            .Select(kv => kv.Key)
            .Union(defaultOrder) // append any strategies not yet tried
            .ToList();
    }

    // ── Puppet Pattern (Phase 5) ────────────────────────

    /// <summary>
    /// Add a text snapshot and rebuild puppet pattern if enough data.
    /// Called after each scan with OCR text lines from the form.
    /// TODO: implement PuppetPatternBuilder (Phase 5)
    /// </summary>
    /// <param name="formId">Form type ID</param>
    /// <param name="textLines">OCR text lines from current scan (Y-sorted)</param>
    public void AddTextSnapshot(string formId, List<string> textLines)
    {
        var form = GetForm(formId);
        if (form == null) return;

        form.TextSnapshots ??= new List<List<string>>();
        form.TextSnapshots.Add(textLines);
        form.PuppetScanCount++;

        // FIFO: keep max 5 snapshots
        while (form.TextSnapshots.Count > 5)
            form.TextSnapshots.RemoveAt(0);

        // Auto-build pattern when >= 2 snapshots
        if (form.TextSnapshots.Count >= 2)
        {
            form.PuppetPattern = BuildPuppetPattern(form.TextSnapshots);
        }

        form.UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Build puppet pattern by diffing multiple text snapshots.
    /// Phase 5: LCS line alignment + token-level diff + consecutive wildcard merging.
    ///
    /// Algorithm:
    ///   1. Pairwise LCS align baseline (snap[0]) with each subsequent snapshot
    ///   2. Matched lines → token-level diff (stable tokens literal, changed → {*})
    ///   3. Unmatched lines in baseline → entire line becomes {*}
    ///   4. Consecutive {*} tokens within a line → merged to single {*}
    ///   5. Lines that are ONLY {*} across all comparisons → marked as fully dynamic
    /// </summary>
    private static string BuildPuppetPattern(List<List<string>> snapshots)
    {
        if (snapshots.Count < 2) return "";

        var baseline = snapshots[0];
        // Track per-line stability: patternTokens[lineIdx] = token array or null (dynamic)
        var linePatterns = new string[]?[baseline.Count];

        // Initialize from baseline
        for (int i = 0; i < baseline.Count; i++)
            linePatterns[i] = baseline[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // Compare baseline against each subsequent snapshot using LCS line alignment
        for (int s = 1; s < snapshots.Count; s++)
        {
            var other = snapshots[s];
            var alignment = LcsAlignLines(baseline, other);

            for (int i = 0; i < baseline.Count; i++)
            {
                if (linePatterns[i] == null) continue; // already fully dynamic

                if (!alignment.TryGetValue(i, out int otherIdx))
                {
                    // Baseline line not found in this snapshot → fully dynamic
                    linePatterns[i] = null;
                    continue;
                }

                // Token-level diff between current pattern and the matched other line
                var currentTokens = linePatterns[i]!;
                var otherTokens = other[otherIdx].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (currentTokens.Length != otherTokens.Length)
                {
                    // Token count changed → try partial alignment with LCS on tokens
                    linePatterns[i] = DiffTokensLcs(currentTokens, otherTokens);
                }
                else
                {
                    // Same token count → compare each
                    for (int t = 0; t < currentTokens.Length; t++)
                    {
                        if (currentTokens[t] == "{*}") continue; // already wild
                        if (currentTokens[t] != otherTokens[t])
                            currentTokens[t] = "{*}";
                    }
                }
            }
        }

        // Build final pattern lines with consecutive {*} merging
        var patternLines = new List<string>();
        for (int i = 0; i < baseline.Count; i++)
        {
            if (linePatterns[i] == null)
            {
                patternLines.Add("{*}");
                continue;
            }

            var merged = MergeConsecutiveWildcards(linePatterns[i]!);
            patternLines.Add(string.Join(" ", merged));
        }

        return string.Join("\n", patternLines);
    }

    /// <summary>
    /// LCS-based line alignment between two text snapshots.
    /// Returns a mapping: baselineIndex → otherIndex for matched lines.
    /// Uses exact string match (trimmed) for LCS.
    /// </summary>
    private static Dictionary<int, int> LcsAlignLines(List<string> baseline, List<string> other)
    {
        int m = baseline.Count, n = other.Count;
        // Trim lines for comparison (WM_GETTEXT sometimes has leading spaces)
        var bTrimmed = baseline.Select(l => l.Trim()).ToArray();
        var oTrimmed = other.Select(l => l.Trim()).ToArray();

        // Build LCS table
        var dp = new int[m + 1, n + 1];
        for (int i = 1; i <= m; i++)
            for (int j = 1; j <= n; j++)
                dp[i, j] = bTrimmed[i - 1] == oTrimmed[j - 1]
                    ? dp[i - 1, j - 1] + 1
                    : Math.Max(dp[i - 1, j], dp[i, j - 1]);

        // Backtrack to find matched pairs
        var result = new Dictionary<int, int>();
        int bi = m, oi = n;
        while (bi > 0 && oi > 0)
        {
            if (bTrimmed[bi - 1] == oTrimmed[oi - 1])
            {
                result[bi - 1] = oi - 1;
                bi--; oi--;
            }
            else if (dp[bi - 1, oi] >= dp[bi, oi - 1])
                bi--;
            else
                oi--;
        }

        return result;
    }

    /// <summary>
    /// Token-level LCS diff when token counts differ between snapshots.
    /// Returns merged token array where changed tokens become {*}.
    /// </summary>
    private static string[] DiffTokensLcs(string[] current, string[] other)
    {
        int m = current.Length, n = other.Length;
        var dp = new int[m + 1, n + 1];
        for (int i = 1; i <= m; i++)
            for (int j = 1; j <= n; j++)
            {
                var ct = current[i - 1] == "{*}" ? "{*}" : current[i - 1];
                dp[i, j] = (ct != "{*}" && ct == other[j - 1])
                    ? dp[i - 1, j - 1] + 1
                    : Math.Max(dp[i - 1, j], dp[i, j - 1]);
            }

        // Backtrack: matched tokens stay, unmatched → {*}
        var result = new List<string>();
        int ci = m, oi2 = n;
        var matchedCurrent = new HashSet<int>();
        var matchedOther = new HashSet<int>();

        while (ci > 0 && oi2 > 0)
        {
            var ct = current[ci - 1] == "{*}" ? "{*}" : current[ci - 1];
            if (ct != "{*}" && ct == other[oi2 - 1])
            {
                matchedCurrent.Add(ci - 1);
                matchedOther.Add(oi2 - 1);
                ci--; oi2--;
            }
            else if (dp[ci - 1, oi2] >= dp[ci, oi2 - 1])
                ci--;
            else
                oi2--;
        }

        // Build output: keep matched tokens, replace unmatched with {*}
        for (int i = 0; i < current.Length; i++)
            result.Add(matchedCurrent.Contains(i) ? current[i] : "{*}");

        return result.ToArray();
    }

    /// <summary>
    /// Merge consecutive {*} tokens into a single {*}.
    /// Example: ["{*}", "{*}", "매도", "{*}"] → ["{*}", "매도", "{*}"]
    /// </summary>
    private static string[] MergeConsecutiveWildcards(string[] tokens)
    {
        var result = new List<string>();
        bool lastWasWild = false;
        foreach (var t in tokens)
        {
            if (t == "{*}")
            {
                if (!lastWasWild)
                    result.Add("{*}");
                lastWasWild = true;
            }
            else
            {
                result.Add(t);
                lastWasWild = false;
            }
        }
        return result.ToArray();
    }

    // ── Persistence ──────────────────────────────────────

    /// <summary>
    /// Save a specific form's experience to disk.
    /// </summary>
    public void SaveForm(string formId)
    {
        if (!_forms.TryGetValue(formId, out var form)) return;

        if (!Directory.Exists(_expDir))
            Directory.CreateDirectory(_expDir);

        var path = Path.Combine(_expDir, $"form_{formId}.json");
        var json = JsonSerializer.Serialize(form, JsonOpts);
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// Save all form experiences to disk.
    /// </summary>
    public void SaveAll()
    {
        foreach (var formId in _forms.Keys)
            SaveForm(formId);
    }

    /// <summary>
    /// Load all form experiences from disk.
    /// </summary>
    private void LoadAll()
    {
        if (!Directory.Exists(_expDir)) return;

        foreach (var file in Directory.GetFiles(_expDir, "form_*.json"))
        {
            try
            {
                var json = File.ReadAllText(file);
                var form = JsonSerializer.Deserialize<FormExperience>(json, JsonOpts);
                if (form != null && !string.IsNullOrEmpty(form.FormId))
                    _forms[form.FormId] = form;
            }
            catch { /* skip corrupt files */ }
        }
    }

    /// <summary>
    /// Get summary stats string.
    /// </summary>
    public string GetStatsString()
    {
        if (_forms.Count == 0) return "Experience DB: empty";

        var lastDate = LastUpdated;
        var dateStr = lastDate.HasValue && lastDate.Value > DateTime.MinValue
            ? lastDate.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm")
            : "never";

        return $"Experience DB: {TotalControlCount} controls in {FormTypeCount} form types (updated {dateStr})";
    }
}

// ── Data Models ──────────────────────────────────────────

/// <summary>
/// Learned knowledge about a specific form type (e.g., [1101] 현재가).
/// </summary>
public sealed class FormExperience
{
    [JsonPropertyName("form_id")]
    public string FormId { get; set; } = "";

    [JsonPropertyName("form_name")]
    public string FormName { get; set; } = "";

    [JsonPropertyName("learned_at")]
    public DateTime LearnedAt { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("scan_count")]
    public int ScanCount { get; set; }

    [JsonPropertyName("controls")]
    public List<ControlExperience> Controls { get; set; } = new();

    // ── Structural Fingerprint ──

    /// <summary>
    /// Sorted format string of control tokens: "{NormalizedClass}:{Cid}:{SizeBucket}:{PosBucket}"
    /// Human-readable structural signature of this form type.
    /// </summary>
    [JsonPropertyName("fingerprint")]
    public string? Fingerprint { get; set; }

    /// <summary>
    /// SHA256 hash (first 16 hex chars) of the fingerprint string.
    /// Used for fast equality comparison between form types.
    /// </summary>
    [JsonPropertyName("fingerprint_hash")]
    public string? FingerprintHash { get; set; }

    // ── OCR Keyword Pattern ──

    /// <summary>
    /// Fixed OCR keywords that appear consistently across multiple scan instances.
    /// Dynamic data (prices, stock codes) are filtered out via intersection.
    /// scan_count == 1: all OCR words as candidates.
    /// scan_count >= 2: intersection-refined, high confidence.
    /// </summary>
    [JsonPropertyName("ocr_keywords")]
    public List<string>? OcrKeywords { get; set; }

    // ── Puppet Pattern (포펫 패턴) ──
    //
    // 폼 전체 텍스트의 레이아웃 보존 패턴.
    // 시점에 따라 변하는 동적 값은 {*}로 치환, 고정 레이블은 그대로 유지.
    //
    // 생성 알고리즘:
    //   1. 첫 스캔: OCR 라인별 텍스트를 TextSnapshots[0]에 저장 (baseline)
    //   2. 이후 스캔: 같은 Y좌표 대역의 라인을 토큰 단위로 diff
    //   3. 변한 토큰 → {*}, 안 변한 토큰 → 고정 텍스트
    //   4. scan_count >= 3 이면 PuppetPattern 안정화
    //
    // 활용:
    //   - FormTypeIdentifier Level 4: 패턴 매칭으로 폼 식별
    //   - 상태 감지: 고정부 일치 확인 + 동적부 값 추출
    //   - 변화 감지: 동적부 값 변경 → 상태 전환 이벤트
    //   - Assert 자동생성: 패턴에서 기대값 템플릿 추출

    /// <summary>
    /// Puppet pattern: form text with dynamic values replaced by {*}.
    /// Each line preserves layout position. null until scan_count >= 2.
    /// Example: "조건식: {*}\n매매유형: 자동매수/자동매도\n투자금액 {*} 로스컷 {*}"
    /// </summary>
    [JsonPropertyName("puppet_pattern")]
    public string? PuppetPattern { get; set; }

    /// <summary>
    /// Raw text snapshots from each scan (최대 5개 보관, FIFO).
    /// 라인별 텍스트 리스트. 누적 비교하여 PuppetPattern 자동 생성.
    /// </summary>
    [JsonPropertyName("text_snapshots")]
    public List<List<string>>? TextSnapshots { get; set; }

    /// <summary>
    /// Number of scans used to build the puppet pattern.
    /// Pattern is considered stable when >= 3.
    /// </summary>
    [JsonPropertyName("puppet_scan_count")]
    public int PuppetScanCount { get; set; }
}

/// <summary>
/// Learned knowledge about a specific control within a form.
/// </summary>
public sealed class ControlExperience
{
    [JsonPropertyName("cid")]
    public int ControlId { get; set; }

    [JsonPropertyName("class")]
    public string? ClassName { get; set; }

    [JsonPropertyName("role")]
    public string? Role { get; set; }

    [JsonPropertyName("ocr_text")]
    public string? OcrText { get; set; }

    [JsonPropertyName("ocr_confidence")]
    public double OcrConfidence { get; set; }

    /// <summary>
    /// Raw WM_GETTEXT value from Win32 API (may differ from OCR text).
    /// Useful for Edit/ComboBox controls where OCR is unreliable.
    /// </summary>
    [JsonPropertyName("wm_gettext")]
    public string? WmGetText { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("relative_x")]
    public double RelativeX { get; set; }

    [JsonPropertyName("relative_y")]
    public double RelativeY { get; set; }

    [JsonPropertyName("hit_count")]
    public int HitCount { get; set; }

    [JsonPropertyName("success_count")]
    public int SuccessCount { get; set; }

    [JsonPropertyName("fail_count")]
    public int FailCount { get; set; }

    [JsonIgnore]
    public double SuccessRate =>
        (SuccessCount + FailCount) == 0 ? 1.0 : (double)SuccessCount / (SuccessCount + FailCount);

    // ── Click Strategy Stats ──
    // SmartClickButton이 각 전략의 성공/실패를 여기에 누적.
    // 다음 클릭 시 성공률 높은 전략부터 시도하도록 순서 최적화.

    /// <summary>
    /// Per-strategy success/fail counts. Key: "bm_click", "wm_lbutton", "send_input".
    /// null until first click attempt recorded.
    /// </summary>
    [JsonPropertyName("click_strategies")]
    public Dictionary<string, ClickStrategyStats>? ClickStrategies { get; set; }
}

/// <summary>
/// Success/fail stats for a specific click strategy on a specific control.
/// </summary>
public sealed class ClickStrategyStats
{
    [JsonPropertyName("success")]
    public int Success { get; set; }

    [JsonPropertyName("fail")]
    public int Fail { get; set; }

    [JsonIgnore]
    public double SuccessRate => (Success + Fail) == 0 ? 0.5 : (double)Success / (Success + Fail);
}
