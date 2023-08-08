using Bogus;
using CandidateTesting.DanielHelerPohlmann.Sources.Messages;
using FluentAssertions;

namespace CandidateTesting.DanielHelerPohlmann.Tests.DomainObjects
{
    public class ArgsParamSetupTests
    {
        private readonly Faker _faker = new Faker();

        [Fact]
        public void IsValid_ShouldReturnTrue_WhenArgsAreValid()
        {
            // Arrange
            var args = new[] {
            $"{_faker.Internet.UrlWithPath()}/{_faker.System.FileName()}",
            _faker.System.FilePath()
        };
            var param = new Models.ArgsParamSetup(args);

            // Act
            var result = param.IsValid();

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void UrlFileImput_ShouldBeInvalid_WhenNullOrEmptyOrWhitespace(string url)
        {
            // Arrange
            var args = new[] { url, _faker.System.DirectoryPath() };

            // Act
            var argsParamSetup = new Models.ArgsParamSetup(args);
            var validationResult = argsParamSetup.IsValid();

            // Assert
            validationResult.Should().BeFalse();
            argsParamSetup?.ValidationResult?.Errors?.Should()?.Contain(e => e.PropertyName == nameof(Models.ArgsParamSetup.UrlFileImput));
        }

        [Fact]
        public void UrlFileImput_ShouldBeInvalid_WhenUrlIsNotValid()
        {
            // Arrange
            var path = _faker.System.FilePath();
            var url = _faker.Random.String2(5);
            var args = new[] { url, path };


            // Act
            var argsParamSetup = new Models.ArgsParamSetup(args);
            var validationResult = argsParamSetup.IsValid();

            // Assert
            validationResult.Should().BeFalse();
            argsParamSetup?.ValidationResult?.Errors?.Should()?.Contain(e => e.PropertyName == nameof(Models.ArgsParamSetup.UrlFileImput) && e.ErrorMessage == Message.UrlIsNotValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void PatchFileOutput_ShouldBeInvalid_WhenNullOrEmptyOrWhitespace(string path)
        {
            // Arrange
            var args = new[] { _faker.System.FileName(), path };

            // Act
            var argsParamSetup = new Models.ArgsParamSetup(args);
            var validationResult = argsParamSetup.IsValid();

            // Assert
            validationResult.Should().BeFalse();
            argsParamSetup?.ValidationResult?.Errors?.Should()?.Contain(e => e.PropertyName == nameof(Models.ArgsParamSetup.UrlFileImput));
        }

        [Fact]
        public void PatchFileOutput_ShouldBeInvalid_WhenPathIsNotValid()
        {
            // Arrange
            var path = _faker.Random.String2(5);
            var url = $"{_faker.Internet.UrlWithPath()}/{_faker.System.FileName()}";
            var args = new[] { url, path };


            // Act
            var argsParamSetup = new Models.ArgsParamSetup(args);
            var validationResult = argsParamSetup.IsValid();

            // Assert
            validationResult.Should().BeFalse();
            argsParamSetup?.ValidationResult?.Errors?.Should()?.Contain(e => e.PropertyName == nameof(Models.ArgsParamSetup.PatchFileOutput) && e.ErrorMessage == Message.PathIsNotValid);
        }
    }
}