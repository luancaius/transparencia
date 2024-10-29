namespace Entities.ValueObject;

public class Cnpj
{
    private readonly string _value;

    public Cnpj(string cnpj)
    {
        if (string.IsNullOrEmpty(cnpj))
        {
            throw new ArgumentException("CNPJ cannot be null or empty.");
        }

        if (!IsValidCnpj(cnpj))
        {
            throw new ArgumentException($"Invalid CNPJ: {cnpj}");
        }

        _value = cnpj;
    }

    public override string ToString()
    {
        return _value;
    }

    private bool IsValidCnpj(string cnpj)
    {
        // Remove any non-numeric characters
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        // Check if the CNPJ has 14 digits
        if (cnpj.Length != 14)
            return false;

        // Check if the CNPJ has all identical digits
        if (new string(cnpj.Distinct().ToArray()).Length == 1)
            return false;

        // Calculate and verify the first check digit
        int[] multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int sum = 0;
        for (int i = 0; i < 12; i++)
            sum += int.Parse(cnpj[i].ToString()) * multiplier1[i];
        int remainder = sum % 11;
        remainder = remainder < 2 ? 0 : 11 - remainder;
        if (remainder != int.Parse(cnpj[12].ToString()))
            return false;

        // Calculate and verify the second check digit
        int[] multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        sum = 0;
        for (int i = 0; i < 13; i++)
            sum += int.Parse(cnpj[i].ToString()) * multiplier2[i];
        remainder = sum % 11;
        remainder = remainder < 2 ? 0 : 11 - remainder;
        if (remainder != int.Parse(cnpj[13].ToString()))
            return false;

        return true;
    }
}
