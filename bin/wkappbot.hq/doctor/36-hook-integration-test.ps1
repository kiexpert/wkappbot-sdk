# wkdoctor check: hook integration validation (Gemini/Codex/Claude)
# Verifies that wkharness.ps1 responds with correct JSON and exit codes per-family.

$gitHubDir = try { Split-Path $repoRoot -Parent } catch { 'D:\GitHub' }
$harnessPath = Join-Path $gitHubDir 'wkappbot-kih\tools\wkharness.ps1'

if (-not (Test-Path $harnessPath)) {
    Add-Check 'Hook Integration' 'fail' "harness script not found at $harnessPath"
    Emit 'x' 'Hook Integration' 'wkharness.ps1 missing, cannot test protocol'
    return
}

function Test-HookProtocol {
    param(
        [string]$Family, # 'gemini', 'codex', 'claude'
        [string]$InputJson,
        [int]$ExpectExit
    )

    $env:WKHARNESS_SKIP_LOG_CHECK = "1"
    $model = switch ($Family) {
        'gemini' { 'gemini-2.0-flash' }
        'codex'  { 'codex-mini' }
        'claude' { 'claude-sonnet-4-6' }
    }

    try {
        $proc = New-Object System.Diagnostics.Process
        $proc.StartInfo.FileName = "powershell.exe"
        $proc.StartInfo.Arguments = "-NoProfile -NonInteractive -ExecutionPolicy Bypass -File `"$harnessPath`" -Family $Family"
        $proc.StartInfo.RedirectStandardInput = $true
        $proc.StartInfo.RedirectStandardOutput = $true
        $proc.StartInfo.RedirectStandardError = $true
        $proc.StartInfo.UseShellExecute = $false
        $proc.StartInfo.CreateNoWindow = $true
        
        $proc.StartInfo.EnvironmentVariables['WKHARNESS_SKIP_LOG_CHECK'] = "1"
        $proc.StartInfo.EnvironmentVariables['WKHARNESS_AGENT_MODEL'] = $model

        $null = $proc.Start()
        $proc.StandardInput.WriteLine($InputJson)
        $proc.StandardInput.Close()

        $stdout = $proc.StandardOutput.ReadToEnd()
        $proc.WaitForExit(5000)
        $exitCode = $proc.ExitCode

        $j = try { $stdout | ConvertFrom-Json } catch { $null }
        $resolvedModel = if ($j) { $j.model } else { "PARSE_ERROR" }
        $decision = if ($j) { $j.decision } else { "MISSING" }

        # For Gemini/Codex, wkharness.ps1 is now configured to detect via WKHARNESS_AGENT_MODEL
        # and exit 0 with JSON.
        $modelMatch = ($resolvedModel -match $Family) -or ($resolvedModel -match 'flash' -and $Family -eq 'gemini') -or ($resolvedModel -match 'mini' -and $Family -eq 'codex')
        # Claude blocks by EXIT CODE only (emits NO stdout JSON by design -> $j null -> model='PARSE_ERROR'
        # is EXPECTED, not a failure). Verify exit-code alone for claude; gemini/codex emit JSON so check it.
        if ($Family -eq 'claude') {
            $ok = ($exitCode -eq $ExpectExit)
        } else {
            # gemini/codex: a deny now EXITS NON-ZERO (theater-fix 2026-06-17) + emits JSON. Verify the
            # deny ACTUALLY blocks (exit != 0) -- the stale exit-0 expectation was the theater the fix removed.
            $ok = ($exitCode -ne 0) -and ($decision -eq 'deny') -and $modelMatch
        }
        
        if ($ok) {
            return $true
        } else {
            Emit '!' "HookTest:$Family" "exit=$exitCode (exp $ExpectExit), decision=$decision, model='$resolvedModel' (exp match $Family)"
            return $false
        }
    } finally {
        $env:WKHARNESS_SKIP_LOG_CHECK = $null
    }
}

# 1. Gemini Block Test (Exit 0 + JSON)
$geminiBlock = '{"tool_name":"Agent","tool_input":{"prompt":"do nothing"}}'
$geminiOk = Test-HookProtocol -Family 'gemini' -InputJson $geminiBlock -ExpectExit 0

# 2. Codex Block Test (Exit 0 + JSON)
$codexBlock = '{"tool_name":"Agent","tool_input":{"prompt":"do nothing"}}'
$codexOk = Test-HookProtocol -Family 'codex' -InputJson $codexBlock -ExpectExit 0

# 3. Claude Block Test (Exit 1 + JSON on stderr)
# Actually, I standardized it to print JSON on stdout for everyone now,
# but exit 1 for Claude.
$claudeBlock = '{"tool_name":"Agent","tool_input":{"prompt":"do nothing"}}'
$claudeOk = Test-HookProtocol -Family 'claude' -InputJson $claudeBlock -ExpectExit 1

if ($geminiOk -and $codexOk -and $claudeOk) {
    Add-Check 'Hook Protocol' 'ok' 'verified'
    Emit 'ok' 'Hook Protocol' 'Gemini/Codex/Claude communication standards verified'
} else {
    Add-Check 'Hook Protocol' 'fail' "validation failed (Gemini=$geminiOk, Codex=$codexOk, Claude=$claudeOk)"
    Emit 'fail' 'Hook Protocol' 'Cross-family communication regression detected!'
}

# === SKILL-LOCK per-family guide+block (user directive 2026-06-15) ===========
# For each family, attempt an EDIT of a skill-locked source file and verify the
# harness BLOCKS (decision=deny) AND GUIDES (lists skills to read). on-load is
# fail-open on a missing JSONL, so the empty transcript_path isolates skill-lock.
# Reports per-family; does NOT auto-lift the emergency write-freeze marker (that
# stays under ROOT control until proper cross-family integration is confirmed).
$lockedSrc = Join-Path $gitHubDir 'wkappbot-kih\tools\wkharness-guards.ps1'
function Test-SkillLockFamily {
    param([string]$Family, [string]$Model)
    if (-not (Test-Path $lockedSrc)) { return $null }   # can't test -> inconclusive
    $fp = ($lockedSrc -replace '\\','\\')
    $payload = '{"tool_name":"Edit","tool_input":{"file_path":"' + $fp + '"},"session_id":"slk-doctor","transcript_path":""}'
    try {
        $psi = New-Object System.Diagnostics.ProcessStartInfo
        $psi.FileName = 'powershell.exe'
        $psi.Arguments = "-NoProfile -NonInteractive -ExecutionPolicy Bypass -File `"$harnessPath`""
        $psi.RedirectStandardInput = $true; $psi.RedirectStandardOutput = $true; $psi.RedirectStandardError = $true
        $psi.UseShellExecute = $false; $psi.CreateNoWindow = $true
        $psi.EnvironmentVariables['WKHARNESS_AGENT_MODEL'] = $Model
        $psi.EnvironmentVariables['WKHARNESS_SKIP_LOG_CHECK'] = '1'
        $p = [System.Diagnostics.Process]::Start($psi)
        $p.StandardInput.WriteLine($payload); $p.StandardInput.Close()
        $all = $p.StandardOutput.ReadToEnd() + "`n" + $p.StandardError.ReadToEnd()
        $p.WaitForExit(8000)
        $blocked = ($all -match '"decision"\s*:\s*"deny"')
        # GUIDE must be SKILL-LOCK-specific. Exclude the on-load reflex message (which
        # also contains the generic phrase "skill read") so we do not green-light on
        # on-load masking skill-lock. Real study-lock markers only:
        $guided  = ($all -match '(?i)study[\s-]?lock|harness:skill|should have read')
        return ($blocked -and $guided)
    } catch { return $false }
}
$slkResults = @{
    gemini = (Test-SkillLockFamily -Family 'gemini' -Model 'gemini-2.0-flash')
    codex  = (Test-SkillLockFamily -Family 'codex'  -Model 'codex-mini')
    claude = (Test-SkillLockFamily -Family 'claude' -Model 'claude-sonnet-4-6')
}
$slkFailed = @($slkResults.GetEnumerator() | Where-Object { $_.Value -ne $true } | ForEach-Object { $_.Key })
if ($slkFailed.Count -eq 0) {
    Add-Check 'Skill-Lock Per-Family' 'ok' 'all families block+guide a locked-source edit'
    Emit 'ok' 'Skill-Lock Per-Family' 'locked-source edit is blocked+guided for Gemini/Codex/Claude'
} else {
    Add-Check 'Skill-Lock Per-Family' 'fail' ("not enforced for: " + ($slkFailed -join ', '))
    Emit 'x' 'Skill-Lock Per-Family' ("skill-lock NOT block+guide for: " + ($slkFailed -join ', ') + " -- emergency write-freeze must stay ACTIVE until fixed (see harness-verified.json)")
}
