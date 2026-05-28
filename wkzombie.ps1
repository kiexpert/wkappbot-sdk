# wkzombie.ps1 -- one-shot zombie cleanup (wk*-safe, no infinite loop)
param([int]$MaxAge=45,[switch]$DryRun,[string]$CmdlineFilter="")
$z=@(Get-Process wkappbot-core -EA SilentlyContinue|Where-Object{
    $age=[int]((Get-Date)-$_.StartTime).TotalSeconds
    if($age-lt$MaxAge){return $false}
    if($CmdlineFilter){
        $cmd=(Get-CimInstance Win32_Process -Filter "ProcessId=$($_.Id)" -EA 0).CommandLine
        if(!$cmd-or$cmd-notlike"*$CmdlineFilter*"){return $false}
    }
    $true
})
if(!$z.Count){Write-Host "[wkzombie] clean (0 zombies, MaxAge=${MaxAge}s)";exit 0}
foreach($p in $z){
    $age=[int]((Get-Date)-$p.StartTime).TotalSeconds
    $cmd=(Get-CimInstance Win32_Process -Filter "ProcessId=$($p.Id)" -EA 0).CommandLine
    $short=if($cmd){$cmd.Substring(0,[Math]::Min(100,$cmd.Length))}else{"(no cmdline)"}
    if($DryRun){Write-Host "[wkzombie] DRY PID $($p.Id) $($p.Name) age=${age}s | $short"}
    else{Stop-Process -Id $p.Id -Force -EA 0;Write-Host "[wkzombie] killed PID $($p.Id) $($p.Name) age=${age}s | $short"}
}
if(!$DryRun){Write-Host "[wkzombie] done: $($z.Count) zombie(s)"}