using Deputies.Domain.Entities;

public interface IPersonRepository
{
    Task SavePersonAsync(Person person);
    Task<Person?> GetPersonByCpfAsync(string cpf);
}