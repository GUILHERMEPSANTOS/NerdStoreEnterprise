using NSE.Pedido.API.Application.DTO;
using NSE.Pedido.Domain.Vouchers;

namespace NSE.Pedido.API.Application.Queries
{
    public class VoucherQueries : IVoucherQueries
    {
        private readonly IVoucherRepository _voucherRepository;
        private bool IsValidVoucher;

        public VoucherQueries(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {
            var voucher = await _voucherRepository.GetVoucherByCode(code);

            CheckAndUpdateVoucherValidity(voucher);

            if (!IsValidVoucher) return null;

            return new VoucherDTO
            {
                Code = voucher.Code,
                Discount = voucher.Discount,
                DiscountType = (int)voucher.DiscountType,
                Percentage = voucher.Percentage
            };
        }

        private void CheckAndUpdateVoucherValidity(Voucher voucher)
        {
            IsValidVoucher = !(voucher is null) && voucher.CanUse();
        }
    }
}