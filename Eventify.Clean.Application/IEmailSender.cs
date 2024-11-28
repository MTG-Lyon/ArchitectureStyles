namespace Eventify.Clean.Application;

public interface IEmailSender
{
    Task Send(Email email);
}

public record Email(
    string From,
    string To,
    string Subject,
    string Content
);