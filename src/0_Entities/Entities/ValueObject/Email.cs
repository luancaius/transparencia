using System.Net.Mail;

namespace Entities.ValueObject;

public class Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Value = "No email provided.";
        }
        else if (!IsValidEmail(value))
        {
            throw new ArgumentException($"Invalid email format - {value}" );
        }
        else
        {
            Value = value;
        }
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public override string ToString()
    {
        return Value;
    }
}