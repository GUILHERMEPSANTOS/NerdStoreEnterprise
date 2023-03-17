using Core.Data;
using Microsoft.EntityFrameworkCore;
using NSE.Pedido.Domain.Vouchers;
using NSE.Pedido.Infra.Context;

namespace NSE.Pedido.Infra.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        public readonly OrdersContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public VoucherRepository(OrdersContext context)
        {
            _context = context;
        }
        public async Task<Voucher> GetVoucherByCode(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(voucher => voucher.Code == code);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}