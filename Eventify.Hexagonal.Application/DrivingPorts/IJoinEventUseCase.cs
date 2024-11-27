namespace Eventify.Hexagonal.Application.DrivingPorts;

public interface IJoinEventUseCase
{
    Task Join(Guid eventId, string emailAddress);
}