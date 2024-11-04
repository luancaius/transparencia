using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.Entities
{
    public class Person
    {
        public Person(Cpf cpf, Name name)
        {
            Cpf = cpf ?? throw new ArgumentNullException(nameof(cpf), "CPF cannot be null.");
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null.");
        }

        public Cpf Cpf { get; }
        
        public Name Name { get; }
        
        public override bool Equals(object obj)
        {
            if (obj is Person other)
            {
                return Cpf.Equals(other.Cpf);
            }
            return false;
        }
    }
}