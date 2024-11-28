namespace Eventify.Hexagonal.Application.DrivingPorts.Participation;

public interface IJoinEventUseCase
{
    Task Join(Guid eventId, string emailAddress);
}