using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;

namespace TB.WEBAPP.SUBMITMOTOR.Adapters
{
    public class FormFileAdapter(IFormFile formFile) : IFileContent
    {

        private readonly IFormFile _formFile = formFile;

        public string FileName => _formFile.FileName;
        public string ContentType => _formFile.ContentType;
        public long Length => _formFile.Length;

        public async Task<byte[]> GetBytesAsync()
        {
            using var ms = new MemoryStream();
            await _formFile.CopyToAsync(ms);
            return ms.ToArray();
        }

    }
}
