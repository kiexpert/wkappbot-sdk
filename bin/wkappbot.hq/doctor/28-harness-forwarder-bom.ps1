# wkdoctor check: forwarder-bom -- detect UTF-8 BOM (0xEF 0xBB 0xBF) in harness forwarders and
# settings.json. BOM breaks PowerShell script execution and JSON parsing. Auto-strips on detect.
# Uses ReadAllBytes (not Get-Content) since Get-Content hides BOM. FAIL-OPEN.

try {
    $targets = @(
        'D:\GitHub\wkharness.ps1',
        'D:\GitHub\wkharness-post.ps1',
        (Join-Path $env:USERPROFILE '.claude\settings.json')
    )
    $bomFound = @()
    $bomFixed = @()
    foreach ($path in $targets) {
        if (-not (Test-Path -LiteralPath $path)) { continue }
        try {
            $bytes = [IO.File]::ReadAllBytes($path)
            $hasBom = ($bytes.Length -ge 3) -and ($bytes[0] -eq 0xEF) -and ($bytes[1] -eq 0xBB) -and ($bytes[2] -eq 0xBF)
            if (-not $hasBom) { continue }
            $bomFound += $path
            $raw = [IO.File]::ReadAllText($path, [Text.Encoding]::UTF8)
            if ($raw.Length -gt 0 -and [int][char]$raw[0] -eq 0xFEFF) { $raw = $raw.Substring(1) }
            [IO.File]::WriteAllText($path, $raw, (New-Object System.Text.UTF8Encoding($false)))
            $bytes2 = [IO.File]::ReadAllBytes($path)
            $stillBom = ($bytes2.Length -ge 3) -and ($bytes2[0] -eq 0xEF) -and ($bytes2[1] -eq 0xBB) -and ($bytes2[2] -eq 0xBF)
            if (-not $stillBom) {
                $bomFixed += $path
                Emit 'ok' 'forwarder-bom:fixed' "BOM stripped: $path"
            } else {
                Emit '!' 'forwarder-bom:fix' "BOM strip failed for $path"
            }
        } catch {
            Emit 'ok' 'forwarder-bom' "n/a (file check error, fail-open): $path -- $_"
        }
    }
    if ($bomFound.Count -eq 0) {
        Add-Check 'forwarder-bom' 'ok' 'no BOM in wkharness.ps1/wkharness-post.ps1/settings.json'
        Emit 'ok' 'forwarder-bom' 'no BOM in wkharness.ps1/wkharness-post.ps1/settings.json'
    } else {
        $unfixed = $bomFound | Where-Object { $_ -notin $bomFixed }
        if ($unfixed.Count -eq 0) {
            Add-Check 'forwarder-bom' 'ok' "BOM found and stripped in $($bomFixed.Count) file(s)"
        } else {
            Add-Check 'forwarder-bom' 'warn' "BOM in $($unfixed.Count) file(s) could not be stripped"
            Emit '!' 'forwarder-bom' "BOM strip incomplete: $($unfixed -join ', ')"
        }
    }
} catch {
    Add-Check 'forwarder-bom' 'ok' "n/a (check error, fail-open): $_"
    Emit 'ok' 'forwarder-bom' 'n/a (check error, fail-open)'
}
