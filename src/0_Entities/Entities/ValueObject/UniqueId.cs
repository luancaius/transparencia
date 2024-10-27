using Entities.ValueObject;
using System.Collections.Generic;

namespace Entities.ValueObject
{
    public class UniqueId : ValueObject
    {
        private readonly Dictionary<IdSourceEnum, string> _idSources;

        public UniqueId()
        {
            _idSources = new Dictionary<IdSourceEnum, string>();
        }

        public void AddId(IdSourceEnum source, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("ID cannot be null or whitespace.");

            if (_idSources.TryGetValue(source, out var existingId) && existingId == id)
                throw new InvalidOperationException($"The ID '{id}' is already assigned to the source '{source}'.");

            _idSources[source] = id;
        }

        public string? GetIdBySource(IdSourceEnum source)
        {
            return _idSources.TryGetValue(source, out var id) ? id : null;
        }

        public IReadOnlyDictionary<IdSourceEnum, string> GetAllIds()
        {
            return _idSources;
        }

        public override IEnumerable<object?> GetAtomicValues()
        {
            foreach (var kvp in _idSources)
            {
                yield return kvp;
            }
        }

        public override string ToString()
        {
            return string.Join(", ", _idSources.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
        }
    }
}