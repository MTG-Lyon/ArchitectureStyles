using Eventify.Hexagonal.Application.DrivenPorts;

namespace Eventify.Hexagonal.DrivenAdapters.InMemory;

public class SystemClock : IClock
{
    public DateTime Now() =>
        DateTime.UtcNow;
}