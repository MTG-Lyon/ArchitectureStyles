using Eventify.Hexagonal.Domain.Models;
using Eventify.Hexagonal.Domain.Ports2;
using Eventify.Hexagonal.Domain.UseCases;
using Eventify.Hexagonal.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Hexagonal.Infrastructure.Adapters;

public class SqlEventRepository(EventifyDbContext dbContext) : IEventRepository
{
    public Task Save(Event @event)
    {
        var entity = new EventEntity
        {
            Name = @event.Name.Value
        };

        dbContext.Events.Add(entity);

        return dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<EventListItemDto>> GetAll() =>
        await dbContext.Events
            .Select(x => new EventListItemDto(x.Name))
            .ToListAsync();
}