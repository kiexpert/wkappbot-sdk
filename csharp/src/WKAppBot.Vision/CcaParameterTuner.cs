using System.Text.Json;
using System.Text.Json.Serialization;

namespace WKAppBot.Vision;

/// <summary>
/// Auto-tuning CCA parameters per process using Gemini Vision feedback.
/// When Gemini says "this is an icon, not text" → misclassification signal → adjust thresholds.
///
/// Parameters tuned:
///   1. TextMinDensity   — minimum foreground pixel density for text (default 0.10)
///   2. TextMaxDensity   — maximum density for text (above = filled icon) (default 0.85)
///   3. TextMinAR        — minimum aspect ratio for text (default 0.20)
///   4. TextMaxAR        — maximum aspect ratio for text (default 5.0)
///   5. TextMaxSize      — maximum width/height for single glyph (default 80)
///   6. IconMinSize      — minimum width/height for icon (default 8)
///   7. NoiseMaxPixels   — max pixel count for noise (default 4)
///
/// Algorithm: Exponential Moving Average (EMA) with boundary learning.
///   - Each misclassification nudges the boundary toward the misclassified sample
///   - Correct classifications reinforce current boundaries (smaller nudge)
///   - α = 0.15 (learning rate) — stabilizes after ~20 samples
///
/// Storage: experience/{processName}/cca_params.json
/// </summary>
public sealed class CcaParameterTuner
{
    private const double LearningRate = 0.15;       // EMA α for misclassification
    private const double ReinforcementRate = 0.02;   // EMA α for correct classification
    private const int MinSamplesForStable = 15;      // min samples before parameters are "confident"

    public CcaParams Params { get; private set; } = new();

    private readonly string? _savePath;

    /// <summary>
    /// Create tuner for a specific a11y node in the experience DB.
    /// </summary>
    /// <param name="controlDir">
    /// Experience DB control directory, e.g.:
    ///   {expDir}/form_{formId}/controls/cid_{N}/
    ///   {expDir}/form_{formId}/tree/{path}/
    /// Pass null for in-memory only (no persistence).
    /// </param>
    public CcaParameterTuner(string? controlDir = null)
    {
        if (!string.IsNullOrEmpty(controlDir))
        {
            Directory.CreateDirectory(controlDir);
            _savePath = Path.Combine(controlDir, "cca_params.json");
            Load();
        }
    }

    /// <summary>
    /// Feed a classification result back to the tuner.
    /// </summary>
    /// <param name="predicted">What CCA predicted (Text/Icon/etc.)</param>
    /// <param name="actual">What Gemini said it actually is</param>
    /// <param name="density">Component's pixel density</param>
    /// <param name="aspectRatio">Component's aspect ratio (w/h)</param>
    /// <param name="width">Component width in pixels</param>
    /// <param name="height">Component height in pixels</param>
    public void Feedback(
        ConnectedComponentAnalyzer.RegionType predicted,
        ConnectedComponentAnalyzer.RegionType actual,
        double density, double aspectRatio, int width, int height)
    {
        Params.TotalSamples++;
        bool correct = predicted == actual;

        if (correct)
        {
            Params.CorrectCount++;
            // Reinforce: gently tighten boundaries toward this correct sample
            if (actual == ConnectedComponentAnalyzer.RegionType.Text)
            {
                Params.TextMinDensity = Nudge(Params.TextMinDensity, density, ReinforcementRate, lower: true);
                Params.TextMaxDensity = Nudge(Params.TextMaxDensity, density, ReinforcementRate, lower: false);
                Params.TextMinAR = Nudge(Params.TextMinAR, aspectRatio, ReinforcementRate, lower: true);
                Params.TextMaxAR = Nudge(Params.TextMaxAR, aspectRatio, ReinforcementRate, lower: false);
            }
        }
        else
        {
            Params.MisclassifiedCount++;
            // Misclassified: stronger nudge toward the sample
            if (predicted == ConnectedComponentAnalyzer.RegionType.Text
                && actual == ConnectedComponentAnalyzer.RegionType.Icon)
            {
                // CCA said text but it's icon → tighten text boundaries to exclude this sample
                // If density is low → raise min density; if density is high → lower max density
                if (density < Params.TextMinDensity + 0.3)
                    Params.TextMinDensity = Nudge(Params.TextMinDensity, density + 0.05, LearningRate, lower: false);
                else
                    Params.TextMaxDensity = Nudge(Params.TextMaxDensity, density - 0.05, LearningRate, lower: true);

                if (aspectRatio < 1.0)
                    Params.TextMinAR = Nudge(Params.TextMinAR, aspectRatio + 0.1, LearningRate, lower: false);
                else
                    Params.TextMaxAR = Nudge(Params.TextMaxAR, aspectRatio - 0.1, LearningRate, lower: true);
            }
            else if (predicted == ConnectedComponentAnalyzer.RegionType.Icon
                     && actual == ConnectedComponentAnalyzer.RegionType.Text)
            {
                // CCA said icon but it's text → widen text boundaries to include this sample
                if (density < Params.TextMinDensity)
                    Params.TextMinDensity = Nudge(Params.TextMinDensity, density - 0.02, LearningRate, lower: true);
                if (aspectRatio < Params.TextMinAR)
                    Params.TextMinAR = Nudge(Params.TextMinAR, aspectRatio - 0.1, LearningRate, lower: true);
                if (aspectRatio > Params.TextMaxAR)
                    Params.TextMaxAR = Nudge(Params.TextMaxAR, aspectRatio + 0.1, LearningRate, lower: false);
                if (width > Params.TextMaxSize || height > Params.TextMaxSize)
                    Params.TextMaxSize = Math.Max(Params.TextMaxSize, Math.Max(width, height) + 5);
            }
        }

        Params.LastUpdated = DateTime.UtcNow;
        Params.Accuracy = Params.TotalSamples > 0
            ? (double)Params.CorrectCount / Params.TotalSamples
            : 0;

        Save();
    }

    /// <summary>Is the parameter set stable enough to trust?</summary>
    public bool IsStable => Params.TotalSamples >= MinSamplesForStable && Params.Accuracy > 0.75;

    private static double Nudge(double current, double target, double alpha, bool lower)
    {
        double delta = target - current;
        if (lower && delta > 0) return current;
        if (!lower && delta < 0) return current;
        return Math.Max(0.01, Math.Min(0.99, current + alpha * delta));
    }

    private void Load()
    {
        if (_savePath == null || !File.Exists(_savePath)) return;
        try
        {
            var json = File.ReadAllText(_savePath);
            var loaded = JsonSerializer.Deserialize<CcaParams>(json);
            if (loaded != null) Params = loaded;
        }
        catch { /* start fresh */ }
    }

    private void Save()
    {
        if (_savePath == null) return;
        try
        {
            var json = JsonSerializer.Serialize(Params, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_savePath, json);
        }
        catch { /* best effort */ }
    }

    private static string SanitizeName(string s)
    {
        var invalid = Path.GetInvalidFileNameChars();
        return new string(s.Select(c => invalid.Contains(c) ? '_' : c).ToArray());
    }
}

/// <summary>Serializable CCA parameters with tuning metadata.</summary>
public sealed class CcaParams
{
    // ── Tunable thresholds ──
    public double TextMinDensity { get; set; } = 0.10;
    public double TextMaxDensity { get; set; } = 0.85;
    public double TextMinAR { get; set; } = 0.20;
    public double TextMaxAR { get; set; } = 5.0;
    public int TextMaxSize { get; set; } = 80;
    public int IconMinSize { get; set; } = 8;
    public int NoiseMaxPixels { get; set; } = 4;

    // ── Tuning metadata ──
    public int TotalSamples { get; set; }
    public int CorrectCount { get; set; }
    public int MisclassifiedCount { get; set; }
    public double Accuracy { get; set; }
    public DateTime LastUpdated { get; set; }

    [JsonIgnore]
    public bool IsDefault => TotalSamples == 0;
}
