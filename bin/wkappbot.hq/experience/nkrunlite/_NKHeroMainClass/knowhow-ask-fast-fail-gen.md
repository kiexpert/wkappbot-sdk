
## FocusStealer @ 2026-04-17 00:05
- Action `ask-fast-fail-gen` on [_NKHeroMainClass] (nkrunlite, hwnd=0xD0854) stole focus despite nominally being focusless.
- Next invocation will force yield popup (Win32 prop `WKAppBot_FocusStealer-ask-fast-fail-gen` stamped).
- If this is always the case, prefer wrapping with `--yield` or switching to SendInput approach.
