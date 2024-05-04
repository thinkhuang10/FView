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
        this.components = new System.ComponentModel.Container();
        this.label1 = new System.Windows.Forms.Label();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.listView1 = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.button4 = new System.Windows.Forms.Button();
        this.button5 = new System.Windows.Forms.Button();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.colorDialog1 = new System.Windows.Forms.ColorDialog();
        this.checkBox1 = new System.Windows.Forms.CheckBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.panel1 = new System.Windows.Forms.Panel();
        this.label6 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.panel3 = new System.Windows.Forms.Panel();
        this.panel2 = new System.Windows.Forms.Panel();
        this.button7 = new System.Windows.Forms.Button();
        this.button6 = new System.Windows.Forms.Button();
        this.button9 = new System.Windows.Forms.Button();
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.panel1.SuspendLayout();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(14, 21);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(31, 14);
        this.label1.TabIndex = 0;
        this.label1.Text = "变量";
        this.textBox1.HideSelection = false;
        this.textBox1.Location = new System.Drawing.Point(55, 17);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(222, 22);
        this.textBox1.TabIndex = 0;
        this.toolTip1.SetToolTip(this.textBox1, "此处也可以手动填写变量，格式为[变量名]");
        this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[4] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4 });
        this.listView1.FullRowSelect = true;
        this.listView1.GridLines = true;
        this.listView1.HideSelection = false;
        this.listView1.Location = new System.Drawing.Point(16, 115);
        this.listView1.Name = "listView1";
        this.listView1.Size = new System.Drawing.Size(354, 322);
        this.listView1.TabIndex = 6;
        this.listView1.UseCompatibleStateImageBehavior = false;
        this.listView1.View = System.Windows.Forms.View.Details;
        this.listView1.SelectedIndexChanged += new System.EventHandler(listView1_SelectedIndexChanged);
        this.columnHeader1.Text = "最小值";
        this.columnHeader2.Text = "最大值";
        this.columnHeader3.Text = "颜色";
        this.columnHeader4.Text = "闪烁";
        this.button1.Location = new System.Drawing.Point(376, 377);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 10;
        this.button1.Text = "确定";
        this.toolTip1.SetToolTip(this.button1, "保存并退出");
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(376, 410);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 11;
        this.button2.Text = "取消";
        this.toolTip1.SetToolTip(this.button2, "退出不保存");
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.button3.Location = new System.Drawing.Point(376, 115);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(87, 27);
        this.button3.TabIndex = 7;
        this.button3.Text = "插入行";
        this.toolTip1.SetToolTip(this.button3, "插入一行到列表中");
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.button4.Location = new System.Drawing.Point(376, 148);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(87, 27);
        this.button4.TabIndex = 8;
        this.button4.Text = "删除行";
        this.toolTip1.SetToolTip(this.button4, "删除选中行");
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(button4_Click);
        this.button5.Location = new System.Drawing.Point(376, 182);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(87, 27);
        this.button5.TabIndex = 9;
        this.button5.Text = "编辑行";
        this.toolTip1.SetToolTip(this.button5, "修改选中行");
        this.button5.UseVisualStyleBackColor = true;
        this.button5.Click += new System.EventHandler(button5_Click);
        this.textBox2.Location = new System.Drawing.Point(16, 83);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(72, 22);
        this.textBox2.TabIndex = 2;
        this.textBox2.Text = "0";
        this.toolTip1.SetToolTip(this.textBox2, "当变量大于最小值小于等于最大值时显示对应颜色");
        this.textBox3.Location = new System.Drawing.Point(98, 83);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(72, 22);
        this.textBox3.TabIndex = 3;
        this.textBox3.Text = "100";
        this.toolTip1.SetToolTip(this.textBox3, "当变量大于最小值小于等于最大值时显示对应颜色");
        this.textBox4.Location = new System.Drawing.Point(177, 83);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(72, 22);
        this.textBox4.TabIndex = 4;
        this.toolTip1.SetToolTip(this.textBox4, "单击改变颜色");
        this.textBox4.Click += new System.EventHandler(textBox4_Click);
        this.checkBox1.AutoSize = true;
        this.checkBox1.Location = new System.Drawing.Point(258, 87);
        this.checkBox1.Name = "checkBox1";
        this.checkBox1.Size = new System.Drawing.Size(50, 18);
        this.checkBox1.TabIndex = 5;
        this.checkBox1.Text = "闪烁";
        this.checkBox1.UseVisualStyleBackColor = true;
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(108, 61);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(43, 14);
        this.label2.TabIndex = 12;
        this.label2.Text = "最大值";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(29, 61);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(43, 14);
        this.label3.TabIndex = 13;
        this.label3.Text = "最小值";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(191, 61);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(31, 14);
        this.label4.TabIndex = 14;
        this.label4.Text = "颜色";
        this.panel1.Controls.Add(this.label6);
        this.panel1.Controls.Add(this.label5);
        this.panel1.Controls.Add(this.panel3);
        this.panel1.Controls.Add(this.panel2);
        this.panel1.Controls.Add(this.button7);
        this.panel1.Controls.Add(this.button6);
        this.panel1.Location = new System.Drawing.Point(472, 61);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(455, 376);
        this.panel1.TabIndex = 15;
        this.panel1.Visible = false;
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(50, 68);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(91, 14);
        this.label6.TabIndex = 5;
        this.label6.Text = "变量为假时颜色";
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(50, 24);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(91, 14);
        this.label5.TabIndex = 4;
        this.label5.Text = "变量为真时颜色";
        this.panel3.BackColor = System.Drawing.Color.Red;
        this.panel3.Location = new System.Drawing.Point(177, 63);
        this.panel3.Name = "panel3";
        this.panel3.Size = new System.Drawing.Size(101, 19);
        this.panel3.TabIndex = 3;
        this.panel3.Click += new System.EventHandler(panel3_Click);
        this.panel2.BackColor = System.Drawing.Color.LawnGreen;
        this.panel2.Location = new System.Drawing.Point(177, 20);
        this.panel2.Name = "panel2";
        this.panel2.Size = new System.Drawing.Size(101, 19);
        this.panel2.TabIndex = 2;
        this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(panel2_Paint);
        this.panel2.Click += new System.EventHandler(panel2_Click);
        this.button7.Location = new System.Drawing.Point(331, 62);
        this.button7.Name = "button7";
        this.button7.Size = new System.Drawing.Size(87, 27);
        this.button7.TabIndex = 1;
        this.button7.Text = "取消";
        this.button7.UseVisualStyleBackColor = true;
        this.button7.Click += new System.EventHandler(button7_Click);
        this.button6.Location = new System.Drawing.Point(331, 19);
        this.button6.Name = "button6";
        this.button6.Size = new System.Drawing.Size(87, 27);
        this.button6.TabIndex = 0;
        this.button6.Text = "确定";
        this.button6.UseVisualStyleBackColor = true;
        this.button9.Location = new System.Drawing.Point(283, 14);
        this.button9.Name = "button9";
        this.button9.Size = new System.Drawing.Size(87, 27);
        this.button9.TabIndex = 1;
        this.button9.Text = "变量选择";
        this.toolTip1.SetToolTip(this.button9, "点击选择变量");
        this.button9.UseVisualStyleBackColor = true;
        this.button9.Click += new System.EventHandler(button9_Click);
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(485, 449);
        base.Controls.Add(this.button9);
        base.Controls.Add(this.panel1);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.checkBox1);
        base.Controls.Add(this.textBox4);
        base.Controls.Add(this.textBox3);
        base.Controls.Add(this.textBox2);
        base.Controls.Add(this.button5);
        base.Controls.Add(this.button4);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.listView1);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "ysForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "颜色变化";
        base.Load += new System.EventHandler(ysForm_Load);
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
