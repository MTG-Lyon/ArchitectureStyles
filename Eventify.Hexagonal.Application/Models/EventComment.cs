namespace Eventify.Hexagonal.Application.Models;

public record EventComment(DateTime Date, Participant Commenter, string Comment);