using Entities.ValueObject;

namespace Entities.Entities;

public class DeputyGabineteDomain
{
    public string Nome { get; private set; }
    public string Predio { get; private set; }
    public string Sala { get; private set; }
    public string Andar { get; private set; }
    public string Telefone { get; private set; }
    public Email Email { get; private set; }

    private DeputyGabineteDomain()
    {
    }

    public static DeputyGabineteDomain CreateGabinete(string nome, string predio, string sala, string andar,
        string telefone, string email)
    {
        return new DeputyGabineteDomain
        {
            Nome = nome,
            Predio = predio,
            Sala = sala,
            Andar = andar,
            Telefone = telefone,
            Email = new Email(email)
        };
    }
}