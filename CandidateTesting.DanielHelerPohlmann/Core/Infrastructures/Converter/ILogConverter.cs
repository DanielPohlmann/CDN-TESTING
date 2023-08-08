using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;

namespace CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Converter
{
    public interface ILogConverter
    {
        ConvertResult Convert(
            string inputFileName,
            string outputFilePath);
    }
}
