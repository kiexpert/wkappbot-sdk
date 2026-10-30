// node bridge-selftest.mjs  -> PASS n/n. Pure functions only, no CLI and no network.
process.env.WEBSEAT_BRIDGE_NO_LISTEN = '1';
// a file URL next to this script works on Windows too, where a bare pathname would read /D:/...
const { buildPrompt, parseReply, cliArgv } = await import(new URL('./webseat-bridge.mjs', import.meta.url).href);
let pass = 0, total = 0;
const check = (name, ok, info) => { total++; if (ok) pass++; console.log((ok ? 'PASS ' : 'FAIL ') + name + (ok ? '' : ' :: ' + info)); };

const p = buildPrompt([
  { role: 'system', content: 'SYS-X' },
  { role: 'user', content: 'do it' },
  { role: 'assistant', content: '', tool_calls: [{ id: 'c1', type: 'function', function: { name: 'read_file', arguments: '{"path":"a"}' } }] },
  { role: 'tool', tool_call_id: 'c1', content: 'FILE-BODY' }
], [{ type: 'function', function: { name: 'write_file', description: 'write', parameters: { type: 'object' } } }]);
check('P1 system text kept', p.includes('SYS-X'), p.slice(0, 80));
check('P2 tool listed', p.includes('- write_file: write'), '');
check('P3 earlier call and result kept', p.includes('read_file') && p.includes('FILE-BODY'), '');
check('P4 reply format asked', p.includes('REPLY FORMAT'), '');

const r1 = parseReply('sure\n{"content":"ok","tool_calls":[{"name":"write_file","arguments":{"path":"h.js","content":"x"}}]}');
check('R1 tool call parsed', r1.tool_calls && r1.tool_calls[0].function.name === 'write_file', JSON.stringify(r1));
check('R2 arguments are a JSON string', r1.tool_calls && JSON.parse(r1.tool_calls[0].function.arguments).path === 'h.js', '');
const r2 = parseReply('{"content":"done","tool_calls":[]}');
check('R3 no calls -> plain message', r2.content === 'done' && !r2.tool_calls, JSON.stringify(r2));
const r3 = parseReply('just text, no json');
check('R4 non-JSON reply kept as text', r3.content === 'just text, no json', JSON.stringify(r3));

const [c1, a1] = cliArgv('claude', 'sonnet', '/x');
check('A1 claude keeps own tools and user hooks off', c1 === 'claude' && a1.includes('--restricted') && a1.includes('--tools') && a1.includes('sonnet'), a1.join(' '));
const [c2, a2] = cliArgv('codex', '', '/out');
check('A2 codex read-only, last message to file, stdin', c2 === 'codex' && a2.includes('read-only') && a2.includes('/out') && a2[a2.length - 1] === '-', a2.join(' '));
const [c3, a3] = cliArgv('gemini', '', '/x');
check('A3 gemini plan mode headless', c3 === 'gemini' && a3.includes('plan') && a3.includes('-p'), a3.join(' '));

console.log((pass === total ? 'PASS ' : 'FAIL ') + pass + '/' + total);
process.exit(pass === total ? 0 : 1);
