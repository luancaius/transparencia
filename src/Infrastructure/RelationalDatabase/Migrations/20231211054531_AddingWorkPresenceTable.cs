using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddingWorkPresenceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "DiaSessao",
                schema: "congresso");

            migrationBuilder.DropTable(
                name: "DeputyWorkPresences",
                schema: "congresso");
        }
    }
}
