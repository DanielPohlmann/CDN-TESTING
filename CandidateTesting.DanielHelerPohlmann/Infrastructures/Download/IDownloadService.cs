using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;

namespace CandidateTesting.DanielHelerPohlmann.Infrastructures.Download
{
    public interface IDownloadService
    {
        Task<DownloadFileResponse> DownloadFileAsync(DownloadFileRequest request, CancellationToken cancellationToken);
    }
}
