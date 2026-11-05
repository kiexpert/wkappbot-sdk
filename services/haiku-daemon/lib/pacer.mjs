// Two pacers with one interface: `await pacer.acquire()` before each call.
//
// ReadPacer: below the burst threshold (default 4 requests in any 1000 ms) calls go out freely.
// Once a call would EXCEED it, every call keeps a minimum gap (default 500 ms) from the previous one,
// and gap mode stays on until a full window passes with no throttled call (so a sustained hammer
// settles at 2/s instead of re-bursting every second).
//
// WritePacer: sliding 60 s and 1 h windows (defaults 70/min and 450/h), a margin under GitHub's secondary
// content-creation limits of about 80/min and 500/h. backoff() honors a 403/429 Retry-After.
import { realClock } from './clock.mjs';

export class ReadPacer {
  constructor({ clock = realClock, burst = 4, windowMs = 1000, minGapMs = 500 } = {}) {
    Object.assign(this, { clock, burst, windowMs, minGapMs });
    this.stamps = [];
    this.hotUntil = -Infinity;
  }
  _prune(now) {
    while (this.stamps.length && now - this.stamps[0] >= this.windowMs) this.stamps.shift();
  }
  async acquire() {
    let now = this.clock.now();
    this._prune(now);
    if (this.stamps.length >= this.burst || now < this.hotUntil) {
      const last = this.stamps.length ? this.stamps[this.stamps.length - 1] : -Infinity;
      const wait = last + this.minGapMs - now;
      if (wait > 0) await this.clock.sleep(wait);
      now = this.clock.now();
      this._prune(now);
      this.hotUntil = now + this.windowMs;
    }
    this.stamps.push(now);
    return now;
  }
}

export class WritePacer {
  constructor({ clock = realClock, perMinute = 70, perHour = 450 } = {}) {
    if (perMinute >= 80) throw new Error('WritePacer perMinute must stay under 80');
    if (perHour >= 500) throw new Error('WritePacer perHour must stay under 500');
    this.clock = clock;
    this.windows = [{ limit: perMinute, ms: 60_000 }, { limit: perHour, ms: 3_600_000 }];
    this.stamps = [];
    this.blockedUntil = -Infinity;
  }
  // A 403/429 with Retry-After (seconds) blocks every later write until it has passed.
  backoff(retryAfterSec) {
    const ms = Math.max(1, Number(retryAfterSec) || 60) * 1000;
    this.blockedUntil = Math.max(this.blockedUntil, this.clock.now() + ms);
  }
  async acquire() {
    for (;;) {
      const now = this.clock.now();
      if (now < this.blockedUntil) { await this.clock.sleep(this.blockedUntil - now); continue; }
      const horizon = this.windows[this.windows.length - 1].ms;
      while (this.stamps.length && now - this.stamps[0] >= horizon) this.stamps.shift();
      let wait = 0;
      for (const w of this.windows) {
        const inWin = this.stamps.filter((t) => now - t < w.ms);
        if (inWin.length >= w.limit) wait = Math.max(wait, inWin[inWin.length - w.limit] + w.ms - now);
      }
      if (wait > 0) { await this.clock.sleep(wait); continue; }
      this.stamps.push(now);
      return now;
    }
  }
}
