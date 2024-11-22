using Eventify.Clean.Api.Routing;
using Eventify.Clean.Application;
using Eventify.Clean.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

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

new RouteConfigurator(app).MapRoutes();

app.Run();

namespace Eventify.Clean.Api
{
    public class Program
    {
    }
}