#!/usr/bin/env node
// wkappbot-code end-to-end QA: drives REAL OpenCode with the installed wkappbot-code plugin against a scripted
// mock model, so the reflex hooks are judged by what they REFUSE and what they let change on disk.
//
// Usage (after `npm i -g wkappbot-code && wkappbot-code install` in a fresh HOME):
//   node scripts/qa/wkappbot-code-e2e.mjs [--lite]
//   --lite   force the bash lite harness (as on a machine without PowerShell): expect the intent lock to refuse.
// Prints one PASS/FAIL line per case and "RESULT n/N"; exits 1 when any case fails.
// The mock model answers the prompt "RUN:<tool>:<base64 json args>" with exactly that one tool call, then "done".
import http from "node:http";
import fs from "node:fs";
import os from "node:os";
import path from "node:path";
import { spawnSync } from "node:child_process";

const LITE = process.argv.includes("--lite");
const HOME = os.homedir();
const CFG_DIR = path.join(process.env.XDG_CONFIG_HOME || path.join(HOME, ".config"), "opencode");
const CFG = path.join(CFG_DIR, "opencode.json");
const PROJ = fs.mkdtempSync(path.join(os.tmpdir(), "wkcode-e2e-"));
const IS_WIN = process.platform === "win32";
let pass = 0, total = 0;
const ok = (name, cond, why) => { total++; if (cond) { pass++; console.log("PASS " + name); } else console.log("FAIL " + name + " -- " + String(why).slice(0, 400)); };

// ---- mock OpenAI-compatible model ----
let calls = 0;
const server = http.createServer((req, res) => {
  let b = ""; req.on("data", (d) => (b += d)); req.on("end", () => {
    if (!req.url.includes("chat/completions")) { res.writeHead(200, { "content-type": "application/json" }); return res.end('{"data":[{"id":"m"}]}'); }
    calls++;
    const j = JSON.parse(b), msgs = j.messages || [];
    const user = [...msgs].reverse().find((m) => m.role === "user");
    const text = typeof user?.content === "string" ? user.content : (user?.content || []).map((c) => c.text || "").join("");
    const m = text.match(/RUN:([a-z]+):([A-Za-z0-9+/=]+)/);
    res.writeHead(200, { "content-type": "text/event-stream" });
    const send = (o) => res.write("data: " + JSON.stringify(o) + "\n\n");
    const base = { id: "c" + calls, object: "chat.completion.chunk", created: 1, model: "m" };
    const wantTool = m && (j.tools || []).length && !msgs.some((x) => x.role === "tool");
    if (wantTool) {
      send({ ...base, choices: [{ index: 0, delta: { role: "assistant", tool_calls: [{ index: 0, id: "call_" + calls, type: "function", function: { name: m[1], arguments: Buffer.from(m[2], "base64").toString() } }] }, finish_reason: null }] });
      send({ ...base, choices: [{ index: 0, delta: {}, finish_reason: "tool_calls" }] });
    } else {
      send({ ...base, choices: [{ index: 0, delta: { role: "assistant", content: "done" }, finish_reason: null }] });
      send({ ...base, choices: [{ index: 0, delta: {}, finish_reason: "stop" }] });
    }
    res.write("data: [DONE]\n\n"); res.end();
  });
});
await new Promise((r) => server.listen(0, "127.0.0.1", r));
const PORT = server.address().port;

// ---- wire the mock provider into the user's opencode.json (plugin entry from `wkappbot-code install` stays) ----
let cfg = {};
try { cfg = JSON.parse(fs.readFileSync(CFG, "utf8")); } catch (e) { ok("opencode.json readable", false, e.message); }
const pluginEntry = (cfg.plugin || []).find((p) => String(Array.isArray(p) ? p[0] : p).includes("wisdom-kernel"));
ok("plugin entry present in opencode.json", !!pluginEntry, JSON.stringify(cfg.plugin));
cfg.provider = cfg.provider || {};
cfg.provider.mock = { npm: "@ai-sdk/openai-compatible", name: "mock", options: { baseURL: `http://127.0.0.1:${PORT}/v1`, apiKey: "x" }, models: { m: { name: "m", tool_call: true } } };
cfg.permission = { edit: "allow", webfetch: "allow", bash: "allow" };
cfg.model = "mock/m";
fs.writeFileSync(CFG, JSON.stringify(cfg, null, 2));

// ---- the project ----
spawnSync("git", ["init", "-q"], { cwd: PROJ });
fs.writeFileSync(path.join(PROJ, "a.txt"), "x\n");

const env = { ...process.env };
if (LITE) env.WK_APPBOT_FORCE_LITE = "1";
const oc = IS_WIN ? "opencode.cmd" : "opencode";
async function run(tool, args) {
  const b64 = Buffer.from(JSON.stringify(args)).toString("base64");
  const t0 = Date.now();
  const r = await new Promise((resolve) => {
    // async spawn: the mock server above lives in this same process and must keep answering
    const { spawn } = require_cp;
    const ch = spawn(oc, ["run", "--print-logs", "-m", "mock/m", `RUN:${tool}:${b64}`], { cwd: PROJ, env, shell: IS_WIN });
    let out = ""; ch.stdout.on("data", (d) => (out += d)); ch.stderr.on("data", (d) => (out += d));
    const kill = setTimeout(() => { try { ch.kill("SIGKILL"); } catch {} }, 240000);
    ch.on("close", (code) => { clearTimeout(kill); resolve({ code, out: out.replace(/\x1b\[[0-9;]*m/g, "") }); });
    ch.on("error", (e) => { clearTimeout(kill); resolve({ code: -1, out: String(e) }); });
  });
  r.ms = Date.now() - t0;
  r.loadErr = /failed to load plugin/.test(r.out);
  r.refused = /\[wisdom-kernel\]/.test(r.out);
  return r;
}
const require_cp = await import("node:child_process");

const fileA = path.join(PROJ, "a.txt"), fileB = path.join(PROJ, "b.txt");
const cases = LITE ? [
  ["lite: rm -rf ~ is refused by the intent lock", "bash", { command: "rm -rf ~", description: "wipe" }, (r) => r.refused && /intent-lock/.test(r.out)],
] : [
  ["benign bash runs (echo)", "bash", { command: "echo hello-from-tool", description: "hi" }, (r) => !r.refused && /hello-from-tool/.test(r.out)],
  ["write creates a project file", "write", { filePath: fileB, content: "hello" }, (r) => !r.refused && fs.existsSync(fileB) && fs.readFileSync(fileB, "utf8").includes("hello")],
  ["edit changes a project file", "edit", { filePath: fileA, oldString: "x", newString: "y" }, (r) => !r.refused && fs.readFileSync(fileA, "utf8").includes("y")],
  ["read passes", "read", { filePath: fileA }, (r) => !r.refused],
  ["git commit --no-verify is refused", "bash", { command: "git commit --no-verify -m x", description: "commit" }, (r) => r.refused],
  ["rm -rf ~ is refused", "bash", { command: "rm -rf ~", description: "wipe" }, (r) => r.refused],
];
for (const [name, tool, args, check] of cases) {
  const r = await run(tool, args);
  ok("plugin loads (" + name + ")", !r.loadErr, (r.out.match(/.*failed to load plugin.*/) || [""])[0]);
  const tail = r.out.split("\n").filter((l) => l.trim() && !/level=/.test(l)).slice(-4).join(" | ");
  ok(name + " [" + r.ms + "ms]", r.code === 0 && check(r), "rc=" + r.code + " :: " + tail);
}
console.log("model calls: " + calls + "  platform: " + process.platform + "/" + process.arch + (LITE ? "  mode: lite" : "  mode: full"));
console.log("RESULT " + pass + "/" + total);
server.close();
process.exit(pass === total ? 0 : 1);
