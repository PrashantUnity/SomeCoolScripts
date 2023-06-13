using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using SkiaSharp;
// Now You Can Create Resume Easily Using This Template 
// For It Uses QuestPDF Library available on Nuget 
/*
// Package Manager
Install-Package QuestPDF

// .NET CLI
dotnet add package QuestPDF

// Package reference in .csproj file
<PackageReference Include="QuestPDF" Version="2022.12.1" />
*/
// For More in Depth Or Custom Changes Follow This Link
// https://www.questpdf.com/introduction.html
QuestPDF.Settings.License = LicenseType.Community;
Document.Create(container =>
{
    container
    .Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Margin(1, Unit.Centimetre);
        page.PageColor(Colors.White);
        page.MarginTop(0);

        page.DefaultTextStyle(TextStyle
            .Default
            .FontFamily(Fonts.Calibri)
            .Fallback(x => x.FontFamily("Segoe UI Emoji")));

        page.Header().Element(Nav);
        page.Content().Element(Body);
        page.Footer().Element(Foot);
    });
}).ShowInPreviewer();

void Foot(IContainer obj)
{
    obj.AlignCenter()
            .Text(x =>
            {
                //x.Span("Page ");
                //x.CurrentPageNumber();
            });
}

void Body(IContainer obj)
{
    obj
        .PaddingVertical(.3f, Unit.Centimetre)
        .Column(column =>
        {
            column.Spacing(5);

            column.Item().Element(x => WorkExperience(x));
            column.Item().Element(x => PastProject(x));
            column.Item().Element(x => TechnicalSkill(x));
            column.Item().Element(x => TrainingAndInternship(x));
            column.Item().Element(x => Education(x));
            column.Item().Element(x => Certification(x));
        });
}
void WorkExperience(IContainer container)
{
    container.Column
        (column =>
        {
            column.Item()
            .AlignLeft()
            .Text("Work Experience")
            .FontSize(15);

            column.Item().PaddingVertical(0.1f).LineHorizontal(1).LineColor(Colors.Black);

            column.Item()
            .Element(x => HeadLeftRight(x, "Programmer Analyst at Cognizant Technology Solutions India Pvt Ltd", "November 2021-Present"));
            List<string> experience = new List<string>()
            {
                        "Development,Testing,Analysis, bug fixing, documenting the current functionality and presenting it to the client",
                        "Enhanced a C# .NET Desktop Application built in .NET Framework which adjusts the rates of the Commodities present in file.",
                        "Proposed and successfully implemented feature to improve the code flow.",
                        "Developed C# .NET Web Application build in .NET framework as per client requirement",
                        "Performed Code analysis and feature enhancement.",
                        "Design and Delivery of project in timely manner with regular presentation to client on the project progress"
            };
            foreach (var item in experience)
            {
                column.Item()
                .PaddingLeft(3)
                .Text("👉  " + item)
                .FontSize(9);
            }
        });
}

void PastProject(IContainer container)
{
    container.Column(column =>
    {
        column.Item()
            .AlignLeft()
            .Text("Past Projects")
            .FontSize(15);

        column.Item().PaddingVertical(0.1f).LineHorizontal(1).LineColor(Colors.Black);

        column.Item()
        .Element(x => HeadLeftRight(x, "Oracle MetaBackup", "January 23 - March 23"));
        column.Item()
        .PaddingLeft(3)
        .Text("👉  " + "Developed a Meta Data backup tool using Windows Forms and .NET 7, allowing users to easily and securely back up important metadata for their applications |Language : C# , Tools : Visual Studio 2022")
        .FontSize(9);

        column.Item()
        .Element(x => HeadLeftRight(x, "Text Reader", "December 2022"));
        column.Item()
        .PaddingLeft(3)
        .Text("👉  " + "Developed a custom text reader using C# and .NET, capable of efficiently reading and processing large text files, improving productivity and reducing errors for users dealing with large volumes of text data |Language : C# , Tools : Visual Studio 2022")
        .FontSize(9);

        column.Item()
        .Element(x => HeadLeftRight(x, "Email Extractor", "October 22 - November 22"));
        column.Item()
        .PaddingLeft(3)
        .Text("👉  " + "Built an email extractor using C# and .NET, leveraging advanced data processing techniques to extract relevant information from large volumes of emails |Language : C# , Tools : Visual Studio 2022")
        .FontSize(9);

        column.Item()
        .Element(x => HeadLeftRight(x, "Web API", "April 22 - August 22"));
        column.Item()
        .PaddingLeft(3)
        .Text("👉  " + "Designed and implemented a .NET Core Web API to provide a lightweight, cross-platform solution for building HTTP-based web services —Language : C# , Tools : Visual Studio 2022")
        .FontSize(9);
    });
}

void TechnicalSkill(IContainer container)
{
    container.Column
        (column =>
        {
            column.Item()
            .AlignLeft()
            .Text("Technical Skills")
            .FontSize(15);

            column.Item().PaddingVertical(0.1f).LineHorizontal(1).LineColor(Colors.Black);
            var lsSkill = new List<List<string>>()
            {
                        new List<string>
                        {
                            "C#",
                            ".NET Core",
                            ".NET Core Web API",
                            ".NET Framework",
                        },
                        new List<string>
                        {
                            "Entity Framework",
                            "Razer Pages",
                            "Blazor Server",
                            "Blazor Webassembly"
                        },
                        new List<string>
                        {
                            "Visual Studio",
                            "Unity Engine",
                            "LINQ Pad",
                            "Blender"
                        },
                        new List<string>
                        {
                            "Procreate",
                            "Data Structure",
                            "Algorithms",
                            "OOPS"
                        },
            };

            foreach (var skill in lsSkill)
            {
                column.Item()
                    .Row(row =>
                    {
                        foreach (var item in skill)
                        {
                            row.AutoItem().PaddingHorizontal(3)
                            .PaddingVertical(2)
                            .Element(x => Badge(x, item));
                        }
                    });
            }

        });
}
void TrainingAndInternship(IContainer container)
{
    container.Column(column =>
    {
        column.Item()
            .AlignLeft()
            .Text("Training And Internships")
            .FontSize(15);

        column.Item().PaddingVertical(0.1f).LineHorizontal(1).LineColor(Colors.Black);
        column.Item()
        .Text("👉  " + "Trained on C# by Cognizant from August 2021 - October 2021 CSD Program")
        .FontSize(10);

    });
}
void Education(IContainer container)
{

    container.Column(column =>
    {
        column.Item()
            .AlignLeft()
            .Text("Education ")
            .FontSize(15);

        column.Item().PaddingVertical(0.1f).LineHorizontal(1).LineColor(Colors.Black);
        column.Item().Element(x => HeadLeftRight(x, "Haldia Institute of Technology, Haldia", "2017 - 2021"));
        column.Item()
        .PaddingLeft(3)
        .Text("👉  " + "Bachelor of Technology in Electrical Engineering. CGPA: 8.7/10.00")
        .FontSize(10);

        column.Item().Element(x => HeadLeftRight(x, "Bijendra Public School, Purnea", "2016"));
        column.Item()
        .PaddingLeft(3)
        .Text("👉  " + "12th Standard CBSE. Percentage:65")
        .FontSize(10);


        column.Item().Element(x => HeadLeftRight(x, "Indian Public School, Purnea", "2014"));
        column.Item()
        .PaddingLeft(3)
        .Text("👉  " + "10th Standard CBSE. CGPA: 9.8/10.00")
        .FontSize(10);
    });
}
void Certification(IContainer container)
{
    container.Column(column =>
    {
        column.Item()
            .AlignLeft()
            .Text("Certifications/Achievements")
            .FontSize(15);

        column.Item().PaddingVertical(0.1f).LineHorizontal(1).LineColor(Colors.Black);

        var ls = new List<string>()
                {
                    "Accquired 2nd Rank in Techtory’s Dot Net Backend Developer: 18 th February 2023 to 19th February 2023",
                    "Microsoft Certified: Power Platform Fundamentals February 21, 2022. Certification number : I151-0030",
                    "Microsoft Certified: Dynamics 365 Fundamentals (CRM)February 12, 2022. Certification number : I140-6374",
                    "C# Advanced Topics: Prepare for Technical Interviews (Udemy)",
                    "C# Intermediate: Classes, Interfaces and OOP (Udemy "
                };
        foreach (var item in ls)
        {
            column.Item()
            .PaddingLeft(3)
            .Text("👉  " + item)
            .FontSize(10);
        }
    });
}

void HeadLeftRight(IContainer container, string head, string date)
{
    container.Row(row =>
    {
        row.RelativeItem()
        .AlignLeft()
        .Text(head)
        .FontSize(10)
        .Bold();

        row.AutoItem()
        .AlignRight()
        .Text(date)
        .FontSize(10)
        .Bold()
        .Italic();
    });
}

void Badge(IContainer container, string item)
{

    container
    .Background(Colors.Grey.Lighten2)
    .MinimalBox()
    .Layers(layers =>
    {
        layers.Layer()
                .Canvas((canvas, size) =>
                {
                    DrawRoundedRectangle(Colors.White, false);
                    DrawRoundedRectangle(Colors.Blue.Darken2, true);

                    void DrawRoundedRectangle(string color, bool isStroke)
                    {
                        using var paint = new SKPaint
                        {
                            Color = SKColor.Parse(color),
                            IsStroke = isStroke,
                            StrokeWidth = 2,
                            IsAntialias = true
                        };

                        canvas.DrawRoundRect(0, 0, size.Width, size.Height, 20, 20, paint);
                    }
                });

        layers.PrimaryLayer()
            .PaddingHorizontal(10)
            .Text(item)
            .FontSize(10)
            .FontColor(Colors.Blue.Darken2)
            .SemiBold();

    });
}

void Nav(IContainer container)
{
    container
        .Column(column =>
        {
            column.Item().Element(x => Name(x));
            column.Item().Element(x => Contact(x));
            column.Item().PaddingVertical(2).LineHorizontal(3).LineColor(Colors.Black);
        });
}
void Name(IContainer container)
{
    container
        .AlignCenter()
        .Text($"Prashant Priyadarshi")
        .FontSize(35)
        .FontFamily(Fonts.Calibri);
}
void Contact(IContainer container)
{
    int padding = 3;
    float scale = 0.5f;
    int seperator = 7;
    float fontsize = 20;
    container
        .Scale(scale)
        .Row(row =>
        {
            row.RelativeItem()
            .AlignCenter()
            .PaddingHorizontal(padding)
            .Text("776487XXXX | Example@mail.com | github.com/Prashantunity")
            .FontSize(fontsize);

        });

}
