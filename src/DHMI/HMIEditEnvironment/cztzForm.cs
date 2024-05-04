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

    private void button1_Click(object sender, EventArgs e)
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
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.textBox5 = new System.Windows.Forms.TextBox();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.button3 = new System.Windows.Forms.Button();
        this.pictureBox2 = new System.Windows.Forms.PictureBox();
        this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
        this.label8 = new System.Windows.Forms.Label();
        this.label9 = new System.Windows.Forms.Label();
        this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
        this.label10 = new System.Windows.Forms.Label();
        this.label11 = new System.Windows.Forms.Label();
        this.label12 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.groupControl2).BeginInit();
        this.groupControl2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.groupControl1).BeginInit();
        this.groupControl1.SuspendLayout();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(182, 217);
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
        this.textBox1.Location = new System.Drawing.Point(255, 33);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(69, 22);
        this.textBox1.TabIndex = 1;
        this.textBox1.Text = "100";
        this.textBox3.Location = new System.Drawing.Point(77, 18);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(189, 22);
        this.textBox3.TabIndex = 0;
        this.textBox3.TextChanged += new System.EventHandler(textBox3_TextChanged);
        this.textBox3.Click += new System.EventHandler(textBox3_Click);
        this.textBox4.Location = new System.Drawing.Point(254, 34);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(69, 22);
        this.textBox4.TabIndex = 1;
        this.textBox4.Text = "100";
        this.textBox5.Location = new System.Drawing.Point(90, 34);
        this.textBox5.Name = "textBox5";
        this.textBox5.Size = new System.Drawing.Size(69, 22);
        this.textBox5.TabIndex = 0;
        this.textBox5.Text = "0";
        this.textBox2.Location = new System.Drawing.Point(90, 33);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(69, 22);
        this.textBox2.TabIndex = 0;
        this.textBox2.Text = "0";
        this.button3.Location = new System.Drawing.Point(274, 15);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(87, 27);
        this.button3.TabIndex = 1;
        this.button3.Text = "变量选择";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBox2.Location = new System.Drawing.Point(18, 203);
        this.pictureBox2.Name = "pictureBox2";
        this.pictureBox2.Size = new System.Drawing.Size(340, 1);
        this.pictureBox2.TabIndex = 35;
        this.pictureBox2.TabStop = false;
        this.groupControl2.Controls.Add(this.label8);
        this.groupControl2.Controls.Add(this.label9);
        this.groupControl2.Controls.Add(this.textBox2);
        this.groupControl2.Controls.Add(this.textBox1);
        this.groupControl2.Location = new System.Drawing.Point(19, 119);
        this.groupControl2.Name = "groupControl2";
        this.groupControl2.Size = new System.Drawing.Size(342, 65);
        this.groupControl2.TabIndex = 3;
        this.groupControl2.Text = "向上像素变化";
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(204, 36);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(43, 14);
        this.label8.TabIndex = 15;
        this.label8.Text = "最大值";
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(43, 36);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(43, 14);
        this.label9.TabIndex = 15;
        this.label9.Text = "最小值";
        this.groupControl1.Controls.Add(this.label10);
        this.groupControl1.Controls.Add(this.label11);
        this.groupControl1.Controls.Add(this.textBox5);
        this.groupControl1.Controls.Add(this.textBox4);
        this.groupControl1.Location = new System.Drawing.Point(19, 48);
        this.groupControl1.Name = "groupControl1";
        this.groupControl1.Size = new System.Drawing.Size(342, 65);
        this.groupControl1.TabIndex = 2;
        this.groupControl1.Text = "值变化";
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(207, 37);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(43, 14);
        this.label10.TabIndex = 11;
        this.label10.Text = "最大值";
        this.label11.AutoSize = true;
        this.label11.Location = new System.Drawing.Point(43, 37);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(43, 14);
        this.label11.TabIndex = 10;
        this.label11.Text = "最小值";
        this.label12.AutoSize = true;
        this.label12.Location = new System.Drawing.Point(16, 21);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(55, 14);
        this.label12.TabIndex = 32;
        this.label12.Text = "绑定变量";
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(374, 256);
        base.Controls.Add(this.pictureBox2);
        base.Controls.Add(this.groupControl2);
        base.Controls.Add(this.groupControl1);
        base.Controls.Add(this.label12);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.textBox3);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "cztzForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "垂直拖拽";
        base.Load += new System.EventHandler(Form4_Load);
        ((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
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
