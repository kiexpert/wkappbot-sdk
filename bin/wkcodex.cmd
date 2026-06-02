@echo off
:: wkcodex.cmd -- CMD wrapper for wkcodex.ps1/wkharness (harness-managed)
:: Usage: wkcodex "task with 3+ skill refs" [-C dir]
C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe -NoProfile -NonInteractive -ExecutionPolicy Bypass -File "D:\GitHub\wkcodex.ps1" %*
exit /b %ERRORLEVEL%
