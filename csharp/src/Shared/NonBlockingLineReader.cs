// SPDX-License-Identifier: MIT
using System.Collections.Concurrent;

namespace WKAppBot.Shared;

/// <summary>
/// Reusable non-blocking line reader for interactive CLI loops.
///
/// A dedicated background thread calls Console.ReadLine() continuously and pushes
/// completed lines into a bounded queue. The main loop calls Take / TryTake to
/// consume them -- it never blocks on the TTY directly, so long-running work
/// between reads (AI calls, file IO, ...) does not prevent the user from typing
/// ahead. Lines the user submits while the main loop is busy land in the queue
/// and are available on the next Take with zero visible latency.
///
/// Terminal-native line editing is preserved: because we delegate to ReadLine(),
/// the OS / shell handles backspace, left/right arrow cursor moves, and command
/// history (F7/F8 on cmd.exe, readline bindings in bash, etc.). No raw-mode
/// character-by-character handling needed -- the cost for richer editing would
/// be a full TUI rewrite.
///
/// Usage:
///   using var reader = new NonBlockingLineReader();
///   while (true)
///   {
///       Console.Write("prompt> ");
///       var line = reader.Take();
///       if (line == null) break;  // EOF (stdin closed / Ctrl+D / Ctrl+Z)
///       Handle(line);
///   }
/// </summary>
public sealed class NonBlockingLineReader : IDisposable
{
    private readonly BlockingCollection<string?> _queue; // null sentinel = EOF
    private readonly Thread _readerThread;
    private readonly CancellationTokenSource _cts = new();
    private volatile bool _eofReceived;

    /// <summary>
    /// <paramref name="boundedCapacity"/> caps the pre-type-ahead buffer so a
    /// runaway paste does not balloon memory. 64 entries is enough for any
    /// sane human typing pattern yet small enough that a stuck reader applies
    /// back-pressure almost immediately.
    /// </summary>
    public NonBlockingLineReader(int boundedCapacity = 64)
    {
        if (boundedCapacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(boundedCapacity));

        _queue = new BlockingCollection<string?>(boundedCapacity);
        _readerThread = new Thread(ReaderLoop)
        {
            IsBackground = true,
            Name = "nonblocking-line-reader",
        };
        _readerThread.Start();
    }

    /// <summary>Lines currently pre-queued (the user typed ahead while caller was busy).</summary>
    public int PendingCount => _queue.Count;

    /// <summary>Reader hit EOF (stdin closed / Ctrl+D / Ctrl+Z+Enter).</summary>
    public bool IsEof => _eofReceived;

    /// <summary>
    /// Block until a line arrives. Returns the line, or null when the reader has
    /// hit EOF and the queue is drained. Respects the supplied <paramref name="ct"/>.
    /// </summary>
    public string? Take(CancellationToken ct = default)
    {
        try
        {
            return _queue.Take(ct);
        }
        catch (InvalidOperationException)
        {
            // Queue completed (no more lines will ever arrive).
            return null;
        }
        catch (OperationCanceledException)
        {
            return null;
        }
    }

    /// <summary>
    /// Non-blocking poll with optional wait. Returns true if a line was retrieved.
    /// <paramref name="timeoutMs"/> = 0 for a pure poll, -1 to wait indefinitely.
    /// </summary>
    public bool TryTake(out string? line, int timeoutMs = 0, CancellationToken ct = default)
    {
        line = null;
        try
        {
            return _queue.TryTake(out line, timeoutMs, ct);
        }
        catch (ObjectDisposedException) { return false; }
        catch (OperationCanceledException) { return false; }
    }

    private void ReaderLoop()
    {
        try
        {
            while (!_cts.IsCancellationRequested)
            {
                string? line;
                try
                {
                    line = Console.ReadLine();
                }
                catch (IOException)
                {
                    // stdin closed (pipe ended) -- treat as EOF.
                    line = null;
                }
                catch (InvalidOperationException)
                {
                    // Console handle released after Dispose. Stop cleanly.
                    line = null;
                }

                if (line == null)
                {
                    _eofReceived = true;
                    try { _queue.Add(null, _cts.Token); } catch { }
                    break;
                }

                try { _queue.Add(line, _cts.Token); }
                catch (OperationCanceledException) { break; }
                catch (InvalidOperationException) { break; } // queue completed
            }
        }
        catch
        {
            // Last-resort: never throw from the background thread.
        }
        finally
        {
            try { _queue.CompleteAdding(); } catch { }
        }
    }

    public void Dispose()
    {
        try { _cts.Cancel(); } catch { }
        try { _queue.CompleteAdding(); } catch { }
        // Don't Join -- the reader is blocked on Console.ReadLine() which cannot
        // be cancelled from another thread without P/Invoke trickery. Background
        // + daemonic is enough; the thread dies with the process.
        try { _cts.Dispose(); } catch { }
        try { _queue.Dispose(); } catch { }
    }
}