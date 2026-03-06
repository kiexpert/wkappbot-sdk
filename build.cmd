@echo off
setlocal
set "SDK_DIR=W:\SDK\dotnet"
set "PROJECT_DIR=W:\GitHub\WKAppBot\csharp\src\WKAppBot.CLI"
set "OUT_DIR=%PROJECT_DIR%\bin\Release\net8.0-windows10.0.22621.0\win-x64\publish"
echo Building wkappbot (self-contained x64 publish)...
"%SDK_DIR%\dotnet" publish "%PROJECT_DIR%" --configuration Release --runtime win-x64 --self-contained true /p:PublishSingleFile=true /p:PublishTrimmed=false /p:PublishReadyToRun=false
if errorlevel 1 (
  echo Publish failed. Check log above.
  exit /b 1
)
echo Deploying published exe...
copy /y "%OUT_DIR%\wkappbot.exe" "W:\SDK\bin\wkappbot.exe" >nul
if errorlevel 1 (
  echo Primary copy failed: staging fallback...
  del /q "W:\SDK\bin\wkappbot*.exe*" 2>nul
  copy /y "%OUT_DIR%\wkappbot.exe" "W:\SDK\bin\wkappbot.new.exe" >nul
  if errorlevel 1 (
    echo Secondary copy failed; please stop wkappbot.exe and rerun.
    exit /b 1
  )
  echo Secondary copy succeeded; wkappbot.new.exe staged.
  call "W:\SDK\bin\wkappbot.new.exe" eye tick
  exit /b 0
)
echo Build/deploy done. AppBotEye will broadcast policy on next run.
call "W:\SDK\bin\wkappbot.exe" eye tick
endlocal
