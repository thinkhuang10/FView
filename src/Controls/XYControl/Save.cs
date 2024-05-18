using System;
using System.Collections.Generic;
using System.Drawing;

namespace XYControl
{
    [Serializable]
    public class Save
    {
        public Color chartForeColor = Color.Black;

        public Color chartBackColor = Color.Gray;

        public double xAxisMin = 0;

        public double xAxisMax = 100;

        public double yAxisMin = 0;

        public double yAxisMax = 100;

        public Color axisLabelColor = Color.DarkBlue;

        public string chartTitle = "XY曲线";

        public Color chartTitleColor = Color.Black;

        public float chartTitleSize = 10.0f;

        public bool chartTitleIsBold = false;

        public string xAxisTitle = "X轴";

        public string yAxisTitle = "Y轴";

        public Color xAxisTitleForeColor = Color.Black;

        public Color yAxisTitleForeColor = Color.Black;

        public float xAxisTitleSize = 10.0f;

        public float yAxisTitleSize = 10.0f;

        public bool xAxisTitleIsBold = false;

        public bool yAxisTitleIsBold = false;

        public Color gridColor = Color.White;

        public uint verticalGridCount = 10;

        public uint horizonalGridCount = 10;

        public int dynamicPointSize = 10;

        public bool isShowDynamicPointLabel = true;

        public float dynamicPointLabelSize = 8.0f;

        public Color dynamicPointLabelBackColor = Color.White;

        public Color dynamicPointLabelForeColor = Color.Black;

        public int decimalPlace = 1;

        public int seriesBorderWidth = 2;

        public int refreshInterval = 1000;

        public List<PointInfo> pointInfos = new List<PointInfo>();
    }
}
