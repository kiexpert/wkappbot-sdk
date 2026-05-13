@echo off
setlocal EnableExtensions EnableDelayedExpansion

set "ROOT=%~dp0"
set "CONN=%ROOT%connectors\chatgpt-wkappbot"
set "LOGDIR=%CONN%\.wk-data"
set "RELAY_LOG=%LOGDIR%\relay.log"
set "TUNNEL_LOG=%LOGDIR%\tunnel.log"

if not exist "%LOGDIR%" mkdir "%LOGDIR%" >nul 2>nul

where node >nul 2>nul
if errorlevel 1 (
  echo [ERROR] Node.js 20+ is required.
  echo Install from https://nodejs.org/ then run this file again.
  exit /b 1
)

where wkappbot >nul 2>nul
if errorlevel 1 (
  echo [WARN] wkappbot is not on PATH. The relay can start, but the local agent will fail until wkappbot is available.
)

cd /d "%CONN%" || exit /b 1

if "%WKAPPBOT_TOKEN%"=="" (
  set "WKAPPBOT_TOKEN=dev-local-token"
  echo [WARN] WKAPPBOT_TOKEN was empty. Using dev-local-token for this process.
)

echo [1/4] Starting WKAppBot ChatGPT relay on http://127.0.0.1:8787 ...
start "WKAppBot ChatGPT Relay" /min cmd /c "cd /d "%CONN%" && set WKAPPBOT_TOKEN=%WKAPPBOT_TOKEN%&& node src\server.js > "%RELAY_LOG%" 2>&1"

for /l %%i in (1,1,20) do (
  powershell -NoProfile -ExecutionPolicy Bypass -Command "try { $r = Invoke-WebRequest -UseBasicParsing http://127.0.0.1:8787/health -TimeoutSec 1; if ($r.StatusCode -eq 200) { exit 0 } } catch { exit 1 }" >nul 2>nul
  if not errorlevel 1 goto relay_ready
  timeout /t 1 /nobreak >nul
)

echo [ERROR] Relay did not become healthy. See: %RELAY_LOG%
exit /b 1

:relay_ready
echo [2/4] Relay is healthy.

echo [3/4] Starting local WKAppBot agent ...
start "WKAppBot ChatGPT Agent" /min cmd /c "cd /d "%CONN%" && set WK_RELAY=http://127.0.0.1:8787&& set WKAPPBOT_TOKEN=%WKAPPBOT_TOKEN%&& node src\agent.js >> "%LOGDIR%\agent.log" 2>&1"

where cloudflared >nul 2>nul
if errorlevel 1 (
  echo [WARN] cloudflared was not found on PATH.
  echo Install it, or use ngrok manually: ngrok http 8787
  echo Relay URL for local test: http://127.0.0.1:8787
  exit /b 0
)

echo [4/4] Starting free Cloudflare Tunnel. Waiting for public HTTPS URL ...
if exist "%TUNNEL_LOG%" del "%TUNNEL_LOG%" >nul 2>nul
start "WKAppBot Cloudflare Tunnel" /min cmd /c "cloudflared tunnel --url http://127.0.0.1:8787 > "%TUNNEL_LOG%" 2>&1"

for /l %%i in (1,1,30) do (
  for /f "usebackq tokens=*" %%u in (`powershell -NoProfile -ExecutionPolicy Bypass -Command "$p='%TUNNEL_LOG%'; if(Test-Path $p){ $m=Select-String -Path $p -Pattern 'https://[-a-zA-Z0-9.]+trycloudflare.com' -AllMatches | Select-Object -Last 1; if($m){ $m.Matches[0].Value } }"`) do set "PUBLIC_URL=%%u"
  if not "!PUBLIC_URL!"=="" goto tunnel_ready
  timeout /t 1 /nobreak >nul
)

echo [WARN] Tunnel started but public URL was not detected yet.
echo Check log: %TUNNEL_LOG%
echo Relay URL: http://127.0.0.1:8787
exit /b 0

:tunnel_ready
echo.
echo ============================================================
echo WKAppBot ChatGPT connector is ready.
echo.
echo Public HTTPS URL:
echo !PUBLIC_URL!
echo.
echo ChatGPT Action server URL:
echo !PUBLIC_URL!
echo.
echo Health:
echo !PUBLIC_URL!/health
echo.
echo Use bearer token:
echo %WKAPPBOT_TOKEN%
echo ============================================================
echo.
pause
