using Eventify.Clean.Application.Events;
using Eventify.Clean.Application.Events.Administration;
using Eventify.Clean.Application.Events.Participation;
using Eventify.Clean.Domain.Exceptions.Base;
using Eventify.Clean.Infrastructure;
using Eventify.Clean.Presentation.Routing.Bodies;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Clean.Presentation.Routing;

public class RouteConfigurator(WebApplication app)
{
    public void MapRoutes()
    {
        app.MapPost("/events", async (
                [FromServices] RegisterEventUseCase useCase,
                [FromBody] RegisterEventBody body
            )
            =>
        {
            try
            {
                await useCase.Register(body.Name);
                return Results.Ok();
            }
            catch (Exception e) when(e is IDomainException)
            {
                return Results.Problem(statusCode: 403, title: e.Message);
            }
            catch (ArgumentException e)
            {
                return Results.Problem(statusCode: 400, title: e.Message);
            }
        });

        app.MapGet("/events", async (ListAllEventsUseCase useCase) =>
        {
            var results = await useCase.ListAll();
            
            return Results.Ok(results);
        });

        app.MapPut("/events/{eventId}/describe",
            async (
                [FromServices] DescribeEventUseCase useCase, 
                [FromRoute] Guid eventId,
                [FromBody] DescribeEventBody body) =>
            {
                try
                {
                    await useCase.Describe(eventId, body.Description);

                    return Results.Ok();
                }
                catch (Exception e) when(e is IDomainException)
                {
                    return Results.Problem(statusCode: 403, title: e.Message);
                }
                catch (EntityNotFoundException e)
                {
                    return Results.Problem(statusCode: 404, title: e.Message);
                }
            });

        app.MapPut("/events/{eventId}/publish",
            async (
                [FromServices] PublishEventUseCase useCase, 
                [FromRoute] Guid eventId
            ) =>
            {
                try
                {
                    await useCase.Publish(eventId);

                    return Results.Ok();
                }
                catch (Exception e) when(e is IDomainException)
                {
                    return Results.Problem(statusCode: 403, title: e.Message);
                }
                catch (EntityNotFoundException e)
                {
                    return Results.Problem(statusCode: 404, title: e.Message);
                }
            });

        app.MapPost("/events/{eventId}/participants",
            async (
                [FromServices] JoinEventUseCase useCase, 
                [FromRoute] Guid eventId,
                [FromBody] JoinEventBody body
            ) =>
            {
                try
                {
                    await useCase.Join(eventId, body.EmailAddress);

                    return Results.Ok();
                }
                catch (Exception e) when(e is IDomainException)
                {
                    return Results.Problem(statusCode: 403, title: e.Message);
                }
                catch (EntityNotFoundException e)
                {
                    return Results.Problem(statusCode: 404, title: e.Message);
                }
            });

        app.MapPost("/events/{eventId}/comments",
            async (
                [FromServices] CommentEventUseCase useCase, 
                [FromRoute] Guid eventId,
                [FromBody] CommentEventBody body
            ) =>
            {
                try
                {
                    await useCase.Comment(eventId, body.Commenter, body.Comment);

                    return Results.Ok();
                }
                catch (Exception e) when(e is IDomainException)
                {
                    return Results.Problem(statusCode: 403, title: e.Message);
                }
                catch (EntityNotFoundException e)
                {
                    return Results.Problem(statusCode: 404, title: e.Message);
                }
            });

        app.MapGet("/events/{eventId}",
            async (
                [FromServices] GetEventDetailsUseCase useCase, 
                [FromRoute] Guid eventId
            ) =>
            {
                var details = await useCase.GetEventDetails(eventId);

                return Results.Ok(details);
            });
    }

}