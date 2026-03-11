namespace WKAppBot.CLI;

// MCP tool descriptions — separated for readability
// Referenced by McpCommand.cs HandleToolsList()
internal partial class Program
{
    // ── Core: Unified A11y ────────────────────────────────────────

    const string A11yDesc =
        "WKAppBot unified accessibility — ONE command to control ANY window or web element on Windows. " +
        "Combines UIA (UI Automation), Win32, and CDP (Chrome DevTools Protocol) with automatic fallback. " +
        "Designed for focusless-first automation: controls apps WITHOUT stealing your keyboard focus.\n\n" +
        "## Grap Pattern (window targeting)\n" +
        "All tools use 'grap' (GRab Accessibility Pattern) to find windows:\n" +
        "- Wildcard: \"*Notepad*\", \"*계산기*\" (glob-style * and ?)\n" +
        "- Literal: \"Calculator\" (exact title match)\n" +
        "- Regex: \"regex:btn_\\\\d+\" (prefix with regex:)\n" +
        "- OR: \"*Notepad*;*Calculator*\" (semicolon-separated, matches either)\n" +
        "- Process: \"*(notepad.exe*\" (matches against '[Class] Title (proc hwnd=XX WxH)')\n" +
        "- Handle: \"*hwnd=001A0F2C*\" (direct window handle)\n\n" +
        "## #Scope (element targeting within a window)\n" +
        "Append #name to narrow into UIA elements or CSS selectors:\n" +
        "- UIA scope: \"*App*#*MenuBar*\" → finds 'MenuBar' element inside App window\n" +
        "- Multi-level: \"*App*#*Panel*#*Button*\" → Panel → Button (hierarchical drill-down)\n" +
        "- CSS (auto-detected for web views): \"*Chrome*#button.submit\", \"*Electron*#[aria-label='Send']\"\n" +
        "- Detection: starts with . or [ → CSS; contains > ~ + : or HTML tags → CSS; contains * ? → UIA\n\n" +
        "## Actions\n" +
        "Window: close, minimize, maximize, restore, focus, move (--x/--y), resize (--w/--h)\n" +
        "Element: invoke, click, toggle, expand, collapse, select, scroll, type (\"text\", also keystroke/hotkey), set-value (\"text\"), set-range (--value)\n" +
        "Query: find, read, highlight\n" +
        "Discovery: inspect (UIA element tree), windows (list all windows), screenshot (capture window), ocr (extract text)\n" +
        "Async: wait (poll until window/element appears, --timeout/--interval), eval (execute JavaScript via CDP, \"js expr\")\n" +
        "Utility: clipboard (show help), clipboard-read (read text), clipboard-write (write text/files — mixed mode with [file:] markers)\n" +
        "File I/O: file-read (read file as Unicode — supports --encoding 949/utf-8/etc.), file-write (write Unicode content as target encoding — use @file to reference temp content)\n" +
        "AI Agents (삼두협의체): ask-gpt (ask ChatGPT), ask-gemini (ask Google Gemini) — auto image capture from responses\n" +
        "Feedback: suggest (send feature request to Slack webhook + local suggestions.jsonl, optional file attachment)\n\n" +
        "## Fallback Chain (battle-tested!)\n" +
        "CSS selector on Chrome/Electron class → CDP engine → UIA fallback.\n" +
        "UIA pattern on web view class → if UIA fails → CDP retry.\n" +
        "Native app → UIA → Win32 message → SendInput (3-tier).\n\n" +
        "## Real-World Scenarios (proven in production)\n\n" +
        "### Legacy MFC/Win32 Apps (e.g. Korean stock trading HTS)\n" +
        "Owner-drawn MFC controls have NO UIA text — GetWindowText returns empty string, " +
        "buttons are bitmap-rendered. Solution: screenshot + OCR fallback reads the screen, " +
        "then coordinate-based click hits the right spot. We automate 20+ year old trading apps daily.\n\n" +
        "### Electron/Chrome Web Views (ChatGPT, Gemini, Slack, VS Code)\n" +
        "Single grap pattern \"*ChatGPT*#textarea\" auto-detects Chrome_WidgetWin_1 class → " +
        "routes to CDP engine → types via Input.insertText (works with React controlled inputs). " +
        "No manual port config needed — CDP port auto-discovered from process tree via netstat.\n\n" +
        "### Focusless Background Automation\n" +
        "Type text into a minimized window without bringing it to front. " +
        "UIA Invoke/Value/Toggle patterns are inherently focusless. " +
        "For MFC edit controls: PostMessage WM_CHAR sends keystrokes without stealing focus. " +
        "User keeps working while the bot automates in the background.\n\n" +
        "### Multi-Window Orchestration\n" +
        "Close all matching windows: action=close, grap=\"*Untitled*\", all=true. " +
        "Or target by process: \"*(chrome.exe*\" to find all Chrome windows. " +
        "Ancestor process protection: won't accidentally kill its own parent process tree.\n\n" +
        "### Dialog/Popup Auto-Dismissal\n" +
        "Unexpected popup blocking your automation? WKAppBot detects blocker windows (~5ms) " +
        "and auto-dismisses known dialog patterns. Experience DB learns which buttons work for each popup.\n\n" +
        "## Android ADB (adb:// scheme)\n" +
        "Prefix grap with adb:// to target Android devices via USB:\n" +
        "- Format: adb://device/package#scope (same #scope logic as Windows UIA)\n" +
        "- Auto-detect: adb://*heromts* (single device auto-select)\n" +
        "- Device+package: adb://Fold5/*heromts*#해외잔고\n" +
        "- Supported: inspect, find, windows, screenshot, click, read, scroll, type, close\n" +
        "- Experience DB: tree snapshots + action logs saved to profiles/{pkg}_exp/ and experience/android/{pkg}/\n" +
        "- Scope resolution: content-desc → text → resource-id (mirrors Windows UIA Name → AutomationId)\n\n" +
        "## Quick Examples\n" +
        "- List all windows: action=windows\n" +
        "- Inspect element tree: action=inspect, grap=\"*Notepad*\", depth=5\n" +
        "- Screenshot: action=screenshot, grap=\"*HTS*\" (background window OK)\n" +
        "- OCR legacy app: action=ocr, grap=\"*영웅문*\" (bitmap text → recognized text)\n" +
        "- Close all Notepads: action=close, grap=\"*Notepad*\", all=true\n" +
        "- Click web button: action=click, grap=\"*Chrome*#button.primary\"\n" +
        "- Type in native app: action=type, grap=\"*MyApp*#*SearchBox*\", text=\"hello\"\n" +
        "- Keystroke/hotkey: action=type, grap=\"*App*\", text=\"Ctrl+S\" (also: \"+Shift hello -Shift\", \"F5\")\n" +
        "- Read web heading: action=read, grap=\"*Electron*#h1.title\"\n" +
        "- Invoke MFC button: action=invoke, grap=\"*영웅문*#*조회*\" (Korean stock HTS)\n" +
        "- Toggle checkbox: action=toggle, grap=\"*Settings*#*DarkMode*\"\n" +
        "- Wait for dialog: action=wait, grap=\"*SaveAs*\", timeout=15000\n" +
        "- Wait for element: action=wait, grap=\"*App*#*ProgressDone*\", timeout=30000\n" +
        "- Eval JS in Chrome: action=eval, grap=\"*Chrome*\", text=\"document.title\"\n" +
        "- Eval with tab hint: action=eval, grap=\"*Chrome*#ChatGPT\", text=\"document.querySelectorAll('article').length\"\n" +
        "- Ask GPT: action=ask-gpt, text=\"How to get text from owner-drawn MFC button?\"\n" +
        "- Ask Gemini: action=ask-gemini, text=\"Analyze this UI screenshot\", grap=\"screenshot.png\"\n" +
        "- Ask with image: action=ask-gpt, text=\"What buttons are in this dialog?\", grap=\"dialog.png\"\n" +
        "- Android devices: action=windows, grap=\"adb://\"\n" +
        "- Android inspect: action=inspect, grap=\"adb://Fold5/*heromts*\", depth=10\n" +
        "- Android click tab: action=click, grap=\"adb://*heromts*#미체결\"\n" +
        "- Android type: action=type, grap=\"adb://*heromts*#검색\", text=\"삼성전자\"\n" +
        "- Android screenshot: action=screenshot, grap=\"adb://SM*\"\n" +
        "- Clipboard read: action=clipboard-read (Unicode + CP949 auto-detect)\n" +
        "- Clipboard write text: action=clipboard-write, text=\"hello world\"\n" +
        "- Clipboard copy files: action=clipboard-write, grap=\"report.pdf\" (CF_HDROP)\n" +
        "- Clipboard mixed: action=clipboard-write — pass text+file args, text→.txt with [file:] markers\n" +
        "- Send feature request: action=suggest, text=\"Please add X feature because Y\"\n" +
        "- Suggest with screenshot: action=suggest, text=\"UI bug: button misaligned\", grap=\"screenshot.png\"\n" +
        "- Read CP949 Korean source: action=file-read, grap=\"src/legacy.cpp\", --encoding 949 → outputs Unicode\n" +
        "- Write CP949 Korean source: action=file-write, grap=\"src/legacy.cpp\", text=\"@/tmp/edit.txt\", --encoding 949 (Claude writes UTF-8 to @file, wkappbot re-encodes)\n" +
        "- Read UTF-8 file: action=file-read, grap=\"config.json\" (default encoding UTF-8)\n" +
        "- Write UTF-8 file: action=file-write, grap=\"output.txt\", text=\"line1\\nline2\"";
}
