using Core.DomainObjects;
using NSE.Pedido.Domain.Orders.Entities.Strategies.DiscountStrategies;
using NSE.Pedido.Domain.Vouchers;

namespace NSE.Pedido.Domain.Orders.Entities.Factories.DiscountFactories
{
    public class DiscountFactory
    {
        public static IDiscountStrategy MakeDiscountStrategy(Voucher voucher)
        {
            switch (voucher.DiscountType)
            {
                case VoucherDiscountType.Percentage:
                    return new PercentageDiscountStrategy(voucher.Percentage ?? 0);
                case VoucherDiscountType.Value:
                    return new ValueDiscountStrategy(voucher.Discount ?? 0);
                default:
                    throw new DomainException("Tipo de desconto inválido");
            }
        }
    }
}