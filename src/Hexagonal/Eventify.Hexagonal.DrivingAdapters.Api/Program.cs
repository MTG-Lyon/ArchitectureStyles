using System.Text.Json.Serialization;
using Eventify.Hexagonal.Application;
using Eventify.Hexagonal.DrivenAdapters.Sql;
using Eventify.Hexagonal.DrivingAdapters.Api.Routing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services
    .RegisterApplication()
    .RegisterInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

new RouteConfigurator(app).MapRoutes();

app.Run();

namespace Eventify.Hexagonal.DrivingAdapters.Api
{
    public class Program;
}