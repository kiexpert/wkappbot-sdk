# haiku-daemon

The Haiku service behind the WKAppBot Code demo. It runs as the `haiku-service` GitHub Actions workflow,
answers prompts with `claude -p --model haiku` (text only, every tool disabled), and publishes a status
JSON that the portal shows as a service-VM row.

## Kill switch

`KILL_SWITCH` holds `closed` or `open`. It is **closed** by default and reads as closed whenever it is
missing, empty or malformed. Closed means no public request reaches claude: the visitor gets
`{"status":"refused","reason":"kill_switch_closed"}`. Internal requests (the runner's own local inbox,
never the public wire) are still answered so CI can prove the path. A live run re-reads the file from
`main` every 30 s. Opening it spends the owner's subscription and needs the owner's explicit go.


## Provider slot

`provider.json` holds one slot (`family` + `model`; `claude` / `haiku` now) and a command template per family.
Moving to another family later is a config change: add its entry under `families` and point `slot` at it.
The claude family runs `claude -p --model haiku --tools "" --output-format text` with the in-house persona
as the system prompt and `CLAUDE_CODE_MAX_OUTPUT_TOKENS` = `max_tokens` (600). The persona answers in Korean,
does not volunteer its base model, and answers honestly when asked directly. `display_name` is the stand-in's
shown name.

## Limits

| What | Rule |
|---|---|
| Read pacer | Free up to 4 requests per second. Past that, every request keeps a 500 ms gap until a full second passes unthrottled. Shared by every read of a run. |
| Polling | ETag / `If-None-Match`; a 304 costs no rate limit. At start a `[PROBE]` log line measures this on the live channel. |
| Write pacer | At most 70 writes per 60 s and 450 per hour (GitHub's secondary content-creation limits are about 80/min and 500/h). A 403/429 `Retry-After` blocks every later write until it passes; the answers stay queued. |
| Daily cap | `daily_cap_writes` (400) wire writes per UTC day. At the cap the daemon stops writing to the wire and reports `state: capped`. |
| Per visitor | `per_visitor_max` (3) requests per fixed `per_visitor_window_min` (10) minute window. |
| Simulation | Each answer waits an extra 300-900 ms and 3% get `rate_limited` / `simulated_429` (in-house realism). `HAIKU_SIM=0` turns both off. |
| Prompt | Cut to 4000 characters. Answer cut to 16000 characters. The model call times out after 120 s. |

## The signaler wire

Secret `HAIKU_RELAY_LINK` is the channel's share link
(`#relay=<provider>&proj=<owner/repo>&issue=<n>&session=<id>&k=<base64url AES key>`), and `HAIKU_WIRE_TOKEN`
is a token that can read and comment on that issue. Neither is in this repo. With no link, the daemon runs
without a public wire.

- One GitHub issue per channel, and each peer owns one comment. The daemon is the **host** peer. It has a
  stable 8-character id per channel, so a restarted or successor run adopts and keeps editing the same note.
- Note body: a tag line, then `{v:1, role, from, ack, next, ring:[{seq, ct}]}`, where `ct` is base64url(12-byte IV +
  AES-GCM of `{kind, data}`). The daemon **writes `wksig-relay`** (the only tag the portal copy reads) and
  **reads any `*-relay` tag** (`wk-signal-relay` from the vendor copy too).
- Guests post `kind: "ask"` `{visitor, turn, text}`. The daemon answers `kind: "answer"`
  `{to, visitor, turn, status, text, reason, retry_after_ms, latency_ms}`. `status` is `ok`, `refused`
  (`kill_switch_closed` / `daily_cap`), `rate_limited` (`per_visitor` / `simulated_429`) or `error`.
- Reads use `GET .../issues/{n}/comments?per_page=100` with `If-None-Match`. The first write is a `POST`; every
  later write is a `PATCH` of the same note. All answers made in one tick ride one write, so a visitor turn
  costs at most one write (`writes_per_turn` in the status).
- Cadence: after a new ask, polls at 100/200/400 ms, then every 500 ms for 60 s, then every 1200 ms when idle.
  No burst when under 20% of the rate budget remains.
- On a cold start, turns from a guest note untouched for 10 minutes are not answered. Turns already answered
  in the daemon's own ring are never answered again.

Every request and every answer is also kept as one JSON file in the run's state dir (`req/<id>.json`,
`ans/<id>.json`), and the internal inbox uses the same file drop. Two local request shapes are accepted:
a header map (`{"headers":{"X-Request-Id","X-Visitor-Id"},"body":{"prompt"}}`) and flat fields
(`{"id","visitor","prompt"}`).

## Status JSON (schema 1)

Path: `status.json` on branch `svc/haiku-daemon`, served at
`https://raw.githubusercontent.com/kiexpert/wkappbot-sdk/svc/haiku-daemon/status.json`.
It is written at most once a minute while busy, at least every 5 minutes while idle, and at start and stop.

| Field | Meaning |
|---|---|
| `schema`, `service` | `1`, `haiku-daemon` |
| `display_name` | The in-house stand-in's shown name |
| `state` | `starting`, `running`, `capped`, `draining`, `handed_off` or `stopped` |
| `kill_switch` | `closed` or `open` |
| `wire` | `github` (public wire attached) or `unset` |
| `queue` | Requests waiting for an answer |
| `requests_today`, `cap`, `wire_writes_today` | Asks received and wire writes made today (UTC), and the daily write cap |
| `answered_count`, `refused_count`, `rate_limited_count`, `error_count` | Counters for this run |
| `writes_per_turn` | Wire writes per answered wire turn (≤ 1) |
| `last_answer_at`, `last_latency_ms`, `last_first_byte_ms` | Last successful answer (ISO-8601 UTC) and its latencies |
| `started_at`, `updated_at`, `handoff_at` | Run timestamps; `updated_at` is the heartbeat |
| `run_id`, `runner_id` | The Actions run that holds the service lease, and its runner |

It never carries a visitor id, a peer id, a prompt or an answer text. An `updated_at` older than 15 minutes means the service is down.

## Handoff before the 6 h runner cap

A run holds the lease (`run_id` in status.json). At 5h15m it dispatches a successor with `takeover=true`.
The successor resets the status branch to one commit carrying its own `run_id`; the predecessor's next
conditional write fails, it re-reads, sees the new owner, drains its queue and exits. A run with no
successor stops at 5h45m. The `7,37 * * * *` schedule is a watchdog: a run that finds a live lease exits at once.

## Tests

```bash
node --test test/*.test.mjs
```

Both pacers, the ETag poller, the rate limit, the kill switch, the wire (a fake GitHub issue-comments API) and the handoff use a fake clock; claude is a
fake binary (`test/fake-claude.sh`). The workflow's `selftest` job makes one real Haiku call with the
kill switch closed.
