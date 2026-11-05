// Status JSON the portal shows as a service-VM row. Schema v1; fields are only ever appended.
//   schema, service, display_name, state, kill_switch, wire, queue, requests_today, cap, wire_writes_today,
//   answered_count, refused_count, rate_limited_count, error_count, writes_per_turn,
//   last_answer_at, last_latency_ms, last_first_byte_ms, started_at, updated_at, handoff_at, run_id, runner_id
// state: starting | running | capped | draining | handed_off | stopped. Timestamps are ISO-8601 UTC or null.
// It never carries a visitor id, a peer id, a prompt or an answer text.
export const STATUS_SCHEMA = 1;

export function makeStatus({ runId = null, runnerId = null, displayName = null, cap = null, clock }) {
  const iso = (ms) => (ms == null ? null : new Date(ms).toISOString());
  const s = {
    schema: STATUS_SCHEMA, service: 'haiku-daemon', display_name: displayName, state: 'starting',
    kill_switch: 'closed', wire: 'unset', queue: 0, requests_today: 0, cap, wire_writes_today: 0,
    answered_count: 0, refused_count: 0, rate_limited_count: 0, error_count: 0, writes_per_turn: null,
    last_answer_at: null, last_latency_ms: null, last_first_byte_ms: null,
    started_at: iso(clock.now()), updated_at: iso(clock.now()), handoff_at: null, run_id: runId, runner_id: runnerId,
  };
  s.touch = () => { s.updated_at = iso(clock.now()); return s; };
  s.toJSON = () => { const { touch, toJSON, ...rest } = s; return rest; };
  return s;
}
