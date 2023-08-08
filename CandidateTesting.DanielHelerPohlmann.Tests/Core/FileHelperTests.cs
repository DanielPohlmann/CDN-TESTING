using Bogus;
using CandidateTesting.DanielHelerPohlmann.Core.Helpers;
using FluentAssertions;

namespace CandidateTesting.DanielHelerPohlmann.Tests.Core
{
    public class FileHelperTests
    {
        [Fact]
        public void CreatePathFromFilePath_Should_CreateDirectory()
        {
            // Arrange
            var filePath = new Faker().System.FilePath();
            var directoryPath = Path.GetDirectoryName(filePath);

            // Act
            FileHelper.CreatePathFromFilePath(filePath);

            // Assert
            Directory.Exists(directoryPath).Should().BeTrue();
        }

        [Fact]
        public void DeleteFilePathIfExist_Should_DeleteFile()
        {
            // Arrange
            var filePath = new Faker().System.FilePath();
            FileHelper.CreatePathFromFilePath(filePath);

            // Act
            FileHelper.DeleteFilePathIfExist(filePath);

            // Assert
            File.Exists(filePath).Should().BeFalse();
        }
    }
}
