using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RelationalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class removing_partido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deputado_PartidoAtual_PartidoAtualId",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropTable(
                name: "PartidoAtual",
                schema: "congresso");

            migrationBuilder.DropIndex(
                name: "IX_Deputado_PartidoAtualId",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.DropColumn(
                name: "PartidoAtualId",
                schema: "congresso",
                table: "Deputado");

            migrationBuilder.AlterColumn<string>(
                name: "UrlFoto",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UriPartido",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Uri",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UrlFoto",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UriPartido",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Uri",
                schema: "congresso",
                table: "Deputado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartidoAtualId",
                schema: "congresso",
                table: "Deputado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PartidoAtual",
                schema: "congresso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartidoAtual", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deputado_PartidoAtualId",
                schema: "congresso",
                table: "Deputado",
                column: "PartidoAtualId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deputado_PartidoAtual_PartidoAtualId",
                schema: "congresso",
                table: "Deputado",
                column: "PartidoAtualId",
                principalSchema: "congresso",
                principalTable: "PartidoAtual",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
