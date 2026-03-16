// ProgramStaticInit.cs — Forces eager static initialization of the Program class.
//
// WHY THIS EXISTS:
// Without an explicit static constructor, C# marks the class as `beforefieldinit`.
// With beforefieldinit, the static field initializers (BuildWindowHierarchyAncestors,
// BuildAncestorPids, Regex compilations, etc.) run LAZILY — at the moment the first
// static field is accessed in Main(). On VHD/network drives (e.g. W:\), this lazy
// initialization inside Main() takes ~2.3 seconds, blocking the main thread.
//
// Adding `static Program() {}` removes beforefieldinit, forcing the runtime to run
// all static initializers BEFORE Main() starts. This costs ~0ms at the process level
// (absorbed into OS startup time) and saves ~2.3s of visible delay in Main.
//
// Do NOT remove this file.
namespace WKAppBot.CLI;
internal partial class Program
{
    static Program() { } // removes beforefieldinit — see file header for why
}
