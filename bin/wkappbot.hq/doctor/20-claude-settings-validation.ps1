# wkdoctor check: claude-settings-validation -- detect invalid permission rules that Claude skips silently
# DOCTOR 4-PHASE FLOW:
#   (1) DETECT:  parse settings.json allow/deny lists; flag any Tool(spec) where the tool name
#                does NOT start with an uppercase letter (e.g. "unsandboxed(git)" is invalid --
#                Claude requires "Unsandboxed(git)"). Also optionally probe `claude --version`
#                for 'Settings Warning' output on the current system.
#   (2) SAFE:    report only -- do NOT auto-modify settings.json (a separate higher-risk feature).
#   (3) SOLVE:   emit the exact corrected rule(s) so the user can fix with one copy-paste.
#   (4) PREVENT: runs every doctor pass; invalid rules never accumulate silently.
# FAIL-OPEN: any error / missing binary / missing file -> 'n/a (fail-open)', never crash.
# FAST: one settings.json read + one optional fast `claude --version` probe (< 3s).

# Derive self-sufficient paths (resilient against $binDir clobber by earlier modules)
$_m20_myDir  = Split-Path -Parent $MyInvocation.MyCommand.Path   # .../wkappbot.hq/doctor
$_m20_hqDir  = Split-Path -Parent $_m20_myDir                    # .../wkappbot.hq
$_m20_sdkBin = Split-Path -Parent $_m20_hqDir                    # .../bin (SDK bin root)

# ── PHASE 1: DETECT ──────────────────────────────────────────────────────────

# Settings files to scan: global user + project-level (if present)
$_m20_settingsFiles = @()
$_m20_globalSettings = Join-Path $env:USERPROFILE '.claude\settings.json'
if (Test-Path $_m20_globalSettings -PathType Leaf) {
    $_m20_settingsFiles += [PSCustomObject]@{ Path = $_m20_globalSettings; Label = 'global' }
}
# Project-level: look for .claude/settings.json relative to the repo root
$_m20_projectSettings = $null
try {
    $_m20_projectSettings = Join-Path (Split-Path $_m20_sdkBin -Parent) '.claude\settings.json'
} catch {}
if ($_m20_projectSettings -and (Test-Path $_m20_projectSettings -PathType Leaf)) {
    $_m20_settingsFiles += [PSCustomObject]@{ Path = $_m20_projectSettings; Label = 'project' }
}

# Regex: a permission rule that has a Tool name part -- anything before the first '('
# A valid tool name must start with uppercase A-Z or be an MCP-pattern (mcp__...)
# We flag rules that match  ^[a-z][^\(]*\(  (starts lowercase, has opening paren)
$_m20_lowercaseToolRx = [regex]'^[a-z][^\(]*\('

$_m20_allViolations = [System.Collections.Generic.List[PSCustomObject]]::new()
$_m20_parseErrors   = @()

foreach ($_m20_sf in $_m20_settingsFiles) {
    try {
        $rawText = [IO.File]::ReadAllText($_m20_sf.Path, [Text.Encoding]::UTF8)
        $parsed  = $null
        try {
            $parsed = ConvertFrom-Json -InputObject $rawText -ErrorAction Stop
        } catch {
            $_m20_parseErrors += "$($_m20_sf.Label): JSON parse error -- $_"
            continue
        }

        $ruleSets = @()
        if ($parsed.permissions -and $parsed.permissions.allow) {
            $ruleSets += [PSCustomObject]@{ Field = 'allow'; Rules = @($parsed.permissions.allow) }
        }
        if ($parsed.permissions -and $parsed.permissions.deny) {
            $ruleSets += [PSCustomObject]@{ Field = 'deny';  Rules = @($parsed.permissions.deny) }
        }

        foreach ($rs in $ruleSets) {
            foreach ($rule in $rs.Rules) {
                $ruleStr = "$rule"
                # Skip mcp__ prefixes -- they are lowercase by convention and not a tool name
                if ($ruleStr -match '^mcp__') { continue }
                # Flag: starts with a lowercase letter and contains '(' (looks like a Tool(spec))
                if ($_m20_lowercaseToolRx.IsMatch($ruleStr)) {
                    # Derive the corrected version: uppercase the first letter only
                    $fixed = $ruleStr.Substring(0, 1).ToUpperInvariant() + $ruleStr.Substring(1)
                    $_m20_allViolations.Add([PSCustomObject]@{
                        File    = $_m20_sf.Label
                        Field   = $rs.Field
                        Bad     = $ruleStr
                        Fixed   = $fixed
                    })
                }
            }
        }
    } catch {
        $_m20_parseErrors += "$($_m20_sf.Label): read error -- $_"
    }
}

# ── FAST PROBE: claude --version for startup warnings ────────────────────────
# `claude --version` is near-instant and emits 'Settings Warning' lines to stderr
# when Claude detects invalid rules. We capture combined output and grep for them.
$_m20_probeWarnings = @()
$_m20_probeMethod   = 'n/a (fail-open)'
try {
    $claudeExe = (Get-Command 'claude' -ErrorAction SilentlyContinue)
    if ($claudeExe) {
        $probeOut = & claude --version 2>&1 | Out-String
        $_m20_probeMethod = 'cli-probe(--version)'
        # Extract any Settings Warning lines
        $probeLines = $probeOut -split '\r?\n'
        foreach ($pl in $probeLines) {
            $pt = $pl.Trim()
            if ($pt -match 'Settings\s+Warning|Invalid\s+permission\s+rule|was\s+skipped') {
                $_m20_probeWarnings += $pt
            }
        }
    }
} catch {
    $_m20_probeMethod = 'n/a (fail-open)'
}

# ── PHASE 2 + 3: SAFE report + SOLVE ─────────────────────────────────────────

$totalViolations = $_m20_allViolations.Count
$probeIssues     = $_m20_probeWarnings.Count
$parseIssueCount = $_m20_parseErrors.Count

if ($totalViolations -eq 0 -and $probeIssues -eq 0 -and $parseIssueCount -eq 0) {
    $detail = "no invalid rules in $($_m20_settingsFiles.Count) settings file(s); probe=$_m20_probeMethod"
    Add-Check 'settings:rule-casing' 'ok' $detail
    Emit 'ok' 'settings:rule-casing' $detail
} else {
    $parts = @()
    if ($totalViolations -gt 0) { $parts += "$totalViolations invalid rule(s)" }
    if ($probeIssues -gt 0)     { $parts += "$probeIssues CLI warning(s)" }
    if ($parseIssueCount -gt 0) { $parts += "$parseIssueCount parse error(s)" }
    $summary = ($parts -join '; ') + " -- probe=$_m20_probeMethod"

    Add-Check 'settings:rule-casing' 'warn' $summary
    Emit '!' 'settings:rule-casing' $summary

    # SOLVE: emit each bad rule + its fix
    foreach ($v in $_m20_allViolations) {
        $healMsg = "[$($v.File)/$($v.Field)] `"$($v.Bad)`" -> fix: `"$($v.Fixed)`" (tool name must start uppercase)"
        Add-Check 'settings:rule-casing:fix' 'warn' $healMsg
        Emit '!' 'settings:rule-casing:fix' $healMsg
    }
    foreach ($pw in $_m20_probeWarnings) {
        $short = if ($pw.Length -gt 120) { $pw.Substring(0, 120) + '...' } else { $pw }
        Add-Check 'settings:rule-casing:cli' 'warn' $short
        Emit '!' 'settings:rule-casing:cli' $short
    }
    foreach ($pe in $_m20_parseErrors) {
        Add-Check 'settings:rule-casing:parse' 'warn' $pe
        Emit '!' 'settings:rule-casing:parse' $pe
    }
}

# ── PHASE 4: PREVENT (self-documenting) ──────────────────────────────────────
# This check runs every doctor pass -- invalid permission rules cannot accumulate silently.
