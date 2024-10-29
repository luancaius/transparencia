#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace RelationalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class fixing_uf_length : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UfNascimento",
                schema: "congresso",
                table: "deputados",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.CreateTable(
                name: "pessoas",
                schema: "general",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pessoas_Cpf",
                schema: "general",
                table: "pessoas",
                column: "Cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pessoas",
                schema: "general");

            migrationBuilder.AlterColumn<string>(
                name: "UfNascimento",
                schema: "congresso",
                table: "deputados",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
