namespace NSE.Carrinho.Api.Domain
{
    public class CustomerShoppingCart
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public CustomerShoppingCart(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }   

        public CustomerShoppingCart() { }
    }
}