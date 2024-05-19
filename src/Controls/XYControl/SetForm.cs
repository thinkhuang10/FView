using CommonSnappableTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace XYControl
{
    public partial class SetForm : Form
    {
        private readonly Save saveData;
        private readonly ColorDialog colorDialog = new ColorDialog();
        private const string LineGridViewFirstColumnName = "No";

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

            // 初始化点表格
            foreach (var item in saveData.pointInfos)
            {
                PointDataGrid.Rows.Add(item.XVar, item.YVar, item.PointColor);
            }

            // 初始化曲线表格
            LineDataGrid.Columns.Add(new DataGridViewColumn
            {
                Name = LineGridViewFirstColumnName,
                HeaderText = "序号",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = new DataGridViewTextBoxCell()
            });
            LineDataGrid.Columns[LineGridViewFirstColumnName].Width = 60;

            foreach (var item in saveData.lineInfos)
            {
                var columnNum = LineDataGrid.Columns.Add(new DataGridViewColumn
                {
                    Name = "曲线" + LineDataGrid.ColumnCount,
                    HeaderText = "曲线" + LineDataGrid.ColumnCount,
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    CellTemplate = new DataGridViewTextBoxCell()
                });

                SetLineDataGrid(item, columnNum);
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            #region 验证输入

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

            #endregion

            #region 保存配置到序列化文件中

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

            saveData.lineInfos.Clear();
            for (var i = 1; i < LineDataGrid.Columns.Count; i++)
            {
                var lineColor = (Color)LineDataGrid.Rows[0].Cells[i].Value;
                var pointInfos = new List<PointInfo>();
                for (var j = 1; j < LineDataGrid.Rows.Count; j++)
                {
                    var cellValue = LineDataGrid.Rows[j].Cells[i].Value;
                    if (null == cellValue || string.IsNullOrEmpty(cellValue.ToString()))
                        continue;

                    var vals = cellValue.ToString().Split(',');
                    if (2 != vals.Length)
                        continue;

                    pointInfos.Add(new PointInfo()
                    {
                        XVar = vals[0],
                        YVar = vals[1]
                    });
                }

                saveData.lineInfos.Add(new LineInfo() { 
                    LineColor = lineColor,
                    PointInfos = pointInfos
                });
            }

            #endregion

            DialogResult = DialogResult.OK;
        }

        private void ExitButton_Click(object sender, EventArgs e)
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
            var form = new AddLineForm(XAxisMin.Text.Trim(), XAxisMax.Text.Trim(),
                YAxisMin.Text.Trim(), YAxisMax.Text.Trim());
            form.GetVarTableEvent += GetVarTableEvent;
            if (form.ShowDialog() != DialogResult.OK)
                return;

            if (null != form.lineInfo)
            {
                var column = new DataGridViewColumn
                {
                    Name = "曲线" + LineDataGrid.ColumnCount,
                    HeaderText = "曲线" + LineDataGrid.ColumnCount,
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    CellTemplate = new DataGridViewTextBoxCell()
                };
                var columnNum = LineDataGrid.Columns.Add(column);

                SetLineDataGrid(form.lineInfo, columnNum);
            }
        }

        private void SetLineDataGrid(LineInfo lineInfo, int columnNum)
        {
            if (lineInfo.PointInfos.Count + 1 > LineDataGrid.Rows.Count)
            {
                LineDataGrid.Rows.Add(lineInfo.PointInfos.Count + 1 - LineDataGrid.Rows.Count);
            }

            LineDataGrid.Rows[0].Cells[0].Value = "颜色";
            for (var i = 1; i < LineDataGrid.Rows.Count; i++)
            {
                LineDataGrid.Rows[i].Cells[0].Value = i;
            }

            LineDataGrid.Columns[columnNum].Width = 230;
            LineDataGrid.Rows[0].Cells[columnNum].Value = lineInfo.LineColor;
            for (var i = 1; i <= lineInfo.PointInfos.Count; i++)
            {
                var XVar = lineInfo.PointInfos[i - 1].XVar;
                var YVar = lineInfo.PointInfos[i - 1].YVar;
                LineDataGrid.Rows[i].Cells[columnNum].Value = string.Concat(XVar, ",", YVar);
            }
        }

        private void DeleteLineButton_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show("确认删除选中的曲线？", "提示", MessageBoxButtons.OKCancel);
            if (dr != DialogResult.OK)
                return;

            for (var i = LineDataGrid.SelectedColumns.Count; i >= 1; i--)
            {
                // 表格的第一列不能被删除
                if (0 == LineDataGrid.SelectedColumns[i - 1].Index)
                    continue;

                LineDataGrid.Columns.RemoveAt(LineDataGrid.SelectedColumns[i - 1].Index);
            }
        }

        private void LineDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 获取选中曲线绑定的所有变量
            var pointInfos = new List<PointInfo>();
            for (var i = 1; i < LineDataGrid.Rows.Count; i++)
            {
                var cellValue = LineDataGrid.Rows[i].Cells[e.ColumnIndex].Value;
                if (null == cellValue || string.IsNullOrEmpty(cellValue.ToString()))
                    continue;

                var vals = cellValue.ToString().Split(',');
                if (2 != vals.Length)
                    continue;

                pointInfos.Add(new PointInfo()
                {
                    XVar = vals[0],
                    YVar = vals[1]
                });
            }

            var form = new AddLineForm(XAxisMin.Text.Trim(), XAxisMax.Text.Trim(),
                YAxisMin.Text.Trim(), YAxisMax.Text.Trim(), new LineInfo
                {
                    LineColor = (Color)LineDataGrid.Rows[0].Cells[e.ColumnIndex].Value,
                    PointInfos = pointInfos
                });
            form.GetVarTableEvent += GetVarTableEvent;
            if (DialogResult.OK != form.ShowDialog())
                return;

            SetLineDataGrid(form.lineInfo, e.ColumnIndex);
        }

        private void AddPointButton_Click(object sender, EventArgs e)
        {
            var form = new AddPointForm(XAxisMin.Text.Trim(), XAxisMax.Text.Trim(),
                YAxisMin.Text.Trim(), YAxisMax.Text.Trim());
            form.GetVarTableEvent += GetVarTableEvent;
            if (DialogResult.OK != form.ShowDialog())
                return;

            if (null == form.PointInfo)
                return;

            PointDataGrid.Rows.Add(form.PointInfo.XVar, form.PointInfo.YVar, form.PointInfo.PointColor);
        }

        private void DeletePointButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确认删除选中的动态点？", "提示", MessageBoxButtons.OKCancel))
                return;

            for (var i = PointDataGrid.SelectedRows.Count; i >= 1; i--) 
            {
                PointDataGrid.Rows.RemoveAt(PointDataGrid.SelectedRows[i - 1].Index);
            }
        }

        private void PointDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = PointDataGrid.Rows[e.RowIndex];
            var form = new AddPointForm(XAxisMin.Text.Trim(), XAxisMax.Text.Trim(),
                YAxisMin.Text.Trim(), YAxisMax.Text.Trim(), new PointInfo
            {
                XVar = row.Cells["XVar"].Value.ToString(),
                YVar = row.Cells["YVar"].Value.ToString(),
                PointColor = (Color)row.Cells["PointColor"].Value
            });
            form.GetVarTableEvent += GetVarTableEvent;
            if (DialogResult.OK != form.ShowDialog())
                return;

            var pointInfo = form.PointInfo;
            if (null != pointInfo)
            {
                row.Cells["XVar"].Value = pointInfo.XVar;
                row.Cells["YVar"].Value = pointInfo.YVar;
                row.Cells["PointColor"].Value = pointInfo.PointColor;
            }
        }

        #endregion

    }
}
