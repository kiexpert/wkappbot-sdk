# WKAppBot Experience DB XBuild Knowhow Map

This is a reference-only document. It does not introduce new logic.

## 1) XBuild (A11Y) profile nodes
- Root: `wkappbot.hq/profiles/{profile}_exp/`
- Profile-level: `wkappbot.hq/profiles/{profile}_exp/knowhow.md`
- Form-level: `wkappbot.hq/profiles/{profile}_exp/form_{formId}/knowhow.md`
- Form action-level: `wkappbot.hq/profiles/{profile}_exp/form_{formId}/knowhow-{action}.md`
- Form failure log: `wkappbot.hq/profiles/{profile}_exp/form_{formId}/knowhow-failed-actions.md`

## 2) OS nodes (process/window-class)
- Root: `wkappbot.hq/experience/{process}/`
- App-level: `wkappbot.hq/experience/{process}/knowhow.md`
- Class action-level: `wkappbot.hq/experience/{process}/{class}/knowhow-{action}.md`
- Class failure log: `wkappbot.hq/experience/{process}/{class}/knowhow-failed-actions.md`

## 3) Common action tokens
- `invoke`
- `click`
- `type`
- `set-value`
- `scroll`
- `wait`

## 4) Recommended authoring split
- Put cross-cutting rules in `knowhow.md`.
- Put reproducible action steps in `knowhow-{action}.md`.
- Put repeated failures and recovery flow in `knowhow-failed-actions.md`.

## 5) Quick write/read commands
```bash
wkappbot knowhow write <form-id> "lesson text" --profile <profile>
wkappbot knowhow write <form-id> "lesson text" --cid <cid> --profile <profile>
wkappbot knowhow read <form-id> --cid <cid>
```

## 6) Source references (current behavior)
- `csharp/src/WKAppBot.CLI/Commands/InspectionCommands.cs`
- `csharp/src/WKAppBot.CLI/Commands/A11yCommand.cs`
- `CLAUDE.md`
