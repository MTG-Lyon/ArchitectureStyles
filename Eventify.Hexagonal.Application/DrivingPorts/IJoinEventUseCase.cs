namespace Eventify.Hexagonal.Domain.DrivingPorts;

public interface IJoinEventUseCase
{
    Task Join(Guid eventId, string emailAddress);
}