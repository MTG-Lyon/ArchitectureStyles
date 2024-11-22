using Eventify.Clean.Application.Events;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Clean.Api.Routing;

public class RouteConfigurator(WebApplication app)
{
    public void MapRoutes()
    {
        MapPostEvent();

        MapGetEvents();
    }

    private void MapPostEvent() =>
        app.MapPost("/events", async (
                [FromServices] CreateNewEventUserCase useCase,
                [FromBody] CreateNewEventBody body
            )
            =>
        {
            try
            {
                await useCase.CreateNewEvent(body.Name);
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
        app.MapGet("/events", async (ListAllEventsUseCase useCase) => Results.Ok(await useCase.ListAll()));
}