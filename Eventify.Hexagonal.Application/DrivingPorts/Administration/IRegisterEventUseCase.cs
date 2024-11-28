namespace Eventify.Hexagonal.Application.DrivingPorts.Administration;

public interface IRegisterEventUseCase
{
    Task<Guid> Register(string name);
}