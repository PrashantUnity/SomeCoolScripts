using System;
using System.Collections.Generic; 
using System.IO;
using System.Linq; 
namespace Renamer
{
	public class Rename
	{
		public static void Main()
		{
			Console.WriteLine("Enter File extension format : Like *.mkv,");
			var format = Console.ReadLine();

			// directory where all files are located Location
			Console.WriteLine("directory where all files are located Location full path");
			var dir = Console.ReadLine();



			// text to be removed
			Console.WriteLine("Text To Be removed/replaced");
			var replace = Console.ReadLine();

			Console.WriteLine("Text Place Holder");
			var placeholder = Console.ReadLine();


			var allFiles = Directory.GetFiles(dir, $"{format}").Select(Path.GetFileName);

			foreach (var i in allFiles)
			{
				if (!File.Exists(Path.Combine(dir, i.Replace($"{replace}", $"{placeholder}"))))
				{
				    File.Move(Path.Combine(dir, i), Path.Combine(dir, i.Replace($"{replace}", $"{placeholder}")));
				}
			} 
		}
	}
}
