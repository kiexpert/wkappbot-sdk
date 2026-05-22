# hidden-chrome-killer.ps1 -- kill hidden Chrome windows immediately
while ($true) {
    $cdpProcs = netstat -ano 2>$null |
        Select-String ":9[3-9]\d\d\s" |
        ForEach-Object { ($_ -split '\s+')[-1] } |
        Where-Object { $_ -match '^\d+$' } |
        Select-Object -Unique

    foreach ($pid in $cdpProcs) {
        try {
            $p = Get-Process -Id $pid -ErrorAction Stop
            if ($p.Name -ne 'chrome') { continue }
            if ($p.MainWindowHandle -eq [IntPtr]::Zero) {
                Write-Host "$(Get-Date -Format 'HH:mm:ss')  KILL  Hidden Chrome PID=$pid" -ForegroundColor Red
                & taskkill /PID $pid /F 2>$null | Out-Null
            }
        } catch {}
    }
    Start-Sleep 5
}