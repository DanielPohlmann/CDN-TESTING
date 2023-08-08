namespace CandidateTesting.DanielHelerPohlmann.Core.DomainObjects
{
    public class DownloadFileResponse
    {
        public DownloadFileResponse(bool isSuccess, string? message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; }
        public string? Message { get; }
    }
}
