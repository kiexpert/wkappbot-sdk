# AppBot Android (scaffold)

Minimal Kotlin app that drives the phone through an AccessibilityService and serves a
localhost MCP endpoint. Status: debug build verified by CI (build + emulator MCP check); not yet exercised on a physical device.

## Connect a model

1. Install the APK, open AppBot, and tap **Open accessibility settings**. Enable AppBot.
2. Tap **Start MCP server**. A foreground notification shows `127.0.0.1:8765`.
3. Copy the token shown in the app.
4. Point the MCP client at:
   - URL: `http://127.0.0.1:8765/mcp`
   - Header: `Authorization: Bearer <token>`

Only loopback peers are accepted. Tap **Stop** in the notification to disconnect every model.

## Tools

| Tool | Purpose |
|---|---|
| `a11y_tree` | Active window as JSON tree |
| `a11y_find` | Find nodes by text, description or id |
| `a11y_click` | Click the first clickable match |
| `a11y_type` | Set text on an editable node |
| `a11y_scroll` | Scroll forward or backward |
| `global_action` | `back`, `home`, `recents`, `notifications`, `quick_settings`, `power_dialog`, `lock_screen` |

## Build

Requires JDK 17 and the Android SDK (`ANDROID_HOME`). Intended for the SDK QA runner:

```
gradle assembleDebug
```

CI: `.github/workflows/android-appbot-build.yml` builds the debug APK and proves on an Android emulator that `tools/list` returns the six tools with the bearer token and that a request without it gets HTTP 401. Protocol surface is hand-written JSON-RPC 2.0 (initialize, tools/list, tools/call); no external MCP library.
