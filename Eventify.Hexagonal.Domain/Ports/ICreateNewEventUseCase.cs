namespace Eventify.Hexagonal.Domain.Ports;

public interface ICreateNewEventUseCase
{
    Task Execute(string name);
}