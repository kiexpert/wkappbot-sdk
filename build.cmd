@echo off
setlocal EnableExtensions EnableDelayedExpansion

rem ===========================================================================
rem  WKAppBot SDK build.cmd  -  Build launcher + download core, then deploy
rem
rem  Public SDK note: only the launcher source ships in this repo.
rem  wkappbot-core.exe is built in the private dev repo and downloaded
rem  from a GitHub Release (see :download_release).
rem
rem  Usage:
rem    build.cmd                   Download release binaries (always required
rem                                for core), then build launcher from source.
rem    build.cmd --download-only   Download from latest GitHub Release only;
rem                                skip launcher source build (no .NET SDK needed).
rem    build.cmd --deploy-only     Skip publish; re-deploy from existing publish output.
rem    build.cmd --no-build        Skip compile; just package + copy + hot-swap.
rem                                Use when invoked from a csproj post-publish event.
rem
rem  Deploy target:  <repo>\bin\
rem    wkappbot.exe       Launcher  (AOT, ~1 MB; starts core, relays pipe cmds,
rem                                  handles hot-swap trigger)
rem    wkappbot-core.exe  Core      (single-file ~25 MB; downloaded binary)
rem
rem  Hot-swap flow (Eye is running):
rem    1. csproj post-publish target copies core  -> wkappbot-core.new.exe
rem    2. build.cmd copies launcher               -> wkappbot.new.exe
rem    3. Eye tick triggers self-detect: drains in-flight pipes, self-replaces
rem    4. Watchdog (below): if .new.exe survives 3 s -> taskkill + force-promote
rem
rem  AI note: never pass --no-verify or skip hooks; never force-push; always
rem    use hot-swap path (do NOT taskkill manually before building).
rem ===========================================================================

rem ROOT_DIR: auto-detect from this script's location (works on any machine)
set "ROOT_DIR=%~dp0"
if "%ROOT_DIR:~-1%"=="\" set "ROOT_DIR=%ROOT_DIR:~0,-1%"

rem DOTNET_EXE: prefer system install, fall back to repo-local dotnet
set "DOTNET_EXE=dotnet"
if exist "C:\Program Files\dotnet\dotnet.exe" set "DOTNET_EXE=C:\Program Files\dotnet\dotnet.exe"

set "LAUNCHER_PROJ=%ROOT_DIR%\csharp\src\WKAppBot.Launcher\WKAppBot.Launcher.csproj"
set "LAUNCHER_OUT=%ROOT_DIR%\csharp\src\WKAppBot.Launcher\bin\Release\net8.0-windows\win-x64\publish"
set "BIN_DIR=%ROOT_DIR%\bin"
set "DEPLOY_ONLY=0"
set "NO_BUILD=0"
set "DOWNLOAD_ONLY=0"
if /I "%~1"=="--deploy-only"   set "DEPLOY_ONLY=1"
if /I "%~1"=="--no-build"      set "NO_BUILD=1"
if /I "%~1"=="--download-only" set "DOWNLOAD_ONLY=1"
if /I "%~2"=="--deploy-only"   set "DEPLOY_ONLY=1"
if /I "%~2"=="--no-build"      set "NO_BUILD=1"

rem ---------------------------------------------------------------------------
rem  DOWNLOAD PATH: fetch pre-built binaries from latest GitHub Release
rem  Core is binary-only in this public SDK (no source) -- always required.
rem  Launcher source build follows below; --download-only short-circuits it.
rem ---------------------------------------------------------------------------
if "%DEPLOY_ONLY%"=="0" (
  if not exist "%BIN_DIR%\wkappbot-core.exe" (
    echo [BUILD] bin\wkappbot-core.exe not found -- fetching from GitHub Release...
    call :download_release
    if "%DOWNLOAD_ONLY%"=="1" exit /b 0
  ) else (
    if "%DOWNLOAD_ONLY%"=="1" (
      echo [BUILD] Binaries already present. Use --deploy-only to redeploy.
      exit /b 0
    )
  )
)

set "DOTNET_CLI_HOME=%ROOT_DIR%\.dotnet"
set "DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1"
set "DOTNET_CLI_TELEMETRY_OPTOUT=1"
set "MSBuildEnableWorkloadResolver=false"

rem AOT (Launcher) requires vswhere.exe so MSVC linker can be located.
rem Unconditionally prepend VS Installer dir - harmless if already present.
if exist "C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe" (
  set "PATH=C:\Program Files (x86)\Microsoft Visual Studio\Installer;%PATH%"
)

rem Verify dotnet is available (either system PATH or explicit path set above)
where dotnet >nul 2>nul
if errorlevel 1 (
  if not exist "%DOTNET_EXE%" (
    echo [BUILD] dotnet not found. Install .NET SDK 8 from https://dot.net
    exit /b 1
  )
)
echo [BUILD] dotnet: %DOTNET_EXE%

if "%DEPLOY_ONLY%"=="0" (
  echo [1/3] Publish launcher (AOT, single-file, self-contained)
  call :publish_launcher
  if errorlevel 1 exit /b 1

  if not exist "%LAUNCHER_OUT%\wkappbot.exe" (
    echo [BUILD] Missing launcher output: "%LAUNCHER_OUT%\wkappbot.exe"
    exit /b 1
  )
  if not exist "%BIN_DIR%\wkappbot-core.exe" (
    echo [BUILD] Missing core binary: "%BIN_DIR%\wkappbot-core.exe"
    echo [BUILD]   wkappbot-core.exe is downloaded, not built from source in this SDK.
    echo [BUILD]   Re-run build.cmd to fetch it from GitHub Release.
    exit /b 1
  )
) else (
  echo [BUILD] deploy-only mode (skip publish)
)

echo [2/3] Deploy binaries to %BIN_DIR%
rem Core was already placed in %BIN_DIR% by :download_release.

rem Launcher deploy: rename current to .old, put new at original path.
rem Running launcher can be renamed on Windows NTFS; new connections use the new file.
set "TS=%date:~0,4%%date:~5,2%%date:~8,2%-%time:~0,2%%time:~3,2%"
set "TS=!TS: =0!"
if exist "%BIN_DIR%\wkappbot.exe" (
  echo [BUILD]   rename launcher: wkappbot.exe -> wkappbot.old-!TS!.exe
  move /y "%BIN_DIR%\wkappbot.exe" "%BIN_DIR%\wkappbot.old-!TS!.exe" >nul 2>nul
)
echo [BUILD]   copy launcher: %LAUNCHER_OUT%\wkappbot.exe -> %BIN_DIR%\wkappbot.exe
copy /y "%LAUNCHER_OUT%\wkappbot.exe" "%BIN_DIR%\wkappbot.exe" >nul
if errorlevel 1 (
  echo [BUILD] launcher copy failed
  exit /b 1
)
rem Update busybox symlink aliases
for %%A in (a11y wka11y wkedit grap grep) do (
  if exist "%BIN_DIR%\%%A.exe" copy /y "%BIN_DIR%\wkappbot.exe" "%BIN_DIR%\%%A.exe" >nul
)

if not exist "%BIN_DIR%\wkappbot.exe" (
  if exist "%BIN_DIR%\wkappbot.new.exe" (
    echo [BUILD] launcher staged for hot-swap: "%BIN_DIR%\wkappbot.new.exe"
  ) else (
    echo [BUILD] launcher deploy missing: "%BIN_DIR%\wkappbot.exe"
    exit /b 1
  )
)
if not exist "%BIN_DIR%\wkappbot-core.exe" (
  if exist "%BIN_DIR%\wkappbot-core.new.exe" (
    echo [BUILD] core staged for hot-swap: "%BIN_DIR%\wkappbot-core.new.exe"
  ) else (
    echo [BUILD] core deploy missing: "%BIN_DIR%\wkappbot-core.exe"
    exit /b 1
  )
)

rem Keep deployment single-file expectation clean.
del /q "%BIN_DIR%\wkappbot-core.dll" >nul 2>nul
del /q "%BIN_DIR%\wkappbot-core.deps.json" >nul 2>nul
del /q "%BIN_DIR%\wkappbot-core.runtimeconfig.json" >nul 2>nul
del /q "%BIN_DIR%\wkappbot.dll" >nul 2>nul
del /q "%BIN_DIR%\wkappbot.deps.json" >nul 2>nul
del /q "%BIN_DIR%\wkappbot.runtimeconfig.json" >nul 2>nul

echo [3/3] Eye tick trigger (hot-swap detect + pipe drain)
call "%BIN_DIR%\wkappbot-core.exe" eye tick --timeout 15 >nul 2>nul

rem Hot-swap watchdog:
rem If *.new.exe is still present after 3s, treat as failure, kill all, and promote .new -> .exe.
rem Launcher: no .new.exe watchdog needed - deployed directly to wkappbot.exe
rem Old launcher (.old-*.exe) cleanup: delete anything older than current
for %%F in ("%BIN_DIR%\wkappbot.old-*.exe") do (
  del /q "%%F" >nul 2>nul
)

if exist "%BIN_DIR%\wkappbot-core.new.exe" (
  echo [BUILD] waiting core hot-swap ^(max 3s^)...
  for /L %%I in (1,1,3) do (
    if exist "%BIN_DIR%\wkappbot-core.new.exe" timeout /t 1 /nobreak >nul
  )
  if exist "%BIN_DIR%\wkappbot-core.new.exe" (
    echo [BUILD] core hot-swap timeout -> kill all and retry promote
    taskkill /f /im wkappbot.exe >nul 2>nul
    taskkill /f /im wkappbot-core.exe >nul 2>nul
    if exist "%BIN_DIR%\wkappbot-core.new.exe" (
      move /Y "%BIN_DIR%\wkappbot-core.new.exe" "%BIN_DIR%\wkappbot-core.exe" >nul 2>nul
      if errorlevel 1 (
        if exist "%BIN_DIR%\wkappbot-core.exe" (
          echo [BUILD] core promote race resolved by existing exe
        ) else (
          echo [BUILD] core promote failed: "%BIN_DIR%\wkappbot-core.new.exe"
          exit /b 1
        )
      ) else (
        echo [BUILD] core promote ok
      )
    ) else (
      echo [BUILD] core .new disappeared after kill
    )
    if exist "%BIN_DIR%\wkappbot-core.exe" call "%BIN_DIR%\wkappbot-core.exe" eye tick --timeout 15 >nul 2>nul
  )
)

rem Cleanup stale handoff artifacts after promote/kill paths.
if exist "%BIN_DIR%\wkappbot-core.exe" del /q "%BIN_DIR%\wkappbot-core.new.exe" >nul 2>nul
del /q "%BIN_DIR%\wkappbot-core.old.exe" >nul 2>nul

echo [BUILD] done

rem Ask Claude what this tool can do, in the user's language (detected via code page)
if exist "%BIN_DIR%\wkappbot.exe" (
  "%BIN_DIR%\wkappbot.exe" help > "%TEMP%\wkappbot-help-tmp.txt" 2>nul
  for /f "tokens=3 delims=: " %%A in ('chcp') do set "CODEPAGE=%%A"
  set "LANG_HINT=English"
  if "!CODEPAGE!"=="949" set "LANG_HINT=Korean"
  if "!CODEPAGE!"=="932" set "LANG_HINT=Japanese"
  if "!CODEPAGE!"=="936" set "LANG_HINT=Simplified Chinese"
  if "!CODEPAGE!"=="950" set "LANG_HINT=Traditional Chinese"
  set "AI_PROMPT=WKAppBot just built successfully! Respond entirely in !LANG_HINT!. You are an expert WKAppBot assistant. Give a rich enthusiastic welcome: (1) explain in 2-3 sentences what WKAppBot is and why it matters for AI agents, (2) list 5 specific things the user can try RIGHT NOW with exact commands and what each does, (3) share 1 hidden pro tip that surprises most new users. Use clear sections and make it exciting."
  echo.
  echo ============================================================
  echo  WKAppBot is ready. Asking Claude for a personalized intro...
  echo ============================================================
  echo.
  "%BIN_DIR%\wkappbot.exe" chat "!AI_PROMPT!" 2>nul || echo [BUILD] (Claude CLI not available -- run: wkappbot chat "!AI_PROMPT!")
  del /q "%TEMP%\wkappbot-help-tmp.txt" 2>nul
)

exit /b 0

:publish_launcher
rem AOT: PublishSingleFile must NOT be passed (AOT is already single-file; combining them errors)
echo [BUILD]   proj : %LAUNCHER_PROJ%
echo [BUILD]   out  : %LAUNCHER_OUT%\wkappbot.exe
if "%NO_BUILD%"=="1" (
  echo [BUILD]   step : dotnet publish --no-build --no-restore (package only^)
  "%DOTNET_EXE%" publish "%LAUNCHER_PROJ%" --configuration Release --runtime win-x64 --self-contained true --no-restore --no-build -m:1 -v minimal
  if errorlevel 1 ( echo [BUILD] launcher publish failed & exit /b 1 )
  exit /b 0
)
echo [BUILD]   step : dotnet publish (no-restore)
"%DOTNET_EXE%" publish "%LAUNCHER_PROJ%" --configuration Release --runtime win-x64 --self-contained true --no-restore -m:1 -v minimal /p:SkipDeployLauncher=true
if not errorlevel 1 exit /b 0
echo [BUILD]   step : dotnet publish (with restore)
"%DOTNET_EXE%" publish "%LAUNCHER_PROJ%" --configuration Release --runtime win-x64 --self-contained true -m:1 -v minimal /p:SkipDeployLauncher=true
if errorlevel 1 (
  echo [BUILD] launcher publish failed
  exit /b 1
)
exit /b 0

:download_release
rem Download pre-built binaries from latest GitHub Release.
rem Tries gh CLI first (handles auth for private repos), then falls back
rem to unauthenticated PowerShell (works only when repo is public).
echo [BUILD]   Fetching latest release from GitHub...
set "DL_TMP=%TEMP%\wkappbot-dl-%RANDOM%"
mkdir "%DL_TMP%" 2>nul

rem --- Try gh CLI (preferred: works for private repos if user is logged in) ---
where gh >nul 2>nul
if not errorlevel 1 (
  echo [BUILD]   Using gh CLI...
  gh release download --repo kiexpert/wkappbot-sdk --pattern "*.zip" --dir "%DL_TMP%" 2>&1
  if not errorlevel 1 goto :extract_release
  echo [BUILD]   gh download failed -- trying unauthenticated...
)

rem --- Fallback: unauthenticated PowerShell (public repo only) ---
powershell -NoProfile -ExecutionPolicy Bypass -Command ^
  "try { $r = Invoke-RestMethod 'https://api.github.com/repos/kiexpert/wkappbot-sdk/releases/latest'; ^
   $a = $r.assets | Where-Object { $_.name -like '*.zip' } | Select-Object -First 1; ^
   if (!$a) { exit 1 }; ^
   Invoke-WebRequest $a.browser_download_url -OutFile '%DL_TMP%\wkappbot.zip' -UseBasicParsing; ^
   Write-Host '[BUILD]   Downloaded.' } catch { exit 1 }" 2>&1
if errorlevel 1 (
  echo [BUILD]   Download unavailable. wkappbot-core.exe must be obtained
  echo [BUILD]   from a GitHub Release (no source available in this repo).
  rmdir /s /q "%DL_TMP%" 2>nul
  exit /b 1
)

:extract_release
rem Extract zip and copy binaries to bin\
for %%F in ("%DL_TMP%\*.zip") do (
  powershell -NoProfile -ExecutionPolicy Bypass -Command "Expand-Archive -Path '%%F' -DestinationPath '%DL_TMP%\out' -Force"
)
if exist "%DL_TMP%\out\wkappbot.exe"      copy /y "%DL_TMP%\out\wkappbot.exe"      "%BIN_DIR%\wkappbot.exe"      >nul
if exist "%DL_TMP%\out\wkappbot-core.exe" copy /y "%DL_TMP%\out\wkappbot-core.exe" "%BIN_DIR%\wkappbot-core.exe" >nul
rmdir /s /q "%DL_TMP%" 2>nul

if not exist "%BIN_DIR%\wkappbot-core.exe" (
  echo [BUILD]   Extract failed -- core binary missing from release ZIP.
  exit /b 1
)
echo [BUILD]   Binaries installed from GitHub Release.

rem Seed HQ after download
if not exist "%BIN_DIR%\wkappbot.hq\skills"   mkdir "%BIN_DIR%\wkappbot.hq\skills"
if not exist "%BIN_DIR%\wkappbot.hq\handlers" mkdir "%BIN_DIR%\wkappbot.hq\handlers"
if exist "%ROOT_DIR%\skills"   xcopy /s /q /y "%ROOT_DIR%\skills\*"   "%BIN_DIR%\wkappbot.hq\skills\"   >nul
if exist "%ROOT_DIR%\handlers" xcopy /s /q /y "%ROOT_DIR%\handlers\*" "%BIN_DIR%\wkappbot.hq\handlers\" >nul

echo [BUILD]   done (downloaded from release)
rem In --download-only mode the caller exits immediately after this returns;
rem otherwise the launcher is rebuilt from source and redeployed below.
exit /b 0
