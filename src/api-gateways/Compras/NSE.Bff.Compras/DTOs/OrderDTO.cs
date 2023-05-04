using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Validation;

namespace NSE.Bff.Compras.DTOs
{
    public class OrderDTO
    {
        #region Order

        public int Code { get; set; }
        // Authorized = 1,
        // Paid = 2,
        // Refused = 3,
        // Delivered = 4,
        // Canceled = 5

        public decimal Amount { get; set; }
        public List<CartItemDTO> OrderItems { get; set; }

        public string VoucherCode { get; set; }
        public bool HasVoucher { get; set; }
        public decimal Discount { get; set; }


        public int Status { get; set; }
        public DateTime Date { get; set; }

        #endregion

        #region Address
        public AddressDTO Address { get; set; }

        #endregion

        #region Credit Card

        [Required(ErrorMessage = "Card number is required")]
        [DisplayName("Card number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Please inform card holder")]
        [DisplayName("Holder")]
        public string Holder { get; set; }

        [Required(ErrorMessage = "Credit card expiration is required")]
        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "The expiration must be in form of MM/YY")]
        [CreditCardExpired(ErrorMessage = "Expired Credit Card")]
        [DisplayName("Expiration MM/YY")]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "Security code is required")]
        [DisplayName("Security Code")]
        public string SecurityCode { get; set; }

        #endregion
    }
}