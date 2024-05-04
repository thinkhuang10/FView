using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class kdbhForm : XtraForm
{
    private readonly CGlobal theglobal;

    public TextBox textBox4;

    public TextBox textBox5;

    public TextBox textBox3;

    public TextBox textBox1;

    private Button button2;

    private Button button1;

    private Label label2;

    public System.Windows.Forms.ComboBox comboBox1;

    public TextBox textBox2;

    private GroupControl groupControl3;

    private GroupControl groupControl2;

    private Label label9;

    private Label label10;

    private GroupControl groupControl1;

    private Label label11;

    private Label label12;

    private Label label13;

    private PictureBox pictureBox2;

    private Button button4;

    public kdbhForm(CGlobal _theglobal)
    {
        theglobal = _theglobal;
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox3.Text == "")
        {
            theglobal.SelectedShapeList[0].kdbh = false;
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
        catch
        {
            MessageBox.Show("数值输入框内存在不可识别数值,请从新输入");
            return;
        }
        try
        {
            theglobal.SelectedShapeList[0].kdbhbianliang = textBox3.Text;
            theglobal.SelectedShapeList[0].kdbhxiangsumax = Convert.ToInt32(textBox1.Text);
            theglobal.SelectedShapeList[0].kdbhxiangsumin = Convert.ToInt32(textBox2.Text);
            theglobal.SelectedShapeList[0].kdbhzhimin = Convert.ToInt32(textBox5.Text);
            theglobal.SelectedShapeList[0].kdbhzhimax = Convert.ToInt32(textBox4.Text);
            theglobal.SelectedShapeList[0].kdbhcankao = comboBox1.SelectedIndex + 1;
            if (!theglobal.SelectedShapeList[0].kdbh && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].kdbh = true;
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

    private void kdbhForm_Load(object sender, EventArgs e)
    {
        try
        {
            textBox3.Text = theglobal.SelectedShapeList[0].kdbhbianliang;
            textBox1.Text = theglobal.SelectedShapeList[0].kdbhxiangsumax.ToString();
            textBox2.Text = theglobal.SelectedShapeList[0].kdbhxiangsumin.ToString();
            textBox5.Text = theglobal.SelectedShapeList[0].kdbhzhimin.ToString();
            textBox4.Text = theglobal.SelectedShapeList[0].kdbhzhimax.ToString();
            comboBox1.SelectedIndex = theglobal.SelectedShapeList[0].kdbhcankao - 1;
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
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.textBox5 = new System.Windows.Forms.TextBox();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.button2 = new System.Windows.Forms.Button();
        this.button1 = new System.Windows.Forms.Button();
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.label2 = new System.Windows.Forms.Label();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
        this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
        this.label9 = new System.Windows.Forms.Label();
        this.label10 = new System.Windows.Forms.Label();
        this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
        this.label11 = new System.Windows.Forms.Label();
        this.label12 = new System.Windows.Forms.Label();
        this.label13 = new System.Windows.Forms.Label();
        this.pictureBox2 = new System.Windows.Forms.PictureBox();
        this.button4 = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)this.groupControl3).BeginInit();
        this.groupControl3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl2).BeginInit();
        this.groupControl2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl1).BeginInit();
        this.groupControl1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
        base.SuspendLayout();
        this.textBox4.Location = new System.Drawing.Point(242, 34);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(70, 22);
        this.textBox4.TabIndex = 1;
        this.textBox4.Text = "100";
        this.textBox5.Location = new System.Drawing.Point(82, 34);
        this.textBox5.Name = "textBox5";
        this.textBox5.Size = new System.Drawing.Size(70, 22);
        this.textBox5.TabIndex = 0;
        this.textBox5.Text = "0";
        this.textBox3.HideSelection = false;
        this.textBox3.Location = new System.Drawing.Point(77, 18);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(204, 22);
        this.textBox3.TabIndex = 0;
        this.textBox1.Location = new System.Drawing.Point(242, 33);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(70, 22);
        this.textBox1.TabIndex = 1;
        this.textBox1.Text = "100";
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(275, 287);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 6;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.button1.Location = new System.Drawing.Point(182, 287);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 5;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Items.AddRange(new object[2] { "左", "右" });
        this.comboBox1.Location = new System.Drawing.Point(139, 33);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(98, 22);
        this.comboBox1.TabIndex = 0;
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(66, 36);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(67, 14);
        this.label2.TabIndex = 13;
        this.label2.Text = "变化基准点";
        this.textBox2.Location = new System.Drawing.Point(82, 33);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(70, 22);
        this.textBox2.TabIndex = 0;
        this.textBox2.Text = "0";
        this.groupControl3.Controls.Add(this.comboBox1);
        this.groupControl3.Controls.Add(this.label2);
        this.groupControl3.Location = new System.Drawing.Point(18, 188);
        this.groupControl3.Name = "groupControl3";
        this.groupControl3.Size = new System.Drawing.Size(342, 65);
        this.groupControl3.TabIndex = 4;
        this.groupControl3.Text = "变化基准点";
        this.groupControl2.Controls.Add(this.label9);
        this.groupControl2.Controls.Add(this.label10);
        this.groupControl2.Controls.Add(this.textBox2);
        this.groupControl2.Controls.Add(this.textBox1);
        this.groupControl2.Location = new System.Drawing.Point(19, 117);
        this.groupControl2.Name = "groupControl2";
        this.groupControl2.Size = new System.Drawing.Size(342, 65);
        this.groupControl2.TabIndex = 3;
        this.groupControl2.Text = "对应宽度";
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(33, 36);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(43, 14);
        this.label9.TabIndex = 12;
        this.label9.Text = "最小值";
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(193, 36);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(43, 14);
        this.label10.TabIndex = 13;
        this.label10.Text = "最大值";
        this.groupControl1.Controls.Add(this.label11);
        this.groupControl1.Controls.Add(this.label12);
        this.groupControl1.Controls.Add(this.textBox5);
        this.groupControl1.Controls.Add(this.textBox4);
        this.groupControl1.Location = new System.Drawing.Point(19, 46);
        this.groupControl1.Name = "groupControl1";
        this.groupControl1.Size = new System.Drawing.Size(342, 65);
        this.groupControl1.TabIndex = 2;
        this.groupControl1.Text = "绑定变量";
        this.label11.AutoSize = true;
        this.label11.Location = new System.Drawing.Point(193, 37);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(43, 14);
        this.label11.TabIndex = 11;
        this.label11.Text = "最大值";
        this.label12.AutoSize = true;
        this.label12.Location = new System.Drawing.Point(33, 37);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(43, 14);
        this.label12.TabIndex = 10;
        this.label12.Text = "最小值";
        this.label13.AutoSize = true;
        this.label13.Location = new System.Drawing.Point(16, 21);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(55, 14);
        this.label13.TabIndex = 22;
        this.label13.Text = "绑定变量";
        this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBox2.Location = new System.Drawing.Point(18, 272);
        this.pictureBox2.Name = "pictureBox2";
        this.pictureBox2.Size = new System.Drawing.Size(340, 1);
        this.pictureBox2.TabIndex = 21;
        this.pictureBox2.TabStop = false;
        this.button4.Location = new System.Drawing.Point(287, 17);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(75, 23);
        this.button4.TabIndex = 1;
        this.button4.Text = "选择变量";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(button4_Click);
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(374, 326);
        base.Controls.Add(this.button4);
        base.Controls.Add(this.groupControl3);
        base.Controls.Add(this.groupControl2);
        base.Controls.Add(this.groupControl1);
        base.Controls.Add(this.label13);
        base.Controls.Add(this.pictureBox2);
        base.Controls.Add(this.textBox3);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "kdbhForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "宽度变化";
        base.Load += new System.EventHandler(kdbhForm_Load);
        ((System.ComponentModel.ISupportInitialize)this.groupControl3).EndInit();
        this.groupControl3.ResumeLayout(false);
        this.groupControl3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl2).EndInit();
        this.groupControl2.ResumeLayout(false);
        this.groupControl2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl1).EndInit();
        this.groupControl1.ResumeLayout(false);
        this.groupControl1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
