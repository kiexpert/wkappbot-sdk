function validateExecute(input) {
  if (!input || typeof input !== 'object') return 'input_must_be_object';
  if (!Array.isArray(input.args)) return 'args_must_be_array';
  if (input.args.length === 0) return 'args_required';
  if (input.args.length > 32) return 'too_many_args';
  for (const arg of input.args) {
    if (typeof arg !== 'string') return 'arg_must_be_string';
    if (arg.length > 4096) return 'arg_too_long';
  }
  return null;
}

function validateComplete(input) {
  if (!input || typeof input !== 'object') return 'input_must_be_object';
  if (!input.id || typeof input.id !== 'string') return 'id_required';
  return null;
}

function validateHeartbeat(input) {
  if (!input || typeof input !== 'object') return 'input_must_be_object';
  if (input.agentId && typeof input.agentId !== 'string') return 'agent_id_must_be_string';
  return null;
}

module.exports = { validateExecute, validateComplete, validateHeartbeat };
