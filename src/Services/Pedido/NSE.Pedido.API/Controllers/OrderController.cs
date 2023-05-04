using Core.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Pedido.API.Application.Commands;
using NSE.Pedido.API.Application.Queries;
using NSE.WebApi.Core.Controllers;
using NSE.WebApi.Core.User;

namespace NSE.Pedido.API.Controllers
{
    [Authorize, Route("orders")]
    public class OrderController : MainController
    {

        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;
        private readonly IOrderQueries _orderQueries;

        public OrderController(IMediatorHandler mediator, IAspNetUser user, IOrderQueries orderQueries)
        {
            _mediator = mediator;
            _user = user;
            _orderQueries = orderQueries;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderCommand order)
        {
            var result = await _mediator.SendCommand(order);

            return CustomResponse(result);
        }

        [HttpGet("last")]
        public async Task<IActionResult> LastOrder()
        {
            var customerId = _user.GetUserId();
            var order = await _orderQueries.GetLastOrder(customerId);

            return order is null ? NoContent() : CustomResponse(order);
        }

        [HttpGet("customerr")]
        public async Task<IActionResult> Customers()
        {
            var customerId = _user.GetUserId();
            var orders = await _orderQueries.GetOrdersBy(customerId);

            return orders is null ? NoContent() : CustomResponse(orders);
        }
    }
}