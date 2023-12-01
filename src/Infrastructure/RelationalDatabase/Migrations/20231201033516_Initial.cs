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
                name: "Congresso");

            migrationBuilder.CreateTable(
                name: "Gabinete",
                schema: "Congresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gabinete", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartidoAtual",
                schema: "Congresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartidoAtual", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deputado",
                schema: "Congresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiglaPartido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UriPartido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiglaUf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Legislatura = table.Column<int>(type: "int", nullable: false),
                    UrlFoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeCivil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeParlamentarAtual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UfRepresentacaoAtual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SituacaoNaLegislaturaAtual = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartidoAtualId = table.Column<int>(type: "int", nullable: false),
                    GabineteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deputado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deputado_Gabinete_GabineteId",
                        column: x => x.GabineteId,
                        principalSchema: "Congresso",
                        principalTable: "Gabinete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deputado_PartidoAtual_PartidoAtualId",
                        column: x => x.PartidoAtualId,
                        principalSchema: "Congresso",
                        principalTable: "PartidoAtual",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comissao",
                schema: "Congresso",
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
                        principalSchema: "Congresso",
                        principalTable: "Deputado",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comissao_DeputadoId",
                schema: "Congresso",
                table: "Comissao",
                column: "DeputadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Deputado_GabineteId",
                schema: "Congresso",
                table: "Deputado",
                column: "GabineteId");

            migrationBuilder.CreateIndex(
                name: "IX_Deputado_PartidoAtualId",
                schema: "Congresso",
                table: "Deputado",
                column: "PartidoAtualId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comissao",
                schema: "Congresso");

            migrationBuilder.DropTable(
                name: "Deputado",
                schema: "Congresso");

            migrationBuilder.DropTable(
                name: "Gabinete",
                schema: "Congresso");

            migrationBuilder.DropTable(
                name: "PartidoAtual",
                schema: "Congresso");
        }
    }
}
