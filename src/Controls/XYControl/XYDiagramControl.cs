using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonSnappableTypes;
using FControl.SetFroms;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace XYControl
{
    public partial class XYDiagramControl: UserControl
    {
        private XYSave saveData = new XYSave();

        [Browsable(false)]
        public bool isRuning { get; set; }

        private OxyColor _foregroundColor = OxyColors.Black;
        private OxyColor _gridColor = OxyColors.Gray;
        private OxyColor _markerColor = OxyColors.Red;
        private int _horizontalGrid = 10;
        private int _verticalGrid = 10;
        private int _dynamicPointDiameter = 5;
        private bool _showDynamicPointValue = true;
        private float _dynamicPointFontSize = 10;
        private int _decimalPlaces = 2;
        private int _curveLineWidth = 1;
        private int _refreshTime = 1000;

        [Category("颜色")]
        [DisplayName("前景颜色")]
        public OxyColor ForegroundColor
        {
            get { return _foregroundColor; }
            set { _foregroundColor = value; }
        }

        [Category("颜色")]
        [DisplayName("背景颜色")]
        public OxyColor BackgroundColor
        {
            get { return saveData.BackgroundColor; }
            set
            {
                saveData.BackgroundColor = value;
                if (this.plotView1.Model != null)
                    this.plotView1.Model.Background = value;
            }
        }

        [Category("颜色")]
        [DisplayName("网格颜色")]
        public OxyColor GridColor
        {
            get { return _gridColor; }
            set { _gridColor = value; }
        }


        [Category("颜色")]
        [DisplayName("标注颜色")]
        public OxyColor MarkerColor
        {
            get { return _markerColor; }
            set { _markerColor = value; }
        }

        [Category("网格数")]
        [DisplayName("水平网格")]
        public int HorizontalGrid
        {
            get { return _horizontalGrid; }
            set
            {
                _horizontalGrid = value;
                Invalidate();
            }
        }

        [Category("网格数")]
        [DisplayName("垂直网格")]
        public int VerticalGrid
        {
            get { return _verticalGrid; }
            set
            {
                _verticalGrid = value;
                Invalidate();
            }
        }

        [Category("动态点")]
        [DisplayName("动态点直径")]
        public int DynamicPointDiameter
        {
            get { return _dynamicPointDiameter; }
            set { _dynamicPointDiameter = value; }
        }

        [Category("动态点")]
        [DisplayName("是否显示动态点值")]
        public bool ShowDynamicPointValue
        {
            get { return _showDynamicPointValue; }
            set { _showDynamicPointValue = value; }
        }

        [Category("动态点")]
        [DisplayName("动态点字体大小")]
        public float DynamicPointFontSize
        {
            get { return _dynamicPointFontSize; }
            set { _dynamicPointFontSize = value; }
        }

        [Category("标题/显示信息")]
        [DisplayName("标题")]
        public string Title
        {
            get { return saveData.Title; }
            set
            {
                saveData.Title = value;
                if (this.plotView1.Model != null)
                    this.plotView1.Model.Title = value;
            }
        }

        [Category("标题/显示信息")]
        [DisplayName("X信息")]
        public string XLabel
        {
            get { return saveData.XLabel; }
            set
            {
                saveData.XLabel = value;
                if (this.plotView1.Model != null)
                    this.plotView1.Model.Axes[0].Title = value;
            }
        }


        [Category("标题/显示信息")]
        [DisplayName("Y信息")]
        public string YLabel
        {
            get { return saveData.YLabel; }
            set
            {
                saveData.YLabel = value;
                if (this.plotView1.Model != null)
                    this.plotView1.Model.Axes[1].Title = value;
            }
        }

        [Category("标题/显示信息")]
        [DisplayName("X轴标签颜色")]
        public OxyColor XLabelColor
        {
            get { return saveData.XLabelColor; }
            set
            {
                saveData.XLabelColor = value;
                if (this.plotView1.Model != null)
                    this.plotView1.Model.Axes[0].TextColor = value;
            }
        }

        [Category("标题/显示信息")]
        [DisplayName("Y轴标签颜色")]
        public OxyColor YLabelColor
        {
            get { return saveData.YLabelColor; }
            set
            {
                saveData.XLabelColor = value;
                if (this.plotView1.Model != null)
                    this.plotView1.Model.Axes[1].TextColor = value;
            }
        }

        [Category("其他")]
        [DisplayName("小数位")]
        public int DecimalPlaces
        {
            get { return _decimalPlaces; }
            set { _decimalPlaces = value; }
        }

        [Category("其他")]
        [DisplayName("曲线线宽")]
        public int CurveLineWidth
        {
            get { return _curveLineWidth; }
            set { _curveLineWidth = value; }
        }

        [Category("其他")]
        [DisplayName("刷新时间")]
        public int RefreshTime
        {
            get { return _refreshTime; }
            set { _refreshTime = value; }
        }

        [Category("范围")]
        [DisplayName("X最大值")]
        public double XMax
        {
            get { return saveData.XMax; }
            set
            {
                saveData.XMax = value;
                if (this.plotView1.Model != null)
                    this.plotView1.Model.Axes[0].Maximum = value;
            }
        }

        [Category("范围")]
        [DisplayName("X最小值")]
        public double XMin
        {
            get { return saveData.XMin; }
            set
            {
                saveData.XMin = value;
                if (this.plotView1.Model != null)
                    this.plotView1.Model.Axes[0].Minimum = value;
            }
        }

        [Category("范围")]
        [DisplayName("Y最大值")]
        public double YMax
        {
            get { return saveData.YMax; }
            set
            {
                saveData.YMax = value;
                if (this.plotView1.Model != null)
                    this.plotView1.Model.Axes[1].Maximum = value;
            }
        }

        [Category("范围")]
        [DisplayName("Y最小值")]
        public double YMin
        {
            get { return saveData.YMin; }
            set
            {
                saveData.YMin = value;
                if (this.plotView1.Model != null)
                    this.plotView1.Model.Axes[1].Minimum = value;
            }
        }

        //PlotModel myModel;

        public XYDiagramControl()
        {
            InitializeComponent();
            TestDemo();
        }
        private void XYDiagramControl_Load(object sender, EventArgs e)
        {
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom, // 设置X轴位置
                Minimum = 0, // 设置X轴最小值
                Maximum = 10, // 设置X轴最大值
                Title = "X轴", // 设置X轴标题
                TitleColor = OxyColors.Black, // 设置X轴标题颜色
                TitleFontSize = 12, // 设置X轴标题字体大小
                MajorGridlineStyle = LineStyle.Solid, // 设置X轴主网格线样式
                MajorGridlineColor = OxyColors.Gray, // 设置X轴主网格线颜色
                MajorGridlineThickness = 1, // 设置X轴主网格线宽度
                MinorGridlineStyle = LineStyle.Dot, // 设置X轴次网格线样式
                MinorGridlineColor = OxyColors.LightGray, // 设置X轴次网格线颜色
                MinorGridlineThickness = 1, // 设置X轴次网格线宽度
                AxislineStyle = LineStyle.Solid, // 设置X轴轴线样式
                AxislineColor = OxyColors.Black, // 设置X轴轴线颜色
                AxislineThickness = 1, // 设置X轴轴线宽度
                TickStyle = OxyPlot.Axes.TickStyle.Outside, // 设置X轴刻度样式
                TicklineColor = OxyColors.Black, // 设置X轴刻度线颜色 
                Angle = 90, // 设置X轴刻度标签旋转角度
            };

            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left, // 设置Y轴位置
                Minimum = -2.5, // 设置Y轴最小值
                Maximum = 2.5, // 设置Y轴最大值
                Title = "Y轴", // 设置Y轴标题
                TitleColor = OxyColors.Black, // 设置Y轴标题颜色
                TitleFontSize = 12, // 设置Y轴标题字体大小
                MajorGridlineStyle = LineStyle.Solid, // 设置Y轴主网格线样式
                MajorGridlineColor = OxyColors.Gray, // 设置Y轴主网格线颜色
                MajorGridlineThickness = 1, // 设置Y轴主网格线宽度
                MinorGridlineStyle = LineStyle.Dot, // 设置Y轴次网格线样式
                MinorGridlineColor = OxyColors.LightGray, // 设置Y轴次网格线颜色
                MinorGridlineThickness = 1, // 设置Y轴次网格线宽度
                AxislineStyle = LineStyle.Solid, // 设置Y轴轴线样式
                AxislineColor = OxyColors.Black, // 设置Y轴轴线颜色
                AxislineThickness = 1, // 设置Y轴轴线宽度
                TickStyle = OxyPlot.Axes.TickStyle.Outside, // 设置Y轴刻度样式
                TicklineColor = OxyColors.Black, // 设置Y轴刻度线颜色 
                Angle = 90 // 设置Y轴刻度标签旋转角度
            };

            // 将轴添加到PlotView中
            plotView1.Model.Axes.Add(xAxis);
            plotView1.Model.Axes.Add(yAxis);
        }

        private void TestDemo()
        {
            saveData.Points = new PlotModel { Title = "Example 1" };
            saveData.Points.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            saveData.Points.Series.Add(new FunctionSeries(Math.Sin, 1, 11, 0.2, "sin(x)"));
            saveData.Points.Annotations.Add(new PointAnnotation { X = 2.5, Y = 1.56, Fill = OxyColors.Red, });
            this.plotView1.Model = saveData.Points;
        }

        private void plotView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (isRuning)
                return;

            saveData.Title = this.plotView1.Model.Title;
            //saveData.ForegroundColor = this.plotView1.Model.ForegroundColor;
            saveData.BackgroundColor = this.plotView1.Model.Background;
            saveData.GridColor = this.plotView1.Model.Axes[0].MajorGridlineColor;
            saveData.MarkColor = this.plotView1.Model.Axes[0].MajorGridlineColor;
            saveData.HorizontalGrid = int.Parse(this.plotView1.Model.Axes[0].MinorGridlineThickness.ToString());
            saveData.VerticalGrid = int.Parse(this.plotView1.Model.Axes[1].MinorGridlineThickness.ToString());
            //saveData.DynamicPointDiameter = DynamicPointDiameter;
            //saveData.ShowDynamicPointValue = ShowDynamicPointValue;
            //saveData.DynamicPointFontSize = DynamicPointFontSize;
            saveData.XLabel = this.plotView1.Model.Axes[0].Title;
            saveData.YLabel = this.plotView1.Model.Axes[1].Title;
            saveData.XLabelColor = this.plotView1.Model.Axes[0].MajorGridlineColor;
            saveData.YLabelColor = this.plotView1.Model.Axes[1].MajorGridlineColor;
            saveData.DecimalPlaces = DecimalPlaces;
            saveData.CurveLineWidth = CurveLineWidth;
            saveData.RefreshTime = RefreshTime;
            saveData.XMax = this.plotView1.Model.Axes[0].Maximum;
            saveData.XMin = this.plotView1.Model.Axes[0].Minimum;
            saveData.YMax = this.plotView1.Model.Axes[1].Maximum;
            saveData.YMin = this.plotView1.Model.Axes[1].Minimum;

            XYSetForm form = new XYSetForm(saveData);


            if (form.ShowDialog() == DialogResult.OK)
            {
                Title = saveData.Title;
                ForegroundColor = saveData.ForegroundColor;
                BackgroundColor = saveData.BackgroundColor;
                GridColor = saveData.GridColor;
                MarkerColor = saveData.MarkColor;
                HorizontalGrid = saveData.HorizontalGrid;
                VerticalGrid = saveData.VerticalGrid;
                DynamicPointDiameter = saveData.DynamicPointDiameter;
                ShowDynamicPointValue = saveData.ShowDynamicPointValue;
                DynamicPointFontSize = saveData.DynamicPointFontSize;
                XLabel = saveData.XLabel;
                YLabel = saveData.YLabel;
                XLabelColor = saveData.XLabelColor;
                YLabelColor = saveData.YLabelColor;
                DecimalPlaces = saveData.DecimalPlaces;
                CurveLineWidth = saveData.CurveLineWidth;
                RefreshTime = saveData.RefreshTime;
                XMax = saveData.XMax;
                XMin = saveData.XMin;
                YMax = saveData.YMax;
                YMin = saveData.YMin;
                //myModel.Title = saveData.Name;
            }

            plotView1.Model.InvalidatePlot(true);
        }

        public byte[] Serialize()
        {
            XYSave graph = saveData;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, graph);
            byte[] result = memoryStream.ToArray();
            memoryStream.Dispose();
            return result;
        }

        public void DeSerialize(byte[] bytes)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream(bytes);
            saveData = (XYSave)binaryFormatter.Deserialize(memoryStream);
            memoryStream.Dispose();
        }

        public Bitmap GetLogo()
        {
            //    ResourceManager resourceManager = new ResourceManager(typeof(图标));
            //    return (Bitmap)resourceManager.GetObject("icon");
            return null;
        }

        public void Stop()
        {
        }


        public event GetValue GetValueEvent;

        public event SetValue SetValueEvent;

        public event GetDataBase GetDataBaseEvent;

        public event GetVarTable GetVarTableEvent;

        public event GetValue GetSystemItemEvent;
    }
}
