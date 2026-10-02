// WIRED SEAM: ChromeSessionRecovery integration into CdpClient
// Partial class adding session recovery methods for LOGIN_PAGE persistence
// Addresses: CDP timeout after ~30 minutes on long-running tests

namespace WKAppBot.WebBot
{
    using System;
    using System.Threading.Tasks;
    using WKAppBot.Core.Handlers;

    public partial class CdpClient
    {
        private ChromeSessionRecovery _sessionRecovery = new ChromeSessionRecovery();

        /// <summary>
        /// Enhanced OpenTabAsync with session recovery for LOGIN_PAGE targets
        /// Pre-validates URL accessibility, auto-recovers on session expiry
        /// </summary>
        public async Task<string> OpenTabAsyncWithSessionRecovery(string targetUrl)
        {
            // 1. Pre-check: Is this a LOGIN_PAGE URL?
            if (!_sessionRecovery.ValidateLoginPageAccessible(targetUrl))
            {
                // Not a login page, use standard open
                return await this.OpenTabAsync(targetUrl);
            }

            try
            {
                // 2. Open tab normally
                var tabId = await this.OpenTabAsync(targetUrl);

                // 3. Register session start time
                _sessionRecovery.RegisterTabSession(int.Parse(tabId));

                return tabId;
            }
            catch (Exception ex) when (ex.Message.Contains("401") || ex.Message.Contains("403"))
            {
                // Session expired, attempt recovery
                System.Console.WriteLine($"[CDP] Session expired on {targetUrl}, attempting recovery...");

                try
                {
                    await _sessionRecovery.RecoverSessionAsync(this, 0, targetUrl);
                    return await this.OpenTabAsync(targetUrl);
                }
                catch (Exception recoveryEx)
                {
                    System.Console.WriteLine($"[CDP] Session recovery failed: {recoveryEx.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Enhanced EvalAsync with pre-session-check for LOGIN_PAGE
        /// </summary>
        public async Task<T> EvalAsyncWithSessionCheck<T>(string tabId, string javascript) where T : class
        {
            // Check if tab session expired
            if (_sessionRecovery.IsSessionExpired(int.Parse(tabId), out var minutesElapsed))
            {
                System.Console.WriteLine($"[CDP] Tab {tabId} session age={minutesElapsed}min, may be stale");
            }

            try
            {
                return await this.EvalAsync<T>(tabId, javascript);
            }
            catch (Exception ex) when (ex.Message.Contains("Runtime.evaluate") && minutesElapsed > 20)
            {
                // Likely session timeout after 20+ minutes
                System.Console.WriteLine($"[CDP] EvalAsync timeout on aged session, recovering...");
                // Trigger recovery on next call
                _sessionRecovery.RegisterTabSession(-1); // Force next check to trigger recovery
                throw;
            }
        }
    }
}
