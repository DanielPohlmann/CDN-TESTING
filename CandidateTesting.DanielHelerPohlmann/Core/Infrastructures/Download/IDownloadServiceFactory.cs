using CandidateTesting.DanielHelerPohlmann.Infrastructures.Download;

namespace CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Download
{
    public interface IDownloadServiceFactory
    {
        IDownloadService Create();
    }
}
