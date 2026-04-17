using System.Text.RegularExpressions;

namespace WKAppBot.Win32.Window;

/// <summary>
/// Three-level form type identification engine.
///
/// Given a window (with optional title, structural fingerprint, and/or OCR words),
/// identifies which known form type it matches in the Experience DB.
///
/// Matching priority:
///   Level 1: Title pattern "[NNNN] 폼이름" -> exact match (confidence 1.0)
///   Level 2: Structural fingerprint hash -> exact match (confidence 0.95)
///   Level 3: OCR keyword intersection -> ratio-based match (confidence 0.8 × matchRatio)
///
/// Designed for progressive capability:
///   - Win32 API available -> all three levels usable
///   - Image-only (remote desktop) -> Level 2 (if controls detected) + Level 3 (OCR keywords)
///   - OCR-only (no structure) -> Level 3 only
/// </summary>
public static class FormTypeIdentifier
{
    private static readonly Regex FormTitleRegex = new(@"\[(\d+)\]\s+(.+)", RegexOptions.Compiled);

    /// <summary>
    /// Identify a form's type using up to three matching levels.
    /// Returns the best match, or null if no match found.
    /// </summary>
    /// <param name="windowTitle">Window title (e.g., "[1101] 현재가") -- null if image-only</param>
    /// <param name="fingerprintHash">Structural fingerprint hash -- null if not computed</param>
    /// <param name="ocrWords">OCR-extracted words from the form -- null if no OCR</param>
    /// <param name="expDb">Experience DB with known form types</param>
    /// <returns>Best match with confidence, or null</returns>
    public static FormTypeMatch? Identify(
        string? windowTitle,
        string? fingerprintHash,
        IReadOnlyList<string>? ocrWords,
        ExperienceDb expDb)
    {
        // Level 1: Title pattern -- fastest and most reliable
        if (!string.IsNullOrEmpty(windowTitle))
        {
            var match = MatchByTitle(windowTitle, expDb);
            if (match != null) return match;
        }

        // Level 2: Structural fingerprint hash -- structural identity
        if (!string.IsNullOrEmpty(fingerprintHash))
        {
            var match = MatchByFingerprint(fingerprintHash, expDb);
            if (match != null) return match;
        }

        // Level 3: OCR keyword matching -- works with image-only input
        if (ocrWords != null && ocrWords.Count > 0)
        {
            var match = MatchByOcrKeywords(ocrWords, expDb);
            if (match != null) return match;
        }

        return null;
    }

    /// <summary>
    /// Level 1: Match by window title pattern "[NNNN] FormName".
    /// </summary>
    private static FormTypeMatch? MatchByTitle(string windowTitle, ExperienceDb expDb)
    {
        var m = FormTitleRegex.Match(windowTitle);
        if (!m.Success) return null;

        var formId = m.Groups[1].Value;
        var formName = m.Groups[2].Value.Trim();

        var form = expDb.GetForm(formId);
        if (form == null)
        {
            // Not in experience DB yet, but we still know the form type from the title
            return new FormTypeMatch(formId, formName, 1.0, "title");
        }

        return new FormTypeMatch(formId, form.FormName, 1.0, "title");
    }

    /// <summary>
    /// Level 2: Match by structural fingerprint hash (exact match).
    /// </summary>
    private static FormTypeMatch? MatchByFingerprint(string fingerprintHash, ExperienceDb expDb)
    {
        foreach (var (formId, form) in expDb.GetAllForms())
        {
            if (string.IsNullOrEmpty(form.FingerprintHash)) continue;

            if (string.Equals(form.FingerprintHash, fingerprintHash, StringComparison.OrdinalIgnoreCase))
            {
                return new FormTypeMatch(formId, form.FormName, 0.95, "fingerprint");
            }
        }

        return null;
    }

    /// <summary>
    /// Level 3: Match by OCR keyword intersection.
    /// Compares input OCR words against each form type's keyword list.
    /// Returns best match if matchRatio >= 0.5.
    /// </summary>
    private static FormTypeMatch? MatchByOcrKeywords(
        IReadOnlyList<string> ocrWords, ExperienceDb expDb)
    {
        var ocrWordSet = new HashSet<string>(ocrWords, StringComparer.Ordinal);

        FormTypeMatch? bestMatch = null;

        foreach (var (formId, form) in expDb.GetAllForms())
        {
            if (form.OcrKeywords == null || form.OcrKeywords.Count == 0) continue;

            // Count how many of the form's keywords appear in the OCR words
            int matchCount = form.OcrKeywords.Count(kw => ocrWordSet.Contains(kw));
            double matchRatio = (double)matchCount / form.OcrKeywords.Count;

            // Threshold: at least 50% of keywords must match
            if (matchRatio < 0.5) continue;

            double confidence = 0.8 * matchRatio;

            if (bestMatch == null || confidence > bestMatch.Confidence)
            {
                bestMatch = new FormTypeMatch(formId, form.FormName, confidence, "ocr_keywords");
            }
        }

        return bestMatch;
    }
}

/// <summary>
/// Result of form type identification.
/// </summary>
public sealed record FormTypeMatch(
    /// <summary>Form type ID (e.g., "1101")</summary>
    string FormId,
    /// <summary>Form type name (e.g., "현재가")</summary>
    string FormName,
    /// <summary>Match confidence (0.0~1.0)</summary>
    double Confidence,
    /// <summary>Which method produced this match: "title", "fingerprint", or "ocr_keywords"</summary>
    string MatchMethod
);
