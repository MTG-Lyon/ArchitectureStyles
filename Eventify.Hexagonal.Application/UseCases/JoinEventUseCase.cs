using Eventify.Hexagonal.Domain.DrivenPorts;
using Eventify.Hexagonal.Domain.DrivingPorts;
using Eventify.Hexagonal.Domain.Models;

namespace Eventify.Hexagonal.Domain.UseCases;

public class JoinEventUseCase(IEventRepository eventRepository) 
    : IJoinEventUseCase
{
    public async Task Join(Guid eventId, string emailAddress)
    {
        var @event = await eventRepository.Get(eventId);

        var participant = new Participant(new EmailAddress(emailAddress));
        
        @event.Join(participant);

        await eventRepository.Save(@event);
    }
}