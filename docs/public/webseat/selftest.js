'use strict';
// Offline self-test for webseat v2. Uses the preinstalled Chromium; no network except the mocked OpenRouter key route.
const path = require('path');
const { chromium } = require('playwright');

const PAGE = 'file://' + path.join(__dirname, 'index.html');
const KEYS = 'https://openrouter.ai/api/v1/auth/keys';
const KEY_HEADERS = {
  'access-control-allow-origin': '*',
  'access-control-allow-headers': 'content-type',
  'access-control-allow-methods': 'POST, OPTIONS'
};
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

    // ---- Case C: reload, files persisted ----
    await page.reload();
    const c = await page.evaluate(() => window.webseat.fs['hello.js']);
    check('C1 hello.js persisted after reload', c === 'console.log(6*7)', 'hello.js = ' + JSON.stringify(c));
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
