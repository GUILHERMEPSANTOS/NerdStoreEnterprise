namespace NSE.Pedido.Domain.Orders.Entities.Strategies.DiscountStrategies
{
    public interface IDiscountStrategy
    {
        decimal CalculateDiscount(decimal amount);
    }
}