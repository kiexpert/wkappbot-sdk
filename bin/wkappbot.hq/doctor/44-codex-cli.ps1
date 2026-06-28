# wkdoctor check: codex CLI -- @openai/codex npm package

$codexCmd = Get-Command codex -ErrorAction SilentlyContinue
if ($codexCmd) {
    try {
        $codexVer = & codex --version 2>&1 | Select-Object -First 1
        $verStr = if ($codexVer) { "$codexVer".Trim() } else { "unknown" }
        Add-Check 'codex (CLI)' 'ok' $verStr
        Emit 'ok' 'codex (CLI)' $verStr
    } catch {
        Add-Check 'codex (CLI)' 'warn' "found but --version failed -- try: npm install -g @openai/codex"
        Emit '!' 'codex (CLI)' 'version check failed'
    }
} else {
    Add-Check 'codex (CLI)' 'warn' 'not found -- npm install -g @openai/codex'
    Emit '!' 'codex (CLI)' 'missing'
}