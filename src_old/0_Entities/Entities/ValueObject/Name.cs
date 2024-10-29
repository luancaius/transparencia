namespace Entities.ValueObject
{
    public class Name : ValueObject
    {
        public string FirstName { get; }
        public string? LastName { get; }
        public string? FullName { get; }
        public string? Nickname { get; }

        public Name(string firstName, string? lastName = null, string? fullName = null, string? nickname = null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.");

            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            Nickname = nickname;
        }

        public override IEnumerable<object?> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
            yield return FullName;
            yield return Nickname;
        }

        public override string ToString() => Nickname ?? FullName ?? FirstName;
    }
}