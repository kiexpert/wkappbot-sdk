@echo off
setlocal EnableExtensions EnableDelayedExpansion

rem ===========================================================================
rem  WKAppBot build.cmd  -  Publish, copy, and hot-swap deploy
rem
rem  Usage:
rem    build.cmd               Full build: compile + publish + copy + hot-swap
rem    build.cmd --deploy-only Skip publish entirely; re-deploy from existing
rem                            publish output (binaries must already be there)
rem    build.cmd --no-build    Skip compile; just package + copy + hot-swap.
rem                            Use when invoked from a csproj post-publish event
rem                            (MSBuild already compiled; avoids double-compile).
rem
rem  Deploy target:  <repo>\bin\
rem    wkappbot.exe       Launcher  (AOT, ~1 MB; starts core, relays pipe cmds,
rem                                  handles hot-swap trigger)
rem    wkappbot-core.exe  Core      (single-file ~25 MB; all CLI logic + Eye loop)
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

set "CLI_PROJ=%ROOT_DIR%\csharp\src\WKAppBot.CLI\WKAppBot.CLI.csproj"
set "LAUNCHER_PROJ=%ROOT_DIR%\csharp\src\WKAppBot.Launcher\WKAppBot.Launcher.csproj"
set "CLI_OUT=%ROOT_DIR%\csharp\src\WKAppBot.CLI\bin\Release\net8.0-windows10.0.22621.0\win-x64\publish"
set "LAUNCHER_OUT=%ROOT_DIR%\csharp\src\WKAppBot.Launcher\bin\Release\net8.0-windows\win-x64\publish"
set "BIN_DIR=%ROOT_DIR%\bin"
set "DEPLOY_ONLY=0"
set "NO_BUILD=0"
if /I "%~1"=="--deploy-only" set "DEPLOY_ONLY=1"
if /I "%~1"=="--no-build"    set "NO_BUILD=1"
if /I "%~2"=="--deploy-only" set "DEPLOY_ONLY=1"
if /I "%~2"=="--no-build"    set "NO_BUILD=1"

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
  echo [1/4] Publish launcher (AOT, single-file, self-contained)
  call :publish_launcher
  if errorlevel 1 exit /b 1

  echo [2/4] Publish core (single-file, self-contained)
  call :publish_core
  if errorlevel 1 exit /b 1

  if not exist "%LAUNCHER_OUT%\wkappbot.exe" (
    echo [BUILD] Missing launcher output: "%LAUNCHER_OUT%\wkappbot.exe"
    exit /b 1
  )
  if not exist "%CLI_OUT%\wkappbot-core.exe" (
    echo [BUILD] Missing core output: "%CLI_OUT%\wkappbot-core.exe"
    exit /b 1
  )
) else (
  echo [BUILD] deploy-only mode (skip publish)
)

echo [3/4] Deploy binaries to %BIN_DIR%
rem Core deploy: handled by csproj post-publish target (copies to .new.exe for hot-swap).

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

echo [4/4] Eye tick trigger (hot-swap detect + pipe drain)
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
"%DOTNET_EXE%" publish "%LAUNCHER_PROJ%" --configuration Release --runtime win-x64 --self-contained true --no-restore -m:1 -v minimal
if not errorlevel 1 exit /b 0
echo [BUILD]   step : dotnet publish (with restore)
"%DOTNET_EXE%" publish "%LAUNCHER_PROJ%" --configuration Release --runtime win-x64 --self-contained true -m:1 -v minimal
if errorlevel 1 (
  echo [BUILD] launcher publish failed
  exit /b 1
)
exit /b 0

:publish_core
echo [BUILD]   proj : %CLI_PROJ%
echo [BUILD]   out  : %CLI_OUT%\wkappbot-core.exe
if "%NO_BUILD%"=="1" (
  echo [BUILD]   step : dotnet publish --no-build --no-restore (package only^)
  "%DOTNET_EXE%" publish "%CLI_PROJ%" --configuration Release --runtime win-x64 --self-contained true --no-restore --no-build -m:1 -v minimal /p:PublishSingleFile=true /p:PublishTrimmed=false /p:PublishReadyToRun=false /p:PublishAot=false
  if errorlevel 1 ( echo [BUILD] core publish failed & exit /b 1 )
  exit /b 0
)
echo [BUILD]   step : dotnet publish (no-restore)
"%DOTNET_EXE%" publish "%CLI_PROJ%" --configuration Release --runtime win-x64 --self-contained true --no-restore -m:1 -v minimal /p:PublishSingleFile=true /p:PublishTrimmed=false /p:PublishReadyToRun=false /p:PublishAot=false
if not errorlevel 1 exit /b 0
echo [BUILD]   step : dotnet publish (with restore)
"%DOTNET_EXE%" publish "%CLI_PROJ%" --configuration Release --runtime win-x64 --self-contained true -m:1 -v minimal /p:PublishSingleFile=true /p:PublishTrimmed=false /p:PublishReadyToRun=false /p:PublishAot=false
if errorlevel 1 (
  echo [BUILD] core publish failed
  exit /b 1
)
exit /b 0
