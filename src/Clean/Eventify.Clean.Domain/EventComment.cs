namespace Eventify.Clean.Domain;

public record EventComment(DateTime Date, Participant Commenter, string Comment);