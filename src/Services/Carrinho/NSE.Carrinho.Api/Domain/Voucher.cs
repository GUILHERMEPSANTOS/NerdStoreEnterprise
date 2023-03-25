

namespace NSE.Carrinho.Api.Domain;
public class Voucher
{
    public string Code { get; set; }
    public decimal? Percentage { get; set; }
    public decimal? Discount { get; set; }
    public VoucherDiscountType DiscountType { get; set; }
}

public enum VoucherDiscountType
{
    Percentage = 0,
    Value = 1
}
