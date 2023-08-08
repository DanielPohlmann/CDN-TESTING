using CandidateTesting.DanielHelerPohlmann.Core.Extenssions;
using CandidateTesting.DanielHelerPohlmann.Core.Resources;
using MediatR;

namespace CandidateTesting.DanielHelerPohlmann.Core.DomainObjects
{
    public class DownloadFileRequest : IRequest<DownloadFileResponse>
    {
        public DownloadFileRequest(string? url)
        {
            Uri = !string.IsNullOrWhiteSpace(url) ? new Uri(url) : throw new FileException(Resource.InvalidURL);

            var fileName = url.IsValidPathAndFileName() ? Path.GetFileName(Uri.LocalPath) : throw new FileException(Resource.InvalidFileURL);

            FileName = fileName;
        }

        public Uri Uri { get; }
        public string FileName { get; }
    }
}
