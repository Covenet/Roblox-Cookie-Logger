using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace MyHideConsole
{
    class Program
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();

        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();

        [DllImport("kernel32.dll")]
        public static extern Boolean AttachConsole(Int32 ProcessId);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        // Also consider whether you're being lazy or not.
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        
    }
}
## i tried makeing the console app run in the backround but it dosent work i tried