# 52-wkwrap-important-tools.ps1 -- wkdoctor reconnects important wk* tools to wkwrap.
# Policy: repair only in the bin that already owns the sibling <tool>.ps1. Do not move
# implementations between USER (wkappbot-sdk/bin) and DEV (WKAppBot/bin) locations.
# Skill: wkwrap-important-tool-connection-doctor-reconnect

$importantWkTools = @(
    'wkpace','wkhippo','wkagent-name','wktasklist','wkdoctor','wkcdp','wkask','wkedit',
    'wkcodex','wkclaude','wkjobs','wkkill','wkfind','wkspeak','wksplit','wkpush'  # wkgemini removed 2026-06-22 (Gemini CLI EOL)
)

$binPlans = @(
    [pscustomobject]@{ Tier='USER'; Bin='D:\GitHub\wkappbot-sdk\bin' },
    [pscustomobject]@{ Tier='DEV';  Bin='D:\GitHub\WKAppBot\bin' }
)

function Test-WkSameFileHash {
    param([string]$A, [string]$B)
    try {
        if (-not (Test-Path -LiteralPath $A) -or -not (Test-Path -LiteralPath $B)) { return $false }
        return ((Get-FileHash -LiteralPath $A -Algorithm SHA256).Hash -eq (Get-FileHash -LiteralPath $B -Algorithm SHA256).Hash)
    } catch { return $false }
}

function Repair-WkWrapMember {
    param(
        [string]$Source,
        [string]$Destination,
        [string]$Tool,
        [string]$Member
    )
    try {
        if (-not (Test-Path -LiteralPath $Source)) { return "missing-source:${Member}" }
        $needsCopy = $true
        if (Test-Path -LiteralPath $Destination) {
            $needsCopy = -not (Test-WkSameFileHash -A $Source -B $Destination)
        }
        if ($needsCopy) {
            Copy-Item -LiteralPath $Source -Destination $Destination -Force
            return "repaired:${Member}"
        }
        return "ok:${Member}"
    } catch {
        return "failed:${Member}:$($_.Exception.Message)"
    }
}

foreach ($plan in $binPlans) {
    $bin = [string]$plan.Bin
    $tier = [string]$plan.Tier
    if (-not (Test-Path -LiteralPath $bin -PathType Container)) {
        Add-Check "wkwrap:${tier}" 'warn' "bin missing: $bin"
        Emit 'warn' "wkwrap:${tier}" "bin missing: $bin"
        continue
    }

    $wrapExe = Join-Path $bin 'wkwrap.exe'
    $wrapCmd = Join-Path $bin 'wkwrap.cmd'
    $wrapSh  = Join-Path $bin 'wkwrap.sh'
    if (-not ((Test-Path -LiteralPath $wrapExe) -and (Test-Path -LiteralPath $wrapCmd) -and (Test-Path -LiteralPath $wrapSh))) {
        Add-Check "wkwrap:${tier}:source" 'fail' "wkwrap source trio missing in $bin"
        Emit 'fail' "wkwrap:${tier}:source" "wkwrap source trio missing in $bin"
        continue
    }

    foreach ($tool in $importantWkTools) {
        $ps1 = Join-Path $bin "$tool.ps1"
        if (-not (Test-Path -LiteralPath $ps1)) {
            Add-Check "wkwrap:${tier}:${tool}" 'warn' "no sibling $tool.ps1 in $bin; not reconnecting across tiers"
            Emit 'warn' "wkwrap:${tier}:${tool}" "no sibling ps1; skipped"
            continue
        }

        $cmd = Join-Path $bin "$tool.cmd"
        $sh  = Join-Path $bin "$tool.sh"
        $exe = Join-Path $bin "$tool.exe"
        $actions = @(
            (Repair-WkWrapMember -Source $wrapCmd -Destination $cmd -Tool $tool -Member 'cmd'),
            (Repair-WkWrapMember -Source $wrapSh  -Destination $sh  -Tool $tool -Member 'sh'),
            (Repair-WkWrapMember -Source $wrapExe -Destination $exe -Tool $tool -Member 'exe')
        )

        $cmdOk = Test-WkSameFileHash -A $wrapCmd -B $cmd
        $shOk  = Test-WkSameFileHash -A $wrapSh  -B $sh
        $exeOk = Test-WkSameFileHash -A $wrapExe -B $exe
        $status = if ($cmdOk -and $shOk -and $exeOk) { 'ok' } else { 'fail' }
        $detail = "ps1=ok cmd=$cmdOk sh=$shOk exe=$exeOk actions=$($actions -join ',')"
        Add-Check "wkwrap:${tier}:${tool}" $status $detail
        Emit $status "wkwrap:${tier}:${tool}" $detail
    }
}
