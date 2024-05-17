using System;
using System.Drawing;

namespace XYControl
{
    [Serializable]
    public class Save
    {
        public Color chartBackColor = Color.Gray;

        public Color chartForeColor = Color.Black;

        public double xAxisMin = 0;

        public double xAxisMax = 100;

        public double yAxisMin = 0;

        public double yAxisMax = 100;

        public Color axisLabelColor = Color.DarkBlue;

        public string chartTitle = "XY曲线";

        public Color chartTitleColor = Color.Black;

        public float chartTitleSize = 12.0f;

        public bool chartTitleIsBold = false;

        public string xAxisTitle = "X轴";

        public string yAxisTitle = "Y轴";

        public Color xAxisTitleForeColor = Color.Black;

        public Color yAxisTitleForeColor = Color.Black;

        public float xAxisTitleSize = 12.0f;

        public float yAxisTitleSize = 12.0f;

        public bool xAxisTitleIsBold = false;

        public bool yAxisTitleIsBold = false;

        public Color gridColor = Color.White;

        public uint xAxisGridCount = 10;

        public uint yAxisGridCount = 10;

        public uint dynamicPointSize = 10;

        public bool isShowDynamicPolitLabel = true;

        public float dynamicPolitLabelSize = 8.0f;

        public Color dynamicPolitLabelBackColor = Color.White;

        public Color dynamicPolitLabelForeColor = Color.Black;

        public uint decimalPlace = 1;

        public int seriesBorderWidth = 2;

        public int millisecond = 1000;
    }
}
