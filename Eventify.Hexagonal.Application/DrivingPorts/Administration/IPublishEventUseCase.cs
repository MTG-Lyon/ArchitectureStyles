namespace Eventify.Hexagonal.Application.DrivingPorts.Administration;

public interface IPublishEventUseCase
{
    Task Publish(Guid eventId);
}