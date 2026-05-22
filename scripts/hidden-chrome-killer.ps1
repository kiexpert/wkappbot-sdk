# hidden-chrome-killer.ps1 -- hidden Chrome = immediate kill, no questions asked
while ($true) {
    Get-Process chrome -ErrorAction SilentlyContinue |
        Where-Object { $_.MainWindowHandle -eq [IntPtr]::Zero } |
        ForEach-Object {
            Write-Host "$(Get-Date -Format 'HH:mm:ss')  KILL  hidden Chrome PID=$($_.Id)" -ForegroundColor Red
            & taskkill /PID $_.Id /F 2>$null | Out-Null
        }
    Start-Sleep 5
}