#!/bin/sh
# common.sh -- shared checks for the harsh Linux legs (M22 alpine, M23 read-only).
# POSIX sh only: busybox ash, dash and bash all run it. Sourced, never executed.
# Results go to $QA_OUT/qa-result-$HG_LEG as key=value lines; the workflow uploads that file.
# The seven symbols are the list mode-qa-harsh.sh M17 greps for. Keep the two lists identical.

HG_SYMBOLS="normKo skill-reinforce-nudge web-result-skill-edit WK_QUEUED_GATE Get-WkQueuedInterimGate wkhippo_reflex_noise wkhippo_reflex_stop_refused"
HG_FAILS=0

hg_res() { # hg_res KEY VALUE -- append one key=value line and echo it
  mkdir -p "$QA_OUT" 2>/dev/null
  printf '%s=%s\n' "$1" "$2" >> "$QA_OUT/qa-result-$HG_LEG"
  printf 'RES %s=%s\n' "$1" "$2"
}

hg_check() { # hg_check KEY RC -- RC 0 is a pass; anything else is a fail and counts
  if [ "$2" = 0 ]; then hg_res "$1" pass; else hg_res "$1" fail; HG_FAILS=$((HG_FAILS + 1)); fi
}

hg_symbols() { # sets HG_PRESENT and HG_MISSING in the installed harness; rc 0 when all seven are present
  HG_PRESENT=0; HG_MISSING=""
  for s in $HG_SYMBOLS; do
    if grep -rqF -- "$s" "$HOME/.wk-harness" 2>/dev/null; then
      HG_PRESENT=$((HG_PRESENT + 1))
    else
      HG_MISSING="$HG_MISSING $s"
    fi
  done
  [ "$HG_PRESENT" = 7 ]
}

hg_help() { # wkappbot --help and wkhippo --help must both exit 0
  wkappbot --help >/dev/null 2>&1; a=$?
  wkhippo --help >/dev/null 2>&1; b=$?
  hg_res help_wkappbot_rc "$a"
  hg_res help_wkhippo_rc "$b"
  [ "$a" = 0 ] && [ "$b" = 0 ]
}

hg_stop_cmd() { # prints the Stop command that ~/.claude/settings.json names for the harness
  node -e 'const fs=require("fs");let j={};try{j=JSON.parse(fs.readFileSync(process.argv[1],"utf8"))}catch(e){process.exit(3)}for(const m of (j.hooks&&j.hooks.Stop)||[])for(const h of m.hooks||[])if(/wkharness/.test(h.command||"")){console.log(h.command);process.exit(0)}process.exit(4)' "$HOME/.claude/settings.json"
}

hg_stop() { # one Stop event piped into the installed Stop hook: rc 0, or rc 2 with text (the documented refusal)
  cmd=$(hg_stop_cmd 2>/dev/null)
  if [ -z "$cmd" ]; then hg_res stop_hook_found no; hg_check stop_roundtrip 1; return 1; fi
  hg_res stop_hook_found yes
  hg_res stop_hook_cmd "$(printf '%s' "$cmd" | cut -c1-160)"
  out=$(printf '%s' '{"hook_event_name":"Stop","session_id":"qa-harsh","cwd":"/tmp","stop_hook_active":false,"last_assistant_message":"qa ping"}' | sh -c "$cmd" 2>&1)
  rc=$?
  hg_res stop_hook_rc "$rc"
  if [ -n "$out" ]; then hg_res stop_hook_text yes; else hg_res stop_hook_text no; fi
  if [ "$rc" = 0 ]; then ok=0
  elif [ "$rc" = 2 ] && [ -n "$out" ]; then ok=0
  else ok=1
  fi
  hg_check stop_roundtrip "$ok"
}

hg_tail_checks() { # the three checks every leg ends with: seven symbols, both --help, one Stop round trip
  hg_symbols; s=$?
  hg_res symbols_present "$HG_PRESENT"
  hg_res symbols_missing "${HG_MISSING:- none}"
  hg_check symbols_7 "$s"
  hg_help; h=$?
  hg_check help_both "$h"
  hg_stop
}

hg_finish() { # verdict line; returns the fail count
  if [ "$HG_FAILS" = 0 ]; then hg_res verdict PASS; else hg_res verdict FAIL; fi
  echo "HARSH-LEG $HG_LEG fails=$HG_FAILS"
  return "$HG_FAILS"
}
