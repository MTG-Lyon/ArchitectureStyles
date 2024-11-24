namespace Eventify.Hexagonal.Domain.DrivingPorts;

public interface IRegisterEventUseCase
{
    Task<Guid> Register(string name);
}