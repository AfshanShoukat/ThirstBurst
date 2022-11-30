using Microsoft.EntityFrameworkCore.Migrations;

namespace ThirstBurst.Data.Migrations
{
    public partial class Rcreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Drink",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Drink_CompanyId",
                table: "Drink",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drink_Company_CompanyId",
                table: "Drink",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drink_Company_CompanyId",
                table: "Drink");

            migrationBuilder.DropIndex(
                name: "IX_Drink_CompanyId",
                table: "Drink");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Drink");
        }
    }
}
