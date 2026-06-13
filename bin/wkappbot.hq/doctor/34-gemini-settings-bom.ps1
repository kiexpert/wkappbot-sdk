# wkdoctor check: gemini-settings-bom -- detect and strip UTF-8 BOM from Gemini settings files
# (1) DETECT: scan global gemini settings and autonomy policies for UTF-8 BOM (\xef\xbb\xbf)
# (2) HEAL:   strip the 3-byte BOM and write back as clean UTF-8
# (3) SOLVE:  report heal status or clean state
# (4) PREVENT: runs every doctor pass to ensure Gemini CLI starts correctly

# Target files
$_m_gem_targets = @(
    Join-Path $env:USERPROFILE '.gemini\settings.json',
    Join-Path $env:USERPROFILE '.gemini\policies\autonomy.toml',
    Join-Path $env:USERPROFILE '.gemini\antigravity-cli\settings.json'
)

$_m_gem_healed = 0
$_m_gem_errors = @()
$_m_gem_found  = 0

foreach ($p in $_m_gem_targets) {
    if (Test-Path $p -PathType Leaf) {
        try {
            $bytes = [IO.File]::ReadAllBytes($p)
            if ($bytes.Count -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF) {
                $_m_gem_found++
                # HEAL: Strip BOM
                $newBytes = New-Object byte[] ($bytes.Count - 3)
                [Array]::Copy($bytes, 3, $newBytes, 0, $newBytes.Count)
                [IO.File]::WriteAllBytes($p, $newBytes)
                $_m_gem_healed++
            }
        } catch {
            $_m_gem_errors += "$p: $( $_.Exception.Message )"
        }
    }
}

# Report
if ($_m_gem_found -eq 0 -and $_m_gem_errors.Count -eq 0) {
    Add-Check 'gemini:settings-bom' 'ok' 'No UTF-8 BOM detected in gemini settings files.'
    Emit 'ok' 'gemini:settings-bom' 'clean'
} else {
    $status = if ($_m_gem_errors.Count -gt 0) { 'warn' } else { 'ok' }
    $detail = "Found $_m_gem_found files with BOM, healed $_m_gem_healed."
    if ($_m_gem_errors.Count -gt 0) { $detail += " Errors: $($_.m_gem_errors.Count)" }
    
    Add-Check 'gemini:settings-bom' $status $detail
    Emit $(if ($status -eq 'ok') { 'ok' } else { '!' }) 'gemini:settings-bom' $detail
    
    foreach ($err in $_m_gem_errors) {
        Add-Check 'gemini:settings-bom:error' 'warn' $err
        Emit '!' 'gemini:settings-bom:error' $err
    }
}