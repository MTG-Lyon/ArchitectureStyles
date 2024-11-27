using Eventify.Hexagonal.Application.DrivenPorts;

namespace Eventify.Hexagonal.DrivenAdapters.InMemory;

internal class InMemoryEmailSender : IEmailSender
{
    public Task Send(Email email) =>
        Task.CompletedTask;
}