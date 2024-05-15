using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot.Series;
using FControl.XYDiagram;
using OxyPlot.Axes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using OxyPlot;
using OxyPlot.Annotations;

namespace FControl.SetFroms
{
    public partial class XYSetCurveForm : Form
    {
        public XYSave saveData = new XYSave();
        private XYSave tempSaveData = new XYSave();
        private ColorDialog colorDialog1 = new ColorDialog();
        private int seriesCount = 0;
        private int currentIndex = 0;
        private int addFlg;
        private bool isLine = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">数据对象</param>
        /// <param name="cell">点击列数</param>
        /// <param name="flg">是否是新增 新增：0 修改：1</param>
        /// <param name="line">点或线 线：true 点：false</param>
        public XYSetCurveForm(XYSave data, int cell, int flg, bool line)
        {
            InitializeComponent();
            tempSaveData = (XYSave)data.Clone();
            seriesCount = cell;
            addFlg = flg;
            isLine = line;
        }

        private void XYSetCurveForm_Load(object sender, EventArgs e)
        {
            txtXScope.Text = "(" + tempSaveData.XMin.ToString() + "," + tempSaveData.XMax.ToString() + ")";
            txtYScope.Text = "(" + tempSaveData.YMin.ToString() + "," + tempSaveData.YMax.ToString() + ")";
            
            if (addFlg == 0)
            {
                btnUpd.Enabled = false;
                if (isLine)
                {
                    seriesCount = tempSaveData.Points.Series.Count + 1;
                    var scatterSeries = new FunctionSeries();
                    scatterSeries.Background = OxyColors.Transparent;
                    tempSaveData.Points.Series.Add(scatterSeries);
                }
                else
                {
                    seriesCount = tempSaveData.Points.Series.Count;
                    var scatterSeries = new PointAnnotation();
                    scatterSeries.Fill = OxyColors.Transparent;
                    tempSaveData.Points.Annotations.Add(scatterSeries);
                }

            }
            else
            {
                btnAdd.Enabled = false;
                if (isLine)
                {
                    var series = tempSaveData.Points.Series[seriesCount - 1];
                    lblColor.BackColor = OxyColorToColor(series.Background);
                    foreach (var item in (series as DataPointSeries).Points)
                    {
                        dataGridView1.Rows.Add(item.X, item.Y);
                    }
                }
                else
                {
                    var annotation = (PointAnnotation)tempSaveData.Points.Annotations[seriesCount];
                    lblColor.BackColor = OxyColorToColor(annotation.Fill);
                    dataGridView1.Rows.Add(annotation.X, annotation.Y);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtX.Text) || string.IsNullOrEmpty(txtY.Text))
            {
                MessageBox.Show("请输入有效的坐标！");
                return;
            }
            double x = 0, y = 0;
            if (!double.TryParse(txtX.Text, out x) || !double.TryParse(txtY.Text, out y))
            {
                MessageBox.Show("请输入有效的坐标！");
                return;
            }
            if (tempSaveData.XMin >= x || x >= tempSaveData.XMax || tempSaveData.YMin >= y || y >= tempSaveData.YMax)
            {
                MessageBox.Show("请输入有效的坐标！");
                return;
            }
            dataGridView1.Rows.Add(x, y);

            if (isLine)
            {
                var lineSer = tempSaveData.Points.Series[seriesCount - 1] as LineSeries;
                lineSer.Background = ColorToOxyColor(lblColor.BackColor);
                lineSer.Points.Add(new DataPoint(x, y));

            }
            else
            {
                tempSaveData.Points.Annotations.Add(new PointAnnotation { X = x, Y = y, Fill = ColorToOxyColor(lblColor.BackColor) });
            }

            MessageBox.Show("坐标已添加！");
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[currentIndex].Cells[0].Value = txtX.Text;
            dataGridView1.Rows[currentIndex].Cells[1].Value = txtY.Text;

            if (isLine)
            {
                var lineSer = tempSaveData.Points.Series[seriesCount - 1] as LineSeries;
                lineSer.Points[currentIndex] = new DataPoint(double.Parse(txtX.Text), double.Parse(txtY.Text));
            }
            else
            {
                var annotation = (PointAnnotation)tempSaveData.Points.Annotations[seriesCount - 1];
                annotation.X = double.Parse(txtX.Text);
                annotation.Y = double.Parse(txtY.Text);
            }

            MessageBox.Show("坐标已更新！");
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Remove(dataGridView1.Rows[currentIndex]);
            if (isLine)
            {
                var lineSer = tempSaveData.Points.Series[seriesCount - 1] as LineSeries;
                lineSer.Points.RemoveAt(currentIndex);
            }
            else 
                tempSaveData.Points.Annotations.RemoveAt(seriesCount);

            MessageBox.Show("坐标已删除！");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isLine)
            {
                var lineSer = tempSaveData.Points.Series[seriesCount - 1] as LineSeries;
                lineSer.Background = ColorToOxyColor(lblColor.BackColor);
            }
            else
            {
                var annotation = (PointAnnotation)tempSaveData.Points.Annotations[seriesCount - 1];
                annotation.Fill = ColorToOxyColor(lblColor.BackColor);
            }
            saveData = tempSaveData;

            base.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            if (dataGridView1.Rows[e.RowIndex].Cells[0].Value == null ||
                dataGridView1.Rows[e.RowIndex].Cells[1].Value == null)
                return;

            txtX.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtY.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            currentIndex = e.RowIndex;
        }

        private void lblColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                lblColor.BackColor = colorDialog1.Color;
        }

        private Color OxyColorToColor(OxyColor color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        private OxyColor ColorToOxyColor(Color color)
        {
            return OxyColor.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
