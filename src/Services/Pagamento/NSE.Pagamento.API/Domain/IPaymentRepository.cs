using Core.DomainObjects.Data;

namespace NSE.Pagamento.API.Domain
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void AddPayment(Payment payment);
    }
}