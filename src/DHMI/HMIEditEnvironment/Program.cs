using HMIEditEnvironment.TagManager;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Control.CheckForIllegalCrossThreadCalls = false;
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(defaultValue: false);
        Application.Run(new MDIParent1());
        //Application.Run(new TagManagerForm());
    }
}
