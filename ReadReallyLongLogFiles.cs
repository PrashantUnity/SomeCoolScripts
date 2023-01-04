using System;
using System.Collections.Generic;
using System.IO;

namespace ReadTestFile
{
    internal class Program
    {
        static string path = @"PutYourPathHere\ReadMe.log";
        const int MaxLine = 1000;
        static int counter = 1;
        static void Main()
        {
            using (var sr = new StreamReader(path))
            {
                string logPath = @"C:\Users\Prashant\Downloads\New folder\ReadTestFile\"; // change to your desire location
                var str = "";
                var list = new List<string>();
                while ((str = sr.ReadLine()) != null)
                {
                    list.Add(str);
                    if (counter % MaxLine == 0)
                    {
                        var tempPath = logPath + counter + ".txt";
                        File.Create(tempPath).Dispose();
                        File.AppendAllLines(tempPath, list);
                        list.Clear();
                    }
                    counter++;
                }
            }
        }
    }
}
