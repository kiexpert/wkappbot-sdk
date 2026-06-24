# Synthetic self-test for 53-agy-harness-bypass-detect-proto.ps1
# Tests the DETECTOR LOGIC ONLY -- does NOT call the installer on live settings.
# Run: pwsh -NonInteractive -File .\53-agy-harness-bypass-detect-selftest-sonnet.ps1
# Expected: all PASS, exit 0. Any FAIL = detector bug.

Set-StrictMode -Off
$ErrorActionPreference = 'Stop'

$pass = 0; $fail = 0

function Assert-Equal {
    param([string]$Label, $Got, $Expected)
    if ($Got -eq $Expected) {
        Write-Host "[PASS] $Label" -ForegroundColor Green
        $script:pass++
    } else {
        Write-Host "[FAIL] $Label -- got '$Got' expected '$Expected'" -ForegroundColor Red
        $script:fail++
    }
}

# ---- inline the detection helpers (copy from proto so test is self-contained) ----

# PS5.1-safe pattern: collect into a plain [string[]] script-block-local array,
# return with comma prefix to prevent pipeline unrolling to chars.
# Callers MUST wrap with @() to flatten the one-element wrapper: @(_Agy53_Get...).

# PS5.1 array-return contract:
#   - non-empty: return ,[string[]]$out  (comma prevents pipeline unrolling)
#   - empty:     return @()              (no comma -- ,@() wraps to count=1)
# Callers MUST use @(fn ...) to unwrap the non-empty case.

function _Agy53_GetCompromiseReasons {
    param($Json, [string]$Label)
    if ($null -eq $Json) { return }
    if ($Json.approvalMode -eq 'yolo') {
        Write-Output "$Label approvalMode='yolo'"
    }
    # grantedPermissions detection: works for both PSCustomObject and hashtable
    $hasGranted = $false
    if ($Json -is [hashtable]) {
        $hasGranted = $Json.ContainsKey('grantedPermissions')
    } else {
        foreach ($prop in $Json.psobject.properties) {
            if ($prop.Name -eq 'grantedPermissions') { $hasGranted = $true; break }
        }
    }
    if ($hasGranted) {
        Write-Output "$Label grantedPermissions present"
    }
}

function _Agy53_GetAgyLocalReasons {
    param($Json)
    if ($null -eq $Json) { return }
    # (a) + (b): delegate to shared checker
    _Agy53_GetCompromiseReasons -Json $Json -Label 'agy'
    # (c) enablePermanentToolApproval
    $epta = $null
    try {
        if ($Json -is [hashtable]) { $epta = $Json['security']['enablePermanentToolApproval'] }
        else { $epta = $Json.security.enablePermanentToolApproval }
    } catch {}
    if ($epta -eq $true) {
        Write-Output "agy security.enablePermanentToolApproval=true"
    }
}

Write-Host "`n=== 53-agy-harness-bypass-detect SYNTHETIC TESTS ===" -ForegroundColor Cyan

# ---- AST parse check ----
Write-Host "`n-- AST parse (0 errors) --" -ForegroundColor Cyan
$protoPath = Join-Path $PSScriptRoot '53-agy-harness-bypass-detect-proto.ps1'
$parseErrors = $null
$null = [System.Management.Automation.Language.Parser]::ParseFile($protoPath, [ref]$null, [ref]$parseErrors)
Assert-Equal 'AST parse errors = 0' $parseErrors.Count 0

# ---- CASE 1: compromised mock (yolo + grantedPermissions in agy-local) ----
Write-Host "`n-- Case 1: compromised mock (yolo + allow wildcards + grantedPermissions) --" -ForegroundColor Cyan
$compromisedMock = [PSCustomObject]@{
    approvalMode        = 'yolo'
    grantedPermissions  = [PSCustomObject]@{ 'command(*)' = 'allow'; 'read_file(*)' = 'allow' }
    security            = [PSCustomObject]@{ enablePermanentToolApproval = $true }
    permissions         = [PSCustomObject]@{
        allow = @('command(*)','read_file(*)','write_file(*)','unsandboxed(*)','execute_url(*)','read_url(*)','mcp(*)')
        deny  = @()
    }
    hooks = [PSCustomObject]@{ PreToolUse = @() }
}
$r1 = [string[]]@(_Agy53_GetAgyLocalReasons -Json $compromisedMock)
Assert-Equal 'Case1: detected (reasons > 0)' ($r1.Count -gt 0) $true
Assert-Equal 'Case1: yolo reason present'    ($r1 -match 'yolo' | Select-Object -First 1) 'agy approvalMode=''yolo'''
Assert-Equal 'Case1: grantedPermissions reason present' ($r1 -match 'grantedPermissions' | Measure-Object).Count 1
Assert-Equal 'Case1: enablePermanentToolApproval reason' ($r1 -match 'enablePermanentToolApproval' | Measure-Object).Count 1
Assert-Equal 'Case1: total 3 reasons'        $r1.Count 3

# ---- CASE 2: healthy mock (ask, no grantedPermissions, enablePermanentToolApproval=false) ----
Write-Host "`n-- Case 2: healthy mock (ask, clean) --" -ForegroundColor Cyan
$healthyMock = [PSCustomObject]@{
    approvalMode = 'ask'
    security     = [PSCustomObject]@{ enablePermanentToolApproval = $false }
    permissions  = [PSCustomObject]@{ allow = @(); deny = @('unsandboxed(*)','execute_url(*)','read_url(*)','mcp(*)') }
    hooks        = [PSCustomObject]@{
        PreToolUse = @(
            [PSCustomObject]@{
                id    = 'harness'
                hooks = @([PSCustomObject]@{ type = 'command'; command = 'powershell ... wkharness-agy-main.ps1 -Family agy'; timeout = 30000 })
            }
        )
    }
}
$r2 = [string[]]@(_Agy53_GetAgyLocalReasons -Json $healthyMock)
Assert-Equal 'Case2: no reasons (clean)'    $r2.Count 0

# ---- CASE 3: parent settings compromised (yolo in ~/.gemini/settings.json) ----
Write-Host "`n-- Case 3: parent settings yolo bleed --" -ForegroundColor Cyan
$parentCompromised = [PSCustomObject]@{
    approvalMode = 'yolo'
}
$r3 = [string[]]@(_Agy53_GetCompromiseReasons -Json $parentCompromised -Label 'parent(.gemini)')
Assert-Equal 'Case3: parent yolo detected'  ($r3.Count -gt 0) $true
Assert-Equal 'Case3: reason contains label' ([bool]($r3[0] -match 'parent\(\.gemini\)')) $true

# ---- CASE 4: only enablePermanentToolApproval=true (no yolo, no grantedPermissions yet) ----
Write-Host "`n-- Case 4: enablePermanentToolApproval=true only --" -ForegroundColor Cyan
$eptaMock = [PSCustomObject]@{
    approvalMode = 'ask'
    security     = [PSCustomObject]@{ enablePermanentToolApproval = $true }
    permissions  = [PSCustomObject]@{ allow = @(); deny = @() }
}
$r4 = [string[]]@(_Agy53_GetAgyLocalReasons -Json $eptaMock)
Assert-Equal 'Case4: epta detected'         ($r4.Count -eq 1) $true
Assert-Equal 'Case4: reason is epta'        ([bool]($r4[0] -match 'enablePermanentToolApproval')) $true

# ---- CASE 5: null json (settings file absent) -> no false positive ----
Write-Host "`n-- Case 5: null json (file absent) --" -ForegroundColor Cyan
$r5 = [string[]]@(_Agy53_GetAgyLocalReasons -Json $null)
Assert-Equal 'Case5: null json -> 0 reasons' $r5.Count 0

# ---- CASE 6: grantedPermissions present but approvalMode=ask (cached single tool) ----
Write-Host "`n-- Case 6: grantedPermissions cached without yolo --" -ForegroundColor Cyan
$cachedMock = [PSCustomObject]@{
    approvalMode       = 'ask'
    grantedPermissions = @{ 'read_file(*)' = 'allow' }
    security           = [PSCustomObject]@{ enablePermanentToolApproval = $false }
}
$r6 = [string[]]@(_Agy53_GetAgyLocalReasons -Json $cachedMock)
Assert-Equal 'Case6: grantedPermissions detected even without yolo' ($r6.Count -ge 1) $true
Assert-Equal 'Case6: reason is grantedPermissions' ($r6 -match 'grantedPermissions' | Measure-Object).Count 1

# ---- Summary ----
Write-Host "`n=== RESULTS: $pass PASS, $fail FAIL ===" -ForegroundColor $(if ($fail -eq 0) { 'Green' } else { 'Red' })
if ($fail -gt 0) { exit 1 } else { exit 0 }
