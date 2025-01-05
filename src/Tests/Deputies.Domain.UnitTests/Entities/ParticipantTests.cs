using Deputies.Domain.Entities;
using Deputies.Domain.ValueObjects;

namespace Deputies.Domain.UnitTests.Entities
{
    public class ParticipantTests
    {
        [Fact]
        public void Create_Person_With_String_Name_Succeeds()
        {
            // Arrange
            var cpf = new Cpf("12345678900");
            var name = new PersonName("John Doe");

            // Act
            var person = Person.Create(cpf, name);

            // Assert
            Assert.Equal("John Doe", person.DisplayName);
            Assert.Equal(cpf, person.Cpf);
        }

        [Fact]
        public void Create_Company_With_String_Name_Succeeds()
        {
            // Arrange
            var cnpj = new Cnpj("10217831000173");
            var companyName = "ACME Telecom";

            // Act
            var company = Company.Create(cnpj, companyName);

            // Assert
            Assert.Equal("ACME Telecom", company.DisplayName);
            Assert.Equal(cnpj, company.Cnpj);
        }

        [Fact]
        public void Person_Equality_Based_On_Cpf()
        {
            // Arrange
            var person1 = Person.Create(
                new Cpf("12345678900"), 
                new PersonName("John Doe")

            );

            var person2 = Person.Create(
                new Cpf("12345678900"), 
                new PersonName("Johnny")
            );

            // Same CPF => considered equal
            Assert.True(person1.Equals(person2));
            Assert.Equal(person1.GetHashCode(), person2.GetHashCode());
        }

        [Fact]
        public void Company_Equality_Based_On_Cnpj()
        {
            // Arrange
            var company1 = Company.Create(
                new Cnpj("10217831000173"),
                "ACME Telecom A"
            );

            var company2 = Company.Create(
                new Cnpj("10217831000173"),
                "ACME Telecom B"
            );

            // Same CNPJ => considered equal
            Assert.True(company1.Equals(company2));
            Assert.Equal(company1.GetHashCode(), company2.GetHashCode());
        }
    }
}
