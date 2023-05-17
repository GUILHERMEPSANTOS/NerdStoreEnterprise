using Microsoft.Extensions.Options;
using NSE.Pagamento.API.Domain;
using NSE.Pagamento.NerdsPag;

namespace NSE.Pagamento.API.Facade
{
    public class CreditCardPaymentFacade : IPaymentFacade
    {
        public readonly BillingConfig _billingConfig;

        public CreditCardPaymentFacade(IOptions<BillingConfig> billingConfig)
        {
            _billingConfig = billingConfig.Value;
        }

        public async Task<Domain.Transaction> AuthorizePayment(Payment payment)
        {
            var nerdsPagSvc = new NerdsPagService(_billingConfig.DefaultApiKey,
                _billingConfig.DefaultEncryptionKey);

            var cardHashGen = new CardHash(nerdsPagSvc)
            {
                CardNumber = payment.CreditCard.CardNumber,
                CardHolderName = payment.CreditCard.Holder,
                CardExpirationDate = payment.CreditCard.ExpirationDate,
                CardCvv = payment.CreditCard.SecurityCode
            };

            var cardHash = cardHashGen.Generate();

            var transaction = new NSE.Pagamento.NerdsPag.Transaction(nerdsPagSvc)
            {
                CardHash = cardHash,
                CardNumber = payment.CreditCard.CardNumber,
                CardHolderName = payment.CreditCard.Holder,
                CardExpirationDate = payment.CreditCard.ExpirationDate,
                CardCvv = payment.CreditCard.SecurityCode,
                PaymentMethod = PaymentMethod.CreditCard,
                Amount = payment.Amount
            };

            return ToTransaction(await transaction.AuthorizeCardTransaction());
        }
        public static Domain.Transaction ToTransaction(NerdsPag.Transaction transaction)
        {
            return new Domain.Transaction
            {
                Id = Guid.NewGuid(),
                TransactionStatus = (Domain.TransactionStatus)transaction.Status,
                Amount = transaction.Amount,
                CreditCardCompany = transaction.CardBrand,
                AuthorizationCode = transaction.AuthorizationCode,
                TransactionCost = transaction.Cost,
                TransactionDate = transaction.TransactionDate,
                NSU = transaction.Nsu,
                TID = transaction.Tid
            };
        }
    }
}