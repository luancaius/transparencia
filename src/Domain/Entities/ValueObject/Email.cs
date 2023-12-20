namespace Entities.ValueObject;

public class Email
{
    public string Value { get; private set; }

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

        Value = value;
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
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