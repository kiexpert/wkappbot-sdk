Display name config suggestion superseded

Date: 2026-04-01

Reason:
- Current naming strategy is host-aware and workspace-derived.
- Recent runtime checks show prompt and Slack naming already follow the current spec shape without a per-project config file.
- Latest user direction allows dropping older naming suggestions if they do not match the current naming spec.

Observed runtime evidence:
- `wkappbot-core.exe prompt list`
  - `클롣[WG-WKAppBot]`
  - `클롣[W-HTS_Project]`
- `wkappbot-core.exe slack send "display-name dry run" --dry-run`
  - `bot-name=클롣[WG-WKAppBot]`

Conclusion:
- No separate per-project display-name config was added.
- This suggestion is superseded by the current workspace-derived display name strategy.
