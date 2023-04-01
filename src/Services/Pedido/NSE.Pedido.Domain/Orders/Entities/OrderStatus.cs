namespace NSE.Pedido.Domain.Orders
{
    public enum OrderStatus
    {
        Authorized = 1,
        Paid = 2,
        Refused = 3,
        Delivered = 4,
        Canceled = 5
    }
}
