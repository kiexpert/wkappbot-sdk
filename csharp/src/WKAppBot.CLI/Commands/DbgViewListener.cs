using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace WKAppBot.CLI;

/// <summary>
/// Captures OutputDebugString messages via DBWIN_BUFFER shared memory.
/// Same mechanism as Sysinternals DebugView.
/// Only one global listener can be active at a time (auto-reset event exclusivity).
/// </summary>
internal sealed class DbgViewListener : IDisposable
{
    // DBWIN_BUFFER layout: [4 bytes PID][4096 bytes ANSI message] = 4100 total
    private const int BufferSize = 4100;
    private const int PidOffset = 0;
    private const int DataOffset = 4;
    private const int DataSize = 4096;

    private EventWaitHandle? _bufferReady;  // we signal this to let sender write
    private EventWaitHandle? _dataReady;    // sender signals this when data is available
    private MemoryMappedFile? _mmf;
    private MemoryMappedViewAccessor? _accessor;
    private Thread? _listenerThread;
    private volatile bool _disposed;
    private readonly ManualResetEventSlim _stopSignal = new(false);

    /// <summary>Filter by process ID. 0 = capture all.</summary>
    public int FilterPid { get; set; }

    /// <summary>Fired when a debug string is captured. Args: (pid, message).</summary>
    public event Action<int, string>? MessageReceived;

    /// <summary>
    /// Try to start listening. Returns false if another listener (e.g. DebugView) is active.
    /// </summary>
    public bool TryStart()
    {
        try
        {
            // Create or open the named events.
            // If another listener already owns these, CreateNew will throw
            // and CreateOrOpen may succeed but we'll compete for messages.
            bool createdBuffer, createdData;
            _bufferReady = new EventWaitHandle(false, EventResetMode.AutoReset, "DBWIN_BUFFER_READY", out createdBuffer);
            _dataReady = new EventWaitHandle(false, EventResetMode.AutoReset, "DBWIN_DATA_READY", out createdData);

            if (!createdBuffer || !createdData)
            {
                // Another listener exists — warn but continue (user chose to run)
                Console.Error.WriteLine("[DBG] WARNING: another debug listener detected (DebugView?). Messages may be split.");
            }

            _mmf = MemoryMappedFile.CreateOrOpen("DBWIN_BUFFER", BufferSize, MemoryMappedFileAccess.ReadWrite);
            _accessor = _mmf.CreateViewAccessor(0, BufferSize, MemoryMappedFileAccess.ReadWrite);

            _listenerThread = new Thread(ListenerLoop)
            {
                IsBackground = true,
                Name = "DbgView-Listener",
                Priority = ThreadPriority.AboveNormal // don't miss messages
            };
            _listenerThread.Start();
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[DBG] Failed to start: {ex.Message}");
            Cleanup();
            return false;
        }
    }

    private void ListenerLoop()
    {
        var handles = new WaitHandle[] { _dataReady!, _stopSignal.WaitHandle };
        var msgBuf = new byte[DataSize];

        // Signal that the buffer is ready for the first write
        try { _bufferReady!.Set(); } catch { return; }

        while (!_disposed)
        {
            int which;
            try { which = WaitHandle.WaitAny(handles, 1000); }
            catch (ObjectDisposedException) { break; }

            if (which == 1) break;         // stop signal
            if (which == WaitHandle.WaitTimeout) continue; // periodic check

            // Data is ready — read PID + message
            try
            {
                int pid = _accessor!.ReadInt32(PidOffset);
                _accessor.ReadArray(DataOffset, msgBuf, 0, DataSize);

                // Find null terminator
                int len = Array.IndexOf(msgBuf, (byte)0);
                if (len < 0) len = DataSize;

                // ANSI → string (OutputDebugStringA is ANSI; W variant also goes through ANSI path in shared memory)
                var message = Encoding.Default.GetString(msgBuf, 0, len).TrimEnd('\r', '\n');

                // Signal buffer ready for next write ASAP
                _bufferReady!.Set();

                // PID filter
                if (FilterPid > 0 && pid != FilterPid) continue;

                if (!string.IsNullOrEmpty(message))
                    MessageReceived?.Invoke(pid, message);
            }
            catch (ObjectDisposedException) { break; }
            catch
            {
                // Signal buffer ready even on error to avoid blocking senders
                try { _bufferReady!.Set(); } catch { }
            }
        }
    }

    public void Stop()
    {
        _disposed = true;
        _stopSignal.Set();
        _listenerThread?.Join(2000);
        Cleanup();
    }

    private void Cleanup()
    {
        try { _accessor?.Dispose(); } catch { }
        try { _mmf?.Dispose(); } catch { }
        try { _bufferReady?.Dispose(); } catch { }
        try { _dataReady?.Dispose(); } catch { }
        try { _stopSignal.Dispose(); } catch { }
        _accessor = null;
        _mmf = null;
        _bufferReady = null;
        _dataReady = null;
    }

    public void Dispose() => Stop();
}
