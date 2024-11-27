using Eventify.Clean.Application;
using Eventify.Clean.Presentation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Reqnroll;

namespace Eventify.Tests.Acceptance.Configuration.TestServers;

public class CleanTestServer(
    IReqnrollOutputHelper outputHelper
) : WebApplicationFactory<Program>, ITestServer
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        var databaseName = Guid.NewGuid().ToString();
        
        builder
            .ConfigureLogging(x =>
            {
                x.AddSimpleConsole(option =>
                {
                    option.IncludeScopes = false;
                    option.TimestampFormat = "hh:mm:ss.ff ";
                });
                x.Services.AddSingleton<ILoggerProvider>(BuildDelegateLoggerProvider);
            })
            .ConfigureServices(services =>
            {
                services.ReplaceWithInMemoryDatabase(databaseName);
                services.AddSingleton<IClock>(_ => Substitute.For<IClock>());
            })
            ;
    }

    private DelegateLoggerProvider BuildDelegateLoggerProvider(IServiceProvider serviceProvider)
    {
        var action = new Action<string>(outputHelper.WriteLine);
        return ActivatorUtilities.CreateInstance<DelegateLoggerProvider>(serviceProvider, action);
    }

    public string Name => "Clean";

    public Task Initialize()
    {
        _ = Server;
        
        return Task.CompletedTask;
    }

    public T GetService<T>() where T : notnull =>
        Services.GetRequiredService<T>();

    public void OverrideCurrentDate(DateTime now) =>
        Services.GetRequiredService<IClock>().Now().Returns(now);

    public DateTime GetCurrentDate() =>
        Services.GetRequiredService<IClock>().Now();

    public EmailTest GetLastSentEmailTo(string toEmail) =>
        throw new NotImplementedException();
}