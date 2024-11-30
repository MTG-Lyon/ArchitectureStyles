using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.Application.DrivingPorts;
using Eventify.Hexagonal.Application.Models;
using Eventify.Hexagonal.Application.UseCases;
using Eventify.Infrastructure.Database.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Hexagonal.DrivenAdapters.Sql.Adapters;

public class SqlEventRepository(EventifyDbContext dbContext) : IEventRepository
{
    public async Task<bool> Exists(string name) =>
        await dbContext.Events.AnyAsync(x => x.Name == name);

    public async Task<Event> Get(Guid eventId)
    {
        var entity = await dbContext.Events
            .Include(x => x.Participants)
            .Include(x => x.Comments)
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
                .ToList(),
            entity.Comments
                .Select(x => new EventComment(
                    x.Date,
                    new Participant(new EmailAddress(x.Commenter)),
                    x.Comment
                ))
                .ToList()
        );
    }

    public async Task<EventDetailsDto> GetDetails(Guid eventId) =>
        await dbContext.Events
            .Where(x => x.Id == eventId)
            .Select(x => new EventDetailsDto(
                x.Comments
                    .Select(c => new CommentDto(c.Date, c.Commenter, c.Comment))
                    .OrderBy(c => c.Date)
                    .ToList()
            ))
            .SingleOrDefaultAsync()
        ?? throw new EntityNotFoundException($"The event with id {eventId} was not found");

    public async Task Save(Event @event)
    {
        var existing = await dbContext.Events
            .Include(x => x.Participants)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == @event.Id);

        if (existing is null)
        {
            existing = new EventEntity
            {
                Id = Guid.NewGuid(),
                Participants = new List<ParticipantEntity>(),
                Comments = new List<CommentEntity>()
            };
            dbContext.Events.Add(existing);
        }

        existing.Name = @event.Name.Value;
        existing.Description = @event.Description;
        existing.Status = @event.Status.ToString();

        UpdateParticipants(@event, existing);
        UpdateComments(@event, existing);

        await dbContext.SaveChangesAsync();
    }

    private static void UpdateComments(Event @event, EventEntity existing)
    {
        existing.Comments.Clear();
        
        foreach (var comment in @event.Comments)
        {
            var commentEntity = new CommentEntity
            {
                Date = comment.Date,
                Commenter = comment.Commenter.EmailAddress.Value,
                Comment = comment.Comment
            };

            existing.Comments.Add(commentEntity);
        }
    }

    private static void UpdateParticipants(Event @event, EventEntity existing)
    {
        existing.Participants.Clear();
        
        foreach (var participant in @event.Participants)
        {
            var participantEntity = new ParticipantEntity
            {
                EmailAddress = participant.EmailAddress.Value
            };

            existing.Participants.Add(participantEntity);
        }
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
}