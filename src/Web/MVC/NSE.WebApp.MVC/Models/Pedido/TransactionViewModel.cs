

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NSE.WebApp.MVC.Models.Carrinho;
using NSE.WebApp.MVC.Models.Cliente;

namespace NSE.WebApp.MVC.Models.Pedido
{
    public class TransactionViewModel
    {
         #region Order

        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public string VoucherCode { get; set; }
        public bool HasVoucher { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        #endregion

        #region Address
        public AddressViewModel Address { get; set; }

        #endregion
    }
}