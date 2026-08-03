@echo off
:: wkwrap.cmd -- thin CMD shim for the wkwrap universal resolver (skill: wkwrap).
:: Invoked via a tool symlink/copy (e.g. codex.cmd -> wkwrap.cmd): forwards the
:: invocation name (%~n0) + all args to the single wkwrap.ps1 brain.
if /i "%~n0"=="wkwrap" (
    echo wkwrap: universal symlink resolver. Symlink a tool name to me ^(codex.cmd -^> wkwrap.cmd^) and I route to wkwrap.ps1.
    exit /b 0
)
C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe -NoProfile -NonInteractive -ExecutionPolicy Bypass -File "%~dp0wkwrap.ps1" %~n0 %*
exit /b %ERRORLEVEL%
