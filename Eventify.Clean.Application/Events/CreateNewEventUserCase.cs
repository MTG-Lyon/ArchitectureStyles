using Eventify.Clean.Domain;
using Event = Eventify.Clean.Domain.Event;
using EventName = Eventify.Clean.Domain.EventName;

namespace Eventify.Clean.Application.Events;

public class CreateNewEventUserCase(IEventRepository repository)
{
    public async Task CreateNewEvent(string eventName)
    {
        var @event = new Event(new EventName(eventName));

        await repository.Save(@event);
    }
}