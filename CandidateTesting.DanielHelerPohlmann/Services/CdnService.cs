using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Helpers.Timer;
using CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Converter;
using CandidateTesting.DanielHelerPohlmann.Models;
using CandidateTesting.DanielHelerPohlmann.Sources.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CandidateTesting.DanielHelerPohlmann.Services
{
    public class CdnService : ICdnService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CdnService> _logger;
        private readonly ILogFileConverter _logFileConverter;
        private readonly ITimer _timer;

        public CdnService(
            IMediator mediator,
            ILogger<CdnService> logger,
            ILogFileConverter logFileConverter,
            ITimer timer)
        {
            _mediator = mediator;
            _logger = logger;
            _logFileConverter = logFileConverter;
            _timer = timer;
        }

        public async Task ConvertLog(ArgsParamSetup argsParamSetup)
        {
            var request = new DownloadFileRequest(argsParamSetup.UrlFileImput);

            await DownloadFile(request);

            ConvertLogFile(
                request.FileName,
                argsParamSetup.PatchFileOutput ?? string.Empty
            );
        }

        private void ConvertLogFile(
            string inputFileName,
            string? outputFilePath)
        {
            _timer.StartTimer();
            _logger.LogInformation(Message.StarConvert);

            var result = _logFileConverter.ConvertLogFile(
                inputFileName,
                outputFilePath
            );

            if (!result.IsSuccess)
            {
                _logger.LogError(result.Message);
                return;
            }

            _logger.LogInformation(result.Message);


            _timer.StopTimer();
            _logger.LogInformation(string.Format(Message.FinishConvert, _timer?.GetTotalTime() ?? 0));
        }

        private async Task DownloadFile(DownloadFileRequest downloadFileRequest)
        {
            _timer.StartTimer();
            _logger.LogInformation(Message.StartDownload);

            var response = await _mediator.Send(
                downloadFileRequest,
                CancellationToken.None
            );

            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return;
            }

            _timer.StopTimer();
            _logger.LogInformation(string.Format(Message.FinishDownload, (_timer?.GetTotalTime() ?? 0)));
        }
    }
}










