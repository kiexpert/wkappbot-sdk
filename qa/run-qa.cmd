@echo off
rem All-platform QA launcher (Windows). Python: %PYTHON_CMD% -> py -3 -> python.
setlocal
chcp 65001 >nul
set PYTHONIOENCODING=utf-8
if defined PYTHON_CMD (
  "%PYTHON_CMD%" "%~dp0run-qa.py" %*
  exit /b %ERRORLEVEL%
)
where py >nul 2>&1
if %ERRORLEVEL%==0 (
  py -3 "%~dp0run-qa.py" %*
) else (
  python "%~dp0run-qa.py" %*
)
exit /b %ERRORLEVEL%
