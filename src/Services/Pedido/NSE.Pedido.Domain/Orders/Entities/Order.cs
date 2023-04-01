using Core.DomainObjects;
using NSE.Pedido.Domain.Orders.Entities.Factories.DiscountFactories;
using NSE.Pedido.Domain.Orders.Entities.Strategies.DiscountStrategies;
using NSE.Pedido.Domain.Vouchers;

namespace NSE.Pedido.Domain.Orders
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool HasVoucher { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime DateAdded { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public Address Address { get; private set; }
        private readonly List<OrderItem> _ordersItems;
        private IReadOnlyCollection<OrderItem> OrderItems => _ordersItems;
        public IDiscountStrategy DiscountStrategy { get; private set; }
        // EF Core 
        public Voucher Voucher { get; private set; }


        internal void Authorize()
        {
            OrderStatus = OrderStatus.Authorized;
        }

        public void SetAddress(Address address)
        {
            Address = address;
        }

        internal void SetVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherId = voucher.Id;
            HasVoucher = true;
            ConfigureDiscountStretagy(voucher);
        }

        private void ConfigureDiscountStretagy(Voucher voucher)
        {
            DiscountStrategy = DiscountFactory.MakeDiscountStrategy(voucher);
        }

        internal void CalculateOrderAmount()
        {
            Amount = _ordersItems.Sum(orderItem => orderItem.CalculateAmount());
            CalculateAmount();
        }

        internal void CalculateAmount()
        {
            if (!HasVoucher) return;

            Discount = DiscountStrategy.CalculateDiscount(Amount);
            Amount = (Amount - Discount) < 0 ? 0 : (Amount - Discount);
        }
    }
}