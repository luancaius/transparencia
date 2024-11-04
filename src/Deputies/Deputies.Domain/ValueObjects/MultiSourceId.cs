using System;
using System.Collections.Generic;
using System.Linq;

namespace Deputies.Domain.ValueObjects
{
    public class MultiSourceId
    {
        private readonly Dictionary<string, string> _ids;

        public MultiSourceId(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                throw new ArgumentException("Key and value cannot be null or empty.");

            _ids = new Dictionary<string, string> { { key, value } };
        }

        public void Add(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                throw new ArgumentException("Key and value cannot be null or empty.");

            if (_ids.ContainsKey(key))
                throw new ArgumentException($"The key '{key}' already exists in MultiSourceId.");

            _ids[key] = value;
        }

        public string this[string source]
        {
            get => _ids.ContainsKey(source) ? _ids[source] : null;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("ID value cannot be null or empty.");

                if (_ids.ContainsKey(source))
                    throw new ArgumentException($"The key '{source}' already exists in MultiSourceId.");

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