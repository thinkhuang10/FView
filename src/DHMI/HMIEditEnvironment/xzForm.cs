using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class xzForm : XtraForm
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

    private Label label5;

    private Label label6;

    public TextBox textBox5;

    public TextBox textBox2;

    private Label label2;

    private Label label3;

    public TextBox textBox6;

    public TextBox textBox7;

    private Label label8;

    private GroupControl groupControl1;

    private GroupControl groupControl2;

    private GroupControl groupControl3;

    private Button button4;

    public xzForm(CGlobal _theglobal)
    {
        theglobal = _theglobal;
        InitializeComponent();
    }

    private void Form4_Load(object sender, EventArgs e)
    {
        try
        {
            textBox3.Text = theglobal.SelectedShapeList[0].mbxzbianliang;
            textBox5.Text = theglobal.SelectedShapeList[0].mbxzzhimin.ToString();
            textBox4.Text = theglobal.SelectedShapeList[0].mbxzzhimax.ToString();
            textBox6.Text = theglobal.SelectedShapeList[0].mbxzjiaodumin.ToString();
            textBox2.Text = theglobal.SelectedShapeList[0].mbxzjiaodumax.ToString();
            textBox1.Text = theglobal.SelectedShapeList[0].mbxzzhongxinpianzhiright.ToString();
            textBox7.Text = theglobal.SelectedShapeList[0].mbxzzhongxinpianzhidown.ToString();
            if ("0" == textBox1.Text && "0" == textBox2.Text && "0" == textBox5.Text && "0" == textBox6.Text && "0" == textBox7.Text && "0" == textBox4.Text)
            {
                textBox2.Text = "360";
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
            theglobal.SelectedShapeList[0].mbxz = false;
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
            theglobal.SelectedShapeList[0].mbxzbianliang = textBox3.Text;
            theglobal.SelectedShapeList[0].mbxzzhimin = Convert.ToInt32(textBox5.Text);
            theglobal.SelectedShapeList[0].mbxzzhimax = Convert.ToInt32(textBox4.Text);
            theglobal.SelectedShapeList[0].mbxzjiaodumin = Convert.ToInt32(textBox6.Text);
            theglobal.SelectedShapeList[0].mbxzjiaodumax = Convert.ToInt32(textBox2.Text);
            theglobal.SelectedShapeList[0].mbxzzhongxinpianzhiright = Convert.ToInt32(textBox1.Text);
            theglobal.SelectedShapeList[0].mbxzzhongxinpianzhidown = Convert.ToInt32(textBox7.Text);
            if (!theglobal.SelectedShapeList[0].mbxz && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].mbxz = true;
            }
            base.DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception)
        {
        }
    }

    private void Button2_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
        Close();
    }

    private void Button4_Click(object sender, EventArgs e)
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
        this.label1 = new System.Windows.Forms.Label();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.textBox5 = new System.Windows.Forms.TextBox();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.textBox6 = new System.Windows.Forms.TextBox();
        this.textBox7 = new System.Windows.Forms.TextBox();
        this.label8 = new System.Windows.Forms.Label();
        this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
        this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
        this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
        this.button4 = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.groupControl1).BeginInit();
        this.groupControl1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl2).BeginInit();
        this.groupControl2.SuspendLayout();
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
        this.button2.Click += new System.EventHandler(Button2_Click);
        this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBox1.Location = new System.Drawing.Point(17, 273);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(340, 1);
        this.pictureBox1.TabIndex = 2;
        this.pictureBox1.TabStop = false;
        this.textBox1.Location = new System.Drawing.Point(83, 33);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(70, 22);
        this.textBox1.TabIndex = 0;
        this.textBox1.Text = "0";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(22, 36);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(55, 14);
        this.label1.TabIndex = 14;
        this.label1.Text = "横向偏移";
        this.textBox3.HideSelection = false;
        this.textBox3.Location = new System.Drawing.Point(76, 19);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(193, 22);
        this.textBox3.TabIndex = 0;
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(15, 22);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(55, 14);
        this.label4.TabIndex = 9;
        this.label4.Text = "绑定变量";
        this.textBox4.Location = new System.Drawing.Point(242, 33);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(70, 22);
        this.textBox4.TabIndex = 1;
        this.textBox4.Text = "100";
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(193, 36);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(43, 14);
        this.label5.TabIndex = 11;
        this.label5.Text = "最大值";
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(33, 36);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(43, 14);
        this.label6.TabIndex = 10;
        this.label6.Text = "最小值";
        this.textBox5.Location = new System.Drawing.Point(82, 33);
        this.textBox5.Name = "textBox5";
        this.textBox5.Size = new System.Drawing.Size(70, 22);
        this.textBox5.TabIndex = 0;
        this.textBox5.Text = "0";
        this.textBox2.Location = new System.Drawing.Point(242, 33);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(70, 22);
        this.textBox2.TabIndex = 1;
        this.textBox2.Text = "360";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(195, 36);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(43, 14);
        this.label2.TabIndex = 13;
        this.label2.Text = "最大值";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(33, 36);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(43, 14);
        this.label3.TabIndex = 12;
        this.label3.Text = "最小值";
        this.textBox6.Location = new System.Drawing.Point(82, 33);
        this.textBox6.Name = "textBox6";
        this.textBox6.Size = new System.Drawing.Size(70, 22);
        this.textBox6.TabIndex = 0;
        this.textBox6.Text = "0";
        this.textBox7.Location = new System.Drawing.Point(243, 33);
        this.textBox7.Name = "textBox7";
        this.textBox7.Size = new System.Drawing.Size(70, 22);
        this.textBox7.TabIndex = 1;
        this.textBox7.Text = "0";
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(182, 36);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(55, 14);
        this.label8.TabIndex = 15;
        this.label8.Text = "纵向偏移";
        this.groupControl1.Controls.Add(this.textBox4);
        this.groupControl1.Controls.Add(this.label5);
        this.groupControl1.Controls.Add(this.label6);
        this.groupControl1.Controls.Add(this.textBox5);
        this.groupControl1.Location = new System.Drawing.Point(18, 47);
        this.groupControl1.Name = "groupControl1";
        this.groupControl1.Size = new System.Drawing.Size(342, 65);
        this.groupControl1.TabIndex = 2;
        this.groupControl1.Text = "绑定变量";
        this.groupControl2.Controls.Add(this.textBox2);
        this.groupControl2.Controls.Add(this.textBox6);
        this.groupControl2.Controls.Add(this.label3);
        this.groupControl2.Controls.Add(this.label2);
        this.groupControl2.Location = new System.Drawing.Point(18, 118);
        this.groupControl2.Name = "groupControl2";
        this.groupControl2.Size = new System.Drawing.Size(342, 65);
        this.groupControl2.TabIndex = 3;
        this.groupControl2.Text = "对应角度";
        this.groupControl3.Controls.Add(this.label8);
        this.groupControl3.Controls.Add(this.textBox7);
        this.groupControl3.Controls.Add(this.label1);
        this.groupControl3.Controls.Add(this.textBox1);
        this.groupControl3.Location = new System.Drawing.Point(17, 189);
        this.groupControl3.Name = "groupControl3";
        this.groupControl3.Size = new System.Drawing.Size(342, 65);
        this.groupControl3.TabIndex = 4;
        this.groupControl3.Text = "旋转中心偏移";
        this.button4.Location = new System.Drawing.Point(275, 16);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(87, 27);
        this.button4.TabIndex = 1;
        this.button4.Text = "变量选择";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(Button4_Click);
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(374, 326);
        base.Controls.Add(this.button4);
        base.Controls.Add(this.groupControl3);
        base.Controls.Add(this.groupControl2);
        base.Controls.Add(this.groupControl1);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.textBox3);
        base.Controls.Add(this.pictureBox1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "xzForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "旋转";
        base.Load += new System.EventHandler(Form4_Load);
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.groupControl1).EndInit();
        this.groupControl1.ResumeLayout(false);
        this.groupControl1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl2).EndInit();
        this.groupControl2.ResumeLayout(false);
        this.groupControl2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl3).EndInit();
        this.groupControl3.ResumeLayout(false);
        this.groupControl3.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
