# DPI Coordinate Fix for WKAppBot Core

## Bug
ChromeLauncher.ComputePlacementNearCaller() returns Win32 PHYSICAL pixels. Those values are passed verbatim to Browser.setWindowBounds and to Chrome --window-position, both of which expect LOGICAL/CSS pixels. At 150% DPI Chrome lands 1.5x too far.

## Scope
3 files in D:/GitHub/WKAppBot/csharp/src/WKAppBot.WebBot
