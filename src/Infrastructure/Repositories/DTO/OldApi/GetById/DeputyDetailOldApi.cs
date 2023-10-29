using System.Xml.Linq;

namespace Repositories.DTO.OldApi.GetById
{
    public class DeputyDetailOldApi
    {
        public int IdeCadastro { get; set; }
        public string Nome { get; set; }
        public string NomeParlamentar { get; set; }
        public string UrlFoto { get; set; } // not present in XML provided
        public string Sexo { get; set; }
        public string Uf { get; set; } // mapped from ufRepresentacaoAtual
        public string Partido { get; set; } // mapped from partidoAtual
        public string Gabinete { get; set; } // mapped from gabinete->numero
        public string Anexo { get; set; } // mapped from gabinete->anexo
        public string Fone { get; set; } // mapped from gabinete->telefone
        public string Email { get; set; }
        public Comissoes Comissoes { get; set; }

        public DeputyDetailOldApi(string deputyDetailOldApiRaw)
        {
            try
            {
                XDocument doc = XDocument.Parse(deputyDetailOldApiRaw);
                XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";
                XNamespace camara = "https://www.camara.gov.br/SitCamaraWS/Deputados";
                XNamespace empty = "";

                var deputy = doc.Descendants(soap + "Body")
                    .Descendants(camara + "ObterDetalhesDeputadoResponse")
                    .Descendants(camara + "ObterDetalhesDeputadoResult")
                    .Descendants(empty + "Deputados")
                    .Descendants(empty + "Deputado")
                    .FirstOrDefault(); // Assuming one result, if multiple use First() or Single()

                if (deputy != null)
                {
                    IdeCadastro = (int)deputy.Element(empty + "ideCadastro");
                    Nome = (string)deputy.Element(empty + "nomeCivil");
                    NomeParlamentar = (string)deputy.Element(empty + "nomeParlamentarAtual");
                    Sexo = (string)deputy.Element(empty + "sexo");
                    Uf = (string)deputy.Element(empty + "ufRepresentacaoAtual");
                    Partido = (string)deputy.Element(empty + "partidoAtual").Element(empty + "sigla");
                    Gabinete = (string)deputy.Element(empty + "gabinete").Element(empty + "numero");
                    Anexo = (string)deputy.Element(empty + "gabinete").Element(empty + "anexo");
                    Fone = (string)deputy.Element(empty + "gabinete").Element(empty + "telefone");
                    Email = (string)deputy.Element(empty + "email");
                    Comissoes = new Comissoes
                    {
                        Titular = deputy.Element(empty + "comissoes").Elements(empty + "comissao")
                            .Where(c => (string)c.Element(empty + "condicaoMembro") == "Titular")
                            .Select(c => (string)c.Element(empty + "nomeComissao")).ToList(),
                        Suplente = deputy.Element(empty + "comissoes").Elements(empty + "comissao")
                            .Where(c => (string)c.Element(empty + "condicaoMembro") == "Suplente")
                            .Select(c => (string)c.Element(empty + "nomeComissao")).ToList()
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private DeputyDetailOldApi()
        {
            throw new NotImplementedException();
        }
    }
}
