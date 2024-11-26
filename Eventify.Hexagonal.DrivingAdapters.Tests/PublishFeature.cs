using Eventify.Hexagonal.Application;
using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.Application.DrivingPorts;
using Eventify.Hexagonal.Application.Models.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Hexagonal.DrivingAdapters.Tests;

public class PublishFeature
{
    private readonly IRegisterEventUseCase _registerEvent;
    private readonly IPublishEventUseCase _publishEvent;

    public PublishFeature()
    {
        var provider = new ServiceCollection()
            .RegisterApplication()
            .AddSingleton<IEventRepository, InMemoryEventRepository>()
            .BuildServiceProvider();

        _registerEvent = provider.GetRequiredService<IRegisterEventUseCase>();
        _publishEvent = provider.GetRequiredService<IPublishEventUseCase>();
    }
    
    [Fact]
    public async Task Cannot_publish_event_if_already_published()
    {
        var eventId = await _registerEvent.Register("Test");
        
        await _publishEvent.Publish(eventId);
        
        var action = () => _publishEvent.Publish(eventId);
        
        await action.Should().ThrowAsync<EventAlreadyPublishedException>();
    }
}