using CandidateTesting.DanielHelerPohlmann.Models;
using CandidateTesting.DanielHelerPohlmann.Services;
using CandidateTesting.DanielHelerPohlmann.Sources.Messages;
using Microsoft.Extensions.Logging;

namespace CandidateTesting.DanielHelerPohlmann
{
    public class Setup
    {
        private readonly ICdnService _cdnService;
        private readonly ILogger<Setup> _logger;

        public Setup(ICdnService dependency, ILogger<Setup> logger)
        {
            _cdnService = dependency;
            _logger = logger;
        }

        public async Task Run(string[] args)
        {
            ////args = new string[] { "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt", "./output/minhaCdn1.txt" };

            _logger.LogInformation(Message.ProgramStarted);

            var argsParamSetup = new ArgsParamSetup(args);

            if (!argsParamSetup.IsValid())
            {
                var message = string.Join(
                    "\n",
                    argsParamSetup.ValidationResult?.Errors?.Select(x => $"{x.PropertyName}: {x.ErrorMessage}") ?? new string[0]
                    );

                _logger.LogError(message);

                return;
            }

            await _cdnService.ConvertLog(argsParamSetup);

            _logger.LogInformation(Message.ProgramEnded);
        }
    }

}
