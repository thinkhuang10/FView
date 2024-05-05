using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class txycForm : XtraForm
{
    private readonly CGlobal theglobal;

    private Button button1;

    private Button button2;

    private Label label1;

    private TextBox textBox1;

    private CheckBox checkBox1;

    private PictureBox pictureBox1;

    private Button button4;

    private Label label2;

    public txycForm(CGlobal _theglobal)
    {
        InitializeComponent();
        theglobal = _theglobal;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox1.Text == "")
        {
            theglobal.SelectedShapeList[0].txyc = false;
            Close();
            return;
        }
        try
        {
            theglobal.SelectedShapeList[0].txycbianliang = textBox1.Text;
            theglobal.SelectedShapeList[0].txycnotbianliang = checkBox1.Checked;
            if (!theglobal.SelectedShapeList[0].txyc && MessageBox.Show("是否激活相关配置?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                theglobal.SelectedShapeList[0].txyc = true;
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

    private void textBox1_Click(object sender, EventArgs e)
    {
    }

    private void txycForm_Load(object sender, EventArgs e)
    {
        try
        {
            textBox1.Text = theglobal.SelectedShapeList[0].txycbianliang;
            checkBox1.Checked = theglobal.SelectedShapeList[0].txycnotbianliang;
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
        checkBox1 = new System.Windows.Forms.CheckBox();
        pictureBox1 = new System.Windows.Forms.PictureBox();
        button4 = new System.Windows.Forms.Button();
        label2 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(118, 92);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(87, 27);
        button1.TabIndex = 3;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        button2.Location = new System.Drawing.Point(211, 92);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(87, 27);
        button2.TabIndex = 4;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(29, 23);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(55, 14);
        label1.TabIndex = 2;
        label1.Text = "绑定变量";
        textBox1.HideSelection = false;
        textBox1.Location = new System.Drawing.Point(90, 20);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(98, 22);
        textBox1.TabIndex = 0;
        checkBox1.AutoSize = true;
        checkBox1.Location = new System.Drawing.Point(90, 48);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new System.Drawing.Size(50, 18);
        checkBox1.TabIndex = 2;
        checkBox1.Text = "取反";
        checkBox1.UseVisualStyleBackColor = true;
        pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        pictureBox1.Location = new System.Drawing.Point(20, 81);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new System.Drawing.Size(270, 1);
        pictureBox1.TabIndex = 9;
        pictureBox1.TabStop = false;
        button4.Location = new System.Drawing.Point(194, 17);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(87, 27);
        button4.TabIndex = 1;
        button4.Text = "变量选择";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(button4_Click);
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(29, 49);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(55, 14);
        label2.TabIndex = 2;
        label2.Text = "是否取反";
        base.AcceptButton = button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = button2;
        base.ClientSize = new System.Drawing.Size(310, 136);
        base.Controls.Add(button4);
        base.Controls.Add(pictureBox1);
        base.Controls.Add(checkBox1);
        base.Controls.Add(textBox1);
        base.Controls.Add(label2);
        base.Controls.Add(label1);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "txycForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "图形隐藏";
        base.Load += new System.EventHandler(txycForm_Load);
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
