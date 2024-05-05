using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class cztzForm : XtraForm
{
    private readonly CGlobal theglobal;

    private Button button1;

    private Button button2;

    public TextBox textBox1;

    public TextBox textBox3;

    public TextBox textBox4;

    public TextBox textBox5;

    public TextBox textBox2;

    private Button button3;

    private PictureBox pictureBox2;

    private GroupControl groupControl2;

    private Label label8;

    private Label label9;

    private GroupControl groupControl1;

    private Label label10;

    private Label label11;

    private Label label12;

    public cztzForm(CGlobal _theglobal)
    {
        InitializeComponent();
        theglobal = _theglobal;
    }

    private void Form4_Load(object sender, EventArgs e)
    {
        try
        {
            textBox3.Text = theglobal.SelectedShapeList[0].cztzbianliang;
            textBox1.Text = (theglobal.SelectedShapeList[0].cztzyidongmax * -1).ToString();
            textBox2.Text = (theglobal.SelectedShapeList[0].cztzyidongmin * -1).ToString();
            textBox5.Text = theglobal.SelectedShapeList[0].cztzzhibianhuamin.ToString();
            textBox4.Text = theglobal.SelectedShapeList[0].cztzzhibianhuamax.ToString();
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

    private void Button1_Click(object sender, EventArgs e)
    {
        if (textBox3.Text == "")
        {
            theglobal.SelectedShapeList[0].cztz = false;
            Close();
            return;
        }
        foreach (string iOItem in CheckIOExists.IOItemList)
        {
            if (!(iOItem == textBox3.Text))
            {
                continue;
            }
            goto IL_0084;
        }
        MessageBox.Show("输入的变量有误,请从新输入");
        return;
    IL_0084:
        try
        {
            if (Convert.ToDouble(textBox1.Text) == Convert.ToDouble(textBox2.Text))
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
            theglobal.SelectedShapeList[0].cztzbianliang = textBox3.Text;
            theglobal.SelectedShapeList[0].cztzyidongmax = Convert.ToInt32(textBox1.Text) * -1;
            theglobal.SelectedShapeList[0].cztzyidongmin = Convert.ToInt32(textBox2.Text) * -1;
            theglobal.SelectedShapeList[0].cztzzhibianhuamin = Convert.ToInt32(textBox5.Text);
            theglobal.SelectedShapeList[0].cztzzhibianhuamax = Convert.ToInt32(textBox4.Text);
            if (!theglobal.SelectedShapeList[0].cztz && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].cztz = true;
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
        IOForm iOForm = new()
        {
            Edit = false
        };
        if (iOForm.ShowDialog() == DialogResult.OK)
        {
            textBox3.Text = iOForm.io;
        }
    }

    private void textBox3_TextChanged(object sender, EventArgs e)
    {
    }

    private void button3_Click(object sender, EventArgs e)
    {
        IOForm iOForm = new()
        {
            Edit = false
        };
        if (iOForm.ShowDialog() == DialogResult.OK)
        {
            textBox3.Text = iOForm.io;
        }
    }

    private void InitializeComponent()
    {
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        textBox1 = new System.Windows.Forms.TextBox();
        textBox3 = new System.Windows.Forms.TextBox();
        textBox4 = new System.Windows.Forms.TextBox();
        textBox5 = new System.Windows.Forms.TextBox();
        textBox2 = new System.Windows.Forms.TextBox();
        button3 = new System.Windows.Forms.Button();
        pictureBox2 = new System.Windows.Forms.PictureBox();
        groupControl2 = new DevExpress.XtraEditors.GroupControl();
        label8 = new System.Windows.Forms.Label();
        label9 = new System.Windows.Forms.Label();
        groupControl1 = new DevExpress.XtraEditors.GroupControl();
        label10 = new System.Windows.Forms.Label();
        label11 = new System.Windows.Forms.Label();
        label12 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
        ((System.ComponentModel.ISupportInitialize)groupControl2).BeginInit();
        groupControl2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
        groupControl1.SuspendLayout();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(182, 217);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 4;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(Button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(275, 217);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 5;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        textBox1.Location = new System.Drawing.Point(255, 33);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(69, 22);
        textBox1.TabIndex = 1;
        textBox1.Text = "100";
        textBox3.Location = new System.Drawing.Point(77, 18);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(189, 22);
        textBox3.TabIndex = 0;
        textBox3.TextChanged += new System.EventHandler(textBox3_TextChanged);
        textBox3.Click += new System.EventHandler(textBox3_Click);
        textBox4.Location = new System.Drawing.Point(254, 34);
        textBox4.Name = "textBox4";
        textBox4.Size = new System.Drawing.Size(69, 22);
        textBox4.TabIndex = 1;
        textBox4.Text = "100";
        textBox5.Location = new System.Drawing.Point(90, 34);
        textBox5.Name = "textBox5";
        textBox5.Size = new System.Drawing.Size(69, 22);
        textBox5.TabIndex = 0;
        textBox5.Text = "0";
        textBox2.Location = new System.Drawing.Point(90, 33);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(69, 22);
        textBox2.TabIndex = 0;
        textBox2.Text = "0";
        button3.Location = new System.Drawing.Point(274, 15);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(87, 27);
        button3.TabIndex = 1;
        button3.Text = "变量选择";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        pictureBox2.Location = new System.Drawing.Point(18, 203);
        pictureBox2.Name = "pictureBox2";
        pictureBox2.Size = new System.Drawing.Size(340, 1);
        pictureBox2.TabIndex = 35;
        pictureBox2.TabStop = false;
        groupControl2.Controls.Add(label8);
        groupControl2.Controls.Add(label9);
        groupControl2.Controls.Add(textBox2);
        groupControl2.Controls.Add(textBox1);
        groupControl2.Location = new System.Drawing.Point(19, 119);
        groupControl2.Name = "groupControl2";
        groupControl2.Size = new System.Drawing.Size(342, 65);
        groupControl2.TabIndex = 3;
        groupControl2.Text = "向上像素变化";
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(204, 36);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(43, 14);
        label8.TabIndex = 15;
        label8.Text = "最大值";
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(43, 36);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(43, 14);
        label9.TabIndex = 15;
        label9.Text = "最小值";
        groupControl1.Controls.Add(label10);
        groupControl1.Controls.Add(label11);
        groupControl1.Controls.Add(textBox5);
        groupControl1.Controls.Add(textBox4);
        groupControl1.Location = new System.Drawing.Point(19, 48);
        groupControl1.Name = "groupControl1";
        groupControl1.Size = new System.Drawing.Size(342, 65);
        groupControl1.TabIndex = 2;
        groupControl1.Text = "值变化";
        label10.AutoSize = true;
        label10.Location = new System.Drawing.Point(207, 37);
        label10.Name = "label10";
        label10.Size = new System.Drawing.Size(43, 14);
        label10.TabIndex = 11;
        label10.Text = "最大值";
        label11.AutoSize = true;
        label11.Location = new System.Drawing.Point(43, 37);
        label11.Name = "label11";
        label11.Size = new System.Drawing.Size(43, 14);
        label11.TabIndex = 10;
        label11.Text = "最小值";
        label12.AutoSize = true;
        label12.Location = new System.Drawing.Point(16, 21);
        label12.Name = "label12";
        label12.Size = new System.Drawing.Size(55, 14);
        label12.TabIndex = 32;
        label12.Text = "绑定变量";
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(374, 256);
        base.Controls.Add(pictureBox2);
        base.Controls.Add(groupControl2);
        base.Controls.Add(groupControl1);
        base.Controls.Add(label12);
        base.Controls.Add(button3);
        base.Controls.Add(textBox3);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "cztzForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "垂直拖拽";
        base.Load += new System.EventHandler(Form4_Load);
        ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
        ((System.ComponentModel.ISupportInitialize)groupControl2).EndInit();
        groupControl2.ResumeLayout(false);
        groupControl2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
        groupControl1.ResumeLayout(false);
        groupControl1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
