#!/usr/bin/env bash
# Release-gate helper for the os-qa respawn road (see .github/workflows/wkappbot-code-os-qa.yml, RESPAWN ON A BAD
# NETWORK). A leg whose runner had a dead network writes e2e=respawned and starts a fresh run of itself, titled
# "... leg=<leg> respawn=<n> parent=<first run id>". For every such leg this waits for the NEWEST respawn of that leg
# and parent to finish, then puts its qa-result in place of the respawned one. If no respawn finishes in WAIT_MIN
# minutes, or the newest respawn is itself still a respawn, the respawned result stays and scripts/npm-release-gate.sh
# refuses that leg (e2e is not success) -- a network blip is never counted as a pass.
# Env: QA_DIR (default qa), WAIT_MIN (default 45), GH_TOKEN, GITHUB_REPOSITORY.
set -u
QA_DIR="${QA_DIR:-qa}"; WAIT_MIN="${WAIT_MIN:-45}"
for f in "$QA_DIR"/*.txt; do
  [ -f "$f" ] || continue
  grep -qx 'e2e=respawned' "$f" || continue
  leg="$(sed -n 's/^leg=//p' "$f" | head -1)"; parent="$(sed -n 's/^parent=//p' "$f" | head -1)"
  echo "RESPAWN $leg: the first attempt hit a dead network; waiting for its respawn (parent $parent)"
  deadline=$(( $(date +%s) + WAIT_MIN * 60 )); last=""
  while :; do
    row="$(gh run list -R "$GITHUB_REPOSITORY" -w wkappbot-code-os-qa.yml -e workflow_dispatch -L 100 \
      --json databaseId,displayTitle,status \
      --jq "[.[] | select(.displayTitle | test(\" leg=$leg respawn=[0-9]+ parent=$parent\$\"))] | sort_by(.databaseId) | last | select(. != null) | \"\(.databaseId) \(.status)\"" 2>/dev/null)"
    id="${row%% *}"; st="${row#* }"
    if [ -n "$row" ] && [ "$st" = completed ] && [ "$id" != "$last" ]; then
      last="$id"; t="$(mktemp -d)"
      if gh run download "$id" -R "$GITHUB_REPOSITORY" -n "qa-result-$leg" -D "$t" >/dev/null 2>&1 && [ -f "$t/$leg.txt" ]; then
        if grep -qx 'e2e=respawned' "$t/$leg.txt"; then
          echo "RESPAWN $leg: run $id respawned again; waiting for the next one"
        else
          cp "$t/$leg.txt" "$f"; echo "RESPAWN $leg: using the result of respawn run $id"; sed 's/^/RESPAWN   /' "$f"; break
        fi
      else
        echo "RESPAWN $leg: run $id finished without a qa-result-$leg artifact"
      fi
    fi
    if [ "$(date +%s)" -ge "$deadline" ]; then echo "RESPAWN $leg: no finished respawn within $WAIT_MIN min -- the gate refuses this leg"; break; fi
    sleep 30
  done
done
exit 0
