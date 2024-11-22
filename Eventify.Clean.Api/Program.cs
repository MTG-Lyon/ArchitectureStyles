using System.ComponentModel.DataAnnotations;
using Eventify.Clean.Api;
using Eventify.Clean.Application;
using Eventify.Clean.Application.Events;
using Eventify.Clean.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterApplication();
builder.Services.RegisterInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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
    catch (ArgumentException e)
    {
        return Results.Problem(statusCode: 400, title: e.Message);
    }
});

app.MapGet("/events", async (ListAllEventsUseCase useCase) => Results.Ok(await useCase.ListAll()));

app.Run();

namespace Eventify.Clean.Api
{
    public class CreateNewEventBody
    {
        [Required] public string Name { get; set; } = default!;
    }

    public class Program
    {
    }
}