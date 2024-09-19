namespace CofiApp.Contracts.Files;

public record FileResponse
{
    public FileResponse(Stream stream, string contentType)
    {
        Stream = stream;
        ContentType = contentType;
    }

    public Stream Stream { get; set; }

    public string ContentType { get; set; }
}
