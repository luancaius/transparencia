using Entities.ValueObject;

namespace Entities.DomainEntities
{
    public class Deputy : Person
    {
        public Deputy(List<Legislatura> legislaturas, string nickname,  Guid id, string firstName, string lastName, DateTime dateOfBirth, String email, Phone phone, 
            Address address, Cpf cpf, Gender gender) 
            : base(id, firstName, lastName, dateOfBirth, email, phone, address, cpf, gender)
        {
            Legislaturas = legislaturas;
            Nickname = nickname;
        }

        public List<Legislatura> Legislaturas { get; private set; }
        public string Nickname { get; private set; }
    }
}
