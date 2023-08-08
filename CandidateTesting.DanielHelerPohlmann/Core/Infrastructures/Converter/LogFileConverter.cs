using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Extenssions;
using CandidateTesting.DanielHelerPohlmann.Core.Resources;

namespace CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Converter
{
    public class LogFileConverter : ILogFileConverter
    {
        readonly ILogConverter _converter;

        public LogFileConverter(ILogConverterFactory factory)
        {
            _converter = factory.CreateConverter();
        }

        public ConvertResult ConvertLogFile(
            string inputFileName,
            string? outputFilePath)
        {
            inputFileName.IsValidPathThrow(Resource.InvalidFilePath);

            outputFilePath?.IsValidPathThrow(Resource.InvalidFilePath);

            return _converter.Convert(inputFileName, outputFilePath ?? string.Empty);
        }
    }   
}
