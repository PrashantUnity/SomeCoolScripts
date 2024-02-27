```
// 2>nul||@goto :batch
/*
:batch
@echo off
setlocal

:: find csc.exe
set "csc="
for /r "%SystemRoot%\Microsoft.NET\Framework\" %%# in ("*csc.exe") do  set "csc=%%#"

if not exist "%csc%" (
   echo no .net framework installed
   exit /b 10
)

if not exist "%~n0.exe" (
   call %csc% /nologo /warn:0 /out:"%~n0.exe" "%~dpsfnx0" || (
      exit /b %errorlevel% 
   )
)
%~n0.exe %*
endlocal & exit /b %errorlevel%

*/

// taken help from this stack overflow posts
//https://stackoverflow.com/questions/553143/compiling-executing-a-c-sharp-source-file-in-command-prompt
//https://stackoverflow.com/questions/15099523/changing-console-windows-size-throws-argumentoutofrangeexception
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MouseMovement
{
    internal class Program
    {
        static long count = 0;
        static readonly string read = "Mouse Movement Interval in Second : ";
        static readonly string times = "Time's cursor moved ";
        static Stopwatch stopwatch = new Stopwatch();

        static readonly Random random = new Random();
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public SendInputEventType type;
            public MouseKeybdhardwareInputUnion mkhi;
        }
        [StructLayout(LayoutKind.Explicit)]
        struct MouseKeybdhardwareInputUnion
        {
            [FieldOffset(0)]
            public MouseInputData mi;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        [Flags]
        enum MouseEventFlags : uint
        {
            MOUSEEVENTF_WHEEL = 0x0800,
        }
        enum SendInputEventType : int
        {
            InputMouse
        }
        struct MouseInputData
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [Flags]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001,
            ES_USER_PRESENT = 0x00000004
        }
        private enum Color
        {
            white = ConsoleColor.White,
            yellow = ConsoleColor.Yellow,
            blue = ConsoleColor.Blue,
            green = ConsoleColor.Green,
            black = ConsoleColor.Black,
            red = ConsoleColor.Red
        }
        static void Main()
        {
            
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(read);
            var time = Convert.ToDouble(Console.ReadLine());
            stopwatch.Start();
            var ts = new CancellationTokenSource();
            CancellationToken ct = ts.Token;
            Task task = Task.Run(() =>
            {
                MouseMovement(time==0?1:time,ct);
            },ct);
            Task space = Task.Run(() =>
            {
                if (ConsoleKey.Spacebar == Console.ReadKey().Key)
                {
                    ts.Cancel();
                    Environment.Exit(0);
                }
            });
            space.Wait();
            
        }
        public static void MouseMovement(double time, CancellationToken ct)
        {
            Rectangle screenRes = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            int widtMax = screenRes.Width;
            int heighMax = screenRes.Height;
            while (true)
            {
                if(ct.IsCancellationRequested) return; 
                SetCursorPos(random.Next(1, widtMax), random.Next(1, heighMax));
                int sleepTime = (int)(1000 * time);
                Thread.Sleep(sleepTime);
                count++;
                WriteToConsole(count,time);
                PreventSleep();
                var scroll = random.Next(1, 10);
                if(scroll<3) ScrollDown(random.Next(10, 300)); 
                if(scroll>8) ScrollUp(random.Next(10, 300)); 
            }
        }
        static void PreventSleep()
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
        }
        private static void WriteToConsole(long count, double time)
        {
            Console.Clear();

            ColorChange(Color.blue);
            Console.Write(read);

            ColorChange(Color.green);
            Console.Write( time);

            ColorChange(Color.white);
            Console.WriteLine(" second");

            Console.WriteLine();
            Console.WriteLine();

            ColorChange(Color.yellow);
            Console.Write(times);

            ColorChange(Color.green);
            Console.Write(": "+count);

            ColorChange(Color.yellow);
            Console.Write(" in ");

            ColorChange(Color.green);
            Console.Write(stopwatch.Elapsed.TotalSeconds);

            ColorChange(Color.white);
            Console.WriteLine(" second");

            
            Console.WriteLine();
            Console.WriteLine();

            ColorChange(Color.red);
            Console.WriteLine("Press SpaceBar To Close Application");
        }
        
        private static void ColorChange(Color color)
        {
            Console.ForegroundColor = (ConsoleColor)color;
        }
        static void ScrollUp(int amount)
        {
            INPUT mouseInput = new INPUT
            {
                type = SendInputEventType.InputMouse
            };
            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_WHEEL;
            mouseInput.mkhi.mi.mouseData = (uint)amount;
            SendInput(1, ref mouseInput, Marshal.SizeOf(mouseInput));
        }

        static void ScrollDown(int amount)
        {
            INPUT mouseInput = new INPUT
            {
                type = SendInputEventType.InputMouse
            };
            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_WHEEL;
            mouseInput.mkhi.mi.mouseData = 0 - (uint)amount;
            SendInput(1, ref mouseInput, Marshal.SizeOf(mouseInput));
        }
    }
}
```
