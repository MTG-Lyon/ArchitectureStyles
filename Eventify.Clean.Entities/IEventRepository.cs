namespace Eventify.Clean.Entities;

public interface IEventRepository
{
    Task Save(Event @event);
    Task<IReadOnlyCollection<EventListItemDto>> GetAll();
    Task<bool> Exists(string eventName);
}