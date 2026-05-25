# wkdoctor check: wkappbot.hq/ directory
# Heal: create if missing

# Check 10: wkappbot.hq/
$hq = Join-Path $binDir 'wkappbot.hq'
if (Test-Path $hq -PathType Container) {
    Add-Check 'wkappbot.hq/' 'ok' 'present'
    Emit 'ok' 'wkappbot.hq/' 'present'
} else {
    Add-Check 'wkappbot.hq/' 'warn' 'not found'
    Emit '!' 'wkappbot.hq/' 'creating...'
    try {
        New-Item -ItemType Directory -Path $hq -Force | Out-Null
        New-Item -ItemType Directory -Path (Join-Path $hq 'doctor') -Force | Out-Null
        if (Test-Path $hq -PathType Container) {
            Add-Check 'wkappbot.hq/' 'ok' 'self-healed: directory created'
            Emit 'ok' 'wkappbot.hq/' 'self-healed'
        }
    } catch {
        Add-Check 'wkappbot.hq/' 'warn' ('create failed: ' + $_.Exception.Message)
        Emit '!' 'wkappbot.hq/' 'create failed'
    }
}
