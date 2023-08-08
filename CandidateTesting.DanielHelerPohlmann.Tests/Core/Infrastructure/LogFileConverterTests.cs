using Bogus;
using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Converter;
using CandidateTesting.DanielHelerPohlmann.Core.Resources;
using FluentAssertions;
using Moq;

namespace CandidateTesting.DanielHelerPohlmann.Tests.Core
{
    public class LogFileConverterTests
    {
        private readonly Mock<ILogConverterFactory> _factoryMock;
        private readonly Mock<ILogConverter> _converterMock;
        private readonly LogFileConverter _converter;
        private readonly Faker _faker;

        public LogFileConverterTests()
        {
            _converterMock = new Mock<ILogConverter>();
            _factoryMock = new Mock<ILogConverterFactory>();
            _factoryMock.Setup(x => x.CreateConverter()).Returns(_converterMock.Object);
            _converter = new LogFileConverter(_factoryMock.Object);
            _faker = new Faker();
        }

        [Fact]
        public void ConvertLogFile_InvalidInputPath_ThrowsException()
        {
            // Arrange
            var fakeOutputPath = new Faker().System.FilePath();
            var invalidInputPath = string.Empty;

            // Act
            var action = () => _converter.ConvertLogFile(invalidInputPath, fakeOutputPath);

            // Assert
            action.Should().Throw<FileException>()
                .WithMessage(Resource.InvalidFilePath);
        }

        [Fact]
        public void ConvertLogFile_InvalidOutputPath_ThrowsException()
        {
            // Arrange
            var fakeInputPath = new Faker().System.FilePath();
            var invalidOutputPath = string.Empty;

            // Act
            var action = () => _converter.ConvertLogFile(fakeInputPath, invalidOutputPath);

            // Assert
            action.Should().Throw<FileException>()
                .WithMessage(Resource.InvalidFilePath);
        }


        [Fact]
        public void ConvertLogFile_CallsConverter()
        {
            // Arrange
            string inputPath = _faker.System.FilePath();
            string outputPath = _faker.System.FilePath();

            _converterMock.Setup(x => x.Convert(inputPath, outputPath)).Returns(new ConvertResult(true, 1, ""));

            // Act
            _converter.ConvertLogFile(inputPath, outputPath);

            // Assert
            _converterMock.Verify(x => x.Convert(inputPath, outputPath), Times.Once);
        }
    }
}