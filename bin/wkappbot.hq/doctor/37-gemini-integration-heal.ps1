# wkdoctor check: gemini integration (TERM, rg.exe, Pace cache, nesting)

$hasHealed = $false

# 1. 256-color support (TERM)
if ($env:TERM -ne 'xterm-256color') {
    Add-Check 'gemini 256-color' 'warn' "TERM environment missing or not 256-color"
    [Environment]::SetEnvironmentVariable('TERM', 'xterm-256color', 'User')
    $env:TERM = 'xterm-256color'
    Emit '+' 'gemini 256-color' 'auto-healed: added TERM=xterm-256color to User env'
    $hasHealed = $true
} else {
    Emit '+' 'gemini 256-color' 'TERM=xterm-256color present'
}

# 2. Ripgrep check
if (-not (Get-Command rg.exe -ErrorAction SilentlyContinue)) {
    Add-Check 'gemini ripgrep' 'warn' 'rg.exe not found in PATH'
    Emit '!' 'gemini ripgrep' 'rg.exe is missing. Falling back to GrepTool causes lag.'
} else {
    Emit '+' 'gemini ripgrep' 'rg.exe found in PATH'
}

# 3. HWND Leak / Breakaway Denied
Emit '+' 'gemini breakaway' 'Breakaway warnings and HWND leaks noted for upstream fixing.'

# 4. Nested Agent Risk
if ((Get-Process -Id $PID).Name -match 'agy') {
    Emit '!' 'gemini nested' 'Agy process nesting gemini CLI increases loop risk.'
} else {
    Emit '+' 'gemini nested' 'No nesting risk detected'
}

# 5. Harness Wiring & Safety Block
$AgySettings = Join-Path $env:USERPROFILE '.gemini\antigravity-cli\settings.json'
$isMalfunctioning = $false
$malfunctionReason = ""

if (Test-Path $AgySettings) {
    try {
        $j = Get-Content $AgySettings -Raw | ConvertFrom-Json
        if (-not $j.hooks.BeforeTool) {
            $isMalfunctioning = $true
            $malfunctionReason = "BeforeTool hook is missing in settings.json"
        }
    } catch {
        $isMalfunctioning = $true
        $malfunctionReason = "Failed to parse Agy settings.json"
    }
} else {
    $isMalfunctioning = $true
    $malfunctionReason = "Agy settings.json not found"
}

if ($isMalfunctioning) {
    Add-Check 'gemini harness' 'fail' "Harness wiring malfunction: $malfunctionReason"
    Emit '!' 'gemini harness' 'Malfunction detected. Safety measure: BLOCKING basic write tools.'
    
    try {
        $autoPath = Join-Path $env:USERPROFILE '.gemini\policies\autonomy.toml'
        $blockRules = @"

[[rule]]
toolName = "write_to_file"
decision = "deny"
priority = 999

[[rule]]
toolName = "replace_file_content"
decision = "deny"
priority = 999

[[rule]]
toolName = "multi_replace_file_content"
decision = "deny"
priority = 999
"@
        Add-Content -Path $autoPath -Value $blockRules -Encoding UTF8
        Emit '+' 'gemini safety' 'Basic write tools have been explicitly blocked in autonomy.toml'
        $hasHealed = $true
    } catch {
        Emit '!' 'gemini safety' "Failed to apply safety block to autonomy.toml: $_"
    }
} else {
    Emit '+' 'gemini harness' 'Harness wiring is intact.'
}

if ($hasHealed) {
    Write-Host "[+] Gemini integration -- self-healed/safety-blocked successfully" -ForegroundColor Green
}

