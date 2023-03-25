using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSE.Carrinho.API.Data.Migrations
{
    public partial class Vouchercorrecao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VoucherDiscountType",
                table: "CustomerShoppingCarts",
                newName: "Voucher_DiscountType");

            migrationBuilder.AlterColumn<string>(
                name: "VoucherCode",
                table: "CustomerShoppingCarts",
                type: "Varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "Varchar(50)",
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Voucher_DiscountType",
                table: "CustomerShoppingCarts",
                newName: "VoucherDiscountType");

            migrationBuilder.AlterColumn<string>(
                name: "VoucherCode",
                table: "CustomerShoppingCarts",
                type: "Varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "Varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
