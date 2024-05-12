using System;
using System.IO;

namespace Util
{
    public  class PathHelper
    {
        public static string GetOutputPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "Output\\";
        }

        public static string GetLogicCodePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "LogicCode\\";
        }

        public static string GetDefaultProjectPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                ConstantHelper.SoftwareName, "Projects");
        }
    }
}
