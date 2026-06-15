# wkdoctor check: harness connection -- AGY and Gemini (Claude) settings.json health & auto-install
# Runs within wkdoctor context. $binDir, $repoRoot are already defined by wkdoctor.ps1.

$gitHubDir = try { Split-Path $repoRoot -Parent } catch { 'D:\GitHub' }
# CRITICAL: Always use the central shim paths at D:\GitHub for settings.json integration.
# These shims handle model detection and stream-safe JSON communication.
$harnessPath = "D:\GitHub\wkharness.ps1"
$harnessPostPath = "D:\GitHub\wkharness-post.ps1"

# Helper function to install / fix settings.json
function Install-HarnessSettings {
    param(
        [string]$Type, # 'agy' or 'claude'
        [string]$Path
    )

    # Claude (~/.claude) talks to the family-aware kih harness DIRECTLY. kih emits
    # Claude-native hook output (silent-allow / {"decision":...}). Routing Claude
    # through the central D:\GitHub\wkharness.ps1 shim (gemini-cli-generated) re-wraps
    # output into gemini's {"decision","result"} schema -> Claude rejects every hook
    # with "Invalid input" AND the shim's TOML-logger pollutes ~/.gemini policy with
    # toolName=Bash rules. AGY/Gemini keep the shim (it translates gemini tool names).
    if ($Type -eq 'claude') {
        # -Family claude: authoritative family hint so kih skips the gemini/codex
        # family classification (faster + avoids misdetection). Tier still detected.
        $preCmd  = "powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File `"D:\GitHub\wkappbot-kih\tools\wkharness.ps1`" -Family claude"
        $postCmd = "powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File `"D:\GitHub\wkappbot-kih\tools\wkharness-post.ps1`""
    } else {
        $preCmd  = "powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File `"$harnessPath`""
        $postCmd = "powershell -NoProfile -NonInteractive -ExecutionPolicy Bypass -File `"$harnessPostPath`""
    }

    $commonBeforeHooks = @([ordered]@{
        id    = 'harness'
        hooks = @([ordered]@{ type = 'command'; command = $preCmd; timeout = 30; statusMessage = 'wkharness(kih)...' })
    })
    $commonPostHooks = @([ordered]@{
        id    = 'harness-post'
        hooks = @([ordered]@{ type = 'command'; command = $postCmd; timeout = 10 })
    })

    if ($Type -eq 'agy') {
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
            'command(*)',
            'unsandboxed(*)',
            'read_file(*)',
            'write_file(*)',
            'execute_url(*)',
            'read_url(*)',
            'mcp(*)'
        )

        $existingAgy['hooks'] = @{
            PreToolUse = $commonBeforeHooks
            PostToolUse = $commonPostHooks
        }
        $agyJson = ConvertTo-Json -InputObject $existingAgy -Depth 10
        [IO.File]::WriteAllText($Path, $agyJson, [Text.Encoding]::UTF8)
    } else {
        # Claude/Gemini
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

        # Claude Code uses PreToolUse (NOT gemini's BeforeTool). ~/.claude is Claude-only;
        # gemini-cli has its own ~/.gemini/settings.json installed separately.
        $cSettings.hooks = @{
            PreToolUse  = $commonBeforeHooks
            PostToolUse = $commonPostHooks
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
        $hasWildcard = $raw.permissions -and $raw.permissions.allow -and ($raw.permissions.allow -contains 'command(*)')
        if ($hasHarness -and $hasWildcard) { $agyOk = $true }
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
        Emit 'ok' 'Harness (AGY)' 'self-healed successfully'
    } catch { Emit 'fail' 'Harness (AGY)' "failed to connect: $_" }
}

# --- 2. Claude/Gemini Check ---
$claudeDir = Join-Path $env:USERPROFILE '.claude'
$claudeSettings = Join-Path $claudeDir 'settings.json'
$claudeOk = $false

if (Test-Path $claudeSettings -PathType Leaf) {
    try {
        $raw = Get-Content $claudeSettings -Encoding UTF8 -Raw | ConvertFrom-Json
        # Claude uses PreToolUse. Healthy ONLY if it points at the kih harness directly
        # (not the gemini shim); otherwise re-heal to kih.
        $hooks = $raw.hooks.PreToolUse
        $hasHarness = $hooks -and ($hooks | Where-Object { $_.id -eq 'harness' -and ($_.hooks.command -match 'wkappbot-kih') })      
        $hasWildcard = $raw.permissions -and $raw.permissions.allow -and ($raw.permissions.allow -contains 'Bash(*)')
        if ($hasHarness -and $hasWildcard) { $claudeOk = $true }
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
        Emit 'ok' 'Harness (Gemini/Claude)' 'self-healed successfully'
    } catch { Emit 'fail' 'Harness (Gemini/Claude)' "failed to connect: $_" }
}
