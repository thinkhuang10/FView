using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class spydForm : XtraForm
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

    private Button button3;

    public TextBox textBox2;

    private GroupControl groupControl2;

    private Label label7;

    private Label label3;

    private GroupControl groupControl1;

    private Label label8;

    private Label label9;

    public spydForm(CGlobal _theglobal)
    {
        InitializeComponent();
        theglobal = _theglobal;
    }

    private void Form4_Load(object sender, EventArgs e)
    {
        try
        {
            textBox3.Text = theglobal.SelectedShapeList[0].spydbianliang;
            textBox1.Text = theglobal.SelectedShapeList[0].spydxiangsumax.ToString();
            textBox2.Text = theglobal.SelectedShapeList[0].spydxiangsumin.ToString();
            textBox5.Text = theglobal.SelectedShapeList[0].spydzhimin.ToString();
            textBox4.Text = theglobal.SelectedShapeList[0].spydzhimax.ToString();
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
            theglobal.SelectedShapeList[0].spyd = false;
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
            base.DialogResult = DialogResult.OK;
            theglobal.SelectedShapeList[0].spydbianliang = textBox3.Text;
            theglobal.SelectedShapeList[0].spydxiangsumax = Convert.ToInt32(textBox1.Text);
            theglobal.SelectedShapeList[0].spydxiangsumin = Convert.ToInt32(textBox2.Text);
            theglobal.SelectedShapeList[0].spydzhimin = Convert.ToInt32(textBox5.Text);
            theglobal.SelectedShapeList[0].spydzhimax = Convert.ToInt32(textBox4.Text);
            if (!theglobal.SelectedShapeList[0].spyd && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].spyd = true;
            }
        }
        catch (Exception)
        {
        }
        base.DialogResult = DialogResult.OK;
        Close();
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
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.textBox5 = new System.Windows.Forms.TextBox();
        this.button3 = new System.Windows.Forms.Button();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
        this.label7 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
        this.label8 = new System.Windows.Forms.Label();
        this.label9 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.groupControl2).BeginInit();
        this.groupControl2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl1).BeginInit();
        this.groupControl1.SuspendLayout();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(178, 217);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 4;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(275, 217);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 5;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBox1.Location = new System.Drawing.Point(17, 204);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(340, 1);
        this.pictureBox1.TabIndex = 2;
        this.pictureBox1.TabStop = false;
        this.textBox1.Location = new System.Drawing.Point(254, 33);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(69, 22);
        this.textBox1.TabIndex = 1;
        this.textBox1.Text = "100";
        this.textBox3.HideSelection = false;
        this.textBox3.Location = new System.Drawing.Point(76, 19);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(189, 22);
        this.textBox3.TabIndex = 0;
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(15, 22);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(55, 14);
        this.label4.TabIndex = 9;
        this.label4.Text = "绑定变量";
        this.textBox4.Location = new System.Drawing.Point(254, 34);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(69, 22);
        this.textBox4.TabIndex = 1;
        this.textBox4.Text = "100";
        this.textBox5.Location = new System.Drawing.Point(89, 34);
        this.textBox5.Name = "textBox5";
        this.textBox5.Size = new System.Drawing.Size(69, 22);
        this.textBox5.TabIndex = 0;
        this.textBox5.Text = "0";
        this.button3.Location = new System.Drawing.Point(274, 16);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(87, 27);
        this.button3.TabIndex = 1;
        this.button3.Text = "变量选择";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.textBox2.Location = new System.Drawing.Point(89, 33);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(69, 22);
        this.textBox2.TabIndex = 0;
        this.textBox2.Text = "0";
        this.groupControl2.Controls.Add(this.label7);
        this.groupControl2.Controls.Add(this.label3);
        this.groupControl2.Controls.Add(this.textBox2);
        this.groupControl2.Controls.Add(this.textBox1);
        this.groupControl2.Location = new System.Drawing.Point(18, 120);
        this.groupControl2.Name = "groupControl2";
        this.groupControl2.Size = new System.Drawing.Size(342, 65);
        this.groupControl2.TabIndex = 3;
        this.groupControl2.Text = "向右移动像素";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(205, 36);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(43, 14);
        this.label7.TabIndex = 15;
        this.label7.Text = "最大值";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(40, 36);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(43, 14);
        this.label3.TabIndex = 15;
        this.label3.Text = "最小值";
        this.groupControl1.Controls.Add(this.label8);
        this.groupControl1.Controls.Add(this.label9);
        this.groupControl1.Controls.Add(this.textBox4);
        this.groupControl1.Controls.Add(this.textBox5);
        this.groupControl1.Location = new System.Drawing.Point(18, 49);
        this.groupControl1.Name = "groupControl1";
        this.groupControl1.Size = new System.Drawing.Size(342, 65);
        this.groupControl1.TabIndex = 2;
        this.groupControl1.Text = "绑定变量";
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(205, 37);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(43, 14);
        this.label8.TabIndex = 11;
        this.label8.Text = "最大值";
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(43, 37);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(43, 14);
        this.label9.TabIndex = 10;
        this.label9.Text = "最小值";
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(374, 256);
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
        base.Name = "spydForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "水平移动";
        base.Load += new System.EventHandler(Form4_Load);
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.groupControl2).EndInit();
        this.groupControl2.ResumeLayout(false);
        this.groupControl2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl1).EndInit();
        this.groupControl1.ResumeLayout(false);
        this.groupControl1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
