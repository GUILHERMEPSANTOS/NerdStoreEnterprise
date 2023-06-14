using Core.DomainObjects;
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

        public async Task<ResponseMessage> CancelTransaction(Guid orderId)
        {
            var transactions = await _paymentRepository.GetTransactionsByOrderId(orderId);
            var authorizedTransaction = transactions.FirstOrDefault(transaction => transaction.TransactionStatus == TransactionStatus.Authorized);

            if (authorizedTransaction == null)
            {
                throw new DomainException($"Transação não encontrada para o pedido: {orderId}");
            }

            var transaction = await _paymentFacade.CancelTransaction(authorizedTransaction);

            if (transaction.TransactionStatus != TransactionStatus.Paid)
            {
                AddError("Pagamento", $"Impossível cancelar o pagamento, do pedido: {orderId}");

                return new ResponseMessage(validationResult);
            }

            transaction.PaymentId = authorizedTransaction.PaymentId;
            _paymentRepository.AddTransaction(transaction);

            if (!await _paymentRepository.UnitOfWork.Commit())
            {
                AddError(propertyName: "Pagamento", errorMessage: $"Não foi possível concluir o cancelamento do pagamento do pedido. {orderId}.");

                return new ResponseMessage(validationResult);
            }

            return new ResponseMessage(validationResult);
        }

        public async Task<ResponseMessage> GetTransaction(Guid orderId)
        {
            var transactions = await _paymentRepository.GetTransactionsByOrderId(orderId);
            var authorizedTransaction = transactions.FirstOrDefault(transaction => transaction.TransactionStatus == TransactionStatus.Authorized);

            if (authorizedTransaction == null)
            {
                throw new DomainException($"Transação não encontrada para o pedido: {orderId}");
            }

            var transaction = await _paymentFacade.GetTransaction(authorizedTransaction);

            if (transaction.TransactionStatus != TransactionStatus.Paid)
            {
                AddError("Pagamento", $"Impossível realizar o pagamento. {orderId}");

                return new ResponseMessage(validationResult);
            }

            transaction.PaymentId = authorizedTransaction.PaymentId;
            _paymentRepository.AddTransaction(transaction);

            if (!await _paymentRepository.UnitOfWork.Commit())
            {
                AddError(propertyName: "Pagamento", errorMessage: $"Não foi possível concluir a captura do pagamento do pedido. {orderId}.");

                return new ResponseMessage(validationResult);
            }

            return new ResponseMessage(validationResult);
        }
    }
}