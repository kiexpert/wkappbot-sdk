# hidden-chrome-killer.ps1 -- kill hidden MAIN Chrome browser (not worker processes)
# Worker processes (renderer/GPU/network) have --type= flag -- skip those
# Main browser process has no --type= and should have a visible window
while ($true) {
    try {
        Get-WmiObject Win32_Process -Filter "Name='chrome.exe'" |
            Where-Object { $_.CommandLine -notmatch '--type=' } |
            ForEach-Object {
                $p = Get-Process -Id $_.ProcessId -ErrorAction SilentlyContinue
                if ($p -and $p.MainWindowHandle -eq [IntPtr]::Zero) {
                    Write-Host "$(Get-Date -Format 'HH:mm:ss')  KILL  hidden Chrome PID=$($_.ProcessId) (no window, main process)" -ForegroundColor Red
                    & taskkill /PID $_.ProcessId /F 2>$null | Out-Null
                }
            }
    } catch {}
    Start-Sleep 5
}