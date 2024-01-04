using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class adding_supplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeputadoDespesa_empresa_CompanyId",
                schema: "congresso",
                table: "DeputadoDespesa");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                schema: "congresso",
                table: "DeputadoDespesa",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_DeputadoDespesa_CompanyId",
                schema: "congresso",
                table: "DeputadoDespesa",
                newName: "IX_DeputadoDespesa_SupplierId");

            migrationBuilder.CreateTable(
                name: "fornecedores",
                schema: "general",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_empresa_Cnpj",
                schema: "general",
                table: "empresa",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fornecedores_Cnpj",
                schema: "general",
                table: "fornecedores",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fornecedores_Cpf",
                schema: "general",
                table: "fornecedores",
                column: "Cpf",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeputadoDespesa_fornecedores_SupplierId",
                schema: "congresso",
                table: "DeputadoDespesa",
                column: "SupplierId",
                principalSchema: "general",
                principalTable: "fornecedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeputadoDespesa_fornecedores_SupplierId",
                schema: "congresso",
                table: "DeputadoDespesa");

            migrationBuilder.DropTable(
                name: "fornecedores",
                schema: "general");

            migrationBuilder.DropIndex(
                name: "IX_empresa_Cnpj",
                schema: "general",
                table: "empresa");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                schema: "congresso",
                table: "DeputadoDespesa",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_DeputadoDespesa_SupplierId",
                schema: "congresso",
                table: "DeputadoDespesa",
                newName: "IX_DeputadoDespesa_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeputadoDespesa_empresa_CompanyId",
                schema: "congresso",
                table: "DeputadoDespesa",
                column: "CompanyId",
                principalSchema: "general",
                principalTable: "empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
