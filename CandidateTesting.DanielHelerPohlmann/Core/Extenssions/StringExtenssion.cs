using CandidateTesting.DanielHelerPohlmann.Core.DomainObjects;
using System.IO;
using System.Text.RegularExpressions;

namespace CandidateTesting.DanielHelerPohlmann.Core.Extenssions
{
    public static class StringExtenssion
    {
        public static string? Get(this string[] args, int postion) =>
            args is null || args.Length <= postion ? null : args[postion];

        public static bool UrlIsFileValid(this string url)
        {
            var create = Uri.TryCreate(url, UriKind.Absolute, out Uri? result);

            return create ? (result?.LocalPath.IsValidPathAndFileName() ?? false) : create;
        }

        public static bool IsValidPathAndFileName(this string path)
        {
            string pattern = @"[^\\/]+\.\w+$";
            Match match = Regex.Match(path, pattern);
            return match.Success;
        }

        public static void IsValidPathThrow(this string path, string message)
        {
            if (string.IsNullOrWhiteSpace(path) || !path.IsValidPathAndFileName())
            {
                throw new FileException(message ?? string.Empty);
            }
        }
    }
}
