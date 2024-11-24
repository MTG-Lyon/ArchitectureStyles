using Eventify.Hexagonal.Domain.DrivenPorts;
using Eventify.Hexagonal.Domain.Models;
using Eventify.Hexagonal.Domain.UseCases;
using Eventify.Infrastructure.Database.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Hexagonal.Infrastructure.Adapters;

public class SqlEventRepository(EventifyDbContext dbContext) : IEventRepository
{
    public async Task<Event> Get(Guid eventId)
    {
        var entity = await dbContext.Events
            .Include(x => x.Participants)
            .SingleOrDefaultAsync(x => x.Id == eventId);
        
        if (entity is null)
        {
            throw new EntityNotFoundException($"The event with id {eventId} was not found");
        }
        
        return Event.Rehydrate(
            entity.Id,
            new EventName(entity.Name),
            entity.Description ?? string.Empty,
            Enum.Parse<EventStatus>(entity.Status),
            entity.Participants
                .Select(x => new Participant(new EmailAddress(x.EmailAddress)))
                .ToList()
        );
    }

    public async Task Save(Event @event)
    {
        var existing = await dbContext.Events.FirstOrDefaultAsync(x => x.Id == @event.Id);

        if (existing is null)
        {
            existing = new EventEntity
            {
                Id = Guid.NewGuid(),
                Participants = new List<ParticipantEntity>()
            };
            dbContext.Events.Add(existing);
        }
        
        existing.Name = @event.Name.Value;
        existing.Description = @event.Description;
        existing.Status = @event.Status.ToString();
        existing.Participants.Clear();
        
        foreach (var participant in @event.Participants)
        {
            var participantEntity = new ParticipantEntity
            {
                EmailAddress = participant.EmailAddress.Value
            };
            
            existing.Participants.Add(participantEntity);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<EventListItemDto>> GetAll() =>
        await dbContext.Events
            .Select(x => new EventListItemDto(
                x.Id,
                x.Name,
                x.Description ?? string.Empty,
                Enum.Parse<EventStatus>(x.Status),
                x.Participants
                    .Select(y => new ParticipantDto(y.EmailAddress))
                    .ToList()
            ))
            .ToListAsync();

    public async Task<bool> Exists(string name) =>
        await dbContext.Events.AnyAsync(x => x.Name == name);
}