using Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<TEvent>(TEvent evento) where TEvent : Event
        {
            await _mediator.Publish(evento);
        }

        public async Task<ValidationResult> SendCommand<TCommand>(TCommand command) where TCommand : Command
        {
            return await _mediator.Send(command);
        }
    }
}