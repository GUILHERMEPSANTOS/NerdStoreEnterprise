using Core.DomainObjects;

namespace NSE.Catalogo.API.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Image { get; set; }
        public int StockQuantity { get; set; }

        public void TakeFromInventory(int quantity)
        {
            if (StockQuantity >= quantity)
                StockQuantity -= quantity;
        }
        internal bool IsAvailable(int quantity)
        {
            return Active && StockQuantity >= quantity;
        }
    }
}