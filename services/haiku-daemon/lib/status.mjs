// Status JSON the portal shows as a service-VM row. Schema v1 (fields are stable; new fields only appended):
//   schema, service, state, kill_switch, queue, answered_count, refused_count, rate_limited_count,
//   error_count, last_answer_at, last_latency_ms, started_at, updated_at, handoff_at, run_id
// state: starting | running | draining | handed_off | stopped. Timestamps are ISO-8601 UTC or null.
// It never carries a visitor id, a prompt or an answer text.
export const STATUS_SCHEMA = 1;

export function makeStatus({ runId = null, clock }) {
  const iso = (ms) => (ms == null ? null : new Date(ms).toISOString());
  const s = {
    schema: STATUS_SCHEMA, service: 'haiku-daemon', state: 'starting', kill_switch: 'closed',
    queue: 0, answered_count: 0, refused_count: 0, rate_limited_count: 0, error_count: 0,
    last_answer_at: null, last_latency_ms: null,
    started_at: iso(clock.now()), updated_at: iso(clock.now()), handoff_at: null, run_id: runId,
  };
  s.touch = () => { s.updated_at = iso(clock.now()); return s; };
  s.toJSON = () => { const { touch, toJSON, ...rest } = s; return rest; };
  return s;
}
