using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.Application.DrivingPorts.Administration;

namespace Eventify.Hexagonal.Application.UseCases.Administration;

internal class PublishEventUseCase(IEventRepository eventRepository) : IPublishEventUseCase
{
    public async Task Publish(Guid eventId)
    {
        var @event = await eventRepository.Get(eventId);

        @event.Publish();

        await eventRepository.Save(@event);
    }
}