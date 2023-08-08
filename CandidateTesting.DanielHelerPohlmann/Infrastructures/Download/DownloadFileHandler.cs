using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Download;
using MediatR;

namespace CandidateTesting.DanielHelerPohlmann.Infrastructures.Download
{
    public class DownloadFileHandler : IRequestHandler<DownloadFileRequest, DownloadFileResponse>
    {
        private readonly IDownloadService _downloadService;

        public DownloadFileHandler(IDownloadServiceFactory downloadServiceFactory)
        {
            _downloadService = downloadServiceFactory.Create();
        }

        public async Task<DownloadFileResponse> Handle(DownloadFileRequest request, CancellationToken cancellationToken)
        {
            return await _downloadService.DownloadFileAsync(request, cancellationToken);
        }
    }
}
