using DevExpress.XtraEditors;
using ShapeRuntime;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class spbfbForm : XtraForm
{
    private readonly CGlobal theglobal;

    private Button button1;

    private Button button2;

    private PictureBox pictureBox1;

    private Label label4;

    public TextBox textBox1;

    public TextBox textBox3;

    public TextBox textBox4;

    public TextBox textBox5;

    private RadioButton radioButton2;

    private RadioButton radioButton1;

    public TextBox textBox2;

    private Button button3;

    private GroupControl groupControl2;

    private Label label3;

    private Label label7;

    private GroupControl groupControl1;

    private Label label8;

    private Label label9;

    private GroupControl groupControl3;


    public spbfbForm(CGlobal _theglobal)
    {
        InitializeComponent();
        theglobal = _theglobal;
    }

    private void Form4_Load(object sender, EventArgs e)
    {
        try
        {
            textBox3.Text = theglobal.SelectedShapeList[0].spbfbbianliang;
            if (theglobal.SelectedShapeList[0].spbfbcankao == 1)
            {
                radioButton1.Checked = true;
            }
            if (theglobal.SelectedShapeList[0].spbfbcankao == 2)
            {
                radioButton2.Checked = true;
            }
            textBox1.Text = theglobal.SelectedShapeList[0].spbfbbaifenbimax.ToString();
            textBox2.Text = theglobal.SelectedShapeList[0].spbfbbaifenbimin.ToString();
            textBox5.Text = theglobal.SelectedShapeList[0].spbfbzhimin.ToString();
            textBox4.Text = theglobal.SelectedShapeList[0].spbfbzhimax.ToString();
            if ("0" == textBox1.Text && "0" == textBox2.Text && "0" == textBox5.Text && "0" == textBox4.Text)
            {
                textBox1.Text = "100";
                textBox4.Text = "100";
            }
        }
        catch (Exception)
        {
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox3.Text == "")
        {
            theglobal.SelectedShapeList[0].spbfb = false;
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
        catch (Exception)
        {
            MessageBox.Show("数值输入框内存在不可识别数值,请从新输入");
            return;
        }
        try
        {
            base.DialogResult = DialogResult.OK;
            theglobal.SelectedShapeList[0].spbfbbianliang = textBox3.Text;
            theglobal.SelectedShapeList[0].BrushStyle = CShape._BrushStyle.百分比填充;
            if (theglobal.SelectedShapeList[0].Color1 == theglobal.SelectedShapeList[0].Color2)
            {
                theglobal.SelectedShapeList[0].Color2 = Color.White;
            }
            int spbfbcankao = 0;
            if (radioButton1.Checked)
            {
                spbfbcankao = 1;
            }
            if (radioButton2.Checked)
            {
                spbfbcankao = 2;
            }
            theglobal.SelectedShapeList[0].spbfbcankao = spbfbcankao;
            theglobal.SelectedShapeList[0].spbfbbaifenbimax = Convert.ToInt32(textBox1.Text);
            theglobal.SelectedShapeList[0].spbfbbaifenbimin = Convert.ToInt32(textBox2.Text);
            theglobal.SelectedShapeList[0].spbfbzhimin = Convert.ToInt32(textBox5.Text);
            theglobal.SelectedShapeList[0].spbfbzhimax = Convert.ToInt32(textBox4.Text);
            if (!theglobal.SelectedShapeList[0].spbfb && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].spbfb = true;
            }
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
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.textBox5 = new System.Windows.Forms.TextBox();
        this.radioButton2 = new System.Windows.Forms.RadioButton();
        this.radioButton1 = new System.Windows.Forms.RadioButton();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.button3 = new System.Windows.Forms.Button();
        this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
        this.label3 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
        this.label8 = new System.Windows.Forms.Label();
        this.label9 = new System.Windows.Forms.Label();
        this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.groupControl2).BeginInit();
        this.groupControl2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl1).BeginInit();
        this.groupControl1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl3).BeginInit();
        this.groupControl3.SuspendLayout();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(182, 287);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 5;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(275, 287);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 6;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBox1.Location = new System.Drawing.Point(16, 273);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(340, 1);
        this.pictureBox1.TabIndex = 2;
        this.pictureBox1.TabStop = false;
        this.textBox1.Location = new System.Drawing.Point(255, 33);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(68, 22);
        this.textBox1.TabIndex = 1;
        this.textBox1.Text = "100";
        this.textBox3.HideSelection = false;
        this.textBox3.Location = new System.Drawing.Point(75, 19);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(193, 22);
        this.textBox3.TabIndex = 0;
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(14, 22);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(55, 14);
        this.label4.TabIndex = 9;
        this.label4.Text = "绑定变量";
        this.textBox4.Location = new System.Drawing.Point(255, 33);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(69, 22);
        this.textBox4.TabIndex = 1;
        this.textBox4.Text = "100";
        this.textBox5.Location = new System.Drawing.Point(90, 33);
        this.textBox5.Name = "textBox5";
        this.textBox5.Size = new System.Drawing.Size(69, 22);
        this.textBox5.TabIndex = 0;
        this.textBox5.Text = "0";
        this.radioButton2.AutoSize = true;
        this.radioButton2.Location = new System.Drawing.Point(202, 38);
        this.radioButton2.Name = "radioButton2";
        this.radioButton2.Size = new System.Drawing.Size(73, 18);
        this.radioButton2.TabIndex = 1;
        this.radioButton2.Text = "由右到左";
        this.radioButton2.UseVisualStyleBackColor = true;
        this.radioButton1.AutoSize = true;
        this.radioButton1.Checked = true;
        this.radioButton1.Location = new System.Drawing.Point(44, 38);
        this.radioButton1.Name = "radioButton1";
        this.radioButton1.Size = new System.Drawing.Size(73, 18);
        this.radioButton1.TabIndex = 0;
        this.radioButton1.TabStop = true;
        this.radioButton1.Text = "由左到右";
        this.radioButton1.UseVisualStyleBackColor = true;
        this.textBox2.Location = new System.Drawing.Point(90, 33);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(68, 22);
        this.textBox2.TabIndex = 0;
        this.textBox2.Text = "0";
        this.button3.Location = new System.Drawing.Point(274, 16);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(87, 27);
        this.button3.TabIndex = 1;
        this.button3.Text = "变量选择";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.groupControl2.Controls.Add(this.label3);
        this.groupControl2.Controls.Add(this.label7);
        this.groupControl2.Controls.Add(this.textBox1);
        this.groupControl2.Controls.Add(this.textBox2);
        this.groupControl2.Location = new System.Drawing.Point(19, 120);
        this.groupControl2.Name = "groupControl2";
        this.groupControl2.Size = new System.Drawing.Size(342, 65);
        this.groupControl2.TabIndex = 3;
        this.groupControl2.Text = "对应填充百分比";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(7, 36);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(77, 14);
        this.label3.TabIndex = 12;
        this.label3.Text = "最小填充(%)";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(173, 36);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(77, 14);
        this.label7.TabIndex = 11;
        this.label7.Text = "最大填充(%)";
        this.groupControl1.Controls.Add(this.label8);
        this.groupControl1.Controls.Add(this.label9);
        this.groupControl1.Controls.Add(this.textBox4);
        this.groupControl1.Controls.Add(this.textBox5);
        this.groupControl1.Location = new System.Drawing.Point(19, 49);
        this.groupControl1.Name = "groupControl1";
        this.groupControl1.Size = new System.Drawing.Size(342, 65);
        this.groupControl1.TabIndex = 2;
        this.groupControl1.Text = "绑定变量";
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(199, 36);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(43, 14);
        this.label8.TabIndex = 11;
        this.label8.Text = "最大值";
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(41, 36);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(43, 14);
        this.label9.TabIndex = 10;
        this.label9.Text = "最小值";
        this.groupControl3.Controls.Add(this.radioButton1);
        this.groupControl3.Controls.Add(this.radioButton2);
        this.groupControl3.Location = new System.Drawing.Point(19, 191);
        this.groupControl3.Name = "groupControl3";
        this.groupControl3.Size = new System.Drawing.Size(342, 65);
        this.groupControl3.TabIndex = 4;
        this.groupControl3.Text = "填充方向";
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(374, 326);
        base.Controls.Add(this.groupControl3);
        base.Controls.Add(this.groupControl2);
        base.Controls.Add(this.groupControl1);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.textBox3);
        base.Controls.Add(this.pictureBox1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "spbfbForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "水平百分比";
        base.Load += new System.EventHandler(Form4_Load);
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.groupControl2).EndInit();
        this.groupControl2.ResumeLayout(false);
        this.groupControl2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl1).EndInit();
        this.groupControl1.ResumeLayout(false);
        this.groupControl1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl3).EndInit();
        this.groupControl3.ResumeLayout(false);
        this.groupControl3.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
