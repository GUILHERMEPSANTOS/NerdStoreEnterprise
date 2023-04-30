using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSE.Cliente.API.Data.Migrations
{
    public partial class correcaoaddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Adresses",
                newName: "StreetAddress");

            migrationBuilder.RenameColumn(
                name: "HouseNumber",
                table: "Adresses",
                newName: "BuildingNumber");

            migrationBuilder.RenameColumn(
                name: "Complement",
                table: "Adresses",
                newName: "SecondaryAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "Adresses",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "SecondaryAddress",
                table: "Adresses",
                newName: "Complement");

            migrationBuilder.RenameColumn(
                name: "BuildingNumber",
                table: "Adresses",
                newName: "HouseNumber");
        }
    }
}
