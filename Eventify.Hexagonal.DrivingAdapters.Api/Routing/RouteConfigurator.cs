using Eventify.Hexagonal.Domain.DrivingPorts;
using Eventify.Hexagonal.Domain.Models;
using Eventify.Hexagonal.Domain.UseCases;
using Eventify.Hexagonal.Infrastructure.Adapters;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Hexagonal.Api.Routing;

public class RouteConfigurator(WebApplication app)
{
    public void MapRoutes()
    {
        app.MapPost("/events", async (
                [FromServices] IRegisterEventUseCase useCase,
                [FromBody] RegisterEventBody body
            )
            =>
        {
            try
            {
                await useCase.Register(body.Name);
                return Results.Ok();
            }
            catch (EventWithSameNameAlreadyExistsException e)
            {
                return Results.Problem(statusCode: 403, title: e.Message);
            }
            catch (ArgumentException e)
            {
                return Results.Problem(statusCode: 400, title: e.Message);
            }
        });

        app.MapGet("/events", async (IListAllEventsUseCase useCase) =>
        {
            var results = await useCase.ListAll();
            
            return Results.Ok(results);
        });

        app.MapPut("/events/{eventId}/describe",
            async (
                [FromServices] IDescribeEventUseCase useCase, 
                [FromRoute] Guid eventId,
                [FromBody] DescribeEventBody body) =>
            {
                try
                {
                    await useCase.Describe(eventId, body.Description);

                    return Results.Ok();
                }
                catch (EntityNotFoundException e)
                {
                    return Results.Problem(statusCode: 404, title: e.Message);
                }
            });

        app.MapPut("/events/{eventId}/publish",
            async (
                [FromServices] IPublishEventUseCase useCase, 
                [FromRoute] Guid eventId
            ) =>
            {
                try
                {
                    await useCase.Publish(eventId);

                    return Results.Ok();
                }
                catch (EventAlreadyPublishedException e)
                {
                    return Results.Problem(statusCode: 403, title: e.Message);
                }
                catch (EntityNotFoundException e)
                {
                    return Results.Problem(statusCode: 404, title: e.Message);
                }
            });
    }
}