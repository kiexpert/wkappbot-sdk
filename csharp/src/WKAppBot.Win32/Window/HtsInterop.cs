using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WKAppBot.Win32.Native;

namespace WKAppBot.Win32.Window;

/// <summary>
/// HTS-specific interop: WM_COPYDATA form open, MDI child management, memory tracking.
/// Consolidates 3 PowerShell patterns from VIGSOne/tools/leak_test_*.ps1:
///   - repeat:   simple open/close N times
///   - memory:   open M / close K per cycle + memory table
///   - ctx-only: anchor form + repeated open/close (V8 Context isolation)
/// </summary>
public static class HtsInterop
{
    private const int COPYDATA_OPEN_FORM = 100;

    /// <summary>
    /// Open an HTS form via WM_COPYDATA (dwData=100).
    /// Path is encoded as system-default ANSI (CP949 on Korean Windows)
    /// to match the MBCS target application.
    /// </summary>
    public static bool OpenForm(IntPtr hMainWnd, string xmfFilePath)
    {
        byte[] pathBytes = Encoding.Default.GetBytes(xmfFilePath + "\0");

        IntPtr lpData = Marshal.AllocHGlobal(pathBytes.Length);
        try
        {
            Marshal.Copy(pathBytes, 0, lpData, pathBytes.Length);

            var cds = new COPYDATASTRUCT
            {
                dwData = new IntPtr(COPYDATA_OPEN_FORM),
                cbData = pathBytes.Length,
                lpData = lpData
            };

            NativeMethods.SendMessageW(hMainWnd, NativeMethods.WM_COPYDATA, IntPtr.Zero, ref cds);
            return true;
        }
        finally
        {
            Marshal.FreeHGlobal(lpData);
        }
    }

    /// <summary>
    /// Close the active MDI child window.
    /// </summary>
    public static bool CloseActiveChild(IntPtr hMainWnd)
    {
        var hChild = WindowFinder.GetActiveMDIChild(hMainWnd);
        if (hChild == IntPtr.Zero) return false;

        NativeMethods.SendMessageW(hChild, NativeMethods.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        return true;
    }

    /// <summary>
    /// Take a memory snapshot of the target process.
    /// </summary>
    public static MemorySnapshot TakeMemorySnapshot(Process proc)
    {
        proc.Refresh();
        return new MemorySnapshot
        {
            Timestamp = DateTime.Now,
            WorkingSetMB = Math.Round(proc.WorkingSet64 / (1024.0 * 1024.0), 1),
            PrivateMemMB = Math.Round(proc.PrivateMemorySize64 / (1024.0 * 1024.0), 1),
        };
    }

    // ===================================================================
    // Pattern 1: REPEAT — simple open/close (leak_test_repeat.ps1)
    // ===================================================================

    /// <summary>
    /// Simple open/close form N times. Reports progress via callback.
    /// Port of: leak_test_repeat.ps1
    /// </summary>
    public static StressResult RunRepeat(
        IntPtr hMainWnd,
        string xmfFilePath,
        Process targetProc,
        int iterations,
        int openDelayMs = 1000,
        int closeDelayMs = 1000,
        Action<StressCycleEvent>? onCycle = null,
        CancellationToken ct = default)
    {
        var baseline = TakeMemorySnapshot(targetProc);
        var sw = Stopwatch.StartNew();
        int openOk = 0, closeOk = 0;

        for (int i = 1; i <= iterations; i++)
        {
            ct.ThrowIfCancellationRequested();

            // OPEN
            int before = WindowFinder.CountMDIChildren(hMainWnd);
            OpenForm(hMainWnd, xmfFilePath);
            Thread.Sleep(300);
            int after = WindowFinder.CountMDIChildren(hMainWnd);
            bool opened = after > before;
            if (opened) openOk++;

            onCycle?.Invoke(new StressCycleEvent
            {
                Cycle = i, Phase = "OPEN",
                Success = opened,
                MdiBefore = before, MdiAfter = after,
                Memory = TakeMemorySnapshot(targetProc),
                Baseline = baseline
            });

            Thread.Sleep(Math.Max(0, openDelayMs - 300));
            ct.ThrowIfCancellationRequested();

            // CLOSE
            bool closed = CloseActiveChild(hMainWnd);
            if (closed) closeOk++;

            onCycle?.Invoke(new StressCycleEvent
            {
                Cycle = i, Phase = "CLOSE",
                Success = closed,
                MdiBefore = after, MdiAfter = closed ? after - 1 : after,
                Memory = TakeMemorySnapshot(targetProc),
                Baseline = baseline
            });

            Thread.Sleep(closeDelayMs);
        }

        sw.Stop();
        var final = TakeMemorySnapshot(targetProc);
        int remainingMdi = WindowFinder.CountMDIChildren(hMainWnd);

        return new StressResult
        {
            Pattern = "repeat",
            Iterations = iterations,
            Elapsed = sw.Elapsed,
            Baseline = baseline, Final = final,
            OpenCount = iterations, CloseCount = iterations,
            OpenOk = openOk, CloseOk = closeOk,
            RemainingMdi = remainingMdi
        };
    }

    // ===================================================================
    // Pattern 2: MEMORY — asymmetric open/close + memory table (leak_test_memory.ps1)
    // ===================================================================

    /// <summary>
    /// Open openCount forms, close closeCount per cycle. Track memory each cycle.
    /// Port of: leak_test_memory.ps1
    /// </summary>
    public static StressResult RunMemory(
        IntPtr hMainWnd,
        string xmfFilePath,
        Process targetProc,
        int iterations,
        int openCount = 3,
        int closeCount = 1,
        int delayMs = 800,
        Action<StressCycleEvent>? onCycle = null,
        CancellationToken ct = default)
    {
        if (closeCount <= 0) closeCount = openCount;

        var baseline = TakeMemorySnapshot(targetProc);
        var sw = Stopwatch.StartNew();
        int totalOpened = 0, totalClosed = 0;

        for (int i = 1; i <= iterations; i++)
        {
            ct.ThrowIfCancellationRequested();

            // OPEN phase
            for (int j = 0; j < openCount; j++)
            {
                OpenForm(hMainWnd, xmfFilePath);
                totalOpened++;
                Thread.Sleep(delayMs);
            }

            // CLOSE phase
            for (int j = 0; j < closeCount; j++)
            {
                CloseActiveChild(hMainWnd);
                totalClosed++;
                Thread.Sleep(delayMs);
            }

            // Measure
            var mem = TakeMemorySnapshot(targetProc);
            int mdi = WindowFinder.CountMDIChildren(hMainWnd);

            onCycle?.Invoke(new StressCycleEvent
            {
                Cycle = i, Phase = "CYCLE",
                Success = true,
                MdiBefore = -1, MdiAfter = mdi,
                Memory = mem, Baseline = baseline
            });
        }

        // Close remaining MDI children
        int remaining = WindowFinder.CountMDIChildren(hMainWnd);
        for (int j = 0; j < remaining; j++)
        {
            CloseActiveChild(hMainWnd);
            totalClosed++;
            Thread.Sleep(300);
        }
        Thread.Sleep(1000);

        sw.Stop();
        var final = TakeMemorySnapshot(targetProc);

        return new StressResult
        {
            Pattern = "memory",
            Iterations = iterations,
            Elapsed = sw.Elapsed,
            Baseline = baseline, Final = final,
            OpenCount = totalOpened, CloseCount = totalClosed,
            OpenOk = totalOpened, CloseOk = totalClosed,
            RemainingMdi = WindowFinder.CountMDIChildren(hMainWnd)
        };
    }

    // ===================================================================
    // Pattern 3: CTX-ONLY — anchor form + repeated open/close (leak_test_ctx_only.ps1)
    // ===================================================================

    /// <summary>
    /// Keep anchor form open (DLL stays loaded), open/close 2nd form N times.
    /// Isolates V8 Context create/destroy leaks from DLL load/unload.
    /// Port of: leak_test_ctx_only.ps1
    /// </summary>
    public static StressResult RunCtxOnly(
        IntPtr hMainWnd,
        string xmfFilePath,
        Process targetProc,
        int iterations,
        int batchSize = 1,
        int delayMs = 800,
        Action<StressCycleEvent>? onCycle = null,
        CancellationToken ct = default)
    {
        var sw = Stopwatch.StartNew();

        // Phase 1: Open anchor form (DLL stays loaded)
        onCycle?.Invoke(new StressCycleEvent
        {
            Cycle = 0, Phase = "ANCHOR_OPEN",
            Success = true, MdiBefore = 0, MdiAfter = 0,
            Memory = TakeMemorySnapshot(targetProc),
            Baseline = TakeMemorySnapshot(targetProc)
        });

        OpenForm(hMainWnd, xmfFilePath);
        Thread.Sleep(1500);

        var baseline = TakeMemorySnapshot(targetProc);
        int anchorMdi = WindowFinder.CountMDIChildren(hMainWnd);

        onCycle?.Invoke(new StressCycleEvent
        {
            Cycle = 0, Phase = "ANCHOR_READY",
            Success = true, MdiBefore = 0, MdiAfter = anchorMdi,
            Memory = baseline, Baseline = baseline
        });

        int totalOpened = 0, totalClosed = 0;

        // Phase 2: Repeated open/close
        for (int i = 1; i <= iterations; i++)
        {
            ct.ThrowIfCancellationRequested();

            // OPEN batch
            for (int b = 0; b < batchSize; b++)
            {
                OpenForm(hMainWnd, xmfFilePath);
                totalOpened++;
                Thread.Sleep(Math.Max(300, delayMs / 2));
            }

            // CLOSE batch
            for (int b = 0; b < batchSize; b++)
            {
                CloseActiveChild(hMainWnd);
                totalClosed++;
                Thread.Sleep(Math.Max(300, delayMs / 2));
            }

            // Measure
            var mem = TakeMemorySnapshot(targetProc);
            int mdi = WindowFinder.CountMDIChildren(hMainWnd);

            onCycle?.Invoke(new StressCycleEvent
            {
                Cycle = i, Phase = "CYCLE",
                Success = true,
                MdiBefore = -1, MdiAfter = mdi,
                Memory = mem, Baseline = baseline
            });
        }

        // Phase 3: Close anchor form
        CloseActiveChild(hMainWnd);
        totalClosed++;
        Thread.Sleep(1000);

        onCycle?.Invoke(new StressCycleEvent
        {
            Cycle = iterations + 1, Phase = "ANCHOR_CLOSE",
            Success = true, MdiBefore = -1,
            MdiAfter = WindowFinder.CountMDIChildren(hMainWnd),
            Memory = TakeMemorySnapshot(targetProc),
            Baseline = baseline
        });

        sw.Stop();
        var final = TakeMemorySnapshot(targetProc);

        return new StressResult
        {
            Pattern = "ctx-only",
            Iterations = iterations,
            Elapsed = sw.Elapsed,
            Baseline = baseline, Final = final,
            OpenCount = totalOpened + 1,  // +1 for anchor
            CloseCount = totalClosed,
            OpenOk = totalOpened + 1,
            CloseOk = totalClosed,
            RemainingMdi = WindowFinder.CountMDIChildren(hMainWnd),
            TotalContexts = iterations * batchSize
        };
    }
}

// ===================================================================
// Data models
// ===================================================================

/// <summary>
/// Process memory snapshot at a point in time.
/// </summary>
public sealed class MemorySnapshot
{
    public DateTime Timestamp { get; init; }
    public double WorkingSetMB { get; init; }
    public double PrivateMemMB { get; init; }

    public double WsDeltaMB(MemorySnapshot baseline) =>
        Math.Round(WorkingSetMB - baseline.WorkingSetMB, 1);
    public double PrivDeltaMB(MemorySnapshot baseline) =>
        Math.Round(PrivateMemMB - baseline.PrivateMemMB, 1);
}

/// <summary>
/// Event emitted per cycle/phase during stress test.
/// </summary>
public sealed class StressCycleEvent
{
    public int Cycle { get; init; }
    /// <summary>"OPEN", "CLOSE", "CYCLE", "ANCHOR_OPEN", "ANCHOR_READY", "ANCHOR_CLOSE"</summary>
    public string Phase { get; init; } = "";
    public bool Success { get; init; }
    public int MdiBefore { get; init; }
    public int MdiAfter { get; init; }
    public MemorySnapshot Memory { get; init; } = null!;
    public MemorySnapshot Baseline { get; init; } = null!;
}

/// <summary>
/// Final summary of a stress test run.
/// </summary>
public sealed class StressResult
{
    public string Pattern { get; init; } = "";
    public int Iterations { get; init; }
    public TimeSpan Elapsed { get; init; }
    public MemorySnapshot Baseline { get; init; } = null!;
    public MemorySnapshot Final { get; init; } = null!;
    public int OpenCount { get; init; }
    public int CloseCount { get; init; }
    public int OpenOk { get; init; }
    public int CloseOk { get; init; }
    public int RemainingMdi { get; init; }
    public int TotalContexts { get; init; }

    public double PrivGrowthMB => Final.PrivDeltaMB(Baseline);
    public double WsGrowthMB => Final.WsDeltaMB(Baseline);

    /// <summary>Per-cycle KB (private memory growth / iterations)</summary>
    public double PerCycleKB => Iterations > 0
        ? Math.Round(PrivGrowthMB * 1024 / Iterations, 1)
        : 0;

    /// <summary>Per-context KB (for ctx-only pattern)</summary>
    public double PerContextKB => TotalContexts > 0
        ? Math.Round(PrivGrowthMB * 1024 / TotalContexts, 1)
        : 0;
}
