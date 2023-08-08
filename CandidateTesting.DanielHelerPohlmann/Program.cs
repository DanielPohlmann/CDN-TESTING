using CandidateTesting.DanielHelerPohlmann;
using CandidateTesting.DanielHelerPohlmann.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .CreateLogger();

var services = new ServiceCollection();
services.RegisterServices();

var serviceProvider = services.BuildServiceProvider();

var setup = serviceProvider.GetService<Setup>();

if (setup is not null)
{
    await setup.Run(args);
}