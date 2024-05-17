using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace XYControl
{
    public partial class MainControl: UserControl
    {
        private const string ChartAreaName = "chartArea";
        private readonly Save saveData = new Save();

        public MainControl()
        {
            InitializeComponent();
        }

        #region 屏蔽部分没必要显示的属性并修改部分显示属性的名称

        [Browsable(false)]
        public new System.Windows.Forms.Cursor Cursor
        {
            get
            {
                return base.Cursor;
            }
            set
            {
                base.Cursor = value;
            }
        }

        [Browsable(false)]
        public new Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        [Browsable(false)]
        public new BorderStyle BorderStyle
        {
            get
            {
                return base.BorderStyle;
            }
            set
            {
                base.BorderStyle = value;
            }
        }

        [Browsable(false)]
        public new ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
            }
        }

        [Browsable(false)]
        public new Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        [Browsable(false)]
        public new Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        [Browsable(false)]
        public new RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
            }
        }

        [Browsable(false)]
        public new bool UseWaitCursor
        {
            get
            {
                return base.UseWaitCursor;
            }
            set
            {
                base.UseWaitCursor = value;
            }
        }

        [Browsable(false)]
        public new Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        [ReadOnly(false)]
        [Description("控件左上角相对于其容器左上角的坐标")]
        [DisplayName("位置")]
        [Category("布局")]
        public new Point Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                base.Location = value;
            }
        }

        [Description("控件的大小（以像素为单位）")]
        [Category("布局")]
        [DisplayName("大小")]
        [ReadOnly(false)]
        public new Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
            }
        }

        [Browsable(false)]
        public new AnchorStyles Anchor
        {
            get
            {
                return base.Anchor;
            }
            set
            {
                base.Anchor = value;
            }
        }

        [Browsable(false)]
        public new bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
            }
        }

        [Browsable(false)]
        public new AutoSizeMode AutoSizeMode
        {
            get
            {
                return base.AutoSizeMode;
            }
            set
            {
                base.AutoSizeMode = value;
            }
        }

        [Browsable(false)]
        public new Padding Padding
        {
            get
            {
                return base.Padding;
            }
            set
            {
                base.Padding = value;
            }
        }

        [Browsable(false)]
        public new DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = value;
            }
        }

        [Browsable(false)]
        public new Padding Margin
        {
            get
            {
                return base.Margin;
            }
            set
            {
                base.Margin = value;
            }
        }

        [Browsable(false)]
        public new Size MaximumSize
        {
            get
            {
                return base.MaximumSize;
            }
            set
            {
                base.MaximumSize = value;
            }
        }

        [Browsable(false)]
        public new Size MinimumSize
        {
            get
            {
                return base.MinimumSize;
            }
            set
            {
                base.MinimumSize = value;
            }
        }

        [Browsable(false)]
        public new bool AutoScroll
        {
            get
            {
                return base.AutoScroll;
            }
            set
            {
                base.AutoScroll = value;
            }
        }

        [Browsable(false)]
        public new Size AutoScrollMargin
        {
            get
            {
                return base.AutoScrollMargin;
            }
            set
            {
                base.AutoScrollMargin = value;
            }
        }

        [Browsable(false)]
        public new Size AutoScrollMinSize
        {
            get
            {
                return base.AutoScrollMinSize;
            }
            set
            {
                base.AutoScrollMinSize = value;
            }
        }

        [Browsable(false)]
        public new bool CausesValidation
        {
            get
            {
                return base.CausesValidation;
            }
            set
            {
                base.CausesValidation = value;
            }
        }

        [Browsable(false)]
        public new string AccessibleName
        {
            get
            {
                return base.AccessibleName;
            }
            set
            {
                base.AccessibleName = value;
            }
        }

        [Browsable(false)]
        public new AccessibleRole AccessibleRole
        {
            get
            {
                return base.AccessibleRole;
            }
            set
            {
                base.AccessibleRole = value;
            }
        }

        [Browsable(false)]
        public new string AccessibleDescription
        {
            get
            {
                return base.AccessibleDescription;
            }
            set
            {
                base.AccessibleDescription = value;
            }
        }

        [Browsable(false)]
        public new object Tag
        {
            get
            {
                return base.Tag;
            }
            set
            {
                base.Tag = value;
            }
        }

        [Browsable(false)]
        public new ControlBindingsCollection DataBindings => base.DataBindings;

        [Browsable(false)]
        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }

        [Browsable(false)]
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
            }
        }

        [Browsable(false)]
        public new bool AllowDrop
        {
            get
            {
                return base.AllowDrop;
            }
            set
            {
                base.AllowDrop = value;
            }
        }

        [Browsable(false)]
        public new ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
            }
        }

        [Browsable(false)]
        public new int TabIndex
        {
            get
            {
                return base.TabIndex;
            }
            set
            {
                base.TabIndex = value;
            }
        }

        [Browsable(false)]
        public new bool TabStop
        {
            get
            {
                return base.TabStop;
            }
            set
            {
                base.TabStop = value;
            }
        }

        [Browsable(false)]
        public new AutoValidate AutoValidate
        {
            get
            {
                return base.AutoValidate;
            }
            set
            {
                base.AutoValidate = value;
            }
        }

        [Browsable(false)]
        public new ImeMode ImeMode
        {
            get
            {
                return base.ImeMode;
            }
            set
            {
                base.ImeMode = value;
            }
        }

        #endregion

        private void UserControl_Load(object sender, EventArgs e)
        {
            InitChart();
            InitTitle();
            InitChartArea();
            InitGrid();

            AddLine1("line1");
            AddLine2("line2");
            AddPoint("point");
        }

        private void InitChart()
        {
            xyChart.BackColor = saveData.chartBackColor;    // 设置背景色
        }

        private void InitTitle()
        {
            var title = xyChart.Titles.Add(saveData.chartTitle);    // 设置标题文本
            title.ForeColor = saveData.chartTitleColor;             // 设置标题颜色
            title.Font = new Font(FontFamily.GenericSansSerif, saveData.chartTitleSize,     // 设置标题字体大小
                saveData.chartTitleIsBold ? FontStyle.Bold : FontStyle.Regular);            // 设置标题是否为粗体
        }

        private void InitChartArea()
        {
            var chartArea = new ChartArea(ChartAreaName);

            chartArea.BackColor = saveData.chartForeColor;      // 设置前景色

            chartArea.AxisX.Minimum = saveData.xAxisMin;        // 设置X轴最小值
            chartArea.AxisX.Maximum = saveData.xAxisMax;        // 设置X轴最大值

            chartArea.AxisY.Minimum = saveData.yAxisMin;        // 设置Y轴最小值
            chartArea.AxisY.Maximum = saveData.yAxisMax;        // 设置Y轴最大值

            // 设置标注颜色
            chartArea.AxisX.LabelStyle = new LabelStyle
            {
                ForeColor = saveData.axisLabelColor,
            };

            chartArea.AxisY.LabelStyle = new LabelStyle
            {
                ForeColor = saveData.axisLabelColor
            };

            chartArea.AxisX.Title = saveData.xAxisTitle;                    // 设置X信息
            chartArea.AxisX.TitleForeColor = saveData.xAxisTitleForeColor;  // 设置X信息颜色
            chartArea.AxisX.TitleFont = new Font(FontFamily.GenericSansSerif, saveData.xAxisTitleSize,     // 设置X信息字体大小
                saveData.xAxisTitleIsBold ? FontStyle.Bold : FontStyle.Regular);                           // 设置X信息是否为粗体

            chartArea.AxisY.Title = saveData.yAxisTitle;                    // 设置Y信息
            chartArea.AxisY.TitleForeColor = saveData.yAxisTitleForeColor;  // 设置Y信息颜色
            chartArea.AxisY.TitleFont = new Font(FontFamily.GenericSansSerif, saveData.yAxisTitleSize,     // 设置Y信息字体大小
                saveData.yAxisTitleIsBold ? FontStyle.Bold : FontStyle.Regular);                           // 设置Y信息是否为粗体

            xyChart.ChartAreas.Add(chartArea);
        }

        private void InitGrid()
        {
            xyChart.ChartAreas[ChartAreaName].AxisX.MajorGrid.Enabled = true;
            xyChart.ChartAreas[ChartAreaName].AxisX.MajorGrid.LineColor = saveData.gridColor;
            xyChart.ChartAreas[ChartAreaName].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Solid;
            xyChart.ChartAreas[ChartAreaName].AxisX.MajorGrid.IntervalType = DateTimeIntervalType.Number;
            xyChart.ChartAreas[ChartAreaName].AxisX.MajorGrid.Interval = (saveData.xAxisMax - saveData.xAxisMin) / saveData.xAxisGridCount;

            xyChart.ChartAreas[ChartAreaName].AxisY.MajorGrid.Enabled = true;
            xyChart.ChartAreas[ChartAreaName].AxisY.MajorGrid.LineColor = saveData.gridColor;
            xyChart.ChartAreas[ChartAreaName].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Solid;
            xyChart.ChartAreas[ChartAreaName].AxisY.MajorGrid.IntervalType = DateTimeIntervalType.Number;
            xyChart.ChartAreas[ChartAreaName].AxisY.MajorGrid.Interval = (saveData.yAxisMax - saveData.yAxisMin) / saveData.yAxisGridCount;
        }

        private void AddLine1(string lineName)
        {
            var series = xyChart.Series.Add(lineName);
            series.BorderWidth = saveData.seriesBorderWidth;    // 曲线宽度
            series.Color = Color.Yellow;
            series.ChartArea = ChartAreaName;
            series.ChartType = SeriesChartType.Line;

            series.Points.AddXY(1, 2);
            series.Points.AddXY(31, 42);
        }

        private void AddLine2(string lineName)
        {
            var series = xyChart.Series.Add(lineName);
            series.BorderWidth = saveData.seriesBorderWidth;    // 曲线宽度
            series.Color = Color.Blue;
            series.ChartArea = ChartAreaName;
            series.ChartType = SeriesChartType.Line;
            series.Points.AddXY(11, 32);
            series.Points.AddXY(1, 26);
            series.Points.AddXY(55, 36);
        }

        private void AddPoint(string pointName)
        {
            var series = xyChart.Series.Add(pointName);
            series.ChartArea = ChartAreaName;
            series.ChartType = SeriesChartType.Point;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 10;
            
            var font = new Font(FontFamily.GenericSansSerif, saveData.dynamicPolitLabelSize);
            series.Points.Add(new DataPoint
            {
                Color = Color.Red,
                XValue = 41,
                YValues = new double[] { 32 },
                IsValueShownAsLabel = saveData.isShowDynamicPolitLabel,
                Label = "#VALX, #VALY",
                LabelBackColor = saveData.dynamicPolitLabelBackColor,
                LabelForeColor = saveData.dynamicPolitLabelForeColor,
                Font = font
            });
            series.Points.Add(new DataPoint
            {
                Color = Color.Green,
                XValue = 21,
                YValues = new double[] { 66 },
                IsValueShownAsLabel = saveData.isShowDynamicPolitLabel,
                Label = "#VALX, #VALY",
                LabelBackColor = saveData.dynamicPolitLabelBackColor,
                LabelForeColor = saveData.dynamicPolitLabelForeColor,
                Font = font
            });
        }

        private void Chart_DoubleClick(object sender, EventArgs e)
        {
            var form = new SetForm();
            form.ShowDialog();
        }

    }
}
