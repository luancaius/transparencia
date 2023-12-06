using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class WorkPresence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_DeputyExpenses_DeputadoId",
                schema: "congresso",
                table: "DeputyExpenses",
                column: "DeputadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeputyExpenses",
                schema: "congresso");
        }
    }
}
