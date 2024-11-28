using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.Application.DrivingPorts.Participation;
using Eventify.Hexagonal.Application.Models;

namespace Eventify.Hexagonal.Application.UseCases.Participation;

internal class JoinEventUseCase(IEventRepository eventRepository) 
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