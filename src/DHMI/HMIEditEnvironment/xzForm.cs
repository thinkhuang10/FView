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
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        pictureBox1 = new System.Windows.Forms.PictureBox();
        textBox1 = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        textBox3 = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        textBox4 = new System.Windows.Forms.TextBox();
        label5 = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        textBox5 = new System.Windows.Forms.TextBox();
        textBox2 = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        textBox6 = new System.Windows.Forms.TextBox();
        textBox7 = new System.Windows.Forms.TextBox();
        label8 = new System.Windows.Forms.Label();
        groupControl1 = new DevExpress.XtraEditors.GroupControl();
        groupControl2 = new DevExpress.XtraEditors.GroupControl();
        groupControl3 = new DevExpress.XtraEditors.GroupControl();
        button4 = new System.Windows.Forms.Button();
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
        button1.TabIndex = 5;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(275, 287);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 6;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(Button2_Click);
        pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        pictureBox1.Location = new System.Drawing.Point(17, 273);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new System.Drawing.Size(340, 1);
        pictureBox1.TabIndex = 2;
        pictureBox1.TabStop = false;
        textBox1.Location = new System.Drawing.Point(83, 33);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(70, 22);
        textBox1.TabIndex = 0;
        textBox1.Text = "0";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(22, 36);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(55, 14);
        label1.TabIndex = 14;
        label1.Text = "横向偏移";
        textBox3.HideSelection = false;
        textBox3.Location = new System.Drawing.Point(76, 19);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(193, 22);
        textBox3.TabIndex = 0;
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(15, 22);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(55, 14);
        label4.TabIndex = 9;
        label4.Text = "绑定变量";
        textBox4.Location = new System.Drawing.Point(242, 33);
        textBox4.Name = "textBox4";
        textBox4.Size = new System.Drawing.Size(70, 22);
        textBox4.TabIndex = 1;
        textBox4.Text = "100";
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(193, 36);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(43, 14);
        label5.TabIndex = 11;
        label5.Text = "最大值";
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(33, 36);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(43, 14);
        label6.TabIndex = 10;
        label6.Text = "最小值";
        textBox5.Location = new System.Drawing.Point(82, 33);
        textBox5.Name = "textBox5";
        textBox5.Size = new System.Drawing.Size(70, 22);
        textBox5.TabIndex = 0;
        textBox5.Text = "0";
        textBox2.Location = new System.Drawing.Point(242, 33);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(70, 22);
        textBox2.TabIndex = 1;
        textBox2.Text = "360";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(195, 36);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(43, 14);
        label2.TabIndex = 13;
        label2.Text = "最大值";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(33, 36);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(43, 14);
        label3.TabIndex = 12;
        label3.Text = "最小值";
        textBox6.Location = new System.Drawing.Point(82, 33);
        textBox6.Name = "textBox6";
        textBox6.Size = new System.Drawing.Size(70, 22);
        textBox6.TabIndex = 0;
        textBox6.Text = "0";
        textBox7.Location = new System.Drawing.Point(243, 33);
        textBox7.Name = "textBox7";
        textBox7.Size = new System.Drawing.Size(70, 22);
        textBox7.TabIndex = 1;
        textBox7.Text = "0";
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(182, 36);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(55, 14);
        label8.TabIndex = 15;
        label8.Text = "纵向偏移";
        groupControl1.Controls.Add(textBox4);
        groupControl1.Controls.Add(label5);
        groupControl1.Controls.Add(label6);
        groupControl1.Controls.Add(textBox5);
        groupControl1.Location = new System.Drawing.Point(18, 47);
        groupControl1.Name = "groupControl1";
        groupControl1.Size = new System.Drawing.Size(342, 65);
        groupControl1.TabIndex = 2;
        groupControl1.Text = "绑定变量";
        groupControl2.Controls.Add(textBox2);
        groupControl2.Controls.Add(textBox6);
        groupControl2.Controls.Add(label3);
        groupControl2.Controls.Add(label2);
        groupControl2.Location = new System.Drawing.Point(18, 118);
        groupControl2.Name = "groupControl2";
        groupControl2.Size = new System.Drawing.Size(342, 65);
        groupControl2.TabIndex = 3;
        groupControl2.Text = "对应角度";
        groupControl3.Controls.Add(label8);
        groupControl3.Controls.Add(textBox7);
        groupControl3.Controls.Add(label1);
        groupControl3.Controls.Add(textBox1);
        groupControl3.Location = new System.Drawing.Point(17, 189);
        groupControl3.Name = "groupControl3";
        groupControl3.Size = new System.Drawing.Size(342, 65);
        groupControl3.TabIndex = 4;
        groupControl3.Text = "旋转中心偏移";
        button4.Location = new System.Drawing.Point(275, 16);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(87, 27);
        button4.TabIndex = 1;
        button4.Text = "变量选择";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(Button4_Click);
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(374, 326);
        base.Controls.Add(button4);
        base.Controls.Add(groupControl3);
        base.Controls.Add(groupControl2);
        base.Controls.Add(groupControl1);
        base.Controls.Add(label4);
        base.Controls.Add(textBox3);
        base.Controls.Add(pictureBox1);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "xzForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "旋转";
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
