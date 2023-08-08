namespace CandidateTesting.DanielHelerPohlmann.Core.DomainObjects
{
    public class ConvertResult
    {
        public ConvertResult(bool isSuccess, long line, string? message) 
        {
            IsSuccess = isSuccess;
            Message = message;
            LinesProcessed = line;
        }

        public bool IsSuccess { get; }
        public long LinesProcessed { get; }
        public string? Message { get; }
    }
}
