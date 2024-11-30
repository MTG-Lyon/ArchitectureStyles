using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.Application.DrivingPorts.Participation;
using Eventify.Hexagonal.Application.Models;

namespace Eventify.Hexagonal.Application.UseCases.Participation;

internal class CommentEventUseCase(IEventRepository repository, IClock clock, IEmailSender emailSender) 
    : ICommentEventUseCase
{
    public async Task Comment(Guid eventId, string commenter, string comment)
    {
        var @event = await repository.Get(eventId);

        var eventComment = new EventComment(
            clock.Now(), 
            new Participant(new EmailAddress(commenter)), 
            comment
        );
        
        @event.AddComment(eventComment);

        await repository.Save(@event);
        
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