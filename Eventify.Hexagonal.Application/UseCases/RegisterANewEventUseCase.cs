using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.Application.DrivingPorts;
using Eventify.Hexagonal.Application.Models;
using Eventify.Hexagonal.Application.Models.Exceptions;

namespace Eventify.Hexagonal.Application.UseCases;

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