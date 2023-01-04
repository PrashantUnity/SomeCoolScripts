using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JatinChadha
{
    class Program
    {
        static List<string> list = new List<string>();
        static string input = @"path\Input.txt";
        static string output = @"path\Output.txt";
        static string FinalEmails = @"path\FinalEmails.txt";
        static char[] seperator = new char[] { ' ', ',', '&', ';' ,'>' ,':' ,'"' };
        static string excelPath = @"path\Email.xlsx";
        static void Main()
        {
            Function();
            FinalOutPut();
            Emails();
        }
        private static void Emails()
        {
            var hash = new HashSet<string>();
            string line = "";
            using (StreamReader sr = new StreamReader(output))
            {
                var str = "";
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.EndsWith("=========="))
                    {
                        hash.Add(str);
                        str = "";
                    }
                    else
                    {
                        str += line + ",";
                    }
                }
            }
            using (var rdr = new StreamWriter(FinalEmails, true))
            {
                foreach (var item in hash)
                {
                    rdr.WriteLine(item);
                }
            }
        }
        private static void FinalOutPut()
        {
            using (var rdr = new StreamWriter(output, true))
            {
                foreach (var item in list)
                {
                    rdr.WriteLine(item);
                }
            }
        }
        public static void Reader()
        {
            string line = "";
            using (StreamReader sr = new StreamReader(input))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    var ls = line.Split(seperator);
                    foreach (var item in ls)
                    {
                        if (item.Length > 0 && item.EndsWith("@pepsico.com"))
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            list.Add("==================================");
        }
        public static void Writer(string path,object value,bool append)
        {
            using (var rdr = new StreamWriter(path,append))
            {
                rdr.WriteLine(value);
            }
        }
        public static  void Function()
        {
            using (var stream = File.Open(excelPath, FileMode.Open, FileAccess.Read))
            {
                var counter = 0;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        Writer(input, reader[3],false);
                        Reader();
                        counter++;
                    }
                }
                Console.WriteLine(counter);
            }
        }
    }
}
