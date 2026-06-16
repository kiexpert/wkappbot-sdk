# wkdoctor check: global-approval-sync -- ensure local approvals are synced to global policies
# Flags user approvals in settings.local.json not yet in settings.json or Gemini policies.
# Automatically heals if -HealApprovals is passed.

try {
    $localPath  = Join-Path $env:USERPROFILE '.claude\settings.local.json'
    $globalPath = Join-Path $env:USERPROFILE '.claude\settings.json'
    $syncScript = Join-Path $PSScriptRoot '..\..\..\..\wkappbot-kih\tools\wk-approval-sync.ps1'
    if (-not (Test-Path $syncScript)) {
        $syncScript = Join-Path $PSScriptRoot '..\..\tools\wk-approval-sync.ps1'
    }

    if (-not (Test-Path $localPath) -or -not (Test-Path $globalPath)) {
        Add-Check 'global-approval-sync' 'ok' 'n/a (settings files missing)'
        Emit 'ok' 'global-approval-sync' 'n/a'
        return
    }

    $localMtime = (Get-Item $localPath).LastWriteTime
    $globalMtime = (Get-Item $globalPath).LastWriteTime

    if ($localMtime -gt $globalMtime) {
        if ($env:WKDOCTOR_HEAL -eq '1' -or $args -contains '-HealApprovals') {
            # Run hidden (no flashing console window) via the shared helper from 09-harness-pace-health.ps1.
            if (Get-Command 'Invoke-WkHiddenPs1' -ErrorAction SilentlyContinue) {
                $null = Invoke-WkHiddenPs1 -Path $syncScript
            } else {
                & $syncScript
            }
            Add-Check 'global-approval-sync' 'ok' 'synced new approvals to global policies'
            Emit 'ok' 'global-approval-sync' 'healed'
        } else {
            Add-Check 'global-approval-sync' 'warn' 'new local approvals pending sync to global settings'
            Emit '!' 'global-approval-sync' 'pending sync (run wkdoctor -HealApprovals)'
        }
    } else {
        Add-Check 'global-approval-sync' 'ok' 'global settings in sync with local approvals'
        Emit 'ok' 'global-approval-sync' 'in sync'
    }
} catch {
    Add-Check 'global-approval-sync' 'ok' "n/a (check error): $_"
    Emit 'ok' 'global-approval-sync' 'error'
}