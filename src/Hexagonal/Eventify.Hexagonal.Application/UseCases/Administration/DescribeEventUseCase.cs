using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.Application.DrivingPorts.Administration;

namespace Eventify.Hexagonal.Application.UseCases.Administration;

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