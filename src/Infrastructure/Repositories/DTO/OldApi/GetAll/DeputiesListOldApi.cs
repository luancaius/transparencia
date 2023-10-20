using System.Xml.Linq;
using System.Xml.Serialization;

namespace Repositories.DTO.OldApi.GetAll;

public class DeputiesListOldApi
{
    public DeputiesListOldApi(String rawDeputiesList)
    {
        try
        {
            XDocument doc = XDocument.Parse(rawDeputiesList);
            XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace camara = "https://www.camara.gov.br/SitCamaraWS/Deputados";
            XNamespace empty = "";

            Deputies = doc.Descendants(soap + "Body")
                .Descendants(camara + "ObterDeputadosResponse")
                .Descendants(camara + "ObterDeputadosResult")
                .Descendants(empty + "deputados")
                .Descendants("deputado")
                .Select(d => new DeputyOldApi
                {
                    IdeCadastro = (int)d.Element("ideCadastro"),
                    CodOrcamento = (string)d.Element("codOrcamento"),
                    Condicao = (string)d.Element("condicao"),
                    Matricula = (int)d.Element("matricula"),
                    IdParlamentar = (int)d.Element("idParlamentar"),
                    Nome = (string)d.Element("nome"),
                    NomeParlamentar = (string)d.Element("nomeParlamentar"),
                    UrlFoto = (string)d.Element("urlFoto"),
                    Sexo = (string)d.Element("sexo"),
                    Uf = (string)d.Element("uf"),
                    Partido = (string)d.Element("partido"),
                    Gabinete = (string)d.Element("gabinete"),
                    Anexo = (string)d.Element("anexo"),
                    Fone = (string)d.Element("fone"),
                    Email = (string)d.Element("email"),
                    Comissoes = new Comissoes
                    {
                        Titular = (string)d.Element("comissoes").Element("titular"),
                        Suplente = (string)d.Element("comissoes").Element("suplente"),
                    }
                }).ToList();

            Console.WriteLine(Deputies);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<DeputyOldApi> Deputies { get; set; }
}
