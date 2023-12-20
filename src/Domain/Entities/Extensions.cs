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
}