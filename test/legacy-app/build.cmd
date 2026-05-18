@echo off
setlocal

pushd "%~dp0" >nul

where cmake >nul 2>nul
if errorlevel 1 (
  echo CMake was not found on PATH.
  popd >nul
  exit /b 1
)

set "BUILD_DIR=%CD%\build"

where cl >nul 2>nul
if not errorlevel 1 (
  cmake -S . -B "%BUILD_DIR%" -G "NMake Makefiles"
  if errorlevel 1 goto :fail
  cmake --build "%BUILD_DIR%" --config Release
  if errorlevel 1 goto :fail
  goto :done
)

where g++ >nul 2>nul
if not errorlevel 1 (
  cmake -S . -B "%BUILD_DIR%" -G "MinGW Makefiles"
  if errorlevel 1 goto :fail
  cmake --build "%BUILD_DIR%"
  if errorlevel 1 goto :fail
  goto :done
)

echo Neither cl.exe nor g++ was found on PATH.
echo Open a Visual Studio Developer Command Prompt or install MinGW.
popd >nul
exit /b 1

:fail
popd >nul
exit /b 1

:done
echo Built %BUILD_DIR%\LegacyControlZoo.exe
popd >nul
exit /b 0
