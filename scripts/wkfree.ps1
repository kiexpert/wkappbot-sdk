#Requires -Version 5.1
[CmdletBinding()]
param(
    [Parameter(Position=0)] [string] $Provider,
    [Parameter(Position=1)] [string] $Prompt,
    [int] $Timeout = 120,
    [int] $Wait = 15,
    [switch] $Survey
)

$ErrorActionPreference = 'Stop'

function Write-WKLine {
    param([string] $Message, [string] $Color = 'Gray')
    Write-Host $Message -ForegroundColor $Color
}

function Invoke-WK {
    param(
        [Parameter(Mandatory=$true)] [string[]] $Args,
        [int] $TimeoutSec = 120
    )

    $psi = New-Object System.Diagnostics.ProcessStartInfo
    $psi.FileName = 'wkappbot'
    $psi.Arguments = ($Args | ForEach-Object {
        if ($_ -match '[\s"]') { '"' + ($_ -replace '"', '\"') + '"' } else { $_ }
    }) -join ' '
    $psi.UseShellExecute = $false
    $psi.RedirectStandardOutput = $true
    $psi.RedirectStandardError = $true
    $psi.CreateNoWindow = $true

    $p = New-Object System.Diagnostics.Process
    $p.StartInfo = $psi
    [void] $p.Start()

    if (-not $p.WaitForExit($TimeoutSec * 1000)) {
        try { $p.Kill() } catch {}
        throw "wkappbot timed out after ${TimeoutSec}s: $($Args -join ' ')"
    }

    $stdout = $p.StandardOutput.ReadToEnd()
    $stderr = $p.StandardError.ReadToEnd()
    $text = (($stdout, $stderr) -join "`n").Trim()

    return [PSCustomObject]@{
        ExitCode = $p.ExitCode
        Text = $text
    }
}

function Get-ProviderUrl {
    param([string] $Name)
    $map = @{
        duck       = 'https://duck.ai'
        perplexity = 'https://www.perplexity.ai'
        mistral    = 'https://chat.mistral.ai'
        deepseek   = 'https://chat.deepseek.com'
        hugging    = 'https://huggingface.co/chat'
        groq       = 'https://groq.com'
        venice     = 'https://venice.ai/chat'
        phind      = 'https://www.phind.com'
    }
    if (-not $map.ContainsKey($Name)) {
        throw "Unknown provider '$Name'. Use: $(@($map.Keys) -join ', ')"
    }
    return $map[$Name]
}

function New-JsString {
    param([AllowNull()][string] $Value)
    return ($Value | ConvertTo-Json -Compress)
}

function Invoke-CdpJs {
    param(
        [int] $Port,
        [string] $Js,
        [int] $TimeoutSec = 120
    )
    $grap = "{proc:'chrome',cdp:$Port}"
    return Invoke-WK -Args @('a11y', 'read', $grap, '--eval-js', $Js) -TimeoutSec $TimeoutSec
}

function Show-Usage {
    Write-WKLine 'Usage:' Cyan
    Write-WKLine '  powershell -File bin/wkfree.ps1 <provider> ''<prompt>'' [-Timeout <sec>] [-Wait <sec>] [-Survey]' Gray
    Write-WKLine 'ParamBlock|param|CommandType' DarkGray
    Write-WKLine 'ParamBlock: provider prompt Timeout Wait Survey' DarkGray
    Write-WKLine 'Providers: duck perplexity mistral deepseek hugging groq venice phind' Gray
}

if (-not $Provider) {
    Show-Usage
    return
}

if (-not $Survey -and -not $Prompt) {
    Show-Usage
    return
}

$Provider = $Provider.ToLowerInvariant()
$url = Get-ProviderUrl -Name $Provider
$ts = Get-Date -Format 'yyyyMMdd-HHmmss'
$logDir = Join-Path $PSScriptRoot 'wkappbot.hq\logs\wkfree'
New-Item -ItemType Directory -Force -Path $logDir | Out-Null
$logPath = Join-Path $logDir "$Provider-$ts.log"

function Add-Log {
    param([string] $Line)
    Add-Content -LiteralPath $logPath -Encoding UTF8 -Value $Line
}

Write-WKLine "[OPEN] $Provider $url" Cyan
$open = Invoke-WK -Args @('cdp', 'open', $url) -TimeoutSec $Timeout
Add-Log "[OPEN] $($open.Text)"

if ($open.Text -notmatch 'cdp:(\d+)') {
    Write-WKLine "[ERROR] Could not parse cdp:PORT from wkappbot output" Red
    Write-WKLine $open.Text DarkRed
    Add-Log "[ERROR] no cdp port"
    exit 1
}

$port = [int] $Matches[1]
$grapText = "{proc:'chrome',cdp:$port}"
Write-WKLine "[CDP] $grapText" Green
Start-Sleep -Seconds 3

$loginJs = @'
(function(){
  var text = (document.body && document.body.innerText || '').toLowerCase();
  var hasInput = !!document.querySelector('textarea,[contenteditable="true"],[role="textbox"],input[type="text"],input:not([type])');
  var login = /\b(sign in|log in|login|create account|sign up)\b/.test(text);
  if (login && !hasInput) return 'LOGIN_REQUIRED';
  return 'OK';
})()
'@

$login = Invoke-CdpJs -Port $port -Js $loginJs -TimeoutSec $Timeout
Add-Log "[LOGIN_CHECK] $($login.Text)"
if ($login.Text -match 'LOGIN_REQUIRED') {
    Write-WKLine '[LOGIN_REQUIRED]' Yellow
    Add-Log '[LOGIN_REQUIRED]'
    exit 2
}

$probeJs = @'
(function(){
  var el = document.querySelector('textarea,[contenteditable="true"],[role="textbox"],input[type="text"],input:not([type])');
  if (!el) return 'NO_INPUT';
  var tag = (el.tagName || '').toLowerCase();
  var role = el.getAttribute('role') || '';
  var editable = el.getAttribute('contenteditable') || '';
  return 'INPUT tag=' + tag + ' role=' + role + ' contenteditable=' + editable;
})()
'@

$probe = Invoke-CdpJs -Port $port -Js $probeJs -TimeoutSec $Timeout
Add-Log "[PROBE] $($probe.Text)"
if ($probe.Text -match 'NO_INPUT') {
    Write-WKLine '[NO_INPUT]' Red
    exit 1
}

Write-WKLine "[PROBE] $($probe.Text)" Green
if ($Survey) {
    Write-WKLine "[SURVEY] input probe only" Cyan
    Add-Log '[SURVEY] input probe only'
    return
}

$promptJson = New-JsString -Value $Prompt
$injectJs = @"
(function(){
  var text = $promptJson;
  var el = document.querySelector('textarea,[contenteditable="true"],[role="textbox"],input[type="text"],input:not([type])');
  if (!el) return 'NO_INPUT';
  el.focus();
  if (el.isContentEditable || el.getAttribute('contenteditable') === 'true') {
    el.textContent = text;
    el.dispatchEvent(new InputEvent('input', {bubbles:true, inputType:'insertText', data:text}));
  } else {
    var proto = Object.getPrototypeOf(el);
    var desc = Object.getOwnPropertyDescriptor(proto, 'value');
    if (desc && desc.set) desc.set.call(el, text); else el.value = text;
    el.dispatchEvent(new Event('input', {bubbles:true}));
    el.dispatchEvent(new Event('change', {bubbles:true}));
  }
  return 'INJECTED';
})()
"@

$inject = Invoke-CdpJs -Port $port -Js $injectJs -TimeoutSec $Timeout
Add-Log "[INJECT] $($inject.Text)"
if ($inject.Text -notmatch 'INJECTED') {
    Write-WKLine "[ERROR] prompt injection failed: $($inject.Text)" Red
    exit 1
}
Write-WKLine '[INJECT] prompt set' Green

$submitJs = @'
(function(){
  var el = document.querySelector('textarea,[contenteditable="true"],[role="textbox"],input[type="text"],input:not([type])');
  if (el) {
    el.focus();
    ['keydown','keypress','keyup'].forEach(function(type){
      el.dispatchEvent(new KeyboardEvent(type, {key:'Enter', code:'Enter', keyCode:13, which:13, bubbles:true, cancelable:true}));
    });
  }
  var buttons = Array.prototype.slice.call(document.querySelectorAll('button[type="submit"],button[aria-label*="send" i],button[title*="send" i],button[data-testid*="send" i]'));
  var btn = buttons.find(function(b){ return !b.disabled && b.offsetParent !== null; });
  if (btn) { btn.click(); return 'SUBMITTED_CLICK'; }
  var form = el && el.closest && el.closest('form');
  if (form) {
    if (form.requestSubmit) form.requestSubmit(); else form.submit();
    return 'SUBMITTED_FORM';
  }
  return 'SUBMITTED_ENTER';
})()
'@

$submit = Invoke-CdpJs -Port $port -Js $submitJs -TimeoutSec $Timeout
Add-Log "[SUBMIT] $($submit.Text)"
Write-WKLine "[SUBMIT] $($submit.Text)" Green

Start-Sleep -Seconds $Wait

$readJs = @'
(function(){
  var selectors = [
    '[data-role="assistant"]',
    '[data-testid*="assistant" i]',
    '.message',
    '.prose',
    'article',
    '[role="article"]'
  ];
  var nodes = [];
  selectors.forEach(function(sel){
    Array.prototype.forEach.call(document.querySelectorAll(sel), function(el){
      var text = (el.innerText || '').trim();
      if (text && el.offsetParent !== null) nodes.push(text);
    });
  });
  if (!nodes.length) {
    var body = (document.body && document.body.innerText || '').trim();
    if (body) nodes.push(body);
  }
  var out = nodes.length ? nodes[nodes.length - 1] : '';
  return out.slice(0, 2000);
})()
'@

$read = Invoke-CdpJs -Port $port -Js $readJs -TimeoutSec $Timeout
$result = $read.Text.Trim()
if (-not $result) { $result = '[NO_RESULT]' }

Write-WKLine '[RESULT]' Cyan
Write-Host $result
Add-Log '[RESULT]'
Add-Log $result
Write-WKLine "[LOG] $logPath" DarkGray
