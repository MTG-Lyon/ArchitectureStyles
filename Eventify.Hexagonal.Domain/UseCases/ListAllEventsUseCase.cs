using Eventify.Hexagonal.Domain.Ports;
using Eventify.Hexagonal.Domain.Ports2;

namespace Eventify.Hexagonal.Domain.UseCases;

public class ListAllEventsUseCase(IEventRepository eventRepository) : IListAllEventsUseCase
{
    public async Task<IReadOnlyCollection<EventListItemDto>> ListAll() =>
        await eventRepository.GetAll();
}