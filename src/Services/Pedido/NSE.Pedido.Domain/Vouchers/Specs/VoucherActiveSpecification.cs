using System.Linq.Expressions;
using Core.Specification;

namespace NSE.Pedido.Domain.Vouchers.Specs
{
    public class VoucherActiveSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
        {
            return voucher => voucher.Active && !voucher.Used;
        }
    }
}