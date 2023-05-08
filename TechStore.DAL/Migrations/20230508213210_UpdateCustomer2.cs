using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.DAL.Migrations
{
    public partial class UpdateCustomer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Login",
                table: "Customers",
                column: "Login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Login",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
