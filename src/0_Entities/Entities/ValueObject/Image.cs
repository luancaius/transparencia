namespace Entities.ValueObject;

public class Image
{
    public string Url { get; private set; }
    public string Description { get; private set; }

    public Image(string url, string description)
    {
        Url = url;
        Description = description;
    }
}