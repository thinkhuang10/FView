using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class zfcscForm : XtraForm
{
    private readonly CGlobal theglobal;

    private Button button1;

    private Button button2;

    private Label label1;

    private TextBox textBox1;

    private Button button3;

    private PictureBox pictureBox1;


    public zfcscForm(CGlobal _theglobal)
    {
        InitializeComponent();
        theglobal = _theglobal;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox1.Text == "")
        {
            theglobal.SelectedShapeList[0].zfcsc = false;
            Close();
            return;
        }
        try
        {
            theglobal.SelectedShapeList[0].zfcscbianliang = textBox1.Text;
            if (!theglobal.SelectedShapeList[0].zfcsc && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].zfcsc = true;
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
        Close();
    }

    private void zfcscForm_Load(object sender, EventArgs e)
    {
        try
        {
            textBox1.Text = theglobal.SelectedShapeList[0].zfcscbianliang;
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
        this.label1 = new System.Windows.Forms.Label();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.button3 = new System.Windows.Forms.Button();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(142, 97);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 2;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(235, 97);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 3;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(32, 37);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(47, 14);
        this.label1.TabIndex = 2;
        this.label1.Text = "变    量";
        this.textBox1.HideSelection = false;
        this.textBox1.Location = new System.Drawing.Point(92, 34);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(116, 22);
        this.textBox1.TabIndex = 0;
        this.button3.Location = new System.Drawing.Point(215, 31);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(87, 27);
        this.button3.TabIndex = 1;
        this.button3.Text = "变量选择";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBox1.Location = new System.Drawing.Point(17, 85);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(300, 1);
        this.pictureBox1.TabIndex = 6;
        this.pictureBox1.TabStop = false;
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(334, 136);
        base.Controls.Add(this.pictureBox1);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "zfcscForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "字符串输出";
        base.Load += new System.EventHandler(zfcscForm_Load);
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
