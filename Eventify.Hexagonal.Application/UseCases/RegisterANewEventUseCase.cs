using Eventify.Hexagonal.Domain.DrivenPorts;
using Eventify.Hexagonal.Domain.DrivingPorts;
using Eventify.Hexagonal.Domain.Models;

namespace Eventify.Hexagonal.Domain.UseCases;

internal class RegisterANewEventUseCase(
    IEventRepository eventRepository
) : IRegisterEventUseCase
{
    public async Task<Guid> Register(string name)
    {
        if(await eventRepository.Exists(name))
        {
            throw new EventWithSameNameAlreadyExistsException("The event name is already taken");
        }
        
        var @event = Event.Register(new EventName(name));

        await eventRepository.Save(@event);
        
        return @event.Id;
    }
}

public class EventWithSameNameAlreadyExistsException(string message) : Exception(message);