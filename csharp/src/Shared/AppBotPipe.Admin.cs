// AppBotPipe.Admin.cs -- Admin/sudo entry points for AppBotPipe.
// Linked via csproj: <Compile Include="..\..\Shared\AppBotPipe.Admin.cs" Link="Shared\AppBotPipe.Admin.cs" />
//
// Partial class extension. All admin/elevation logic enters through these
// methods so callers have a single reference point:
//
//   AppBotPipe.IsElevated()                    -- am I running as admin?
//   AppBotPipe.AdminPing(timeoutMs)            -- is the admin Eye responsive?
//   AppBotPipe.AdminExecute(cmd, args, ...)    -- run a command via admin Eye proxy
//   AppBotPipe.EnsureAdmin(reason)             -- ping, promote .new.exe, UAC spawn if needed
//
// Internal impl lives in WKAppBot.CLI (SudoHandler, ElevatedEyeClient).
// The thin-delegate wrappers here use reflection-free static forwarders
// set up by Core at startup via AppBotPipe.SetAdminImpl(...). Launcher
// gets the same API but admin spawn is a no-op (returns false) since
// Launcher itself shouldn't drive UAC.

using System.Security.Principal;

internal static partial class AppBotPipe
{
    // Impl delegates installed by Core at startup. Null on Launcher side.
    static Func<int, bool>? _implAdminPing;
    static Func<string, string[], int, int>? _implAdminExecute; // cmd, args, timeoutMs -> exit code (-1 = unreachable)
    static Func<string, bool>? _implEnsureAdmin; // reason -> true if admin path ready

    /// <summary>Install Core's admin implementation. Called once by CLI bootstrap.</summary>
    public static void SetAdminImpl(
        Func<int, bool>? adminPing,
        Func<string, string[], int, int>? adminExecute,
        Func<string, bool>? ensureAdmin)
    {
        _implAdminPing = adminPing;
        _implAdminExecute = adminExecute;
        _implEnsureAdmin = ensureAdmin;
    }

    // Cached once per process. Elevation does not change mid-process, so checking
    // WindowsIdentity on every call wastes ~200us. Cache on first read.
    static int _elevatedCache = -1; // -1 = unknown, 0 = false, 1 = true

    /// <summary>
    /// True iff the current process is running with administrator privileges.
    /// Result is cached for the lifetime of the process (elevation is immutable).
    /// </summary>
    public static bool IsElevated()
    {
        if (_elevatedCache >= 0) return _elevatedCache == 1;
        int v = 0;
        try
        {
            using var id = WindowsIdentity.GetCurrent();
            v = new WindowsPrincipal(id).IsInRole(WindowsBuiltInRole.Administrator) ? 1 : 0;
        }
        catch { v = 0; }
        System.Threading.Interlocked.CompareExchange(ref _elevatedCache, v, -1);
        return _elevatedCache == 1;
    }

    /// <summary>
    /// True if the admin Eye pipe (wkappbot_elevated) responds within timeoutMs.
    /// Safe to call from non-admin; returns false if the admin Eye is not running.
    /// </summary>
    public static bool AdminPing(int timeoutMs = 100)
        => _implAdminPing?.Invoke(timeoutMs) ?? PingAdminPipe(timeoutMs);

    /// <summary>
    /// Run <paramref name="cmd"/> with <paramref name="args"/> through the admin Eye proxy.
    /// Returns the command's exit code, or -1 if the admin Eye did not respond.
    /// </summary>
    public static int AdminExecute(string cmd, string[] args, int timeoutMs = 5000)
        => _implAdminExecute?.Invoke(cmd, args, timeoutMs) ?? -1;

    /// <summary>
    /// Ping admin Eye; if absent, promote any staged .new.exe, then spawn via UAC and
    /// wait for handshake. Returns true iff the admin path is ready afterwards.
    /// Caller should fall through to non-admin on false (UAC cancelled / spawn timeout).
    /// </summary>
    public static bool EnsureAdmin(string reason = "--sudo request")
        => _implEnsureAdmin?.Invoke(reason) ?? false;

    /// <summary>
    /// Direct named-pipe probe used by Launcher (no Core impl installed) -- no
    /// dependency on Core types. Kept lightweight; EnsureAdmin/AdminExecute require Core.
    /// </summary>
    static bool PingAdminPipe(int timeoutMs)
    {
        try
        {
            using var probe = new System.IO.Pipes.NamedPipeClientStream(
                ".", "wkappbot_elevated", System.IO.Pipes.PipeDirection.InOut);
            using var cts = new System.Threading.CancellationTokenSource(timeoutMs);
            probe.ConnectAsync(timeoutMs, cts.Token).GetAwaiter().GetResult();
            return probe.IsConnected;
        }
        catch { return false; }
    }
}
