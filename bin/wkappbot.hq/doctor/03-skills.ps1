# wkdoctor check: skills catalog
# Heal: copy repo skills/ to user .wkappbot/skills/ if sparse

$skillDirs = @(
    (Join-Path $repoRoot 'skills'),
    (Join-Path $env:USERPROFILE '.wkappbot\skills'),
    (Join-Path $env:USERPROFILE 'AppData\Local\wkappbot\skills')
)
$skillCount = 0
foreach ($sd in $skillDirs) {
    if (Test-Path $sd -PathType Container) {
        $skillCount += (Get-ChildItem $sd -Recurse -Filter '*.skill.json' -ErrorAction SilentlyContinue).Count
    }
}

if ($skillCount -gt 10) {
    Add-Check 'Skills (local)' 'ok' "$skillCount .skill.json files"
    Emit 'ok' 'Skills (local)' "$skillCount files"
} else {
    Add-Check 'Skills (local)' 'warn' "only $skillCount files"
    Emit '!' 'Skills (local)' "only $skillCount -- healing..."
    $repoSkills = Join-Path $repoRoot 'skills'
    $userSkills = Join-Path $env:USERPROFILE '.wkappbot\skills'
    if (Test-Path $repoSkills -PathType Container) {
        if (-not (Test-Path $userSkills)) { New-Item -ItemType Directory $userSkills -Force | Out-Null }
        Copy-Item -Path (Join-Path $repoSkills '*') -Destination $userSkills -Recurse -Force -ErrorAction SilentlyContinue
        $newCount = (Get-ChildItem $userSkills -Recurse -Filter '*.skill.json' -ErrorAction SilentlyContinue).Count
        if ($newCount -gt $skillCount) {
            Add-Check 'Skills (local)' 'ok' "self-healed: $newCount files after copy"
            Emit 'ok' 'Skills (local)' "self-healed: $newCount files"
        } else {
            Add-Check 'Skills (local)' 'warn' 'still sparse -- run: wkappbot skill install'
            Emit '!' 'Skills (local)' 'run: wkappbot skill install'
        }
    } else {
        Add-Check 'Skills (local)' 'warn' 'no repo skills/ -- run: wkappbot skill install'
        Emit '!' 'Skills (local)' 'run: wkappbot skill install'
    }
}
