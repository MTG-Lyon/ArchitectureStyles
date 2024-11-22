using Eventify.Hexagonal.Domain.UseCases;

namespace Eventify.Hexagonal.Domain.Ports;

public interface IListAllEventsUseCase
{
    Task<IReadOnlyCollection<EventListItemDto>> ListAll();
}