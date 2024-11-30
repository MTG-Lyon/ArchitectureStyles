namespace Eventify.Hexagonal.Application.DrivingPorts;

public interface IGetEventDetailsUseCase
{
    Task<EventDetailsDto> GetEventDetails(Guid eventId);
}

public record EventDetailsDto(IReadOnlyCollection<CommentDto> Comments);

public record CommentDto(DateTime Date, string Commenter, string Comment);