using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class gdbhForm : XtraForm
{
    private readonly CGlobal theglobal;

    public TextBox textBox4;

    public TextBox textBox5;

    private Label label4;

    public TextBox textBox3;

    public TextBox textBox1;

    private Button button2;

    private Button button1;

    public System.Windows.Forms.ComboBox comboBox1;

    public TextBox textBox2;

    private GroupControl groupControl3;

    private Label label7;

    private GroupControl groupControl2;

    private Label label9;

    private Label label10;

    private GroupControl groupControl1;

    private Label label11;

    private Label label12;

    private Label label13;

    private PictureBox pictureBox2;

    private Button button4;

    public gdbhForm(CGlobal _theglobal)
    {
        InitializeComponent();
        theglobal = _theglobal;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox3.Text == "")
        {
            theglobal.SelectedShapeList[0].gdbh = false;
            Close();
            return;
        }
        try
        {
            if (Convert.ToDouble(textBox4.Text) == Convert.ToDouble(textBox5.Text))
            {
                MessageBox.Show("上限等于下限,请从新输入");
                return;
            }
        }
        catch (Exception)
        {
            MessageBox.Show("数值输入框内存在不可识别数值,请从新输入");
            return;
        }
        try
        {
            theglobal.SelectedShapeList[0].gdbhbianliang = textBox3.Text;
            theglobal.SelectedShapeList[0].gdbhxiangsumax = Convert.ToInt32(textBox1.Text);
            theglobal.SelectedShapeList[0].gdbhxiangsumin = Convert.ToInt32(textBox2.Text);
            theglobal.SelectedShapeList[0].gdbhzhimin = Convert.ToInt32(textBox5.Text);
            theglobal.SelectedShapeList[0].gdbhzhimax = Convert.ToInt32(textBox4.Text);
            theglobal.SelectedShapeList[0].gdbhcankao = comboBox1.SelectedIndex + 1;
            if (!theglobal.SelectedShapeList[0].gdbh && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].gdbh = true;
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
        base.DialogResult = DialogResult.Cancel;
        Close();
    }

    private void textBox3_Click(object sender, EventArgs e)
    {
    }

    private void gdbhForm_Load(object sender, EventArgs e)
    {
        try
        {
            textBox3.Text = theglobal.SelectedShapeList[0].gdbhbianliang;
            textBox1.Text = theglobal.SelectedShapeList[0].gdbhxiangsumax.ToString();
            textBox2.Text = theglobal.SelectedShapeList[0].gdbhxiangsumin.ToString();
            textBox5.Text = theglobal.SelectedShapeList[0].gdbhzhimin.ToString();
            textBox4.Text = theglobal.SelectedShapeList[0].gdbhzhimax.ToString();
            comboBox1.SelectedIndex = theglobal.SelectedShapeList[0].gdbhcankao - 1;
            if ("0" == textBox1.Text && "0" == textBox2.Text && "0" == textBox5.Text && "0" == textBox4.Text)
            {
                textBox1.Text = "100";
                textBox4.Text = "100";
                comboBox1.SelectedIndex = 0;
            }
        }
        catch (Exception)
        {
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent != "")
        {
            int selectionStart = textBox3.SelectionStart;
            int selectionLength = textBox3.SelectionLength;
            string text = textBox3.Text.Remove(selectionStart, selectionLength);
            textBox3.Text = text.Insert(selectionStart, "[" + varTableEvent + "]");
        }
    }

    private void InitializeComponent()
    {
        textBox4 = new System.Windows.Forms.TextBox();
        textBox5 = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        textBox3 = new System.Windows.Forms.TextBox();
        textBox1 = new System.Windows.Forms.TextBox();
        button2 = new System.Windows.Forms.Button();
        button1 = new System.Windows.Forms.Button();
        comboBox1 = new System.Windows.Forms.ComboBox();
        textBox2 = new System.Windows.Forms.TextBox();
        groupControl3 = new DevExpress.XtraEditors.GroupControl();
        label7 = new System.Windows.Forms.Label();
        groupControl2 = new DevExpress.XtraEditors.GroupControl();
        label9 = new System.Windows.Forms.Label();
        label10 = new System.Windows.Forms.Label();
        groupControl1 = new DevExpress.XtraEditors.GroupControl();
        label11 = new System.Windows.Forms.Label();
        label12 = new System.Windows.Forms.Label();
        label13 = new System.Windows.Forms.Label();
        pictureBox2 = new System.Windows.Forms.PictureBox();
        button4 = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)groupControl3).BeginInit();
        groupControl3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)groupControl2).BeginInit();
        groupControl2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
        groupControl1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
        base.SuspendLayout();
        textBox4.Location = new System.Drawing.Point(242, 34);
        textBox4.Name = "textBox4";
        textBox4.Size = new System.Drawing.Size(70, 22);
        textBox4.TabIndex = 1;
        textBox4.Text = "100";
        textBox5.Location = new System.Drawing.Point(82, 34);
        textBox5.Name = "textBox5";
        textBox5.Size = new System.Drawing.Size(70, 22);
        textBox5.TabIndex = 0;
        textBox5.Text = "0";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(302, 219);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(55, 14);
        label4.TabIndex = 8;
        label4.Text = "绑定变量";
        textBox3.HideSelection = false;
        textBox3.Location = new System.Drawing.Point(77, 18);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(204, 22);
        textBox3.TabIndex = 0;
        textBox1.Location = new System.Drawing.Point(242, 33);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(70, 22);
        textBox1.TabIndex = 1;
        textBox1.Text = "100";
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(275, 287);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 6;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        button1.Location = new System.Drawing.Point(182, 287);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 5;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Items.AddRange(new object[2] { "上", "下" });
        comboBox1.Location = new System.Drawing.Point(139, 33);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(98, 22);
        comboBox1.TabIndex = 0;
        textBox2.Location = new System.Drawing.Point(82, 33);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(70, 22);
        textBox2.TabIndex = 0;
        textBox2.Text = "0";
        groupControl3.Controls.Add(label7);
        groupControl3.Controls.Add(comboBox1);
        groupControl3.Location = new System.Drawing.Point(18, 188);
        groupControl3.Name = "groupControl3";
        groupControl3.Size = new System.Drawing.Size(342, 65);
        groupControl3.TabIndex = 4;
        groupControl3.Text = "变化基准点";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(66, 36);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(67, 14);
        label7.TabIndex = 13;
        label7.Text = "变化基准点";
        groupControl2.Controls.Add(label9);
        groupControl2.Controls.Add(label10);
        groupControl2.Controls.Add(textBox2);
        groupControl2.Controls.Add(textBox1);
        groupControl2.Location = new System.Drawing.Point(19, 117);
        groupControl2.Name = "groupControl2";
        groupControl2.Size = new System.Drawing.Size(342, 65);
        groupControl2.TabIndex = 3;
        groupControl2.Text = "对应高度";
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(33, 36);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(43, 14);
        label9.TabIndex = 12;
        label9.Text = "最小值";
        label10.AutoSize = true;
        label10.Location = new System.Drawing.Point(195, 36);
        label10.Name = "label10";
        label10.Size = new System.Drawing.Size(43, 14);
        label10.TabIndex = 13;
        label10.Text = "最大值";
        groupControl1.Controls.Add(label11);
        groupControl1.Controls.Add(label12);
        groupControl1.Controls.Add(textBox5);
        groupControl1.Controls.Add(textBox4);
        groupControl1.Location = new System.Drawing.Point(19, 46);
        groupControl1.Name = "groupControl1";
        groupControl1.Size = new System.Drawing.Size(342, 65);
        groupControl1.TabIndex = 2;
        groupControl1.Text = "绑定变量";
        label11.AutoSize = true;
        label11.Location = new System.Drawing.Point(195, 37);
        label11.Name = "label11";
        label11.Size = new System.Drawing.Size(43, 14);
        label11.TabIndex = 11;
        label11.Text = "最大值";
        label12.AutoSize = true;
        label12.Location = new System.Drawing.Point(33, 37);
        label12.Name = "label12";
        label12.Size = new System.Drawing.Size(43, 14);
        label12.TabIndex = 10;
        label12.Text = "最小值";
        label13.AutoSize = true;
        label13.Location = new System.Drawing.Point(16, 21);
        label13.Name = "label13";
        label13.Size = new System.Drawing.Size(55, 14);
        label13.TabIndex = 28;
        label13.Text = "绑定变量";
        pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        pictureBox2.Location = new System.Drawing.Point(18, 272);
        pictureBox2.Name = "pictureBox2";
        pictureBox2.Size = new System.Drawing.Size(340, 1);
        pictureBox2.TabIndex = 27;
        pictureBox2.TabStop = false;
        button4.Location = new System.Drawing.Point(287, 17);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(75, 23);
        button4.TabIndex = 1;
        button4.Text = "选择变量";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(button4_Click);
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(374, 326);
        base.Controls.Add(button4);
        base.Controls.Add(groupControl3);
        base.Controls.Add(groupControl2);
        base.Controls.Add(groupControl1);
        base.Controls.Add(label13);
        base.Controls.Add(pictureBox2);
        base.Controls.Add(label4);
        base.Controls.Add(textBox3);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "gdbhForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "高度变化";
        base.Load += new System.EventHandler(gdbhForm_Load);
        ((System.ComponentModel.ISupportInitialize)groupControl3).EndInit();
        groupControl3.ResumeLayout(false);
        groupControl3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)groupControl2).EndInit();
        groupControl2.ResumeLayout(false);
        groupControl2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
        groupControl1.ResumeLayout(false);
        groupControl1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
