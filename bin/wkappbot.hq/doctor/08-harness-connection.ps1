# wkdoctor check: harness connection -- AGY and Gemini (Claude) settings.json health & auto-install
# Runs within wkdoctor context. $binDir, $repoRoot are already defined by wkdoctor.ps1.

$gitHubDir = try { Split-Path $repoRoot -Parent } catch { 'D:\GitHub' }
$gitHubDirSlash = $gitHubDir -replace '\\','/'
$harnessPath = "$gitHubDirSlash/wkharness.ps1"
$harnessPostPath = "$gitHubDirSlash/wkharness-post.ps1"
$personalTools = "$gitHubDirSlash/personal-docs/tools"

# Helper function to install / fix settings.json
function Install-HarnessSettings {
    param(
        [string]$Type, # 'agy' or 'claude'
        [string]$Path
    )

    $beforeHooks = @()
    if (Test-Path (Join-Path $personalTools 'audit-log.ps1')) {
        $beforeHooks += [ordered]@{
            id    = 'audit'
            hooks = @([ordered]@{
                type    = 'command'
                command = "powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File `"$personalTools/audit-log.ps1`""
                async   = $true
            })
        }
    }

    if ($Type -eq 'agy') {
        $beforeHooks += [ordered]@{
            id    = 'harness'
            hooks = @([ordered]@{
                type          = 'command'
                command       = "powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File `"$harnessPath`""
                timeout       = 30
                statusMessage = 'wkharness(kih)...'
            })
        }

        $existingAgy = $null
        if (Test-Path $Path) {
            try {
                $aText = [IO.File]::ReadAllText($Path, [Text.Encoding]::UTF8)
                $existingAgy = ConvertFrom-Json -InputObject $aText -AsHashtable
            } catch {}
        }
        if (-not $existingAgy) { $existingAgy = @{} }
        
        $existingAgy['approvalMode'] = 'yolo'
        if (-not $existingAgy['security']) { $existingAgy['security'] = @{} }
        $existingAgy['security']['enablePermanentToolApproval'] = $true
        
        if (-not $existingAgy['permissions']) { $existingAgy['permissions'] = @{} }
        $existingAgy['permissions']['allow'] = @(
            '*',
            'command(git)', 'unsandboxed(git)',
            'command(powershell)', 'unsandboxed(powershell)',
            'command(pwsh)', 'unsandboxed(pwsh)',
            'command(bash)', 'unsandboxed(bash)',
            'command(cmd)', 'unsandboxed(cmd)',
            'command(sh)', 'unsandboxed(sh)',
            'command(gh)', 'unsandboxed(gh)',
            'command(wkappbot)', 'unsandboxed(wkappbot)'
        )
        
        $existingAgy['hooks'] = @{
            PreToolUse = $beforeHooks
            PostToolUse = @(
                @{
                    id    = 'harness-post'
                    hooks = @(@{
                        type    = 'command'
                        command = "powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File `"$harnessPostPath`""
                        timeout = 10
                    })
                }
            )
        }
        $agyJson = ConvertTo-Json -InputObject $existingAgy -Depth 10
        [IO.File]::WriteAllText($Path, $agyJson, [Text.Encoding]::UTF8)
    } else {
        # Claude/Gemini
        $preHooks = @()
        $preHooks += [ordered]@{
            hooks = @([ordered]@{
                type          = 'command'
                command       = "powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File `"$harnessPath`""
                timeout       = 30
                statusMessage = 'wkharness(kih)...'
            })
        }
        if (Test-Path (Join-Path $personalTools 'audit-log.ps1')) {
            $preHooks += [ordered]@{
                hooks = @([ordered]@{
                    type    = 'command'
                    command = "powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File `"$personalTools/audit-log.ps1`""
                    async   = $true
                })
            }
        }

        $cSettings = $null
        if (Test-Path $Path) {
            try {
                $cText = [IO.File]::ReadAllText($Path, [Text.Encoding]::UTF8)
                $cSettings = ConvertFrom-Json -InputObject $cText -AsHashtable
            } catch {}
        }
        if (-not $cSettings) { $cSettings = @{} }
        if (-not $cSettings.permissions) { $cSettings.permissions = @{} }
        $cSettings.permissions.defaultMode = 'bypassPermissions'
        $cSettings.permissions.allow = @(
            'Bash(*)', 'PowerShell(*)', 'Read(*)', 'Write(*)', 'Edit(*)', 'Glob(*)', 'Grep(*)', 'WebFetch(*)', 'WebSearch(*)',
            'mcp__wkappbot__wkappbot', 'mcp__wkappbot__wkappbot_cli', 'Bash(python *)',
            'command(git)', 'unsandboxed(git)',
            'command(powershell)', 'unsandboxed(powershell)',
            'command(pwsh)', 'unsandboxed(pwsh)',
            'command(bash)', 'unsandboxed(bash)',
            'command(cmd)', 'unsandboxed(cmd)',
            'command(sh)', 'unsandboxed(sh)',
            'command(gh)', 'unsandboxed(gh)',
            'command(wkappbot)', 'unsandboxed(wkappbot)'
        )
        $cSettings.skipDangerousModePermissionPrompt = $true
        $cSettings.skipAutoPermissionPrompt = $true
        if ($cSettings.permissions.ContainsKey('deny')) { $null = $cSettings.permissions.Remove('deny') }
        
        $cSettings.hooks = @{
            PreToolUse = $preHooks
            PostToolUse = @(
                @{
                    hooks = @(@{
                        type    = 'command'
                        command = "powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File `"$harnessPostPath`""
                        timeout = 10
                    })
                }
            )
        }
        if (-not $cSettings.model) { $cSettings.model = 'opus' }

        $cJson = ConvertTo-Json -InputObject $cSettings -Depth 10
        [IO.File]::WriteAllText($Path, $cJson, [Text.Encoding]::UTF8)
    }
}

# --- 1. AGY Check ---
$agyDir = Join-Path $env:USERPROFILE '.gemini\antigravity-cli'
$agySettings = Join-Path $agyDir 'settings.json'
$agyOk = $false

if (Test-Path $agySettings -PathType Leaf) {
    try {
        $raw = Get-Content $agySettings -Encoding UTF8 -Raw | ConvertFrom-Json
        $hasHarness = $raw.hooks -and $raw.hooks.PreToolUse -and ($raw.hooks.PreToolUse | Where-Object { $_.id -eq 'harness' })
        $hasWildcard = $raw.permissions -and $raw.permissions.allow -and ($raw.permissions.allow -contains '*')
        
        $required = @(
            'command(git)', 'unsandboxed(git)',
            'command(powershell)', 'unsandboxed(powershell)',
            'command(pwsh)', 'unsandboxed(pwsh)',
            'command(bash)', 'unsandboxed(bash)',
            'command(cmd)', 'unsandboxed(cmd)',
            'command(sh)', 'unsandboxed(sh)',
            'command(gh)', 'unsandboxed(gh)',
            'command(wkappbot)', 'unsandboxed(wkappbot)'
        )
        $hasRequired = $true
        foreach ($r in $required) {
            if ($raw.permissions.allow -notcontains $r) {
                $hasRequired = $false
                break
            }
        }
        if ($hasHarness -and $hasWildcard -and $hasRequired) {
            $agyOk = $true
        }
    } catch {}
}

if ($agyOk) {
    Add-Check 'Harness (AGY)' 'ok' 'connected'
    Emit 'ok' 'Harness (AGY)' 'connected & yolo config verified'
} else {
    Add-Check 'Harness (AGY)' 'warn' 'not connected or misconfigured'
    Emit '!' 'Harness (AGY)' 'harness integration missing. Auto-installing...'
    try {
        if (-not (Test-Path $agyDir)) { $null = [IO.Directory]::CreateDirectory($agyDir) }
        Install-HarnessSettings -Type 'agy' -Path $agySettings
        Add-Check 'Harness (AGY)' 'ok' 'self-healed: harness connected'
        Emit 'ok' 'Harness (AGY)' 'self-healed successfully'
    } catch {
        Add-Check 'Harness (AGY)' 'fail' "auto-install failed: $_"
        Emit 'fail' 'Harness (AGY)' "failed to connect: $_"
    }
}

# --- 2. Claude/Gemini Check ---
$claudeDir = Join-Path $env:USERPROFILE '.claude'
$claudeSettings = Join-Path $claudeDir 'settings.json'
$claudeOk = $false

if (Test-Path $claudeSettings -PathType Leaf) {
    try {
        $raw = Get-Content $claudeSettings -Encoding UTF8 -Raw | ConvertFrom-Json
        $hasHarness = $raw.hooks -and $raw.hooks.PreToolUse -and ($raw.hooks.PreToolUse.hooks | Where-Object { $_.command -match 'wkharness\.ps1' })
        $hasWildcard = $raw.permissions -and $raw.permissions.allow -and ($raw.permissions.allow -contains 'Bash(*)')
        
        $required = @(
            'command(git)', 'unsandboxed(git)',
            'command(powershell)', 'unsandboxed(powershell)',
            'command(pwsh)', 'unsandboxed(pwsh)',
            'command(bash)', 'unsandboxed(bash)',
            'command(cmd)', 'unsandboxed(cmd)',
            'command(sh)', 'unsandboxed(sh)',
            'command(gh)', 'unsandboxed(gh)',
            'command(wkappbot)', 'unsandboxed(wkappbot)'
        )
        $hasRequired = $true
        foreach ($r in $required) {
            if ($raw.permissions.allow -notcontains $r) {
                $hasRequired = $false
                break
            }
        }
        if ($hasHarness -and $hasWildcard -and $hasRequired) {
            $claudeOk = $true
        }
    } catch {}
}

if ($claudeOk) {
    Add-Check 'Harness (Gemini/Claude)' 'ok' 'connected'
    Emit 'ok' 'Harness (Gemini/Claude)' 'connected & wildcard permissions verified'
} else {
    Add-Check 'Harness (Gemini/Claude)' 'warn' 'not connected or misconfigured'
    Emit '!' 'Harness (Gemini/Claude)' 'harness integration missing. Auto-installing...'
    try {
        if (-not (Test-Path $claudeDir)) { $null = [IO.Directory]::CreateDirectory($claudeDir) }
        Install-HarnessSettings -Type 'claude' -Path $claudeSettings
        Add-Check 'Harness (Gemini/Claude)' 'ok' 'self-healed: harness connected'
        Emit 'ok' 'Harness (Gemini/Claude)' 'self-healed successfully'
    } catch {
        Add-Check 'Harness (Gemini/Claude)' 'fail' "auto-install failed: $_"
        Emit 'fail' 'Harness (Gemini/Claude)' "failed to connect: $_"
    }
}


