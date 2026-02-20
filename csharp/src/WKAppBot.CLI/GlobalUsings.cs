// WPF (UseWPF=true) changes implicit usings — many System.* namespaces that were
// auto-imported for console apps are no longer included. Re-add them here so
// existing code continues to work without per-file changes.
global using System.IO;
global using System.Net.Http;
