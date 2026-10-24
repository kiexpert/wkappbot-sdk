#!/usr/bin/env bash
# Install the kih harness on this runner from ./kih (after clone-kih.sh), the way a user does:
# kih install.sh detects the OS and wires ~/.claude/settings.json hooks and the repo's core.hooksPath.
# WKAPPBOT_CORE (env) points the installer at the binary under test instead of downloading one.
# Linux/macOS need root for /opt and /usr/local/bin, so the installer runs under sudo with this HOME.
set -euo pipefail
[ -f kih/install.sh ] || { echo "::error::kih/install.sh missing (run clone-kih.sh first)"; exit 1; }
echo "kih install: $(git -C kih rev-parse --short HEAD), WKAPPBOT_CORE=${WKAPPBOT_CORE:-unset}"
case "$(uname -s)" in
  Linux|Darwin)
    sudo -E env "HOME=$HOME" "PATH=$PATH" "WKAPPBOT_CORE=${WKAPPBOT_CORE:-}" bash kih/install.sh
    sudo chown -R "$(id -u):$(id -g)" "$HOME/.claude" kih 2>/dev/null || true ;;
  *)
    bash kih/install.sh ;;
esac
echo "installed: $(ls -la "$HOME/.claude/settings.json" 2>/dev/null || echo 'no ~/.claude/settings.json')"
echo "kih core.hooksPath: $(git -C kih config --get core.hooksPath || echo unset)"
