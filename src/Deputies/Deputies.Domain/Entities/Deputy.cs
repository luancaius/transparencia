using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.Entities
{
    public class Deputy
    {
        public Deputy(Person person, string deputyName, string party, MultiSourceId multiSourceId)
        {
            Person = person ?? throw new ArgumentNullException(nameof(person), "Person cannot be null.");
            DeputyName = !string.IsNullOrWhiteSpace(deputyName) ? deputyName : throw new ArgumentException("Deputy name cannot be null or empty.", nameof(deputyName));
            Party = !string.IsNullOrWhiteSpace(party) ? party : throw new ArgumentException("Party cannot be null or empty.", nameof(party));
            MultiSourceId = multiSourceId ?? throw new ArgumentNullException(nameof(multiSourceId), "MultiSourceId cannot be null.");
        }

        public Person Person { get; }
        
        public string DeputyName { get; }
        
        public string Party { get; }
        
        public MultiSourceId MultiSourceId { get; }

        public override bool Equals(object obj)
        {
            if (obj is Deputy other)
            {
                return Person.Equals(other.Person);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Person);
        }

        public override string ToString()
        {
            return $"{DeputyName} ({Party}) - {MultiSourceId}";
        }
    }
}