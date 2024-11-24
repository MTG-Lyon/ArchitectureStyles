using Eventify.Hexagonal.Domain;
using Eventify.Hexagonal.Domain.DrivingPorts;
using Eventify.Hexagonal.Domain.UseCases;
using Eventify.Hexagonal.DrivenAdapters.InMemory;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services
    .RegisterApplication()
    .RegisterInMemoryDatabase();

var serviceProvider = services.BuildServiceProvider();

var createUseCase = serviceProvider.GetRequiredService<IRegisterEventUseCase>();
var listUseCase = serviceProvider.GetRequiredService<IListAllEventsUseCase>();

while (true)
{
    Console.WriteLine();
    Console.Write("Enter the name of the event > ");
    
    var name = Console.ReadLine();
    
    if (string.IsNullOrWhiteSpace(name))
    {
        break;
    }

    try
    {
        await createUseCase.Register(name);
    }
    catch (EventWithSameNameAlreadyExistsException)
    {
        Console.WriteLine("The event name is already taken, please choose another name.");
        continue;
    }
    
    var events = await listUseCase.ListAll();
    
    Console.WriteLine("All events are: ");
    
    foreach (var @event in events)
    {
        Console.WriteLine("- " + @event.Name);
    }
}