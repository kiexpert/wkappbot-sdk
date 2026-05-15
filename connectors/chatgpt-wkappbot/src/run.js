const { execFile } = require('child_process');

function runFile(command, args, timeoutMs) {
  return new Promise((resolve) => {
    const child = execFile(command, args, { windowsHide: true }, (error, stdout, stderr) => {
      resolve({
        ok: !error,
        code: error ? (error.code || 1) : 0,
        stdout,
        stderr
      });
    });

    if (timeoutMs > 0) {
      setTimeout(() => {
        try { child.kill(); } catch (_) {}
      }, timeoutMs);
    }
  });
}

module.exports = { runFile };
