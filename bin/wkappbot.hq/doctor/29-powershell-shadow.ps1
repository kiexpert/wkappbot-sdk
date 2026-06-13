# wkdoctor check: powershell-shadow -- detect PowerShell.cmd/.ps1 shims in PATH that shadow
# the real C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe. Such shims break harness
# hooks that invoke the shell directly. Auto-fix: rename to .bak (NOT delete). FAIL-OPEN.

try {
    $sysShell = 'C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe'
    $pathDirs = ($env:PATH -split ';') | Where-Object { $_ -ne '' }
    $sys32Pos = -1
    for ($i = 0; $i -lt $pathDirs.Count; $i++) {
        if ($pathDirs[$i] -match '(?i)System32\WindowsPowerShell') { $sys32Pos = $i; break }
    }
    $shadows = @()
    foreach ($dir in $pathDirs) {
        if (-not (Test-Path -LiteralPath $dir)) { continue }
        $pos = [array]::IndexOf($pathDirs, $dir)
        foreach ($ext in @('.cmd', '.ps1')) {
            $f = Join-Path $dir ('powershell' + $ext)
            if (Test-Path -LiteralPath $f) {
                if ($sys32Pos -lt 0 -or $pos -lt $sys32Pos) { $shadows += $f }
            }
        }
    }
    foreach ($dir in @('D:\GitHub\WKAppBot\bin', 'D:\GitHub\wkappbot-sdk\bin')) {
        if (-not (Test-Path -LiteralPath $dir)) { continue }
        foreach ($ext in @('.cmd', '.ps1')) {
            $f = Join-Path $dir ('powershell' + $ext)
            if ((Test-Path -LiteralPath $f) -and ($shadows -notcontains $f)) { $shadows += $f }
        }
    }
    if ($shadows.Count -eq 0) {
        Add-Check 'powershell-shadow' 'ok' 'no shim shadowing system shell.exe'
        Emit 'ok' 'powershell-shadow' 'no shim shadowing system shell.exe'
    } else {
        $fixed = @(); $failed = @()
        foreach ($shadow in $shadows) {
            Emit '!' 'powershell-shadow' "shadow found: $shadow"
            try {
                Rename-Item -LiteralPath $shadow -NewName ($shadow + '.bak') -Force -ErrorAction Stop
                $fixed += $shadow
                Emit 'ok' 'powershell-shadow:fixed' "renamed to .bak: $shadow"
            } catch {
                $failed += $shadow
                Emit '!' 'powershell-shadow:fix' "could not rename $shadow -- $_"
            }
        }
        if ($failed.Count -eq 0) {
            Add-Check 'powershell-shadow' 'ok' "shadow(s) found and .bak'd: $($fixed -join ', ')"
        } else {
            Add-Check 'powershell-shadow' 'warn' "$($failed.Count) shadow(s) could not be renamed"
        }
    }
} catch {
    Add-Check 'powershell-shadow' 'ok' "n/a (check error, fail-open): $_"
    Emit 'ok' 'powershell-shadow' 'n/a (check error, fail-open)'
}
