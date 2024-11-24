using Eventify.Hexagonal.Domain.DrivingPorts;
using Eventify.Hexagonal.Domain.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Hexagonal.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegisterEventUseCase, RegisterANewEventUseCase>();
        services.AddScoped<IListAllEventsUseCase, ListAllEventsUseCase>();
        services.AddScoped<IDescribeEventUseCase, DescribeEventUseCase>();
        services.AddScoped<IPublishEventUseCase, PublishEventUseCase>();

        return services;
    }
}