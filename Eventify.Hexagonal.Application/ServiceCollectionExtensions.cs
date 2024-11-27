using Eventify.Hexagonal.Application.DrivingPorts;
using Eventify.Hexagonal.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Hexagonal.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddTransient<IRegisterEventUseCase, RegisterANewEventUseCase>();
        services.AddTransient<IListAllEventsUseCase, ListAllEventsUseCase>();
        services.AddTransient<IDescribeEventUseCase, DescribeEventUseCase>();
        services.AddTransient<IPublishEventUseCase, PublishEventUseCase>();
        services.AddTransient<IJoinEventUseCase, JoinEventUseCase>();

        return services;
    }
}