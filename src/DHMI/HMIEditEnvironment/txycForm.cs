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
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.checkBox1 = new System.Windows.Forms.CheckBox();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.button4 = new System.Windows.Forms.Button();
        this.label2 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(118, 92);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 27);
        this.button1.TabIndex = 3;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Location = new System.Drawing.Point(211, 92);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(87, 27);
        this.button2.TabIndex = 4;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(29, 23);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(55, 14);
        this.label1.TabIndex = 2;
        this.label1.Text = "绑定变量";
        this.textBox1.HideSelection = false;
        this.textBox1.Location = new System.Drawing.Point(90, 20);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(98, 22);
        this.textBox1.TabIndex = 0;
        this.checkBox1.AutoSize = true;
        this.checkBox1.Location = new System.Drawing.Point(90, 48);
        this.checkBox1.Name = "checkBox1";
        this.checkBox1.Size = new System.Drawing.Size(50, 18);
        this.checkBox1.TabIndex = 2;
        this.checkBox1.Text = "取反";
        this.checkBox1.UseVisualStyleBackColor = true;
        this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBox1.Location = new System.Drawing.Point(20, 81);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(270, 1);
        this.pictureBox1.TabIndex = 9;
        this.pictureBox1.TabStop = false;
        this.button4.Location = new System.Drawing.Point(194, 17);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(87, 27);
        this.button4.TabIndex = 1;
        this.button4.Text = "变量选择";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(button4_Click);
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(29, 49);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(55, 14);
        this.label2.TabIndex = 2;
        this.label2.Text = "是否取反";
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new System.Drawing.Size(310, 136);
        base.Controls.Add(this.button4);
        base.Controls.Add(this.pictureBox1);
        base.Controls.Add(this.checkBox1);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "txycForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "图形隐藏";
        base.Load += new System.EventHandler(txycForm_Load);
        ((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
