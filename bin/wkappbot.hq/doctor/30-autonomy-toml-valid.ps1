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
    for ($i = 0; $i -lt $lines.Length; $i++) {
        if ($lines[$i] -match '^\s*\w+\s*=\s*"[^"]*\\[^"]*"') { $badPaths += $i }
        if ($lines[$i] -match '^\s*priority\s*=\s*(\d+)') {
            if ([int]$Matches[1] -gt 999) { $badPriority += $i }
        }
    }
    
    if ($badPaths.Count -eq 0 -and $badPriority.Count -eq 0) {
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
