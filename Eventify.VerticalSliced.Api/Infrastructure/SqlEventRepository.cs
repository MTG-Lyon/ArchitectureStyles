using Eventify.Hexagonal.Infrastructure.Database;
using Eventify.VerticalSliced.Api.Domain;
using Eventify.VerticalSliced.Api.UseCases.ListExistingEvents;
using Microsoft.EntityFrameworkCore;
using IEventRepository = Eventify.VerticalSliced.Api.UseCases.CreateNewEvent.IEventRepository;

namespace Eventify.VerticalSliced.Api.Infrastructure;

public class SqlEventRepository(EventifyDbContext dbContext) : IEventRepository, UseCases.ListExistingEvents.IEventRepository
{
    public Task Save(Event @event)
    {
        var entity = new EventEntity
        {
            Id = Guid.NewGuid(),
            Name = @event.Name.Value
        };

        dbContext.Events.Add(entity);

        return dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<EventListItemDto>> GetAll() =>
        await dbContext.Events
            .Select(x => new EventListItemDto(x.Id,x.Name))
            .ToListAsync();
}