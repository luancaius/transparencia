namespace Entities.ValueObject;

public class Legislatura
{
    public int Numero { get; private set; }

    private Legislatura() { }

    public Legislatura(int numero)
    {
        if (numero < 48)
        {
            throw new ArgumentException(nameof(numero), "Número da legislatura não suportado.");
        }

        Numero = numero;
    }

    public static Legislatura CriarLegislaturaPorAno(int ano)
    {
        if (ano < 1987)
        {
            throw new ArgumentException("Ano não suportado", nameof(ano));
        }

        int baseAno = 1987; 
        int baseLegislatura = 48; 

        int diferencaAnos = ano - baseAno;
        int numeroLegislatura = baseLegislatura + (diferencaAnos / 4);

        return new Legislatura(numeroLegislatura);
    }
}

