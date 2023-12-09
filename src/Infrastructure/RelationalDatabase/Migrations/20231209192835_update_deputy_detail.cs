using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class update_deputy_detail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numero",
                schema: "congresso",
                table: "Gabinete",
                newName: "Sala");

            migrationBuilder.RenameColumn(
                name: "Anexo",
                schema: "congresso",
                table: "Gabinete",
                newName: "Predio");

            migrationBuilder.AddColumn<string>(
                name: "Andar",
                schema: "congresso",
                table: "Gabinete",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "congresso",
                table: "Gabinete",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                schema: "congresso",
                table: "Gabinete",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CondicaoEleitoral",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                schema: "congresso",
                table: "Deputado",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFalecimento",
                schema: "congresso",
                table: "Deputado",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                schema: "congresso",
                table: "Deputado",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Escolaridade",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MunicipioNascimento",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomeEleitoral",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RedeSocial",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Situacao",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UfNascimento",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlWebsite",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Andar",
                schema: "congresso",
                table: "Gabinete");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "congresso",
                table: "Gabinete");

            migrationBuilder.DropColumn(
                name: "Nome",
                schema: "congresso",
                table: "Gabinete");

            migrationBuilder.DropColumn(
                name: "CondicaoEleitoral",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropColumn(
                name: "Cpf",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropColumn(
                name: "Data",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropColumn(
                name: "DataFalecimento",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropColumn(
                name: "Escolaridade",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropColumn(
                name: "MunicipioNascimento",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropColumn(
                name: "NomeEleitoral",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropColumn(
                name: "RedeSocial",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropColumn(
                name: "Situacao",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropColumn(
                name: "UfNascimento",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropColumn(
                name: "UrlWebsite",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.RenameColumn(
                name: "Sala",
                schema: "congresso",
                table: "Gabinete",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "Predio",
                schema: "congresso",
                table: "Gabinete",
                newName: "Anexo");
        }
    }
}
