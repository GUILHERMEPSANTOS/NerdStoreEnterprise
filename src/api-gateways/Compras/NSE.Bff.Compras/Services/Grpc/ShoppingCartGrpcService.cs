using NSE.Bff.Compras.DTOs;
using NSE.Bff.Compras.Services.Grpc.Interfaces;
using NSE.Carrinho.Api.Services.gRPC;
using static NSE.Carrinho.Api.Services.gRPC.ShoppingCartOrders;

namespace NSE.Bff.Compras.Services.Grpc
{
    public class ShoppingCartGrpcService : IShoppingCartGrpcService
    {
        private readonly ILogger<ShoppingCartGrpcService> _logger;
        private readonly ShoppingCartOrdersClient _shoppingCartOrdersClient;

        public ShoppingCartGrpcService(ShoppingCartOrdersClient shoppingCartOrdersClient, ILogger<ShoppingCartGrpcService> logger)
        {
            _shoppingCartOrdersClient = shoppingCartOrdersClient;
            _logger = logger;
        }

        public async Task<ShoppingCartDTO> GetShoppingCart()
        {
            _logger.LogInformation("BFF Chamando GetShoppingCartAsync");

            var response = await _shoppingCartOrdersClient.GetShoppingCartAsync(new GetShoppingCartRequest());

            return MapShoppingCartProtoResponseDTO(response);
        }

        private static ShoppingCartDTO MapShoppingCartProtoResponseDTO(CustomerShoppingCartClientResponse shoppingCartResponse)
        {
            var cartDto = new ShoppingCartDTO
            {
                Total = (decimal)shoppingCartResponse.Total,
                Discount = (decimal)shoppingCartResponse.Discount,
                HasVoucher = shoppingCartResponse.Hasvoucher
            };

            if (shoppingCartResponse.Voucher != null)
            {
                cartDto.Voucher = new VoucherDTO
                {
                    Code = shoppingCartResponse.Voucher.Code,
                    Percentage = (decimal?)shoppingCartResponse.Voucher.Percentage,
                    Discount = (decimal?)shoppingCartResponse.Voucher.Discount,
                    DiscountType = shoppingCartResponse.Voucher.Discounttype
                };
            }

            foreach (var item in shoppingCartResponse.Items)
            {
                cartDto.Items.Add(new CartItemDTO
                {
                    Name = item.Name,
                    Image = item.Image,
                    ProductId = Guid.Parse(item.Productid),
                    Quantity = item.Quantity,
                    Price = (decimal)item.Price
                });
            }

            return cartDto;
        }
    }
}