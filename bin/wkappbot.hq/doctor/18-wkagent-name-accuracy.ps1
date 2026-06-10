# wkdoctor check: wkagent-name accuracy -- model-id detection sanity
# DOCTOR 4-PHASE FLOW:
#   (1) DETECT: get AUTHORITATIVE model from the active session JSONL last assistant
#               message .message.model, OR from the active-session-<slug>.json harness
#               cache (both written by wkharness on every tool-call for this CWD).
#               Then call wkagent-name ONCE and capture its stdout.
#   (2) SAFE:   report only -- no blocking, no session edits.
#   (3) SOLVE:  on mismatch, point at the resolver file + the resolution order that
#               returned the wrong value (env override? chain walk? session fallback?).
#   (4) PREVENT: this check runs every session; model-detection regressions surface
#               immediately.
# FAIL-OPEN: wkagent-name absent, JSONL unreadable, or authoritative source unclear
#            => na/warn, never crash the doctor.
# SPEED: one wkagent-name invocation + tail-50 JSONL read. Total < 1 second.
#
# Resolver under test:
#   D:\GitHub\WKAppBot\bin\wkagent-name.ps1
#   Resolution order: (1) env WKHARNESS_AGENT_MODEL  (2) Win32 parent chain --model flag
#                     (3) active-session-<slug>.json fallback

# Derive self-sufficient SDK bin path (resilient against $binDir clobber by other modules)
$_m18_myDir  = Split-Path -Parent $MyInvocation.MyCommand.Path  # .../wkappbot.hq/doctor
$_m18_hqDir  = Split-Path -Parent $_m18_myDir                   # .../wkappbot.hq
$_m18_sdkBin = Split-Path -Parent $_m18_hqDir                   # .../bin (SDK bin root)

$CHECK_NAME    = 'wkagent-name:accuracy'
$RESOLVER_PS1  = 'D:\GitHub\WKAppBot\bin\wkagent-name.ps1'
$RESOLVER_CMD  = 'wkagent-name.cmd'

# ── Normaliser: map shorthand aliases to canonical model-id prefix ────────────────────────
# wkagent-name may emit a short alias (e.g. "haiku", "sonnet", "opus") while the
# authoritative source emits a full id (e.g. "claude-haiku-4-5-20251001").
# Normalise both sides to the lowercase FAMILY token ("haiku"/"sonnet"/"opus"/"codex"/"gemini")
# so that "claude-sonnet-4-6" and "sonnet" compare equal.
function Normalize-ModelId {
    param([string]$Id)
    if ([string]::IsNullOrWhiteSpace($Id)) { return '' }
    $id = $Id.Trim().ToLower()
    # Short aliases: return as-is (already family token)
    if ($id -in @('haiku','sonnet','opus','codex','gemini','root','unknown')) { return $id }
    # Full ids: extract family token from e.g. "claude-sonnet-4-6", "claude-haiku-4-5-20251001"
    if ($id -match 'haiku')  { return 'haiku' }
    if ($id -match 'sonnet') { return 'sonnet' }
    if ($id -match 'opus')   { return 'opus' }
    if ($id -match 'codex')  { return 'codex' }
    if ($id -match 'gemini') { return 'gemini' }
    # Fallback: return as-is (preserves unusual ids for comparison)
    return $id
}

# ── PHASE 1A: DETECT -- AUTHORITATIVE MODEL ───────────────────────────────────────────────
$authModel   = $null
$authSource  = 'none'

# Prefer $repoRoot (set by wkdoctor.ps1) over Get-Location (which is the bin dir when
# wkdoctor is invoked directly; $repoRoot is the user's actual project root).
$_m18_activeCwd = if ($repoRoot -and (Test-Path $repoRoot -PathType Container)) {
    $repoRoot
} else {
    (Get-Location).Path
}

# Primary: tail the active JSONL for this CWD and find the last assistant .message.model
try {
    $cwdSlug = $_m18_activeCwd -replace '[/\\:]', '-' -replace '^-|-$', ''
    $jsonlDir = Join-Path $env:USERPROFILE ".claude\projects\$cwdSlug"
    if (Test-Path $jsonlDir -PathType Container) {
        $newestJsonl = Get-ChildItem $jsonlDir -Filter '*.jsonl' -File -ErrorAction SilentlyContinue |
            Where-Object { $_.Name -match '^[0-9a-f-]{36}\.jsonl$' } |
            Sort-Object LastWriteTime -Descending |
            Select-Object -First 1
        if ($newestJsonl) {
            $tailLines = [System.IO.File]::ReadAllLines($newestJsonl.FullName, [System.Text.Encoding]::UTF8) |
                Select-Object -Last 100
            foreach ($line in $tailLines) {
                try {
                    $obj = $line | ConvertFrom-Json -ErrorAction Stop
                    if ($obj.type -eq 'assistant' -and $obj.message -and $obj.message.model) {
                        $authModel  = $obj.message.model
                        $authSource = "JSONL:$($newestJsonl.Name.Substring(0,8))"
                    }
                } catch {}
            }
        }
    }
} catch {}

# Secondary fallback: read active-session-<slug>.json harness cache (8-hour window)
if (-not $authModel) {
    try {
        $cwd     = $_m18_activeCwd
        $slug    = $cwd -replace '[/\\:]', '-' -replace '^-|-$', ''
        $asjPath = Join-Path $env:USERPROFILE ".claude\wkharness\active-session-$slug.json"
        if (Test-Path $asjPath) {
            $j = Get-Content $asjPath -Encoding UTF8 -Raw | ConvertFrom-Json
            if ($j.model -and $j.cwd -and
                (($j.cwd -replace '\\','/') -eq ($cwd -replace '\\','/'))) {
                $tsAge = try {
                    [DateTimeOffset]::Now - [DateTimeOffset]::Parse([string]$j.ts)
                } catch { $null }
                if (-not $tsAge -or $tsAge.TotalSeconds -le 28800) {
                    $authModel  = $j.model
                    $authSource = 'active-session-json'
                }
            }
        }
    } catch {}
}

# ── PHASE 1B: DETECT -- WKAGENT-NAME REPORTED MODEL ──────────────────────────────────────
$reportedModel  = $null
$toolAvailable  = $false
$toolExitCode   = -1

try {
    $cmdInfo = Get-Command $RESOLVER_CMD -ErrorAction SilentlyContinue
    if ($cmdInfo) {
        $toolAvailable = $true
        # Invoke via call operator (&) -- fastest, no subprocess overhead.
        # Suppress stderr (ancestor chain) with 2>$null; capture stdout only.
        $stdout = & $RESOLVER_CMD 2>$null
        $toolExitCode  = $LASTEXITCODE
        $reportedModel = ([string]$stdout).Trim()
    }
} catch {}

# ── PHASE 2: SAFE -- compute status, build display strings ───────────────────────────────
if (-not $toolAvailable) {
    $detail = "wkagent-name.cmd not found on PATH -- install WKAppBot bin or run setup.ps1"
    Add-Check $CHECK_NAME 'warn' $detail
    Emit '!' $CHECK_NAME $detail
    return
}

if (-not $authModel) {
    $detail = "authoritative source unreadable (no recent JSONL + no active-session json for CWD) -- reported: '$reportedModel' (unverifiable)"
    Add-Check $CHECK_NAME 'warn' $detail
    Emit '!' $CHECK_NAME $detail
    return
}

if ([string]::IsNullOrWhiteSpace($reportedModel)) {
    $detail = "wkagent-name emitted nothing (exit=$toolExitCode) -- authoritative: '$authModel' ($authSource). Resolver: $RESOLVER_PS1"
    Add-Check $CHECK_NAME 'warn' $detail
    Emit '!' $CHECK_NAME $detail
    return
}

$normAuth     = Normalize-ModelId $authModel
$normReported = Normalize-ModelId $reportedModel
$match        = ($normAuth -eq $normReported)

$detail = "reported='$reportedModel' (norm=$normReported)  auth='$authModel' (norm=$normAuth, src=$authSource)"

if ($match) {
    Add-Check $CHECK_NAME 'ok' "$detail -- MATCH"
    Emit 'ok' $CHECK_NAME "$detail -- MATCH"
} else {
    # ── PHASE 3: SOLVE -- point at the resolver ──────────────────────────────────────────
    $solveHint = "MISMATCH -- resolver $RESOLVER_PS1 returned wrong family. Check: (1) WKHARNESS_AGENT_MODEL env override? (2) Win32 parent chain captured a stale --model flag? (3) active-session fallback stale/wrong-CWD? Resolution order in wkharness-agent-name.ps1"
    $detail += " | $solveHint"
    Add-Check $CHECK_NAME 'warn' $detail
    Emit '!' $CHECK_NAME $detail
}

# ── PHASE 4: PREVENT -- self-documenting (this check runs every session) ─────────────────
# No additional guard needed; the doctor itself is the recurring prevention mechanism.
