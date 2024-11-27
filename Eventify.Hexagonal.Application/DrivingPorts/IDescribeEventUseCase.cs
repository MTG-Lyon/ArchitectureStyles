namespace Eventify.Hexagonal.Application.DrivingPorts;

public interface IDescribeEventUseCase
{
    Task Describe(Guid eventId, string description);
}