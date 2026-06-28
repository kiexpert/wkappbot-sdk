# PROTO for doctor/50-codex-symlink.ps1
# FIX 2026-06-13: "missing entirely" branch now attempts repair (was silently skipping)

$findRepairSource = {
    $src = $null
    $vscodeExts = Get-ChildItem "$env:USERPROFILE\.vscode\extensions" -Filter 'openai.chatgpt-*' -Directory -ErrorAction SilentlyContinue | Sort-Object Name -Descending
    if ($vscodeExts.Count -gt 0) {
        $cand = Join-Path $vscodeExts[0].FullName 'bin\windows-x86_64\codex.exe'
        if (Test-Path $cand -ErrorAction SilentlyContinue) { $src = $cand }
    }
    if (-not $src) {
        $fallback = Join-Path $env:USERPROFILE '.codex\.sandbox-bin\codex.exe'
        if (Test-Path $fallback -ErrorAction SilentlyContinue) { $src = $fallback }
    }
    $src
}

$candidates = @(
    'D:/GitHub/WKAppBot/bin/codex.exe',
    'D:/GitHub/wkappbot-sdk/bin/codex.exe'
)

foreach ($path in $candidates) {
    $pathResolved = [System.IO.Path]::GetFullPath($path)
    $fileInfo = Get-Item -LiteralPath $pathResolved -Force -ErrorAction SilentlyContinue
    $isLink = $fileInfo -and (($fileInfo.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0)
    if ($fileInfo -and -not $isLink) {
        $siblingHelper = Join-Path (Split-Path $pathResolved -Parent) 'codex-windows-sandbox-setup.exe'
        if (Test-Path -LiteralPath $siblingHelper -ErrorAction SilentlyContinue) {
            Add-Check "codex symlink: $path" 'ok' "real file (sandbox helper present)"
            Emit 'ok' "codex symlink: $path" 'real file (sandbox helper present)'
        } else {
            Add-Check "codex symlink: $path" 'warn' "real file but sandbox helper missing"
            Emit 'warn' "codex symlink: $path" 'real file but sandbox helper missing'
        }
        continue
    }
    $repairSource = & $findRepairSource
    if (-not $repairSource) {
        Add-Check "codex symlink: $path" 'fail' 'no repair source found (vscode ext + ~/.codex/.sandbox-bin both missing)'
        Emit 'fail' "codex symlink: $path" 'no repair source found'
        continue
    }
    try {
        if ($fileInfo) { Remove-Item -LiteralPath $pathResolved -Force -ErrorAction Stop }
        New-Item -ItemType SymbolicLink -Path $pathResolved -Target $repairSource -Force -ErrorAction Stop | Out-Null
        Add-Check "codex symlink: $path" 'ok' "symlink -> $repairSource"
        Emit 'ok' "codex symlink: $path" "symlink -> $repairSource"
    } catch {
        Add-Check "codex symlink: $path" 'fail' "repair failed: $_"
        Emit 'fail' "codex symlink: $path" "repair failed: $_"
    }
}
