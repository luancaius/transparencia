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
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomeEleitoral = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomeCivil = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SiglaPartido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SiglaUf = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UfNascimento = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UfRepresentacaoAtual = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deputado", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deputado",
                schema: "congresso");
        }
    }
}
