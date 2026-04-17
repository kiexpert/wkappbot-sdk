
## FocusStealer @ 2026-04-16 22:36
- Action `ask-after-send-Gemini` on [Windows.UI.Core.CoreWindow] (SearchHost, hwnd=0x10260) stole focus despite nominally being focusless.
- Next invocation will force yield popup (Win32 prop `WKAppBot_FocusStealer-ask-after-send-Gemini` stamped).
- If this is always the case, prefer wrapping with `--yield` or switching to SendInput approach.
