using Eventify.Hexagonal.Application;
using Eventify.Hexagonal.Application.DrivenPorts;
using Eventify.Hexagonal.Application.DrivingPorts;
using Eventify.Hexagonal.Application.DrivingPorts.Administration;
using Eventify.Hexagonal.Application.Models;
using Eventify.Hexagonal.Application.Models.Exceptions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Hexagonal.DrivingAdapters.Tests;

public class PublishFeature
{
    private readonly IRegisterEventUseCase _registerEvent;
    private readonly IPublishEventUseCase _publishEvent;
    private readonly IEventRepository _repository;
    private readonly IListAllEventsUseCase _listEvents;

    public PublishFeature()
    {
        var provider = new ServiceCollection()
            .RegisterApplication()
            .AddSingleton<IEventRepository, InMemoryEventRepository>()
            .BuildServiceProvider();

        _registerEvent = provider.GetRequiredService<IRegisterEventUseCase>();
        _publishEvent = provider.GetRequiredService<IPublishEventUseCase>();
        _listEvents = provider.GetRequiredService<IListAllEventsUseCase>();
        _repository = provider.GetRequiredService<IEventRepository>();
    }
    
    [Fact]
    public async Task Publish_event_updates_status()
    {
        var eventId = await _registerEvent.Register("my fantastic event");
        
        await _publishEvent.Publish(eventId);
        
        var @event  = await _repository.Get(eventId);
        
        @event
            .Status
            .Should()
            .Be(EventStatus.Published);
    }
    
    [Fact]
    public async Task Publish_event_updates_status_in_event_list()
    {
        var eventId = await _registerEvent.Register("my fantastic event");
        
        await _publishEvent.Publish(eventId);
        
        var @event  = (await _listEvents.ListAll()).Single();
        
        @event
            .Status
            .Should()
            .Be(EventStatus.Published);
    }
    
    [Fact]
    public async Task Cannot_publish_event_if_already_published()
    {
        var eventId = await _registerEvent.Register("Test");
        
        await _publishEvent.Publish(eventId);
        
        var action = () => _publishEvent.Publish(eventId);
        
        await action
            .Should()
            .ThrowAsync<EventAlreadyPublishedException>();
    }
}