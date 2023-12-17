namespace Entities.ValueObject;

public enum Gender
{
    Male,
    Female,
    NonBinary,
    PreferNotToSay,
    Other
}

public static class GenderExtensions
{
    public static Gender FromString(string genderString)
    {
        if (string.IsNullOrWhiteSpace(genderString))
        {
            throw new ArgumentException("Gender string cannot be null or whitespace.");
        }

        if (Enum.TryParse<Gender>(genderString, true, out var gender))
        {
            return gender;
        }
        else
        {
            throw new ArgumentException($"'{genderString}' is not a valid gender.");
        }
    }
}