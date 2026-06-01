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
        try {
            Move-Item -LiteralPath $Target -Destination $Backup -Force -ErrorAction Stop
        }
        catch {
            throw "failed to park current wrapper as .old: $($_.Exception.Message)"
        }
    }

    & $Csc /nologo /target:exe "/out:$Temp" $Source
    if ($LASTEXITCODE -ne 0 -or -not (Test-PortableExe -Path $Temp)) {
        throw "wrapper rebuild failed (csc exit $LASTEXITCODE)"
    }

    try {
        Move-Item -LiteralPath $Temp -Destination $Target -Force -ErrorAction Stop
    }
    catch {
        throw "failed to install rebuilt wrapper: $($_.Exception.Message)"
    }

    if (-not (Test-PortableExe -Path $Target)) {
        throw "installed wrapper is not a valid PE"
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
    Add-Check 'wkwrap.exe recovery' 'ok' 'already healthy'
    Emit 'ok' 'wkwrap.exe recovery' $targetPath
}
elseif (-not $hasSource) {
    Add-Check 'wkwrap.exe recovery' 'fail' "source missing: $sourcePath"
    Emit 'fail' 'wkwrap.exe recovery' 'source missing'
}
elseif (-not $usedCsc) {
    Add-Check 'wkwrap.exe recovery' 'fail' 'csc.exe not found'
    Emit 'fail' 'wkwrap.exe recovery' 'csc.exe missing'
}
else {
    try {
        Invoke-WkwrapHotSwap -Source $sourcePath -Target $targetPath -Backup $backupPath -Temp $tempPath -Csc $usedCsc
        Add-Check 'wkwrap.exe recovery' 'ok' "self-healed from $sourcePath"
        Emit 'ok' 'wkwrap.exe recovery' "self-healed via $usedCsc"
    }
    catch {
        Add-Check 'wkwrap.exe recovery' 'fail' $_.Exception.Message
        Emit 'fail' 'wkwrap.exe recovery' $_.Exception.Message
        if (Test-PortableExe -Path $backupPath) {
            try {
                Copy-Item -LiteralPath $backupPath -Destination $targetPath -Force -ErrorAction Stop
                if (Test-PortableExe -Path $targetPath) {
                    Add-Check 'wkwrap.exe rollback' 'warn' 'restored from .old backup'
                    Emit '!' 'wkwrap.exe rollback' 'restored from .old backup'
                }
            }
            catch {
                Add-Check 'wkwrap.exe rollback' 'warn' "rollback failed: $($_.Exception.Message)"
                Emit '!' 'wkwrap.exe rollback' 'rollback failed'
            }
        }
    }
}
