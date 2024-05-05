using DevExpress.XtraEditors;
using ShapeRuntime;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class czbfbForm : XtraForm
{
    private readonly CGlobal theglobal;

    private Button button1;

    private Button button2;

    private PictureBox pictureBox1;

    private Label label1;

    private Label label4;

    public TextBox textBox1;

    public TextBox textBox3;

    public TextBox textBox4;

    public TextBox textBox5;

    private RadioButton radioButton2;

    private RadioButton radioButton1;

    public TextBox textBox2;

    private Label label3;

    private Button button3;

    private GroupControl groupControl1;

    private Label label2;

    private Label label7;

    private GroupControl groupControl2;

    private GroupControl groupControl3;

    public czbfbForm(CGlobal _theglobal)
    {
        InitializeComponent();

        theglobal = _theglobal;
    }

    private void Form4_Load(object sender, EventArgs e)
    {
        try
        {
            if (theglobal.SelectedShapeList[0].czbfbcankao == 1)
            {
                radioButton1.Checked = true;
            }
            if (theglobal.SelectedShapeList[0].czbfbcankao == 2)
            {
                radioButton2.Checked = true;
            }
            textBox3.Text = theglobal.SelectedShapeList[0].czbfbbianliang;
            textBox1.Text = theglobal.SelectedShapeList[0].czbfbbaifenbimax.ToString();
            textBox2.Text = theglobal.SelectedShapeList[0].czbfbbaifenbimin.ToString();
            textBox5.Text = theglobal.SelectedShapeList[0].czbfbzhimin.ToString();
            textBox4.Text = theglobal.SelectedShapeList[0].czbfbzhimax.ToString();
            if ("0" == textBox1.Text && "0" == textBox2.Text && "0" == textBox5.Text && "0" == textBox4.Text)
            {
                textBox1.Text = "100";
                textBox4.Text = "100";
            }
        }
        catch
        {
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox3.Text == "")
        {
            theglobal.SelectedShapeList[0].czbfb = false;
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
            if (Convert.ToDouble(textBox2.Text) < 0.0 || Convert.ToDouble(textBox2.Text) > 100.0 || Convert.ToDouble(textBox1.Text) < 0.0 || Convert.ToDouble(textBox1.Text) > 100.0)
            {
                MessageBox.Show("百分比信息不正确,请从新输入");
                return;
            }
        }
        catch
        {
            MessageBox.Show("数值输入框内存在不可识别数值,请从新输入");
            return;
        }
        try
        {
            base.DialogResult = DialogResult.OK;
            int czbfbcankao = 0;
            if (radioButton1.Checked)
            {
                czbfbcankao = 1;
            }
            if (radioButton2.Checked)
            {
                czbfbcankao = 2;
            }
            theglobal.SelectedShapeList[0].BrushStyle = CShape._BrushStyle.百分比填充;
            if (theglobal.SelectedShapeList[0].Color1 == theglobal.SelectedShapeList[0].Color2)
            {
                theglobal.SelectedShapeList[0].Color2 = Color.White;
            }
            theglobal.SelectedShapeList[0].czbfbcankao = czbfbcankao;
            theglobal.SelectedShapeList[0].czbfbbianliang = textBox3.Text;
            theglobal.SelectedShapeList[0].czbfbbaifenbimax = Convert.ToInt32(textBox1.Text);
            theglobal.SelectedShapeList[0].czbfbbaifenbimin = Convert.ToInt32(textBox2.Text);
            theglobal.SelectedShapeList[0].czbfbzhimin = Convert.ToInt32(textBox5.Text);
            theglobal.SelectedShapeList[0].czbfbzhimax = Convert.ToInt32(textBox4.Text);
            if (!theglobal.SelectedShapeList[0].czbfb && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].czbfb = true;
            }
            Close();
        }
        catch
        {
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
        Close();
    }

    private void button3_Click(object sender, EventArgs e)
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
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        textBox1 = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        textBox3 = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        textBox4 = new System.Windows.Forms.TextBox();
        textBox5 = new System.Windows.Forms.TextBox();
        pictureBox1 = new System.Windows.Forms.PictureBox();
        radioButton2 = new System.Windows.Forms.RadioButton();
        radioButton1 = new System.Windows.Forms.RadioButton();
        textBox2 = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        button3 = new System.Windows.Forms.Button();
        groupControl1 = new DevExpress.XtraEditors.GroupControl();
        label2 = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        groupControl2 = new DevExpress.XtraEditors.GroupControl();
        groupControl3 = new DevExpress.XtraEditors.GroupControl();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
        groupControl1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)groupControl2).BeginInit();
        groupControl2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)groupControl3).BeginInit();
        groupControl3.SuspendLayout();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(182, 287);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 2;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(275, 287);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 3;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        textBox1.Location = new System.Drawing.Point(255, 34);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(69, 22);
        textBox1.TabIndex = 1;
        textBox1.Text = "100";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(173, 37);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(77, 14);
        label1.TabIndex = 11;
        label1.Text = "最大填充(%)";
        textBox3.HideSelection = false;
        textBox3.Location = new System.Drawing.Point(75, 19);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(193, 22);
        textBox3.TabIndex = 0;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(14, 22);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(55, 14);
        label4.TabIndex = 8;
        label4.Text = "绑定变量";
        textBox4.Location = new System.Drawing.Point(255, 34);
        textBox4.Name = "textBox4";
        textBox4.Size = new System.Drawing.Size(69, 22);
        textBox4.TabIndex = 1;
        textBox4.Text = "100";
        textBox5.Location = new System.Drawing.Point(90, 34);
        textBox5.Name = "textBox5";
        textBox5.Size = new System.Drawing.Size(69, 22);
        textBox5.TabIndex = 0;
        textBox5.Text = "0";
        pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        pictureBox1.Location = new System.Drawing.Point(16, 273);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new System.Drawing.Size(340, 1);
        pictureBox1.TabIndex = 2;
        pictureBox1.TabStop = false;
        radioButton2.AutoSize = true;
        radioButton2.Location = new System.Drawing.Point(200, 42);
        radioButton2.Name = "radioButton2";
        radioButton2.Size = new System.Drawing.Size(73, 18);
        radioButton2.TabIndex = 1;
        radioButton2.Text = "由下到上";
        radioButton2.UseVisualStyleBackColor = true;
        radioButton1.AutoSize = true;
        radioButton1.Checked = true;
        radioButton1.Location = new System.Drawing.Point(46, 42);
        radioButton1.Name = "radioButton1";
        radioButton1.Size = new System.Drawing.Size(73, 18);
        radioButton1.TabIndex = 0;
        radioButton1.TabStop = true;
        radioButton1.Text = "由上到下";
        radioButton1.UseVisualStyleBackColor = true;
        textBox2.Location = new System.Drawing.Point(90, 34);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(69, 22);
        textBox2.TabIndex = 0;
        textBox2.Text = "0";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(7, 37);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(77, 14);
        label3.TabIndex = 12;
        label3.Text = "最小填充(%)";
        button3.Location = new System.Drawing.Point(274, 16);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(87, 27);
        button3.TabIndex = 1;
        button3.Text = "变量选择";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        groupControl1.Controls.Add(label2);
        groupControl1.Controls.Add(label7);
        groupControl1.Controls.Add(textBox4);
        groupControl1.Controls.Add(textBox5);
        groupControl1.Location = new System.Drawing.Point(19, 49);
        groupControl1.Name = "groupControl1";
        groupControl1.Size = new System.Drawing.Size(342, 65);
        groupControl1.TabIndex = 18;
        groupControl1.Text = "绑定变量";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(206, 37);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(43, 14);
        label2.TabIndex = 11;
        label2.Text = "最大值";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(43, 37);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(43, 14);
        label7.TabIndex = 10;
        label7.Text = "最小值";
        groupControl2.Controls.Add(textBox1);
        groupControl2.Controls.Add(textBox2);
        groupControl2.Controls.Add(label3);
        groupControl2.Controls.Add(label1);
        groupControl2.Location = new System.Drawing.Point(19, 120);
        groupControl2.Name = "groupControl2";
        groupControl2.Size = new System.Drawing.Size(342, 65);
        groupControl2.TabIndex = 19;
        groupControl2.Text = "对应填充百分比";
        groupControl3.Controls.Add(radioButton1);
        groupControl3.Controls.Add(radioButton2);
        groupControl3.Location = new System.Drawing.Point(19, 191);
        groupControl3.Name = "groupControl3";
        groupControl3.Size = new System.Drawing.Size(342, 65);
        groupControl3.TabIndex = 20;
        groupControl3.Text = "填充方向";
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(374, 326);
        base.Controls.Add(groupControl3);
        base.Controls.Add(groupControl2);
        base.Controls.Add(groupControl1);
        base.Controls.Add(button3);
        base.Controls.Add(label4);
        base.Controls.Add(textBox3);
        base.Controls.Add(pictureBox1);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "czbfbForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "垂直百分比";
        base.Load += new System.EventHandler(Form4_Load);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
        groupControl1.ResumeLayout(false);
        groupControl1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)groupControl2).EndInit();
        groupControl2.ResumeLayout(false);
        groupControl2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)groupControl3).EndInit();
        groupControl3.ResumeLayout(false);
        groupControl3.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
