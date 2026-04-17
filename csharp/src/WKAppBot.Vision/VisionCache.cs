using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace WKAppBot.Vision;

/// <summary>
/// 2-level Vision cache: memory (session) + disk (persistent JSON).
/// "경험치 축적 시스템" -- each element location is learned and reused.
///
/// Cache key = SHA256(class_path | description | WxH)
///   - class_path: Win32 window class hierarchy (e.g., "ApplicationFrameWindow/...")
///   - description: what we're looking for (YAML target.description)
///   - WxH: window dimensions (resizing invalidates cached coordinates)
///
/// Per-control learning:
///   - hit_count: how many times this cache entry was used
///   - success_count / fail_count: outcome tracking for future ML
///   - Entries with high fail rate get lower effective confidence
///
/// TTL: entries expire after ttl_days (default 7) -- app updates may change UI.
/// </summary>
public sealed class VisionCache : IDisposable
{
    private readonly string _cacheDir;
    private readonly int _ttlDays;
    private readonly ConcurrentDictionary<string, VisionCacheEntry> _memory = new();
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Create a VisionCache.
    /// </summary>
    /// <param name="cacheDir">Directory for disk cache (default: "data/vision_cache/entries")</param>
    /// <param name="ttlDays">Cache TTL in days (default: 7)</param>
    public VisionCache(string cacheDir = "data/vision_cache/entries", int ttlDays = 7)
    {
        _cacheDir = cacheDir;
        _ttlDays = ttlDays;

        // Ensure cache directory exists
        if (!Directory.Exists(_cacheDir))
            Directory.CreateDirectory(_cacheDir);

        // Pre-load disk entries into memory
        LoadDiskEntries();
    }

    /// <summary>
    /// Build the cache key string from components.
    /// </summary>
    public static string BuildKey(string classPath, string description, int windowWidth, int windowHeight)
    {
        return $"{classPath}|{description}|{windowWidth}x{windowHeight}";
    }

    /// <summary>
    /// Get SHA256 hash of a key (first 16 chars) for filename.
    /// Safe for Korean/special characters.
    /// </summary>
    private static string HashKey(string key)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(key));
        return Convert.ToHexString(bytes)[..16].ToLowerInvariant();
    }

    /// <summary>
    /// Try to get a cached entry. Returns null if not found or expired.
    /// Updates hit_count and last_used on hit.
    /// </summary>
    public VisionCacheEntry? Get(string classPath, string description, int windowWidth, int windowHeight)
    {
        var key = BuildKey(classPath, description, windowWidth, windowHeight);
        var hash = HashKey(key);

        // Level 1: Memory
        if (_memory.TryGetValue(hash, out var entry))
        {
            if (IsExpired(entry))
            {
                _memory.TryRemove(hash, out _);
                TryDeleteDisk(hash);
                return null;
            }

            entry.HitCount++;
            entry.LastUsed = DateTime.UtcNow;

            // Adjust effective confidence by success rate
            // (future ML hook -- for now just return the entry)
            return entry;
        }

        // Level 2: Disk (already loaded at startup, so this shouldn't happen often)
        var diskEntry = TryLoadDisk(hash);
        if (diskEntry != null)
        {
            if (IsExpired(diskEntry))
            {
                TryDeleteDisk(hash);
                return null;
            }

            diskEntry.HitCount++;
            diskEntry.LastUsed = DateTime.UtcNow;
            _memory[hash] = diskEntry;
            return diskEntry;
        }

        return null;
    }

    /// <summary>
    /// Store a cache entry (memory + disk).
    /// </summary>
    public void Put(string classPath, string description, int windowWidth, int windowHeight,
                    VisionCacheEntry entry)
    {
        var key = BuildKey(classPath, description, windowWidth, windowHeight);
        var hash = HashKey(key);

        // Store in memory
        _memory[hash] = entry;

        // Persist to disk
        SaveDisk(hash, entry);
    }

    /// <summary>
    /// Record that a cached location led to a successful action.
    /// Increments success_count for per-control learning.
    /// </summary>
    public void RecordSuccess(string classPath, string description, int windowWidth, int windowHeight)
    {
        var key = BuildKey(classPath, description, windowWidth, windowHeight);
        var hash = HashKey(key);

        if (_memory.TryGetValue(hash, out var entry))
        {
            entry.SuccessCount++;
            entry.LastUsed = DateTime.UtcNow;
            SaveDisk(hash, entry);
        }
    }

    /// <summary>
    /// Record that a cached location led to a failed action.
    /// Increments fail_count. High fail rate -> entry may need refresh.
    /// </summary>
    public void RecordFailure(string classPath, string description, int windowWidth, int windowHeight)
    {
        var key = BuildKey(classPath, description, windowWidth, windowHeight);
        var hash = HashKey(key);

        if (_memory.TryGetValue(hash, out var entry))
        {
            entry.FailCount++;
            entry.LastUsed = DateTime.UtcNow;

            // Auto-invalidate if success rate drops below 30%
            if (entry.SuccessRate < 0.3 && (entry.SuccessCount + entry.FailCount) >= 3)
            {
                _memory.TryRemove(hash, out _);
                TryDeleteDisk(hash);
                return;
            }

            SaveDisk(hash, entry);
        }
    }

    /// <summary>
    /// Invalidate a specific cache entry (e.g., when Vision API returns different results).
    /// </summary>
    public void Invalidate(string classPath, string description, int windowWidth, int windowHeight)
    {
        var key = BuildKey(classPath, description, windowWidth, windowHeight);
        var hash = HashKey(key);
        _memory.TryRemove(hash, out _);
        TryDeleteDisk(hash);
    }

    /// <summary>
    /// Get cache statistics.
    /// </summary>
    public (int totalEntries, int expiredEntries, double avgSuccessRate) GetStats()
    {
        int total = _memory.Count;
        int expired = 0;
        double totalRate = 0;
        int ratedCount = 0;

        foreach (var entry in _memory.Values)
        {
            if (IsExpired(entry)) expired++;
            if (entry.SuccessCount + entry.FailCount > 0)
            {
                totalRate += entry.SuccessRate;
                ratedCount++;
            }
        }

        double avgRate = ratedCount > 0 ? totalRate / ratedCount : 1.0;
        return (total, expired, avgRate);
    }

    /// <summary>
    /// Clean up expired entries from memory and disk.
    /// </summary>
    public int CleanExpired()
    {
        int removed = 0;
        foreach (var (hash, entry) in _memory)
        {
            if (IsExpired(entry))
            {
                _memory.TryRemove(hash, out _);
                TryDeleteDisk(hash);
                removed++;
            }
        }
        return removed;
    }

    // -- Private helpers ------------------------------------─

    private bool IsExpired(VisionCacheEntry entry)
    {
        return (DateTime.UtcNow - entry.CreatedAt).TotalDays > _ttlDays;
    }

    private string GetDiskPath(string hash)
    {
        return Path.Combine(_cacheDir, $"{hash}.json");
    }

    private void SaveDisk(string hash, VisionCacheEntry entry)
    {
        try
        {
            var json = JsonSerializer.Serialize(entry, JsonOpts);
            File.WriteAllText(GetDiskPath(hash), json);
        }
        catch { /* Disk write failure is non-fatal */ }
    }

    private VisionCacheEntry? TryLoadDisk(string hash)
    {
        try
        {
            var path = GetDiskPath(hash);
            if (!File.Exists(path)) return null;
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<VisionCacheEntry>(json, JsonOpts);
        }
        catch { return null; }
    }

    private void TryDeleteDisk(string hash)
    {
        try
        {
            var path = GetDiskPath(hash);
            if (File.Exists(path)) File.Delete(path);
        }
        catch { /* Non-fatal */ }
    }

    private void LoadDiskEntries()
    {
        try
        {
            if (!Directory.Exists(_cacheDir)) return;

            foreach (var file in Directory.GetFiles(_cacheDir, "*.json"))
            {
                try
                {
                    var json = File.ReadAllText(file);
                    var entry = JsonSerializer.Deserialize<VisionCacheEntry>(json, JsonOpts);
                    if (entry != null)
                    {
                        var hash = Path.GetFileNameWithoutExtension(file);
                        if (!IsExpired(entry))
                            _memory[hash] = entry;
                        else
                            TryDeleteDisk(hash);
                    }
                }
                catch { /* Skip corrupt entries */ }
            }
        }
        catch { /* Directory read failure -- start with empty cache */ }
    }

    public void Dispose()
    {
        // Persist all memory entries to disk on shutdown
        foreach (var (hash, entry) in _memory)
        {
            SaveDisk(hash, entry);
        }
    }
}
