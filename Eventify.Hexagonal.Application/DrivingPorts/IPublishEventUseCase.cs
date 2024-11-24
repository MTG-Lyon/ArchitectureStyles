namespace Eventify.Hexagonal.Domain.DrivingPorts;

public interface IPublishEventUseCase
{
    Task Publish(Guid eventId);
}