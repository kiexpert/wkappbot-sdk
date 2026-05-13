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

module.exports = { appendEvent };
