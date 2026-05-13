const http = require('http');
const { execFile } = require('child_process');

const relay = process.env.WK_RELAY || 'http://127.0.0.1:8787';
const bin = process.env.WKAPPBOT_BIN || 'wkappbot';

function post(path, body) {
  return new Promise((resolve, reject) => {
    const data = JSON.stringify(body || {});
    const req = http.request(relay + path, {
      method: 'POST',
      headers: {
        'content-type': 'application/json',
        'content-length': Buffer.byteLength(data)
      }
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

function run(command, args) {
  return new Promise((resolve) => {
    execFile(command, args, { windowsHide: true }, (error, stdout, stderr) => {
      resolve({
        ok: !error,
        code: error ? (error.code || 1) : 0,
        stdout,
        stderr
      });
    });
  });
}

async function loop() {
  const polled = await post('/poll');

  if (!polled.job) {
    return setTimeout(loop, 1000);
  }

  const input = polled.job.input || {};
  const args = Array.isArray(input.args) ? input.args : ['--version'];

  const result = await run(bin, args);

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
