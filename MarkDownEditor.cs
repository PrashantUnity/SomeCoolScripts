static void Main()
{
    var path = @"path\30-Days-SDE-Sheet-Practice";
    var notePath = @"path\Notes.txt";

    var markDownText= new List<string>();
    var parentDirectories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
    foreach (string dir in parentDirectories)
    {
        string rootDirectory = dir;
        string[] Files = Directory.GetFiles(rootDirectory);

        if (Files.Length == 0)
            continue;

        var headerName = rootDirectory.Split('\\').ToArray();
        markDownText.Add("## "+headerName[headerName.Length-1]  );
        int nestesListCounter = 1;
        foreach (string file in Files)
        {
            using(var stramreader = new StreamReader(file))
            {
                var readedLineText = "";
                while((readedLineText =stramreader.ReadLine())!=null)
                {
                    if (readedLineText.StartsWith("# https://leetcode.com"))
                    {
                        var questionName = readedLineText.Split(' ')[1].Split('/').ToArray();
                        markDownText.Add($"   {nestesListCounter++}. [{string.Join(" ",questionName[questionName.Length-2].ToUpper().Split('-'))}]({readedLineText.Split(' ')[1]})");
                    }
                }
            }
        }
    }
    File.WriteAllLines(notePath, markDownText);
}
