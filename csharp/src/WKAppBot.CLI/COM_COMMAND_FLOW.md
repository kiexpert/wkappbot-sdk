# COM Command Flow (v1)

`wkappbot com` provides a generic, folder-scoped COM command UX.

## Session model
- Session file: `./.wkcom/session.json`
- Default profile (if no session file): `kiwoom`
- This lets each project folder keep its own COM target context.

## Commands
- `wkappbot com ls`
- `wkappbot com use <name>`
- `wkappbot com current`
- `wkappbot com methods`
- `wkappbot com call <method> [params...]`

## v1 Adapter
- `kiwoom` (via existing Named Pipe + KiwoomProxy)
- Reuses existing pipe request path and response formatting.

## Example
```bash
wkappbot com ls
wkappbot com use kiwoom
wkappbot com methods
wkappbot com call GetConnectState
wkappbot com call GetLoginInfo ACCNO
```
