using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.Application.DrivingPorts;

namespace Eventify.Hexagonal.Application.UseCases;

internal class PublishEventUseCase(IEventRepository eventRepository) : IPublishEventUseCase
{
    public async Task Publish(Guid eventId)
    {
        var @event = await eventRepository.Get(eventId);

        @event.Publish();

        await eventRepository.Save(@event);
    }
}