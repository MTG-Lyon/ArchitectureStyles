using Eventify.Hexagonal.Domain.DrivenPorts;
using Eventify.Hexagonal.Domain.DrivingPorts;
using Eventify.Hexagonal.Domain.Models;

namespace Eventify.Hexagonal.Domain.UseCases;

public class ListAllEventsUseCase(IEventRepository eventRepository) : IListAllEventsUseCase
{
    public async Task<IReadOnlyCollection<EventListItemDto>> ListAll() =>
        await eventRepository.GetAll();
}

public record EventListItemDto(
    Guid Id,
    string Name,
    string Description,
    EventStatus Status
);