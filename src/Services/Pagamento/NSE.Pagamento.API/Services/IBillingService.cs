using Core.Messages.Integration;
using NSE.Pagamento.API.Domain;

namespace NSE.Pagamento.API.Services
{
    public interface IBillingService
    {
        Task<ResponseMessage> AuthorizePayment(Payment payment);
        Task<ResponseMessage> GetTransaction(Guid orderId);
        Task<ResponseMessage> CancelTransaction(Guid orderId);
    }
}