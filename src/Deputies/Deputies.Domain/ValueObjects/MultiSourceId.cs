namespace Deputies.Domain.ValueObjects
{
    public class MultiSourceId
    {
        private readonly Dictionary<string, string> _ids;

        public MultiSourceId(Dictionary<string, string> ids)
        {
            if (ids == null || ids.Count == 0 || ids.Values.All(string.IsNullOrEmpty))
                throw new ArgumentException("A valid MultiSourceId must have at least one non-null ID.");

            _ids = ids;
        }

        public string this[string source]
        {
            get => _ids.ContainsKey(source) ? _ids[source] : null;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("ID value cannot be null or empty.");
                _ids[source] = value;
            }
        }

        public IReadOnlyDictionary<string, string> Ids => _ids;

        public override bool Equals(object obj)
        {
            if (obj is MultiSourceId other)
                return _ids.SequenceEqual(other._ids);
            return false;
        }

        public override int GetHashCode() => _ids.Aggregate(0, (acc, kv) => acc ^ kv.Key.GetHashCode() ^ kv.Value.GetHashCode());

        public override string ToString() => string.Join(", ", _ids.Select(kv => $"{kv.Key}: {kv.Value}"));
    }
}