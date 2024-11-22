using Eventify.Hexagonal.Api;
using Eventify.Hexagonal.Domain;
using Eventify.Hexagonal.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

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

new RouteConfigurator(app).MapRoutes();

app.Run();

namespace Eventify.Hexagonal.Api
{
    public class Program;
}