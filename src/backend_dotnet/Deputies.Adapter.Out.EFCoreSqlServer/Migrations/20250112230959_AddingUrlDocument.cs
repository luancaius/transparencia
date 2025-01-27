using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deputies.Adapter.Out.EFCoreSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddingUrlDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlDocument",
                table: "DeputyExpenses",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlDocument",
                table: "DeputyExpenses");
        }
    }
}
