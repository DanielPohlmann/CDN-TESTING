using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;

namespace CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Converter
{
    public interface ILogFileConverter
    {
        ConvertResult ConvertLogFile(
            string inputFileName,
            string? outputFilePath);
    }
}
