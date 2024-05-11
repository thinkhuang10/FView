using System;
using System.IO;

namespace HMICompile;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        FileInfo fileInfo = new(args[0]);
        Compiler compiler = new(args[0], fileInfo.Directory.FullName + "\\LogicCode\\", fileInfo.Directory.FullName + "\\Output\\");
        compiler.CreateScript();
        compiler.ZipResource(compress: true, dirtyCompile: true, null, Console.WriteLine);
        compiler.DynamicCompile();
        compiler.CreateCab();
    }
}
