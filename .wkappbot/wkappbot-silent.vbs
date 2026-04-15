On Error Resume Next
Set fso = CreateObject("Scripting.FileSystemObject")
Set f = fso.OpenTextFile("d:\GitHub\WKAppBot\.wkappbot\watchdog.log", 8, True)
f.WriteLine "[WATCHDOG] " & Now() & " launch guardian"
f.Close
Set fso = Nothing
Set ws = CreateObject("WScript.Shell")
ws.CurrentDirectory = "d:\GitHub\WKAppBot"
ws.Run """D:\GitHub\WKAppBot\bin\wkappbot-core.exe"" eye guardian --respawn-delay 10 --poll-ms 10000 --tick-timeout-ms 5000", 0, False
