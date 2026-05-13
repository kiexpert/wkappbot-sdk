function getConfig() {
  return {
    port: Number(process.env.PORT || 8787),
    relayUrl: process.env.WK_RELAY || 'http://127.0.0.1:8787',
    bin: process.env.WKAPPBOT_BIN || 'wkappbot',
    dataDir: process.env.WK_DATA_DIR || '.wk-data',
    agentId: process.env.WK_AGENT_ID || 'default',
    timeoutMs: Number(process.env.WK_TIMEOUT_MS || 120000)
  };
}

module.exports = { getConfig };
