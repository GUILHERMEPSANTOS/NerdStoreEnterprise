using FluentValidation.Results;
using NSE.Carrinho.Api.Application.Validations;

namespace NSE.Carrinho.Api.Domain
{
    public class CustomerShoppingCart
    {
        internal const int MAX_ITEMS = 5;
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public ValidationResult ValidationResult { get; set; }
        public bool HasVoucher { get; set; }
        public decimal Discount { get; set; }
        public Voucher Voucher { get; set; }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            HasVoucher = true;
            CalculateShoppingCartPrice();
        }

        public void CalculateDiscountPrice()
        {
            if (!HasVoucher) return;

            decimal discount = 0;
            decimal price = Total;

            if (Voucher.DiscountType == VoucherDiscountType.Value)
            {
                if (Voucher.Discount.HasValue)
                {
                    discount = Voucher.Discount.Value;
                    price -= discount;
                }
            }

            if (Voucher.DiscountType == VoucherDiscountType.Percentage)
            {
                if (Voucher.Percentage.HasValue)
                {
                    discount = (price * Voucher.Percentage.Value) / 100;
                    price -= discount;
                }
            }

            Total = price < 0 ? 0 : price;
            Discount = discount;
        }


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
            CalculateDiscountPrice();
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