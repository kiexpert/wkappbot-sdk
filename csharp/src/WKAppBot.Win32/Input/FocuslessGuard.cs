namespace WKAppBot.Win32.Input;

/// <summary>
/// Global guard for --force-focusless mode.
/// When enabled, blocks any operation that steals focus or moves the physical cursor.
/// All focus-stealing Win32 calls (SetCursorPos, SendInput, SetForegroundWindow)
/// will throw FocuslessViolationException instead of executing.
///
/// Design principle: Guards are placed at the lowest-level wrapper functions
/// (MouseInput, KeyboardInput, SmartSetForegroundWindow) so no caller can bypass them.
///
/// Usage:
///   FocuslessGuard.Enabled = true;  // CLI에서 --force-focusless 파싱 시
///   MouseInput.Click(x, y);         // -> FocuslessViolationException!
///   UiaLocator.TryInvoke(el);       // -> OK (UIA Invoke는 focusless)
/// </summary>
public static class FocuslessGuard
{
    private static volatile bool _enabled;

    /// <summary>
    /// Enable/disable focusless guard. When true, all focus-stealing operations are blocked.
    /// Set this from CLI argument parsing (--force-focusless).
    /// </summary>
    public static bool Enabled
    {
        get => _enabled;
        set => _enabled = value;
    }

    /// <summary>
    /// Throws FocuslessViolationException if the guard is enabled.
    /// Call this at the top of any method that steals focus or moves the cursor.
    /// </summary>
    /// <param name="operation">Human-readable description of the blocked operation
    /// (e.g., "SetCursorPos", "SendInput(mouse)", "SetForegroundWindow")</param>
    public static void AssertAllowed(string operation)
    {
        if (_enabled)
            throw new FocuslessViolationException(operation);
    }

    /// <summary>
    /// Returns true if the operation would be blocked (guard is enabled).
    /// Use when throwing is not appropriate -- check return value and handle gracefully.
    /// Prints [FOCUSLESS] BLOCKED message to stderr.
    /// </summary>
    public static bool IsBlocked(string operation)
    {
        if (!_enabled) return false;
        Console.Error.WriteLine($"[FOCUSLESS] BLOCKED: {operation} -- --force-focusless is active");
        return true;
    }
}

/// <summary>
/// Exception thrown when a focus-stealing operation is attempted in --force-focusless mode.
/// Catch this at the command level to provide a clean error message.
/// </summary>
public class FocuslessViolationException : InvalidOperationException
{
    public string Operation { get; }

    public FocuslessViolationException(string operation)
        : base($"[FOCUSLESS] BLOCKED: {operation} -- --force-focusless is active. " +
               "This operation requires physical cursor/focus control which is prohibited in focusless mode.")
    {
        Operation = operation;
    }
}
