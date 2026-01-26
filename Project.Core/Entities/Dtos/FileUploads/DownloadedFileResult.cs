namespace Project.Core.Entities.Dtos.FileUploads;

public class DownloadedFileResult
{
    public string FileName { get; set; }

    public string ContentType { get; set; }

    public byte[] Content { get; set; }
}