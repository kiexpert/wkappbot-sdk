# haiku-daemon

The Haiku service behind the AppBot Code demo. It runs as the `haiku-service` GitHub Actions workflow,
answers prompts with `claude -p --model haiku` (text only, every tool disabled), and publishes a status
JSON that the portal shows as a service-VM row.

## Kill switch

`KILL_SWITCH` holds `closed` or `open`. It is **closed** by default and reads as closed whenever it is
missing, empty or malformed. Closed means no public request reaches claude: the visitor gets
`{"status":"refused","reason":"kill_switch_closed"}`. Internal requests (the runner's own local inbox,
never the public wire) are still answered so CI can prove the path. A live run re-reads the file from
`main` every 30 s. Opening it spends the owner's subscription and needs the owner's explicit go.

## Limits

| What | Rule |
|---|---|
| Read pacer | Free up to 4 requests per second. Past that, every request keeps a 500 ms gap until a full second passes unthrottled. |
| Polling | ETag / `If-None-Match`; a 304 costs no rate limit. |
| Write pacer | At most 60 writes in any 60 s window (GitHub's secondary content-creation limit is about 80/min). |
| Per visitor | At most 3 requests per fixed 10-minute window. |
| Prompt | Cut to 4000 characters. Answer cut to 16000 characters. Claude times out after 120 s. |

## Request and answer objects

One JSON object per request and one per answer, each written as a file (`req/<id>.json`, `ans/<id>.json`).
Ids match `[A-Za-z0-9_-]{1,80}`. Two request dialects are accepted:

```json
{"headers": {"X-Request-Id": "r1", "X-Visitor-Id": "v1"}, "body": {"prompt": "hi"}}
{"id": "r1", "visitor": "v1", "prompt": "hi"}
```

Answer: `{"id","visitor","model":"haiku","dialect","status":"ok|refused|rate_limited|error","text"?,"reason"?,"retry_after_ms"?,"latency_ms"?,"answered_at"}`.

## Status JSON (schema 1)

Path: `status.json` on branch `svc/haiku-daemon`, served at
`https://raw.githubusercontent.com/kiexpert/wkappbot-sdk/svc/haiku-daemon/status.json`.

| Field | Meaning |
|---|---|
| `schema` | `1` |
| `service` | `haiku-daemon` |
| `state` | `starting`, `running`, `draining`, `handed_off` or `stopped` |
| `kill_switch` | `closed` or `open` |
| `queue` | Requests waiting for an answer |
| `answered_count`, `refused_count`, `rate_limited_count`, `error_count` | Counters for this run |
| `last_answer_at`, `last_latency_ms` | Last successful answer (ISO-8601 UTC) and its latency |
| `started_at`, `updated_at`, `handoff_at` | Run timestamps; `updated_at` is the heartbeat (at least every 5 min) |
| `run_id` | The Actions run that holds the service lease |

It never carries a visitor id, a prompt or an answer text. If `updated_at` is older than 15 minutes, the service is down.

## Handoff before the 6 h runner cap

A run holds the lease (`run_id` in status.json). At 5h15m it dispatches a successor with `takeover=true`.
The successor resets the status branch to one commit carrying its own `run_id`; the predecessor's next
conditional write fails, it re-reads, sees the new owner, drains its queue and exits. A run with no
successor stops at 5h45m. The `7,37 * * * *` schedule is a watchdog: a run that finds a live lease exits at once.

## Tests

```bash
node --test test/*.test.mjs
```

Both pacers, the ETag poller, the rate limit, the kill switch and the handoff use a fake clock; claude is a
fake binary (`test/fake-claude.sh`). The workflow's `selftest` job makes one real Haiku call with the
kill switch closed.
