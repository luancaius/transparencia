using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.Entities;

public class Deputy
{
    public Name Name { get; private set; }
    public Cpf Cpf { get; private set; }
    public Company Company { get; private set; }
    public Deputy(Name name, Cpf cpf)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Cpf = cpf ?? throw new ArgumentNullException(nameof(cpf));
    }
}