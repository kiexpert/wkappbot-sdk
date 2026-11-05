// One daemon instance: pull every wire, drop requests, answer the queue, publish answers and status.
import { DropStore, validId } from './store.mjs';
import { normalize } from './wire.mjs';
import { readKillSwitch, allowedBySwitch } from './killswitch.mjs';
import { VisitorLimiter } from './ratelimit.mjs';
import { WritePacer } from './pacer.mjs';
import { makeStatus } from './status.mjs';
import { askClaude } from './answer.mjs';
import { realClock } from './clock.mjs';

export class HaikuService {
  constructor({ dir, wires, killSwitchPath, clock = realClock, ask = askClaude, limiter, writePacer,
    publishStatus = async () => {}, runId = null, log = () => {}, preTick = async () => {} }) {
    Object.assign(this, { wires, killSwitchPath, clock, ask, publishStatus, log, preTick });
    this.store = new DropStore(dir);
    this.limiter = limiter || new VisitorLimiter({ clock });
    this.writePacer = writePacer || new WritePacer({ clock });
    this.status = makeStatus({ runId, clock });
    this.origins = new Map(); // id -> { origin, wire }
    this.accepting = true;
  }

  async pull() {
    for (const wire of this.wires) {
      for (const { raw, origin } of await wire.poll()) {
        const req = normalize(raw);
        if (!req || !validId(req.id)) { this.log('[DROP] malformed request'); continue; }
        if (this.store.putRequest({ ...req, origin, received_at: this.clock.now() })) {
          this.origins.set(req.id, { origin, wire });
        }
      }
    }
  }

  async answerOne(req) {
    const ks = readKillSwitch(this.killSwitchPath);
    this.status.kill_switch = ks;
    const base = { id: req.id, visitor: req.visitor, model: 'haiku', dialect: req.dialect };
    if (!allowedBySwitch(ks, req.origin)) {
      this.status.refused_count++;
      return { ...base, status: 'refused', reason: 'kill_switch_closed' };
    }
    if (req.origin !== 'internal') {
      const rl = this.limiter.check(req.visitor);
      if (!rl.ok) {
        this.status.rate_limited_count++;
        return { ...base, status: 'rate_limited', retry_after_ms: rl.retryAfterMs };
      }
    }
    const t0 = this.clock.now();
    const r = await this.ask(req.prompt);
    const latency = this.clock.now() - t0;
    if (!r.ok) { this.status.error_count++; return { ...base, status: 'error', error: r.error, latency_ms: latency }; }
    this.status.answered_count++;
    this.status.last_answer_at = new Date(this.clock.now()).toISOString();
    this.status.last_latency_ms = latency;
    return { ...base, status: 'ok', text: r.text, latency_ms: latency };
  }

  async drain(max = Infinity) {
    let n = 0;
    for (const req of this.store.pending()) {
      if (n++ >= max) break;
      const ans = { ...(await this.answerOne(req)), answered_at: new Date(this.clock.now()).toISOString() };
      this.store.putAnswer(ans);
      const w = this.origins.get(req.id)?.wire || this.wires.find((x) => x.origin === req.origin);
      if (w) { await this.writePacer.acquire(); await w.publishAnswer(ans); }
      this.store.dropRequest(req.id);
    }
    this.status.queue = this.store.pending().length;
  }

  async tick() {
    await this.preTick();
    this.status.kill_switch = readKillSwitch(this.killSwitchPath);
    if (this.accepting) await this.pull();
    await this.drain();
    if (this.status.state === 'starting') this.status.state = 'running';
  }

  async flushStatus() {
    this.status.touch();
    await this.writePacer.acquire();
    await this.publishStatus(this.status.toJSON());
  }
}
