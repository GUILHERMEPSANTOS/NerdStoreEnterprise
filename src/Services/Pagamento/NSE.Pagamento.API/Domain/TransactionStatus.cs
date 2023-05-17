namespace NSE.Pagamento.API.Domain
{
    public enum TransactionStatus
    {
        Authorized = 1,
        Paid,
        Denied,
        Refund,
        Canceled
    }
}