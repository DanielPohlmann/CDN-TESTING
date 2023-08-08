using Bogus;
using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Models;

namespace CandidateTesting.DanielHelerPohlmann.Tests.Builders
{
    public class CdnServiceTestObjectBuilder
    {
        public ArgsParamSetup? ArgsParamSetup { get; }
        public DownloadFileRequest? DownloadFileRequest { get; }
        public string? FilePath { get; }
        public string? PatchFileOutput { get; }
        public TimeSpan? TotalTime { get; }
        public string? MessageResult { get; }

        public CdnServiceTestObjectBuilder(){}

        public CdnServiceTestObjectBuilder(
            ArgsParamSetup? argsParamSetup,
            DownloadFileRequest? downloadFileRequest,
            string? filePath,
            string? patchFileOutput,
            TimeSpan? totalTime,
            string? messageResult)
        {
            ArgsParamSetup = argsParamSetup;
            DownloadFileRequest = downloadFileRequest;
            FilePath = filePath;
            PatchFileOutput = patchFileOutput;
            TotalTime = totalTime.GetValueOrDefault();
            MessageResult = messageResult;
        }

        public class Builder
        {
            private readonly Faker _faker = new Faker();
            private ArgsParamSetup? _argsParamSetup;
            private DownloadFileRequest? _downloadFileRequest;
            private string? _filePath;
            private string? _patchFileOutput;
            private TimeSpan? _totalTime;
            private string? _messageResult;

            public Builder WithDefaultValues()
            {
                var urlFilePath = $"{_faker.Internet.UrlWithPath()}/{_faker.System.FileName()}";
                _argsParamSetup = new ArgsParamSetup(new[] { urlFilePath, $"{_faker.System.DirectoryPath()}/{_faker.System.FileName()}" });
                _downloadFileRequest = new DownloadFileRequest(urlFilePath);
                _filePath = $"{_faker.System.DirectoryPath()}/{_faker.System.FileName()}";
                _patchFileOutput = $"{_faker.System.DirectoryPath()}/{_faker.System.FileName()}";
                _totalTime = TimeSpan.FromSeconds(_faker.Random.Double());
                _messageResult = _faker.Random.String2(5);
                return this;
            }

            public Builder WithArgsParamSetup(ArgsParamSetup argsParamSetup)
            {
                _argsParamSetup = argsParamSetup;
                return this;
            }

            public Builder WithDownloadFileRequest(DownloadFileRequest downloadFileRequest)
            {
                _downloadFileRequest = downloadFileRequest;
                return this;
            }

            public Builder WithFilePath(string filePath)
            {
                _filePath = filePath;
                return this;
            }

            public Builder WithPatchFileOutput(string patchFileOutput)
            {
                _patchFileOutput = patchFileOutput;
                return this;
            }

            public Builder WithTotalTime(TimeSpan totalTime)
            {
                _totalTime = totalTime;
                return this;
            }

            public Builder WithMessageResult(string messageResult)
            {
                _messageResult = messageResult;
                return this;
            }

            public CdnServiceTestObjectBuilder Build()
            {
                return new CdnServiceTestObjectBuilder(_argsParamSetup, _downloadFileRequest, _filePath, _patchFileOutput, _totalTime.GetValueOrDefault(), _messageResult);
            }
        }
    }
}
