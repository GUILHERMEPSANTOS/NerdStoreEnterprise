using Core.Data;
using Microsoft.EntityFrameworkCore;
using NSE.Pagamento.API.Data.Context;
using NSE.Pagamento.API.Domain;

namespace NSE.Pagamento.API.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BillingContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public PaymentRepository(BillingContext context)
        {
            _context = context;
        }

        public void AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
        }


        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
        }
        public async Task<IEnumerable<Transaction>> GetTransactionsByOrderId(Guid orderId)
        {
            return await _context.Transactions.AsNoTracking()
                .Where(transaction => transaction.Payment.OrderId == orderId).ToArrayAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}