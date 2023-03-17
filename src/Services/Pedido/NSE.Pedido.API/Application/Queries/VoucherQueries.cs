using NSE.Pedido.API.Application.DTO;
using NSE.Pedido.Domain.Vouchers;

namespace NSE.Pedido.API.Application.Queries
{
    public class VoucherQueries : IVoucherQueries
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherQueries(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {
            var voucher = await _voucherRepository.GetVoucherByCode(code);

            if (voucher is null) return null;

            return new VoucherDTO
            {
                Code = voucher.Code,
                Discount = voucher.Discount,
                DiscountType = (int)voucher.DiscountType,
                Percentage = voucher.Percentage
            };
        }
    }
}