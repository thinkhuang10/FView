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
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        button3 = new System.Windows.Forms.Button();
        pictureBox1 = new System.Windows.Forms.PictureBox();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(142, 97);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 2;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(235, 97);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 3;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(32, 37);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(47, 14);
        label1.TabIndex = 2;
        label1.Text = "变    量";
        textBox1.HideSelection = false;
        textBox1.Location = new System.Drawing.Point(92, 34);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(116, 22);
        textBox1.TabIndex = 0;
        button3.Location = new System.Drawing.Point(215, 31);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(87, 27);
        button3.TabIndex = 1;
        button3.Text = "变量选择";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        pictureBox1.Location = new System.Drawing.Point(17, 85);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new System.Drawing.Size(300, 1);
        pictureBox1.TabIndex = 6;
        pictureBox1.TabStop = false;
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(334, 136);
        base.Controls.Add(pictureBox1);
        base.Controls.Add(button3);
        base.Controls.Add(textBox1);
        base.Controls.Add(label1);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "zfcscForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "字符串输出";
        base.Load += new System.EventHandler(zfcscForm_Load);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
