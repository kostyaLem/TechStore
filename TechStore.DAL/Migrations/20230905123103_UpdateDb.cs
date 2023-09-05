using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.DAL.Migrations
{
    public partial class UpdateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_StoredImages_ImageId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_StoredImages_ImageId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "StoredImages");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ImageId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ImageId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Customers");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Employees",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Customers",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StoredImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoredImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ImageId",
                table: "Employees",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ImageId",
                table: "Customers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_StoredImages_ProductId",
                table: "StoredImages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_StoredImages_ImageId",
                table: "Customers",
                column: "ImageId",
                principalTable: "StoredImages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_StoredImages_ImageId",
                table: "Employees",
                column: "ImageId",
                principalTable: "StoredImages",
                principalColumn: "Id");
        }
    }
}
