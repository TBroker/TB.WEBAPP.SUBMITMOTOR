using System.Text;
using System.Text.RegularExpressions;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Validators
{
    public static class FileValidator
    {
        private static readonly string[] AllowedExtensions = [".png", ".jpg", ".jpeg", ".pdf"];
        private const long MaxFileSizeInBytes = 10 * 1024 * 1024; // 10 MB

        private static readonly string JavaScriptPattern = @"[Jj][Aa][Vv][Aa][Ss][Cc][Rr][Ii][Pp][Tt]|<script>|</script>|[Ff][Uu][Nn][Cc][Tt][Ii][Oo][Nn]";
        private static readonly string FormPattern = @"[Ff][Oo][Rr][Mm]|[Mm][Ee][Tt][Hh][Oo][Dd]=(""GET""|""POST"")|[Tt][Yy][Pp][Ee]=""SUBMIT""";

        public static bool IsValidSize(IFileContent file) =>
            file.Length <= MaxFileSizeInBytes;

        public static bool IsValidExtension(IFileContent file)
        {
            var extension = Path.GetExtension(file.FileName ?? "").ToLowerInvariant();
            return AllowedExtensions.Contains(extension);
        }

        public static async Task<bool> IsValidContentAsync(IFileContent file)
        {
            var content = Encoding.UTF8.GetString(await file.GetBytesAsync());

            return !(Regex.IsMatch(content, JavaScriptPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline) ||
                     Regex.IsMatch(content, FormPattern, RegexOptions.IgnoreCase | RegexOptions.Multiline));
        }

        public static async Task<bool> IsValidFileAsync(IFileContent file)
        {
            return IsValidSize(file) &&
                   IsValidExtension(file) &&
                   await IsValidContentAsync(file);
        }
    }
}
