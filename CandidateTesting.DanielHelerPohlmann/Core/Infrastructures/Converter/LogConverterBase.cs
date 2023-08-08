using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;

namespace CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Converter
{
    public class LogConverterBase : ILogConverter
    {
        public virtual ConvertResult Convert(
            string inputFileName,
            string outputFilePath)
        {
            throw new NotImplementedException();
        }
    }
}
