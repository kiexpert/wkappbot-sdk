// Conditional GET with ETag / If-None-Match. A 304 returns the cached body and, on the GitHub API,
// does not count against the primary rate limit. Every request goes through the read pacer.
export class EtagPoller {
  constructor({ fetchFn = globalThis.fetch, pacer, headers = {} } = {}) {
    Object.assign(this, { fetchFn, pacer, headers });
    this.cache = new Map(); // url -> { etag, body }
    this.stats = { requests: 0, notModified: 0 };
    this.last = null; // { status, remaining, limit } of the last response, for budget-aware cadence
  }
  async get(url) {
    if (this.pacer) await this.pacer.acquire();
    const hit = this.cache.get(url);
    const h = { ...this.headers };
    if (hit?.etag) h['If-None-Match'] = hit.etag;
    this.stats.requests++;
    const res = await this.fetchFn(url, { headers: h });
    const hv = (k) => res.headers?.get?.(k) ?? null;
    const num = (x) => (x == null ? null : Number(x));
    this.last = { status: res.status, remaining: num(hv('x-ratelimit-remaining') ?? hv('ratelimit-remaining')),
      limit: num(hv('x-ratelimit-limit') ?? hv('ratelimit-limit')) };
    if (res.status === 304 && hit) {
      this.stats.notModified++;
      return { status: 304, changed: false, body: hit.body };
    }
    if (res.status === 404) return { status: 404, changed: false, body: null };
    if (!res.ok) throw new Error(`GET ${url} -> ${res.status}`);
    const body = await res.json();
    const etag = res.headers.get('etag');
    if (etag) this.cache.set(url, { etag, body });
    return { status: res.status, changed: true, body };
  }
}
