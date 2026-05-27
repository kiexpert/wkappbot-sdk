#!/usr/bin/env bash
# wksed.sh -- sed-compatible wrapper; intercepts -i for wkappbot file edit
# If no -i flag: passes through to real sed unchanged (read-only = safe).
# If -i flag + s/// expression: routes to wkappbot file edit (UTF-8 safe).
# If -i flag + unknown/complex expression: falls back to real sed.
# Usage: exactly like sed -- wksed.sh [sed-options] [expressions] files...

SED_INPLACE=0
SED_INPLACE_SUFFIX=""
EXPRESSIONS=()
FILES=()
PASSTHROUGH_ARGS=()

# Parse arguments (sed-compatible)
while [[ $# -gt 0 ]]; do
    case "$1" in
        -i)
            SED_INPLACE=1
            PASSTHROUGH_ARGS+=("-i")
            shift
            # Optional suffix: -i .bak or -i.bak
            if [[ $# -gt 0 && "$1" != -* && "$1" != */* ]]; then
                SED_INPLACE_SUFFIX="$1"
                PASSTHROUGH_ARGS+=("$1")
                shift
            fi
            ;;
        -i*)
            SED_INPLACE=1
            SED_INPLACE_SUFFIX="${1#-i}"
            PASSTHROUGH_ARGS+=("$1")
            shift
            ;;
        -e)
            EXPRESSIONS+=("$2")
            PASSTHROUGH_ARGS+=("-e" "$2")
            shift 2
            ;;
        -e*)
            EXPRESSIONS+=("${1#-e}")
            PASSTHROUGH_ARGS+=("$1")
            shift
            ;;
        -n|-r|-E|-z)
            PASSTHROUGH_ARGS+=("$1")
            shift
            ;;
        -f)
            # Script file: too complex, will use real sed
            PASSTHROUGH_ARGS+=("-f" "$2")
            shift 2
            ;;
        --)
            shift
            FILES+=("$@")
            break
            ;;
        -*)
            PASSTHROUGH_ARGS+=("$1")
            shift
            ;;
        *)
            if [[ ${#EXPRESSIONS[@]} -eq 0 && ${#FILES[@]} -eq 0 ]]; then
                EXPRESSIONS+=("$1")   # first non-flag non-file = expression
            else
                FILES+=("$1")
            fi
            shift
            ;;
    esac
done

# No -i: pure read-only, pass straight to real sed
if [[ "$SED_INPLACE" -eq 0 ]]; then
    exec sed "${PASSTHROUGH_ARGS[@]}" "${FILES[@]}"
fi

# -i with backup suffix: real sed is fine (creates backup = no corruption risk)
if [[ -n "$SED_INPLACE_SUFFIX" ]]; then
    exec sed "${PASSTHROUGH_ARGS[@]}" "${FILES[@]}"
fi

# -i without suffix + s/// only: try wkappbot file edit
_PY=$(powershell -NoProfile -NonInteractive -Command '$env:PYTHON_CMD' 2>/dev/null | tr -d $'\r\n')
[[ -z "$_PY" ]] && _PY="python3"

_wkappbot_edit() {
    local OLD="$1" NEW="$2" FILE="$3" ALL="$4"
    local TO TN
    TO=$(mktemp); TN=$(mktemp)
    printf "%s" "$OLD" > "$TO"
    printf "%s" "$NEW" > "$TN"
    if [[ "$ALL" == 1 ]]; then
        wkappbot file edit --old-file "$TO" --new-file "$TN" "$FILE" --replace-all
    else
        wkappbot file edit --old-file "$TO" --new-file "$TN" "$FILE"
    fi
    local RC=$?; rm -f "$TO" "$TN"; return $RC
}

ALL_SIMPLE=1
for EXPR in "${EXPRESSIONS[@]}"; do
    # Only simple s/old/new/[g] without -n, -r etc counts as "simple"
    if [[ ! "$EXPR" =~ ^s(.)(.+)\1(.*)\1([g]?)$ ]]; then
        ALL_SIMPLE=0; break
    fi
done

if [[ "$ALL_SIMPLE" -eq 0 ]]; then
    echo "[wksed] complex expression: falling back to real sed" >&2
    exec sed "${PASSTHROUGH_ARGS[@]}" "${FILES[@]}"
fi

# Route each s/// to wkappbot file edit per file
for FILE in "${FILES[@]}"; do
    [[ ! -f "$FILE" ]] && { echo "[wksed] WARNING: $FILE not found" >&2; continue; }
    for EXPR in "${EXPRESSIONS[@]}"; do
        [[ "$EXPR" =~ ^s(.)(.+)\1(.*)\1([g]?)$ ]]
        OLD="${BASH_REMATCH[2]}"; NEW="${BASH_REMATCH[3]}"; FLAGS="${BASH_REMATCH[4]}"
        G=0; [[ "$FLAGS" == *g* ]] && G=1
        _wkappbot_edit "$OLD" "$NEW" "$FILE" "$G" || {
            echo "[wksed] wkappbot failed: real sed fallback" >&2
            sed -i -e "$EXPR" "$FILE"
        }
    done
done