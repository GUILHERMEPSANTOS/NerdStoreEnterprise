namespace NSE.Pagamento.NerdsPag
{
    public enum TransactionStatus
    {
        Authorized = 1,
        Paid,
        Refused,
        Chargeback,
        Cancelled
    }
}