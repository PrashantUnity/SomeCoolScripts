using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CpuUses
{
    class Program
    {
        static PerformanceCounter cpuCounter;
        static PerformanceCounter ramCounter;
        static int interval =3000;
        const string path = "YourPathOfTextFile";
        
        static void Main()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            GetCurrentCpuUsage();
            GetAvailableRAM();
            StoreInTextFile();
            //PrintOnConsole();
        }
        public static void PrintOnConsole()
        {
            Console.WriteLine(string.Format("{0,6} {1,20:N0} {2,25:N0}", "Cpu Uses", "Available Ram", "Current DateTime"));
            while (true)
            {
                Console.WriteLine(string.Format("{0,6} {1,20:N0} {2,30:N0}", GetCurrentCpuUsage(), GetAvailableRAM(), DateTime.Now.ToString()));
                System.Threading.Thread.Sleep(interval);
            }
        }
        public static void StoreInTextFile()
        {
            using (var sw = new StreamWriter(path))
            {

                sw.WriteLine(string.Format("{0,6} {1,20:N0} {2,25:N0}", "Cpu Uses", "Available Ram", "Current DateTime"));
            }

            while (true)
            {
                using (var sw = new StreamWriter(path, true))
                {

                    sw.WriteLine(string.Format("{0,6} {1,20:N0} {2,30:N0}", GetCurrentCpuUsage(), GetAvailableRAM(), DateTime.Now.ToString()));
                }
                System.Threading.Thread.Sleep(interval);
            }
        }
        public static string GetCurrentCpuUsage()
        {
            return (int)cpuCounter.NextValue() + "%";
        }

        public static string GetAvailableRAM()
        {
            return (int)ramCounter.NextValue() + "MB";
        }

    }
}
