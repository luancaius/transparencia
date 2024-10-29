namespace Entities.ValueObject;

public class Gabinete
{
    public string Nome { get; private set; }
    public string Predio { get; private set; }
    public string Sala { get; private set; }
    public string Andar { get; private set; }
    public string Telefone { get; private set; }
    public Email Email { get; private set; }

    private Gabinete()
    {
    }

    public static Gabinete CreateGabinete(string nome, string predio, string sala, string andar,
        string telefone, string email)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            nome = "Sem informação do gabinete";
        }
        return new Gabinete
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