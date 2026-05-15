// Stage 2 / Stage 3 placement validation telemetry helpers.
// Splits the JSON-writing scaffolding out of MyCdpContext.Stage23.cs so the
// main stage logic stays under the 400-line soft cap and reads as a flow,
// not a stack of writer-boilerplate.

using System.Text;
using System.Text.Json;

namespace WKAppBot.Launcher;

partial class Program
{
    /// <summary>
    /// Append a stage-2 placement_validation record to
    /// wkappbot.hq/runtime/cdp-state.jsonl. Mirrors the schema used by
    /// AppendPlacementValidation in MyCdpContext.cs so cdp-mon / suggest
    /// triage can join the two streams on "kind": "placement_validation".
    /// </summary>
    static void AppendStage2Record(
        string trigger,
        long elapsedMs,
        RECT expected,
        RECT final,
        bool ok,
        int attempts,
        string cmd)
    {
        try
        {
            var jsonlPath = ResolveStageJsonlPath();
            using var ms = new MemoryStream();
            using (var writer = new Utf8JsonWriter(ms))
            {
                writer.WriteStartObject();
                writer.WriteString("ts", DateTimeOffset.UtcNow);
                writer.WriteString("kind", "placement_validation");
                writer.WriteNumber("stage", 2);
                writer.WriteString("trigger", trigger);
                writer.WriteNumber("elapsed_ms", elapsedMs);
                writer.WriteNumber("attempts", attempts);
                writer.WriteBoolean("success", ok);
                if (!string.IsNullOrEmpty(cmd)) writer.WriteString("cmd", cmd);
                WriteRectArray(writer, "expected", expected);
                WriteRectArray(writer, "final", final);
                writer.WriteEndObject();
            }
            WKAppBot.Shared.ToolOutputStore.AppBotAppendFile(jsonlPath, Encoding.UTF8.GetString(ms.ToArray()));
        }
        catch
        {
            // best-effort telemetry only
        }
    }

    /// <summary>
    /// Append a stage-3 placement_validation record to the shared JSONL.
    /// Captures DPI-aware decision data so an operator can see what the
    /// caller-DPI vs. chrome-DPI was at correction time.
    /// </summary>
    static void AppendStage3Record(
        uint callerDpi,
        uint chromeDpi,
        bool dpiMatch,
        bool corrected,
        bool success,
        RECT targetRect,
        RECT actualRect,
        string note,
        string cmd)
    {
        try
        {
            var jsonlPath = ResolveStageJsonlPath();
            double callerScale = callerDpi / 96.0;
            double chromeScale = chromeDpi / 96.0;

            using var ms = new MemoryStream();
            using (var writer = new Utf8JsonWriter(ms))
            {
                writer.WriteStartObject();
                writer.WriteString("ts", DateTimeOffset.UtcNow);
                writer.WriteString("kind", "placement_validation");
                writer.WriteNumber("stage", 3);
                writer.WriteNumber("caller_dpi", callerDpi);
                writer.WriteNumber("chrome_dpi", chromeDpi);
                writer.WriteString("dpi_scale", $"caller={callerScale * 100:F0}% chrome={chromeScale * 100:F0}%");
                writer.WriteBoolean("dpi_match", dpiMatch);
                writer.WriteBoolean("corrected", corrected);
                writer.WriteBoolean("success", success);
                writer.WriteString("note", note);
                if (!string.IsNullOrEmpty(cmd)) writer.WriteString("cmd", cmd);
                WriteRectArray(writer, "target", targetRect);
                WriteRectArray(writer, "actual", actualRect);
                writer.WriteEndObject();
            }
            WKAppBot.Shared.ToolOutputStore.AppBotAppendFile(jsonlPath, Encoding.UTF8.GetString(ms.ToArray()));
        }
        catch
        {
            // best-effort telemetry only
        }
    }

    /// <summary>
    /// Returns wkappbot.hq/runtime/cdp-state.jsonl next to the launcher
    /// binary, creating the runtime dir if missing. Shared by stage 2 and
    /// stage 3 records so they land in the same audit stream as the stage 1
    /// placement_validation records appended from MyCdpContext.cs.
    /// </summary>
    static string ResolveStageJsonlPath()
    {
        var exeDir = Path.GetDirectoryName(Environment.ProcessPath ?? "") ?? ".";
        var runtimeDir = Path.Combine(exeDir, "wkappbot.hq", "runtime");
        Directory.CreateDirectory(runtimeDir);
        return Path.Combine(runtimeDir, "cdp-state.jsonl");
    }

    /// <summary>
    /// Write a RECT as a 6-element JSON array: [L, T, R, B, W, H]. Matches
    /// the array shape used by AppendPlacementValidation in MyCdpContext.cs
    /// so all placement records have a consistent layout.
    /// </summary>
    static void WriteRectArray(Utf8JsonWriter writer, string propertyName, RECT r)
    {
        writer.WritePropertyName(propertyName);
        writer.WriteStartArray();
        writer.WriteNumberValue(r.Left);
        writer.WriteNumberValue(r.Top);
        writer.WriteNumberValue(r.Right);
        writer.WriteNumberValue(r.Bottom);
        writer.WriteNumberValue(r.Width);
        writer.WriteNumberValue(r.Height);
        writer.WriteEndArray();
    }
}
