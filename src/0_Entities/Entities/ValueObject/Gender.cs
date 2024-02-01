namespace Entities.ValueObject;

public enum Gender
{
    Male,
    Female,
    Other,
    Unknown
}

public static class GenderExtensions
{
    public static Gender FromString(string genderString)
    {
        if (string.IsNullOrWhiteSpace(genderString))
        {
            return Gender.Unknown;
        }

        switch (genderString.ToLower())
        {
            case "m": return Gender.Male;
            case "male": return Gender.Male;
            case "f": return Gender.Female; 
            case "female": return Gender.Female; 

        }
        
        if (Enum.TryParse<Gender>(genderString, true, out var gender))
        {
            return gender;
        }
        
        throw new ArgumentException($"'{genderString}' is not a valid gender.");
    }
}