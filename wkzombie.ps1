# wkzombie.ps1 -- one-shot zombie cleanup (wk*-safe). Use -ExcludeFilter to skip services (e.g. mcp).
param([int]$MaxAge=45,[switch]$DryRun,[string]$CmdlineFilter="",[string]$ExcludeFilter="")
$z=@(Get-Process wkappbot-core -EA SilentlyContinue|Where-Object{
    $age=[int]((Get-Date)-$_.StartTime).TotalSeconds
    if($age-lt$MaxAge){return $false}
    if($CmdlineFilter){
        $cmd=(Get-CimInstance Win32_Process -Filter "ProcessId=$($_.Id)" -EA 0).CommandLine
        if(!$cmd-or$cmd-notlike"*$CmdlineFilter*"){return $false}
    }
    if($ExcludeFilter){
        $ex=(Get-CimInstance Win32_Process -Filter "ProcessId=$($_.Id)" -EA 0).CommandLine
        if($ex -and $ex -like "*$ExcludeFilter*"){return $false}
    }
    $true
})
if(!$z.Count){Write-Host "[wkzombie] clean (0 zombies, MaxAge=${MaxAge}s)";exit 0}
foreach($p in $z){
    $age=[int]((Get-Date)-$p.StartTime).TotalSeconds
    $cmd=(Get-CimInstance Win32_Process -Filter "ProcessId=$($p.Id)" -EA 0).CommandLine
    $short=if($cmd){$cmd.Substring(0,[Math]::Min(100,$cmd.Length))}else{"(no cmdline)"}
    if($DryRun){Write-Host "[wkzombie] DRY PID $($p.Id) $($p.Name) age=${age}s | $short"}
    else{Stop-Process -Id $p.Id -Force -EA 0;& "$env:SystemRoot/System32/taskkill.exe" /F /PID $p.Id 2>$null|Out-Null;Write-Host "[wkzombie] killed PID $($p.Id) $($p.Name) age=${age}s | $short"}
}
if(!$DryRun){Write-Host "[wkzombie] done: $($z.Count) zombie(s)"}