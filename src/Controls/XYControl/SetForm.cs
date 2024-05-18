using System;
using System.Windows.Forms;

namespace XYControl
{
    public partial class SetForm : Form
    {
        private Save saveData;
        private readonly ColorDialog colorDialog = new ColorDialog();

        public SetForm(Save saveData)
        {
            InitializeComponent();
            this.saveData = saveData;
        }

        private void XYSetForm_Load(object sender, EventArgs e)
        {
            ChartForeColor.BackColor = saveData.chartForeColor;
            ChartBackColor.BackColor = saveData.chartBackColor;
            GridColor.BackColor = saveData.gridColor;
            AxisLabelColor.BackColor = saveData.axisLabelColor;
            HorizonalGridCount.Text = saveData.horizonalGridCount.ToString();
            VerticalGridCount.Text = saveData.verticalGridCount.ToString();
            DynamicPointSize.Text = saveData.dynamicPointSize.ToString();

            IsShowDynamicPointLabel.Checked = saveData.isShowDynamicPointLabel;
            DynamicPointLabelForeColor.BackColor = saveData.dynamicPointLabelForeColor;
            DynamicPointLabelBackColor.BackColor = saveData.dynamicPointLabelBackColor;
            DynamicPointLabelSize.Text = saveData.dynamicPointLabelSize.ToString();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (!uint.TryParse(HorizonalGridCount.Text.Trim(), out uint horizonalGridCount)
                || 0 == horizonalGridCount)
            {
                MessageBox.Show("请输入正确的水平网格数.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!uint.TryParse(VerticalGridCount.Text.Trim(), out uint verticalGridCount)
                || 0 == verticalGridCount)
            {
                MessageBox.Show("请输入正确的垂直网格数.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(DynamicPointSize.Text.Trim(), out int dynamicPointSize)
                || dynamicPointSize <= 0) 
            {
                MessageBox.Show("请输入正确的动态点直径.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(DynamicPointLabelSize.Text.Trim(), out int dynamicPointLabelSize)
                || dynamicPointLabelSize <= 0)
            {
                MessageBox.Show("请输入正确的动态点字体大小.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            saveData.chartForeColor = ChartForeColor.BackColor;
            saveData.chartBackColor = ChartBackColor.BackColor;
            saveData.gridColor = GridColor.BackColor;
            saveData.axisLabelColor = AxisLabelColor.BackColor;

            saveData.horizonalGridCount = horizonalGridCount;
            saveData.verticalGridCount = verticalGridCount;

            saveData.dynamicPointSize = dynamicPointSize;

            saveData.isShowDynamicPointLabel = IsShowDynamicPointLabel.Checked;
            saveData.dynamicPointLabelForeColor = DynamicPointLabelForeColor.BackColor;
            saveData.dynamicPointLabelBackColor = DynamicPointLabelBackColor.BackColor;
            saveData.dynamicPointLabelSize = dynamicPointLabelSize;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void lblXLabelColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
                lblXLabelColor.BackColor = colorDialog.Color;
        }

        private void lblYLabelColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
                lblYLabelColor.BackColor = colorDialog.Color;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //XYSetCurveForm curveForm = new XYSetCurveForm(saveData, 0, 0, true);
            //if (curveForm.ShowDialog() == DialogResult.OK)
            //{
            //    saveData = curveForm.saveData;
            //    SetDataGruidView();
            //}
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //XYSetCurveForm curveForm = new XYSetCurveForm(saveData, e.ColumnIndex, 1, true);
            //if (curveForm.ShowDialog() == DialogResult.OK)
            //{
            //    saveData = curveForm.saveData;
            //    SetDataGruidView();
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.saveData.Points.Series.Clear();
            //this.dataGridView1.Rows.Clear();
            //this.dataGridView1.Columns.Clear();
            //MessageBox.Show("曲线已清空！");
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //this.saveData.Points.Annotations.Clear();
            //this.dataGridView2.Rows.Clear();
            //MessageBox.Show("标注已清空！");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //XYSetCurveForm curveForm = new XYSetCurveForm(saveData, 1, 0, false);
            //if (curveForm.ShowDialog() == DialogResult.OK)
            //{
            //    saveData = curveForm.saveData;
            //    SetDataGruidView();
            //}
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //XYSetCurveForm curveForm = new XYSetCurveForm(saveData, e.RowIndex,1, false);
            //if (curveForm.ShowDialog() == DialogResult.OK)
            //{
            //    saveData = curveForm.saveData;
            //    SetDataGruidView();
            //}
        }

        #region Tab - 常规
        private void ChartForeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            ChartForeColor.BackColor = colorDialog.Color;
        }

        private void ChartBackColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            ChartBackColor.BackColor = colorDialog.Color;
        }

        private void GridColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            GridColor.BackColor = colorDialog.Color;
        }

        private void AxisLabelColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            AxisLabelColor.BackColor = colorDialog.Color;
        }
        #endregion

        #region Tab - 数据


        #endregion

        private void IsShowDynamicPointLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (IsShowDynamicPointLabel.Checked)
            {
                DynamicPointLabelForeColor.Enabled = true;
                DynamicPointLabelBackColor.Enabled = true;
                DynamicPointLabelSize.Enabled = true;
            }
            else
            {
                DynamicPointLabelForeColor.Enabled = false;
                DynamicPointLabelBackColor.Enabled = false;
                DynamicPointLabelSize.Enabled = false;
            }
        }

        private void DynamicPointLabelForeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            DynamicPointLabelForeColor.BackColor = colorDialog.Color;
        }

        private void DynamicPointLabelBackColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            DynamicPointLabelBackColor.BackColor = colorDialog.Color;
        }
    }
}
