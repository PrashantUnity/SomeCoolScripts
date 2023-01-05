using System.IO;
using System;

namespace ForGeneral
{
    internal class MoveFiles
    {
        public static void Move()
        {
            try
            {
                // setting path 
                string destinationDirectory = @"C:\Users\priya\OneDrive\Desktop\desti\";
                string sourceDirectory = @"C:\Users\priya\OneDrive\Desktop\source";

                // getting all subdirectory of sourceDirectory
                string[] dirs = Directory.GetDirectories(sourceDirectory, "*", SearchOption.TopDirectoryOnly);
                

                foreach (string dir in dirs)
                {
                    string rootDirectory = dir;
                    string[] Files = Directory.GetFiles(rootDirectory);

                    if (Files.Length == 0)
                        continue;

                    // Moving each file one by one
                    foreach (string file in Files)
                    {
                        Console.WriteLine($"Currently Moving {file}");
                        File.Move(file, $"{destinationDirectory}{Path.GetFileName(file)}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
