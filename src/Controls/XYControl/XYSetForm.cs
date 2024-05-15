using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using XYControl;
using OxyPlot.Annotations;

namespace FControl.SetFroms
{
    public partial class XYSetForm : Form
    {
        private XYSave saveData = new XYSave();
        private ColorDialog colorDialog1;

        public XYSetForm(XYSave save)
        {
            InitializeComponent();
            saveData = save;
        }

        private void XYSetForm_Load(object sender, EventArgs e)
        {
            colorDialog1 = new ColorDialog();
            txtTitle.Text = saveData.Title;
            txtXLabel.Text = saveData.XLabel;
            txtYLabel.Text = saveData.YLabel;
            lblForeColor.BackColor = OxyColorToColor(saveData.ForegroundColor);
            lblBackColor.BackColor = OxyColorToColor(saveData.BackgroundColor);
            lblGridColor.BackColor = OxyColorToColor(saveData.GridColor);
            lblMarkColor.BackColor = OxyColorToColor(saveData.MarkColor);
            lblXLabelColor.BackColor = OxyColorToColor(saveData.XLabelColor);
            lblYLabelColor.BackColor = OxyColorToColor(saveData.YLabelColor);
            txtMinX.Text = saveData.XMin.ToString();
            txtMaxX.Text = saveData.XMax.ToString();
            txtMinY.Text = saveData.YMin.ToString();
            txtMaxY.Text = saveData.YMax.ToString();

            SetDataGruidView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveData.Title = txtTitle.Text;
            saveData.XLabel = txtXLabel.Text;
            saveData.YLabel = txtYLabel.Text;
            saveData.ForegroundColor = ColorToOxyColor(lblForeColor.BackColor);
            saveData.BackgroundColor = ColorToOxyColor(lblBackColor.BackColor);
            saveData.GridColor = ColorToOxyColor(lblGridColor.BackColor);
            saveData.MarkColor = ColorToOxyColor(lblMarkColor.BackColor);
            saveData.XLabelColor = ColorToOxyColor(lblXLabelColor.BackColor);
            saveData.YLabelColor = ColorToOxyColor(lblYLabelColor.BackColor);
            saveData.XMin = double.Parse(txtMinX.Text);
            saveData.XMax = double.Parse(txtMaxX.Text);
            saveData.YMin = double.Parse(txtMinY.Text);
            saveData.YMax = double.Parse(txtMaxY.Text);

            base.DialogResult = DialogResult.OK;
            Close();
        }

        private void lblForeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                lblForeColor.BackColor = colorDialog1.Color;
        }

        private void lblBackColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                lblBackColor.BackColor = colorDialog1.Color;
        }

        private void lblGridColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                lblGridColor.BackColor = colorDialog1.Color;
        }

        private void lblMarkColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                lblMarkColor.BackColor = colorDialog1.Color;
        }

        private void lblXLabelColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                lblXLabelColor.BackColor = colorDialog1.Color;
        }

        private void lblYLabelColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                lblYLabelColor.BackColor = colorDialog1.Color;
        }

        private OxyColor ColorToOxyColor(Color color)
        {
            return OxyColor.FromArgb(color.A, color.R, color.G, color.B);
        }

        private Color OxyColorToColor(OxyColor color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            XYSetCurveForm curveForm = new XYSetCurveForm(saveData, 0, 0, true);
            if (curveForm.ShowDialog() == DialogResult.OK)
            {
                saveData = curveForm.saveData;
                SetDataGruidView();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            XYSetCurveForm curveForm = new XYSetCurveForm(saveData, e.ColumnIndex, 1, true);
            if (curveForm.ShowDialog() == DialogResult.OK)
            {
                saveData = curveForm.saveData;
                SetDataGruidView();
            }
        }

        private void SetDataGruidView()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            DataGridViewRow row = new DataGridViewRow();
            dataGridView1.Columns.Add("No", "序号");
            dataGridView1.Columns["No"].ReadOnly = true;
            int i = 0;
            foreach (var item in saveData.Points.Series)
            {
                i++;
                dataGridView1.Columns.Add("Curve" + i.ToString(), "曲线" + i.ToString());
                dataGridView1.Columns["Curve" + i.ToString()].ReadOnly = true;
            }
            i = 0;
            int z = 1;
            foreach (var item in saveData.Points.Series)
            {
                if (i == 0)
                {
                    int j = dataGridView1.Rows.Add(1, "ARGB(" + item.TextColor.A.ToString() + "," + item.TextColor.R.ToString() + "," + item.TextColor.G.ToString() + "," + item.TextColor.B.ToString() + ")");
                    foreach (var point in ((DataPointSeries)item).Points)
                    {
                        row = new DataGridViewRow();
                        dataGridView1.Rows.Add(dataGridView1.Rows.Count.ToString(), point.X.ToString() + "," + point.Y.ToString());
                    }
                    z++;
                }
                else
                {
                    dataGridView1.Rows[0].Cells[z].Value = "ARGB(" + item.TextColor.A.ToString() + "," + item.TextColor.R.ToString() + "," + item.TextColor.G.ToString() + "," + item.TextColor.B.ToString() + ")";
                    int a = 1;
                    foreach (var point in ((DataPointSeries)item).Points)
                    {
                        dataGridView1.Rows[a].Cells[z].Value = point.X.ToString() + "," + point.Y.ToString();
                        a++;
                    }
                    z++;
                }
                i++;
            }

            foreach (var item in saveData.Points.Annotations)
            {
                var annotation = (PointAnnotation)item;
                dataGridView2.Rows.Add(annotation.X, annotation.Y, "RGB(" + annotation.Fill.R.ToString() + "," + annotation.Fill.G.ToString() + "," + annotation.Fill.B.ToString() + ")");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.saveData.Points.Series.Clear();
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();
            MessageBox.Show("曲线已清空！");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.saveData.Points.Annotations.Clear();
            this.dataGridView2.Rows.Clear();
            MessageBox.Show("标注已清空！");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            XYSetCurveForm curveForm = new XYSetCurveForm(saveData, 1, 0, false);
            if (curveForm.ShowDialog() == DialogResult.OK)
            {
                saveData = curveForm.saveData;
                SetDataGruidView();
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            XYSetCurveForm curveForm = new XYSetCurveForm(saveData, e.RowIndex,1, false);
            if (curveForm.ShowDialog() == DialogResult.OK)
            {
                saveData = curveForm.saveData;
                SetDataGruidView();
            }
        }
    }
}
