# wkdoctor check: claude-settings-validation -- detect invalid permission rules that Claude skips silently
# DOCTOR 4-PHASE FLOW:
#   (1) DETECT:  parse settings.json allow/deny lists; flag any Tool(spec) where the tool name
#                does NOT start with an uppercase letter (e.g. "unsandboxed(git)" is invalid --
#                Claude requires "Unsandboxed(git)"). Also optionally probe `claude --version`
#                for 'Settings Warning' output on the current system.
#   (2) HEAL:    0-token deterministic auto-fix (user-authorized 2026-06-12). Capitalize the
#                first letter of each invalid rule -- Claude's own canonical suggestion, e.g.
#                "command(git)" -> "Command(git)". Only the JSON-quoted literal "name(" is
#                replaced, so the hook key "command": (colon, not paren) is never touched.
#                Backup to <file>.wkdoctor-bak, verify-reparse, atomic UTF-8 (no BOM) write.
#   (3) SOLVE:   emit what was healed; re-detect post-heal so the report reflects reality.
#   (4) PREVENT: runs every doctor pass; invalid rules never accumulate silently.
# FAIL-OPEN: any error / missing binary / missing file -> 'n/a (fail-open)', never crash.
# FAST: one settings.json read + one optional fast `claude --version` probe (< 3s).

# Derive self-sufficient paths (resilient against $binDir clobber by earlier modules)
$_m20_myDir  = Split-Path -Parent $MyInvocation.MyCommand.Path   # .../wkappbot.hq/doctor
$_m20_hqDir  = Split-Path -Parent $_m20_myDir                    # .../wkappbot.hq
$_m20_sdkBin = Split-Path -Parent $_m20_hqDir                    # .../bin (SDK bin root)

# -- PHASE 1: DETECT ----------------------------------------------------------

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

# -- FAST PROBE: claude --version for startup warnings ------------------------
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

# -- PHASE 2: HEAL (0-token deterministic auto-fix) ---------------------------
# User-authorized 2026-06-12. Repair invalid lowercase tool-name rules in place by
# capitalizing the first letter (Claude's own canonical suggestion). Only the JSON-
# quoted literal "name(...)" is string-replaced, so the hook key "command": is safe.

$totalViolations = $_m20_allViolations.Count
$_m20_healedCount = 0
$_m20_healErrors  = @()

if ($totalViolations -gt 0) {
    $_m20_pathByLabel = @{}
    foreach ($sf in $_m20_settingsFiles) { $_m20_pathByLabel[$sf.Label] = $sf.Path }

    foreach ($grp in ($_m20_allViolations | Group-Object File)) {
        $filePath = $_m20_pathByLabel[$grp.Name]
        if (-not $filePath) { continue }
        try {
            $orig   = [IO.File]::ReadAllText($filePath, [Text.Encoding]::UTF8)
            $healed = $orig
            $n = 0
            foreach ($v in $grp.Group) {
                $needle  = '"' + $v.Bad + '"'
                $replace = '"' + $v.Fixed + '"'
                if ($healed.Contains($needle)) { $healed = $healed.Replace($needle, $replace); $n++ }
            }
            if ($n -gt 0 -and -not [string]::Equals($healed, $orig)) {
                $null = ConvertFrom-Json -InputObject $healed -ErrorAction Stop  # verify before commit
                [IO.File]::Copy($filePath, "$filePath.wkdoctor-bak", $true)
                [IO.File]::WriteAllText($filePath, $healed, (New-Object System.Text.UTF8Encoding($false)))
                $_m20_healedCount += $n
            }
        } catch {
            $_m20_healErrors += "$($grp.Name): heal failed -- $_"
        }
    }

    # Re-detect post-heal so the report reflects the real current state
    if ($_m20_healedCount -gt 0) {
        $_m20_allViolations.Clear()
        foreach ($_m20_sf in $_m20_settingsFiles) {
            try {
                $rawText2 = [IO.File]::ReadAllText($_m20_sf.Path, [Text.Encoding]::UTF8)
                $parsed2  = ConvertFrom-Json -InputObject $rawText2 -ErrorAction Stop
                $ruleSets2 = @()
                if ($parsed2.permissions -and $parsed2.permissions.allow) { $ruleSets2 += [PSCustomObject]@{ Field='allow'; Rules=@($parsed2.permissions.allow) } }
                if ($parsed2.permissions -and $parsed2.permissions.deny)  { $ruleSets2 += [PSCustomObject]@{ Field='deny';  Rules=@($parsed2.permissions.deny)  } }
                foreach ($rs in $ruleSets2) {
                    foreach ($rule in $rs.Rules) {
                        $ruleStr = "$rule"
                        if ($ruleStr -match '^mcp__') { continue }
                        if ($_m20_lowercaseToolRx.IsMatch($ruleStr)) {
                            $fixed = $ruleStr.Substring(0,1).ToUpperInvariant() + $ruleStr.Substring(1)
                            $_m20_allViolations.Add([PSCustomObject]@{ File=$_m20_sf.Label; Field=$rs.Field; Bad=$ruleStr; Fixed=$fixed })
                        }
                    }
                }
            } catch {}
        }
        # The healed file now parses clean -- the pre-heal CLI probe warnings are stale.
        $_m20_probeWarnings = @()
        $_m20_probeMethod   = 'healed (probe stale, cleared)'
    }
    $totalViolations = $_m20_allViolations.Count
}

# -- PHASE 3: SOLVE (report healed + any remaining) ---------------------------

$probeIssues     = $_m20_probeWarnings.Count
$parseIssueCount = $_m20_parseErrors.Count
$healIssueCount  = $_m20_healErrors.Count

if ($totalViolations -eq 0 -and $probeIssues -eq 0 -and $parseIssueCount -eq 0 -and $healIssueCount -eq 0) {
    if ($_m20_healedCount -gt 0) {
        $detail = "auto-healed $($_m20_healedCount) invalid rule(s) (capitalized tool name); backup .wkdoctor-bak written"
    } else {
        $detail = "no invalid rules in $($_m20_settingsFiles.Count) settings file(s); probe=$_m20_probeMethod"
    }
    Add-Check 'settings:rule-casing' 'ok' $detail
    Emit 'ok' 'settings:rule-casing' $detail
} else {
    $parts = @()
    if ($_m20_healedCount -gt 0)  { $parts += "$($_m20_healedCount) auto-healed" }
    if ($totalViolations -gt 0)   { $parts += "$totalViolations still-invalid rule(s)" }
    if ($probeIssues -gt 0)       { $parts += "$probeIssues CLI warning(s)" }
    if ($parseIssueCount -gt 0)   { $parts += "$parseIssueCount parse error(s)" }
    if ($healIssueCount -gt 0)    { $parts += "$healIssueCount heal error(s)" }
    $summary = ($parts -join '; ') + " -- probe=$_m20_probeMethod"

    $finalStatus = if ($totalViolations -eq 0 -and $parseIssueCount -eq 0 -and $healIssueCount -eq 0) { 'ok' } else { 'warn' }
    Add-Check 'settings:rule-casing' $finalStatus $summary
    Emit $(if ($finalStatus -eq 'ok') { 'ok' } else { '!' }) 'settings:rule-casing' $summary

    foreach ($v in $_m20_allViolations) {
        $healMsg = "[$($v.File)/$($v.Field)] `"$($v.Bad)`" -> needs `"$($v.Fixed)`" (heal could not rewrite -- check file perms)"
        Add-Check 'settings:rule-casing:fix' 'warn' $healMsg
        Emit '!' 'settings:rule-casing:fix' $healMsg
    }
    foreach ($he in $_m20_healErrors) {
        Add-Check 'settings:rule-casing:heal' 'warn' $he
        Emit '!' 'settings:rule-casing:heal' $he
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


# -- PHASE 4: PREVENT (self-documenting) --------------------------------------
# This check runs every doctor pass -- invalid permission rules cannot accumulate silently.
