using System.ComponentModel.DataAnnotations;
using Eventify.Hexagonal.Api;
using Eventify.Hexagonal.Domain;
using Eventify.Hexagonal.Domain.Ports;
using Eventify.Hexagonal.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.RegisterDomain();
builder.Services.RegisterInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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
    catch (ArgumentException e)
    {
        return Results.Problem(statusCode: 400, title: e.Message);
    }
});

app.MapGet("/events", async (IListAllEventsUseCase useCase) => Results.Ok((object?)await useCase.ListAll()));

app.Run();

namespace Eventify.Hexagonal.Api
{
    public class CreateNewEventBody
    {
        [Required] public string Name { get; set; } = default!;
    }

    public class Program
    {
    }
}