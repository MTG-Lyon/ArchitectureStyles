using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.Application.DrivingPorts;
using Eventify.Hexagonal.Application.Models;
using Eventify.Hexagonal.Application.UseCases;

namespace Eventify.Hexagonal.DrivingAdapters.Tests;

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

    public Task<EventDetailsDto> GetDetails(Guid eventId)
    {
        var result = _events.Values
            .Where(x => x.Id == eventId)
            .Select(x => new EventDetailsDto(
                x.Comments
                    .Select(c => new CommentDto(c.Date, c.Commenter.EmailAddress.Value, c.Comment))
                    .OrderBy(c => c.Date)
                    .ToList()
            ))
            .Single();
        
        return Task.FromResult(result);
    }
}