using Eventify.Hexagonal.Domain.Models;
using Eventify.Hexagonal.Domain.Ports;
using Eventify.Hexagonal.Domain.Ports2;

namespace Eventify.Hexagonal.Domain.UseCases;

internal class CreateNewEventUseCase(
    IEventRepository eventRepository
) : ICreateNewEventUseCase
{
    public async Task Execute(string name)
    {
        if(await eventRepository.Exists(name))
        {
            throw new EventWithSameNameAlreadyExistsException("The event name is already taken");
        }
        
        var @event = new Event(new EventName(name));

        await eventRepository.Save(@event);
    }
}

public class EventWithSameNameAlreadyExistsException(string message) : Exception(message);