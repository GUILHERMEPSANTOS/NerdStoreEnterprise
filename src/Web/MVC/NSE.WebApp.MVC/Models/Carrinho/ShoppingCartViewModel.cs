using NSE.WebApp.MVC.Models.Pedido;

namespace NSE.WebApp.MVC.Models.Carrinho
{
    public class ShoppingCartViewModel
    {
        public decimal Total { get; set; }
        public VoucherViewModel Voucher { get; set; }
        public bool HasVoucher { get; set; }
        public decimal Discount { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
    }
}