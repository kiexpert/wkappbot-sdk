# hidden-chrome-killer.ps1
# Kill hidden Chrome that holds wkappbot CDP ports (9300-9995)
# Reason: hidden Chrome grabs CDP port + keeps playing audio, blocking project tabs
while ($true) {
    # Find Chrome PIDs holding wkappbot CDP ports
    $lines = netstat -ano 2>$null | Select-String "LISTENING" |
        Where-Object { $_ -match ':9([3-9]\d\d)\s' }

    foreach ($line in $lines) {
        if ($line -notmatch ':9([3-9]\d\d)\s.*?(\d+)$') { continue }
        $port = $Matches[1]
        $pid  = [int]$Matches[2]
        try {
            $p = Get-Process -Id $pid -ErrorAction Stop
            if ($p.Name -ne 'chrome') { continue }
            # Hidden = no window handle = holding port silently
            if ($p.MainWindowHandle -eq [IntPtr]::Zero) {
                Write-Host "$(Get-Date -Format 'HH:mm:ss')  KILL  Chrome PID=$pid port=9$port (hidden+CDP port grab)" -ForegroundColor Red
                & taskkill /PID $pid /F 2>$null | Out-Null
            }
        } catch {}
    }
    Start-Sleep 5
}