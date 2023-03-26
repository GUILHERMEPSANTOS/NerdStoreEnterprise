namespace NSE.Bff.Compras.DTOs
{
    public class ShoppingCartDTO
    {
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public bool HasVoucher { get; set; }
        public VoucherDTO Voucher { get; set; }
        public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
    }
}