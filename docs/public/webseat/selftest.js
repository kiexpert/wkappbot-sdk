'use strict';
// Offline self-test for webseat v3. Uses the preinstalled Chromium; no network except the mocked OpenRouter key route.
const fs = require('fs');
const path = require('path');
const { chromium } = require('playwright');

const PAGE = require('url').pathToFileURL(path.join(__dirname, 'index.html')).href;
const KEYS = 'https://openrouter.ai/api/v1/auth/keys';
const KEY_HEADERS = {
  'access-control-allow-origin': '*',
  'access-control-allow-headers': 'content-type',
  'access-control-allow-methods': 'POST, OPTIONS'
};
const SP_SECRET = 'sk-ant-SECRET-1234';
const SC_SECRET = 'sc-SECRET-5678';
const ISO = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(\.\d+)?Z$/;
const UUID = /^[0-9a-f]{8}-[0-9a-f]{4}-4[0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/;
const CORS = {
  'access-control-allow-origin': '*',
  'access-control-allow-methods': 'GET, POST, OPTIONS',
  'access-control-allow-headers': 'content-type, authorization, x-api-key, anthropic-version, anthropic-dangerous-direct-browser-access'
};
// one entry per login path; each mocks the provider's key-check endpoint
const LOGIN_PATHS = [
  { provider: 'anthropic', key: 'sk-ant-test', url: 'https://api.anthropic.com/v1/models',
    keyPage: 'https://console.anthropic.com/settings/keys', oauth: false,
    auth: (h) => h['x-api-key'] === 'sk-ant-test' && h['anthropic-dangerous-direct-browser-access'] === 'true' },
  { provider: 'openai-compatible', key: 'sk-openai-test', url: 'https://api.openai.com/v1/models',
    keyPage: 'https://platform.openai.com/api-keys', oauth: false, auth: (h) => h.authorization === 'Bearer sk-openai-test' },
  { provider: 'gemini', key: 'gemini-test', url: 'https://generativelanguage.googleapis.com/v1beta/openai/models',
    keyPage: 'https://aistudio.google.com/apikey', oauth: false, auth: (h) => h.authorization === 'Bearer gemini-test' },
  { provider: 'openrouter', key: 'sk-or-test', url: 'https://openrouter.ai/api/v1/key',
    keyPage: 'https://openrouter.ai/settings/keys', oauth: true, auth: (h) => h.authorization === 'Bearer sk-or-test' }
];
const parseJsonl = (text) => text.split(/\r?\n/).filter((l) => l.trim()).map((l) => { try { return JSON.parse(l); } catch (e) { return null; } });
const results = [];
const check = (name, ok, why) => results.push({ name, ok: !!ok, why: ok ? '' : (why || 'assertion false') });

(async () => {
  const pageErrors = [];
  let browser = null;
  try {
    browser = await chromium.launch();
    const ctx = await browser.newContext();
    // every http(s) request is aborted; the OpenRouter key route is mocked per page below
    await ctx.route('**/*', (route) => {
      if (/^https?:/i.test(route.request().url())) return route.abort();
      return route.continue();
    });
    const track = (p) => p.on('pageerror', (e) => pageErrors.push(String((e && e.message) || e)));

    // ---- Case A: command-only, mocks ----
    const page = await ctx.newPage();
    track(page);
    await page.goto(PAGE);
    try {
      check('A0 command-only is checked by default', await page.isChecked('#commandOnly'));
      await page.selectOption('#sp-provider', 'mock');
      await page.selectOption('#sc-provider', 'mock-sector');
      await page.evaluate(() => window.webseat.runPrompt('go'));
      const a = await page.evaluate(() => ({
        hello: window.webseat.fs['hello.js'],
        log: document.getElementById('log').textContent,
        results: window.webseat.lastSpokesmanToolResults.slice()
      }));
      check('A1 sector wrote hello.js', a.hello === 'console.log(6*7)', 'hello.js = ' + JSON.stringify(a.hello));
      check('A2 log contains 42', a.log.includes('42'));
      check('A3 log contains "spokesman done"', a.log.includes('spokesman done'));
      check('A4 log contains "[sector]"', a.log.includes('[sector]'));
      check('A5 every agent_cmd result contains "REPORT:"',
        a.results.length > 0 && a.results.every((r) => r.includes('REPORT:')), 'results = ' + JSON.stringify(a.results));
      check('A6 every agent_cmd result contains "changed files: hello.js"',
        a.results.length > 0 && a.results.every((r) => r.includes('changed files: hello.js')), 'results = ' + JSON.stringify(a.results));
      check('A7 no agent_cmd result contains the file text',
        a.results.length > 0 && a.results.every((r) => !r.includes('console.log(6*7)')), 'results = ' + JSON.stringify(a.results));
    } catch (e) {
      check('A run', false, String((e && e.stack) || e));
    }

    // ---- Case B: OpenRouter callback ----
    const pb = await ctx.newPage();
    track(pb);
    await pb.addInitScript(() => {
      try { sessionStorage.setItem('webseat2.pkce', 'v'.repeat(64)); } catch (e) { /* blocked */ }
    });
    await pb.route(KEYS, (route) => route.fulfill({
      status: 200, headers: KEY_HEADERS, contentType: 'application/json',
      body: JSON.stringify({ key: 'sk-or-test' })
    }));
    await pb.goto(PAGE + '?code=abc');
    await pb.waitForFunction(
      () => document.getElementById('sp-apiKey').value === 'sk-or-test' && location.search === '',
      null, { timeout: 8000 }
    ).catch(() => {});
    const b = await pb.evaluate(() => ({
      provider: document.getElementById('sp-provider').value,
      key: document.getElementById('sp-apiKey').value,
      search: location.search
    }));
    check('B1 spokesman provider is openrouter', b.provider === 'openrouter', 'provider = ' + b.provider);
    check('B2 key input is sk-or-test', b.key === 'sk-or-test', 'key = ' + b.key);
    check('B3 location.search is empty', b.search === '', 'search = ' + b.search);

    // ---- Case C: reload, files and the session autosave persist ----
    const before = await page.evaluate(() => ({ n: window.webseat.history.length, id: window.webseat.sessionId() }));
    await page.reload();
    const c = await page.evaluate(() => ({
      hello: window.webseat.fs['hello.js'], n: window.webseat.history.length, id: window.webseat.sessionId(),
      log: document.getElementById('log').textContent
    }));
    check('C1 hello.js persisted after reload', c.hello === 'console.log(6*7)', 'hello.js = ' + JSON.stringify(c.hello));
    check('C2 session autosaved: same id and history after reload',
      before.n > 0 && c.n === before.n && c.id === before.id && c.log.includes('spokesman done'),
      'before = ' + JSON.stringify(before) + ', after n=' + c.n + ' id=' + c.id);

    // ---- Case E: export via the button; keys never in the file ----
    await page.fill('#sp-apiKey', SP_SECRET);
    await page.fill('#sc-apiKey', SC_SECRET);
    const [dl] = await Promise.all([page.waitForEvent('download'), page.click('#exportSession')]);
    const exported = fs.readFileSync(await dl.path(), 'utf8');
    const rows = parseJsonl(exported);
    const msgs = rows.filter((r) => r && r.type !== 'webseat-files');
    const filesRow = rows.find((r) => r && r.type === 'webseat-files');
    check('E1 export is a .jsonl download of JSON lines', /\.jsonl$/.test(dl.suggestedFilename()) && rows.every((r) => r !== null),
      'name = ' + dl.suggestedFilename());
    check('E2 every message line has Claude Code shape', msgs.length > 0 && msgs.every((r) =>
      (r.type === 'user' || r.type === 'assistant') && r.message && r.message.role === r.type &&
      'content' in r.message && ISO.test(r.timestamp) && r.sessionId === before.id && UUID.test(r.sessionId)),
      'first = ' + JSON.stringify(msgs[0]));
    check('E3 sector lines are isSidechain:true, spokesman lines are not',
      msgs.some((r) => r.isSidechain === true) && msgs.some((r) => r.isSidechain === false));
    check('E4 export holds agent_cmd tool_use and tool_result blocks',
      msgs.some((r) => JSON.stringify(r.message.content).includes('"name":"agent_cmd"')) &&
      msgs.some((r) => JSON.stringify(r.message.content).includes('"type":"tool_result"')));
    check('E5 no API key in the export', !exported.includes(SP_SECRET) && !exported.includes(SC_SECRET));
    check('E6 export carries the files', !!filesRow && filesRow.files['hello.js'] === 'console.log(6*7)');

    // ---- Case I: import into a fresh browser profile, then round-trip ----
    const ctx2 = await browser.newContext();
    await ctx2.route('**/*', (route) => (/^https?:/i.test(route.request().url()) ? route.abort() : route.continue()));
    const pi = await ctx2.newPage();
    track(pi);
    await pi.goto(PAGE);
    await pi.setInputFiles('#importFile', { name: 'session.jsonl', mimeType: 'application/jsonl', buffer: Buffer.from(exported) });
    await pi.waitForFunction(() => document.getElementById('log').textContent.includes('imported session'), null, { timeout: 8000 }).catch(() => {});
    const im = await pi.evaluate(() => ({
      hello: window.webseat.fs['hello.js'], n: window.webseat.history.length, id: window.webseat.sessionId(),
      again: window.webseat.exportSession(), log: document.getElementById('log').textContent
    }));
    check('I1 import restores the files', im.hello === 'console.log(6*7)', 'hello.js = ' + JSON.stringify(im.hello));
    check('I2 import restores the spokesman history and session id',
      im.n === msgs.filter((r) => !r.isSidechain).length && im.id === before.id && im.log.includes('spokesman done'),
      'n = ' + im.n + ', id = ' + im.id);
    const rows2 = parseJsonl(im.again);
    const strip = (rs) => JSON.stringify(rs.filter((r) => r && r.type !== 'webseat-files'));
    const files2 = (rows2.find((r) => r && r.type === 'webseat-files') || {}).files;
    check('R1 round trip: re-export matches the original lines and files',
      strip(rows2) === strip(rows) && JSON.stringify(files2) === JSON.stringify(filesRow && filesRow.files));
    await pi.selectOption('#sp-provider', 'mock');
    await pi.selectOption('#sc-provider', 'mock-sector');
    const cont = await pi.evaluate(async () => {
      const n0 = window.webseat.history.length;
      const out = await window.webseat.runPrompt('again');
      return { n0, n1: window.webseat.history.length, out };
    });
    check('R2 imported session continues', cont.out === 'spokesman done' && cont.n1 > cont.n0, JSON.stringify(cont));

    // ---- Case L: one login path per provider, mocked endpoints ----
    for (const lp of LOGIN_PATHS) {
      const got = [];
      await pi.route(lp.url, (route) => {
        const req = route.request();
        if (req.method() === 'OPTIONS') return route.fulfill({ status: 204, headers: CORS });
        const h = req.headers();
        got.push(h);
        const ok = lp.auth(h);
        return route.fulfill({ status: ok ? 200 : 401, headers: CORS, contentType: 'application/json', body: ok ? '{"data":[]}' : '{"error":"bad key"}' });
      });
      await pi.selectOption('#sp-provider', lp.provider);
      await pi.fill('#sp-apiKey', lp.key);
      await pi.click('#sp-check');
      await pi.waitForFunction(() => /key (ok|check failed)/.test(document.getElementById('sp-check-status').textContent), null, { timeout: 8000 }).catch(() => {});
      const st = await pi.evaluate(() => ({
        status: document.getElementById('sp-check-status').textContent,
        getkey: document.getElementById('sp-getkey').href,
        signin: !document.getElementById('signin').hidden,
        stored: localStorage.getItem('webseat2.settings') || ''
      }));
      check('L ' + lp.provider + ': key check passes, Get key link, sign-in ' + (lp.oauth ? 'shown' : 'hidden'),
        st.status === 'key ok' && got.length === 1 && st.getkey === lp.keyPage && st.signin === lp.oauth && st.stored.includes(lp.key),
        JSON.stringify(st) + ' requests=' + got.length);
      await pi.unroute(lp.url);
    }
    await pi.selectOption('#sp-provider', 'mock');
    check('L mock: login row hidden', await pi.evaluate(() => document.getElementById('sp-login').hidden));

    // ---- Case S: skill tools against the real SDK catalog file ----
    const CATALOG = require('url').pathToFileURL(path.join(__dirname, '..', '..', 'skills', 'skills-data-full.js')).href;
    const ps = await ctx.newPage();
    track(ps);
    await ps.addInitScript((u) => { window.WEBSEAT_SKILLS_URL = u; }, CATALOG);
    await ps.goto(PAGE);
    const sk = await ps.evaluate(async () => {
      const ex = (name, args) => window.webseat.execTool({ id: 't', name: name, args: args });
      return {
        search: await ex('skill_search', { query: 'cdp timeout' }),
        read: await ex('skill_read', { id: 'a11y-find' }),
        miss: await ex('skill_read', { id: 'no-such-skill-xyz' }),
        tools: window.webseat.spokesmanToolNames()
      };
    });
    check('S1 skill_search returns id :: first-step lines', sk.search.includes(' :: ') && !sk.search.startsWith('error'), sk.search.slice(0, 200));
    check('S2 skill_read returns the id then numbered steps', sk.read.startsWith('a11y-find\n1. '), sk.read.slice(0, 200));
    check('S3 skill_read of a missing id is an error', sk.miss === 'error: no such skill: no-such-skill-xyz', sk.miss);
    check('S4 command-only spokesman keeps the skill tools but not read_file',
      sk.tools.includes('skill_search') && sk.tools.includes('skill_read') && !sk.tools.includes('read_file'), sk.tools.join(','));
    const pn = await ctx.newPage();
    track(pn);
    await pn.addInitScript((u) => { window.WEBSEAT_SKILLS_URL = u; }, CATALOG.replace('skills-data-full.js', 'missing-catalog.js'));
    await pn.goto(PAGE);
    const unreachable = await pn.evaluate(() => window.webseat.execTool({ id: 't', name: 'skill_search', args: { query: 'cdp' } }));
    check('S5 an unreachable catalog is a tool error, not a crash', unreachable.startsWith('error: skill catalog not reachable'), unreachable);
  } catch (e) {
    check('setup', false, String((e && e.stack) || e));
  } finally {
    if (browser) await browser.close().catch(() => {});
  }

  // ---- Case D: no uncaught page errors ----
  check('D no uncaught page errors', pageErrors.length === 0, pageErrors.join(' | '));

  const passed = results.filter((r) => r.ok).length;
  results.forEach((r) => console.log((r.ok ? 'ok   ' : 'FAIL ') + r.name + (r.ok ? '' : ' -- ' + r.why)));
  if (passed === results.length) {
    console.log('PASS ' + passed + '/' + results.length);
    process.exit(0);
  }
  console.log('FAIL ' + passed + '/' + results.length);
  process.exit(1);
})();
