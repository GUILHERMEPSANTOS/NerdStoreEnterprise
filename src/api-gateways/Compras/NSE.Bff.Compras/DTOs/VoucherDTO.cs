namespace NSE.Bff.Compras.DTOs
{
    public class VoucherDTO
    {
        public string Code { get;  set; }
        public decimal? Percentage { get;  set; }
        public decimal? Discount { get;  set; }
        public int DiscountType { get;  set; }
    }
}