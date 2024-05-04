using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HMIRunForm;

internal static class Program
{
    public const int SWP_HIDEWINDOW = 128;

    public const int SWP_SHOWWINDOW = 64;

    [DllImport("user32.dll")]
    private static extern IntPtr FindWindowEx(int hwnd2, int hWnd2, string lpsz1, string lpsz2);

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hwnd, int nIndex);

    [DllImport("user32")]
    public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

    [STAThread]
    private static void Main(string[] args)
    {
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(defaultValue: false);
            RunForm mainForm = new();
            Application.Run(mainForm);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + Environment.NewLine + ex.Source + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            SetWindowPos(FindWindowEx(0, 0, "Shell_TrayWnd", "").ToInt32(), 0, 0, 0, 0, 0, 64);
        }
    }
}
