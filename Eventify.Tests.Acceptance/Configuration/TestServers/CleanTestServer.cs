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
                services.AddSingleton<IEmailSender>(_ => Substitute.For<IEmailSender>());
            })
            ;
    }

    private DelegateLoggerProvider BuildDelegateLoggerProvider(IServiceProvider serviceProvider)
    {
        var action = new Action<string>(outputHelper.WriteLine);
        return ActivatorUtilities.CreateInstance<DelegateLoggerProvider>(serviceProvider, action);
    }

    public Task Initialize()
    {
        _ = Server;
        
        return Task.CompletedTask;
    }

    public void OverrideCurrentDate(DateTime now) =>
        Services.GetRequiredService<IClock>().Now().Returns(now);

    public DateTime GetCurrentDate() =>
        Services.GetRequiredService<IClock>().Now();

    public EmailTest GetLastSentEmailTo(string toEmail) =>
        Services
            .GetRequiredService<IEmailSender>()
            .ReceivedCalls()
            .Where(x => x.GetMethodInfo().Name == nameof(IEmailSender.Send))
            .Select(x => (Email)x.GetArguments().ElementAt(0)!)
            .Where(x => x.To == toEmail)
            .Select(x => new EmailTest(
                x.Subject,
                x.Content
            ))
            .SingleOrDefault() ?? throw new InvalidOperationException("No email sent to " + toEmail);
}