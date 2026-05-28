# wktaskkill.ps1 -- wkappbot taskkill wrapper: auto-kills ZOMBIE+UNKNOWN, spares PROTECTED
# Usage: ./wktaskkill.ps1 /IM wkappbot-core.exe [extra args]
param([Parameter(ValueFromRemainingArguments)][string[]]$Args)
$out = & wkappbot taskkill @Args 2>&1
$out | ForEach-Object { Write-Host $_ }
$autoPids = @()
$out | ForEach-Object {
    if ($_ -match "\[(?:ZOMBIE|UNKNOWN)\]" -and $_ -match "PID (\d+)") {
        $autoPids += [int]$Matches[1]
    }
}
if ($autoPids.Count -gt 0) {
    Write-Host "[wktaskkill] auto-killing $($autoPids.Count) ZOMBIE/UNKNOWN: $($autoPids -join ",")"
    & wkappbot taskkill --force ($autoPids -join ",") 2>&1 | ForEach-Object { Write-Host $_ }
}