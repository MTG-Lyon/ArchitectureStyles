using Eventify.VerticalSlice.Domain;

namespace Eventify.VerticalSlice.UseCases.CommentEvent;

public class UseCase(IEventRepository eventRepository, IClock clock, IEmailSender emailSender)
{
    public async Task CommentEvent(Guid eventId, string commenter, string comment)
    {
        var @event = await eventRepository.Get(eventId);

        var eventComment = new EventComment(
            clock.Now(), 
            new Participant(new EmailAddress(commenter)), 
            comment
        );
        
        @event.AddComment(eventComment);

        await eventRepository.Save(@event);
        
        await SendEmails(commenter, @event, eventComment);
    }

    private async Task SendEmails(string commenter, Event @event, EventComment eventComment)
    {
        var emailsToSend = @event.Participants
            .Where(x => x != eventComment.Commenter)
            .Select(p => new Email(
                "no-reply@eventify.com",
                p.EmailAddress.Value,
                "Eventify - New comment",
                $"{commenter} has commented on the event \"{@event.Name.Value}\""
            ))
            .ToList();

        foreach (var email in emailsToSend)
        {
            await emailSender.Send(email);
        }
    }
}