using Eventify.Hexagonal.Domain.DrivingPorts;
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

        app.MapGet("/events", async (IListAllEventsUseCase useCase) => Results.Ok((object?)await useCase.ListAll()));

        app.MapPost("/events/{eventId}/description",
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
    }
}