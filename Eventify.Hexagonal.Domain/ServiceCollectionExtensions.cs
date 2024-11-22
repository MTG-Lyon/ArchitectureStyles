using Eventify.Hexagonal.Domain.Ports;
using Eventify.Hexagonal.Domain.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Hexagonal.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDomain(this IServiceCollection services)
    {
        services.AddScoped<ICreateNewEventUseCase, CreateNewEventUseCase>();
        services.AddScoped<IListAllEventsUseCase, ListAllEventsUseCase>();

        return services;
    }
}