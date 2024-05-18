using CommonSnappableTypes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace XYControl
{
    public partial class SetForm : Form
    {
        private readonly Save saveData;
        private readonly ColorDialog colorDialog = new ColorDialog();

        public event GetVarTable GetVarTableEvent;

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

            ChartTitle.Text = saveData.chartTitle;
            ChartTitleColor.BackColor = saveData.chartTitleColor;
            ChartTitleSize.Text =  saveData.chartTitleSize.ToString();

            XAxisTitle.Text = saveData.xAxisTitle;
            XAxisTitleForeColor.BackColor = saveData.xAxisTitleForeColor;
            XAxisTitleSize.Text = saveData.xAxisTitleSize.ToString();

            YAxisTitle.Text = saveData.yAxisTitle;
            YAxisTitleForeColor.BackColor = saveData.yAxisTitleForeColor;
            YAxisTitleSize.Text = saveData.yAxisTitleSize.ToString();

            DecimalPlace.Text = saveData.decimalPlace.ToString();
            SeriesBorderWidth.Text = saveData.seriesBorderWidth.ToString();
            RefreshInterval.Text = saveData.refreshInterval.ToString();

            XAxisMin.Text = saveData.xAxisMin.ToString();
            XAxisMax.Text = saveData.xAxisMax.ToString();
            YAxisMin.Text = saveData.yAxisMin.ToString();
            YAxisMax.Text = saveData.yAxisMax.ToString();

            PointDataGrid.Rows.Clear();
            foreach (var item in saveData.pointInfos)
            {
                PointDataGrid.Rows.Add(item.XVar, item.YVar, item.PointColor);
            }
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

            if (!int.TryParse(ChartTitleSize.Text.Trim(), out int chartTitleSize)
                || chartTitleSize <= 0)
            {
                MessageBox.Show("请输入正确的标题字体大小.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(XAxisTitleSize.Text.Trim(), out int xAxisTitleSize)
                || xAxisTitleSize <= 0)
            {
                MessageBox.Show("请输入正确的X信息字体大小.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(YAxisTitleSize.Text.Trim(), out int yAxisTitleSize)
                || yAxisTitleSize <= 0)
            {
                MessageBox.Show("请输入正确的Y信息字体大小.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(DecimalPlace.Text.Trim(), out int decimalPlace))
            {
                MessageBox.Show("请输入正确的小数位数.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(SeriesBorderWidth.Text.Trim(), out int seriesBorderWidth)
                || seriesBorderWidth <= 0)
            {
                MessageBox.Show("请输入正确的曲线宽度.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(RefreshInterval.Text.Trim(), out int refreshInterval)
                || refreshInterval <= 0)
            {
                MessageBox.Show("请输入正确的刷新时间.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(XAxisMin.Text.Trim(), out int xAxisMin))
            {
                MessageBox.Show("请输入正确的X最小值.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(XAxisMax.Text.Trim(), out int xAxisMax))
            {
                MessageBox.Show("请输入正确的X最大值.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (xAxisMin >= xAxisMax)
            {
                MessageBox.Show("请输入正确的X最小值和最大值.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(YAxisMin.Text.Trim(), out int yAxisMin))
            {
                MessageBox.Show("请输入正确的Y最小值.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(YAxisMax.Text.Trim(), out int yAxisMax))
            {
                MessageBox.Show("请输入正确的Y最大值.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (yAxisMin >= yAxisMax)
            {
                MessageBox.Show("请输入正确的Y最小值和最大值.", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            saveData.chartTitle = ChartTitle.Text.Trim();
            saveData.chartTitleColor = ChartTitleColor.BackColor;
            saveData.chartTitleSize = chartTitleSize;

            saveData.xAxisTitle = XAxisTitle.Text.Trim();
            saveData.xAxisTitleForeColor = XAxisTitleForeColor.BackColor;
            saveData.xAxisTitleSize = xAxisTitleSize;

            saveData.yAxisTitle = YAxisTitle.Text.Trim();
            saveData.yAxisTitleForeColor = YAxisTitleForeColor.BackColor;
            saveData.yAxisTitleSize = yAxisTitleSize;

            saveData.decimalPlace = decimalPlace;
            saveData.seriesBorderWidth = seriesBorderWidth;
            saveData.refreshInterval = refreshInterval;

            saveData.xAxisMin = xAxisMin;
            saveData.xAxisMax = xAxisMax;
            saveData.yAxisMin = yAxisMin;
            saveData.yAxisMax = yAxisMax;

            saveData.pointInfos.Clear();
            for (var i = 0;i< PointDataGrid.Rows.Count;i++)
            {
                var pointColor = (Color)PointDataGrid.Rows[i].Cells["PointColor"].Value;
                saveData.pointInfos.Add(new PointInfo()
                {
                    XVar = PointDataGrid.Rows[i].Cells["XVar"].Value.ToString(),
                    YVar = PointDataGrid.Rows[i].Cells["YVar"].Value.ToString(),
                    PointColor = null == pointColor ? Color.Green: pointColor,
                }); 
            }

            DialogResult = DialogResult.OK;
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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

        private void ChartTitleColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            ChartTitleColor.BackColor = colorDialog.Color;
        }

        private void XAxisTitleForeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            XAxisTitleForeColor.BackColor = colorDialog.Color;
        }

        private void YAxisTitleForeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            YAxisTitleForeColor.BackColor = colorDialog.Color;
        }

        #endregion

        #region Tab - 数据

        private void AddLineButton_Click(object sender, EventArgs e)
        {
            //XYSetCurveForm curveForm = new XYSetCurveForm(saveData, 0, 0, true);
            //if (curveForm.ShowDialog() == DialogResult.OK)
            //{
            //    saveData = curveForm.saveData;
            //    SetDataGruidView();
            //}
        }

        private void DeleteLineButton_Click(object sender, EventArgs e)
        {
            //this.saveData.Points.Series.Clear();
            //this.dataGridView1.Rows.Clear();
            //this.dataGridView1.Columns.Clear();
            //MessageBox.Show("曲线已清空！");
        }

        private void AddPointButton_Click(object sender, EventArgs e)
        {
            var form = new AddPointForm(saveData);
            form.GetVarTableEvent += GetVarTableEvent;
            if (DialogResult.OK != form.ShowDialog())
                return;

            PointDataGrid.Rows.Add(form.pointInfo.XVar, form.pointInfo.YVar, form.pointInfo.PointColor);
        }

        private void DeletePointButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认删除选中的动态点？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                for (int i = 0; i < PointDataGrid.SelectedRows.Count; i++)
                {
                    PointDataGrid.Rows.RemoveAt(i);
                }
            }
        }

        private void PointDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var form = new AddPointForm(saveData, PointDataGrid.Rows[e.RowIndex]);
            form.GetVarTableEvent += GetVarTableEvent;
            if (DialogResult.OK != form.ShowDialog())
                return;

            var pointInfo = form.pointInfo;
            PointDataGrid.Rows[e.RowIndex].Cells["XVar"].Value = pointInfo.XVar;
            PointDataGrid.Rows[e.RowIndex].Cells["YVar"].Value = pointInfo.YVar;
            PointDataGrid.Rows[e.RowIndex].Cells["PointColor"].Value = pointInfo.PointColor;
        }

        #endregion

    }
}
