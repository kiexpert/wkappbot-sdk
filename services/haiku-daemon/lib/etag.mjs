// Conditional GET with ETag / If-None-Match. A 304 returns the cached body and, on the GitHub API,
// does not count against the primary rate limit. Every request goes through the read pacer.
export class EtagPoller {
  constructor({ fetchFn = globalThis.fetch, pacer, headers = {} } = {}) {
    Object.assign(this, { fetchFn, pacer, headers });
    this.cache = new Map(); // url -> { etag, body }
    this.stats = { requests: 0, notModified: 0 };
  }
  async get(url) {
    if (this.pacer) await this.pacer.acquire();
    const hit = this.cache.get(url);
    const h = { ...this.headers };
    if (hit?.etag) h['If-None-Match'] = hit.etag;
    this.stats.requests++;
    const res = await this.fetchFn(url, { headers: h });
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
