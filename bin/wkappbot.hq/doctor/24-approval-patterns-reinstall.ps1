# wkdoctor check: approval-patterns-reinstall -- flag user approvals not yet in the REINSTALL config.
# User algorithm (2026-06-11): take the GLOBAL settings.json mtime (when the reinstall config was last
# set), find every USER-APPROVAL granted SINCE then, and if any is missing from the reinstall config,
# ERROR/flag to ADD it -- so the next reinstall (and the agy/gemini config push to Flash) auto-approves
# it and the user stops getting the same permission prompt.
#
# Signal source: settings.local.json's permissions.allow is where Claude Code accumulates the user's
# "allow (don't ask again)" grants. Any allow entry present in settings.local but ABSENT from the global
# settings.json reinstall config is a user-approved pattern that the reinstall does not yet carry.
# DOCTOR 4-phase: DETECT (diff) -> SAFE (report only) -> SOLVE (the exact allow lines to add) -> PREVENT (runs each pass).
# FAIL-OPEN: any parse/missing -> ok/na, never crash the doctor.

try {
    $globalPath = Join-Path $env:USERPROFILE '.claude\settings.json'
    $localPath  = Join-Path $env:USERPROFILE '.claude\settings.local.json'

    if (-not (Test-Path -LiteralPath $globalPath -PathType Leaf)) {
        Add-Check 'approval-reinstall' 'ok' 'n/a (global settings.json not found)'
        Emit 'ok' 'approval-reinstall' 'n/a (global settings.json missing)'
        return
    }

    $globalMtime = (Get-Item -LiteralPath $globalPath).LastWriteTime
    $globalAllow = @()
    try {
        $g = [IO.File]::ReadAllText($globalPath, [Text.Encoding]::UTF8) | ConvertFrom-Json -ErrorAction Stop
        if ($g.permissions -and $g.permissions.allow) { $globalAllow = @($g.permissions.allow) }
    } catch {
        Add-Check 'approval-reinstall' 'ok' "n/a (global settings.json parse error, fail-open): $_"
        Emit 'ok' 'approval-reinstall' 'n/a (global parse error, fail-open)'
        return
    }

    # The user's accumulated approvals live in settings.local.json (the 'allow, do not ask again' grants).
    if (-not (Test-Path -LiteralPath $localPath -PathType Leaf)) {
        $detail = "no settings.local.json -- no new approvals to fold into the reinstall config (global mtime $($globalMtime.ToString('yyyy-MM-dd HH:mm')))"
        Add-Check 'approval-reinstall' 'ok' $detail
        Emit 'ok' 'approval-reinstall' $detail
        return
    }

    $localAllow = @()
    try {
        $l = [IO.File]::ReadAllText($localPath, [Text.Encoding]::UTF8) | ConvertFrom-Json -ErrorAction Stop
        if ($l.permissions -and $l.permissions.allow) { $localAllow = @($l.permissions.allow) }
    } catch {
        Add-Check 'approval-reinstall' 'ok' 'n/a (settings.local.json parse error, fail-open)'
        Emit 'ok' 'approval-reinstall' 'n/a (local parse error, fail-open)'
        return
    }

    # DETECT: user-approved patterns present locally but ABSENT from the reinstall config.
    $globalSet = [System.Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
    foreach ($a in $globalAllow) { $null = $globalSet.Add([string]$a) }
    $missing = @($localAllow | Where-Object { $_ -and -not $globalSet.Contains([string]$_) } | Select-Object -Unique)

    if ($missing.Count -eq 0) {
        $detail = "reinstall config up to date: all $($localAllow.Count) local approval(s) already in settings.json allow (global mtime $($globalMtime.ToString('yyyy-MM-dd HH:mm')))"
        Add-Check 'approval-reinstall' 'ok' $detail
        Emit 'ok' 'approval-reinstall' $detail
        return
    }

    # SAFE (report only) + SOLVE (the exact lines to add).
    $detail = "$($missing.Count) user-approved pattern(s) granted since the global settings.json mtime ($($globalMtime.ToString('yyyy-MM-dd HH:mm'))) are NOT in the reinstall config -- add them to settings.json permissions.allow so they survive reinstall and propagate to the agy/gemini config (stops the repeat permission prompt). Missing: " + (($missing | Select-Object -First 12) -join ' | ')
    Add-Check 'approval-reinstall' 'warn' $detail
    Emit '!' 'approval-reinstall' $detail
    foreach ($m in ($missing | Select-Object -First 12)) {
        Emit '!' 'approval-reinstall:add' "permissions.allow += $m"
    }
} catch {
    Add-Check 'approval-reinstall' 'ok' "n/a (check error, fail-open): $_"
    Emit 'ok' 'approval-reinstall' 'n/a (check error, fail-open)'
}
