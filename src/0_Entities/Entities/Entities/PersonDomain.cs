using Entities.ValueObject;

namespace Entities.Entities
{
    public class PersonDomain
    {
        public Name Name { get; private set; }
        public Cpf CPF { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public Estado? EstadoNascimento { get; private set; }
        public string? MunicipioNascimento { get; private set; }
        public Email? Email { get; private set; }
        public Gender? Gender { get; private set; }
        public Escolaridade? Escolaridade { get; private set; }

        private PersonDomain(Name name, DateTime? dateOfBirth, Email? email, 
            Estado? estadoNascimento, string? municipioNascimento, Cpf cpf, Gender? gender, Escolaridade? escolaridade)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Email = email;
            EstadoNascimento = estadoNascimento;
            MunicipioNascimento = municipioNascimento;
            CPF = cpf;
            Gender = gender;
            Escolaridade = escolaridade;
        }

        public static PersonDomain CreateSimplePerson(string firstName, string cpf)
        {
            return new PersonDomain(new Name(firstName), null, null, null, null, new Cpf(cpf), null, null);
        }
        
        public static PersonDomain CreatePerson(string firstName, string? lastName, string? nickname, DateTime dateOfBirth, 
            string email, string estadoNascimento, string municipioNascimento, string cpf, string gender, string escolaridade)
        {
            var name = new Name(firstName, lastName, nickname);
            var emailObj = new Email(email);
            var estado = estadoNascimento.ConvertStringToEstado();
            var cpfObj = new Cpf(cpf);
            var genderEnum = Extensions.GenderFromString(gender);
            var escolaridadeEnum = Extensions.EscolaridadeFromString(escolaridade);

            return new PersonDomain(name, dateOfBirth, emailObj, estado, municipioNascimento, cpfObj, genderEnum, escolaridadeEnum);
        }
    }
}
