using Eventify.Clean.Domain;
using Event = Eventify.Clean.Domain.Event;
using EventName = Eventify.Clean.Domain.EventName;

namespace Eventify.Clean.Application.Events;

public class CreateNewEventUserCase(IEventRepository repository)
{
    public async Task CreateNewEvent(string eventName)
    {
        if (await repository.Exists(eventName))
        {
            throw new EventWithSameNameAlreadyExistsException("The event name is already taken");
        }
        
        var @event = new Event(new EventName(eventName));

        await repository.Save(@event);
    }
}

public class EventWithSameNameAlreadyExistsException(string message) : Exception(message);