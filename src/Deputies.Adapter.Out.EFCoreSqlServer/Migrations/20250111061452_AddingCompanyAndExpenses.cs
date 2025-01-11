using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deputies.Adapter.Out.EFCoreSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddingCompanyAndExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeputyExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeputyId = table.Column<int>(type: "int", nullable: true),
                    BuyerPersonId = table.Column<int>(type: "int", nullable: false),
                    SupplierCpfCnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeputyExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeputyExpenses_Deputies_DeputyId",
                        column: x => x.DeputyId,
                        principalTable: "Deputies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeputyExpenses_Persons_BuyerPersonId",
                        column: x => x.BuyerPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Cnpj",
                table: "Companies",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeputyExpenses_BuyerPersonId",
                table: "DeputyExpenses",
                column: "BuyerPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DeputyExpenses_DeputyId",
                table: "DeputyExpenses",
                column: "DeputyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "DeputyExpenses");
        }
    }
}
