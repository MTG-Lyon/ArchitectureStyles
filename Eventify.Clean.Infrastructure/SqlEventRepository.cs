using Eventify.Clean.Entities;
using Eventify.Infrastructure.Database.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Clean.Infrastructure;

public class SqlEventRepository(EventifyDbContext dbContext) : IEventRepository
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

    public async Task<bool> Exists(string eventName) =>
        await dbContext.Events.AnyAsync(x => x.Name == eventName);
}