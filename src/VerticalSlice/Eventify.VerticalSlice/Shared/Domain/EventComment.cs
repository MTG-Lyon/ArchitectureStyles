namespace Eventify.VerticalSlice.Shared.Domain;

public record EventComment(DateTime Date, Participant Commenter, string Comment);