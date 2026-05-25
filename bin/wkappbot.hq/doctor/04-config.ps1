# wkdoctor check: configuration -- nightly guard, PATH, config.json

# Check 7: Nightly guard
$gSettings = Join-Path $env:USERPROFILE '.claude\settings.json'
if (Test-Path $gSettings -PathType Leaf) {
    $raw = Get-Content $gSettings -Encoding UTF8 -Raw
    if ($raw -match 'nightly-schedule-guard') {
        Add-Check 'Nightly guard' 'ok' 'registered'
        Emit 'ok' 'Nightly guard' '~/.claude/settings.json'
    } else {
        $hookPath = Join-Path (Split-Path $binDir -Parent) '.claude\nightly-schedule-guard.ps1'
        Add-Check 'Nightly guard' 'warn' 'hook not in settings.json'
        Emit '!' 'Nightly guard' "add PreToolUse hook for: $hookPath"
    }
} else {
    Add-Check 'Nightly guard' 'warn' 'settings.json not found'
    Emit '!' 'Nightly guard' 'settings.json missing'
}

# Check 8: bin/ in PATH
$binAbs = (Resolve-Path $binDir -ErrorAction SilentlyContinue)
$inPath = $false
if ($binAbs) {
    foreach ($d in ($env:PATH -split ';' | Where-Object { $_ })) {
        $r = Resolve-Path $d -ErrorAction SilentlyContinue
        if ($r -and $r.Path -eq $binAbs.Path) { $inPath = $true; break }
    }
}
if ($inPath) {
    Add-Check 'PATH (bin/)' 'ok' 'in PATH'
    Emit 'ok' 'PATH (bin/)' $binDir
} else {
    Add-Check 'PATH (bin/)' 'warn' 'not in PATH'
    Emit '!' 'PATH (bin/)' 'adding to user PATH...'
    try {
        $cur = [Environment]::GetEnvironmentVariable('PATH', 'User')
        if ($cur -notlike "*$binDir*") {
            [Environment]::SetEnvironmentVariable('PATH', "$cur;$binDir", 'User')
            $env:PATH += ";$binDir"
            Add-Check 'PATH (bin/)' 'ok' 'self-healed: added to PATH (restart shell)'
            Emit 'ok' 'PATH (bin/)' 'self-healed: restart shell'
        }
    } catch {
        Add-Check 'PATH (bin/)' 'warn' "PATH update failed: $($_.Exception.Message)"
        Emit '!' 'PATH (bin/)' 'add manually to PATH'
    }
}

# Check 9: .wkappbot/config.json
$cfgJson = Join-Path $repoRoot '.wkappbot\config.json'
if (Test-Path $cfgJson -PathType Leaf) {
    Add-Check 'config.json' 'ok' 'present'
    Emit 'ok' 'config.json' 'present'
} else {
    Add-Check 'config.json' 'warn' 'not found'
    Emit '!' 'config.json' 'running setup.ps1...'
    $setup = Join-Path $repoRoot 'setup.ps1'
    if (Test-Path $setup -PathType Leaf) {
        try {
            & $setup 2>&1 | Out-Null
            if (Test-Path $cfgJson -PathType Leaf) {
                Add-Check 'config.json' 'ok' 'self-healed: setup.ps1 ran'
                Emit 'ok' 'config.json' 'self-healed'
            } else {
                Add-Check 'config.json' 'warn' 'setup.ps1 ran but config missing'
                Emit '!' 'config.json' 'run setup.ps1 manually'
            }
        } catch {
            Add-Check 'config.json' 'warn' "failed: $($_.Exception.Message)"
            Emit '!' 'config.json' 'run setup.ps1 manually'
        }
    } else {
        Add-Check 'config.json' 'warn' 'setup.ps1 not found'
        Emit '!' 'config.json' 'check repo integrity'
    }
}
