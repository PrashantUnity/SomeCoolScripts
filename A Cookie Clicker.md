# Modify As Your Like

```cs
using System.Runtime.InteropServices;

namespace MouseClicker;

public class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;
    public static int Main(string[] args)
    {
        int count = 0;
        while(count++<10000)
        {
            DoMouseClick();
        }
        return 0;
    }
    public static void DoMouseClick()
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN , 0, 0, 0, 0); 
        mouse_event( MOUSEEVENTF_LEFTUP, 0, 0, 0, 0); 
    }
}
```
