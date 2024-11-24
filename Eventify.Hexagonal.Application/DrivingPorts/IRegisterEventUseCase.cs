namespace Eventify.Hexagonal.Domain.DrivingPorts;

public interface IRegisterEventUseCase
{
    Task Register(string name);
}