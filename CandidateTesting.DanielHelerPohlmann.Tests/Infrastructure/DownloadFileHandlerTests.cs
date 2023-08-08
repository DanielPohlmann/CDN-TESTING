using Bogus;
using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Download;
using CandidateTesting.DanielHelerPohlmann.Infrastructures.Download;
using Moq;

namespace CandidateTesting.DanielHelerPohlmann.Tests.Infrastructure
{
    public class DownloadFileHandlerTests
    {
        private readonly Mock<IDownloadService> _downloadServiceMock;
        private readonly Mock<IDownloadServiceFactory> _downloadServiceFactoryMock;
        private readonly DownloadFileHandler _handler;
        private readonly Faker _faker;
        
        public DownloadFileHandlerTests()
        {
            _downloadServiceFactoryMock = new Mock<IDownloadServiceFactory>();
            _downloadServiceMock = new Mock<IDownloadService>();
            _downloadServiceFactoryMock.Setup(x => x.Create()).Returns(_downloadServiceMock.Object);
            _handler = new DownloadFileHandler(_downloadServiceFactoryMock.Object);
            _faker = new Faker();
        }

        [Fact]
        public async Task Handle_Returns_DownloadFileResponse()
        {
            // Arrange
            var fakeRequest = new DownloadFileRequest($"{_faker.Internet.UrlWithPath()}/{_faker.System.FileName()}");

            var cancellationToken = new CancellationToken();

            _downloadServiceMock.Setup(svc => svc.DownloadFileAsync(fakeRequest, cancellationToken))
                               .ReturnsAsync(new DownloadFileResponse(true, "Success"));

            _downloadServiceFactoryMock.Setup(factory => factory.Create())
                                       .Returns(_downloadServiceMock.Object);

            // Act
            var result = await _handler.Handle(fakeRequest, cancellationToken);

            // Assert
            Assert.IsType<DownloadFileResponse>(result);
        }
    }
}
