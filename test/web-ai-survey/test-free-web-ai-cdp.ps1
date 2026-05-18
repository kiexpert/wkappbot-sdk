$ErrorActionPreference = 'Continue'
$CI = $env:GITHUB_ACTIONS -eq 'true'

# Free web AI CDP-feasibility survey.
# For each candidate site: cdp open -> detect input element -> attempt injection
# -> sniff login wall. Classifies as CDP_OK / LOGIN_REQUIRED / NO_INPUT / ERROR.

$Sites = @(
    @{ Name='chatgpt.com';            Url='https://chatgpt.com' }
    @{ Name='gemini.google.com';      Url='https://gemini.google.com' }
    @{ Name='grok.com';               Url='https://grok.com' }
    @{ Name='duck.ai';                Url='https://duck.ai' }
    @{ Name='perplexity.ai';          Url='https://www.perplexity.ai' }
    @{ Name='copilot.microsoft.com';  Url='https://copilot.microsoft.com' }
    @{ Name='chat.deepseek.com';      Url='https://chat.deepseek.com' }
    @{ Name='chat.mistral.ai';        Url='https://chat.mistral.ai' }
    @{ Name='huggingface.co/chat';    Url='https://huggingface.co/chat' }
    @{ Name='you.com';                Url='https://you.com' }
    @{ Name='groq.com';               Url='https://groq.com' }
    @{ Name='meta.ai';                Url='https://www.meta.ai' }
    @{ Name='kimi.ai';                Url='https://www.kimi.ai' }
    @{ Name='qwen.ai';                Url='https://chat.qwen.ai' }
    @{ Name='pi.ai';                  Url='https://pi.ai' }
    @{ Name='phind.com';              Url='https://www.phind.com' }
    @{ Name='venice.ai';              Url='https://venice.ai/chat' }
    @{ Name='aifreeforever.com';      Url='https://aifreeforever.com' }
    @{ Name='cohere.com/chat';        Url='https://coral.cohere.com' }
)

$OpenTimeout = if ($CI) { 45 } else { 30 }
$EvalTimeout = 15
$LogDir = 'bin/wkappbot.hq/logs/web-ai-survey'
New-Item -Force -ItemType Directory -Path $LogDir | Out-Null

$script:Results = @()

function Invoke-WK {
    param([string[]]$WkArgs, [int]$TimeoutSec = 15)
    $logBase = Join-Path $env:TEMP "wk-aisurvey-$(Get-Random)"
    $proc = Start-Process wkappbot -ArgumentList $WkArgs `
        -RedirectStandardOutput "$logBase.out" -RedirectStandardError "$logBase.err" `
        -PassThru -NoNewWindow
    $exited = $proc.WaitForExit($TimeoutSec * 1000)
    if (-not $exited) {
        try { $proc.Kill() } catch {}
        Remove-Item "$logBase.out","$logBase.err" -ErrorAction SilentlyContinue
        return [pscustomobject]@{ Ok=$false; ExitCode=124; Output="TIMEOUT ${TimeoutSec}s" }
    }
    $out = (Get-Content "$logBase.out","$logBase.err" -ErrorAction SilentlyContinue) -join "`n"
    $exit = $proc.ExitCode
    Remove-Item "$logBase.out","$logBase.err" -ErrorAction SilentlyContinue
    return [pscustomobject]@{ Ok=($exit -eq 0); ExitCode=$exit; Output=$out }
}

function Get-PortFromOpen {
    param([string]$Text)
    if ($Text -match 'cdp:(\d+)') { return $Matches[1] }
    return $null
}

function Test-Site {
    param([hashtable]$Site)

    $name = $Site.Name
    $url  = $Site.Url
    Write-Host ""
    Write-Host "=== $name ($url) ==="

    $openRes = Invoke-WK @('cdp','open',$url) $OpenTimeout
    $port = Get-PortFromOpen $openRes.Output
    if (-not $port) {
        Save-Log $name 'open' $openRes.Output
        return [pscustomobject]@{ Site=$name; Status='ERROR'; Detail='cdp open: no port' }
    }
    $grap = "{proc:'chrome',cdp:$port}"
    Start-Sleep -Seconds 3

    # Step 3: detect any input surface.
    $jsInput = "(function(){var e=document.querySelector('textarea,[contenteditable=true],[role=textbox],input[type=text],input[type=search]');return e?(e.tagName+'#'+(e.id||'')+'.'+(e.className||'').split(' ')[0]):'NULL';})()"
    $inputRes = Invoke-WK @('a11y','read',$grap,'--eval-js',$jsInput) $EvalTimeout
    $hasInput = ($inputRes.Output -match '(TEXTAREA|DIV|INPUT|SPAN|P)#') -and ($inputRes.Output -notmatch 'NULL')

    # Step 5: login wall sniff (run regardless of input -- some pages gate behind modal).
    $jsLogin = "(function(){var t=(document.body.innerText||'').toLowerCase().slice(0,4000);var k=['sign in','log in','create account','sign up','log-in','login','signin'];for(var i=0;i<k.length;i++){if(t.indexOf(k[i])>=0)return 'WALL:'+k[i];}return 'NOWALL';})()"
    $loginRes = Invoke-WK @('a11y','read',$grap,'--eval-js',$jsLogin) $EvalTimeout
    $loginWall = $loginRes.Output -match 'WALL:'

    $injectDetail = ''
    if ($hasInput) {
        # Step 4: best-effort inject. Set value + dispatch input event so React/Vue see it.
        # We do NOT submit -- this is a feasibility probe, not a real ask.
        $jsInject = "(function(){var e=document.querySelector('textarea,[contenteditable=true],[role=textbox]');if(!e)return 'NOINPUT';try{if(e.isContentEditable){e.focus();e.innerText='say: SURVEY_OK';e.dispatchEvent(new InputEvent('input',{bubbles:true}));}else{var s=Object.getOwnPropertyDescriptor(window.HTMLTextAreaElement.prototype,'value').set;s.call(e,'say: SURVEY_OK');e.dispatchEvent(new Event('input',{bubbles:true}));}return 'INJECTED';}catch(x){return 'ERR:'+x.message.slice(0,80);}})()"
        $injRes = Invoke-WK @('a11y','read',$grap,'--eval-js',$jsInject) $EvalTimeout
        Start-Sleep -Seconds 5
        $jsRead = "(document.body.innerText||'').slice(0,200).replace(/\s+/g,' ')"
        $readRes = Invoke-WK @('a11y','read',$grap,'--eval-js',$jsRead) $EvalTimeout
        Save-Log $name 'read' $readRes.Output
        if ($injRes.Output -match 'INJECTED') { $injectDetail = 'inject=ok' }
        elseif ($injRes.Output -match 'ERR:') { $injectDetail = 'inject=err' }
        else { $injectDetail = 'inject=skip' }
    }

    Save-Log $name 'open'  $openRes.Output
    Save-Log $name 'input' $inputRes.Output
    Save-Log $name 'login' $loginRes.Output

    $status =
        if (-not $hasInput -and $loginWall) { 'LOGIN_REQUIRED' }
        elseif (-not $hasInput)              { 'NO_INPUT' }
        elseif ($loginWall)                  { 'LOGIN_REQUIRED' }
        else                                  { 'CDP_OK' }

    $detail = "port=$port"
    if ($injectDetail) { $detail += " $injectDetail" }
    if ($hasInput)     { $detail += ' input=yes' }
    if ($loginWall)    { $detail += ' wall=yes' }

    return [pscustomobject]@{ Site=$name; Status=$status; Detail=$detail }
}

function Save-Log {
    param([string]$Site,[string]$Tag,[string]$Text)
    $safe = $Site -replace '[\\/:*?"<>|]','_'
    $path = Join-Path $LogDir "$safe-$Tag.log"
    $Text | Out-File -FilePath $path -Encoding utf8
}

# Start Eye (no-op if already running)
Start-Process wkappbot.exe -ArgumentList 'eye' -WindowStyle Hidden -ErrorAction SilentlyContinue
Start-Sleep -Seconds 2

foreach ($site in $Sites) {
    $r = Test-Site $site
    $script:Results += $r
    Write-Host "[RESULT] $($r.Site)  $($r.Status)  $($r.Detail)"
}

Write-Host ""
Write-Host "================ SURVEY SUMMARY ================"
foreach ($r in $script:Results) {
    Write-Host ("[RESULT] {0,-26} {1,-15} {2}" -f $r.Site, $r.Status, $r.Detail)
}
$ok    = ($script:Results | Where-Object { $_.Status -eq 'CDP_OK' }).Count
$wall  = ($script:Results | Where-Object { $_.Status -eq 'LOGIN_REQUIRED' }).Count
$noin  = ($script:Results | Where-Object { $_.Status -eq 'NO_INPUT' }).Count
$err   = ($script:Results | Where-Object { $_.Status -eq 'ERROR' }).Count
$total = $script:Results.Count
Write-Host ""
Write-Host "[SUMMARY] $ok/$total CDP_OK without login"
Write-Host "[SUMMARY] LOGIN_REQUIRED=$wall  NO_INPUT=$noin  ERROR=$err"

# Save machine-readable summary.
$summaryPath = Join-Path $LogDir 'summary.json'
@{
    timestamp = (Get-Date -Format 'o')
    total     = $total
    cdp_ok    = $ok
    login     = $wall
    no_input  = $noin
    error     = $err
    results   = $script:Results
} | ConvertTo-Json -Depth 4 | Set-Content -Path $summaryPath -Encoding utf8
Write-Host "[SUMMARY] saved -> $summaryPath"

if ($err -eq $total) { exit 1 } else { exit 0 }
