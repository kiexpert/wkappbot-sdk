# wkdoctor check: gemini-settings-bom -- validate Gemini & Antigravity settings and strip BOMs
# (1) DETECT: scan settings files for BOM and check if they are missing YOLO or harness hooks.
# (2) HEAL:   strip BOM and execute wkharness-gemini-install.ps1 to restore YOLO settings.

try {
    $toolsDir = 'D:\GitHub\wkappbot-kih\tools'
    if (-not (Test-Path $toolsDir)) {
        $toolsDir = Join-Path $PSScriptRoot '..\..\..\..\wkappbot-kih\tools'
    }
    $installScript = Join-Path $toolsDir 'wkharness-gemini-install.ps1'

    $geminiDir = Join-Path $env:USERPROFILE '.gemini'
    $targets = @(
        (Join-Path $geminiDir 'settings.json'),
        (Join-Path $geminiDir 'policies\autonomy.toml'),
        (Join-Path $geminiDir 'antigravity-cli\settings.json')
    )

    $bomFound  = 0
    $bomHealed = 0
    $badSettings = $false
    $errors = @()
    $wiringDetail = @()

    # 1. Check & Heal BOM
    foreach ($p in $targets) {
        if (Test-Path $p -PathType Leaf) {
            try {
                $bytes = [IO.File]::ReadAllBytes($p)
                if ($bytes.Count -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF) {
                    $bomFound++
                    if ($env:WKDOCTOR_HEAL -eq '1') {
                        $newBytes = New-Object byte[] ($bytes.Count - 3)
                        [Array]::Copy($bytes, 3, $newBytes, 0, $newBytes.Count)
                        [IO.File]::WriteAllBytes($p, $newBytes)
                        $bomHealed++
                    }
                }
            } catch {
                $errors += "BOM check failed on ${p}: $_"
            }
        }
    }

    # 2. Check settings content validity (YOLO + hooks)
    $settingsPath = Join-Path $geminiDir 'settings.json'
    $agySettingsPath = Join-Path $geminiDir 'antigravity-cli\settings.json'

    # Check root settings.json
    if (Test-Path $settingsPath) {
        try {
            $s = Get-Content $settingsPath -Raw | ConvertFrom-Json
            if ($s.approvalMode -ne 'yolo' -or -not $s.hooks.BeforeTool) {
                $badSettings = $true
            }
            # Deep harness-wiring invariants (manual review 2026-06-16 codified into the doctor so a
            # regression is caught every run, not by chance). Each failure -> badSettings + a reason.
            $beforeHooks = @($s.hooks.BeforeTool | ForEach-Object { $_.hooks })
            $harnessHook = $beforeHooks | Where-Object { $_.command -match 'wkharness' } | Select-Object -First 1
            $harnessCmd  = if ($harnessHook) { [string]$harnessHook.command } else { '' }
            # (1) BeforeTool harness hook must target the PER-FAMILY main, not the shared heart or a stray.
            if (-not ($harnessCmd -match 'wkharness-gemini-main\.ps1')) {
                $badSettings = $true; $wiringDetail += 'BeforeTool harness not -> wkharness-gemini-main.ps1; '
            }
            # (2) no reference to the retired stray hub D:\GitHub\wkharness.ps1 (deleted 2026-06-16).
            if ($harnessCmd -match '(?i)github[\\/]+wkharness\.ps1') {
                $badSettings = $true; $wiringDetail += 'references retired stray D:\GitHub\wkharness.ps1; '
            }
            # (3) a literal TAB inside the command path = the \t-escape JSON corruption.
            if ($harnessCmd -match "`t") {
                $badSettings = $true; $wiringDetail += 'TAB char in harness command (path corruption); '
            }
            # (4) gemini timeouts are MILLISECONDS; <3000 means the ~1.5s hook always times out -> gemini fails OPEN (unenforced).
            if ($harnessHook -and $harnessHook.timeout -and [int]$harnessHook.timeout -lt 3000) {
                $badSettings = $true; $wiringDetail += ('harness timeout ' + [int]$harnessHook.timeout + 'ms too small (instant-timeout/fail-open); ')
            }
            # (5) AfterTool post hook (wkharness-post.ps1) must EXIST -- post-tool knowledge-ops/study-lock/nudges. Regression guard (dropped 2026-06-16).
            # match BOTH shared wkharness-post.ps1 AND per-family wkharness-<fam>-post.ps1
            # (gemini wires wkharness-gemini-post.ps1; the bare 'wkharness-post' regex missed it
            #  -> falsely reported "AfterTool MISSING").
            $afterCmd = @($s.hooks.AfterTool | ForEach-Object { $_.hooks } | Where-Object { $_.command -match 'wkharness(?:-\w+)?-post' }) | Select-Object -First 1
            if (-not $afterCmd) {
                $badSettings = $true; $wiringDetail += 'AfterTool wkharness-post.ps1 hook MISSING (post-tool nudges/study-lock/knowledge-reward lost); '
            }
        } catch {
            $badSettings = $true
        }
    } else {
        $badSettings = $true
    }

    # Check antigravity-cli settings.json
    if (Test-Path $agySettingsPath) {
        try {
            $s = Get-Content $agySettingsPath -Raw | ConvertFrom-Json
            if ($s.approvalMode -ne 'yolo' -or -not $s.hooks.PreToolUse) {
                $badSettings = $true
            }
            # Check permissions
            if (-not ($s.permissions -and $s.permissions.allow -contains 'command(*)')) {
                $badSettings = $true
            }
        } catch {
            $badSettings = $true
        }
    } else {
        $badSettings = $true
    }

    # 3. Heal bad settings if needed
    $settingsHealed = $false
    if ($badSettings) {
        if ($env:WKDOCTOR_HEAL -eq '1' -and (Test-Path $installScript)) {
            # Run hidden (no flashing console window) via the shared helper from 09-harness-pace-health.ps1.
            if (Get-Command 'Invoke-WkHiddenPs1' -ErrorAction SilentlyContinue) {
                $null = Invoke-WkHiddenPs1 -Path $installScript
            } else {
                # Fallback (helper not loaded): in-process, still no grandchild console.
                & $installScript | Out-Null
            }
            $settingsHealed = $true
            $badSettings = $false
        }
    }

    # 4. Report
    if ($bomFound -eq 0 -and -not $badSettings -and $errors.Count -eq 0) {
        Add-Check 'gemini:settings-bom' 'ok' 'Gemini and Antigravity settings are clean and configured (YOLO).'
        Emit 'ok' 'gemini:settings-bom' 'clean'
    } else {
        $status = if ($errors.Count -gt 0 -or ($badSettings -and -not $settingsHealed)) { 'warn' } else { 'ok' }
        
        $msg = ""
        if ($bomFound -gt 0) {
            $msg += "Found $bomFound files with BOM (healed $bomHealed). "
        }
        if ($badSettings) {
            $msg += "Gemini settings are misconfigured or missing hooks/YOLO. "
            if ($wiringDetail.Count -gt 0) { $msg += ('WIRING: ' + ($wiringDetail -join '')) }
        }
        if ($settingsHealed) {
            $msg += "Reinstalled correct Gemini/Antigravity settings. "
        }
        
        Add-Check 'gemini:settings-bom' $status $msg.Trim()
        Emit $(if ($status -eq 'ok') { 'ok' } else { '!' }) 'gemini:settings-bom' $msg.Trim()

        foreach ($err in $errors) {
            Add-Check 'gemini:settings-bom:error' 'warn' $err
            Emit '!' 'gemini:settings-bom:error' $err
        }
    }
} catch {
    Add-Check 'gemini:settings-bom' 'ok' "n/a (check error): $_"
    Emit 'ok' 'gemini:settings-bom' 'error'
}
