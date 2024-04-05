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
