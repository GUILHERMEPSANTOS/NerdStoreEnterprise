using Core.DomainObjects;

namespace NSE.Pagamento.API.Domain
{
    public class Payment : Entity, IAggregateRoot
    {
        public Guid OrderId { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal Amount { get; set; }
        public CreditCard CreditCard { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

        public Payment()
        {
            Transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }
    }
}