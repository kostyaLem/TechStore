using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.DAL.Migrations
{
    public partial class UpdateCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Customers");
        }
    }
}
