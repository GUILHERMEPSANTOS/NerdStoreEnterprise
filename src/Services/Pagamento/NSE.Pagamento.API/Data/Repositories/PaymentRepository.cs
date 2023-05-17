using Core.Data;
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}