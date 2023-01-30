namespace NSE.Carrinho.Api.Domain
{
    public class CartItem
    {
        public CartItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public Guid ShoppingCartId { get; set; }
        public CustomerShoppingCart CustomerShoppingCart { get; set; }

    }
}