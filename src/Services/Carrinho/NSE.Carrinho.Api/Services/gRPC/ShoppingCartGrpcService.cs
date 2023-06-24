using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using NSE.Carrinho.Api.Application.Interfaces;
using NSE.Carrinho.Api.Domain;
using NSE.Carrinho.Api.Services.gRPC;
using static NSE.Carrinho.Api.Services.gRPC.ShoppingCartOrders;

namespace NSE.Carrinho.API.Services.gRPC
{
    [Authorize]
    public class ShoppingCartGrpcService : ShoppingCartOrdersBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<ShoppingCartGrpcService> _logger;

        public ShoppingCartGrpcService(IShoppingCartService shoppingCartService, ILogger<ShoppingCartGrpcService> logger)
        {
            _shoppingCartService = shoppingCartService;
            _logger = logger;
        }


        public override async Task<CustomerShoppingCartClientResponse> GetShoppingCart(GetShoppingCartRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Chamando GetShoppingCart no gRPC");

            var response = await _shoppingCartService.GetCustomerShoppingCart() ?? new CustomerShoppingCart();

            return MapShoppingCartClientToProtoResponse(response);
        }

        private static CustomerShoppingCartClientResponse MapShoppingCartClientToProtoResponse(CustomerShoppingCart shoppingCart)
        {
            var shoppingCartResponse = new CustomerShoppingCartClientResponse
            {
                Id = shoppingCart.Id.ToString(),
                CustomerId = shoppingCart.CustomerId.ToString(),
                Total = (double)shoppingCart.Total,
                Discount = (double)shoppingCart.Discount,
                Hasvoucher = shoppingCart.HasVoucher,
            };

            if (shoppingCart.Voucher != null)
            {
                shoppingCartResponse.Voucher = new VoucherResponse
                {
                    Code = shoppingCart.Voucher.Code,
                    Percentage = (double?)shoppingCart.Voucher.Percentage ?? 0,
                    Discount = (double?)shoppingCart.Voucher.Discount ?? 0,
                    Discounttype = (int)shoppingCart.Voucher.DiscountType
                };
            }

            foreach (var item in shoppingCart.Items)
            {
                shoppingCartResponse.Items.Add(new CartItemResponse
                {
                    Id = item.Id.ToString(),
                    Name = item.Name,
                    Image = item.Image,
                    Productid = item.ProductId.ToString(),
                    Quantity = item.Quantity,
                    Price = (double)item.Price
                });
            }

            return shoppingCartResponse;
        }
    }
}
