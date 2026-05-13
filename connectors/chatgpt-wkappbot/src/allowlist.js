const allowed = new Set([
  '--version',
  'windows',
  'skill',
  'a11y',
  'ask'
]);

function isAllowedArgs(args) {
  if (!Array.isArray(args) || args.length === 0) {
    return false;
  }

  return allowed.has(String(args[0] || ''));
}

module.exports = { isAllowedArgs };
