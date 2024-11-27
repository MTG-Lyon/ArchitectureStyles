namespace Eventify.Hexagonal.Application.DrivingPorts;

public interface IRegisterEventUseCase
{
    Task<Guid> Register(string name);
}