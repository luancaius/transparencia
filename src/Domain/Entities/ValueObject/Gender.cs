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

        switch (genderString)
        {
            case "M": return Gender.Male; 
            case "F": return Gender.Female; 
        }
        
        if (Enum.TryParse<Gender>(genderString, true, out var gender))
        {
            return gender;
        }
        
        throw new ArgumentException($"'{genderString}' is not a valid gender.");
    }
}