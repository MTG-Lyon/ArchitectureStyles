using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.Application.DrivingPorts;
using Eventify.Hexagonal.Application.Models;

namespace Eventify.Hexagonal.Application.UseCases;

internal class ListAllEventsUseCase(IEventRepository eventRepository) : IListAllEventsUseCase
{
    public async Task<IReadOnlyCollection<EventListItemDto>> ListAll() =>
        await eventRepository.GetAll();
}

public record EventListItemDto(
    Guid Id,
    string Name,
    string Description,
    EventStatus Status,
    IReadOnlyCollection<ParticipantDto> Participants
);

public record ParticipantDto(string EmailAddress);
