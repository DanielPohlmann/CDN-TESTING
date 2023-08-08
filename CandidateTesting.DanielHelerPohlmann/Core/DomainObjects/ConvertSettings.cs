namespace CandidateTesting.DanielHelerPohlmann.Core.DomainObjects
{
    public class ConvertSettings
    {
        public const string Convert = "Convert";

        public long ProgressLineLength { get; set; } = 1;
        public string InputPath { get; set; } = "./input/";
    }
}
