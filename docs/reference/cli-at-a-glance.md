# CLI at a Glance

```bash
# Discovery
wkappbot a11y inspect <grap>           # dump UIA tree
wkappbot a11y find <grap>              # resolve + print # TARGET
wkappbot a11y highlight <grap>         # visualize with zoom overlay
wkappbot find <keyword>                # unified window-title + UIA element search
wkappbot scan <window-title>           # scan app structure, build Experience DB

# Interaction (focusless where possible)
wkappbot a11y click  <grap>
wkappbot a11y type   <grap> "text"
wkappbot a11y read   <grap>            # CDP-first on browser windows, UIA fallback
wkappbot a11y scroll <grap> down 3
wkappbot dismiss "<window-title>"      # auto-dismiss popups (OCR importance check)

# Browser / CDP
wkappbot a11y read   "{proc:'chrome'}"                   # full page text via CDP
wkappbot a11y click  ".submit-btn"                       # CSS selector
wkappbot a11y read   "{proc:'chrome'}" --eval-js "document.title"

# Scenarios & high-level automation
wkappbot run scenario.yaml             # run a YAML test scenario
wkappbot do "<window>" <form> <button> # full combo-select + click + dialog flow

# AI delegation
wkappbot ask triad  "is this lock-free?"
wkappbot ask claude "explain this chart" chart.png

# Daemon & context
wkappbot eye tick                      # one-shot status + ctx%
wkappbot newchat "continue from: ..."  # handoff to fresh session
wkappbot claude-usage                  # JSONL size + context %
```

Full options: `wkappbot <command> --help` and the [command reference](commands.md).
