# wkdoctor check: harness-symlink-canonical -- detect wrapper symlinks that still point at the STALE
# personal-docs harness instead of the canonical wkappbot-kih body.
# Background (2026-06-11 forensics): the harness source was migrated to wkappbot-kih, but a prior
# delete of the personal-docs copies (eed7ef5) was REVERTED (98a9d38 "personal-docs was still LIVE")
# because the WKAppBot/bin wrapper SYMLINKS still targeted personal-docs -- so deleting the source
# broke the tool chain. This check makes that drift VISIBLE every pass. The REPAIR (re-point to kih)
# + the on-init REINSTALL are session-init duties and require kih to first hold the reconciled latest
# source -- see cross-family-harness-integration-ref (the divergence trap). This module is DETECT-only.
# FAIL-OPEN: any error -> ok/na, never crash the doctor.

try {
    $binDir = @('D:\GitHub\WKAppBot\bin', (Join-Path $env:USERPROFILE 'GitHub\WKAppBot\bin')) |
        Where-Object { Test-Path -LiteralPath $_ } | Select-Object -First 1
    if (-not $binDir) {
        Add-Check 'harness-symlink-canonical' 'ok' 'n/a (WKAppBot/bin not local)'
        Emit 'ok' 'harness-symlink-canonical' 'n/a (bin not local)'
        return
    }

    # The Claude-named wrapper set (the primary harness entry points).
    $wrapperNames = @('Agent','Bash','Cmd','Edit','Glob','Grep','PowerShell','Read','Write')
    $stale = New-Object 'System.Collections.Generic.List[string]'
    $checked = 0
    foreach ($n in $wrapperNames) {
        foreach ($ext in @('.cmd','.ps1')) {
            $p = Join-Path $binDir ($n + $ext)
            if (-not (Test-Path -LiteralPath $p)) { continue }
            try {
                $item = Get-Item -LiteralPath $p -Force -ErrorAction Stop
                $target = $item.Target
                if (-not $target) { continue }   # not a symlink
                $checked++
                $tgt = [string]$target
                if ($tgt -match '(?i)personal-docs') {
                    $stale.Add("$($n)$ext -> $tgt")
                }
            } catch { }
        }
    }

    if ($stale.Count -eq 0) {
        $detail = "$checked wrapper symlink(s) canonical (none point at personal-docs)"
        Add-Check 'harness-symlink-canonical' 'ok' $detail
        Emit 'ok' 'harness-symlink-canonical' $detail
    } else {
        $detail = "$($stale.Count)/$checked wrapper symlink(s) STILL target personal-docs (stale -- canonical is wkappbot-kih/tools). Deleting personal-docs would break the tool chain until these repoint. " + (($stale | Select-Object -First 6) -join ' | ')
        Add-Check 'harness-symlink-canonical' 'warn' $detail
        Emit '!' 'harness-symlink-canonical' $detail
        Emit '!' 'harness-symlink-canonical:fix' 'reconcile personal-docs edits into kih first (divergence trap), then repoint these symlinks + reinstall on session-init -- see cross-family-harness-integration-ref'
    }
} catch {
    Add-Check 'harness-symlink-canonical' 'ok' "n/a (check error, fail-open): $_"
    Emit 'ok' 'harness-symlink-canonical' 'n/a (check error, fail-open)'
}
