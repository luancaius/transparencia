namespace Repositories.DTO.OldApi.WorkPresence;

public class DiaDeSessao
{
    public string Data { get; set; }
    public string FrequenciaNoDia { get; set; }
    public string Justificativa { get; set; }
    public int QtdeSessoes { get; set; }
    public List<Sessao> Sessoes { get; set; }
}