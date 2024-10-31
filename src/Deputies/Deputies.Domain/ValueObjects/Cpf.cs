namespace Deputies.Domain.ValueObjects;

public class Cpf
{
    private readonly string _value;

    public Cpf(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
        {
            throw new ArgumentException("CPF cannot be null or empty.");
        }
        
        if (!IsValidCpf(cpf))
        {
            throw new ArgumentException("Invalid CPF.");
        }

        _value = cpf;
    }

    public override string ToString()
    {
        return _value;
    }

    private bool IsValidCpf(string cpf)
    {
        // Remove any non-numeric characters
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        // Step 1: Check if the CPF has 11 digits
        if (cpf.Length != 11)
            return false;

        // Step 2: Check if the CPF has all identical digits
        if (new string(cpf.Distinct().ToArray()).Length == 1)
            return false;

        // Step 3: Calculate and verify the first check digit
        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += (10 - i) * int.Parse(cpf[i].ToString());
        int remainder = sum % 11;
        if (remainder < 2)
            remainder = 0;
        else
            remainder = 11 - remainder;
        if (remainder != int.Parse(cpf[9].ToString()))
            return false;

        // Step 4: Calculate and verify the second check digit
        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += (11 - i) * int.Parse(cpf[i].ToString());
        remainder = sum % 11;
        if (remainder < 2)
            remainder = 0;
        else
            remainder = 11 - remainder;
        if (remainder != int.Parse(cpf[10].ToString()))
            return false;

        return true;
    }
}