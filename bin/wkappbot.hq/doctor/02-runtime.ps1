# wkdoctor check: runtime -- Eye daemon, .NET SDK

# Check 4: Eye daemon
$coreProcs = @(Get-Process -Name 'wkappbot-core' -ErrorAction SilentlyContinue)
if ($coreProcs.Count -gt 0) {
    $pid1 = $coreProcs[0].Id
    Add-Check 'Eye (daemon)' 'ok' "PID $pid1 ($($coreProcs.Count) instance(s))"
    Emit 'ok' 'Eye (daemon)' "PID $pid1"
} else {
    Add-Check 'Eye (daemon)' 'warn' 'not running -- run: wkappbot eye'
    Emit '!' 'Eye (daemon)' 'not running'
}

# Check 5: .NET SDK
$dotnet = 'C:\Program Files\dotnet\dotnet.exe'
if (Test-Path $dotnet -PathType Leaf) {
    $dnRaw = (& $dotnet --version 2>&1 | Select-Object -First 1)
    $dnVer = if ($null -ne $dnRaw) { "$dnRaw".Trim() } else { "" }
    if (-not $dnVer) {
        Add-Check ".NET SDK" "warn" "dotnet --version returned nothing -- winget install Microsoft.DotNet.SDK.8"
        Emit "!" ".NET SDK" "version query failed"
        return
    }
    $major = 0; try { $major = [int]($dnVer -split '\.')[0] } catch {}
    if ($major -ge 8) {
        Add-Check '.NET SDK' 'ok' $dnVer
        Emit 'ok' '.NET SDK' $dnVer
    } else {
        Add-Check '.NET SDK' 'warn' "found $dnVer (need 8+) -- winget install Microsoft.DotNet.SDK.8"
        Emit '!' '.NET SDK' "found $dnVer (need 8+)"
    }
} else {
    Add-Check '.NET SDK' 'fail' 'not found -- winget install Microsoft.DotNet.SDK.8'
    Emit 'fail' '.NET SDK' 'install .NET 8 SDK'
}
