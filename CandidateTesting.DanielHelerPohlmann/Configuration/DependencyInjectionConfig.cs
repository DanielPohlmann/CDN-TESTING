using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Helpers.Timer;
using CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Converter;
using CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Download;
using CandidateTesting.DanielHelerPohlmann.Infrastructures.Converter;
using CandidateTesting.DanielHelerPohlmann.Infrastructures.Download;
using CandidateTesting.DanielHelerPohlmann.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using System.Globalization;
using System.Reflection;
using Timer = CandidateTesting.DanielHelerPohlmann.Core.Helpers.Timer.Timer;

namespace CandidateTesting.DanielHelerPohlmann.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();  
            
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IConfiguration>(x => configuration);
            services.AddTransient<Setup>();
            services.AddScoped<ITimer, Timer>();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddScoped<IDownloadService, DownloadService>();
            services.AddScoped<IDownloadServiceFactory, DownloadServiceFactory>();
            services.AddScoped<ICdnService, CdnService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient<ILogFileConverter>(cfg =>
            new LogFileConverter(
                new LogFormatAgoraConverterFactory(
                    cfg.GetRequiredService<IOptions<ConvertSettings>>()
                    )
                )
            );

            services.Configure<DownloadSettings>(cfg => configuration.GetSection(DownloadSettings.Download).Bind(cfg));
            services.Configure<ConvertSettings>(cfg => configuration.GetSection(ConvertSettings.Convert).Bind(cfg));

            services.AddOptions();
        }
    }
}
