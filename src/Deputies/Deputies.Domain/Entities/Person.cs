// Domain/Entities/Person.cs
using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.Entities
{
    public class Person
    {
        private Person(Cpf cpf, Name name)
        {
            Cpf = cpf;
            Name = name;
        }

        public Cpf Cpf { get; }
        public Name Name { get; }

        public static Person Create(Cpf cpf, Name name)
        {
            if (cpf == null) throw new ArgumentNullException(nameof(cpf), "CPF cannot be null.");
            if (name == null) throw new ArgumentNullException(nameof(name), "Name cannot be null.");
            
            return new Person(cpf, name);
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Person other)
            {
                return Cpf.Equals(other.Cpf);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Cpf.GetHashCode();
        }
    }
}