function getToken() {
  return process.env.WKAPPBOT_TOKEN || '';
}

function isAllowed(req) {
  const token = getToken();
  if (!token) return true;

  const value = req.headers.authorization || '';
  return value === `Bearer ${token}`;
}

module.exports = { isAllowed };
