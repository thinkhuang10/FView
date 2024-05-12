using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class DOForm : XtraForm
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

    public DOForm(CGlobal _theglobal)
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
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        textBox1 = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        textBox3 = new System.Windows.Forms.TextBox();
        textBox4 = new System.Windows.Forms.TextBox();
        pictureBox1 = new System.Windows.Forms.PictureBox();
        button3 = new System.Windows.Forms.Button();
        label4 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(167, 117);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 4;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(260, 117);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 5;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        textBox1.HideSelection = false;
        textBox1.Location = new System.Drawing.Point(80, 25);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(158, 22);
        textBox1.TabIndex = 0;
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(19, 30);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(55, 14);
        label1.TabIndex = 3;
        label1.Text = "变      量";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(218, 66);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(19, 14);
        label2.TabIndex = 4;
        label2.Text = "关";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(77, 65);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(19, 14);
        label3.TabIndex = 5;
        label3.Text = "开";
        textBox3.Location = new System.Drawing.Point(102, 62);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(80, 22);
        textBox3.TabIndex = 2;
        textBox3.Text = "打开";
        textBox4.Location = new System.Drawing.Point(244, 62);
        textBox4.Name = "textBox4";
        textBox4.Size = new System.Drawing.Size(87, 22);
        textBox4.TabIndex = 3;
        textBox4.Text = "关闭";
        pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        pictureBox1.Location = new System.Drawing.Point(12, 106);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new System.Drawing.Size(335, 1);
        pictureBox1.TabIndex = 9;
        pictureBox1.TabStop = false;
        button3.Location = new System.Drawing.Point(244, 24);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(87, 27);
        button3.TabIndex = 1;
        button3.Text = "变量选择";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(19, 65);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(55, 14);
        label4.TabIndex = 12;
        label4.Text = "提示信息";
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(359, 156);
        base.Controls.Add(label4);
        base.Controls.Add(button3);
        base.Controls.Add(pictureBox1);
        base.Controls.Add(textBox4);
        base.Controls.Add(textBox3);
        base.Controls.Add(label3);
        base.Controls.Add(label2);
        base.Controls.Add(label1);
        base.Controls.Add(textBox1);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "doForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "数字量输出";
        base.Load += new System.EventHandler(doForm_Load);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
