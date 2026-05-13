@echo off
setlocal EnableExtensions EnableDelayedExpansion

set "ROOT=%~dp0"
set "CONN=%ROOT%connectors\chatgpt-wkappbot"
set "LOGDIR=%CONN%\.wk-data"
set "RELAY_LOG=%LOGDIR%\relay.log"
set "AGENT_LOG=%LOGDIR%\agent.log"
set "TUNNEL_LOG=%LOGDIR%\tunnel.log"

if not exist "%LOGDIR%" mkdir "%LOGDIR%" >nul 2>nul
if exist "%ROOT%bin\wkappbot.exe" set "PATH=%ROOT%bin;%PATH%"
call :refresh_path

call :ensure_powershell || exit /b 1
call :ensure_winget
call :ensure_node || exit /b 1
call :ensure_cloudflared
call :ensure_browser

where wkappbot >nul 2>nul
if errorlevel 1 echo [WARN] wkappbot is not on PATH. Repo bin was checked; agent may fail until wkappbot is available.

cd /d "%CONN%" || exit /b 1

if "%WKAPPBOT_TOKEN%"=="" (
  set "WKAPPBOT_TOKEN=dev-local-token"
  echo [WARN] WKAPPBOT_TOKEN was empty. Using dev-local-token for this process.
)

echo [1/4] Starting relay on http://127.0.0.1:8787 ...
start "WKAppBot ChatGPT Relay" /min cmd /c "cd /d "%CONN%" && set WKAPPBOT_TOKEN=%WKAPPBOT_TOKEN%&& node src\server.js > "%RELAY_LOG%" 2>&1"

for /l %%i in (1,1,20) do (
  powershell -NoProfile -ExecutionPolicy Bypass -Command "try { $r = Invoke-WebRequest -UseBasicParsing http://127.0.0.1:8787/health -TimeoutSec 1; if ($r.StatusCode -eq 200) { exit 0 } } catch { exit 1 }" >nul 2>nul
  if not errorlevel 1 goto relay_ready
  timeout /t 1 /nobreak >nul
)

echo [ERROR] Relay did not become healthy. See: %RELAY_LOG%
call :open_log "%RELAY_LOG%"
exit /b 1

:relay_ready
echo [2/4] Relay is healthy.

echo [3/4] Starting local WKAppBot agent ...
start "WKAppBot ChatGPT Agent" /min cmd /c "cd /d "%CONN%" && set WK_RELAY=http://127.0.0.1:8787&& set WKAPPBOT_TOKEN=%WKAPPBOT_TOKEN%&& node src\agent.js >> "%AGENT_LOG%" 2>&1"

where cloudflared >nul 2>nul
if errorlevel 1 (
  echo [WARN] cloudflared is still not available. Opening download page.
  call :open_browser "https://developers.cloudflare.com/cloudflare-one/connections/connect-networks/downloads/"
  echo Relay URL for local test: http://127.0.0.1:8787
  exit /b 0
)

echo [4/4] Starting free Cloudflare Tunnel. Waiting for public HTTPS URL ...
if exist "%TUNNEL_LOG%" del "%TUNNEL_LOG%" >nul 2>nul
start "WKAppBot Cloudflare Tunnel" /min cmd /c "cloudflared tunnel --url http://127.0.0.1:8787 > "%TUNNEL_LOG%" 2>&1"

for /l %%i in (1,1,45) do (
  for /f "usebackq tokens=*" %%u in (`powershell -NoProfile -ExecutionPolicy Bypass -Command "$p='%TUNNEL_LOG%'; if(Test-Path $p){ $m=Select-String -Path $p -Pattern 'https://[-a-zA-Z0-9.]+trycloudflare.com' -AllMatches | Select-Object -Last 1; if($m){ $m.Matches[0].Value } }"`) do set "PUBLIC_URL=%%u"
  if not "!PUBLIC_URL!"=="" goto tunnel_ready
  timeout /t 1 /nobreak >nul
)

echo [WARN] Tunnel started but public URL was not detected yet.
echo Check log: %TUNNEL_LOG%
echo Relay URL: http://127.0.0.1:8787
call :open_log "%TUNNEL_LOG%"
exit /b 0

:tunnel_ready
echo.
echo ============================================================
echo WKAppBot ChatGPT connector is ready.
echo Public HTTPS URL: !PUBLIC_URL!
echo Health: !PUBLIC_URL!/health
echo Bearer token: %WKAPPBOT_TOKEN%
echo ============================================================
echo.
call :open_browser "https://chatgpt.com/gpts/editor"
pause
exit /b 0

:ensure_powershell
where powershell >nul 2>nul
if not errorlevel 1 exit /b 0
echo [ERROR] PowerShell is required for setup checks.
exit /b 1

:ensure_winget
where winget >nul 2>nul
if not errorlevel 1 exit /b 0
echo [WARN] winget was not found. Automatic installation may be limited.
call :open_browser "https://apps.microsoft.com/detail/9nblggh4nns1"
exit /b 0

:ensure_node
where node >nul 2>nul
if not errorlevel 1 exit /b 0
echo [SETUP] Node.js was not found. Trying winget install...
where winget >nul 2>nul
if errorlevel 1 (
  call :open_browser "https://nodejs.org/"
  exit /b 1
)
winget install --id OpenJS.NodeJS.LTS -e --accept-source-agreements --accept-package-agreements
call :refresh_path
where node >nul 2>nul
if not errorlevel 1 exit /b 0
echo [ERROR] Node installed, but current terminal PATH was not refreshed. Run this file again from a new terminal.
exit /b 1

:ensure_cloudflared
where cloudflared >nul 2>nul
if not errorlevel 1 exit /b 0
echo [SETUP] cloudflared was not found. Trying winget install...
where winget >nul 2>nul
if errorlevel 1 exit /b 0
winget install --id Cloudflare.cloudflared -e --accept-source-agreements --accept-package-agreements
call :refresh_path
exit /b 0

:ensure_browser
where chrome >nul 2>nul
if not errorlevel 1 exit /b 0
where msedge >nul 2>nul
if not errorlevel 1 exit /b 0
where winget >nul 2>nul
if errorlevel 1 exit /b 0
echo [SETUP] Chrome/Edge was not found. Trying Chrome install...
winget install --id Google.Chrome -e --accept-source-agreements --accept-package-agreements
call :refresh_path
exit /b 0

:refresh_path
for /f "usebackq tokens=*" %%p in (`powershell -NoProfile -ExecutionPolicy Bypass -Command "[Environment]::GetEnvironmentVariable('Path','Machine') + ';' + [Environment]::GetEnvironmentVariable('Path','User')"`) do set "PATH=%%p;%ROOT%bin;%PATH%"
exit /b 0

:open_browser
set "URL=%~1"
where chrome >nul 2>nul
if not errorlevel 1 ( start "" chrome "%URL%" & exit /b 0 )
where msedge >nul 2>nul
if not errorlevel 1 ( start "" msedge "%URL%" & exit /b 0 )
start "" "%URL%"
exit /b 0

:open_log
set "LOG=%~1"
if exist "%LOG%" start "" notepad "%LOG%"
exit /b 0
