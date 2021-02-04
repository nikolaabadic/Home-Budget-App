using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeBudget.Domain.Migrations
{
    public partial class AddingDBSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Belonging_Categories_CategoryID",
                table: "Belonging");

            migrationBuilder.DropForeignKey(
                name: "FK_Belonging_Payments_PaymentID_AccountID_RecipientID",
                table: "Belonging");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Belonging",
                table: "Belonging");

            migrationBuilder.RenameTable(
                name: "Belonging",
                newName: "Belongings");

            migrationBuilder.RenameIndex(
                name: "IX_Belonging_PaymentID_AccountID_RecipientID",
                table: "Belongings",
                newName: "IX_Belongings_PaymentID_AccountID_RecipientID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Belongings",
                table: "Belongings",
                columns: new[] { "CategoryID", "PaymentID", "AccountID", "RecipientID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Belongings_Categories_CategoryID",
                table: "Belongings",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Belongings_Payments_PaymentID_AccountID_RecipientID",
                table: "Belongings",
                columns: new[] { "PaymentID", "AccountID", "RecipientID" },
                principalTable: "Payments",
                principalColumns: new[] { "PaymentID", "AccountID", "RecipientID" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Belongings_Categories_CategoryID",
                table: "Belongings");

            migrationBuilder.DropForeignKey(
                name: "FK_Belongings_Payments_PaymentID_AccountID_RecipientID",
                table: "Belongings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Belongings",
                table: "Belongings");

            migrationBuilder.RenameTable(
                name: "Belongings",
                newName: "Belonging");

            migrationBuilder.RenameIndex(
                name: "IX_Belongings_PaymentID_AccountID_RecipientID",
                table: "Belonging",
                newName: "IX_Belonging_PaymentID_AccountID_RecipientID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Belonging",
                table: "Belonging",
                columns: new[] { "CategoryID", "PaymentID", "AccountID", "RecipientID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Belonging_Categories_CategoryID",
                table: "Belonging",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Belonging_Payments_PaymentID_AccountID_RecipientID",
                table: "Belonging",
                columns: new[] { "PaymentID", "AccountID", "RecipientID" },
                principalTable: "Payments",
                principalColumns: new[] { "PaymentID", "AccountID", "RecipientID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
