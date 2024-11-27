using Eventify.Infrastructure.Database.Database;
using Eventify.VerticalSlice.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventify.VerticalSlice.Shared.Infrastructure;

public class SqlEventRepository(EventifyDbContext dbContext) : 
    UseCases.CreateNewEvent.IEventRepository,
    UseCases.DescribeEvent.IEventRepository, 
    UseCases.JoinEvent.IEventRepository, 
    UseCases.PublishEvent.IEventRepository, 
    UseCases.CommentEvent.IEventRepository
{
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
        existing.Participants.Clear();
        
        foreach (var participant in @event.Participants)
        {
            var participantEntity = new ParticipantEntity
            {
                EmailAddress = participant.EmailAddress.Value
            };
            
            existing.Participants.Add(participantEntity);
        }
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

        await dbContext.SaveChangesAsync();
    }
}