using Core.DomainObjects.Data;

namespace NSE.Pedido.Domain.Vouchers
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher> GetVoucherByCode(string code);
    }
}