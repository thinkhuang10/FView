using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class doForm : XtraForm
{
    private readonly CGlobal theglobal;

    private Button button1;

    private Button button2;

    private TextBox textBox1;

    private Label label1;

    private Label label2;

    private Label label3;

    private TextBox textBox3;

    private TextBox textBox4;

    private PictureBox pictureBox1;

    private Button button3;

    private Label label4;

    public doForm(CGlobal _theglobal)
    {
        theglobal = _theglobal;
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox1.Text == "")
        {
            theglobal.SelectedShapeList[0].doo = false;
            Close();
            return;
        }
        try
        {
            theglobal.SelectedShapeList[0].dobianlaing = textBox1.Text;
            theglobal.SelectedShapeList[0].dotishioff = textBox4.Text;
            theglobal.SelectedShapeList[0].dotishion = textBox3.Text;
            if (!theglobal.SelectedShapeList[0].doo && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].doo = true;
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

    private void doForm_Load(object sender, EventArgs e)
    {
        try
        {
            textBox1.Text = theglobal.SelectedShapeList[0].dobianlaing;
            textBox4.Text = theglobal.SelectedShapeList[0].dotishioff;
            textBox3.Text = theglobal.SelectedShapeList[0].dotishion;
        }
        catch (Exception)
        {
        }
    }

    private void button3_Click(object sender, EventArgs e)
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

    private void InitializeComponent()
    {
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.button3 = new System.Windows.Forms.Button();
        this.label4 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(167, 117);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 4;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(260, 117);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 5;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.textBox1.HideSelection = false;
        this.textBox1.Location = new System.Drawing.Point(80, 25);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(158, 22);
        this.textBox1.TabIndex = 0;
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(19, 30);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(55, 14);
        this.label1.TabIndex = 3;
        this.label1.Text = "变      量";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(218, 66);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(19, 14);
        this.label2.TabIndex = 4;
        this.label2.Text = "关";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(77, 65);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(19, 14);
        this.label3.TabIndex = 5;
        this.label3.Text = "开";
        this.textBox3.Location = new System.Drawing.Point(102, 62);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(80, 22);
        this.textBox3.TabIndex = 2;
        this.textBox3.Text = "打开";
        this.textBox4.Location = new System.Drawing.Point(244, 62);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(87, 22);
        this.textBox4.TabIndex = 3;
        this.textBox4.Text = "关闭";
        this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBox1.Location = new System.Drawing.Point(12, 106);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(335, 1);
        this.pictureBox1.TabIndex = 9;
        this.pictureBox1.TabStop = false;
        this.button3.Location = new System.Drawing.Point(244, 24);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(87, 27);
        this.button3.TabIndex = 1;
        this.button3.Text = "变量选择";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(19, 65);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(55, 14);
        this.label4.TabIndex = 12;
        this.label4.Text = "提示信息";
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(359, 156);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.pictureBox1);
        base.Controls.Add(this.textBox4);
        base.Controls.Add(this.textBox3);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "doForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "数字量输出";
        base.Load += new System.EventHandler(doForm_Load);
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
