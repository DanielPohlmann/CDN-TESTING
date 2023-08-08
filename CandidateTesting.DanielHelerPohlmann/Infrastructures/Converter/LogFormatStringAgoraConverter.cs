using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using CandidateTesting.DanielHelerPohlmann.Core.Helpers;
using CandidateTesting.DanielHelerPohlmann.Core.Infrastructures.Converter;
using CandidateTesting.DanielHelerPohlmann.Sources.Messages;
using CandidateTesting.DanielHelerPohlmann.Sources.Templates;
using Microsoft.Extensions.Options;

namespace CandidateTesting.DanielHelerPohlmann.Infrastructures.Converter
{
    public class LogFormatStringAgoraConverter : LogConverterBase
    {
        readonly ConvertSettings _settings;
        public LogFormatStringAgoraConverter(IOptions<ConvertSettings> settings)
        {
            _settings = settings.Value;
            lineCount = 0;
        }

        public override ConvertResult Convert(
            string inputFileName,
            string outputFilePath
            )
        {
            try
            {
                FileHelper.CreatePathFromFilePath(outputFilePath);
                WriteAndReadFile(inputFileName, outputFilePath);
                return new ConvertResult(true,
                    lineCount,
                    string.Format(Message.SucessConvert, lineCount)
                    );
            }
            catch (Exception ex)
            {
                return new ConvertResult(false,
                    lineCount,
                    string.Format(Message.ErrorConvert, lineCount, ex.Message)
                    );
            }
        }

        private long lineCount;

        private void WriteAndReadFile(
            string inputFileName,
            string outputFilePath)
        {
            using (StreamReader reader = new StreamReader($"{_settings.InputPath}{inputFileName}"))
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                string convertedHeader = ConvertHeader();

                writer.WriteLine(convertedHeader);

                while (!reader?.EndOfStream ?? false)
                {
                    lineCount++;

                    string line = reader?.ReadLine() ?? string.Empty;

                    string convertedLine = ConvertLineBody(line, ref lineCount);

                    if (!string.IsNullOrWhiteSpace(convertedLine))
                        writer.WriteLine(convertedLine);

                    LogProgress();
                }
            }
        }

        private void LogProgress()
        {
            if (lineCount % _settings.ProgressLineLength <= 0)
            {
                Console.WriteLine(string.Format(Message.ProgressConvert, lineCount));
                ////Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }

        private string ConvertHeader()
        {
            var date = DateTime.Now.ToString("G");
            return string.Format(TemplateLog.HeaderAgoraTemplate1_0, date);
        }

        private string ConvertLineBody(string input, ref long index)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            string[] fields = input.Split('|');

            if (fields.Length < 5)
            {
                throw new InvalidOperationException(string.Format(Message.InvalidLogLineFormat, index));
            }

            string status = fields[1];
            string request = fields[3].TrimStart('"').Split(' ')[0];
            string path = fields[3].TrimStart('"').Split(' ')[1];
            double size = double.Parse(fields[4].TrimEnd('"').Replace(".", ","));
            string responseTime = fields[0];
            string cacheStatus = fields[2];
            return string.Format(TemplateLog.BodyAgoraTemplate1_0, request, status, path, (long)size, responseTime, cacheStatus);
        }
    }
}
