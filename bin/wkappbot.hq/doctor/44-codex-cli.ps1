# wkdoctor check: codex CLI -- @openai/codex npm package

$codexCmd = Get-Command codex -ErrorAction SilentlyContinue
if ($codexCmd) {
    $verStr = 'unknown'
    try {
        $codexPath = $codexCmd.Source
        $raw = (& $codexPath --version) 2>$null
        if ($raw) { $verStr = [string]$raw[0] }
    } catch {}
    if ($verStr -and $verStr -ne 'unknown') {
        Add-Check 'codex (CLI)' 'ok' $verStr
        Emit 'ok' 'codex (CLI)' $verStr
    } else {
        Add-Check 'codex (CLI)' 'warn' "found but --version failed -- try: npm install -g @openai/codex"
        Emit '!' 'codex (CLI)' 'version check failed'
    }
} else {
    Add-Check 'codex (CLI)' 'warn' 'not found -- npm install -g @openai/codex'
    Emit '!' 'codex (CLI)' 'missing'
}