const { execFile } = require('child_process');

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

async function main() {
  const result = await run('wkappbot', ['--version']);
  console.log(JSON.stringify(result, null, 2));
}

main().catch((err) => {
  console.error(err);
  process.exit(1);
});
