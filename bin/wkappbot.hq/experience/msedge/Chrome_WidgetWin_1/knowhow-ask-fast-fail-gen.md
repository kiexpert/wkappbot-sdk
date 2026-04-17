
## FocusStealer @ 2026-04-17 00:26
- Action `ask-fast-fail-gen` on [Chrome_WidgetWin_1] (msedge, hwnd=0x306BE) stole focus despite nominally being focusless.
- Next invocation will force yield popup (Win32 prop `WKAppBot_FocusStealer-ask-fast-fail-gen` stamped).
- If this is always the case, prefer wrapping with `--yield` or switching to SendInput approach.
