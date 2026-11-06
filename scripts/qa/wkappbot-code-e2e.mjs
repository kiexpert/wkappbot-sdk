#!/usr/bin/env node
// wkappbot-code end-to-end QA: drives REAL OpenCode with the installed wkappbot-code plugin against a scripted mock
// model, so the reflex body is judged by what it REFUSES, what it lets change on disk, and what every model request
// carries. Cases mirror the kih driver wkappbot-code/selftest-opencode-e2e.sh (owner order 2026-11-06).
//
// Usage (after `npm i -g wkappbot-code && wkappbot-code install` in a fresh HOME):
//   node scripts/qa/wkappbot-code-e2e.mjs [--uninstall BEFORE_SNAPSHOT_FILE]
//   --uninstall F   finally run `wkappbot-code uninstall` and compare the machine state with snapshot F
//                   (written before install by the same snapshot() below: `node ... --snapshot F`).
//   --snapshot F    only write the state snapshot to F and exit.
// Prints one PASS/FAIL line per case and "RESULT n/N"; exits 1 when any case fails.
// The mock model answers the prompt "RUN:<base64 JSON array of {name, arguments}>" with those tool calls, one per
// model turn, then "done"; it records whether each request's system prompt carried the [wisdom-kernel rules] block.
import http from "node:http";
import fs from "node:fs";
import os from "node:os";
import path from "node:path";
import { spawn, spawnSync } from "node:child_process";

const IS_WIN = process.platform === "win32";
const HOME = os.homedir();
const arg = (k) => { const i = process.argv.indexOf(k); return i > 0 ? process.argv[i + 1] : ""; };

function snapshot() {
  const g = spawnSync("git", ["config", "--global", "--get", "core.hooksPath"], { encoding: "utf8" });
  const st = path.join(HOME, ".claude", "settings.json");
  const prefix = (spawnSync("npm", ["prefix", "-g"], { encoding: "utf8", shell: IS_WIN }).stdout || "").trim();
  const binDir = IS_WIN ? prefix : path.join(prefix, "bin");
  const ls = (d) => { try { return fs.readdirSync(d).filter((n) => !/^(node_modules|npm|npx|corepack|node|wkappbot-code|wk-appbot-code|opencode)(\.cmd|\.ps1)?$/i.test(n)).sort().join(" "); } catch { return ""; } };
  const sum = (f) => { try { return String(fs.readFileSync(f).length) + ":" + fs.readFileSync(f, "utf8").split("").reduce((a, c) => (a * 31 + c.charCodeAt(0)) >>> 0, 7); } catch { return "absent"; } };
  return ["hooksPath=" + ((g.stdout || "").trim() || "unset"), "settings=" + sum(st), "prefix-bin=" + ls(binDir), "local-bin=" + ls(path.join(HOME, ".local", "bin"))].join("\n") + "\n";
}
if (arg("--snapshot")) { fs.writeFileSync(arg("--snapshot"), snapshot()); console.log(snapshot()); process.exit(0); }

const CFG = path.join(process.env.XDG_CONFIG_HOME || path.join(HOME, ".config"), "opencode", "opencode.json");
// The project lives under HOME, as a user's would, and is a repo WITH a commit (OpenCode keys its project on it).
const PROJ = fs.mkdtempSync(path.join(HOME, "wkcode-e2e-"));
let pass = 0, total = 0;
const ok = (name, cond, why) => { total++; if (cond) { pass++; console.log("PASS " + name); } else console.log("FAIL " + name + " -- " + String(why).slice(0, 500)); };

// ---- mock OpenAI-compatible model ----
let calls = 0, withRules = 0;
const server = http.createServer((req, res) => {
  let b = ""; req.on("data", (d) => (b += d)); req.on("end", () => {
    if (!req.url.includes("chat/completions")) { res.writeHead(200, { "content-type": "application/json" }); return res.end('{"data":[{"id":"m"}]}'); }
    calls++;
    const j = JSON.parse(b), msgs = j.messages || [];
    const sys = msgs.filter((m) => m.role === "system").map((m) => (typeof m.content === "string" ? m.content : JSON.stringify(m.content))).join("\n");
    if (sys.includes("[wisdom-kernel rules]")) withRules++;
    const user = [...msgs].reverse().find((m) => m.role === "user");
    const text = typeof user?.content === "string" ? user.content : (user?.content || []).map((c) => c.text || "").join("");
    const m = text.match(/RUN:([A-Za-z0-9+/=]+)/);
    const plan = m ? JSON.parse(Buffer.from(m[1], "base64").toString()) : [];
    const done = msgs.filter((x) => x.role === "tool").length;
    res.writeHead(200, { "content-type": "text/event-stream" });
    const send = (o) => res.write("data: " + JSON.stringify(o) + "\n\n");
    const base = { id: "c" + calls, object: "chat.completion.chunk", created: 1, model: "m" };
    const next = (j.tools || []).length ? plan[done] : null;
    if (next) {
      send({ ...base, choices: [{ index: 0, delta: { role: "assistant", tool_calls: [{ index: 0, id: "call_" + calls, type: "function", function: { name: next.name, arguments: JSON.stringify(next.arguments) } }] }, finish_reason: null }] });
      send({ ...base, choices: [{ index: 0, delta: {}, finish_reason: "tool_calls" }] });
    } else {
      send({ ...base, choices: [{ index: 0, delta: { role: "assistant", content: "MOCK-DONE" }, finish_reason: null }] });
      send({ ...base, choices: [{ index: 0, delta: {}, finish_reason: "stop" }] });
    }
    res.write("data: [DONE]\n\n"); res.end();
  });
});
await new Promise((r) => server.listen(0, "127.0.0.1", r));
const PORT = server.address().port;

// ---- wire the mock provider into the user's opencode.json (the plugin entry from `wkappbot-code install` stays) ----
let cfg = {};
try { cfg = JSON.parse(fs.readFileSync(CFG, "utf8")); } catch (e) { ok("opencode.json readable", false, e.message); }
const pluginEntry = (cfg.plugin || []).find((p) => String(Array.isArray(p) ? p[0] : p).includes("wisdom-kernel"));
ok("plugin entry present in opencode.json", !!pluginEntry, JSON.stringify(cfg.plugin));
cfg.provider = { ...(cfg.provider || {}), mock: { npm: "@ai-sdk/openai-compatible", name: "mock", options: { baseURL: `http://127.0.0.1:${PORT}/v1`, apiKey: "x" }, models: { m: { name: "m", tool_call: true } } } };
cfg.permission = { edit: "allow", webfetch: "allow", bash: "allow" };
cfg.model = "mock/m";
fs.writeFileSync(CFG, JSON.stringify(cfg, null, 2));

const git = (...a) => spawnSync("git", a, { cwd: PROJ });
git("init", "-q"); fs.writeFileSync(path.join(PROJ, "a.txt"), "hi\n"); git("add", "a.txt");
git("-c", "user.email=qa", "-c", "user.name=qa", "commit", "-qm", "init");

// stdin closed (`opencode run` reads piped stdin and waits for EOF); PWD names the project (OpenCode reads it).
const env = { ...process.env, PWD: PROJ };
async function run(plan) {
  const b64 = Buffer.from(JSON.stringify(plan)).toString("base64");
  const t0 = Date.now();
  const r = await new Promise((resolve) => {
    const ch = spawn(IS_WIN ? "opencode.cmd" : "opencode", ["run", "--print-logs", "-m", "mock/m", "RUN:" + b64], { cwd: PROJ, env, shell: IS_WIN, stdio: ["ignore", "pipe", "pipe"] });
    let out = ""; ch.stdout.on("data", (d) => (out += d)); ch.stderr.on("data", (d) => (out += d));
    const kill = setTimeout(() => { try { ch.kill("SIGKILL"); } catch {} }, 240000);
    ch.on("close", (code) => { clearTimeout(kill); resolve({ code, out: out.replace(/\x1b\[[0-9;]*m/g, "") }); });
    ch.on("error", (e) => { clearTimeout(kill); resolve({ code: -1, out: String(e) }); });
  });
  r.ms = Date.now() - t0;
  r.loadErr = /failed to load plugin|Plugin export/.test(r.out);
  r.refused = /\[wisdom-kernel\]/.test(r.out);
  r.lines = r.out.split("\n").filter((l) => /wisdom-kernel|risk-guard|intent-lock|fail-closed|MOCK-DONE/.test(l) && !/level=/.test(l)).slice(0, 3).join(" | ");
  return r;
}

const A = path.join(PROJ, "a.txt"), B = path.join(PROJ, "b.txt");
const bash = (command) => ({ name: "bash", arguments: { command, description: "qa" } });
const search = (w) => bash("wkskill search " + w);
const cases = [
  ["echo-hello runs", [bash("echo hello")], (r) => !r.refused && /hello/.test(r.out)],
  ["read passes", [{ name: "read", arguments: { filePath: A } }], (r) => !r.refused],
  ["no-verify refused", [bash("git commit --no-verify -m x")], (r) => r.refused],
  ["rm-home refused", [bash("rm -rf ~")], (r) => r.refused && fs.existsSync(path.join(HOME, ".config"))],
  ["unset-hookspath refused", [bash("git config --global --unset core.hooksPath")], (r) => r.refused],
  ["push-force refused", [bash("git push --force origin main")], (r) => r.refused],
  ["write-unpaid refused by the intent lock", [{ name: "write", arguments: { filePath: B, content: "unpaid" } }], (r) => r.refused && /intent-lock/.test(r.out) && !fs.existsSync(B)],
  ["write after a skill search creates the file", [search("write a project file"), { name: "write", arguments: { filePath: B, content: "written by the e2e" } }], () => fs.existsSync(B) && fs.readFileSync(B, "utf8").includes("written by the e2e")],
  ["edit after a skill search changes the file", [search("edit a project file"), { name: "edit", arguments: { filePath: A, oldString: "hi", newString: "edited by the e2e" } }], () => fs.readFileSync(A, "utf8").includes("edited by the e2e")],
];
for (const [name, plan, check] of cases) {
  const r = await run(plan);
  ok("plugin loads (" + name + ")", !r.loadErr, (r.out.match(/.*(failed to load plugin|Plugin export).*/) || [""])[0]);
  ok(name + " [" + r.ms + "ms]", r.code === 0 && check(r), "rc=" + r.code + " :: " + (r.lines || r.out.split("\n").filter((l) => l.trim() && !/level=/.test(l)).slice(-4).join(" | ")));
}
ok("per-call injection: every model request carries the rules (" + withRules + "/" + calls + ")", calls > 0 && withRules === calls, withRules + " of " + calls);

const before = arg("--uninstall");
if (before) {
  const u = spawnSync(IS_WIN ? "wkappbot-code.cmd" : "wkappbot-code", ["uninstall"], { encoding: "utf8", shell: IS_WIN });
  const after = snapshot(), was = fs.readFileSync(before, "utf8");
  ok("uninstall rc 0 and the machine state equals the pre-install snapshot", u.status === 0 && after === was, "rc=" + u.status + "\nbefore:\n" + was + "after:\n" + after + (u.stdout || "").slice(-300) + (u.stderr || "").slice(-300));
}
console.log("model calls: " + calls + "  platform: " + process.platform + "/" + process.arch);
console.log("RESULT " + pass + "/" + total);
server.close();
process.exit(pass === total ? 0 : 1);
