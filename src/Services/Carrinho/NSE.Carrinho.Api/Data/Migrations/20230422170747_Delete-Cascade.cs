using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSE.Carrinho.API.Data.Migrations
{
    public partial class DeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CustomerShoppingCarts_ShoppingCartId",
                table: "CartItems");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CustomerShoppingCarts_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId",
                principalTable: "CustomerShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CustomerShoppingCarts_ShoppingCartId",
                table: "CartItems");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CustomerShoppingCarts_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId",
                principalTable: "CustomerShoppingCarts",
                principalColumn: "Id");
        }
    }
}
