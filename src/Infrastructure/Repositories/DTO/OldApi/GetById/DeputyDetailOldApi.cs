using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Repositories.DTO.OldApi.GetById;

    public class DeputyDetailOldApi : BaseEntity
    {
        public int IdeCadastro { get; set; }
        public string NomeCivil { get; set; }
        public string NomeParlamentarAtual { get; set; }
        public string Sexo { get; set; }
        public string UfRepresentacaoAtual { get; set; }
        public string SituacaoNaLegislaturaAtual { get; set; }
        public int IdParlamentarDeprecated { get; set; }
        public PartidoAtual PartidoAtual { get; set; }
        public Gabinete Gabinete { get; set; }
        public List<Comissao> Comissoes { get; set; }
        public string Email { get; set; }
        public List<PeriodoExercicio> PeriodosExercicio { get; set; }
        public List<ItemHistoricoLider> HistoricoLider { get; set; }

        public DeputyDetailOldApi(string deputyDetailOldApiRaw)
        {
            try
            {
                XDocument doc = XDocument.Parse(deputyDetailOldApiRaw);
                XNamespace ns = "";

                var deputy = doc.Descendants(ns + "Deputado").FirstOrDefault();

                if (deputy != null)
                {
                    IdeCadastro = (int)deputy.Element(ns + "ideCadastro");
                    IdEntity = IdeCadastro;
                    NomeCivil = (string)deputy.Element(ns + "nomeCivil");
                    NomeParlamentarAtual = (string)deputy.Element(ns + "nomeParlamentarAtual");
                    Sexo = (string)deputy.Element(ns + "sexo");
                    UfRepresentacaoAtual = (string)deputy.Element(ns + "ufRepresentacaoAtual");
                    SituacaoNaLegislaturaAtual = (string)deputy.Element(ns + "situacaoNaLegislaturaAtual");
                    IdParlamentarDeprecated = (int)deputy.Element(ns + "idParlamentarDeprecated");
                    PartidoAtual = new PartidoAtual
                    {
                        IdPartido = (string)deputy.Element(ns + "partidoAtual").Element(ns + "idPartido"),
                        Sigla = (string)deputy.Element(ns + "partidoAtual").Element(ns + "sigla"),
                        Nome = (string)deputy.Element(ns + "partidoAtual").Element(ns + "nome")
                    };
                    Gabinete = new Gabinete
                    {
                        Numero = (string)deputy.Element(ns + "gabinete").Element(ns + "numero"),
                        Anexo = (string)deputy.Element(ns + "gabinete").Element(ns + "anexo"),
                        Telefone = (string)deputy.Element(ns + "gabinete").Element(ns + "telefone")
                    };
                    Comissoes = deputy.Element(ns + "comissoes").Elements(ns + "comissao")
                        .Select(c => new Comissao
                        {
                            IdOrgaoLegislativoCD = (int)c.Element(ns + "idOrgaoLegislativoCD"),
                            SiglaComissao = (string)c.Element(ns + "siglaComissao"),
                            NomeComissao = (string)c.Element(ns + "nomeComissao"),
                            CondicaoMembro = (string)c.Element(ns + "condicaoMembro"),
                            DataEntrada = (string)c.Element(ns + "dataEntrada"), // Consider parsing to DateTime
                            DataSaida = (string)c.Element(ns + "dataSaida") // Consider parsing to DateTime
                        }).ToList();
                    Email = (string)deputy.Element(ns + "email");
                    PeriodosExercicio = deputy.Element(ns + "periodosExercicio").Elements(ns + "periodoExercicio")
                        .Select(p => new PeriodoExercicio
                        {
                            SiglaUFRepresentacao = (string)p.Element(ns + "siglaUFRepresentacao"),
                            SituacaoExercicio = (string)p.Element(ns + "situacaoExercicio"),
                            DataInicio = (string)p.Element(ns + "dataInicio"), // Consider parsing to DateTime
                            DataFim = (string)p.Element(ns + "dataFim") // Consider parsing to DateTime
                        }).ToList();
                    HistoricoLider = deputy.Element(ns + "historicoLider").Elements(ns + "itemHistoricoLider")
                        .Select(h => new ItemHistoricoLider
                        {
                            IdHistoricoLider = (int)h.Element(ns + "idHistoricoLider"),
                            IdCargoLideranca = (string)h.Element(ns + "idCargoLideranca"),
                            DescricaoCargoLideranca = (string)h.Element(ns + "descricaoCargoLideranca"),
                            NumOrdemCargo = (int)h.Element(ns + "numOrdemCargo"),
                            DataDesignacao = (string)h.Element(ns + "dataDesignacao"), // Consider parsing to DateTime
                            DataTermino = (string)h.Element(ns + "dataTermino"), // Consider parsing to DateTime
                            CodigoUnidadeLideranca = (string)h.Element(ns + "codigoUnidadeLideranca"),
                            SiglaUnidadeLideranca = (string)h.Element(ns + "siglaUnidadeLideranca"),
                            IdBlocoPartido = (int)h.Element(ns + "idBlocoPartido")
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


// Define additional classes for nested elements
        public class PartidoAtual
        {
            public string IdPartido { get; set; }
            public string Sigla { get; set; }
            public string Nome { get; set; }
        }

        public class Gabinete
        {
            public string Numero { get; set; }
            public string Anexo { get; set; }
            public string Telefone { get; set; }
        }

        public class Comissao
        {
            public int IdOrgaoLegislativoCD { get; set; }
            public string SiglaComissao { get; set; }
            public string NomeComissao { get; set; }
            public string CondicaoMembro { get; set; }
            public string DataEntrada { get; set; } // Consider parsing to DateTime
            public string DataSaida { get; set; } // Consider parsing to DateTime
        }

        public class PeriodoExercicio
        {
            public string SiglaUFRepresentacao { get; set; }
            public string SituacaoExercicio { get; set; }
            public string DataInicio { get; set; } // Consider parsing to DateTime
            public string DataFim { get; set; } // Consider parsing to DateTime
        }

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
            public int IdBlocoPartido { get; set; }
        }
    

