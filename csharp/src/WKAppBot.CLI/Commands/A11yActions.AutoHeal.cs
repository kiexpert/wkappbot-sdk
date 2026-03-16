using FlaUI.Core.AutomationElements;
using WKAppBot.Win32.Accessibility;
using WKAppBot.Win32.Input;
using WKAppBot.Win32.Window;
using WKAppBot.Win32.Native;
using System.Drawing.Imaging;

namespace WKAppBot.CLI;

internal partial class Program
{
    // ── Auto-Heal: click/invoke 전 티어 실패 시 삼두에 해결방법 자동 문의 ──────────────────
    //
    // 동작 조건:
    //   1. --auto-heal 플래그 명시  OR
    //   2. CWD가 WKAppBot 프로젝트 폴더 (안전장치: 앱봇 작업 중에만 자동 발동)
    //
    // 흐름: A11yClick/A11yInvoke 각 tier → _autoHealTiers 에 결과 기록
    //        → 최종 false 반환 시 FireAutoHeal → 슬랙 알림 + Task.Run(삼두 문의)

    // Per-thread tier log: populated during A11yClick/A11yInvoke execution.
    [ThreadStatic] static List<string>? _autoHealTiers;

    // Per-thread current command log file path — set at command start, captured before Task.Run.
    // Enables triad queries to attach the full action log for detailed diagnosis.
    [ThreadStatic] internal static string? _currentLogPath;

    /// <summary>Returns the current command's log file path (pre-move location).</summary>
    internal static string? GetCurrentLogPath() => _currentLogPath;

    internal static bool ShouldAutoHeal(string[] args)
    {
        if (args.Any(a => a == "--auto-heal")) return true;
        // Auto-on when working inside the WKAppBot project (safety guard — avoids spam in other projects)
        var cwd = Environment.CurrentDirectory.Replace('\\', '/');
        return cwd.Contains("WKAppBot", StringComparison.OrdinalIgnoreCase);
    }

    // ── Focusless Alternative Query ─────────────────────────────────────────────────────────
    // SendInput 도달 = 포커스리스 전 티어 실패 → "성공"이 아님.
    // 첫 번째 도달 시: 삼두에 포커스리스 대안 문의 (sentinel: knowhow-triad.md).
    // 삼두 무관하게: 실패 로그를 경험DB에 자동 저장.
    internal static void QueryFocuslessAlternativeOnce(IntPtr hwnd, AutomationElement el)
    {
        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            var procName = "unknown";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
            var clsBuf = new System.Text.StringBuilder(256);
            NativeMethods.GetClassNameW(hwnd, clsBuf, clsBuf.Capacity);
            var className = clsBuf.ToString();

            var safeProc  = SanitizePathTokenForExp(procName);
            var safeClass = SanitizePathTokenForExp(className);
            var expDir    = Path.Combine(DataDir, "experience", safeProc, safeClass);
            Directory.CreateDirectory(expDir);

            // Capture element info synchronously (AutomationElement not safe across threads)
            var name  = el.Properties.Name.ValueOrDefault ?? "";
            var type  = "";
            try { type = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
            var aid   = el.Properties.AutomationId.ValueOrDefault ?? "";
            var rect  = GetBoundingRect(el);
            var coords = rect != null
                ? $"({(rect.Value.Left + rect.Value.Right) / 2},{(rect.Value.Top + rect.Value.Bottom) / 2}) size={rect.Value.Width}x{rect.Value.Height}"
                : "no rect";
            var winTitle = "";
            try { winTitle = NativeMethods.GetWindowTextW(hwnd); } catch { }

            // Capture log path before Task.Run (ThreadStatic not accessible across threads)
            var capturedLogPath = _currentLogPath;

            // ── 실패 로그 → 경험DB 자동 저장 (삼두 무관) ──────────────────────────────
            var failLogPath = Path.Combine(expDir, $"faillog-click-{DateTime.Now:yyyyMMdd_HHmmss}.txt");
            var tried = _autoHealTiers;
            var triedStr = tried is { Count: > 0 }
                ? string.Join("\n", tried.Select((t, i) => $"  {i + 1}. {t}"))
                : "  (no tier log)";
            File.WriteAllText(failLogPath,
                $"# Click Failure Log — SendInput reached (all focusless tiers failed)\n" +
                $"Time   : {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                $"Window : proc={procName}, class={className}, title=\"{winTitle}\"\n" +
                $"Element: name=\"{name}\" type={type} aid=\"{aid}\" {coords}\n\n" +
                $"Tried:\n{triedStr}\n\n" +
                $"Action log: {capturedLogPath ?? "(unavailable)"}\n");
            Console.WriteLine($"[FOCUSLESS] fail-log → {Path.GetFileName(failLogPath)}");

            // ── 삼두 숙제: suggestions.jsonl에 추가 ───────────────────────────────────────
            // 중복 방지: suggestions.jsonl files 배열에 동일 expDir 경로가 있으면 이미 제출된 것
            var suggPath = Path.Combine(DataDir, "suggestions.jsonl");
            bool alreadyRegistered = false;
            if (File.Exists(suggPath))
            {
                var expDirNorm = expDir.Replace('\\', '/').TrimEnd('/') + "/";
                foreach (var line in File.ReadAllLines(suggPath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    try
                    {
                        var node = System.Text.Json.Nodes.JsonNode.Parse(line);
                        if (node?["status"]?.GetValue<string>() is "done" or "archived") continue;
                        var files = node?["files"]?.AsArray();
                        if (files == null) continue;
                        foreach (var f in files)
                        {
                            var fp = f?.GetValue<string>()?.Replace('\\', '/') ?? "";
                            if (fp.StartsWith(expDirNorm, StringComparison.OrdinalIgnoreCase))
                            { alreadyRegistered = true; break; }
                        }
                        if (alreadyRegistered) break;
                    }
                    catch { }
                }
            }
            if (alreadyRegistered) return;

            var suggestionText =
                $"[FOCUSLESS-HOMEWORK] SendInput reached for `{name}` in `{procName}/{className}` — " +
                $"all focusless tiers failed. Find best focusless click alternative.\n" +
                $"Context: title=\"{winTitle}\", element type={type}, aid=\"{aid}\", {coords}\n" +
                $"Tried: {triedStr.Replace("\n", " | ")}\n" +
                $"Fail log: {failLogPath}";
            var suggEntry = System.Text.Json.JsonSerializer.Serialize(new
            {
                ts     = DateTime.UtcNow.ToString("o"),
                from   = "auto-heal",
                cwd    = Environment.CurrentDirectory,
                text   = suggestionText,
                files  = capturedLogPath != null ? new[] { capturedLogPath } : Array.Empty<string>(),
                status = "pending",
                tag    = "focusless-homework"
            });
            try
            {
                File.AppendAllText(suggPath, suggEntry + "\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[FOCUSLESS] 삼두숙제 등록 → suggestions.jsonl ({procName}/{className} \"{name}\")");
                Console.ResetColor();
            }
            catch { }
        }
        catch { /* best effort */ }
    }

    // ── Focusless Success Knowhow Recording ────────────────────────────────────────────────
    // Two categories:
    //   Focusless  — zero focus change (FLUTTERVIEW WM_LBUTTON, WM_LBUTTON PostMessage, etc.)
    //   MinFocus   — brief focus steal + restore (UIA Invoke w/ focus-restore, MSAA put_accValue)
    //
    // Saved to: wkappbot.hq/experience/{proc}/{class}/knowhow-{action}.md
    // Read by:  LoadKnowhow() at action start → skip straight to known-working tier
    // Triad fills unknowns → response saved as draft entry in the same file.

    internal enum KnowhowCategory { Focusless, MinFocus }

    internal static void RecordTierSuccess(IntPtr hwnd, AutomationElement el, string action,
        string tierName, KnowhowCategory category)
    {
        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            var procName = "unknown";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
            var clsBuf = new System.Text.StringBuilder(256);
            NativeMethods.GetClassNameW(hwnd, clsBuf, clsBuf.Capacity);
            var className = clsBuf.ToString();
            if (string.IsNullOrEmpty(procName) || string.IsNullOrEmpty(className)) return;

            var expDir = Path.Combine(DataDir, "experience",
                SanitizePathTokenForExp(procName), SanitizePathTokenForExp(className));
            Directory.CreateDirectory(expDir);
            var mdPath = Path.Combine(expDir, $"knowhow-{action}.md");

            var name = el.Properties.Name.ValueOrDefault ?? "";
            var type = "";
            try { type = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
            var aid  = el.Properties.AutomationId.ValueOrDefault ?? "";
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var entry = $"- **{tierName}** — confirmed {date}  " +
                        $"(el: {type} \"{name}\" aid={aid})\n";

            var section = category == KnowhowCategory.Focusless
                ? "## ✅ Focusless (포커스 변경 없음)"
                : "## ⚡ Min-focus (포커스 잠깐 양보 후 복원)";

            string existing = File.Exists(mdPath) ? File.ReadAllText(mdPath) : "";

            // Skip if identical tier already recorded
            if (existing.Contains(tierName, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[KNOWHOW] already recorded: {tierName} ({procName}/{className})");
                return;
            }

            // Init file if new
            if (string.IsNullOrEmpty(existing))
                existing = $"# Focusless Knowhow — {action} on {procName}/{className}\n\n" +
                           $"## ✅ Focusless (포커스 변경 없음)\n\n" +
                           $"## ⚡ Min-focus (포커스 잠깐 양보 후 복원)\n\n" +
                           $"## ❌ 안 됨 (실패 확인)\n";

            // Insert entry under the right section
            var insertAfter = section + "\n";
            var idx = existing.IndexOf(insertAfter, StringComparison.Ordinal);
            if (idx >= 0)
                existing = existing.Insert(idx + insertAfter.Length, entry);
            else
                existing += $"\n{section}\n{entry}";

            File.WriteAllText(mdPath, existing);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[KNOWHOW] ✓ recorded: {tierName} ({category}) → {Path.GetFileName(mdPath)} ({procName}/{className})");
            Console.ResetColor();
        }
        catch { /* best effort */ }
    }

    /// <summary>Load knowhow hint for action start. Returns preferred tier names in order (focusless first).</summary>
    internal static List<string> LoadKnowhow(IntPtr hwnd, string action)
    {
        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            var procName = "unknown";
            try { procName = System.Diagnostics.Process.GetProcessById((int)pid).ProcessName; } catch { }
            var clsBuf = new System.Text.StringBuilder(256);
            NativeMethods.GetClassNameW(hwnd, clsBuf, clsBuf.Capacity);
            var className = clsBuf.ToString();

            var mdPath = Path.Combine(DataDir, "experience",
                SanitizePathTokenForExp(procName), SanitizePathTokenForExp(className),
                $"knowhow-{action}.md");
            if (!File.Exists(mdPath)) return [];

            var lines = File.ReadAllLines(mdPath)
                .Where(l => l.TrimStart().StartsWith("- **"))
                .Select(l => { var s = l.IndexOf("**", 4); return s > 0 ? l[4..s] : ""; })
                .Where(t => !string.IsNullOrEmpty(t))
                .ToList();

            if (lines.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[KNOWHOW] {action} on {procName}/{className}: known tiers → [{string.Join(", ", lines)}]");
                Console.ResetColor();
            }
            return lines;
        }
        catch { return []; }
    }

    internal static void FireAutoHeal(string action, AutomationElement el, IntPtr hwnd, string windowTitle)
    {
        var name = el.Properties.Name.ValueOrDefault ?? "";
        var type = "";
        try { type = el.Properties.ControlType.ValueOrDefault.ToString(); } catch { }
        var aid  = el.Properties.AutomationId.ValueOrDefault ?? "";
        var rect = GetBoundingRect(el);
        var coords = rect != null
            ? $"center=({(rect.Value.Left + rect.Value.Right) / 2},{(rect.Value.Top + rect.Value.Bottom) / 2}), size={rect.Value.Width}x{rect.Value.Height}"
            : "no rect";

        var cls = new System.Text.StringBuilder(128);
        NativeMethods.GetClassNameW(hwnd, cls, cls.Capacity);

        // Window/element state checks — helps triad diagnose hidden/disabled/occluded targets
        bool winVisible  = NativeMethods.IsWindowVisible(hwnd);
        bool winEnabled  = NativeMethods.IsWindowEnabled(hwnd);
        bool elEnabled   = true;
        bool elOffscreen = false;
        try { elEnabled   = el.Properties.IsEnabled.ValueOrDefault; }  catch { }
        try { elOffscreen = el.Properties.IsOffscreen.ValueOrDefault; } catch { }
        // Z-order: check if any other window occludes the element center
        string occludedBy = "";
        if (rect != null)
        {
            int cx = (rect.Value.Left + rect.Value.Right) / 2;
            int cy = (rect.Value.Top + rect.Value.Bottom) / 2;
            var topHwnd = NativeMethods.WindowFromPoint(new POINT { X = cx, Y = cy });
            if (topHwnd != IntPtr.Zero && topHwnd != hwnd &&
                NativeMethods.GetAncestor(topHwnd, 2 /* GA_ROOT */) != hwnd)
            {
                var topCls = new System.Text.StringBuilder(64);
                NativeMethods.GetClassNameW(topHwnd, topCls, topCls.Capacity);
                occludedBy = $" occluded-by=0x{topHwnd.ToInt64():X} ({topCls})";
            }
        }
        var stateInfo = $"win=visible:{winVisible},enabled:{winEnabled} el=enabled:{elEnabled},offscreen:{elOffscreen}{occludedBy}";

        var tried = _autoHealTiers;
        var triedStr = tried is { Count: > 0 }
            ? string.Join("\n", tried.Select((t, i) => $"  {i + 1}. {t}"))
            : "  (no tier log)";

        // Capture screenshots + OCR — bundled as attachments for the triad.
        // (1) Element region: tight crop → OCR target verification
        // (2) Window context: wider view → triad sees the full UI state
        var attachFiles = new List<string>();
        string ocrLine = "";
        var tmpDir = Path.Combine(Path.GetTempPath(), "wkappbot_autoheal");
        Directory.CreateDirectory(tmpDir);
        var sessionTag = $"{DateTime.Now:yyyyMMdd_HHmmss}_{action}";

        // Action log — captured at command start, moved to logs/old/ after action completes.
        // Background Task.Run waits briefly for the move then attaches the file.
        var capturedLogPathForHeal = _currentLogPath;

        // Source file auto-match: A11yActions.{PascalAction}.cs — triad can read the exact code that failed.
        // Convention: action "click" → A11yActions.Click.cs, "invoke" → A11yActions.Invoke.cs
        // Only available in dev context (CWD=WKAppBot), which is already the ShouldAutoHeal guard.
        var actionPascal = char.ToUpper(action[0]) + action[1..];
        var srcFile = Path.Combine(Environment.CurrentDirectory,
            "csharp", "src", "WKAppBot.CLI", "Commands", $"A11yActions.{actionPascal}.cs");
        if (File.Exists(srcFile))
        {
            attachFiles.Add(srcFile);
            Console.WriteLine($"[AUTO-HEAL] source attached → A11yActions.{actionPascal}.cs");
        }

        if (rect != null && rect.Value.Width > 0 && rect.Value.Height > 0)
        {
            try
            {
                // Element region screenshot → save → OCR
                using var elBmp = ScreenCapture.CaptureScreenRegion(
                    rect.Value.Left, rect.Value.Top, rect.Value.Width, rect.Value.Height);
                var elPath = Path.Combine(tmpDir, $"{sessionTag}_element.png");
                elBmp.Save(elPath, ImageFormat.Png);
                attachFiles.Add(elPath);
                Console.WriteLine($"[AUTO-HEAL] element screenshot → {Path.GetFileName(elPath)}");

                // OCR on same bitmap (before dispose)
                using var ocr = new WKAppBot.Vision.SimpleOcrAnalyzer();
                var ocrResult = ocr.RecognizeAll(elBmp).GetAwaiter().GetResult();
                var ocrText = ocrResult.FullText?.Trim() ?? "";
                if (!string.IsNullOrEmpty(ocrText))
                {
                    bool nameMatch = string.IsNullOrEmpty(name) ||
                                     ocrText.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                                     name.Contains(ocrText, StringComparison.OrdinalIgnoreCase);
                    ocrLine = $"\nOCR text in element area: \"{ocrText}\" (element.Name=\"{name}\", {(nameMatch ? "✓ match" : "⚠ MISMATCH — possible wrong target!")})";
                    Console.ForegroundColor = nameMatch ? ConsoleColor.Gray : ConsoleColor.Yellow;
                    Console.WriteLine($"[OCR-VERIFY] \"{ocrText}\" vs element.Name=\"{name}\" → {(nameMatch ? "✓ match" : "⚠ MISMATCH")}");
                    Console.ResetColor();
                }
            }
            catch (Exception ex) { Console.WriteLine($"[AUTO-HEAL] element screenshot failed: {ex.Message}"); }
        }

        // Window context screenshot (broader view)
        try
        {
            using var winBmp = ScreenCapture.CaptureWindow(hwnd);
            var winPath = Path.Combine(tmpDir, $"{sessionTag}_window.png");
            winBmp.Save(winPath, ImageFormat.Png);
            attachFiles.Add(winPath);
            Console.WriteLine($"[AUTO-HEAL] window screenshot → {Path.GetFileName(winPath)}");
        }
        catch { }

        // English prompt — triad AIs receive English for efficiency
        var question = $"""
            [AUTO-HEAL] All '{action}' tiers failed on a UI element. Suggest a working approach.
            Screenshots attached: element region + window context.

            Window : class={cls}, title="{windowTitle}", hwnd=0x{hwnd.ToInt64():X8}
            State  : {stateInfo}
            Element: name="{name}", type={type}, automationId="{aid}", {coords}{ocrLine}

            Tried (all failed / unverified):
            {triedStr}

            Suggest the best alternative. Prefer wkappbot CLI commands:
              a11y click/invoke with --x/--y coords, win-click, or a custom Win32/CDP approach.
            If code is needed, give a minimal C# snippet using PostMessage/SendInput/UIA APIs.
            """;

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"[AUTO-HEAL] {action} — all tiers failed → querying triad ({attachFiles.Count} attachments)...");
        Console.ResetColor();

        SlackPostToThread($"🔧 *[AUTO-HEAL]* `{action}` 전 티어 실패 (`{name}` on `{windowTitle}`) → 삼두 해결방법 문의 중... ({attachFiles.Count}개 스샷 첨부)");

        var capturedFiles = attachFiles.ToList(); // snapshot for closure
        var capturedLogForHeal = capturedLogPathForHeal;
        // Fire triad in background — user gets answers in the same Slack thread when ready
        _ = Task.Run(() =>
        {
            // Attach action log (moved to logs/old/ after action finishes)
            if (capturedLogForHeal != null)
            {
                var dir = Path.GetDirectoryName(capturedLogForHeal)!;
                var fn  = Path.GetFileName(capturedLogForHeal);
                var oldPath = Path.Combine(dir, "old", fn);
                for (int i = 0; i < 6 && !File.Exists(oldPath); i++)
                    Task.Delay(500).Wait();
                var logToAttach = File.Exists(oldPath) ? oldPath
                                : File.Exists(capturedLogForHeal) ? capturedLogForHeal : null;
                if (logToAttach != null)
                {
                    capturedFiles.Add(logToAttach);
                    Console.WriteLine($"[AUTO-HEAL] action log attached → {Path.GetFileName(logToAttach)}");
                }
            }

            try
            {
                AskTriadParallel(question, slackReport: true, timeoutSec: 120,
                    attachFiles: capturedFiles.Count > 0 ? capturedFiles : null,
                    newSession: true, loopMode: false, loopMaxSteps: 3,
                    loopRetry: 0, loopMaxParallel: 1, modelHint: null, noWait: false);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[AUTO-HEAL] triad query failed: {ex.Message}");
            }
            finally
            {
                // Clean up temp screenshots after triad is done
                foreach (var f in capturedFiles)
                    try { File.Delete(f); } catch { }
            }
        });
    }
}
