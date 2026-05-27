---
id: grap
app: wkappbot-workflow
description: "GRAP = Grab Accessible Pattern -- JSON5-extended universal UI element address. {JSON5-window}#AbsTagPath: identifies any Win32/UIA/web/Android element. a11y find outputs verified '# TARGET \"grap\" [OK] Nms' for direct copy-paste targeting."
tags: [grap, pattern, a11y, uia, scope, json5, wildcard, hwnd, adb, tab-portal, find, abs-path, project]
---

> **Refresh**: `wkappbot skill read grap --if-newer` — v1.37 (2026-04-07)

# grap -- UI Element Address System (Window + UIA Scope + Web + ADB)

## Steps

1. [WHAT-IS-GRAP] grap = UI address. Before '#' = which window. After '#' = which UIA element inside.
Three-tier: Win32 window -> UIA subtree -> web content (CDP).
a11y find <grap> outputs clean markdown sections to stdout -- paste-ready grap + ready-to-run command.
NEW FORMAT (v5.13+):
## FOCUS
"{hwnd:...,pid:...,proc:'...'}#absTagPath"  <- focused window (dim)

## TARGET
"{hwnd:...,pid:...,proc:'chrome',domain:'chatgpt.com'}#absTagPath"  <- paste-ready grap
wkappbot a11y find "..."  <- copy this line to re-run
[OK] Nms  <- verify mark on line AFTER command (no [[double brackets]])
WindowTitle -> stderr (noise-free stdout)
MULTIPLE TARGETS: ## TARGETS N matches header + one ## TARGET block per window.
2. [WINDOW-PATTERNS] Before '#' -- addressing a top-level windoD:
  Substring:   'ChatGPT'                    contains, case-insensitive
  Wildcard:    '*Notepad*'                      * = any chars
  OR(;):       '*Notepad*;*Calculator*'           matches either
  JSON5:       {title:'Claude',proc:'chrome'}  AND logic
  HWND direct: {hwnd:0x010B084A}             instant lookup
  HWND bracket:[0012061C]                    inspect/windows output format
  Regex:       regex:^AppName                    prefix required
3. [JSON5-SPEC] JSON5-extended grap syntax -- official window address grammar.
SYNTAX: {field:value, field:value, ...}  -- all fields AND'd
  Single-quoted strings OK: {proc:'chrome'}
  Unquoted keys OK:         {proc:'chrome',domain:'claude.ai'}
  Trailing comma OK:        {proc:'chrome',}
  Hex literals OK:          {hwnd:0x010B084A}
FIELDS (all optional, combine freely):
  hwnd:   0x010B084A    Win32 HWND -- instant exact lookup, session-scoped
  pid:    19016         process ID -- session-scoped
  proc:   'chrome'      process name (no .exe), case-insensitive
  domain: 'claude.ai'   browser domain via CDP -- most stable web identifier
  title:  'Claude'      window title substring (30-char cap)
  cls:    'Chrome_WidgetWin_1'  Win32 window class -- essential for MFC/old apps
  cid:    100           Win32 control ID
  url:    'https://...' browser URL substring via CDP
OPERATORS:
  AND (default):    {proc:'chrome',domain:'chatgpt.com'}
  Array OR:         {title:['ChatGPT','Claude']}  -- matches if title contains either
  Glob in value:    {title:'*AppBot*'}             -- * and ? work in string values
  Regex in value:   {title:/AppBot.*/}             -- /regex/ literal, full .NET regex
  OR at top level:  use ;-syntax before '#':  '*Notepad*;*Calculator*'
COMPACT GRAP RULE (# TARGET verified output):
  browser  -> {hwnd,pid,proc,domain}         no cls/title/url
  MFC/old  -> {hwnd,pid,proc,cls}            cls essential; no accessibility tree = needs class
  modern   -> {hwnd,pid,proc}               proc sufficient
  NEVER include title/url in stored graps -- volatile, session-specific
4. [SCOPE-CHAIN] After '#' -- UIA element drill-down. Each segment = one tree level:
  '*App*#*Button*'        find Button anywhere under App
  '*App*#Panel#*OK*'      Panel first, then OK inside
  '*App*#'               empty scope = drill to keyboard-focused element
  Depth: up to 25 levels. Match: Name first, AutomationId fallback (both substring).
  Container-first: Pane/Group preferred over Button/Edit at same level.
  Regex segment: '*App*#regex:btn_\d+'
5. [TAB-PORTAL] Browser tab switching built into scope chain:
  '*Chrome*#ChatGPT#Send'
  -> (1) find TabItem 'ChatGPT' -> SelectionItem.Select() (focusless)
  -> (2) 600ms wait -> jump to RootWebArea
  -> (3) find 'Send' inside web content
  Works on Chrome/Edge/Electron. Combine with JSON5:
  '{domain:\'claude.ai\'}#.ProseMirror'  = JSON5 window + CSS scope in web
6. [WIN32-CHILD] '/' drills Win32 child windows (MFC/legacy multi-child-window apps):
  'Parent/Child'              Win32 GetChildren, pattern match
  'Parent/Child#UiaScope'     Win32 drill THEN UIA scope
  'Window/'                   trailing / = focus-tunnel to focused child + element
  Used for AppName/legacy apps where UIA tree is fragmented across child HWNDs.
  Example: 'AppName/MainPanel#InputField'
7. [ANDROID] adb:// prefix routes to Android UIA pipeline:
  'adb://Fold5/*heromts*#해외잔고'          device / package / scope
  'adb://*heromts*#해외잔고'              auto-detect device
  'adb://Fold5:outer/*pkg*'            :outer = foldable outer display
  Format: adb://[device[:display]]/[pkg-grap]#[scope#scope...]
8. [NODE-TAG] Element label format in CURSOR/TARGET sections:
  <Button>              ControlType only
  <ButtonOK>            ControlType + AutomationId
  <Button'확인'>         ControlType + Name (single-quoted, no truncation)
  <Button2Email>        ControlType + SiblingIndex + AutomationId
  <Group x7>            7 consecutive unnamed Groups (run-length in CURSOR)
  attrs: ltwh=x,y,w,h  actions="Invoke,..."
Use as scope: <ButtonOK> -> #OK   <Button'확인'> -> #*확인*
9. [ABS-TAG-PATH] Official absolute UIA tag path -- BuildAbsoluteTagPath output.
Format: Type_aid / Type_Nth / Type -- compressed repeats use //
  Doc_RootWebArea/Gro_1th/Gro////Gro_main/Gro_thread/Gro//////Edi_msg
Type = first 3 chars of UIA ControlType:
  Doc=Document  Gro=Group  Edi=Edit  But=Button  Pan=Pane  Win=Window
  Tex=Text      Lst=List   Tab=Tab   Tre=Tree     Img=Image  Lnk=Hyperlink
_aid  = AutomationId (truncated at first '-' for readability)
_Nth  = sibling index fallback when no AutomationId
//    = consecutive unnamed same-type nodes compressed (Gro/Gro/Gro -> Gro//)
Resolution: Type_aid -> ByAutomationId(aid) fast; Type_Nth -> sibling BFS; bare -> name BFS
VERIFIED: # TARGET line includes [OK] = FindByTitle(compactGrap) confirms correct hwnd
10. [FIND-OUTPUT] 'a11y find <grap>' stdout format (v5.13+):

## FOCUS                          (dim) -- focused window section
"{hwnd,pid,proc,...}#absTagPath"  paste-ready grap (double-quoted)

## TARGET                         (bold cyan) -- matched window section
"{hwnd,pid,proc,domain,...}#absTagPath"  paste-ready grap with mandatory fields
wkappbot a11y find "..."          COPY THIS LINE to re-run
[OK] Nms                          verify mark on line AFTER command
[stderr] WindowTitle              window title goes to stderr (not captured by pipes)

MANDATORY GRAP FIELDS (BuildFindGrap order):
  hwnd:        always first -- primary unambiguous identifier
  pid:         immediately after hwnd (collision safety, same-proc windows)
  proc:        process name always included
  domain:      browser/web windows (most stable identifier)
  file:        file:// browsing windows
  cls:         legacy MFC/old apps (no accessibility tree)
  <matchedField>: auto-injected when search matched via cmd/title/uia/etc.
                  e.g. searching chatgpt.com may match VS Code via cmd -- cmd:'...' injected

MULTIPLE TARGETS: ## TARGETS N matches (outer loop), one ## TARGET block per match.
All [DIAG:*] [KNOWHOW:*] -> stderr only. VERDICT suppressed.
11. [TIPS] Real-world patterns:
# Paste & go: copy quoted grap from # TARGET, strip outer quotes
  wkappbot a11y click '{hwnd:0x010B084A,proc:\'chrome\',domain:\'chatgpt.com\'}#Doc_RootWebArea/Gro_main/Edi_msg'
# Find then use abs path for precise targeting
  wkappbot a11y find '{domain:\'chatgpt.com\'}#*Message*'
# Regex value match
  wkappbot a11y find '{title:/^AppName.*/}'
# Window-level only
  wkappbot a11y find '{domain:\'chatgpt.com\'}'
# Electron (VS Code) -- not browser CDP, use proc
  '{title:\'WKAppBot\',proc:\'Code\'}'
# legacy app inner panel + UIA element
  'AppName/MainPanel#InputField'
# Wait for window
  wkappbot a11y wait '*InstallComplete*' --condition visible --timeout 60
# hack-hover: live abs path ticker
  wkappbot a11y hack-hover '*targetApp*'
12. CWD FIELD (2026-05-02): {cwd:'D:/path'} finds the subordinate child process window (e.g. PseudoConsoleWindow of cmd inside WT tab). Add hwnd field empty string to show hwnd in output. WT insight: each WT tab has its OWN CASCADIA_HOSTING_WINDOW_CLASS top-level hwnd -- not shared. All tabs same WT pid but unique hwnd. GetForegroundWindow() in WT returns exact tab hwnd. Use {cwd:'...'} grap at schedule fire time for dynamic tab targeting instead of stale TargetHwnd. Chain: find(cwd) -> PseudoConsoleWindow -> GetAncestor -> CASCADIA tab hwnd -> inject.
13. [MDI-CHILD] (2026-05-02) MDI child windows are now included as segments in grap paths: {proc:app}#{mdi-child-title}#Node[aid=X]. The MDI child title sits between window grap and leaf node, using # (UIA scope) since MDI children are part of the UIA tree. Emitted automatically by win-click deprecation suggest line: 'wkappbot --sudo win-click hwnd:0x... x y' now prints '[WIN-CLICK:SUGGEST] a11y click <grap>#<node>' with MDI child segment included when applicable. The suggested 'a11y click <grap> --x N --y N' is paste-ready. Example: a11y click "{proc:nfrunlite}#Chart#Button[aid=Buy]" --x 100 --y 50.
14. [WIN-CLICK-DEPRECATED] (2026-05-02) win-click is deprecated and only emits a paste-ready a11y click suggestion. When migrating skills/scripts that previously used 'wkappbot --sudo win-click hwnd:0x... cx cy', replace with 'wkappbot a11y click <grap> --x cx --y cy'. The new grap may include an MDI child segment (see [MDI-CHILD] step). Window-relative coords are still required -- subtract popup origin from screen coords as before.
15. STOP-EARLY PATH (MFC/legacy): grap child path '/A/B/C' can stop at ANY intermediate hwnd -- you do not need to reach a leaf element. Example: '*HeroApp*/MDIClient/Form1101' targets the Form1101 container window directly (for screenshot, click, or further child enumeration). Use when the container IS the target, not just a waypoint to a field. Combine with cls glob: '{proc:App}#MDIClient/*Panel*' -- each segment uses PatternMatcher so cls, title, cid all work as partial matches at every level.
16. OR COVERAGE-SUM MECHANICS (2026-05-18): ; OR is NOT first-match-wins. Each candidate window gets a SkillCoverageScore = sum(tok.Length/field.Length) across ALL patterns that match it. Same formula as skill search ranking. With multiple ; patterns, a window matching MORE patterns scores HIGHER. Use --all with invoke to fan-out to every match: 'a11y invoke "pat1;pat2;pat3" --all' invokes ALL matched buttons in one call. Ideal for bulk popup dismiss.
17. SPECIAL CHAR QUOTING IN FIELD VALUES (2026-05-18): Field values containing # or other special chars MUST be single-quoted or the char is misinterpreted. cls:#32770 WRONG -- bare # is parsed as grap scope separator. cls:'#32770' CORRECT. Rule: always quote cls values for Win32 dialog classes (#32770, etc.) and any value with #/:/;.
18. SHELL PARSING BREAKS REGEX VALUES (2026-05-18): /regex/ field values like {title:/AppBot/} work in wkappbot JSON5 parser but bash/cmd shell truncates or corrupts the value before delivery. Workaround: use Python subprocess list args (no shell): subprocess.run(['wkappbot','a11y','find','{title:/pattern/}']) -- bypasses shell entirely and delivers regex intact. Do NOT try to escape /regex/ in bash quotes -- unreliable across shells.
19. SHELL BRACE EXPANSION GOTCHA (2026-05-18): {proc:'x',cls:'y'} WITHOUT outer quotes triggers bash brace expansion -- commas split it into separate tokens: proc:'x' and cls:'y'. Always wrap grap in double or single outer quotes. The /regex/ delivery issue blamed on 'shell parsing' is actually this: missing outer quotes causing brace-split. With correct quoting, /regex/ values reach wkappbot intact. Actual /regex/ matching may still fail on elevated windows (UIA title not readable) -- separate issue.
20. CORRECTION on step 18 (2026-05-18): /regex/ shell parsing issue is actually bash brace expansion (step 19), not shell corruption of /. The Python subprocess workaround in step 18 is valid but the root cause explanation was wrong. /regex/ values DO reach wkappbot intact when outer quotes are present. The matching failure on elevated windows is a separate UIA accessibility issue unrelated to shell.
21. CORRECTION (2026-05-18, Codex review): Step 16 coverage-sum claim is UNVERIFIED. Testing Claude;WindowsTerminal vs WindowsTerminal;Claude showed pattern ORDER affects results, not multi-match count. ; OR is likely position-priority not coverage-sum-reranking. --all flag fan-out is confirmed correct. Step 18 overstated -- delete the 'shell corrupts /' claim, root cause is brace expansion (step 19). Step 19/20 brace expansion is bash/zsh specific -- PowerShell and cmd do NOT do brace expansion so warning does not apply there. Risk: semicolons inside field VALUES like {title:'A;B'} may be re-split by wkappbot SplitGrap internally even with outer quotes.
22. OPUS SOURCE REVIEW (2026-05-18): ; OR actual mechanic confirmed from A11yCommand.cs:655-668 -- naive Split(';') then order-preserving accumulation deduped by hwnd. Per-pattern internal score = patLen/snippetLen (WindowFinder.cs:263,479-485) within each pattern only. SkillCoverageScore (SkillCommand.Helpers.cs:145) is the SKILL-SEARCH ranker, NOT used in window finding -- completely separate code path. Steps 16 coverage-sum claim is demonstrably false. Correct description: ; OR yields matches in pattern declaration ORDER, first pattern's matches first. Step 17 justification wrong: GrapHelper.SplitGrap skips # inside {} depth tracking, so cls:#32770 parses identically to cls:'#32770' -- but quoting recommended for clarity/future-safety. Semicolon-inside-value split risk (Step 21) is REAL but lives in A11yCommand.cs:656 not SplitGrap -- {title:'A;B'} is shredded into {title:'A' and B'} (malformed). UIA element OR also first-match-wins (UiaLocator.cs:155-167 return on first hit) not coverage-summed.
