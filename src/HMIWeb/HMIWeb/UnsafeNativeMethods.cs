using System;
using System.Runtime.InteropServices;

namespace HMIWeb;

internal static class UnsafeNativeMethods
{
    public const int WM_LBUTTONDOWN = 513;

    public const int WM_LBUTTONUP = 514;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool PostMessage(HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    public static extern IntPtr GetParent(HandleRef hWnd);
}
