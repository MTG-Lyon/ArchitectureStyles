using System.Text.Json.Serialization;
using Eventify.Clean.Application;
using Eventify.Clean.Infrastructure;
using Eventify.Clean.Presentation.Routing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

namespace Eventify.Clean.Presentation
{
    public class Program
    {
    }
}