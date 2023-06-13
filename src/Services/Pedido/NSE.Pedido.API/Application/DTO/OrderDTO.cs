using NSE.Pedido.Domain.Orders;

namespace NSE.Pedido.API.Application.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Code { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public decimal Discount { get; set; }
        public string VoucherCode { get; set; }
        public bool HasVoucher { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
        public AddressDTO Address { get; set; }

        public static OrderDTO ToOrderDTO(Order order)
        {
            var orderDTO = new OrderDTO
            {
                Id = order.Id,
                Code = order.Code,
                Status = (int)order.OrderStatus,
                Date = order.DateAdded,
                Amount = order.Amount,
                Discount = order.Discount,
                HasVoucher = order.HasVoucher,
                OrderItems = new List<OrderItemDTO>(),
                Address = new AddressDTO()
            };

            foreach (var item in order.OrderItems)
            {
                orderDTO.OrderItems.Add(OrderItemDTO.ToOrderItemDTO(item));
            }

            orderDTO.Address = AddressDTO.ToAddressDTO(order);

            return orderDTO;
        }
    }
}