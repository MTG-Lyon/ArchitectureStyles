namespace Eventify.Hexagonal.Application.DrivenPorts;

public interface IClock
{
    public DateTime Now();
}