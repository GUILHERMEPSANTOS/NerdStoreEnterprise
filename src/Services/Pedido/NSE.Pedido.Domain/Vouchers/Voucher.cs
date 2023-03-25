using Core.DomainObjects;
using NSE.Pedido.Domain.Vouchers.Specs;

namespace NSE.Pedido.Domain.Vouchers
{
    public class Voucher : Entity, IAggregateRoot
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? Discount { get; private set; }
        public int Quantity { get; private set; }
        public VoucherDiscountType DiscountType { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UsedAt { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        public bool CanUse()
        {
            var specifications = new VoucherActiveSpecification()
                .And(new VoucherDateSpecification())
                .And(new VoucherQuantitySpecification());

            return specifications.IsSatisfiedBy(this);
        }

        public void SetAsUsed()
        {
            Used = true;
            UsedAt = DateTime.Now;
            Active = false;
            Quantity = 0;
        }
    }
}