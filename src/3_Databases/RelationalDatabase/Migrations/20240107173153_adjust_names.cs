#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace RelationalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class adjust_names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeputadoDespesa_deputado_DeputadoId",
                schema: "congresso",
                table: "DeputadoDespesa");

            migrationBuilder.DropForeignKey(
                name: "FK_DeputadoDespesa_fornecedores_SupplierId",
                schema: "congresso",
                table: "DeputadoDespesa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_empresa",
                schema: "general",
                table: "empresa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeputadoDespesa",
                schema: "congresso",
                table: "DeputadoDespesa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_deputado",
                schema: "congresso",
                table: "deputado");

            migrationBuilder.RenameTable(
                name: "empresa",
                schema: "general",
                newName: "empresas",
                newSchema: "general");

            migrationBuilder.RenameTable(
                name: "DeputadoDespesa",
                schema: "congresso",
                newName: "deputado_despesas",
                newSchema: "congresso");

            migrationBuilder.RenameTable(
                name: "deputado",
                schema: "congresso",
                newName: "deputados",
                newSchema: "congresso");

            migrationBuilder.RenameIndex(
                name: "IX_empresa_Cnpj",
                schema: "general",
                table: "empresas",
                newName: "IX_empresas_Cnpj");

            migrationBuilder.RenameIndex(
                name: "IX_DeputadoDespesa_SupplierId",
                schema: "congresso",
                table: "deputado_despesas",
                newName: "IX_deputado_despesas_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_DeputadoDespesa_DeputadoId",
                schema: "congresso",
                table: "deputado_despesas",
                newName: "IX_deputado_despesas_DeputadoId");

            migrationBuilder.RenameIndex(
                name: "IX_deputado_Cpf",
                schema: "congresso",
                table: "deputados",
                newName: "IX_deputados_Cpf");

            migrationBuilder.AddPrimaryKey(
                name: "PK_empresas",
                schema: "general",
                table: "empresas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deputado_despesas",
                schema: "congresso",
                table: "deputado_despesas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deputados",
                schema: "congresso",
                table: "deputados",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_deputado_despesas_deputados_DeputadoId",
                schema: "congresso",
                table: "deputado_despesas",
                column: "DeputadoId",
                principalSchema: "congresso",
                principalTable: "deputados",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_deputado_despesas_fornecedores_SupplierId",
                schema: "congresso",
                table: "deputado_despesas",
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
                name: "FK_deputado_despesas_deputados_DeputadoId",
                schema: "congresso",
                table: "deputado_despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_deputado_despesas_fornecedores_SupplierId",
                schema: "congresso",
                table: "deputado_despesas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_empresas",
                schema: "general",
                table: "empresas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_deputados",
                schema: "congresso",
                table: "deputados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_deputado_despesas",
                schema: "congresso",
                table: "deputado_despesas");

            migrationBuilder.RenameTable(
                name: "empresas",
                schema: "general",
                newName: "empresa",
                newSchema: "general");

            migrationBuilder.RenameTable(
                name: "deputados",
                schema: "congresso",
                newName: "deputado",
                newSchema: "congresso");

            migrationBuilder.RenameTable(
                name: "deputado_despesas",
                schema: "congresso",
                newName: "DeputadoDespesa",
                newSchema: "congresso");

            migrationBuilder.RenameIndex(
                name: "IX_empresas_Cnpj",
                schema: "general",
                table: "empresa",
                newName: "IX_empresa_Cnpj");

            migrationBuilder.RenameIndex(
                name: "IX_deputados_Cpf",
                schema: "congresso",
                table: "deputado",
                newName: "IX_deputado_Cpf");

            migrationBuilder.RenameIndex(
                name: "IX_deputado_despesas_SupplierId",
                schema: "congresso",
                table: "DeputadoDespesa",
                newName: "IX_DeputadoDespesa_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_deputado_despesas_DeputadoId",
                schema: "congresso",
                table: "DeputadoDespesa",
                newName: "IX_DeputadoDespesa_DeputadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_empresa",
                schema: "general",
                table: "empresa",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deputado",
                schema: "congresso",
                table: "deputado",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeputadoDespesa",
                schema: "congresso",
                table: "DeputadoDespesa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeputadoDespesa_deputado_DeputadoId",
                schema: "congresso",
                table: "DeputadoDespesa",
                column: "DeputadoId",
                principalSchema: "congresso",
                principalTable: "deputado",
                principalColumn: "Id");

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
    }
}
