const fs = require('fs');
const path = require('path');

const dir = process.env.WK_DATA_DIR || '.wk-data';

function file(name) {
  return path.join(dir, name + '.json');
}

function load(name, fallback) {
  try {
    return JSON.parse(fs.readFileSync(file(name), 'utf8'));
  } catch (_) {
    return fallback;
  }
}

function save(name, value) {
  fs.mkdirSync(dir, { recursive: true });
  fs.writeFileSync(file(name), JSON.stringify(value, null, 2));
}

module.exports = { load, save };
