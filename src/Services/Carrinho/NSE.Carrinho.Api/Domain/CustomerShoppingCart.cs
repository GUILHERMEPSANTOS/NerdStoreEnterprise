using FluentValidation.Results;
using NSE.Carrinho.Api.Application.Validations;

namespace NSE.Carrinho.Api.Domain
{
    public class CustomerShoppingCart
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public ValidationResult ValidationResult { get; set; }


        internal bool IsValid()
        {
            var errors = Items.SelectMany(item => new CartItemValidation().Validate(item).Errors).ToList();
            errors.AddRange(new CustomerShoppingCartValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(errors);

            return ValidationResult.IsValid;

        }

        public CustomerShoppingCart(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }

        public CustomerShoppingCart() { }

        internal void AddItem(CartItem item)
        {
            item.SetShoppingCart(Id);

            if (HasItem(item))
            {
                var itemRef = GetCartItemBy(item.ProductId);

                itemRef.AddUnit(item.Quantity);
                item = itemRef;
                Items.Remove(itemRef);
            }

            Items.Add(item);
            CalculateShoppingCartPrice();
        }

        internal bool HasItem(CartItem item)
        {
            return Items.Any(cartItem => cartItem.ProductId == item.ProductId);
        }

        internal CartItem GetCartItemBy(Guid ProductId)
        {
            return Items.FirstOrDefault(item => item.ProductId == ProductId);
        }

        internal void CalculateShoppingCartPrice()
        {
            Total = Items.Sum(item => item.CalculatePrice());
        }

        internal void UpdateCartItem(CartItem item)
        {
            var itemRef = GetCartItemBy(item.ProductId);

            Items.Remove(itemRef);
            Items.Add(item);

            CalculateShoppingCartPrice();
        }

        internal void UpdateUnit(CartItem item, int unities)
        {
            item.UpdateUnit(unities);
            UpdateCartItem(item);
        }

        internal void RemoveItem(CartItem item)
        {
            var itemExistsInList = GetCartItemBy(item.ProductId);

            Items.Remove(itemExistsInList);

            CalculateShoppingCartPrice();
        }
    }
}