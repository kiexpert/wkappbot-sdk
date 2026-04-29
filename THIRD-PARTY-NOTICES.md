# Third-Party Notices

WKAppBot SDK (launcher source) depends on the following open-source packages:

## .NET Runtime

**Microsoft .NET 8**  
License: MIT  
https://github.com/dotnet/runtime

## NuGet packages (launcher)

| Package | Version | License |
|---------|---------|---------|
| Microsoft.DotNet.ILCompiler | 8.0.x | MIT |
| Microsoft.NET.ILLink.Tasks | 8.0.x | MIT |
| Microsoft.NET.Test.Sdk | 17.x | MIT |
| xunit | 2.x | Apache 2.0 |
| xunit.runner.visualstudio | 2.x | Apache 2.0 |

Run `dotnet list package --include-transitive` in `csharp/` for the full dependency list.

## Core binary

`wkappbot-core.exe` is closed-source and distributed as a pre-built binary.
It includes additional third-party components not listed here.

---

All trademarks are the property of their respective owners.
