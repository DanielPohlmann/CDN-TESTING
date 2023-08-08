namespace CandidateTesting.DanielHelerPohlmann.Core.Helpers
{
    public static class FileHelper
    {
        public static void CreatePathFromFilePath(string filePath)
        {
            var directoryPath = Path.GetDirectoryName(filePath);
            if (directoryPath is not null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public static void DeleteFilePathIfExist(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
