using System.Drawing;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WKAppBot.Win32.Input;

namespace WKAppBot.Vision;

/// <summary>
/// Claude Vision API integration for element detection.
/// 2nd tier in Chain of Responsibility (after Accessibility, before Coordinate).
///
/// Flow:
///   1. Capture window screenshot (focusless -- PrintWindow/BitBlt)
///   2. Encode as base64 PNG
///   3. Send to Claude API with element description
///   4. Parse JSON response -> ElementLocation
///   5. Cache result in VisionCache for future use
/// </summary>
public sealed class VisionAnalyzer : IDisposable
{
    private readonly string _apiKey;
    private readonly string _model;
    private readonly HttpClient _http;
    private readonly double _confidenceThreshold;
    private const string ApiUrl = "https://api.anthropic.com/v1/messages";

    public VisionAnalyzer(
        string? apiKey = null,
        string model = "claude-sonnet-4-20250514",
        double confidenceThreshold = 0.7)
    {
        _apiKey = apiKey ?? Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY")
            ?? throw new InvalidOperationException(
                "ANTHROPIC_API_KEY not set. Provide via constructor or environment variable.");
        _model = model;
        _confidenceThreshold = confidenceThreshold;

        _http = new HttpClient();
        _http.DefaultRequestHeaders.Add("x-api-key", _apiKey);
        _http.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
        _http.Timeout = TimeSpan.FromSeconds(30);
    }

    /// <summary>
    /// Analyze a screenshot to find an element by description.
    /// Returns the location of the found element, or null if not found/low confidence.
    /// </summary>
    public async Task<ElementLocation?> FindElement(
        Bitmap screenshot,
        string description,
        CancellationToken ct = default)
    {
        var base64 = ScreenCapture.ToPngBase64(screenshot);

        var prompt = $@"You are analyzing a Windows application screenshot to find a specific UI element.

TASK: Find the UI element described as: ""{description}""

RESPOND with ONLY a JSON object (no markdown, no explanation):
{{""x"": <center_x_pixels>, ""y"": <center_y_pixels>, ""width"": <element_width>, ""height"": <element_height>, ""confidence"": <0.0_to_1.0>, ""label"": ""<what_the_element_is>"", ""control_type"": ""<Button|TextBox|Label|etc>""}}

If the element is NOT found, respond with:
{{""found"": false, ""confidence"": 0.0}}

RULES:
- x, y are the CENTER coordinates of the element in pixels from the image top-left
- confidence should reflect how certain you are this is the correct element
- Be precise -- a few pixels off can miss a small button";

        var requestBody = new
        {
            model = _model,
            max_tokens = 256,
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = new object[]
                    {
                        new
                        {
                            type = "image",
                            source = new
                            {
                                type = "base64",
                                media_type = "image/png",
                                data = base64
                            }
                        },
                        new
                        {
                            type = "text",
                            text = prompt
                        }
                    }
                }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync(ApiUrl, content, ct);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync(ct);
        return ParseResponse(responseJson);
    }

    /// <summary>
    /// Parse the Claude API response into an ElementLocation.
    /// </summary>
    private ElementLocation? ParseResponse(string apiResponse)
    {
        try
        {
            using var doc = JsonDocument.Parse(apiResponse);
            var root = doc.RootElement;

            // Navigate to content[0].text
            if (!root.TryGetProperty("content", out var contentArr)) return null;
            if (contentArr.GetArrayLength() == 0) return null;

            var textBlock = contentArr[0];
            if (!textBlock.TryGetProperty("text", out var textElem)) return null;

            var text = textElem.GetString();
            if (string.IsNullOrWhiteSpace(text)) return null;

            // Parse the inner JSON from Claude's response
            // Strip any markdown code fences if present
            text = text.Trim();
            if (text.StartsWith("```"))
            {
                var lines = text.Split('\n');
                text = string.Join('\n', lines.Skip(1).TakeWhile(l => !l.StartsWith("```")));
            }

            using var innerDoc = JsonDocument.Parse(text);
            var inner = innerDoc.RootElement;

            // Check if element was not found
            if (inner.TryGetProperty("found", out var foundProp) && !foundProp.GetBoolean())
                return null;

            var location = new ElementLocation
            {
                X = inner.TryGetProperty("x", out var xp) ? xp.GetInt32() : 0,
                Y = inner.TryGetProperty("y", out var yp) ? yp.GetInt32() : 0,
                Width = inner.TryGetProperty("width", out var wp) ? wp.GetInt32() : 0,
                Height = inner.TryGetProperty("height", out var hp) ? hp.GetInt32() : 0,
                Confidence = inner.TryGetProperty("confidence", out var cp) ? cp.GetDouble() : 0,
                Label = inner.TryGetProperty("label", out var lp) ? lp.GetString() : null,
                ControlType = inner.TryGetProperty("control_type", out var ctp) ? ctp.GetString() : null,
            };

            // Filter by confidence threshold
            if (location.Confidence < _confidenceThreshold)
                return null;

            return location;
        }
        catch
        {
            return null;
        }
    }

    public void Dispose()
    {
        _http.Dispose();
    }
}

/// <summary>
/// Location of a UI element found by Vision.
/// </summary>
public sealed class ElementLocation
{
    /// <summary>Center X in screenshot pixel coordinates</summary>
    public int X { get; set; }

    /// <summary>Center Y in screenshot pixel coordinates</summary>
    public int Y { get; set; }

    /// <summary>Element width in pixels</summary>
    public int Width { get; set; }

    /// <summary>Element height in pixels</summary>
    public int Height { get; set; }

    /// <summary>Confidence score (0.0~1.0)</summary>
    public double Confidence { get; set; }

    /// <summary>Human-readable label from Vision API</summary>
    public string? Label { get; set; }

    /// <summary>Control type identified by Vision API</summary>
    public string? ControlType { get; set; }

    public int CenterX => X;
    public int CenterY => Y;
}
