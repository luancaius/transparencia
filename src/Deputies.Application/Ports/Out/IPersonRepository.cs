using Deputies.Domain.Entities;

namespace Deputies.Application.Ports.Out;

public interface IPersonRepository
{
    Task SavePersonAsync(Person person);
    Task<Person?> GetPersonByCpfAsync(string cpf);
}