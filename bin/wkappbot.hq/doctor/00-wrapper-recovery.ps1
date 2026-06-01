# wkdoctor check: wrapper recovery -- personal-docs wkwrap.exe hot-swap
# Heal: rebuild the wrapper exe from source, preserve backup, restore on failure.

$wrapperRoot = $env:WKWRAP_HOTSWAP_ROOT
if (-not $wrapperRoot) {
    $wrapperRoot = 'D:\GitHub\personal-docs\tools\wrappers'
}

$sourcePath = Join-Path $wrapperRoot 'wkwrap.cs'
$targetPath = Join-Path $wrapperRoot 'wkwrap.exe'
$backupPath = Join-Path $wrapperRoot 'wkwrap.exe.old'
$tempPath   = Join-Path $wrapperRoot 'wkwrap.exe.new'
$cscCandidates = @(
    'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe',
    'C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe'
)

function Test-PortableExe {
    param([string]$Path)

    if (-not (Test-Path -LiteralPath $Path -PathType Leaf)) {
        return $false
    }

    try {
        $fs = [System.IO.File]::OpenRead($Path)
        try {
            if ($fs.Length -lt 2) {
                return $false
            }
            return ($fs.ReadByte() -eq 0x4D -and $fs.ReadByte() -eq 0x5A)
        }
        finally {
            $fs.Dispose()
        }
    }
    catch {
        return $false
    }
}

function Test-LfsPointer {
    param([string]$Path)

    try {
        $head = Get-Content -LiteralPath $Path -Encoding UTF8 -TotalCount 2 -ErrorAction Stop
        return (($head -join "`n") -like 'version https://git-lfs.github.com/spec/v1*')
    }
    catch {
        return $false
    }
}

function Invoke-WkwrapHotSwap {
    param(
        [string]$Source,
        [string]$Target,
        [string]$Backup,
        [string]$Temp,
        [string]$Csc
    )

    if (Test-Path -LiteralPath $Temp) {
        Remove-Item -LiteralPath $Temp -Force -ErrorAction SilentlyContinue
    }

    if (Test-Path -LiteralPath $Target) {
        if (Test-Path -LiteralPath $Backup) {
            Remove-Item -LiteralPath $Backup -Force -ErrorAction SilentlyContinue
        }
        Move-Item -LiteralPath $Target -Destination $Backup -Force -ErrorAction Stop
    }

    & $Csc /nologo /target:exe "/out:$Temp" $Source *> $null
    if ($LASTEXITCODE -ne 0 -or -not (Test-PortableExe -Path $Temp)) {
        throw 'rebuild failed'
    }

    Move-Item -LiteralPath $Temp -Destination $Target -Force -ErrorAction Stop
    if (-not (Test-PortableExe -Path $Target)) {
        throw 'install failed'
    }
}

$hasSource = Test-Path -LiteralPath $sourcePath -PathType Leaf
$targetOk = Test-PortableExe -Path $targetPath
$targetBroken = (-not $targetOk) -or (Test-LfsPointer -Path $targetPath)
$usedCsc = $null
foreach ($cand in $cscCandidates) {
    if (Test-Path -LiteralPath $cand -PathType Leaf) {
        $usedCsc = $cand
        break
    }
}

if ($targetOk -and -not $targetBroken) {
    Add-Check 'wkwrap.exe recovery' 'ok' 'healthy'
    Emit 'ok' 'wkwrap.exe recovery' ''
}
elseif (-not $hasSource) {
    Add-Check 'wkwrap.exe recovery' 'fail' 'source missing'
    Emit 'fail' 'wkwrap.exe recovery' ''
}
elseif (-not $usedCsc) {
    Add-Check 'wkwrap.exe recovery' 'fail' 'csc missing'
    Emit 'fail' 'wkwrap.exe recovery' ''
}
else {
    try {
        Invoke-WkwrapHotSwap -Source $sourcePath -Target $targetPath -Backup $backupPath -Temp $tempPath -Csc $usedCsc
        Add-Check 'wkwrap.exe recovery' 'ok' 'recovered'
        Emit 'ok' 'wkwrap.exe recovery' ''
    }
    catch {
        if (Test-PortableExe -Path $backupPath) {
            try {
                Copy-Item -LiteralPath $backupPath -Destination $targetPath -Force -ErrorAction Stop
            }
            catch {
                # Keep the failure surface terse; the check status below is enough.
            }
        }
        Add-Check 'wkwrap.exe recovery' 'fail' 'recovery failed'
        Emit 'fail' 'wkwrap.exe recovery' ''
    }
}
