using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSE.Cliente.API.Data.Migrations
{
    public partial class Clientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(254)", nullable: false),
                    Cpf = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false),
                    Excluded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    HouseNumber = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Complement = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    Neighborhood = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    ZipCode = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    City = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    State = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_CustomerId",
                table: "Adresses",
                column: "CustomerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
