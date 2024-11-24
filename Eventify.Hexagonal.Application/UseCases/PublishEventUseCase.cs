using Eventify.Hexagonal.Domain.DrivenPorts;
using Eventify.Hexagonal.Domain.DrivingPorts;

namespace Eventify.Hexagonal.Domain.UseCases;

internal class PublishEventUseCase(IEventRepository eventRepository) : IPublishEventUseCase
{
    public async Task Publish(Guid eventId)
    {
        var @event = await eventRepository.Get(eventId);

        @event.Publish();

        await eventRepository.Save(@event);
    }
}