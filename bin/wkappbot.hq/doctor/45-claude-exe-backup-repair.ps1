# doctor/45-claude-exe-backup-repair.ps1
# Detects and auto-repairs a missing Claude Code executable after a failed self-update.
# ROOT CAUSE (2026-07-03): the Claude Code self-updater renames the live claude.exe to a
# timestamped claude.exe.old.<ts> backup, then should write a fresh claude.exe -- but if that
# final step fails (network blip, disk issue, interrupted process), claude.exe is left GONE
# with only .old backups remaining. Every OTHER terminal then fails to launch claude at all
# ('is not recognized as an internal or external command'). Requested in suggest
# ts=2026-06-10T02:26:35.4137168Z ('wkdoctor -- functional check of each family CLI + auto-repair
# of broken/mis-targeted symlinks (claude, wkappbot busybox)'), still open as of this fix.

$claudeBinDir = 'D:\SDK\npm\node_modules\@anthropic-ai\claude-code\bin'
$claudeExe = Join-Path $claudeBinDir 'claude.exe'

if (-not (Test-Path -LiteralPath $claudeBinDir -ErrorAction SilentlyContinue)) {
    Add-Check 'claude.exe presence' 'warn' "claude-code bin dir not found at $claudeBinDir -- skipping (non-standard npm install location)"
    Emit 'warn' 'claude.exe presence' "bin dir not found: $claudeBinDir"
} elseif (Test-Path -LiteralPath $claudeExe -PathType Leaf -ErrorAction SilentlyContinue) {
    Add-Check 'claude.exe presence' 'ok' 'claude.exe present'
    Emit 'ok' 'claude.exe presence' 'claude.exe present'
} else {
    $backups = Get-ChildItem -LiteralPath $claudeBinDir -Filter 'claude.exe.old.*' -File -ErrorAction SilentlyContinue |
        Sort-Object LastWriteTime -Descending
    if ($backups.Count -eq 0) {
        Add-Check 'claude.exe presence' 'fail' 'claude.exe missing, no .old backup to restore -- run: npm install -g @anthropic-ai/claude-code'
        Emit 'fail' 'claude.exe presence' 'missing, no backup -- reinstall required'
    } else {
        $newest = $backups[0]
        try {
            Copy-Item -LiteralPath $newest.FullName -Destination $claudeExe -Force -ErrorAction Stop
            Add-Check 'claude.exe presence' 'ok' "repaired: restored from $($newest.Name)"
            Emit 'ok' 'claude.exe presence' "auto-repaired from $($newest.Name)"
        } catch {
            Add-Check 'claude.exe presence' 'fail' "missing, backup found ($($newest.Name)) but repair copy failed: $_"
            Emit 'fail' 'claude.exe presence' "repair failed: $_"
        }
    }
}
