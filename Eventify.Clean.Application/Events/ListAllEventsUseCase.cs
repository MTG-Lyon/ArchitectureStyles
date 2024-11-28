using Eventify.Clean.Domain;

namespace Eventify.Clean.Application.Events;

public class ListAllEventsUseCase(IEventRepository repository)
{
    public async Task<IReadOnlyCollection<EventListItemDto>> ListAll() =>
        await repository.GetAll();
}