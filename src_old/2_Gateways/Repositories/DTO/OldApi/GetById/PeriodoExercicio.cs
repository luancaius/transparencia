namespace Repositories.DTO.OldApi.GetById;

public class PeriodoExercicio
{
    public string SiglaUFRepresentacao { get; set; }
    public string SituacaoExercicio { get; set; }
    public string DataInicio { get; set; } // Consider parsing to DateTime
    public string DataFim { get; set; } // Consider parsing to DateTime
}