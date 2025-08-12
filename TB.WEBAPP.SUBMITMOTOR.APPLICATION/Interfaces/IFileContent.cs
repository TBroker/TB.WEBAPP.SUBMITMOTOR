namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces
{
    public interface IFileContent
    {
        string FileName { get; }
        string ContentType { get; }
        long Length { get; }

        Task<byte[]> GetBytesAsync();
    }
}