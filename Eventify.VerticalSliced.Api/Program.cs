using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Eventify.Infrastructure.Database;
using Eventify.VerticalSliced.Api.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .RegisterUserCases()
    .RegisterDatabase();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        );
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

// app.MapPost("/events", async (
//         [FromServices] ICreateNewEventUseCase useCase,
//         [FromBody] CreateNewEventBody body
//     )
//     =>
// {
//     try
//     {
//         await useCase.Execute(body.Name);
//         return Results.Ok();
//     }
//     catch (ArgumentException e)
//     {
//         return Results.Problem(statusCode: 400, title: e.Message);
//     }
// });
//
// app.MapGet("/events", async (IListAllEventsUseCase useCase) => Results.Ok((object?)await useCase.ListAll()));

app.Run();

namespace Eventify.VerticalSliced.Api
{
    

    public class Program
    {
    }
}