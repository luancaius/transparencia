using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "congresso");

            migrationBuilder.CreateTable(
                name: "Deputado",
                schema: "congresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeEleitoral = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeCivil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeParlamentarAtual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiglaPartido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UriPartido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiglaUf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Legislatura = table.Column<int>(type: "int", nullable: false),
                    UrlFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UfRepresentacaoAtual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SituacaoNaLegislaturaAtual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFalecimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UfNascimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MunicipioNascimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Escolaridade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedeSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Situacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CondicaoEleitoral = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deputado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comissao",
                schema: "congresso",
                columns: table => new
                {
                    IdOrgaoLegislativoCD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiglaComissao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeComissao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CondicaoMembro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataSaida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeputadoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comissao", x => x.IdOrgaoLegislativoCD);
                    table.ForeignKey(
                        name: "FK_Comissao_Deputado_DeputadoId",
                        column: x => x.DeputadoId,
                        principalSchema: "congresso",
                        principalTable: "Deputado",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeputyExpenses",
                schema: "congresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    IdDeputy = table.Column<int>(type: "int", nullable: false),
                    HasData = table.Column<bool>(type: "bit", nullable: false),
                    TipoDespesa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodDocumento = table.Column<int>(type: "int", nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodTipoDocumento = table.Column<int>(type: "int", nullable: false),
                    DataDocumento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorDocumento = table.Column<double>(type: "float", nullable: false),
                    UrlDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeFornecedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CnpjCpfFornecedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorLiquido = table.Column<double>(type: "float", nullable: false),
                    ValorGlosa = table.Column<double>(type: "float", nullable: false),
                    NumRessarcimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodLote = table.Column<int>(type: "int", nullable: false),
                    Parcela = table.Column<int>(type: "int", nullable: false),
                    DeputadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeputyExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeputyExpenses_Deputado_DeputadoId",
                        column: x => x.DeputadoId,
                        principalSchema: "congresso",
                        principalTable: "Deputado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeputyWorkPresences",
                schema: "congresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Legislatura = table.Column<int>(type: "int", nullable: false),
                    CarteiraParlamentar = table.Column<int>(type: "int", nullable: false),
                    NomeParlamentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiglaPartido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiglaUF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeputadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeputyWorkPresences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeputyWorkPresences_Deputado_DeputadoId",
                        column: x => x.DeputadoId,
                        principalSchema: "congresso",
                        principalTable: "Deputado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaSessao",
                schema: "congresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FrequenciaNoDia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Justificativa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtdeSessoes = table.Column<int>(type: "int", nullable: false),
                    DeputadoId = table.Column<int>(type: "int", nullable: false),
                    DeputyWorkPresenceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaSessao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaSessao_Deputado_DeputadoId",
                        column: x => x.DeputadoId,
                        principalSchema: "congresso",
                        principalTable: "Deputado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiaSessao_DeputyWorkPresences_DeputyWorkPresenceId",
                        column: x => x.DeputyWorkPresenceId,
                        principalSchema: "congresso",
                        principalTable: "DeputyWorkPresences",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comissao_DeputadoId",
                schema: "congresso",
                table: "Comissao",
                column: "DeputadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DeputyExpenses_DeputadoId",
                schema: "congresso",
                table: "DeputyExpenses",
                column: "DeputadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DeputyWorkPresences_DeputadoId",
                schema: "congresso",
                table: "DeputyWorkPresences",
                column: "DeputadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaSessao_DeputadoId",
                schema: "congresso",
                table: "DiaSessao",
                column: "DeputadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaSessao_DeputyWorkPresenceId",
                schema: "congresso",
                table: "DiaSessao",
                column: "DeputyWorkPresenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comissao",
                schema: "congresso");

            migrationBuilder.DropTable(
                name: "DeputyExpenses",
                schema: "congresso");

            migrationBuilder.DropTable(
                name: "DiaSessao",
                schema: "congresso");

            migrationBuilder.DropTable(
                name: "DeputyWorkPresences",
                schema: "congresso");

            migrationBuilder.DropTable(
                name: "Deputado",
                schema: "congresso");
        }
    }
}
