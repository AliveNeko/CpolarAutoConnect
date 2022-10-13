using System.Runtime.InteropServices;

namespace CpolarAutoConnect.Core.Util;

public static class MessageUtil
{
#if Windows
    // Use DllImport to import the Win32 MessageBox function.
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);
#endif
    public static int Alert(string caption, string text)
    {
#if Windows
        return MessageBox(new IntPtr(0), text, caption, 0);
#elif Linux
        Console.WriteLine(text);
        return 0;
#endif
    }
}