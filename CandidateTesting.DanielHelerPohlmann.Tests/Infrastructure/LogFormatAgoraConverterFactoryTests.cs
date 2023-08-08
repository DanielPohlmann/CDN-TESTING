using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Infrastructures.Converter;
using Microsoft.Extensions.Options;
using Moq;

namespace CandidateTesting.DanielHelerPohlmann.Tests.Infrastructure
{
    public class LogFormatAgoraConverterFactoryTests
    {
        private readonly Mock<IOptions<ConvertSettings>> _optionsMock;
        private readonly LogFormatAgoraConverterFactory _factory;

        public LogFormatAgoraConverterFactoryTests()
        {
            _optionsMock = new Mock<IOptions<ConvertSettings>>();
            _factory = new LogFormatAgoraConverterFactory(_optionsMock.Object);
        }

        [Fact]
        public void CreateConverter_ShouldReturn_LogFormatStringAgoraConverter()
        {
            // Arrange
            var expectedConverter = new LogFormatStringAgoraConverter(_optionsMock.Object);

            // Act
            var actualConverter = _factory.CreateConverter();

            // Assert
            Assert.IsType<LogFormatStringAgoraConverter>(actualConverter);
            Assert.Equal(expectedConverter.GetType(), actualConverter.GetType());
        }
    }
}
