using CandidateTesting.DanielHelerPohlmann.Configuration;
using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Helpers.Timer;
using CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Converter;
using CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Download;
using CandidateTesting.DanielHelerPohlmann.Infrastructures.Download;
using CandidateTesting.DanielHelerPohlmann.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CandidateTesting.DanielHelerPohlmann.Tests.Configuration
{
    public class DependencyInjectionConfigTests
    {
        [Fact]
        public void RegisterServices_AddsExpectedServices()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();
            var httpClientMock = new Mock<HttpClient>();

            // Act
            services.AddSingleton(httpClientMock.Object);
            services.AddSingleton<IConfiguration>(configuration);
            services.AddLogging();
            services.AddOptions();
            services.Configure<DownloadSettings>(x=> configuration.GetSection(DownloadSettings.Download));
            services.Configure<ConvertSettings>(x=> configuration.GetSection(ConvertSettings.Convert));
            services.RegisterServices();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            Assert.NotNull(serviceProvider.GetService<HttpClient>());
            Assert.NotNull(serviceProvider.GetService<IConfiguration>());
            Assert.NotNull(serviceProvider.GetService<Setup>());
            Assert.NotNull(serviceProvider.GetService<ITimer>());
            Assert.NotNull(serviceProvider.GetService<IDownloadService>());
            Assert.NotNull(serviceProvider.GetService<IDownloadServiceFactory>());
            Assert.NotNull(serviceProvider.GetService<ICdnService>());
            Assert.NotNull(serviceProvider.GetService<ILogFileConverter>());
        }
    }
}
