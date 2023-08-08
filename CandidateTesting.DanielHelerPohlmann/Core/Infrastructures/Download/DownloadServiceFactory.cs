using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Infrastructures.Download;
using Microsoft.Extensions.Options;

namespace CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Download
{
    public class DownloadServiceFactory : IDownloadServiceFactory
    {
        readonly HttpClient _httpClient;
        readonly IOptions<DownloadSettings> _settings;

        public DownloadServiceFactory(HttpClient httpClient, IOptions<DownloadSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public IDownloadService Create()
        {
            return new DownloadService(_httpClient, _settings);
        }
    }
}
