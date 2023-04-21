using Core.Specification.Validation;

namespace NSE.Pedido.Domain.Vouchers.Specs
{
    public class VoucherValidation : SpecValidator<Voucher>
    {
        public VoucherValidation()
        {
            var dataSpecification = new VoucherDateSpecification();
            var quantitySpecication = new VoucherDateSpecification();
            var activeSpecification = new VoucherDateSpecification();

            Add("dataSpecification", new Rule<Voucher>(dataSpecification, "Este voucher está expirado"));
            Add("quantitySpecication", new Rule<Voucher>(quantitySpecication, "Este voucher já foi utilizado"));
            Add("activeSpecification", new Rule<Voucher>(activeSpecification, "Este voucher não está mais ativo"));
        }
    }
}