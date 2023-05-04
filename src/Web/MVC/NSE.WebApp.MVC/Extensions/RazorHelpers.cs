using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;

namespace NSE.WebApp.MVC.Extensions
{
    public static class RazorHelpers
    {
        public static string HashEmailForGravatar(this RazorPage page, string email)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
            var sBuilder = new StringBuilder();

            foreach (var item in data)
            {
                sBuilder.Append(item.ToString("X2"));
            }

            return sBuilder.ToString();
        }

        public static string UnitByProductAmount(this RazorPage page, int unities, decimal amount)
        {
            return $"{unities}x {FormatCurrency(amount)} = Total: {FormatCurrency(amount * unities)}";
        }

        public static string StockMessage(this RazorPage page, int quantity)
        {
            return quantity > 0 ? $"Apenas {quantity} em estoque!" : "Produto esgotado";
        }

        public static string FormatCurrency(this RazorPage page, decimal valor)
        {
            return FormatCurrency(valor);
        }

        private static string FormatCurrency(decimal valor)
        {
            return string.Format(System.Globalization.CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valor);
        }

        public static string UnityByProduct(this RazorPage page, int units)
        {
            return units > 1 ? $"{units} unidades" : $"{units} unidade";
        }

        public static string ShowStatus(this RazorPage page, int status)
        {
            var statusMensagem = "";
            var statusClasse = "";

            switch (status)
            {
                case 1:
                    statusClasse = "info";
                    statusMensagem = "Processing";
                    break;
                case 2:
                    statusClasse = "primary";
                    statusMensagem = "Approved";
                    break;
                case 3:
                    statusClasse = "danger";
                    statusMensagem = "Refused";
                    break;
                case 4:
                    statusClasse = "success";
                    statusMensagem = "Delivered";
                    break;
                case 5:
                    statusClasse = "warning";
                    statusMensagem = "Canceled";
                    break;

            }

            return $"<span class='badge badge-{statusClasse}'>{statusMensagem}</span>";
        }

        public static string SelectOptionsByQuantity(this RazorPage page, int quantity, int selectedValue = 0)
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= quantity; i++)
            {
                var selected = "";
                if (i == selectedValue) selected = "selected";
                sb.Append($"<option {selected} value='{i}'>{i}</option>");
            }

            return sb.ToString();
        }
    }
}