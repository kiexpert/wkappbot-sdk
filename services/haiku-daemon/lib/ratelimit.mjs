// Fixed per-visitor rate limit: at most `max` requests per visitor in each fixed window of `windowMs`.
// Windows are aligned to multiples of windowMs, so the limit is the same for every visitor and easy to state.
import { realClock } from './clock.mjs';

export class VisitorLimiter {
  constructor({ clock = realClock, max = 3, windowMs = 10 * 60_000, maxVisitors = 10_000 } = {}) {
    Object.assign(this, { clock, max, windowMs, maxVisitors });
    this.buckets = new Map(); // visitor -> { win, n }
  }
  check(visitor) {
    const now = this.clock.now();
    const win = Math.floor(now / this.windowMs);
    const key = String(visitor || 'anonymous');
    let b = this.buckets.get(key);
    if (!b || b.win !== win) {
      if (!b && this.buckets.size >= this.maxVisitors) this._evict(win);
      b = { win, n: 0 };
      this.buckets.set(key, b);
    }
    if (b.n >= this.max) return { ok: false, retryAfterMs: (win + 1) * this.windowMs - now };
    b.n++;
    return { ok: true, remaining: this.max - b.n };
  }
  _evict(win) {
    for (const [k, b] of this.buckets) if (b.win !== win) this.buckets.delete(k);
  }
}
