#!/usr/bin/env bash
# Find the private package-source repo (by TARGET_HASH, a sha256 prefix; never named) and a token (T1, T2, T3, tried
# in that order) that can reach it. Writes REPO, TOK, PAGES to $GITHUB_ENV; masks both. Moved verbatim from
# npm-publish-wkappbot-code.yml so its pack and publish jobs share one copy.
set -u
for t in "$T1" "$T2" "$T3"; do
  [ -n "$t" ] || continue
  names=$(GH_TOKEN="$t" gh repo list kiexpert --visibility private --limit 300 --json nameWithOwner -q '.[].nameWithOwner' 2>/dev/null || true)
  for n in $names; do
    if [ "$(printf %s "$n" | sha256sum | cut -c1-16)" = "$TARGET_HASH" ]; then
      echo "::add-mask::$n"
      echo "::add-mask::${n#*/}"
      echo "::add-mask::$t"
      { echo "REPO=$n"; echo "TOK=$t"; echo "PAGES=https://${n%%/*}.github.io/${n#*/}"; } >> "$GITHUB_ENV"
      echo "target found"
      exit 0
    fi
  done
done
echo "no token reaches the target repo"; exit 1
