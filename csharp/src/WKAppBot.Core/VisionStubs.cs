// Stubs for Vision types removed in open-source build
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;

namespace WKAppBot.Vision
{
    public class VisionCache : IDisposable
    {
        public VisionCache(string cachePath, int ttlDays) { }

        public VisionCacheEntry? Get(string classPath, string description, int winW, int winH) => null;

        public void Put(string classPath, string description, int winW, int winH, VisionCacheEntry entry) { }

        public (int totalEntries, int misses, double avgRate) GetStats() => (0, 0, 0.0);

        public void Dispose() { }
    }

    public class VisionCacheEntry
    {
        public float Confidence { get; set; }
        public int HitCount { get; set; }

        public static VisionCacheEntry FromAbsolute(
            string classPath, string description, int winW, int winH,
            int absX, int absY, int rectLeft, int rectTop,
            int w, int h, float confidence, string text, string source)
        {
            return new VisionCacheEntry { Confidence = confidence, HitCount = 0 };
        }

        public (int absX, int absY) ToAbsolute(int rectLeft, int rectTop, int winW, int winH) => (0, 0);
    }

    public class VisionAnalyzer : IDisposable
    {
        public VisionAnalyzer(string model = "claude-3-5-sonnet", double confidenceThreshold = 0.5) { }

        public async Task<VisionLocation?> FindElement(Bitmap bmp, string description) => null;

        public void Dispose() { }
    }

    public class VisionLocation
    {
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Confidence { get; set; }
        public string? Label { get; set; }
        public string? ControlType { get; set; }
    }

    public class SimpleOcrAnalyzer : IDisposable
    {
        public SimpleOcrAnalyzer(string primaryLanguage = "en-US", double confidenceThreshold = 0.5) { }

        public static IEnumerable<string> GetAvailableLanguages() => new[] { "en-US" };

        public static string ComputeFormHash(Bitmap bmp) => "";

        public async Task<List<OcrSegment>> SegmentAll(Bitmap bmp) => new();

        public async Task<OcrMatchResult?> FindElement(Bitmap bmp, string description) => null;

        public async Task<OcrRecognitionResult> RecognizeAll(Bitmap bmp) => new();

        public void Dispose() { }
    }

    public class OcrRecognitionResult : List<OcrSegment>
    {
        public string FullText { get; set; } = "";
        public List<OcrWord> Words { get; set; } = new();
        public double UpscaleFactor { get; set; } = 1.0;
    }

    public class OcrSegment
    {
        public string Text { get; set; } = "";
        public double RelX { get; set; }
        public double RelY { get; set; }
        public double RelW { get; set; }
        public double RelH { get; set; }
        public float Confidence { get; set; }
        public string? Source { get; set; }
        public string? ControlType { get; set; }
    }

    public class OcrSegmentCache : IDisposable
    {
        public OcrSegmentCache(string cachePath) { }

        public OcrSegmentCacheEntry? LoadIfFresh(string classPath, string formHash, int winW, int winH) => null;

        public void Save(string classPath, int winW, int winH, OcrSegmentCacheEntry entry) { }

        public void LearnSegment(string classPath, string formHash, int winW, int winH, OcrSegment segment) { }

        public string? SaveBlob(Bitmap crop, string label) => null;

        public static OcrSegment? BestMatch(List<OcrSegment> segments, string description) => null;

        public static List<OcrSegment> ParseA11yJson(string json) => new();

        public void Dispose() { }
    }

    public class OcrSegmentCacheEntry
    {
        public string FormHash { get; set; } = "";
        public DateTime BuildAt { get; set; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public List<OcrSegment> Segments { get; set; } = new();
    }

    public class OcrMatchResult
    {
        public string MatchedText { get; set; } = "";
        public int X { get; set; }
        public int Y { get; set; }
        public float Confidence { get; set; }
        public string MatchType { get; set; } = "none";
    }

    public class OcrWord
    {
        public string Text { get; set; } = "";
        public float Confidence { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class OcrFullResult : List<OcrWord>
    {
        public string FullText { get; set; } = "";
        public List<OcrWord> Words { get; set; } = new();
    }

    public class OcrCorrectionDb
    {
        public OcrCorrectionDb(string path) { }
    }

    public class ConnectedComponentAnalyzer : IDisposable
    {
        public class Region
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public List<(int x, int y)> Pixels { get; set; } = new();
            public RegionType RegionType { get; set; }
        }

        public static RegionType RegionType => RegionType.Unknown;

        public List<Region> DetectTable(Bitmap bmp) => new();

        public void Dispose() { }
    }

    public class ChartAnalyzer : IDisposable
    {
        public ChartAnalyzer(Bitmap? bmp = null) { }
        public List<CandleData> Analyze() => new();
        public static string GenerateDebugOverlay(string text) => "";
        public void Dispose() { }
    }

    public class TooltipCalibrator
    {
        public TooltipCalibrator(string? configPath = null) { }
        public int TargetProcessId { get; set; }
        public IntPtr ChartWindowHandle { get; set; }
        public int WindowLeft { get; set; }
        public int WindowTop { get; set; }
        public int TooltipWaitMs { get; set; }
        public int MaxProbeCandles { get; set; }
        public bool SaveDebugScreenshots { get; set; }
        public string DebugOutputDir { get; set; } = "";
        public void CalibrateFromTooltips() { }
    }

    public class CandleData
    {
        public float Open { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public float Close { get; set; }
        public long Time { get; set; }
        public float Volume { get; set; }
    }

    public class GrapMatcher
    {
        public bool Matches(string text) => false;
    }

    public class DynamicA11yAnalyzer
    {
        public DynamicA11yAnalyzer(string? configPath = null) { }
        public static string GenerateDynId(string text) => "";
    }

    public class CcaUiaFusedMatcher
    {
        public bool Matches(object node, string grap) => false;
    }

    public enum RegionType
    {
        Unknown,
        Text,
        Button,
        Table
    }
}

namespace WKAppBot.Android
{
    public class AdbExperienceDb
    {
        public AdbExperienceDb(string path) { }
        public string GetA11yDir() => "";
        public string GetOsDir() => "";
        public List<string> GetKnowhowFiles() => new();
        public void LogAction(string action) { }
        public void SaveTreeSnapshot(AndroidA11yTree tree) { }
    }

    public class AdbClient : IDisposable
    {
        public async Task<AdbResult> Shell(string command) => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> ShellRaw(string command) => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> Home() => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> Back() => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> RecentApps() => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> Tap(int x, int y) => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> LongPress(int x, int y) => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> Swipe(int x1, int y1, int x2, int y2) => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> InputText(string text) => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> KeyEvent(string key) => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> ForceStop(string package) => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> Screencap() => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> ClipboardPaste(string text) => new AdbResult { IsSuccess = true };
        public async Task<AdbResult> BroadcastText(string text) => new AdbResult { IsSuccess = true };
        public async Task<string> GetCurrentActivity() => "";
        public async Task<List<(string serial, int displayId)>> ListDevices() => new();
        public async Task<AdbResult> Run(string cmd) => new AdbResult { IsSuccess = true };
        public void Dispose() { }
    }

    public class AdbDeviceRegistry : IDisposable
    {
        public AdbDeviceRegistry(string configPath, int timeout) { }
        public AdbClient ResolveDevice(string serial) => new();
        public List<(string serial, int displayId)> ListAll() => new();
        public void Dispose() { }
    }

    public class AndroidNode
    {
        public string ResourceId { get; set; } = "";
        public string Text { get; set; } = "";
        public string ContentDescription { get; set; } = "";
        public bool Clickable { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public int? W { get; set; }
        public int? H { get; set; }
    }

    public class AndroidA11yTree
    {
        public AndroidA11yTree(string json) { }
        public List<AndroidNode> Nodes { get; set; } = new();
        public AndroidNode GetRoot() => new();
        public List<AndroidNode> GetFocusChain() => new();
        public List<AndroidNode> FindByPackage(string package) => new();
        public List<AndroidNode> FindAllInScope(string scope) => new();
        public AndroidNode ResolveScope(string scope) => new();
        public string DumpTree() => "";
    }

    public class AdbGrapInfo
    {
        public string PackageName { get; set; } = "";
        public string ActivityName { get; set; } = "";
        public string Package { get; set; } = "";
        public string ScopePath { get; set; } = "";
        public List<string> Scopes { get; set; } = new();
    }

    public class AdbResult
    {
        public bool IsSuccess { get; set; }
        public string Output { get; set; } = "";
        public string Error { get; set; } = "";
        public bool IsOk { get; set; }
        public string StdErr { get; set; } = "";
    }

    public class AdbGrapRouter
    {
        public static AdbGrapRouter Instance { get; } = new();
    }

    public class AdbActionLog
    {
        public List<string> Actions { get; set; } = new();
    }

    public class AdbActionReadiness
    {
        public bool IsReady { get; set; }
        public string Reason { get; set; } = "";
    }
}
