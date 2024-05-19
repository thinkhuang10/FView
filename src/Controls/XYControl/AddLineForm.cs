using CommonSnappableTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace XYControl
{
    public partial class AddLineForm : Form
    {
        private Save saveData;

        public event GetVarTable GetVarTableEvent;

        public LineInfo lineInfo;

        private readonly ColorDialog colorDialog = new ColorDialog();

        public AddLineForm(Save saveData)
        {
            InitializeComponent();
            this.saveData = saveData;
        }

        public AddLineForm(Save saveData, LineInfo lineInfo)
        {
            InitializeComponent();
            this.saveData = saveData;
            this.lineInfo = lineInfo;
        }

        private void AddLineForm_Load(object sender, EventArgs e)
        {
            XAxisScope.Text = string.Concat("(", saveData.xAxisMin, ",", saveData.xAxisMax, ")");
            YAxisScope.Text = string.Concat("(", saveData.yAxisMin, ",", saveData.yAxisMax, ")");

            if (null != lineInfo)
            {
                LineColor.BackColor = lineInfo.LineColor;
                foreach (var pointInfo in lineInfo.PointInfos)
                {
                    LineGridView.Rows.Add(pointInfo.XVar, pointInfo.YVar);
                }
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
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

            LineGridView.Rows.Add(XAxisVar.Text.Trim(), YAxisVar.Text.Trim());
        }

        private void ModifyButton_Click(object sender, EventArgs e)
        {
            for (var i = LineGridView.SelectedRows.Count; i >= 1; i--)
            {
                var rowIndex = LineGridView.SelectedRows[i - 1].Index;
                LineGridView.Rows[rowIndex].Cells["XVar"].Value = XAxisVar.Text;
                LineGridView.Rows[rowIndex].Cells["YVar"].Value = YAxisVar.Text;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show("确认删除选中的动态点？", "提示", MessageBoxButtons.OKCancel);
            if (dr != DialogResult.OK)
                return;

            for (var i = LineGridView.SelectedRows.Count; i >= 1; i--)
            {
                LineGridView.Rows.RemoveAt(LineGridView.SelectedRows[i - 1].Index);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (0 == LineGridView.Rows.Count)
            {
                DialogResult = DialogResult.OK;
                return;
            }

            var pointInfos = new List<PointInfo>();
            for (var i = 0; i< LineGridView.Rows.Count; i++)
            {
                pointInfos.Add(new PointInfo()
                {
                    XVar = LineGridView.Rows[i].Cells["XVar"].Value.ToString(),
                    YVar = LineGridView.Rows[i].Cells["YVar"].Value.ToString()
                });
            }

            lineInfo = new LineInfo
            {
                LineColor = LineColor.BackColor,
                PointInfos = pointInfos
            };

            DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void LineGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            if (LineGridView.Rows[e.RowIndex].Cells[0].Value == null ||
                LineGridView.Rows[e.RowIndex].Cells[1].Value == null)
                return;

            XAxisVar.Text = LineGridView.Rows[e.RowIndex].Cells["XVar"].Value.ToString();
            YAxisVar.Text = LineGridView.Rows[e.RowIndex].Cells["YVar"].Value.ToString();
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

        private void LineColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            LineColor.BackColor = colorDialog.Color;
        }

    }
}
