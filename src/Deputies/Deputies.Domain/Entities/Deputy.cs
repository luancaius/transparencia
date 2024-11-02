using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.Entities;

public class Deputy
{
    public Person Person { get; set; }
    public string DeputyName { get; set; }
    public string Party { get; set; }
    public MultiSourceId MultiSourceId { get; set; }
}