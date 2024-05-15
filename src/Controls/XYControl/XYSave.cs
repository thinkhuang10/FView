using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace XYControl
{
    [Serializable]
    public class XYSave
    {
        public OxyColor ForegroundColor { get; set; }

        public OxyColor BackgroundColor { get; set; }

        public OxyColor GridColor { get; set; }

        public OxyColor MarkColor { get; set; }

        public int HorizontalGrid { get; set; }

        public int VerticalGrid { get; set; }

        public int DynamicPointDiameter { get; set; }

        public bool ShowDynamicPointValue { get; set; }

        public float DynamicPointFontSize { get; set; }

        public string Title { get; set; }

        public string XLabel { get; set; }

        public string YLabel { get; set; }

        public OxyColor XLabelColor { get; set; }

        public OxyColor YLabelColor { get; set; }

        public int DecimalPlaces { get; set; }

        public int CurveLineWidth { get; set; }

        public int RefreshTime { get; set; }

        public double XMin { get; set; }

        public double XMax { get; set; }

        public double YMin { get; set; }

        public double YMax { get; set; }

        public PlotModel Points { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
