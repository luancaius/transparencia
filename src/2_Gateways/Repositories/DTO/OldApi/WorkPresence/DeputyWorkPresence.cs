using System.Xml.Linq;

namespace Repositories.DTO.OldApi.WorkPresence;

public class DeputyWorkPresence : BaseEntityDTO
{
    public int Ano { get; private set; }
    public int Mes { get; private set; }
    public int Legislatura { get; set; }
    public int CarteiraParlamentar { get; set; }
    public string NomeParlamentar { get; set; }
    public string SiglaPartido { get; set; }
    public string SiglaUF { get; set; }
    public List<DiaDeSessao> DiasDeSessoes { get; set; }

    public DeputyWorkPresence(string deputyDeputyWorkPresenceRaw, int year, int month, int id)
    {
        try
        {
            Ano = year;
            Mes = month;
            Id = $"{year}-{month}-{id}";
            if (string.IsNullOrEmpty(deputyDeputyWorkPresenceRaw))
            {
                return;
            }
            
            XDocument doc = XDocument.Parse(deputyDeputyWorkPresenceRaw);
            XNamespace ns = "";

            var parlamentar = doc.Descendants(ns + "parlamentar").FirstOrDefault();

            if (parlamentar != null)
            {
                Legislatura = (int)parlamentar.Element(ns + "legislatura");
                CarteiraParlamentar = (int)parlamentar.Element(ns + "carteiraParlamentar");
                NomeParlamentar = (string)parlamentar.Element(ns + "nomeParlamentar");
                SiglaPartido = (string)parlamentar.Element(ns + "siglaPartido").Value.Trim();
                SiglaUF = (string)parlamentar.Element(ns + "siglaUF");

                DiasDeSessoes = parlamentar.Element(ns + "diasDeSessoes2").Elements(ns + "dia")
                    .Select(d => new DiaDeSessao
                    {
                        Data = (string)d.Element(ns + "data"),
                        FrequenciaNoDia = (string)d.Element(ns + "frequencianoDia"),
                        Justificativa = (string)d.Element(ns + "justificativa"),
                        QtdeSessoes = (int)d.Element(ns + "qtdeSessoes"),
                        Sessoes = d.Element(ns + "sessoes").Elements(ns + "sessao")
                            .Select(s => new Sessao
                            {
                                Descricao = (string)s.Element(ns + "descricao"),
                                Frequencia = (string)s.Element(ns + "frequencia")
                            }).ToList()
                    }).ToList();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}