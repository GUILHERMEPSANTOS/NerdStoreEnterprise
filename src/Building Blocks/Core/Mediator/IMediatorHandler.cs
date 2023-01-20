using Core.Messages;
using FluentValidation.Results;

namespace Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<TEvent>(TEvent evento) where TEvent : Event;

        Task<ValidationResult> SendCommand<TCommand>(TCommand command) where TCommand : Command; 
    }
}