const { rebuildState } = require('./replay');

function loadBootstrapState() {
  const state = rebuildState();

  for (const job of state.jobs || []) {
    if (job.state === 'running') {
      job.state = 'queued';
      job.requeuedAfterRestart = true;
    }
  }

  return state;
}

module.exports = { loadBootstrapState };
