using Eventify.Hexagonal.Domain.DrivenPorts;
using Eventify.Hexagonal.Domain.DrivingPorts;

namespace Eventify.Hexagonal.Domain.UseCases;

internal class DescribeEventUseCase(IEventRepository eventRepository)
    : IDescribeEventUseCase
{
    public async Task Describe(Guid eventId, string description)
    {
        var @event = await eventRepository.Get(eventId);

        @event.Describe(description);

        await eventRepository.Save(@event);
    }
}