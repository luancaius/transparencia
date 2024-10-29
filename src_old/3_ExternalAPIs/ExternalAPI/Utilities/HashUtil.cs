using System.Security.Cryptography;
using System.Text;

namespace ExternalAPI.Utilities;

public static class HashUtil
{
    public static string GetHash(string input)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}