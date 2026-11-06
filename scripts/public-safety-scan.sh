#!/usr/bin/env bash
# Public-safety scan: fail when a public directory leaks private markers,
# email addresses (bot no-reply addresses excepted) or the owner's home path.
# Usage: bash scripts/public-safety-scan.sh [dir]   (default: skills)
set -u
DIR="${1:-skills}"
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
MARKERS="$ROOT/.github/public-safety-markers.txt"
[ -d "$DIR" ] || { echo "public-safety-scan: no such dir: $DIR" >&2; exit 2; }
[ -f "$MARKERS" ] || { echo "public-safety-scan: missing $MARKERS" >&2; exit 2; }

PAT="$(grep -v '^[[:space:]]*#' "$MARKERS" | sed '/^[[:space:]]*$/d' | paste -sd'|')"
EMAIL='[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}'
NOREPLY='(no-?reply|users\.noreply\.github\.com)'
HOMEPATH='Users[/\\]+kiexp'

hits=0
while IFS= read -r -d '' f; do
  # Blank "id"/"app" values: they are stable identifiers, never masked.
  # One HIT per match: markers, home path, and every non-bot email address.
  while IFS= read -r m; do
    echo "HIT $f:$m"
    hits=$((hits+1))
  done < <(sed -E 's/("(id|app)"[[:space:]]*:[[:space:]]*")[^"]*"/\1"/g' "$f" \
           | grep -noiE -e "($PAT)" -e "$HOMEPATH" -e "$EMAIL" \
           | grep -viE "^[0-9]+:[^:]*$NOREPLY[^:]*\$")
done < <(find "$DIR" -type f -print0 | sort -z)

echo "PUBLIC-SAFETY HITS $hits"
[ "$hits" -eq 0 ]
