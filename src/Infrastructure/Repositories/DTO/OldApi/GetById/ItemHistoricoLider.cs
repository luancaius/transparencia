namespace Repositories.DTO.OldApi.GetById;

public class ItemHistoricoLider
{
    public int IdHistoricoLider { get; set; }
    public string IdCargoLideranca { get; set; }
    public string DescricaoCargoLideranca { get; set; }
    public int NumOrdemCargo { get; set; }
    public string DataDesignacao { get; set; } // Consider parsing to DateTime
    public string DataTermino { get; set; } // Consider parsing to DateTime
    public string CodigoUnidadeLideranca { get; set; }
    public string SiglaUnidadeLideranca { get; set; }
    public string IdBlocoPartido { get; set; }
}