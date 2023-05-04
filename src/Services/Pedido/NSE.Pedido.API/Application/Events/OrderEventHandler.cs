using Core.Messages.Integration;
using MediatR;
using NSE.MessageBus;

namespace NSE.Pedido.API.Application.Events
{
    public class OrderEventHandler : INotificationHandler<OrderDoneEvent>
    {
        private readonly IMessageBus _messageBus;

        public OrderEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(OrderDoneEvent notification, CancellationToken cancellationToken)
        {
            await _messageBus.PublishAsync<OrderDoneIntegrationEvent>(new OrderDoneIntegrationEvent(notification.CustomerId));
        }
    }
}