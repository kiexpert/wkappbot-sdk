namespace WKAppBot.CLI;

// MCP tool descriptions -- separated for readability
// Referenced by McpCommand.cs HandleToolsList()
internal partial class Program
{
    // -- Core: Unified A11y ----------------------------------------

    const string A11yDesc =
        "WKAppBot unified accessibility -- ONE command to control ANY window or web element on Windows. " +
        "Combines UIA (UI Automation), Win32, and CDP (Chrome DevTools Protocol) with automatic fallback. " +
        "Designed for focusless-first automation: controls apps WITHOUT stealing your keyboard focus.\n" +
        "MCP config: add to ~/.claude/mcp.json -> { \"mcpServers\": { \"wkappbot\": { \"command\": \"wkappbot\", \"args\": [\"mcp\"] } } }\n\n" +
        "## Grap Pattern (window targeting)\n" +
        "All tools use 'grap' (GRab Accessible Pattern) to find windows:\n" +
        "- Wildcard: \"*Notepad*\", \"*계산기*\" (glob-style * and ?)\n" +
        "- Literal: \"Calculator\" (exact title match)\n" +
        "- Regex: \"regex:btn_\\\\d+\" (prefix with regex:)\n" +
        "- OR: \"*Notepad*;*Calculator*\" (semicolon-separated, matches either)\n" +
        "- Process: \"*(notepad.exe*\" (matches against '[Class] Title (proc hwnd=XX WxH)')\n" +
        "- Handle: \"*hwnd=001A0F2C*\" (direct window handle)\n\n" +
        "## #Scope (element targeting within a window)\n" +
        "Append #name to narrow into UIA elements or CSS selectors:\n" +
        "- UIA scope: \"*App*#*MenuBar*\" -> finds 'MenuBar' element inside App window\n" +
        "- Multi-level: \"*App*#*Panel*#*Button*\" -> Panel -> Button (hierarchical drill-down)\n" +
        "- CSS (auto-detected for web views): \"*Chrome*#button.submit\", \"*Electron*#[aria-label='Send']\"\n" +
        "- Detection: starts with . or [ -> CSS; contains > ~ + : or HTML tags -> CSS; contains * ? -> UIA\n\n" +
        "## Actions\n" +
        "Window: close, minimize, maximize, restore, focus, move (--x/--y), resize (--w/--h)\n" +
        "Element: invoke, click, toggle, expand, collapse, select, scroll, type (\"text\", also keystroke/hotkey), set-value (\"text\"), set-range (--value)\n" +
        "Query: find, read, highlight\n" +
        "Discovery: inspect (UIA element tree), windows (list all windows), screenshot (capture window), ocr (extract text)\n" +
        "Async: wait (poll until window/element appears, --timeout/--interval), eval (execute JavaScript via CDP, \"js expr\")\n" +
        "Utility: clipboard (show help), clipboard-read (read text), clipboard-write (write text/files -- mixed mode with [file:] markers)\n" +
        "File I/O: file-read (read file as Unicode -- supports --encoding 949/utf-8/etc.), file-write (write Unicode content as target encoding -- use @file to reference temp content, auto backup ON), file-edit (search-replace in file -- old_string->text, auto backup, encoding-aware, multi-file glob)\n" +
        "AI Agents (삼두협의체): ask-gpt (ask ChatGPT), ask-gemini (ask Google Gemini), ask-claude (ask Claude Desktop) -- vision-capable, auto image capture from responses\n" +
        "  ! Vision input: use image_path param for clarity (e.g. image_path=\"screenshot.png\"). grap also works (backward-compat).\n" +
        "Messaging: slack (send Slack message -- text goes to configured channel)\n" +
        "  Example: action=slack, text=\"Build completed successfully!\"\n" +
        "Feedback: suggest (send feature request to Slack webhook + local suggestions.jsonl, optional file attachment)\n" +
        "  ! suggest: ALWAYS write in English -- Korean text = 2-3x token waste. Short & precise wins.\n" +
        "Diagnostics: eye (one-shot eye tick -- returns status of all Claude/Kro cards + Slack inbox)\n" +
        "  Example: action=eye -- returns card summary with process health, context %, recent thoughts\n\n" +
        "## Fallback Chain (battle-tested!)\n" +
        "CSS selector on Chrome/Electron class -> CDP engine -> UIA fallback.\n" +
        "UIA pattern on web view class -> if UIA fails -> CDP retry.\n" +
        "Native app -> UIA -> Win32 message -> SendInput (3-tier).\n\n" +
        "## Real-World Scenarios (proven in production)\n\n" +
        "### Legacy MFC/Win32 Apps (e.g. Korean stock trading HTS)\n" +
        "Owner-drawn MFC controls have NO UIA text -- GetWindowText returns empty string, " +
        "buttons are bitmap-rendered. Solution: screenshot + OCR fallback reads the screen, " +
        "then coordinate-based click hits the right spot. We automate 20+ year old trading apps daily.\n\n" +
        "### Electron/Chrome Web Views (ChatGPT, Gemini, Slack, VS Code)\n" +
        "Single grap pattern \"*ChatGPT*#textarea\" auto-detects Chrome_WidgetWin_1 class -> " +
        "routes to CDP engine -> types via Input.insertText (works with React controlled inputs). " +
        "No manual port config needed -- CDP port auto-discovered from process tree via netstat.\n\n" +
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
        "### Flutter Windows Apps\n" +
        "Flutter renders via its own engine -- UIA tree is nearly empty (no button elements). " +
        "UIA invoke/click succeeds (result=ok) but Flutter widget callbacks never fire. " +
        "Workaround: use coordinate-based click targeting the FLUTTERVIEW child window directly.\n" +
        "Step 1 -- find FLUTTERVIEW child: action=inspect, grap=\"*flutter_app*\", depth=2 (look for FLUTTERVIEW class in children)\n" +
        "Step 2 -- screenshot to find button coords: action=screenshot, grap=\"*flutter_app*\"\n" +
        "Step 3 -- click with coords: action=click, grap=\"*flutter_app*\", text=\"--x 320 --y 480\" (window-relative coords)\n" +
        "Note: FLUTTERVIEW child is typically offset ~8px left, ~31px top from outer window client area (title bar). " +
        "If click misses, inspect outer vs inner offsets and adjust coords accordingly.\n\n" +
        "## Android ADB (adb:// scheme)\n" +
        "Prefix grap with adb:// to target Android devices via USB:\n" +
        "- Format: adb://device/package#scope (same #scope logic as Windows UIA)\n" +
        "- Auto-detect: adb://*heromts* (single device auto-select)\n" +
        "- Device+package: adb://Fold5/*heromts*#해외잔고\n" +
        "- Supported: inspect, find, windows, screenshot, click, read, scroll, type, close\n" +
        "- Experience DB: tree snapshots + action logs saved to profiles/{pkg}_exp/ and experience/android/{pkg}/\n" +
        "- Scope resolution: content-desc -> text -> resource-id (mirrors Windows UIA Name -> AutomationId)\n\n" +
        "## Quick Examples\n" +
        "- List all windows: action=windows\n" +
        "- Inspect element tree: action=inspect, grap=\"*Notepad*\", depth=5\n" +
        "- Screenshot: action=screenshot, grap=\"*HTS*\" (background window OK)\n" +
        "- OCR legacy app: action=ocr, grap=\"*영웅문*\" (bitmap text -> recognized text)\n" +
        "- Close all Notepads: action=close, grap=\"*Notepad*\", all=true\n" +
        "- Click web button: action=click, grap=\"*Chrome*#button.primary\"\n" +
        "- Type in native app: action=type, grap=\"*MyApp*#*SearchBox*\", text=\"hello\"\n" +
        "- Keystroke/hotkey: action=type, grap=\"*App*\", text=\"Ctrl+S\" (also: \"+Shift hello -Shift\", \"F5\")\n" +
        "- Read web heading: action=read, grap=\"*Electron*#h1.title\"\n" +
        "- Invoke MFC button: action=invoke, grap=\"*영웅문*#*조회*\" (Korean stock HTS)\n" +
        "- Toggle checkbox: action=toggle, grap=\"*Settings*#*DarkMode*\"\n" +
        "- Wait for dialog: action=wait, grap=\"*SaveAs*\", timeout=15000\n" +
        "- Wait for element: action=wait, grap=\"*App*#*ProgressDone*\", timeout=30000\n" +
        "- Eval JS in Chrome: action=eval, grap=\"*Chrome*#domain.com\", text=\"document.title\" ! ALWAYS specify tab hint!\n" +
        "- Eval with URL hint: action=eval, grap=\"*Chrome*#share/69ae513a\", text=\"document.body.innerText\" (URL substring match)\n" +
        "- Eval with title hint: action=eval, grap=\"*Chrome*#ChatGPT\", text=\"document.querySelectorAll('article').length\"\n" +
        "- ! BAD: action=eval, grap=\"*Chrome*\", text=\"...\" -- hits active tab only, unreliable! Always use #tab-hint\n" +
        "- Ask GPT: action=ask-gpt, text=\"How to get text from owner-drawn MFC button?\"\n" +
        "- Ask Gemini (with image): action=ask-gemini, text=\"Analyze this UI\", image_path=\"screenshot.png\"\n" +
        "- Ask Claude Desktop: action=ask-claude, text=\"Review this code\", image_path=\"capture.png\"\n" +
        "- Ask with image (legacy): action=ask-gpt, text=\"What buttons?\", grap=\"dialog.png\" (grap=image works too)\n" +
        "- Send Slack message: action=slack, text=\"Automation done! ✅\"\n" +
        "- Eye tick (status): action=eye -- shows all Claude/Kro card states + Slack inbox\n" +
        "- Android devices: action=windows, grap=\"adb://\"\n" +
        "- Android inspect: action=inspect, grap=\"adb://Fold5/*heromts*\", depth=10\n" +
        "- Android click tab: action=click, grap=\"adb://*heromts*#미체결\"\n" +
        "- Android type: action=type, grap=\"adb://*heromts*#검색\", text=\"삼성전자\"\n" +
        "- Android screenshot: action=screenshot, grap=\"adb://SM*\"\n" +
        "- Clipboard read: action=clipboard-read (Unicode + CP949 auto-detect)\n" +
        "- Clipboard write text: action=clipboard-write, text=\"hello world\"\n" +
        "- Clipboard copy files: action=clipboard-write, grap=\"report.pdf\" (CF_HDROP)\n" +
        "- Clipboard mixed: action=clipboard-write -- pass text+file args, text->.txt with [file:] markers\n" +
        "- Send feature request: action=suggest, text=\"Please add X feature because Y\"\n" +
        "- Suggest with screenshot: action=suggest, text=\"UI bug: button misaligned\", grap=\"screenshot.png\"\n" +
        "- Read CP949 Korean source: action=file-read, grap=\"src/legacy.cpp\", --encoding 949 -> outputs Unicode\n" +
        "- Write CP949 Korean source: action=file-write, path=\"src/legacy.cpp\", text=\"@/tmp/edit.txt\", encoding=949 (writes target encoding, creates .bak first)\n" +
        "- Read UTF-8 file: action=file-read, grap=\"config.json\" (default encoding UTF-8)\n" +
        "- Write UTF-8 file: action=file-write, path=\"output.txt\", text=\"line1\\nline2\"\n" +
        "- Dry-run write preview: action=file-write, path=\"output.txt\", text=\"draft\", dry_run=true\n" +
        "- Edit file (search-replace): action=file-edit, path=\"src/foo.cs\", old_string=\"oldValue\", text=\"newValue\"\n" +
        "  -> auto backup: src/foo.cs.bak-TIMESTAMP.txt (printed in output so you can restore on mistake)\n" +
        "  -> encoding-aware: auto-detects UTF-8/CP949; use encoding=949 to override\n" +
        "- Replace all occurrences: action=file-edit, path=\"src/foo.cs\", old_string=\"foo\", text=\"bar\", replace_all=true\n" +
        "- Regex replace: action=file-edit, path=\"*.cs\", old_string=\"v(\\\\d+)\\.0\", text=\"v$1.1\", use_regex=true, replace_all=true\n" +
        "- CP949 file edit: action=file-edit, path=\"legacy.cpp\", old_string=\"old\", text=\"new\", encoding=949\n" +
        "- Skip backup: action=file-edit, path=\"tmp.txt\", old_string=\"x\", text=\"y\", i_really_want_no_backup=true\n" +
        "- Tool-friendly aliases: action=file-edit, path=\"src/foo.cs\", old_string=\"before\", new_string=\"after\", dry_run=true";
}
