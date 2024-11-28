using Eventify.Hexagonal.Application;
using Eventify.Hexagonal.Application.DrivingPorts;
using Eventify.Hexagonal.Application.DrivingPorts.Administration;
using Eventify.Hexagonal.Application.Models.Exceptions.Base;
using Eventify.Hexagonal.DrivenAdapters.InMemory;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

var provider = services
    .RegisterApplication()
    .RegisterInMemoryDatabase()
    .BuildServiceProvider();

var createUseCase = provider.GetRequiredService<IRegisterEventUseCase>();
var listUseCase = provider.GetRequiredService<IListAllEventsUseCase>();

while (true)
{
    try
    {
        Console.WriteLine();
        Console.Write("Enter your command > ");

        var command = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(command))
        {
            break;
        }
        
        var parameters = ParseCommand(command);

        var action = parameters.ActionName switch
        {
            "create" => createUseCase.Register(parameters.Value),
            _ => Task.CompletedTask
        };

        await action;

        var events = await listUseCase.ListAll();

        Console.WriteLine("All events are: ");

        foreach (var @event in events)
        {
            Console.WriteLine("- " + @event.Name);
        }
    }
    catch (Exception e) when (e is IDomainException)
    {
        Console.WriteLine(e.Message);
    }
}

(string ActionName, string Value) ParseCommand(string command)
{
    var parts = command.Split(" ");
    var actionName = parts.First();
    var value = string.Join(" ", parts.Skip(1));
    return (actionName, value);
}