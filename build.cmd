@echo off
setlocal EnableExtensions

set "SDK_DIR=W:\SDK\dotnet"
set "DOTNET_EXE=%SDK_DIR%\dotnet.exe"
set "SDK_VER=8.0.418"
set "WORKLOAD_LOCATOR=%SDK_DIR%\sdk\%SDK_VER%\Sdks\Microsoft.NET.SDK.WorkloadAutoImportPropsLocator\Sdk"
set "MANIFEST_LOCATOR=%SDK_DIR%\sdk\%SDK_VER%\Sdks\Microsoft.NET.SDK.WorkloadManifestTargetsLocator\Sdk"

set "ROOT_DIR=W:\GitHub\WKAppBot"
set "CLI_PROJ=%ROOT_DIR%\csharp\src\WKAppBot.CLI\WKAppBot.CLI.csproj"
set "LAUNCHER_PROJ=%ROOT_DIR%\csharp\src\WKAppBot.Launcher\WKAppBot.Launcher.csproj"
set "CLI_OUT=%ROOT_DIR%\csharp\src\WKAppBot.CLI\bin\Release\net8.0-windows10.0.22621.0\win-x64\publish"
set "LAUNCHER_OUT=%ROOT_DIR%\csharp\src\WKAppBot.Launcher\bin\Release\net8.0-windows\win-x64\publish"
set "BIN_DIR=W:\SDK\bin"
set "DEPLOY_ONLY=0"
if /I "%~1"=="--deploy-only" set "DEPLOY_ONLY=1"

set "DOTNET_CLI_HOME=%ROOT_DIR%\.dotnet"
set "DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1"
set "DOTNET_CLI_TELEMETRY_OPTOUT=1"
set "MSBuildEnableWorkloadResolver=false"

if not exist "%DOTNET_EXE%" set "DOTNET_EXE=C:\Program Files\dotnet\dotnet.exe"
if exist "%DOTNET_EXE%" if not exist "%WORKLOAD_LOCATOR%" set "DOTNET_EXE=C:\Program Files\dotnet\dotnet.exe"
if exist "%DOTNET_EXE%" if not exist "%MANIFEST_LOCATOR%" set "DOTNET_EXE=C:\Program Files\dotnet\dotnet.exe"
if not exist "%DOTNET_EXE%" (
  echo [BUILD] dotnet.exe not found.
  exit /b 1
)

if "%DEPLOY_ONLY%"=="0" (
  echo [1/4] Publish launcher (single-file, self-contained)
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

echo [3/4] Deploy binaries
echo [BUILD] Deploy is handled by csproj post-publish targets.
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

echo [4/4] Eye tick trigger
call "%BIN_DIR%\wkappbot-core.exe" eye tick >nul 2>nul

rem Hot-swap watchdog:
rem If *.new.exe is still present after 3s, treat as failure, kill all, and promote .new -> .exe.
if exist "%BIN_DIR%\wkappbot.new.exe" (
  echo [BUILD] waiting launcher hot-swap ^(max 3s^)...
  for /L %%I in (1,1,3) do (
    if exist "%BIN_DIR%\wkappbot.new.exe" timeout /t 1 /nobreak >nul
  )
  if exist "%BIN_DIR%\wkappbot.new.exe" (
    echo [BUILD] launcher hot-swap timeout -> kill all and retry promote
    taskkill /f /im wkappbot.exe >nul 2>nul
    taskkill /f /im wkappbot-core.exe >nul 2>nul
    if exist "%BIN_DIR%\wkappbot.new.exe" (
      move /Y "%BIN_DIR%\wkappbot.new.exe" "%BIN_DIR%\wkappbot.exe" >nul 2>nul
      if errorlevel 1 (
        if exist "%BIN_DIR%\wkappbot.exe" (
          echo [BUILD] launcher promote race resolved by existing exe
        ) else (
          echo [BUILD] launcher promote failed: "%BIN_DIR%\wkappbot.new.exe"
          exit /b 1
        )
      ) else (
        echo [BUILD] launcher promote ok
      )
    ) else (
      echo [BUILD] launcher .new disappeared after kill
    )
  )
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
    if exist "%BIN_DIR%\wkappbot-core.exe" call "%BIN_DIR%\wkappbot-core.exe" eye tick >nul 2>nul
  )
)

rem Cleanup stale handoff artifacts after promote/kill paths.
if exist "%BIN_DIR%\wkappbot.exe" del /q "%BIN_DIR%\wkappbot.new.exe" >nul 2>nul
if exist "%BIN_DIR%\wkappbot-core.exe" del /q "%BIN_DIR%\wkappbot-core.new.exe" >nul 2>nul
del /q "%BIN_DIR%\wkappbot.old.exe" >nul 2>nul
del /q "%BIN_DIR%\wkappbot-core.old.exe" >nul 2>nul

echo [BUILD] done
exit /b 0

:publish_launcher
"%DOTNET_EXE%" publish "%LAUNCHER_PROJ%" --configuration Release --runtime win-x64 --self-contained true --no-restore -m:1 -v minimal /p:PublishSingleFile=true /p:PublishTrimmed=false
if not errorlevel 1 exit /b 0
echo [BUILD] launcher no-restore failed -> retry with restore
"%DOTNET_EXE%" publish "%LAUNCHER_PROJ%" --configuration Release --runtime win-x64 --self-contained true -m:1 -v minimal /p:PublishSingleFile=true /p:PublishTrimmed=false
if errorlevel 1 (
  echo [BUILD] launcher publish failed
  exit /b 1
)
exit /b 0

:publish_core
"%DOTNET_EXE%" publish "%CLI_PROJ%" --configuration Release --runtime win-x64 --self-contained true --no-restore -m:1 -v minimal /p:PublishSingleFile=true /p:PublishTrimmed=false /p:PublishReadyToRun=false /p:PublishAot=false
if not errorlevel 1 exit /b 0
echo [BUILD] core no-restore failed -> retry with restore
"%DOTNET_EXE%" publish "%CLI_PROJ%" --configuration Release --runtime win-x64 --self-contained true -m:1 -v minimal /p:PublishSingleFile=true /p:PublishTrimmed=false /p:PublishReadyToRun=false /p:PublishAot=false
if errorlevel 1 (
  echo [BUILD] core publish failed
  exit /b 1
)
exit /b 0
