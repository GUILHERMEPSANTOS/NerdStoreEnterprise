using Core.Messages;
using NSE.Pedido.API.Application.DTO;
using NSE.Pedido.API.Application.Validations;

namespace NSE.Pedido.API.Application.Commands
{
    public class AddOrderCommand : Command
    {
        // Order
        public Guid CustomerId { get; set; }
        public decimal Amount { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }

        //Voucher
        public string VoucherCode { get; set; }
        public bool HasVoucher { get; set; }
        public decimal Discount { get; set; }

        //Adress
        public AddressDTO Address { get; set; }

        //card
        public string CardNumber { get; set; }
        public string Holder { get; set; }
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }

        public bool IsValid()
        {
            ValidationResult = new AddOrderValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}