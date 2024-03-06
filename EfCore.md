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
