using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.Entities;

public class Person
{
    public Cpf Cpf { get; set; }
    public Name Name { get; set; }
}