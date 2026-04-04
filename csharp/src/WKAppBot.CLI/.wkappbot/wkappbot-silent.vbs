On Error Resume Next
Set fso = CreateObject("Scripting.FileSystemObject")
Set f = fso.OpenTextFile("W:\GitHub\WKAppBot\csharp\src\WKAppBot.CLI\.wkappbot\watchdog.log", 8, True)
f.WriteLine "[WATCHDOG] " & Now() & " launch guardian"
f.Close
Set fso = Nothing
Set ws = CreateObject("WScript.Shell")
ws.CurrentDirectory = "W:\GitHub\WKAppBot\csharp\src\WKAppBot.CLI"
ws.Run """W:\SDK\bin\wkappbot-core.exe"" eye guardian --respawn-delay 10 --poll-ms 10000 --tick-timeout-ms 5000", 0, False
