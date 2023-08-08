using Bogus;
using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Infrastructures.Download;
using CandidateTesting.DanielHelerPohlmann.Sources.Messages;
using CandidateTesting.DanielHelerPohlmann.Sources.Templates;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;

namespace CandidateTesting.DanielHelerPohlmann.Tests.Infrastructure
{
    public class DownloadServiceTests
    {
        private readonly Faker _faker;
        public DownloadServiceTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public async Task DownloadFileAsync_Should_Return_Success_Response()
        {
            // Arrange
            var downloadSettings = new DownloadSettings { BufferLength = 1024, InputPath = "./input/" };
            var optionsMock = new Mock<IOptions<DownloadSettings>>();
            optionsMock.SetupGet(x => x.Value).Returns(downloadSettings);

            var content = new ByteArrayContent(new byte[] { 0x00, 0x01, 0x02 });
            content.Headers.ContentLength = 3;
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = content
            };
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object);

            var service = new DownloadService(httpClient, optionsMock.Object);

            var urlFile = _faker.System.FileName();
            var urlPathFile = $"{_faker.Internet.UrlWithPath()}/{urlFile}";
            var request = new DownloadFileRequest(urlPathFile);

            // Act
            var result = await service.DownloadFileAsync(request, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(string.Format(Message.DownloadSucess, $"{downloadSettings.InputPath}{urlFile}", 3), result.Message);
            File.Delete($"{downloadSettings.InputPath}{urlFile}");
        }

        [Fact]
        public async Task DownloadFileAsync_Should_Return_Error_Response()
        {
            // Arrange
            var optionsMock = new Mock<IOptions<DownloadSettings>>();
            var httpClient = new HttpClient();
            var service = new DownloadService(httpClient, optionsMock.Object);

            var request = new DownloadFileRequest($"{_faker.Internet.UrlWithPath()}{_faker.System.FileName()}");

            // Act
            var result = await service.DownloadFileAsync(request, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Message ?? string.Empty);
        }
    }
}
