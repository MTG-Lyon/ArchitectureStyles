using Eventify.Clean.Domain;

namespace Eventify.Clean.Application.Events.Participation;

public class JoinEventUseCase(IEventRepository eventRepository)
{
    public async Task Join(Guid eventId, string emailAddress)
    {
        var @event = await eventRepository.Get(eventId);

        var participant = new Participant(new EmailAddress(emailAddress));
        
        @event.Join(participant);

        await eventRepository.Save(@event);
    }
}