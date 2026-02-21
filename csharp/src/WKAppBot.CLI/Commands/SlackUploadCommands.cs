using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;
using WKAppBot.WebBot;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Native;
using WKAppBot.Win32.Window;

namespace WKAppBot.CLI;

// partial class: Slack file upload + screenshot commands
internal partial class Program
{
    /// <summary>
    /// Upload a file to Slack channel (v2 API: getUploadURLExternal → PUT → completeUploadExternal).
    /// </summary>
    static async Task<bool> SlackUploadFileAsync(string botToken, string channel, string filePath,
        string? title = null, string? threadTs = null, string? initialComment = null)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"[SLACK] File not found: {filePath}");
            return false;
        }

        var fileInfo = new FileInfo(filePath);
        var fileName = fileInfo.Name;
        var fileSize = fileInfo.Length;
        title ??= fileName;

        using var http = new HttpClient();

        // Step 1: Get upload URL
        var getUrlParams = $"filename={Uri.EscapeDataString(fileName)}&length={fileSize}";
        using var getUrlReq = new HttpRequestMessage(HttpMethod.Get,
            $"https://slack.com/api/files.getUploadURLExternal?{getUrlParams}");
        getUrlReq.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);

        var getUrlResp = await http.SendAsync(getUrlReq);
        var getUrlBody = await getUrlResp.Content.ReadAsStringAsync();
        var getUrlJson = JsonSerializer.Deserialize<JsonNode>(getUrlBody);

        if (getUrlJson?["ok"]?.GetValue<bool>() != true)
        {
            Console.WriteLine($"[SLACK] getUploadURLExternal failed: {getUrlJson?["error"]}");
            return false;
        }

        var uploadUrl = getUrlJson["upload_url"]?.GetValue<string>();
        var fileId = getUrlJson["file_id"]?.GetValue<string>();
        if (string.IsNullOrEmpty(uploadUrl) || string.IsNullOrEmpty(fileId))
        {
            Console.WriteLine("[SLACK] Missing upload_url or file_id in response");
            return false;
        }

        Console.WriteLine($"[SLACK] Uploading {fileName} ({fileSize:N0} bytes)...");

        // Step 2: Upload file content via POST to the upload URL
        using var fileContent = new StreamContent(File.OpenRead(filePath));
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
        var uploadResp = await http.PostAsync(uploadUrl, fileContent);
        if (!uploadResp.IsSuccessStatusCode)
        {
            Console.WriteLine($"[SLACK] File upload failed: HTTP {uploadResp.StatusCode}");
            return false;
        }

        // Step 3: Complete the upload (share to channel)
        var completePayload = new JsonObject
        {
            ["files"] = new JsonArray(new JsonObject
            {
                ["id"] = fileId,
                ["title"] = title
            })
        };

        if (!string.IsNullOrEmpty(channel))
            completePayload["channel_id"] = channel;
        if (!string.IsNullOrEmpty(threadTs))
            completePayload["thread_ts"] = threadTs;
        if (!string.IsNullOrEmpty(initialComment))
            completePayload["initial_comment"] = initialComment;

        using var completeReq = new HttpRequestMessage(HttpMethod.Post,
            "https://slack.com/api/files.completeUploadExternal");
        completeReq.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", botToken);
        completeReq.Content = new StringContent(completePayload.ToJsonString(),
            System.Text.Encoding.UTF8, "application/json");

        var completeResp = await http.SendAsync(completeReq);
        var completeBody = await completeResp.Content.ReadAsStringAsync();
        var completeJson = JsonSerializer.Deserialize<JsonNode>(completeBody);

        if (completeJson?["ok"]?.GetValue<bool>() != true)
        {
            Console.WriteLine($"[SLACK] completeUploadExternal failed: {completeJson?["error"]}");
            return false;
        }

        Console.WriteLine($"[SLACK] File uploaded: {fileName}");
        return true;
    }

    /// <summary>
    /// Upload a file to Slack.
    /// Usage: wkappbot slack upload &lt;file-path&gt; [--channel ID] [--thread TS] [--title "name"]
    /// </summary>
    static int SlackUploadCommand(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: wkappbot slack upload <file-path> [--channel ID] [--thread TS] [--title \"name\"]");
            return 1;
        }

        string? filePath = null;
        string? explicitChannel = null;
        string? threadTs = null;
        string? title = null;
        string? comment = null;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--channel" && i + 1 < args.Length)
                explicitChannel = args[++i];
            else if (args[i] == "--thread" && i + 1 < args.Length)
                threadTs = args[++i];
            else if (args[i] == "--title" && i + 1 < args.Length)
                title = args[++i];
            else if (args[i] == "--comment" && i + 1 < args.Length)
                comment = args[++i];
            else if (filePath == null)
                filePath = args[i];
        }

        if (string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine("[SLACK] ERROR: file path required");
            return 1;
        }

        var config = LoadSlackConfig();
        if (config == null) return 1;

        var botToken = config["bot_token"]?.GetValue<string>();
        var channel = explicitChannel ?? config["channel"]?.GetValue<string>();

        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
        {
            Console.WriteLine("[SLACK] ERROR: bot_token or channel not found");
            return 1;
        }

        var ok = SlackUploadFileAsync(botToken, channel, filePath, title, threadTs, comment)
            .GetAwaiter().GetResult();

        return ok ? 0 : 1;
    }

    /// <summary>
    /// Capture a screenshot and upload to Slack.
    /// Usage: wkappbot slack screenshot [window-title] [--channel ID] [--thread TS]
    /// </summary>
    static int SlackScreenshotCommand(string[] args)
    {
        string? windowTitle = null;
        string? explicitChannel = null;
        string? threadTs = null;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--channel" && i + 1 < args.Length)
                explicitChannel = args[++i];
            else if (args[i] == "--thread" && i + 1 < args.Length)
                threadTs = args[++i];
            else if (windowTitle == null)
                windowTitle = args[i];
        }

        var config = LoadSlackConfig();
        if (config == null) return 1;

        var botToken = config["bot_token"]?.GetValue<string>();
        var channel = explicitChannel ?? config["channel"]?.GetValue<string>();

        if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(channel))
        {
            Console.WriteLine("[SLACK] ERROR: bot_token or channel not found");
            return 1;
        }

        // Capture screenshot
        string screenshotPath;
        var outputDir = Path.Combine(DataDir, "output", "screenshots");
        Directory.CreateDirectory(outputDir);
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        if (!string.IsNullOrEmpty(windowTitle))
        {
            // Capture specific window
            var windows = WKAppBot.Win32.Window.WindowFinder.FindByTitle(windowTitle);
            if (windows.Count == 0)
            {
                Console.WriteLine($"[SLACK] Window not found: {windowTitle}");
                return 1;
            }

            var hwnd = windows[0].Handle;
            var safeTitle = windowTitle.Replace(" ", "_").Replace("/", "_").Replace("\\", "_");
            screenshotPath = Path.Combine(outputDir, $"slack_{timestamp}_{safeTitle}.png");
            var bmp = WKAppBot.Win32.Input.ScreenCapture.CaptureWindow(hwnd);
            if (bmp == null)
            {
                Console.WriteLine("[SLACK] Screenshot capture failed");
                return 1;
            }
            bmp.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);
            bmp.Dispose();
            Console.WriteLine($"[SLACK] Captured: {windows[0].Title} -> {screenshotPath}");
        }
        else
        {
            // Capture entire primary screen
            screenshotPath = Path.Combine(outputDir, $"slack_{timestamp}_screen.png");
            var bounds = System.Windows.Forms.Screen.PrimaryScreen!.Bounds;
            using var bmp = new System.Drawing.Bitmap(bounds.Width, bounds.Height);
            using var g = System.Drawing.Graphics.FromImage(bmp);
            g.CopyFromScreen(bounds.Location, System.Drawing.Point.Empty, bounds.Size);
            bmp.Save(screenshotPath, System.Drawing.Imaging.ImageFormat.Png);
            Console.WriteLine($"[SLACK] Captured: full screen -> {screenshotPath}");
        }

        // Upload to Slack
        var title2 = !string.IsNullOrEmpty(windowTitle)
            ? $"Screenshot: {windowTitle}"
            : "Screenshot: Full Screen";

        var ok = SlackUploadFileAsync(botToken, channel, screenshotPath, title2, threadTs)
            .GetAwaiter().GetResult();

        return ok ? 0 : 1;
    }
}
