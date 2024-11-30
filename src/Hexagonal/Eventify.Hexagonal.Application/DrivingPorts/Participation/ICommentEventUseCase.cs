namespace Eventify.Hexagonal.Application.DrivingPorts.Participation;

public interface ICommentEventUseCase
{
    Task Comment(Guid eventId, string commenter, string comment);
}