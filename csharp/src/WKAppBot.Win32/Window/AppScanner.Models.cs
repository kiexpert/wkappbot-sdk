using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

// -- Data models ----------------------------------------─

/// <summary>
/// Complete scan result for a target window.
/// </summary>
public sealed class AppScanResult
{
    public string WindowTitle { get; init; } = "";
    public string WindowClass { get; init; } = "";
    public string ProcessName { get; init; } = "";
    public int ProcessId { get; init; }
    public IntPtr Handle { get; init; }
    public RECT Rect { get; init; }

    /// <summary>Top-level child zones</summary>
    public List<ZoneScanEntry> Zones { get; } = new();

    /// <summary>MDIClient handle (if found)</summary>
    public IntPtr MdiHandle { get; set; }

    /// <summary>MDI child forms</summary>
    public List<FormScanEntry> Forms { get; } = new();
}

/// <summary>
/// A classified top-level child zone.
/// </summary>
public sealed class ZoneScanEntry
{
    public IntPtr Handle { get; init; }
    public string ClassName { get; init; } = "";
    public string? Title { get; init; }
    public int ControlId { get; init; }
    public RECT Rect { get; init; }
    public bool IsVisible { get; init; }
    public ZoneInfo Zone { get; init; } = new(ZoneType.Unknown, "");

    /// <summary>Notable children found inside bars (Edit inputs, webviews, etc.)</summary>
    public List<ZoneScanEntry> NotableChildren { get; } = new();
}

/// <summary>
/// A scanned MDI child form.
/// </summary>
public sealed class FormScanEntry
{
    public IntPtr Handle { get; init; }
    public string ClassName { get; init; } = "";
    public string Title { get; init; } = "";
    public int ControlId { get; init; }
    public RECT Rect { get; init; }
    public bool IsVisible { get; init; }

    /// <summary>Form ID extracted from title (e.g., "1101", "0606")</summary>
    public string? FormId { get; init; }

    /// <summary>Form name extracted from title (e.g., "현재가", "키움종합차트")</summary>
    public string? FormName { get; init; }

    /// <summary>Stock code found in the form (e.g., "005930")</summary>
    public string? StockCode { get; init; }

    /// <summary>Number of direct child windows (complexity hint)</summary>
    public int DirectChildCount { get; init; }
}

/// <summary>
/// OCR word info -- lightweight struct for passing OCR results from Vision layer.
/// Avoids direct dependency on WKAppBot.Vision in Win32 project.
/// </summary>
public sealed class OcrWordInfo
{
    public string Text { get; init; } = "";
    public int X { get; init; }
    public int Y { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
}

/// <summary>
/// Result summary from OCR learning pass.
/// </summary>
public sealed class OcrLearnResult
{
    public int FormsScanned { get; set; }
    public int ControlsLearned { get; set; }
    public int DetailScreenshots { get; set; }
    public int DetailTextChanges { get; set; }
    public List<string> Errors { get; } = new();

    public override string ToString()
    {
        var errStr = Errors.Count > 0 ? $", {Errors.Count} errors" : "";
        var detailStr = DetailScreenshots > 0
            ? $", {DetailScreenshots} screenshots + {DetailTextChanges} text changes"
            : "";
        return $"OCR Learning: {ControlsLearned} controls learned from {FormsScanned} form types{detailStr}{errStr}";
    }
}
