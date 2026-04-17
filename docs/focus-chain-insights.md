# Focus Chain Insights (2026-03-07~08)

User's key insights on automation strategy, collected over two days of work.

## Core Insight: Hot Focus Invariant

> "사용자에게 보여주고 뭔가를 하려면 최종창이 명확하게 포커싱될것이고,
> 뎁스가 아무리 깊더라도 최적화해서 랜더링되게 설계될것이다~ 엑빌까지도"

**The invariant**: If UI is visible and interactive to the user, the OS/app MUST have keyboard focus on it.
This is a design guarantee -- apps that violate it are broken by definition.

**Implication for automation**:
- Focus chain = the always-correct path to the active element
- No need for full tree search (expensive, fragile) -- just follow the focus
- Works at any depth: even 29 levels deep, `FocusedElement -> GetParent -> ... -> root` is O(n)
- "29까지 파보려면 전같으면 열댓번 삽질을 했겠네~ 랙과 싸우면서"

## Hot Focus Line: Orthogonal to Depth

> "뎁스란 개념은 메인에서 파들어가는건데~ 포커스관점에선 역으로 따라가는거니
> 마이너스 확장개념이고~ 뎁스는 애초에 상관이 없었던것"

**Depth and Focus Chain are orthogonal axes**:
- **Depth** = root -> down (+N direction, tree expansion)
- **Focus Chain** = focused element -> up (-N direction, ancestry traversal)

They operate in opposite directions on the same tree. Depth limiting the focus chain
was a **category error** -- they were never in the same dimension to begin with.
Not "depth-independent" but "depth-irrelevant by nature."

- Applied universally across all a11y commands ("엑빌 공통")

### Win32 Level
- `GetGUIThreadInfo(threadId)` -> `hwndFocus` (keyboard focus child)
- Walk `GetParent()` chain up to top-level window
- Show focused child, owned popups, style tags for each window

### UIA Level
- `_automation.FocusedElement()` -> walk `TreeWalker.GetParent()` chain
- Verify chain contains target window via `NativeWindowHandle` property
- Trim at target window boundary

## Focus Chain as Automation Compass

> "윈도 탐색만 해도~ 브로커가 누군지 한눈에 보이겠다"

The focus chain instantly reveals:
- **Which window is actually active** (not just foreground, but keyboard-focused child)
- **Blocker identification**: popup dialogs stealing focus show up in the chain
- **Target verification**: confirm the right control has focus before input
- **Experience DB enrichment**: hot focus line windows deserve dump-level recording

## Chrome Trusted Gesture Requirement

Chrome distinguishes "trusted" vs "untrusted" user gestures:
- `Runtime.evaluate` -> `element.click()` = **untrusted** -> file dialogs BLOCKED
- `Input.dispatchMouseEvent` (mousePressed + mouseReleased) = **trusted** -> file dialogs ALLOWED

This is a Chrome security feature, not a bug. CDP trusted clicks are essential for:
- File upload dialogs (Gemini, ChatGPT)
- Permission prompts
- Any action requiring "user activation"

## Win32 File Dialog Automation

Native OS file dialog (`#32770`) automation:
- UIA `ValuePattern.SetValue()` works but "열기" button may not respond
- **Reliable method**: Win32 `WM_SETTEXT` on Edit control + `BM_CLICK` on Button
- Dialog structure: `#32770` -> `ComboBoxEx32` -> `ComboBox` -> `Edit`
- Find dialog: `EnumWindows` + title match ("열기"/"Open") + structural verification (ComboBoxEx32 + DUIViewWndClassName)

## Focusless-First Principle (Reinforced)

> "셋포그라운드는 입력확보단계에서만 사용하삼"
> "리스토어는 포커스리스로 무조건 해주세요"

- `SetForegroundWindow` = ONLY in InputReadiness/EnsureFocus phase
- Window restore = `SW_SHOWNOACTIVATE` (never `SW_RESTORE`)
- No background threads polling/forcing foreground (CdpFocusGuard deleted)
- User may switch windows mid-operation -> automation must not fight back

## Whisper Study Insights (from previous session)

- Word-level timestamp extraction from Whisper transcription
- Audio segment slicing for vocabulary study
- Experience ring buffer pattern (0~9) applicable to audio segments
- Deferred: word-level MP3 slicing implementation
