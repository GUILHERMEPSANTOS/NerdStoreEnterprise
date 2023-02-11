namespace NSE.WebApp.MVC.Models.Carrinho
{
    public class ShoppingCartViewModel
    {
        public decimal Total { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
    }
}