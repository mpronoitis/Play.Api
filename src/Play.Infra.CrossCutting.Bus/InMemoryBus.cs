using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using Play.Domain.EventSourcing.Events;

namespace Play.Infra.CrossCutting.Bus;

public sealed class InMemoryBus : IMediatorHandler
{
    private readonly IMediator _mediator;
    private readonly IEventStore _eventStore;

    public InMemoryBus(IMediator mediator,IEventStore eventStore)
    {
        _mediator = mediator;
        _eventStore = eventStore;
    }
    

    /// <summary>
    ///     Send an event to the bus to be processed
    /// </summary>
    /// <param name="event">The event obj <see cref="Event" /></param>
    /// <typeparam name="T">The type of the event </typeparam>
    public async Task PublishEvent<T>(T @event) where T : Event
    {
        if (!@event.MessageType.Equals("DomainNotification"))
            
            // Save the event in the event store (do not save event to db)
             _eventStore.Save(@event);

            await _mediator.Publish(@event);
    }

    /// <summary>
    ///     Send a command to the bus to be processed
    /// </summary>
    /// <param name="command">The command obj <see cref="Command" /></param>
    /// <typeparam name="T">The type of the command</typeparam>
    /// <returns></returns>
    public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }
}