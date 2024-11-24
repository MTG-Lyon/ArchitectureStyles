using Eventify.Hexagonal.Domain.UseCases;

namespace Eventify.Hexagonal.Domain.DrivingPorts;

public interface IListAllEventsUseCase
{
    Task<IReadOnlyCollection<EventListItemDto>> ListAll();
}