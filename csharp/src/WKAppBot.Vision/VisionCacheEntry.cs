using System.Text.Json.Serialization;

namespace WKAppBot.Vision;

/// <summary>
/// A cached Vision API result — "경험치" (experience point) for a UI element.
/// Stored as JSON on disk, keyed by (class_path + description + window_size).
///
/// Design principles:
///   - Relative coordinates (0.0~1.0): valid even if window position changes
///   - Per-control learning: each entry tracks hit_count, success_count for future ML
///   - TTL expiration: default 7 days (app updates may change UI)
///   - SHA256 hash filename: safe for Korean/special characters
/// </summary>
public sealed class VisionCacheEntry
{
    // ── Cache key components ────────────────────────────────
    /// <summary>Win32 class hierarchy path (e.g., "ApplicationFrameWindow/Windows.UI.Core.CoreWindow")</summary>
    [JsonPropertyName("class_path")]
    public string ClassPath { get; set; } = "";

    /// <summary>YAML target.description or element description used for Vision API query</summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = "";

    /// <summary>Window width at time of capture</summary>
    [JsonPropertyName("window_width")]
    public int WindowWidth { get; set; }

    /// <summary>Window height at time of capture</summary>
    [JsonPropertyName("window_height")]
    public int WindowHeight { get; set; }

    // ── Location (relative to window) ───────────────────────
    /// <summary>Relative X position within window (0.0~1.0)</summary>
    [JsonPropertyName("relative_x")]
    public double RelativeX { get; set; }

    /// <summary>Relative Y position within window (0.0~1.0)</summary>
    [JsonPropertyName("relative_y")]
    public double RelativeY { get; set; }

    /// <summary>Element width in pixels at capture time</summary>
    [JsonPropertyName("element_width")]
    public int ElementWidth { get; set; }

    /// <summary>Element height in pixels at capture time</summary>
    [JsonPropertyName("element_height")]
    public int ElementHeight { get; set; }

    // ── Vision API result metadata ──────────────────────────
    /// <summary>Confidence score from Vision API (0.0~1.0)</summary>
    [JsonPropertyName("confidence")]
    public double Confidence { get; set; }

    /// <summary>Human-readable label returned by Vision API</summary>
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    /// <summary>Control type as identified by Vision API (e.g., "Button", "TextBox")</summary>
    [JsonPropertyName("control_type")]
    public string? ControlType { get; set; }

    // ── Learning / stats (per-control experience) ───────────
    /// <summary>When this entry was first created</summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>When this entry was last used (read from cache)</summary>
    [JsonPropertyName("last_used")]
    public DateTime LastUsed { get; set; } = DateTime.UtcNow;

    /// <summary>How many times this cache entry was hit (read)</summary>
    [JsonPropertyName("hit_count")]
    public int HitCount { get; set; }

    /// <summary>
    /// How many times the cached location led to a successful action.
    /// Track success/failure for future ML-based confidence adjustment.
    /// </summary>
    [JsonPropertyName("success_count")]
    public int SuccessCount { get; set; }

    /// <summary>
    /// How many times the cached location led to a failed action.
    /// High fail_count with low success_count → entry should be invalidated.
    /// </summary>
    [JsonPropertyName("fail_count")]
    public int FailCount { get; set; }

    // ── Computed properties ─────────────────────────────────

    /// <summary>
    /// Success rate based on outcomes. Returns 1.0 if no outcomes recorded yet.
    /// Future ML can use this to weight cache confidence.
    /// </summary>
    [JsonIgnore]
    public double SuccessRate =>
        (SuccessCount + FailCount) == 0 ? 1.0 : (double)SuccessCount / (SuccessCount + FailCount);

    /// <summary>
    /// Convert relative coordinates back to absolute screen coordinates
    /// given the current window position and size.
    /// </summary>
    public (int x, int y) ToAbsolute(int windowLeft, int windowTop, int windowWidth, int windowHeight)
    {
        int x = windowLeft + (int)(RelativeX * windowWidth);
        int y = windowTop + (int)(RelativeY * windowHeight);
        return (x, y);
    }

    /// <summary>
    /// Create a cache entry from an absolute location within a window.
    /// </summary>
    public static VisionCacheEntry FromAbsolute(
        string classPath, string description,
        int windowWidth, int windowHeight,
        int absX, int absY, int windowLeft, int windowTop,
        int elemWidth, int elemHeight,
        double confidence, string? label = null, string? controlType = null)
    {
        return new VisionCacheEntry
        {
            ClassPath = classPath,
            Description = description,
            WindowWidth = windowWidth,
            WindowHeight = windowHeight,
            RelativeX = windowWidth > 0 ? (double)(absX - windowLeft) / windowWidth : 0,
            RelativeY = windowHeight > 0 ? (double)(absY - windowTop) / windowHeight : 0,
            ElementWidth = elemWidth,
            ElementHeight = elemHeight,
            Confidence = confidence,
            Label = label,
            ControlType = controlType,
            CreatedAt = DateTime.UtcNow,
            LastUsed = DateTime.UtcNow,
            HitCount = 0,
            SuccessCount = 0,
            FailCount = 0
        };
    }
}
