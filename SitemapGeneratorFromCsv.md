## Read Csv File
> Keep in mind that I have Hard Coded The Position of Data
```csharp
public class ReadSitemapCsv
{
    public static List<Url> Read(string path)
    {
        var ls = File.ReadAllLines(path)
        .Select(x=>x.Split(',').ToList())
        .Select(x=>new Url{Loc = x[0],Lastmod=x[1],Changefreq=x[2],Priority=Convert.ToDouble(x[3])}).ToList(); 
        return ls;
    }
}
```
## Converting C# Class To SiteMap But It Screw Something so.

```csharp
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => Encoding.UTF8;
}
public class ToText
{
    public static List<string> Serialize(Urlset urlsets)
    {
        var ls = new List<string>();
        var serializer = new XmlSerializer(typeof(Urlset));
        using (var stream = new Utf8StringWriter())
        {
            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true }))
            {
                serializer.Serialize(writer, urlsets);
            }
            //Console.WriteLine(stream.ToString());
            ls.Add(stream.ToString());
        }
        return ls;
    }
}
```
## patch For Above 
```csharp
// implement Storing of url Sets in One Csv File Github
string path = @"Result.xml";
string csvPath =@"SitemapCsv.csv";
var one = new Urlset()
{ 
    Xmlns ="http://www.sitemaps.org/schemas/sitemap/0.9",
    Urls = ReadSitemapCsv.Read(csvPath)
}; 

var ls = ToText.Serialize(one); 
File.WriteAllLines(path,ls);
ls.Clear();

try
{
    var arr = File.ReadAllLines(path);
    arr[1] ="<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">";
    File.WriteAllLines(Path.Combine(Directory.GetCurrentDirectory() +"FinalResult.xml"),arr);
}
catch
{
    Console.WriteLine("You are not doing your work Seriously");
}


```
```csharp
using System.Xml.Serialization;  
[XmlRoot(ElementName="url")]
public class Url
{
    [XmlElement(ElementName = "loc")]
    public string Loc { get; set; }

    [XmlElement(ElementName = "lastmod")]
    public string Lastmod { get; set; }

    [XmlElement(ElementName = "changefreq")]
    public string Changefreq { get; set; }

    [XmlElement(ElementName = "priority")]
    public double Priority { get; set; }
}
```
```csharp
using System.Xml.Serialization; 
[XmlRoot(ElementName="urlset")]
public class Urlset
{
    [XmlElement(ElementName = "url")]
    public List<Url> Urls { get; set; }

    [XmlAttribute(AttributeName = "xmlns")]
    public string Xmlns { get; set; }
}
```
