using Newtonsoft.Json;
using System;

namespace Repositories.DTO.NewApi.Expense
{
    public class DeputyExpense
    {
        // Properties to store data
        public int Ano { get; private set; }
        public int Mes { get; private set; }
        public int IdDeputy { get; private set; }
        public bool HasData { get; private set; }
        public string TipoDespesa { get; private set; }
        public int CodDocumento { get; private set; }
        public string TipoDocumento { get; private set; }
        public int CodTipoDocumento { get; private set; }
        public DateTime? DataDocumento { get; private set; }
        public string NumDocumento { get; private set; }
        public double ValorDocumento { get; private set; }
        public string UrlDocumento { get; private set; }
        public string NomeFornecedor { get; private set; }
        public string CnpjCpfFornecedor { get; private set; }
        public double ValorLiquido { get; private set; }
        public double ValorGlosa { get; private set; }
        public string NumRessarcimento { get; private set; }
        public int CodLote { get; private set; }
        public int Parcela { get; private set; }

        public DeputyExpense(string rawDeputyExpense, int ano, int mes, int id)
        {
            try
            {
                dynamic jsonObject = JsonConvert.DeserializeObject(rawDeputyExpense);
                dynamic dadosArray = jsonObject.dados;

                IdDeputy = id;
                if (dadosArray.Count > 0)
                {
                    dynamic dadosItem = dadosArray[0]; // Accessing the first item
                    HasData = true; 
                    Ano = dadosItem.ano;
                    Mes = dadosItem.mes;
                    TipoDespesa = dadosItem.tipoDespesa;
                    CodDocumento = dadosItem.codDocumento;
                    TipoDocumento = dadosItem.tipoDocumento;
                    CodTipoDocumento = dadosItem.codTipoDocumento;
                    DataDocumento = DateTime.TryParse(dadosItem.dataDocumento.ToString(), out DateTime dataDocumento) ? dataDocumento : (DateTime?)null;
                    NumDocumento = dadosItem.numDocumento;
                    ValorDocumento = (double)dadosItem.valorDocumento;
                    UrlDocumento = dadosItem.urlDocumento;
                    NomeFornecedor = dadosItem.nomeFornecedor;
                    CnpjCpfFornecedor = dadosItem.cnpjCpfFornecedor;
                    ValorLiquido = (double)dadosItem.valorLiquido;
                    ValorGlosa = (double)dadosItem.valorGlosa;
                    NumRessarcimento = dadosItem.numRessarcimento;
                    CodLote = dadosItem.codLote;
                    Parcela = dadosItem.parcela;
                }
                else
                {
                    Ano = ano;
                    Mes = mes;
                    HasData = false;
                }
                // Process links if needed
                // [Add link processing here if necessary]
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error parsing JSON: " + ex.Message);
            }
        }
    }
}
