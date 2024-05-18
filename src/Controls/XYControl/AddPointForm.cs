using CommonSnappableTypes;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace XYControl
{
    public partial class AddPointForm : Form
    {
        public event GetVarTable GetVarTableEvent;

        public PointInfo pointInfo;

        public Save saveData;

        private readonly ColorDialog colorDialog = new ColorDialog();

        private DataGridViewRow dataGridViewRow;

        public AddPointForm(Save saveData)
        {
            InitializeComponent();
            this.saveData = saveData;
        }

        public AddPointForm(Save saveData, DataGridViewRow dataGridViewRow)
        {
            InitializeComponent();
            this.saveData = saveData;
            this.dataGridViewRow = dataGridViewRow;
        }

        private void AddPointForm_Load(object sender, EventArgs e)
        {
            XAxisScope.Text = string.Concat("(", saveData.xAxisMin, ",", saveData.xAxisMax, ")");
            YAxisScope.Text = string.Concat("(", saveData.yAxisMin, ",", saveData.yAxisMax, ")");

            if (null != dataGridViewRow)
            {
                XAxisVar.Text = dataGridViewRow.Cells["XVar"].Value.ToString();
                YAxisVar.Text = dataGridViewRow.Cells["YVar"].Value.ToString();
                PointColor.BackColor = (Color)dataGridViewRow.Cells["PointColor"].Value;
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

            pointInfo = new PointInfo
            {
                XVar = XAxisVar.Text.Trim(),
                YVar = YAxisVar.Text.Trim(),
                PointColor = PointColor.BackColor
            };

            DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void SelectedXAxisVarButton_Click(object sender, EventArgs e)
        {
            var variables = GetVarTableEvent("").Split('|');
            if (variables.Length <= 0)
                return;

            XAxisVar.Text = variables.First();
        }

        private void SelectedYAxisVarButton_Click(object sender, EventArgs e)
        {
            var variables = GetVarTableEvent("").Split('|');
            if (variables.Length <= 0)
                return;

            YAxisVar.Text = variables.First();
        }

        private void PointColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            PointColor.BackColor = colorDialog.Color;
        }
    }
}
