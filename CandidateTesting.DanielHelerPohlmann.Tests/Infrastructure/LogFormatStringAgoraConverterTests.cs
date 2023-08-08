using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Helpers;
using CandidateTesting.DanielHelerPohlmann.Infrastructures.Converter;
using CandidateTesting.DanielHelerPohlmann.Sources.Messages;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace CandidateTesting.DanielHelerPohlmann.Tests.Infrastructure
{
    public class LogFormatStringAgoraConverterTests
    {
        private readonly Mock<IOptions<ConvertSettings>> _mockOptions;
        private readonly LogFormatStringAgoraConverter _converter;
        private const string OUTPUT_PATCH_FILE = "./output/output_test.log";
        private const string INPUT_FILE = "input_test.log";
        private const string INPUT_PATCH = "./input/";
        private string InputPatchFile { get; }

        public LogFormatStringAgoraConverterTests()
        {
            _mockOptions = new Mock<IOptions<ConvertSettings>>();
            _mockOptions.Setup(x => x.Value).Returns(new ConvertSettings { 
                ProgressLineLength = 1, 
                InputPath = INPUT_PATCH
            });
            _converter = new LogFormatStringAgoraConverter(_mockOptions.Object);
            InputPatchFile = $"{INPUT_PATCH}{INPUT_FILE}";
        }

        [Fact]
        public void Convert_WhenValidInput_ReturnsSuccessResult()
        {
            // Arrange
            CreateFile(new List<string>() {
                "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2",
                "101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4"
            });

            // Act
            var result = _converter.Convert(INPUT_FILE, OUTPUT_PATCH_FILE);

            // Assert
            ValidateResponse(result);

            using (StreamReader reader = new StreamReader(OUTPUT_PATCH_FILE))
            {
                ValidateHeader(reader);

                reader.ReadLine().Should().Be("\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT");
                reader.ReadLine().Should().Be("\"MINHA CDN\" POST 200 /myImages 319 101 MISS");
            }

            // Clean up
            CleanUp();
        }

        [Fact]
        public void Convert_WhenConvertLineBody_ThrowsInvalidOperationException()
        {
            // Arrange
            var lines = new List<string>() {
                "200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2"
            };
            CreateFile(lines);

            // Act
            var result = _converter.Convert(INPUT_FILE, OUTPUT_PATCH_FILE);

            // Act & Assert
            result.IsSuccess.Should().BeFalse();
            result.LinesProcessed.Should().Be(lines.Count);
            result.Message.Should().NotBeNullOrWhiteSpace();

            // Clean up
            CleanUp();
        }

        [Fact]
        public void Convert_WhenConvertLineBody_IgnoreLineEmpty()
        {
            // Arrange
            CreateFile(new List<string>() {
                string.Empty,
                "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2"
            });

            // Act
            var result = _converter.Convert(INPUT_FILE, OUTPUT_PATCH_FILE);

            // Assert
            ValidateResponse(result);

            using (StreamReader reader = new StreamReader(OUTPUT_PATCH_FILE))
            {
                ValidateHeader(reader);

                reader.ReadLine().Should().Be("\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT");
            }

            // Clean up
            CleanUp();
        }

        private void CreateFile(List<string> lines)
        {
            FileHelper.CreatePathFromFilePath(InputPatchFile);
            using (StreamWriter writer = new StreamWriter(InputPatchFile))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }

        private void ValidateHeader(StreamReader reader)
        {
            reader.ReadLine().Should().Be("#Version: 1.0");
            reader.ReadLine().Should().Contain("#Date:");
            reader.ReadLine().Should().Be("#Fields: provider http-method status-code uri-path time-taken response-size cache-status");
        }

        private void ValidateResponse(ConvertResult result)
        {
            result.IsSuccess.Should().BeTrue();
            result.LinesProcessed.Should().Be(2);
            result.Message.Should().Be(string.Format(Message.SucessConvert, result.LinesProcessed));
            File.Exists(OUTPUT_PATCH_FILE).Should().BeTrue();
        }

        private void CleanUp()
        {
            File.Delete(InputPatchFile);
            File.Delete(OUTPUT_PATCH_FILE);
        }
    }
}
