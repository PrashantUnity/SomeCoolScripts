## How To Display C# string value as Html

> In this Example I Have used Markdig TO generate Html then 

```csharp
using Microsoft.DotNet.Interactive.Formatting;
Formatter.DefaultMimeType ="text/html";

var result = Markdig.Markdown.ToHtml(
"""
### Fenced Code Block
{
  "firstName": "John",
  "lastName": "Smith",
  "age": 25
}
"""); 
result.DisplayAs(Formatter.DefaultMimeType)
```
