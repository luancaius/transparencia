
namespace Deputies.Domain.ValueObjects;

/// <summary>
/// Holds multiple (key, value) IDs from various sources.
/// Example: { "CamaraApi": "123", "AnotherSource": "XYZ" }
/// </summary>
public class MultiSourceId
{
    private readonly Dictionary<string, string> _ids;

    /// <summary>
    /// Create a MultiSourceId with an initial (key, value).
    /// </summary>
    public MultiSourceId(string key, string value)
    {
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            throw new ArgumentException("Key and value cannot be null or empty.");

        _ids = new Dictionary<string, string> { { key, value } };
    }

    /// <summary>
    /// Private constructor for internal use or factory methods.
    /// </summary>
    private MultiSourceId(Dictionary<string, string> existingIds)
    {
        _ids = existingIds ?? new Dictionary<string, string>();
    }

    /// <summary>
    /// Exposes the dictionary of IDs as a read-only structure.
    /// </summary>
    public IReadOnlyDictionary<string, string> Ids => _ids;

    /// <summary>
    /// Adds another (key, value) pair to the MultiSourceId.
    /// </summary>
    public void Add(string key, string value)
    {
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            throw new ArgumentException("Key and value cannot be null or empty.");

        if (_ids.ContainsKey(key))
            throw new ArgumentException($"The key '{key}' already exists in MultiSourceId.");

        _ids[key] = value;
    }

    /// <summary>
    /// Static factory to create a MultiSourceId from an existing dictionary.
    /// </summary>
    public static MultiSourceId FromDictionary(Dictionary<string, string> dictionary)
    {
        return new MultiSourceId(new Dictionary<string, string>(dictionary));
    }

    public override bool Equals(object obj)
    {
        if (obj is MultiSourceId other)
        {
            // Compare dictionaries by sorting keys to ensure consistent order
            return _ids.OrderBy(x => x.Key)
                .SequenceEqual(other._ids.OrderBy(x => x.Key));
        }
        return false;
    }

    public override int GetHashCode()
    {
        return _ids.Aggregate(0, (acc, kv) => acc ^ kv.Key.GetHashCode() ^ kv.Value.GetHashCode());
    }

    public override string ToString()
    {
        return string.Join(", ", _ids.Select(kv => $"{kv.Key}: {kv.Value}"));
    }
}