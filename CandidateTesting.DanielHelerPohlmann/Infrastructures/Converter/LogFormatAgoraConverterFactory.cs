using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Converter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CandidateTesting.DanielHelerPohlmann.Infrastructures.Converter
{
    public class LogFormatAgoraConverterFactory : ILogConverterFactory
    {
        readonly IOptions<ConvertSettings> _settings;
        public LogFormatAgoraConverterFactory(IOptions<ConvertSettings> settings)
        {
            _settings = settings;
        }

        public ILogConverter CreateConverter()
        {
            return new LogFormatStringAgoraConverter(_settings);
        }
    }
}
