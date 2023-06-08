using Core.Messages.Integration;
using NSE.Pagamento.API.Domain;
using NSE.Pagamento.API.Facade;

namespace NSE.Pagamento.API.Services
{
    public class BillingService : ServiceBase, IBillingService
    {
        private readonly IPaymentFacade _paymentFacade;
        private readonly IPaymentRepository _paymentRepository;

        public BillingService(IPaymentFacade paymentFacade, IPaymentRepository paymentRepository)
        {
            _paymentFacade = paymentFacade;
            _paymentRepository = paymentRepository;
        }

        public async Task<ResponseMessage> AuthorizePayment(Payment payment)
        {
            Transaction transaction = await _paymentFacade.AuthorizePayment(payment);

            if (transaction.TransactionStatus != TransactionStatus.Authorized)
            {
                AddError(propertyName: "Pagamento", errorMessage: "Pagamento recusado. Por favor, entre em contato com sua operadora.");
                return new ResponseMessage(validationResult);
            }

            payment.AddTransaction(transaction);
            _paymentRepository.AddPayment(payment);

            if (!await _paymentRepository.UnitOfWork.Commit())
            {
                AddError(propertyName: "Pagamento", errorMessage: "Houve um erro ao efetuar o pagamento.");

                 // Canceling the payment on the service

                return new ResponseMessage(validationResult);
            }


            return new ResponseMessage(validationResult);
        }
    }
}