namespace Deputies.Domain.ValueObjects;

public class Name
{
    public string? FirstName { get; }
    public string? LastName { get; }
    public string? FullName { get; }

    public Name(string? firstName, string? lastName = null, string? fullName = null)
    {
        // Check if the provided values satisfy the rule
        if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("A valid Name must have either a FullName or both FirstName and LastName.");
        }

        // If FirstName is provided, ensure that LastName is also provided (unless FullName is used instead)
        if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName) && string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Last name is required when only FirstName is provided.");
        }

        FirstName = firstName;
        LastName = lastName;
        FullName = fullName;
    }

    public override string ToString() => FullName ?? $"{FirstName} {LastName}".Trim();
}