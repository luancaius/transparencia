using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Deputies.Domain.ValueObjects
{
    public class Cpf
    {
        private readonly string _value;

        public Cpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                throw new ArgumentException("CPF cannot be null or empty.");
            }

            // Remove any non-numeric characters to standardize
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (!IsValidCpf(cpf))
            {
                throw new ArgumentException("Invalid CPF.");
            }

            _value = cpf;
        }

        // Returns the CPF with mask (e.g., 123.456.789-09)
        public string GetMasked()
        {
            return ConvertToMasked(_value);
        }

        // Returns the CPF without mask (e.g., 12345678909)
        public string GetUnmasked()
        {
            return _value;
        }

        public override string ToString()
        {
            return GetMasked(); // Return masked CPF by default for display
        }

        public override bool Equals(object obj)
        {
            if (obj is Cpf other)
            {
                return _value == other._value;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        private static bool IsValidCpf(string cpf)
        {
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

        private static string ConvertToMasked(string cpf)
        {
            // Format the CPF as 123.456.789-09
            return Regex.Replace(cpf, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
        }
    }
}
