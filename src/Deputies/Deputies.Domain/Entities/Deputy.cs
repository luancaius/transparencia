// Domain/Entities/Deputy.cs
using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.Entities
{
    public class Deputy
    {
        private Deputy(Person person, string deputyName, string party, MultiSourceId multiSourceId)
        {
            Person = person;
            DeputyName = deputyName;
            Party = party;
            MultiSourceId = multiSourceId;
        }

        public Person Person { get; }
        public string DeputyName { get; }
        public string Party { get; }
        public MultiSourceId MultiSourceId { get; }

        public static Deputy Create(Person person, string deputyName, string party, MultiSourceId multiSourceId)
        {
            if (person == null) 
                throw new ArgumentNullException(nameof(person), "Person cannot be null.");
            
            if (string.IsNullOrWhiteSpace(deputyName)) 
                throw new ArgumentException("Deputy name cannot be null or empty.", nameof(deputyName));
            
            if (string.IsNullOrWhiteSpace(party)) 
                throw new ArgumentException("Party cannot be null or empty.", nameof(party));
            
            if (multiSourceId == null) 
                throw new ArgumentNullException(nameof(multiSourceId), "MultiSourceId cannot be null.");

            return new Deputy(person, deputyName, party, multiSourceId);
        }

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