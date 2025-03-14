using Entities.ValueObject;

namespace Entities;

public static class Extensions
{
    public static Estado ConvertStringToEstado(this string estadoString)
    {
        if (string.IsNullOrEmpty(estadoString))
        {
            return Estado.SemInformacao;
        }

        if (Enum.TryParse(estadoString, true, out Estado estado))
        {
            return estado;
        }

        throw new ArgumentException("Valor inválido para Estado", nameof(estadoString));
    }
    
    public static Gender GenderFromString(string genderString)
    {
        if (string.IsNullOrWhiteSpace(genderString))
        {
            return Gender.Unknown;
        }

        switch (genderString.ToLower())
        {
            case "m": return Gender.Male;
            case "male": return Gender.Male;
            case "f": return Gender.Female;
            case "female": return Gender.Female;
        }

        if (Enum.TryParse<Gender>(genderString, true, out var gender))
        {
            return gender;
        }

        throw new ArgumentException($"'{genderString}' is not a valid gender.");
    }

    public static Escolaridade EscolaridadeFromString(string escolaridadeString)
    {
        if (string.IsNullOrEmpty(escolaridadeString))
        {
            return Escolaridade.Desconhecida;
        }

        switch (escolaridadeString)
        {
            case "Ensino Superior":
            case "Superior Completo":
            case "Superior": return Escolaridade.SuperiorCompleto;
            case "Ensino Médio": return Escolaridade.EnsinoMedio;
            case "Ensino Fundamental": return Escolaridade.Primario;
            case "Pós-Graduação": return Escolaridade.Mestrado;
            case "Mestrado": return Escolaridade.Mestrado;
            case "Doutorado": return Escolaridade.Doutorado;
            default:
                return Escolaridade.Desconhecida;
        }
    }
}