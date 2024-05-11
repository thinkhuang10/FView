using HMIEditEnvironment;
using ShapeRuntime;
using System.Drawing;

namespace Util
{
    public class SizeHelper
    {

        public static Size SetProjectRunSize()
        {
            Size size = default;
            foreach (DataFile df in CEditEnvironmentGlobal.dfs)
            {
                if (size.Width < df.location.X + df.size.Width)
                {
                    size.Width = df.location.X + df.size.Width;
                }
                if (size.Height < df.location.Y + df.size.Height)
                {
                    size.Height = df.location.Y + df.size.Height;
                }
            }
            if (CEditEnvironmentGlobal.dhp != null)
            {
                CEditEnvironmentGlobal.dhp.ProjectSize = size;
            }
            return size;
        }
    }
}
