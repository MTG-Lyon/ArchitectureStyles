namespace Eventify.Hexagonal.Domain.DrivingPorts;

public interface IDescribeEventUseCase
{
    Task Describe(Guid eventId, string description);
}