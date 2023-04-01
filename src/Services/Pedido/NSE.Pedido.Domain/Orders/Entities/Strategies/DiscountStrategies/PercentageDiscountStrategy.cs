namespace NSE.Pedido.Domain.Orders.Entities.Strategies.DiscountStrategies
{
    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal? _percentage;

        public PercentageDiscountStrategy(decimal? percentage)
        {
            _percentage = percentage;
        }

        public decimal CalculateDiscount(decimal amount)
        {
            var discount = amount * _percentage / 100;

            return discount ?? 0;
        }
    }
}