# HttClient

## fetch content from url

```cs
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization; 
using (var client = new HttpClient())
{
    client.BaseAddress=new Uri("https://skyline.github.com/prashantunity/");
    var content =await  client.GetStringAsync("2022.json");
} 
```

## Deserialize data from json or string

```cs
// Root is Class Model of the incoming data
var data =JsonSerializer.Deserialize<Root>(content);
```

## Program.cs

```csharp
builder.Services.AddHttpClient("MyClient",client=>
{
    client.BaseAddress = new Uri("https://todoapi-36862104.azurewebsites.net/");
    client.DefaultRequestHeaders.Add("X-API-KEY", settings.ApiKey);
});
```

## Index.cshtml.cs

```c
var uri = $"api/TodoItems?userName={Uri.EscapeDataString(SearchString)}&page=1&pageSize=10";
var response = await _httpClient.GetAsync(uri);
```

## Create.cshtml.cs

```csharp
var jsonData = JsonConvert.SerializeObject(Item);
var data = new StringContent(jsonData,Encoding.UTF8,"application/json");
var response = await _httpClient.PostAsync("api/TodoItems",data);
```

## Delete.cshtml.cs

```c
ViewData["code"] = $"<i>{(int)response.StatusCode}</i>";
```

## Delete.cshtml

```csharp
@if(ViewData["code"] != null)
{
    <h2>
        The status code of the response is @Html.Raw(ViewData["code"])
    </h2>
}
```
