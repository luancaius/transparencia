namespace Entity.API2_Soap.GetListaPresenca;

public class DeputadoItemPresencaSoap
{
    public int NumMatriculaDeputado { get; set; }
    public string Data { get; set; }
    public string FrequenciaNoDia { get; set; }
    public string Justificativa { get; set; }
    public int QtdeSessoes { get; set; }
    public List<Sessao> Sessoes { get; set; }
}