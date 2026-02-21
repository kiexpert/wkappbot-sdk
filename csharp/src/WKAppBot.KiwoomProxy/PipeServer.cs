// PipeServer.cs — Named Pipe server for KiwoomProxy.
// Listens on \\.\pipe\wkappbot_kiwoom for JSON-RPC requests from 64-bit CLI.
// Supports bidirectional communication: requests from CLI, events pushed to CLI.

using System.IO.Pipes;
using System.Text.Json;

namespace WKAppBot.KiwoomProxy;

/// <summary>
/// Named Pipe server that handles JSON-RPC requests and pushes COM events.
/// Single client at a time (one pipe instance).
/// </summary>
public class PipeServer : IDisposable
{
    public const string PipeName = "wkappbot_kiwoom";

    private NamedPipeServerStream? _pipeServer;
    private CancellationTokenSource _cts = new();
    private bool _disposed;
    private bool _clientConnected;

    /// <summary>Fired when a request is received. Handler must return the response.</summary>
    public event Func<PipeRequest, PipeResponse>? OnRequest;

    /// <summary>Whether a client is currently connected.</summary>
    public bool IsClientConnected => _clientConnected;

    /// <summary>
    /// Start listening for client connections in a loop.
    /// This should be called from a background thread (not STA).
    /// Uses BeginInvoke to marshal requests to the STA thread.
    /// </summary>
    public async Task ListenAsync(CancellationToken ct)
    {
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct, _cts.Token);
        var token = linkedCts.Token;

        while (!token.IsCancellationRequested)
        {
            try
            {
                _pipeServer = new NamedPipeServerStream(
                    PipeName,
                    PipeDirection.InOut,
                    1, // max instances
                    PipeTransmissionMode.Byte,
                    PipeOptions.Asynchronous);

                Console.WriteLine($"[KIWOOM] Pipe server waiting on \\\\.\\pipe\\{PipeName}");
                await _pipeServer.WaitForConnectionAsync(token);
                _clientConnected = true;
                Console.WriteLine("[KIWOOM] Pipe client connected");

                // Handle requests from this client
                await HandleClientAsync(_pipeServer, token);
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                Console.WriteLine($"[KIWOOM] Pipe error: {ex.Message}");
            }
            finally
            {
                _clientConnected = false;
                _pipeServer?.Dispose();
                _pipeServer = null;
            }

            // Small delay before re-listen
            if (!token.IsCancellationRequested)
                await Task.Delay(100, token).ConfigureAwait(false);
        }

        Console.WriteLine("[KIWOOM] Pipe server stopped");
    }

    private async Task HandleClientAsync(NamedPipeServerStream pipe, CancellationToken ct)
    {
        while (pipe.IsConnected && !ct.IsCancellationRequested)
        {
            try
            {
                var json = await PipeWire.ReadMessageAsync(pipe, ct);
                if (json == null) break; // client disconnected

                var request = PipeWire.Deserialize<PipeRequest>(json);
                if (request == null)
                {
                    var errorResp = new PipeResponse { Id = -1, Error = "Invalid request JSON" };
                    await PipeWire.WriteMessageAsync(pipe, errorResp, ct);
                    continue;
                }

                // Dispatch to handler
                PipeResponse response;
                if (OnRequest != null)
                {
                    try
                    {
                        response = OnRequest.Invoke(request);
                    }
                    catch (Exception ex)
                    {
                        response = new PipeResponse { Id = request.Id, Error = ex.Message };
                    }
                }
                else
                {
                    response = new PipeResponse { Id = request.Id, Error = "No request handler" };
                }

                await PipeWire.WriteMessageAsync(pipe, response, ct);
            }
            catch (OperationCanceledException) { break; }
            catch (IOException) { break; } // pipe broken
            catch (Exception ex)
            {
                Console.WriteLine($"[KIWOOM] Request handling error: {ex.Message}");
            }
        }

        Console.WriteLine("[KIWOOM] Pipe client disconnected");
    }

    /// <summary>
    /// Push an event to the connected client.
    /// Called from the STA thread when COM events arrive.
    /// </summary>
    public async Task PushEventAsync(PipeEvent evt, CancellationToken ct = default)
    {
        if (_pipeServer == null || !_clientConnected) return;

        try
        {
            await PipeWire.WriteMessageAsync(_pipeServer, evt, ct);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[KIWOOM] Event push error: {ex.Message}");
        }
    }

    public void Stop()
    {
        _cts.Cancel();
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        Stop();
        _pipeServer?.Dispose();
    }
}
