using CommonSnappableTypes;
using System;
using System.Linq;
using System.Windows.Forms;

namespace XYControl
{
    public partial class AddPointForm : Form
    {
        public event GetVarTable GetVarTableEvent;
        public PointInfo PointInfo;

        private readonly ColorDialog colorDialog = new ColorDialog();

        private readonly string xAxisMin;
        private readonly string xAxisMax;
        private readonly string yAxisMin;
        private readonly string yAxisMax;

        public AddPointForm(string xAxisMin, string xAxisMax, string yAxisMin, string yAxisMax)
        {
            InitializeComponent();
            this.xAxisMin = xAxisMin;
            this.xAxisMax = xAxisMax;
            this.yAxisMin = yAxisMin;
            this.yAxisMax = yAxisMax;
        }

        public AddPointForm(string xAxisMin, string xAxisMax, string yAxisMin, string yAxisMax, PointInfo pointInfo)
        {
            InitializeComponent();
            this.xAxisMin = xAxisMin;
            this.xAxisMax = xAxisMax;
            this.yAxisMin = yAxisMin;
            this.yAxisMax = yAxisMax;
            PointInfo = pointInfo;
        }

        private void AddPointForm_Load(object sender, EventArgs e)
        {
            XAxisScope.Text = string.Concat("(", xAxisMin, ",", xAxisMax, ")");
            YAxisScope.Text = string.Concat("(", yAxisMin, ",", yAxisMax, ")");

            if (null != PointInfo)
            {
                XAxisVar.Text = PointInfo.XVar;
                YAxisVar.Text = PointInfo.YVar;
                PointColor.BackColor = PointInfo.PointColor;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(XAxisVar.Text.Trim()))
            {
                MessageBox.Show("请选择X轴绑定变量.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(YAxisVar.Text.Trim()))
            {
                MessageBox.Show("请选择Y轴绑定变量.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PointInfo = new PointInfo
            {
                XVar = XAxisVar.Text.Trim(),
                YVar = YAxisVar.Text.Trim(),
                PointColor = PointColor.BackColor
            };

            DialogResult = DialogResult.OK;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void SelectedXAxisVarButton_Click(object sender, EventArgs e)
        {
            if (null == GetVarTableEvent)
                return;

            var variables = GetVarTableEvent("").Split('|');
            if (variables.Length <= 0)
                return;

            if (string.IsNullOrEmpty(variables.First()))
                return;

            XAxisVar.Text = string.Concat("[", variables.First(), "]");
        }

        private void SelectedYAxisVarButton_Click(object sender, EventArgs e)
        {
            if (null == GetVarTableEvent)
                return;

            var variables = GetVarTableEvent("").Split('|');
            if (variables.Length <= 0)
                return;

            if (string.IsNullOrEmpty(variables.First()))
                return;

            YAxisVar.Text = string.Concat("[", variables.First(), "]");
        }

        private void PointColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            PointColor.BackColor = colorDialog.Color;
        }
    }
}
