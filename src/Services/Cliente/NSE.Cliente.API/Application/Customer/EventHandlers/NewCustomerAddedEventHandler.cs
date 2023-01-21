using MediatR;
using NSE.Cliente.API.Application.Customer.Events;

namespace NSE.Cliente.API.Application.Customer.EventHandlers
{
    public class NewCustomerAddedEventHandler : INotificationHandler<NewCustomerAddedEvent>
    {
        public Task Handle(NewCustomerAddedEvent notification, CancellationToken cancellationToken)
        {
            // Send confirmation event
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*****************************************************************");
            Console.WriteLine($"The aggregate event {notification.AggregateId} was manipulated!");
            Console.WriteLine("*****************************************************************");
            Console.ForegroundColor = ConsoleColor.White;

            return Task.CompletedTask;
        }
    }
}