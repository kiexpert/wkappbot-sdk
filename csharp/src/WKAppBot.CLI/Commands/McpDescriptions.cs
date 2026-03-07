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
        "Element: invoke, click, toggle, expand, collapse, select, scroll, type (--text), set-value (--text), set-range (--value)\n" +
        "Query: find, read, highlight\n" +
        "Discovery: inspect (UIA element tree), windows (list all windows), screenshot (capture window), ocr (extract text)\n" +
        "Async: wait (poll until window/element appears, --timeout/--interval), eval (execute JavaScript via CDP, --text)\n" +
        "AI Agents (삼두협의체): ask-gpt (ask ChatGPT), ask-gemini (ask Google Gemini) — auto image capture from responses\n\n" +
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
        "## Quick Examples\n" +
        "- List all windows: action=windows\n" +
        "- Inspect element tree: action=inspect, grap=\"*Notepad*\", depth=5\n" +
        "- Screenshot: action=screenshot, grap=\"*HTS*\" (background window OK)\n" +
        "- OCR legacy app: action=ocr, grap=\"*영웅문*\" (bitmap text → recognized text)\n" +
        "- Close all Notepads: action=close, grap=\"*Notepad*\", all=true\n" +
        "- Click web button: action=click, grap=\"*Chrome*#button.primary\"\n" +
        "- Type in native app: action=type, grap=\"*MyApp*#*SearchBox*\", text=\"hello\"\n" +
        "- Read web heading: action=read, grap=\"*Electron*#h1.title\"\n" +
        "- Invoke MFC button: action=invoke, grap=\"*영웅문*#*조회*\" (Korean stock HTS)\n" +
        "- Toggle checkbox: action=toggle, grap=\"*Settings*#*DarkMode*\"\n" +
        "- Wait for dialog: action=wait, grap=\"*SaveAs*\", timeout=15000\n" +
        "- Wait for element: action=wait, grap=\"*App*#*ProgressDone*\", timeout=30000\n" +
        "- Eval JS in Chrome: action=eval, grap=\"*Chrome*\", text=\"document.title\"\n" +
        "- Eval with tab hint: action=eval, grap=\"*Chrome*#ChatGPT\", text=\"document.querySelectorAll('article').length\"\n" +
        "- Ask GPT: action=ask-gpt, text=\"How to get text from owner-drawn MFC button?\"\n" +
        "- Ask Gemini: action=ask-gemini, text=\"Analyze this UI screenshot\", grap=\"screenshot.png\"\n" +
        "- Ask with image: action=ask-gpt, text=\"What buttons are in this dialog?\", grap=\"dialog.png\"";
}
