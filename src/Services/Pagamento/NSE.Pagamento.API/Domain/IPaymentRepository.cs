using Core.DomainObjects.Data;

namespace NSE.Pagamento.API.Domain
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void AddPayment(Payment payment);
        void AddTransaction(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionsByOrderId(Guid orderId);
    }
}