using HMIEditEnvironment;
using System;
using System.IO;

namespace Util
{
    public class DirectoryHelper
    {
        public static DirectoryInfo GetProjectHMIDirectory()
        {
            return new FileInfo(CEditEnvironmentGlobal.ProjectHPFFilePath).Directory;
        }

        public static DirectoryInfo GetHMIRunDirectory()
        {
            return new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "HMIRun\\");
        }

        public static DirectoryInfo GetUserControlDirectory()
        {
            return new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "UserControl\\");
        }
    }
}
