namespace Eventify.Hexagonal.Application.DrivingPorts;

public interface IPublishEventUseCase
{
    Task Publish(Guid eventId);
}