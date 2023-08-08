using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Helpers.Timer;
using CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Converter;
using CandidateTesting.DanielHelerPohlmann.Models;
using CandidateTesting.DanielHelerPohlmann.Services;
using CandidateTesting.DanielHelerPohlmann.Tests.Builders;
using CandidateTesting.DanielHelerPohlmann.Tests.Helper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
namespace CandidateTesting.DanielHelerPohlmann.Tests.Services
{
    public class CdnServiceTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<CdnService>> _loggerMock;
        private readonly Mock<ILogFileConverter> _logFileConverterMock;
        private readonly ITimer _timerMock;

        public CdnServiceTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<CdnService>>();
            _logFileConverterMock = new Mock<ILogFileConverter>();
            _timerMock = new CandidateTesting.DanielHelerPohlmann.Core.Helpers.Timer.Timer();
        }

        [Fact]
        public async Task ConvertLog_Should_DownloadFile_And_ConvertLogFile_Sucess()
        {
            // Arrange
            var cdnServiceTestObject = new CdnServiceTestObjectBuilder.Builder().WithDefaultValues().Build();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DownloadFileRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DownloadFileResponse(true, cdnServiceTestObject.MessageResult)) //<-- return Task to allow await to continue
                .Verifiable("Notification was not sent.");

            _logFileConverterMock
                .Setup(lfc => lfc.ConvertLogFile(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ConvertResult(true, cdnServiceTestObject.TotalTime.GetValueOrDefault().Milliseconds, cdnServiceTestObject.MessageResult));

            var cdnService = new CdnService(
                _mediatorMock.Object,
                _loggerMock.Object,
                _logFileConverterMock.Object,
                _timerMock
            );

            // Act
            await cdnService.ConvertLog(cdnServiceTestObject?.ArgsParamSetup ?? new ArgsParamSetup(new string[] { }));

            // Assert
            _mediatorMock.Verify(x => x.Send(It.IsAny<DownloadFileRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            _logFileConverterMock.Verify(lfc => lfc.ConvertLogFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _loggerMock.Verify<CdnService>(LogLevel.Information, Times.Exactly(5));
            _loggerMock.Verify<CdnService>(LogLevel.Error, Times.Never());
        }

        [Fact]
        public async Task ConvertLog_Should_DownloadFile_And_ConvertLogFile_Convert_Error()
        {
            // Arrange
            var cdnServiceTestObject = new CdnServiceTestObjectBuilder.Builder().WithDefaultValues().Build();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DownloadFileRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DownloadFileResponse(true, cdnServiceTestObject.MessageResult)) //<-- return Task to allow await to continue
                .Verifiable("Notification was not sent.");

            _logFileConverterMock
                .Setup(lfc => lfc.ConvertLogFile(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ConvertResult(false, cdnServiceTestObject.TotalTime.GetValueOrDefault().Milliseconds, cdnServiceTestObject.MessageResult));

            var cdnService = new CdnService(
                _mediatorMock.Object,
                _loggerMock.Object,
                _logFileConverterMock.Object,
                _timerMock
            );

            // Act
            await cdnService.ConvertLog(cdnServiceTestObject.ArgsParamSetup ?? new ArgsParamSetup(new string[] { }));

            // Assert
            _mediatorMock.Verify(x => x.Send(It.IsAny<DownloadFileRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            _logFileConverterMock.Verify(lfc => lfc.ConvertLogFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _loggerMock.Verify<CdnService>(LogLevel.Error, Times.AtLeastOnce());
        }

        [Fact]
        public async Task ConvertLog_Should_DownloadFile_And_ConvertLogFile_Download_Error()
        {
            // Arrange
            var cdnServiceTestObject = new CdnServiceTestObjectBuilder.Builder().WithDefaultValues().Build();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DownloadFileRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DownloadFileResponse(false, cdnServiceTestObject.MessageResult)) //<-- return Task to allow await to continue
                .Verifiable("Notification was not sent.");

            _logFileConverterMock
                .Setup(lfc => lfc.ConvertLogFile(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ConvertResult(true, cdnServiceTestObject.TotalTime.GetValueOrDefault().Milliseconds, cdnServiceTestObject.MessageResult));

            var cdnService = new CdnService(
                _mediatorMock.Object,
                _loggerMock.Object,
                _logFileConverterMock.Object,
                _timerMock
            );

            // Act
            await cdnService.ConvertLog(cdnServiceTestObject?.ArgsParamSetup ?? new ArgsParamSetup(new string[] { }));

            // Assert
            _mediatorMock.Verify(x => x.Send(It.IsAny<DownloadFileRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            _logFileConverterMock.Verify(lfc => lfc.ConvertLogFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _loggerMock.Verify<CdnService>(LogLevel.Error, Times.AtLeastOnce());
        }
    }
}