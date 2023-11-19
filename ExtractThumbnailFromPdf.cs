// requirement
// NuGet\Install-Package Ghostscript.NET
// https://ghostscript.com/releases/gsdnld.html
// Bear in mind this .Net FrameWork Code Not Intended for .Net Core Or Other Version

using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using System.Drawing.Imaging;

var parentPath = @"D:\dir";

ReadAllFilles(parentPath);
void ReadAllFilles(string path)
{
    var ls = Directory.GetFiles(path);
    foreach (var item in ls)
    {
        if(item.EndsWith(".pdf"))
        {   
            try
            {
                Extract(item, Directory.GetParent(item).FullName, Path.GetFileNameWithoutExtension(item));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); 
            }
            break;
        }
    }
}
 
void Extract(string inputPdfPath,string outputPath,string name)
{
    int desired_dpi = 96; 
    GhostscriptVersionInfo gvi = new GhostscriptVersionInfo(@"C:\Program Files\gs\gs10.01.2\bin\gsdll64.dll");

    using (var rasterizer = new GhostscriptRasterizer())
    {
        rasterizer.Open(inputPdfPath, gvi, false);

        for (var pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
        {
            var pageFilePath = Path.Combine(outputPath, string.Format("{0}.png", name));

            var img = rasterizer.GetPage(desired_dpi, pageNumber);
            img.Save(pageFilePath, ImageFormat.Png);

            Console.WriteLine(pageFilePath);
            break;
        }
    }
}
