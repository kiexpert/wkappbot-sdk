// One daemon instance: pull every wire, drop requests, answer the queue, publish answers.
import { DropStore, validId } from './store.mjs';
import { normalize } from './wire.mjs';
import { readKillSwitch, allowedBySwitch } from './killswitch.mjs';
import { VisitorLimiter } from './ratelimit.mjs';
import { WritePacer } from './pacer.mjs';
import { makeStatus } from './status.mjs';
import { askProvider, loadProvider } from './answer.mjs';
import { RetryAfter } from './ghwire.mjs';
import { realClock } from './clock.mjs';

const day = (ms) => new Date(ms).toISOString().slice(0, 10);

export class HaikuService {
  constructor({ dir, wires, killSwitchPath, clock = realClock, cfg = loadProvider(), ask, limiter, writePacer,
    publishStatus = async () => {}, runId = null, runnerId = null, log = () => {}, preTick = async () => {},
    rng = Math.random, simulate = true }) {
    Object.assign(this, { wires, killSwitchPath, clock, cfg, publishStatus, log, preTick, rng, simulate });
    this.ask = ask || ((p) => askProvider(p, { cfg }));
    const L = cfg.limits || {};
    this.cap = L.daily_cap_writes ?? 400;
    this.store = new DropStore(dir);
    this.limiter = limiter || new VisitorLimiter({ clock, max: L.per_visitor_max ?? 3, windowMs: (L.per_visitor_window_min ?? 10) * 60_000 });
    this.writePacer = writePacer || new WritePacer({ clock });
    this.status = makeStatus({ runId, runnerId, displayName: cfg.display_name, cap: this.cap, clock });
    this.status.wire = wires.some((w) => w.flush) ? 'github' : 'unset';
    this.origins = new Map(); // id -> wire
    this.accepting = true;
    this.day = day(clock.now()); this.turns = 0; this.wireWrites = 0;
  }

  _rollDay() {
    const d = day(this.clock.now());
    if (d !== this.day) { this.day = d; this.status.requests_today = 0; this.status.wire_writes_today = 0; }
  }
  capped() { return this.status.wire_writes_today >= this.cap; }

  async pull() {
    for (const wire of this.wires) {
      for (const { raw, origin } of await wire.poll()) {
        const req = normalize(raw);
        if (!req || !validId(req.id)) { this.log('[DROP] malformed request'); continue; }
        if (this.store.putRequest({ ...req, origin, received_at: this.clock.now() })) {
          this.origins.set(req.id, wire);
          this.status.requests_today++;
        }
      }
    }
  }

  async answerOne(req) {
    const ks = readKillSwitch(this.killSwitchPath);
    this.status.kill_switch = ks;
    const base = { id: req.id, visitor: req.visitor, model: this.cfg.slot.model, dialect: req.dialect, meta: req.meta };
    if (!allowedBySwitch(ks, req.origin)) { this.status.refused_count++; return { ...base, status: 'refused', reason: 'kill_switch_closed' }; }
    if (req.origin !== 'internal') {
      if (this.capped()) { this.status.refused_count++; return { ...base, status: 'refused', reason: 'daily_cap' }; }
      const rl = this.limiter.check(req.visitor);
      if (!rl.ok) { this.status.rate_limited_count++; return { ...base, status: 'rate_limited', reason: 'per_visitor', retry_after_ms: rl.retryAfterMs }; }
    }
    const sim = this.simulate ? this.cfg.simulate || {} : {};
    if (sim.rate_429 && this.rng() < sim.rate_429) { // in-house API realism: an occasional 429
      this.status.rate_limited_count++;
      return { ...base, status: 'rate_limited', reason: 'simulated_429', retry_after_ms: 2000 };
    }
    const t0 = this.clock.now();
    if (sim.latency_ms) { const [a, b] = sim.latency_ms; await this.clock.sleep(a + Math.floor(this.rng() * (b - a))); }
    const r = await this.ask(req.prompt);
    const latency = this.clock.now() - t0;
    if (!r.ok) { this.status.error_count++; return { ...base, status: 'error', error: r.error, latency_ms: latency }; }
    this.status.answered_count++;
    this.status.last_answer_at = new Date(this.clock.now()).toISOString();
    this.status.last_latency_ms = latency;
    this.status.last_first_byte_ms = r.first_byte_ms == null ? null : latency - (r.total_ms ?? 0) + r.first_byte_ms;
    return { ...base, status: 'ok', text: r.text, latency_ms: latency, first_byte_ms: this.status.last_first_byte_ms };
  }

  async drain(max = Infinity) {
    this._rollDay();
    let n = 0;
    for (const req of this.store.pending()) {
      if (n++ >= max) break;
      const ans = { ...(await this.answerOne(req)), answered_at: new Date(this.clock.now()).toISOString() };
      this.store.putAnswer(ans);
      const w = this.origins.get(req.id) || this.wires.find((x) => x.origin === req.origin);
      if (w) await w.publishAnswer(ans);
      if (w?.flush) this.turns++;
      this.store.dropRequest(req.id);
    }
    await this.flushWires();
    this.status.queue = this.store.pending().length;
  }

  // One paced write per wire that has queued answers. A 403/429 Retry-After backs the pacer off and
  // leaves the answers queued for the next tick. At the daily cap nothing more is written to the wire.
  async flushWires() {
    for (const w of this.wires) {
      if (!w.flush || !w.pendingWrites() || this.capped()) continue;
      await this.writePacer.acquire();
      try {
        if (await w.flush()) { this.status.wire_writes_today++; this.wireWrites++; }
      } catch (e) {
        if (e instanceof RetryAfter) { this.writePacer.backoff(e.sec); this.log(`[WIRE] ${e.message}`); } else throw e;
      }
    }
    if (this.turns) this.status.writes_per_turn = Math.round((this.wireWrites / this.turns) * 100) / 100;
    if (this.capped()) this.status.state = 'capped';
  }

  nextDelayMs(fallback) {
    const ds = this.wires.map((w) => w.nextDelayMs?.()).filter((x) => x != null);
    return ds.length ? Math.min(...ds) : fallback;
  }

  async tick() {
    await this.preTick();
    this.status.kill_switch = readKillSwitch(this.killSwitchPath);
    if (this.accepting) await this.pull();
    await this.drain();
    if (this.status.state === 'starting' || (this.status.state === 'capped' && !this.capped())) this.status.state = 'running';
  }

  async flushStatus() {
    this.status.touch();
    await this.publishStatus(this.status.toJSON());
  }
}
