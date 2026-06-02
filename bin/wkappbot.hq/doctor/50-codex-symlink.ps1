# wkdoctor check: codex.exe symlink integrity and auto-repair
# Detects broken symlinks to codex and auto-repairs them

$candidates = @(
    'D:/GitHub/WKAppBot/bin/codex.exe',
    'D:/GitHub/wkappbot-sdk/bin/codex.exe'
)

foreach ($path in $candidates) {
    $pathResolved = [System.IO.Path]::GetFullPath($path)

    # Get-Item -Force returns the symlink entry itself even when the target is
    # missing. We MUST NOT gate on Test-Path -PathType Leaf first -- that
    # returns false for a broken symlink and the entry gets mis-classified as
    # 'missing entirely', skipping auto-repair.
    $fileInfo = Get-Item -LiteralPath $pathResolved -Force -ErrorAction SilentlyContinue

    # Truly missing: not even the symlink entry exists.
    if (-not $fileInfo) {
        Add-Check "codex symlink: $path" 'warn' "missing entirely"
        Emit 'warn' "codex symlink: $path" 'missing entirely'
        continue
    }

    $isLink = ($fileInfo.Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0

    # Plain real file -- also verify sibling sandbox helper exists, otherwise
    # codex.exe dies immediately on launch with 'spawn setup refresh'.
    if (-not $isLink) {
        $siblingHelper = Join-Path (Split-Path $pathResolved -Parent) 'codex-windows-sandbox-setup.exe'
        if (Test-Path -LiteralPath $siblingHelper -ErrorAction SilentlyContinue) {
            Add-Check "codex symlink: $path" 'ok' "real file (sandbox helper present)"
            Emit 'ok' "codex symlink: $path" 'real file (sandbox helper present)'
        } else {
            Add-Check "codex symlink: $path" 'warn' "real file but sandbox helper missing -- codex dies on launch"
            Emit 'warn' "codex symlink: $path" 'real file but sandbox helper missing -- codex dies on launch'
        }
        continue
    }

    # It is a symlink. PS5.1 LinkTarget may be empty; fall back to .Target.
    $linkTarget = $fileInfo.LinkTarget
    if ([string]::IsNullOrEmpty($linkTarget)) {
        try { $linkTarget = (Get-Item -LiteralPath $pathResolved).Target } catch { $linkTarget = '<unknown>' }
    }
    # Traverse the link to probe target existence.
    $targetExists = $false
    try { $targetExists = [System.IO.File]::Exists($pathResolved) } catch {}
    if ($targetExists) {
        Add-Check "codex symlink: $path" 'ok' "symlink -> $linkTarget"
        Emit 'ok' "codex symlink: $path" "symlink -> $linkTarget"
        continue
    }

    # Symlink is broken, attempt auto-repair
    Emit 'warn' "codex symlink: $path" "broken symlink -> $linkTarget"

    $repairSource = $null

    # Try: latest VS Code openai.chatgpt extension
    $vscodeExtensions = Get-ChildItem "$env:USERPROFILE\.vscode\extensions" -Filter 'openai.chatgpt-*' -Directory -ErrorAction SilentlyContinue | Sort-Object Name -Descending
    if ($vscodeExtensions.Count -gt 0) {
        $candidate = Join-Path $vscodeExtensions[0].FullName 'bin\windows-x86_64\codex.exe'
        if (Test-Path $candidate -ErrorAction SilentlyContinue) {
            $repairSource = $candidate
        }
    }

    # Fallback: ~/.codex/.sandbox-bin/codex.exe
    if (-not $repairSource) {
        $fallback = Join-Path $env:USERPROFILE '.codex\.sandbox-bin\codex.exe'
        if (Test-Path $fallback -ErrorAction SilentlyContinue) {
            $repairSource = $fallback
        }
    }

    # If we found a repair source, try to fix the symlink
    if ($repairSource) {
        try {
            Remove-Item -Path $pathResolved -Force -ErrorAction Stop
            New-Item -ItemType SymbolicLink -Path $pathResolved -Target $repairSource -Force -ErrorAction Stop | Out-Null
            Add-Check "codex symlink: $path" 'ok' "auto-repaired -> $repairSource"
            Emit 'ok' "codex symlink: $path" "auto-repaired -> $repairSource"
        } catch {
            Add-Check "codex symlink: $path" 'fail' "repair failed: $_"
            Emit 'fail' "codex symlink: $path" "repair failed: $_"
        }
    } else {
        Add-Check "codex symlink: $path" 'fail' "no repair source found"
        Emit 'fail' "codex symlink: $path" 'no repair source found'
    }
}
