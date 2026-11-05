// Two pacers with one interface: `await pacer.acquire()` before each call.
//
// ReadPacer: below the burst threshold (default 4 requests in any 1000 ms) calls go out freely.
// Once a call would EXCEED it, every call keeps a minimum gap (default 500 ms) from the previous one,
// and gap mode stays on until a full window passes with no throttled call (so a sustained hammer
// settles at 2/s instead of re-bursting every second).
//
// WritePacer: a sliding 60 s window that never lets more than `perMinute` writes through (default 60),
// a margin under GitHub's secondary content-creation limit of about 80 per minute.
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
  constructor({ clock = realClock, perMinute = 60, windowMs = 60_000 } = {}) {
    if (perMinute >= 80) throw new Error('WritePacer perMinute must stay under 80');
    Object.assign(this, { clock, perMinute, windowMs });
    this.stamps = [];
  }
  _prune(now) {
    while (this.stamps.length && now - this.stamps[0] >= this.windowMs) this.stamps.shift();
  }
  async acquire() {
    let now = this.clock.now();
    this._prune(now);
    while (this.stamps.length >= this.perMinute) {
      await this.clock.sleep(this.stamps[0] + this.windowMs - now);
      now = this.clock.now();
      this._prune(now);
    }
    this.stamps.push(now);
    return now;
  }
}
