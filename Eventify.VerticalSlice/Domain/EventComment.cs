namespace Eventify.VerticalSlice.Domain;

public record EventComment(DateTime Date, Participant Commenter, string Comment);