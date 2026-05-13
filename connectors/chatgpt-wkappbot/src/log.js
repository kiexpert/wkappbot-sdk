const fs = require('fs');
const path = require('path');

const dataDir = process.env.WK_DATA_DIR || path.join(process.cwd(), '.wk-data');
const logPath = path.join(dataDir, 'events.jsonl');

function appendEvent(type, payload) {
  try {
    fs.mkdirSync(dataDir, { recursive: true });
    const row = JSON.stringify({
      time: new Date().toISOString(),
      type,
      payload
    });
    fs.appendFileSync(logPath, row + '\n');
  } catch (_) {
  }
}

function readEvents() {
  try {
    if (!fs.existsSync(logPath)) return [];

    return fs.readFileSync(logPath, 'utf8')
      .split(/\r?\n/)
      .filter(Boolean)
      .map(line => {
        try { return JSON.parse(line); } catch (_) { return null; }
      })
      .filter(Boolean);
  } catch (_) {
    return [];
  }
}

module.exports = { appendEvent, readEvents };
