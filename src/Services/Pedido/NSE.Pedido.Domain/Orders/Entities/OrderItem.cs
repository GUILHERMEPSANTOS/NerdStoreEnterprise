using Core.DomainObjects;

namespace NSE.Pedido.Domain.Orders
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        public Address Address { get; set; }
        //EF Core
        public Order Order { get; set; }

        protected OrderItem()
        {
        }

        public OrderItem(Guid orderId, Guid productId, int quantity, decimal price,
            Address address, Order order, string productImage = null)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            ProductImage = productImage;
            Price = price;
            Address = address;
            Order = order;
        }

        internal decimal CalculateAmount()
        {
            return Quantity * Price;
        }
    }
}