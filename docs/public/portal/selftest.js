'use strict';
// Portal self-test: Korean shell, sample chips, separate settings page, mock spokesman round, Python via run_python.
// node docs/public/portal/selftest.js  -> PASS n/n. The folder is served over local HTTP so both pages share one
// origin (and one localStorage) as on GitHub Pages. Needs network only for the Pyodide runtime (jsdelivr).
const fs = require('fs');
const http = require('http');
const path = require('path');
const { chromium } = require('playwright');

const TYPES = { '.html': 'text/html; charset=utf-8', '.js': 'text/javascript', '.mjs': 'text/javascript' };
const server = http.createServer((req, res) => {
  const name = path.basename(decodeURIComponent((req.url || '/').split('?')[0])) || 'index.html';
  const file = path.join(__dirname, name);
  if (!fs.existsSync(file)) { res.writeHead(404); res.end(); return; }
  res.writeHead(200, { 'content-type': TYPES[path.extname(file)] || 'application/octet-stream' });
  fs.createReadStream(file).pipe(res);
});

const results = [];
const check = (name, ok, why) => results.push({ name, ok: !!ok, why: ok ? '' : (why || 'assertion false') });
const shown = (page, sel) => page.$eval(sel, (e) => getComputedStyle(e).display !== 'none');

(async () => {
  await new Promise((r) => server.listen(0, '127.0.0.1', r));
  const base = 'http://127.0.0.1:' + server.address().port + '/';
  const pageErrors = [];
  let browser = null;
  try {
    browser = await chromium.launch();
    const ctx = await browser.newContext();
    const page = await ctx.newPage();
    page.on('pageerror', (e) => pageErrors.push(String((e && e.message) || e)));
    await page.goto(base + 'index.html');

    check('P1 title is the Korean portal name', (await page.title()) === '바이브코딩 포털', await page.title());
    const chips = await page.$$eval('#chips .chip', (bs) => bs.map((b) => b.getAttribute('data-prompt') || ''));
    check('P2 four sample chips, each with a prompt', chips.length === 4 && chips.every((p) => p.length > 20), String(chips.length));
    await page.click('#chips .chip');
    check('P3 a chip fills the prompt', (await page.inputValue('#prompt')) === chips[0]);
    check('P4 portal hides the settings and links to the settings page',
      !(await shown(page, 'section.settings')) && (await shown(page, 'a.to-settings')));

    const sp = await ctx.newPage();
    sp.on('pageerror', (e) => pageErrors.push(String((e && e.message) || e)));
    await sp.goto(base + 'settings.html');
    check('P5 settings page shows the settings and hides the workspace',
      (await shown(sp, 'section.settings')) && !(await shown(sp, 'main')) && (await shown(sp, 'a.to-portal')));
    await sp.selectOption('#sp-provider', 'mock');
    await sp.selectOption('#sc-provider', 'mock-sector');
    await page.reload();
    const prov = await page.evaluate(() => [document.getElementById('sp-provider').value, document.getElementById('sc-provider').value]);
    check('P6 a choice made on the settings page reaches the portal', prov[0] === 'mock' && prov[1] === 'mock-sector', prov.join(','));

    await page.evaluate(() => window.webseat.runPrompt('go'));
    const files = await page.evaluate(() => Object.keys(window.webseat.fs));
    check('P7 mock spokesman round writes hello.js', files.includes('hello.js'), files.join(','));

    const py = await page.evaluate(() => window.webseat.execTool({ id: 't', name: 'run_python',
      args: { code: "print(6*7)\nopen('out.txt','w').write(open('hello.js').read().upper())" } }));
    check('P8 run_python prints 42', py.includes('42'), py.slice(0, 300));
    const out = await page.evaluate(() => window.webseat.fs['out.txt']);
    check('P9 a file written in Python comes back as a virtual file', out === 'CONSOLE.LOG(6*7)', String(out));
  } catch (e) {
    check('run', false, String((e && e.stack) || e));
  } finally {
    if (browser) await browser.close().catch(() => {});
    server.close();
  }
  check('P10 no uncaught page errors', pageErrors.length === 0, pageErrors.join(' | '));

  const passed = results.filter((r) => r.ok).length;
  results.forEach((r) => console.log((r.ok ? 'ok   ' : 'FAIL ') + r.name + (r.ok ? '' : ' -- ' + r.why)));
  console.log((passed === results.length ? 'PASS ' : 'FAIL ') + passed + '/' + results.length);
  process.exit(passed === results.length ? 0 : 1);
})();
