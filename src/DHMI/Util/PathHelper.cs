using System;

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
    }
}
