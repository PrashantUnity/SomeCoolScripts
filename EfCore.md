## Delete Database data

```cs
var task = await ctx.Challenges.ExecuteDeleteAsync();
```

## Download Array as json file

```cs 
    challenges = await ctx.Challenges.ToArrayAsync();
    string json = JsonSerializer.Serialize(challenges);
    // Invoke JavaScript function to download the JSON file
    await JSRuntime.InvokeVoidAsync("downloadFile", "data.json", json, "application/json");
```

```js
window.downloadFile = (fileName, content, contentType) => {
    const blob = new Blob([content], { type: contentType });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    window.URL.revokeObjectURL(url);
    document.body.removeChild(a);
};
```
## download as image
```cs
// Inside your Blazor component's .razor.cs file
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

public partial class YourComponent : ComponentBase
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; }

    // Sample byte array (replace this with your actual byte array)
    private byte[] imageBytes = GetSampleImageBytes();

    private async Task DownloadImage()
    {
        // Convert byte array to base64 string
        string base64String = Convert.ToBase64String(imageBytes);

        // Invoke JavaScript function to download the image
        await JSRuntime.InvokeVoidAsync("downloadImageFromBase64", base64String);
    }

    // Sample method to generate sample image bytes (replace this with your actual data)
    private static byte[] GetSampleImageBytes()
    {
        // This is just a placeholder. Replace this with your actual byte array.
        return new byte[] { /* your byte array data */ };
    }
}
```
```js
// Inside your Blazor application's JavaScript file (e.g., wwwroot/scripts/custom.js)
window.downloadImageFromBase64 = function (base64String) {
    // Convert base64 string to binary data
    var binaryString = atob(base64String);

    // Create a Uint8Array to hold the binary data
    var byteArray = new Uint8Array(binaryString.length);
    for (var i = 0; i < binaryString.length; i++) {
        byteArray[i] = binaryString.charCodeAt(i);
    }

    // Create a Blob from the binary data
    var blob = new Blob([byteArray], { type: 'image/jpeg' });

    // Create a blob URL for the Blob
    var blobUrl = URL.createObjectURL(blob);

    // Create a temporary anchor element to trigger the download
    var a = document.createElement('a');
    a.href = blobUrl;  // Set the href attribute to the blob URL
    a.download = 'image.jpg';  // Set the download attribute to specify the file name

    // Programmatically trigger a click event on the anchor element
    a.dispatchEvent(new MouseEvent('click'));

    // Clean up
    URL.revokeObjectURL(blobUrl);
};
```
