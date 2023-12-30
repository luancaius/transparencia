﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "congresso");

            migrationBuilder.EnsureSchema(
                name: "general");

            migrationBuilder.CreateTable(
                name: "deputado",
                schema: "congresso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeEleitoral = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomeCivil = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SiglaPartido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UfNascimento = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UfRepresentacaoAtual = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdApi = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deputado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "empresa",
                schema: "general",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresa", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "DeputadoDespesa",
                schema: "congresso",
                columns: table => new
                {
                    DeputyExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeExpense = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmountDocument = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiptUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TypeExpense = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TypeReceipt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumberDocument = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdDocument = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    DeputyId = table.Column<int>(type: "int", nullable: false),
                    DeputadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeputadoDespesa", x => x.DeputyExpenseId);
                    table.ForeignKey(
                        name: "FK_DeputadoDespesa_deputado_DeputadoId",
                        column: x => x.DeputadoId,
                        principalSchema: "congresso",
                        principalTable: "deputado",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeputadoDespesa_empresa_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "general",
                        principalTable: "empresa",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_deputado_Cpf",
                schema: "congresso",
                table: "deputado",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeputadoDespesa_CompanyId",
                schema: "congresso",
                table: "DeputadoDespesa",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeputadoDespesa_DeputadoId",
                schema: "congresso",
                table: "DeputadoDespesa",
                column: "DeputadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeputadoDespesa",
                schema: "congresso");

            migrationBuilder.DropTable(
                name: "deputado",
                schema: "congresso");

            migrationBuilder.DropTable(
                name: "empresa",
                schema: "general");
        }
    }
}
