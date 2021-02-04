using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeBudget.Domain.Migrations
{
    public partial class AddingcolumnstoAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalExpense",
                table: "Accounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalIncome",
                table: "Accounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalExpense",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TotalIncome",
                table: "Accounts");
        }
    }
}
