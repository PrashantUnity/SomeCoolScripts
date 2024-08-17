# Video Stream Over Internet

```csharp
[HttpGet("{fileName}")]
public IActionResult GetMedia(string fileName)
{
    var filePath = _mediaService.GetMediaFilePath(fileName);
    if (!System.IO.File.Exists(filePath))
    {
        return NotFound();
    }

    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read); 
    var mimeType = "video/mp4";  


    var fileInfo = new FileInfo(filePath);

    long totalLength = fileInfo.Length;
    long start = 0;
    long end = totalLength - 1;

    if (Request.Headers.ContainsKey("Range"))
    {
        var range = Request.Headers["Range"].ToString();
        var rangeSplit = range.Replace("bytes=", "").Split('-');
        start = long.Parse(rangeSplit[0]);

        if (rangeSplit.Length > 1 && !string.IsNullOrEmpty(rangeSplit[1]))
            end = long.Parse(rangeSplit[1]);
    }

    var contentLength = end - start + 1;
    var responseStream = new MemoryStream();
    fileStream.Seek(start, SeekOrigin.Begin);
    fileStream.CopyTo(responseStream, (int)contentLength);
    responseStream.Seek(0, SeekOrigin.Begin);

    Response.StatusCode = (int)HttpStatusCode.PartialContent;
    Response.Headers.AcceptRanges = "bytes";
    Response.Headers.ContentLength = contentLength;
    Response.Headers.ContentType = "video/mp4";
    Response.Headers.ContentRange = $"bytes {start}-{end}/{totalLength}";

    return new FileStreamResult(responseStream, mimeType);
}

```
