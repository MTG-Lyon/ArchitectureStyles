using Eventify.Hexagonal.Api.Routing;
using Eventify.Hexagonal.Domain.Ports;
using Eventify.Hexagonal.Domain.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Hexagonal.Api;

public class RouteConfigurator(WebApplication app)
{
    public void MapRoutes()
    {
        MapPostEvent();
        MapGetEvents();
    }

    private void MapPostEvent() =>
        app.MapPost("/events", async (
                [FromServices] ICreateNewEventUseCase useCase,
                [FromBody] CreateNewEventBody body
            )
            =>
        {
            try
            {
                await useCase.Execute(body.Name);
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

    private void MapGetEvents() =>
        app.MapGet("/events", async (IListAllEventsUseCase useCase) => Results.Ok((object?)await useCase.ListAll()));
}