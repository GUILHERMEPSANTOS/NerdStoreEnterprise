using System.Data;
using Dapper;
using NSE.Pedido.API.Application.DTO;
using NSE.Pedido.Domain.Orders.Interfaces;

namespace NSE.Pedido.API.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDTO> GetAuthorizedOrder()
        {
            using var connection = _orderRepository.GetDbConnection();

            var sql = @"SELECT Id                = Orders.Id
                              ,CustomerId        = Orders.CustomerId
                              ,OrderItemId       = OrderItems.Id
                              ,Id                = OrderItems.Id
                              ,OrderId           = OrderItems.OrderId
                              ,ProductId         = OrderItems.ProductId
                              ,Quantity          = OrderItems.Quantity
                        FROM Orders
                          INNER JOIN OrderItems
                          ON Orders.Id = OrderItems.OrderId
                        WHERE Orders.OrderStatus = 1
                        ORDER BY Orders.DateAdded;";

            var orderDTO = await connection.QueryAsync<OrderDTO, OrderItemDTO, OrderDTO>(
                sql: sql,
                commandType: CommandType.Text,
                map: (order, orderItem) =>
                {
                    order.OrderItems ??= new List<OrderItemDTO>();
                    order.OrderItems.Add(orderItem);

                    return order;
                },
                 splitOn: "OrderId,OrderItemId"
            );

            return orderDTO.FirstOrDefault();
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersBy(Guid customerId)
        {
            var orders = await _orderRepository.GetOrdersBy(customerId);

            return orders.Select(OrderDTO.ToOrderDTO);
        }

        //TODO: 21/04/2023 - corrigir isso futuramente :_:
        public async Task<OrderDTO> GetLastOrder(Guid customerId)
        {
            const string sql = @"SELECT
                                P.ID AS 'PRODUCTID', P.CODE, P.HASVOUCHER, P.DISCOUNT, P.AMOUNT,P.ORDERSTATUS,
                                P.STREETADDRESS,P.BUILDINGNUMBER, P.NEIGHBORHOOD, P.ZIPCODE, P.SECONDARYADDRESS, P.CITY, P.STATE,
                                PIT.ID AS 'PRODUCTITEMID',PIT.PRODUCTNAME, PIT.QUANTITY, PIT.PRODUCTIMAGE, PIT.PRICE 
                                FROM Orders P 
                                INNER JOIN OrderITEMS PIT ON P.ID = PIT.OrderID 
                                WHERE P.CUSTOMERID = @customerId 
                                AND P.DateAdded between DATEADD(minute, -5,  @Now) and @Now
                                ORDER BY P.DateAdded DESC";

            var order = await _orderRepository.GetDbConnection()
                .QueryAsync<dynamic>(sql, new { customerId, Now = DateTime.Now });

            if (!order.Any())
                return null;

            return MapOrder(order);
        }

        private OrderDTO MapOrder(dynamic result)
        {
            var order = new OrderDTO
            {
                Code = result[0].CODE,
                Status = result[0].ORDERSTATUS,
                Amount = result[0].AMOUNT,
                Discount = result[0].DISCOUNT,
                HasVoucher = result[0].HASVOUCHER,

                OrderItems = new List<OrderItemDTO>(),
                Address = new AddressDTO
                {
                    StreetAddress = result[0].STREETADDRESS,
                    Neighborhood = result[0].NEIGHBORHOOD,
                    ZipCode = result[0].ZIPCODE,
                    City = result[0].CITY,
                    SecondaryAddress = result[0].SECONDARYADDRESS,
                    State = result[0].STATE,
                    BuildingNumber = result[0].BUILDINGNUMBER
                }
            };

            foreach (var item in result)
            {
                var orderItem = new OrderItemDTO
                {
                    Name = item.PRODUCTNAME,
                    Price = item.PRICE,
                    Quantity = item.QUANTITY,
                    Image = item.PRODUCTIMAGE
                };

                order.OrderItems.Add(orderItem);
            }

            return order;
        }

    }
}