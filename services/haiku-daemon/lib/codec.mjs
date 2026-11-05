// Signaler-wire note codec (the portal relay's format).
// A note body is: TAG "\n" JSON envelope
//   { v:1, role:"host"|"guest", from:<8-char base36 peer id>, ack:<highest seq seen from the other side>,
//     next:<rolled note marker or null>, ring:[{ seq, ct }] }
// ct = base64url(12-byte IV + AES-GCM ciphertext of JSON { kind, data }).
// Dialects: we WRITE "wksig-relay" (the only tag the portal copy reads) and READ any /^[a-z][a-z0-9-]*-relay\n/
// (the vendor copy writes "wk-signal-relay").
const { subtle } = globalThis.crypto;
const getRandomValues = (a) => globalThis.crypto.getRandomValues(a);

export const WRITE_TAG = 'wksig-relay';
const TAG_RE = /^([a-z][a-z0-9-]*-relay)\n/;

export const b64u = {
  enc: (buf) => Buffer.from(buf).toString('base64url'),
  dec: (s) => new Uint8Array(Buffer.from(String(s), 'base64url')),
};

export async function importKey(k) {
  const raw = b64u.dec(k);
  if (![16, 24, 32].includes(raw.length)) throw new Error('relay key must be 128/192/256-bit');
  return subtle.importKey('raw', raw, 'AES-GCM', false, ['encrypt', 'decrypt']);
}

export async function seal(key, obj) {
  const iv = getRandomValues(new Uint8Array(12));
  const ct = new Uint8Array(await subtle.encrypt({ name: 'AES-GCM', iv }, key, new TextEncoder().encode(JSON.stringify(obj))));
  const out = new Uint8Array(12 + ct.length);
  out.set(iv); out.set(ct, 12);
  return b64u.enc(out);
}

export async function open(key, ct) {
  const raw = b64u.dec(ct);
  if (raw.length < 13) throw new Error('short ct');
  const pt = await subtle.decrypt({ name: 'AES-GCM', iv: raw.slice(0, 12) }, key, raw.slice(12));
  return JSON.parse(new TextDecoder().decode(pt));
}

// -> { tag, env } or null when the body is not a relay note.
export function parseNote(body) {
  const m = TAG_RE.exec(String(body || ''));
  if (!m) return null;
  try {
    const env = JSON.parse(body.slice(m[0].length));
    if (!env || env.v !== 1 || !Array.isArray(env.ring) || typeof env.from !== 'string') return null;
    return { tag: m[1], env };
  } catch { return null; }
}

export function formatNote(env, tag = WRITE_TAG) {
  return `${tag}\n${JSON.stringify(env)}`;
}

export function newPeerId() {
  let s = '';
  for (const b of getRandomValues(new Uint8Array(8))) s += (b % 36).toString(36);
  return s;
}

// Share-link fragment: #relay=<provider>&proj=<owner/repo>&issue=<n>&session=<id>&k=<base64url key>
export function parseLink(link) {
  const frag = String(link || '').split('#').pop();
  const p = new URLSearchParams(frag);
  const r = { provider: p.get('relay'), proj: p.get('proj'), issue: Number(p.get('issue')), session: p.get('session'), k: p.get('k') };
  if (!r.provider || !r.proj || !r.issue || !r.k) throw new Error('relay link is missing a field');
  return r;
}
