using Eventify.Hexagonal.Application.UseCases;

namespace Eventify.Hexagonal.Application.DrivingPorts;

public interface IListAllEventsUseCase
{
    Task<IReadOnlyCollection<EventListItemDto>> ListAll();
}