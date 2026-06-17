# wkdoctor check: autonomy-toml -- detect invalid TOML in ~/.gemini/policies/autonomy.toml.
# Fixes:
#  - convert double-quoted path values to single-quoted TOML literals (unescaped backslash)
#  - enforce priority <= 999 to prevent tier overflow
# FAIL-OPEN.

try {
    $tomlPath = Join-Path $env:USERPROFILE '.gemini\policies\autonomy.toml'
    if (-not (Test-Path -LiteralPath $tomlPath)) {
        Add-Check 'autonomy-toml' 'ok' 'n/a (autonomy.toml not present)'
        Emit 'ok' 'autonomy-toml' 'n/a (autonomy.toml not present)'
        return
    }
    $lines = [IO.File]::ReadAllLines($tomlPath, [Text.Encoding]::UTF8)
    $badPaths = @()
    $badPriority = @()
    $badTool = @()      # invalid gemini tool names (write_to_file -> write_file) -> "Unrecognized tool name"
    for ($i = 0; $i -lt $lines.Length; $i++) {
        if ($lines[$i] -match '^\s*\w+\s*=\s*"[^"]*\\[^"]*"') { $badPaths += $i }
        if ($lines[$i] -match '^\s*priority\s*=\s*(\d+)') {
            if ([int]$Matches[1] -gt 999) { $badPriority += $i }
        }
        if ($lines[$i] -match '^\s*toolName\s*=\s*"write_to_file"') { $badTool += $i }
    }
    # duplicate-rule detection by FULL [[rule]] block content (commandPrefix/target/commandRegex
    # INCLUDED), CONSISTENT with the auto-fix block-dedup below. A coarse toolName|decision|priority
    # signature falsely flagged rules that differ ONLY by commandPrefix (e.g. the 4 knowledge-op
    # allow rules all share run_shell_command|allow|100) -> a perpetual false [x] fail the block-dedup
    # could never resolve. Count only TRUE content-duplicate blocks.
    $allBlocks = [regex]::Split((($lines -join "`n")), '(?m)(?=^\s*\[\[rule\]\])') | Where-Object { $_ -match '\[\[rule\]\]' }
    $seenB = @{}; $dupRules = 0
    foreach ($b in $allBlocks) { $k = ($b -replace '\s+', ' ').Trim(); if ($seenB.ContainsKey($k)) { $dupRules++ } else { $seenB[$k] = $true } }

    if ($badTool.Count -gt 0 -or $dupRules -gt 0) {
        # surface as a FAIL (user 2026-06-17: a false 'valid' OK while gemini warns is unacceptable),
        # then auto-fix: write_to_file->write_file + dedup identical [[rule]] blocks.
        Add-Check 'autonomy-toml' 'fail' "INVALID policy: $($badTool.Count) invalid tool-name(s) (write_to_file -> write_file) + $dupRules duplicate rule(s)"
        Emit 'fail' 'autonomy-toml' "INVALID: $($badTool.Count) bad tool-name(s) + $dupRules dup rule(s) -- gemini emits 'Unrecognized tool name'. Fixing..."
        try {
            $txt = ($lines -join "`n") -replace 'toolName\s*=\s*"write_to_file"', 'toolName = "write_file"'
            $blocks = [regex]::Split($txt, '(?m)(?=^\s*\[\[rule\]\])') | Where-Object { $_.Trim() }
            $seen = @{}; $keep = @()
            foreach ($b in $blocks) { $k = ($b -replace '\s+', ' ').Trim(); if (-not $seen.ContainsKey($k)) { $seen[$k] = $true; $keep += $b.TrimEnd() } }
            [IO.File]::WriteAllText($tomlPath, (($keep -join "`n") + "`n"), (New-Object System.Text.UTF8Encoding($false)))
            Emit 'ok' 'autonomy-toml:fixed' "fixed: write_to_file->write_file + deduped to $($keep.Count) unique rule(s)"
        } catch { Emit '!' 'autonomy-toml:fix' "fix failed (fail-open): $_" }
    }
    elseif ($badPaths.Count -eq 0 -and $badPriority.Count -eq 0) {
        Add-Check 'autonomy-toml' 'ok' 'autonomy.toml valid'
        Emit 'ok' 'autonomy-toml' 'autonomy.toml valid (paths and priorities OK)'    
    } else {
        Emit '!' 'autonomy-toml' "found $($badPaths.Count) bad paths and $($badPriority.Count) high priorities -- fixing..."
        try {
            $fixed = $lines | ForEach-Object {
                $l = $_
                # Fix paths
                if ($l -match '^(\s*\w+\s*=\s*)"([^"]*\\[^"]*)"(.*)$') {
                    $l = "$($Matches[1])'$($Matches[2])'$($Matches[3])"
                }
                # Fix priorities
                if ($l -match '^(\s*priority\s*=\s*)(\d+)(.*)$') {
                    if ([int]$Matches[2] -gt 999) {
                        $l = "$($Matches[1])999$($Matches[3])"
                    }
                }
                $l
            }
            [IO.File]::WriteAllText($tomlPath, ($fixed -join "`n") + "`n", (New-Object System.Text.UTF8Encoding($false)))
            Add-Check 'autonomy-toml' 'ok' "fixed $($badPaths.Count) paths and $($badPriority.Count) priorities"
            Emit 'ok' 'autonomy-toml:fixed' "fixed autonomy.toml: paths -> literals, priorities -> capped at 999"
        } catch {
            Emit '!' 'autonomy-toml:fix' "fix failed (fail-open): $_"
            Add-Check 'autonomy-toml' 'warn' "autonomy.toml invalid but fix failed: $_"
        }
    }
} catch {
    Add-Check 'autonomy-toml' 'ok' "n/a (check error, fail-open): $_"
    Emit 'ok' 'autonomy-toml' 'n/a (check error, fail-open)'
}
