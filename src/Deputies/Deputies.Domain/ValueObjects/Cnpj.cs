using System.Text.RegularExpressions;

namespace Deputies.Domain.ValueObjects;

public class Cnpj
{
    private readonly string _value;

    public Cnpj(string cnpj)
    {
        if (string.IsNullOrEmpty(cnpj))
        {
            throw new ArgumentException("CNPJ cannot be null or empty.");
        }

        // Remove any non-numeric characters to standardize
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (!IsValidCnpj(cnpj))
        {
            throw new ArgumentException("Invalid CNPJ.");
        }

        _value = cnpj;
    }

    // Returns the CNPJ with mask (e.g., 12.345.678/0001-95)
    public string GetMasked()
    {
        return ConvertToMasked(_value);
    }

    // Returns the CNPJ without mask (e.g., 12345678000195)
    public string GetUnmasked()
    {
        return _value;
    }

    public override string ToString()
    {
        return GetMasked(); // Return masked CNPJ by default for display
    }

    public override bool Equals(object obj)
    {
        if (obj is Cnpj other)
        {
            return _value == other._value;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    private static bool IsValidCnpj(string cnpj)
    {
        if (cnpj.Length != 14)
            return false;

        // Step 1: Check if all digits are the same (invalid CNPJ)
        if (new string(cnpj.Distinct().ToArray()).Length == 1)
            return false;

        // Step 2: Calculate and validate the first check digit
        int[] firstWeights = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int firstSum = 0;
        for (int i = 0; i < 12; i++)
            firstSum += int.Parse(cnpj[i].ToString()) * firstWeights[i];
        int firstRemainder = firstSum % 11;
        int firstDigit = (firstRemainder < 2) ? 0 : 11 - firstRemainder;
        if (firstDigit != int.Parse(cnpj[12].ToString()))
            return false;

        // Step 3: Calculate and validate the second check digit
        int[] secondWeights = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int secondSum = 0;
        for (int i = 0; i < 13; i++)
            secondSum += int.Parse(cnpj[i].ToString()) * secondWeights[i];
        int secondRemainder = secondSum % 11;
        int secondDigit = (secondRemainder < 2) ? 0 : 11 - secondRemainder;
        if (secondDigit != int.Parse(cnpj[13].ToString()))
            return false;

        return true;
    }

    private static string ConvertToMasked(string cnpj)
    {
        // Format the CNPJ as 12.345.678/0001-95
        return Regex.Replace(cnpj, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");
    }
}