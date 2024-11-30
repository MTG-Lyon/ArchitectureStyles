namespace Eventify.Hexagonal.Application.DrivingPorts.Administration;

public interface IDescribeEventUseCase
{
    Task Describe(Guid eventId, string description);
}