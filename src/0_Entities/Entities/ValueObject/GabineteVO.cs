namespace Entities.ValueObject;

public class GabineteVO
{
    public string Nome { get; private set; }
    public string Predio { get; private set; }
    public string Sala { get; private set; }
    public string Andar { get; private set; }
    public string Telefone { get; private set; }
    public Email Email { get; private set; }

    private GabineteVO()
    {
    }

    public static GabineteVO CreateGabinete(string nome, string predio, string sala, string andar,
        string telefone, string email)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            nome = "Sem informação do gabinete";
        }
        return new GabineteVO
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