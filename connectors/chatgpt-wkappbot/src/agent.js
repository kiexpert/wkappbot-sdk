const http = require('http');
const { isAllowedArgs } = require('./allowlist');
const { getConfig } = require('./config');
const { runFile } = require('./run');

const config = getConfig();
const token = process.env.WKAPPBOT_TOKEN || '';

function post(path, body) {
  return new Promise((resolve, reject) => {
    const data = JSON.stringify(body || {});
    const headers = {
      'content-type': 'application/json',
      'content-length': Buffer.byteLength(data)
    };

    if (token) {
      headers.authorization = `Bearer ${token}`;
    }

    const req = http.request(config.relayUrl + path, {
      method: 'POST',
      headers
    }, res => {
      let raw = '';
      res.on('data', chunk => raw += chunk);
      res.on('end', () => {
        try {
          resolve(JSON.parse(raw || '{}'));
        } catch (err) {
          reject(err);
        }
      });
    });

    req.on('error', reject);
    req.write(data);
    req.end();
  });
}

async function loop() {
  await post('/heartbeat', {
    agentId: config.agentId,
    meta: { bin: config.bin }
  });

  const polled = await post('/poll', {
    agentId: config.agentId
  });

  if (!polled.job) {
    return setTimeout(loop, 1000);
  }

  const input = polled.job.input || {};
  const args = Array.isArray(input.args) ? input.args : ['--version'];

  if (!isAllowedArgs(args)) {
    await post('/complete', {
      id: polled.job.id,
      result: {
        ok: false,
        code: 403,
        stderr: 'command_not_allowed'
      }
    });

    return setTimeout(loop, 10);
  }

  const result = await runFile(config.bin, args, config.timeoutMs);

  await post('/complete', {
    id: polled.job.id,
    result
  });

  setTimeout(loop, 10);
}

loop().catch((err) => {
  console.error(err);
  process.exit(1);
});
