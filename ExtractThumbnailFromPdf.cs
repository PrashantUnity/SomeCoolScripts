using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using System.Drawing.Imaging;


Sample1();
void Sample1()
{
    int desired_dpi = 96;

    string inputPdfPath = @"source\sample.pdf";
    string outputPath = @"dest";


    // https://ghostscript.com/releases/gsdnld.html
    // Install App from above link locate gs dll
    GhostscriptVersionInfo gvi = new GhostscriptVersionInfo(@"C:\Program Files\gs\gs10.01.2\bin\gsdll64.dll");

    using (var rasterizer = new GhostscriptRasterizer())
    {
        rasterizer.Open(inputPdfPath, gvi, false);

        for (var pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
        {
            var pageFilePath = Path.Combine(outputPath, string.Format("Page-{0}.png", pageNumber));

            var img = rasterizer.GetPage(desired_dpi, pageNumber);
            img.Save(pageFilePath, ImageFormat.Png);

            Console.WriteLine(pageFilePath);
            break; // for single page
        }
    }
}
