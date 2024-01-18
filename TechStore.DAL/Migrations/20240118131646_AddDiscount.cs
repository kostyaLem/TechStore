using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.DAL.Migrations
{
    public partial class AddDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Orders");

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "OrderProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "OrderProducts");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
