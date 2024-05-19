using System;
using System.Collections.Generic;
using System.Drawing;

namespace XYControl
{
    [Serializable]
    public class LineInfo
    {
        public Color LineColor { set; get; }

        public List<PointInfo> PointInfos = new List<PointInfo>();
    }
}
