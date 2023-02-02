using NSE.Carrinho.Api.Application.Validations;

namespace NSE.Carrinho.Api.Domain
{
    public class CartItem
    {
        internal const int MAX_ITEMS = 5;
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public Guid ShoppingCartId { get; set; }
        public CustomerShoppingCart CustomerShoppingCart { get; set; }

        public CartItem()
        {
            Id = Guid.NewGuid();
        }

        internal void SetShoppingCart(Guid id)
        {
            ShoppingCartId = id;
        }

        internal decimal CalculatePrice()
        {
            return Quantity * Price;
        }

        internal void AddUnit(int quantity)
        {
            Quantity = +quantity;
        }

        internal bool IsValid()
        {
            return new CartItemValidation().Validate(this).IsValid;
        }
    }
}