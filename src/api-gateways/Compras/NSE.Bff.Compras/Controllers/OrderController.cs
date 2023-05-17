

using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Bff.Compras.DTOs;
using NSE.Bff.Compras.Services.Interfaces;
using NSE.WebApi.Core.Controllers;

namespace NSE.Bff.Compras.Controllers
{
    [Authorize, Route("compras/pedido")]
    public class OrderController : MainController
    {
        private readonly ICatalogService _catalogService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(ICatalogService catalogService, IShoppingCartService shoppingCartService, IOrderService orderService, ICustomerService customerService)
        {
            _catalogService = catalogService;
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
            _customerService = customerService;
        }


        [HttpPost("adicionar")]
        public async Task<IActionResult> AddOrder(OrderDTO order)
        {
            var cart = await _shoppingCartService.GetShoppingCart();
            var cartProductsIds = GetListProductIdsInCart(cart);
            var productsActive = await _catalogService.GetProducts(cartProductsIds);
            var address = await _customerService.GetAddress();

            if (!await CheckShoppingCartProductsIsValid(cart, productsActive)) return CustomResponse();

            PopulateOrderData(cart, address, order);

            return CustomResponse(await _orderService.FinishOrder(order));
        }


        [HttpGet("ultimo")]
        public async Task<IActionResult> LastOrder()
        {
            var order = await _orderService.GetLastOrder();
            if (order is null)
            {
                AddErrorsProcessing("Pedido não encontrado!");
                return CustomResponse();
            }

            return CustomResponse(order);
        }

        [HttpGet("cliente")]
        public async Task<IActionResult> CustomerList()
        {
            var orders = await _orderService.GetCustomers();

            return orders == null ? NotFound() : CustomResponse(orders);
        }

        private async Task<bool> CheckShoppingCartProductsIsValid(ShoppingCartDTO cart, IEnumerable<ProductDTO> productsActive)
        {
            var cartProductsIds = GetListProductIdsInCart(cart);
            var productIdsActive = GetListProductIds(productsActive);

            var cartIdsHasTheSameSizeOfProductsIdsActive = AreListsOfDifferentLengths(cartProductsIds, productIdsActive);

            if (cartIdsHasTheSameSizeOfProductsIdsActive)
            {
                BuildInactiveProductsErrorMessage(cart, productsActive);
                return false;
            }

            return await ValidateShoppingCartProducts(cart, productsActive);

        }
        private IEnumerable<Guid> GetListProductIdsInCart(ShoppingCartDTO cart)
        {
            return cart.Items.Select(item => item.ProductId);
        }

        private IEnumerable<Guid> GetListProductIds(IEnumerable<ProductDTO> products)
        {
            return products.Select(product => product.Id);
        }

        private bool AreListsOfDifferentLengths<ListA, ListB>(IEnumerable<ListA> listA, IEnumerable<ListB> listB)
        {
            return listA.Count() != listB.Count();
        }

        private void BuildInactiveProductsErrorMessage(ShoppingCartDTO cart, IEnumerable<ProductDTO> productsActive)
        {
            var cartProductsIds = GetListProductIdsInCart(cart);
            var productIdsActive = GetListProductIds(productsActive);

            var itemsIdsNotActive = GetProductsNotActiveInCart(cartProductsIds, productIdsActive);

            foreach (var itemId in itemsIdsNotActive)
            {
                var cartItem = cart.Items.FirstOrDefault(item => item.ProductId == itemId);
                AddErrorsProcessing($"O item {cartItem.Name} não está mais disponível no cátalogo, o remova do carrinho para prosseguir com a compra");
            }
        }

        private IEnumerable<Guid> GetProductsNotActiveInCart(IEnumerable<Guid> cartProductsIds, IEnumerable<Guid> productIdsActive)
        {
            return cartProductsIds.Except(productIdsActive);
        }
        private async Task<bool> ValidateShoppingCartProducts(ShoppingCartDTO cart, IEnumerable<ProductDTO> producstActive)
        {
            foreach (var item in cart.Items)
            {
                var product = GetProductBy(item, producstActive);
                var isPriceEqual = CheckCartItemPriceAgainstCatalog(item, product);

                if (!isPriceEqual)
                {
                    var messgaeError = SetMessageErrorNotEqualPrice(item, product);
                    var isRemoveItem = await RemoveItemFromShoppingCart(item);

                    if (!isRemoveItem)
                    {
                        return false;
                    }
                    var isAddedItemInCart = await AddItemToShoppingCart(item, product);

                    if (!isAddedItemInCart)
                    {
                        return false;
                    }

                    ClearErrors();
                    AddErrorsProcessing(messgaeError + "Atualizamos o valor em seu carrinho, realize a conferência do pedido e se preferir remova o produto");

                    return false;
                }
            }

            return true;
        }

        private ProductDTO GetProductBy(CartItemDTO cartItem, IEnumerable<ProductDTO> productsActive)
        {
            return productsActive.FirstOrDefault(product => product.Id == cartItem.ProductId);
        }
        private bool CheckCartItemPriceAgainstCatalog(CartItemDTO item, ProductDTO productsActive)
        {
            return item.Price == productsActive.Price;
        }

        private string SetMessageErrorNotEqualPrice(CartItemDTO item, ProductDTO product)
        {
            var msgErro = $"O produto {item.Name} mudou de valor (de: " +
                                $"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", item.Price)} para: " +
                                $"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", product.Price)}) desde que foi adicionado ao carrinho.";

            AddErrorsProcessing(msgErro);

            return msgErro;
        }

        private async Task<bool> RemoveItemFromShoppingCart(CartItemDTO cartItem)
        {
            var response = await _shoppingCartService.RemoveCartItem(cartItem.ProductId);

            var hasErrors = ResponseHasErrors(response);

            if (hasErrors)
            {
                SetMessageErrorRemoveItemInCart(cartItem);

                return false;
            }

            return true;
        }


        private void SetMessageErrorRemoveItemInCart(CartItemDTO item)
        {
            AddErrorsProcessing($"Não foi possível remover automaticamente o produto {item.Name} do seu carrinho, _" +
                                                    "remova e adicione novamente caso ainda deseje comprar este item");
        }


        private async Task<bool> AddItemToShoppingCart(CartItemDTO cartItem, ProductDTO product)
        {
            cartItem.Price = product.Price;

            var response = await _shoppingCartService.AddCartItem(cartItem);

            var hasErrors = ResponseHasErrors(response);

            if (hasErrors)
            {
                SetMessageErrorAddItemInCart(cartItem);

                return false;
            }

            return true;
        }

        private void SetMessageErrorAddItemInCart(CartItemDTO item)
        {
            AddErrorsProcessing($"Não foi possível atualizar automaticamente o produto {item.Name} do seu carrinho, _" +
                                                   "adicione novamente caso ainda deseje comprar este item");
        }

        private void PopulateOrderData(ShoppingCartDTO shoppingCart, AddressDTO address, OrderDTO order)
        {
            order.VoucherCode = shoppingCart.Voucher?.Code;
            order.HasVoucher = shoppingCart.HasVoucher;
            order.Amount = shoppingCart.Total;
            order.Discount = shoppingCart.Discount;
            order.OrderItems = shoppingCart.Items;

            order.Address = address;
        }
    }

}