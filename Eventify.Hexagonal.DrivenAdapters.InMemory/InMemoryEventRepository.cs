using Eventify.Hexagonal.Domain.DrivenPorts;
using Eventify.Hexagonal.Domain.Models;
using Eventify.Hexagonal.Domain.UseCases;

namespace Eventify.Hexagonal.DrivenAdapters.InMemory;

internal class InMemoryEventRepository : IEventRepository
{
    private readonly Dictionary<Guid,Event> _events = new();

    public Task<Event> Get(Guid eventId) =>
        Task.FromResult(_events[eventId]);

    public Task Save(Event @event)
    {
        _events[@event.Id] = @event;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<EventListItemDto>> GetAll()
    {
        var items = _events.Values
            .Select(x => 
                new EventListItemDto(
                    x.Id,
                    x.Name.Value,
                    x.Description,
                    x.Status,
                    x.Participants
                        .Select(y => new ParticipantDto(y.EmailAddress.Value))
                        .ToList()
                )
            )
            .ToList();
        
        return Task.FromResult<IReadOnlyCollection<EventListItemDto>>(items);
    }

    public Task<bool> Exists(string name)
    {
        var exists = _events.Values.Any(x => x.Name.Value == name);
        return Task.FromResult(exists);
    }
}