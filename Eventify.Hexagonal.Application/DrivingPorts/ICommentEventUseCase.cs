namespace Eventify.Hexagonal.Application.DrivingPorts;

public interface ICommentEventUseCase
{
    Task Comment(Guid eventId, string commenter, string comment);
}