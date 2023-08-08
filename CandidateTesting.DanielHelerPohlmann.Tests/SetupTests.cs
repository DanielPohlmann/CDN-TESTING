using Bogus;
using CandidateTesting.DanielHelerPohlmann.Models;
using CandidateTesting.DanielHelerPohlmann.Services;
using CandidateTesting.DanielHelerPohlmann.Tests.Helper;
using Microsoft.Extensions.Logging;
using Moq;

namespace CandidateTesting.DanielHelerPohlmann.Tests
{
    public class SetupTests
    {
        private readonly Setup _setup;
        private readonly Mock<ICdnService> _cdnServiceMock;
        private readonly Mock<ILogger<Setup>> _loggerMock;
        private readonly Faker _faker;

        public SetupTests()
        {
            _cdnServiceMock = new Mock<ICdnService>();
            _loggerMock = new Mock<ILogger<Setup>>();
            _setup = new Setup(_cdnServiceMock.Object, _loggerMock.Object);
            _faker = new Faker();
        }

        [Fact]
        public async Task Run_ValidArgsParamSetup_CallsCdnServiceConvertLog()
        {
            // Arrange
            var url = $"{_faker.Internet.UrlWithPath()}/{_faker.System.FileName()}";
            var path = _faker.System.FilePath();
            var fakeArgs = new[] { url, path };

            // Act
            await _setup.Run(fakeArgs);

            // Assert
            _cdnServiceMock.Verify(mock => mock.ConvertLog(
                It.IsAny<ArgsParamSetup>()),
                Times.Once
                );
        }

        [Fact]
        public async Task Run_InvalidArgsParamSetup_LogsError()
        {
            // Arrange
            var fakeArgs = new string[0];

            // Act
            await _setup.Run(fakeArgs);

            // Assert
            _loggerMock.Verify<Setup>(LogLevel.Error, Times.Once());
        }
    }
}