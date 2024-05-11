using HMIEditEnvironment;
using System.IO;

namespace Util
{
    public class DirectoryHelper
    {
        public static DirectoryInfo GetProjectHMIPath()
        {
            return new FileInfo(CEditEnvironmentGlobal.ProjectFile).Directory;
        }
    }
}
