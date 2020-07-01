using System;
using System.Runtime.InteropServices;

namespace SPMConnectAPI
{
    public static class WinTopMost
    {
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly UInt32 SWP_NOSIZE = 0x0001;
        public static readonly UInt32 SWP_NOMOVE = 0x0002;
        public static readonly UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    }
}