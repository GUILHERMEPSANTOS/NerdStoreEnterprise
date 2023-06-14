using NSE.Pagamento.API.Domain;

namespace NSE.Pagamento.API.Facade
{
    public interface IPaymentFacade
    {
        Task<Transaction> AuthorizePayment(Payment payment);
        Task<Transaction> GetTransaction(Transaction transaction);
        Task<Transaction> CancelTransaction(Transaction transaction);
    }
}