using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSE.Carrinho.API.Data.Migrations
{
    public partial class Voucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "CustomerShoppingCarts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "HasVoucher",
                table: "CustomerShoppingCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "CustomerShoppingCarts",
                type: "Varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "VoucherDiscount",
                table: "CustomerShoppingCarts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VoucherDiscountType",
                table: "CustomerShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "VoucherPercentage",
                table: "CustomerShoppingCarts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CartItems",
                type: "Varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "Varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "CartItems",
                type: "Varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "Varchar(100)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "CustomerShoppingCarts");

            migrationBuilder.DropColumn(
                name: "HasVoucher",
                table: "CustomerShoppingCarts");

            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "CustomerShoppingCarts");

            migrationBuilder.DropColumn(
                name: "VoucherDiscount",
                table: "CustomerShoppingCarts");

            migrationBuilder.DropColumn(
                name: "VoucherDiscountType",
                table: "CustomerShoppingCarts");

            migrationBuilder.DropColumn(
                name: "VoucherPercentage",
                table: "CustomerShoppingCarts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CartItems",
                type: "Varchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "Varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "CartItems",
                type: "Varchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "Varchar(100)",
                oldNullable: true);
        }
    }
}
