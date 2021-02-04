using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeBudget.Domain.Migrations
{
    public partial class AdjustingtheBelongingFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Belongings",
                table: "Belongings");

            migrationBuilder.AddColumn<int>(
                name: "OwnerID",
                table: "Belongings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Belongings",
                table: "Belongings",
                columns: new[] { "CategoryID", "PaymentID", "AccountID", "RecipientID", "OwnerID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Belongings",
                table: "Belongings");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Belongings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Belongings",
                table: "Belongings",
                columns: new[] { "CategoryID", "PaymentID", "AccountID", "RecipientID" });
        }
    }
}
