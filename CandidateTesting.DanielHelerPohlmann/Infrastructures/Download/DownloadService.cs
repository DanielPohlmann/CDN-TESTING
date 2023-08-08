using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Helpers;
using CandidateTesting.DanielHelerPohlmann.Sources.Messages;
using Microsoft.Extensions.Options;

namespace CandidateTesting.DanielHelerPohlmann.Infrastructures.Download
{
    public class DownloadService : IDownloadService
    {
        readonly HttpClient _httpClient;
        readonly DownloadSettings _settings;

        public DownloadService(HttpClient httpClient, IOptions<DownloadSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<DownloadFileResponse> DownloadFileAsync(DownloadFileRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var fileNamePath = $"{_settings.InputPath}{request.FileName}";
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, request.Uri);
                var response = await _httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead);
                var totalBytes = response.Content.Headers.ContentLength;
                var bytesRead = 0L;
                var buffer = new byte[_settings.BufferLength];
                var filePathTmp = fileNamePath + ".tmp";

                FileHelper.CreatePathFromFilePath(fileNamePath);
                FileHelper.CreatePathFromFilePath(filePathTmp);

                FileHelper.DeleteFilePathIfExist(filePathTmp);

                using (var fileStream = new FileStream(filePathTmp, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                using (var contentStream = await response.Content.ReadAsStreamAsync())
                {
                    var totalRead = 0L;
                    var readCount = 0;
                    while ((readCount = await contentStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, readCount);
                        bytesRead += readCount;
                        totalRead += readCount;
                        
                        Console.WriteLine(string.Format(Message.Downloaded, bytesRead, totalBytes, ((double)bytesRead / totalBytes ?? 0).ToString("P2")));
                        ////Console.SetCursorPosition(0, Console.CursorTop - 1);
                    }
                }

                FileHelper.DeleteFilePathIfExist(fileNamePath);
                File.Move(filePathTmp, fileNamePath);

                return new DownloadFileResponse(
                       true,
                       string.Format(Message.DownloadSucess, fileNamePath, totalBytes)
                   );
            }
            catch (Exception ex)
            {
                return new DownloadFileResponse(
                   false,
                   ex.Message
               );
            }
        }
    }
}
