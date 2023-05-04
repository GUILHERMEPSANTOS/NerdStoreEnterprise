using Core.DomainObjects;

namespace NSE.Pedido.Domain.Orders
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; }
        public decimal Price { get; set; }
        //EF Core
        public Order Order { get; set; }

        protected OrderItem()
        {
        }

        public OrderItem(Guid productId, string productName, int quantity, decimal price, string productImage = null)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            ProductImage = productImage;
            Price = price;
        }

        internal decimal CalculateAmount()
        {
            return Quantity * Price;
        }
    }
}