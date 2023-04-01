namespace NSE.Pedido.Domain.Orders.Entities.Strategies.DiscountStrategies
{
    public class ValueDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal? _value
;
        public ValueDiscountStrategy(decimal? value)
        {
            _value = value;
        }

        public decimal CalculateDiscount(decimal amount)
        {
            var discount = _value;

            return discount ?? 0;
        }
    }
}