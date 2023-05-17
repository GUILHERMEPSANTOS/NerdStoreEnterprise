using Core.Messages.Integration;
using NSE.Pagamento.API.Domain;
using NSE.Pagamento.API.Facade;

namespace NSE.Pagamento.API.Services
{
    public class BillingService : IBillingService
    {
        private readonly IPaymentFacade _paymentFacade;
        private readonly IPaymentRepository _paymentRepository;
        public Task<ResponseMessage> AuthorizePayment(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}