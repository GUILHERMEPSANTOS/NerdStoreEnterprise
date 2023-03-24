using System.Linq.Expressions;
using Core.Specification;

namespace NSE.Pedido.Domain.Vouchers.Specs
{
    public class VoucherDateSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression()
        {
            return voucher => voucher.ExpirationDate >= DateTime.Now;
        }
    }
}