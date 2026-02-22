# ActiveX/COM Named Pipe Calling Convention v1 (Draft)

Goal: hide COM complexity behind a stable JSON-RPC style pipe contract.

## Transport
- Named Pipe: `\\.\pipe\wkappbot_kiwoom`
- Frame: `uint32 length (LE)` + `utf-8 json payload`
- Request/response are one-shot pairs.
- Events are server-push messages with `type="event"`.

## Request
```json
{
  "v": 1,
  "id": "req-0001",
  "traceId": "trc-abc",
  "type": "call",
  "method": "CommConnect",
  "params": [],
  "timeoutMs": 30000,
  "meta": { "client": "wkappbot-cli" }
}
```

## Response
```json
{
  "v": 1,
  "id": "req-0001",
  "traceId": "trc-abc",
  "ok": true,
  "result": 0,
  "error": null,
  "elapsedMs": 118
}
```

Failure example:
```json
{
  "v": 1,
  "id": "req-0001",
  "traceId": "trc-abc",
  "ok": false,
  "result": null,
  "error": {
    "code": "COM_E_UNEXPECTED",
    "message": "0x8000FFFF",
    "retryable": true,
    "data": { "method": "CommConnect" }
  },
  "elapsedMs": 121
}
```

## Event
```json
{
  "v": 1,
  "type": "event",
  "event": "OnEventConnect",
  "ts": "2026-02-22T09:00:01Z",
  "payload": { "nErrCode": 0 }
}
```

## Required Methods (minimum)
- `GetStatus`
- `CommConnect`
- `GetConnectState`
- `GetLoginInfo`
- `KOA_Functions`
- `SetInputValue`
- `CommRqData`
- `SetRealReg`

## Error Code Convention
- `PIPE_*`: transport/protocol errors
- `TIMEOUT_*`: timeout/deadline errors
- `COM_*`: COM invocation errors
- `VALIDATION_*`: request validation errors

## Safety Rules
- Always require `id` and `traceId`
- Always enforce timeout (`timeoutMs`)
- Never block forever on COM callbacks
- Keep response payload JSON-serializable only
