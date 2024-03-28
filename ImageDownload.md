# Download Image

```js
window.downloadImage = (base64Image, fileName) => {
    const byteCharacters = atob(base64Image);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray]);
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.style.display = 'none';
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    window.URL.revokeObjectURL(url);
};

window.generateImage = function (id) {
    html2canvas(document.getElementById(id)).then(function (canvas) {
        var image = canvas.toDataURL('image/png');

        var a = document.createElement('a');
        a.href = image;
        a.download = 'generated_image.png';
        a.click();
    });
}
 
window.downloadImageUrl = (imageUrl, fileName) => {
    fetch(imageUrl)
        .then(response => response.blob())
        .then(blob => {
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.style.display = 'none';
            a.href = url;
            // Set the file name
            a.download = fileName;
            document.body.appendChild(a);
            a.click();
            window.URL.revokeObjectURL(url);
        })
        .catch(error => console.error('Error downloading image:', error));
};
```
## Library for html2canvass to image
```html
<script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.3.2/html2canvas.min.js"></script>
```

## Skbitmap To Img
```cs
byte[] ConvertSKBitmapToByteArray(SKBitmap bitmap, SKEncodedImageFormat format)
{
    using (var image = SKImage.FromBitmap(bitmap))
    using (var data = image.Encode(format, 100))
    {
        return data.ToArray();
    }
}
var arr = bmp.Bytes;// ConvertSKBitmapToByteArray(bmp, SKEncodedImageFormat.Png);
await JSRuntime.InvokeVoidAsync("downloadImage", Convert.ToBase64String(arr), "image.png");
await JSRuntime.InvokeVoidAsync("downloadImageUrl", $"https://picsum.photos/200/300", "image.png");
await JSRuntime.InvokeVoidAsync("generateImage", IDOfImageToDownload); // element as canvass as image
```

## Skiasharpcanvass view customm size
```razor
<SKCanvasView @ref="canvasReference"
              OnPaintSurface="@OnPaintSurface"
              style="@($"height: {Height}px; width: {Width}px;")"
              @onclick="ButtonClicked" id="@id"/>

```
