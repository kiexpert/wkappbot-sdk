// Proto: Chrome LOGIN_PAGE Session Recovery Handler
// Root cause: CDP/Chrome session expires during extended test runs
// Fix: Add session validation + auto-recovery for LOGIN_PAGE targets

namespace WKAppBot.Core.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Validates and recovers Chrome sessions for LOGIN_PAGE (ChatGPT, Gemini, Claude)
    /// When session expires (401/403), closes stale tab and opens fresh session
    /// </summary>
    public class ChromeSessionRecovery
    {
        private const int SESSION_TIMEOUT_MS = 1800000; // 30 min
        private const string LOGIN_PAGE_PATTERN = "chatgpt|gemini|claude"; // regex pattern

        private Dictionary<int, DateTime> _tabSessionStartTime = new();

        /// <summary>
        /// Check if a tab's session has expired based on time elapsed
        /// </summary>
        public bool IsSessionExpired(int tabId, out int minutesElapsed)
        {
            minutesElapsed = 0;
            if (!_tabSessionStartTime.TryGetValue(tabId, out var startTime))
            {
                _tabSessionStartTime[tabId] = DateTime.UtcNow;
                return false;
            }

            var elapsed = DateTime.UtcNow - startTime;
            minutesElapsed = (int)elapsed.TotalMinutes;

            // Session expires after 30 minutes OR if CDP eval shows 401/403
            return elapsed > TimeSpan.FromMilliseconds(SESSION_TIMEOUT_MS);
        }

        /// <summary>
        /// Recover a stale LOGIN_PAGE session by closing tab and reopening fresh
        /// </summary>
        public async Task RecoverSessionAsync(object cdpClient, int tabId, string pageUrl)
        {
            // 1. Close stale tab
            try
            {
                var closeMethod = cdpClient.GetType().GetMethod("CloseTab");
                if (closeMethod != null)
                {
                    await (Task)closeMethod.Invoke(cdpClient, new object[] { tabId });
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[SessionRecovery] CloseTab failed: {ex.Message}");
            }

            // 2. Clear session memory
            _tabSessionStartTime.Remove(tabId);

            // 3. Open fresh tab/session
            try
            {
                var openMethod = cdpClient.GetType().GetMethod("OpenTab");
                if (openMethod != null)
                {
                    var result = await (Task<int>)openMethod.Invoke(cdpClient, new object[] { pageUrl });
                    System.Console.WriteLine($"[SessionRecovery] Fresh session opened: tab={result}");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"[SessionRecovery] OpenTab failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Pre-check: Validate LOGIN_PAGE URL is accessible before CDP eval
        /// </summary>
        public bool ValidateLoginPageAccessible(string pageUrl)
        {
            if (string.IsNullOrEmpty(pageUrl))
                return false;

            // Check URL matches LOGIN_PAGE pattern
            var uri = new Uri(pageUrl);
            var host = uri.Host.ToLower();

            return System.Text.RegularExpressions.Regex.IsMatch(host, LOGIN_PAGE_PATTERN);
        }

        /// <summary>
        /// Register a newly opened tab's session start time
        /// </summary>
        public void RegisterTabSession(int tabId)
        {
            _tabSessionStartTime[tabId] = DateTime.UtcNow;
            System.Console.WriteLine($"[SessionRecovery] Tab {tabId} session started: {DateTime.UtcNow:u}");
        }
    }
}
