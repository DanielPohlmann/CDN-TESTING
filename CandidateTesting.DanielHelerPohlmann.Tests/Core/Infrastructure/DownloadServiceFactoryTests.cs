using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Download;
using CandidateTesting.DanielHelerPohlmann.Infrastructures.Download;
using Microsoft.Extensions.Options;
using Moq;

namespace CandidateTesting.DanielHelerPohlmann.Tests.Core.Infrastructure
{
    public class DownloadServiceFactoryTests
    {
        private readonly Mock<HttpClient> _httpClientMock;
        private readonly Mock<IOptions<DownloadSettings>> _optionsMock;

        public DownloadServiceFactoryTests()
        {
            _httpClientMock = new Mock<HttpClient>();
            _optionsMock = new Mock<IOptions<DownloadSettings>>();
        }

        [Fact]
        public void Create_ReturnsDownloadServiceInstance()
        {
            // Arrange
            var factory = new DownloadServiceFactory(_httpClientMock.Object, _optionsMock.Object);

            // Act
            var result = factory.Create();

            // Assert
            Assert.IsType<DownloadService>(result);
        }
    }
}
