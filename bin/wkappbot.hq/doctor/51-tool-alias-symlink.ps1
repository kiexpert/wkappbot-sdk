# wkdoctor check: direct agy.exe alias integrity and auto-repair
# Doctor only diagnoses and re-checks; the install process owns link creation.

function Normalize-PathForCompare {
    param([string]$Path)
    if ([string]::IsNullOrWhiteSpace($Path)) { return '' }
    try {
        return ([System.IO.Path]::GetFullPath($Path)).TrimEnd('\','/').ToLowerInvariant()
    } catch {
        return ([string]$Path).TrimEnd('\','/').ToLowerInvariant()
    }
}

function Get-LinkTargetText {
    param([Parameter(Mandatory)]$Entry)

    $value = $null
    try { $value = $Entry.LinkTarget } catch {}
    if ([string]::IsNullOrWhiteSpace([string]$value)) {
        try { $value = $Entry.Target } catch {}
    }
    if ($value -is [System.Array]) {
        $value = $value[0]
    }
    return [string]$value
}

function Test-ToolAliasLinkHealth {
    param(
        [Parameter(Mandatory)][string]$LinkPath,
        [Parameter(Mandatory)][string]$TargetPath
    )

    $entry = Get-Item -LiteralPath $LinkPath -Force -ErrorAction SilentlyContinue
    if (-not $entry) {
        return @{ Healthy = $false; Target = '' }
    }

    $isLink = ($entry.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0
    $actualTarget = Get-LinkTargetText $entry
    $actualResolved = Normalize-PathForCompare $actualTarget
    $expectedResolved = Normalize-PathForCompare $TargetPath
    $healthy = $isLink -and (Test-Path -LiteralPath $LinkPath -PathType Leaf) -and ($actualResolved -eq $expectedResolved)

    return @{ Healthy = $healthy; Target = $actualTarget }
}

function Get-WkAppBotBinRoot {
    $pathValue = [Environment]::GetEnvironmentVariable('PATH', 'Process')
    foreach ($dir in ($pathValue -split ';')) {
        if ([string]::IsNullOrWhiteSpace($dir)) {
            continue
        }
        $root = $dir.Trim()
        foreach ($candidate in @('wkappbot.exe', 'wkappbot.cmd', 'wkappbot')) {
            $path = Join-Path $root $candidate
            if (Test-Path -LiteralPath $path -PathType Leaf) {
                return [System.IO.Path]::GetFullPath($root)
            }
        }
    }

    return $null
}

function Get-AgyInstallExePath {
    $target = Join-Path $env:LOCALAPPDATA 'agy\bin\agy.exe'
    if (Test-Path -LiteralPath $target -PathType Leaf) {
        return [System.IO.Path]::GetFullPath($target)
    }
    return $null
}

function Get-RgExePath {
    # Discover a bundled ripgrep (VS Code / chatgpt-codex extension ship rg.exe). GLOB -- never
    # hardcode the version-stamped extension path (it changes on every update). Newest wins.
    $globs = @(
        (Join-Path $env:USERPROFILE '.vscode\extensions\*\bin\windows-x86_64\rg.exe'),
        (Join-Path $env:USERPROFILE '.vscode\extensions\*\bin\windows-x86_64\codex-path\rg.exe'),
        (Join-Path $env:USERPROFILE '.vscode\extensions\*\node_modules\@vscode\ripgrep\bin\rg.exe')
    )
    $hits = foreach ($g in $globs) { Get-ChildItem -Path $g -ErrorAction SilentlyContinue }
    $newest = $hits | Sort-Object LastWriteTime -Descending | Select-Object -First 1
    if ($newest) { return [System.IO.Path]::GetFullPath($newest.FullName) }
    return $null
}

$script:WrapperInstallRan = $false

function Invoke-WrapperInstall {
    param(
        [Parameter(Mandatory)][string]$Reason
    )

    if ($script:WrapperInstallRan) {
        return
    }

    $installerCandidates = @(
        (Join-Path 'D:\GitHub\wkappbot-kih\tools' 'wkharness-session-init.ps1')
    )
    if ($env:WKHARNESS_REPO_ROOT) {
        $installerCandidates = @(
            (Join-Path $env:WKHARNESS_REPO_ROOT 'tools\wkharness-session-init.ps1')
        ) + $installerCandidates
    }

    $installer = $installerCandidates | Where-Object { $_ -and (Test-Path -LiteralPath $_ -PathType Leaf) } | Select-Object -First 1
    if (-not $installer) {
        throw "wrapper installer not found for repair request: $Reason"
    }

    $psExe = Join-Path $env:SystemRoot 'System32\WindowsPowerShell\v1.0\powershell.exe'
    & $psExe -NoProfile -NonInteractive -ExecutionPolicy Bypass -File $installer 2>&1 | ForEach-Object {
        Write-Host "  [wrapper-install] $_" -ForegroundColor DarkGray
    }
    if ($LASTEXITCODE -ne 0) {
        throw "wrapper installer failed with exit $LASTEXITCODE"
    }
    $script:WrapperInstallRan = $true
}

function Ensure-ToolAliasLink {
    param(
        [Parameter(Mandatory)][string]$LinkPath,
        [Parameter(Mandatory)][string]$TargetPath,
        [Parameter(Mandatory)][string]$Label
    )

    $linkResolved = [System.IO.Path]::GetFullPath($LinkPath)
    $targetResolved = [System.IO.Path]::GetFullPath($TargetPath)
    $targetExists = Test-Path -LiteralPath $targetResolved -PathType Leaf

    if (-not $targetExists) {
        Add-Check $Label 'fail' "repair source missing: $targetResolved"
        Emit 'fail' $Label "repair source missing: $targetResolved"
        return
    }

    $state = Test-ToolAliasLinkHealth -LinkPath $linkResolved -TargetPath $targetResolved
    if (-not (Get-Item -LiteralPath $linkResolved -Force -ErrorAction SilentlyContinue)) {
        Emit 'warn' $Label "missing -- reconnecting -> $targetResolved"
    } elseif ($state.Healthy) {
        Add-Check $Label 'ok' "symlink -> $($state.Target)"
        Emit 'ok' $Label "symlink -> $($state.Target)"
        return
    } else {
        Emit 'warn' $Label "broken or mismatched -> $($state.Target)"
    }

    try {
        Invoke-WrapperInstall -Reason $Label
        $recheck = Test-ToolAliasLinkHealth -LinkPath $linkResolved -TargetPath $targetResolved
        if ($recheck.Healthy) {
            Add-Check $Label 'ok' "auto-repaired via wrapper install -> $targetResolved"
            Emit 'ok' $Label "auto-repaired via wrapper install -> $targetResolved"
        } else {
            throw "wrapper install completed but $linkResolved still targets '$($recheck.Target)'"
        }
    } catch {
        Add-Check $Label 'fail' "repair failed via wrapper install: $_"
        Emit 'fail' $Label "repair failed via wrapper install: $_"
    }
}

$doctorDir = $PSScriptRoot
$doctorBin = [System.IO.Path]::GetFullPath((Split-Path -Parent (Split-Path -Parent $doctorDir)))
$pathBin = Get-WkAppBotBinRoot
$rootLabel = 'PATH appbot bin'
if (-not $pathBin) {
    $pathBin = $doctorBin
    $rootLabel = 'doctor folder'
}
$agyInstallExe = Get-AgyInstallExePath
$aliasRoots = @(
    @{ Root = $pathBin; Label = $rootLabel }
)

foreach ($root in $aliasRoots) {
    $rootPath = [System.IO.Path]::GetFullPath($root.Root)
    $aliasPairs = @(
        @{ Label = "$($root.Label) agy.exe alias";      Link = (Join-Path $rootPath 'agy.exe');      Target = $agyInstallExe }
    )

    foreach ($pair in $aliasPairs) {
        Ensure-ToolAliasLink -LinkPath $pair.Link -TargetPath $pair.Target -Label $pair.Label
    }
}

# rg.exe alias (user 2026-06-17): when rg.exe is absent from PATH, gemini falls back to a slow
# GrepTool (the doctor's "gemini ripgrep -- rg.exe is missing" warn). Symlink the bundled VS
# Code/codex ripgrep into the appbot bin (copy-fallback if symlink is unprivileged -- New-Item
# SymbolicLink needs admin/developer-mode). Self-heals every doctor run. Glob-discovered target.
$rgTarget = Get-RgExePath
$rgLink   = Join-Path ([System.IO.Path]::GetFullPath($pathBin)) 'rg.exe'
if (-not $rgTarget) {
    Add-Check 'rg.exe alias' 'warn' 'no bundled rg.exe found (VS Code / codex ripgrep absent) -- gemini grep falls back to GrepTool (lag)'
    Emit 'warn' 'rg.exe alias' 'no bundled rg.exe found'
} elseif (Test-Path -LiteralPath $rgLink -PathType Leaf) {
    Add-Check 'rg.exe alias' 'ok' "present at $rgLink"
    Emit 'ok' 'rg.exe alias' "rg.exe present (gemini grep fast-path)"
} else {
    $rgDone = $false
    try {
        New-Item -ItemType SymbolicLink -Path $rgLink -Target $rgTarget -Force -ErrorAction Stop | Out-Null
        $rgDone = $true
        Add-Check 'rg.exe alias' 'ok' "symlinked -> $rgTarget"
        Emit 'ok' 'rg.exe alias' "rg.exe symlinked -> $rgTarget"
    } catch {
        try {
            Copy-Item -LiteralPath $rgTarget -Destination $rgLink -Force -ErrorAction Stop
            $rgDone = $true
            Add-Check 'rg.exe alias' 'ok' "copied (symlink unprivileged) -> $rgTarget"
            Emit 'ok' 'rg.exe alias' "rg.exe copied into bin (symlink needs admin/dev-mode) -> $rgTarget"
        } catch {
            Add-Check 'rg.exe alias' 'warn' "could not symlink/copy rg.exe (fail-open): $_"
            Emit 'warn' 'rg.exe alias' "rg.exe install failed (fail-open): $_"
        }
    }
}
