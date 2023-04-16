using Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace NSE.Pedido.API.Application.Commands
{
    public class OrderCommandHandler : CommandHandler, IRequestHandler<AddOrderCommand, ValidationResult>
    {
        public Task<ValidationResult> Handle(AddOrderCommand message, CancellationToken cancellationToken)
        {

             // command validation
           

            // Map Order
           

            // apply voucher, if exists
          

            // Validate order
          

            // pay the order
          

            // If paid, authorize order!
          

            // Adding event
          

            // Add Order Repositorio
             // command validation
          

            // Map Order
          

            // apply voucher, if exists
            

            // Validate order
            

            // pay the order
            

            // If paid, authorize order!
            

            // Adding event
            

            // Add Order Repositorio
            

            // Commiting order and voucher data
            

            // Commiting order and voucher data
            
            throw new NotImplementedException();
        }
    }
}