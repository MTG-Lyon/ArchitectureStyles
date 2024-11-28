using Eventify.Clean.Domain;
using Eventify.Clean.Domain.Exceptions;

namespace Eventify.Clean.Application.Events.Administration;

public class RegisterEventUseCase(IEventRepository eventRepository)
{
    public async Task<Guid> Register(string name)
    {
        if (await eventRepository.Exists(name))
        {
            throw new EventWithSameNameAlreadyExistsException("The event name is already taken");
        }
        
        var @event = Event.Register(new EventName(name));

        await eventRepository.Save(@event);
        
        return @event.Id;
    }
}