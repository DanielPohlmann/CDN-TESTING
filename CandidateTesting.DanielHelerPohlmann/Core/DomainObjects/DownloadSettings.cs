namespace CandidateTesting.DanielHelerPohlmann.Core.DomainObjects
{
    public class DownloadSettings
    {
        public const string Download = "DownloadSettings";

        public long BufferLength { get; set; } = 1024;

        public string InputPath { get; set; } = "./input/";
    }
}
