using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class ysForm : XtraForm
{
    private readonly CGlobal theglobal;

    private readonly int c;

    private IContainer components;

    private Label label1;

    private TextBox textBox1;

    private ListView listView1;

    private Button button1;

    private Button button2;

    private Button button3;

    private Button button4;

    private Button button5;

    private TextBox textBox2;

    private TextBox textBox3;

    private TextBox textBox4;

    private ColumnHeader columnHeader1;

    private ColumnHeader columnHeader2;

    private ColumnHeader columnHeader3;

    private ColumnHeader columnHeader4;

    private ColorDialog colorDialog1;

    private CheckBox checkBox1;

    private Label label2;

    private Label label3;

    private Label label4;

    private Panel panel1;

    private Panel panel3;

    private Panel panel2;

    private Button button7;

    private Button button6;

    private Label label6;

    private Label label5;

    private Button button9;

    private ToolTip toolTip1;

    public ysForm(CGlobal _theglobal, int _c)
    {
        c = _c;
        theglobal = _theglobal;
        InitializeComponent();
    }

    private void textBox4_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            textBox4.BackColor = colorDialog1.Color;
        }
    }

    private void button3_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToSingle(textBox3.Text) < Convert.ToSingle(textBox2.Text))
            {
                MessageBox.Show("最大值小于最小值,请从新输入");
                return;
            }
            ListViewItem listViewItem = new(textBox2.Text);
            ListViewItem.ListViewSubItem item = new(listViewItem, textBox3.Text);
            ListViewItem.ListViewSubItem listViewSubItem = new(listViewItem, textBox4.BackColor.ToString())
            {
                BackColor = textBox4.BackColor,
                ForeColor = textBox4.BackColor
            };
            ListViewItem.ListViewSubItem item2 = new(listViewItem, checkBox1.Checked ? "闪烁" : "无闪烁");
            listViewItem.SubItems.Add(item);
            listViewItem.SubItems.Add(listViewSubItem);
            listViewItem.SubItems.Add(item2);
            listViewItem.UseItemStyleForSubItems = false;
            listView1.Items.Add(listViewItem);
        }
        catch (Exception)
        {
            MessageBox.Show("请输入合法变量值");
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count != 0)
        {
            listView1.Items.Remove(listView1.SelectedItems[0]);
        }
    }

    private void button5_Click(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count == 0)
        {
            return;
        }
        try
        {
            if (Convert.ToSingle(textBox3.Text) < Convert.ToSingle(textBox2.Text))
            {
                MessageBox.Show("最大值小于最小值,请从新输入");
                return;
            }
            listView1.SelectedItems[0].SubItems[0].Text = textBox2.Text;
            listView1.SelectedItems[0].SubItems[1].Text = textBox3.Text;
            listView1.SelectedItems[0].SubItems[2].Text = textBox4.BackColor.ToString();
            listView1.SelectedItems[0].SubItems[2].BackColor = textBox4.BackColor;
            listView1.SelectedItems[0].SubItems[3].Text = (checkBox1.Checked ? "闪烁" : "无闪烁");
        }
        catch (Exception)
        {
            MessageBox.Show("请输入合法变量值");
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            switch (c)
            {
                case 1:
                    {
                        theglobal.SelectedShapeList[0].bxysbhbianliang = textBox1.Text;
                        int count3 = listView1.Items.Count;
                        if (count3 == 0)
                        {
                            theglobal.SelectedShapeList[0].bxysbh = false;
                            break;
                        }
                        theglobal.SelectedShapeList[0].bxysbhmin = new float[count3];
                        theglobal.SelectedShapeList[0].bxysbhmax = new float[count3];
                        theglobal.SelectedShapeList[0].bxysbhys = new Color[count3];
                        theglobal.SelectedShapeList[0].bxysbhss = new bool[count3];
                        for (int k = 0; k < listView1.Items.Count; k++)
                        {
                            theglobal.SelectedShapeList[0].bxysbhmin[k] = Convert.ToSingle(listView1.Items[k].SubItems[0].Text);
                            theglobal.SelectedShapeList[0].bxysbhmax[k] = Convert.ToSingle(listView1.Items[k].SubItems[1].Text);
                            ref Color reference3 = ref theglobal.SelectedShapeList[0].bxysbhys[k];
                            reference3 = listView1.Items[k].SubItems[2].BackColor;
                            theglobal.SelectedShapeList[0].bxysbhss[k] = listView1.Items[k].SubItems[3].Text == "闪烁";
                        }
                        if (!theglobal.SelectedShapeList[0].bxysbh && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            theglobal.SelectedShapeList[0].bxysbh = true;
                        }
                        break;
                    }
                case 2:
                    {
                        theglobal.SelectedShapeList[0].tcs1ysbhbianliang = textBox1.Text;
                        int count2 = listView1.Items.Count;
                        if (count2 == 0)
                        {
                            theglobal.SelectedShapeList[0].tcs1ysbh = false;
                            break;
                        }
                        theglobal.SelectedShapeList[0].tcs1ysbhmin = new float[count2];
                        theglobal.SelectedShapeList[0].tcs1ysbhmax = new float[count2];
                        theglobal.SelectedShapeList[0].tcs1ysbhys = new Color[count2];
                        theglobal.SelectedShapeList[0].tcs1ysbhss = new bool[count2];
                        for (int j = 0; j < listView1.Items.Count; j++)
                        {
                            theglobal.SelectedShapeList[0].tcs1ysbhmin[j] = Convert.ToSingle(listView1.Items[j].SubItems[0].Text);
                            theglobal.SelectedShapeList[0].tcs1ysbhmax[j] = Convert.ToSingle(listView1.Items[j].SubItems[1].Text);
                            ref Color reference2 = ref theglobal.SelectedShapeList[0].tcs1ysbhys[j];
                            reference2 = listView1.Items[j].SubItems[2].BackColor;
                            theglobal.SelectedShapeList[0].tcs1ysbhss[j] = listView1.Items[j].SubItems[3].Text == "闪烁";
                        }
                        if (!theglobal.SelectedShapeList[0].tcs1ysbh && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            theglobal.SelectedShapeList[0].tcs1ysbh = true;
                        }
                        break;
                    }
                case 3:
                    {
                        theglobal.SelectedShapeList[0].tcs2ysbhbianliang = textBox1.Text;
                        int count = listView1.Items.Count;
                        if (count == 0)
                        {
                            theglobal.SelectedShapeList[0].tcs2ysbh = false;
                            break;
                        }
                        theglobal.SelectedShapeList[0].tcs2ysbhmin = new float[count];
                        theglobal.SelectedShapeList[0].tcs2ysbhmax = new float[count];
                        theglobal.SelectedShapeList[0].tcs2ysbhys = new Color[count];
                        theglobal.SelectedShapeList[0].tcs2ysbhss = new bool[count];
                        for (int i = 0; i < listView1.Items.Count; i++)
                        {
                            theglobal.SelectedShapeList[0].tcs2ysbhmin[i] = Convert.ToSingle(listView1.Items[i].SubItems[0].Text);
                            theglobal.SelectedShapeList[0].tcs2ysbhmax[i] = Convert.ToSingle(listView1.Items[i].SubItems[1].Text);
                            ref Color reference = ref theglobal.SelectedShapeList[0].tcs2ysbhys[i];
                            reference = listView1.Items[i].SubItems[2].BackColor;
                            theglobal.SelectedShapeList[0].tcs2ysbhss[i] = listView1.Items[i].SubItems[3].Text == "闪烁";
                        }
                        if (!theglobal.SelectedShapeList[0].tcs2ysbh && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            theglobal.SelectedShapeList[0].tcs2ysbh = true;
                        }
                        break;
                    }
            }
            base.DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception)
        {
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void textBox1_Click(object sender, EventArgs e)
    {
    }

    private void ysForm_Load(object sender, EventArgs e)
    {
        panel1.Location = new Point(16, 61);
        try
        {
            switch (c)
            {
                case 1:
                    {
                        textBox1.Text = theglobal.SelectedShapeList[0].bxysbhbianliang;
                        for (int j = 0; j < theglobal.SelectedShapeList[0].bxysbhmin.Length; j++)
                        {
                            ListViewItem listViewItem2 = new(new string[4]
                            {
                        theglobal.SelectedShapeList[0].bxysbhmin[j].ToString(),
                        theglobal.SelectedShapeList[0].bxysbhmax[j].ToString(),
                        theglobal.SelectedShapeList[0].bxysbhys[j].ToString(),
                        theglobal.SelectedShapeList[0].bxysbhss[j] ? "闪烁" : "无闪烁"
                            });
                            listViewItem2.SubItems[2].BackColor = theglobal.SelectedShapeList[0].bxysbhys[j];
                            listViewItem2.SubItems[2].ForeColor = theglobal.SelectedShapeList[0].bxysbhys[j];
                            listViewItem2.UseItemStyleForSubItems = false;
                            listView1.Items.Add(listViewItem2);
                        }
                        break;
                    }
                case 2:
                    {
                        if (theglobal.SelectedShapeList[0].boolysbh)
                        {
                            theglobal.SelectedShapeList[0].tcs1ysbhbianliang = theglobal.SelectedShapeList[0].boolysbhbianliang;
                            theglobal.SelectedShapeList[0].tcs1ysbhmin = new float[2] { -1f, 0f };
                            theglobal.SelectedShapeList[0].tcs1ysbhmax = new float[2] { 0f, 1f };
                            theglobal.SelectedShapeList[0].tcs1ysbhys = new Color[2]
                            {
                        theglobal.SelectedShapeList[0].boolysbhfalsecolor,
                        theglobal.SelectedShapeList[0].boolysbhtruecolor
                            };
                            CShape cShape = theglobal.SelectedShapeList[0];
                            bool[] tcs1ysbhss = new bool[2];
                            cShape.tcs1ysbhss = tcs1ysbhss;
                            theglobal.SelectedShapeList[0].tcs1ysbh = true;
                            theglobal.SelectedShapeList[0].boolysbh = false;
                        }
                        textBox1.Text = theglobal.SelectedShapeList[0].tcs1ysbhbianliang;
                        for (int k = 0; k < theglobal.SelectedShapeList[0].tcs1ysbhmin.Length; k++)
                        {
                            ListViewItem listViewItem3 = new(new string[4]
                            {
                        theglobal.SelectedShapeList[0].tcs1ysbhmin[k].ToString(),
                        theglobal.SelectedShapeList[0].tcs1ysbhmax[k].ToString(),
                        theglobal.SelectedShapeList[0].tcs1ysbhys[k].ToString(),
                        theglobal.SelectedShapeList[0].tcs1ysbhss[k] ? "闪烁" : "无闪烁"
                            });
                            listViewItem3.SubItems[2].BackColor = theglobal.SelectedShapeList[0].tcs1ysbhys[k];
                            listViewItem3.SubItems[2].ForeColor = theglobal.SelectedShapeList[0].tcs1ysbhys[k];
                            listViewItem3.UseItemStyleForSubItems = false;
                            listView1.Items.Add(listViewItem3);
                        }
                        break;
                    }
                case 3:
                    {
                        textBox1.Text = theglobal.SelectedShapeList[0].tcs2ysbhbianliang;
                        for (int i = 0; i < theglobal.SelectedShapeList[0].tcs2ysbhmin.Length; i++)
                        {
                            ListViewItem listViewItem = new(new string[4]
                            {
                        theglobal.SelectedShapeList[0].tcs2ysbhmin[i].ToString(),
                        theglobal.SelectedShapeList[0].tcs2ysbhmax[i].ToString(),
                        theglobal.SelectedShapeList[0].tcs2ysbhys[i].ToString(),
                        theglobal.SelectedShapeList[0].tcs2ysbhss[i] ? "闪烁" : "无闪烁"
                            });
                            listViewItem.SubItems[2].BackColor = theglobal.SelectedShapeList[0].tcs2ysbhys[i];
                            listViewItem.SubItems[2].ForeColor = theglobal.SelectedShapeList[0].tcs2ysbhys[i];
                            listViewItem.UseItemStyleForSubItems = false;
                            listView1.Items.Add(listViewItem);
                        }
                        break;
                    }
            }
        }
        catch (Exception)
        {
        }
    }

    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listView1.SelectedItems.Count != 0)
        {
            textBox2.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox4.BackColor = listView1.SelectedItems[0].SubItems[2].BackColor;
            checkBox1.Checked = listView1.SelectedItems[0].SubItems[3].Text == "闪烁";
        }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
    }

    private void panel2_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            panel2.BackColor = colorDialog1.Color;
        }
    }

    private void panel3_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            panel3.BackColor = colorDialog1.Color;
        }
    }

    private void panel2_Paint(object sender, PaintEventArgs e)
    {
    }

    private void button7_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
        Close();
    }

    private void button9_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent != "")
        {
            int selectionStart = textBox1.SelectionStart;
            int selectionLength = textBox1.SelectionLength;
            string text = textBox1.Text.Remove(selectionStart, selectionLength);
            textBox1.Text = text.Insert(selectionStart, "[" + varTableEvent + "]");
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        label1 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        listView1 = new System.Windows.Forms.ListView();
        columnHeader1 = new System.Windows.Forms.ColumnHeader();
        columnHeader2 = new System.Windows.Forms.ColumnHeader();
        columnHeader3 = new System.Windows.Forms.ColumnHeader();
        columnHeader4 = new System.Windows.Forms.ColumnHeader();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        button4 = new System.Windows.Forms.Button();
        button5 = new System.Windows.Forms.Button();
        textBox2 = new System.Windows.Forms.TextBox();
        textBox3 = new System.Windows.Forms.TextBox();
        textBox4 = new System.Windows.Forms.TextBox();
        colorDialog1 = new System.Windows.Forms.ColorDialog();
        checkBox1 = new System.Windows.Forms.CheckBox();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        panel1 = new System.Windows.Forms.Panel();
        label6 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        panel3 = new System.Windows.Forms.Panel();
        panel2 = new System.Windows.Forms.Panel();
        button7 = new System.Windows.Forms.Button();
        button6 = new System.Windows.Forms.Button();
        button9 = new System.Windows.Forms.Button();
        toolTip1 = new System.Windows.Forms.ToolTip(components);
        panel1.SuspendLayout();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(14, 21);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(31, 14);
        label1.TabIndex = 0;
        label1.Text = "变量";
        textBox1.HideSelection = false;
        textBox1.Location = new System.Drawing.Point(55, 17);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(222, 22);
        textBox1.TabIndex = 0;
        toolTip1.SetToolTip(textBox1, "此处也可以手动填写变量，格式为[变量名]");
        listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[4] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
        listView1.FullRowSelect = true;
        listView1.GridLines = true;
        listView1.HideSelection = false;
        listView1.Location = new System.Drawing.Point(16, 115);
        listView1.Name = "listView1";
        listView1.Size = new System.Drawing.Size(354, 322);
        listView1.TabIndex = 6;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.View = System.Windows.Forms.View.Details;
        listView1.SelectedIndexChanged += new System.EventHandler(listView1_SelectedIndexChanged);
        columnHeader1.Text = "最小值";
        columnHeader2.Text = "最大值";
        columnHeader3.Text = "颜色";
        columnHeader4.Text = "闪烁";
        button1.Location = new System.Drawing.Point(376, 377);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 10;
        button1.Text = "确定";
        toolTip1.SetToolTip(button1, "保存并退出");
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(376, 410);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 11;
        button2.Text = "取消";
        toolTip1.SetToolTip(button2, "退出不保存");
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        button3.Location = new System.Drawing.Point(376, 115);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(87, 27);
        button3.TabIndex = 7;
        button3.Text = "插入行";
        toolTip1.SetToolTip(button3, "插入一行到列表中");
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        button4.Location = new System.Drawing.Point(376, 148);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(87, 27);
        button4.TabIndex = 8;
        button4.Text = "删除行";
        toolTip1.SetToolTip(button4, "删除选中行");
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(button4_Click);
        button5.Location = new System.Drawing.Point(376, 182);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(87, 27);
        button5.TabIndex = 9;
        button5.Text = "编辑行";
        toolTip1.SetToolTip(button5, "修改选中行");
        button5.UseVisualStyleBackColor = true;
        button5.Click += new System.EventHandler(button5_Click);
        textBox2.Location = new System.Drawing.Point(16, 83);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(72, 22);
        textBox2.TabIndex = 2;
        textBox2.Text = "0";
        toolTip1.SetToolTip(textBox2, "当变量大于最小值小于等于最大值时显示对应颜色");
        textBox3.Location = new System.Drawing.Point(98, 83);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(72, 22);
        textBox3.TabIndex = 3;
        textBox3.Text = "100";
        toolTip1.SetToolTip(textBox3, "当变量大于最小值小于等于最大值时显示对应颜色");
        textBox4.Location = new System.Drawing.Point(177, 83);
        textBox4.Name = "textBox4";
        textBox4.Size = new System.Drawing.Size(72, 22);
        textBox4.TabIndex = 4;
        toolTip1.SetToolTip(textBox4, "单击改变颜色");
        textBox4.Click += new System.EventHandler(textBox4_Click);
        checkBox1.AutoSize = true;
        checkBox1.Location = new System.Drawing.Point(258, 87);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new System.Drawing.Size(50, 18);
        checkBox1.TabIndex = 5;
        checkBox1.Text = "闪烁";
        checkBox1.UseVisualStyleBackColor = true;
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(108, 61);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(43, 14);
        label2.TabIndex = 12;
        label2.Text = "最大值";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(29, 61);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(43, 14);
        label3.TabIndex = 13;
        label3.Text = "最小值";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(191, 61);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(31, 14);
        label4.TabIndex = 14;
        label4.Text = "颜色";
        panel1.Controls.Add(label6);
        panel1.Controls.Add(label5);
        panel1.Controls.Add(panel3);
        panel1.Controls.Add(panel2);
        panel1.Controls.Add(button7);
        panel1.Controls.Add(button6);
        panel1.Location = new System.Drawing.Point(472, 61);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(455, 376);
        panel1.TabIndex = 15;
        panel1.Visible = false;
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(50, 68);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(91, 14);
        label6.TabIndex = 5;
        label6.Text = "变量为假时颜色";
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(50, 24);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(91, 14);
        label5.TabIndex = 4;
        label5.Text = "变量为真时颜色";
        panel3.BackColor = System.Drawing.Color.Red;
        panel3.Location = new System.Drawing.Point(177, 63);
        panel3.Name = "panel3";
        panel3.Size = new System.Drawing.Size(101, 19);
        panel3.TabIndex = 3;
        panel3.Click += new System.EventHandler(panel3_Click);
        panel2.BackColor = System.Drawing.Color.LawnGreen;
        panel2.Location = new System.Drawing.Point(177, 20);
        panel2.Name = "panel2";
        panel2.Size = new System.Drawing.Size(101, 19);
        panel2.TabIndex = 2;
        panel2.Paint += new System.Windows.Forms.PaintEventHandler(panel2_Paint);
        panel2.Click += new System.EventHandler(panel2_Click);
        button7.Location = new System.Drawing.Point(331, 62);
        button7.Name = "button7";
        button7.Size = new System.Drawing.Size(87, 27);
        button7.TabIndex = 1;
        button7.Text = "取消";
        button7.UseVisualStyleBackColor = true;
        button7.Click += new System.EventHandler(button7_Click);
        button6.Location = new System.Drawing.Point(331, 19);
        button6.Name = "button6";
        button6.Size = new System.Drawing.Size(87, 27);
        button6.TabIndex = 0;
        button6.Text = "确定";
        button6.UseVisualStyleBackColor = true;
        button9.Location = new System.Drawing.Point(283, 14);
        button9.Name = "button9";
        button9.Size = new System.Drawing.Size(87, 27);
        button9.TabIndex = 1;
        button9.Text = "变量选择";
        toolTip1.SetToolTip(button9, "点击选择变量");
        button9.UseVisualStyleBackColor = true;
        button9.Click += new System.EventHandler(button9_Click);
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(485, 449);
        base.Controls.Add(button9);
        base.Controls.Add(panel1);
        base.Controls.Add(label4);
        base.Controls.Add(label3);
        base.Controls.Add(label2);
        base.Controls.Add(checkBox1);
        base.Controls.Add(textBox4);
        base.Controls.Add(textBox3);
        base.Controls.Add(textBox2);
        base.Controls.Add(button5);
        base.Controls.Add(button4);
        base.Controls.Add(button3);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.Controls.Add(listView1);
        base.Controls.Add(textBox1);
        base.Controls.Add(label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "ysForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "颜色变化";
        base.Load += new System.EventHandler(ysForm_Load);
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
