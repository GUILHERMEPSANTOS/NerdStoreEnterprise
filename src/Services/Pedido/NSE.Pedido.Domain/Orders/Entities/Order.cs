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
        public Voucher Voucher { get; private set; }
        private readonly List<OrderItem> _ordersItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _ordersItems;
        private IDiscountStrategy DiscountStrategy { get; set; }

        public Order(Guid customerId, decimal amount, List<OrderItem> ordersItems
                    , bool hasVoucher = false, decimal discount = 0, Guid? voucherId = null)
        {

            CustomerId = customerId;
            Amount = amount;
            Discount = discount;

            HasVoucher = hasVoucher;
            VoucherId = voucherId;
            _ordersItems = ordersItems;

        }

        protected Order()
        {
        }

        public void Authorize()
        {
            OrderStatus = OrderStatus.Authorized;
        }

        public void SetAddress(Address address)
        {
            Address = address;
        }

        public void SetVoucher(Voucher voucher)
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

        public void CalculateOrderAmount()
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